using System.Threading.Tasks;
using TheMuscleBar.AppCode.DAL;
using TheMuscleBar.AppCode.Interfaces;

namespace TheMuscleBar.AppCode.Reops
{
    public class APILogin : IAPILogin
    {
        private IDapperRepository _dapper;
        public APILogin(IDapperRepository dapper)
        {
            _dapper = dapper;
        }
        public async Task<bool> SaveLog(string request, string response, string method, string tid = "", bool IsIncmOut = false, string CallingFrom = "")
        {
            bool res = false;
            int i = await _dapper.ExecuteAsync("insert into APILog(Request,Response,Method,EntryOn,TID,IsIncomingOutgoing,CallingFrom) Values (@request,@response,@method,getDate(),@tid,@IsIncmOut,@CallingFrom)", new
            {
                request,
                response,
                method,
                tid,
                IsIncmOut,
                CallingFrom
            }, System.Data.CommandType.Text);
            if (i > 0)
            {
                res = true;
            }
            return res;
        }
    }
}
