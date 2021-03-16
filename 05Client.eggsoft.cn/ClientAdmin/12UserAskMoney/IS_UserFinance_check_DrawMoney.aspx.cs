using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._12UserAskMoney
{
    public partial class IS_UserFinance_check_DrawMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.View_User_AskGetMoney bll_View_User_AskGetMoney = new EggsoftWX.BLL.View_User_AskGetMoney();


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("MoneyManage_IS_UserFinance_check_DrawMoney")))
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

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            String strWhere = "ShopClientID=" + strShopClientID + " and ";
            if (CheckBox_IIS_Admin.Checked)
            {
                ViewState["RecordCount"] = bll_View_User_AskGetMoney.ExistsCount(strWhere + "IFSendMoney=1");
            }
            else
            {
                ViewState["RecordCount"] = bll_View_User_AskGetMoney.ExistsCount(strWhere + "IFSendMoney=0");
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
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            string strCardDESC = "(Isnull(CardName,'用户特征'+cast(UserID as varchar(32)))+(CASE WHEN isnull(payment_no,'')<>'' THEN '已支付流水号为'+isnull(payment_no,'') ELSE '' END)+(  CASE WHEN Isnull(ResultCode, '') = '成功' THEN '成功' WHEN isnull(ResultCode,'')='success' THEN ',已支付成功'+isnull(ResultCode,'') ELSE ',已支付失败'+','+isnull(ResultCode,'') END)) as CardName";

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("View_User_AskGetMoney.ID", "desc");//第一排序字段  
            sql.addOrderField("View_User_AskGetMoney.UpdateTime", "desc");//第一排序字段  
            sql.table = "View_User_AskGetMoney";
            sql.outfields = "ShopUserID," + strCardDESC + ",ID,AskMoney,UpdateTime,AskMemo,UserID,IFSendMoney,UserRealName";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());


            String strWhere = "ShopClientID=" + strShopClientID + " and ";
            if (CheckBox_IIS_Admin.Checked)
            {
                strWhere += "isnull(IFSendMoney,0)=1 ";

            }
            else
            {
                strWhere += "isnull(IFSendMoney,0)=0 ";
            }


            sql.where = strWhere;

            //Query()
            int intRecordCount = bll_View_User_AskGetMoney.ExistsCount(strWhere);
            string strSql = sql.getSQL(intRecordCount);
            gvAnnounce.DataSource = bll_View_User_AskGetMoney.SelectList(strSql);

            gvAnnounce.DataBind();
        }



        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[0].Text;


                string strUserID = e.Row.Cells[3].Text;
                e.Row.Cells[3].Text = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                Decimal myCountyuEArgMoney = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(strUserID), out myCountyuEArgMoney);
                e.Row.Cells[6].Text = "¥" + Eggsoft_Public_CL.Pub.getPubMoney(myCountyuEArgMoney);

                string strNeedMoney = e.Row.Cells[5].Text;
                e.Row.Cells[5].Text = "¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strNeedMoney)) + "";
                string strUserMoney = e.Row.Cells[6].Text;
                strUserMoney = strUserMoney.Substring(1, strUserMoney.Length - 1);
                e.Row.Cells[6].Text = "<a target='_blank' href='/ClientAdmin/09System_Status/UserStatus_Money.aspx?userid=" + strUserID + "'>¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strUserMoney)) + "</a>";

                if (e.Row.Cells[10].Text == "True")
                {
                    e.Row.Cells[10].Text = "已转";
                    e.Row.Cells[11].Text = "已转";
                }
                else
                {
                    if (strUserMoney.toDecimal() >= strNeedMoney.toDecimal())
                    {
                        e.Row.Cells[10].Text = "<span id=\"IS_Admin_check_AsyncStatus" + strID + "\"><a href=\"#\" style=\"color:blue\" onclick=\"IS_Admin_check_Async_ShowFuction(" + strID + ",1);\">已经线下转账猛击这里</a></span>";
                        e.Row.Cells[11].Text += "<span id=\"IS_Admin_check_AsyncStatus_WeiXinRed" + strID + "\"><a href=\"#\" style=\"color:blue\" onclick=\"IS_Admin_check_Async_ShowFuction_DoWeiXinHoneBao(" + strID + "," + strNeedMoney + ",1);\">微信实时转账</a></span>";
                    }
                    else
                    {
                        e.Row.Cells[10].Text = "余额不足，无法操作，建议删除";
                        e.Row.Cells[11].Text = "余额不足，无法操作，建议删除";
                    }
                    e.Row.Cells[12].Text = "<a href=\"IS_UserFinance_check_DrawMoney_Manage.aspx?type=Delete&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=IS_UserFinance_check_DrawMoney.aspx\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
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