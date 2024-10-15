using Store.Reposatory.Interfaces;
using Store.Reposatory.Repositories;
using Store.Service.Services.ProductService.DTOs;
using Store.Service.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Store.Service.HandleResponses;
using System.Reflection.Metadata.Ecma335;
using Store.Reposatory.BasketReposatory;
using Store.Service.Services.BasketService.DTOs;
using Store.Service.Services.BasketService;
using Store.Service.TokenServices;
using Store.Service.UserService;
using Store.Service.OrderService.Dtos;
using Store.Service.PaymentService;
using Store.Service.OrderService;

namespace Store.API.ApplicationServicesExtensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services) {


            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IProductServices, ProductServices>();
            Services.AddScoped<IBasketReposatory, BasketReposatory>();
            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<ITokenServices, TokenServices>();
            Services.AddAutoMapper(typeof(ProductProfile));
            Services.AddAutoMapper(typeof(BasketProfile));
            Services.AddAutoMapper(typeof(OrderProfileDto));
            Services.Configure<ApiBehaviorOptions>(options =>
          options.InvalidModelStateResponseFactory = ActionContext =>
          {
              var errors = ActionContext.ModelState.Where(model => model.Value.Errors.Count > 0).
              SelectMany(model => model.Value.Errors).Select(error => error.ErrorMessage).ToList();

              var errorresponse = new ValidationErrorResponse
              {
                  Errors = errors
              };
              return new BadRequestObjectResult(errorresponse);

          });
            return Services;
        }
        
    }
}
