using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver._Admin
{
    public partial class Login : System.Web.UI.Page
    {
        EggsoftWX.BLL.tab_Admin_User bll_Admin_User = new EggsoftWX.BLL.tab_Admin_User();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (Eggsoft.Common.Session.Read("m._ShangHaiDianzi__VCode") != txtValidCode.Text.Trim())
                Eggsoft.Common.JsUtil.ShowMsg("验证码错误!", "?");
            AdminValidate(txtUserID, txtUserPass);
            AdminLogin(txtUserID, txtUserPass);
        }

        public void AdminValidate(TextBox txtUserID, TextBox txtUserPass)
        {
            Eggsoft.Common.ControlValidate.TextBoxValidate(txtUserID, "帐号", "NullVal||", "", "");
            Eggsoft.Common.ControlValidate.TextBoxValidate(txtUserPass, "密码", "Null||", "", "");
        }

        public void AdminLogin(TextBox txtUserID, TextBox txtUserPass)
        {

            string UserID = txtUserID.Text.Trim();
            string UserPass = txtUserPass.Text.Trim();
            string strDESUserPass = Eggsoft.Common.DESCrypt.GetMd5Str32(UserPass);

            if (UserID.Length < 2 || UserID.Length > 22)
                Eggsoft.Common.JsUtil.ShowMsg("帐号长度2~22个字符!", "javascript:history.back()");
            if (UserPass.Length < 2 || UserPass.Length > 15)
                Eggsoft.Common.JsUtil.ShowMsg("密码长度2~15个字符!" + UserPass.Length, "javascript:history.back()");
            if (bll_Admin_User.Exists("and UserName='" + UserID + "' and Password='" + strDESUserPass + "'"))
            {




                string userip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(userip))
                {
                    userip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (Eggsoft.Common.IPCheck.MainShowCheck(userip)) return;//限制一些ip的登录

                bll_Admin_User.Update("lastLoginIP='" + userip + "'", "UserName='" + UserID + "'");
                bll_Admin_User.Update("LoginTimes=LoginTimes+1", "UserName='" + UserID + "'");
                bll_Admin_User.Update("lastLoginTimes='" + DateTime.Now.ToString() + "'", "UserName='" + UserID + "'");




                Eggsoft.Common.CommAuthen._Admin_SetAuthen(UserID);


                Eggsoft.Common.debug_Log.Call_WriteLog("lastLoginIP='" + userip + "'UserName='" + UserID + "'", "微云总后台", "登陆成功");


                Eggsoft.Common.JsUtil.LocationNewHref("default.aspx");
            }
            else
            {
                Eggsoft.Common.JsUtil.ShowMsg("帐号密码错误!");
            }
        }

    }
}