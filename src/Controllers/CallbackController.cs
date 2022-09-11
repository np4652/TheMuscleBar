using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using static TheMuscleBar.AppCode.PaymentGateway.CashFree.Models;
using System.Net;
using TheMuscleBar.AppCode.Extensions;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.AppCode.Helper;
using TheMuscleBar.Models.ViewModel;
using QRCoder;
using System.Web;
using static TheMuscleBar.AppCode.PhonePay.WebScrapModel;
using TheMuscleBar.AppCode.PhonePay;

namespace TheMuscleBar.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CallbackController : Controller
    {
        private readonly AppCode.PaymentGateway.IPaymentGatewayService _pgService;
        private readonly IUserService _userService;
        private readonly IAPILogin _apiLogin;
        private readonly ITransactionService _transactionService;
        private readonly APIConfig _apiConfig;
        private IPhonePayService _phonePayService;
        public CallbackController(AppCode.PaymentGateway.IPaymentGatewayService pgService, IUserService userService, ITransactionService transactionService, APIConfig apiConfig, IPhonePayService phonePayService, IAPILogin apiLogin)
        {
            _pgService = pgService;
            _userService = userService;
            _transactionService = transactionService;
            _apiConfig = apiConfig;
            _phonePayService = phonePayService;
            _apiLogin = apiLogin;
        }
        [HttpGet("/order/{TID}/{transactionId}")]
        public async Task<IActionResult> order(int TID, string transactionId)
        {
            var response = new Response<TransactionViewModel>
            {
                StatusCode=ResponseStatus.Success,
                ResponseText=ResponseStatus.Success.ToString(),
                Result = new TransactionViewModel()
            };
            TID = TID-5;
            var encrypt = HashEncryption.O.MD5Hash(AppUtility.O.CreateTransactionId(TID));
            if (encrypt!=transactionId)
            {
                response.StatusCode = ResponseStatus.Failed;
                response.ResponseText = "Invalid Request";
                return View(response);
            }
            var transactionDetail = await _transactionService.GetTransactionDetailByTID(TID);
            if (transactionDetail.TransactionId>0)
            {
                string paumentUrl = _transactionService.PaymentUrl(transactionDetail); //PaymentUrl(transactionDetail)
                response.Result.QR = $"data:image/png;base64,{Convert.ToBase64String(GetQR(paumentUrl))}";
                response.Result.DisplayName = transactionDetail.DisplayName;
                response.Result.UPIUrl = paumentUrl;
                 if (string.IsNullOrEmpty(transactionDetail.WebhookURL))
                {
                    transactionDetail.WebhookURL = $"{Request.Scheme}://{Request.Host}/transaction/completed";
                }
                response.Result.WebhookURL = $"{transactionDetail.WebhookURL}?tr={transactionId}&tid={TID}&amount={transactionDetail.Amount}&requestedId={transactionDetail.RequestedId}&status=pending";
                _transactionService.CreateCallbackLog(transactionDetail.RequestedId, response.Result.WebhookURL, string.Empty, HookType.Webhook);
            }
            return View(response);
        }

        [HttpGet("transaction/completed")]
        public IActionResult complete()
        {
            return View();
        }
        private byte[] GetQR(string url)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData QCD = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(QCD);
            return AppUtility.O.ConvertBitmapToBytes(qRCode.GetGraphic(20));
        }
        private string PaymentUrl(TransactionDetailResponse transactionDetail)
        {
            string transactionId = AppUtility.O.CreateTransactionId(transactionDetail.TransactionId);
            StringBuilder url = new StringBuilder(_apiConfig.qrUrl);
            url.Replace("{{QRID}}", transactionDetail.QRID);
            url.Replace("{{displayname}}", transactionDetail.DisplayName);
            url.Replace("{{Amount}}", transactionDetail.Amount.ToString());
            url.Replace("{{transactionNote}}", transactionId);
            url.Replace("{{merchantTransactionId}}", transactionId);
            return url.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> CashFreeStatusCheck(int TID)
        {
            var res = _pgService.StatusCheck(new StatusCheckRequest
            {
                TID = TID,
                PGID = PaymentGatewayType.CASHFREE,
            });
            return Json(res);
        }

        [HttpGet("/CashFreereturn")]
        public async Task<IActionResult> CashFreereturn(string order_id, string order_token)
        {
            int tid = Convert.ToInt32(order_id?.Replace("TID", ""));
            var res = await _pgService.StatusCheck(new StatusCheckRequest
            {
                TID = tid,
                PGID = PaymentGatewayType.CASHFREE,
            });
            /* Assign package and update DB*/
            if (res.StatusCode == ResponseStatus.Success)
            {
                var response = await _userService.Assignpackage(tid);
            }
            /* End */
            StringBuilder html = new StringBuilder(@"<html><head><script>
                                (()=>{
                                        var obj={TID:""{TID}"",Amount:""{Amount}"",TransactionID:""{TransactionID}"",statusCode:""{statusCode}"",reason:""{reason}"",origin:""addMoney"",gateway:""CashFree""}
                                        localStorage.setItem('obj', JSON.stringify(obj));
                                        window.close()
                                   })();</script></head><body><h6>Redirect to site.....</h6></body></html>");
            html.Replace("{TID}", res.Result.OrderId);
            html.Replace("{Amount}", "");
            html.Replace("{TransactionID}", res.Result.OrderId);
            html.Replace("{statusCode}", ((int)res.StatusCode).ToString()); // comesfrom DB
            html.Replace("{reason}", res.Result.OrderStatus);
            return Content(html.ToString(), contentType: "text/html; charset=utf-8");
        }

        [HttpGet, HttpPost]
        [Route("/CashFreenotify")]
        public async Task<IActionResult> CashFreenotify()
        {
            StringBuilder resp = new StringBuilder("");
            var request = HttpContext.Request;
            var callbackAPIReq = new CallbackData
            {
                Method = request.Method,
                APIID = 0,
                Content = resp.ToString(),
                Scheme = request.Scheme,
                Path = request.Path
            };
            try
            {
                if (request.Method == "POST")
                {
                    if (request.HasFormContentType)
                    {
                        if (request.Form.Keys.Count > 0)
                        {
                            foreach (var item in request.Form.Keys)
                            {
                                request.Form.TryGetValue(item, out StringValues strVal);
                                if (resp.Length == 0)
                                {
                                    resp.AppendFormat("{0}={1}", item, strVal);
                                }
                                else
                                {
                                    resp.AppendFormat("&{0}={1}", item, strVal);
                                }
                            }
                        }
                    }
                    else
                    {
                        resp = new StringBuilder(request.GetRawBodyStringAsync().Result);
                    }
                }
                else
                {
                    resp = new StringBuilder(request.QueryString.ToString());
                }
                if (resp.Length == 0)
                {
                    resp = new StringBuilder(request.QueryString.ToString());
                }
            }
            catch (Exception ex)
            {
                resp = new StringBuilder(ex.Message);
                //cMl.ErrorLog(GetType().Name, "CashFreenotify", resp.ToString());
            }
            callbackAPIReq.Content = WebUtility.UrlDecode(resp.ToString());

            var Is = await _apiLogin.SaveLog(JsonConvert.SerializeObject(callbackAPIReq), string.Empty, "cashfreeCallback", "NA");
            if (Is)
            {
                var cfCData = JsonConvert.DeserializeObject<CFRealCallbackResp>(callbackAPIReq.Content);
                var _res = UpdateCashfreeResponse(new CashfreeCallbackResponse
                {
                    orderId = cfCData.data.order.order_id,
                    orderAmount = Convert.ToDecimal(cfCData.data.order.order_amount),
                    referenceId = cfCData.data.payment.bank_reference,
                    txStatus = cfCData.data.payment.payment_status,
                    paymentMode = cfCData.data.payment.payment_group,
                    txMsg = cfCData.data.payment.payment_message,
                    txTime = cfCData.data.payment.payment_time,
                    signature = "",
                });
            }
            return Ok();
        }

        /* have to Edit 
         * 1. check status by API
         * 2. Replace TID from orderId and get only numeric value
         * 3. ProcGetTransactionPGDetail by passing commonInt instead of commonStr
         * 4. check amount get by  status check with amount get by proc
         * 5. create a private function to modify  PaymentMode according db
     */
        public async Task<Response> UpdateCashfreeResponse(CashfreeCallbackResponse PGResponse)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = ResponseStatus.Failed.ToString()
            };
            try
            {
                if (PGResponse == null)
                    return res;
                if (string.IsNullOrEmpty(PGResponse.orderId))
                    return res;
                if (string.IsNullOrEmpty(PGResponse.referenceId))
                    return res;
                if (PGResponse.orderAmount <= 0)
                    return res;
                if (string.IsNullOrEmpty(PGResponse.paymentMode))
                    return res;
                if (string.IsNullOrEmpty(PGResponse.txStatus))
                    return res;

                int TID = !string.IsNullOrEmpty(PGResponse.orderId) ? Convert.ToInt32(PGResponse.orderId.Replace("TID", string.Empty, StringComparison.OrdinalIgnoreCase)) : 0;

                var procRes = await _pgService.GetInitiatedPaymentDetail(TID);
                if (procRes.StatusCode == ResponseStatus.Success)
                {
                    if (true)//procRes.Status.In(RechargeRespType.REQUESTSENT, RechargeRespType.PENDING)
                    {
                        var appId = procRes.Result.MerchantID;
                        var secretKey = procRes.Result.MerchantKey;
                        var orderId = procRes.Result.TID;
                        if (procRes.Result.PGID == PaymentGatewayType.CASHFREE)
                        {
                            var stsCheckResp = await _pgService.StatusCheck(new StatusCheckRequest
                            {
                                TID = Convert.ToInt32(procRes.Result.TID),
                                PGID = procRes.Result.PGID
                            });
                            if (stsCheckResp != null)
                            {
                                if (procRes.Result.Amount == stsCheckResp.Result.OrderAmount)
                                {
                                    var PayMethod = PGResponse.paymentMode;
                                    string status = "P";
                                    if (stsCheckResp.Result.OrderStatus.Equals("OK", StringComparison.OrdinalIgnoreCase) && PGResponse.txStatus.Equals("success", StringComparison.OrdinalIgnoreCase))
                                    {
                                        status = "S";
                                    }
                                    else if (PGResponse.txStatus.Equals("failed", StringComparison.OrdinalIgnoreCase))
                                    {
                                        status = "F";
                                    }
                                    else
                                    {
                                        var apiResponse = (CashfreeStatusResponse)stsCheckResp.Result.APIResponse;
                                        if ((stsCheckResp.Result.OrderStatus.Equals("error", StringComparison.OrdinalIgnoreCase) && apiResponse.reason.Contains("Order Id does not exist", StringComparison.OrdinalIgnoreCase)))
                                        {
                                            status = "F";
                                        }
                                    }
                                    /* update status in DB */
                                    res = await _pgService.updateInitiatedPayment(TID, status);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        /* Test */
        [Route("/RefreshSessionToken")]
        public async Task<PhonePayResponse<RefreshTokenResponse>> RefreshSessionToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _phonePayService.RefreshSessionToken(request);
            return result;
        }
        /* Test End */
    }
}