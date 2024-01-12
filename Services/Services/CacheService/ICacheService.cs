using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CaheService
{
    public interface ICacheService
    {
        Task SetCacheResponseAsync(string cacheKey,object response, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsync(string cacheKey);

    }
}
