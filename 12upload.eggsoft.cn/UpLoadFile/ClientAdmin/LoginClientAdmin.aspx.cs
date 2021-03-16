using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin
{
    public partial class LoginClientAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = Request.QueryString["Act"];
                if (type != null)
                {
                    if (type.ToString() == "gotouserFrom_ClientAdmin")
                    {
                        string stringUserID = Request.QueryString["ShoClientID"];
                        Eggsoft.Common.CommAuthen.Client_SetAuthenAdmin(stringUserID);
                        string GoToUrl = Request.QueryString["GoToUrl"];
                        Eggsoft.Common.JsUtil.LocationNewHref(Server.UrlDecode(GoToUrl));
                        //Eggsoft.Common.JsUtil.ShowMsg("登陆成功!", "default.aspx");
                    }
                }
            }
        }
    }
}