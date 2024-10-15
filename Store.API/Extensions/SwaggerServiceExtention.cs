using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Store.API.Extensions
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection Services)
        {
            Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreAPI", Version = "v1" });
                var securtyscheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization Header using Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer",


                    }


                };
                options.AddSecurityDefinition("bearer", securtyscheme);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securtyscheme,new[] { "bearer" } }

                    };
                options.AddSecurityRequirement(securityRequirement);


            });
          
            
            return Services;
        }
    }
}
