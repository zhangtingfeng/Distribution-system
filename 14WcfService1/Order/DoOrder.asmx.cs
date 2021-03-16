using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WeiXin.Lib.Core.Helper.WXPay;
using WeiXin.Lib.Core.Helper;

namespace _14WcfS.Order
{
    /// <summary>
    /// DoOrder 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DoOrder : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        /// <summary>
        /// 添加微砍价购物车   1清空购物车 2 添加购物车 3 生成订单 修改价格 4 发起支付 
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">购买人</param>
        /// <returns></returns>
        public String _Service_AddToCart_WeiKanJia()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                String strMasterUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strMasterUserID"]);
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                String strWeiKanJiaID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strWeiKanJiaID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strTSign = (context.QueryString["TSign"]);

                #region 检查签名
                string strSafeCode = "";
                EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserID + strMasterUserID + strWeiKanJiaID + strShopClientID + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode));
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strUserID + strMasterUserID + strWeiKanJiaID + strShopClientID  strSafeCode" + strUserID + " " + strMasterUserID + " " + strWeiKanJiaID + " " + strShopClientID + " " + strSafeCode, "添加微砍价购物车签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    return "";
                }
                #endregion 检查签名
                int intMasterUserID = 0;
                int intUserID = 0;
                int intWeiKanJiaID = 0;
                int intShopClientID = 0;

                int.TryParse(strMasterUserID, out intMasterUserID);
                int.TryParse(strUserID, out intUserID);
                int.TryParse(strWeiKanJiaID, out intWeiKanJiaID);
                int.TryParse(strShopClientID, out intShopClientID);
                if ((Eggsoft_Public_CL.Pub.GetShopClientIDFromWeiKanJiaID(intWeiKanJiaID) != intShopClientID) || (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString()) != intShopClientID))
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("微砍价发起订单严重错误");
                }
                int intErrorCode = -1;
                lock ("WeikanJia201602100906")
                {
                    /// 1清空购物车 2 添加购物车 3 生成订单 修改价格 4 发起支付 
                    bool boolContinue = false;
                    if (intMasterUserID == 0) intMasterUserID = intUserID;
                    int intGoodID = 0;
                    EggsoftWX.BLL.tab_WeiKanJia_Master BLL_tab_WeiKanJia_Master = new EggsoftWX.BLL.tab_WeiKanJia_Master();
                    EggsoftWX.Model.tab_WeiKanJia_Master Model_tab_WeiKanJia_Master = BLL_tab_WeiKanJia_Master.GetModel("ShopClientID=" + intShopClientID + " and WeikanJiaID=" + intWeiKanJiaID + " and UserID=" + intUserID);
                    EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                    EggsoftWX.Model.tab_WeiKanJia Model_tab_WeiKanJia = BLL_tab_WeiKanJia.GetModel(intWeiKanJiaID);
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
                    if ((Model_tab_User != null) && (Model_tab_WeiKanJia != null))
                    {
                        boolContinue = true;
                    }
                    int INTMAxIDboolintMasterUserIDtab_WeiKanJiaNowPrice = 0;////这个价格只能使用一次

                    #region 2 添加购物车
                    #region 2 取微砍价的价格  取价格
                    decimal decimalNowPrice = 0;
                    string strErrorCode = "";
                    string strErrorDescription = "";

                    if (boolContinue)
                    {
                        boolContinue = false;
                        if (Model_tab_WeiKanJia != null)
                        {
                            if ((Model_tab_WeiKanJia.isSaled == true) && (Model_tab_WeiKanJia.isdeleted != 1))
                            {
                                #region  获取价格 如有砍价价格 就 取最后的价格  没有的话 取砍价的原价
                                //EggsoftWX.BLL.tab_WeiKanJia_Master BLL_tab_WeiKanJia_Master = new EggsoftWX.BLL.tab_WeiKanJia_Master();
                                string strWhere = "[UserID]=" + intUserID + " and  ShopClientID=" + intShopClientID + " and WeikanJiaID=" + intWeiKanJiaID + " and IsDeleted<>1 and IsBuyed<>1";
                                Model_tab_WeiKanJia_Master = BLL_tab_WeiKanJia_Master.GetModel(strWhere);
                                if (Model_tab_WeiKanJia_Master != null)
                                {
                                    decimalNowPrice = Convert.ToDecimal(Model_tab_WeiKanJia_Master.NowPrice);////可以砍价购买
                                    INTMAxIDboolintMasterUserIDtab_WeiKanJiaNowPrice = Model_tab_WeiKanJia_Master.ID;////如果后面Continue成功。。要只为已购买过。
                                }
                                else
                                {
                                    decimalNowPrice = Convert.ToDecimal(Model_tab_WeiKanJia.StartPrice);/////只能原价购买
                                }
                                boolContinue = true;
                                #endregion
                            }
                            else
                            {
                                boolContinue = false;
                            }
                        }

                    }
                    #endregion

                    if (boolContinue)
                    {
                        #region  复制一下原商品信息
                        int intOldGoodID = Convert.ToInt32(Model_tab_WeiKanJia.GoodID);
                        EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        //EggsoftWX.Model.tab_Goods Model_tab_Goods_OLD = BLL_tab_Goods.GetModel(intOldGoodID);

                        #endregion

                        //string strGoodID = "0";
                        #region 2 找一个本商户的 已删除的 商品 。把他的 分销功能  的参数  置空。。。如果 找不到这样的 商品。就 生成一个带删除标志的


                        //int.TryParse(strGoodID, out intGoodID);

                        if (intGoodID > 0)
                        {
                            boolContinue = false;
                            #region 1清空购物车
                            //

                            ///Eggsoft_Public_CL.ShoppingCart.ClearShoppingCart(true, intUserID);
                            #endregion
                            #region 2 添加购物车
                            int intAddStatus = Eggsoft_Public_CL.ShoppingCart.AddToShoppingCart(intUserID, intOldGoodID, 1, 0, new string[] { "0", "0" }, new string[] { "0", "0" }, new string[] { "0", "0" }, 1, Model_tab_WeiKanJia.ID, INTMAxIDboolintMasterUserIDtab_WeiKanJiaNowPrice, "");
                            if (intAddStatus == 1)
                            {
                                intErrorCode = 0;
                                boolContinue = true;
                            }

                            #endregion

                        }
                        #endregion

                    }
                    #region 从购物车 生成订单  修改价格
                    if (boolContinue)
                    {
                        #region lock do  购物车  到  待付款
                        string strLock = "201602121657";
                        lock (strLock)
                        {

                            #region 插入初始数据
                            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                            string strOrderNum = "";

                            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                            int intID = my_BLL_tab_Order.Add(my_Model_tab_Order);///先用存储过程插入  然后在更新 防止重复插入相同的ID
                            my_Model_tab_Order = my_BLL_tab_Order.GetModel(intID);
                            strOrderNum = DateTime.Now.ToString("yyyyMMddHHmmss") + Eggsoft.Common.StringNum.Add000000Num(intID, 2);
                            my_Model_tab_Order.OrderNum = strOrderNum;

                            if (Model_tab_WeiKanJia.MustAddress_Master == true)
                            {
                                int? inttab_OrderUser_Address = 0;
                                if (Model_tab_User.Default_Address != null)
                                {
                                    inttab_OrderUser_Address = Model_tab_User.Default_Address;
                                }

                                my_Model_tab_Order.User_Address = Convert.ToInt32(inttab_OrderUser_Address);
                            }

                            String strOrderName = "";
                            #endregion

                            #region for 购物车循环
                            int intOrderNameStatus = 0;
                            string strGoodName = "微砍价" + Model_tab_WeiKanJia.Topic;

                            EggsoftWX.Model.tab_Orderdetails my_Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();

                            my_Model_tab_Orderdetails.GoodID = intGoodID;
                            my_Model_tab_Orderdetails.GoodName = strGoodName;

                            if (intOrderNameStatus == 0) strOrderName = strGoodName;
                            if ((intOrderNameStatus > 0) && (strOrderName.IndexOf("等") == -1)) strOrderName = strOrderName + "等";
                            intOrderNameStatus++;

                            my_Model_tab_Orderdetails.GoodPrice = decimalNowPrice;
                            //my_Model_tab_Orderdetails.FenXiaoMoney = my_Model_tab_Goods.FenXiaoMoney;
                            my_Model_tab_Orderdetails.OrderID = intID;
                            my_Model_tab_Orderdetails.OrderCount = 1;
                            my_Model_tab_Orderdetails.CreatDateTime = DateTime.Now;
                            my_Model_tab_Orderdetails.ParentID = 0;
                            my_Model_tab_Orderdetails.GrandParentID = 0;
                            my_Model_tab_Orderdetails.GreatParentID = 0;


                            my_Model_tab_Orderdetails.MoneyCredits = 0;
                            my_Model_tab_Orderdetails.MoneyWeBuy8Credits = 0;
                            my_Model_tab_Orderdetails.Beans = 0;
                            my_Model_tab_Orderdetails.VouchersNum_List = "";
                            EggsoftWX.BLL.tab_Orderdetails my_BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                            int intOrderDetailID = my_BLL_tab_Orderdetails.Add(my_Model_tab_Orderdetails);



                            #endregion for 循环


                            #region 生成订单
                            //Decimal DecimalOrderMoney = 0;



                            my_Model_tab_Order.OrderNum = strOrderNum;
                            my_Model_tab_Order.ShopClient_ID = intShopClientID;
                            my_Model_tab_Order.TotalMoney = decimalNowPrice;
                            my_Model_tab_Order.OrderName = strOrderName;
                            my_Model_tab_Order.UserID = intUserID;
                            my_Model_tab_Order.FreightShowText = "微砍价 包邮 ";


                            if (Model_tab_WeiKanJia_Master != null)
                            {
                                my_Model_tab_Order.FreightShowText += "微砍价 " + Model_tab_WeiKanJia.Topic + " " + Model_tab_WeiKanJia_Master.MasterContactMan + " " + Model_tab_WeiKanJia_Master.MasteContactPhone + " " + Model_tab_User.NickName;
                            }
                            my_BLL_tab_Order.Update(my_Model_tab_Order);

                            Eggsoft_Public_CL.ShoppingCart.ClearShoppingCart(false, Convert.ToInt32(strUserID), "服务到待付款里面去");//到待付款里面去

                            Eggsoft_Public_CL.GoodP.tellShopClientID_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成
                            Eggsoft_Public_CL.GoodP.tellShopClientID_O2O_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成
                            Eggsoft_Public_CL.GoodP.tell_DistributionMoney_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成

                            #region  INTMAxIDboolintMasterUserIDtab_WeiKanJiaNowPrice = Model_tab_WeiKanJia_Master.ID;////如果后面Continue成功。。要只为已购买过。

                            if (INTMAxIDboolintMasterUserIDtab_WeiKanJiaNowPrice > 0)
                            {
                                Model_tab_WeiKanJia_Master.IsBuyed = true;
                                BLL_tab_WeiKanJia_Master.Update(Model_tab_WeiKanJia_Master);
                            }
                            #endregion
                            string strordernum = "";
                            string strmyallmoney = "";
                            string strordername = "";

                            if (decimalNowPrice > (Decimal)0.001)
                            {
                                strErrorCode = "81";///都准备好了，可以开始支付
                                strErrorDescription = "正在申请微信支付";
                                strordernum = strOrderNum;
                                strmyallmoney = Eggsoft_Public_CL.Pub.getPubMoney(decimalNowPrice);
                                strordername = strOrderName;

                                #region 检查是否满足现在能购买的条件  满足支付条件了  让前端去处理   在本文档中属于重复语句
                                //intErrorCode = 0;
                                //int.TryParse(strErrorCode, out intErrorCode);
                                //if (intErrorCode > 0)/// 满足支付条件了  让前端去处理
                                //{
                                str = "{\"ErrorCode\":" + strErrorCode + ",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\",\"OrderINT\":\"" + intID + "\",\"OrderNum\":\"" + strordernum + "\",\"MyallMoney\":\"" + strmyallmoney + "\",\"OrderName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strordername) + "\"}";
                                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                                {
                                    HttpRequest Request = HttpContext.Current.Request;
                                    HttpResponse Response = HttpContext.Current.Response;
                                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                                    string callback = Request["jsonp"];
                                    Response.Write(callback + "(" + str + ")");
                                    Response.End();//结束后续的操作，直接返回所需要的字符串
                                }
                                return str;
                                //}
                                #endregion

                                //  Eggsoft.Common.JsUtil.LocationNewHref("/paychoice.aspx?ordernum=" + strOrderNum + "&myallmoney=" +  + "&ordername=" + strOrderName);
                            }
                            else
                            {
                                strErrorCode = "82";
                                strErrorDescription = "免支付成功申请";
                                Eggsoft_Public_CL.GoodP.IGetMoney(strOrderNum, "WeiBaiPay", "0");
                                #region 检查是否满足现在能购买的条件  满足支付条件了  让前端去处理   在本文档中属于重复语句
                                //intErrorCode = 0;
                                //int.TryParse(strErrorCode, out intErrorCode);
                                //if (intErrorCode > 0)/// 满足支付条件了  让前端去处理
                                //{
                                str = "{\"ErrorCode\":" + strErrorCode + ",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\",\"OrderNum\":\"" + strordernum + "\",\"MyallMoney\":\"" + strmyallmoney + "\",\"OrderName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strordername) + "\"}";
                                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                                {
                                    HttpRequest Request = HttpContext.Current.Request;
                                    HttpResponse Response = HttpContext.Current.Response;
                                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                                    string callback = Request["jsonp"];
                                    Response.Write(callback + "(" + str + ")");
                                    Response.End();//结束后续的操作，直接返回所需要的字符串
                                }
                                return str;
                                //}
                                #endregion
                                //Eggsoft.Common.JsUtil.LocationNewHref("/cart_good2.aspx?out_trade_no=" + strOrderNum);
                            }
                            #endregion 生成订单


                        }




                        #endregion
                    }
                    #endregion
                    #endregion
                }

                str = "{\"ErrorCode\":" + intErrorCode + "}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
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
            return str;
        }





        [WebMethod]
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">分享人</param>
        /// <returns></returns>
        public String _Service_AddToCart()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);
                String strGoodID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["GoodID"]);
                //String strParentID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ParentID"]);
                String strbuyCount = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["buyCount"]);
                String MultiBuyType = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["MultiBuyType"]);////商品种类
                String strTSign = (context.QueryString["TSign"]);

                #region 检查签名
                string strSafeCode = "";
                EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserID + strGoodID + strbuyCount + MultiBuyType + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode));
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strUserID+ strGoodID+ strParentID+ strbuyCount MultiBuyType" + strUserID + " " + strGoodID + " " + strbuyCount + " " + MultiBuyType + " " + strSafeCode, "添加购物车签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    return "";
                }
                #endregion 检查签名




                int intErrorCode = 0;

                if (strGoodID == null) { intErrorCode = 6; };
                int pIntUserID = Convert.ToInt32(strUserID);
                int pIntGoodID = Convert.ToInt32(strGoodID);


                //int intParentID = Convert.ToInt32(strParentID);


                string strType_Money = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["Type_Money"]);
                string[] strMoney_List = new string[] { "0" };

                if (String.IsNullOrEmpty(strType_Money) == false)
                {
                    if (strType_Money == "1")
                    {
                        string strMoney_ = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["_Money_"]);
                        strMoney_List = new string[] { "1", strMoney_ };
                    }
                }

                #region 处理财富积分
                ///http://localhost:8014/Order/DoOrder.asmx/_Service_AddToCart?UserID=41192&goodid=1769&parentid=41192&buycount=64&multibuytype=0&Type_Wealth=1&_Wealth_=7359.91&TypeNumber_Vouchers_Bean=2&VouchersMoney_=1203.84&Type_Money=1&_Money_=14091.61&TSign=717a920de4059317808285111fff4548&jsonp=jsonp7Callback&_=1507878434803
                string strType_Wealth = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["Type_Wealth"]);
                string[] strWealth_List = new string[] { "0" };
                if (String.IsNullOrEmpty(strType_Wealth) == false)
                {
                    if (strType_Wealth == "1")
                    {
                        string str_Wealth = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["_Wealth_"]);
                        strWealth_List = new string[] { "1", str_Wealth };
                    }
                }
                #endregion

                string strTypeNumber_Vouchers_Bean = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["TypeNumber_Vouchers_Bean"]);
                string[] strNumber_Vouchers_Bean_List = new string[] { "0" };
                if (String.IsNullOrEmpty(strTypeNumber_Vouchers_Bean) == false)
                {

                    if (strTypeNumber_Vouchers_Bean == "2")
                    {
                        string strMoney_ = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["VouchersMoney_"]);
                        strNumber_Vouchers_Bean_List = new string[] { strTypeNumber_Vouchers_Bean, strMoney_ };
                    }
                    else if (strTypeNumber_Vouchers_Bean == "3")
                    {
                        string strBean_ = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["Bean_"]);
                        strNumber_Vouchers_Bean_List = new string[] { strTypeNumber_Vouchers_Bean, strBean_ };

                    }
                    else if (strTypeNumber_Vouchers_Bean == "4")
                    {
                        string VouchersNum_ = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["VouchersNum_"]);
                        string VouchersMoney_ = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["VouchersMoney_"]);
                        strNumber_Vouchers_Bean_List = new string[] { strTypeNumber_Vouchers_Bean, VouchersNum_, VouchersMoney_ };
                    }
                }
                intErrorCode = Eggsoft_Public_CL.ShoppingCart.AddToShoppingCart(pIntUserID, pIntGoodID, Int32.Parse(strbuyCount), Int32.Parse(MultiBuyType), strMoney_List, strWealth_List, strNumber_Vouchers_Bean_List, 0, 0, 0, "");

                str = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "服务添加购物车");
            }
            finally
            {

            }
            return str;
        }






        [WebMethod]
        /// <summary>
        /// 计算价格
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">分享人</param>
        /// <returns></returns>
        public string CountCurPrice()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                string strGoodID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strGoodID"]);
                string strbuyCount = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strbuyCount"]);
                string strMultiBuyType = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strMultiBuyType"]);
                string strMoneyCredits = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strMoneyCredits"]);
                string strVouchersNum_List = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strVouchersNum_List"]);
                if (String.IsNullOrEmpty(strVouchersNum_List) == false) strVouchersNum_List = "#" + strVouchersNum_List;
                string strMoneyWeBuy8Credits = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strMoneyWeBuy8Credits"]);///购物券
                string strMoneyWealth = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strMoneyWealth"]);///财富积分
                string strBeans = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strBeans"]);
                string strOutGoodMoney = "";

                String strTSign = (context.QueryString["TSign"]);
                #region 检查签名
                string strSafeCode = "";
                EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserID + strGoodID + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode));
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(" strUserID + strGoodID=" + strUserID + " " + strGoodID + strSafeCode, "计算能用多少现金余额签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    return "检查签名";
                }
                #endregion 检查签名


                int intUserID = 0;
                int.TryParse(strUserID, out intUserID);
                Decimal Decimal_My_Freight = 0;//处理运费问题
                string strShowYunFei = ""; string strMultiBuyTypeName = "";
                Eggsoft_Public_CL.AllcartYunFeiList myAllcartYunFeiList = new Eggsoft_Public_CL.AllcartYunFeiList();

                /////dec_Good_Money =本详细订单 用户需要支付的现金（已扣除购物券+现金） 
                Decimal dec_Good_Money = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_Price(intUserID, strGoodID, strbuyCount, strMultiBuyType, strMoneyCredits, strVouchersNum_List, strMoneyWeBuy8Credits, strMoneyWealth, strBeans, out strOutGoodMoney, out Decimal_My_Freight, out strShowYunFei, true, out strMultiBuyTypeName, false, out myAllcartYunFeiList, 0, 0, "0");
                bool boolGoodsShowYunFei = Eggsoft_Public_CL.Pub.boolShowPower(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID).ToString(), "GoodsShowYunFei");

                dec_Good_Money = dec_Good_Money - Decimal_My_Freight;///现在运费单列 所以要减去


                if (boolGoodsShowYunFei == false)
                {
                    //dec_Good_Money = dec_Good_Money - Decimal_My_Freight;///不显示运费
                    strShowYunFei = "";///不显示运费
                }
                else
                {
                    //strShowYunFei = "运费：";
                    //strShowYunFei += "¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_My_Freight);
                }                                                                                      ///

                str = "{\"YunFei\":\"" + strShowYunFei + "\",\"GoodMoney\":\"" + strOutGoodMoney + "\",\"ShowPayGoodMoney\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(dec_Good_Money) + "\"}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "服务计算价格", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "服务计算价格");
            }
            finally
            {

            }
            return str;


        }

        [WebMethod]
        /// <summary>
        /// 计算能用多少现金余额  这个和商品个数会变化有关   界面初始化使用
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">分享人</param>
        /// <returns></returns>
        public string Count_Do_CountyuEArgMoney()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string strReturnText = "";

            try
            {
                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                string strGoodID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strGoodID"]);
                string strbuyCount = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strbuyCount"]);
                string strMultiBuyType = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strMultiBuyType"]);

                String strTSign = (context.QueryString["TSign"]);

                #region 检查签名
                ///var TSign = hex_md5_8(String(varGoodID) + String(strbuyCount) + String(###UserID###) + String(VarMultiType) +  varNetUserSafeCode);///String(varpOperationCenterID)+String(varOperationIDGoods) +


                string strSafeCode = "";
                EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strGoodID + strbuyCount + strUserID + strMultiBuyType + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode));
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strGoodID + strbuyCount + strUserID + strMultiBuyType=" + strGoodID + " " + strbuyCount + " " + strUserID + " " + strMultiBuyType + " " + strbuyCount + " " + strSafeCode, "计算能用多少现金余额签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    return "检查签名";
                }
                #endregion 检查签名


                #region 得到商品价格 如果是代理商 就取代理商价格
                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
                Model_tab_Goods = BLL_tab_Goods.GetModel(Int32.Parse(strGoodID));
                Decimal DecimalGoodPrice = Convert.ToDecimal(Model_tab_Goods.PromotePrice);
                if ((strMultiBuyType != "0") && (strMultiBuyType != ""))//multi price
                {
                    EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice BLL_tab_Goods_MultiSelectTypePrice = new EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice();
                    EggsoftWX.Model.tab_Goods_MultiSelectTypePrice Model_tab_Goods_MultiSelectTypePrice = BLL_tab_Goods_MultiSelectTypePrice.GetModel(Int32.Parse(strMultiBuyType));

                    DecimalGoodPrice = Model_tab_Goods_MultiSelectTypePrice.GoodPrice;

                }



                Decimal UsergoodTotalCreditsMoney_Vouchers = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(strUserID), out UsergoodTotalCreditsMoney_Vouchers);  //

                Decimal UsergoodTotalCreditsMoney_Wealth = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Int32.Parse(strUserID), out UsergoodTotalCreditsMoney_Wealth);  //


                Decimal Decimal__Agent_ProductPrice = 0;
                Decimal Decimal__Agent_MaxGouWuQuan = 0;
                Decimal Decimal__Agent_MaxWealth = 0;////购物券

                EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("UserID=" + strUserID + " and ProductID=" + strGoodID + " and Empowered=1");
                if (Model_tab_ShopClient_Agent__ProductClassID != null)
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + " and IsDeleted=0 and Empowered=1 and AgentLevelSelect>0  and IsDeleted=0 ");
                    if (Model_tab_ShopClient_Agent_!=null && Model_tab_ShopClient_Agent_.IsDeleted != 0)
                    {
                        Model_tab_ShopClient_Agent_.IsDeleted = 0;
                        Model_tab_ShopClient_Agent_.UpdateBy = "计算能用多少现金余额";
                        Model_tab_ShopClient_Agent_.UpdateTime = DateTime.Now;
                        BLL_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_);
                    }

                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();
                        EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("shopClient_Agent_Level_ID=" + Model_tab_ShopClient_Agent_.AgentLevelSelect + " and ProductID=" + strGoodID);
                        if (Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID != null)
                        {
                            Decimal__Agent_ProductPrice = Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID.ProductPrice.toDecimal();
                            Decimal__Agent_MaxGouWuQuan = Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID.MaxGouWuQuan.toDecimal();
                            Decimal__Agent_MaxWealth = Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID.MaxWealth.toDecimal();
                        }
                    }
                }
                if (Decimal__Agent_ProductPrice > 0) DecimalGoodPrice = Decimal__Agent_ProductPrice;
                #endregion

                //#region 处理可用现金
                //Decimal pPayDecimalPromotePrice = DecimalGoodPrice * Int32.Parse(strbuyCount);
                //Decimal myCountMoney = 0;
                //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(strUserID), out myCountMoney);
                //Decimal minReturnCountMoney = pPayDecimalPromotePrice >= myCountMoney ? myCountMoney : pPayDecimalPromotePrice;//现金全部可用
                //#endregion 处理可用现金

                #region 处理可用购物券   处理可用财富积分
                Decimal minCountMoney_Vouchers = 0;
                Decimal minCountMoney_Wealth = 0;
                if (Decimal__Agent_ProductPrice > 0)///存在代理   就读他的购物券
                {
                    Decimal CanAgentgoodTotalCreditsMoney = Decimal__Agent_MaxGouWuQuan * Int32.Parse(strbuyCount);
                    minCountMoney_Vouchers = CanAgentgoodTotalCreditsMoney >= UsergoodTotalCreditsMoney_Vouchers ? UsergoodTotalCreditsMoney_Vouchers : CanAgentgoodTotalCreditsMoney;

                    Decimal CanAgentgoodTotalCreditsMoney_Wealth = Decimal__Agent_MaxWealth * Int32.Parse(strbuyCount);
                    minCountMoney_Wealth = CanAgentgoodTotalCreditsMoney_Wealth >= UsergoodTotalCreditsMoney_Wealth ? UsergoodTotalCreditsMoney_Wealth : CanAgentgoodTotalCreditsMoney_Wealth;

                }
                else
                {
                    Decimal pDecimalShopping_Vouchers_Percent = Convert.ToDecimal(Model_tab_Goods.Shopping_Vouchers_Percent);
                    Decimal CanUsegoodTotalCreditsMoney = pDecimalShopping_Vouchers_Percent * Int32.Parse(strbuyCount);
                    minCountMoney_Vouchers = CanUsegoodTotalCreditsMoney >= UsergoodTotalCreditsMoney_Vouchers ? UsergoodTotalCreditsMoney_Vouchers : CanUsegoodTotalCreditsMoney;


                    Decimal pDecimalShopping_Wealth_Percent = Convert.ToDecimal(Model_tab_Goods.WealthMoney);
                    Decimal CanUsegoodTotalCreditsMoney_Wealth = pDecimalShopping_Wealth_Percent * Int32.Parse(strbuyCount);
                    minCountMoney_Wealth = CanUsegoodTotalCreditsMoney_Wealth >= UsergoodTotalCreditsMoney_Wealth ? UsergoodTotalCreditsMoney_Wealth : CanUsegoodTotalCreditsMoney_Wealth;

                }
                #endregion 处理可用购物券

                #region 处理可用现金
                Decimal pPayDecimalPromotePrice = DecimalGoodPrice * Int32.Parse(strbuyCount);
                Decimal myCountMoney = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(strUserID), out myCountMoney);

                ////优先用购物券 再用现金 。最多现金取总额和购物券的差值  minReturnCountMoney = minReturnCountMoney - minCountMoney_Vouchers;//要减去购物券
                Decimal minReturnCountMoney = myCountMoney >= (pPayDecimalPromotePrice - minCountMoney_Vouchers - minCountMoney_Wealth) ? (pPayDecimalPromotePrice - minCountMoney_Vouchers - minCountMoney_Wealth) : myCountMoney;

                //Decimal minReturnCountMoney = pPayDecimalPromotePrice >= myCountMoney ? myCountMoney : pPayDecimalPromotePrice;//现金全部可用



                #endregion 处理可用现金



                #region 处理可用优惠券
                Decimal minCountMoney_Vouchers_YouHuiQuan = 0;
                if (Decimal__Agent_ProductPrice > 0)///存在代理价格
                {
                    // Decimal pmyNowPayDecimalPromotePrice = Decimal__Agent_ProductPrice * Int32.Parse(strbuyCount);

                    Decimal CanAgentgoodTotalCreditsMoney = Decimal__Agent_MaxGouWuQuan * Int32.Parse(strbuyCount);
                    minCountMoney_Vouchers_YouHuiQuan = CanAgentgoodTotalCreditsMoney;// >= goodTotalCreditsMoney_Vouchers ? goodTotalCreditsMoney_Vouchers : pmyNowPayDecimalPromotePrice;
                }
                else
                {
                    Decimal pDecimalShopping_Vouchers_Percent = Convert.ToDecimal(Model_tab_Goods.Shopping_Vouchers_Percent);
                    Decimal CanUsegoodTotalCreditsMoney = pDecimalShopping_Vouchers_Percent * Int32.Parse(strbuyCount);
                    minCountMoney_Vouchers_YouHuiQuan = CanUsegoodTotalCreditsMoney;// >= goodTotalCreditsMoney_Vouchers ? goodTotalCreditsMoney_Vouchers : CanUsegoodTotalCreditsMoney;
                }
                #endregion 处理可用优惠券

                //}
                //else
                //{
                //    Decimal pDecimalShopping_Vouchers_Percent = Convert.ToDecimal(Model_tab_Goods.Shopping_Vouchers_Percent);
                //    Decimal CanUsegoodTotalCreditsMoney = pDecimalShopping_Vouchers_Percent * Int32.Parse(strbuyCount);
                //    minCountMoney_Vouchers = CanUsegoodTotalCreditsMoney >= goodTotalCreditsMoney_Vouchers ? goodTotalCreditsMoney_Vouchers : CanUsegoodTotalCreditsMoney;
                //}




                strReturnText = "{\"minReturnCountMoney\":\"" + minReturnCountMoney + "\",";////本次购买可所用现金
                strReturnText += "\"ShowminReturnCountMoney\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(minReturnCountMoney) + "\",";////本次购买可所用现金
                strReturnText += "\"minCountMoney_Vouchers\":\"" + minCountMoney_Vouchers + "\",";////本次购买可所用购物券
                strReturnText += "\"ShowminCountMoney_Vouchers\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(minCountMoney_Vouchers) + "\",";////本次购买可所用购物券
                strReturnText += "\"minCountMoney_Wealth\":\"" + minCountMoney_Wealth + "\",";////本次购买可所用财富积分
                strReturnText += "\"ShowminCountMoney_Wealth\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(minCountMoney_Wealth) + "\",";////本次购买可所用财富积分
                strReturnText += "\"minCountMoney_Vouchers_YouHuiQuan\":\"" + minCountMoney_Vouchers_YouHuiQuan + "\",";////本次购买可所用购物优惠券
                strReturnText += "\"ShowminCountMoney_Vouchers_YouHuiQuan\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(minCountMoney_Vouchers_YouHuiQuan) + "\"";////本次购买可所用购物优惠券
                strReturnText += "}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + strReturnText + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "服务计算能用多少现金余额", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "服务计算能用多少现金余额");
            }
            finally
            {

            }
            return strReturnText;


        }


        //服务器端需要执行的操作示例:
        [WebMethod]
        public string Change_User_Address()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                string strpub_Int_Session_CurUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strpub_Int_Session_CurUserID"]);
                string strXiangXiDiZhiID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strXiangXiDiZhiID"]);


                if (String.IsNullOrEmpty(strXiangXiDiZhiID) == false)
                {
                    #region 充值 user 的省  为了 免运费的方案  等等
                    EggsoftWX.BLL.tab_User_Address my_BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();
                    EggsoftWX.Model.tab_User_Address my_Model_tab_User_Address = my_BLL_tab_User_Address.GetModel(Int32.Parse(strXiangXiDiZhiID));

                    EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();

                    if ((string.IsNullOrEmpty(my_Model_tab_User_Address.pc_province) == false))
                    {
                        #region //后期更改 往表里插入了pc_province]      ,[pc_city]      ,[pc_district]  如果存在有效数据可以用这里的
                        EggsoftWX.Model.tab_User my_Model_tab_User = my_BLL_tab_User.GetModel(Int32.Parse(strpub_Int_Session_CurUserID));
                        if (string.IsNullOrEmpty(my_Model_tab_User_Address.pc_district) == true)////处理直辖市情况  程序的不足引起的 如上海市上海市闵行区
                        {
                            my_Model_tab_User.Default_Address = Int32.Parse(strXiangXiDiZhiID);
                            my_Model_tab_User.Sheng = my_Model_tab_User_Address.pc_province;
                            my_Model_tab_User.City = my_Model_tab_User_Address.pc_province;
                            my_Model_tab_User.Area = my_Model_tab_User_Address.pc_city;
                            my_BLL_tab_User.Update(my_Model_tab_User);
                        }
                        else
                        {
                            my_Model_tab_User.Default_Address = Int32.Parse(strXiangXiDiZhiID);
                            my_Model_tab_User.Sheng = my_Model_tab_User_Address.pc_province;
                            my_Model_tab_User.City = my_Model_tab_User_Address.pc_city;
                            my_Model_tab_User.Area = my_Model_tab_User_Address.pc_district;
                            my_BLL_tab_User.Update(my_Model_tab_User);
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

                        string strUpdate = "Default_Address=" + strXiangXiDiZhiID;
                        if (String.IsNullOrEmpty(strReturnProvince) == false)
                        {
                            strUpdate += ",Sheng='" + strReturnProvince + "'";
                            my_BLL_tab_User.Update(strUpdate, "ID=" + strpub_Int_Session_CurUserID);

                            #region 优化算法  以后就不会进入这里  。因为 省份的优化已写出
                            ///优化算法  以后就不会进入这里  。因为 省份的优化已写出
                            ///
                            my_Model_tab_User_Address.pc_province = strReturnProvince;
                            my_BLL_tab_User_Address.Update(my_Model_tab_User_Address);
                            #endregion
                        }
                        else
                        {
                            my_BLL_tab_User.Update(strUpdate, "ID=" + strpub_Int_Session_CurUserID);
                        }
                        //string strUpdate = "Default_Address=" + strXiangXiDiZhiID;
                        //if (String.IsNullOrEmpty(strReturnProvince) == false) strUpdate += ",Sheng='" + strReturnProvince + "'";

                        //my_BLL_tab_User.Update(strUpdate, "ID=" + strpub_Int_Session_CurUserID);
                    }
                    #endregion


                }




                str = "{\"ErrorCode\":\"0\"}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
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
            return str;
        }


        /// <summary>
        /// 处理购物车立即购买事项
        /// </summary>       
        /// <returns></returns>
        [WebMethod]
        public string CartbuyNow()
        {
            try
            {
                #region 检查页面输入条件
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string pub_Int_Session_CurUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["pub_Int_Session_CurUserID"]);
                string getShouHuoDiZhi = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["RadioButtonList_Address"]);
                string pub_Int_ShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["pub_Int_ShopClientID"]);

                string strErrorCode = "";
                string strErrorDescription = "";

                int intUser_Address = 0;
                int intgetdGeto2oShop = 0;
                int intAdd_tab_ShopClient_O2O_TakeGoods = 0;

                EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                System.Data.DataTable dt_DataTable_ShopingCart = my_tab_Order_ShopingCart.GetList("UserID=" + pub_Int_Session_CurUserID + " and IsDeleted<>1").Tables[0];

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("id=" + pub_Int_Session_CurUserID);
                int Int_HowToGetProduct = Convert.ToInt32(Model_tab_User.HowToGetProduct);
                if (Int_HowToGetProduct == 0)///快速配送的
                {
                    #region 检查快速配送的
                    if (String.IsNullOrEmpty(getShouHuoDiZhi) == false)
                    {
                        if (getShouHuoDiZhi == "null")
                        {
                            intUser_Address = 0;
                        }
                        else
                        {
                            //Eggsoft.Common.debug_Log.Call_WriteLog("getShouHuoDiZhi=" + getShouHuoDiZhi);
                            intUser_Address = Convert.ToInt32(getShouHuoDiZhi);
                        }
                    }
                    else
                    {
                        intUser_Address = 0;
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
                            strErrorCode = "-3";
                            strErrorDescription = "请先输入收货地址";


                            ///   Eggsoft.Common.JsUtil.ShowMsg("请先输入收货地址", Pub_Agent_Path + "/cart_self.aspx?paymoney=paymoneymusthaveaddress");
                        }
                    }



                    #endregion
                }
                else if (Int_HowToGetProduct == 1)///上门自取的
                {
                    #region 检查快速配送的
                    string strGetZitiRenName = context.QueryString["ZitiRenName"];
                    string strGetZitiRenMobile = context.QueryString["ZitiRenMobile"];
                    string strZitiRenDate = context.QueryString["ZitiRenDate"];
                    string strZitiRenTime = context.QueryString["ZitiRenTime"];
                    string getdGeto2oShop = context.QueryString["varGeto2oShop"];

                    if (strGetZitiRenName.Length == 0)
                    {
                        strErrorCode = "-4";
                        strErrorDescription = "输入自取人姓名";
                        //Eggsoft.Common.JsUtil.ShowMsg("输入自取人姓名", -1);
                        //return;
                    }
                    try
                    {
                        Convert.ToInt64(strGetZitiRenMobile.Trim());
                        //return;
                    }
                    catch (Exception ex)
                    {
                        strErrorCode = "-5";
                        strErrorDescription = "输入手机号格式不正确";
                        //Eggsoft.Common.JsUtil.ShowMsg("输入手机号格式不正确", -1);
                        //return;
                    }
                    if (strGetZitiRenMobile.Length != 11)
                    {
                        //Eggsoft.Common.JsUtil.ShowMsg("手机号长度必须是11位", -1);
                        strErrorCode = "-6";
                        strErrorDescription = "手机号长度必须是11位";
                        //return;
                    }

                    if (String.IsNullOrEmpty(getdGeto2oShop) == false)
                    {
                        intgetdGeto2oShop = getdGeto2oShop.toInt32();
                    }
                    string[] strDateList = strZitiRenDate.Split('-');
                    if (string.IsNullOrEmpty(strZitiRenTime)) strZitiRenTime = DateTime.Now.ToString("HH:mm"); string[] strTimeList = strZitiRenTime.Split(':');
                    DateTime dtTakeDateTime = new DateTime(Int32.Parse(strDateList[0]), Int32.Parse(strDateList[1]), Int32.Parse(strDateList[2]), Int32.Parse(strTimeList[0]), Int32.Parse(strTimeList[1]), 0);

                    EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods BLL_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods();
                    EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods Model_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods();
                    Model_tab_ShopClient_O2O_TakeGoods.TakeName = strGetZitiRenName;
                    Model_tab_ShopClient_O2O_TakeGoods.HadTaked = 0;
                    Model_tab_ShopClient_O2O_TakeGoods.TakePhone = strGetZitiRenMobile;
                    Model_tab_ShopClient_O2O_TakeGoods.TakeDateTime = dtTakeDateTime;
                    Model_tab_ShopClient_O2O_TakeGoods.TakeO2OShopID = intgetdGeto2oShop;
                    Model_tab_ShopClient_O2O_TakeGoods.UserID = Int32.Parse(pub_Int_Session_CurUserID);
                    intAdd_tab_ShopClient_O2O_TakeGoods = BLL_tab_ShopClient_O2O_TakeGoods.Add(Model_tab_ShopClient_O2O_TakeGoods);
                    #endregion
                }
                #region 检查是否满足现在能购买的条件  没有的话 直接返回了
                int intErrorCode = 0;
                int.TryParse(strErrorCode, out intErrorCode);
                if (intErrorCode < 0)
                {
                    string str = "{\"ErrorCode\":\"" + strErrorCode + "\",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\"}";
                    if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                    {
                        HttpRequest Request = HttpContext.Current.Request;
                        HttpResponse Response = HttpContext.Current.Response;
                        Response.ContentEncoding = System.Text.Encoding.UTF8;
                        string callback = Request["jsonp"];
                        Response.Write(callback + "(" + str + ")");
                        Response.End();//结束后续的操作，直接返回所需要的字符串
                    }
                    return str;
                }
                #endregion
                #endregion

                #region lock do  购物车  到  待付款
                string strLock = "kjnasskjdfaksjdfhakjsdf657u473423fd";
                lock (strLock)
                {
                    if (dt_DataTable_ShopingCart.Rows.Count == 0)
                    {
                        strErrorCode = "-7";
                        strErrorDescription = "购物车清空";
                        #region 检查是否满足现在能购买的条件  没有的话 直接返回了   在本文档中属于重复语句
                        intErrorCode = 0;
                        int.TryParse(strErrorCode, out intErrorCode);
                        if (intErrorCode < 0)
                        {
                            string str = "{\"ErrorCode\":\"" + strErrorCode + "\",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\"}";
                            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                            {
                                HttpRequest Request = HttpContext.Current.Request;
                                HttpResponse Response = HttpContext.Current.Response;
                                Response.ContentEncoding = System.Text.Encoding.UTF8;
                                string callback = Request["jsonp"];
                                Response.Write(callback + "(" + str + ")");
                                Response.End();//结束后续的操作，直接返回所需要的字符串
                            }
                            return str;
                        }
                        #endregion
                        //Eggsoft.Common.JsUtil.ShowMsg("购物车清空", "javascript:history.back();");
                        //ztf return;
                    }





                    #region 插入初始数据
                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();

                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                    EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                    string strOrderNum = "";

                    EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                    int intID = my_BLL_tab_Order.Add(my_Model_tab_Order);///先用存储过程插入  然后在更新 防止重复插入相同的ID
                    my_Model_tab_Order = my_BLL_tab_Order.GetModel(intID);
                    strOrderNum = DateTime.Now.ToString("yyyyMMddHHmmss") + Eggsoft.Common.StringNum.Add000000Num(intID, 2);
                    my_Model_tab_Order.OrderNum = strOrderNum;
                    String strOrderName = "";

                    String strUserID = "";
                    String strParentID = "";
                    String strGrandParentID = "";
                    String strGreatParentID = "";
                    string strTeamID = "";
                    #endregion
                    #region 解决 一个商品颜色 不同 首件运费的问题
                    List<int> myProductIDList = new List<int>();

                    #endregion
                    #region for 购物车循环
                    int intOrderNameStatus = 0;
                    ArrayList allCartFullViewList = new ArrayList();
                    for (int i = 0; i < dt_DataTable_ShopingCart.Rows.Count; i++)
                    {

                        String strtab_Order_ShopingCartID = dt_DataTable_ShopingCart.Rows[i]["ID"].ToString();
                        String strGoodID = dt_DataTable_ShopingCart.Rows[i]["GoodID"].ToString();
                        strUserID = dt_DataTable_ShopingCart.Rows[i]["UserID"].ToString();
                        //strParentID = dt_DataTable_ShopingCart.Rows[i]["ParentID"].ToString();
                        //strGrandParentID = dt_DataTable_ShopingCart.Rows[i]["GrandParentID"].ToString();
                        //strGreatParentID = dt_DataTable_ShopingCart.Rows[i]["GreatParentID"].ToString();
                        EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(pub_Int_ShopClientID.toInt32(), pub_Int_Session_CurUserID.toInt32(), strGoodID.toInt32());
                        int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();

                        strTeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(pub_Int_Session_CurUserID.toString()).toString();
                        strParentID = (intLength > 0) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(strUserID.toInt32()).toString() : "";
                        strGrandParentID = (intLength > 1) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(strParentID.toInt32()).toString() : "";
                        strGreatParentID = (intLength > 2) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(strGrandParentID.toInt32()).toString() : "";



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

                        EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
                        my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Convert.ToInt32(strGoodID));
                        string strGoodName = my_Model_tab_Goods.Name;
                        string strSingleMoney = "";


                        bool boolIFSecondBuy = false;
                        EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = Eggsoft_Public_CL.GoodP.GetSecondBuyInfoPrice(Int32.Parse(strGoodID), out boolIFSecondBuy);

                        if (boolIFSecondBuy && strGoodType == "0")
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
                        else if ((strMultiBuyType != "0") && (strMultiBuyType != "") && (strGoodType == "0"))//multi price
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
                            // Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_Price(pub_Int_Session_CurUserID, strGoodID, strGoodID_Count, strMultiBuyType, strMoneyCredits, strVouchersNum_List, strMoneyWeBuy8Credits, strMoneyWealth, strBeans, out strGoodPrice, out outDecimal_My_Freight, out strShowYunFei, boolmyProductIDListFirstDoFreight, out strMultiBuyTypeName, boolCartSameGood, out myAllcartYunFeiList, Int32.Parse(strGoodType), Int32.Parse(strGoodTypeId), strGoodTypeIdBuyInfo);


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
                        else if (strGoodType == "2")///微团购  
                        {
                            EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                            EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Int32.Parse(strGoodTypeId));
                            if (Model_tab_TuanGou != null)
                            {
                                strGoodName = "团购 " + strGoodName;
                            }
                            if (String.IsNullOrEmpty(strGoodTypeIdBuyInfo) == false && strGoodTypeIdBuyInfo != "0")
                            {
                                strGoodName += "团员拼团编号:" + strGoodTypeIdBuyInfo;
                            }

                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_TuanGou.EachPeoplePrice));
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
                        else if (strGoodType == "6")///运营中心商品
                        {
                            #region 本月已有购买限制
                            int intOutMaxCount = 0;
                            int intNowCount = 0;
                            Eggsoft_Public_CL.GoodP_YunYingZhongXin myGoodP_YunYingZhongXin = new Eggsoft_Public_CL.GoodP_YunYingZhongXin();
                            myGoodP_YunYingZhongXin.checkLimitBuyEveryMonth(Model_tab_User.ID, pub_Int_ShopClientID.toInt32(), strGoodTypeIdBuyInfo.toInt32(), out intOutMaxCount, out intNowCount);
                            if (intOutMaxCount > 0 && (strGoodID_Count.toInt32() + intNowCount > intOutMaxCount))
                            {

                                strErrorDescription = "本月已有" + intNowCount + "单,最多可以下" + (intOutMaxCount - intNowCount) + "单!";
                                string str = "{\"ErrorCode\":\"-7\",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\"}";
                                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                                {
                                    HttpRequest Request = HttpContext.Current.Request;
                                    HttpResponse Response = HttpContext.Current.Response;
                                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                                    string callback = Request["jsonp"];
                                    Response.Write(callback + "(" + str + ")");
                                    Response.End();//结束后续的操作，直接返回所需要的字符串
                                }
                                return str;
                            }
                            #endregion 本月已有购买限制

                            #region 是否已经完全出局 完全出局也不能买
                            Boolean boolHaveRunAway = myGoodP_YunYingZhongXin.checkHaveQuitUser(Model_tab_User.ID, pub_Int_ShopClientID.toInt32(), strGoodTypeIdBuyInfo.toInt32());
                            if (boolHaveRunAway)
                            {
                                strErrorDescription = "已经完全出局，该ID号无权再下单!";
                                string str = "{\"ErrorCode\":\"-7\",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\"}";
                                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                                {
                                    HttpRequest Request = HttpContext.Current.Request;
                                    HttpResponse Response = HttpContext.Current.Response;
                                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                                    string callback = Request["jsonp"];
                                    Response.Write(callback + "(" + str + ")");
                                    Response.End();//结束后续的操作，直接返回所需要的字符串
                                }
                                return str;
                            }
                            #endregion 本月已有购买限制


                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                        }
                        else
                        {
                            strSingleMoney = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                        }
                        EggsoftWX.Model.tab_Orderdetails my_Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();

                        my_Model_tab_Orderdetails.GoodID = intGood;
                        my_Model_tab_Orderdetails.GoodName = strGoodName;

                        if (intOrderNameStatus == 0) strOrderName = strGoodName;
                        if ((intOrderNameStatus > 0) && (strOrderName.IndexOf("等") == -1)) strOrderName = strOrderName + "等";
                        intOrderNameStatus++;

                        bool boolmyProductIDListFirstDoFreight = true;
                        if (myProductIDList.Contains(intGood))
                        {
                            boolmyProductIDListFirstDoFreight = false;
                        }
                        else
                        {
                            myProductIDList.Add(intGood);
                        }
                        string strGoodPrice = "";
                        Decimal outDecimal_My_Freight = 0;//处理运费问题
                        string strShowYunFei = ""; string strMultiBuyTypeName = "";
                        Eggsoft_Public_CL.AllcartYunFeiList myAllcartYunFeiList = new Eggsoft_Public_CL.AllcartYunFeiList();
                        /////dec_Good_Money =本详细订单 用户需要支付的现金（已扣除购物券+现金）
                        Decimal dec_Good_Money_Agent = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_Price(Int32.Parse(pub_Int_Session_CurUserID), strGoodID, strGoodID_Count, strMultiBuyType, strMoneyCredits, strVouchersNum_List, strMoneyWeBuy8Credits, strMoneyWealth, strBeans, out strGoodPrice, out outDecimal_My_Freight, out strShowYunFei, boolmyProductIDListFirstDoFreight, out strMultiBuyTypeName, true, out myAllcartYunFeiList, Int32.Parse(strGoodType), Int32.Parse(strGoodTypeId), strGoodTypeIdBuyInfo);

                        my_Model_tab_Orderdetails.GoodPrice = (strGoodPrice.toDecimal());
                        //my_Model_tab_Orderdetails.FenXiaoMoney = my_Model_tab_Goods.FenXiaoMoney;
                        my_Model_tab_Orderdetails.OrderID = intID;
                        my_Model_tab_Orderdetails.OrderCount = Convert.ToInt32(strGoodID_Count);
                        my_Model_tab_Orderdetails.CreatDateTime = DateTime.Now;
                        my_Model_tab_Orderdetails.ParentID = strParentID.toInt32();
                        my_Model_tab_Orderdetails.GrandParentID = strGrandParentID.toInt32();
                        my_Model_tab_Orderdetails.GreatParentID = strGreatParentID.toInt32();
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
                            my_Model_tab_Orderdetails.Freight = 0;
                        }
                        else
                        {
                            my_Model_tab_Orderdetails.Freight = outDecimal_My_Freight;
                        }
                        int intOrderDetailID = my_BLL_tab_Orderdetails.Add(my_Model_tab_Orderdetails);

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
                                    Model_tab_Shopping_Vouchers.UserID = Int32.Parse(strUserID);
                                    Model_tab_Shopping_Vouchers.ShopClientID = Int32.Parse(pub_Int_ShopClientID);
                                    Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = intOrderDetailID;
                                    //Model_tab_Shopping_Vouchers.Consumed = true;//加入购物车时搞过了  这里重复写了
                                    BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                                }
                            }
                        }

                        #region 更新财富积分 追踪去向作用
                        //更新购物券
                        if (strMoneyWealth.toDecimal() > 0)
                        {
                            EggsoftWX.BLL.b015_OrderDetail_WealthBuy BLL_b015_OrderDetail_WealthBuy = new EggsoftWX.BLL.b015_OrderDetail_WealthBuy();
                            BLL_b015_OrderDetail_WealthBuy.Update("WealthDetailID=@WealthDetailID,updatetime=getdate(),updateby='进入订单表'", "UserID=@UserID and ShopingCartID=@ShopingCartID and UseOrNotuse=1", intOrderDetailID, strUserID, strtab_Order_ShopingCartID);
                        }
                        #endregion 更新财富积分 追踪去向作用

                    }
                    #endregion for 循环

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




                    #region 生成订单
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
                    my_Model_tab_Order.ShopClient_ID = Int32.Parse(pub_Int_ShopClientID);
                    my_Model_tab_Order.TotalMoney = DecimalOrderMoney;
                    my_Model_tab_Order.OrderName = strOrderName;
                    my_Model_tab_Order.UserID = Convert.ToInt32(strUserID);

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


                    #region 增加待付款未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "购物车到待付款";
                    Model_b011_InfoAlertMessage.CreateBy = "购物车到待付款";
                    Model_b011_InfoAlertMessage.UpdateBy = "购物车到待付款";
                    Model_b011_InfoAlertMessage.UserID = my_Model_tab_Order.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_cart_good";
                    Model_b011_InfoAlertMessage.TypeTableID = my_Model_tab_Order.ID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加待付款未处理信息


                    Eggsoft_Public_CL.ShoppingCart.ClearShoppingCart(false, Convert.ToInt32(strUserID), "购物车到待付款");//到待付款里面去

                    Eggsoft_Public_CL.GoodP.tellShopClientID_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成
                    Eggsoft_Public_CL.GoodP.tellShopClientID_O2O_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成
                    Eggsoft_Public_CL.GoodP.tell_DistributionMoney_UserWillPayMoney_ByWeiXin(strOrderNum);///即将付款通知 订单已生成


                    string strordernum = "";
                    string strmyallmoney = "";
                    string strordername = "";

                    if (DecimalOrderMoney > (Decimal)0.001)
                    {
                        strErrorCode = "81";///都准备好了，可以开始支付
                        strErrorDescription = "正在申请微信支付";
                        strordernum = strOrderNum;
                        strmyallmoney = Eggsoft_Public_CL.Pub.getPubMoney(DecimalOrderMoney);
                        strordername = strOrderName;

                        #region 检查是否满足现在能购买的条件  满足支付条件了  让前端去处理   在本文档中属于重复语句
                        //intErrorCode = 0;
                        //int.TryParse(strErrorCode, out intErrorCode);
                        //if (intErrorCode > 0)/// 满足支付条件了  让前端去处理
                        //{
                        string str = "{\"ErrorCode\":\"" + strErrorCode + "\",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\",\"OrderINT\":\"" + intID + "\",\"OrderNum\":\"" + strordernum + "\",\"MyallMoney\":\"" + strmyallmoney + "\",\"OrderName\":\"" + strordername + "\"}";
                        if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                        {
                            HttpRequest Request = HttpContext.Current.Request;
                            HttpResponse Response = HttpContext.Current.Response;
                            Response.ContentEncoding = System.Text.Encoding.UTF8;
                            string callback = Request["jsonp"];
                            Response.Write(callback + "(" + str + ")");
                            Response.End();//结束后续的操作，直接返回所需要的字符串
                        }
                        return str;
                        //}
                        #endregion

                        //  Eggsoft.Common.JsUtil.LocationNewHref("/paychoice.aspx?ordernum=" + strOrderNum + "&myallmoney=" +  + "&ordername=" + strOrderName);
                    }
                    else
                    {
                        strErrorCode = "82";
                        strErrorDescription = "免支付成功申请";
                        Eggsoft_Public_CL.GoodP.IGetMoney(strOrderNum, "WeiBaiPay", "0");
                        #region 检查是否满足现在能购买的条件  满足支付条件了  让前端去处理   在本文档中属于重复语句
                        //intErrorCode = 0;
                        //int.TryParse(strErrorCode, out intErrorCode);
                        //if (intErrorCode > 0)/// 满足支付条件了  让前端去处理
                        //{
                        string str = "{\"ErrorCode\":\"" + strErrorCode + "\",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\",\"OrderNum\":\"" + strordernum + "\",\"MyallMoney\":\"" + strmyallmoney + "\",\"OrderName\":\"" + strordername + "\"}";
                        if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                        {
                            HttpRequest Request = HttpContext.Current.Request;
                            HttpResponse Response = HttpContext.Current.Response;
                            Response.ContentEncoding = System.Text.Encoding.UTF8;
                            string callback = Request["jsonp"];
                            Response.Write(callback + "(" + str + ")");
                            Response.End();//结束后续的操作，直接返回所需要的字符串
                        }
                        return str;
                        //}
                        #endregion
                        //Eggsoft.Common.JsUtil.LocationNewHref("/cart_good2.aspx?out_trade_no=" + strOrderNum);
                    }
                    #endregion 生成订单


                }




                #endregion
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "购物车错误", "线程异常");
            }
            catch (Exception myException)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(myException, "购物车错误，AAAAA立即处理");
            }

            finally { }
            #region 如果能执行到这里说明前面错聊，这里不该执行的 检查是否满足现在能购买的条件  没有的话 直接返回了   在本文档中属于重复语句
            string str_99ErrorCode = "-99";
            string str_99ErrorDescription = "如果能执行到这里说明前面错了，这里不该执行的";
            int int_99ErrorCode = 0;
            int.TryParse(str_99ErrorCode, out int_99ErrorCode);

            string str_99 = "{\"ErrorCode\":\"" + str_99ErrorCode + "\",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(str_99ErrorDescription) + "\"}";
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str_99 + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return str_99;

            #endregion
        }


        /// <summary>
        /// 处理购物车立即购买事项
        /// </summary>       
        /// <returns></returns>
        [WebMethod]
        public string CheckPayAcyion()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            int intErrorCode = -1;
            int intCheckCount = 0;
            try
            {
                String strOrderINT = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["OrderINT"]);//订单记录的ID
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                Eggsoft.Common.debug_Log.Call_WriteLog("strOrderINT=" + strOrderINT + "  strShopClientID=" + strShopClientID, "检测是否支付成功", "检测数据库");

                if (string.IsNullOrEmpty(strOrderINT) == false)
                {

                    checkagain:
                    EggsoftWX.BLL.tab_Order my_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                    my_Model_tab_Order = my_tab_Order.GetModel("id=" + strOrderINT + "");

                    if (Convert.ToBoolean(my_Model_tab_Order.PayStatus))
                    {
                        intErrorCode = 0;///成功得到支付检测
                    }
                    else
                    {
                        intCheckCount++;
                        if (intCheckCount < 20)////大于20秒不检查 认为执行失败
                        {
                            Thread.Sleep(1000);///暂停执行
                            goto checkagain;
                        }
                    }
                }

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "服务处理购物车立即购买事项", "线程异常");
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee, "服务处理购物车立即购买事项");
            }
            finally { }

            String strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";

            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return strReturn;

        }

        //服务器端需要执行的操作示例:
        [WebMethod]
        public string method1()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            string strGoodID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strGoodID"]);
            string strbuyCount = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strbuyCount"]);

            string str = "{\"msg\":\"这里" + strGoodID + "是主要内888888888888容" + strbuyCount + "\"}";
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intOrderID"></param>
        /// <param name="intShopClient_ID"></param>
        /// <param name="strRefundMoney">退款金额 单位为分</param>
        /// <returns></returns>
        [WebMethod]
        public string refundMoney()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            //string strCheckCodeGuid = (context.QueryString["CheckCodeGuid"]);
            string strOrderID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["OrderID"]);
            string strShopClient_ID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClient_ID"]);
            string strRefundMoney = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["RefundMoney"]);
            string strRefundNumber = (context.QueryString["RefundNumber"]);
            string strAAAsafecode = (context.QueryString["safecode"]);
            string strSafeCode = "uiajkwefdhaskdfasdjfaskfdhkasdfasd";
            string strMe = Eggsoft.Common.DESCrypt.GetMd5Str32(strOrderID + strShopClient_ID + strRefundMoney + strRefundNumber + strSafeCode);
            if (strAAAsafecode != strMe)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("strOrderID + strShopClient_ID + strRefundMoney + strSafeCode=" + strOrderID + strShopClient_ID + strRefundMoney + strSafeCode, "退款申请", "签名失败");
                Eggsoft.Common.debug_Log.Call_WriteLog("strAAAsafecode=" + strAAAsafecode, "退款申请", "签名失败");
                return "签名失败";

            }



            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Model_tab_Order = BLL_tab_Order.GetModel("ID=" + strOrderID + " and ShopClient_ID=" + strShopClient_ID);
            if (Model_tab_Order == null)
            {
                return "找不到可退的订单";//找不到可退的订单
            }

            //Model_tab_Order.ShopClient_ID

            WxPayModel wxPayModel = new WxPayModel(strShopClient_ID.toInt32());

            //创建Model
            UnifiedWxPayModel model = UnifiedWxPayModel.CreateUnifiedModel(wxPayModel.AppId, wxPayModel.PartnerID, wxPayModel.PartnerKey);


            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
            Model_EngineerMode = BLL_EngineerMode.GetModel("ShopClientID=" + strShopClient_ID);

            string strOutCreateOrderRefundXml = "";

            ////退款资金来源 refund_account 否 String(30) REFUND_SOURCE_RECHARGE_FUNDS 
            //            仅针对老资金流商户使用

            //            REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款（默认使用未结算资金退款）

            //REFUND_SOURCE_RECHARGE_FUNDS-- - 可用余额退款
            string strrefund_account = "REFUND_SOURCE_UNSETTLED_FUNDS";

            //Model_tab_Order.PaywayOrderNum = "5555555555";
            //创建Model
            ///UnifiedWxPayModel.            UnifiedPrePayMessage result = WeiXinHelper.UnifiedPrePay(model.CreatePrePayPackage(description, strOrderNum, totalFee, createIp, notifyUrl, openId));
            string strCreateOrderRefundXml = model.CreateOrderRefundXml(strShopClient_ID.toInt32(), Model_tab_Order.OrderNum, Model_tab_Order.PaywayOrderNum, (Model_tab_Order.TotalMoney * 100).toInt32().toString(), strRefundNumber, (strRefundMoney.toDecimal() * 100).toInt32().toString(), strrefund_account);
            bool boolSuccess = WeiXinHelper.Refund(strCreateOrderRefundXml, Model_EngineerMode.Apiclient_cert_Pem, Model_EngineerMode.Apiclient_cert_Pem_Password, out strOutCreateOrderRefundXml);

            if (boolSuccess == false)
            {
                strrefund_account = "REFUND_SOURCE_RECHARGE_FUNDS";
                strCreateOrderRefundXml = model.CreateOrderRefundXml(strShopClient_ID.toInt32(), Model_tab_Order.OrderNum, Model_tab_Order.PaywayOrderNum, (Model_tab_Order.TotalMoney * 100).toInt32().toString(), strRefundNumber, (strRefundMoney.toDecimal() * 97).toInt32().toString(), strrefund_account);///自动扣掉3%
                boolSuccess = WeiXinHelper.Refund(strCreateOrderRefundXml, Model_EngineerMode.Apiclient_cert_Pem, Model_EngineerMode.Apiclient_cert_Pem_Password, out strOutCreateOrderRefundXml);
            }

            return boolSuccess ? "SUCCESS" : strOutCreateOrderRefundXml;
        }

        /// <summary>
        /// 获取购物车数量 商品
        /// </summary>
        /// <param name="intOrderID"></param>
        /// <param name="intShopClient_ID"></param>
        /// <returns></returns>

        [WebMethod]
        public string getOrder_ShopingCart()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strTSign = (context.QueryString["TSign"]);

                #region 检查签名
                string strSafeCode = "";
                EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserID + strShopClientID + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode));
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strUserID + strShopClientID  strSafeCode" + strUserID + " " + strShopClientID + " " + strSafeCode, "获取购物车数量商品签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    return "";
                }
                #endregion 检查签名
                int intUserID = 0;
                int intShopClientID = 0;

                int.TryParse(strUserID, out intUserID);
                int.TryParse(strShopClientID, out intShopClientID);
                if ((Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString()) != intShopClientID))
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("获取购物车数量商品严重错误");
                }
                Int32 Int32Count = Eggsoft_Public_CL.GoodP.getOrder_ShopingCart(intUserID, intShopClientID);



                str = "{\"ErrorCode\":" + 0 + ",\"CountShopingCart\":" + Int32Count + "}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "获取购物车数量", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "获取购物车数量");
            }
            finally
            {

            }
            return str;
        }

    }
}
