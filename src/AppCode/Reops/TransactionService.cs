using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Helper;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Reops
{
    public class TransactionService : ITransactionService
    {
        private IDapperRepository _dapper;
        private readonly APIConfig _apiConfig;
        public TransactionService(IDapperRepository dapper, APIConfig apiConfig)
        {
            _dapper = dapper;
            _apiConfig = apiConfig;
        }

        public async Task<InitiateTransactionResponse> InitiateTransaction(TransactionRequest entity)
        {
            var response = new InitiateTransactionResponse();
            if (entity.EntryBy < 1)
            {
                response.ResponseText = "Access denied";
                goto Finish;
            }
            else if (entity.Amount < 1)
            {
                response.ResponseText = "Amount must be greater than 0";
                goto Finish;
            }
            else if (string.IsNullOrEmpty(entity.UPIId))
            {
                response.ResponseText = "Invalid UPIId";
                goto Finish;
            }
            else if (string.IsNullOrEmpty(entity.RequestedId))
            {
                response.ResponseText = "RequestedId can not be null or empty";
                goto Finish;
            }
            response = await _dapper.GetAsync<InitiateTransactionResponse>("proc_InitiateTransaction", new
            {
                entity.TransactionId,
                entity.RequestedId,
                entity.Amount,
                entity.UPIId,
                entity.ServerHookURL,
                entity.WebHookURL,
                entity.EntryBy,
                TxnObject = entity.TxnObject ?? string.Empty,
                Status = entity.Status ?? "P",
            }, CommandType.StoredProcedure);
Finish:
            return response ?? new InitiateTransactionResponse();
        }

        public async Task<TransactionDetailResponse> GetTransactionDetailByTID(int TID)
        {
            string sqlQuery = @"select t.TransactionId,t.RequestedId,t.Amount,t.[Status],u.qrId,u.displayName,t.ServerHookURL,t.WebhookURL from Transactions t(nolock) Inner join UPISetting u(nolock) on u.Id = t.UPISettingId where t.TransactionId=@TID";
            var response = await _dapper.GetAsync<TransactionDetailResponse>(sqlQuery, new
            {
                TID
            }, CommandType.Text);
            return response ?? new TransactionDetailResponse();
        }

        public async Task<UPISettingWithTIDDetail> GetUPISettingByTID(int TID)
        {
            string sqlQuery = @"select t.TransactionId,t.ServerHookUrl,t.RequestedId,t.Amount,t.[Status],t.UTR, t.PaymentApp, t.PayerName, t.PayerVPA, t.PayerPhone, dbo.Custom24hoursFormat(t.EntryOn) EntryOn,u.*  from Transactions t(nolock) Inner join UPISetting u(nolock) on u.Id = t.UPISettingId where t.TransactionId=@TID";
            var response = await _dapper.GetAsync<UPISettingWithTIDDetail>(sqlQuery, new
            {
                TID
            }, CommandType.Text);
            return response ?? new UPISettingWithTIDDetail();
        }

        public async Task<UPISettingWithTIDDetail> GetUPISettingByRequestedID(string requestedId)
        {
            string sqlQuery = @"select t.TransactionId,t.RequestedId,t.Amount,t.[Status],u.*  from Transactions t(nolock) Inner join UPISetting u(nolock) on u.Id = t.UPISettingId where t.RequestedId=@RequestedId";
            var response = await _dapper.GetAsync<UPISettingWithTIDDetail>(sqlQuery, new
            {
                requestedId
            }, CommandType.Text);
            return response ?? new UPISettingWithTIDDetail();
        }

        public async Task<Response> UpdateTransactionStatus(UpdateTransactionStatusRequest request)
        {
            string sqlQuery = @"UPDATE Transactions  set IsServerHookProceed = @isServerhook, [Status] = @Status,TxnObject=@TxnObject,UTR=@UTR,Remark=@Remark,PayerPhone=@PayerPhone,PayerName=@PayerName,PayerVPA=@PayerVPA,PaymentApp=@PaymentApp where TransactionId=@TID";
            int i = await _dapper.ExecuteAsync(sqlQuery, new
            {
                request.TID,
                request.Status,
                request.TxnObject,
                request.UTR,
                request.Remark,
                request.PayerPhone,
                request.PayerName,
                request.PayerVPA,
                request.PaymentApp,
                request.isServerhook
            }, CommandType.Text);
            var response = new Response();
            if (i==0)
            {
                response.StatusCode = ResponseStatus.Failed;
                response.ResponseText = "No TID found for updation.";
            }
            else if (i > 0 && i < 50)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
            }
            else
            {
                response.ResponseText = "Something went wrong.";
            }
            return response;
        }

        public async Task<TransactionStatusResponse> GetTransactionStatus(string requestedId)
        {
            var trDetail = await _dapper.GetAsync<TransactionStatusResponse>("Proc_GetTransactionStatus", new
            {
                requestedId
            }, CommandType.StoredProcedure);
            return trDetail;
        }

        public char PaymentState(string status)
        {
            status = string.IsNullOrEmpty(status) ? "PENDING" : status;
            Dictionary<string, char> dictionary = new Dictionary<string, char>
            {
                {"FAILED", 'F'},
                {"SUCCESS", 'S'},
                {"PENDING", 'P'},
            };
            dictionary.TryGetValue(status.ToUpper(), out char value);
            value = value=='\0' ? 'P' : value;
            return value;
        }
        public string TransactionStatus(char status)
        {
            Dictionary<char, string> dictionary = new Dictionary<char, string>
            {
                {'F', "FAILED"},
                {'S', "SUCCESS"},
                {'P', "PENDING"}
            };
            dictionary.TryGetValue(status, out string value);
            return value;
        }
        public async Task<IEnumerable<StatusCheckJobRequest>> GetAllTransactionsAsync()
        {
            string sp = "Proc_GetTransactionStatusJob";
            var res = await _dapper.GetAllAsync<StatusCheckJobRequest>(sp, null, CommandType.StoredProcedure);
            return res ?? new List<StatusCheckJobRequest>();
        }
        public async Task<Response> UpdateTransServerHook(int TID)
        {
            string sqlQuery = @"update Transactions Set IsServerHookProceed = 1 where TransactionId=@TID";
            int i = await _dapper.ExecuteAsync(sqlQuery, new { TID }, CommandType.Text);
            var response = new Response();
            if (i == 0)
            {
                response.StatusCode = ResponseStatus.Failed;
                response.ResponseText = "No TID found for updation.";
            }
            else if (i > 0 && i < 50)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
            }
            else
            {
                response.ResponseText = "Something went wrong.";
            }
            return response;
        }
        public async Task<Response> UpdateTransStatusCheck(int TID)
        {
            string sqlQuery = @"update Transactions Set StatusCheckCount = ISNULL(StatusCheckCount,0) + 1 where TransactionId=@TID";
            int i = await _dapper.ExecuteAsync(sqlQuery, new { TID }, CommandType.Text);
            var response = new Response();
            if (i == 0)
            {
                response.StatusCode = ResponseStatus.Failed;
                response.ResponseText = "No TID found for updation.";
            }
            else if (i > 0 && i < 50)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
            }
            else
            {
                response.ResponseText = "Something went wrong.";
            }
            return response;
        }

        public string PaymentUrl(TransactionDetailResponse transactionDetail)
        {
            string transactionId = AppUtility.O.CreateTransactionId(transactionDetail.TransactionId);
            StringBuilder url = new StringBuilder(_apiConfig.qrUrl);
            url.Replace("{{QRID}}", transactionDetail.QRID);
            url.Replace("{{displayname}}", transactionDetail.DisplayName);
            url.Replace("{{Amount}}", transactionDetail.Amount.ToString());
            url.Replace("{{transactionNote}}", transactionId);
            url.Replace("{{merchantTransactionId}}", transactionId);
            return url.ToString();
        }

        public async Task CreateCallbackLog(string requestedId, string request, string response, HookType hookType)
        {
            try
            {
                string sqlQuery = @"insert into CallbackHittingLog(RequestedId,Request,Response,EntryOn,HookType) values(@requestedId,@request,@response,GetDate(),@hookType)";
                await _dapper.ExecuteAsync(sqlQuery, new { requestedId, request, response, hookType = (char)hookType }, CommandType.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
