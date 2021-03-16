using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.User
{
    public partial class Html5LocalStorge : System.Web.UI.Page
    {

        protected string pub_strShopClientID = "";
        protected string pub_strAspxCallBackURL = "";
        protected string pub_strmyOauth1URL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int intShopClientID = 0;
                    string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
                    pub_strShopClientID = strShopClientID;
                    int.TryParse(strShopClientID, out intShopClientID);
                    string strAlertDayScript = "";
                    DateTime? out_AuthorTime = DateTime.MinValue;
                    int span_Days = Eggsoft_Public_CL.ClassP.CheckAuthorTime(strShopClientID, out strAlertDayScript, out out_AuthorTime);///检查有效日期


                    //if (intShopClientID == 21)
                    //{
                    if (span_Days > 0)
                    {

                    }
                    else
                    {
                        //Eggsoft.Common.JsUtil.ShowMsg("数据维护中", "https://000001shiyidianzi.eggsoft.cn/product-2.aspx");
                        Eggsoft.Common.JsUtil.ShowMsg("数据维护中", -100);
                        return;//直接跳出
                    }
                    //}
                    //else
                    //{
                    //    if (span_Days > 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        Eggsoft.Common.JsUtil.ShowMsg("数据维护中", -100);
                    //        //Eggsoft.Common.JsUtil.ShowMsg("数据维护中", "https://000001shiyidianzi.eggsoft.cn/product-2.aspx");
                    //        //Eggsoft.Common.JsUtil.LocationNewHref("https://000001shiyidianzi.eggsoft.cn/product-2.aspx");
                    //        return;//直接跳出
                    //    }
                    //}

                    string strRedirect_uri = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/WxURL/myOauth1-" + intShopClientID + ".aspx";
                    strRedirect_uri = strRedirect_uri.ToLower();

                    string strWeiXinAppId = "";

                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                    Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);

                    if (Model_tab_ShopClient_EngineerMode != null)
                    {
                        strWeiXinAppId = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                    }

                    pub_strAspxCallBackURL = Request.QueryString["AspxCallBackURL"];
                    #region tab_ShopClient_WeiXin_stateurl   转换到一个整数
                    EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl BLL_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl();
                    bool boolstrstate = BLL_tab_ShopClient_WeiXin_Stateurl.Exists("UrlFrom='" + HttpUtility.UrlDecode(pub_strAspxCallBackURL) + "' and ShopClientID=" + intShopClientID);
                    Int32 int32AspxCallBackURLState = 0;
                    if (boolstrstate)
                    {
                        EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = BLL_tab_ShopClient_WeiXin_Stateurl.GetModel("UrlFrom='" + HttpUtility.UrlDecode(pub_strAspxCallBackURL) + "' and ShopClientID=" + intShopClientID);
                        Model_tab_ShopClient_WeiXin_Stateurl.updateTime = DateTime.Now;
                        Model_tab_ShopClient_WeiXin_Stateurl.intFromCount += 1;
                        BLL_tab_ShopClient_WeiXin_Stateurl.Update(Model_tab_ShopClient_WeiXin_Stateurl);
                        int32AspxCallBackURLState = Model_tab_ShopClient_WeiXin_Stateurl.ID;
                    }
                    else
                    {
                        EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl();
                        Model_tab_ShopClient_WeiXin_Stateurl.intFromCount = 1;
                        Model_tab_ShopClient_WeiXin_Stateurl.UrlFrom = Eggsoft.Common.StringNum.MaxLengthString(HttpUtility.UrlDecode(pub_strAspxCallBackURL), 400);
                        Model_tab_ShopClient_WeiXin_Stateurl.ShopClientID = intShopClientID;
                        int32AspxCallBackURLState = BLL_tab_ShopClient_WeiXin_Stateurl.Add(Model_tab_ShopClient_WeiXin_Stateurl);
                    }
                    #endregion
                    string strWXRedirect_uri = System.Web.HttpContext.Current.Server.UrlEncode(strRedirect_uri);
                    pub_strmyOauth1URL = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strWeiXinAppId + "&redirect_uri=" + strWXRedirect_uri + "&response_type=code&scope=snsapi_base&state=" + int32AspxCallBackURLState + "#wechat_redirect";
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception ee)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ee, "本地登录");
                }
                finally { }
            }
        }
    }
}