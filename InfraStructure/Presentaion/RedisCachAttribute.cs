using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text; 
using System.Threading.Tasks;

namespace Presentaion
{
    internal class RedisCachAttribute(int durationInSec=60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var cachService= context.HttpContext.RequestServices.GetRequiredService<IServiceManger>().cachService;
            var cachkey = GenerateCacheKey(context.HttpContext.Request);
            var result=await cachService.GetCacheitem(cachkey);

            if(result is not null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "Application/Json",
                    StatusCode = (int)HttpStatusCode.OK,
                };
                //return;
            }
            else
            {
                var res=await next.Invoke();

                if(res.Result is OkObjectResult okObjectResult)
                {
                   await cachService.SetCacheitem(cachkey, okObjectResult,TimeSpan.FromSeconds(durationInSec));
                }

            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var Keybuilder = new StringBuilder();
            Keybuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q=>q.Key))
            {
                Keybuilder.Append($"|{item.Key}-{item.Value}");
                //appending the query key and value to the biggest key
            }
            return Keybuilder.ToString();
            //{{BaseUrl}}://api/products?pageIndex=1&pageSize=10   For example=>CachKey that i will use for search with it on RedisDb
        }


    }
}
