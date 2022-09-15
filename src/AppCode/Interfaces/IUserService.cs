using TheMuscleBar.Models;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IUserService : IRepository<ApplicationUser>
    {   
        Task<Response<string>> CollectFee(CollectFee collectFee);
    }
}
