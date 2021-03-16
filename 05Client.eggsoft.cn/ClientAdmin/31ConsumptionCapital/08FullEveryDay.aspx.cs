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
    public partial class _08FullEveryDay : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";
        public string strTextBox_StartTime = "";
        public string strTextBox_EndTime = "";

        EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay bllb007_OperationReturnMoneyEveryDay = new EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if(!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_08FullEveryDay")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限

            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if(!IsPostBack)
            {
                strTextBox_StartTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
                strTextBox_EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                Button1_Click_Query(null, null);



                #region 初始化今日参考订单数
                EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                int IntAllCount = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList("select sum(ActiveOrderNum) from b008_OpterationUserActiveReturnMoneyOrderNum where  OrderDetailID is not null and b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID and ActiveOrderNum>0", 1, str_Pub_ShopClientID.toInt32()).Tables[0].Rows[0][0].toInt32();
                Label1.Text = "今日" + DateTime.Now.ToLongDateString() + "参考订单数:" + IntAllCount + "(该数字仅参考使用)";
                #endregion 初始化今日参考订单数
            }
        }

        private void InitGoPage()
        {
            ddlGoPage.Items.Clear();
            for(int i = 1; i <= GetPageCount(); i++)
            {
                ddlGoPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlGoPage.SelectedValue = ViewState["PageIndex"].ToString();
        }

        private void BindAnnounce()
        {
            string strWhere = ViewState["searchWhere"].ToString();
            gvAnnounce.DataSource = bllb007_OperationReturnMoneyEveryDay.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                string strb007_OperationReturnMoneyEveryDayID = e.Row.Cells[0].Text;
                //str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                //EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                //EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                //string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                //string strLink = "http://" + strErJiYuMing + "/addfunction/04zc_project/03zc.html?zcid=" + strZhongChouIDID;

                //if (e.Row.Cells[1].Text == "True")
                //    e.Row.Cells[1].Text = "上架";
                //else
                //    e.Row.Cells[1].Text = "<span style=\"color: #FF0066;\">下架</span>";

                //    string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                //    string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";


                //    string strModify = strNavigateUrl + Server.UrlEncode("27ZC_Project/Zc01_Manage.aspx?type=Modify&ID=" + strZhongChouIDID + "&CallBackUrl=Board_01ZC_Project.aspx*PageIndex=" + ViewState["PageIndex"].ToString());
                //    string strDelete = strNavigateUrl + Server.UrlEncode("27ZC_Project/Zc01_Manage.aspx?type=Delete&ID=" + strZhongChouIDID + "&CallBackUrl=Board_01ZC_Project.aspx*PageIndex=" + ViewState["PageIndex"].ToString());

                //    e.Row.Cells[6].Text = "<a href=\"Board_01ZC_ProjectParterList.aspx?ZhongChouID=" + strZhongChouIDID + "&TuanGouSearchType=AllTeamCount\">" + intGetCount(strZhongChouIDID).ToString() + "</a>";
                //    e.Row.Cells[6].Text += " / ";
                //    e.Row.Cells[6].Text += "<a href=\"Board_01ZC_ProjectParterList.aspx?ZhongChouID=" + strZhongChouIDID + "&TuanGouSearchType=SuccessTeamCount\"><span style=\"color:red;\">" + intGetSuccessCount(strZhongChouIDID).ToString() + "</span></a>";

                //    e.Row.Cells[7].Text = "<a href=\"" + strModify + "\">修改</a>";
                //    e.Row.Cells[8].Text = "<a href=\"" + strDelete + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";


                //    string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLink, str_Pub_upLoadpath, "");
                //    e.Row.Cells[10].Text = "<a target=\"_blank\" href=\"" + strImageUrl + "\"><img style=\"margin-top:3px;margin-bottom:3px;\" width=\"100\" height=\"100\" id=\"ErWeiMaSao" + strZhongChouIDID + "\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" /></a>";
                //}
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
            if(Int32.Parse(ViewState["PageIndex"].ToString()) > 1)
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
            if(Int32.Parse(ViewState["PageIndex"].ToString()) < GetPageCount())
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
            if(GetPageCount() <= 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
                lbtnNext.Enabled = false;
                lbtnLast.Enabled = false;
            }
            else
            {
                if(Int32.Parse(ViewState["PageIndex"].ToString()) <= 1)
                {
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    lbtnNext.Enabled = true;
                    lbtnLast.Enabled = true;
                }
                else
                {
                    if(Int32.Parse(ViewState["PageIndex"].ToString()) >= GetPageCount())
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
            if(String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 20;

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            string strSessionWhere = " ShopClient_ID=" + strShopClientID;


            string strini = Request.QueryString["ini"];
            bool boolIni = false;
            bool.TryParse(strini, out boolIni);
            if(boolIni)
            {

            }
            else
            {
                strTextBox_StartTime = Request.Form["TextBox_StartTime"];
                strTextBox_EndTime = Request.Form["TextBox_EndTime"];
            }

            DateTime my_OrderStartTime = DateTime.Now;
            if(string.IsNullOrEmpty(strTextBox_StartTime) == false)
            {
                my_OrderStartTime = DateTime.ParseExact(strTextBox_StartTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                strSessionWhere += " and CreatTime>='" + my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            Eggsoft.Common.Session.Add("OrderSQLWhere_StartDateTime", strTextBox_StartTime);
            DateTime my_OrderEndTime = DateTime.Now;
            if(string.IsNullOrEmpty(strTextBox_EndTime) == false)
            {
                my_OrderEndTime = DateTime.ParseExact(strTextBox_EndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                strSessionWhere += " and CreatTime<='" + my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            Eggsoft.Common.Session.Add("OrderSQLWhere_EndDateTime", strTextBox_EndTime);



            if(string.IsNullOrEmpty(TextBox_ThisDayMoneyAuto.Text) == false)
            {
                strSessionWhere += " and (b007_OperationReturnMoneyEveryDay.ThisDayMoneyAuto " + DropDownList_ThisDayMoneyAuto.Text + TextBox_ThisDayMoneyAuto.Text.toDecimal() + ")";
            }
            if(string.IsNullOrEmpty(TextBoxThisDayMoneyByBoss.Text) == false)
            {
                strSessionWhere += " and (b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss " + DropDownListThisDayMoneyByBoss.Text + TextBoxThisDayMoneyByBoss.Text.toDecimal() + ")";
            }








            //string strZhongChouJointWhere = "";
            //strZhongChouJointWhere += "  (tab_ZC_01Product.ShopClientID = " + strShopClientID + " and tab_ZC_01Product.IsDeleted=0)  and (tab_Goods.ShopClient_ID = " + strShopClientID + ")";
            ViewState["SQLTable"] = "b007_OperationReturnMoneyEveryDay";

            ViewState["SQLWhere"] = strSessionWhere;
            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strSessionWhere, "count(1) as RecordCount");
            string strRecordCount = bllb007_OperationReturnMoneyEveryDay.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {
            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("id", "desc");//第一排序字段  
            sql.table = ViewState["SQLTable"].ToString();

            string stroutfields = @"[id],[ShopClient_ID]
      ,[ThisDayReturnActual]
      ,[ThisDayMoneyByBoss]
      ,[ThisDayMoneyAuto]
      ,[ThisDay],[ThisDayAllActiveOrder],[EveryOrderGet]
      ,[CreateBy]";


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


        protected void btnbtnOperationCenter_Click(object sender, EventArgs e)
        {
            Response.Redirect("02OperationCenter.aspx");
        }

        protected void btnOperationCenterGoodAndReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("04OperationGoods.aspx");
        }
    }
}
