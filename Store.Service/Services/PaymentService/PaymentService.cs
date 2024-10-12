using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.OrderService.Dtos;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dto;
using Stripe;
using Stripe.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public PaymentService(IConfiguration configuration , IUnitOfWork unitOfWork , IBasketService basketService)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            if (input == null)
                throw new Exception("Basket is Empty");
            var deliveryMethod = await _unitOfWork.Repoistory<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId.Value);
            if (deliveryMethod == null)
                throw new Exception("DeliveryMethod Not Provided");
            decimal shippingPrice = deliveryMethod.Price;
            foreach (var item in input.BascetItems)
            {
                var product = await _unitOfWork.Repoistory<Product, int>().GetByIdAsync(item.ProductId);
                if (item.Price != product.Price)
                    item.Price = product.Price;
            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(input.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)input.BascetItems.Sum(item => (item.Quantity * item.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);

                input.PaymentIntentId = paymentIntent.Id;
                input.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.BascetItems.Sum(item => (item.Quantity * item.Price * 100)) + (long)(shippingPrice * 100),
                };
                await service.UpdateAsync(input.PaymentIntentId, options);
            }
            await _basketService.UpdateBasketAsync(input);
            return input;
        }
        public Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
