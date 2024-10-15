using AutoMapper;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductService.DTOs
{
    public class ProductProfile:Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductDetailsDTO>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
               .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.Type.Name))
               .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductUrlResolver>());


            CreateMap<ProductBrand, BrandTypeServiceDTO>();
            CreateMap<ProductType, BrandTypeServiceDTO>();


        }
    }
}
