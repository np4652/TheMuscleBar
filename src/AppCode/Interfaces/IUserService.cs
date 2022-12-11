using TheMuscleBar.Models;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IUserService : IRepository<ApplicationUser>
    {   
        Task<CollectFeeResponse> CollectFee(CollectFee collectFee);
        Task<IEnumerable<UnSubscribedUser>> GetSubscriptionExpired();
        Task<UserDetailsReturn> GetSubscriptionByuser(int userid);
        Task<Response> SaveApiLog(ApiModel req);
        Task<Response> MergeAttendance(DataTable req);
        Task<Response> deleteUsersData(int id);
    }
}
