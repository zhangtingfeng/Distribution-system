using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._26WeiTuanGou
{
    public partial class Board_TuanGouParterList_Bypeople : Eggsoft.Common.DotAdminPage_ClientAdmin
    {



        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.tab_TuanGou blltab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["searchWhereTuanGouNumber"] = Request.QueryString["TuanGouNumber"];

            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

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
            gvAnnounce.DataSource = blltab_TuanGou.SelectList(strWhere);
            gvAnnounce.DataBind();
        }






        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


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




        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 20;

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strTuanGouJointWhere = "SELECT   tab_TuanGou_Partner.UserID,tab_TuanGou_Partner.CreateTime,tab_TuanGou_Partner.OrderID,tab_User.ShopUserID,tab_User.Nickname,tab_User.ContactPhone";
            strTuanGouJointWhere += " FROM      tab_TuanGou_Partner LEFT OUTER JOIN";
            strTuanGouJointWhere += "                tab_User ON tab_TuanGou_Partner.UserID = tab_User.ID";
            strTuanGouJointWhere += " WHERE   (tab_TuanGou_Partner.TuanGouIDNumber = " + ViewState["searchWhereTuanGouNumber"] + ")";


            ViewState["SQLTable"] = "(" + strTuanGouJointWhere + ") as NewNewTable";
            ViewState["SQLWhere"] = " 1=1";

            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + ViewState["SQLWhere"], "count(*) as RecordCount");
            string strRecordCount = blltab_TuanGou.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("CreateTime", "desc");//第一排序字段  
            sql.table = ViewState["SQLTable"].ToString();

            string stroutfields = "*";

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




//SELECT tab_TuanGou_Number.ID , 
//       tab_TuanGou_Number.TuanGouID , 
//       tab_TuanGou_Number.IFFinshedCurMemberShip , 
//       tab_TuanGou_Number.IsDelete , 
//       tab_TuanGou_Number.CreateTime , 
//       V5.SuccessBuyPeopleCount,tab_TuanGou.HowManyPeople
//  FROM
//       tab_TuanGou_Number LEFT OUTER JOIN( 
//                                           SELECT TuanGouIDNumber , 
//                                                  COUNT( *
//                                                       )AS SuccessBuyPeopleCount
//                                             FROM tab_TuanGou_Partner
//                                             GROUP BY TuanGouIDNumber
//                                         )AS V5
//       ON tab_TuanGou_Number.ID
//          = 
//          V5.TuanGouIDNumber
//        LEFT OUTER JOIN tab_TuanGou
//       ON tab_TuanGou_Number.TuanGouID
//          = 
//          tab_TuanGou.ID;




//SELECT   TOP (100) PERCENT tab_TuanGou_Partner.UserID, tab_TuanGou_Partner.ShopClientID, 
//                tab_TuanGou_Partner.BuyPrice, tab_TuanGou_Partner.TuanGouID, tab_User.NickName, 
//                tab_User.HeadImageUrl, tab_TuanGou_Partner.CreateTime, 
//                tab_TuanGou_Partner.TuanGouIDNumber
//FROM      tab_TuanGou_Partner LEFT OUTER JOIN
//                tab_User ON tab_TuanGou_Partner.ShopClientID = tab_User.ShopClientID AND 
//                tab_TuanGou_Partner.UserID = tab_User.ID
//WHERE   (tab_TuanGou_Partner.TuanGouIDNumber = 9)
//ORDER BY tab_TuanGou_Partner.CreateTime