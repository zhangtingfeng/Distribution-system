using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft.Common;

namespace _11WA_ClientShop
{
    public partial class Cart : System.Web.UI.Page
    {
        private EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
        private EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

        protected string ShengCityCountry30PercentOr50 = "48";

        private string BuyNow_FromGoods_strDefault_AddressID = "0";



        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        //protected string Pub_DirectoryCart_Name = "";

        private static Object thisLockClearShoppingCart = new Object();

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
                        if (type == "clearshoppingcart")
                        {
                            lock (thisLockClearShoppingCart)
                            {
                                Eggsoft_Public_CL.ShoppingCart.ClearShoppingCart(true, pub_Int_Session_CurUserID, "用户清空购物车");
                                Eggsoft.Common.JsUtil.ShowMsg("购物车已清空。");
                                Eggsoft.Common.JsUtil.LocationNewHref("/cart.aspx");
                            }
                        }
                        else if (type == "buynow")//继续旧版的支付 新版的前端直接跳了
                        {
                            BuyNow();
                            Response.End();
                        }
                        else if (type == "buynowfromgood")
                        {
                            WaitShowPage();
                        }
                    }
                    else
                    {
                        WaitShowPage();
                    }
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
        private void WaitShowPage()
        {
            string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_Cart_Templet.html");
            strTemplet = strTemplet.Replace("###SAgentPath###", Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID));
            strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码

            string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
            strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
            strTemplet = strTemplet.Replace("###str_pub_Int_Session_CurUserID###", pub_Int_Session_CurUserID.ToString());
            strTemplet = strTemplet.Replace("###pub_Int_ShopClientID###", pub_Int_ShopClientID.ToString());
            strTemplet = strTemplet.Replace("###ServicesURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL());
            strTemplet = strTemplet.Replace("###varglobalcheckPayWay###", Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "V3_js_API") ? "V3_js_API" : "Oldjs_APIPay");

            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
            strTemplet = strTemplet.Replace("###UserIDContactName###", Model_tab_User.ContactMan);
            strTemplet = strTemplet.Replace("###UserIDMobile###", Model_tab_User.ContactPhone);

