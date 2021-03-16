using System;
using System.Web;
using System.Runtime.Caching;

namespace Eggsoft.Common
{
    /// <summary>
    /// 缓存相关的操作类
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {

            ObjectCache cache = MemoryCache.Default;
            Object fileContents = cache.Get(CacheKey);
            return fileContents;

            //            System.Runtime.Caching.ObjectCache objCache = HttpRuntime.Cache;
            //		System.c.Web.Caching.Cache objCache = HttpRuntime.Cache;
            //	return objCache[CacheKey];

        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(300.0);///5分钟 300秒

            cache.Set(CacheKey, objObject, policy);

            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            //objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            //objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);

            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = absoluteExpiration;
            policy.SlidingExpiration = slidingExpiration;

            cache.Set(CacheKey, objObject, policy);


            //ObjectCache cache = MemoryCache.Default;
            //cache.Set(

        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">Key</param>
        public static void Remove(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            if (cache.Contains(key))
            {
                cache.Remove(key);
            }
        }

    }


    ///// <summary>
    ///// 缓存相关的操作类
    ///// 李天平
    ///// 2006.4.1
    ///// </summary>
    //public class DataCache
    //{
    //    /// <summary>
    //    /// 获取当前应用程序指定CacheKey的Cache值
    //    /// </summary>
    //    /// <param name="CacheKey"></param>
    //    /// <returns></returns>
    //    public static object GetCache(string CacheKey)
    //    {
    //        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
    //        return objCache[CacheKey];
    //    }

    //    /// <summary>
    //    /// 设置当前应用程序指定CacheKey的Cache值
    //    /// </summary>
    //    /// <param name="CacheKey"></param>
    //    /// <param name="objObject"></param>
    //    public static void SetCache(string CacheKey, object objObject)
    //    {
    //        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
    //        objCache.Insert(CacheKey, objObject);
    //    }


    //}
}
