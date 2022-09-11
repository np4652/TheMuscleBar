using Dapper;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheMuscleBar.AppCode.Helper;

namespace TheMuscleBar.AppCode.Reops
{
    //public class UsersRepo : IUserService
    //{
    //    private IDapperRepository _dapper;
    //    private IMapper _mapper;
    //    public UsersRepo(IDapperRepository dapper, IMapper mapper)
    //    {
    //        _dapper = dapper;
    //        _mapper = mapper;
    //    }



    //    public async Task<Response> AddAsync(ApplicationUser entity)
    //    {
    //        var param = _mapper.Map<ApplicationUserProcModel>(entity);
    //        var res = await _dapper.ExecuteAsync("UpdateUser", param, commandType: CommandType.StoredProcedure);
    //        var response = new Response
    //        {
    //            StatusCode=ResponseStatus.Success,
    //            ResponseText=ResponseStatus.Success.ToString()
    //        };
    //        if (res < 0 || res > 10)
    //        {
    //            response.StatusCode=ResponseStatus.Failed;
    //            response.ResponseText= AppUtility.O.GetErrorDescription(res);
    //        }
    //        return response;
    //    }


    //    public Task<Response> DeleteAsync(int id)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public async Task<IEnumerable<ApplicationUser>> GetAllAsync(ApplicationUser entity = null, int loginId = 0)
    //    {
    //        List<ApplicationUser> res = new List<ApplicationUser>();
    //        entity = entity == null ? new ApplicationUser() : entity;
    //        try
    //        {
    //            var dbparams = new DynamicParameters();

    //            var ires = await _dapper.GetAllAsync<ApplicationUser>("proc_users",
    //                new
    //                {
    //                    userId = entity.Id,
    //                    userName = entity.UserName ?? string.Empty,
    //                    Role = entity.Role ?? string.Empty,
    //                    loginId = loginId,
    //                    OnlyDebtCustomer = entity.OnlyDebtCustomer
    //                }, commandType: CommandType.StoredProcedure);
    //            res = ires.ToList();
    //        }
    //        catch (Exception ex)
    //        { }
    //        return res;
    //    }

    //    public async Task<Response<ApplicationUser>> GetByIdAsync(int id)
    //    {
    //        Response<ApplicationUser> res = new Response<ApplicationUser>();
    //        try
    //        {
    //            var result = await _dapper.GetAsync<ApplicationUser>("proc_users", new { UserID = id }, commandType: CommandType.StoredProcedure);
    //            res = new Response<ApplicationUser>
    //            {
    //                StatusCode = ResponseStatus.Success,
    //                Result = result
    //            };
    //        }
    //        catch (Exception ex)
    //        {
    //            res = new Response<ApplicationUser>
    //            {
    //                StatusCode = ResponseStatus.Failed,
    //                ResponseText = ResponseStatus.Failed.ToString(),
    //                Result = new ApplicationUser(),
    //                Exception = ex
    //            };
    //        }
    //        return res;
    //    }

    //    public Task<IReadOnlyList<ApplicationUser>> GetDropdownAsync(ApplicationUser entity = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<Response> MapFOSToCustomer(int userId, int fosId)
    //    {
    //        var response = new Response
    //        {
    //            StatusCode = ResponseStatus.Success,
    //            ResponseText = ResponseStatus.Success.ToString()
    //        };
    //        string sqlQuery = @"If (Select [Role] from vw_Users where Id = @fosId) <> 'fos'
    //                                RAISERROR ('Requested map is not fos',17, 4);
    //                            Else
    //                                Update Users SET FOSId=@fosId where Id=@userId;";
    //        var res = await _dapper.ExecuteAsync(sqlQuery, new { userId, fosId }, commandType: CommandType.Text);
    //        if (res<0 || res>10)
    //        {
    //            response.StatusCode = ResponseStatus.Failed;
    //            response.ResponseText = "Requested mapid may not be FOS";
    //        }
    //        return response;
    //    }

    //    public async Task<Response<decimal>> GetBalanceAsync(int id)
    //    {
    //        var res = new Response<decimal>
    //        {
    //            StatusCode = ResponseStatus.Success,
    //            ResponseText = ResponseStatus.Success.ToString(),
    //            Result = 0
    //        };
    //        try
    //        {
    //            string sqlQuery = "Select CurrentBalance from UserBalance where UserId =@id";
    //            var result = await _dapper.GetAsync<decimal>(sqlQuery, new { id }, commandType: CommandType.Text);
    //            res.Result = result;
    //        }
    //        catch (Exception ex)
    //        {
    //            res = new Response<decimal>
    //            {
    //                StatusCode = ResponseStatus.Failed,
    //                ResponseText = ResponseStatus.Failed.ToString(),
    //                Result = 0,
    //                Exception = ex
    //            };
    //        }
    //        return res;
    //    }

    //    //public Task<Response> ChangeAction(int id)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}
}
