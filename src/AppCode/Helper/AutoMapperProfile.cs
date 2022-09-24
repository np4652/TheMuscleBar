using AutoMapper;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using TheMuscleBar.Models.ViewModel;

namespace TheMuscleBar.AppCode.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmailConfig, EmailSettings>();
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
            CreateMap<RegisterAPIRequest, RegisterViewModel>().ReverseMap();
            CreateMap<ApplicationUser, CollectFeeViewModel>().ReverseMap();
            CreateMap<ApplicationUser, UserUpdateRequest>();
            CreateMap<ApplicationUser, Register>();
            CreateMap<ApplicationUserProcModel, ApplicationUser>().ReverseMap();            
            CreateMap<PaymentGatewayModel, StatusCheckRequest>().ReverseMap();
        }
    }
}
