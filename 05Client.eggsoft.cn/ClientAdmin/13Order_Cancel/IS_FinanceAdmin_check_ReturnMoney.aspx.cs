using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._13Order_Cancel
{
    public partial class IS_FinanceAdmin_check_ReturnMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ReturnMoney bll_tab_ReturnMoney = new EggsoftWX.BLL.tab_ReturnMoney();
        EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();
        EggsoftWX.Model.tab_Order Model_tab_Order = new EggsoftWX.Model.tab_Order();


        protected void Page_Load(object sender, EventArgs e)
        {

            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("MoneyManage_IS_FinanceAdmin_check_ReturnMoney")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

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
                ViewState["RecordCount"] = bll_tab_ReturnMoney.ExistsCount("FinanceCheck=1 and ShopClientID=" + strShopClientID);
            }
            else
            {
                ViewState["RecordCount"] = bll_tab_ReturnMoney.ExistsCount("FinanceCheck=0 and ShopClientID=" + strShopClientID);
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
            sql.addOrderField("Viewtab_ReturnMoney.UpdateTime", "desc");//第一排序字段  
            sql.addOrderField("Viewtab_ReturnMoney.id", "desc");//第二排序字段  
            sql.table = @"(SELECT tab_User.ShopUserID, tab_User.NickName, tab_ReturnMoney.*, 
      tab_Order.IsDeleted
FROM tab_Order RIGHT OUTER JOIN
      tab_ReturnMoney ON
      tab_Order.ShopClient_ID = tab_ReturnMoney.ShopClientID AND
      tab_Order.ID = tab_ReturnMoney.OrderID LEFT OUTER JOIN
      tab_User ON tab_Order.ShopClient_ID = tab_User.ShopClientID AND
      tab_Order.UserID = tab_User.ID )Viewtab_ReturnMoney";
            sql.outfields = "id,[UpdateTime],[OrderID],[RefundMoney],[AdminFinance_User],[FinanceCheck],[AdminCheck],[ShopClientID],[ReturnMoneyTitle],[ReturnMoneyContent],[ShopUserID],[NickName]";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());

            String strWhere = "";
            if (CheckBox_IIS_Admin.Checked)
            {
                strWhere = "FinanceCheck=1 and ShopClientID=" + strShopClientID;

            }
            else
            {
                strWhere = "FinanceCheck=0 and ShopClientID=" + strShopClientID;
            }


            sql.where = strWhere;

            string strSql = sql.getSQL(bll_tab_ReturnMoney.ExistsCount(strWhere));
            gvAnnounce.DataSource = bll_tab_ReturnMoney.SelectList(strSql);

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

                //e.Row.Cells[1].Text = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(Int32.Parse(e.Row.Cells[1].Text));
                //e.Row.Cells[2].Text = Eggsoft_Public_CL.Pub.getPubMoney(Eggsoft_Public_CL.GoodP.GetThisOrderShopGet(Convert.ToInt32(strOrderID)));

                Model_tab_Order = bll_tab_Order.GetModel(Int32.Parse(e.Row.Cells[6].Text));
                string strPayWay = Model_tab_Order.PayWay;
                string str3PayLink = "";
                switch (strPayWay)
                {
                    case "Tenpay":
                        str3PayLink = "http://mch.tenpay.com/";
                        break;
                    case "Alipay":
                        str3PayLink = "https://auth.alipay.com/login/index.htm";
                        break;
                    case "WeiBaiPay":
                        str3PayLink = "#";
                        break;
                    case "KQPay":
                        str3PayLink = "https://passport.99bill.com/sso/login/authentication1.htm?v=v1.4&site2pstoretoken=v1.4~4945C3A0~FC28A27A3004194FD0B390462C52B3C2A1529C3B497A610BDC3AA31B14733E121EF36BDF2D630ED73691EB89F75EA1C3504B7C36D7FDA066C7AF0264C8B388EBDC17733F17712A84EE82565C764544291675937E041AFFF0B1D86D3392BCB3F0EEFC6AB035CD9F372FC3DE34953AF31012AB3773A217CEF2A5B5788FEBB713E0D2A7AE4435F37BF7C091AC1DCECE9D9B5C542F6095BDB18FBA55C43207F2FD4DCE3FE141CAEC33966759321639470B9619BAFE43D6C712741E568F6718BD94A40330E36D05611414&p_error_code=&ssousername=";
                        break;
                }

                e.Row.Cells[6].Text = "<a href=\"" + str3PayLink + "\"  target=\"_blank\">" + Eggsoft_Public_CL.Pub.gePayChineseName(Model_tab_Order.PayWay) + "</a>";


                if (e.Row.Cells[9].Text == "True")
                {
                    e.Row.Cells[9].Text = "<span id=\"IS_Admin_check_AsyncStatus" + strOrderID + "\">已通过</span>";
                }
                else
                {
                    e.Row.Cells[9].Text = "<span id=\"IS_Admin_check_AsyncStatus" + strOrderID + "\"><a href=\"#\" style=\"color:blue\" onclick=\"IS_Admin_check_Async_ShowFuction(" + strOrderID + ",1);\">通过</a></span>";
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