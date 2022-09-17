using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using TheMuscleBar.AppCode.CustomAttributes;
using TheMuscleBar.AppCode.Helper;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authorization;
using ApiRequestUtility;
using TheMuscleBar.AppCode.Enums;
using Hangfire;
using Newtonsoft.Json;
using TheMuscleBar.AppCode.Const;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace TheMuscleBar.Controllers
{
    //[Authorize(Roles = "1")]
    [CanBePaused]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TaskController : Controller
    {   
        private readonly string Connectionstring;
        private IAPILogin _apiLogin;
        private readonly ILogger<TaskController> _logger;
        public TaskController(IConnectionString connectionString, IAPILogin apiLogin, ILogger<TaskController> logger)
        {
            Connectionstring = connectionString.connectionString;            
            _apiLogin = apiLogin;
            _logger = logger;
        }

        public IActionResult PauseTask()
        {
            var storage = new SqlServerStorage(Connectionstring);
            using (var connection = storage.GetConnection())
            {
                connection.Pause(typeof(TaskController));
            }
            return Json("Task paused");
        }

        public IActionResult ResumeTask()
        {
            var storage = new SqlServerStorage(Connectionstring);
            using (var connection = storage.GetConnection())
            {
                connection.Resume(typeof(TaskController));
            }
            return Json("Task Resumed");
        }
        //[AllowAnonymous]
        //public IActionResult ScheduleStatusCheck()
        //{
        //    try
        //    {
        //        string cronExpressionEvery_10_Sec = "0/10 * * * * *";
        //        string cronExpressionEvery_10_Min = "*/10 * * * *";
        //        RecurringJob.AddOrUpdate(() => StatusCheckJob(), cronExpressionEvery_10_Sec);
        //        return Json("Scheduled");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("Something went wrong");
        //    }
        //}        
    }
}