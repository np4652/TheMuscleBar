using Dapper;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Reops
{
    public class PackageService : IPackageService
    {
        private IDapperRepository _dapper;
        public PackageService(IDapperRepository dapper) => _dapper = dapper;
        public async Task<Response> AddAsync(Package entity)
        {
            return await _dapper.GetAsync<Response>("proc_EditPackage", entity, commandType: CommandType.StoredProcedure);
        }

        public Task<Response> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Package>> GetAllAsync(Package entity = null, int loginId = 0)
        {
            string sqlQuery = @"Select * from Package(nolock) order by ind ";
            var res = await _dapper.GetAllAsync<Package>(sqlQuery, entity, CommandType.Text);
            return res ?? new List<Package>();
        }

        public async Task<IEnumerable<BuypackageViewModel>> GetPackage(int userId)
        {
            string sqlQuery = @"Select packageId,EntryOn into #userPackage from UserPackage where UserId = @userId
                                select p.*,IIF(u.PackageId is NULL,0,1) Activated,DATEDIFF(DAY,GETDATE() - P.Validity,u.EntryOn) ExpiredOn  
                                from Package p(nolock) Left Join #userPackage u on u.PackageId = p.PackageId where p.IsActive = 1 order by p.Ind";
            var res = await _dapper.GetAllAsync<BuypackageViewModel>(sqlQuery, new { userId }, commandType: CommandType.Text);
            return res ?? new List<BuypackageViewModel>();
        }


        public async Task<Response<Package>> GetByIdAsync(int PackageId)
        {
            var response = new Response<Package>
            {
                StatusCode = ResponseStatus.Failed
            };
            var dbparams = new DynamicParameters();
            dbparams.Add("PackageId", PackageId);
            string sqlQuery = @"Select * from Package(nolock) where PackageId=@PackageId";
            var result = await _dapper.GetAsync<Package>(sqlQuery, dbparams, commandType: CommandType.Text);
            if (result != null)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
                response.Result = result;
            }
            return response;
        }

        public Task<IReadOnlyList<Package>> GetDropdownAsync(Package entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response> ChangeActiveStatus(int packageId)
        {

            string sqlQuery = @"UPDATE package SET IsActive = 1^IsActive Where PackageId =@packageId";
            int i = await _dapper.ExecuteAsync(sqlQuery, new { packageId }, CommandType.Text);
            var response = new Response();
            if (i > -1)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = ResponseStatus.Success.ToString();
            }
            return response;
        }
    }
}
