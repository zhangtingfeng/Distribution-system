using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WeiXin.Lib.Core.Helper;
using WeiXin.Lib.Core.Helper.WXPay;
using WeiXin.Lib.Core.Models.UnifiedMessage;
using WeiXin.Lib.Core.PayModel;
using Wxpay;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doWS_GetWeiXinSign 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
   
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doWS_GetWeiXinSign : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        /// <summary>
        /// 微信签名相关信息 
        /// </summary>       
        /// <returns></returns>
        [WebMethod]
        public String _GetWeiXinSign()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            String strhttpURL = context.QueryString["httpURL"];
            String strShopClientID = context.QueryString["ShopClientID"];

            int intErrorCode = 1;
            string strReturn = "";
            try
            {
                //strhttpURL = strhttpURL.Replace("(", ":"); //g全局('', '*');
                //strhttpURL = strhttpURL.Replace(")", "?"); //g全局('', '*');
                //strhttpURL = strhttpURL.Replace("*", "&"); //g全局('', '*');
                //strhttpURL = strhttpURL.Replace("@", "/"); //g全局('', '*');//varPath.Replace('/', '@');///他自己有许多&  ，将来替换回来
                //strhttpURL = strhttpURL.Replace("^", "="); //g全局('', '*');//varPath.Replace('=', '^');
                strhttpURL = HttpUtility.UrlDecode(strhttpURL);

                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);
                Eggsoft_Public_CL.WxConfig myWxConfig = new Eggsoft_Public_CL.WxConfig(intShopClientID, strhttpURL);
                intErrorCode = 0;
                strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\",\"appId\":\"" + myWxConfig.appid + "\",\"nonceStr\":\"" + myWxConfig.nonceStr + "\",\"timestamp\":\"" + myWxConfig.timeStamp + "\",\"signature\":\"" + myWxConfig.paySign + "\"}";

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                intErrorCode = -1;
                strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
            }
            finally
            {

            }
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.ContentType = ("application/json;charset=UTF-8");
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "";
        }

        
        /// <summary>
        /// 微信支付相关信息 
        /// </summary>       
        /// <returns></returns>
        [WebMethod]
       
        public String _GetWeiXinPay()
        {
            String WXSPtypeApp = "";
            string strReturn = "";
            try
            {
                strReturn = _14WcfService1.SmallProgram.ClassPay.spayModelGetPay(HttpContext.Current.Request);

            }
            catch (Exception eeeeeException)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeeeeException, "微信支付相关信息 ", "程序报错");
            }

            if (HttpContext.Current.Request["jsonp"] != null || (WXSPtypeApp == "WXSP"))//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.ContentType = ("application/json;charset=UTF-8");
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "";
        }

        /// <summary>
        /// 微云基石 发送 双色球 等获取信息。
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String PubSendWeiXinMessage(string strStringJson, String md5DESCrypt)
        {
            string strPrivateKey = "546354683465907u34u6938465938746905834756";
            if (Eggsoft.Common.DESCrypt.GetMd5Str32(strStringJson + strPrivateKey) == md5DESCrypt)
            {
                int pub_Int_Session_CurUserID = 8568;
                int pub_Int_ShopClientID = 1;

                strStringJson = strStringJson.Replace("\"", "");

                Eggsoft.Common.debug_Log.Call_WriteLog("发送微信通知", "一元云购程序");
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("219-235-0-112请10点之前 桌面修改", strStringJson, strStringJson, "http://www.zhcw.com/");
                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                WeiXinTuWens_ArrayList.Add(First);

                string strRrrrCode = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pub_Int_Session_CurUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                return "0";
            }
            else
            {
                return "-1";
            }
        }

    }
}
