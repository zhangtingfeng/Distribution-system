using System;
using System.Web;
using System.Runtime.Caching;
namespace EggsoftWX.DALFactory
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
            //Object fileContents = cache[CacheKey] as Object;
            //'CacheItem fileContents = cache.GetCacheItem("filecontents");
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
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(600.0);

            cache.Set(CacheKey, objObject, policy);

			//System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			//objCache.Insert(CacheKey, objObject);
		}
	}
}



//ObjectCache cache = MemoryCache.Default;
//            string fileContents = cache["filecontents"] as string;
//            if (fileContents == null)
//            {
//                CacheItemPolicy policy = new CacheItemPolicy();
//                policy.AbsoluteExpiration = 
//                    DateTimeOffset.Now.AddSeconds(60.0);
                
//                List<string> filePaths = new List<string>();
//             string cachedFilePath = Server.MapPath("~") + 
//                 "\\cacheText.txt";
//             filePaths.Add(cachedFilePath);


//                policy.ChangeMonitors.Add(new 
//                    HostFileChangeMonitor(filePaths));

//                // Fetch the file contents.
//                fileContents = File.ReadAllText(cachedFilePath);
                
//                cache.Set("filecontents", fileContents, policy);

//            }

//            Label1.Text = fileContents;