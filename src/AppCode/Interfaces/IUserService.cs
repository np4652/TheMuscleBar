using TheMuscleBar.Models;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models.ViewModel;
using System.Collections.Generic;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IUserService : IRepository<ApplicationUser>
    {   
        Task<Response<string>> CollectFee(CollectFee collectFee);
        Task<IEnumerable<UnSubscribedUser>> GetSubscriptionExpired();
        Task<UserDetailsReturn> GetSubscriptionByuser(int userid);
        Task<Response> SaveApiLog(ApiModel req);
    }
}
