using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Helper;
using Store.Reposatory.Specification.SpecifProducts;
using Store.Service.HandleResponses;
using Store.Service.Helper;
using Store.Service.Services.ProductService;
using Store.Service.Services.ProductService.DTOs;

namespace Store.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;

        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpGet]
        [Cache(30)]
        public async Task<ActionResult<IReadOnlyList<BrandTypeServiceDTO>>> GetAllBrands()
        =>Ok(await productServices.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeServiceDTO>>> GetAllTypes()
      => Ok(await productServices.GetAllTypesAsync());

        [HttpGet("Products")]
        public async Task<ActionResult<PaginatedResultDTO<ProductDetailsDTO>>> GetAllProducts([FromQuery]ProductSpecification input)
    => Ok(await productServices.GetAllProductsAsync( input));
        [HttpGet("Product")]
        public async Task<ActionResult<ProductDetailsDTO>> GetProduct(int? id)
        {
            if (id is null)
                return BadRequest(new CustomException(400, "id is null"));
            var product=(await productServices.GetProductByIdAsync(id));
            if(product is null)
                return NotFound(new CustomException(404));

            return Ok(product);
        }
    }
}
