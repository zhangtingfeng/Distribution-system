using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._34WuLiu
{
    public partial class Board34ZoneChange : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL._031_ZONE bllView_ = new EggsoftWX.BLL._031_ZONE();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("WuLIuAdvance_ZoneChange")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                EggsoftWX.BLL._031_Channel bllView1111 = new EggsoftWX.BLL._031_Channel();
                DropDownListChannel.DataSource = bllView1111.GetDataTable("1000", "ChannelName", " and isnull(IsDeleted,0)=0 and ShopClient_ID = " + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                DropDownListChannel.DataTextField = "ChannelName";
                DropDownListChannel.DataValueField = "ChannelName";
                DropDownListChannel.DataBind();

                System.Collections.ArrayList ar = new System.Collections.ArrayList();
                for (int i = 1; i <= 10; i++)
                {
                    ar.Add(i);
                }
                for (char i = 'A'; i <= 'Z'; i++)
                {
                    ar.Add(i);

                }

                this.DropDownListZone.DataSource = ar;
                this.DropDownListZone.DataBind();



                int intPageIndex = Request.QueryString["PageIndex"].toInt32();
                ViewState["PageIndex"] = intPageIndex > 0 ? intPageIndex : 1;
                CClickeSearch(sender, e);
            }
        }

        private void InitGoPage()
        {
            ddlGoPage.Items.Clear();
            for (int i = 1; i <= GetPageCount(); i++)
            {
                ddlGoPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlGoPage.SelectedValue = ViewState["PageIndex"].toString();
        }

        private void BindAnnounce()
        {
            string strWhere = "";
            if (DropDownListChannel.SelectedIndex > -1 && String.IsNullOrEmpty(DropDownListChannel.SelectedValue) == false)
            {
                strWhere += " and Channel='" + DropDownListChannel.SelectedValue + "'";
            }
            if (DropDownListZone.SelectedIndex > -1 && String.IsNullOrEmpty(DropDownListZone.SelectedValue) == false)
            {
                strWhere += " and Zone='" + DropDownListZone.SelectedValue + "'";
            }

            gvAnnounce.DataSource = bllView_.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].toString()), Int32.Parse(ViewState["PageSize"].ToString()), "[ID]      ,[Channel]      ,[CNCountry],[Zone]", " and isnull(IsDeleted,0)=0 and ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString()+ strWhere, "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //e.Row.Cells[2].Text = e.Row.Cells[2].Text == "1" ? "现金" : "其他";
                //e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToLower() == "auto" ? "自动" : "手动";

                e.Row.Cells[4].Text = "<a href=\"Board34ZoneChange_Manage.aspx?type=Modify&ID=" + e.Row.Cells[0].Text + "&PageIndex=" + ViewState["PageIndex"] + "\">修改</a>";
                e.Row.Cells[5].Text = "<a href=\"Board34ZoneChange_Manage.aspx?type=Delete&ID=" + e.Row.Cells[0].Text + "&PageIndex=" + ViewState["PageIndex"] + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
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
            if (Int32.Parse(ViewState["PageIndex"].toString()) > 1)
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].toString()) - 1;
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
            if (Int32.Parse(ViewState["PageIndex"].toString()) < GetPageCount())
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].toString()) + 1;
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
            lblMsg.Text = "当前页:" + ViewState["PageIndex"].toString() + "/" + GetPageCount().ToString() + " 每页:" + ViewState["PageSize"].ToString() + "条 共:" + ViewState["RecordCount"].ToString() + "条";
            if (GetPageCount() <= 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
                lbtnNext.Enabled = false;
                lbtnLast.Enabled = false;
            }
            else
            {
                if (Int32.Parse(ViewState["PageIndex"].toString()) <= 1)
                {
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    lbtnNext.Enabled = true;
                    lbtnLast.Enabled = true;
                }
                else
                {
                    if (Int32.Parse(ViewState["PageIndex"].toString()) >= GetPageCount())
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
            Response.Redirect("Board34ZoneChange_Manage.aspx?type=Add&PageIndex=" + ViewState["PageIndex"]);
        }

        protected void CClickeSearch(object sender, EventArgs e)
        {
            string strWhere = "";
            if (DropDownListChannel.SelectedIndex>-1 && String.IsNullOrEmpty(DropDownListChannel.SelectedValue) == false)
            {
                strWhere += " and Channel='" + DropDownListChannel.SelectedValue + "'";
            }
            if (DropDownListZone.SelectedIndex > -1 && String.IsNullOrEmpty(DropDownListZone.SelectedValue) == false)
            {
                strWhere += " and Zone='" + DropDownListZone.SelectedValue + "'";
            }
            ViewState["PageSize"] = 20;
            ViewState["RecordCount"] = bllView_.ExistsCount("ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " and isnull(IsDeleted,0)=0"+ strWhere);
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
    }
}