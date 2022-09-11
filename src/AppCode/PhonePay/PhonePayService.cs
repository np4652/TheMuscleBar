using ApiRequestUtility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using static TheMuscleBar.AppCode.PhonePay.WebScrapModel;
using Microsoft.Extensions.Logging;
using System.Text;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.Models;
using TheMuscleBar.AppCode.Reops.Entities;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using TheMuscleBar.AppCode.Interfaces;
using LinqToDB.Common;

namespace TheMuscleBar.AppCode.PhonePay
{
    public class PhonePayService : IPhonePayService
    {
        private readonly ILogger<PhonePayService> _logger;
        private readonly IDapperRepository _dapper;
        private readonly IAPILogin _apiLogin;
        private readonly APIConfig _apiConfig;
        public PhonePayService(ILogger<PhonePayService> logger, IDapperRepository dapper, APIConfig apiConfig,IAPILogin aPILogin)
        {
            _logger = logger;
            _dapper = dapper;
            _apiConfig = apiConfig;
            _apiLogin = aPILogin;
        }

        public async Task<PhonePayResponse<LoginOTPResponse>> SendLoginOTP(LoginOTPRequest request)
        {
            var response = new PhonePayResponse<LoginOTPResponse>
            {
                Resp_desc = "Configuration error.Please contact to admin",
                Resp_code = string.Empty,
                data=new LoginOTPResponse()
            };
            string endPoint = string.Empty;
            string apiResponse = "API Configuration not found.Please check appsettings.json";
            if (_apiConfig!=null && !string.IsNullOrEmpty(_apiConfig.token) && !string.IsNullOrEmpty(_apiConfig.baseUrl))
            {
                request.token = _apiConfig.token;
                string baseUrl = _apiConfig.baseUrl;
                endPoint = $"{baseUrl}/api/phonepe/Action/send_loginotp";
                Dictionary<string, string> headers = new Dictionary<string, string> { };
                try
                {
                    apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                    response = JsonConvert.DeserializeObject<PhonePayResponse<LoginOTPResponse>>(apiResponse);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.SendLoginOTP) });
                }
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.SendLoginOTP), request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<VerifyOTPResponse>> VerifyLoginOTP(VerifyOTPRequest request)
        {
            var response = new PhonePayResponse<VerifyOTPResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Action/verify_loginotp";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<VerifyOTPResponse>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.VerifyLoginOTP) });
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.VerifyLoginOTP), request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<RefreshTokenResponse>> RefreshSessionToken(RefreshTokenRequest request)
        {
            var response = new PhonePayResponse<RefreshTokenResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Action/refresh_session_token";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                var preDeserializ = JsonConvert.DeserializeObject<PhonePayResponse>(apiResponse);
                if (preDeserializ.Resp_code?.ToUpper()=="RCS")
                {
                    response = JsonConvert.DeserializeObject<PhonePayResponse<RefreshTokenResponse>>(apiResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.RefreshSessionToken) });
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.RefreshSessionToken),request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<IEnumerable<GroupInfoResponse>>> GroupInfoList(GroupInfoRequest request)
        {
            PhonePayResponse<IEnumerable<GroupInfoResponse>> response = new PhonePayResponse<IEnumerable<GroupInfoResponse>>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Fetch/group_infolist";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<IEnumerable<GroupInfoResponse>>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.GroupInfoList) });
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.GroupInfoList),request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<UpdateSessionResponse>> UpdateSession(UpdateSessionRequest request)
        {
            var response = new PhonePayResponse<UpdateSessionResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Action/update_session";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<UpdateSessionResponse>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.UpdateSession) });
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.UpdateSession),request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<MerchantProfileResponse>> MerchantProfile(MerchantProfileRequest request)
        {
            var response = new PhonePayResponse<MerchantProfileResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Fetch/merchant_profile";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<MerchantProfileResponse>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.MerchantProfile) });
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.MerchantProfile), request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<StoreListResponse>> StoreList(StoreListRequest request)
        {
            var response = new PhonePayResponse<StoreListResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Fetch/storelist";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<StoreListResponse>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.StoreList) });
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.StoreList),request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<StoreWiseDataResponse>> StorewiseData(StoreWiseDataRequest request)
        {
            var response = new PhonePayResponse<StoreWiseDataResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Fetch/storewise_data";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<StoreWiseDataResponse>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.StorewiseData) });
            }
            await _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.StorewiseData), "NA");
            return response;
        }
        public async Task<PhonePayResponse<QRPOSListResponse>> QRPosList(QRPosListRequest request)
        {
            var response = new PhonePayResponse<QRPOSListResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Fetch/qrpos_list";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<QRPOSListResponse>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.QRPosList) });
            }
            _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.QRPosList), request.mobile);
            return response;
        }

        public async Task<PhonePayResponse<QRDataByIdResponse>> QRDataById(QRDataByIdRequest request)
        {
            var response = new PhonePayResponse<QRDataByIdResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            string endPoint = $"{baseUrl}/api/phonepe/Fetch/qrdata_byid";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<PhonePayResponse<QRDataByIdResponse>>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.QRDataById) });
            }
            await _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.QRDataById), "NA");
            return response;
        }

        /// <summary>
        /// Must Store TID in Log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PhonePayResponse<TxnDataResponse>> TxnData(TxnDataRequest request)
        {
            var response = new PhonePayResponse<TxnDataResponse>();
            request.token = _apiConfig.token;
            string baseUrl = _apiConfig.baseUrl;
            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Remove(baseUrl.Length - 1, 1);
            }
            string endPoint = $"{baseUrl}/api/phonepe/Fetch/txndata";
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint, request, headers).ConfigureAwait(false);
                var res = JsonConvert.DeserializeObject<PhonePayResponse>(apiResponse);
                response.Resp_code = res.Resp_code;
                response.Resp_desc = res.Resp_desc;
                if (res.Resp_code.Equals("RCS", StringComparison.OrdinalIgnoreCase))
                {
                    response = JsonConvert.DeserializeObject<PhonePayResponse<TxnDataResponse>>(apiResponse);
                    if (!string.IsNullOrEmpty(response.data.errorCode) && response.data.data==null)
                    {
                        response.data.data = new TxnData
                        {
                            errorCode = response.data.errorCode
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.TxnData) });
            }
            //await _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.TxnData), request.merchantid,false, "FromPhonePayService");
            return response;
        }

        /// <summary>
        /// Must Store TID in Log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GenrateAmountWiseQR(GenrateAmountWiseQRRequest request)
        {
            string response = "";
            StringBuilder endPoint = new StringBuilder("upi://pay?pa={{QRID}}@ybl&pn={{displayname}}&am={{Amount}}&mam=1&tn={transactionNote}}&tr={{merchantTransactionId}}");
            endPoint.Replace("{{QRID}}", request.QRID);
            endPoint.Replace("{{displayname}}", request.Displayname);
            endPoint.Replace("{{Amount}}", request.Amount.ToString());
            endPoint.Replace("{{transactionNote}}", request.TransactionNote);
            endPoint.Replace("{{merchantTransactionId}}", request.MerchantTransactionId);
            Dictionary<string, string> headers = new Dictionary<string, string> { };
            string apiResponse = string.Empty;
            try
            {
                apiResponse = await AppWebRequest.O.PostJsonDataUsingHWRTLS(endPoint.ToString(), request, headers).ConfigureAwait(false);
                response = JsonConvert.DeserializeObject<string>(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { this.GetType().Name, fn = nameof(this.GenrateAmountWiseQR) });
            }
            await _apiLogin.SaveLog(string.Concat("url:", endPoint, "|param:", JsonConvert.SerializeObject(request)), apiResponse, nameof(this.GenrateAmountWiseQR), request.MerchantTransactionId);
            return response;
        }
        public async Task<IEnumerable<UPISetting>> GetMerchantList(int userId)
        {
            string sqlQuery = @"Select * from UPISetting(nolock) where EntryBy = @userId";
            var res = await _dapper.GetAllAsync<UPISetting>(sqlQuery, new { userId }, commandType: CommandType.Text);
            return res ?? new List<UPISetting>();
        }

    }
}
