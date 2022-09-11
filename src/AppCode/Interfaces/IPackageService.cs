using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IPackageService : IRepository<Package>
    {
        Task<IEnumerable<BuypackageViewModel>> GetPackage(int userId);
        Task<Response> ChangeActiveStatus(int packageId);
    }
}
