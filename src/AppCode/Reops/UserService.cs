using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using System.Data;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Reops.Entities;

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

        public async Task<Response<string>> CollectFee(CollectFee collectFee)
        {
            var res = new Response<string>();
            try
            {
                res = await _dapper.GetAsync<Response<string>>("proc_CollectFee", new
                {
                    collectFee.UserId,
                    TransactionType = (char)collectFee.TransactionType,
                    collectFee.PaymentMode,
                    collectFee.FromDate,
                    collectFee.ToDate,
                    collectFee.Amount,
                    collectFee.Discount,
                    collectFee.EntryBy
                }, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
    }
}