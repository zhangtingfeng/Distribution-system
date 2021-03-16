using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _06ThisDayReturnActualUser : System.Web.UI.Page
    {
        EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_08FullEveryDay")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;



                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toString();
                string strwhere = "ShopClient_ID=" + strShopClientID;
                string strbAskThisDayID = Request.QueryString["AskThisDay"];
                strwhere += " and CreatTime>= '" + strbAskThisDayID + " 00:00:00' and CreatTime<= '" + strbAskThisDayID + " 23:59:59'";
                ViewState["RecordCount"] = BLL_b006_TotalWealth_OperationUser.ExistsCount(strwhere).ToString();
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
            string strbAskThisDayID = Request.QueryString["AskThisDay"];
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toString();

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("b006_TotalWealth_OperationUser.id", "desc");//第一排序字段  
            sql.table = @"(SELECT   b006_TotalWealth_OperationUser.ID, b006_TotalWealth_OperationUser.UserID, 
                b006_TotalWealth_OperationUser.ShopClient_ID, b006_TotalWealth_OperationUser.UpdateTime, 
                b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth, tab_User.ShopUserID,
                b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge, 
                b006_TotalWealth_OperationUser.RemainingSum, 
                b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge, 
                b006_TotalWealth_OperationUser.BoolIfOnlyonceUpdate, b006_TotalWealth_OperationUser.CreatTime, 
                b008_OpterationUserActiveReturnMoneyOrderNum.OrderCount, b006_TotalWealth_OperationUser.OrderDetailID,
                b008_OpterationUserActiveReturnMoneyOrderNum.PayDateTime, 
                b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum, 
                b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit
FROM      b006_TotalWealth_OperationUser
LEFT OUTER JOIN
               tab_User ON b006_TotalWealth_OperationUser.UserID = tab_User.ID AND 
                b006_TotalWealth_OperationUser.ShopClient_ID = tab_User.ShopClientID


LEFT OUTER JOIN
                b008_OpterationUserActiveReturnMoneyOrderNum ON 
                b006_TotalWealth_OperationUser.ShopClient_ID = b008_OpterationUserActiveReturnMoneyOrderNum.ShopClient_ID
                 AND 
                b006_TotalWealth_OperationUser.OrderDetailID = b008_OpterationUserActiveReturnMoneyOrderNum.OrderDetailID
WHERE   (b006_TotalWealth_OperationUser.ShopClient_ID = " + strShopClientID + @") AND 
                (b006_TotalWealth_OperationUser.CreatTime >= '" + strbAskThisDayID + @" 00:00:00') AND 
                (b006_TotalWealth_OperationUser.CreatTime <= '" + strbAskThisDayID + @" 23:59:59')) b006_TotalWealth_OperationUser";
            sql.outfields = @"[ID],PayDateTime,OrderCount,ReturnMoneyUnit
      ,[UserID],OrderDetailID,ShopUserID
      ,[ShopClient_ID]
      ,[UpdateTime]
      ,[ConsumeOrRechargeWealth]
      ,[ConsumeTypeOrRecharge]
      ,[RemainingSum]
      ,[Bool_ConsumeOrRecharge]
      ,[BoolIfOnlyonceUpdate]
      ,[CreatTime]";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());

            string strwhere = "ShopClient_ID=" + strShopClientID;
            strwhere += " and 1=1";
            strwhere += " and (CreatTime >= '" + strbAskThisDayID + @" 00:00:00') AND
               (CreatTime <= '" + strbAskThisDayID + @" 23:59:59')";
            sql.where = strwhere;

            //string strSql = "select top 20 * from tab_Goods_Class where UserID=12 order by tab_Goods_Class.sort asc,tab_Goods_Class.id asc";
            string strSql = sql.getSQL(BLL_b006_TotalWealth_OperationUser.ExistsCount(strwhere));

            gvAnnounce.DataSource = BLL_b006_TotalWealth_OperationUser.SelectList(strSql);

            //gvAnnounce.DataSource = bll.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,ClassName,Sort,UpdateTime", "UserID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strID = e.Row.Cells[1].Text;
                string strUserID = (e.Row.Cells[1].Text);
                e.Row.Cells[1].Text = Eggsoft_Public_CL.Pub.GetNickName(strUserID);

                //string strUserID = e.Row.Cells[3].Text;
                //string strParentID = e.Row.Cells[4].Text;
                //string strGrandParentID = e.Row.Cells[5].Text;
                //string strGreatParentID = e.Row.Cells[6].Text;
                string strOrderDetailID = e.Row.Cells[7].Text;
                if (!string.IsNullOrEmpty(strOrderDetailID))
                {
                    string strSQL = @"SELECT   SUM(ConsumeOrRechargeWealth) AS sumConsumeOrRechargeWealth
                     FROM b006_TotalWealth_OperationUser
                     WHERE (Bool_ConsumeOrRecharge = 0) and OrderDetailID=" + strOrderDetailID;


                    string strDecimalAllMoney = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strSQL).toDecimal().toString();
                    string strLink = string.Format("20Order_FromWealthToMoney.aspx?userid={0}&OrderDetailID={1}", strUserID, strOrderDetailID);
                    e.Row.Cells[6].Text = "<a href='" + strLink + "'>" + strDecimalAllMoney + "</a>";
                }

                //e.Row.Cells[3].Text = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                //e.Row.Cells[4].Text = Eggsoft_Public_CL.Pub.GetNickName(strParentID);

                //Decimal myCountMoney_Vouchers = 0;
                //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney_Vouchers);         
                //e.Row.Cells[7].Text = Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers);
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


    }
}