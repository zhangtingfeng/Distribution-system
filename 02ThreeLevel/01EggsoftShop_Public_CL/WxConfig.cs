using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;



namespace Eggsoft_Public_CL
{

    /// <summary>
    ///Pub 的摘要说明
    /// </summary>
    public class WxConfig
    {

        #region Model WxConfig
        public String _appid = "";
        public String _paySign = "";
        public String _timeStamp = "";
        public String _nonceStr = "";
        public String _strURL = "";


        /// <summary>
        /// 
        /// </summary>
        public string appid
        {
            get { return _appid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string paySign
        {
            get { return _paySign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string timeStamp
        {
            get { return _timeStamp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nonceStr
        {
            get { return _nonceStr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strURL
        {
            get { return _strURL; }
        }


        #endregion Model

        public WxConfig(int intargShopClientID = 0, string strargURL = "")///调整为能适合微信端静态报价的
        {
            int intShopClientID = 0;
            if (intargShopClientID > 0)
            {
                intShopClientID = intargShopClientID;
            }
            else
            {
                string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
                int.TryParse(strShopClientID, out intShopClientID);
            }
            if (intShopClientID == 0)
            {
                _appid = Eggsoft_Public_CL.tab_System_And_.getTab_System("WeiXinAppId");
            }
            else
            {
                EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);

                if (Model_tab_ShopClient_EngineerMode != null)
                {
                    _appid = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                }
            }



            tenpayApp.RequestHandler packageReqHandler = new tenpayApp.RequestHandler(HttpContext.Current);
            string strCheck_SocialPlatform = Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform();
            string strGetTicket = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_GetTicket(intShopClientID);

            _timeStamp = tenpayApp.TenpayUtil.getTimestamp();
            _nonceStr = tenpayApp.TenpayUtil.getNoncestr();

            if (String.IsNullOrEmpty(strargURL) == false)
            {
                _strURL = strargURL;
            }
            else
            {
                _strURL = Eggsoft.Common.Application.httpFullUrl_BeforeUrlRewriting();
            }

            Eggsoft.Common.debug_Log.Call_WriteLog("_strURL=" + _strURL + " nonceStr=" + nonceStr + " strGetTicket=" + strGetTicket + " timeStamp=" + timeStamp);


            //获取package包
            //Eggsoft.Common.JsUtil.ShowMsg
            string packageValue = packageReqHandler.getRequestURL();
            tenpayApp.RequestHandler payHandler = new tenpayApp.RequestHandler(HttpContext.Current);
            payHandler.setParameter("url", _strURL);
            payHandler.setParameter("noncestr", nonceStr);
            payHandler.setParameter("jsapi_ticket", strGetTicket);//oIcdQuHFHXYo6uM2dHihP3tUhqHI
            payHandler.setParameter("timestamp", timeStamp);


            _paySign = payHandler.createSHA1Sign();

        }
        /// <summary>
        /// 分享朋友圈
        /// </summary>
        /// <param name="strReturnFunction">存在的回调函数</param>
        /// <returns></returns>

        public string WxConfig_Get_PulicChageWeiXin(string strReturnFunction = "DefaultNoFunction")
        {
            string strWxConfig_Get_PulicChageWeiXin = "";

            strWxConfig_Get_PulicChageWeiXin += "<script src=\"https://res.wx.qq.com/open/js/jweixin-1.0.0.js\"></script>\n";
            strWxConfig_Get_PulicChageWeiXin += "<script>\n";
            strWxConfig_Get_PulicChageWeiXin += "$(document).ready(function(){ wx.config({\n";
            strWxConfig_Get_PulicChageWeiXin += "    debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。\n";
            strWxConfig_Get_PulicChageWeiXin += "    appId: '" + _appid + "',\n";
            strWxConfig_Get_PulicChageWeiXin += "    timestamp: " + _timeStamp + ",\n";
            strWxConfig_Get_PulicChageWeiXin += "    nonceStr: '" + _nonceStr + "',\n";
            strWxConfig_Get_PulicChageWeiXin += "    signature: '" + _paySign + "',\n";
            strWxConfig_Get_PulicChageWeiXin += "    jsApiList: [\n";
            strWxConfig_Get_PulicChageWeiXin += "      // 所有要调用url=" + _strURL + "的 API 都要加到这个列表中\n";
            strWxConfig_Get_PulicChageWeiXin += "       'onMenuShareTimeline','getLatestAddress','editAddress',\n";
            strWxConfig_Get_PulicChageWeiXin += "'onMenuShareAppMessage'\n";
            strWxConfig_Get_PulicChageWeiXin += "    ]\n";
            strWxConfig_Get_PulicChageWeiXin += " });\n";


            if (strReturnFunction == "DefaultNoFunction")
            {
                strWxConfig_Get_PulicChageWeiXin += " wx.ready(function () {\n";
                strWxConfig_Get_PulicChageWeiXin += "    // 在这里调用 API\n";
                strWxConfig_Get_PulicChageWeiXin += "\n";
                strWxConfig_Get_PulicChageWeiXin += "     var shareData = {\n";
                strWxConfig_Get_PulicChageWeiXin += "        title: WeiXin_shareAppAllPageTitle,\n";
                strWxConfig_Get_PulicChageWeiXin += "        desc: WeiXin_descAppPageContent,\n";
                strWxConfig_Get_PulicChageWeiXin += "        link: path,\n";
                strWxConfig_Get_PulicChageWeiXin += "       imgUrl: WeiXin_imgAllPageUrl\n";
                strWxConfig_Get_PulicChageWeiXin += "    };\n";
                //strWxConfig_Get_PulicChageWeiXin += "    alert(path);\n";

                strWxConfig_Get_PulicChageWeiXin += "    wx.onMenuShareAppMessage(shareData);\n";
                strWxConfig_Get_PulicChageWeiXin += "    wx.onMenuShareTimeline(shareData);\n";



                strWxConfig_Get_PulicChageWeiXin += "\n";
                strWxConfig_Get_PulicChageWeiXin += " });\n";
                strWxConfig_Get_PulicChageWeiXin += "\n";
            }
            else
            {


                strWxConfig_Get_PulicChageWeiXin += "       wx.ready(function () { \n";
                strWxConfig_Get_PulicChageWeiXin += "           wx.onMenuShareAppMessage({\n";
                strWxConfig_Get_PulicChageWeiXin += "               title: WeiXin_shareAppAllPageTitle, // 分享标题\n";
                strWxConfig_Get_PulicChageWeiXin += "               desc: WeiXin_descAppPageContent, // 分享描述\n";
                strWxConfig_Get_PulicChageWeiXin += "               link: path, // 分享链接\n";
                strWxConfig_Get_PulicChageWeiXin += "               imgUrl: WeiXin_imgAllPageUrl, // 分享图标\n";
                strWxConfig_Get_PulicChageWeiXin += "               type: '', // 分享类型,music、video或link，不填默认为link\n";
                strWxConfig_Get_PulicChageWeiXin += "              dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空\n";
                strWxConfig_Get_PulicChageWeiXin += "              success: function () {\n";
                strWxConfig_Get_PulicChageWeiXin += "                  //	alert(\"arg1\");\n";
                if (strReturnFunction != "DefaultNoFunction") strWxConfig_Get_PulicChageWeiXin += "               " + strReturnFunction + "();\n";
                strWxConfig_Get_PulicChageWeiXin += "                  // 用户确认分享后执行的回调函数\n";
                strWxConfig_Get_PulicChageWeiXin += "              },\n";
                strWxConfig_Get_PulicChageWeiXin += "              cancel: function () {\n";
                strWxConfig_Get_PulicChageWeiXin += "                  //  alert(\"用户取消分享朋友后执行的回调函数\");\n";
                strWxConfig_Get_PulicChageWeiXin += "                  // 用户取消分享后执行的回调函数\n";
                strWxConfig_Get_PulicChageWeiXin += "             },\n";
                strWxConfig_Get_PulicChageWeiXin += "\n";
                strWxConfig_Get_PulicChageWeiXin += "         });\n";
                strWxConfig_Get_PulicChageWeiXin += "\n";
                strWxConfig_Get_PulicChageWeiXin += "        wx.onMenuShareTimeline({\n";
                strWxConfig_Get_PulicChageWeiXin += "            title: WeiXin_shareAppAllPageTitle, // 分享标题\n";
                strWxConfig_Get_PulicChageWeiXin += "            link: path, // 分享链接\n";
                strWxConfig_Get_PulicChageWeiXin += "           imgUrl: WeiXin_imgAllPageUrl, // 分享图标\n";
                strWxConfig_Get_PulicChageWeiXin += "            success: function () {\n";
                strWxConfig_Get_PulicChageWeiXin += "               ///alert(\"arg2\");\n";
                strWxConfig_Get_PulicChageWeiXin += "               " + strReturnFunction + "();\n";
                strWxConfig_Get_PulicChageWeiXin += "               // 用户确认分享后执行的回调函数\n";
                strWxConfig_Get_PulicChageWeiXin += "           },\n";
                strWxConfig_Get_PulicChageWeiXin += "           cancel: function () {\n";
                strWxConfig_Get_PulicChageWeiXin += "               //  alert(\"用户取消分享朋友圈后执行的回调函数\");\n";
                strWxConfig_Get_PulicChageWeiXin += "               // 用户取消分享后执行的回调函数\n";
                strWxConfig_Get_PulicChageWeiXin += "           }\n";
                strWxConfig_Get_PulicChageWeiXin += "      });\n";
                strWxConfig_Get_PulicChageWeiXin += "\n";
                strWxConfig_Get_PulicChageWeiXin += "\n";
                strWxConfig_Get_PulicChageWeiXin += "\n";
                strWxConfig_Get_PulicChageWeiXin += "    });\n";
            }
            strWxConfig_Get_PulicChageWeiXin += " wx.error(function (res) {\n";
            strWxConfig_Get_PulicChageWeiXin += "    //alert(res.errMsg);\n";
            strWxConfig_Get_PulicChageWeiXin += " });\n";
            strWxConfig_Get_PulicChageWeiXin += "});</script>\n";

            return strWxConfig_Get_PulicChageWeiXin;
        }

        public static string WxConfig_Change_PulicChageWeiXin(string strTemplet, string strReturnFunction = "DefaultNoFunction")
        {

            //return "";


            string strDo = "";


            #region 本地调试 不要这个
            String strLocalHostDebug_ShopClientID_LocalHostDebug = ConfigurationManager.AppSettings["LocalHostDebug_ShopClientID_LocalHostDebug"];
            int int_ShopClientID = 0;
            int.TryParse(strLocalHostDebug_ShopClientID_LocalHostDebug, out int_ShopClientID);
            int_ShopClientID = 0;
            if (int_ShopClientID > 0)
            {
                strDo = "";
            }
            else
            {
                WxConfig myWxConfig = new WxConfig();
                strDo = myWxConfig.WxConfig_Get_PulicChageWeiXin(strReturnFunction);
            }
            #endregion
            strTemplet = strTemplet.Replace("###_PulicChageWeiXin###", strDo);

            return strTemplet;
        }
    }
}