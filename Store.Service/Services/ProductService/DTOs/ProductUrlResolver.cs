using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductService.DTOs
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDetailsDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductDetailsDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            
                return $"{configuration["BaseUrl"] }{source.PictureUrl}";
           
            return null;
        }
    }
}
