using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

using System;
using TheMuscleBar.AppCode.Data;
using Microsoft.AspNetCore.Http;
using TheMuscleBar.AppCode.Enums;

using TheMuscleBar.Models.ViewModel;
using System.Collections.Generic;

using TheMuscleBar.AppCode.Reops.Entities;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

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
                user.Role = req.Role != Role.Staff ? Role.Customer.ToString() : Role.Staff.ToString();
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
        [HttpPost(nameof(syncattendance))]
        public async Task<IActionResult> syncattendance(SyncData data)
        {
            var req = new ApiModel()
            {
                Request = "syncattendance",
                Response= "syncattendance",
                Params= data.data
            };
            var response = _users.SaveApiLog(req).Result;

            try
            {
                var attlst = JsonConvert.DeserializeObject<List<AttendanceInsert>>(data.data);
                var dt = ToDataTable(attlst);
                await _users.MergeAttendance(dt);
            }
            catch (Exception ex )
            { 
            
            }

            return Ok(response);
        }

        [HttpPost(nameof(CollectFee))]
        public async Task<IActionResult> CollectFee(CollectFee collectFee)
        {
            collectFee.EntryBy = 1;
            collectFee.TransactionType = TransactionType.cr;
            var res = await _users.CollectFee(collectFee);
            return Ok(res);
        }

        [HttpPost(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var users = await _users.GetByIdAsync(id);
                return Ok(users);
            }
            catch(Exception ex)
            {
                return Ok("Something went wrong");
            }
        }

        [HttpPost(nameof(attendance))]
        public async Task<IActionResult> attendance(List<AttendanceInsert> data)
        {

            var dt = ToDataTable(data);
            await _users.MergeAttendance(dt);
            return Ok("Success");
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
     


    }
}