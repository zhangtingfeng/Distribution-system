using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// GoodP_QR 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GoodP_QR : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public String APPCODE_GetGoodErWeiMaImage(string strType, string strShopID, string strhttpUrl, string strParentID, string strGoodsID, string strOperationID = "0")
        {
            //Eggsoft_Public_CL.GoodP.APPCODE_GetGoodErWeiMaImage(strIcon);

            string strGetGoodErWeiMaImage = Class_Pub.APPCODE_GetGoodErWeiMaImage(strType, strShopID, strhttpUrl, strParentID, strGoodsID, strOperationID);

            string str = "{\"Errcode\":\"0\",\"GetGoodErWeiMaImage\":\"" + strGetGoodErWeiMaImage + "\"}";
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return str;
            //return str;

        }
    }
}
