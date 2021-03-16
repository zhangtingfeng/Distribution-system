using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._09System_Status
{
    public partial class GoodsVisit : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.View_WatchGoods bll = new EggsoftWX.BLL.View_WatchGoods();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_GoodsVisit")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                string strShopClient_ID = "ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                ViewState["RecordCount"] = bll.ExistsCount(strShopClient_ID).ToString();
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
            sql.addOrderField("View_WatchGoods.UpdateTime", "desc");//第一排序字段  
            sql.addOrderField("View_WatchGoods.id", "desc");//第二排序字段  
            sql.table = "View_WatchGoods";
            sql.outfields = "View_WatchGoods.ID,View_WatchGoods.Name,View_WatchGoods.Sort,View_WatchGoods.PromotePrice,View_WatchGoods.Price,View_WatchGoods.GoodID,View_WatchGoods.userID,View_WatchGoods.Parent_UserID,View_WatchGoods.GrandParentID,View_WatchGoods.GreatParentID,View_WatchGoods.UpdateTime,View_WatchGoods.Count_Visit";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = "ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            sql.where = strwhere;
            //Response.Write(sql.getSQL());

            //string strSql = "select top 20 * from tab_Goods_Class where UserID=12 order by tab_Goods_Class.sort asc,tab_Goods_Class.id asc";
            string strSql = sql.getSQL(bll.ExistsCount(strwhere));

            gvAnnounce.DataSource = bll.SelectList(strSql);

            //gvAnnounce.DataSource = bll.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,ClassName,Sort,UpdateTime", "UserID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[1].Text;
                e.Row.Cells[1].Text = Eggsoft_Public_CL.GoodP.GetGood_Num_ID_From_Good_ID(Convert.ToInt32(strID));

                string strUserID = e.Row.Cells[3].Text;
                string strParentID = e.Row.Cells[4].Text;
                string strGrandParentID = e.Row.Cells[5].Text;
                string strGreatParentID = e.Row.Cells[6].Text;

                e.Row.Cells[3].Text = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                e.Row.Cells[4].Text = Eggsoft_Public_CL.Pub.GetNickName(strParentID);

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

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("GoodClass_Manage.aspx?type=Add");
        //}
    }
}