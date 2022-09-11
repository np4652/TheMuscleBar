using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using TheMuscleBar.AppCode.CustomAttributes;
using TheMuscleBar.AppCode.Helper;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authorization;
using ApiRequestUtility;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.PhonePay;
using static TheMuscleBar.AppCode.PhonePay.WebScrapModel;
using Hangfire;
using Newtonsoft.Json;
using TheMuscleBar.AppCode.Const;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace TheMuscleBar.Controllers
{
    //[Authorize(Roles = "1")]
    [CanBePaused]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TaskController : Controller
    {
        private readonly ITransactionService _transactionService;
        private IPhonePayService _phonePayService;
        private readonly string Connectionstring;
        private IAPILogin _apiLogin;
        private readonly ILogger<TaskController> _logger;
        public TaskController(IConnectionString connectionString, ITransactionService transactionService, IPhonePayService phonePayService, IAPILogin apiLogin, ILogger<TaskController> logger)
        {
            Connectionstring = connectionString.connectionString;
            _transactionService = transactionService;
            _phonePayService = phonePayService;
            _apiLogin = apiLogin;
            _logger = logger;
        }

        public IActionResult PauseTask()
        {
            var storage = new SqlServerStorage(Connectionstring);
            using (var connection = storage.GetConnection())
            {
                connection.Pause(typeof(TaskController));
            }
            return Json("Task paused");
        }

        public IActionResult ResumeTask()
        {
            var storage = new SqlServerStorage(Connectionstring);
            using (var connection = storage.GetConnection())
            {
                connection.Resume(typeof(TaskController));
            }
            return Json("Task Resumed");
        }
        [AllowAnonymous]
        public IActionResult ScheduleStatusCheck()
        {
            try
            {
                string cronExpressionEvery_10_Sec = "0/10 * * * * *";
                string cronExpressionEvery_10_Min = "*/10 * * * *";
                RecurringJob.AddOrUpdate(() => StatusCheckJob(), cronExpressionEvery_10_Sec);
                return Json("Scheduled");
            }
            catch (Exception ex)
            {
                return Json("Something went wrong");
            }

        }
        [AllowAnonymous]
        public void StatusCheckJob()
        {
            var result = _transactionService.GetAllTransactionsAsync().Result;
            foreach (var item in result)
            {
                StatusCheckAsync(item);
            }
        }

        [AllowAnonymous]
        public async Task StatusCheckAsync(StatusCheckJobRequest transction)
        {
            bool flag = true;
            string request = string.Empty;
            var response = _transactionService.GetTransactionStatus(transction.RequestedId).Result;
            try
            {
                var TxnResult = new Response<TxnResult>();
                if (response.StatusCode == ResponseStatus.Success && response.Status == "P")
                {
                    flag = false;
                    var upiDetail = _transactionService.GetUPISettingByTID(response.TransactionId).Result;
                    if (!string.IsNullOrEmpty(upiDetail.MerchantId))
                    {
                        DateTime fromDate = DateTime.Now;
                        DateTime.TryParseExact(upiDetail.EntryOn, VariableFormats.dd_MMM_yyyy_HH_mm_ss_fff, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
                        string tarnsactionId = AppUtility.O.CreateTransactionId(upiDetail.TransactionId);
                        var req = new WebScrapModel.TxnDataRequest
                        {
                            authtoken = upiDetail.AuthToken,
                            devicefingerprint = upiDetail.DeviceFingerprint,
                            token = upiDetail.Refreshtoken,
                            uuid = upiDetail.UUID,
                            merchantid = upiDetail.MerchantId,
                            fromdate = fromDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss"),
                            todate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        request = JsonConvert.SerializeObject(req);
                        var txnData = _phonePayService.TxnData(req).Result;
                        _apiLogin.SaveLog(request, JsonConvert.SerializeObject(txnData), "TxnData", transction.TransactionId.ToString(), false, "FromJob");
                        if (txnData.Resp_code.Equals("RCS", StringComparison.OrdinalIgnoreCase))
                        {
                            string remark = string.Empty;
                            bool isTerminated = false;
                            TxnResult.Result = txnData.data.data.results?.Where(x => x.merchantTransactionId == tarnsactionId).FirstOrDefault();
                            if (TxnResult.Result == null)
                            {
                                TxnResult.Result = new TxnResult();
                                TimeSpan tSpan = DateTime.Now - fromDate;
                                if(tSpan.Minutes > 30)
                                {
                                    TxnResult.Result.payResponseCode="FAILED";
                                    remark="Transaction Not Completed By Users";
                                    isTerminated = true;
                                }
                            }
                            if (TxnResult.Result.payResponseCode?.ToLower() != "pending")
                            {
                                if (_transactionService.PaymentState(TxnResult.Result.paymentState)=='F' && !isTerminated)
                                {
                                    TxnResult.Result.utr = TxnResult.Result.errorMessage ?? TxnResult.Result.payResponseCode;
                                    TxnResult.Result.payResponseCode="FAILED";
                                }
                                if (Convert.ToDecimal(TxnResult.Result.amount*0.01)!=upiDetail.Amount && !isTerminated)
                                {
                                    TxnResult.Result.payResponseCode = "FAILED";
                                    remark = "Amount miss match";
                                }
                                /* Ensure paymentApp and customerDetails is not null */
                                TxnResult.Result.paymentApp = TxnResult.Result.paymentApp ?? new PaymentApp();
                                TxnResult.Result.customerDetails = TxnResult.Result.customerDetails ?? new CustomerDetails();
                                /* End */
                                var updateStatusRes = _transactionService.UpdateTransactionStatus(new UpdateTransactionStatusRequest
                                {
                                    TID = response.TransactionId,
                                    Status = _transactionService.PaymentState(TxnResult.Result.payResponseCode),
                                    TxnObject = JsonConvert.SerializeObject(TxnResult.Result),
                                    UTR = TxnResult.Result.utr,
                                    Remark = remark,
                                    PaymentApp = TxnResult.Result.paymentApp.displayText,
                                    PayerName = TxnResult.Result.customerDetails.userName,
                                    PayerPhone = TxnResult.Result.customerDetails.phoneNumber,
                                    PayerVPA = TxnResult.Result.vpa
                                }).Result;
                                TxnResult.StatusCode = updateStatusRes.StatusCode;
                                TxnResult.ResponseText = updateStatusRes.ResponseText;
                                response.Status = TxnResult.Result.payResponseCode;
                                response.UTR = TxnResult.Result.utr;
                                response.PaymentApp = TxnResult.Result.paymentApp.displayText;
                                response.PayerVPA = TxnResult.Result.vpa;
                                response.PayerName = TxnResult.Result.customerDetails.userName;
                                response.PayerPhone = TxnResult.Result.customerDetails.phoneNumber;
                                response.Remark = remark;
                                flag = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            if (flag)
            {
                var serverhookUrl = $"{transction.ServerHookURL}?tr={AppUtility.O.CreateTransactionId(transction.TransactionId)}&tid={transction.TransactionId}&amount={transction.Amount}&requestedId={transction.RequestedId}&status={response.Status}&payerVPA={response.PayerVPA}&payerName={response.PayerName}&payerPhone={response.PayerPhone}&paymentApp={response.PaymentApp}&utr={response.UTR}";
                var _res = string.Empty;
                try
                {
                    _res = AppWebRequest.O.CallUsingHttpWebRequest_GETAsync(serverhookUrl, 5).Result;
                }
                catch (Exception ex)
                {
                    _res = ex.Message + "_" +_res;
                }
                _transactionService.CreateCallbackLog(transction.RequestedId, serverhookUrl, _res, HookType.ServerHook);
                _transactionService.UpdateTransServerHook(transction.TransactionId);
            }
            if (string.IsNullOrEmpty(request) || string.IsNullOrWhiteSpace(request))
                request = JsonConvert.SerializeObject(new { RequestedId = transction.RequestedId });
            /* Go to update StatusCheck count*/
            _transactionService.UpdateTransStatusCheck(transction.TransactionId);
            /* end */

            _apiLogin.SaveLog(request, JsonConvert.SerializeObject(response), nameof(this.StatusCheckAsync), transction.TransactionId.ToString(), true, "FromJob");
        }

        public async Task<IActionResult> ExternalStatusCheck()
        {
            var result = await _transactionService.GetAllTransactionsAsync();
            foreach (var item in result)
            {
                StatusCheckAsync(item);
            }
            return Ok("Job Scheduled");
        }
    }
}