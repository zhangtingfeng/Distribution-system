using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.User
{
    public partial class ShopLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Literal__Change.Text = "用户名：";
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Literal__Change.Text = DropDownList1.SelectedItem.ToString() + ":";
        }



        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string strValue = DropDownList1.SelectedValue;


            String strWhere = "";
            switch (strValue)
            {
                case "1":
                    strWhere = "username='" + TextBox_uid.Text.Trim() + "'";
                    break;
                case "2":

                    strWhere = "ContactPhone='" + TextBox_uid.Text.Trim() + "'";

                    break;
                case "3":

                    strWhere = "Email='" + TextBox_uid.Text.Trim() + "'";
                    break;
            }


            string strDESUserPass = Eggsoft.Common.DESCrypt.GetMd5Str32(TextBox_pwd.Text.Trim());
            strWhere = strWhere + " and password='" + strDESUserPass + "'";

            EggsoftWX.BLL.tab_ShopClient mytab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            bool boolExsit = mytab_ShopClient.Exists(strWhere);
            if (boolExsit)
            {
                Eggsoft.Common.JsUtil.TipAndRedirect("关联成功。", "/cart.aspx", "1");

            }
            else
            {
                Eggsoft.Common.JsUtil.ShowMsg("关联失败，信息不匹配");
            }

        }
    }
}