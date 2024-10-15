using Store.Reposatory.Specification.SpecifProducts;
using Store.Service.Helper;
using Store.Service.Services.ProductService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductService
{
    public interface IProductServices
    {
        Task <ProductDetailsDTO> GetProductByIdAsync(int? id);
        Task<PaginatedResultDTO<ProductDetailsDTO>> GetAllProductsAsync(ProductSpecification input);
        Task<IReadOnlyList<BrandTypeServiceDTO>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeServiceDTO>> GetAllTypesAsync();
    }
}
