using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IPaymentGateway : IRepository<PaymentGatewayModel>
    {
        Task<Response> ChangeLiveStatus(int id);
        Task<Response> ChangeLoggingStatus(int id);
        Task <Response> ChangeActiveStatus(int id);
        Task<Response<PaymentGatewayModel>> GetPGDetailByPGIDAsync(PaymentGatewayType paymentGatewayType);
    }
}
