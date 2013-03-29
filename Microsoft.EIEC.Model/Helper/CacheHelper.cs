using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Microsoft.EIEC.Model.Helper
{
    public class CacheHelper
    {
        /// <summary>
        /// MemoryCache.Default returns the single and same instance of in-memory ObjectCache through a static read-only property
        /// </summary>
        private static readonly ObjectCache Cache = MemoryCache.Default;

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)Cache[key];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(T objectToCache, string key) where T : class
        {
            if (Exists(key))
                Clear(key);
            Cache.Add(key, objectToCache, DateTime.Now.AddHours(3));
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add(object objectToCache, string key)
        {
            if (Exists(key))
                Clear(key);
            Cache.Add(key, objectToCache, DateTime.Now.AddDays(30));
        }

         public static void Add(object objectToCache, string key,DateTime time)
        {
            if(Exists(key))
                Clear(key);
            Cache.Add(key, objectToCache,time);
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Clear(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return Cache.Get(key) != null;
        }

        /// <summary>
        /// Gets all cached items as a list by their key.
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetAll()
        {
            return Cache.Select(keyValuePair => keyValuePair.Key).ToList();
        }
    }
}
