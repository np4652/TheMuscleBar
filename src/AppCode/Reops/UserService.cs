using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using System.Data;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models.ViewModel;

namespace TheMuscleBar.AppCode.Reops
{
    public class UserService : IUserService
    {
        private IDapperRepository _dapper;
        public UserService(IDapperRepository dapper)
        {
            _dapper = dapper;
        }

        public async Task<Response> AddAsync(ApplicationUser entity)
        {
            var res = await _dapper.ExecuteAsync("UpdateUser", entity, commandType: CommandType.StoredProcedure);
            return new Response
            {
                StatusCode = res != -1 ? ResponseStatus.Success : ResponseStatus.Failed,
                ResponseText = res != -1 ? ResponseStatus.Success.ToString() : ResponseStatus.Failed.ToString(),
            };
        }

        public Task<Response> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync(ApplicationUser entity = null, int loginId = 0)
        {
            string sqlQuery = @"select Id ,Email,PhoneNumber,UserName,TwoFactorEnabled,[Name],IsActive,MembershipType,[Address],DOB,AdharNo,MaritalStatus,EntryOn from Users(nolock) order by Id desc";
            var res = await _dapper.GetAllAsync<ApplicationUser>(sqlQuery, entity, CommandType.Text);
            return res ?? new List<ApplicationUser>();
        }

        public async Task<Response<ApplicationUser>> GetByIdAsync(int id)
        {
            Response<ApplicationUser> res = new Response<ApplicationUser>();
            try
            {
                var result = await _dapper.GetAsync<ApplicationUser>("proc_users", new { UserID = id }, commandType: CommandType.StoredProcedure);
                res = new Response<ApplicationUser>
                {
                    StatusCode = ResponseStatus.Success,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                res = new Response<ApplicationUser>
                {
                    StatusCode = ResponseStatus.Failed,
                    ResponseText = ResponseStatus.Failed.ToString(),
                    Result = new ApplicationUser(),
                    Exception = ex
                };
            }
            return res;
        }

        public Task<IReadOnlyList<ApplicationUser>> GetDropdownAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CollectFeeResponse> CollectFee(CollectFee collectFee)
        {
            var res = new CollectFeeResponse();
            try
            {
                DateTime fromDate = DateTime.Now;
                DateTime.TryParseExact(collectFee.FromDate, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDate);
                switch (collectFee.MembershipType)
                {
                    case MembershipType.Monthly:
                        collectFee.ToDate = fromDate.AddMonths(1).ToString("dd MMM yyyy");
                        break;
                    case MembershipType.Quarterly:
                        collectFee.ToDate = fromDate.AddMonths(3).ToString("dd MMM yyyy");
                        break;
                    case MembershipType.HalfYearly:
                        collectFee.ToDate = fromDate.AddMonths(6).ToString("dd MMM yyyy");
                        break;
                    case MembershipType.Yearly:
                        collectFee.ToDate = fromDate.AddMonths(12).ToString("dd MMM yyyy");
                        break;
                }
                res = await _dapper.GetAsync<CollectFeeResponse>("proc_CollectFee", new
                {
                    collectFee.UserId,
                    collectFee.Amount,
                    collectFee.Discount,                    
                    collectFee.FromDate,
                    collectFee.ToDate,
                    collectFee.PaymentMode,
                    collectFee.EntryBy,
                    collectFee.PhoneNumber
                }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return res;
        }


        public async Task<IEnumerable<UnSubscribedUser>> GetSubscriptionExpired()
        {
            try
            {
                var res = await _dapper.GetAllAsync<UnSubscribedUser>("proc_proc_SubscriptionbyUser", null, CommandType.StoredProcedure);
                return res ?? new List<UnSubscribedUser>();
            }
            catch (Exception ex)
            {
                return new List<UnSubscribedUser>();
            }
        }

        public async Task<UserDetailsReturn> GetSubscriptionByuser(int userid)
        {
            var res = await _dapper.GetAsync<UserDetailsReturn>("proc_SubscriptionbyUser", new { UserID = userid }, CommandType.StoredProcedure);
            return res ?? new UserDetailsReturn();
        }
        public async Task<Response> SaveApiLog(ApiModel req)
        {
            var res = await _dapper.ExecuteAsync("proc_SaveApiLog", req, commandType: CommandType.StoredProcedure);
            return new Response
            {
                StatusCode = res != -1 ? ResponseStatus.Success : ResponseStatus.Failed,
                ResponseText = res != -1 ? ResponseStatus.Success.ToString() : ResponseStatus.Failed.ToString(),
            };
        }
    }
}