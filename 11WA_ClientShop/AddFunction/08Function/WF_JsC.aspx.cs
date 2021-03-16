using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.AddFunction._08Function
{
    public partial class WF_JsC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.Query;
            Response.Write(url);
            //url = ? id = 5 & name = kelli
        }
    }
}