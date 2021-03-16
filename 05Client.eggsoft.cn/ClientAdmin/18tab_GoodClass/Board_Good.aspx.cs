using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass
{
    public partial class Board_Good : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_Good")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";

            if (!IsPostBack)
            {

                Button1_Click_Query(null, null);


                //if (Int32.Parse(ViewState["RecordCount"].ToString())<2)    btnAdd.Visible=true;

            }
        }


        private void InitGoPageVisiableGoodClass()
        {

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
            gvAnnounce.DataSource = bll.SelectList(strWhere);
            gvAnnounce.DataBind();
        }



        private string getClassName(string strID)
        {
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Model = new EggsoftWX.Model.tab_Goods();
            Model = bll.GetModel(Int32.Parse(strID));

            int Class1_ID = Convert.ToInt32(Model.Class1_ID);
            int Class2_ID = Convert.ToInt32(Model.Class2_ID);
            int Class3_ID = Convert.ToInt32(Model.Class3_ID);

            int ShopClient_ID = Convert.ToInt32(Model.ShopClient_ID);

            string strClass1Name = "";
            string strClass2Name = "";
            string strClass3Name = "";

            if (new EggsoftWX.BLL.tab_Class1().Exists("id=" + Class1_ID.ToString()))
            {
                strClass1Name = new EggsoftWX.BLL.tab_Class1().GetList("ClassName", "id=" + Class1_ID.ToString()).Tables[0].Rows[0]["ClassName"].ToString();
            }
            if ((Class2_ID > 0) && (new EggsoftWX.BLL.tab_Class2().Exists("id=" + Class2_ID.ToString())))
            {
                strClass2Name = new EggsoftWX.BLL.tab_Class2().GetList("ClassName", "id=" + Class2_ID.ToString()).Tables[0].Rows[0]["ClassName"].ToString();
            }

            if ((Class3_ID > 0) && (new EggsoftWX.BLL.tab_Class3().Exists("id=" + Class3_ID.ToString())))
            {
                strClass3Name = new EggsoftWX.BLL.tab_Class3().GetList("ClassName", "id=" + Class3_ID.ToString()).Tables[0].Rows[0]["ClassName"].ToString();
            }
            string strClassName = strClass1Name;
            if (strClass2Name != "") strClassName += "\\" + strClass2Name;
            if (strClass3Name != "") strClassName += "\\" + strClass3Name;
            return strClassName;

        }


        private string getDisCountPrice(string strID)
        {
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Model = new EggsoftWX.Model.tab_Goods();
            Model = bll.GetModel(Int32.Parse(strID));

            Decimal PromotePrice = Convert.ToDecimal(Model.PromotePrice);
            string strPromotePrice = "";

            strPromotePrice = PromotePrice.ToString("###0.00");

            return strPromotePrice;

        }

        private string getPrice(string strID)
        {
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Model = new EggsoftWX.Model.tab_Goods();
            Model = bll.GetModel(Int32.Parse(strID));

            Decimal Price = Convert.ToDecimal(Model.Price);
            string strPrice = "";

            strPrice = Price.ToString("###0.00");

            return strPrice;

        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[0].Text;
                e.Row.Cells[0].Text = Eggsoft_Public_CL.GoodP.GetGood_Num_ID_From_Good_ID(Convert.ToInt32(strID));
                e.Row.Cells[2].Text = getClassName(strID);

                e.Row.Cells[4].Text = "¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(e.Row.Cells[4].Text));



                if (e.Row.Cells[6].Text == "True")
                    e.Row.Cells[6].Text = "上架";
                else
                    e.Row.Cells[6].Text = "<span style=\"color: #FF0066;\">下架</span>";



                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";


                string strModify = strNavigateUrl + Server.UrlEncode("Good_Manage.aspx?type=Modify&ID=" + strID + "&CallBackUrl=Board_Good.aspx*PageIndex=" + ViewState["PageIndex"].ToString());
                string strDelete = strNavigateUrl + Server.UrlEncode("Good_Manage.aspx?type=Delete&ID=" + strID + "&CallBackUrl=Board_Good.aspx*PageIndex=" + ViewState["PageIndex"].ToString());

                e.Row.Cells[7].Text = "<a href=\"" + strModify + "\">修改</a>";
                e.Row.Cells[8].Text = "<a href=\"" + strDelete + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

                str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值

                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage("https://" + strErJiYuMing + "/product-" + strID + ".aspx", str_Pub_upLoadpath, "");
                e.Row.Cells[9].Text = "<a target=\"_blank\" href=\"" + strImageUrl + "\"><img style=\"margin-top:3px;margin-bottom:3px;\" width=\"100\" height=\"100\" id=\"ErWeiMaSao" + strID + "\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" /></a>";

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

            string strAdd = strNavigateUrl + Server.UrlEncode("Good_Manage.aspx?type=Add");
            Response.Redirect(strAdd);
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 5;


            string strIfissaled = CheckBox_IfUp.Checked ? "1" : "0";
            string strWhere = " and issaled=" + strIfissaled;


            if (string.IsNullOrEmpty(TextBox_GoodName.Text) == false)
            {
                strWhere += " and Name like '%" + TextBox_GoodName.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_LongInfo.Text) == false)
            {
                strWhere += " and LongInfo like '%" + TextBox_LongInfo.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_ShortInfo.Text) == false)
            {
                strWhere += " and ShortInfo like '%" + TextBox_ShortInfo.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_Price.Text) == false)
            {
                strWhere += " and price " + DropDownList_Price.Text + TextBox_Price.Text;
            }
            if (string.IsNullOrEmpty(TextBox_Price.Text) == false)
            {
                strWhere += " and price " + DropDownList_Price.Text + TextBox_Price.Text;
            }
            if (string.IsNullOrEmpty(TextBox_PromotePrice.Text) == false)
            {
                strWhere += " and PromotePrice " + DropDownList_PromotePrice.Text + TextBox_PromotePrice.Text;
            }
            if (string.IsNullOrEmpty(TextBox_AgentMoney.Text) == false)
            {
                strWhere += " and AgentPercent " + DropDownList_AgentPrice.Text + TextBox_AgentMoney.Text;
            }
            if (string.IsNullOrEmpty(TextBox_ShopMaxMoney.Text) == false)
            {
                strWhere += " and Shopping_Vouchers_Percent " + DropDownList_GouWuQuan.Text + TextBox_ShopMaxMoney.Text;
            }
            if (string.IsNullOrEmpty(TextBox_KuCunCount.Text) == false)
            {
                strWhere += " and KuCunCount " + DropDownList_KuCunCount.Text + TextBox_KuCunCount.Text;
            }

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strClass1_ID = Request.QueryString["Class1_ID"];
            if (!String.IsNullOrEmpty(strClass1_ID)) strWhere += " and Class1_ID=" + strClass1_ID;
            string strClass2_ID = Request.QueryString["Class2_ID"];
            if (!String.IsNullOrEmpty(strClass2_ID)) strWhere += " and Class2_ID=" + strClass2_ID;
            string strClass3_ID = Request.QueryString["Class3_ID"];
            if (!String.IsNullOrEmpty(strClass3_ID)) strWhere += " and Class3_ID=" + strClass3_ID;

            ViewState["SQLWhere"] = strWhere;
            ViewState["RecordCount"] = bll.ExistsCount("ShopClient_ID=" + str_Pub_ShopClientID + " and IsDeleted=0" + strWhere);
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("tab_Goods.sort", "asc");//第一排序字段  
            sql.addOrderField("tab_Goods.UpdateTime", "desc");//第二排序字段  
            sql.addOrderField("tab_Goods.id", "desc");//第二排序字段  
            sql.table = "tab_Goods";
            sql.outfields = "ID,PromotePrice,IS_Admin_check,Class1_ID,Class2_ID,Class3_ID,Good_Class,isSaled,Name,Icon,Sort,UpdateTime,Webuy8_DistributionMoney_Value";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = "ShopClient_ID=" + str_Pub_ShopClientID + "  and isdeleted=0 " + ViewState["SQLWhere"];
            sql.where = strwhere;
            string strSql = sql.getSQL(bll.ExistsCount(strwhere));

            ViewState["searchWhere"] = strSql;// " and ShopClientID=" + strShopClientID + strWhere;


            BindAnnounce();
            ShowState();

        }
    }
}