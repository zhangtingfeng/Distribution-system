using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _02OperationCenter : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strModifyASPX = "03OperationCenter_Manage.aspx";
        private string strBoardASPX = "02OperationCenter.aspx";

        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_02OperationCenter")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限

            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";

            if (!IsPostBack)
            {
                Button1_Click_Query(null, null);
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
            string strWhere = ViewState["searchWhere"].ToString();
            gvAnnounce.DataSource = blltab_ZC_01Product.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strZhongChouIDID = e.Row.Cells[0].Text;
                //str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                //EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                //EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                //string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                //string strLink = "http://" + strErJiYuMing + "/addfunction/04zc_project/03zc.html?zcid=" + strZhongChouIDID;

                //if (e.Row.Cells[1].Text == "True")
                //    e.Row.Cells[1].Text = "上架";
                //else
                //    e.Row.Cells[1].Text = "<span style=\"color: #FF0066;\">下架</span>";

                e.Row.Cells[10].Text = "<a href=\"" + strModifyASPX + "?type=Modify&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()) + "\">修改</a>";
                e.Row.Cells[10].Text += "<br /><a href=\"" + strModifyASPX + "?type=Delete&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()) + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
            }
        }

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            pathSearch();
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
            pathSearch();
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
            pathSearch();
            InitGoPage();
        }
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = GetPageCount();
            pathSearch();
            InitGoPage();
        }
        protected void ddlGoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = Int32.Parse(ddlGoPage.SelectedValue);
            pathSearch();
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
            Response.Redirect(strModifyASPX + "?type=Add&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()));
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 20;


            string strIfissaled = CheckBox_IfRunningState.Checked ? "1" : "0";
            string strWhere = " and (RunningState=" + strIfissaled + ")";


            if (string.IsNullOrEmpty(TextBox_NickName.Text) == false)
            {
                strWhere += " and (NickName like '%" + TextBox_NickName.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox_YuE.Text) == false)
            {
                strWhere += " and (b003_TotalCredits_OperationCenterRemainingSum " + DropDownList_DestinationPrice.Text + TextBox_YuE.Text + ")";
            }


            if (string.IsNullOrEmpty(TextBox_MasterName.Text) == false)
            {
                strWhere += " and (MasterName like '%" + TextBox_MasterName.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox_MasterPhone.Text) == false)
            {
                strWhere += " and (MasterPhone like '%" + TextBox_MasterPhone.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox_ShopUserID.Text) == false)
            {
                strWhere += " and (ShopUserID =" + TextBox_ShopUserID.Text + ")";
            }


            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strZhongChouJointWhere = "";
            strZhongChouJointWhere += " IsDeleted=0 ";

            string strSQLTable = string.Format(@" SELECT   b002_OperationCenter.ID, b002_OperationCenter.ParentID, b002_OperationCenter.UserID, 
                b002_OperationCenter.ShopClient_ID, b002_OperationCenter.MasterName, 
                b002_OperationCenter.MasterPhone, b002_OperationCenter.MasterAddress, 
                b002_OperationCenter.BankAccountUserName, b002_OperationCenter.BankAccountName, 
                b002_OperationCenter.BankAccountNumber, b002_OperationCenter.RunningState, 
                b002_OperationCenter.AccountState,b002_OperationCenter.ShareholderState, b002_OperationCenter.CreateBy, b002_OperationCenter.UpdateTime, 
                b002_OperationCenter.UpdateBy, b002_OperationCenter.CreatTime, b002_OperationCenter.IsDeleted, 
                tab_User.ContactMan, tab_User.NickName, tab_User.UserRealName, tab_User.ContactPhone, 
                tab_User.ShopUserID, V42.UserID AS b003_TotalCredits_OperationCenterUserID, 
                V42.RemainingSum AS b003_TotalCredits_OperationCenterRemainingSum, 
                V41.UserID AS TotalCredits_Consume_Or_RechargeUserID, 
                V41.RemainingSum AS TotalCredits_Consume_Or_RechargeRemainingSum, 
                V7.ShopUserID AS ParentIDShopUserID, V7.NickName AS ParentIDExprNickName, 
                V7.UserRealName AS ParentIDExprUserRealName
FROM      b002_OperationCenter LEFT OUTER JOIN
            (SELECT   tab_User.UserRealName, tab_User.ContactPhone, tab_User.ShopUserID, tab_User.NickName, 
                b002_OperationCenter.*
FROM      b002_OperationCenter LEFT OUTER JOIN
                tab_User ON b002_OperationCenter.UserID = tab_User.ID)    V7 ON b002_OperationCenter.ParentID = V7.ID LEFT OUTER JOIN
                    (SELECT   tab_TotalCredits_Consume_Or_Recharge_1.UserID, 
                                     tab_TotalCredits_Consume_Or_Recharge_1.RemainingSum
                     FROM      (SELECT   MAX(ID) AS maxID
                                      FROM      tab_TotalCredits_Consume_Or_Recharge
                                      WHERE   (ShopClient_ID = {0})
                                      GROUP BY UserID) AS V6_2 LEFT OUTER JOIN
                                     tab_TotalCredits_Consume_Or_Recharge AS tab_TotalCredits_Consume_Or_Recharge_1 ON 
                                     V6_2.maxID = tab_TotalCredits_Consume_Or_Recharge_1.ID) AS V41 ON 
                b002_OperationCenter.UserID = V41.UserID AND b002_OperationCenter.ShopClient_ID = {0} LEFT OUTER JOIN
                    (SELECT   b003_TotalCredits_OperationCenter_1.UserID, b003_TotalCredits_OperationCenter_1.RemainingSum
                     FROM      (SELECT   MAX(ID) AS maxID
                                      FROM      b003_TotalCredits_OperationCenter
                                      WHERE   (ShopClient_ID = {0})
                                      GROUP BY UserID) AS V61 LEFT OUTER JOIN
                                     b003_TotalCredits_OperationCenter AS b003_TotalCredits_OperationCenter_1 ON 
                                     V61.maxID = b003_TotalCredits_OperationCenter_1.ID) AS V42 ON 
                b002_OperationCenter.UserID = V42.UserID LEFT OUTER JOIN
                tab_User ON b002_OperationCenter.ShopClient_ID = tab_User.ShopClientID AND 
                b002_OperationCenter.UserID = tab_User.ID AND b002_OperationCenter.ShopClient_ID = {0} WHERE   (b002_OperationCenter.ShopClient_ID = {0})
", strShopClientID);

            ViewState["SQLTable"] = "(" + strSQLTable + ") vTable";

            ViewState["SQLWhere"] = strZhongChouJointWhere + strWhere;
            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strZhongChouJointWhere, "count(*) as RecordCount") + strWhere;
            string strRecordCount = blltab_ZC_01Product.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {
            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            //sql.addOrderField("tab_ZC_01Product.sort", "asc");//第一排序字段  
            sql.addOrderField("id", "asc");//第二排序字段  
            sql.table = ViewState["SQLTable"].ToString();



            string stroutfields = @"[ID],ParentID
      ,[UserID]
      ,[ShopClient_ID]
      ,[MasterName]
      ,[MasterPhone]
      ,[MasterAddress]
      ,[BankAccountUserName]
      ,[BankAccountName]
      ,[BankAccountNumber]
      ,[RunningState]
      ,[AccountState],[ShareholderState]
      ,[CreateBy]
      ,[UpdateTime]
      ,[UpdateBy]
      ,[CreatTime]
      ,[IsDeleted]
      ,[ContactMan]
      ,[NickName]
      ,[UserRealName]
      ,[ContactPhone]
      ,[ShopUserID],
ParentIDShopUserID
,
ParentIDExprNickName=(case isnull(ParentIDShopUserID,0) when 0 then '无上级' else ParentIDExprNickName end)
,[ParentIDExprUserRealName]
      ,[b003_TotalCredits_OperationCenterUserID]
      ,[b003_TotalCredits_OperationCenterRemainingSum]
      ,[TotalCredits_Consume_Or_RechargeUserID]
      ,[TotalCredits_Consume_Or_RechargeRemainingSum]";


            sql.outfields = stroutfields;
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = ViewState["SQLWhere"].ToString();
            sql.where = strwhere;
            string strSql = sql.getSQL(Int32.Parse(ViewState["RecordCount"].ToString()));

            ViewState["searchWhere"] = strSql;// " and ShopClientID=" + strShopClientID + strWhere;


            BindAnnounce();
            ShowState();

        }


    }
}
