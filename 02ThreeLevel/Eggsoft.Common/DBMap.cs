using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;


//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    /// <summary>
    /// 数据库表影射
    /// </summary>
    public class DBMap
    {

        private static string GetTablePre()
        {
            string CacheKey = "Tiger_TablePre";
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                string ModelCache = ConfigHelper.GetConfigString("Eggsoft_TablePre");
                DataCache.SetCache(CacheKey, (object)ModelCache);
                objModel = (object)ModelCache;
                //DataCache.Insert("Eggsoft_TablePre", ConfigurationManager.AppSettings["Eggsoft_TablePre"], 10 * 24 * 60 * 60);
            }
            return objModel as string;;
            //return CacheUtil.Read("Eggsoft_TablePre") 
        }

        public static string DotIdentification_TestRecord
        {
            get { return GetTablePre() + "Identification_TestRecord"; }
        }

        public static string DotStudent
        {
            get { return GetTablePre() + "Student"; }
        }
    }
}
