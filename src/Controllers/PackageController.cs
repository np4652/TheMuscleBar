using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
    public class PackageController : Controller
    {
        public readonly IPackageService _PackageService;
        public readonly AppCode.PaymentGateway.IPaymentGatewayService _pgService;
        public readonly IPaymentGateway _pgMaster;
        private IUserService _users;
        public PackageController(IPackageService PackageService, AppCode.PaymentGateway.IPaymentGatewayService pgService, IPaymentGateway pgMaster,IUserService users)
        {
            _PackageService = PackageService;
            _pgService = pgService;
            _pgMaster = pgMaster;
            _users = users;
        }
        [Authorize(Roles = "1")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int PackageId = -1)
        {

            var response = await _PackageService.GetByIdAsync(PackageId);
            var entity = response.Result;
            return PartialView(entity ?? new Package());
        }

        public async Task<IActionResult> All()
        {
            var response = await _PackageService.GetAllAsync(new Package());
            var entity = response;
            return PartialView(entity ?? new List<Package>());
        }


        [HttpPost]
        public async Task<IActionResult> Save(Package Package)
        {
            var response = await _PackageService.AddAsync(Package);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeActiveStatus(int packageId)
        {
            var response = await _PackageService.ChangeActiveStatus(packageId);
            return Json(response);
        }

        public IActionResult Packages()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> _Package()
        {
            var response = await _PackageService.GetPackage(User.GetLoggedInUserId<int>());
            var entity = response;
            return PartialView(entity ?? new List<BuypackageViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> GetPackage(int PackageId)
        {
            var packageDetails = await _PackageService.GetByIdAsync(PackageId);
            if(packageDetails.Result.Cost < 1)
            {
                int userId = User.GetLoggedInUserId<int>();
                var res = await _users.AssignPackage(userId,PackageId);
                return Json(new { redirect = false ,res.StatusCode, res.ResponseText});
            }
            var pgMaster = await _pgMaster.GetAllAsync();
            if (pgMaster != null)
            {
                var PGs = pgMaster.Where(x => x.IsActive).Select(x => x.PGId);
                if (PGs.Count() > 1)
                {
                    return PartialView("~/Views/PaymentGateway/_ChoosePaymentGatway.cshtml", new PGDisplayModel
                    {
                        PGs = PGs,
                        PackageId = PackageId
                    });
                }
                else
                {
                    return Json(new {redirect=true, PackageId, pgId =  PGs.FirstOrDefault()  });            
                }
            }
            return BadRequest("No payment gateway found");
        }

        [Route("InitiatePayment")]        
        public async Task<IActionResult> GetPackage(int packageId, PaymentGatewayType paymentGatewayType)
        {
            var res = await _pgMaster.GetPGDetailByPGIDAsync(paymentGatewayType);
            if (res.StatusCode == ResponseStatus.Success)
            {
                var response = await InitiatePayment(packageId, new PaymentGatewayRequest
                {
                    PGID = res.Result.PGId,
                    URL = res.Result.BaseURL,
                    MerchantID = res.Result.MerchantID,
                    MerchantKey = res.Result.MerchantKey,
                    IsLive = res.Result.IsLive,
                    SuccessURL = res.Result.SuccessURL,
                    FailedURL = res.Result.FailURL,
                    StatusCheckURL = res.Result.StatusCheckURL,
                    IsLoggingTrue = res.Result.IsLoggingTrue,
                });
                return View("~/Views/PaymentGateway/PGRedirect.cshtml", response);
            }
            return BadRequest("Something went wrong");
        }

        private async Task<Response<PaymentGatewayResponse>> InitiatePayment(int packageId, PaymentGatewayRequest request)
        {
            var response = new Response<PaymentGatewayResponse>();
            var packageDetail = await _PackageService.GetByIdAsync(packageId);
            if (packageDetail.StatusCode == ResponseStatus.Success)
            {
                int userId = User.GetLoggedInUserId<int>();
                if (userId > 0)
                {
                    var userDetail = await _users.GetByIdAsync(userId);
                    if(userDetail.StatusCode== ResponseStatus.Success)
                    {
                        request.UserID = userId;
                        request.Amount = packageDetail.Result.Cost;
                        var logResponse = await _pgService.SaveInitiatePayment(request, packageId);
                        if (logResponse.StatusCode == ResponseStatus.Success)
                        {
                            request.TID = $"TID{logResponse.Result.ToString().PadLeft(5, '0')}"; request.Amount = packageDetail.Result.Cost;
                            request.Domain = $"{Request.Scheme}://{Request.Host}";
                            request.Pincode = "226010";
                            request.UserID = userDetail.Result.Id;
                            request.EmailID = userDetail.Result.Email;
                            request.MobileNo = userDetail.Result.PhoneNumber;
                            response = await _pgService.GeneratePGRequestForWebAsync(request);
                        }
                    }
                }
            }
            return response;
        }
    }
}