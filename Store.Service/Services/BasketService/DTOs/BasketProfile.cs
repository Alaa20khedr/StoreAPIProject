using AutoMapper;
using Store.Reposatory.BasketReposatory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService.DTOs
{
    public class BasketProfile:Profile
    {
        public BasketProfile() {

            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDTO>().ReverseMap();
        }
    }
}
