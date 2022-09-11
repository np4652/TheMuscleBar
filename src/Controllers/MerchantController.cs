using Microsoft.AspNetCore.Mvc;
using static TheMuscleBar.AppCode.PhonePay.WebScrapModel;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.PhonePay;
using TheMuscleBar.AppCode.Interfaces;
using System;
using TheMuscleBar.Models;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TheMuscleBar.AppCode.Reops.Entities;
using System.Collections.Generic;
using TheMuscleBar.AppCode.Extensions;
using Microsoft.AspNetCore.Authorization;
using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MerchantController : Controller
    {
        private IPhonePayService _phonePayService;
        private IUPISettingService _upiSetting;
        private readonly ILogger _logger;
        private IMapper _mapper;
        public MerchantController(IPhonePayService phonePayService, IUPISettingService upiSetting, IMapper mapper, ILogger<MerchantController> logger)
        {
            _phonePayService = phonePayService;
            _upiSetting = upiSetting;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> _MerchantDetail()
        {
            var response = await _phonePayService.GetMerchantList(User.GetLoggedInUserId<int>());
            var entity = response;
            return PartialView(entity ?? new List<UPISetting>());
        }


        #region Phonepay
        public async Task<IActionResult> SendLoginOTP(LoginOTPRequest request)
        {
            var response = new Response<RequestBase>();
            var upiSetting = await _upiSetting.GetUPISettingByMobile(request.mobile);
            response.ResponseText = upiSetting.ResponseText;
            if (upiSetting.StatusCode==ResponseStatus.Failed)
            {
                request.uuid = Guid.NewGuid().ToString();
                request.devicefingerprint =  Guid.NewGuid().ToString();
                var result = await _phonePayService.SendLoginOTP(request);
                response.ResponseText = result.Resp_desc;
                if (result.Resp_code.Equals("RCS", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(result.data.token))
                {
                    response.StatusCode=ResponseStatus.Success;
                    response.ResponseText=result.data.token;
                    response.Result=new RequestBase
                    {
                        uuid = request.uuid,
                        devicefingerprint = request.devicefingerprint
                    };
                }
            }
            return Ok(response);
        }
        public async Task<IActionResult> VerifyLoginOTP(VerifyOTPRequest request)
        {
            var response = new Response();
            var result = await _phonePayService.VerifyLoginOTP(request);
            try
            {
                if ((result.Resp_code?.Equals("RCS", StringComparison.OrdinalIgnoreCase) ?? false) && !string.IsNullOrEmpty(result.data.token))
                {
                    // if roleId 103 not found return msg you are not authorized to access this service.Please contact to Phonepay customer care
                    int groupId = result.data.groups?.Where(x => x.userRole.roleId=="R2009211617492894506103").FirstOrDefault()?.groupId ?? 0;
                    response.ResponseText = groupId > 0 ? response.ResponseText : "you are not authorized to access this service.Please contact to Phonepay customer care";
                    if (groupId > 0)
                    {
                        bool Is = result.data.groupSelection;
                        var refreshTokenRequest = _mapper.Map<RefreshTokenRequest>(request);
                        refreshTokenRequest.authtoken =  result.data.token;
                        refreshTokenRequest.refreshtoken = result.data.refreshToken;
                        refreshTokenRequest.mobile = request.mobile;
                        // if groupSelection : false than must call RefreshSessionToken and GroupInfoList
                        if (!result.data.groupSelection)
                        {
                            var refreshResponse = await _phonePayService.RefreshSessionToken(refreshTokenRequest);
                            if (refreshResponse.Resp_code?.Equals("RCS", StringComparison.OrdinalIgnoreCase) ?? false)
                            {
                                var groupInfoRequest = _mapper.Map<GroupInfoRequest>(refreshTokenRequest);
                                groupInfoRequest.mobile = request.mobile;
                                var groupInfoResponse = await _phonePayService.GroupInfoList(groupInfoRequest);
                                if (groupInfoResponse.Resp_code?.Equals("RCS", StringComparison.OrdinalIgnoreCase) ?? false)
                                {
                                    Is=true;
                                }
                            }
                        }

                        if (Is)
                        {
                            var updateSessionRequest = _mapper.Map<UpdateSessionRequest>(refreshTokenRequest);
                            updateSessionRequest.usergroupid = groupId.ToString();
                            updateSessionRequest.mobile = request.mobile;
                            var updateSessionResponse = await _phonePayService.UpdateSession(updateSessionRequest);
                            if (updateSessionResponse.Resp_code?.Equals("RCS", StringComparison.OrdinalIgnoreCase)??false)
                            {
                                var merchantProfileRequest = _mapper.Map<MerchantProfileRequest>(request);
                                merchantProfileRequest.authtoken = updateSessionResponse.data.token;
                                merchantProfileRequest.mobile = request.mobile; 
                                PhonePayResponse<MerchantProfileResponse> profileResponse = await _phonePayService.MerchantProfile(merchantProfileRequest);
                                if (profileResponse.Resp_code?.Equals("RCS", StringComparison.OrdinalIgnoreCase)??false)
                                {
                                    var storeListRequest = _mapper.Map<StoreListRequest>(merchantProfileRequest);
                                    storeListRequest.merchantid = profileResponse.data.data.merchantId;
                                    storeListRequest.mobile = request.mobile;
                                    var storeListResponse = await _phonePayService.StoreList(storeListRequest);
                                    if (storeListResponse.Resp_code?.Equals("RCS", StringComparison.OrdinalIgnoreCase)??false)
                                    {
                                        foreach (StoreListingResponse store in storeListResponse.data.data.storeListingResponses)
                                        {
                                            var qrPosListRequest = _mapper.Map<QRPosListRequest>(storeListRequest);
                                            qrPosListRequest.storeid = store.storeId;
                                            qrPosListRequest.mobile = request.mobile;
                                            var qrPosListResponse = await _phonePayService.QRPosList(qrPosListRequest);
                                            if ((qrPosListResponse.Resp_code?.Equals("RCS", StringComparison.OrdinalIgnoreCase) ?? false))
                                            {
                                                string qrId = qrPosListResponse.data.data?.FirstOrDefault().id;
                                                if (!string.IsNullOrEmpty(qrId))
                                                {
                                                    response = await _upiSetting.AddAsync(new UPISetting
                                                    {
                                                        Mobile = request.mobile,
                                                        UUID = request.uuid,
                                                        DeviceFingerprint = request.devicefingerprint,
                                                        UserId = result.data.userId,
                                                        AuthToken = updateSessionResponse.data.token,
                                                        Refreshtoken = updateSessionResponse.data.refreshToken,
                                                        UserGroupId = groupId.ToString(),
                                                        MerchantId = profileResponse.data.data.merchantId,
                                                        StoreId = store.storeId,
                                                        QrId = qrId,
                                                        DisplayName = profileResponse.data.data.displayName,
                                                        EntryBy = User?.GetLoggedInUserId<int>() ?? 0
                                                    });
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { className = this.GetType().Name, fn = nameof(VerifyLoginOTP) });
            }
            return Ok(response);
        }

        public async Task<PhonePayResponse<RefreshTokenResponse>> RefreshSessionToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _phonePayService.RefreshSessionToken(request);
            return result;
        }

        public async Task<IActionResult> GroupInfoList([FromBody] GroupInfoRequest request)
        {
            var result = await _phonePayService.GroupInfoList(request);
            return Ok(result);
        }

        public async Task<IActionResult> UpdateSession([FromBody] UpdateSessionRequest request)
        {
            var result = await _phonePayService.UpdateSession(request);
            return Ok(result);
        }

        public async Task<IActionResult> MerchantProfile(MerchantProfileRequest request)
        {
            var result = await _phonePayService.MerchantProfile(request);
            return Ok(result);
        }

        public async Task<IActionResult> StoreList([FromBody] StoreListRequest request)
        {
            var result = await _phonePayService.StoreList(request);
            return Ok(result);
        }
        public async Task<IActionResult> StorewiseData([FromBody] StoreWiseDataRequest request)
        {
            var result = await _phonePayService.StorewiseData(request);
            return Ok(result);
        }

        public async Task<IActionResult> QRPosList([FromBody] QRPosListRequest request)
        {
            var result = await _phonePayService.QRPosList(request);
            return Ok(result);
        }

        public async Task<IActionResult> QRDataById([FromBody] QRDataByIdRequest request)
        {
            var result = await _phonePayService.QRDataById(request);
            return Ok(result);
        }

        public async Task<IActionResult> TxnData([FromBody] TxnDataRequest request)
        {
            var result = await _phonePayService.TxnData(request);
            return Ok(result);
        }
        #endregion  Test
    }
}
