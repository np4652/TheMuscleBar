using ApiRequestUtility;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

namespace TheMuscleBar.Controllers
{
    //[Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private IPhonePayService _phonePayService;
        private IUPISettingService _UPISettingService;
        private IMapper _mapper;
        private IAPILogin _apiLogin;
        private readonly ILogger<TransactionController> _logger;
        public TransactionController(ITransactionService transactionService, IPhonePayService phonePayService,
            IUPISettingService uPISettingService, IMapper mapper, IAPILogin apiLogin, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _phonePayService = phonePayService;
            _UPISettingService = uPISettingService;
            _mapper = mapper;
            _apiLogin = apiLogin;
            _logger = logger;
        }
        public async Task<IActionResult> StatusCheck(int TID)
        {
            bool isRefreshCalled = false;
            string request = string.Empty;
            TID = TID - 5;
            var upiDetail = await _transactionService.GetUPISettingByTID(TID);
            var response = new Response<TxnResult>
            {
                ResponseText = "Invalid Request",
                Result = new TxnResult()
                {
                    utr=upiDetail.UTR,
                    vpa=upiDetail.PayerVPA,
                    customerDetails=new WebScrapModel.CustomerDetails
                    {
                        phoneNumber=upiDetail.PayerPhone,
                        userName=upiDetail.PayerName
                    },
                    paymentApp=new PaymentApp
                    {
                        displayText = upiDetail.PaymentApp
                    }
                }
            };
            try
            {
                if (upiDetail.Status == 'P')
                {
                    if (!string.IsNullOrEmpty(upiDetail.MerchantId))
                    {
                        DateTime fromDate = DateTime.Now;
                        DateTime.TryParseExact(upiDetail.EntryOn, VariableFormats.dd_MMM_yyyy_HH_mm_ss_fff, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
                        string tarnsactionId = AppUtility.O.CreateTransactionId(upiDetail.TransactionId);
GetTransactionHistory:
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
                        var txnData = await _phonePayService.TxnData(req);
                        /* Save outgoing log*/
                        _apiLogin.SaveLog(request, JsonConvert.SerializeObject(txnData), nameof(this.StatusCheck), TID.ToString(), false, "BehindQR");                        
                        /**/

                        if (txnData.Resp_code.Equals("RCS", StringComparison.OrdinalIgnoreCase))
                        {
                            if (txnData.data.data.errorCode?.ToUpper() == "PR003")
                            {
                                if (!isRefreshCalled)
                                {
                                    isRefreshCalled = true;
                                    var upiData = await _UPISettingService.GetUPISettingByQRId(upiDetail.QrId);
                                    var refreshReqParam = _mapper.Map<RefreshTokenRequest>(upiData);
                                    var refreshTokenResponse = await RefreshSessionToken(refreshReqParam, upiData.Mobile, upiDetail.EntryBy);
                                    if (refreshTokenResponse.StatusCode == ResponseStatus.Success)
                                    {
                                        goto GetTransactionHistory;
                                    }
                                    _UPISettingService.LogoutUPI(upiDetail.Mobile);
                                    response.ResponseText = "Session logged out.Please login first";
                                }
                            }
                            else
                            {
                                response.Result = txnData.data.data.results?.Where(x => x.merchantTransactionId == tarnsactionId).FirstOrDefault();
                                if (response.Result != null)
                                {
                                    string remark = string.Empty;
                                    if (_transactionService.PaymentState(response.Result.payResponseCode) == 'F' || _transactionService.PaymentState(response.Result.paymentState) == 'F')
                                    {
                                        //response.Result.utr = response.Result.errorMessage;
                                        response.Result.utr = response.Result.errorMessage ?? response.Result.payResponseCode;
                                        response.Result.payResponseCode="FAILED";
                                    }
                                    if (Convert.ToDecimal(response.Result.amount * 0.01) != upiDetail.Amount)
                                    {
                                        response.Result.payResponseCode = "FAILED";
                                        remark = "Amount miss match";
                                    }
                                    /* Ensure paymentApp and customerDetails is not null */
                                    response.Result.paymentApp = response.Result.paymentApp ?? new PaymentApp();
                                    response.Result.customerDetails = response.Result.customerDetails ?? new WebScrapModel.CustomerDetails();
                                    /* end */
                                    /* If Get Success or Failed */
                                    bool isServerhook = false;
                                    var serverhookUrl = $"{upiDetail.ServerHookURL}?tr={AppUtility.O.CreateTransactionId(upiDetail.TransactionId)}&tid={upiDetail.TransactionId}&amount={upiDetail.Amount}&requestedId={upiDetail.RequestedId}&status={response.Result.payResponseCode}&payerVPA={response.Result.vpa}&payerName={response.Result.customerDetails.userName}&payerPhone={response.Result.customerDetails.phoneNumber}&paymentApp={response.Result.paymentApp.displayText}&utr={response.Result.utr}";
                                    var _res = string.Empty;
                                    try
                                    {
                                        _res = AppWebRequest.O.CallUsingHttpWebRequest_GETAsync(serverhookUrl, 5).Result;
                                        isServerhook = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        _res = ex.Message + "_" + _res;
                                    }

                                    /* than Hit server url and goto DB set IsServerHookProceed = 1 with UTR and Payer Details */
                                    var updateStatusRes = await _transactionService.UpdateTransactionStatus(new UpdateTransactionStatusRequest
                                    {
                                        TID = TID,
                                        Status = _transactionService.PaymentState(response.Result.payResponseCode),
                                        TxnObject = JsonConvert.SerializeObject(response.Result),
                                        UTR = response.Result.utr,
                                        Remark = remark,
                                        PaymentApp = response.Result.paymentApp.displayText,
                                        PayerName = response.Result.customerDetails.userName,
                                        PayerPhone = response.Result.customerDetails.phoneNumber,
                                        PayerVPA = response.Result.vpa,
                                        isServerhook = isServerhook
                                    });
                                    response.StatusCode = updateStatusRes.StatusCode;
                                    response.ResponseText = updateStatusRes.ResponseText;
                                }
                            }
                        }
                    }
                }
                else
                {
                    response.Result.payResponseCode = _transactionService.TransactionStatus(upiDetail.Status);
                    response.Result.merchantTransactionId = upiDetail.TransactionId.ToString();
                    response.Result.amount = Convert.ToInt32(upiDetail.Amount);
                    response.Result.utr = upiDetail.UTR;
                    response.Result.paymentApp.displayText = upiDetail.PaymentApp;
                    response.Result.vpa = upiDetail.PayerVPA;
                    response.Result.customerDetails.userName = upiDetail.PayerName;
                    response.Result.customerDetails.phoneNumber = upiDetail.PayerPhone;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { className = this.GetType().Name, fn = nameof(StatusCheck) });
            }
            response.Result = response.Result ?? new TxnResult
            {
                paymentApp = new PaymentApp(),
                customerDetails = new WebScrapModel.CustomerDetails(),
            };

            if (string.IsNullOrEmpty(request) || string.IsNullOrWhiteSpace(request))
                request = JsonConvert.SerializeObject(new { TID = TID });
            _apiLogin.SaveLog(request, JsonConvert.SerializeObject(response), nameof(this.StatusCheck), TID.ToString(), true, "BehindQR");
            var payerDetail = new
            {
                utr = response.Result.utr ?? "N/A",
                paymentApp = response.Result.paymentApp.displayText ?? "N/A",
                payerVPA = response.Result.vpa ?? "N/A",
                payerName = response.Result.customerDetails.userName ?? "N/A",
                payerPhone = response.Result.customerDetails.phoneNumber ?? "N/A",
            };
            string payerDetailStr = AppUtility.O.GetQueryString(payerDetail);
            return Ok(new
            {
                response.StatusCode,
                response.ResponseText,
                result = new
                {
                    status = response.Result.payResponseCode,
                    requestedId = response.Result.merchantTransactionId,
                    amount = response.Result.amount,
                    utr = response.Result.utr,
                    paymentApp = response.Result.paymentApp.displayText,
                    payerVPA = response.Result.vpa,
                    payerName = response.Result.customerDetails.userName,
                    payerPhone = response.Result.customerDetails.phoneNumber,
                    payerDetailStr = payerDetailStr,
                }
            });
        }

        private async Task<Response> RefreshSessionToken(RefreshTokenRequest request, string mobileNo, int entryBy)
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
            }
            return response;
        }
    }
}