using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._13Order_Cancel
{
    public partial class IS_Finance_check_DrawMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_ASK bll_tab_ShopClient_ASK = new EggsoftWX.BLL.tab_ShopClient_ASK();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitGoPageReadData();
            }
        }


        private void InitGoPageReadData()
        {

            ViewState["PageIndex"] = 1;
            ViewState["PageSize"] = 20;

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString().ToLower();

            if (CheckBox_IIS_Admin.Checked)
            {
                ViewState["RecordCount"] = bll_tab_ShopClient_ASK.ExistsCount("IFPayByShenMa_Finace=1 and ShopClientID=" + strShopClientID);
            }
            else
            {
                ViewState["RecordCount"] = bll_tab_ShopClient_ASK.ExistsCount("IFPayByShenMa_Finace=0 and ShopClientID=" + strShopClientID);
            }

            BindAnnounce();
            ShowState();
            InitGoPage();

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
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString().ToLower();

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("tab_ShopClient_ASK.UpdateTime", "desc");//第一排序字段  
            sql.addOrderField("tab_ShopClient_ASK.id", "desc");//第二排序字段  
            sql.table = "tab_ShopClient_ASK";
            sql.outfields = "ShopClientID,ID,ShopClientAsk,UpdateTime,type,OrderID,IFPayByShenMa_Finace,IFPayByShenMa_Manager";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());

            String strWhere = "";
            if (CheckBox_IIS_Admin.Checked)
            {
                strWhere = "IFPayByShenMa_Finace=1 and ShopClientID=" + strShopClientID;

            }
            else
            {
                strWhere = "IFPayByShenMa_Finace=0 and ShopClientID=" + strShopClientID;
            }


            sql.where = strWhere;

            string strSql = sql.getSQL(bll_tab_ShopClient_ASK.ExistsCount(strWhere));
            gvAnnounce.DataSource = bll_tab_ShopClient_ASK.SelectList(strSql);

            gvAnnounce.DataBind();
        }



        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strOrderID = e.Row.Cells[0].Text;

                //string strDistributionMoneyDisValue = e.Row.Cells[11].Text;
                //if (String.IsNullOrEmpty(e.Row.Cells[11].Text) == true) strDistributionMoneyDisValue = "1";
                e.Row.Cells[0].Text = Eggsoft_Public_CL.GoodP.GetOrderNum_From_OrderID(Convert.ToInt32(strOrderID));

                e.Row.Cells[1].Text = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(Int32.Parse(e.Row.Cells[1].Text));
                e.Row.Cells[2].Text = Eggsoft_Public_CL.Pub.getPubMoney(Eggsoft_Public_CL.GoodP.GetThisOrderShopGet(Convert.ToInt32(strOrderID)));


                if (e.Row.Cells[8].Text == "True")
                {
                    // e.Row.Cells[6].Text = "<span id=\"IS_Admin_check_AsyncStatusShow" + strOrderID + "\">已审</span>";
                    e.Row.Cells[8].Text = "<span id=\"IS_Admin_check_AsyncStatus" + strOrderID + "\"><a href=\"#\" style=\"color:blue\" onclick=\"IS_Admin_check_Async_ShowFuction(" + strOrderID + ",0);\">未结单</a></span>";
                }
                else
                {
                    //e.Row.Cells[8].Text = "<span id=\"IS_Admin_check_AsyncStatusShow" + strOrderID + "\">未申</span>";
                    e.Row.Cells[8].Text = "<span id=\"IS_Admin_check_AsyncStatus" + strOrderID + "\"><a href=\"#\" style=\"color:blue\" onclick=\"IS_Admin_check_Async_ShowFuction(" + strOrderID + ",1);\">已结单</a></span>";
                }
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

        protected void CheckBox_IIS_Admin_CheckedChanged(object sender, EventArgs e)
        {
            InitGoPageReadData();
        }
    }
}