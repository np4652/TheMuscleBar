using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IAPILogin 
    {
        Task<bool> SaveLog(string request, string response, string method, string tid = "", bool IsIncmOut = false, string CallingFrom = "");
    }
}
