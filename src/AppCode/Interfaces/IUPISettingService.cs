using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IUPISettingService : IRepository<UPISetting>
    {
        Task<Response<UPISetting>> GetUPISettingByMobile(string mobile);
        Task<Response> LogoutUPI(string mobileno);
        Task<Response> UpdateRefreshToken(UPISetting request);
        Task<UPISetting> GetUPISettingByQRId(string QRID);
        Task<bool> IsAnyConfigurationExists(int userId);
    }
}
