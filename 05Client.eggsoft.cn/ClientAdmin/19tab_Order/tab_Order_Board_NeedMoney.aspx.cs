using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._19tab_Order
{
    public partial class tab_Order_Board_NeedMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strTextBox_OrderStartTime = "";
        public string strTextBox_OrderEndTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_NeedMoney")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                try
                {
                    intiSQLSession();
                    BindBigClass();///分页跳转时  初始化用的
                    //Button1_Click_Query(null, null);
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




        public string getUserName(string strUserID, string stShopClientID)
        {
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();

            Model_tab_User = BLL_tab_User.GetModel(Convert.ToInt32(strUserID));
            if (Model_tab_User != null)
            {
                String strCustomerName = "可能未关注";
                String strNickname = Model_tab_User.NickName;
                if (String.IsNullOrEmpty(strNickname) == false) strCustomerName = strNickname;

                return "<a title=\"发送客服消息\" target=\"_blank\" href=\"../SendMessage.aspx?UserID=" + strUserID + "&ShopClientID=" + stShopClientID + "\">" + strCustomerName + "</a>";
            }
            else
            {
                return "";
            }
        }

        public string getPayStatus(string PayStatus)
        {

            string strGetPayStatus = "";
            switch (bool.Parse(PayStatus))
            {
                case false:
                    strGetPayStatus = "未支付";
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
            //return;
            AspNetPager1_PageChanged(null, null);
            AspNetPager2.UrlRewritePattern = "tab_Order_Board_NeedMoney.aspx?pageIndex={0}";
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //return;

            DataList myDataList = (DataList)e.Item.FindControl("OrderDatail");
            if (myDataList != null)
            {
                String strOrder_ID = "0";

                HiddenField Field_strOrder_ID = (HiddenField)e.Item.FindControl("Order_ID");
                if (Field_strOrder_ID != null)
                {
                    strOrder_ID = Field_strOrder_ID.Value.ToString().Trim();
                }


                EggsoftWX.BLL.tab_Orderdetails blltab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                Eggsoft.Common.JsUtil.ShowMsg("OrderID='" + strOrder_ID);
                string strSQL = @"SELECT   tab_Order.UserID, tab_Orderdetails.*
FROM      tab_Orderdetails LEFT OUTER JOIN
                tab_Order ON tab_Orderdetails.OrderID = tab_Order.ID where tab_Orderdetails.OrderID=@OrderID and tab_Orderdetails.isdeleted<>1 order by tab_Orderdetails.id asc";

                myDataList.DataSource = blltab_Orderdetails.SelectList(strSQL, strOrder_ID.toInt32());

                myDataList.DataBind();
            }
        }

        protected void DataList2_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            String strBig2ClassID = "0";

            HiddenField Field_Big2ClassID = (HiddenField)e.Item.FindControl("Class2_ID");
            if (Field_Big2ClassID != null)
            {
                strBig2ClassID = Field_Big2ClassID.Value.ToString().Trim();
            }

            DataList myDataList = (DataList)e.Item.FindControl("dlst3Class");
            if (myDataList != null)
            {
                EggsoftWX.BLL.tab_Class3 bll = new EggsoftWX.BLL.tab_Class3();
                //int Big2ClassID = Int32.Parse(dlst2Class.DataKeys[e.Item.ItemIndex].ToString());
                myDataList.DataSource = bll.GetList("*", "Class2_ID=" + strBig2ClassID + " order by Sort asc");
                myDataList.DataBind();

            }

        }



        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("tab_Class1_Add.aspx");
        //}
        protected void Btt_Money_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrder_Detail_ID = TextBox_OrderID.Text.Trim();
                string strTxtNewMoney = TxtNewMoney.Text.Trim();
                if (strTxtNewMoney.Length == 0)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("金额不能为空！");
                    return;
                }


                EggsoftWX.BLL.tab_Orderdetails myBll_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                myBll_tab_Orderdetails.Update("GoodPrice=" + strTxtNewMoney + ",ModifyPriceUpdateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'", "ID=" + strOrder_Detail_ID);



                #region 更新订单金额
                string strUpdateOrderID = myBll_tab_Orderdetails.GetList("OrderID", "ID=" + strOrder_Detail_ID).Tables[0].Rows[0][0].ToString();

                DataTable myDataTable = myBll_tab_Orderdetails.GetList("GoodPrice,OrderCount,Freight", "OrderID=" + strUpdateOrderID).Tables[0];
                Decimal myAllDecimal = 0;

                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    Decimal myDecimal = Convert.ToDecimal(myDataTable.Rows[i]["GoodPrice"].ToString()) * Convert.ToInt32(myDataTable.Rows[i]["OrderCount"].ToString()) + Convert.ToDecimal(myDataTable.Rows[i]["Freight"].ToString());
                    myAllDecimal += myDecimal;
                }
                string strOrderNum = DateTime.Now.ToString("yyyyMMddHHmmss") + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strUpdateOrderID), 8);
                new EggsoftWX.BLL.tab_Order().Update("TotalMoney=" + myAllDecimal + ",OrderNum='" + strOrderNum + "'", "ID=" + strUpdateOrderID);
                #endregion
                #region 微信消息
                EggsoftWX.Model.tab_Orderdetails myModel_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();
                myModel_tab_Orderdetails = myBll_tab_Orderdetails.GetModel("ID=" + strOrder_Detail_ID);

                string strweixinOrderID = myModel_tab_Orderdetails.OrderID.ToString();
                string strGoodName = myModel_tab_Orderdetails.GoodName.ToString();
                EggsoftWX.BLL.tab_Order myBll_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order myModel_tab_Order = new EggsoftWX.Model.tab_Order();
                myModel_tab_Order = myBll_tab_Order.GetModel("ID=" + strweixinOrderID);


                EggsoftWX.BLL.tab_ShopClient myBLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient myModel_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                myModel_tab_ShopClient = myBLL_tab_ShopClient.GetModel("id=" + myModel_tab_Order.ShopClient_ID);
                string strDescription = "修改金额成功:\n" + strGoodName + "，修改后的金额是¥" + strTxtNewMoney + " \nby " + myModel_tab_ShopClient.ShopClientName;


                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = myModel_tab_ShopClient.ShopClientName + "" + " 更改价格通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(myModel_tab_Orderdetails.GoodID));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);


                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, "https://" + myModel_tab_ShopClient.ErJiYuMing + "/cart_good.aspx");
                WeiXinTuWens_ArrayList.Add(First);


                EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User myModel_tab_User = myBLL_tab_User.GetModel((myModel_tab_Order.UserID));

                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_tab_User.ID, 0, WeiXinTuWens_ArrayList);


                //if (boolsendImage)
                //{
                Eggsoft.Common.JsUtil.ShowMsg("发送微信消息已进入处理队列！");
                //}
                #endregion


                Response.Write("<script>window.location.href=window.location.href;</script>");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }

            finally
            { }
        }

        protected void Button_PayWay_Click(object sender, EventArgs e)
        {
            string strOrderID = TextBox_OrderID.Text.Trim();
            string strTxtNewPayWay = RadioButtonList1.SelectedValue;
            if (strTxtNewPayWay.Length > 0)
            {
                //Eggsoft.Common.JsUtil.ShowMsg("PayStatus=" + strTxtNewPayWay);

                new EggsoftWX.BLL.tab_Order().Update("PayStatus=" + strTxtNewPayWay, "ID=" + strOrderID);

                Response.Write("<script>window.location.href=window.location.href;</script>");
            }
        }


        protected void Button_FaHuo_Click(object sender, EventArgs e)
        {
            string strOrderID = TextBox_OrderID.Text.Trim();
            //string strTxtNewPayWay = RadioButtonList1.SelectedValue;
            //if (strTxtNewPayWay.Length > 0)
            //{
            //    Eggsoft.Common.JsUtil.ShowMsg("DeliveryStatus=" + strTxtNewPayWay);

            new EggsoftWX.BLL.tab_Order().Update("DeliveryStatus=" + 1, "ID=" + strOrderID);

            Response.Write("<script>window.location.href=window.location.href;</script>");
            //}
        }

        protected void Button_FaHuo_Cancel_Click(object sender, EventArgs e)
        {
            string strOrderID = TextBox_OrderID.Text.Trim();

            new EggsoftWX.BLL.tab_Order().Update("DeliveryStatus=" + 0, "ID=" + strOrderID);

            Response.Write("<script>window.location.href=window.location.href;</script>");
            //}
        }


        //EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();
        //EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();

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
                }

                #endregion

                #region 生成试图
                string strsp_V_OrderSearchView_OrderSearch_New00 = Eggsoft.Common.FileFolder.ReadFile("sp_V_OrderSearchView_OrderSearch_New00.txt");
                strsp_V_OrderSearchView_OrderSearch_New00 = String.Format(strsp_V_OrderSearchView_OrderSearch_New00, strShopClientID, 0, my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss"), my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss"));

                //sp_V_OrderSearchView_OrderSearch_New00 mmmsp_V_OrderSearchView_OrderSearch_New00 = new sp_V_OrderSearchView_OrderSearch_New00();
                //bool boolDoView = mmmsp_V_OrderSearchView_OrderSearch_New00.sp_V_OrderSearchView_OrderSearch_New00_StoredProcedure(Int32.Parse(strShopClientID), my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss"), my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                #endregion

                string strtableWhereSQL = Eggsoft.Common.FileFolder.ReadFile("View_OrderSearch_SQL.txt");
                strtableWhereSQL = String.Format(strtableWhereSQL, strShopClientID, my_OrderStartTime.ToString("yyyy-MM-dd HH:mm:ss"), my_OrderEndTime.ToString("yyyy-MM-dd HH:mm:ss"), 0);

                string strWhere = " isdeleted<>1 and PayStatus=0";
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
        /// 5个文件 只要做好 一个 直接 复制即可
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

                Response.Redirect("tab_Order_Board_NeedMoney.aspx");
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