using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Order
{
    /// <summary>
    /// DoSelf_51 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DoSelf_51 : System.Web.Services.WebService
    {
        private static object objectmuu = new object();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        /// <summary>
        /// 自助支付
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">购买人</param>
        /// <returns></returns>
        public String _GetGameInfo()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
            int intShopClientID = 0;
            int.TryParse(strShopClientID, out intShopClientID);

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);

            string strXML = Model_tab_ShopClient.XML;
            Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
            bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
            string strShopLogoImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + XML__Class_Shop_Client.ShopLogoImage;


            string str = "";
            str = "{\"ErrorCode\":0,\"ShopName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Model_tab_ShopClient.ShopClientName) + "\"";
            str += ",\"ShopLogoImage\":\"" + strShopLogoImage + "\"";
            str += "}";
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


        [WebMethod]
        /// <summary>
        /// 自助支付
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">购买人</param>
        /// <returns></returns>
        public String _Service_AddToCart_51PaySelf()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strGoodPrice = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["GoodPrice"]);

                int intUserID = 0;
                int int_PayGoodID = 0;
                int intShopClientID = 0;
                Decimal DecimalGoodPrice = 0;


                string strDoSelf_51_GoodID = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "DoSelf_51_GoodID");
                int.TryParse(strDoSelf_51_GoodID, out int_PayGoodID);

                string strErrorCode = "";///都准备好了，可以开始支付
                if (int_PayGoodID == 0)
                {
                    string strErrorDescription = "相关参数自助收款商品编号不能为0";
                    Eggsoft.Common.debug_Log.Call_WriteLog("strShopClientID=" + strShopClientID, "自助收款", strErrorDescription);
                    strErrorCode = "82";///都准备好了，可以开始支付
                    str = "{\"ErrorCode\":" + strErrorCode + ",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\"}";
                }
                else
                {
                    int.TryParse(strUserID, out intUserID);
                    int.TryParse(strShopClientID, out intShopClientID);
                    Decimal.TryParse(strGoodPrice, out DecimalGoodPrice);

                    lock (objectmuu)
                    {
                        #region 2 添加购物车
                        #region 从购物车 生成订单  修改价格
                        #region 插入初始数据
                        EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                        string strOrderNum = "";

                        EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                        int intID = my_BLL_tab_Order.Add(my_Model_tab_Order);///先用存储过程插入  然后在更新 防止重复插入相同的ID
                        my_Model_tab_Order = my_BLL_tab_Order.GetModel(intID);
                        strOrderNum = DateTime.Now.ToString("yyyyMMddHHmmss") + Eggsoft.Common.StringNum.Add000000Num(intID, 2);
                        my_Model_tab_Order.OrderNum = strOrderNum;
                        my_Model_tab_Order.User_Address = 0;

                        #endregion

                        #region for 购物车循环
                        int intOrderNameStatus = 0;
                        string strGoodName = "自助付款";

                        EggsoftWX.Model.tab_Orderdetails my_Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();

                        my_Model_tab_Orderdetails.GoodID = int_PayGoodID;
                        my_Model_tab_Orderdetails.GoodName = strGoodName;

                        intOrderNameStatus++;

                        my_Model_tab_Orderdetails.GoodPrice = DecimalGoodPrice;
                        //my_Model_tab_Orderdetails.FenXiaoMoney = my_Model_tab_Goods.FenXiaoMoney;
                        my_Model_tab_Orderdetails.OrderID = intID;
                        my_Model_tab_Orderdetails.OrderCount = 1;
                        my_Model_tab_Orderdetails.CreatDateTime = DateTime.Now;
                        my_Model_tab_Orderdetails.ParentID = 0;
                        my_Model_tab_Orderdetails.GrandParentID = 0;
                        my_Model_tab_Orderdetails.GreatParentID = 0;
                        my_Model_tab_Orderdetails.ShopClient_ID = intShopClientID;
                        my_Model_tab_Orderdetails.GoodType = 4;//0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        my_Model_tab_Orderdetails.MoneyCredits = 0;
                        my_Model_tab_Orderdetails.MoneyWeBuy8Credits = 0;
                        my_Model_tab_Orderdetails.Beans = 0;
                        my_Model_tab_Orderdetails.VouchersNum_List = "";
                        EggsoftWX.BLL.tab_Orderdetails my_BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                        int intOrderDetailID = my_BLL_tab_Orderdetails.Add(my_Model_tab_Orderdetails);

                        #endregion for 循环
                        #region 生成订单

                        my_Model_tab_Order.OrderNum = strOrderNum;
                        my_Model_tab_Order.ShopClient_ID = intShopClientID;
                        my_Model_tab_Order.TotalMoney = DecimalGoodPrice;
                        my_Model_tab_Order.OrderName = strGoodName;
                        my_Model_tab_Order.UserID = intUserID;
                        my_Model_tab_Order.FreightShowText = "自助付款 包邮 ";
                        my_BLL_tab_Order.Update(my_Model_tab_Order);
                        #endregion
                        string strordernum = "";
                        string strmyallmoney = "";
                        //string strordername = "";


                        #endregion

                        strErrorCode = "81";///都准备好了，可以开始支付
                        string strErrorDescription = "正在申请微信支付";
                        strordernum = strOrderNum;
                        strmyallmoney = Eggsoft_Public_CL.Pub.getPubMoney(DecimalGoodPrice);
                        str = "{\"ErrorCode\":" + strErrorCode + ",\"ErrorDescription\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strErrorDescription) + "\",\"OrderINT\":\"" + intID + "\",\"OrderNum\":\"" + strordernum + "\",\"MyallMoney\":\"" + strmyallmoney + "\",\"OrderName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strGoodName) + "\"}";

                        #endregion

                    }
                }

                #region 检查是否满足现在能购买的条件  满足支付条件了  让前端去处理   在本文档中属于重复语句

                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
                #endregion



            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "自助收款", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "自助收款", "程序报错");
            }
            finally
            {

            }
            return str;
        }



        [WebMethod]
        /// <summary>
        /// 自助支付
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">购买人</param>
        /// <returns></returns>
        public String _Service_AddToCart_51PaySelf_AutoFaHuo()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                String strOrderINT = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["OrderINT"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);

                int intOrderINT = 0;
                int intShopClientID = 0;

                int.TryParse(strOrderINT, out intOrderINT);
                int.TryParse(strShopClientID, out intShopClientID);

                lock (objectmuu)
                {

                    #region 自助发货处理

                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                    my_Model_tab_Order = my_BLL_tab_Order.GetModel("id=" + strOrderINT + "");
                    my_Model_tab_Order.isReceipt = true;
                    my_Model_tab_Order.DeliveryText = "自助收款,自动发货";
                    my_Model_tab_Order.UpdateDateTime = DateTime.Now;
                    my_BLL_tab_Order.Update(my_Model_tab_Order);

                    str = "{\"ErrorCode\":0}";
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
                    #endregion




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
    }
}
