using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.User
{
    public partial class AjaxUserInfoModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Request.QueryString["userName"].ToString();
            if (userName == "James Hao")
            {
                Response.Write("用户名已经存在！");
            }
            else
            {
                Response.Write("您可以使用此用户名！");
            }
        }
    }
}