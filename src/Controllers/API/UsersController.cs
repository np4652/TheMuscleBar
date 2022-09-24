using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TheMuscleBar.AppCode.CustomAttributes;
using System.Linq;
using System;
using TheMuscleBar.AppCode.Data;
using Microsoft.AspNetCore.Http;
using TheMuscleBar.AppCode.Enums;
using Microsoft.AspNetCore.Authorization;
using TheMuscleBar.Models.ViewModel;
using System.Collections.Generic;
using TheMuscleBar.AppCode.Helper;

namespace TheMuscleBar.Controllers.API
{
    [ApiController]
    [Route("api/{controller}/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : ControllerBase
    {
        private IUserService _users;
        private IMapper _mapper;
        private readonly ApplicationUserManager _userManager;
        private readonly LoginResponse loginResponse;
        private readonly ApplicationUser _user;
        public UserController(IHttpContextAccessor httpContext, IUserService users, IMapper mapper, ApplicationUserManager userManager)
        {
            _users = users;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterAPIRequest req)
        {
            var model = _mapper.Map<RegisterViewModel>(req);
           
            Response<UserDetailsReturn> response = new Response<UserDetailsReturn>()
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Registration Failed"
            };
            model.Password = string.IsNullOrEmpty(model.Password) ? "Welcome123" : model.Password;
            model.ConfirmPassword = string.IsNullOrEmpty(model.ConfirmPassword) ? "Welcome123" : model.ConfirmPassword;
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(model);
                user.UserName = model.Email.Trim();
                user.Role = Role.Customer.ToString();
                var res = await _userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    user = _userManager.FindByEmailAsync(user.Email).Result;

                    //await _userManager.AddToRoleAsync(user, Role.Customer.ToString());
                    model.Password = string.Empty;
                    model.Email = string.Empty;
                    response.StatusCode = ResponseStatus.Success;
                    response.ResponseText = "User Register Successfully";
                    var UserDetailsReturn = new UserDetailsReturn()
                    {
                        UserId = user.Id,
                        Name = user.Name
                    };
                    response.Result = UserDetailsReturn;
                    ModelState.Clear();
                    model = null;
                    model = new RegisterViewModel();
                }

                return Ok(response);
            }
            return Ok(response);
        }



        [HttpGet(nameof(SubscriptionExpired))]
        public async Task<IActionResult> SubscriptionExpired()
        {
            var res = _users.GetSubscriptionExpired().Result;
            return Ok(res ?? new List<UnSubscribedUser>());
        }

        [HttpGet(nameof(SubscriptionByuser))]
        public async Task<IActionResult> SubscriptionByuser(int userId)
        {
            Response<UserDetailsReturn> response = new Response<UserDetailsReturn>()
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Not Subscribed"
            };
            var res = _users.GetSubscriptionByuser(userId).Result;
            if (res != null && res.UserId>1)
            {
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = "Success";
                response.Result = res;
            }
            return Ok(response);
        }
    }
}