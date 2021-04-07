using LibraryApi.Controllers;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class RedisOnCallDeveloperLookup : ILookupOnCallDevelopers
    {
        private readonly IDistributedCache _cache;

        public RedisOnCallDeveloperLookup(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<OnCallDevelopersResponse> GetOnCallDeveloperAsync()
        {
            // Check to see if it is in the cache.
            var cachedResponse = await _cache.GetAsync("oncall");
            if (cachedResponse != null)
            {
                // if it is there, we just return that thing.
                string storedResponse = Encoding.UTF8.GetString(cachedResponse);
                OnCallDevelopersResponse response = JsonSerializer.Deserialize<OnCallDevelopersResponse>(storedResponse);
                return response;
            }
            else
            {
                // if it isn't, we:
                // - do the work to recreate it.
                var dev = new OnCallDevelopersResponse
                {
                    Name = "Ryan",
                    Email = "ryan@compuserve.com",
                    Until = DateTime.Now.AddHours(12)
                };



                // - put in the cache
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddSeconds(15));
                string serializedDev = JsonSerializer.Serialize(dev);
                // REDIS needs it to be a byte array
                byte[] encodedDev = Encoding.UTF8.GetBytes(serializedDev);
                await _cache.SetAsync("oncall", encodedDev, options);
                return dev;
                // - return that thing.        }
            }
        }
    }
}
