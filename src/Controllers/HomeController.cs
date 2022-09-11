using AutoMapper;
using TheMuscleBar.AppCode.CustomAttributes;
using TheMuscleBar.AppCode.Helper;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Migrations;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Diagnostics;
using TheMuscleBar.AppCode.Extensions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheMuscleBar.AppCode.Reops;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace TheMuscleBar.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IHttpContextAccessor _httpContext;
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider IServiceProvider;
        private readonly IReportService _reportService;
        private readonly ApplicationUser _user;
        private IUPISettingService _upiSetting;
        private readonly IRepository<EmailConfig> _emailConfig;
        private IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IReportService reportService, IHttpContextAccessor httpContext, IUserService userService, IServiceProvider ServiceProvider, IRepository<EmailConfig> emailConfig, IMapper mapper, IUPISettingService upiSetting)
        {
            _userService = userService;
            _httpContext = httpContext;
            _logger = logger;
            _emailConfig = emailConfig;
            IServiceProvider = ServiceProvider;
            _reportService = reportService;
            _mapper = mapper;
            if (_httpContext != null && _httpContext.HttpContext != null)
            {
                _user = (ApplicationUser)_httpContext?.HttpContext.Items["User"];
            }
            _upiSetting = upiSetting;
        }

        [Route("/dashboard")]
        public async Task<IActionResult> Index()
        {
            string role = User.GetLoggedInUserRoles() ?? string.Empty;
            string path = "_AdminDashboard";
            if (role.Equals("apiuser", StringComparison.OrdinalIgnoreCase) || role.Equals("2"))
            {
                path = "_ApiDashboard";
                if (!await _upiSetting.IsAnyConfigurationExists(User.GetLoggedInUserId<int>()))
                {
                    path = "~/views/Merchant/Index.cshtml";
                }
            }
            return View(path);
        }
        public IActionResult APIDoc()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RecentActivity()
        {
            var response = await _reportService.GetTransactionReport(new TransactionReportRequest
            {
                UserId = User.GetLoggedInUserId<int>(),
                Top = 10
            });
            var entity = response;
            return PartialView(entity ?? new List<TransactionReport>());
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [JWTAuthorize]
        public ActionResult Get()
        {
            return Json(_user);
        }

        [HttpPost, Route(nameof(RunMigration))]
        public IActionResult RunMigration(string DatabaseName)
        {
            var result = MigrationManager.MigrateDatabase(IServiceProvider, DatabaseName);
            return Json(result);
        }

        public void NLog()
        {
            _logger.LogInformation("Requested a Random API");
            int count;
            try
            {
                for (count = 0; count <= 5; count++)
                {
                    if (count == 3)
                    {
                        throw new Exception("Random Exception Occured");
                    }
                    else
                    {
                        _logger.LogInformation("Iteration Count is {iteration}", count);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        [HttpPost]
        [Route("welcome")]
        public IActionResult Welcome(string userName)
        {
            var jobId = BackgroundJob.Enqueue(() => SendWelcomeMail(userName));
            return Ok($"Job Id {jobId} Completed. Welcome Mail Sent!");
        }

        public void SendWelcomeMail(string userName)
        {
            var config = _emailConfig.GetAllAsync().Result;
            config = _emailConfig.GetAllAsync(new EmailConfig { Id = 2 }).Result;
            var setting = _mapper.Map<EmailSettings>(config.FirstOrDefault());
            var _ = AppUtility.O.SendMail(setting);
        }
        [HttpGet("KeyCode")]
        public IActionResult KeyCode()
        {
            return View();
        }
        [HttpPost("UserDashboardCount")]
        public async Task<IActionResult> UserDashboardCount()
        {
            var result = await _reportService.GetUserDashboardsummary(User.GetLoggedInUserId<int>());
            return Json(result);
        }

        [HttpPost("TotalUserDashboardAmnt")]
        public async Task<IActionResult> TotalUserDashboardAmnt()
        {
            var result = await _reportService.GetUserDashboardAmnt(User.GetLoggedInUserId<int>());
            //var result = new APIUserCounts();
            return Json(result);
        }

        [HttpPost("AdminDashboardCount")]
        public async Task<IActionResult> AdminDashboardCount()
        {
            var result = await _reportService.GetAdminDashboardsummary(User.GetLoggedInUserId<int>());
            return Json(result);
        }

        [HttpPost("TotalAdminDashboardAmnt")]
        public async Task<IActionResult> TotalAdminDashboardAmnt()
        {
            var result = await _reportService.GetAdminDashboardAmnt(User.GetLoggedInUserId<int>());
            return Json(result);
        }

        [HttpPost("UserDashboardChart")]
        public async Task<IActionResult> UserDashboardChart()
        {
            var res = await _reportService.GetAPIDashboardGraphData(User.GetLoggedInUserId<int>());
            foreach (var item in res)
            {
                item.EntryOn = GetMilisecond(item.EntryOnDate);
            }
            return Json(res);
        }
        [HttpPost("ExpiredActive")]
        public async Task<IActionResult> ExpiredActive()
        {
            var result = await _reportService.GetExpiredActive(User.GetLoggedInUserId<int>());
            return Json(result);
        }
        [HttpPost("PieStatus")]
        public async Task<IActionResult> PieStatus(int Type)
        {
            var result = await _reportService.GetPieChart(User.GetLoggedInUserId<int>(), Type);
            return Json(result);
        }
        public long GetMilisecond(DateTime dateTime)
        {
            return (long)(dateTime.Date - new DateTime(1970, 1, 1, 0, 0 , 0)).TotalMilliseconds;
        }

    }
}
