using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class WeiXinMenu : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_System_Menu_WeiXin BLL_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.BLL.tab_ShopClient_System_Menu_WeiXin();
        EggsoftWX.BLL.tab_ShopClient_NET_ROOT_Menu BLL_tab_ShopClient_NET_ROOT_Menu = new EggsoftWX.BLL.tab_ShopClient_NET_ROOT_Menu();

        protected void Page_Load(object sender, EventArgs e)
        {

             #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_WeiXinMenu")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            

            if (!IsPostBack)
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                Label_Development.Text = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/WxURL/ModeD-" + strShopClientID + ".aspx";


                IniReadRootMenu();
                IniReadMenuContent();
            }
        }

        protected void IniReadMenuContent()
        {
            String strThisIDURL = Request.QueryString["ThisID"];
            if (strThisIDURL != null)
            {
                EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin Model_tab_ShopClient_System_Menu_WeiXin = BLL_tab_ShopClient_System_Menu_WeiXin.GetModel(Convert.ToInt32(strThisIDURL));
                TextBox_MenuName.Text = Model_tab_ShopClient_System_Menu_WeiXin.MenuName;
                RadioButtonList_View_Click.SelectedValue = Model_tab_ShopClient_System_Menu_WeiXin.MenuType.ToString();
                TextBox_MenuContent.Text = Model_tab_ShopClient_System_Menu_WeiXin.MenuContent;
                TextBox_Pos.Text = Model_tab_ShopClient_System_Menu_WeiXin.Pos.ToString();
                //            Model_tab_ShopClient_System_Menu_WeiXin.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                RadioButtonList_View_Click_SelectedIndexChanged(null, null);
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

            bool myINC_User_ID = BLL_tab_ShopClient_System_Menu_WeiXin.Exists("ParentID=0 and ShopClientID=" + strShopClientID);
            if (myINC_User_ID)
            {
                System.Data.DataTable myDataTable = BLL_tab_ShopClient_System_Menu_WeiXin.GetList("ParentID=0 and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strMenuName = myDataTable.Rows[i]["MenuName"].ToString();
                    string strMMenuType = myDataTable.Rows[i]["MenuType"].ToString();
                    string strMenuContent = myDataTable.Rows[i]["MenuContent"].ToString();
                    string strID = myDataTable.Rows[i]["ID"].ToString();

                    TreeNode tnRoot = new TreeNode();
                    tnRoot.Text = strMenuName;
                    tnRoot.Value = strID;
                    tnRoot.ToolTip = "单击进行编辑" + strID;
                    tnRoot.NavigateUrl = "WeiXinMenu.aspx?ParentID=" + strID + "&ThisID=" + strID;
                    TreeView_Menu.Nodes.Add(tnRoot);


                    if (strThisID == strID)
                    {
                        tnRoot.ShowCheckBox = true;
                        tnRoot.Checked = true;
                        tnRoot.Checked = true;
                    }

                    System.Data.DataTable myChildDataTable = BLL_tab_ShopClient_System_Menu_WeiXin.GetList("ParentID=" + strID + " and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                    for (int j = 0; j < myChildDataTable.Rows.Count; j++)
                    {
                        string strChildMenuName = myChildDataTable.Rows[j]["MenuName"].ToString();
                        string strChildMMenuType = myChildDataTable.Rows[j]["MenuType"].ToString();
                        string strChildMenuContent = myChildDataTable.Rows[j]["MenuContent"].ToString();
                        string strChildID = myChildDataTable.Rows[j]["ID"].ToString();

                        TreeNode tnRootChild = new TreeNode();
                        tnRootChild.Text = strChildMenuName;
                        tnRootChild.Value = strChildID;
                        tnRootChild.ToolTip = "单击进行编辑" + strChildID;
                        tnRootChild.NavigateUrl = "WeiXinMenu.aspx?ParentID=" + strID + "&ThisID=" + strChildID;
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
            else
            {////复制tab_ShopClient_NET_ROOT_Menu的数据




            }
        }


        protected void Button_EdiMenu_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            String strThisID = Request.QueryString["ThisID"];
            if (strThisID != null)
            {
                string str_SelectedValue_Type = RadioButtonList_View_Click.SelectedValue;
                int intResource = Convert.ToInt32(str_SelectedValue_Type);
                if ((intResource == 1) || (intResource == 2) || (intResource == 3))
                {
                    string ResourceID = TextBox_MenuContent.Text.Trim();
                    EggsoftWX.BLL.tab_ShopClient_System_XML_Resource bll_tab_ShopClient_System_XML_Resource = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();
                    if (bll_tab_ShopClient_System_XML_Resource.Exists(Convert.ToInt32(ResourceID)))
                    {
                        string strType = bll_tab_ShopClient_System_XML_Resource.GetList("type", "ID=" + ResourceID + " and ShopClientID=" + strShopClientID).Tables[0].Rows[0][0].ToString();
                        if (str_SelectedValue_Type != strType)
                        {
                            Eggsoft.Common.JsUtil.ShowMsg("目前的素材类型选择错误，系统已自动为你纠正。");
                            RadioButtonList_View_Click.SelectedValue = strType;
                            str_SelectedValue_Type = strType;
                        }
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("素材的ID号不存在，请查询。");
                        return;
                    }
                }
                else if (intResource == 4)
                {
                    if (IsHttp(TextBox_MenuContent.Text.Trim()))
                    {


                    }

                    else
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("输入的不是网址。");
                        return;
                    }

                }


                EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin Model_tab_ShopClient_System_Menu_WeiXin = BLL_tab_ShopClient_System_Menu_WeiXin.GetModel(Convert.ToInt32(strThisID));
                Model_tab_ShopClient_System_Menu_WeiXin.MenuName = TextBox_MenuName.Text.Trim();
                Model_tab_ShopClient_System_Menu_WeiXin.MenuType = Convert.ToInt32(str_SelectedValue_Type);
                Model_tab_ShopClient_System_Menu_WeiXin.MenuContent = TextBox_MenuContent.Text.Trim();
                Model_tab_ShopClient_System_Menu_WeiXin.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                BLL_tab_ShopClient_System_Menu_WeiXin.Update(Model_tab_ShopClient_System_Menu_WeiXin);

                String strParentID = Request.QueryString["ParentID"];
                if (strParentID == null) strParentID = "0";
                Eggsoft.Common.JsUtil.LocationNewHref("WeiXinMenu.aspx?ParentID=" + strParentID + "&ThisID=" + strThisID + "");
            }
        }


        protected void Button_NewRoot_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            int myExistsCount = BLL_tab_ShopClient_System_Menu_WeiXin.ExistsCount("ParentID=0 and ShopClientID=" + strShopClientID);
            if (myExistsCount >= 3)
            {
                Eggsoft.Common.JsUtil.ShowMsg("根菜单已经3有个");
            }
            else
            {
                EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin Model_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin();
                Model_tab_ShopClient_System_Menu_WeiXin.MenuName = TextBox_MenuName.Text.Trim();
                //Model_tab_ShopClient_System_Menu_WeiXin.MenuType = Convert.ToInt32(RadioButtonList_View_Click.SelectedValue);
                //if (Model_tab_ShopClient_System_Menu_WeiXin.MenuType == 4)
                //{
                //    if (IsHttp(TextBox_MenuContent.Text.Trim()))
                //    {
                Model_tab_ShopClient_System_Menu_WeiXin.ShopClientID = Int32.Parse(strShopClientID);
                Model_tab_ShopClient_System_Menu_WeiXin.MenuContent = TextBox_MenuContent.Text.Trim();
                Model_tab_ShopClient_System_Menu_WeiXin.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                BLL_tab_ShopClient_System_Menu_WeiXin.Add(Model_tab_ShopClient_System_Menu_WeiXin);
                Eggsoft.Common.JsUtil.LocationNewHref("WeiXinMenu.aspx");
                //}
                //else
                //{
                //    Eggsoft.Common.JsUtil.ShowMsg("输入的不是网址。");
                //    return;                
                //}
                //}

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
                int myExistsCount = BLL_tab_ShopClient_System_Menu_WeiXin.ExistsCount("ParentID=" + strParentID + " and ShopClientID=" + strShopClientID);
                if (myExistsCount >= 5)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("子菜单已经5有个");
                }
                else
                {
                    string str_SelectedValue_Type = RadioButtonList_View_Click.SelectedValue;
                    int intResource = Convert.ToInt32(str_SelectedValue_Type);
                    if ((intResource == 1) || (intResource == 2) || (intResource == 3))
                    {
                        string ResourceID = TextBox_MenuContent.Text.Trim();
                        int intResourceID = 0;
                        int.TryParse(ResourceID, out intResourceID);
                      
                        EggsoftWX.BLL.tab_ShopClient_System_XML_Resource bll_tab_ShopClient_System_XML_Resource = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();
                        if (bll_tab_ShopClient_System_XML_Resource.Exists(intResourceID))
                        {
                            string strType = bll_tab_ShopClient_System_XML_Resource.GetList("type", "ID=" + intResourceID + " and ShopClientID=" + strShopClientID).Tables[0].Rows[0][0].ToString();
                            if (str_SelectedValue_Type != strType)
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("目前的素材类型选择错误，系统已自动为你纠正。");
                                RadioButtonList_View_Click.SelectedValue = strType;
                                str_SelectedValue_Type = strType;
                            }
                        }
                        else
                        {
                            Eggsoft.Common.JsUtil.ShowMsg("素材的ID号不存在，请查询。");
                            return;
                        }
                    }
                    else if (intResource == 4)
                    {
                        if (IsHttp(TextBox_MenuContent.Text.Trim()))
                        {


                        }

                        else
                        {
                            Eggsoft.Common.JsUtil.ShowMsg("输入的不是网址。");
                            return;
                        }

                    }


                    EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin Model_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin();
                    Model_tab_ShopClient_System_Menu_WeiXin.MenuName = TextBox_MenuName.Text.Trim();
                    Model_tab_ShopClient_System_Menu_WeiXin.MenuType = Convert.ToInt32(str_SelectedValue_Type);
                    Model_tab_ShopClient_System_Menu_WeiXin.MenuContent = TextBox_MenuContent.Text.Trim();
                    Model_tab_ShopClient_System_Menu_WeiXin.ParentID = Convert.ToInt32(strParentID);
                    Model_tab_ShopClient_System_Menu_WeiXin.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                    Model_tab_ShopClient_System_Menu_WeiXin.ShopClientID = Convert.ToInt32(strShopClientID);


                    int intThis = BLL_tab_ShopClient_System_Menu_WeiXin.Add(Model_tab_ShopClient_System_Menu_WeiXin);
                    Eggsoft.Common.JsUtil.LocationNewHref("WeiXinMenu.aspx?ParentID=" + strParentID + "&ThisID=" + intThis);

                }
            }
        }

        protected void Button_DeleteMenu_Click(object sender, EventArgs e)
        {
            String strThisID = Request.QueryString["ThisID"];
            if (strThisID != null)
            {

                if (BLL_tab_ShopClient_System_Menu_WeiXin.Exists("ParentID=" + strThisID))
                {
                    Eggsoft.Common.JsUtil.ShowMsg("有子菜单，不能删除！");
                }
                else
                {
                    BLL_tab_ShopClient_System_Menu_WeiXin.Delete(Convert.ToInt32(strThisID));
                    Eggsoft.Common.JsUtil.LocationNewHref("WeiXinMenu.aspx");
                }
            }
        }

        public bool IsHttp(string str_IsHttp)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_IsHttp, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?");//验证电话号码
        }


        protected void RadioButtonList_View_Click_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (RadioButtonList_View_Click.SelectedValue == "5")
            //{
            //    TextBox_MenuContent.TextMode = TextBoxMode.MultiLine;
            //    TextBox_MenuContent.Height=500;
            //    RequiredFieldValidator_http_Resource.Enabled = false;
            //    RegularExpressionValidator_Http.Enabled = false;
            //    RegularExpressionValidator_TextBox_MenuContent.Enabled = false;
            //    RequiredFieldValidator_TextBox_MenuContent.Enabled = false;
            //}
            //else
            //{
            //    TextBox_MenuContent.TextMode = TextBoxMode.SingleLine;
            //    TextBox_MenuContent.Height = 25;
            //    RequiredFieldValidator_http_Resource.Enabled = true;
            //    RegularExpressionValidator_Http.Enabled = true;
            //    RegularExpressionValidator_TextBox_MenuContent.Enabled = true;
            //    RequiredFieldValidator_TextBox_MenuContent.Enabled = true;


            //    if (RadioButtonList_View_Click.SelectedValue == "4")
            //    {
            //        RegularExpressionValidator_Http.Enabled = true;
            //        RegularExpressionValidator_TextBox_MenuContent.Enabled = false;
            //        RequiredFieldValidator_TextBox_MenuContent.Enabled = false;
            //    }
            //    else
            //    {
            //        RegularExpressionValidator_Http.Enabled = false;
            //        RegularExpressionValidator_TextBox_MenuContent.Enabled = true;
            //        RequiredFieldValidator_TextBox_MenuContent.Enabled = true;
            //    }

            //}

        }








        protected void ButtonReReadRootMenu_Click(object sender, EventArgs e)
        {
            //String strParentID = Request.QueryString["ParentID"];
            String strThisID = Request.QueryString["ThisID"];

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            bool myINC_User_ID = BLL_tab_ShopClient_NET_ROOT_Menu.Exists("ParentID=0 and ShopClientID=" + strShopClientID);///网站根菜单是否存在数据
            if (myINC_User_ID)
            {
                BLL_tab_ShopClient_System_Menu_WeiXin.Delete("ShopClientID=" + strShopClientID);

                EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin Model_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin();


                System.Data.DataTable myDataTable = BLL_tab_ShopClient_NET_ROOT_Menu.GetList("ParentID=0 and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strID = myDataTable.Rows[i]["ID"].ToString();
                    string strMenuName = myDataTable.Rows[i]["MenuName"].ToString();
                    string strMenuLink = myDataTable.Rows[i]["MenuLink"].ToString();
                    string strPos = myDataTable.Rows[i]["Pos"].ToString();

                    Model_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin();
                    Model_tab_ShopClient_System_Menu_WeiXin.MenuName = strMenuName;
                    Model_tab_ShopClient_System_Menu_WeiXin.MenuContent = strMenuLink;
                    Model_tab_ShopClient_System_Menu_WeiXin.MenuType = 4;
                    Model_tab_ShopClient_System_Menu_WeiXin.Pos = Int32.Parse(strPos);
                    Model_tab_ShopClient_System_Menu_WeiXin.ParentID = 0;
                    Model_tab_ShopClient_System_Menu_WeiXin.ShopClientID = Int32.Parse(strShopClientID);
                    int intaddParentID = BLL_tab_ShopClient_System_Menu_WeiXin.Add(Model_tab_ShopClient_System_Menu_WeiXin);

                    System.Data.DataTable myDataTableChild = BLL_tab_ShopClient_NET_ROOT_Menu.GetList("ParentID=" + strID + " and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                    for (int j = 0; j < myDataTableChild.Rows.Count; j++)
                    {
                        string strChildMenuName = myDataTableChild.Rows[j]["MenuName"].ToString();
                        string strChildMenuLink = myDataTableChild.Rows[j]["MenuLink"].ToString();
                        string strChildPos = myDataTableChild.Rows[j]["Pos"].ToString();

                        Model_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.Model.tab_ShopClient_System_Menu_WeiXin();
                        Model_tab_ShopClient_System_Menu_WeiXin.MenuName = strChildMenuName;
                        Model_tab_ShopClient_System_Menu_WeiXin.MenuContent = strChildMenuLink;
                        Model_tab_ShopClient_System_Menu_WeiXin.MenuType = 4;
                        Model_tab_ShopClient_System_Menu_WeiXin.Pos = Int32.Parse(strChildPos);
                        Model_tab_ShopClient_System_Menu_WeiXin.ParentID = intaddParentID;
                        Model_tab_ShopClient_System_Menu_WeiXin.ShopClientID = Int32.Parse(strShopClientID);
                        BLL_tab_ShopClient_System_Menu_WeiXin.Add(Model_tab_ShopClient_System_Menu_WeiXin);
                    }
                }
                Eggsoft.Common.JsUtil.ShowMsg("读取成功", "/ClientAdmin/05EngineerMode/WeiXinMenu.aspx");
            }
            else
            {
                Eggsoft.Common.JsUtil.ShowMsg("网站根菜单也没有数据", -1);
            }
        }
        protected void ButtonReReadRootMenu_Click1(object sender, EventArgs e)
        {

        }
    }
}