using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Store.Service.CachServices;
using System.Text;

namespace Store.API.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveInSecond;

        public CacheAttribute(int   TimeToLiveInSecond)
        {
            timeToLiveInSecond = TimeToLiveInSecond;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService=context.HttpContext.RequestServices.GetRequiredService<ICacheServices>(); 
            var cacheKey= GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCacheResponse(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            var executedContext=await next();
            if (executedContext.Result is OkObjectResult response)
                await cacheService.SetCacheResponseAsync(cacheKey, response.Value, TimeSpan.FromSeconds(timeToLiveInSecond));

            

            

        }
        private string GenerateCacheKeyFromRequest(HttpRequest request) 
        {
            StringBuilder CacheKey= new StringBuilder();
            CacheKey.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
                CacheKey.Append($"|{key}-{value}");

            return CacheKey.ToString();
        }
    }
}
