using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepoistory _basketRepoistory;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepoistory basketRepoistory,
            IMapper mapper)
        {
            _basketRepoistory = basketRepoistory;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        => await _basketRepoistory.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepoistory.GetBasketAsync(basketId);
            if (basket == null) 
                return new CustomerBasketDto();
            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basketDto)
        {
            if (basketDto.Id is null)
                basketDto.Id = GenerateRandomBasketId();
            var customerBasket = _mapper.Map<CustomerBasket>(basketDto);
            var updateBasket = await _basketRepoistory.UpdateBasketAsync(customerBasket);
            var mappedUpdateBasket = _mapper.Map<CustomerBasketDto>(updateBasket);
            return mappedUpdateBasket;

        }
        private string GenerateRandomBasketId()
        {
            Random random = new Random();
            int randomDigits = random.Next(1000, 10000);
            return $"BS-{randomDigits}";
        }
    }
}
