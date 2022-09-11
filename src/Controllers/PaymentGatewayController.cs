using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.AppCode.Interfaces;
using TheMuscleBar.AppCode.Reops.Entities;
using TheMuscleBar.Models;

namespace TheMuscleBar.Controllers
{
    [Authorize(Roles = "1")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentGatewayController : Controller
    {
        private readonly IPaymentGateway _paymentGateway;
        public PaymentGatewayController(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(PaymentGatewayModel paymentGateway)
        {
            var res = await _paymentGateway.AddAsync(paymentGateway);
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> All()
        {
            var response = await _paymentGateway.GetAllAsync(new PaymentGatewayModel());
            var entity = response;
            return PartialView(entity ?? new List<PaymentGatewayModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id = -1)
        {
            var response = await _paymentGateway.GetByIdAsync(id);
            var entity = response.Result;
            return PartialView(entity ?? new PaymentGatewayModel());
        }

        [HttpPost]
        public async Task<IActionResult>ChangeLiveStatus(int id)
        {            
            var response = await _paymentGateway.ChangeLiveStatus(id);
            return Json(response);
        }

        [HttpPost]
        public async Task <IActionResult> ChangeLoggingStatus(int id)
        {
            var response = await _paymentGateway.ChangeLoggingStatus(id);
            return Json(response);

        }
        [HttpPost]
        public async Task<IActionResult> ChangeActiveStatus(int id)
        {
            var response=await _paymentGateway.ChangeActiveStatus(id);
            return Json(response);
        }



    }
}

