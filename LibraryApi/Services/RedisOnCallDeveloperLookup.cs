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
            Byte[] cachedResponse;
            try
            {
                cachedResponse = await _cache.GetAsync("oncall");

            }
            catch (Exception)
            {

                return await GetTheRealData();
            }            
            
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
                OnCallDevelopersResponse dev = await GetTheRealData();

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

        private async Task<OnCallDevelopersResponse> GetTheRealData()
        {
            await Task.Delay(3000);
            // - do the work to recreate it.
            return new OnCallDevelopersResponse
            {
                Name = "Ryan",
                Email = "ryan@compuserve.com",
                Until = DateTime.Now.AddHours(12)
            };
        }
    }
}
