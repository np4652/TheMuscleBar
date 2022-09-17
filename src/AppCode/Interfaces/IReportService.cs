using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ErrorModel>> GetErrorlog();
        Task<IEnumerable<Legder>> GetLedger();
        Task<IEnumerable<SubscripitionReport>> GetSubscripitionReports();
    }   
}



 