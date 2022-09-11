using ApiRequestUtility;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Const;
using TheMuscleBar.AppCode.CustomAttributes;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Helper;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.PhonePay;
using TheMuscleBar.AppCode.Reops;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using static TheMuscleBar.AppCode.PhonePay.WebScrapModel;

namespace TheMuscleBar.Controllers.API
{
    [JWTAuthorize]
    [ApiController]
    [Route("api/")]
    public class TransactionController : ControllerBase
    {
        private IMapper _mapper;
        private readonly ApplicationUser _user;
        private readonly ITransactionService _transactionService;
        private IPhonePayService _phonePayService;
        private IAPILogin _apiLogin;
        private IUPISettingService _UPISettingService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IHttpContextAccessor httpContext, IPhonePayService phonePayService, IMapper mapper, ITransactionService transactionService, IUPISettingService UPISettingService, ILogger<TransactionController> logger, IAPILogin apiLogin)
        {
            _mapper = mapper;
            _transactionService = transactionService;
            _phonePayService = phonePayService;
            _UPISettingService = UPISettingService;
            _logger = logger;
            if (httpContext != null && httpContext.HttpContext != null)
            {
                LoginResponse loginResponse = (LoginResponse)httpContext?.HttpContext.Items["User"];
                if (loginResponse != null)
                {
                    _user = loginResponse.Result;
                }
            }
            _apiLogin = apiLogin;
        }

