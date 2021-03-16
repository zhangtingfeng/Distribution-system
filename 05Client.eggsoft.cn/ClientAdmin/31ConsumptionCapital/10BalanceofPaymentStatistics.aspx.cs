using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _10BalanceofPaymentStatistics : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.b002_OperationCenter bll_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
        protected void Page_Load(object sender, EventArgs e)
        {


            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_10BalanceofPaymentStatistics")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限
            if (!IsPostBack)
            {


                ViewState["PageIndex"] = 1;
                if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];

                ViewState["PageSize"] = 20;
                pathSearch();


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

        private string GetTable()
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            string sql_table = @"(SELECT V4.*,
 b002_OperationCenter.MasterName AS ParentIDMasterName,
 b002_OperationCenter.BankAccountUserName AS ParentIDBankAccountUserName 
FROM (SELECT TOP (100) PERCENT b002_OperationCenter.*,
 tab_User.UserRealName,
 tab_User.ShopUserID,
 tab_User.NickName,
 b002_OperationCenter.ShopClient_ID AS Expr1
 FROM b002_OperationCenter
 LEFT OUTER JOIN tab_User
 ON b002_OperationCenter.UserID = tab_User.ID
 WHERE ( b002_OperationCenter.IsDeleted = 0 )
 AND ( b002_OperationCenter.ShopClient_ID = " + strShopClientID + @" ) and  tab_User.ShopClientID=" + strShopClientID + @"    ) V4
 LEFT OUTER JOIN b002_OperationCenter
 ON V4.ShopClient_ID = b002_OperationCenter.ShopClient_ID
 AND V4.ParentID = b002_OperationCenter.ID
) ___tab__OperationCenterUser
                ";
            return sql_table;
        }


        private void BindAnnounce()
        {

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("id", "desc");//第一排序字段  
            sql.table = GetTable();
            //tab_User";
            sql.outfields = @"[ID]
      ,[ParentID]
      ,[UserID]
      ,[ShopClient_ID]
      ,[MasterName]
      ,[MasterPhone]
      ,[MasterAddress]
      ,[BankAccountUserName]
      ,[BankAccountName]
      ,[BankAccountNumber]
      ,[RunningState]
      ,[AccountState]
      ,[CreateBy]
      ,[UpdateTime]
      ,[UpdateBy]
      ,[CreatTime]
      ,[IsDeleted]
      ,[UserRealName]
      ,[ShopUserID]
      ,[NickName],ParentIDMasterName,ParentIDBankAccountUserName,(isnull(ParentIDMasterName,'')+isnull(ParentIDBankAccountUserName,'')) as ParentIDMasterNameParentIDBankAccountUserName";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            sql.where = ViewState["searchWhere"].toString();
            Int32 Int32count = bll_b002_OperationCenter.SelectList("select count(1) from " + GetTable() + " where 1=1 and " + ViewState["searchWhere"].toString()).Tables[0].Rows[0][0].toInt32();

            string strSql = sql.getSQL(Int32count);

            gvAnnounce.DataSource = bll_b002_OperationCenter.SelectList(strSql);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Decimal myCountWealth = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuETotalCredits_OperationCenter(Int32.Parse(e.Row.Cells[0].Text), out myCountWealth);
                e.Row.Cells[7].Text = "<a href=\"11CenterUser_MoneyStatus.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountWealth) + "</a>";


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

        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            pathSearch();
        }

        protected void pathSearch()
        {
            string strWhere = "";

            strWhere += " and RunningState=" + (CheckBox_RunningState.Checked ? 1 : 0);

            if (string.IsNullOrEmpty(TextBox_MasterName.Text) == false)
            {
                strWhere += " and [MasterName] like '%" + TextBox_MasterName.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_MasterPhone.Text) == false)
            {
                strWhere += " and MasterPhone like '%" + TextBox_MasterPhone.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_MasterPhone.Text) == false)
            {
                strWhere += " and MasterAddress like '%" + TextBox_MasterPhone.Text + "%'";
            }
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            ViewState["searchWhere"] = "ShopClient_ID=" + strShopClientID + strWhere;

            Int32 Int32count = bll_b002_OperationCenter.SelectList("select count(1) from " + GetTable() + " where 1=1 and " + ViewState["searchWhere"].toString()).Tables[0].Rows[0][0].toInt32();

            ViewState["RecordCount"] = Int32count;



            ViewState["PageIndex"] = 1;
            BindAnnounce();
            ShowState();
            InitGoPage();
        }

    }
}