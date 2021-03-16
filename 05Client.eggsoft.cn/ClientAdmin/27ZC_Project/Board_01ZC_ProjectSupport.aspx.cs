using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._27ZC_Project
{
    public partial class Board_01ZC_ProjectSupport : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";
        string str_ZC_01ProductID = "";
        EggsoftWX.BLL.tab_ZC_01Product_Support blltab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
        protected void Page_Load(object sender, EventArgs e)
        {
            str_ZC_01ProductID = Request.QueryString["ZCID"].ToString();
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";

            if (!IsPostBack)
            {
                Button1_Click_Query(null, null);
            }

            #region 显示商品名称
            EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
            EggsoftWX.Model.tab_ZC_01Product Modeltab_ZC_01Product = blltab_ZC_01Product.GetModel(Int32.Parse(str_ZC_01ProductID));
            ;
            EggsoftWX.BLL.tab_Goods blltab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Modeltab_Goods = blltab_Goods.GetModel(Convert.ToInt32(Modeltab_ZC_01Product.SourceGoodID));

            LabelGoodName.Text = Modeltab_Goods.Name;
            #endregion
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
            gvAnnounce.DataSource = blltab_ZC_01Product_Support.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strtab_ZC_01Product_SupportID = e.Row.Cells[0].Text;

                if (e.Row.Cells[2].Text == "True")
                    e.Row.Cells[2].Text = "上架";
                else
                    e.Row.Cells[2].Text = "<span style=\"color: #FF0066;\">下架</span>";


                if (e.Row.Cells[4].Text == "0")
                    e.Row.Cells[4].Text = "支付即发货";
                else if (e.Row.Cells[4].Text == "1")
                    e.Row.Cells[4].Text = "双色球中奖发货";
                else if (e.Row.Cells[4].Text == "2")
                    e.Row.Cells[4].Text = "福彩3D中奖发货";
                else if (e.Row.Cells[4].Text == "3")
                    e.Row.Cells[4].Text = "无偿支持,不需回报,无需发货";
                else if (e.Row.Cells[4].Text == "4")
                    e.Row.Cells[4].Text = "股权类众筹,后期回报,无需发货";

                if (e.Row.Cells[6].Text == "0")
                    e.Row.Cells[6].Text = "无";
               

                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";


                string strModify = strNavigateUrl + Server.UrlEncode("27ZC_Project/ZC_01Product_Support.aspx?type=Modify&ID=" + strtab_ZC_01Product_SupportID + "&CallBackUrl=Board_01ZC_ProjectSupport.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "^ZCID=" + str_ZC_01ProductID);
                string strDelete = strNavigateUrl + Server.UrlEncode("27ZC_Project/ZC_01Product_Support.aspx?type=Delete&ID=" + strtab_ZC_01Product_SupportID + "&CallBackUrl=Board_01ZC_ProjectSupport.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "^ZCID=" + str_ZC_01ProductID);


                e.Row.Cells[9].Text = "<a href=\"" + strModify + "\">修改</a>";
                e.Row.Cells[10].Text = "<a href=\"" + strDelete + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";


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
            str_ZC_01ProductID = Request.QueryString["ZCID"].ToString();

            string strAdd = strNavigateUrl + Server.UrlEncode("27ZC_Project/ZC_01Product_Support.aspx?type=Add&ZCID=" + str_ZC_01ProductID);
            Response.Redirect(strAdd);
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 20;


            string strIfissaled = CheckBox_IfUp.Checked ? "1" : "0";
            string strWhere = " and (tab_ZC_01Product_Support.IsSales=" + strIfissaled + ")";


            if (string.IsNullOrEmpty(TextBox_Name.Text) == false)
            {
                strWhere += " and (v3.Name like '%" + TextBox_Name.Text + "%')";
            }


            if (string.IsNullOrEmpty(TextBox_NcikName.Text) == false)
            {
                strWhere += " and (v3.NickNameAll like '%" + TextBox_NcikName.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox_SalesPrice.Text) == false)
            {
                strWhere += " and (tab_ZC_01Product_Support.SalesPrice " + DropDownList_SalesPrice.Text + TextBox_SalesPrice.Text + ")";
            }


            if (string.IsNullOrEmpty(TextBox_SalesPricePromiseAndReturn.Text) == false)
            {
                strWhere += " and (tab_ZC_01Product.SalesPricePromiseAndReturn like '%" + TextBox_SalesPricePromiseAndReturn.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBox1PartnerAllCount.Text) == false)
            {
                strWhere += " and (SalesAllMoneytab.AllParterCount " + DropDownList1PartnerAllCount.Text + TextBox1PartnerAllCount.Text + ")";
            }
            if (string.IsNullOrEmpty(TextBox2ParterMoney.Text) == false)
            {
                strWhere += " and (SalesAllMoneytab.SalesAllMoney " + DropDownList2ParterMoney.Text + TextBox2ParterMoney.Text + ")";
            }

            if (string.IsNullOrEmpty(TextBox2ParterMoney.Text) == false)
            {
                strWhere += " and (tab_ZC_01Product.SalesPricePromiseAndReturn like '%" + TextBox_SalesPricePromiseAndReturn.Text + "%')";
            }

            if (RadioButtonListSupportWay.SelectedValue.ToString() != "-1")
            {
                strWhere += " and (tab_ZC_01Product_Support.SupportWay= " + RadioButtonListSupportWay.SelectedValue.ToString() + ")";
            }


            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();





            string strZhongChouJointWhere = "";
            strZhongChouJointWhere += "  (tab_ZC_01Product_Support.ZC_01ProductID = " + str_ZC_01ProductID + " and tab_ZC_01Product_Support.ShopClientID = " + strShopClientID + " and tab_ZC_01Product_Support.IsDeleted=0)  ";

            string strTable = strGetPubTable();
            ViewState["SQLTable"] = strTable;
            ViewState["SQLWhere"] = strZhongChouJointWhere + strWhere;
            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strZhongChouJointWhere, "count(*) as RecordCount") + strWhere;
            string strRecordCount = blltab_ZC_01Product_Support.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {
            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("tab_ZC_01Product_Support.sort", "asc");//第一排序字段  
            sql.addOrderField("tab_ZC_01Product_Support.id", "asc");//第二排序字段  
            sql.table = ViewState["SQLTable"].ToString();

            string stroutfields = "tab_ZC_01Product_Support.ZC_01ProductID , ";
            stroutfields += "  tab_ZC_01Product_Support.SalesPrice , ";
            stroutfields += "  tab_ZC_01Product_Support.IsSales , ";
            stroutfields += "  tab_ZC_01Product_Support.SupportWay , ";
            stroutfields += "  tab_ZC_01Product_Support.AgentPrice , ";
            stroutfields += "  tab_ZC_01Product_Support.Sort , ";
            stroutfields += "  tab_ZC_01Product_Support.ID , ";
            stroutfields += "  tab_ZC_01Product_Support.SalesLimit , ";
            stroutfields += "  tab_ZC_01Product_Support.Name, ";
            stroutfields += "  tab_ZC_01Product_Support.SalesPricePromiseAndReturn , ";
            stroutfields += "  tab_ZC_01Product_Support.SourceGoodID , ";
            stroutfields += "  tab_ZC_01Product_Support.ShopClientID , ";
            stroutfields += "  v3.NickNameAll,SalesAllMoneytab.SalesAllMoney ,SalesAllMoneytab.AllParterCount";


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


        private string strGetPubTable()
        {
            string strTable = "(tab_ZC_01Product_Support ";
            //strTable += "  ON tab_ZC_01Product_Support.ZC_01ProductID ";
            //strTable += "       =  ";
            //strTable += "       tab_ZC_01Product_Support_GetBonus.ZC_01ProductID ";
            //strTable += "   AND tab_ZC_01Product_Support.ID ";
            //strTable += "       =  ";
            //strTable += "       tab_ZC_01Product_Support_GetBonus.SupportID ";
            strTable += "                             LEFT OUTER JOIN(  ";
            strTable += "                                             SELECT SUM( PayPrice ";
            strTable += "                                                      )AS SalesAllMoney , count(*) as AllParterCount , ";
            strTable += "                                                   SupportID ";
            strTable += "                                              FROM tab_ZC_01Product_PartnerList ";
            strTable += "                                              WHERE Ispay = 1 and ZC_01ProductID=" + str_ZC_01ProductID;
            strTable += "                                               AND IsDeleted = 0 and shopclientID=" + str_Pub_ShopClientID;
            strTable += "                                             GROUP BY SupportID ";
            strTable += "                                         )AS SalesAllMoneytab ";
            strTable += "   ON SalesAllMoneytab.SupportID ";
            strTable += "      =  ";
            strTable += "     tab_ZC_01Product_Support.ID ";
            strTable += "                           LEFT OUTER JOIN(  ";
            strTable += "  select SupportID, [NickNameAll]=stuff((select ','+[NickName] from [Vroduct_PartnerListNickname] t where SupportID=[Vroduct_PartnerListNickname].SupportID for xml path('')), 1, 1, '')  ";
            strTable += "  from Vroduct_PartnerListNickname   ";
            strTable += " group by Vroduct_PartnerListNickname.SupportID  ";
            strTable += "                                    )AS V3 ";
            strTable += "   ON tab_ZC_01Product_Support.ID ";
            strTable += "     =  ";
            strTable += "     V3.SupportID)";

            return strTable;
        }

    }
}
