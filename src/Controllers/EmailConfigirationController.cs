using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TheMuscleBar.Controllers
{
    [Authorize(Roles = "1")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EmailConfigirationController : Controller
    {
        public readonly IRepository<EmailConfig> _emailConfigService;
        public EmailConfigirationController(IRepository<EmailConfig> emailConfigService)
        {
            _emailConfigService = emailConfigService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id = -1)
        {
            var response = await _emailConfigService.GetByIdAsync(id);
            var entity = response.Result;
            return PartialView(entity ?? new EmailConfig());
        }

        public async Task<IActionResult> All()
        {
            var response = await _emailConfigService.GetAllAsync(new EmailConfig());
            var entity = response;
            return PartialView(entity ?? new List<EmailConfig>());
        }

        public async Task<JsonResult> Dropdown()
        {
            var response = await _emailConfigService.GetAllAsync(new EmailConfig());
            var entity = response ?? new List<EmailConfig>();
            return Json(entity.Select(x => new { x.Id, x.EmailFrom }).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Save(EmailConfig emailConfig)
        {
            var response = await _emailConfigService.AddAsync(emailConfig);
            return Json(response);
        }
    }
}