
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.Dto;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{
    
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerCashBalanceGetOptions>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
         => Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));

    }
}
