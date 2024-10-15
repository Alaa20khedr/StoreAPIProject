using AutoMapper;
using StackExchange.Redis;
using Store.Data.Entities.IdentityEntity;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService.Dtos
{
    public class OrderProfileDto:Profile
    {
        public OrderProfileDto() 
        { 
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<AddressDto,ShippingAddress>().ReverseMap();
            CreateMap<Orders, OrderResultDto>().
               ForMember(dest => dest.DelivaryMethodName, options => options.MapFrom(src => src.DelivaryMethod.ShortName))
               .
               ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DelivaryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.itemOrdered.ProductItemId)).
                ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.itemOrdered.ProductName)).
                ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.itemOrdered.PictureUrl)).
                ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemUrlResolver>()).ReverseMap();

        }
    }
}
