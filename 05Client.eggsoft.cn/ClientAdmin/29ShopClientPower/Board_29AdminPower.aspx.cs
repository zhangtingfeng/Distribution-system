using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05ClientEggsoftCn.ClientAdmin._28Member
{
    public partial class Board_29AdminPower : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strManageURL = "Manage_29AdminPower.aspx";

        EggsoftWX.BLL.tab_ShopClient_AdminUser tab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_ShopClient_AdminUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board_29AdminPower")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = tab_ShopClient_AdminUser.ExistsCount(" isnull(isDeleted,0)=0 and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                BindAnnounce();
                ShowState();
                InitGoPage();
            }
        }

        private void InitGoPage()
        {
            ddlGoPage.Items.Clear();
            for (int i = 1; i <= GetPageCount(); i++)
            {
                ddlGoPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlGoPage.SelectedValue = ViewState["PageIndex"].ToString();
        }

        private void BindAnnounce()
        {
            ///,[InputMoney]      ,[BonusMoney]      ,[BonusGouWuQuan]      ,[BonusDesc]

            gvAnnounce.DataSource = tab_ShopClient_AdminUser.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,UserRealName,ShopClientAdmin,ShopClient_Role_PowerID,EnterpriseOrganizationID", " and isnull(IsDeleted,0)=0 and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strShopClient_Role_PowerID = e.Row.Cells[3].Text;
                EggsoftWX.BLL.tab_ShopClient_Role_Power bll = new EggsoftWX.BLL.tab_ShopClient_Role_Power();
                EggsoftWX.Model.tab_ShopClient_Role_Power Model = bll.GetModel((strShopClient_Role_PowerID).toInt32());
                if (Model != null)
                {
                    e.Row.Cells[3].Text = Model.RoleName;
                }

                string strEnterpriseOrganizationID = e.Row.Cells[4].Text;
                Int32 Int32EnterpriseOrganizationID = strEnterpriseOrganizationID.toInt32();
                EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization blltab_ShopClient_EnterpriseOrganization = new EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization();
                if (Int32EnterpriseOrganizationID < 0)
                {
                    EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Modeltab_ShopClient_EnterpriseOrganization = blltab_ShopClient_EnterpriseOrganization.GetModel(-Int32EnterpriseOrganizationID);
                    if (Modeltab_ShopClient_EnterpriseOrganization != null)
                    {
                        e.Row.Cells[4].Text = Modeltab_ShopClient_EnterpriseOrganization.OrganizationName + "普通职员";
                    }
                }
                else if (Int32EnterpriseOrganizationID == 0)
                {
                    e.Row.Cells[4].Text = "企业管理团队";
                }
                else
                {
                    EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Modeltab_ShopClient_EnterpriseOrganization = blltab_ShopClient_EnterpriseOrganization.GetModel(Int32EnterpriseOrganizationID);
                    //string strShowManager = blltab_ShopClient_EnterpriseOrganization.Exists("ParentID=" + strEnterpriseOrganizationID + " and isnull(isDeleted,0)=0") ? "管理团队" : "";
                    if (Modeltab_ShopClient_EnterpriseOrganization != null)
                    { e.Row.Cells[4].Text = Modeltab_ShopClient_EnterpriseOrganization.OrganizationName + "管理团队"; }
                }
                //e.Row.Cells[2].Text = e.Row.Cells[2].Text == "1" ? "现金" : "其他";
                //e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToLower() == "auto" ? "自动" : "手动";

                e.Row.Cells[5].Text = "<a href=\"" + strManageURL + "?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[6].Text = "<a href=\"" + strManageURL + "?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
            }
        }

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            BindAnnounce();
            ShowState();
            InitGoPage();
        }

        protected void lbtnPrev_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(ViewState["PageIndex"].ToString()) > 1)
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].ToString()) - 1;
            }
            else
            {
                ViewState["PageIndex"] = GetPageCount();
            }
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(ViewState["PageIndex"].ToString()) < GetPageCount())
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].ToString()) + 1;
            }
            else
            {
                ViewState["PageIndex"] = 1;
            }
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = GetPageCount();
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void ddlGoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = Int32.Parse(ddlGoPage.SelectedValue);
            BindAnnounce();
            ShowState();
        }

        private void ShowState()
        {
            lblMsg.Text = "当前页:" + ViewState["PageIndex"].ToString() + "/" + GetPageCount().ToString() + " 每页:" + ViewState["PageSize"].ToString() + "条 共:" + ViewState["RecordCount"].ToString() + "条";
            if (GetPageCount() <= 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
                lbtnNext.Enabled = false;
                lbtnLast.Enabled = false;
            }
            else
            {
                if (Int32.Parse(ViewState["PageIndex"].ToString()) <= 1)
                {
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    lbtnNext.Enabled = true;
                    lbtnLast.Enabled = true;
                }
                else
                {
                    if (Int32.Parse(ViewState["PageIndex"].ToString()) >= GetPageCount())
                    {
                        lbtnFirst.Enabled = true;
                        lbtnPrev.Enabled = true;
                        lbtnNext.Enabled = false;
                        lbtnLast.Enabled = false;
                    }
                    else
                    {
                        lbtnFirst.Enabled = true;
                        lbtnPrev.Enabled = true;
                        lbtnNext.Enabled = true;
                        lbtnLast.Enabled = true;
                    }
                }
            }
        }

        private int GetPageCount()
        {
            int pageCount = Int32.Parse(ViewState["RecordCount"].ToString()) % Int32.Parse(ViewState["PageSize"].ToString()) == 0 ? (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString())) : (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString()) + 1);
            return pageCount;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect(strManageURL + "?type=Add");
        }

        protected void btnAdd_Click_Role(object sender, EventArgs e)
        {
            Response.Redirect("Board_29RolePower.aspx");
        }

        protected void btnAdd_Click_Organization(object sender, EventArgs e)
        {
            Response.Redirect("../28Member/EnterpriseOrganization.aspx");
        }
    }
}