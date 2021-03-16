using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._19tab_Order
{
    public partial class tab_Fund_Order_Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strTextBox_OrderStartTime = "";
        public string strTextBox_OrderEndTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    intiSQLSession();
                    BindBigClass();
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "19tab_Order", "线程异常");
                }
                catch (Exception Exceptiondddd)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "19tab_Order");
                }
            }

        }

        public string showOperate(string strOrderID)
        {
            string strshowOperate = "";


            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Model_tab_Order = new EggsoftWX.Model.tab_Order();
            Model_tab_Order = BLL_tab_Order.GetModel(Int32.Parse(strOrderID));

            bool boolPayStatus = Convert.ToBoolean(Model_tab_Order.PayStatus);

            DateTime dt2 = DateTime.Now;  //new DateTime(2013, 10, 13, 19, 18, 50);
            TimeSpan ts1 = dt2.Subtract(Convert.ToDateTime(Model_tab_Order.PayDateTime));
            int DaysInterval1 = ts1.Days;
            int doubleInterval1 = ts1.Milliseconds;

            //EggsoftWX.BLL.tab_ShopClient_ASK bll_tab_ShopClient_ASK = new EggsoftWX.BLL.tab_ShopClient_ASK();
            //bool sendMoney = bll_tab_ShopClient_ASK.Exists("OrderID=" + strOrderID + " and IFPayByShenMa_Finace=1");
            //if (sendMoney)
            //{
            //    strshowOperate += "该订单微店财务已结算！";
            //}
            //else if (boolPayStatus && (DaysInterval1 >= 0))
            //{
            //    strshowOperate += "<a href=\"../10tab_ShopClient/IwantGetMoney.aspx?OrderID=" + strOrderID + "\">向财务申请结算</a>";
            //}

            EggsoftWX.BLL.tab_ReturnMoney bll_tab_ReturnMoney = new EggsoftWX.BLL.tab_ReturnMoney();
            bool ReturnMoney = bll_tab_ReturnMoney.Exists("OrderID=" + strOrderID + " and FinanceCheck=1");
            if (ReturnMoney)
            {
                strshowOperate += "<br />该订单微店财务已退款！";
            }
            else if (boolPayStatus && (DaysInterval1 >= 0))
            {
                strshowOperate += "<br /><a href=\"../10tab_ShopClient/IwantRetunMoney.aspx?OrderID=" + strOrderID + "\">向财务申请退款</a>";
            }
            return strshowOperate;
        }

        //    <%# Eval("ID") %>
        public string showFenXiaoList(string strOrderDetailID)
        {

            //Eggsoft_Public_CL
            Decimal deShopGet = 0;

            string strBlockShow = Eggsoft_Public_CL.GoodP.GetThisOrderShopGet_showFenXiaoList(strOrderDetailID, out deShopGet);

            string strShowMoneList = strBlockShow;
            return strShowMoneList;

        }


        //    <%# Eval("ID") %>
        public string getColor(string strID)
        {
            String strColor = "";

            int conToInt16 = Convert.ToInt32(strID);
            bool mybool = Convert.ToBoolean(conToInt16 % 2);
            if (mybool)
            {
                strColor = "#ECF5FF";
            }
            else
            {
                strColor = "#E3E3E3";
            }

            return strColor;
        }

        public string getPayStatus(string PayStatus)
        {

            string strGetPayStatus = "";
            switch (PayStatus.toBoolean())
            {
                case false:
                    strGetPayStatus = "<span style=\"color:red;\">未支付</span>";
                    break;
                case true:
                    strGetPayStatus = "已支付";
                    break;

            }
            return strGetPayStatus;
        }


        public string getDeliveryStatus(string DeliveryStatus)
        {

            string strDeliveryStatus = "";
            switch (DeliveryStatus)
            {
                case "":
                    strDeliveryStatus = "未发货";
                    break;
                case "1":
                    strDeliveryStatus = "已发货." + DeliveryStatus;
                    break;

            }
            return strDeliveryStatus;
        }


        public void BindBigClass()
        {
            AspNetPager1_PageChanged(null, null);
            AspNetPager2.UrlRewritePattern = "tab_Fund_Order_Board.aspx?pageIndex={0}";

        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            DataList my_OrderDatail_DataList = (DataList)e.Item.FindControl("OrderDatail");
            if (my_OrderDatail_DataList != null)
            {
                String strOrder_ID = "0";

                HiddenField Field_strOrder_ID = (HiddenField)e.Item.FindControl("Order_ID");
                if (Field_strOrder_ID != null)
                {
                    strOrder_ID = Field_strOrder_ID.Value.ToString().Trim();
                }


                EggsoftWX.BLL.tab_Orderdetails blltab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                string strSQL = @"SELECT   tab_Order.UserID, tab_Orderdetails.*
FROM      tab_Orderdetails LEFT OUTER JOIN
                tab_Order ON tab_Orderdetails.OrderID = tab_Order.ID where tab_Orderdetails.OrderID=@OrderID and tab_Orderdetails.isdeleted<>1 order by tab_Orderdetails.id asc";

                my_OrderDatail_DataList.DataSource = blltab_Orderdetails.SelectList(strSQL, strOrder_ID.toInt32());
                my_OrderDatail_DataList.DataBind();

            
            }
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("tab_Class1_Add.aspx");
        }


        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            lock ("ojblock20160228" + strShopClientID)
            {
                EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();

                int intpageIndex = 1;

                string strRequest = Request.QueryString["pageIndex"];
                if (string.IsNullOrEmpty(strRequest) == false)
                {
                    intpageIndex = Convert.ToInt32(strRequest);
                }
                #region 得到开始 结束时间
                DateTime my_OrderStartTime = DateTime.Now;
                string strTempTextBox_OrderStartTime = Eggsoft.Common.Session.Read("OrderSQLWhere_StartDateTime");
                if (string.IsNullOrEmpty(strTempTextBox_OrderStartTime) == false)
                {
                    my_OrderStartTime = DateTime.ParseExact(strTempTextBox_OrderStartTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                DateTime my_OrderEndTime = DateTime.Now;
                string strTempTextBox_OrderEndTime = Eggsoft.Common.Session.Read("OrderSQLWhere_EndDateTime");
                if (string.IsNullOrEmpty(strTempTextBox_OrderEndTime) == false)
                {
                    my_OrderEndTime = DateTime.ParseExact(strTempTextBox_OrderEndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    //strSessionWhere += " and CreateDateTime<='" + my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                #endregion
                #region 生成试图
                string strsp_V_OrderSearchView_OrderSearch_New00 = Eggsoft.Common.FileFolder.ReadFile("sp_V_OrderSearchView_OrderSearch_New00.txt");
                strsp_V_OrderSearchView_OrderSearch_New00 = String.Format(strsp_V_OrderSearchView_OrderSearch_New00, strShopClientID, 1, my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss"), my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                #endregion


                string strtableWhereSQL = Eggsoft.Common.FileFolder.ReadFile("View_OrderSearch_SQL.txt");
                strtableWhereSQL = String.Format(strtableWhereSQL, strShopClientID, my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss"), my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss"), 1);

                string strWhere = " isdeleted<>1 and PayStatus=1 and isReceipt=1 and DeliveryText<>\'\' ";
                string strReadSessionWhere = Eggsoft.Common.Session.Read("OrderSQLWhere");
                strWhere += strReadSessionWhere;
                #region 得到个数
                System.Text.StringBuilder strCountSql = new System.Text.StringBuilder();
                strCountSql.Append("select count(1) from (" + strtableWhereSQL + ") as tableWhereSQL where" + strWhere);
                object obj = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strsp_V_OrderSearchView_OrderSearch_New00 + strCountSql.ToString());
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = Int32.Parse(obj.ToString());
                }
                int intAllCountsNum = cmdresult;
                #endregion
                AspNetPager2.RecordCount = intAllCountsNum;
                AspNetPager2.CurrentPageIndex = intpageIndex;


                web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                sql.addOrderField("id", "desc");//第二排序字段  
                sql.table = "(" + strtableWhereSQL + ") as tableWhereSQL";
                sql.outfields = "*";
                sql.nowPageIndex = intpageIndex;
                sql.pagesize = AspNetPager2.PageSize;
                sql.where = strWhere;
                string strSql = sql.getSQL(AspNetPager2.RecordCount);
                System.Data.DataTable myDataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.Query(strsp_V_OrderSearchView_OrderSearch_New00 + strSql).Tables[0];
                int intCount = myDataTable.Rows.Count;

                this.DataList1.DataSource = EggsoftWX.SQLServerDAL.DbHelperSQL.Query(strsp_V_OrderSearchView_OrderSearch_New00 + strSql);
                DataList1.DataKeyField = "ID";
                this.DataList1.DataBind();
            }
        }

        /// <summary>
        /// 5个文件 只要做好 一个 直接 复制即可  注意  Response.Redirect("tab_Order_Board_WaitGiveGoods.aspx");
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            try
            {
                //string strSQL = "datediff(d,CreateDateTime,getdate())<= 3000";
                string strSessionWhere = "";

                if (string.IsNullOrEmpty(TextBox_ShopUserID.Text.Trim()) == false)
                {
                    strSessionWhere += " and (ShopUserID=" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_ShopUserID.Text.Trim()) + ")";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_ShopUserID", Eggsoft.Common.CommUtil.SafeFilter(TextBox_ShopUserID.Text.Trim()));


                if (string.IsNullOrEmpty(TextBox_UserInfo.Text.Trim()) == false)
                {
                    strSessionWhere += " and (UserRealName like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()) + "%' or address_RealName like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()) + "%' or UserNickName like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()) + "%')";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_RealName", Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()));
                if (string.IsNullOrEmpty(TextBox_UserAddress.Text.Trim()) == false)
                {
                    strSessionWhere += " and address_XiangXiDiZHi like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserAddress.Text.Trim()) + "%'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_XiangXiDiZHi", Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserAddress.Text.Trim()));
                if (string.IsNullOrEmpty(TextBox_Tel.Text.Trim()) == false)
                {
                    strSessionWhere += " and (TakePhone like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()) + "%' or address_TelPhone like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()) + "%' or address_MobilePhone like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()) + "%')";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_TelPhone", Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()));
                if (string.IsNullOrEmpty(TextBox_PayPrice.Text.Trim()) == false)
                {
                    strSessionWhere += " and TotalMoney " + DropDownList_PayPrice.Text.Trim() + Eggsoft.Common.CommUtil.SafeFilter(TextBox_PayPrice.Text.Trim());
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_TotalMoney", Eggsoft.Common.CommUtil.SafeFilter(TextBox_PayPrice.Text.Trim()));
                Eggsoft.Common.Session.Add("DropDownList_PayPrice", DropDownList_PayPrice.Text.Trim());
                if (string.IsNullOrEmpty(TextBox_GoodName.Text.Trim()) == false)
                {
                    strSessionWhere += " and allGoodName like '%" + TextBox_GoodName.Text.Trim() + "%'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_GoodName", TextBox_GoodName.Text.Trim());

                string strini = Request.QueryString["ini"];
                bool boolIni = false;
                bool.TryParse(strini, out boolIni);
                if (boolIni)
                {

                }
                else
                {
                    strTextBox_OrderStartTime = Request.Form["TextBox_OrderStartTime"];
                    strTextBox_OrderEndTime = Request.Form["TextBox_OrderEndTime"];
                }

                DateTime my_OrderStartTime = DateTime.Now;
                if (string.IsNullOrEmpty(strTextBox_OrderStartTime) == false)
                {
                    my_OrderStartTime = DateTime.ParseExact(strTextBox_OrderStartTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    strSessionWhere += " and CreateDateTime>='" + my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_StartDateTime", strTextBox_OrderStartTime);
                DateTime my_OrderEndTime = DateTime.Now;
                if (string.IsNullOrEmpty(strTextBox_OrderEndTime) == false)
                {
                    my_OrderEndTime = DateTime.ParseExact(strTextBox_OrderEndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    strSessionWhere += " and CreateDateTime<='" + my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_EndDateTime", strTextBox_OrderEndTime);
                TimeSpan ts = my_OrderEndTime - my_OrderStartTime;
                if (ts.Days > 90)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("订单开始时间与结束时间不能超过三个月", -1);
                }


                if (string.IsNullOrEmpty(TextBox_GoodAllPrice.Text.Trim()) == false)
                {
                    strSessionWhere += " and allGoodPrice " + DropDownList_GoodAllPrice.Text.Trim() + Eggsoft.Common.CommUtil.SafeFilter(TextBox_GoodAllPrice.Text.Trim());
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_allGoodPrice", TextBox_GoodAllPrice.Text.Trim());
                Eggsoft.Common.Session.Add("DropDownList_GoodAllPrice", DropDownList_GoodAllPrice.Text.Trim());

                if (string.IsNullOrEmpty(TextBox_AllGoodsCount.Text.Trim()) == false)
                {
                    strSessionWhere += " and OrderCount " + DropDownList_AllGoodsCount.Text.Trim() + Eggsoft.Common.CommUtil.SafeFilter(TextBox_AllGoodsCount.Text.Trim());
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_OrderCount", TextBox_AllGoodsCount.Text.Trim());
                Eggsoft.Common.Session.Add("TextBox_AllGoodsCount", DropDownList_AllGoodsCount.Text.Trim());

                if (string.IsNullOrEmpty(TextBox_Freight.Text.Trim()) == false)
                {
                    strSessionWhere += " and Freight " + DropDownList_Freight.Text.Trim() + TextBox_Freight.Text.Trim();
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_Freight", Eggsoft.Common.CommUtil.SafeFilter(TextBox_Freight.Text.Trim()));
                Eggsoft.Common.Session.Add("DropDownList_Freight", DropDownList_Freight.Text.Trim());

                if (string.IsNullOrEmpty(TextBox_TakeGoodInfo.Text.Trim()) == false)
                {
                    strSessionWhere += " and (ShopName like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_TakeGoodInfo.Text.Trim()) + "%' or ShopContactMan like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_TakeGoodInfo.Text.Trim()) + "%')";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere__TakeGoodInfo", TextBox_TakeGoodInfo.Text.Trim());

                if (string.IsNullOrEmpty(TextBox_AgentShow.Text.Trim()) == false)
                {
                    TextBox_AgentShow.Text = Eggsoft.Common.CommUtil.SafeFilter(TextBox_AgentShow.Text.Trim());
                    strSessionWhere += " and (GreatParentIDNickName like '%" + TextBox_AgentShow.Text.Trim() + "%' or GreatParentIDUserRealName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ShopNameGreatParentID like '%" + TextBox_AgentShow.Text.Trim() + "%' or GrandParentIDNickName like '%" + TextBox_AgentShow.Text.Trim() + "%' or GrandParentIDUserRealName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ShopNameGrandParentID like '%" + TextBox_AgentShow.Text.Trim() + "%' or ParentIDNickName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ParentIDUserRealName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ShopNameParentID like '%" + TextBox_AgentShow.Text.Trim() + "%')";
                }
                if (string.IsNullOrEmpty(TextBox_OrderNum.Text.Trim()) == false)
                {
                    strSessionWhere += " and OrderNum='" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_OrderNum.Text.Trim()) + "'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere__OrderNum", TextBox_OrderNum.Text.Trim());


                Eggsoft.Common.Session.Add("OrderSQLWhere___AgentShow", TextBox_AgentShow.Text.Trim());

                Eggsoft.Common.Session.Add("OrderSQLWhere", strSessionWhere);

                Response.Redirect("tab_Fund_Order_Board.aspx");
                //BindBigClass();///分页跳转时  初始化用的
                //intiSQLSession();
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "19tab_Order", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "19tab_Order");
            }
        }

        protected void intiSQLSession()
        {
            string strini = Request.QueryString["ini"];
            bool boolIni = false;
            bool.TryParse(strini, out boolIni);
            if (boolIni)
            {
                strTextBox_OrderStartTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd HH:mm:ss");
                strTextBox_OrderEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Button1_Click_Query(null, null);
            }
            else
            {
                TextBox_ShopUserID.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_ShopUserID");
                TextBox_UserInfo.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_RealName");
                TextBox_UserAddress.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_XiangXiDiZHi");
                TextBox_Tel.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_TelPhone");
                TextBox_PayPrice.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_TotalMoney");
                DropDownList_PayPrice.Text = Eggsoft.Common.Session.Read("DropDownList_PayPrice");

                TextBox_GoodName.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_GoodName");

                strTextBox_OrderStartTime = Eggsoft.Common.Session.Read("OrderSQLWhere_StartDateTime");
                if (string.IsNullOrEmpty(strTextBox_OrderStartTime))
                {
                    strTextBox_OrderStartTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd HH:mm:ss");
                }
                strTextBox_OrderEndTime = Eggsoft.Common.Session.Read("OrderSQLWhere_EndDateTime");
                if (string.IsNullOrEmpty(strTextBox_OrderEndTime))
                {
                    strTextBox_OrderEndTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                }

                TextBox_GoodAllPrice.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_allGoodPrice");
                DropDownList_GoodAllPrice.Text = Eggsoft.Common.Session.Read("DropDownList_GoodAllPrice");

                TextBox_AllGoodsCount.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_OrderCount");
                DropDownList_AllGoodsCount.Text = Eggsoft.Common.Session.Read("DropDownList_AllGoodsCount");

                TextBox_Freight.Text = Eggsoft.Common.Session.Read("OrderSQLWhere_Freight");
                DropDownList_Freight.Text = Eggsoft.Common.Session.Read("DropDownList_Freight");

                TextBox_TakeGoodInfo.Text = Eggsoft.Common.Session.Read("OrderSQLWhere__TakeGoodInfo");
                TextBox_AgentShow.Text = Eggsoft.Common.Session.Read("OrderSQLWhere___AgentShow");

                TextBox_OrderNum.Text = Eggsoft.Common.Session.Read("OrderSQLWhere__OrderNum");



            }

        }
    }
}