using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TheMuscleBar.AppCode.Extensions;
using TheMuscleBar.AppCode.Data;
using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : Controller
    {
        private IUserService _users;
        private IMapper _mapper;
        private readonly ApplicationUserManager _userManager;
        public UserController(IUserService users, IMapper mapper, ApplicationUserManager userManager)
        {
            _users = users;
            _mapper = mapper;
            _userManager = userManager;
        }
        [Authorize(Roles = "1")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> UsersList(Role role=Role.Customer)
        {
            int loginId = User.GetLoggedInUserId<int>();
            var users = await _users.GetAllAsync(new ApplicationUser { Role = role.ToString() }, loginId);
            return PartialView("~/Views/Account/PartialView/_UsersList.cshtml", users);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(int id = -1)
        {
            var users = await _users.GetByIdAsync(id);
            var response = _mapper.Map<RegisterViewModel>(users.Result ?? new ApplicationUser());
            return PartialView("~/Views/Account/PartialView/_Register.cshtml", response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Register model)
        {
            Response response = new Response()
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Registration Failed"
            };
            var user = new ApplicationUser
            {
                Id = model.Id,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber
            };
            if (model.Id > 0)
            {
                response = await _users.AddAsync(user);
            }
            return Json(response);
        }

        [HttpPost("GetSecurityCode")]
        public async Task<IActionResult> SecurityCode()
        {
            var user = await _userManager.FindByIdAsync(User.GetLoggedInUserId<int>().ToString());
            return Json(new {SecurityCode = user.ConcurrencyStamp, MerchantId = ""});
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAction(int id)
        {
            var response = await _users.ChangeAction(id);
            return Json(response);
           
        }

        public async Task<IActionResult> Profile()
        {
            ApplicationUser user = await _userManager.FindByIdAsync(User.GetLoggedInUserId<int>().ToString());
            return View(user);
        }

    }
}