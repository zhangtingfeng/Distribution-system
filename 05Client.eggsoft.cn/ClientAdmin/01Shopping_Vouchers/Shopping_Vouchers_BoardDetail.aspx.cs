using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._01Shopping_Vouchers
{
    public partial class Shopping_Vouchers_BoardDetail : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail bll_tab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strScheme_ID = Request.QueryString["Scheme_ID"];
                string strwebuy8_ClientAdmin_Users = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 40;
                ViewState["RecordCount"] = bll_tab_ShopClient_Shopping_VouchersScheme_Detail.ExistsCount("Scheme_ID=" + strScheme_ID + " and ShopClientID=" + strwebuy8_ClientAdmin_Users);
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
            string strScheme_ID = Request.QueryString["Scheme_ID"];
            string strwebuy8_ClientAdmin_Users = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            gvAnnounce.DataSource = bll_tab_ShopClient_Shopping_VouchersScheme_Detail.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), @"[ID]
      ,[Scheme_ID]
      ,[VouchersNum]
      ,[GuWuCheIDOrOrderDetailID]
      ,[ShopClientID]      
      ,[UserID]
      ,[Money]
      ,[MoneyUsed]
      ,[CreatTime]
      ,[UpdateTime]
      ,[ValidateStartTime]
      ,[ValidateEndTime]", " and Scheme_ID=" + strScheme_ID + " and ShopClientID=" + strwebuy8_ClientAdmin_Users, "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                if (e.Row.Cells[1].Text.toInt32() > 0)////GuWuCheIDOrOrderDetailID  归属商品
                {
                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel(e.Row.Cells[1].Text.toInt32());
                    if (Model_tab_Orderdetails != null && Model_tab_Orderdetails.isdeleted.toBoolean() == false)
                    {
                        e.Row.Cells[1].Text = Model_tab_Orderdetails.GoodName;
                    }
                    else
                    {
                        EggsoftWX.BLL.tab_Order_ShopingCart BLL_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                        EggsoftWX.Model.tab_Order_ShopingCart Model_tab_Order_ShopingCart = BLL_tab_Order_ShopingCart.GetModel(e.Row.Cells[1].Text.toInt32());
                        if (Model_tab_Order_ShopingCart != null)
                        {
                            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                            EggsoftWX.Model.tab_Goods Model_tab_Goods = BLL_tab_Goods.GetModel(Model_tab_Order_ShopingCart.GoodID.toInt32());
                            if (Model_tab_Goods != null)
                            {
                                e.Row.Cells[1].Text = Model_tab_Goods.Name;
                            }
                        }
                    }
                }
                #region 优惠券所处位置 及状态
                if (e.Row.Cells[1].Text.toInt32() == 0)
                {
                    e.Row.Cells[2].Text = "未消费";
                }
                else 
                {

                    string strSelect = @"
                    SELECT tab_Orderdetails.ID, tab_Order.PayStatus, tab_Order.isReceipt, tab_Order.UserID
                        FROM      tab_Orderdetails LEFT OUTER JOIN
                                        tab_Order ON tab_Orderdetails.OrderID = tab_Order.ID
                        WHERE (tab_Orderdetails.ID = " + e.Row.Cells[1].Text + " and isdeleted=0)";
                    System.Data.DataTable DataDataTable = BLL_tab_Orderdetails.SelectList(strSelect).Tables[0];
                    if (DataDataTable.Rows.Count > 0)
                    {
                        string strPayStatus = DataDataTable.Rows[0]["PayStatus"].toString();
                        string strisReceipt = DataDataTable.Rows[0]["isReceipt"].toString();
                        if (strPayStatus == "1")
                        {
                            e.Row.Cells[2].Text = "已支付";
                            if (strisReceipt == "1")
                            {
                                e.Row.Cells[2].Text += " 已收货";

                            }
                        }
                        else
                        {
                            e.Row.Cells[2].Text = "未支付";
                        }
                    }
                    else
                    {
                        e.Row.Cells[2].Text = "购物车";
                    }
                }
                #endregion 优惠券所处位置 及状态

                e.Row.Cells[3].Text = Eggsoft_Public_CL.Pub.GetNickName((e.Row.Cells[3].Text)); ;
                e.Row.Cells[4].Text = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(e.Row.Cells[4].Text)) + "元"; ;
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
            Response.Redirect("Shopping_Vouchers_Manage.aspx?type=Add");
        }

    }
}