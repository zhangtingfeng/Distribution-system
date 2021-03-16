using System;
using System.Web;
using System.Runtime.Caching;

namespace Eggsoft.Common
{
    /// <summary>
    /// ������صĲ�����
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
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
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(300.0);///5���� 300��

            cache.Set(CacheKey, objObject, policy);

            //System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            //objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
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
        /// �Ƴ�����
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
    ///// ������صĲ�����
    ///// ����ƽ
    ///// 2006.4.1
    ///// </summary>
    //public class DataCache
    //{
    //    /// <summary>
    //    /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
    //    /// </summary>
    //    /// <param name="CacheKey"></param>
    //    /// <returns></returns>
    //    public static object GetCache(string CacheKey)
    //    {
    //        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
    //        return objCache[CacheKey];
    //    }

    //    /// <summary>
    //    /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
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
