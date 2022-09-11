using AutoMapper;
using TheMuscleBar.AppCode.PhonePay;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
namespace TheMuscleBar.AppCode.Helper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<EmailConfig, EmailSettings>();
            CreateMap<ApplicationUser, RegisterViewModel>();
            CreateMap<ApplicationUser, UserUpdateRequest>();
            CreateMap<ApplicationUser, Register>();
            CreateMap<ApplicationUserProcModel, ApplicationUser>().ReverseMap();
            CreateMap<WebScrapModel.VerifyOTPRequest, WebScrapModel.RefreshTokenRequest>().ReverseMap();
            CreateMap<WebScrapModel.RefreshTokenRequest, WebScrapModel.GroupInfoRequest>().ReverseMap();
            CreateMap<WebScrapModel.RefreshTokenRequest, WebScrapModel.UpdateSessionRequest>().ReverseMap();
            CreateMap<WebScrapModel.VerifyOTPRequest, WebScrapModel.MerchantProfileRequest>().ReverseMap();
            CreateMap<WebScrapModel.MerchantProfileRequest, WebScrapModel.StoreListRequest>().ReverseMap();
            CreateMap<WebScrapModel.StoreListRequest, WebScrapModel.QRPosListRequest>().ReverseMap();
            CreateMap<InitiateTransactionRequest, TransactionRequest>();
            CreateMap<PaymentGatewayModel, StatusCheckRequest>().ReverseMap();
            CreateMap<UPISettingWithTIDDetail, WebScrapModel.RefreshTokenRequest>().ReverseMap();
            CreateMap<UPISetting, WebScrapModel.RefreshTokenRequest>().ReverseMap();
        }
    }
}
