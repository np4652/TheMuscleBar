using Dapper;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.AppCode.Reops
{
    public class EmailConfigRepo : IRepository<EmailConfig>
    {
        private IDapperRepository _dapper;
        public EmailConfigRepo(IDapperRepository dapper)
        {
            _dapper = dapper;
        }
        public async Task<Response> AddAsync(EmailConfig entity)
        {
            string sqlQuery = @"insert into EmailConfig(EmailFrom,[Password],EnableSSL,[Port] ,HostName,UserId ) values (@EmailFrom,@Password,@EnableSSL,@Port ,@HostName,@UserId );";
            if (entity.Id > 0)
                sqlQuery = @"Update EmailConfig SET [Password]=@Password , EnableSSL=@EnableSSL,UserId=@UserId where Id=@Id;";
            var res = await _dapper.ExecuteAsync(sqlQuery, entity, commandType: CommandType.Text);
            return new Response
            {
                StatusCode = res != -1 ? ResponseStatus.Success : ResponseStatus.Failed,
                ResponseText = res != -1 ? ResponseStatus.Success.ToString() : ResponseStatus.Failed.ToString(),
            };
        }




        public async Task<Response> DeleteAsync(int id)
        {
            //Response res = new Response();
            //try
            //{
            //    var dbparams = new DynamicParameters();
            //    dbparams.Add("CategoryID", id);
            //    dbparams.Add("CategoryName", "");
            //    dbparams.Add("ParentID", 0);
            //    dbparams.Add("Icon", "");
            //    dbparams.Add("QueryType", "D");
            //    res = await _dapper.GetAsync<Response>("proc_Category", dbparams, commandType: CommandType.StoredProcedure);
            //}
            //catch (Exception ex)
            //{
            //    res.Exception = ex;
            //}
            //return res;
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<EmailConfig>> GetAllAsync(EmailConfig entity = null, int loginId = 0)
        {
            string sqlQuery = @"Select * from EmailConfig(nolock)";
            entity = entity == null ? new EmailConfig() : entity;
            var res = await _dapper.GetAllAsync<EmailConfig>(entity, sqlQuery);
            return res ?? new List<EmailConfig>();
        }

        public async Task<Response<EmailConfig>> GetByIdAsync(int id)
        {
            var response = new Response<EmailConfig>
            {
                StatusCode = ResponseStatus.Failed
            };
            var dbparams = new DynamicParameters();
            dbparams.Add("ID", id);
            string sqlQuery = @"Select * from EmailConfig(nolock) where ID=@Id";
            var result = await _dapper.GetAsync<EmailConfig>(sqlQuery, dbparams, commandType: CommandType.Text);
            if (result != null)
            {
                response = new Response<EmailConfig>
                {
                    StatusCode = ResponseStatus.Success,
                    Result = result
                };
            }
            return response;
        }
        

        public Task<IReadOnlyList<EmailConfig>> GetDropdownAsync(EmailConfig entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