            string type = Request.QueryString["type"];
            if (string.IsNullOrEmpty(type) == false)
            {
                if (type == "buynowfromgood")
                {
                    strTemplet = strTemplet.Replace("###str_pub_OneKeyQuickPay###", Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "OneKeyQuickPay").ToString());//启用一键快捷支付（客户点击商品页面的立即购买，可快捷弹出微信支付（如果条件具备、譬如收货地址已存在的情况下））
                }
            }



            strTemplet = strTemplet.Replace("###HowToGetProduct###", o2o_Select_Subscribe());
            strTemplet = strTemplet.Replace("###Header###", "");
            strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


            strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

            strTemplet = InitContact_Address(strTemplet);
            Response.Write(strTemplet);


            #region 注销购物车未读消息
            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
            bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart' and Readed=0", pub_Int_Session_CurUserID, Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(pub_Int_Session_CurUserID.toString()));
            #endregion 注销购物车未读消息
        }


        private string o2o_Select_Subscribe()
        {
            string stro2o_Select_Subscribe = "";

            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
            bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + pub_Int_ShopClientID);




            if (boolExsit)
            {
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                int intHowToGetProduct = Convert.ToInt32(Model_tab_User.HowToGetProduct);

                stro2o_Select_Subscribe += "      <script type=\"text/javascript\">\n";
                stro2o_Select_Subscribe += "        $(document).ready(function(){\n";
                stro2o_Select_Subscribe += "        intInThisChoiceClick(" + intHowToGetProduct + ");\n";
                stro2o_Select_Subscribe += "        \n";
                stro2o_Select_Subscribe += "    });\n";
                stro2o_Select_Subscribe += " </script> \n";

                if (intHowToGetProduct == 0)
                {
                    stro2o_Select_Subscribe += "  <div id=\"HowToGetProduct\" class=\"spro_c2\">\n";
                    stro2o_Select_Subscribe += "     <span style=\"float:left; line-height:34px; height:34px;\">取货方式:</span>\n";
                    stro2o_Select_Subscribe += "      <a href=\"javascript:void(0)\" title=\"2166\"><span title=\"0\">快速配送</span></a>\n";
                    stro2o_Select_Subscribe += "      <a href=\"javascript:void(0)\" title=\"2167\"><span title=\"1\">上门自提</span></a>\n";
                    stro2o_Select_Subscribe += "  </div>\n";
                }
                else if (intHowToGetProduct == 1)
                {
                    stro2o_Select_Subscribe += "  <div id=\"HowToGetProduct\" class=\"spro_c2\">\n";
                    stro2o_Select_Subscribe += "     <span style=\"float:left; line-height:34px; height:34px;\">取货方式:</span>\n";
                    stro2o_Select_Subscribe += "      <a href=\"javascript:void(0)\" title=\"2166\"><span title=\"0\">快速配送</span></a>\n";
                    stro2o_Select_Subscribe += "      <a href=\"javascript:void(0)\" title=\"2167\"><span title=\"1\">上门自提</span></a>\n";
                    stro2o_Select_Subscribe += "  </div>\n";
                }
            }
            return stro2o_Select_Subscribe;
        }


        private static object ojbstaticLock = new object();
        /// <summary>
        /// 购物车 到 待付款页面
        /// </summary>
        protected void BuyNow()//继续旧版的支付 新版的前端直接跳了
        {
            try
            {

                #region 检查页面输入条件
                int intUser_Address = 0;
                int intgetdGeto2oShop = 0;
                int intAdd_tab_ShopClient_O2O_TakeGoods = 0;

                EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                System.Data.DataTable dt_DataTable_ShopingCart = my_tab_Order_ShopingCart.GetList("UserID=" + pub_Int_Session_CurUserID + " and IsDeleted<>1").Tables[0];

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                int Int_HowToGetProduct = Convert.ToInt32(Model_tab_User.HowToGetProduct);
                if (Int_HowToGetProduct == 0)///快速配送的
                {
                    #region 快速配送的
                    string getShouHuoDiZhi = Request.Form["RadioButtonList_Address"];
                    if (String.IsNullOrEmpty(getShouHuoDiZhi) == false)
                    {
                        intUser_Address = Convert.ToInt32(getShouHuoDiZhi);
                    }
                    else if (String.IsNullOrEmpty(BuyNow_FromGoods_strDefault_AddressID) == false)
                    {
                        intUser_Address = Convert.ToInt32(BuyNow_FromGoods_strDefault_AddressID);
                    }
                    if (intUser_Address == 0)//支付时必须有收货地址
                    {
                        bool boolPayMoneyMustHaveAddress = false;
                        EggsoftWX.BLL.tab_ShopClient_ShopPar my_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                        EggsoftWX.Model.tab_ShopClient_ShopPar my_Model_tab_ShopClient_ShopPar = my_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + pub_Int_ShopClientID);
                        if (my_Model_tab_ShopClient_ShopPar != null)
                        {
                            boolPayMoneyMustHaveAddress = my_Model_tab_ShopClient_ShopPar.PayMoneyMustHaveAddress.toBoolean();
                        }
                        if (boolPayMoneyMustHaveAddress)
                        {
                            Eggsoft.Common.JsUtil.ShowMsg("请先输入收货地址", Pub_Agent_Path + "/cart_self.aspx?paymoney=paymoneymusthaveaddress");
                        }
                    }



                    #endregion
                }
                else if (Int_HowToGetProduct == 1)///上门自取的
                {
                    string strGetZitiRenName = Request.Form["ZitiRenName"];
                    string strGetZitiRenMobile = Request.Form["ZitiRenMobile"];
                    string strZitiRenDate = Request.Form["ZitiRenDate"];
                    string strZitiRenTime = Request.Form["ZitiRenTime"];

                    if (strGetZitiRenName.Length == 0)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("输入自取人姓名", -1);
                        return;
                    }
                    try
                    {
                        Convert.ToInt64(strGetZitiRenMobile.Trim());
                        //return;
                    }
                    catch (Exception ex)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("输入手机号格式不正确", -1);
                        return;
                    }
                    if (strGetZitiRenMobile.Length != 11)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("手机号长度必须是11位", -1);
                        return;
                    }

                    string getdGeto2oShop = Request.Form["RadioButtonList_dGeto2oShop"];
                    if (String.IsNullOrEmpty(getdGeto2oShop) == false)
                    {
                        intgetdGeto2oShop = Convert.ToInt32(getdGeto2oShop);
                    }
                    string[] strDateList = strZitiRenDate.Split('-');
                    string[] strTimeList = strZitiRenTime.Split(':');
                    DateTime dtTakeDateTime = new DateTime(Int32.Parse(strDateList[0]), Int32.Parse(strDateList[1]), Int32.Parse(strDateList[2]), Int32.Parse(strTimeList[0]), Int32.Parse(strTimeList[1]), 0);

                    EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods BLL_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods();
                    EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods Model_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods();
                    Model_tab_ShopClient_O2O_TakeGoods.TakeName = strGetZitiRenName;
                    Model_tab_ShopClient_O2O_TakeGoods.HadTaked = 0;
                    Model_tab_ShopClient_O2O_TakeGoods.TakePhone = strGetZitiRenMobile;
                    Model_tab_ShopClient_O2O_TakeGoods.TakeDateTime = dtTakeDateTime;
                    Model_tab_ShopClient_O2O_TakeGoods.TakeO2OShopID = intgetdGeto2oShop;
                    Model_tab_ShopClient_O2O_TakeGoods.UserID = pub_Int_Session_CurUserID;
                    intAdd_tab_ShopClient_O2O_TakeGoods = BLL_tab_ShopClient_O2O_TakeGoods.Add(Model_tab_ShopClient_O2O_TakeGoods);


                }
                #endregion

                #region  do  购物车  到  待付款

                lock (ojbstaticLock)
                {
                    if (dt_DataTable_ShopingCart.Rows.Count == 0)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("购物车清空", "javascript:history.back();");
                        return;
                    }

                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();

                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                    EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                    string strOrderNum = "";

                    EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                    int intID = my_BLL_tab_Order.Add(my_Model_tab_Order);///先用存储过程插入  然后在更新 防止重复插入相同的ID
                    my_Model_tab_Order = my_BLL_tab_Order.GetModel(intID);
                    strOrderNum = DateTime.Now.ToString("yyyyMMddHHmmss") + Eggsoft.Common.StringNum.Add000000Num(intID, 2);
                    my_Model_tab_Order.OrderNum = strOrderNum;
                    //Decimal myDecimal = 0;
                    String strOrderName = "";

                
                    #region 解决 一个商品颜色 不同 首件运费的问题
                    //解决 一个商品颜色 不同 首件运费的问题
                    List<int> myProductIDList = new List<int>();

                    #endregion
                    int intOrderNameStatus = 0;
                    ArrayList allCartFullViewList = new ArrayList();
                    for (int i = 0; i < dt_DataTable_ShopingCart.Rows.Count; i++)
                    {
                        #region 循环
                        String strtab_Order_ShopingCartID = dt_DataTable_ShopingCart.Rows[i]["ID"].ToString();
                        String strGoodID = dt_DataTable_ShopingCart.Rows[i]["GoodID"].ToString();
                       

                        EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(my_Model_tab_Order.ShopClient_ID, pub_Int_Session_CurUserID.toInt32(), strGoodID.toInt32());
                        int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();
                        
                        string strTeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(pub_Int_Session_CurUserID.toString()).toString();
                        String strParentID = (intLength > 0) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(pub_Int_Session_CurUserID).toString() : "";
                        String strGrandParentID = (intLength > 1) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(strParentID.toInt32()).toString() : "";
                        String strGreatParentID = (intLength > 2) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(strGrandParentID.toInt32()).toString() : "";

                        //strParentID = dt_DataTable_ShopingCart.Rows[i]["ParentID"].ToString();
                        //strGrandParentID = dt_DataTable_ShopingCart.Rows[i]["GrandParentID"].ToString();
                        //strGreatParentID = dt_DataTable_ShopingCart.Rows[i]["GreatParentID"].ToString();
                        String strMultiBuyType = dt_DataTable_ShopingCart.Rows[i]["MultiBuyType"].ToString();
                        String strVouchersNum_List = dt_DataTable_ShopingCart.Rows[i]["VouchersNum_List"].ToString();
                        String strBeans = dt_DataTable_ShopingCart.Rows[i]["Beans"].ToString();
                        String strMoneyCredits = dt_DataTable_ShopingCart.Rows[i]["MoneyCredits"].ToString();
                        String strMoneyWeBuy8Credits = dt_DataTable_ShopingCart.Rows[i]["MoneyWeBuy8Credits"].ToString();
                        String strMoneyWealth = dt_DataTable_ShopingCart.Rows[i]["WealthMoney"].ToString();
                        String strGoodType = dt_DataTable_ShopingCart.Rows[i]["GoodType"].ToString();
                        String strGoodTypeId = dt_DataTable_ShopingCart.Rows[i]["GoodTypeId"].ToString();
                        String strGoodTypeIdBuyInfo = dt_DataTable_ShopingCart.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                        String strUserSay = dt_DataTable_ShopingCart.Rows[i]["UserSay"].ToString();

                        int intGood = Convert.ToInt32(strGoodID);


                        if (strBeans == "0") strBeans = "";//这里清空 有利于后面检查
                        if (strMoneyCredits == "0.00") strMoneyCredits = "";//这里清空 有利于后面检查
                        if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";//这里清空 有利于后面检查
                        if (strMoneyWealth == "0.00") strMoneyWealth = "";//这里清空 有利于后面检查

                        EggsoftWX.BLL.tab_Orderdetails my_BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();


                        String strGoodID_Count = dt_DataTable_ShopingCart.Rows[i]["GoodIDCount"].ToString();

                        my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Convert.ToInt32(strGoodID));
                        string strGoodName = my_Model_tab_Goods.Name;
                        string strSingleMoney = "";


                        bool boolIFSecondBuy = false;
                        EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = Eggsoft_Public_CL.GoodP.GetSecondBuyInfoPrice(Int32.Parse(strGoodID), out boolIFSecondBuy);

                        if (boolIFSecondBuy)
                        {
                            if (Model_View_SecondSalesGoodList != null)
                            {
                                strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Model_View_SecondSalesGoodList.LimitTimerBuy_TimePrice);//秒杀 并在期限内
                                strGoodName = strGoodName + "秒杀";
                            }
                            else
                            {
                                strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));//秒杀 不在期限内
                            }
                        }
                        else if ((strMultiBuyType != "0") && (strMultiBuyType != ""))//multi price
                        {
                            EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice BLL_tab_Goods_MultiSelectTypePrice = new EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice();
                            EggsoftWX.Model.tab_Goods_MultiSelectTypePrice Model_tab_Goods_MultiSelectTypePrice = BLL_tab_Goods_MultiSelectTypePrice.GetModel(Int32.Parse(strMultiBuyType));
                            if (Model_tab_Goods_MultiSelectTypePrice != null)
                            {
                                strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_Goods_MultiSelectTypePrice.GoodPrice);
                                strGoodName = strGoodName + Model_tab_Goods_MultiSelectTypePrice.GoodMultiName;
                            }
                            else
                            {
                                strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                            }
                        }
                        else if (strGoodType == "2")///微团购  
                        {
                            EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                            EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Int32.Parse(strGoodTypeId));
                            if (Model_tab_TuanGou != null)
                            {
                                strGoodName = "团购 " + strGoodName;
                            }
                            if (String.IsNullOrEmpty(strGoodTypeIdBuyInfo) == false)
                            {
                                strGoodName += "团员拼团编号:" + strGoodTypeIdBuyInfo;
                            }

                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_TuanGou.EachPeoplePrice));
                        }
                        else if (strGoodType == "3")///微众筹 
                        {
                            EggsoftWX.BLL.tab_ZC_01Product BLL_tab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                            EggsoftWX.Model.tab_ZC_01Product Model_tab_ZC_01Product = BLL_tab_ZC_01Product.GetModel(Int32.Parse(strGoodTypeId));


                            EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                            EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(strGoodTypeIdBuyInfo));
                            if (Model_tab_ZC_01Product_Support != null)
                            {
                                strGoodName = "众筹 " + Model_tab_ZC_01Product_Support.Name + " " + strGoodName;
                            }
                            if (String.IsNullOrEmpty(strGoodTypeIdBuyInfo) == false)
                            {
                                strGoodName += "众筹编号:" + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strGoodTypeId), 4) + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strGoodTypeIdBuyInfo), 4);
                            }

                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_ZC_01Product_Support.SalesPrice));
                        }
                        else if (strGoodType == "1")///微砍价
                        {
                            EggsoftWX.BLL.tab_WeiKanJia_Master BLL_tab_WeiKanJia_Master = new EggsoftWX.BLL.tab_WeiKanJia_Master();
                            EggsoftWX.Model.tab_WeiKanJia_Master Model_tab_WeiKanJia_Master = BLL_tab_WeiKanJia_Master.GetModel(Int32.Parse(strGoodTypeIdBuyInfo));

                            Decimal deMoney = 0;
                            if (Model_tab_WeiKanJia_Master != null)
                            {
                                deMoney = Convert.ToDecimal(Model_tab_WeiKanJia_Master.NowPrice);
                                strGoodName = "砍价 " + strGoodName;
                            }
                            if (String.IsNullOrEmpty(strGoodTypeIdBuyInfo) == false)
                            {
                                strGoodName += "砍价编号:" + strGoodTypeIdBuyInfo;
                            }

                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(deMoney);
                        }
                        else if (strGoodType == "6")///运营中心的
                        {
                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));

                        }
                        else
                        {
                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                        }
                        EggsoftWX.Model.tab_Orderdetails my_Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();

                        my_Model_tab_Orderdetails.GoodID = intGood;
                        my_Model_tab_Orderdetails.GoodName = strGoodName;
                        my_Model_tab_Orderdetails.GoodType = Int32.Parse(strGoodType);
                        my_Model_tab_Orderdetails.GoodTypeId = Int32.Parse(strGoodTypeId);
                        my_Model_tab_Orderdetails.GoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;

                        if (intOrderNameStatus == 0) strOrderName = strGoodName;
                        if ((intOrderNameStatus > 0) && (strOrderName.IndexOf("等") == -1)) strOrderName = strOrderName + "等";
                        intOrderNameStatus++;

                        string strGoodPrice = "";
                        Decimal outDecimal_My_Freight = 0;//处理运费问题
                        string strShowYunFei = ""; string strMultiBuyTypeName = "";
                        Eggsoft_Public_CL.AllcartYunFeiList myAllcartYunFeiList = new Eggsoft_Public_CL.AllcartYunFeiList();
                        bool boolmyProductIDListFirstDoFreight = true;
                        if (myProductIDList.Contains(intGood))
                        {
                            boolmyProductIDListFirstDoFreight = false;
                        }
                        else
                        {
                            myProductIDList.Add(intGood);
                        }

                        //////本详细订单 用户需要支付的现金（已扣除购物券+现金） 
                        Decimal dec_Good_Money_Agent = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_Price(pub_Int_Session_CurUserID, strGoodID, strGoodID_Count, strMultiBuyType, strMoneyCredits, strVouchersNum_List, strMoneyWeBuy8Credits, strMoneyWealth, strBeans, out strGoodPrice, out outDecimal_My_Freight, out strShowYunFei, boolmyProductIDListFirstDoFreight, out strMultiBuyTypeName, true, out myAllcartYunFeiList, Int32.Parse(strGoodType), Int32.Parse(strGoodTypeId), strGoodTypeIdBuyInfo);


                        my_Model_tab_Orderdetails.GoodPrice = (strGoodPrice.toDecimal());
                        //my_Model_tab_Orderdetails.FenXiaoMoney = my_Model_tab_Goods.FenXiaoMoney;
                        my_Model_tab_Orderdetails.OrderID = intID;
                        my_Model_tab_Orderdetails.OrderCount = Convert.ToInt32(strGoodID_Count);
                        my_Model_tab_Orderdetails.CreatDateTime = DateTime.Now;
                        my_Model_tab_Orderdetails.ParentID = Int32.Parse(strParentID);
                        my_Model_tab_Orderdetails.GrandParentID = Int32.Parse(strGrandParentID);
                        my_Model_tab_Orderdetails.GreatParentID = Int32.Parse(strGreatParentID);
                        my_Model_tab_Orderdetails.TeamID = strTeamID.toInt32();

                        my_Model_tab_Orderdetails.MoneyCredits = String.IsNullOrEmpty(strMoneyCredits) ? 0 : Decimal.Parse(strMoneyCredits);
                        my_Model_tab_Orderdetails.MoneyWeBuy8Credits = String.IsNullOrEmpty(strMoneyWeBuy8Credits) ? 0 : Decimal.Parse(strMoneyWeBuy8Credits);
                        my_Model_tab_Orderdetails.WealthMoney = String.IsNullOrEmpty(strMoneyWealth) ? 0 : Decimal.Parse(strMoneyWealth);
                        my_Model_tab_Orderdetails.Beans = String.IsNullOrEmpty(strBeans) ? 0 : Int32.Parse(strBeans);
                        my_Model_tab_Orderdetails.VouchersNum_List = strVouchersNum_List;
                        my_Model_tab_Orderdetails.GoodType = Int32.Parse(strGoodType);
                        my_Model_tab_Orderdetails.GoodTypeId = Int32.Parse(strGoodTypeId);
                        my_Model_tab_Orderdetails.GoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                        my_Model_tab_Orderdetails.Pinglun = strUserSay;

                               allCartFullViewList.Add(myAllcartYunFeiList);

                        my_Model_tab_Orderdetails.FreightShowText = strShowYunFei;
                        if (Int_HowToGetProduct == 1)///上门自取的
                        {
                            //dec_Good_Money_Agent = dec_Good_Money_Agent - outDecimal_My_Freight;///上门自取的运费减去；
                            my_Model_tab_Orderdetails.Freight = 0;
                        }
                        else
                        {
                            my_Model_tab_Orderdetails.Freight = outDecimal_My_Freight;
                        }
                        //myDecimal += dec_Good_Money_Agent;

                        int intOrderDetailID = my_BLL_tab_Orderdetails.Add(my_Model_tab_Orderdetails);

                        #region 更新购物券 追踪去向作用
                        //更新购物券
                        if (String.IsNullOrEmpty(strVouchersNum_List) == false)
                        {
                            string[] strEachList = strVouchersNum_List.Split(',');
                            for (int k = 0; k < strEachList.Length; k++)
                            {
                                if (String.IsNullOrEmpty(strEachList[k]) == false)
                                {
                                    string[] strEachListString = strEachList[k].Split('#');
                                    String strVouchersNum = strEachListString[0];

                                    Model_tab_Shopping_Vouchers = BLL_tab_Shopping_Vouchers.GetModel("VouchersNum='" + strVouchersNum + "'");
                                    Model_tab_Shopping_Vouchers.UserID = my_Model_tab_Order.UserID;
                                    Model_tab_Shopping_Vouchers.ShopClientID = pub_Int_ShopClientID;
                                    Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = intOrderDetailID;
                                    //Model_tab_Shopping_Vouchers.Consumed = true;//加入购物车时搞过了  这里重复写了
                                    BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                                }
                            }
                        }
                        #endregion 更新购物券 追踪去向作用


                        #region 更新财富积分 追踪去向作用
                        //更新购物券
                        if (strMoneyWealth.toDecimal() > 0)
                        {
                            EggsoftWX.BLL.b015_OrderDetail_WealthBuy BLL_b015_OrderDetail_WealthBuy = new EggsoftWX.BLL.b015_OrderDetail_WealthBuy();
                            BLL_b015_OrderDetail_WealthBuy.Update("WealthDetailID=@WealthDetailID,updatetime=getdate(),updateby='进入订单表'", "UserID=@UserID and ShopingCartID=@ShopingCartID and UseOrNotuse=1", intOrderDetailID, my_Model_tab_Order.UserID, strtab_Order_ShopingCartID);
                        }
                        #endregion 更新财富积分 追踪去向作用
                        #endregion 循环

                    }

                    #region 全场满运费
                    Decimal DecimalMaxMAXKgNoFright = 0;
                    Decimal DecimalMaxMAXMoneyNoFright = 0;
                    int intMaxMAXIntNoFright = 0;


                    Decimal DecimalAllFright = 0;
                    Decimal DecimalAllKg = 0;
                    Decimal DecimalAllMoney = 0;
                    int intAllIntN = 0;

                    for (int i = 0; i < allCartFullViewList.Count; i++)
                    {
                        Eggsoft_Public_CL.AllcartYunFeiList myEggsoft_Public_CL_AllcartYunFeiList = (Eggsoft_Public_CL.AllcartYunFeiList)allCartFullViewList[i];
                        if (myEggsoft_Public_CL_AllcartYunFeiList.FreightTempletID > 0)
                        {
                            if (myEggsoft_Public_CL_AllcartYunFeiList.MAXKgNoFright > DecimalMaxMAXKgNoFright) DecimalMaxMAXKgNoFright = myEggsoft_Public_CL_AllcartYunFeiList.MAXKgNoFright;
                            if (myEggsoft_Public_CL_AllcartYunFeiList.MAXMoneyNoFright > DecimalMaxMAXMoneyNoFright) DecimalMaxMAXMoneyNoFright = myEggsoft_Public_CL_AllcartYunFeiList.MAXMoneyNoFright;
                            if (myEggsoft_Public_CL_AllcartYunFeiList.MAXIntNoFright > intMaxMAXIntNoFright) intMaxMAXIntNoFright = myEggsoft_Public_CL_AllcartYunFeiList.MAXIntNoFright;
                        }
                        DecimalAllFright += myEggsoft_Public_CL_AllcartYunFeiList.DecimalAllFright;
                        DecimalAllKg += myEggsoft_Public_CL_AllcartYunFeiList.DecimalAllkg;
                        DecimalAllMoney += myEggsoft_Public_CL_AllcartYunFeiList.DecimalAllGoodPrice;
                        intAllIntN += myEggsoft_Public_CL_AllcartYunFeiList.GoodCount;
                    }
                    Decimal onlyGoodsPriceNoFright = DecimalAllMoney - DecimalAllFright;


                    Decimal argDecimal_My_Freight = 0; string strargYunFeiText = "";
                    Eggsoft_Public_CL.ShoppingCart.getYunFeiText_YunFeiText(DecimalMaxMAXKgNoFright, DecimalMaxMAXMoneyNoFright, intMaxMAXIntNoFright, DecimalAllKg, onlyGoodsPriceNoFright, intAllIntN, out argDecimal_My_Freight, out strargYunFeiText);
                    if (argDecimal_My_Freight > 99999999)
                    {
                        argDecimal_My_Freight = DecimalAllFright;///没有满足包邮条件  就取实际的值
                    }
                    else if (argDecimal_My_Freight == 0)
                    {
                        argDecimal_My_Freight = 0;
                    }

                    string strIfShowKg = "";//统计满多少件
                    if (DecimalAllKg > 0)
                    {
                        strIfShowKg = DecimalAllKg > 1 ? "" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllKg) + "公斤," : "" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllKg * 1000) + "克,";
                    }
                    #endregion


                    Decimal DecimalOrderMoney = 0;
                    if (Int_HowToGetProduct == 1)///上门自取的
                    {
                        //dec_Good_Money_Agent = dec_Good_Money_Agent - outDecimal_My_Freight;///上门自取的运费减去；
                        DecimalOrderMoney = onlyGoodsPriceNoFright;
                    }
                    else
                    {
                        DecimalOrderMoney = onlyGoodsPriceNoFright + argDecimal_My_Freight;
                    }


                    my_Model_tab_Order.OrderNum = strOrderNum;
                    my_Model_tab_Order.ShopClient_ID = pub_Int_ShopClientID;
                    my_Model_tab_Order.TotalMoney = DecimalOrderMoney;
                    my_Model_tab_Order.OrderName = strOrderName;
                    //my_Model_tab_Order.UserID = my_Model_tab_Order;

                    if (Int_HowToGetProduct == 0)///快速配送的
                    {
                        my_Model_tab_Order.FreightShowText = "共" + intAllIntN + "件," + strIfShowKg + "￥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllMoney) + "元,运费￥" + Eggsoft_Public_CL.Pub.getPubMoney(argDecimal_My_Freight) + ",总额￥" + Eggsoft_Public_CL.Pub.getPubMoney(onlyGoodsPriceNoFright + argDecimal_My_Freight) + strargYunFeiText + "";
                        my_Model_tab_Order.User_Address = intUser_Address;
                    }
                    else///上门自取的
                    {
                        my_Model_tab_Order.FreightShowText = "共" + intAllIntN + "件," + strIfShowKg + "￥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllMoney) + "元,运费￥0,总额￥" + Eggsoft_Public_CL.Pub.getPubMoney(onlyGoodsPriceNoFright) + "" + "";
                        my_Model_tab_Order.O2OTakedID = intAdd_tab_ShopClient_O2O_TakeGoods;
                    }


                    my_BLL_tab_Order.Update(my_Model_tab_Order);
                    //myAllDecimal += myDecimal;

                    Eggsoft_Public_CL.ShoppingCart.ClearShoppingCart(false, pub_Int_Session_CurUserID, "到待付款里面去");//到待付款里面去

                    Eggsoft_Public_CL.GoodP.tellShopClientID_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成
                    Eggsoft_Public_CL.GoodP.tellShopClientID_O2O_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成
                    Eggsoft_Public_CL.GoodP.tell_DistributionMoney_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成
                    Eggsoft_Public_CL.GoodP.tell_User_UserWillPayMoney_ByWeiXin(strOrderNum);///通知用户 即将付款通知 订单已生成

                    #region 增加待支付未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "待付款通知";
                    Model_b011_InfoAlertMessage.CreateBy = "待付款通知";
                    Model_b011_InfoAlertMessage.UpdateBy = "待付款通知";
                    Model_b011_InfoAlertMessage.UserID = pub_Int_Session_CurUserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = pub_Int_ShopClientID;
                    Model_b011_InfoAlertMessage.Type = "Info_cart_good";
                    Model_b011_InfoAlertMessage.TypeTableID = my_Model_tab_Order.ID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加待支付未处理信息 

                    if (DecimalOrderMoney > (Decimal)0.001)
                    {
                        Eggsoft.Common.JsUtil.LocationNewHref(Pub_Agent_Path + "/paychoice.aspx?ordernum=" + strOrderNum + "&myallmoney=" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalOrderMoney) + "&ordername=" + strOrderName);
                    }
                    else
                    {
                        #region 注销待付款未读消息
                        //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart_good' and Readed=0", pub_Int_Session_CurUserID, Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(pub_Int_Session_CurUserID.toString()));
                        #endregion 注销待付款未读消息


                        Eggsoft_Public_CL.GoodP.IGetMoney(strOrderNum, "WeiBaiPay", "0");
                        Eggsoft.Common.JsUtil.LocationNewHref(Pub_Agent_Path + "/cart_good2.aspx?out_trade_no=" + strOrderNum);
                    }
                }
                //Response.End();

                #endregion

            }
            catch (Exception eeeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeeee, "BuyNow购物车到待付款页面");
            }
        }


        protected String InitContact_Address(String strTemplet)
        {


            string strInitContact_Address = "";

            EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
            System.Data.DataTable my_Default_Address_DataTable = my_BLL_tab_User.GetList("Default_Address", "ID=" + pub_Int_Session_CurUserID).Tables[0];
            string strDefault_AddressID = my_Default_Address_DataTable.Rows[0]["Default_Address"].ToString();


            EggsoftWX.BLL.tab_User_Address my_BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();
            System.Data.DataTable myDataTable = my_BLL_tab_User_Address.GetList("id,XiangXiDiZhi", "UserID=" + pub_Int_Session_CurUserID + " and IsDeleted=0").Tables[0];

            strInitContact_Address += "<table id=\"RadioButtonList_Address\" class=\"RadioButtonList_Address_SmallFont\">";
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string strXiangXiDiZhi = myDataTable.Rows[i]["XiangXiDiZhi"].ToString();
                string strID = myDataTable.Rows[i]["id"].ToString();

                //ListItem newListItem = new ListItem();
                string StrCheck = "";
                if (strDefault_AddressID == strID)
                {
                    #region 保证用户的个人信息地址和当前选择的收获地址是一致的
                    #region 充值 user 的省  为了 免运费的方案  等等
                    EggsoftWX.Model.tab_User_Address my_Model_tab_User_Address = my_BLL_tab_User_Address.GetModel(Int32.Parse(strDefault_AddressID));

                    if ((string.IsNullOrEmpty(my_Model_tab_User_Address.pc_province) == false))
                    {
                        #region //后期更改 往表里插入了pc_province]      ,[pc_city]      ,[pc_district]  如果存在有效数据可以用这里的
                        EggsoftWX.Model.tab_User my_Model_tab_User = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                        if (string.IsNullOrEmpty(my_Model_tab_User_Address.pc_district) == true)////处理直辖市情况  程序的不足引起的 如上海市上海市闵行区
                        {
                            if (my_Model_tab_User.Sheng != my_Model_tab_User_Address.pc_province)
                            {
                                my_Model_tab_User.Sheng = my_Model_tab_User_Address.pc_province;
                                my_Model_tab_User.City = my_Model_tab_User_Address.pc_province;
                                my_Model_tab_User.Area = my_Model_tab_User_Address.pc_city;
                                my_BLL_tab_User.Update(my_Model_tab_User);
                            }
                        }
                        else
                        {
                            if (my_Model_tab_User.Sheng != my_Model_tab_User_Address.pc_province)
                            {
                                my_Model_tab_User.Sheng = my_Model_tab_User_Address.pc_province;
                                my_Model_tab_User.City = my_Model_tab_User_Address.pc_city;
                                my_Model_tab_User.Area = my_Model_tab_User_Address.pc_district;
                                my_BLL_tab_User.Update(my_Model_tab_User);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        string strXiangXiDiZHi = my_Model_tab_User_Address.XiangXiDiZHi;
                        string strReturnProvince = "";

                        int intZiZhiQu = strXiangXiDiZHi.IndexOf("自治区");
                        int intProvince = strXiangXiDiZHi.IndexOf("省");
                        int intCity = strXiangXiDiZHi.IndexOf("市");

                        if (intZiZhiQu > -1)
                        {
                            strReturnProvince = strXiangXiDiZHi.Substring(0, intZiZhiQu + 3);
                        }
                        else if (intProvince > -1)
                        {
                            strReturnProvince = strXiangXiDiZHi.Substring(0, intProvince + 1);
                        }
                        else if (intCity > -1)  ///上海等直辖市
                        {
                            strReturnProvince = strXiangXiDiZHi.Substring(0, intCity + 1);
                        }


                        string strUpdate = "";
                        //string strUpdate = "Default_Address=" + strXiangXiDiZhiID;
                        if (String.IsNullOrEmpty(strReturnProvince) == false)
                        {
                            strUpdate += "Sheng='" + strReturnProvince + "'";
                            my_BLL_tab_User.Update(strUpdate, "ID=" + pub_Int_Session_CurUserID);

                            #region 优化算法  以后就不会进入这里  。因为 省份的优化已写出
                            ///优化算法  以后就不会进入这里  。因为 省份的优化已写出
                            ///
                            my_Model_tab_User_Address.pc_province = strReturnProvince;
                            my_BLL_tab_User_Address.Update(my_Model_tab_User_Address);
                            #endregion
                        }
                    }
                    #endregion
                    #endregion
                    StrCheck = "checked";
                }


                strInitContact_Address += "<tr><td><label>\n";

                strInitContact_Address += "<input id=\"RadioButtonList_Address_" + i + "\" onclick=\"to_change()\" type=\"radio\" " + StrCheck + " value=\"" + strID + "\" name=\"RadioButtonList_Address\">\n";
                strInitContact_Address += strXiangXiDiZhi + "</label>\n";


                strInitContact_Address += "<td><tr>\n";
            }
            strInitContact_Address += "</table>\n";
            strTemplet = strTemplet.Replace("###ShouHuoDizhiPayList###", strInitContact_Address);

            return strTemplet;

        }

    }
}