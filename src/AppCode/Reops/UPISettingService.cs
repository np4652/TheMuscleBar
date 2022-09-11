using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using static Dapper.SqlMapper;

namespace TheMuscleBar.AppCode.Reops
{
    public class UPISettingService : IUPISettingService
    {
        private IDapperRepository _dapper;
        public UPISettingService(IDapperRepository dapper) => _dapper = dapper;
        public async Task<Response> AddAsync(UPISetting entity)
        {
            Response response = new Response();
            try
            {
                response = await _dapper.GetAsync<Response>("proc_addUPISetting", new
                {
                    entity.Id,
                    entity.Mobile,
                    entity.UUID,
                    entity.DeviceFingerprint,
                    UserId = entity.UserId ?? string.Empty,
                    AuthToken = entity.AuthToken ?? string.Empty,
                    Refreshtoken = entity.Refreshtoken ?? string.Empty,
                    UserGroupId = entity.UserGroupId ?? string.Empty,
                    MerchantId = entity.MerchantId ?? string.Empty,
                    StoreId = entity.StoreId ?? string.Empty,
                    QrId = entity.QrId ?? string.Empty,
                    DisplayName = entity.DisplayName ?? string.Empty,
                    entity.EntryBy
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public Task<Response> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UPISetting>> GetAllAsync(UPISetting entity = null, int loginId = 0)
        {
            string sqlQuery = @"Select * from UPISetting(nolock)";
            entity = entity == null ? new UPISetting() : entity;
            var res = await _dapper.GetAllAsync<UPISetting>(entity, sqlQuery);
            return res ?? new List<UPISetting>();
        }

        public async Task<Response<UPISetting>> GetByIdAsync(int id)
        {
            var response = new Response<UPISetting>
            {
                StatusCode = ResponseStatus.Failed
            };
            string sqlQuery = @"Select * from UPISetting(nolock) where Id=@Id";
            var result = await _dapper.GetAsync<UPISetting>(sqlQuery, new { id }, commandType: CommandType.Text);
            if (result != null)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
                response.Result = result;
            }
            return response;
        }

        public Task<IReadOnlyList<UPISetting>> GetDropdownAsync(UPISetting entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<UPISetting>> GetUPISettingByMobile(string mobile)
        {
            var response = new Response<UPISetting>();
            string sqlQuery = @"Select * from UPISetting(nolock) where mobile=@mobile and ISNULL(IsLoggedOut,0) = 0";
            var result = await _dapper.GetAsync<UPISetting>(sqlQuery, new { mobile }, commandType: CommandType.Text);
            if (result != null)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText =  "Already configured";
                response.Result = result;
            }
            return response;
        }

        public async Task<Response> LogoutUPI(string mobileno)
        {
          
            string sqlQuery = @"UPDATE UPISetting SET IsLoggedOut = 1 where mobile=@mobileno";
            int i = await _dapper.ExecuteAsync(sqlQuery, new { mobileno }, commandType: CommandType.Text);
            var response = new Response();
            if (i > -1)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
            }
            return response;
        }

        public async Task<Response> UpdateRefreshToken(UPISetting entity)
        {
            Response response = new Response();
            try
            {
                response = await _dapper.GetAsync<Response>("proc_UpdateRefreshToken", new
                {
                    entity.Id,
                    entity.Mobile,
                    entity.AuthToken,
                    entity.Refreshtoken,
                    entity.EntryBy
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public async Task<UPISetting> GetUPISettingByQRId(string QRID)
        {
            string sqlQuery = @"Select * from UPISetting(nolock) where qrId=@QRID";
            var result = await _dapper.GetAsync<UPISetting>(sqlQuery, new { QRID }, commandType: CommandType.Text);
            return result;
        }

        public async Task<bool> IsAnyConfigurationExists(int userId)
        {
            string sqlQuery = @"Declare @IsExsits bit = 0; Select @IsExsits = 1 from UPISetting Where EntryBy = @userId; Select @IsExsits";
            var result = await _dapper.GetAsync<bool>(sqlQuery, new { userId }, commandType: CommandType.Text);
            return result;
        }
    }
}