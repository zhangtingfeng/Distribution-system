using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._15Advance
{
    public partial class Suggestion_List : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_Suggestion_By_Qiu bll = new EggsoftWX.BLL.tab_Suggestion_By_Qiu();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = bll.ExistsCount("ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                BindAnnounce();
                ShowState();
                InitGoPage();


                //if (Int32.Parse(ViewState["RecordCount"].ToString())<2)    btnAdd.Visible=true;

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
            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("tab_Suggestion_By_Qiu.UpdateTime", "desc");//第一排序字段  
            //sql.addOrderField("tab_Goods_Class.id", "asc");//第二排序字段  
            sql.table = "tab_Suggestion_By_Qiu";
            sql.outfields = "tab_Suggestion_By_Qiu.ID,tab_Suggestion_By_Qiu.Title,tab_Suggestion_By_Qiu.UpdateTime";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = "ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            sql.where = strwhere;
            //        string strwhere = "1=1 ";
            string strSql = sql.getSQL(bll.ExistsCount(strwhere));
            gvAnnounce.DataSource = bll.SelectList(strSql);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[0].Text;
                e.Row.Cells[3].Text = "<a href=\"Suggestion.aspx?type=Modify&ID=" + strID + "\">修改</a>";
                e.Row.Cells[4].Text = "<a href=\"Suggestion.aspx?type=Delete&ID=" + strID + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
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
            Response.Redirect("Suggestion.aspx");
        }
    }
}