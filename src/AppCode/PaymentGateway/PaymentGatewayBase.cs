﻿using Dapper;
using LinqToDB.Reflection;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.PaymentGateway
{
    public abstract class PaymentGatewayBase : IPaymentGatewayService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IDapperRepository _dapper;
        public PaymentGatewayBase(ILogger logger, IDapperRepository dapper)
        {
            _logger = logger;
            _dapper = dapper;
        }

        public virtual async Task<Response<CashFree.Models.CashFreeResponseForApp>> GeneratePGRequestForAppAsync(PaymentGatewayRequest request)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<Response<PaymentGatewayResponse>> GeneratePGRequestForWebAsync(PaymentGatewayRequest request)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<Response<StatusCheckResponse>> StatusCheck(StatusCheckRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void LogError(string errorMsg, string className, string methodName)
        {
            _logger.LogError(errorMsg, new { className, fn = methodName });
        }

        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public Task<Response<int>> SaveInitiatePayment(PaymentGatewayRequest request, int packageId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<PaymentGatewayRequest>> GetInitiatedPaymentDetail(int TID)
        {
            var response = new Response<PaymentGatewayRequest>
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = ResponseStatus.Failed.ToString(),
                Result = new PaymentGatewayRequest()
            };
            string sqlQuery = @"select * from InitiatePayment where TID = @TID";
            response.Result = await _dapper.GetAsync<PaymentGatewayRequest>(sqlQuery, new { TID }, System.Data.CommandType.Text);
            if (response.Result.Amount > 0)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
            }
            return response;
        }


        public async Task<Response> updateInitiatedPayment(int TID, string status)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = ResponseStatus.Failed.ToString()
            };
            string sqlQuery = @"update InitiatePayment SET [Status] = @status where TID = @TID";
            int i = await _dapper.ExecuteAsync(sqlQuery, new { TID, status }, System.Data.CommandType.Text);
            if (i > -1)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Failed.ToString();
            }
            return response;
        }

        public async Task<PaymentGatewayModel> GetConfiguration(PaymentGatewayType pg)
        {
            string sqlQuery = @"Select * from PaymentGatwaydetails(nolock) where PGId=@pg";
            var response = await _dapper.GetAsync<PaymentGatewayModel>(sqlQuery, new { pg }, commandType: CommandType.Text);
            return response ?? new PaymentGatewayModel();
        }
    }
}
