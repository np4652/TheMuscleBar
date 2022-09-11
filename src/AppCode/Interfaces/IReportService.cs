using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IReportService
    {
        
        Task<IEnumerable<APIModel>> GetAPIlog();
        Task<IEnumerable<ErrorModel>> GetErrorlog();
        //Task<IEnumerable<Transactions>> GetTransactionReport();
        Task<IEnumerable<TransactionReport>> GetTransactionReport(TransactionReportRequest request);
        Task<APIUserCounts> GetUserDashboardsummary(int UserId);
        Task<AdminUserCounts> GetAdminDashboardsummary(int UserId);
        Task<IEnumerable<ChartData>> GetAPIDashboardGraphData(int UserId);
        Task<DonutData> GetExpiredActive(int UserId);
        Task<PieChart> GetPieChart(int UserId, int Type);
        Task<AdminUserCounts> GetAdminDashboardAmnt(int UserId);

        Task<APIUserCounts> GetUserDashboardAmnt(int UserId);
        Task<JDataTable<APIModel>> GetAPIlogs(JSONAOData jsonAOData = null);
        Task<IEnumerable<APIModel>> GetAPIlogByID(string TID);
        Task<IEnumerable<CallBackHitLog>> GetAllCallBackHit();
    }   
}



 