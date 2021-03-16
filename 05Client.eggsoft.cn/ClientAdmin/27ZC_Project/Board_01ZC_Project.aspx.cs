using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._27ZC_Project
{
    public partial class Board_01ZC_Project : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board_01ZC_Project")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开的权限

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
                string strZhongChouIDID = e.Row.Cells[0].Text;
                str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                string strLink = "https://" + strErJiYuMing + "/addfunction/04zc_project/03zc.html?zcid=" + strZhongChouIDID;

                if (e.Row.Cells[1].Text == "True")
                    e.Row.Cells[1].Text = "上架";
                else
                    e.Row.Cells[1].Text = "<span style=\"color: #FF0066;\">下架</span>";

                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";


                string strModify = strNavigateUrl + Server.UrlEncode("27ZC_Project/Zc01_Manage.aspx?type=Modify&ID=" + strZhongChouIDID + "&CallBackUrl=Board_01ZC_Project.aspx*PageIndex=" + ViewState["PageIndex"].ToString());
                string strDelete = strNavigateUrl + Server.UrlEncode("27ZC_Project/Zc01_Manage.aspx?type=Delete&ID=" + strZhongChouIDID + "&CallBackUrl=Board_01ZC_Project.aspx*PageIndex=" + ViewState["PageIndex"].ToString());

                e.Row.Cells[6].Text = "<a href=\"Board_01ZC_ProjectParterList.aspx?ZhongChouID=" + strZhongChouIDID + "&TuanGouSearchType=AllTeamCount\">" + intGetCount(strZhongChouIDID).ToString() + "</a>";
                e.Row.Cells[6].Text += " / ";
                e.Row.Cells[6].Text += "<a href=\"Board_01ZC_ProjectParterList.aspx?ZhongChouID=" + strZhongChouIDID + "&TuanGouSearchType=SuccessTeamCount\"><span style=\"color:red;\">" + intGetSuccessCount(strZhongChouIDID).ToString() + "</span></a>";

                e.Row.Cells[7].Text = "<a href=\"" + strModify + "\">修改</a>";
                e.Row.Cells[8].Text = "<a href=\"" + strDelete + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";


                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLink, str_Pub_upLoadpath, "");
                e.Row.Cells[10].Text = "<a target=\"_blank\" href=\"" + strImageUrl + "\"><img style=\"margin-top:3px;margin-bottom:3px;\" width=\"100\" height=\"100\" id=\"ErWeiMaSao" + strZhongChouIDID + "\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" /></a>";
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

            string strAdd = strNavigateUrl + Server.UrlEncode("27ZC_Project/Zc01_Manage.aspx?type=Add");
            Response.Redirect(strAdd);
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 5;


            string strIfissaled = CheckBox_IfUp.Checked ? "1" : "0";
            string strWhere = " and (tab_ZC_01Product.IsSales=" + strIfissaled + ")";


            if (string.IsNullOrEmpty(TextBox_GoodName.Text) == false)
            {
                strWhere += " and (tab_Goods.Name like '%" + TextBox_GoodName.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox_DestinationPrice.Text) == false)
            {
                strWhere += " and (tab_ZC_01Product.DestinationPrice " + DropDownList_DestinationPrice.Text + TextBox_DestinationPrice.Text + ")";
            }


            if (string.IsNullOrEmpty(TextBox_ZCReason.Text) == false)
            {
                strWhere += " and (tab_ZC_01Product.ZCReason like '%" + TextBox_ZCReason.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox_ZCPromiseAndReturn.Text) == false)
            {
                strWhere += " and (tab_ZC_01Product_Support.ZCPromiseAndReturn like '%" + TextBox_ZCPromiseAndReturn.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox_ZCDescribe.Text) == false)
            {
                strWhere += " and (tab_ZC_01Product.ZCDescribe like '%" + TextBox_ZCDescribe.Text + "%')";
            }

            if (string.IsNullOrEmpty(TextBox_GoodDesc.Text) == false)
            {
                strWhere += " and (tab_Goods.ShortInfo like '%" + TextBox_GoodDesc.Text + "%' or tab_Goods.LongInfo like '%" + TextBox_GoodDesc.Text + "%')";
            }


            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();





            string strZhongChouJointWhere = "";
            strZhongChouJointWhere += "  (tab_ZC_01Product.ShopClientID = " + strShopClientID + " and tab_ZC_01Product.IsDeleted=0)  and (tab_Goods.ShopClient_ID = " + strShopClientID + ")";
            ViewState["SQLTable"] = "tab_ZC_01Product LEFT OUTER JOIN  tab_Goods ON tab_ZC_01Product.ShopClientID = tab_Goods.ShopClient_ID AND tab_ZC_01Product.SourceGoodID = tab_Goods.ID";

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
            sql.addOrderField("tab_ZC_01Product.sort", "asc");//第一排序字段  
            sql.addOrderField("tab_ZC_01Product.id", "asc");//第二排序字段  
            sql.table = ViewState["SQLTable"].ToString();

            string stroutfields = "tab_Goods.Name as GoodName, tab_Goods.ShortInfo, tab_Goods.LongInfo, tab_Goods.Icon, tab_ZC_01Product.ID, ";
            stroutfields += "    tab_ZC_01Product.DestinationPrice, tab_ZC_01Product.WhenEndAllGroup, tab_ZC_01Product.ZCDescribe, ";
            stroutfields += "    tab_ZC_01Product.ZCReason, tab_ZC_01Product.ZCPromiseAndReturn, tab_ZC_01Product.SourceGoodID, ";
            stroutfields += "   tab_ZC_01Product.ShopClientID, tab_ZC_01Product.IsSales, tab_ZC_01Product.Sort,  ";
            stroutfields += "   tab_ZC_01Product.IsDeleted, tab_ZC_01Product.CreateTime, ";
            stroutfields += "   tab_ZC_01Product.UpdateTime";
            

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
        /// <summary>
        /// 所有团数
        /// </summary>
        /// <param name="strTuanGouID"></param>
        /// <returns></returns>
        private int intGetCount(String strTuanGouID)
        {
            int intGetCount = 0;
            //string strAllTeamCount = "0";
            //string strTable = strGetPubTable(strTuanGouID);
            //string strCountSQL = "SELECT TuanGouID,count(*) as AllTeamCount from (" + strTable + ") as  newTable group by TuanGouID";
            //System.Data.DataTable myGetCountTable = blltab_TuanGou.SelectList(strCountSQL).Tables[0];
            //if (myGetCountTable.Rows.Count > 0)
            //{
            //    strAllTeamCount = myGetCountTable.Rows[0]["AllTeamCount"].ToString();
            //}

            //int.TryParse(strAllTeamCount, out intGetCount);
            return intGetCount;
        }
        /// <summary>
        /// 所有成功团数
        /// </summary>
        /// <param name="strTuanGouID"></param>
        /// <returns></returns>
        private int intGetSuccessCount(String strTuanGouID)
        {
            int intGetCount = 0;
            //string strAllTeamCount = "0";
            //string strTable = strGetPubTable(strTuanGouID);
            //string strCountSQL = "SELECT TuanGouID,count(*) as sucessTeamCount from (" + strTable + ") as newTable  where ( IFFinshedCurMemberShip=1) group by TuanGouID";//SuccessBuyPeopleCount>=HowManyPeople or
            //System.Data.DataTable myGetCountTable = blltab_TuanGou.SelectList(strCountSQL).Tables[0];
            //if (myGetCountTable.Rows.Count > 0)
            //{
            //    strAllTeamCount = myGetCountTable.Rows[0]["sucessTeamCount"].ToString();
            //}

            //int.TryParse(strAllTeamCount, out intGetCount);
            return intGetCount;
        }
    }
}
