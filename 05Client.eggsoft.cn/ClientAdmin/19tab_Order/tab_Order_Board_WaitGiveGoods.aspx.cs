using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace _05Client.eggsoft.cn.ClientAdmin._19tab_Order
{
    public partial class tab_Order_Board_WaitGiveGoods : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strTextBox_OrderStartTime = "";
        public string strTextBox_OrderEndTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_WaitGiveGoods")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                try
                {
                    string type = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(type) == false)
                    {
                        if (type == "CancelThis")
                        {
                            int OrderINT = Convert.ToInt32(Request.QueryString["OrderINT"]);//订单记录的ID




                            Decimal DecimalReturnMoney = Request.QueryString["ReturnMoney"].toDecimal();
                            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                            string strReturnMoneyDesc = Eggsoft_Public_CL.FaHuoOrderDo.OrderDoCancel(OrderINT, strShopClientID, DecimalReturnMoney);
                            Eggsoft.Common.JsUtil.TipAndRedirect(strReturnMoneyDesc, "/ClientAdmin/19tab_Order/tab_Order_Board_WaitGiveGoods.aspx", 3);
                            Response.End();
                        }
                    }

                    intiSQLSession();
                    BindBigClass();
                    BindBig_WhichCompany();
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
            String strCustomerName = "可能未关注";
            String strNickname = Model_tab_User.NickName;
            if (String.IsNullOrEmpty(strNickname) == false) strCustomerName = strNickname;



            return "<a title=\"发送客服消息\" target=\"_blank\" href=\"../SendMessage.aspx?UserID=" + strUserID + "&ShopClientID=" + stShopClientID + "\">" + strCustomerName + "</a>";
        }


        public string getGetFaHuoXML(string str_Eval_ID)
        {
            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Model_tab_Order = new EggsoftWX.Model.tab_Order();

            Model_tab_Order = BLL_tab_Order.GetModel(Convert.ToInt32(str_Eval_ID));

            string getGetFaHuoXML = "";
            getGetFaHuoXML = Server.HtmlDecode(Model_tab_Order.DeliveryText);
            string getGetFaHuoTitleXML = "";
            if (getGetFaHuoXML.Trim().Length > 0)
            {
                Eggsoft_Public_CL.XML__Class_FahuoDan myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_FahuoDan>(getGetFaHuoXML, System.Text.Encoding.UTF8);

                getGetFaHuoTitleXML += myFahuoDan.FaHuoGongSi + "#";
                getGetFaHuoTitleXML += myFahuoDan.FaHuoDanHao + "#";
                getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenXinMing + "#";
                getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenDianHua + "#";
                getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenDiZhi + "#";
                getGetFaHuoTitleXML += myFahuoDan.FaHuoRenXingMing + "#";
                getGetFaHuoTitleXML += myFahuoDan.FaHuoRenXDianHua + "#";
                getGetFaHuoTitleXML += myFahuoDan.FaHuoRenDiZhi;
            }
            return getGetFaHuoTitleXML;
        }

        public void BindBigClass()
        {
            AspNetPager1_PageChanged(null, null);
            AspNetPager2.UrlRewritePattern = "tab_Order_Board_WaitGiveGoods.aspx?pageIndex={0}";
        }
        public void BindBig_WhichCompany()
        {
            EggsoftWX.BLL.tab_KuaiDiCompany bll = new EggsoftWX.BLL.tab_KuaiDiCompany();
            this.DropDownList_WhichCompany.DataSource = bll.GetList("ID,KuaidiCompanyCode,KuaidiName", " 1=1 order by id asc");
            this.DropDownList_WhichCompany.DataTextField = "KuaidiName";
            this.DropDownList_WhichCompany.DataValueField = "ID";
            this.DropDownList_WhichCompany.DataBind();
            //myDataList.DataSource = 

        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            

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
                EggsoftWX.BLL.tab_Class3 blltab_Class3 = new EggsoftWX.BLL.tab_Class3();
                myDataList.DataSource = blltab_Class3.GetList("*", "Class2_ID=" + strBig2ClassID + " order by Sort asc");
                myDataList.DataBind();
            }

        }



        protected void Button_FaHuo_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderID = TextBox_OrderID.Text;
                if (DropDownList_WhichCompany.Text.Trim().Length == 0)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("发货公司不能为空，请输入快递公司或邮局名称！");
                    return;
                }
                if (TextBox_Num.Text.Trim().Length == 0)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("发货单号不能为空！");
                    return;
                }


                Eggsoft_Public_CL.XML__Class_FahuoDan myFahuoDan = new Eggsoft_Public_CL.XML__Class_FahuoDan();


                #region sendWeiXinMessage
                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("id='" + strOrderID + "'");

                EggsoftWX.BLL.tab_ShopClient myBLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient myModel_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                myModel_tab_ShopClient = myBLL_tab_ShopClient.GetModel("id=" + my_Model_tab_Order.ShopClient_ID);

                string strDescription = myModel_tab_ShopClient.ShopClientName + "已发货。\n";
                strDescription += "";
                strDescription += "发货公司：" + DropDownList_WhichCompany.SelectedItem.Text + "\n";
                strDescription += "发货单号：" + TextBox_Num.Text + "\n";
                strDescription += "收货人姓名：" + TextBox_ToName.Text + "\n";
                //strDescription += "收货人电话：" + TextBox_ToPhone.Text + "\n";
                //strDescription += "收货人地址：" + TextBox_ToAddress.Text + "\n";
                //strDescription += "发货人姓名：" + TextBox_FromName.Text + "\n";
                //strDescription += "发货人电话：" + TextBox_FromPhone.Text + "\n";
                //strDescription += "发货人地址：" + TextBox_FromAddres.Text + "\n";
                // strDescription += "   by " + Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "微店" + " " + myModel_tab_ShopClient.ShopClientName;
                strDescription += "by " + " " + myModel_tab_ShopClient.ShopClientName + "\n";
                //strDescription += "商家微店号：" + Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(myModel_tab_ShopClient.OpenID) + "。输入" + Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(myModel_tab_ShopClient.OpenID) + "#文字内容，可与商家直接对话！" + DateTime.Now + "\n";
                strDescription += DateTime.Now + "\n";

                EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User myModel_tab_User = new EggsoftWX.Model.tab_User();
                myModel_tab_User = myBLL_tab_User.GetModel(Convert.ToInt32(my_Model_tab_Order.UserID));
                string strTitle = myModel_tab_User.NickName + ":商家" + myModel_tab_ShopClient.ShopClientName + "已发货";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(Eggsoft_Public_CL.GoodP.Get_A_GoodID_From_Order_ID(Int32.Parse(strOrderID))[0]));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                //建立ArrayList对象   
                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象   
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, "https://" + myModel_tab_ShopClient.ErJiYuMing + "/cart_good2.aspx");
                WeiXinTuWens_ArrayList.Add(First);

                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_tab_User.ID, 0, WeiXinTuWens_ArrayList);
                #endregion

                // Eggsoft_Public_CL.GoodP.delivernotifyWinXinMessage(Int32.Parse(strOrderID));

                myFahuoDan.FaHuoGongSi = DropDownList_WhichCompany.SelectedItem.Text;
                myFahuoDan.FaHuoDanHao = TextBox_Num.Text;
                myFahuoDan.ShouHuoRenXinMing = TextBox_ToName.Text;
                myFahuoDan.ShouHuoRenDianHua = TextBox_ToPhone.Text;
                myFahuoDan.ShouHuoRenDiZhi = TextBox_ToAddress.Text;
                myFahuoDan.FaHuoRenXingMing = TextBox_FromName.Text;
                myFahuoDan.FaHuoRenXDianHua = TextBox_FromPhone.Text;
                myFahuoDan.FaHuoRenDiZhi = TextBox_FromAddres.Text;
                string strmyFahuoDan = Eggsoft.Common.XmlHelper.XmlSerialize(myFahuoDan, System.Text.Encoding.UTF8);


                #region 注销待付款 未读消息
                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and (Type='Info_cart' or Type='Info_cart_good') and Readed=0", my_Model_tab_Order.UserID, my_Model_tab_Order.ShopClient_ID);
                #endregion 注销待付款 未读消息

                #region 增加已发货未处理信息
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                Model_b011_InfoAlertMessage.InfoTip = "商家发货成功";
                Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                Model_b011_InfoAlertMessage.UserID = my_Model_tab_Order.UserID;
                Model_b011_InfoAlertMessage.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                Model_b011_InfoAlertMessage.Type = "Info_cart_good2";///用户待收货
                Model_b011_InfoAlertMessage.TypeTableID = strOrderID.toInt32();
                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                #endregion 增加已发货未处理信息  

                ///检查是否重置聚合的物流数据。订单号不变 就不重新读取
                //string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                //Eggsoft_Public_CL.GoodP.ResetHTTPjuheWuLiu(myFahuoDan.FaHuoDanHao, Int32.Parse(strOrderID), Int32.Parse(strShopClientID));

                new EggsoftWX.BLL.tab_Order().Update("DeliveryText=@DeliveryText,UpdateDateTime=getdate()", "ID=@ID" , Server.HtmlEncode(strmyFahuoDan), strOrderID);



                Response.Write("<script language='javascript'>alert('发货成功，将自动跳转！');window.location.href='/ClientAdmin/19tab_Order/tab_Order_Board_Wait_UserGetGoods.aspx?ini=true';</script>");

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "19tab_Order", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "19tab_Order");
            }
            finally
            {

            }
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

                string strWhere = " isdeleted<>1 and PayStatus=1  and isReceipt=0 and DeliveryText=\'\' ";
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
        /// 5个文件 只要做好 一个 直接 复制即可     注意  Response.Redirect("tab_Order_Board_WaitGiveGoods.aspx");
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            try
            {
                string strSessionWhere = "";

                if (string.IsNullOrEmpty(TextBox_ShopUserID.Text) == false)
                {
                    strSessionWhere += " and (ShopUserID=" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_ShopUserID.Text.Trim()) + ")";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_ShopUserID", Eggsoft.Common.CommUtil.SafeFilter(TextBox_ShopUserID.Text.Trim()));


                if (string.IsNullOrEmpty(TextBox_UserInfo.Text) == false)
                {
                    strSessionWhere += " and (UserRealName like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()) + "%' or address_RealName like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()) + "%' or UserNickName like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()) + "%')";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_RealName", Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserInfo.Text.Trim()));
                if (string.IsNullOrEmpty(TextBox_UserAddress.Text) == false)
                {
                    strSessionWhere += " and address_XiangXiDiZHi like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserAddress.Text.Trim()) + "%'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_XiangXiDiZHi", Eggsoft.Common.CommUtil.SafeFilter(TextBox_UserAddress.Text.Trim()));
                if (string.IsNullOrEmpty(TextBox_Tel.Text) == false)
                {
                    strSessionWhere += " and (TakePhone like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()) + "%' or address_TelPhone like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()) + "%' or address_MobilePhone like '%" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()) + "%')";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere_TelPhone", Eggsoft.Common.CommUtil.SafeFilter(TextBox_Tel.Text.Trim()));
                if (string.IsNullOrEmpty(TextBox_PayPrice.Text) == false)
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
                Eggsoft.Common.Session.Add("OrderSQLWhere__TakeGoodInfo", TextBox_TakeGoodInfo.Text);

                if (string.IsNullOrEmpty(TextBox_AgentShow.Text) == false)
                {
                    TextBox_AgentShow.Text = Eggsoft.Common.CommUtil.SafeFilter(TextBox_AgentShow.Text.Trim());
                    strSessionWhere += " and (GreatParentIDNickName like '%" + TextBox_AgentShow.Text.Trim() + "%' or GreatParentIDUserRealName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ShopNameGreatParentID like '%" + TextBox_AgentShow.Text.Trim() + "%' or GrandParentIDNickName like '%" + TextBox_AgentShow.Text + "%' or GrandParentIDUserRealName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ShopNameGrandParentID like '%" + TextBox_AgentShow.Text.Trim() + "%' or ParentIDNickName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ParentIDUserRealName like '%" + TextBox_AgentShow.Text.Trim() + "%' or ShopNameParentID like '%" + TextBox_AgentShow.Text.Trim() + "%')";
                }
                if (string.IsNullOrEmpty(TextBox_OrderNum.Text.Trim()) == false)
                {
                    strSessionWhere += " and OrderNum='" + Eggsoft.Common.CommUtil.SafeFilter(TextBox_OrderNum.Text.Trim()) + "'";
                }
                Eggsoft.Common.Session.Add("OrderSQLWhere__OrderNum", TextBox_OrderNum.Text);



                Eggsoft.Common.Session.Add("OrderSQLWhere___AgentShow", TextBox_AgentShow.Text.Trim());

                Eggsoft.Common.Session.Add("OrderSQLWhere", strSessionWhere);

                Response.Redirect("tab_Order_Board_WaitGiveGoods.aspx");
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