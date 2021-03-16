using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._09System_Status
{
    public partial class UserStatus : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_UserStatus")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限


            if (!IsPostBack)
            {
                //string strWhereShopClient_ID = "";

                //strWhereShopClient_ID = "(Api_Authorize=1 or Subscribe=1) and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");


                ViewState["PageIndex"] = 1;
                if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
         
                ViewState["PageSize"] = 20;
                pathSearch();

                //ViewState["RecordCount"] = bll_tab_User.ExistsCount(strWhereShopClient_ID).ToString();
                //BindAnnounce();
                //ShowState();
                //InitGoPage();


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
            sql.addOrderField("id", "desc");//第一排序字段  
            sql.table = @"(SELECT   tab_User.*, tab_User_1.UserRealName AS ParentUserRealName, tab_User_1.ShopUserID AS ParentShopUserID, 
                tab_User_1.NickName AS ParentNickName, tab_User_1.HeadImageUrl AS ParentHeadImageUrl,
				 TeamUserRealName, TeamShopUserID, 
                TeamNickName,  TeamHeadImageUrl
			
FROM      tab_User 

LEFT OUTER JOIN
                tab_User AS tab_User_1 ON tab_User.ShopClientID = tab_User_1.ShopClientID AND
                tab_User.ParentID = tab_User_1.ID

LEFT OUTER JOIN

   (select tab_ShopClient_Agent_.ID,UserID,tab_ShopClient_Agent_.ShopClientID,tab_User.UserRealName AS TeamUserRealName,tab_User.ShopUserID as TeamShopUserID,tab_User.NickName as TeamNickName,tab_User.HeadImageUrl as TeamHeadImageUrl from tab_ShopClient_Agent_ LEFT OUTER JOIN tab_User on tab_User.TeamID = tab_ShopClient_Agent_.ID) viewTeamTable
            on     tab_User.ShopClientID = viewTeamTable.ShopClientID AND
                tab_User.TeamID = viewTeamTable.ID

) Viewtab_User";


            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            StringBuilder mystringBuilder = new StringBuilder();
             string  strSQL= @"(SELECT   V5_1.ID, V5_1.ContactMan, V5_1.ContactPhone, V5_1.UserRealName, V5_1.Country, V5_1.Sheng, V5_1.City, V5_1.Area, 
                V5_1.PostCode, V5_1.Sex, V5_1.Email, V5_1.Address, V5_1.IDCard, V5_1.Default_Address, V5_1.OpenID, 
                V5_1.HeadImageUrl, V5_1.Api_Authorize, V5_1.Subscribe, V5_1.IFShowCityHelp, V5_1.RemainingSum, 
                V5_1.IFSendWeiBaiQuan, V5_1.IFSendWeiBaiQuan_LiuZong, V5_1.SocialPlatform, V5_1.ShopClientID, 
                V5_1.AlipayNumOrWeiXinPay, V5_1.ShopUserID, V5_1.ParentID, V5_1.TeamID, V5_1.HowToGetProduct, 
                V5_1.DefaultO2OShop, V5_1.multi_DuoKeFu_Lastupdatetime, V5_1.Password, V5_1.UserAccount, V5_1.InsertTime, 
                V5_1.SafeCode, V5_1.CreatTime, V5_1.Updatetime, V5_1.CreateBy, V5_1.UpdateBy, V5_1.NickName, V5_1.Isdeleted, 
                V5_1.ParentUserRealName, V5_1.ParentShopUserID, V5_1.ParentNickName, V5_1.ParentHeadImageUrl, 
                V6.NickName AS TeamNickName, V6.UserRealName AS TeamUserRealName,v6.TeamShopUserID,v6.TeamHeadImageUrl,v6.ShopTeamID
FROM      (SELECT   tab_User.ID, tab_User.ContactMan, tab_User.ContactPhone, tab_User.UserRealName, 
                                 tab_User.Country, tab_User.Sheng, tab_User.City, tab_User.Area, tab_User.PostCode, 
                                 tab_User.Sex, tab_User.Email, tab_User.Address, tab_User.IDCard, 
                                 tab_User.Default_Address, tab_User.OpenID, tab_User.HeadImageUrl, 
                                 tab_User.Api_Authorize, tab_User.Subscribe, tab_User.IFShowCityHelp, 
                                 tab_User.RemainingSum, tab_User.IFSendWeiBaiQuan, tab_User.IFSendWeiBaiQuan_LiuZong, 
                                 tab_User.SocialPlatform, tab_User.ShopClientID, tab_User.AlipayNumOrWeiXinPay, 
                                 tab_User.ShopUserID, tab_User.ParentID, tab_User.TeamID, tab_User.HowToGetProduct, 
                                 tab_User.DefaultO2OShop, tab_User.multi_DuoKeFu_Lastupdatetime, tab_User.Password, 
                                 tab_User.UserAccount, tab_User.InsertTime, tab_User.SafeCode, tab_User.CreatTime, 
                                 tab_User.Updatetime, tab_User.CreateBy, tab_User.UpdateBy, tab_User.NickName, 
                                 tab_User.Isdeleted, tab_User_1.UserRealName AS ParentUserRealName, 
                                 tab_User_1.ShopUserID AS ParentShopUserID, tab_User_1.NickName AS ParentNickName, 
                                 tab_User_1.HeadImageUrl AS ParentHeadImageUrl
                 FROM      tab_User LEFT OUTER JOIN
                                 tab_User AS tab_User_1 ON tab_User.ShopClientID = tab_User_1.ShopClientID AND 
                                 tab_User.ParentID = tab_User_1.ID where tab_User.ShopClientID=@ShopClientID) AS V5_1 LEFT OUTER JOIN
                    (SELECT   tab_ShopClient_Agent_.ID, tab_ShopClient_Agent_.UserID, tab_ShopClient_Agent_.ShopTeamID, 
                                     tab_ShopClient_Agent_.ShopClientID, tab_User_2.UserRealName, tab_User_2.ShopUserID, 
                                     tab_User_2.NickName, tab_User_2.HeadImageUrl as TeamHeadImageUrl,tab_User_2.ShopUserID as TeamShopUserID, tab_ShopClient_Agent_.ShopName, 
                                     tab_User_2.ContactMan, tab_ShopClient_Agent_.AgentLevelSelect
                     FROM      tab_ShopClient_Agent_ LEFT OUTER JOIN
                                     tab_User AS tab_User_2 ON tab_ShopClient_Agent_.ShopClientID = tab_User_2.ShopClientID AND 
                                     tab_ShopClient_Agent_.UserID = tab_User_2.ID
                     WHERE   (tab_ShopClient_Agent_.AgentLevelSelect > 0) and  tab_ShopClient_Agent_.ShopClientID=@ShopClientID) AS V6 ON V5_1.ShopClientID = V6.ShopClientID AND 
                V5_1.TeamID = V6.ID)Viewtab_User";
            mystringBuilder.AppendFormat(strSQL, strShopClientID);



            sql.table = mystringBuilder.ToString();



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
      ,[ParentUserRealName]
      ,[ParentShopUserID]
      ,[ParentNickName],[ParentName]=isnull(ParentUserRealName,'')+isnull(ParentNickName,'')
      ,[ParentHeadImageUrl]
      ,[TeamUserRealName]
      ,[TeamShopUserID],[ShopTeamID]
      ,[TeamNickName],[TeamName]=isnull(TeamUserRealName,'')+isnull(TeamNickName,'')
      ,[TeamHeadImageUrl]
";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            sql.where = ViewState["searchWhere"].toString();
            string strSql = sql.getSQL(bll_tab_User.ExistsCount(ViewState["searchWhere"].toString()));
            gvAnnounce.DataSource = bll_tab_User.SelectList(strSql, strShopClientID);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Decimal myCountMoney = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney);
                e.Row.Cells[8].Text = "<a href=\"UserStatus_Money.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney) + "</a>";


                Decimal myCountMoney_Vouchers = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney_Vouchers);
                e.Row.Cells[9].Text = "<a href=\"UserStatus_Quan.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "</a>";


                e.Row.Cells[10].Text += "<br /><br /><a href=\"User_ModifyParent.aspx?type=Modify&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=../09System_Status/UserStatus.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">修改上级</a>";
                e.Row.Cells[13].Text += "<br /><br /><a href=\"User_ModifyTeamID.aspx?type=Modify&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=../09System_Status/UserStatus.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">修改团队</a>";

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
            //string strSearchWhere = ViewState["searchWhere"].ToString();
            ViewState["RecordCount"] = bll_tab_User.ExistsCount(ViewState["searchWhere"].toString());


           
            BindAnnounce();
            ShowState();
            InitGoPage();
        }

    }
}