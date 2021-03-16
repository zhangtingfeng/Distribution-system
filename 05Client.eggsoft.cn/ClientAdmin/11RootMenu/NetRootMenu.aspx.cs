using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._11RootMenu
{
    public partial class NetRootMenu : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_NET_ROOT_Menu BLL_tab_ShopClient_NET_ROOT_Menu = new EggsoftWX.BLL.tab_ShopClient_NET_ROOT_Menu();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_NetRootMenu")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                IniReadRootMenu();
                IniReadMenuContent();
            }
        }

        protected void IniReadMenuContent()
        {
            String strThisIDURL = Request.QueryString["ThisID"];
            if (strThisIDURL != null)
            {
                EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu Model_tab_ShopClient_NET_ROOT_Menu = BLL_tab_ShopClient_NET_ROOT_Menu.GetModel(Convert.ToInt32(strThisIDURL));
                TextBox_MenuName.Text = Model_tab_ShopClient_NET_ROOT_Menu.MenuName;
                TextBox_MenuContent.Text = Model_tab_ShopClient_NET_ROOT_Menu.MenuLink;
                TextBox_Pos.Text = Model_tab_ShopClient_NET_ROOT_Menu.Pos.ToString();
            }
            else
            {
                Button_EditMenu.Visible = false;
                Button_DeleteMenu.Visible = false;
                Button_NewChildMenu.Visible = false;
            }

        }

        protected void IniReadRootMenu()
        {
            String strParentID = Request.QueryString["ParentID"];
            String strThisID = Request.QueryString["ThisID"];

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            bool myINC_User_ID = BLL_tab_ShopClient_NET_ROOT_Menu.Exists("ParentID=0 and ShopClientID=" + strShopClientID);
            if (myINC_User_ID)
            {
                Button_ReadDefaultMenu0.Visible = false;///有的话 就不读取默认

                System.Data.DataTable myDataTable = BLL_tab_ShopClient_NET_ROOT_Menu.GetList("ParentID=0 and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strMenuName = myDataTable.Rows[i]["MenuName"].ToString();
                    string strMenuLink = myDataTable.Rows[i]["MenuLink"].ToString();
                    string strID = myDataTable.Rows[i]["ID"].ToString();

                    TreeNode tnRoot = new TreeNode();
                    tnRoot.Text = strMenuName;
                    tnRoot.Value = strID;
                    tnRoot.ToolTip = "单击进行编辑" + strID;
                    tnRoot.NavigateUrl = "NetRootMenu.aspx?ParentID=" + strID + "&ThisID=" + strID;
                    TreeView_Menu.Nodes.Add(tnRoot);


                    if (strThisID == strID)
                    {
                        tnRoot.ShowCheckBox = true;
                        tnRoot.Checked = true;
                        tnRoot.Checked = true;
                    }

                    System.Data.DataTable myChildDataTable = BLL_tab_ShopClient_NET_ROOT_Menu.GetList("ParentID=" + strID + " and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                    for (int j = 0; j < myChildDataTable.Rows.Count; j++)
                    {
                        string strChildMenuName = myChildDataTable.Rows[j]["MenuName"].ToString();
                        string strChildID = myChildDataTable.Rows[j]["ID"].ToString();

                        TreeNode tnRootChild = new TreeNode();
                        tnRootChild.Text = strChildMenuName;
                        tnRootChild.Value = strChildID;
                        tnRootChild.ToolTip = "单击进行编辑" + strChildID;
                        tnRootChild.NavigateUrl = "NetRootMenu.aspx?ParentID=" + strID + "&ThisID=" + strChildID;
                        tnRoot.ChildNodes.Add(tnRootChild);

                        if (strThisID == strChildID)
                        {
                            tnRootChild.ShowCheckBox = true;
                            tnRootChild.Checked = true;
                            tnRootChild.Checked = true;
                        }
                    }
                }
            }
        }


        protected void Button_EdiMenu_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            String strThisID = Request.QueryString["ThisID"];
            if (strThisID != null)
            {

                if (IsHttp(TextBox_MenuContent.Text.Trim()))
                {
                }
                else
                {
                    Eggsoft.Common.JsUtil.ShowMsg("输入的不是网址。");
                    return;
                }

                EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu Model_tab_ShopClient_NET_ROOT_Menu = BLL_tab_ShopClient_NET_ROOT_Menu.GetModel(Convert.ToInt32(strThisID));
                Model_tab_ShopClient_NET_ROOT_Menu.MenuName = TextBox_MenuName.Text.Trim();
                string strTextBox_MenuContent = TextBox_MenuContent.Text.Trim();
                if (strTextBox_MenuContent.ToLower().IndexOf("eggsoft.cn") != -1) strTextBox_MenuContent = strTextBox_MenuContent.ToLower();///本站的域名 才转化
                Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = strTextBox_MenuContent;
                Model_tab_ShopClient_NET_ROOT_Menu.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                BLL_tab_ShopClient_NET_ROOT_Menu.Update(Model_tab_ShopClient_NET_ROOT_Menu);

                String strParentID = Request.QueryString["ParentID"];
                if (strParentID == null) strParentID = "0";
                Eggsoft.Common.JsUtil.LocationNewHref("NetRootMenu.aspx?ParentID=" + strParentID + "&ThisID=" + strThisID + "");
            }
        }


        protected void Button_NewRoot_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            int myExistsCount = BLL_tab_ShopClient_NET_ROOT_Menu.ExistsCount("ParentID=0 and ShopClientID=" + strShopClientID);
            if (myExistsCount >= 3)
            {
                Eggsoft.Common.JsUtil.ShowMsg("根菜单已经3有个");
            }
            else
            {
                EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu Model_tab_ShopClient_NET_ROOT_Menu = new EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu();
                Model_tab_ShopClient_NET_ROOT_Menu.MenuName = TextBox_MenuName.Text.Trim();

                Model_tab_ShopClient_NET_ROOT_Menu.ShopClientID = Int32.Parse(strShopClientID);
                Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = TextBox_MenuContent.Text.Trim().ToLower();
                Model_tab_ShopClient_NET_ROOT_Menu.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);
                Eggsoft.Common.JsUtil.LocationNewHref("NetRootMenu.aspx");


            }

        }


        protected void Button_NewChildMenu_Click(object sender, EventArgs e)
        {
            String strParentID = Request.QueryString["ParentID"];
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            if (strParentID == null)
            {
                Eggsoft.Common.JsUtil.ShowMsg("没有选择父菜单！");
            }
            else
            {
                int myExistsCount = BLL_tab_ShopClient_NET_ROOT_Menu.ExistsCount("ParentID=" + strParentID + " and ShopClientID=" + strShopClientID);
                if (myExistsCount >= 8)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("子菜单已经8有个");
                }
                else
                {

                    if (IsHttp(TextBox_MenuContent.Text.Trim()))
                    {


                    }

                    else
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("输入的不是网址。");
                        return;
                    }




                    EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu Model_tab_ShopClient_NET_ROOT_Menu = new EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu();
                    Model_tab_ShopClient_NET_ROOT_Menu.MenuName = TextBox_MenuName.Text.Trim();
                    Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = TextBox_MenuContent.Text.Trim().ToLower();
                    Model_tab_ShopClient_NET_ROOT_Menu.ParentID = Convert.ToInt32(strParentID);
                    Model_tab_ShopClient_NET_ROOT_Menu.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                    Model_tab_ShopClient_NET_ROOT_Menu.ShopClientID = Convert.ToInt32(strShopClientID);


                    int intThis = BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);
                    Eggsoft.Common.JsUtil.LocationNewHref("NetRootMenu.aspx?ParentID=" + strParentID + "&ThisID=" + intThis);

                }
            }
        }

        protected void Button_DeleteMenu_Click(object sender, EventArgs e)
        {
            String strThisID = Request.QueryString["ThisID"];
            if (strThisID != null)
            {

                if (BLL_tab_ShopClient_NET_ROOT_Menu.Exists("ParentID=" + strThisID))
                {
                    Eggsoft.Common.JsUtil.ShowMsg("有子菜单，不能删除！");
                }
                else
                {
                    BLL_tab_ShopClient_NET_ROOT_Menu.Delete(Convert.ToInt32(strThisID));
                    Eggsoft.Common.JsUtil.LocationNewHref("NetRootMenu.aspx");
                }
            }
        }

        public bool IsHttp(string str_IsHttp)
        {
            bool boolhttp = System.Text.RegularExpressions.Regex.IsMatch(str_IsHttp, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?");
            bool boolIsSMS = str_IsHttp.ToLower().IndexOf("sms:") != -1;
            bool boolIsTel = str_IsHttp.ToLower().IndexOf("tel:") != -1;

            return boolhttp || boolIsSMS || boolIsTel;//验证IsHttp
        }










        protected void Button_ReadDefaultMenu0_Click(object sender, EventArgs e)
        {
            EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu Model_tab_ShopClient_NET_ROOT_Menu = new EggsoftWX.Model.tab_ShopClient_NET_ROOT_Menu();


            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("id=" + strShopClientID);
            string strYuMing = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower();


            Model_tab_ShopClient_NET_ROOT_Menu.ShopClientID = Int32.Parse(strShopClientID);

            Model_tab_ShopClient_NET_ROOT_Menu.ParentID = 0;
            Model_tab_ShopClient_NET_ROOT_Menu.MenuName = "逛街";
            Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = strYuMing + "/default.aspx";
            BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);

            Model_tab_ShopClient_NET_ROOT_Menu.ParentID = 0;
            Model_tab_ShopClient_NET_ROOT_Menu.MenuName = "管理代理";
            Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = strYuMing;
            int intAdd = BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);

            Model_tab_ShopClient_NET_ROOT_Menu.ParentID = intAdd;
            Model_tab_ShopClient_NET_ROOT_Menu.MenuName = "管理代理";
            Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = strYuMing + "/edityourshopini.aspx";
            BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);

            Model_tab_ShopClient_NET_ROOT_Menu.ParentID = intAdd;
            Model_tab_ShopClient_NET_ROOT_Menu.MenuName = "打电话";
            Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = "tel:" + Model_tab_ShopClient.ContactPhone;
            Model_tab_ShopClient_NET_ROOT_Menu.ParentID = intAdd;
            BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);

            Model_tab_ShopClient_NET_ROOT_Menu.MenuName = "发短信";
            Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = "sms:" + Model_tab_ShopClient.ContactPhone;
            Model_tab_ShopClient_NET_ROOT_Menu.ParentID = intAdd;
            BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);

            Model_tab_ShopClient_NET_ROOT_Menu.ParentID = 0;
            Model_tab_ShopClient_NET_ROOT_Menu.MenuName = "我";
            Model_tab_ShopClient_NET_ROOT_Menu.MenuLink = strYuMing + "/mywebuy.aspx";
            BLL_tab_ShopClient_NET_ROOT_Menu.Add(Model_tab_ShopClient_NET_ROOT_Menu);


            Eggsoft.Common.JsUtil.RefreshCurWindow();
        }
    }
}