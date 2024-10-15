using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.CachServices
{
    public interface ICacheServices
    {

        Task SetCacheResponseAsync(string key, object Response, TimeSpan TimeToLive);
        Task<string> GetCacheResponse(string key);
    }
}
