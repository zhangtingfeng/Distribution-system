using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05ClientEggsoftCn.ClientAdmin._28Member
{
    public partial class Board_28MemberBonus : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strManageURL = "Manage_28MemberBonus.aspx";

        EggsoftWX.BLL.tab_ShopClient_MemberCardBonus blltab_MemberCardBonus = new EggsoftWX.BLL.tab_ShopClient_MemberCardBonus();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = blltab_MemberCardBonus.ExistsCount("ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString()+" and isnull(isdeleted,0)=0");
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

            gvAnnounce.DataSource = blltab_MemberCardBonus.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,[InputMoney]      ,[BonusMoney]      ,[BonusGouWuQuan]      ,[BonusDesc],UpdateBy,CreateBy", " and isnull(isdeleted,0)=0 and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "InputMoney,ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //e.Row.Cells[2].Text = e.Row.Cells[2].Text == "1" ? "现金" : "其他";
                //e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToLower() == "auto" ? "自动" : "手动";
                //e.Row.Cells[4].Text = "<a href=\"" + strManageURL + "?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                //e.Row.Cells[5].Text = "<a href=\"" + strManageURL + "?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

                e.Row.Cells[6].Text = "<a href=\"" + strManageURL + "?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[7].Text = "<a href=\"" + strManageURL + "?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
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

    }
}