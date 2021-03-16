using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _06ThirdplatForm.eggsoft.cn.wxurl
{
    public partial class myOauth1 : System.Web.UI.Page
    {

        public class json_OpenID
        {//        {
            //    "access_token": "OezXcEiiBSKSxW0eoylIeAsR0GmYd1awCffdHgb4fhS_KKf2CotGj2cBNUKQQvj-G0ZWEE5-uBjBz941EOPqDQy5sS_GCs2z40dnvU99Y5AI1bw2uqN--2jXoBLIM5d6L9RImvm8Vg8cBAiLpWA8Vw",
            //    "expires_in": 7200,
            //    "refresh_token": "OezXcEiiBSKSxW0eoylIeAsR0GmYd1awCffdHgb4fhS_KKf2CotGj2cBNUKQQvj-G0ZWEE5-uBjBz941EOPqDQy5sS_GCs2z40dnvU99Y5CZPAwZksiuz_6x_TfkLoXLU7kdKM2232WDXB3Msuzq1A",
            //    "openid": "oLVPpjqs9BhvzwPj5A-vTYAX3GLc",
            //    "scope": "snsapi_userinfo,"
            //}
            public string access_token { get; set; }
            public string expires_in { get; set; }
            public string refresh_token { get; set; }
            public string openid { get; set; }
            public string scope { get; set; }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ///可恶 啊 这里会加载2次
            try
            {
                string code = Request.QueryString["code"];// 用户授权并获取code    ///如果用户同意授权，页面将跳转至 redirect_uri/?code=CODE&state=STATE。若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
                string strstate = Request.QueryString["state"];//用户正在访问的页面；恢复人家正在访问的
                string strWeiXinAppId = "";
                string strWeiXinAppSecret = "";


                String strShopClientID = Request.QueryString["ShopClientID"];

                EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);

                if (Model_tab_ShopClient_EngineerMode != null)
                {
                    strWeiXinAppId = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                    strWeiXinAppSecret = Model_tab_ShopClient_EngineerMode.WeiXinAppSecret;
                    Eggsoft.Common.debug_Log.Call_WriteLog("strWeiXinAppId=" + strWeiXinAppId);
                }



                String strURL = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + strWeiXinAppId + "&secret=" + strWeiXinAppSecret + "&code=" + code + "&grant_type=authorization_code";

                string str_json_openid = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_WeiXin_YiXin_Json(strURL);
                 json_OpenID myjson_OpenID = Eggsoft_Public_CL.JsonHelper.JsonDeserialize<json_OpenID>(str_json_openid);

                try
                {
                    #region 检查是否有openID

                    #region 尝试取出 取UrlFrom
                    Int32 Int32Stateurl = 0;
                    Int32.TryParse(strstate, out Int32Stateurl);
                    EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl BLL_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl();
                    bool boolstrstate = BLL_tab_ShopClient_WeiXin_Stateurl.Exists(Int32Stateurl);
                    if (boolstrstate)
                    {
                        strstate = BLL_tab_ShopClient_WeiXin_Stateurl.GetModel(Int32Stateurl).UrlFrom;
                    }
                    //Eggsoft.Common.JsUtil.ShowMsg("strstate11=" + strstate);
                    //Eggsoft.Common.debug_Log.Call_WriteLog("strstate11=" + strstate);

                    strstate = HttpUtility.UrlEncode(strstate);
                    //Eggsoft.Common.debug_Log.Call_WriteLog("strstate12=" + strstate);
                    #endregion
                    //可恶 啊 这里会加载2次
                    if (String.IsNullOrEmpty(myjson_OpenID.access_token) == false)
                    {
                        if (strstate.ToLower().IndexOf("type%3disreadweixinaddress") != -1)///用户拉微信收获地址
                        {
                            string strSearchURL = "/v3pay_weixin/DefaultAdress.aspx?ScopeAccess_token=" + myjson_OpenID.access_token + "&scope=" + myjson_OpenID.scope + "&myjson_OpenID_openid=" + myjson_OpenID.openid + "&AdressCallBackURL=" + strstate;
                            HttpContext.Current.Response.Redirect(strSearchURL, false);///用户拉起收获地址
                        }
                        else
                        {
                            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                            Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("ID=" + strShopClientID);


                            string strSearchURL = "https://" + Model_tab_ShopClient.ErJiYuMing + "/user/weixinopenid.aspx?scopeaccess_token=" + myjson_OpenID.access_token + "&scope=" + myjson_OpenID.scope + "&myjson_openid_openid=" + myjson_OpenID.openid + "&LocalStorgeCallbackURL=" + strstate;
                            Eggsoft.Common.debug_Log.Call_WriteLog("strSearchURL=" + strSearchURL);
                            HttpContext.Current.Response.Redirect(strSearchURL, false);///直接跳回c
                        }

                    }
                    #endregion


                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt,"auth认证", "线程异常");
                }
                catch (Exception eee)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(eee, "auth认证");
                    Response.End();
                }
                finally { }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "auth认证", "线程异常");
            }

            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee, "auth认证");
            }
            finally { }
        }
    }
}