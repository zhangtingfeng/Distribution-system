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
    public partial class __01OrderDetailEveryDay : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";
        public string strTextBox_StartTime = "";
        public string strTextBox_EndTime = "";

        EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay bllb007_OperationReturnMoneyEveryDay = new EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay();
        EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开订单统计的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_UserOrderDetailEveryDay")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开订单统计的权限

            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (!IsPostBack)
            {
                strTextBox_StartTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
                strTextBox_EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



                Button1_Click_Query(null, null);
            }
            else
            {
                strTextBox_StartTime = Eggsoft.Common.Session.Read("OrderSQLWhere_StartDateTime");
                strTextBox_EndTime = Eggsoft.Common.Session.Read("OrderSQLWhere_EndDateTime");

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
            gvAnnounce.DataSource = bllb007_OperationReturnMoneyEveryDay.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strb007_OperationReturnMoneyEveryDayID = e.Row.Cells[0].Text;

                if (string.IsNullOrEmpty(e.Row.Cells[2].Text.toString().Replace("&nbsp;", ""))) e.Row.Cells[2].Text = "未支付";
                #region 是否转化
                //if (e.Row.Cells[15].Text.toBoolean() == false)
                //{
                //    string strDDD = "<span disabled=\"disabled\"><input  type=\"checkbox\"  disabled=\"disabled\"></span>";
                //    e.Row.Cells[13].Text = strDDD;// + "<br /><a href=\"13OperationCenter_OrderManage.aspx?Type=modify&OrderDatailID=" + e.Row.Cells[0].Text + "\">干预订单</a>";
                //}
                //else
                //{
                //    string strDDD = "<span disabled=\"disabled\"><input  type=\"checkbox\"  checked=\"checked\" disabled=\"disabled\"></span>";
                //    e.Row.Cells[13].Text = strDDD;
                //}
                #endregion 是否转化
                //string strOrderDetailID = e.Row.Cells[0].Text;
                //if (!string.IsNullOrEmpty(strOrderDetailID))
                //{
                //    string strSQL = @"SELECT   SUM(ConsumeOrRechargeWealth) AS sumConsumeOrRechargeWealth
                //     FROM b006_TotalWealth_OperationUser
                //     WHERE (Bool_ConsumeOrRecharge = 0) and OrderDetailID=" + strOrderDetailID;


                //    string strDecimalAllMoney = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strSQL).toDecimal().toString();
                //    string strLink = string.Format("20Order_FromWealthToMoney.aspx?userid={0}&OrderDetailID={1}", e.Row.Cells[19].Text, strOrderDetailID);
                //    e.Row.Cells[17].Text = "<a href='" + strLink + "'>" + strDecimalAllMoney + "</a>";
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



            string strSessionWhere = " 1=1";


            string strini = Request.QueryString["ini"];
            bool boolIni = false;
            bool.TryParse(strini, out boolIni);
            if (boolIni)
            {

            }
            else
            {
                if (String.IsNullOrEmpty(Request.Form["TextBox_OrderStartTime"].toString()) == false)
                {
                    strTextBox_StartTime = Request.Form["TextBox_OrderStartTime"].toString();
                }
                if (String.IsNullOrEmpty(Request.Form["TextBox_OrderEndTime"].toString()) == false)
                {
                    strTextBox_EndTime = Request.Form["TextBox_OrderEndTime"].toString();
                }
            }
            string strInnerWhere = " ";////内部表查询使用
            string strQureryTimeRadio = QureryTimeRadioButtonList.SelectedValue;
            string strItemType = "PayDateTime";
            if (strQureryTimeRadio == "0")
            {
                strItemType = "tab_OrderCreatTime";
            }
            else
            {
                strItemType = "PayDateTime";
            }
            DateTime my_OrderStartTime = DateTime.Now;
            if (string.IsNullOrEmpty(strTextBox_StartTime) == false)
            {
                my_OrderStartTime = DateTime.ParseExact(strTextBox_StartTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);


                strInnerWhere += " and " + strItemType + ">='" + my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            Eggsoft.Common.Session.Add("OrderSQLWhere_StartDateTime", strTextBox_StartTime);
            DateTime my_OrderEndTime = DateTime.Now;
            if (string.IsNullOrEmpty(strTextBox_EndTime) == false)
            {
                my_OrderEndTime = DateTime.ParseExact(strTextBox_EndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                strInnerWhere += " and " + strItemType + "<='" + my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            Eggsoft.Common.Session.Add("OrderSQLWhere_EndDateTime", strTextBox_EndTime);

            strSessionWhere += strInnerWhere;

            TimeSpan ts = my_OrderEndTime.Subtract(my_OrderStartTime).Duration();
            if ((ts.TotalDays) > 92)
            {
                Eggsoft.Common.JsUtil.ShowMsg("时间跨度不能超过3个月！");
                return;
            }
            if (string.IsNullOrEmpty(TextBox_UserName.Text) == false)
            {
                strSessionWhere += " and (nickname like '%" + TextBox_UserName.Text + "%')";
            }
            if (string.IsNullOrEmpty(TextBoxUserID.Text) == false)
            {
                strSessionWhere += " and (ShopUserID =" + TextBoxUserID.Text + ")";
            }

            #region 搜索商品 输入的编号 必须去掉头
            string strHeadNumber = Eggsoft.Common.StringNum.Add000000Num(strShopClientID.toInt32(), 5);
            string sTextBox_GoodID = TextBox_GoodID.Text.toString().Trim();
            if ((String.IsNullOrEmpty(sTextBox_GoodID) == false) && (sTextBox_GoodID.Length > 5) && (sTextBox_GoodID.Substring(0, 5) == strHeadNumber))
            {
                sTextBox_GoodID = sTextBox_GoodID.Substring(5, sTextBox_GoodID.Length - 5);
                Int32 intGoodID = sTextBox_GoodID.toInt32();
                strSessionWhere += " and (GoodID=" + intGoodID + ")";
            }
            #endregion 搜索商品

            #region 搜索团队ID
            if (TextTeamIDName.Text.toInt32().toString() == TextTeamIDName.Text)
            {
                //string strTrueTeamID = "";
                //string strSQL = "select ID from tab_ShopClient_Agent_ where ShopClientID=@ShopClientID and ShopTeamID=@ShopTeamID";
                //System.Data.DataTable Data_DataTable = bllb007_OperationReturnMoneyEveryDay.SelectList(strSQL, strShopClientID, TextTeamIDName.Text).Tables[0];
                //if (Data_DataTable.Rows.Count > 0)
                //{
                //    strTrueTeamID = Data_DataTable.Rows[0]["ID"].ToString();
                strSessionWhere += " and (ShopTeamID=" + TextTeamIDName.Text + ")";
                //}
            }
            #endregion 搜索团队ID





            string strTeamID = "(";
            strTeamID += @"SELECT tab_Orderdetails.ID, tab_Orderdetails.OrderID, 
      tab_Orderdetails.GoodID, tab_Orderdetails.Discount, 
      tab_Orderdetails.GoodName, tab_Orderdetails.GoodPrice, 
      tab_Orderdetails.CreatDateTime, tab_Orderdetails.OrderCount, 
      tab_Orderdetails.Pinglun, tab_Orderdetails.ParentID, 
      tab_Orderdetails.GrandParentID, tab_Orderdetails.GreatParentID, 
      tab_Orderdetails.Over7DaysToBeans, tab_Orderdetails.VouchersNum_List, 
      tab_Orderdetails.Beans, tab_Orderdetails.MoneyCredits, 
      tab_Orderdetails.MoneyWeBuy8Credits, tab_Orderdetails.isdeleted, 
      tab_Orderdetails.Freight, tab_Orderdetails.FreightShowText, 
      tab_Orderdetails.ModifyPriceUpdateDateTime, 
      tab_Orderdetails.UpdateDateTime, tab_Orderdetails.CreatTime, 
      tab_Orderdetails.ShopClient_ID, tab_Orderdetails.GoodType, 
      tab_Orderdetails.GoodTypeId, tab_Orderdetails.GoodTypeIdBuyInfo, 
      tab_Order.PayStatus, tab_Order.PaywayOrderNum, 
      CASE isnull(tab_Order.DeliveryText, '') 
      WHEN '' THEN 'false' ELSE 'true' END AS DeliveryBOOLEAN, 
      tab_User.ID AS Expr1, tab_User.ShopUserID, tab_User.NickName, 
      tab_User_1.NickName AS ParentNickName, 
      tab_User_2.NickName AS GrandParentIDNickName, tab_Order.PayDateTime, 
      tab_Order.CreatTime AS tab_OrderCreatTime, tab_Order.TotalMoney, 
      tab_Order.UserID, tab_ShopClient_Agent_.ShopName, 
      tab_ShopClient_Agent_.ShopTeamID
FROM tab_Orderdetails LEFT OUTER JOIN
      tab_ShopClient_Agent_ ON 
      tab_Orderdetails.ShopClient_ID = tab_ShopClient_Agent_.ShopClientID AND 
      tab_Orderdetails.TeamID = tab_ShopClient_Agent_.ID LEFT OUTER JOIN
      tab_User AS tab_User_2 ON 
      tab_Orderdetails.GrandParentID = tab_User_2.ID LEFT OUTER JOIN
      tab_User AS tab_User_1 ON 
      tab_Orderdetails.ParentID = tab_User_1.ID LEFT OUTER JOIN
      tab_User RIGHT OUTER JOIN
      tab_Order ON tab_User.ID = tab_Order.UserID ON 
      tab_Orderdetails.ShopClient_ID = tab_Order.ShopClient_ID AND 
      tab_Orderdetails.OrderID = tab_Order.ID
WHERE (tab_Orderdetails.GoodType <> 6) AND 
      (tab_Orderdetails.ShopClient_ID = " + strShopClientID + @") AND (tab_Orderdetails.isdeleted = 0) AND
       (tab_Order.IsDeleted = 0) AND (tab_Order.PayStatus = 1) ";

            strTeamID += strInnerWhere.Replace("tab_OrderCreatTime", "tab_Order.CreatTime").Replace("PayDateTime", "tab_Order.PayDateTime") + @") qureyTable";
            ViewState["SQLTable"] = strTeamID;

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

            string strQureryTimeRadio = QureryTimeRadioButtonList.SelectedValue;

            if (strQureryTimeRadio == "0")
            {
                sql.addOrderField("tab_OrderCreatTime", "desc");//第一排序字段   
            }
            else if (strQureryTimeRadio == "1")
            {
                sql.addOrderField("PayDateTime", "desc");//第一排序字段    
            }


            sql.addOrderField("id", "desc");//第一排序字段  
            sql.table = ViewState["SQLTable"].ToString();
            string stroutfields = @"[ID]
      ,[OrderID]
      ,[GoodID],[ShopTeamID],[ShopName]
      ,[Discount]
      ,[GoodName]
      ,[GoodPrice]
      ,[CreatDateTime]
      ,[OrderCount]
      ,[Pinglun]
      ,[ParentID]
      ,[GrandParentID]
      ,[GreatParentID]
      ,[Over7DaysToBeans]
      ,[VouchersNum_List]
      ,[Beans]
      ,[MoneyCredits]
      ,[MoneyWeBuy8Credits]
      ,[isdeleted]
      ,[Freight]
      ,[FreightShowText]
      ,[ModifyPriceUpdateDateTime]
      ,[UpdateDateTime]
      ,[CreatTime]
      ,[ShopClient_ID]
      ,[PayStatus]
      ,[DeliveryBOOLEAN],[UserID]
      ,[ShopUserID]
      ,[NickName]
      ,[ParentNickName]
      ,[GrandParentIDNickName] 
    ,[tab_OrderCreatTime] ,[PayDateTime],[TotalMoney],[PaywayOrderNum]
       ";

            sql.outfields = stroutfields;
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = ViewState["SQLWhere"].ToString();
            sql.where = strwhere;
            string strSql = sql.getSQL(Int32.Parse(ViewState["RecordCount"].ToString()));

            #region 统计全部数据 统计当前页面金额  含运费 
            string strgetSQLTop100percent = sql.getSQLTop100percent(Int32.Parse(ViewState["RecordCount"].ToString()));
            string strPageSql = "select sum(TotalMoney) from (" + strgetSQLTop100percent + ") tableSQLTop100percent";
            LabelPageMoney.Text = bllb007_OperationReturnMoneyEveryDay.SelectList(strPageSql).Tables[0].Rows[0][0].toString();


            sql.nowPageIndex = 1;
            sql.pagesize = Int32.Parse(ViewState["RecordCount"].ToString());////超级大的数 用于页面统计
            string strgetSQLAllRecord = sql.getSQLTop100percent(Int32.Parse(ViewState["RecordCount"].ToString()));
            string strPagesSql = "select sum(TotalMoney) from (" + strgetSQLAllRecord + ") tableSQLTop100percent";
            LabelPagesAllMoney.Text = bllb007_OperationReturnMoneyEveryDay.SelectList(strPagesSql).Tables[0].Rows[0][0].toString();


            #endregion 统计全部数据 统计当前页面金额


            #region 统计全部数据 统计当前页面金额  not含运费 
            //string strgetSQLTop100percent = sql.getSQLTop100percent(Int32.Parse(ViewState["RecordCount"].ToString()));
            string strNotYunFeiPageSql = "select sum(GoodPrice*OrderCount) from (" + strgetSQLTop100percent + ") tableSQLTop100percent";
            LabelPageMoneyNotYunFei.Text = bllb007_OperationReturnMoneyEveryDay.SelectList(strNotYunFeiPageSql).Tables[0].Rows[0][0].toString();


            sql.nowPageIndex = 1;
            sql.pagesize = Int32.Parse(ViewState["RecordCount"].ToString());////超级大的数 用于页面统计
            //string strgetSQLAllRecord = sql.getSQLTop100percent(Int32.Parse(ViewState["RecordCount"].ToString()));
            string strNotPagesSql = "select sum(GoodPrice*OrderCount) from (" + strgetSQLAllRecord + ") tableSQLTop100percent";
            LabelPagesAllMoneyNotYunFei.Text = bllb007_OperationReturnMoneyEveryDay.SelectList(strNotPagesSql).Tables[0].Rows[0][0].toString();


            #endregion 统计全部数据 统计当前页面金额


            ViewState["searchWhere"] = strSql;// " and ShopClientID=" + strShopClientID + strWhere;


            BindAnnounce();
            ShowState();
        }


        protected void lbtnLast_Click_DownLoad(object sender, EventArgs e)
        {
            try
            {
                #region Excel 表格下载
                string strTablename = "[Sheet1$]";
                Eggsoft.Common.debug_Log.Call_WriteLog(strTablename, "财富系统订单表格下载", "数据记录sheet1");

                string strFromfilePath = "ExcelDocument/BaseDocument/12OrderDetailEveryDayDownload_Insert.xlsx";
                string FromfilePath = HttpContext.Current.Server.MapPath(strFromfilePath);

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strDownFileName = "OrderDetailDownload" + Eggsoft.Common.StringNum.Add000000Num(strShopClientID.toInt32(), 5) + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ".xlsx";
                string WillDowmFile = "ExcelDocument/DownLoadDocument/" + strDownFileName;
                string AccessfilePath = HttpContext.Current.Server.MapPath(WillDowmFile);

                Eggsoft.Common.debug_Log.Call_WriteLog(AccessfilePath, "财富系统订单表格下载", "数据记录sheet1");


                System.IO.File.Copy(FromfilePath, AccessfilePath, true);////去掉只读属性
                System.IO.File.SetAttributes(AccessfilePath, System.IO.FileAttributes.Normal);

                //tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn);//实例化OleDbCommand类   



                #region 准备全部表格的数据
                string strSessionWhere = " 1=1";


                string strini = Request.QueryString["ini"];
                bool boolIni = false;
                bool.TryParse(strini, out boolIni);
                if (boolIni)
                {

                }
                else
                {
                    strTextBox_StartTime = Request.Form["TextBox_OrderStartTime"];
                    strTextBox_EndTime = Request.Form["TextBox_OrderEndTime"];
                }

                string strQureryTimeRadio = QureryTimeRadioButtonList.SelectedValue;
                string strItemType = "PayDateTime";
                if (strQureryTimeRadio == "0")
                {
                    strItemType = "tab_OrderCreatTime";
                }
                else
                {
                    strItemType = "PayDateTime";
                }

                DateTime my_OrderStartTime = DateTime.Now;
                if (string.IsNullOrEmpty(strTextBox_StartTime) == false)
                {
                    my_OrderStartTime = DateTime.ParseExact(strTextBox_StartTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    strSessionWhere += " and " + strItemType + ">='" + my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_StartDateTime", strTextBox_StartTime);
                DateTime my_OrderEndTime = DateTime.Now;
                if (string.IsNullOrEmpty(strTextBox_EndTime) == false)
                {
                    my_OrderEndTime = DateTime.ParseExact(strTextBox_EndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    strSessionWhere += " and " + strItemType + "<='" + my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_EndDateTime", strTextBox_EndTime);



                if (string.IsNullOrEmpty(TextBox_UserName.Text) == false)
                {
                    strSessionWhere += " and (nickname like '%" + TextBox_UserName.Text + "%')";
                }
                if (string.IsNullOrEmpty(TextBoxUserID.Text) == false)
                {
                    strSessionWhere += " and (ShopUserID =" + TextBoxUserID.Text + ")";
                }
                //if (TextBoxCenterName.Text.toInt32().toString() == TextBoxCenterName.Text)
                //{
                //    strSessionWhere += " and (goodTypeID=" + TextBoxCenterName.Text + ")";
                //}
                //else if (string.IsNullOrEmpty(TextBoxCenterName.Text) == false)
                //{
                //    strSessionWhere += " and (ParentMasterName like '%" + TextBoxCenterName.Text + "%')";
                //}
                //int intWhichCenter = Request.QueryString["OperationCenterID"].toString().toInt32();
                //if (intWhichCenter > 0)
                //{
                //    strSessionWhere += " and (goodTypeID=" + intWhichCenter + ")";
                //}




                string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strSessionWhere, "count(1) as RecordCount");
                string strRecordCount = bllb007_OperationReturnMoneyEveryDay.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

                web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();

                //string strQureryTimeRadio = QureryTimeRadioButtonList.SelectedValue;

                if (strQureryTimeRadio == "0")
                {
                    sql.addOrderField("tab_OrderCreatTime", "desc");//第一排序字段   
                }
                else if (strQureryTimeRadio == "1")
                {
                    sql.addOrderField("PayDateTime", "desc");//第一排序字段    
                }


                //sql.addOrderField("PayDateTime", "desc");//第一排序字段              
                sql.addOrderField("id", "desc");//第一排序字段  
                sql.table = ViewState["SQLTable"].ToString();
                string stroutfields = @"[ID]
      ,[OrderID]
      ,[GoodID]
      ,[Discount]
      ,[GoodName]
      ,[GoodPrice]
      ,[CreatDateTime]
      ,[OrderCount]
      ,[Pinglun]
      ,[ParentID]
      ,[GrandParentID]
      ,[GreatParentID]
      ,[Over7DaysToBeans]
      ,[VouchersNum_List]
      ,[Beans]
      ,[MoneyCredits]
      ,[MoneyWeBuy8Credits]
      ,[isdeleted]
      ,[Freight]
      ,[FreightShowText]
      ,[ModifyPriceUpdateDateTime]
      ,[UpdateDateTime]
      ,[CreatTime]
      ,[ShopClient_ID]
      ,[GoodType]
      ,[GoodTypeId]
      ,[GoodTypeIdBuyInfo]
      ,[PayStatus]
      ,[DeliveryBOOLEAN],[UserID]
      ,[ShopUserID]
      ,[NickName]
      ,[ParentNickName]
      ,[GrandParentIDNickName]
      ,[GrandParentMasterName]
     ,[tab_OrderCreatTime] ,[PayDateTime],[TotalMoney]";

                sql.outfields = stroutfields;
                sql.nowPageIndex = 1;
                sql.pagesize = 10000000;
                string strwhere = strSessionWhere;
                sql.where = strwhere;
                string strSql = sql.getSQL(Int32.Parse(strRecordCount));
                System.Data.DataTable Data_DataTable = bllb007_OperationReturnMoneyEveryDay.SelectList(strSql).Tables[0];
                #endregion 准备全部表格的数据


                string connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AccessfilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=0'";//记录连接Access的语句   
                System.Data.OleDb.OleDbConnection tem_conn = new System.Data.OleDb.OleDbConnection(connstr);//连接Access数据库   
                System.Data.OleDb.OleDbCommand tem_commOleDbCommand;//定义OleDbCommand类   
                tem_commOleDbCommand = new System.Data.OleDb.OleDbCommand();
                System.Data.OleDb.OleDbTransaction transaction = null;


                try
                {
                    tem_conn.Open();//打开连接的Access数据库   
                                    //多条ＳＱＬ语句是数据库级别的事务！！！
                    transaction = tem_conn.BeginTransaction();

                    // Assign transaction object for a pending local transaction.
                    tem_commOleDbCommand.Connection = tem_conn;
                    tem_commOleDbCommand.Transaction = transaction;

                    #region 写excel文件
                    //tem_commOleDbCommand.CommandText = "CREATE INDEX index_nameE0 ON " + strTablename + " (E0)";
                    //tem_commOleDbCommand.ExecuteNonQuery();
                    #region 原来的Update
                    for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                    {



                        string SQLtem_sql = "";// "select Count(*) From " + strTablename;//设置SQL语句，获取记录个数   
                        SQLtem_sql = @"INSERT INTO " + strTablename + @"(F_1, 
                            F0,
                            F1,
                            F2,
                            F3,
                            F4,
                            F5,F51,
                            F6,
                            F7,
                            F8,
                            F9,
                            F10,
                            F11,
                            F12,
                            F13,
                            F14
) VALUES";
                        SQLtem_sql += "('" + Data_DataTable.Rows[i]["UserID"] + "', ";//F_1用户标识
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["ID"] + "',";//F0 序号
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["Nickname"] + "',";//F1 下单用户
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["ShopUserID"] + "',";//F2 下单用户ID
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["tab_OrderCreatTime"] + "',";//F3订单创建
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["GoodPrice"].toDecimal() + "',";//F4 商品价格
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["OrderCount"].toInt32() + "',";//F5 购买数量
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["PayDateTime"] + "',";//F51 支付时间
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["TotalMoney"].toDecimal() + "',";//F6 支付金额
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["ParentNickName"] + "',";//F7 上级用户
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["GrandParentIDNickName"] + "',";//F8 上上级用户
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["ParentMasterName"] + "',";//F9 运营中心
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["OperationCenterID"] + "',";//F10 运营中心ID
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["GrandParentMasterName"] + "',";//F11 上级运营中心
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["ParentOperationCenterID"] + "',";//F12 上级运营中心ID
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["Over7DaysToBeans"].toBoolean() + "',";//F13 是否转化
                        SQLtem_sql += "'" + Data_DataTable.Rows[i]["DeliveryBOOLEAN"].toBoolean() + "')";//F14 是否发货

                        //SQLtem_sql += "update  " + strTablename + " set ";
                        //SQLtem_sql += "F_1='" + Data_DataTable.Rows[i]["UserID"] + "',";//用户标识
                        //SQLtem_sql += "F0='" + Data_DataTable.Rows[i]["ID"] + "',";//序号
                        //SQLtem_sql += "F1='" + Data_DataTable.Rows[i]["Nickname"] + "',";//下单用户
                        //SQLtem_sql += "F2='" + Data_DataTable.Rows[i]["ShopUserID"] + "',";//下单用户ID
                        //SQLtem_sql += "F3='" + Data_DataTable.Rows[i]["PayDateTime"] + "',";//支付时间
                        //SQLtem_sql += "F4=" + Data_DataTable.Rows[i]["GoodPrice"].toDecimal() + ",";//商品价格
                        //SQLtem_sql += "F5=" + Data_DataTable.Rows[i]["OrderCount"].toInt32() + ",";//购买数量
                        //SQLtem_sql += "F6=" + Data_DataTable.Rows[i]["TotalMoney"].toDecimal() + ",";//支付金额
                        //SQLtem_sql += "F7='" + Data_DataTable.Rows[i]["ParentNickName"] + "',";//上级用户
                        //SQLtem_sql += "F8='" + Data_DataTable.Rows[i]["GrandParentIDNickName"] + "',";//上上级用户
                        //SQLtem_sql += "F9='" + Data_DataTable.Rows[i]["ParentMasterName"] + "',";//运营中心
                        //SQLtem_sql += "F10='" + Data_DataTable.Rows[i]["OperationCenterID"] + "',";//运营中心ID

                        //SQLtem_sql += "F11='" + Data_DataTable.Rows[i]["GrandParentMasterName"] + "',";//上级运营中心
                        //SQLtem_sql += "F12='" + Data_DataTable.Rows[i]["ParentOperationCenterID"] + "',";//上级运营中心ID


                        //SQLtem_sql += "F13='" + Data_DataTable.Rows[i]["Over7DaysToBeans"].toBoolean() + "',";//是否转化
                        //SQLtem_sql += "F14='" + Data_DataTable.Rows[i]["DeliveryBOOLEAN"].toBoolean() + "'";//是否发货


                        //SQLtem_sql += " where E0='E" + (i + 1).toString() + "';";

                        // Execute the commands.
                        tem_commOleDbCommand.CommandText = SQLtem_sql;
                        tem_commOleDbCommand.ExecuteNonQuery();
                        //strAllSQLtem_sql += SQLtem_sql;

                    }
                    #endregion 原来的Update

                    #region 原来的Update
                    //for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                    ////for (int i = 0; i < 2; i++)
                    //{
                    //    string SQLtem_sql = "";// "select Count(*) From " + strTablename;//设置SQL语句，获取记录个数   
                    //    SQLtem_sql += "update  " + strTablename + " set ";
                    //    SQLtem_sql += "F_1='" + Data_DataTable.Rows[i]["UserID"] + "',";//用户标识
                    //    SQLtem_sql += "F0='" + Data_DataTable.Rows[i]["ID"] + "',";//序号
                    //    SQLtem_sql += "F1='" + Data_DataTable.Rows[i]["Nickname"] + "',";//下单用户
                    //    SQLtem_sql += "F2='" + Data_DataTable.Rows[i]["ShopUserID"] + "',";//下单用户ID
                    //    SQLtem_sql += "F3='" + Data_DataTable.Rows[i]["PayDateTime"] + "',";//支付时间
                    //    SQLtem_sql += "F4=" + Data_DataTable.Rows[i]["GoodPrice"].toDecimal() + ",";//商品价格
                    //    SQLtem_sql += "F5=" + Data_DataTable.Rows[i]["OrderCount"].toInt32() + ",";//购买数量
                    //    SQLtem_sql += "F6=" + Data_DataTable.Rows[i]["TotalMoney"].toDecimal() + ",";//支付金额
                    //    SQLtem_sql += "F7='" + Data_DataTable.Rows[i]["ParentNickName"] + "',";//上级用户
                    //    SQLtem_sql += "F8='" + Data_DataTable.Rows[i]["GrandParentIDNickName"] + "',";//上上级用户
                    //    SQLtem_sql += "F9='" + Data_DataTable.Rows[i]["ParentMasterName"] + "',";//运营中心
                    //    SQLtem_sql += "F10='" + Data_DataTable.Rows[i]["OperationCenterID"] + "',";//运营中心ID

                    //    SQLtem_sql += "F11='" + Data_DataTable.Rows[i]["GrandParentMasterName"] + "',";//上级运营中心
                    //    SQLtem_sql += "F12='" + Data_DataTable.Rows[i]["ParentOperationCenterID"] + "',";//上级运营中心ID


                    //    SQLtem_sql += "F13='" + Data_DataTable.Rows[i]["Over7DaysToBeans"].toBoolean() + "',";//是否转化
                    //    SQLtem_sql += "F14='" + Data_DataTable.Rows[i]["DeliveryBOOLEAN"].toBoolean() + "'";//是否发货


                    //    SQLtem_sql += " where E0='E" + (i + 1).toString() + "';";

                    //    // Execute the commands.
                    //    tem_commOleDbCommand.CommandText = SQLtem_sql;
                    //    tem_commOleDbCommand.ExecuteNonQuery();
                    //    //strAllSQLtem_sql += SQLtem_sql;

                    //}
                    #endregion 原来的Update

                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                    #endregion 写excel文件

                }
                catch (Exception Exceptiondddd)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "运营订单表格下载", "写Excel文件失败");
                    try
                    {


                        // Attempt to roll back the transaction.
                        transaction.Rollback();
                    }
                    catch
                    {
                        // Do nothing here; transaction is not active.
                    }

                }
                finally
                {
                    tem_conn.Close();
                }

                // Commit the transaction.


                //Eggsoft.Common.debug_Log.Call_WriteLog(SQLtem_sql, "财富系统订单表格下载", "数据记录sql");

                //tem_comm.CommandText = strAllSQLtem_sql;
                // tem_comm.ExecuteNonQuery();

                //tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn);//实例化OleDbCommand类   
                //RecordCount = (int)tem_comm.ExecuteScalar();//执行SQL语句，并返回结果   




                #endregion Excel 财富系统订单下载


                #region 流方式下载 

                string filePath = Server.MapPath(WillDowmFile);//路径 

                //以字符流的形式下载文件 
                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开 
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strDownFileName, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                #endregion


            }
            catch (Exception Exceptiondddd)
            {


                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "运营订单表格下载");
            }
        }
    }
}
