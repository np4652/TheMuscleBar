using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using static TheMuscleBar.AppCode.PhonePay.WebScrapModel;

namespace TheMuscleBar.AppCode.PhonePay
{
    public interface IPhonePayService
    {
        Task<PhonePayResponse<LoginOTPResponse>> SendLoginOTP(LoginOTPRequest request);
        Task<PhonePayResponse<VerifyOTPResponse>> VerifyLoginOTP(VerifyOTPRequest request);
        Task<PhonePayResponse<RefreshTokenResponse>> RefreshSessionToken(RefreshTokenRequest request);
        Task<PhonePayResponse<IEnumerable<GroupInfoResponse>>> GroupInfoList(GroupInfoRequest request);
        Task<PhonePayResponse<UpdateSessionResponse>> UpdateSession(UpdateSessionRequest request);
        Task<PhonePayResponse<MerchantProfileResponse>> MerchantProfile(MerchantProfileRequest request);
        Task<PhonePayResponse<StoreListResponse>> StoreList(StoreListRequest request);
        Task<PhonePayResponse<StoreWiseDataResponse>> StorewiseData(StoreWiseDataRequest request);
        Task<PhonePayResponse<QRPOSListResponse>> QRPosList(QRPosListRequest request);
        Task<PhonePayResponse<QRDataByIdResponse>> QRDataById(QRDataByIdRequest request);
        Task<PhonePayResponse<TxnDataResponse>> TxnData(TxnDataRequest request);
        Task<string> GenrateAmountWiseQR(GenrateAmountWiseQRRequest request);

        Task<IEnumerable<UPISetting>> GetMerchantList(int userId);
    }
}
