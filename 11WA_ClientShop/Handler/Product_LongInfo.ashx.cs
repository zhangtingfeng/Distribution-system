using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Product_LongInfo 的摘要说明
    /// </summary>
    public class Product_LongInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string strGoodID = context.Request.QueryString["strGoodID"];
                //
                //string strContext=
                int pIntGoodID = 0;
                int.TryParse(strGoodID, out pIntGoodID);

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);

                string pGood_LongText = my_Model_tab_Goods.LongInfo.Trim();
                string strpGood_LongText_HtmlDecode = HttpContext.Current.Server.HtmlDecode(pGood_LongText);

                /*正则表达式替换求助
        如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
        例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                strpGood_LongText_HtmlDecode = System.Text.RegularExpressions.Regex.Replace(strpGood_LongText_HtmlDecode, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");

                string strSearch = "src=\"/upload/";
                string strpGood_LongText = strpGood_LongText_HtmlDecode.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                strSearch = "src=\"/Upload/";
                strpGood_LongText = strpGood_LongText.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                context.Response.Write(strpGood_LongText);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}