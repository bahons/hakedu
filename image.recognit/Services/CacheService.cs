using image.recognit.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace image.recognit.Services
{
    public class CacheService
    {
        private IMemoryCache cache;
        public CacheService(IMemoryCache _cach)
        {
            cache = _cach;
        }

        public bool AddClick(IEnumerable<Search> data)
        {
            try
            {
                cache.Set("search", data, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(6)
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Search> GetSearch()
        {
            IEnumerable<Search> result;
            if (cache.TryGetValue("search", out result))
            {
                return result;
            }
            return null;
        }
    }
}
