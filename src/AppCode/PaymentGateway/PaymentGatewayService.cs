using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.PaymentGateway.CashFree;
using TheMuscleBar.Models;
using static TheMuscleBar.AppCode.PaymentGateway.CashFree.Models;

namespace TheMuscleBar.AppCode.PaymentGateway
{
    public class PaymentGatewayService : PaymentGatewayBase, IPaymentGatewayService
    {
        private readonly ILogger<PaymentGatewayService> _logger;
        private readonly IDapperRepository _dapper;
        private readonly IMapper _mapper;
        private readonly IAPILogin _apiLogin;
        public PaymentGatewayService(ILogger<PaymentGatewayService> logger, IDapperRepository dapper, IMapper mapper, IAPILogin aPILogin) : base(logger, dapper)
        {
            _logger = logger;
            _dapper = dapper;
            _mapper = mapper;
            _apiLogin = aPILogin;
        }

        public IPaymentGatewayService Gateway(PaymentGatewayType gatewayType)
        {
            IPaymentGatewayService service = null;
            switch (gatewayType)
            {
                case PaymentGatewayType.CASHFREE:
                    return new CashFreeService(_logger, _dapper, _mapper, _apiLogin);

            }
            return service;
        }

        public Task<Response<CashFreeResponseForApp>> GeneratePGRequestForAppAsync(PaymentGatewayRequest request)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<Response<PaymentGatewayResponse>> GeneratePGRequestForWebAsync(PaymentGatewayRequest request)
        {
            var obj = Gateway(request.PGID);
            obj.GeneratePGRequestForAppAsync(request);
            var res = new Response<PaymentGatewayResponse>
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = ResponseStatus.Failed.ToString(),
                Result = new PaymentGatewayResponse()
            };
            switch (request.PGID)
            {
                case PaymentGatewayType.CASHFREE:
                    using (var cashFree = new CashFreeService(_logger, _dapper,_mapper, _apiLogin))
                    {
                        res = await cashFree.GeneratePGRequestForWebAsync(request);
                    }
                    break;
            }
            return res;
        }

        public async Task<Response<int>> SaveInitiatePayment(PaymentGatewayRequest request, int packageId)
        {
            Response<int> response = new Response<int>
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = ResponseStatus.Failed.ToString()
            };
            try
            {
                string sqlQuery = @"insert into InitiatePayment(PGID,Amount,PackageId,UserId,EntryOn,[Status]) Values (@PGID,@Amount,@PackageId,@UserId,GETDATE(),'P')
                                    Select SCOPE_IDENTITY()";
                int tid = await _dapper.GetAsync<int>(sqlQuery, new
                {
                    request.PGID,
                    request.Amount,
                    packageId,
                    request.UserID
                }, System.Data.CommandType.Text);
                if (tid > 0)
                {
                    response.StatusCode = ResponseStatus.Success;
                    response.ResponseText = ResponseStatus.Success.ToString();
                    response.Result = tid;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { className = this.GetType().Name, fn = nameof(SaveInitiatePayment) });
            }
            return response;
        }

        public async Task<Response<StatusCheckResponse>> StatusCheck(StatusCheckRequest request)
        {
            Response<StatusCheckResponse> res = new Response<StatusCheckResponse>
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = ResponseStatus.Failed.ToString(),
                Result = new StatusCheckResponse()
            };
            switch (request.PGID)
            {
                case PaymentGatewayType.CASHFREE:
                    using (var cashFree = new CashFreeService(_logger, _dapper, _mapper, _apiLogin))
                    {
                        res = await cashFree.StatusCheck(request);
                    }
                    break;
            }
            return res;
        }
    }
}
