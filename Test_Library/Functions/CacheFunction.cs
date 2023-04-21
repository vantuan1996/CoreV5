using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Test_Library.Functions
{
    public class CacheFunction
    {

        public static bool TryGet<T>(HttpContext context, string identify, out T modelCache)
        {
            var cacher = context.RequestServices.GetService<IMemoryCache>();
            var result = cacher.TryGetValue<T>(identify, out modelCache);
            return result;
        }

        public static void Add<T>(HttpContext httpContext, string key, T data, int cacheTime) where T : class
        {
            var cache = httpContext.RequestServices.GetService<IMemoryCache>();
            try
            {
                if (data == null)
                {
                    return;
                }
                MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(cacheTime);
                cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                cache.Set(key, data, cacheExpirationOptions);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public static void Clear(HttpContext context, string key)
        {
            var Cache = context.RequestServices.GetService<IMemoryCache>();
            Cache.Remove(key);
        }
    }
}
