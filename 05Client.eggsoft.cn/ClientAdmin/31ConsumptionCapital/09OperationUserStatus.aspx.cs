using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _09OperationUserStatus : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.b005_UserID_Operation_ID bll_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 没有打开运营中心的权限
                if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_09OperationUserStatus")))
                {
                    Response.Write("<script>window.close()</script>");
                    return;
                }
                #endregion 没有打开运营中心的权限

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


            string sql_table = @"(SELECT V4.ID,
 V4.ContactMan,
 V4.ContactPhone,
 V4.UserRealName,
 V4.Country,
 V4.Sheng,
 V4.City,
 V4.Area,
 V4.PostCode,
 V4.Sex,
 V4.Email,
 V4.Address,
 V4.Default_Address,
 V4.OpenID,
 V4.HeadImageUrl,
 V4.Api_Authorize,
 V4.Subscribe,
 V4.IFShowCityHelp,
 V4.RemainingSum,
 V4.IFSendWeiBaiQuan,
 V4.IFSendWeiBaiQuan_LiuZong,
 V4.SocialPlatform,
 V4.ShopClientID,
 V4.AlipayNumOrWeiXinPay,
 V4.ShopUserID,
 V4.ParentID,
 V4.HowToGetProduct,
 V4.DefaultO2OShop,
 V4.multi_DuoKeFu_Lastupdatetime,
 V4.Password,
 V4.UserAccount,
 V4.InsertTime,
 V4.SafeCode,
 V4.CreatTime,
 V4.Updatetime,
 V4.CreateBy,
 V4.UpdateBy,
 V4.NickName,
 V4.Isdeleted,
 V4.ShopClientIDUserID_Operation_ID,
 V4.OperationCenterID,
 V4.OperationCenterID_UserID,V4.ActiveAccount,
 tab_User_1.NickName AS OperationCenterNickName,
 V4.UserParentID,
 tab_User.NickName AS UserParentIDNickName,
 V4.IsDeletedUserID_Operation,
 b002_OperationCenter.MasterName AS OperationCenterMasterNamew
