using AutoMapper;
using Store.Data.Entities;
using Store.Reposatory.Interfaces;
using Store.Reposatory.Specification.SpecifProducts;
using Store.Service.Helper;
using Store.Service.Services.ProductService;
using Store.Service.Services.ProductService.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductService
{
    public class ProductServices :IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeServiceDTO>> GetAllBrandsAsync()
        {
            var brand = await unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedbrand=mapper.Map<IReadOnlyList<BrandTypeServiceDTO>>(brand);

            return mappedbrand;
        }
       
          
        
        public async Task<PaginatedResultDTO<ProductDetailsDTO>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpcification(input);
            var products = await unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);
          
            var countspecs = new ProductWithFilterAndCountSpcification(input);
            var count = await unitOfWork.Repository<Product, int>().CountSpcificationAsync(countspecs);
            var mappedproducts = mapper.Map<IReadOnlyList<ProductDetailsDTO>>(products);
           
            return new PaginatedResultDTO<ProductDetailsDTO>(input.PageIndex,input.PageSize, count, mappedproducts);


        }

        public async Task<IReadOnlyList<BrandTypeServiceDTO>> GetAllTypesAsync()
        {
            var type = await unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedtypes = mapper.Map<IReadOnlyList<BrandTypeServiceDTO>>(type);

            return mappedtypes;
        }

        public async Task<ProductDetailsDTO> GetProductByIdAsync(int? id)
        {
            if(id is null) throw new Exception();
            var specs = new ProductWithSpcification(id);
        
            var product=await unitOfWork.Repository<Product,int>().GetWithSpecificByIdAsync(specs);
          
            var mappedproduct = mapper.Map<ProductDetailsDTO>(product);

           
            return mappedproduct;

        }
    }
}
