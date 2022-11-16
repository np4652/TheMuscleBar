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
using TheMuscleBar.Models.ViewModel;

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



        public async Task<IEnumerable<ErrorModel>> GetErrorlog()
        {
            string sqlQuery = @"Select * from NLogs (nolock)order by Id desc";
            var res = await _dapper.GetAllAsync<ErrorModel>(sqlQuery, null, CommandType.Text);
            return res ?? new List<ErrorModel>();
        }

        public async Task<IEnumerable<Legder>> GetLedger()
        {
            string sqlQuery = @"Select u.[Name] UserName,u.PhoneNumber , l.Id,l.UserId,l.TransactionType,l.Amount,l.discount,l.PaymentMode,Convert(varchar,l.EntryOn,106) EntryOn from Ledger l(nolock) inner join Users u(nolock) on u.Id = l.UserId order by l.Id desc";
            var res = await _dapper.GetAllAsync<Legder>(sqlQuery, null, CommandType.Text);
            return res ?? new List<Legder>();
        }

        public async Task<IEnumerable<SubscripitionReport>> GetSubscripitionReports()
        {
            string sqlQuery = @"Select u.[Name] UserName,u.PhoneNumber, us.UserId,us.LedgerId,Convert(varchar,us.DateFrom,106) DateFrom,Convert(varchar,us.DateTo,106) DateTo  
from UserSubscription us(nolock) inner join users u(nolock) on u.Id = us.UserId order by us.id desc";
            var res = await _dapper.GetAllAsync<SubscripitionReport>(sqlQuery, null, CommandType.Text);
            return res ?? new List<SubscripitionReport>();
        }

        public async Task<Invoice> GetInvoice(int tid)
        {
            string sqlQuery = @"Select 'TID' + RIGHT('0000000' + CAST(l.Id AS VARCHAR(7)), 7) TransactionId,u.[Name] UserName,u.PhoneNumber,l.TransactionType,CAST(l.Amount AS int) Amount,l.UserId ,
l.discount,l.PaymentMode,Convert(varchar,l.EntryOn,106) EntryOn ,Convert(varchar,us.DateFrom,106) FromDate,Convert(varchar,us.DateTo,106) ToDate  from Ledger l(nolock) 
inner join Users u(nolock) on u.Id = l.UserId inner join UserSubscription us(nolock) on l.UserId=us.UserId and l.Id=us.LedgerId  where l.Id=@tid order by l.Id desc";
            var res = await _dapper.GetAsync<Invoice>(sqlQuery, new { tid }, CommandType.Text);
            return res ?? new Invoice();
        }

        public async Task<DashboardSummery> GetDashboardSummery()
        {
            var res = await _dapper.GetAsync<DashboardSummery>("Proc_GetDashboardSummery", new { }, commandType: CommandType.StoredProcedure);
            return res ?? new DashboardSummery();
        }
        public async Task<IEnumerable<AttendanceView>> GetAttendance(string fromdate, string todate, int id = 0)
        {

            var res = await _dapper.GetAllAsync<AttendanceView>("proc_selectAttendance", new {userid=id,Fromdate=fromdate ,Todate=todate}, commandType: CommandType.StoredProcedure);
            return res ?? new List<AttendanceView>();
        }
    public async Task<IEnumerable<UserList>> GetUsersList()
    {

        string sqlQuery = @"select ID,Name from  Users where Id<>1 order by Name ";
        var res = await _dapper.GetAllAsync<UserList>(sqlQuery, null, CommandType.Text);
        return res ?? new List<UserList>();
    }
}
}
