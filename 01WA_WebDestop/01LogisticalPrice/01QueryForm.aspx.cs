using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _01WA_WebDestop._01LogisticalPrice
{
    public partial class _01QueryForm : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Eggsoft.Common.Session.Add("Country", TextBox_DesCountry.Text);
            Eggsoft.Common.Session.Add("kgs", TextBox1kgs.Text);
            Eggsoft.Common.Session.Add("type", DropDownListtype.Text);


            String strShopClientID = HttpContext.Current.Request["ShopClientID"];//是不是访问代理的网页；
            Response.Redirect("02QueryList.aspx");
        }
    }
}