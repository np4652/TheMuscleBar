using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using TheMuscleBar.Models.ViewModel;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ErrorModel>> GetErrorlog();
        Task<IEnumerable<Legder>> GetLedger();
        Task<IEnumerable<SubscripitionReport>> GetSubscripitionReports();
        Task<Invoice> GetInvoice(int tid);
        Task<DashboardSummery> GetDashboardSummery();
        Task<IEnumerable<AttendanceView>> GetAttendance(string fromdate, string todate, int id = 0);
        Task<IEnumerable<UserList>> GetUsersList();
    }   
}



 