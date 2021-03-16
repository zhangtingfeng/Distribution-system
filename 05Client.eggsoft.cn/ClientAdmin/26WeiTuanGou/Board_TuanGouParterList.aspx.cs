using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._26WeiTuanGou
{
    public partial class Board_TuanGouParterList : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        public static string strPubServicesURL = ConfigurationManager.AppSettings["ServicesURL"];



        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.tab_TuanGou blltab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["searchWhereTuanGouID"] = Request.QueryString["TuanGouID"];
            ViewState["searchWhereTuanGouSearchType"] = Request.QueryString["TuanGouSearchType"];///  AllTeamCount  OR  SuccessTeamCount

            #region 显示商品名称
            string strGetGoodnameWhere = "SELECT Name FROM tab_Goods LEFT OUTER JOIN tab_TuanGou ON tab_Goods.ID = tab_TuanGou.SourceGoodID where tab_TuanGou.ID=" + ViewState["searchWhereTuanGouID"];
            EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
            System.Data.DataTable strGetGoodnameWhereDataTable = BLL_tab_TuanGou.SelectList(strGetGoodnameWhere).Tables[0];
            if (strGetGoodnameWhereDataTable.Rows.Count > 0)
            {
                Literal_GoodName.Text = strGetGoodnameWhereDataTable.Rows[0]["Name"].ToString();
            }
            #endregion

            #region 成功 或者失败
            if (ViewState["searchWhereTuanGouSearchType"].ToString() == "SuccessTeamCount")
            {
                Literal_SuccessOrFail.Text = "--成功拼团";
            }
            else if (ViewState["searchWhereTuanGouSearchType"].ToString() == "AllTeamCount")
            {
                Literal_SuccessOrFail.Text = "--拼团总数";
            }
            #endregion


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
            gvAnnounce.DataSource = blltab_TuanGou.SelectList(strWhere);
            gvAnnounce.DataBind();
        }






        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strTuanGouID = e.Row.Cells[0].Text;
                string strTuanGouIDNumber = e.Row.Cells[1].Text;

                e.Row.Cells[2].Text = "<a href=\"Board_TuanGouParterList_Bypeople.aspx?TuanGouNumber=" + strTuanGouIDNumber + "\">" + e.Row.Cells[2].Text + "</a>";
                if (e.Row.Cells[5].Text == "True")
                {
                    e.Row.Cells[5].Text = "<a onclick=\"ModifyStatus(" + strTuanGouID + "," + strTuanGouIDNumber + ",0)\" href=\"javascript:;\">修改为尚未成功</a>";
                }
                else if (e.Row.Cells[5].Text == "False")
                {
                    e.Row.Cells[5].Text = "<a onclick=\"ModifyStatus(" + strTuanGouID + "," + strTuanGouIDNumber + ",1)\"  href=\"javascript:;\">修改为已成功</a>";
                }
                str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));

                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                string strLink = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strTuanGouID + "&tuangouidnumber=" + strTuanGouIDNumber;
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";
                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLink, str_Pub_upLoadpath, "");
                e.Row.Cells[6].Text = "<a target=\"_blank\" href=\"" + strImageUrl + "\"><img style=\"margin-top:3px;margin-bottom:3px;\" width=\"100\" height=\"100\" id=\"ErWeiMaSao" + strTuanGouIDNumber + "\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" /></a>";
                e.Row.Cells[0].Text = "<a href=\"" + strLink + "\">" + Literal_GoodName.Text + "</a>";///商品名称
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
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

            string strAdd = strNavigateUrl + Server.UrlEncode("WeiTuanGou/WeiTuanGou_Manage.aspx?type=Add");
            Response.Redirect(strAdd);
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 20;

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strTuanGouJointWhere = "";
            if (ViewState["searchWhereTuanGouSearchType"].ToString() == "AllTeamCount")
            {
                strTuanGouJointWhere += "  1=1";
            }
            else if (ViewState["searchWhereTuanGouSearchType"].ToString() == "SuccessTeamCount")
            {
                strTuanGouJointWhere += "  ( IFFinshedCurMemberShip=1)";///SuccessBuyPeopleCount>=HowManyPeople or
            }

            ViewState["SQLTable"] = "(" + strGetPubTable(ViewState["searchWhereTuanGouID"].ToString()) + ") as NewNewTable";
            ViewState["SQLWhere"] = strTuanGouJointWhere;

            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strTuanGouJointWhere, "count(*) as RecordCount");
            string strRecordCount = blltab_TuanGou.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            //  sql.addOrderField("View_WeiKanJia.UpdateTime", "desc");//第二排序字段  
            sql.addOrderField("CreateTime", "desc");//第一排序字段  
            //sql.addOrderField("tab_TuanGou.id", "asc");//第二排序字段  
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



        private string strGetPubTable(String strTuanGouID)
        {
            string strTable = "SELECT tab_TuanGou_Number.ID , ";
            strTable += " tab_TuanGou_Number.TuanGouID , ";
            strTable += " tab_TuanGou_Number.IFFinshedCurMemberShip , ";
            strTable += "  tab_TuanGou_Number.IsDelete , ";
            strTable += "  tab_TuanGou_Number.CreateTime , ";
            strTable += "  V5.SuccessBuyPeopleCount,tab_TuanGou.HowManyPeople,TuanGouIDNumber";
            strTable += " FROM";
            strTable += "      tab_TuanGou_Number LEFT OUTER JOIN( ";
            strTable += "                                         SELECT TuanGouIDNumber , ";
            strTable += "                                                COUNT( *";
            strTable += "                                                     )AS SuccessBuyPeopleCount";
            strTable += "                                           FROM tab_TuanGou_Partner";
            strTable += "                                          GROUP BY TuanGouIDNumber";
            strTable += "                                      )AS V5";
            strTable += "    ON tab_TuanGou_Number.ID";
            strTable += "       = ";
            strTable += "      V5.TuanGouIDNumber";
            strTable += "   LEFT OUTER JOIN tab_TuanGou";
            strTable += "  ON tab_TuanGou_Number.TuanGouID";
            strTable += "      = ";
            strTable += "     tab_TuanGou.ID where tab_TuanGou_Number.TuanGouID=" + strTuanGouID;
            return strTable;
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