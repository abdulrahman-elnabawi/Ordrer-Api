using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Services.CaheService;
using System.Text;

namespace Demo.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLineInSeconds;

        public CacheAttribute(int timeToLineInSeconds)
        {
            _timeToLineInSeconds = timeToLineInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cachKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponse = await cacheService.GetCacheResponseAsync(cachKey);

            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentresult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentresult;

                return;
            }
            var executedContext = await next();

            if (executedContext.Result is OkObjectResult response)
                await cacheService.SetCacheResponseAsync(cachKey, response.Value, TimeSpan.FromSeconds(_timeToLineInSeconds));

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = new StringBuilder();

            cacheKey.Append($"{request.Path}");

            foreach ( var (key,value) in request.Query.OrderBy(x =>x.Key)) // tuple
            {
                cacheKey.Append($"|{key}-{value}");
            }

            return cacheKey.ToString();


        }

    }
}
