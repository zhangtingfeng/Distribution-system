using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.User
{
    public partial class ShopReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_reg_Click(object sender, EventArgs e)
        {
            EggsoftWX.BLL.tab_ShopClient mytab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            bool boolExsit = mytab_ShopClient.Exists("UserName='" + txt_reguid.Text.Trim() + "'");

            if (boolExsit)
            {
                Eggsoft.Common.JsUtil.ShowMsg("用户名已存在，如忘记密码请联系我们！");
                Eggsoft.Common.JsUtil.TipAndRedirect("用户名已存在，可选择关联账户！", "Login.aspx", "1");

                txt_reguid.Focus();
            }
            else
            {
                EggsoftWX.Model.tab_ShopClient mytab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();
                string strDESUserPass = Eggsoft.Common.DESCrypt.GetMd5Str32(txt_regpwd.Text.Trim());

                mytab_ShopClient_Model.Username = txt_reguid.Text.Trim();
                mytab_ShopClient_Model.PassWord = strDESUserPass;
                mytab_ShopClient_Model.Email = txt_regemail.Text.Trim();
                mytab_ShopClient_Model.ContactPhone = txt_regphoneno.Text.Trim();
                mytab_ShopClient.Add(mytab_ShopClient_Model);


            }
        }
    }
}