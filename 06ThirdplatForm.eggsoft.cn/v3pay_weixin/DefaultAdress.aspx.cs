using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _06ThirdplatForm.eggsoft.cn.v3pay_weixin
{
    public partial class DefaultAdress : System.Web.UI.Page
    {
        protected String pub_GetQueryString_state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string strAdressCallBackURL = Request.QueryString["AdressCallBackURL"];
            pub_GetQueryString_state = HttpUtility.UrlDecode(strAdressCallBackURL);
        }
    }
}