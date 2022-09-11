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

namespace TheMuscleBar.Controllers.API
{
    [ApiController]
    [Route("api/{controller}/")]
    [JWTAuthorize]
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
            if (httpContext != null && httpContext.HttpContext != null)
            {
                loginResponse = (LoginResponse)httpContext?.HttpContext.Items["User"];
                _user = loginResponse.Result;
            }
        }
        [Authorize(Roles = "1")]
        [HttpPost(nameof(UsersList))]
        public async Task<IActionResult> UsersList(string role, bool onlyDebtCustomer = false)
        {
            var users = await _users.GetAllAsync(new ApplicationUser { Role = role, OnlyDebtCustomer = onlyDebtCustomer }, _user.Id);
            var response = users.Select(x => new { x.Name, x.Email, x.FOSId, x.Id, x.PhoneNumber, x.Balance, x.RemainTarget, x.FOS });
            return Ok(response);
        }

        [HttpPost(nameof(GetById))]
        public async Task<Register> GetById(int id = -1)
        {
            var users = await _users.GetByIdAsync(id);
            var response = _mapper.Map<Register>(users.Result ?? new ApplicationUser());
            return response;
        }

        [HttpPost(nameof(Update))]
        public async Task<IResponse> Update(Register model)
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
            return response;
        }
        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Response response = new Response()
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Registration Failed"
            };
            if (!ModelState.IsValid)
            {
                return Ok(response);
            }
            var user = new ApplicationUser
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Email = model.Email,
                Role = Role.APIUser.ToString(),
                Name = model.Name,
                PhoneNumber = model.PhoneNumber
            };
            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                user = _userManager.FindByEmailAsync(user.Email).Result;
                await _userManager.AddToRoleAsync(user, Role.APIUser.ToString());
                model.Password = string.Empty;
                model.Email = string.Empty;
                ModelState.Clear();
                response.StatusCode = ResponseStatus.Success;
                response.ResponseText = "Register Successfully";
            }
            else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.TryAddModelError("", error.Description);
                    response.ResponseText = error.Description;
                }
            }
            return Ok(response);
        }

       
    }
}