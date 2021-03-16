using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver._Admin
{
    public partial class Left : Eggsoft.Common.DotAdminPage__Admin//System.Web.UI.Page
    {
        public String strDefault = "";
        //EggsoftWX.BLL.INC_User bll_INC_User = new EggsoftWX.BLL.INC_User();
        public string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        public String strBoardJPG = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            strBoardJPG = strUpLoadURL + "/UpLoadFile/_Admin/LoginAdmin.aspx?Act=gotouserFrom_Admin&UserID=" + Eggsoft.Common.Session.Read("Eggsoft__Admin__Users") + "&GoToUrl=" + Server.UrlEncode("EngineerMode/BoardJPG.aspx");
            // strResourceZhengWen = strUpLoadURL + "/UpLoadFile/_Admin/LoginAdmin.aspx?Act=gotouserFrom_Admin&UserID=" + Eggsoft.Common.Session.Read("Eggsoft__Admin__Users") + "&GoToUrl=" + Server.UrlEncode("EngineerMode/Resource-ZhengWen.aspx");
            if (Eggsoft.Common.Session.Read("Eggsoft__Admin__Users").ToString().ToLower() == "oliver")
            {
                strDefault = "BoardINC.aspx";

                string strEggsoft_Admin__Users = "";
                strEggsoft_Admin__Users += " <li>\n";
                strEggsoft_Admin__Users += "             <a target=\"main\" href=\"tab_Admin_User/BoardAdminSys.aspx\">管理员管理</a>\n";
                strEggsoft_Admin__Users += "           </li>\n";
                strEggsoft_Admin__Users += "            <li>\n";
                strEggsoft_Admin__Users += "              <a target=\"main\" href=\"tab_System/Board_Manage.aspx\">微店设置</a>\n";
                strEggsoft_Admin__Users += "           </li>\n";

                Literal_Author.Text = strEggsoft_Admin__Users;

            }
            else
            {
                string UserName = Eggsoft.Common.Session.Read("Eggsoft__Admin__Users");
                //string strTypeManageID = bll_INC_User.GetList("ID", "UserName='" + UserName+"'").Tables[0].Rows[0][0].ToString();
                //strDefault = "../default-" + strTypeManageID+".aspx";
            }
        }




        public String getStyleShow(string strMenu)
        {
            string strgetStyleShow = "";

            if (strMenu == "WeiXin")
            {
                if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "YiXin")
                {

                    strgetStyleShow = " style=\"DISPLAY: none\"";
                }

            }
            else if (strMenu == "YiXin")
            {
                if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "WeiXin")
                {

                    strgetStyleShow = " style=\"DISPLAY: none\"";
                }

            }
            return strgetStyleShow;

        }
        public String DisPlay(int intMenu)
        {
            string strDisPlay = "";
            string strNotDisPlay = " style=\"DISPLAY: none\"";

            string strLevel = Eggsoft.Common.Session.Read("Eggsoft__Admin__Users").ToString().ToLower();
            EggsoftWX.BLL.tab_Admin_User BLL_tab_Admin_User = new EggsoftWX.BLL.tab_Admin_User();
            EggsoftWX.Model.tab_Admin_User Modeltab_Admin_User = BLL_tab_Admin_User.GetModel("UserName='" + strLevel + "'");
            if (Modeltab_Admin_User == null) {
                CommAuthen._Admin_AdminLogout();
            }

            if (intMenu == 1)//基本管理
            {
                if (Modeltab_Admin_User.ManagerLevel > 10)
                {
                    strDisPlay = strNotDisPlay;
                }
            }

            if (intMenu == 2)//都显示  商品分类管理
            {
                if (Modeltab_Admin_User.ManagerLevel > 10)
                {
                    strDisPlay = strNotDisPlay;
                }
            }


            if (intMenu == 3)//商户管理模块
            {
                //if (Modeltab_Admin_User.ManagerLevel > 10)
                //{
                //    strDisPlay = strNotDisPlay;
                //}
            }


            if (intMenu == 55)//帮助文档管理
            {
                //if (Modeltab_Admin_User.ManagerLevel > 10)
                //{
                //    strDisPlay = strNotDisPlay;
                //}
            }


            if (intMenu == 4)//开发模式   关闭 不使用
            {
                //if (Modeltab_Admin_User.ManagerLevel > 10)
                //{
                strDisPlay = strNotDisPlay;
                //}
            }

            if (intMenu == 41)//开发模式
            {

                if (Eggsoft.Common.Session.Read("Eggsoft__Admin__Users").ToString().ToLower() != "oliver")
                {
                    strDisPlay = strNotDisPlay;
                }
            }
            if (intMenu == 255)//信息统计
            {

            }

            return strDisPlay;
        }

    }



}