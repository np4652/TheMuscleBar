using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface ITransactionStatusCheckService
    {
        Task<TransactionStatusResponse> StatusCheck(StatusCheckRequestNew statusCheckReq);
    }
}
