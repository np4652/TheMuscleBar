﻿using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.AppCode.DAL;
using Dapper;
using System.Data;
using TheMuscleBar.AppCode.Enums;

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

        public async Task<Response> ChangeAction(int id)
        {

            string sqlQuery = @"UPDATE Users SET IsActive = 1^IsActive Where id = @id";
            int i = await _dapper.ExecuteAsync(sqlQuery, new { id }, CommandType.Text);
            var response = new Response();
            if (i > -1)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
            }
            return response;
        }

        public async Task<Response> AssignPackage(int userId, int packageId)
        {
            var response = await _dapper.GetAsync<Response>("Proc_AssignPackage", new { userId, packageId }, CommandType.StoredProcedure);
            return response;
        }

        public async Task<Response> Assignpackage(int TID)
        {
            var response = await _dapper.GetAsync<Response>("proc_UpdatePayment", new { TID }, commandType: CommandType.StoredProcedure);
            return response;
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
    }
}