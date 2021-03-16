using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _20Order_FromWealthToMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.b006_TotalWealth_OperationUser bll_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage")))
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
                string strwhere = "UserID=" + Request.QueryString["UserID"] + " and OrderDetailID=" + Request.QueryString["OrderDetailID"] + " and ShopClient_ID=" + strShopClientID;
                ViewState["RecordCount"] = bll_b006_TotalWealth_OperationUser.ExistsCount(strwhere).ToString();
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
            sql.addOrderField("b006_TotalWealth_OperationUser.id", "desc");//第一排序字段  
            //sql.table = "b006_TotalWealth_OperationUser";
            sql.table = @"
            b006_TotalWealth_OperationUser";

            sql.outfields = @" [ID]
      ,[UserID]
      ,[ShopClient_ID]
      ,[OrderDetailID]
      ,[UpdateTime]
      ,[ConsumeOrRechargeWealth]
      ,[ConsumeTypeOrRecharge]
      ,[RemainingSum]
      ,[Bool_ConsumeOrRecharge]
      ,[BoolIfOnlyonceUpdate]
      ,[CreatTime]
      ,[Creatby]";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toString();
            string strwhere = "UserID=" + Request.QueryString["UserID"]+ " and OrderDetailID="+ Request.QueryString["OrderDetailID"] + " and ShopClient_ID=" + strShopClientID;
            sql.where = strwhere;
          
            string strSql = sql.getSQL(bll_b006_TotalWealth_OperationUser.ExistsCount(strwhere));

            gvAnnounce.DataSource = bll_b006_TotalWealth_OperationUser.SelectList(strSql);

            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strID = e.Row.Cells[1].Text;
                //e.Row.Cells[1].Text = Eggsoft_Public_CL.GoodP.GetGood_Num_ID_From_Good_ID(Convert.ToInt32(strID));

                //string strUserID = e.Row.Cells[3].Text;
                //string strParentID = e.Row.Cells[4].Text;
                //string strGrandParentID = e.Row.Cells[5].Text;
                //string strGreatParentID = e.Row.Cells[6].Text;

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

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("GoodClass_Manage.aspx?type=Add");
        //}
    }
}