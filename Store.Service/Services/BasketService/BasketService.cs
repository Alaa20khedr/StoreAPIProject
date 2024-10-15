using AutoMapper;
using Store.Reposatory.BasketReposatory;
using Store.Reposatory.BasketReposatory.Models;
using Store.Service.Services.BasketService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketReposatory basketReposatory;
        private readonly IMapper mapper;

        public BasketService(IBasketReposatory basketReposatory , IMapper mapper)
        {
            this.basketReposatory = basketReposatory;
            this.mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
       =>await basketReposatory.DeleteBasketAsync(BasketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string BasketId)
        {
            var basket=await basketReposatory.GetBasketAsync(BasketId);
            if(basket is null)
                return new CustomerBasketDto();
            var mappedBasket= mapper.Map<CustomerBasketDto>(basket);  
            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        {
          var customerbasket=mapper.Map<CustomerBasket>(basket);
           var updatedbasket=await basketReposatory.UpdateBasketAsync(customerbasket);
            var mappedbasket = mapper.Map<CustomerBasketDto>(updatedbasket);
            return mappedbasket;
        }
    }
}
