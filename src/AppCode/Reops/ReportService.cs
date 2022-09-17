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
            string sqlQuery = @"Select u.[Name] UserName,u.PhoneNumber, us.UserId,us.LedgerId,Convert(varchar,us.DateFrom,106) DateFrom,Convert(varchar,us.DateTo,106) DateTo  from UserSubscription us(nolock) inner join users u(nolock) on u.Id = us.UserId";
            var res = await _dapper.GetAllAsync<SubscripitionReport>(sqlQuery, null, CommandType.Text);
            return res ?? new List<SubscripitionReport>();
        }
    }
}