        [HttpPost(nameof(InitiateTransactionAsync))]
        public async Task<IActionResult> InitiateTransactionAsync(InitiateTransactionRequest request)
        {
            bool isRefrehCalled = false;
            string transactionId = "0";
            var response = new InitiateTransactionResponse();
            try
            {
                var param = _mapper.Map<TransactionRequest>(request);
                param.EntryBy = _user.Id;
InitiateTransaction:
                response = await _transactionService.InitiateTransaction(param);
                transactionId = response.TransactionId == 0 ? "NA" : response.TransactionId.ToString();
                if (response.StatusCode == ResponseStatus.Expired && !isRefrehCalled)
                {
                    isRefrehCalled = true;
                    var upiData = await _UPISettingService.GetUPISettingByQRId(request.UPIId);
                    var refreshReqParam = _mapper.Map<RefreshTokenRequest>(upiData);
                    var refreshTokenResponse = await RefreshSessionToken(refreshReqParam, upiData.Mobile);
                    if (refreshTokenResponse.StatusCode == ResponseStatus.Success)
                    {
                        goto InitiateTransaction;
                    }
                }
                if (response.StatusCode == ResponseStatus.Success)
                {
                    var encrypt = HashEncryption.O.MD5Hash(AppUtility.O.CreateTransactionId(response.TransactionId));
                    response.TransactionId = response.TransactionId + 5;
                    response.URL = $"{Request.Scheme}://{Request.Host}/order/{response.TransactionId}/{encrypt}";
                    response.IntentString = _transactionService.PaymentUrl(new TransactionDetailResponse
                    {
                        Amount = param.Amount,
                        DisplayName = string.Empty,
                        QRID = param.UPIId,
                        TransactionId = response.TransactionId
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
            _apiLogin.SaveLog(JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response), nameof(this.InitiateTransactionAsync), transactionId.ToString(), true, "FromAPI");
            return Ok(response);
        }

        [HttpPost("Status")]
        public async Task<IActionResult> StatusAsync([Required] string RequestedId)
        {
            var response = new TransactionStatusResponse
            {
                StatusCode = ResponseStatus.Pending,
                ResponseText = ResponseStatus.Pending.ToString(),
                RequestedId = RequestedId
            };
            string transactionId = "0";
            string request = string.Empty;
            bool IsRefreshCalled = false;
            bool isLoggedOut = false;
            var txnData = new PhonePayResponse<TxnDataResponse>();
            try
            {
                response = await _transactionService.GetTransactionStatus(RequestedId);
                transactionId = response.TransactionId == 0 ? "NA" : response.TransactionId.ToString();
                if (response.StatusCode == ResponseStatus.Success)
                {
                    response.Status=_transactionService.TransactionStatus(response.Status[0]);
                    var TxnResult = new Response<TxnResult>();
                    if (response.Status == "PENDING")
                    {
                        var upiDetail = await _transactionService.GetUPISettingByTID(response.TransactionId);
                        if (upiDetail.IsLoggedOut)
                        {
                            isLoggedOut = true;
                            goto CallRefreshToken;
                        }
CallStatusCheckAPI:
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
                            txnData = await _phonePayService.TxnData(req);
                            if (txnData.Resp_code.Equals("RCS", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrEmpty(txnData.data.errorCode))
                                {
                                    isLoggedOut = true;
                                }
                                else if (txnData.data.data.errorCode?.ToUpper() == "PR003")
                                {
                                    isLoggedOut = true;
                                }
                                if (isLoggedOut)
                                {
                                    goto CallRefreshToken;
                                }
                                TxnResult.Result = txnData.data.data.results?.Where(x => x.merchantTransactionId == tarnsactionId).FirstOrDefault();
                                if (TxnResult.Result != null)
                                {
                                    string remark = string.Empty;
                                    if (!TxnResult.Result.paymentState.Equals("pending", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (_transactionService.PaymentState(TxnResult.Result.paymentState)=='F')
                                        {
                                            TxnResult.Result.utr = TxnResult.Result.errorMessage ?? TxnResult.Result.payResponseCode;
                                            TxnResult.Result.payResponseCode="FAILED";
                                        }
                                        if (Convert.ToDecimal(TxnResult.Result.amount*0.01)!=upiDetail.Amount)
                                        {
                                            TxnResult.Result.payResponseCode = "FAILED";
                                            remark = "Amount missmatched";
                                        }
                                        /* Ensure customer detail and paymentApp not null */
                                        TxnResult.Result.customerDetails = TxnResult.Result.customerDetails ?? new CustomerDetails();
                                        TxnResult.Result.paymentApp = TxnResult.Result.paymentApp ?? new PaymentApp();
                                        /* end */
                                        var updateStatusRes = await _transactionService.UpdateTransactionStatus(new UpdateTransactionStatusRequest
                                        {
                                            TID = response.TransactionId,
                                            Status = _transactionService.PaymentState(TxnResult.Result.payResponseCode),
                                            TxnObject = JsonConvert.SerializeObject(TxnResult.Result),
                                            UTR = TxnResult.Result.utr,
                                            Remark = remark,
                                            PayerName = TxnResult.Result.customerDetails.userName,
                                            PayerPhone = TxnResult.Result.customerDetails.phoneNumber,
                                            PayerVPA = TxnResult.Result.vpa,
                                            PaymentApp = TxnResult.Result.paymentApp.displayText
                                        });
                                        TxnResult.StatusCode = updateStatusRes.StatusCode;
                                        TxnResult.ResponseText = updateStatusRes.ResponseText;
                                        response.Status = TxnResult.Result.payResponseCode;
                                        response.UTR = TxnResult.Result.utr;
                                        response.PaymentApp=TxnResult.Result.paymentApp.displayText;
                                        response.PayerVPA = TxnResult.Result.vpa;
                                        response.PayerName = TxnResult.Result.customerDetails.userName;
                                        response.PayerPhone = TxnResult.Result.customerDetails.phoneNumber;
                                        response.Remark = remark;
                                    }
                                }
                            }
                        }
CallRefreshToken:
                        if (isLoggedOut)
                        {
                            var refreshReqParam = _mapper.Map<RefreshTokenRequest>(upiDetail);
                            var refreshRes = await RefreshSessionToken(refreshReqParam, upiDetail.Mobile);
                            if (refreshRes.StatusCode==ResponseStatus.Success && !IsRefreshCalled)
                            {
                                IsRefreshCalled = true;
                                isLoggedOut = false;
                                goto CallStatusCheckAPI;
                            }
                            response.StatusCode = ResponseStatus.Failed;
                            response.ResponseText = "Session logged out.Please login first";
                            response.Status = txnData.data.data.errorCode;
                            /* go to update IsLoggeout = 1 in UPISetting table */
                            var mobileno = upiDetail.Mobile;
                            _UPISettingService.LogoutUPI(mobileno);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            if (string.IsNullOrEmpty(request) || string.IsNullOrWhiteSpace(request))
                request = JsonConvert.SerializeObject(new { RequestedId = RequestedId });
            _apiLogin.SaveLog(request, JsonConvert.SerializeObject(response), nameof(this.StatusAsync), transactionId.ToString(), true, "FromAPI");
            return Ok(new
            {
                response.StatusCode,
                response.ResponseText,
                result = new
                {
                    response.Status,
                    response.RequestedId,
                    response.Amount,
                    utr = string.IsNullOrEmpty(response.UTR) ? "" : response.UTR,
                    response.PayerVPA,
                    response.PayerName,
                    response.PayerPhone,
                    response.PaymentApp
                }
            });
        }

        private async Task<Response> RefreshSessionToken(RefreshTokenRequest request, string mobileNo)
        {
            var response = new Response();
            var result = await _phonePayService.RefreshSessionToken(request);
            if (result.Resp_code?.ToUpper()=="RCS" && result.data!=null && result.data.code?.ToUpper()!="UNAUTHORIZED")
            {
                response= await _UPISettingService.UpdateRefreshToken(new UPISetting
                {
                    Refreshtoken = result.data.refreshToken,
                    AuthToken = result.data.token,
                    EntryBy =  _user.Id,
                    Mobile = mobileNo
                });
            }
            return response;
        }

        //private async Task<IActionResult> StatusCheckJob()
        //{
        //    var result = await _transactionService.GetAllTransactionsAsync();
        //    foreach (var item in result)
        //    {
        //        await StatusCheckAsync(item);
        //    }
        //    return Ok("Job initiated");
        //}
        //private async Task StatusCheckAsync(StatusCheckJobRequest transction)
        //{
        //    bool flag = true;
        //    var response = await _transactionService.GetTransactionStatus(transction.RequestedId);
        //    var TxnResult = new Response<TxnResult>();
        //    try
        //    {
        //        if (response.StatusCode == ResponseStatus.Success && response.Status == "PENDING")
        //        {
        //            flag = false;
        //            var upiDetail = await _transactionService.GetUPISettingByTID(response.TransactionId);
        //            if (upiDetail.Status == 'P')
        //            {
        //                if (!string.IsNullOrEmpty(upiDetail.MerchantId))
        //                {
        //                    string tarnsactionId = AppUtility.O.CreateTransactionId(upiDetail.TransactionId);
        //                    var txnData = await _phonePayService.TxnData(new WebScrapModel.TxnDataRequest
        //                    {
        //                        authtoken = upiDetail.AuthToken,
        //                        devicefingerprint = upiDetail.DeviceFingerprint,
        //                        token = upiDetail.Refreshtoken,
        //                        uuid = upiDetail.UUID,
        //                        merchantid = upiDetail.MerchantId,
        //                        fromdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        //                        todate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        //                    });
        //                    if (txnData.Resp_code.Equals("RCS", StringComparison.OrdinalIgnoreCase))
        //                    {
        //                        TxnResult.Result = txnData.data.data.results?.Where(x => x.transactionId == tarnsactionId).FirstOrDefault();
        //                        if (TxnResult.Result != null)
        //                        {
        //                            string remark = string.Empty;
        //                            if (!TxnResult.Result.paymentState.Equals("pending", StringComparison.OrdinalIgnoreCase))
        //                            {
        //                                if (Convert.ToDecimal(TxnResult.Result.amount * 0.01) != upiDetail.Amount)
        //                                {
        //                                    TxnResult.Result.paymentState = "FAILED";
        //                                    remark = "Amount mismatch";
        //                                }
        //                                var updateStatusRes = await _transactionService.UpdateTransactionStatus(new UpdateTransactionStatusRequest
        //                                {
        //                                    TID = response.TransactionId,
        //                                    Status = _transactionService.PaymentState(TxnResult.Result.paymentState),
        //                                    TxnObject = JsonConvert.SerializeObject(TxnResult.Result),
        //                                    UTR = TxnResult.Result.utr,
        //                                    Remark = remark,
        //                                    PaymentApp = TxnResult.Result.paymentApp.displayText,
        //                                    PayerName = TxnResult.Result.customerDetails.userName,
        //                                    PayerPhone = TxnResult.Result.customerDetails.phoneNumber,
        //                                    PayerVPA = TxnResult.Result.vpa
        //                                });
        //                                TxnResult.StatusCode = updateStatusRes.StatusCode;
        //                                TxnResult.ResponseText = updateStatusRes.ResponseText;
        //                                response.Status = TxnResult.Result.paymentState;
        //                                flag = true;
        //                            }
        //                            else
        //                            {
        //                                await _transactionService.UpdateTransStatusCheck(transction.TransactionId);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        if (flag)
        //        {
        //            var serverhookUrl = $"{transction.ServerHookURL}?tr={AppUtility.O.CreateTransactionId(transction.TransactionId)}&tid={transction.TransactionId}&amount={transction.Amount}&requestedId={transction.RequestedId}&status={response.Status}";
        //            var _res = await AppWebRequest.O.CallUsingHttpWebRequest_GETAsync(serverhookUrl, 5);
        //            await _transactionService.CreateCallbackLog(transction.RequestedId, serverhookUrl, _res, HookType.ServerHook);
        //            await _transactionService.UpdateTransServerHook(transction.TransactionId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message, new { className = this.GetType().Name, fn = nameof(StatusCheckAsync) });
        //    }
        //}
    }
}