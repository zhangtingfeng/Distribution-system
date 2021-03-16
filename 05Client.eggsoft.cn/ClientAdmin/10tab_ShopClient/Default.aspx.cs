using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    public partial class Default1 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList_Class1_SelectedIndexChanged(object sender, EventArgs e)
        {


            DropDownList_Class2.DataSource = new EggsoftWX.BLL.tab_Class2().GetDataTable("100", "ID,ClassName", "Class1_ID=" + DropDownList_Class1.SelectedValue);
            DropDownList_Class2.DataTextField = "ClassName";
            DropDownList_Class2.DataValueField = "ID";
            DropDownList_Class2.DataBind();
        }
        protected void DropDownList_Class2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList_Class3.DataSource = new EggsoftWX.BLL.tab_Class3().GetDataTable("100", "ID,ClassName", "Class2_ID=" + DropDownList_Class2.SelectedValue);
            DropDownList_Class3.DataTextField = "ClassName";
            DropDownList_Class3.DataValueField = "ID";
            DropDownList_Class3.DataBind();

        }
    }
}