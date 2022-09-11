using TheMuscleBar.Models;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IUserService : IRepository<ApplicationUser>
    {

        Task<Response> ChangeAction(int id);
        Task<Response> AssignPackage(int userId, int packageId);
        Task<Response> Assignpackage(int TID);
    }
}
