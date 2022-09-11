using Hangfire.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Extensions;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ReportController : Controller
    {
        private readonly IReportService _apiService;
        public ReportController(IReportService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> APILog()
        {
            var response = await _apiService.GetAPIlog();
            var entity = response;
            return PartialView(entity ?? new List<APIModel>());
        }
        [HttpPost]
        public async Task<IActionResult> APILogs(JSONAOData jsonAOData, string Col, bool? IncomingOnly, string SearchVal = "")
        {
            jsonAOData.param = new
            {
                Col,
                IncomingOnly
            };
            if(!string.IsNullOrEmpty(SearchVal))
                jsonAOData.search.value = SearchVal;
            var response = await _apiService.GetAPIlogs(jsonAOData);
            var entity = new
            {
                draw = response.draw,
                recordsTotal = response.recordsTotal,
                recordsFiltered = response.recordsFiltered,
                PageSetting = response.PageSetting,
                Data = response.Data.Select(x => new
                {
                    x.Id,
                    x.Request,
                    x.Response,
                    x.TID,
                    x.IsIncomingOutgoing,
                    x.EntryOn,
                    x.Method
                })
            };

            return Json(entity);
        }


        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ErrorLog()
        {
            var response = await _apiService.GetErrorlog();
            var entity = response;
            return PartialView(entity ?? new List<ErrorModel>());
        }
        public IActionResult Transaction()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TransactionReport(TransactionReportRequest request)
        {
            request.UserId = User.GetLoggedInUserId<int>();
            var response = await _apiService.GetTransactionReport(request);
            var entity = response;
            return PartialView(entity ?? new List<TransactionReport>());
        }
        [HttpPost]
        public async Task<IActionResult> GetTransactionReqRes(string TID)
        {
            return PartialView(await _apiService.GetAPIlogByID(TID));
        }
        public IActionResult CallBackReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CallBackReportList()
        {
            return PartialView(await _apiService.GetAllCallBackHit());
        }
    }
}
