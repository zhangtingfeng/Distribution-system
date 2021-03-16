using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Eggsoft_Public_CL
{
    /// <summary>
    ///tab_System_And_ 的摘要说明
    /// </summary>
    public class tab_System_And_
    {
        public tab_System_And_()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string getTab_System(string strShopType)
        {
            String strCityItem = "";

            string CacheKey = "System." + strShopType;
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    EggsoftWX.BLL.tab_System BLL_Tab_System = new EggsoftWX.BLL.tab_System();
                    if (BLL_Tab_System.Exists("ShopType='" + strShopType + "'"))
                    {
                        //Eggsoft.Common.JsUtil.ShowMsg(strShopType);
                        strCityItem = BLL_Tab_System.GetList("ShopInfo", "ShopType='" + strShopType + "'").Tables[0].Rows[0][0].ToString();
                    }

                    Eggsoft.Common.DataCache.SetCache(CacheKey, strCityItem);// 写入缓存
                }
                catch { }
            }
            else
            {
                strCityItem = objType.ToString();
            }
            return strCityItem;
        }

        public static string setTab_System(String strShopType, String strCityItem)
        {

            EggsoftWX.BLL.tab_System BLL_Tab_System = new EggsoftWX.BLL.tab_System();
            if (BLL_Tab_System.Exists("ShopType='" + strShopType + "'"))
            {
                BLL_Tab_System.Update("ShopInfo='" + strCityItem + "'", "ShopType='" + strShopType + "'");
            }
            else
            {
                BLL_Tab_System.Add("ShopInfo,ShopType", "'" + strCityItem + "'" + "," + "'" + strShopType + "'");
            }
            string CacheKey = "System." + strShopType;
            Eggsoft.Common.DataCache.SetCache(CacheKey, strCityItem);// 写入缓存
            return strCityItem;
        }

        //public static string getTab_System(string strShopType)
        //{
        //    String strCityItem = "";

        //    string CacheKey = "System." + strShopType;
        //    object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objType == null)
        //    {
        //        try
        //        {
        //            EggsoftWX.BLL.tab_System BLL_Tab_System = new EggsoftWX.BLL.tab_System();
        //            if (BLL_Tab_System.Exists("ShopType='" + strShopType + "'"))
        //            {
        //                strCityItem = BLL_Tab_System.GetList("ShopInfo", "ShopType='" + strShopType + "'").Tables[0].Rows[0][0].ToString();
        //            }

        //            Eggsoft.Common.DataCache.SetCache(CacheKey, strCityItem);// 写入缓存
        //        }
        //        catch { }
        //    }
        //    else
        //    {
        //        strCityItem = objType.ToString();
        //    }
        //    return strCityItem;
        //}


        //public static string setTab_System(String strShopType, String strCityItem)
        //{

        //    EggsoftWX.BLL.tab_System BLL_Tab_System = new EggsoftWX.BLL.tab_System();
        //    if (BLL_Tab_System.Exists("ShopType='" + strShopType + "'"))
        //    {
        //        BLL_Tab_System.Update("ShopInfo='" + strCityItem + "'", "ShopType='" + strShopType + "'");
        //    }
        //    else
        //    {
        //        BLL_Tab_System.Add("ShopInfo,ShopType", "'" + strCityItem + "'" + "," + "'" + strShopType + "'");
        //    }
        //    string CacheKey = "System." + strShopType;
        //    Eggsoft.Common.DataCache.SetCache(CacheKey, strCityItem);// 写入缓存
        //    return strCityItem;
        //}


    }
}