﻿using Hangfire.Annotations;
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
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ledger()
        {
            return View();
        }

        public async Task<IActionResult> _ledger()
        {
            var response = await _reportService.GetLedger();
            return PartialView(response ?? new List<Legder>());
        }

        public IActionResult Subscriptions()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Attendance()
        {
            var response = await _reportService.GetUsersList();

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> _Attendance(int id,string fromdate,string todate)
        {
            var response = await _reportService.GetAttendance( fromdate, todate, id);

            return PartialView("_attendance", response);
        }
        public async Task<IActionResult> UsersList()
        {
            var response = await _reportService.GetUsersList();

            return View(response);
        }
        public async Task<IActionResult> _Subscriptions(int id, string fromdate, string todate)
        {
            var response = await _reportService.GetSubscripitionReports(fromdate, todate, id);
            return PartialView(response ?? new List<SubscripitionReport>());
        }

        [HttpPost]
        public async Task<IActionResult> ErrorLog()
        {
            var response = await _reportService.GetErrorlog();
            var entity = response;
            return PartialView(entity ?? new List<ErrorModel>());
        }

        [HttpPost]
        public async Task<IActionResult> GetDashboardSummery()
        {
            var response = await _reportService.GetDashboardSummery();            
            return Json(response ?? new DashboardSummery());

        }
        [HttpGet("/Invoice/{tid}")]
        public async Task<IActionResult> Invoice(int tid)
        {
            var response = await _reportService.GetInvoice(tid);
            return View(response ?? new Invoice());
        }
    }
}
