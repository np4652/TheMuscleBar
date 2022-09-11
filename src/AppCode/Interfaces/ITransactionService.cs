using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface ITransactionService
    {
        Task<InitiateTransactionResponse> InitiateTransaction(TransactionRequest entity);
        Task<TransactionDetailResponse> GetTransactionDetailByTID(int TID);
        Task<UPISettingWithTIDDetail> GetUPISettingByTID(int TID);
        Task<Response> UpdateTransactionStatus(UpdateTransactionStatusRequest request);
        Task<TransactionStatusResponse> GetTransactionStatus(string requestedId);
        char PaymentState(string status);
        string TransactionStatus(char status);

        Task<UPISettingWithTIDDetail> GetUPISettingByRequestedID(string requestedId);
        Task<IEnumerable<StatusCheckJobRequest>> GetAllTransactionsAsync();
        Task<Response> UpdateTransServerHook(int TID);
        Task<Response> UpdateTransStatusCheck(int TID);
        string PaymentUrl(TransactionDetailResponse transactionDetail);
        Task CreateCallbackLog(string requestedId, string request, string response, HookType hookType);
    }
}
