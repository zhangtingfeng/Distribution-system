using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._27ZC_Project
{
    public partial class Board_01ZC_ProjectParterList : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_SupportID = "";

        EggsoftWX.BLL.tab_ZC_01Product_PartnerList blltab_ZC_01Product_PartnerList = new EggsoftWX.BLL.tab_ZC_01Product_PartnerList();
        protected void Page_Load(object sender, EventArgs e)
        {
            str_Pub_SupportID = Request.QueryString["ID_Support"].ToString();
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
            gvAnnounce.DataSource = blltab_ZC_01Product_PartnerList.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strID = e.Row.Cells[0].Text;
                //str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                //EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                //EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                //string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                //string strLink = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strID;
          

                //if (e.Row.Cells[1].Text == "True")
                //    e.Row.Cells[1].Text = "上架";
                //else
                //    e.Row.Cells[2].Text = "<span style=\"color: #FF0066;\">下架</span>";

                //string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                //string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";


                //string strModify = strNavigateUrl + Server.UrlEncode("WeiTuanGou/WeiTuanGou_Manage.aspx?type=Modify&ID=" + strID + "&CallBackUrl=Board_WeiTuanGou.aspx*PageIndex=" + ViewState["PageIndex"].ToString());
                //string strDelete = strNavigateUrl + Server.UrlEncode("WeiTuanGou/WeiTuanGou_Manage.aspx?type=Delete&ID=" + strID + "&CallBackUrl=Board_WeiTuanGou.aspx*PageIndex=" + ViewState["PageIndex"].ToString());

                //e.Row.Cells[6].Text = "<a href=\"Board_TuanGouParterList.aspx?TuanGouID=" + strID + "&TuanGouSearchType=AllTeamCount\">" + intGetCount(strID).ToString() + "</a>";
                //e.Row.Cells[6].Text += " / ";
                //e.Row.Cells[6].Text += "<a href=\"Board_TuanGouParterList.aspx?TuanGouID=" + strID + "&TuanGouSearchType=SuccessTeamCount\"><span style=\"color:red;\">" + intGetSuccessCount(strID).ToString() + "</span></a>";

                //e.Row.Cells[7].Text = "<a href=\"" + strModify + "\">修改</a>";
                //e.Row.Cells[8].Text = "<a href=\"" + strDelete + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";


                //string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLink, str_Pub_upLoadpath, "");
                //e.Row.Cells[9].Text = "<a target=\"_blank\" href=\"" + strImageUrl + "\"><img style=\"margin-top:3px;margin-bottom:3px;\" width=\"100\" height=\"100\" id=\"ErWeiMaSao" + strID + "\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" /></a>";
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

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
        //    string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

        //    string strAdd = strNavigateUrl + Server.UrlEncode("WeiTuanGou/WeiTuanGou_Manage.aspx?type=Add");
        //    Response.Redirect(strAdd);
        //}


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 20;


            //string strIfissaled = CheckBox_IfUp.Checked ? "1" : "0";
            //string strWhere = " and (tab_TuanGou.IsSales=" + strIfissaled + ")";


            //if (string.IsNullOrEmpty(TextBox_GoodName.Text) == false)
            //{
            //    strWhere += " and (tab_Goods.Name like '%" + TextBox_GoodName.Text + "%')";
            //}
            //if (string.IsNullOrEmpty(TextBox_TuanGouPrice.Text) == false)
            //{
            //    strWhere += " and (tab_TuanGou.EachPeoplePrice " + DropDownList_TuanGouPrice.Text + TextBox_TuanGouPrice.Text + ")";
            //}
            //if (string.IsNullOrEmpty(TextBox_HowManyBuy.Text) == false)
            //{
            //    strWhere += " and (tab_TuanGou.HowManyPeople " + DropDownList_HowManyBuy.Text + TextBox_HowManyBuy.Text + ")";
            //}

            //if (string.IsNullOrEmpty(TextBox_TuanGouDesc.Text) == false)
            //{
            //    strWhere += " and (tab_TuanGou.TextBox_TuanGouDesc like '%" + TextBox_TuanGouDesc.Text + "%')";
            //}
            //if (string.IsNullOrEmpty(TextBox_GoodShotDesc.Text) == false)
            //{
            //    strWhere += " and (tab_Goods.ShortInfo like '%" + TextBox_GoodShotDesc.Text + "%')";
            //}
            //if (string.IsNullOrEmpty(TextBox_GoodDesc.Text) == false)
            //{
            //    strWhere += " and (tab_Goods.LongInfo like '%" + TextBox_GoodDesc.Text + "%')";
            //}
            string strWhere = " and tab_ZC_01Product_PartnerList.SupportID=" + str_Pub_SupportID;
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            

            string strTuanGouJointWhere = "";
            strTuanGouJointWhere += "  (tab_ZC_01Product_PartnerList.ShopClientID = " + strShopClientID + " and tab_ZC_01Product_PartnerList.IsDeleted=0)  and ([tab_User].ShopClientID = " + strShopClientID + ")";
            ViewState["SQLTable"] = "tab_ZC_01Product_PartnerList LEFT OUTER JOIN  tab_User ON tab_ZC_01Product_PartnerList.UserID = tab_User.ID LEFT OUTER JOIN  tab_Order ON tab_ZC_01Product_PartnerList.OrderID = tab_Order.ID LEFT OUTER JOIN  tab_ZC_01Product_Support_GetBonus ON tab_ZC_01Product_PartnerList.GetBonusID = tab_ZC_01Product_Support_GetBonus.ID";

            ViewState["SQLWhere"] = strTuanGouJointWhere + strWhere;

            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strTuanGouJointWhere, "count(*) as RecordCount") + strWhere;
            string strRecordCount = blltab_ZC_01Product_PartnerList.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            //  sql.addOrderField("View_WeiKanJia.UpdateTime", "desc");//第二排序字段  
            //sql.addOrderField("tab_ZC_01Product_PartnerList.sort", "asc");//第一排序字段  
            sql.addOrderField("tab_ZC_01Product_PartnerList.id", "asc");//第二排序字段  
            sql.table = ViewState["SQLTable"].ToString();

            string stroutfields = "tab_ZC_01Product_PartnerList.PayPrice as PayPrice, tab_ZC_01Product_PartnerList.ID, tab_ZC_01Product_PartnerList.ZCBuysSay, tab_ZC_01Product_PartnerList.PayTime, tab_ZC_01Product_PartnerList.IsCanSendGoods, ";
            stroutfields += "    tab_ZC_01Product_PartnerList.UserID, tab_ZC_01Product_PartnerList.OrderID,  ";
            stroutfields += "    tab_Order.OrderNum, tab_User.NickName, ";
            stroutfields += "   tab_ZC_01Product_Support_GetBonus.BonusContent";

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


        //private string strGetPubTable(String strTuanGouID)
        //{
        //    string strTable = "SELECT tab_TuanGou_Number.ID , ";
        //    strTable += " tab_TuanGou_Number.TuanGouID , ";
        //    strTable += " tab_TuanGou_Number.IFFinshedCurMemberShip , ";
        //    strTable += "  tab_TuanGou_Number.IsDelete , ";
        //    strTable += "  tab_TuanGou_Number.CreateTime , ";
        //    strTable += "  V5.SuccessBuyPeopleCount,tab_TuanGou.HowManyPeople,TuanGouIDNumber";
        //    strTable += " FROM";
        //    strTable += "      tab_TuanGou_Number LEFT OUTER JOIN( ";
        //    strTable += "                                         SELECT TuanGouIDNumber , ";
        //    strTable += "                                                COUNT( *";
        //    strTable += "                                                     )AS SuccessBuyPeopleCount";
        //    strTable += "                                           FROM tab_TuanGou_Partner";
        //    strTable += "                                          GROUP BY TuanGouIDNumber";
        //    strTable += "                                      )AS V5";
        //    strTable += "    ON tab_TuanGou_Number.ID";
        //    strTable += "       = ";
        //    strTable += "      V5.TuanGouIDNumber";
        //    strTable += "   LEFT OUTER JOIN tab_TuanGou";
        //    strTable += "  ON tab_TuanGou_Number.TuanGouID";
        //    strTable += "      = ";
        //    strTable += "     tab_TuanGou.ID where tab_TuanGou_Number.TuanGouID=" + strTuanGouID;
        //    return strTable;
        //}
        ///// <summary>
        ///// 所有团数
        ///// </summary>
        ///// <param name="strTuanGouID"></param>
        ///// <returns></returns>
        //private int intGetCount(String strTuanGouID)
        //{
        //    int intGetCount = 0;
        //    string strAllTeamCount = "0";
        //    string strTable = strGetPubTable(strTuanGouID);
        //    string strCountSQL = "SELECT TuanGouID,count(*) as AllTeamCount from (" + strTable + ") as  newTable group by TuanGouID";
        //    System.Data.DataTable myGetCountTable = blltab_TuanGou.SelectList(strCountSQL).Tables[0];
        //    if (myGetCountTable.Rows.Count > 0)
        //    {
        //        strAllTeamCount = myGetCountTable.Rows[0]["AllTeamCount"].ToString();
        //    }

        //    int.TryParse(strAllTeamCount, out intGetCount);
        //    return intGetCount;
        //}
        /// <summary>
        /// 所有成功团数
        /// </summary>
        /// <param name="strTuanGouID"></param>
        /// <returns></returns>
        //private int intGetSuccessCount(String strTuanGouID)
        //{
        //    int intGetCount = 0;
        //    string strAllTeamCount = "0";
        //    string strTable = strGetPubTable(strTuanGouID);
        //    string strCountSQL = "SELECT TuanGouID,count(*) as sucessTeamCount from (" + strTable + ") as newTable  where ( IFFinshedCurMemberShip=1) group by TuanGouID";//SuccessBuyPeopleCount>=HowManyPeople or
        //    System.Data.DataTable myGetCountTable = blltab_TuanGou.SelectList(strCountSQL).Tables[0];
        //    if (myGetCountTable.Rows.Count > 0)
        //    {
        //        strAllTeamCount = myGetCountTable.Rows[0]["sucessTeamCount"].ToString();
        //    }

        //    int.TryParse(strAllTeamCount, out intGetCount);
        //    return intGetCount;
        //}
    }
}