FROM (SELECT TOP (100) PERCENT tab_User.*,
 b005_UserID_Operation_ID.ShopClientID AS ShopClientIDUserID_Operation_ID,
 b005_UserID_Operation_ID.OperationCenterID,
 b005_UserID_Operation_ID.OperationCenterID_UserID,
 b005_UserID_Operation_ID.UserParentID,b005_UserID_Operation_ID.ActiveAccount,
 b005_UserID_Operation_ID.IsDeleted AS IsDeletedUserID_Operation
 FROM b005_UserID_Operation_ID
 LEFT OUTER JOIN tab_User
 ON b005_UserID_Operation_ID.ShopClientID = tab_User.ShopClientID
 AND b005_UserID_Operation_ID.UserID = tab_User.ID
 WHERE ( b005_UserID_Operation_ID.ShopClientID = " + strShopClientID + @" )) V4
 LEFT OUTER JOIN tab_User
 ON V4.ShopClientID = tab_User.ShopClientID
 AND V4.UserParentID = tab_User.ID
 LEFT OUTER JOIN tab_User AS tab_User_1
 ON V4.ShopClientID = tab_User_1.ShopClientID
 AND V4.OperationCenterID_UserID = tab_User_1.ID
 LEFT OUTER JOIN b002_OperationCenter
 ON V4.ShopClientID = b002_OperationCenter.ShopClient_ID
 AND V4.OperationCenterID = b002_OperationCenter.ID) ___tab__OperationCenterUser
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
      ,[ContactMan]
      ,[ContactPhone]
      ,[UserRealName]
      ,[Country]
      ,[Sheng]
      ,[City]
      ,[Area]
      ,[PostCode]
      ,[Sex]
      ,[Email]
      ,[Address]
      ,[Default_Address]
      ,[OpenID]
      ,[HeadImageUrl]
      ,[Api_Authorize]
      ,[Subscribe]
      ,[IFShowCityHelp]
      ,[RemainingSum]
      ,[IFSendWeiBaiQuan]
      ,[IFSendWeiBaiQuan_LiuZong]
      ,[SocialPlatform]
      ,[ShopClientID]
      ,[AlipayNumOrWeiXinPay]
      ,[ShopUserID]
      ,[ParentID]
      ,[HowToGetProduct]
      ,[DefaultO2OShop]
      ,[multi_DuoKeFu_Lastupdatetime]
      ,[Password]
      ,[UserAccount]
      ,[InsertTime]
      ,[SafeCode]
      ,[CreatTime]
      ,[Updatetime]
      ,[CreateBy]
      ,[UpdateBy]
      ,[NickName]
      ,[Isdeleted]
      ,[ShopClientIDUserID_Operation_ID]
      ,[OperationCenterID],[ActiveAccount]
      ,[OperationCenterID_UserID]
      ,[OperationCenterNickName]
      ,[UserParentID]
      ,[UserParentIDNickName]
      ,[IsDeletedUserID_Operation]
      ,[OperationCenterMasterNamew]";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            //string strwhere = "(Api_Authorize=1 or Subscribe=1) and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            sql.where = ViewState["searchWhere"].toString();
            Int32 Int32count = bll_b005_UserID_Operation_ID.SelectList("select count(1) from " + GetTable() + " where 1=1 and " + ViewState["searchWhere"].toString()).Tables[0].Rows[0][0].toInt32();

            string strSql = sql.getSQL(Int32count);

            gvAnnounce.DataSource = bll_b005_UserID_Operation_ID.SelectList(strSql);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Text += "<br /><a href=\"../09System_Status/User_ModifyParent.aspx?type=Modify&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=../31ConsumptionCapital/09OperationUserStatus.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">修改上级</a>";

                e.Row.Cells[5].Text += "<br /><a href=\"../31ConsumptionCapital/15User_ModifyYunYingZhongXin.aspx?type=Modify&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=../31ConsumptionCapital/09OperationUserStatus.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">修改运营中心</a>";

                #region  修改激活状态
                string strActiveUser_ModifyState = e.Row.Cells[7].Text;
                string strInput = "";
                if (strActiveUser_ModifyState.toBoolean())
                {
                    strInput = @"<input type=""checkbox"" name=""gvAnnounce$ctl02$ctl00"" checked=""checked"" disabled=""enabled"">";
                }
                else
                {
                    strInput = @"<input type=""checkbox"" name=""gvAnnounce$ctl02$ctl00"" disabled=""disabled"">";
                }
                e.Row.Cells[7].Text = strInput + "<br /><a href=\"../31ConsumptionCapital/21ActiveUser_ModifyState.aspx?type=Modify&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=../31ConsumptionCapital/09OperationUserStatus.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">修改激活状态</a>";
                #endregion  修改激活状态

                Decimal myCountWealth = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Int32.Parse(e.Row.Cells[0].Text), out myCountWealth);
                e.Row.Cells[8].Text = "<a href=\"07User_WealthStatus.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountWealth) + "</a>";


                Decimal myCountMoney = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney);
                e.Row.Cells[9].Text = "<a href=\"../09System_Status/UserStatus_Money.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney) + "</a>";


                Decimal myCountMoney_Vouchers = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney_Vouchers);
                e.Row.Cells[10].Text = "<a href=\"../09System_Status/UserStatus_Quan.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "</a>";
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
            ViewState["PageIndex"] = 1;
            pathSearch();
        }

        protected void pathSearch()
        {
            string strWhere = "";


            if (string.IsNullOrEmpty(TextBox_ShopUserID.Text) == false)
            {
                strWhere += " and ShopUserID=" + TextBox_ShopUserID.Text;
            }
            if (string.IsNullOrEmpty(TextBox1OperationCenterID.Text) == false)
            {
                strWhere += " and OperationCenterID=" + TextBox1OperationCenterID.Text;
            }
            if (string.IsNullOrEmpty(TextBox_NickName.Text) == false)
            {
                strWhere += " and [NickName] like '%" + TextBox_NickName.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_ContacPhone.Text) == false)
            {
                strWhere += " and ContactPhone like '%" + TextBox_ContacPhone.Text + "%'";
            }

            strWhere += " and Subscribe = " + (CheckBox_Subscribe.Checked ? 1 : 0) + "";

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();



            ViewState["searchWhere"] = "ShopClientID=" + strShopClientID + strWhere;

            Int32 Int32count = bll_b005_UserID_Operation_ID.SelectList("select count(1) from " + GetTable() + " where 1=1 and " + ViewState["searchWhere"].toString()).Tables[0].Rows[0][0].toInt32();

            ViewState["RecordCount"] = Int32count;




            BindAnnounce();
            ShowState();
            InitGoPage();
        }

    }
}