using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.User
{
    public partial class WeiXinOpenID : System.Web.UI.Page
    {
        protected String pub_strApplicationCheckName = "";
        protected String pub_strApplicationCheckName_S_A_t = "";
        protected String pub_strApplicationCheckName_Scope = "";
        protected String pub_strState = "";
        protected string pub_strShopClientID = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
                    pub_strShopClientID = strShopClientID;


                    string strmyjson_OpenID_openid = Request.QueryString["myjson_OpenID_openid"];
                    string strLocalStorgeCallbackURL = Request.QueryString["LocalStorgeCallbackURL"];
                    string strScopeAccess_token = Request.QueryString["ScopeAccess_token"];
                    string strscope = Request.QueryString["scope"];

                    String strApplicationCheckName = Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName();



                    Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strmyjson_OpenID_openid);
                    Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName + "_S_A_t", strScopeAccess_token);///决定如何检查网页授权的东东
                    Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName + "_Scope", strscope);///决定如何检查网页授权的东东

                    #region  不是本地存储 也执行一下本地存盘
                    bool boolFromLocalStore = false;
                    string strtype = Request.QueryString["type"];
                    if (String.IsNullOrEmpty(strtype) == false)
                    {
                        if (strtype.ToLower() == "ReadedlocalStorageFromWeiXinOpenID")
                        {
                            boolFromLocalStore = true;
                        }
                    }
                    #endregion

                   
                    if (boolFromLocalStore == true)
                    {
                        HttpContext.Current.Response.Redirect(strLocalStorgeCallbackURL);///直接跳回c
                    }
                    else
                    {
                        pub_strApplicationCheckName = strmyjson_OpenID_openid;
                        pub_strApplicationCheckName_S_A_t = strScopeAccess_token;
                        pub_strApplicationCheckName_Scope = strscope;

                        pub_strState = strLocalStorgeCallbackURL;
                    }

                }
                catch (System.Threading.ThreadAbortException euhff)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(euhff, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }
            }
        }
    }
}