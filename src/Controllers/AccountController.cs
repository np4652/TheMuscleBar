using AutoMapper;
using Hangfire;
using TheMuscleBar.AppCode.CustomAttributes;
using TheMuscleBar.AppCode.Data;
using TheMuscleBar.AppCode.Helper;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Org.BouncyCastle.Bcpg;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace TheMuscleBar.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : Controller
    {
        #region Variables
        private readonly JWTConfig _jwtConfig;
        private readonly ApplicationUserManager _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IUserService _users;
        private readonly ILogger<AccountController> _logger;
        private readonly IRepository<EmailConfig> _emailConfig;
        private readonly ITokenService _tokenService;
        private IMapper _mapper;
        #endregion

        public AccountController(IOptions<JWTConfig> jwtConfig,
            ApplicationUserManager userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IUserService users, ITokenService tokenService,
            ILogger<AccountController> logger, IRepository<EmailConfig> emailConfig, IMapper mapper
            )
        {
            _jwtConfig = jwtConfig.Value;
            _logger = logger;
            _emailConfig = emailConfig;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _users = users;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult _Register()
        {
            return PartialView("partialView/_Register", new RegisterViewModel());
        }

        [HttpPost]
        //[ValidateAjax]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Response response = new Response()
            {
                StatusCode = ResponseStatus.warning,
                ResponseText = "Registration Failed"
            };
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(model);
                user.UserName = model.Email.Trim();
                user.Role = Role.Customer.ToString();
                var res = await _userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    user = _userManager.FindByEmailAsync(user.Email).Result;
                    if (model.ProfilePic != null)
                    {
                        response = AppUtility.O.UploadFile(new FileUploadModel
                        {
                            file = model.ProfilePic,
                            FileName = $"profile_{user.Id}.png",
                            FilePath = FileDirectories.Profile,
                            IsThumbnailRequired = false
                        });
                    }
                    //await _userManager.AddToRoleAsync(user, Role.Customer.ToString());
                    model.Password = string.Empty;
                    model.Email = string.Empty;
                    response.StatusCode = ResponseStatus.Success;
                    response.ResponseText = "User Register Successfully";
                    ModelState.Clear();
                    model = null;
                    model = new RegisterViewModel();
                }
                model.ResponseText = response.ResponseText;
                model.StatusCode = response.StatusCode;
                //return View(model);
                return Json(model);
            }
            return View(model);
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            ReturnUrl = ReturnUrl ?? Url.Content("~/");
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.MobileNo, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    ReturnUrl = ReturnUrl?.Trim() == "/" ? "/dashboard" : ReturnUrl;
                    return LocalRedirect(ReturnUrl);
                }
                else if (result.IsLockedOut)
                {
                    ModelState.Remove(string.Empty);
                    ModelState.AddModelError(string.Empty, "Your account is locked out.");
                    var Scheme = Request.Scheme;
                    var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                    var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);
                    var config = _emailConfig.GetAllAsync(new EmailConfig { Id = 2 }).Result;
                    if (config == null || config.Count() == 0)
                        _logger.LogError("No Email configuration found", new { this.GetType().Name, fn = nameof(this.Login) });
                    var setting = _mapper.Map<EmailSettings>(config?.Count() > 0 ? config.FirstOrDefault() : new EmailConfig());
                    setting.Body = content;
                    setting.Subject = "Locked out account information";
                    var _ = AppUtility.O.SendMail(setting);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), new { this.GetType().Name, fn = nameof(this.Login) });
            }
            return View();
        }

        #region Forget Password

        [HttpPost]
        public IActionResult ForgotPasswordView()
        {
            return PartialView(new ForgotPasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Success,
                ResponseText = "The link has been sent, please check your email to reset your password."
            };
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordView));
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var config = _emailConfig.GetAllAsync().Result;
            if (config == null || config.Count() == 0)
            {
                _logger.LogError("No Email configuration found", new { this.GetType().Name, fn = nameof(this.Login) });
                response.ResponseText = "Configuration missing. Please contact to admin.";
                response.StatusCode = ResponseStatus.Failed;
            }
            var setting = _mapper.Map<EmailSettings>(config?.Count() > 0 ? config.FirstOrDefault() : new EmailConfig());
            setting.Body = callback;
            setting.Subject = "Reset password";
            setting.EmailTo = user.Email;
            BackgroundJob.Enqueue(() => sendMail(setting));
            return Json(response);
        }


        [HttpPost("/api/ResetPassword")]
        public async Task<IActionResult> ResetPasswordAPI(ForgotPasswordModel forgotPasswordModel)
        {
            IResponse response = new Response
            {
                StatusCode = ResponseStatus.Success,
                ResponseText = " The link has been sent, please check your email to reset your password."
            };
            if (!ModelState.IsValid)
            {
                response.StatusCode = ResponseStatus.Failed;
                response.ResponseText = ResponseStatus.Failed.ToString();
                goto Finish;
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                response.StatusCode = ResponseStatus.Failed;
                response.ResponseText = "No such email found";
                goto Finish;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var config = _emailConfig.GetAllAsync().Result;
            if (config == null || config.Count() == 0)
                _logger.LogError("No Email configuration found", new { this.GetType().Name, fn = nameof(this.Login) });
            var setting = _mapper.Map<EmailSettings>(config?.Count() > 0 ? config.FirstOrDefault() : new EmailConfig());
            setting.Body = callback;
            setting.Subject = "Reset password";
            setting.EmailTo = user.Email;
            BackgroundJob.Enqueue(() => sendMail(setting));
            response.StatusCode = ResponseStatus.Success;
            response.ResponseText = " The link has been sent, please check your email to reset your password.";
Finish:
            return Json(response);
        }


        public async Task sendMail(EmailSettings emailSettings)
        {
            await AppUtility.O.SendMail(emailSettings);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> UsersDetails(string role)
        {
            if (role.Trim() != "Fos" && role.Trim() != "Consumer")
            {
                role = string.Empty;
            }
            var users = _users.GetAllAsync().Result;
            if (users.Count() > 0)
            {
                users = users.Where(x => x.Role == role);
            }
            return PartialView("~/Views/Account/PartialView/_UsersList.cshtml", users);
        }

        [HttpPost]
        public async Task<IActionResult> UsersDropdown(int id, string role)
        {
            var users = await _users.GetAllAsync(new ApplicationUser { Id = id, Role = role });
            return Json(users.Select(x => new { x.Id, x.Email, x.Name }).ToList());
        }
        
    }
}