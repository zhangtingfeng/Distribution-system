using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _16CheckModifyParent : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strModifyASPX = "17CheckModifyParent_Manage.aspx";
        private string strBoardASPX = "16CheckModifyParent.aspx";

        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.b010_AskModifyParent bllb010_AskModifyParents = new EggsoftWX.BLL.b010_AskModifyParent();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_18OperationWtiteOrder")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限

            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            //str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";

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
            gvAnnounce.DataSource = bllb010_AskModifyParents.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int FeedbackStatus = e.Row.Cells[8].Text.toInt32();
                if (FeedbackStatus == 0)
                {
                    e.Row.Cells[8].Text = "未处理";
                }
                else if (FeedbackStatus == 1)
                {///反馈状态  0 表示未处理   1表示接受申请  2 表示 拒绝申请 
                    e.Row.Cells[8].Text = "接受申请";
                }
                else if (FeedbackStatus == 2)
                {///反馈状态  0 表示未处理   1表示接受申请  2 表示 拒绝申请 
                    e.Row.Cells[8].Text = "拒绝申请";
                }

                e.Row.Cells[10].Text = "<a href=\"" + strModifyASPX + "?type=Modify&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()) + "\">立即反馈</a>";
                e.Row.Cells[10].Text += "<br /><a href=\"" + strModifyASPX + "?type=Delete&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()) + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

                str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                String strCenterID = e.Row.Cells[2].Text.ToString();
                EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("ID=@ID and shopClient_ID=@hopClient_ID", strCenterID.toInt32(), str_Pub_ShopClientID.toInt32());
                if (Model_b002_OperationCenter != null) e.Row.Cells[2].Text = Model_b002_OperationCenter.MasterName;

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
            //string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            //string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

            //string strAdd = strNavigateUrl + Server.UrlEncode("27ZC_Project/Zc01_Manage.aspx?type=Add");
            Response.Redirect(strModifyASPX + "?type=Add&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()));
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 5;


            string strIfissaled = CheckBox_IfDone.Checked ? "1" : "0";
            string strWhere = "";
            if (!CheckBox_IfDone.Checked)
            {
                strWhere += " FeedbackStatus=0";
            }
            else
            {
                strWhere += " FeedbackStatus<>0";
            }
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            if (string.IsNullOrEmpty(TextBox_userShopID.Text) == false)
            {
                strWhere += " and (BuyOrderShopUserID=" + TextBox_userShopID.Text + ")";
            }
            if (string.IsNullOrEmpty(TextBox_UserRealName.Text) == false)
            {
                strWhere += " and (BuyOrderUserRealName like  %" + TextBox_UserRealName.Text + "%)";
            }
            strWhere += " and (ShopClient_ID =" + strShopClientID + ")";



            strWhere += " and IsDeleted=0 ";

            //            string strSQLTable = string.Format(@" SELECT  tab_Goods.Name,tab_Goods.ShortInfo,b004_OperationGoods.* 
            //FROM     b004_OperationGoods LEFT OUTER JOIN
            //               tab_Goods ON b004_OperationGoods.GoodID =tab_Goods.ID AND 
            //               b004_OperationGoods.ShopClient_ID =tab_Goods.ShopClient_ID
            // WHERE   (b004_OperationGoods.ShopClient_ID = {0})
            //", strShopClientID);

            ViewState["SQLTable"] = "b010_AskModifyParent";

            ViewState["SQLWhere"] = strWhere;
            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strWhere, "count(*) as RecordCount") ;
            string strRecordCount = bllb010_AskModifyParents.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {
            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            //sql.addOrderField("tab_ZC_01Product.sort", "asc");//第一排序字段  
            sql.addOrderField("id", "desc");//第二排序字段  
            sql.table = ViewState["SQLTable"].ToString();

            string stroutfields = @"[ID]
      ,[ShopClient_ID]
      ,[OperationCenterID]
      ,[OperationCenterUserID]
      ,[BuyOrderShopUserID]
      ,[BuyOrderUserRealName]
      ,[BuyParentShopUserID]
      ,[BuyGrandParentShopUserID]
      ,[Usertel]
      ,[UserEmail],[FeedbackStatus],[UpdateTime]";


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
