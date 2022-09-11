using ApiRequestUtility;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Const;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Helper;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.PhonePay;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using static TheMuscleBar.AppCode.PhonePay.WebScrapModel;

namespace TheMuscleBar.AppCode.Reops
{
    public class TransactionStatusCheckService : ITransactionStatusCheckService
    {
        private readonly ITransactionService _transactionService;
        private readonly IPhonePayService _phonePayService;
        private readonly IAPILogin _apiLogin;
        private readonly IUPISettingService _UPISettingService;
        private IMapper _mapper;
        private readonly ILogger<TransactionStatusCheckService> _logger;
        public TransactionStatusCheckService(ITransactionService transactionService, IPhonePayService phonePayService, IAPILogin apiLogin, ILogger<TransactionStatusCheckService> logger, IUPISettingService UPISettingService, IMapper mapper)
        {
            _transactionService = transactionService;
            _phonePayService = phonePayService;
            _apiLogin = apiLogin;
            _logger = logger;
            _UPISettingService = UPISettingService;
            _mapper = mapper;
        }
        public async Task<TransactionStatusResponse> StatusCheck(StatusCheckRequestNew statusCheckReq)
        {
            bool isServerHookProceed = statusCheckReq.IsServerHookProceed;
            bool isRefreshCalled = false;
            string request = string.Empty;
            var response = new TransactionStatusResponse
            {
                PayerVPA = statusCheckReq.PayerVPA,
                PayerPhone = statusCheckReq.PayerPhone,
                Amount = statusCheckReq.Amount,
                PayerName = statusCheckReq.PayerName,
                PaymentApp = statusCheckReq.PaymentApp,
                Remark = statusCheckReq.Remark,
                RequestedId = statusCheckReq.RequestedId,
                ResponseText = ResponseStatus.Failed.ToString(),
                Status = statusCheckReq.Status.ToString(),
                TransactionId = statusCheckReq.TransactionId,
                UTR = statusCheckReq.UTR,
            };
            try
            {
                var TxnResult = new Response<TxnResult>();
                if (response.Status == "P")
                {
                    DateTime fromDate = DateTime.Now;
                    DateTime.TryParseExact(statusCheckReq.EntryOn, VariableFormats.dd_MMM_yyyy_HH_mm_ss_fff, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
                    var req = new WebScrapModel.TxnDataRequest
                    {
                        authtoken = statusCheckReq.Authtoken,
                        devicefingerprint = statusCheckReq.Devicefingerprint,
                        token = statusCheckReq.Refreshtoken,
                        uuid = statusCheckReq.UUID,
                        merchantid = statusCheckReq.MerchantId,
                        fromdate = fromDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss"),
                        todate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    
                GetTransactionHistory:
                    request = JsonConvert.SerializeObject(req);
                    var txnData = _phonePayService.TxnData(req).Result;
                    _apiLogin.SaveLog(request, JsonConvert.SerializeObject(txnData), "TxnData", statusCheckReq.TransactionId.ToString(), false, statusCheckReq.CallingFrom);
                    if (txnData.Resp_code.Equals("RCS", StringComparison.OrdinalIgnoreCase))
                    {
                        /* Manage Session logged out case*/
                        if (txnData.data.data.errorCode?.ToUpper() == "PR003")
                        {
                            if (!isRefreshCalled)
                            {
                                isRefreshCalled = true;
                                var upiData = await _UPISettingService.GetUPISettingByQRId(statusCheckReq.QrId);
                                var refreshReqParam = _mapper.Map<RefreshTokenRequest>(upiData);
                                var refreshTokenResponse = await RefreshSessionToken(refreshReqParam, upiData.Mobile, upiData.EntryBy,req);
                                response.StatusCode = refreshTokenResponse.StatusCode;
                                response.ResponseText = refreshTokenResponse.ResponseText;
                                if (refreshTokenResponse.StatusCode == ResponseStatus.Success)
                                {
                                    goto GetTransactionHistory;
                                }
                            }
                        }
                        /* End */

                        string remark = string.Empty;
                        bool isTerminated = false;
                        string tarnsactionId = AppUtility.O.CreateTransactionId(statusCheckReq.TransactionId);
                        TxnResult.Result = txnData.data.data.results?.Where(x => x.merchantTransactionId == tarnsactionId).FirstOrDefault();
                        if (TxnResult.Result == null)
                        {
                            TxnResult.Result = new TxnResult();
                            TimeSpan tSpan = DateTime.Now - fromDate;
                            if (tSpan.Minutes > 30)
                            {
                                TxnResult.Result.payResponseCode = "FAILED";
                                remark = "Transaction Not Completed By Users";
                                isTerminated = true;
                            }
                        }
                        if (TxnResult.Result.payResponseCode?.ToLower() != "pending")
                        {
                            if ((_transactionService.PaymentState(TxnResult.Result.paymentState) == 'F' || _transactionService.PaymentState(TxnResult.Result.payResponseCode) == 'F') && !isTerminated)
                            {
                                TxnResult.Result.utr = TxnResult.Result.errorMessage ?? TxnResult.Result.payResponseCode;
                                TxnResult.Result.payResponseCode = "FAILED";
                            }
                            if (Convert.ToDecimal(TxnResult.Result.amount * 0.01) != statusCheckReq.Amount && !isTerminated)
                            {
                                TxnResult.Result.payResponseCode = "FAILED";
                                remark = "Amount miss match";
                            }
                            /* Ensure paymentApp and customerDetails is not null */
                            TxnResult.Result.paymentApp = TxnResult.Result.paymentApp ?? new PaymentApp();
                            TxnResult.Result.customerDetails = TxnResult.Result.customerDetails ?? new CustomerDetails();
                            /* End */
                            var updateStatusRes = await _transactionService.UpdateTransactionStatus(new UpdateTransactionStatusRequest
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
                            });
                            response.Status = TxnResult.Result.payResponseCode;
                            response.UTR = TxnResult.Result.utr;
                            response.PaymentApp = TxnResult.Result.paymentApp.displayText;
                            response.PayerVPA = TxnResult.Result.vpa;
                            response.PayerName = TxnResult.Result.customerDetails.userName;
                            response.PayerPhone = TxnResult.Result.customerDetails.phoneNumber;
                            response.Remark = remark;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            if (!isServerHookProceed && (response.Status.ToUpper() != "P" || response.Status.ToUpper() != "PENDING"))
            {
                var serverhookUrl = $"{statusCheckReq.ServerHookURL}?tr={AppUtility.O.CreateTransactionId(statusCheckReq.TransactionId)}&tid={statusCheckReq.TransactionId}&amount={statusCheckReq.Amount}&requestedId={statusCheckReq.RequestedId}&status={response.Status}&payerVPA={response.PayerVPA}&payerName={response.PayerName}&payerPhone={response.PayerPhone}&paymentApp={response.PaymentApp}&utr={response.UTR}";
                var _res = string.Empty;
                try
                {
                    _res = AppWebRequest.O.CallUsingHttpWebRequest_GETAsync(serverhookUrl, 5).Result;
                }
                catch (Exception ex)
                {
                    _res = ex.Message + "_" + _res;
                }
                _transactionService.CreateCallbackLog(statusCheckReq.RequestedId, serverhookUrl, _res, HookType.ServerHook);
                _transactionService.UpdateTransServerHook(statusCheckReq.TransactionId);
            }
            if (string.IsNullOrEmpty(request) || string.IsNullOrWhiteSpace(request))
                request = JsonConvert.SerializeObject(new { RequestedId = statusCheckReq.RequestedId });
            /* Go to update StatusCheck count*/
            _transactionService.UpdateTransStatusCheck(statusCheckReq.TransactionId);
            /* end */

            _apiLogin.SaveLog(request, JsonConvert.SerializeObject(response), nameof(this.StatusCheck), statusCheckReq.TransactionId.ToString(), true, statusCheckReq.CallingFrom);
            return response;
        }

        private async Task<Response> RefreshSessionToken(RefreshTokenRequest request, string mobileNo, int entryBy, WebScrapModel.TxnDataRequest req)
        {
            var response = new Response();
            var result = await _phonePayService.RefreshSessionToken(request);
            if (result.Resp_code?.ToUpper() == "RCS" && result.data != null && result.data.code?.ToUpper() != "UNAUTHORIZED")
            {
                response = await _UPISettingService.UpdateRefreshToken(new UPISetting
                {
                    Refreshtoken = result.data.refreshToken,
                    AuthToken = result.data.token,
                    EntryBy = entryBy,
                    Mobile = mobileNo
                });
                req.token = result.data.refreshToken;
                req.authtoken = result.data.token;
            }
            else
            {
                _UPISettingService.LogoutUPI(mobileNo);
                response.ResponseText = "Session logged out.Please login first";
            }
            return response;
        }
    }
}