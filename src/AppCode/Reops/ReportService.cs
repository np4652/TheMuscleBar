using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog.Filters;
using System;
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
    public class ReportService : IReportService
    {
        private IDapperRepository _dapper;
        private readonly ILogger<DapperRepository> _logger;
        public ReportService(IDapperRepository dapper, ILogger<DapperRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }

        public async Task<IEnumerable<APIModel>> GetAPIlog()
        {
            string sqlQuery = @"Select id,Request,Response,convert(varchar,EntryOn,113)EntryOn,Method, TID, IsIncomingOutgoing from APILog(nolock) order by Id desc";

            var res = await _dapper.GetAllAsync<APIModel>(sqlQuery, null, CommandType.Text);
            return res ?? new List<APIModel>();
        } 
        public async Task<JDataTable<APIModel>> GetAPIlogs(JSONAOData jsonAOData = null)
        {
            JDataTable<APIModel> d = new JDataTable<APIModel>();
            try
            {
                string sp = "Proc_GetAPILogs";
                d = await _dapper.GetJDatTableAsync<APIModel>(sp, jsonAOData, CommandType.StoredProcedure);
                d.recordsFiltered = d.PageSetting.TotoalRows;                                                             
                d.recordsTotal = d.PageSetting.TotoalRows;
            }
            catch (Exception ex){}
            return d;
        }
        public async Task<IEnumerable<APIModel>> GetAPIlogByID(string TID)
        {
            string sqlQuery = @"Select Id,Request,Response,dbo.CustomFormat(EntryOn) EntryOn,Method,TID from APILog where TID = @TID order by EntryOn desc";
            var res = await _dapper.GetAllAsync<APIModel>(sqlQuery, new { TID }, CommandType.Text);
            return res ?? new List<APIModel>();
        }
        public async Task<IEnumerable<ErrorModel>> GetErrorlog()
        {
            string sqlQuery = @"Select * from NLogs (nolock)order by Id desc";
            var res = await _dapper.GetAllAsync<ErrorModel>(sqlQuery, null, CommandType.Text);
            return res ?? new List<ErrorModel>();
        }
        public async Task<IEnumerable<TransactionReport>> GetTransactionReport(TransactionReportRequest request)
        {
            var res = new List<TransactionReport>();
            try
            {
                var response = await _dapper.GetAllAsync<TransactionReport>("proc_GetTransactionReport", request, CommandType.StoredProcedure);
                res = response.ToList();
            }
            catch(Exception ex)
            {

            }
            return res ?? new List<TransactionReport>();
        }

        public async Task<APIUserCounts> GetUserDashboardsummary(int UserId)
        {
            return await _dapper.GetAsync<APIUserCounts>("proc_Dashboardsummary", new { userId = UserId, roleId = Role.Customer }, System.Data.CommandType.StoredProcedure);
        }

        public async Task<APIUserCounts> GetUserDashboardAmnt(int UserId)
        {
            return await _dapper.GetAsync<APIUserCounts>("proc_Dashboardsummary", new { userId = UserId, roleId = Role.Customer }, System.Data.CommandType.StoredProcedure);
        }
        public async Task<AdminUserCounts> GetAdminDashboardsummary(int UserId)
        {
            return await _dapper.GetAsync<AdminUserCounts>("proc_Dashboardsummary", new { userId = UserId, roleId = Role.Admin }, System.Data.CommandType.StoredProcedure);
        }

        public async Task<AdminUserCounts> GetAdminDashboardAmnt(int UserId)
        {
            return await _dapper.GetAsync<AdminUserCounts>("proc_Dashboardsummary", new { userId = UserId, roleId = Role.Admin }, System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ChartData>> GetAPIDashboardGraphData(int UserId)
        {
            try
            {
                var res = await _dapper.GetAllAsync<ChartData>("proc_getDashboardGraphData", new { UserId }, CommandType.StoredProcedure);
                return res ?? new List<ChartData>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<ChartData>();
            }
            
        }
        public async Task<DonutData> GetExpiredActive(int UserId)
        {
            string sp = "proc_DashboardDonutChart";
            var res = await _dapper.GetAsync<DonutData>(sp, new { UserId }, CommandType.StoredProcedure);
            return res ?? new DonutData();
        }
        public async Task<PieChart> GetPieChart(int UserId, int Type)
        {
            string sp = "proc_DashboardPieChart";
            var res = await _dapper.GetAsync<PieChart>(sp, new { UserId, Type }, CommandType.StoredProcedure);
            return res ?? new PieChart();
        }
        public async Task<IEnumerable<CallBackHitLog>> GetAllCallBackHit()
        {
            try
            {
                string sqlQuery = @"Select RequestedId,Request,Response,dbo.CustomFormat(EntryOn) EntryOn,HookType from CallbackHittingLog order by EntryOn desc";
                var res = await _dapper.GetAllAsync<CallBackHitLog>(sqlQuery, null, CommandType.Text);
                return res ?? new List<CallBackHitLog>();
            }
            catch(Exception ex)
            {

            }
            return new List<CallBackHitLog>();
        }
    }
}
