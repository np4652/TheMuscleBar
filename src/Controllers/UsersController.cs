using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TheMuscleBar.AppCode.Extensions;
using TheMuscleBar.AppCode.Data;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.Models.ViewModel;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.AppCode.CustomAttributes;
using System.Linq;

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

        [Route("/dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }


        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> UsersList(Role role = Role.Customer)
        {
            int loginId = User.GetLoggedInUserId<int>();
            var users = await _users.GetAllAsync(new ApplicationUser { Role = role.ToString() }, loginId);
            users = users.Where(x => x.Id != loginId);
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

        public async Task<IActionResult> Profile()
        {
            ApplicationUser user = await _userManager.FindByIdAsync(User.GetLoggedInUserId<int>().ToString());
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> CollectForm(int userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId.ToString());
            var res = _mapper.Map<CollectFeeViewModel>(user);
            res.UserId = user.Id;
            return PartialView(res);
        }

        [HttpPost]
        [ValidateAjax]
        public async Task<IActionResult> CollectFee(CollectFee collectFee)
        {
            collectFee.EntryBy = User.GetLoggedInUserId<int>();
            collectFee.TransactionType = TransactionType.cr;
            var res = await _users.CollectFee(collectFee);
            return Json(res);
        }
    }
}