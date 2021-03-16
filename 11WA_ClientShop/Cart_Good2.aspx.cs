using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft.Common;

namespace _11WA_ClientShop
{
    public partial class Cart_Good2 : System.Web.UI.Page
    {
        private EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
        private EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        private static Object thisLockcancelthis = new Object();

        /// <summary>
        /// 待收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();

                    string type = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(type) == false)
                    {
                        if (type == "cancelthis")
                        {
                            //直接生成退款申请

                            try
                            {
                                EggsoftWX.BLL.tab_ReturnMoney bll_tab_ReturnMoney = new EggsoftWX.BLL.tab_ReturnMoney();
                                EggsoftWX.Model.tab_ReturnMoney Model_tab_ReturnMoney = new EggsoftWX.Model.tab_ReturnMoney();

                                string strOrderID = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["OrderINT"]);



                                lock (thisLockcancelthis)
                                {

                                    EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                                    EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                                    EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                                    my_Model_tab_Order = BLL_tab_Order.GetModel(strOrderID.toInt32());

                                    if (bll_tab_ReturnMoney.Exists("OrderID=" + strOrderID) == false)
                                    {

                                        string strWhereIFCancel = "id=" + strOrderID + " and datediff(dd,PayDateTime,getdate())<7 and  isdeleted<>1 and PayStatus=1  and isReceipt=0 and DeliveryText=\'\' ";
                                        bool boolCanCancel = BLL_tab_Order.Exists(strWhereIFCancel);
                                        boolCanCancel = boolCanCancel && !BLL_tab_Orderdetails.Exists("OrderID=" + strOrderID + " and Over7DaysToBeans=1");///也不能转化的
                                        #region 尝试原路退回
                                        string strReturnMoneyDesc = "";
                                        if (boolCanCancel)
                                        {

                                            strReturnMoneyDesc = Eggsoft_Public_CL.FaHuoOrderDo.OrderDoCancel(strOrderID.toInt32(), pub_Int_ShopClientID.toString(), my_Model_tab_Order.TotalMoney.toDecimal());
                                            Eggsoft.Common.debug_Log.Call_WriteLog(strReturnMoneyDesc, "申请退款", "手机端返回消息");
                                        }
                                        else
                                        {
                                            strReturnMoneyDesc = "已发货或者已收货或者超过时限.当前订单不支持在线退款,请联系客服处理";
                                        }

                                        #endregion 尝试原路退回
                                        if (strReturnMoneyDesc.Contains("原路退回成功"))
                                        {
                                            #region 注销已支付未读消息
                                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage01 = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                            bll_b011_InfoAlertMessage01.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart_good2' and Readed=0 and TypeTableID=" + my_Model_tab_Order.ID, my_Model_tab_Order.UserID, my_Model_tab_Order.ShopClient_ID);
                                            #endregion 注销已支付未读消息


                                            Model_tab_ReturnMoney.OrderID = Int32.Parse(strOrderID);
                                            Model_tab_ReturnMoney.RefundMoney = Eggsoft_Public_CL.GoodP.GetThisOrderMoneyNotIncludeYunFei(Int32.Parse(strOrderID));
                                            Model_tab_ReturnMoney.ShopClientID = Eggsoft_Public_CL.GoodP.GetShopClient_ID_From_Order_ID(Int32.Parse(strOrderID));
                                            Model_tab_ReturnMoney.ReturnMoneyContent = my_Model_tab_Order.OrderName + " 支付时间" + my_Model_tab_Order.PayDateTime.ToString() + " 用户未发货直接退款申请 " + strReturnMoneyDesc;
                                            Model_tab_ReturnMoney.ReturnMoneyTitle = "用户退款信息";
                                            Model_tab_ReturnMoney.FinanceCheck = true;
                                            bll_tab_ReturnMoney.Add(Model_tab_ReturnMoney);

                                            Eggsoft.Common.JsUtil.ShowMsg(strReturnMoneyDesc, "/mywebuy.aspx");


                                        }

                                        else
                                        {
                                            Model_tab_ReturnMoney.OrderID = Int32.Parse(strOrderID);
                                            Model_tab_ReturnMoney.RefundMoney = Eggsoft_Public_CL.GoodP.GetThisOrderMoneyNotIncludeYunFei(Int32.Parse(strOrderID));
                                            Model_tab_ReturnMoney.ShopClientID = Eggsoft_Public_CL.GoodP.GetShopClient_ID_From_Order_ID(Int32.Parse(strOrderID));
                                            Model_tab_ReturnMoney.ReturnMoneyContent = my_Model_tab_Order.OrderName + " 支付时间" + my_Model_tab_Order.PayDateTime.ToString() + " 用户未发货直接退款申请 " + strReturnMoneyDesc;
                                            Model_tab_ReturnMoney.ReturnMoneyTitle = "商户退款信息";
                                            bll_tab_ReturnMoney.Add(Model_tab_ReturnMoney);

                                            string strGetShopClientNameFromShopClientID = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(Model_tab_ReturnMoney.ShopClientID);
                                            string strInfo = strGetShopClientNameFromShopClientID + " 订单号" + Eggsoft_Public_CL.GoodP.GetOrderNum_From_OrderID(Int32.Parse(strOrderID));
                                            strInfo += Model_tab_ReturnMoney.ReturnMoneyContent;

                                            Eggsoft_Public_CL.Pub.SendEmail_AddTask("用户退款信息", "admin@eggsoft.cn", "用户退款信息", strInfo);

                                            #region 处理展示给用户的消息 json反解析 不需要事先生成对象
                                            string strDesc = "";
                                            try
                                            {
                                                dynamic myjSonm = Newtonsoft.Json.JsonConvert.DeserializeObject(strReturnMoneyDesc.Replace("退回失败,请联系统管理员：", ""));
                                                strDesc = myjSonm.Err_Code_Des;
                                            }
                                            catch (Exception eeee)
                                            {
                                                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "用户退款信息json反解析不需要事先生成对象");
                                                strDesc = "用户退款反解析失败,可能是远程服务器不能访问";
                                            }

                                            #endregion 处理展示给用户的消息

                                            Eggsoft.Common.JsUtil.ShowMsg(strDesc.toString() + "\\n未结算资金退回可能失败，可用余额退回可能失败。   \\n亲,你的退款申请已直通微店管理员,费用将原路退回！将尽快与您协调！", "/mywebuy.aspx");
                                        }
                                    }
                                    else
                                    {



                                        Model_tab_ReturnMoney = bll_tab_ReturnMoney.GetModel("OrderID=" + strOrderID);

                                        Model_tab_ReturnMoney.ReturnMoneyContent = my_Model_tab_Order.OrderName + " 支付时间" + my_Model_tab_Order.PayDateTime.ToString() + " 用户未发货重复直接退款申请";
                                        Model_tab_ReturnMoney.UpdateTime = DateTime.Now;
                                        bll_tab_ReturnMoney.Update(Model_tab_ReturnMoney);

                                        string strGetShopClientNameFromShopClientID = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(Eggsoft_Public_CL.GoodP.GetShopClient_ID_From_Order_ID(Int32.Parse(strOrderID)));
                                        string strInfo = strGetShopClientNameFromShopClientID + " 订单号" + Eggsoft_Public_CL.GoodP.GetOrderNum_From_OrderID(Int32.Parse(strOrderID));
                                        strInfo += Model_tab_ReturnMoney.ReturnMoneyContent;
                                        string strAdminURL = Eggsoft.Common.CommAuthen._Admin_GetAminURL();

                                        Eggsoft_Public_CL.Pub.SendEmail_AddTask("用户退款信息", "admin@eggsoft.cn", "用户重复退款信息", strInfo);
                                        Eggsoft.Common.JsUtil.ShowMsg("亲,你的退款申请已直通微店管理员,费用将原路退回！重复保存成功！将尽快与您协调！\\n 请不要重复提交信息！", "/mywebuy.aspx");
                                    }
                                }
                            }
                            catch (Exception Exceptione)
                            {
                                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "退款错误");
                                Eggsoft.Common.debug_Log.Call_WriteLog("退款错误：" + Exceptione, "未发货");
                            }
                            finally
                            {

                            }
                        }
                    }


                    #region 注销待收货未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart_good2' and Readed=0", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    #endregion 注销待收货未读消息

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_CartGood2_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "待收货"));

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);

                    strTemplet = strTemplet.Replace("###Header###", "");
                    if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "PC")
                    {
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    }
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                    strTemplet = InitOrders(strTemplet);
                    Response.Write(strTemplet);
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }
            }
        }


        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }
        private String InitOrders(string strTemplet)
        {
            #region InitOrders
            string strOrderCartGoods = "";
            EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods BLL_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods();
            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();

            //商户订单号
            string out_trade_no = Request.QueryString["out_trade_no"];
            ////支付宝交易号
            //string trade_no = Request.QueryString["trade_no"];
            ////交易状态
            //string result = Request.QueryString["result"];
            if (out_trade_no != null)
            {
                #region 商户订单号  正常显示信息
                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + out_trade_no.ToString() + "'");
                my_Model_tab_Order.PayStatus = 1;
                my_Model_tab_Order.UpdateDateTime = DateTime.Now;
                my_BLL_tab_Order.Update(my_Model_tab_Order);

                //string strOrderCartGoods = "";



                strOrderCartGoods += "<div class=\"pro_w_GetMoney\" style=\"padding-left:5px;\">\n";

                strOrderCartGoods += "                       订单号：" + my_Model_tab_Order.OrderNum + "<br />";
                strOrderCartGoods += "                       订单名称：" + my_Model_tab_Order.OrderName + "<br />";
                strOrderCartGoods += "                       订单金额：¥" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "<br />";
                if (my_Model_tab_Order.TotalMoney > 0)
                {
                    strOrderCartGoods += "                       支付类型：" + Eggsoft_Public_CL.Pub.gePayChineseName(my_Model_tab_Order.PayWay) + "<br />";
                }
                else
                {
                    strOrderCartGoods += "                       支付类型：代理商免支付<br />";
                }
                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
                my_Model_tab_User = my_BLL_tab_User.GetModel(Convert.ToInt32(my_Model_tab_Order.UserID));

                if (my_Model_tab_User.Address == "")
                {
                    strOrderCartGoods += "                       收货地址情况：" + "收货地址不清楚，请注意填写！" + "<br />  ";
                }
                else
                {
                    strOrderCartGoods += "                       收货地址：" + "请等待收货！" + "<br />  ";
                }

                if (my_Model_tab_Order.O2OTakedID > 0)
                {
                    strOrderCartGoods += "                       <a style=\"color:blue;\" href='/cart_good2.aspx'>立即查看待收货区域，获取取货二维码</a><br />  ";
                }
                strOrderCartGoods += "    </div>\n";
                #endregion background
            }
            else
            {
                #region background

                EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.BLL.tab_Orderdetails my_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                int userID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();

                System.Data.DataTable myDataTable = BLL_tab_Order.GetList("UserID=" + userID + " and PayStatus=1 and isReceipt=0  and isdeleted<>1 order by id desc").Tables[0];

                if (myDataTable.Rows.Count == 0)
                {
                    strOrderCartGoods = "&nbsp;&nbsp;暂无已付款信息！";
                }
                else
                {
                    #region readTable
                    EggsoftWX.BLL.tab_TuanGou_Number BLL_tab_TuanGou_Number = new EggsoftWX.BLL.tab_TuanGou_Number();
                    EggsoftWX.Model.tab_TuanGou_Number Model_tab_TuanGou_Number = new EggsoftWX.Model.tab_TuanGou_Number();


                    strOrderCartGoods += "<ul id=\"thelist\" style=\"padding-left: 0px;\" class=\"experience_list\">\n";

                    for (int inti = 0; inti < myDataTable.Rows.Count; inti++)
                    {
                        String strShopClient_ID = myDataTable.Rows[inti]["ShopClient_ID"].ToString();
                        String strUser_Address_ID = myDataTable.Rows[inti]["User_Address"].ToString();
                        String str_ID_User_O2OTakedID = myDataTable.Rows[inti]["O2OTakedID"].ToString();

                        String strOrderID = myDataTable.Rows[inti]["ID"].ToString();
                        String strOrderNum = myDataTable.Rows[inti]["OrderNum"].ToString();
                        String strDeliveryText = myDataTable.Rows[inti]["DeliveryText"].ToString();
                        String strPayDateTime = myDataTable.Rows[inti]["PayDateTime"].ToString();
                        String strgePayChineseName = Eggsoft_Public_CL.Pub.gePayChineseName(myDataTable.Rows[inti]["PayWay"].ToString());

                        Decimal allMoney = Decimal.Parse(myDataTable.Rows[inti]["TotalMoney"].ToString());

                        String strTotalMoney = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(allMoney);
                        String strOrderName = myDataTable.Rows[inti]["OrderName"].ToString();
                        DateTime DateTimeCreateDatetime = Convert.ToDateTime(myDataTable.Rows[inti]["CreateDatetime"].ToString());

                        strOrderCartGoods += "<div id=\"pro_w_" + strOrderID + "\" class=\"pro_w\">\n";
                        strOrderCartGoods += "                       <div class=\"Ordertitle\">";
                        strOrderCartGoods += "                       订单号：" + strOrderNum + "<br />";
                        strOrderCartGoods += "                       订单金额：<strong class='price'>" + strTotalMoney + "</strong><br />";
                        strOrderCartGoods += "                       支付类型：" + strgePayChineseName + "<br />";
                        strOrderCartGoods += "     </div>\n";


                        strOrderCartGoods += "     <div class=\"pro_wBuy_Button_mmm\">\n";
                        strOrderCartGoods += "     <div class=\"pro_wBuy_Button_now_\">\n";
                        if (strDeliveryText.Length > 0)
                        {
                            strOrderCartGoods += "   <a id=\"LinkButton_isExp\" class=\"modBtnColor modBtnColor_Red\" href=\"" + Pub_Agent_Path + "/qureyexp-" + strOrderID + ".aspx\">查看物流</a>          <a id=\"LinkButton_isSale\" class=\"modBtnColor modBtnColor_Red\" href=\"javascript: GetGoods(" + strOrderID + ");\">确认收货</a>\n";
                        }
                        else
                        {
                            strOrderCartGoods += "             <a id=\"LinkButton_isSale\" class=\"modBtnColor modBtnColor_Red\" href=\"javascript:void;\"> 等待发货</a>\n";
                        }

                        strOrderCartGoods += "    </div>\n";

                        /// 付款后 一天没发货的 出现 取消订单按钮
                        // /
                        DateTime mt1DateTime = DateTime.Now;
                        DateTime mt2DateTime = DateTime.Now;
                        DateTime.TryParse(strPayDateTime, out mt2DateTime);

                        TimeSpan ts1 = new TimeSpan(mt1DateTime.Ticks);
                        TimeSpan ts2 = new TimeSpan(mt2DateTime.Ticks);
                        TimeSpan ts = ts1.Subtract(ts2).Duration();
                        if (strDeliveryText.Length > 0)
                        {
                        }
                        else
                        {
                            if (ts.Days < 7)////小于7天的 才能出现取消按钮
                            {

                                strOrderCartGoods += "<div class=\"Talk_Order2\"><a href=\"javascript: CancelThis(" + strOrderID + ");\">取消</a></div>";//&nbsp;<a href=\"" + Pub_Agent_Path + "/commentsmessageme.aspx?touserid=" + "s" + strShopClient_ID + "\">留言</a></div>\n";
                            }
                            else
                            {
                            }
                        }

                        strOrderCartGoods += "    </div>\n";
                        #region OrderGoodDetail
                        strOrderCartGoods += "<div class=\"OrderGoodDetail\">\n";
                        System.Data.DataTable myDataTable_Orderdetails = my_tab_Orderdetails.GetList("OrderID=" + strOrderID + " and isdeleted<>1").Tables[0];
                        for (int intj = 0; intj < myDataTable_Orderdetails.Rows.Count; intj++)
                        {

                            String strOrderDetailID = myDataTable_Orderdetails.Rows[intj]["ID"].ToString();
                            String strGoodID = myDataTable_Orderdetails.Rows[intj]["GoodID"].ToString();
                            String strVouchersNum_List = myDataTable_Orderdetails.Rows[intj]["VouchersNum_List"].ToString();
                            String strBeans = myDataTable_Orderdetails.Rows[intj]["Beans"].ToString();
                            String strFreight = myDataTable_Orderdetails.Rows[intj]["Freight"].ToString();
                            String strGoodType = myDataTable_Orderdetails.Rows[intj]["GoodType"].ToString();
                            String strGoodTypeId = myDataTable_Orderdetails.Rows[intj]["GoodTypeId"].ToString();
                            String strGoodTypeIdBuyInfo = myDataTable_Orderdetails.Rows[intj]["GoodTypeIdBuyInfo"].ToString();
                            String strGoodName = Eggsoft_Public_CL.GoodP.GetGoodType(Int32.Parse(strGoodType)) + myDataTable_Orderdetails.Rows[intj]["GoodName"].ToString();

                            String strMoneyCredits = myDataTable_Orderdetails.Rows[intj]["MoneyCredits"].ToString();
                            String strMoneyWeBuy8Credits = myDataTable_Orderdetails.Rows[intj]["MoneyWeBuy8Credits"].ToString();

                            //String strGoodType = myDataTable_Orderdetails.Rows[intj]["GoodType"].ToString();
                            //String strGoodTypeId = myDataTable_Orderdetails.Rows[intj]["GoodTypeId"].ToString();
                            //String strGoodTypeIdBuyInfo = myDataTable_Orderdetails.Rows[intj]["GoodTypeIdBuyInfo"].ToString();


                            if (strBeans == "0") strBeans = "";//这里清空 有利于后面检查
                            if (strMoneyCredits == "0.00") strMoneyCredits = "";//这里清空 有利于后面检查
                            if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";//这里清空 有利于后面检查
                            int intOrderCount = Int32.Parse(myDataTable_Orderdetails.Rows[intj]["OrderCount"].ToString());


                            Decimal GoodPrice = Decimal.Parse(myDataTable_Orderdetails.Rows[intj]["GoodPrice"].ToString());
                            String strGoodPrice = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(GoodPrice);
                            if ((strGoodType == "1") || (strGoodType == "2") || (strGoodType == "3"))
                            {
                                string strReturnGoodPrice = ""; Decimal dec_Good_Money = 0;
                                Eggsoft_Public_CL.ShoppingCart.getGoodPrice(Int32.Parse(strGoodType), Int32.Parse(strGoodTypeId), intOrderCount, strGoodTypeIdBuyInfo, out strReturnGoodPrice, out dec_Good_Money);
                                Decimal DecimalReturnGoodPrice = 0;
                                Decimal.TryParse(strReturnGoodPrice, out DecimalReturnGoodPrice);
                                strGoodPrice = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalReturnGoodPrice);
                            }


                            //---计算购物券
                            Decimal allVouchersMoneyMoney = 0; Decimal allMoneyMoney = 0;
                            if (String.IsNullOrEmpty(strVouchersNum_List) == false)
                            {
                                string[] strEachList = strVouchersNum_List.Split(',');
                                for (int k = 0; k < strEachList.Length; k++)
                                {
                                    if (String.IsNullOrEmpty(strEachList[k]) == false)
                                    {
                                        string[] strEachListString = strEachList[k].Split('#');
                                        String strVouchersMoney = strEachListString[1];
                                        allVouchersMoneyMoney += Decimal.Parse(strVouchersMoney);
                                    }
                                }

                            }
                            else if (String.IsNullOrEmpty(strBeans) == false)
                            {
                                allVouchersMoneyMoney = Decimal.Multiply(Decimal.Parse(strBeans), (Decimal)0.01);
                            }
                            //else if (String.IsNullOrEmpty(strMoneyCredits) == false)
                            //{
                            //    allVouchersMoneyMoney = Decimal.Parse(strMoneyCredits);
                            //}
                            else if (String.IsNullOrEmpty(strMoneyWeBuy8Credits) == false)
                            {
                                allVouchersMoneyMoney = Decimal.Parse(strMoneyWeBuy8Credits);
                            }
                            if (String.IsNullOrEmpty(strMoneyCredits) == false)
                            {
                                allMoneyMoney = Decimal.Parse(strMoneyCredits);
                            }

                            //---计算运费
                            Decimal allFreightMoney = 0;
                            if (String.IsNullOrEmpty(strFreight) == false)
                            {
                                allFreightMoney = Decimal.Parse(strFreight);
                            }
                            String strGoodTotalMoney = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(GoodPrice * intOrderCount - allVouchersMoneyMoney - allMoneyMoney + allFreightMoney);


                            string strGoodLink = ""; string strTuanGouStatus = "";
                            if (strGoodType == "0")
                            {
                                strGoodLink = Pub_Agent_Path + "/product-" + strGoodID + ".aspx";
                            }
                            else if (strGoodType == "1")
                            {
                                strGoodLink = "/Huodong/WeiKanJia/default.html?kanjiaid=" + strGoodTypeId;
                            }
                            else if (strGoodType == "2")
                            {
                                strGoodLink = "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId;
                                int intGoodTypeIdBuyInfo = 0;
                                int.TryParse(strGoodTypeIdBuyInfo, out intGoodTypeIdBuyInfo);
                                Model_tab_TuanGou_Number = BLL_tab_TuanGou_Number.GetModel(intGoodTypeIdBuyInfo);
                                if (Model_tab_TuanGou_Number != null)
                                {
                                    if (Model_tab_TuanGou_Number.Efficacy == false)
                                    {
                                        strTuanGouStatus = "已失效";
                                    }
                                    else if (Model_tab_TuanGou_Number.IFFinshedCurMemberShip == false)
                                    {
                                        strTuanGouStatus = "未拼团结束";
                                    }
                                    else
                                    {
                                        strTuanGouStatus = "拼团成功";
                                    }
                                }
                            }
                            else if (strGoodType == "3")
                            {
                                strGoodLink = "/addfunction/04zc_project/03zc.html?zcid=" + strGoodTypeId;
                            }
                            strOrderCartGoods += "              <a href=\"" + strGoodLink + "\" title=\"" + strOrderDetailID + "\">\n";
                            strOrderCartGoods += "                <div class=\"eachLine\" style=\"font-size:12px;\">     <div class=\"eachItemName\" style=\"width:44%;\">" + Eggsoft.Common.StringNum.MaxLengthString(strTuanGouStatus + " " + strGoodName.Trim(), 12) + "</div>\n";
                            strOrderCartGoods += "                    <div class=\"eachItem\">" + strGoodPrice + "</div>\n";
                            strOrderCartGoods += "                    <div class=\"eachItemShort\" style=\"width:14%;\">" + intOrderCount.ToString() + "</div>\n";
                            strOrderCartGoods += "                    <div class=\"eachItem\" style=\"width:14%;\">" + strGoodTotalMoney + " </div>\n";
                            strOrderCartGoods += "                                      \n";
                            strOrderCartGoods += "                </div></a>\n";

                        }
                        strOrderCartGoods += "  </div>\n"; //end OrderGoodDetail


                        #endregion OrderGoodDetail
                        #region 收货地址
                        strOrderCartGoods += "<div class='UserShouHuoDiZhi'>\n";
                        EggsoftWX.BLL.tab_User_Address my_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();


                        #region 收货地址的补丁包
                        if (String.IsNullOrEmpty(str_ID_User_O2OTakedID) == false)
                        {
                            if (BLL_tab_ShopClient_O2O_TakeGoods.Exists("ID=" + str_ID_User_O2OTakedID + " and ISDeleted=0"))
                            {
                                EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods Model_tab_ShopClient_O2O_TakeGoods = BLL_tab_ShopClient_O2O_TakeGoods.GetModel(Int32.Parse(str_ID_User_O2OTakedID));

                                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(Model_tab_ShopClient_O2O_TakeGoods.TakeO2OShopID);
                                strOrderCartGoods += "上门自取：" + Model_tab_ShopClient_O2O_ShopInfo.ShopName + " " + Model_tab_ShopClient_O2O_ShopInfo.ShopAdress + " 营业时间：" + Model_tab_ShopClient_O2O_ShopInfo.ShopDayTime + "  <br /> 自取时间：" + Model_tab_ShopClient_O2O_TakeGoods.TakeDateTime.ToString("yyyy-MM-dd HH:mm") + "\n";
                                strOrderCartGoods += "<div class=\"pro_wBuy_Button_mmm\"><div class=\"pro_wBuy_Button_now_\"><a id=\"LinkButton_GetSale_" + str_ID_User_O2OTakedID + "\" style=\"width:auto;margin:0px auto;float:none;\" class=\"modBtnColor modBtnColor_Red\" href=\"" + Pub_Agent_Path + "/cart_good2_o2o_book.aspx?type=takegood&orderid=" + strOrderID + "\">取货凭证二维码(请出示该二维码给o2o店主)</a></div></div>\n";
                            }
                        }
                        else
                        {
                            if ((String.IsNullOrEmpty(strUser_Address_ID) == true) || strUser_Address_ID == "0")
                            {
                                string strDefault_Address = "";
                                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(userID);
                                strDefault_Address = Model_tab_User.Default_Address.ToString();
                                BLL_tab_Order.Update("User_Address=" + strDefault_Address, "ID=" + strOrderID);
                                strUser_Address_ID = strDefault_Address;
                            }
                            if (String.IsNullOrEmpty(strUser_Address_ID) == false)
                            {
                                if (my_tab_User_Address.Exists("ID=" + strUser_Address_ID))
                                {
                                    strOrderCartGoods += "用户收货地址：" + my_tab_User_Address.GetList("XiangXiDiZhi", "ID=" + strUser_Address_ID).Tables[0].Rows[0]["XiangXiDiZhi"].ToString() + "<br />  \n";
                                }
                            }
                            if (strDeliveryText.Length <= 0)//商家还没有发货  发货了 这里就不会出现。
                            {
                                strOrderCartGoods += "<div class='addrtit'><a id=\"HyperLink_Write_Address\" class=\"title_Link\" href=\"" + Pub_Agent_Path + "/cart_self.aspx?type=modifyorder&modifyorderid=-" + strOrderID + "\">更改收货地址</a></div>\n";
                            }
                        }
                        #endregion
                        strOrderCartGoods += "    </div>\n";

                        #endregion

                        #region OrderGoodFahuoInfo

                        if (strDeliveryText.Length > 0)
                        {
                            strOrderCartGoods += "      <div class=\"OrderGoodFahuoInfo\">\n";

                            string getGetFaHuoXML = "";
                            getGetFaHuoXML = Server.HtmlDecode(strDeliveryText);
                            string getGetFaHuoTitleXML = "";
                            if (getGetFaHuoXML.Trim().Length > 0)
                            {

                                try
                                {
                                    Eggsoft_Public_CL.XML__Class_FahuoDan myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_FahuoDan>(getGetFaHuoXML, System.Text.Encoding.UTF8);
                                    getGetFaHuoTitleXML += "<strong class=\"tit\">商户发货信息：</strong>" + "<br />\n";
                                    getGetFaHuoTitleXML += "发送单位：" + myFahuoDan.FaHuoGongSi + "<br />\n";
                                    getGetFaHuoTitleXML += "运单号：" + myFahuoDan.FaHuoDanHao + "<br />\n";
                                    getGetFaHuoTitleXML += "收货人姓名：" + myFahuoDan.ShouHuoRenXinMing + "<br />\n";
                                    //getGetFaHuoTitleXML += "收货人电话：" + myFahuoDan.ShouHuoRenDianHua + "<br />\n";
                                    //getGetFaHuoTitleXML += "收货人地址：" + myFahuoDan.ShouHuoRenDiZhi + "<br />\n";
                                    //getGetFaHuoTitleXML += "发货人姓名：" + myFahuoDan.FaHuoRenXingMing + "<br />\n";
                                    //getGetFaHuoTitleXML += "发货人电话：" + myFahuoDan.FaHuoRenXDianHua + "<br />\n";
                                    //getGetFaHuoTitleXML += "发货人地址：" + myFahuoDan.FaHuoRenDiZhi;

                                }
                                catch { }
                                finally { }
                            }
                            strOrderCartGoods += getGetFaHuoTitleXML;
                            strOrderCartGoods += "      </div>\n";
                        }
                        #endregion strOrderCartGoods



                        strOrderCartGoods += " </div>\n";//end prow 
                    }

                    #endregion readTable
                }
                strOrderCartGoods += "</ul>\n";

                //Literal_GetCart.Text = strOrderCartGoods;
                #endregion
            }
            strTemplet = strTemplet.Replace("###WaitPayList###", strOrderCartGoods);
            return strTemplet;
            #endregion InitOrders
        }
    }
}