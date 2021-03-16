using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.User
{
    public partial class HtmlLoginOnyForjump : System.Web.UI.Page
    {
        int pInt_Session_CurUserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pInt_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();

                if (pInt_Session_CurUserID > 0)
                {
                    string CallBackUrl = Request.QueryString["CallBackUrl"];
                    if (String.IsNullOrEmpty(CallBackUrl) == false)
                    {
                        Eggsoft.Common.JsUtil.LocationNewHref(CallBackUrl);
                    }
                    else
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(" Eggsoft.Common.JsUtil.LocationNewHref(CallBackUrl) Is null");

                    }

                }
            }
        }
    }
}