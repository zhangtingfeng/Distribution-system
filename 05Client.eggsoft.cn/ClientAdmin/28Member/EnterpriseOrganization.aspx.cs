using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._28Member
{
    public partial class EnterpriseOrganization1 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization BLL_tab_ShopClient_EnterpriseOrganization = new EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                IniReadRootMenu(0, null);
                IniReadMenuContent();///点击过的事件
            }
        }
        /// <summary>
        /// 点击组织机构事件
        /// </summary>
        protected void IniReadMenuContent()
        {
            String strThisIDURL = Request.QueryString["ThisID"];

            Int32 int32ThisID = 0;
            Int32.TryParse(strThisIDURL, out int32ThisID);
            if (int32ThisID == 0)
            {
                ButtonSaveLevel.Visible = false;
                Button_EditSaveOrganization.Visible = false;
                Button_DeleteOrganization.Visible = false;
            }
            else
            {
                EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Modeltab_ShopClient_EnterpriseOrganization = BLL_tab_ShopClient_EnterpriseOrganization.GetModel(Convert.ToInt32(strThisIDURL));
                TextBox_Pos.Text = (Modeltab_ShopClient_EnterpriseOrganization.Pos).toString();
                TextBox_OrganizationName.Text = Modeltab_ShopClient_EnterpriseOrganization.OrganizationName;
                //ButtonSaveLevel.Visible = false;
                //Button_NewChildOrganization.Visible = false;
                //Button_EditOrganization.Visible = false;
                //Button_DeleteOrganization.Visible = false;
            }

        }

        protected void IniReadRootMenu(Int32 intParentID, TreeNode tnRootMe)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (intParentID == 0 && tnRootMe == null)
            {
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel((strShopClientID).toInt32());

                TreeNode tnRoot = new TreeNode();
                tnRoot.Text = Model_tab_ShopClient.ShopClientName;
                tnRoot.Value = "0";
                tnRoot.ToolTip = "单击进行编辑";
                tnRoot.NavigateUrl = "EnterpriseOrganization.aspx?ParentID=" + "0" + "&ThisID=" + "0";
                TreeView_Organization.Nodes.Add(tnRoot);
                IniReadRootMenu(0, tnRoot);
            }
            else
            {
                String strThisURLID = Request.QueryString["ThisID"];
                bool myExsit_Child_Root_ID = BLL_tab_ShopClient_EnterpriseOrganization.Exists("ParentID=" + intParentID + " and ShopClientID=" + strShopClientID+ " and isnull(isDeleted,0)=0");
                if (myExsit_Child_Root_ID)
                {
                    System.Data.DataTable myDataTable = BLL_tab_ShopClient_EnterpriseOrganization.GetList("ParentID=" + intParentID + " and ShopClientID=" + strShopClientID + " and isnull(isDeleted,0)=0 order by pos asc,id asc").Tables[0];
                    for (int i = 0; i < myDataTable.Rows.Count; i++)
                    {
                        string strMenuName = myDataTable.Rows[i]["OrganizationName"].ToString();
                        string strMenuContent = myDataTable.Rows[i]["OrganizationContent"].ToString();
                        string strChildID = myDataTable.Rows[i]["ID"].ToString();
                        ///string strParentID = myDataTable.Rows[i]["ParentID"].ToString();

                        TreeNode tnRootChild = new TreeNode();
                        tnRootChild.Text = strMenuName;
                        tnRootChild.Value = strChildID;
                        tnRootChild.ToolTip = "单击进行编辑" + strChildID;
                        tnRootChild.NavigateUrl = "EnterpriseOrganization.aspx?ParentID=" + intParentID + "&ThisID=" + strChildID;
                        tnRootMe.ChildNodes.Add(tnRootChild);
                        IniReadRootMenu(Int32.Parse(strChildID), tnRootChild);


                        if (strThisURLID == strChildID)
                        {
                            tnRootChild.ShowCheckBox = true;
                            tnRootChild.Checked = true;
                        }

                    }
                }
                else
                {
                }

            }
        }
      
        

        protected void Button_NewChildOrganization_Click(object sender, EventArgs e)
        {
            String strThisID = Request.QueryString["ThisID"];
            Int32 Int32ThisID = 0;
            Int32.TryParse(strThisID, out Int32ThisID);
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();



            EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Model_tab_ShopClient_EnterpriseOrganizationPaentChild = new EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization();
            Model_tab_ShopClient_EnterpriseOrganizationPaentChild.ParentID = Int32ThisID;
            Model_tab_ShopClient_EnterpriseOrganizationPaentChild.ShopClientID = Convert.ToInt32(strShopClientID);
            Model_tab_ShopClient_EnterpriseOrganizationPaentChild.OrganizationName = TextBox_OrganizationName.Text.Trim();
            Model_tab_ShopClient_EnterpriseOrganizationPaentChild.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
            int intCurID = BLL_tab_ShopClient_EnterpriseOrganization.Add(Model_tab_ShopClient_EnterpriseOrganizationPaentChild);
            Eggsoft.Common.JsUtil.LocationNewHref("EnterpriseOrganization.aspx?ParentID=" + Int32ThisID + "&ThisID=" + intCurID + "");

        }

        protected void Button_EditSaveOrganization_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            String strThisID = Request.QueryString["ThisID"];
            Int32 Int32ThisID = 0;
            Int32.TryParse(strThisID, out Int32ThisID);
            if (Int32ThisID > 0)
            {
                EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Model_tab_ShopClient_EnterpriseOrganizationPaent = BLL_tab_ShopClient_EnterpriseOrganization.GetModel(Int32ThisID);
                Model_tab_ShopClient_EnterpriseOrganizationPaent.OrganizationName = TextBox_OrganizationName.Text.Trim();
                Model_tab_ShopClient_EnterpriseOrganizationPaent.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                Model_tab_ShopClient_EnterpriseOrganizationPaent.UpdateTime = DateTime.Now;
                BLL_tab_ShopClient_EnterpriseOrganization.Update(Model_tab_ShopClient_EnterpriseOrganizationPaent);
                Eggsoft.Common.JsUtil.LocationNewHref("EnterpriseOrganization.aspx?ParentID=" + Model_tab_ShopClient_EnterpriseOrganizationPaent.ParentID + "&ThisID=" + Int32ThisID + "");
            }
        }

        protected void Button_DeleteOrganization_Click(object sender, EventArgs e)
        {
            String strThisID = Request.QueryString["ThisID"];
            if (strThisID != null)
            {
                if (BLL_tab_ShopClient_EnterpriseOrganization.Exists("ParentID=" + strThisID+ " isnull(isDeleted)=0"))
                {
                    Eggsoft.Common.JsUtil.ShowMsg("有子机构，不能删除！");
                }
                else
                {
                    BLL_tab_ShopClient_EnterpriseOrganization.Delete(Convert.ToInt32(strThisID));
                    Eggsoft.Common.JsUtil.LocationNewHref("EnterpriseOrganization.aspx");
                }
            }
        }

        protected void Button_SaveLevelOrganization_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            String strThisID = Request.QueryString["ThisID"];
            Int32 Int32ThisID = 0;
            Int32.TryParse(strThisID, out Int32ThisID);
            if (Int32ThisID > 0)
            {
                EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Model_tab_ShopClient_EnterpriseOrganizationPaent = BLL_tab_ShopClient_EnterpriseOrganization.GetModel(Int32ThisID);
                EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Model_tab_ShopClient_EnterpriseOrganizationPaentSameLevel = new EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization();
                Int32? intParentID = Model_tab_ShopClient_EnterpriseOrganizationPaent.ParentID;
                Model_tab_ShopClient_EnterpriseOrganizationPaentSameLevel.ParentID = intParentID;
                Model_tab_ShopClient_EnterpriseOrganizationPaentSameLevel.ShopClientID = Convert.ToInt32(strShopClientID);
                Model_tab_ShopClient_EnterpriseOrganizationPaentSameLevel.OrganizationName = TextBox_OrganizationName.Text.Trim();
                Model_tab_ShopClient_EnterpriseOrganizationPaentSameLevel.Pos = Convert.ToInt32(TextBox_Pos.Text.Trim());
                int intSameLevel=BLL_tab_ShopClient_EnterpriseOrganization.Add(Model_tab_ShopClient_EnterpriseOrganizationPaentSameLevel);
                Eggsoft.Common.JsUtil.LocationNewHref("EnterpriseOrganization.aspx?ParentID=" + intParentID + "&ThisID=" + intSameLevel + "");
            }
        }
    }
}