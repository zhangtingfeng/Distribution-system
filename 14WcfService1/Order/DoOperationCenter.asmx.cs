using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;

namespace _14WcfService1.Order
{
    /// <summary>
    /// DoOperationCenter 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DoOperationCenter : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        /// <summary>
        /// 添加运营货物到到运营中心。完成支付才算是获取财富成功成功。
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">分享人</param>
        /// <returns></returns>
        public String _Service_AddToCart_OperationCenter()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);
                String strGoodID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["GoodID"]);
                String strParentID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ParentID"]);
                String strbuyCount = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["buyCount"]);
                String strmultibuytype = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["multibuytype"]);
                String strOperationID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["OperationID"]);////
                String strOperationIDGoods = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["OperationIDGoods"]);////

                String strTSign = (context.QueryString["TSign"]);

                #region 检查签名
                string strSafeCode = "";
                EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserID + strGoodID  + strbuyCount + strmultibuytype + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode));///strOperationID + strOperationIDGoods +
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strUserID+ strGoodID+ strParentID+ strbuyCount strOperationID strOperationIDGoods" + strUserID + " " + strGoodID + " " + strbuyCount + " " + strOperationID + " " + strOperationIDGoods + " " + strSafeCode, "添加运营购物车签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    return "";
                }
                #endregion 检查签名

                int intErrorCode = 0;

                if (strGoodID == null) { intErrorCode = 6; };
                int pIntUserID = Convert.ToInt32(strUserID);
                int pIntGoodID = Convert.ToInt32(strGoodID);
                int intParentID = Convert.ToInt32(strParentID);
                int intOperationID = Convert.ToInt32(strOperationID);///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                                                                     ///商品 来源 类型  表的 主键   正常订单 这个 为空  .微砍价 团购 会出现相关主键 商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                int intOperationIDGoods = Convert.ToInt32(strOperationIDGoods);

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


                #region 查找上级运营中心
                int intOperation_Parent_ID = 0;
                EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel(intOperationID);
                if (Model_b002_OperationCenter != null) intOperation_Parent_ID = Model_b002_OperationCenter.ParentID.toInt32();
                #endregion 查找上级运营中心

                intErrorCode = Eggsoft_Public_CL.ShoppingCart.AddToShoppingCart(pIntUserID, pIntGoodID, Int32.Parse(strbuyCount), 0, strMoney_List, new string[] { "0" }, new string[] { "0" }, 6, intOperationID, intOperationIDGoods, "");


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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "运营中心商品加入购物车");
            }
            finally
            {

            }
            return str;
        }


        /// <summary>
        ///先检查是否存在7天内的订单。  根据 BuyOrderUserID 自动带出 上下级关系 。如果是本运营中的粉丝。如果不是 就不带出了。   
        /// </summary>
        /// <returns></returns>
        [WebMethod]

        public String _Service_Check_ParnetID_OperationCenter()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            string strShowInfo = "0";
            try
            {
                String strBuyOrderShopUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["BuyOrderUserID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strJSUserSign = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["JSUserSign"]);
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);




                int intErrorCode = 0;

                EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                string strUserIDRealname = "";
                #region 前端录入合法性检查
                EggsoftWX.Model.tab_User Model_tab_UserAsk = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strBuyOrderShopUserID.toInt32(), strShopClientID);
                if (Model_tab_UserAsk == null)
                {
                    intErrorCode = -78;///用户微店ID不存在
                }
                else
                {
                    strUserIDRealname = Model_tab_UserAsk.UserRealName;
                }

                #region 检查签名
                EggsoftWX.Model.tab_User curModel_tab_User = blltab_User.GetModel(strUserID.toInt32());
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strBuyOrderShopUserID + strShopClientID + strUserID + Eggsoft.Common.DESCrypt.hex_md5_2(curModel_tab_User.SafeCode));
                if (strJSUserSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("" + curModel_tab_User.SafeCode, "申请修改订单服务签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    intErrorCode = -88;///签名失败  
                }
                #endregion 检查签名



                if (intErrorCode == 0)
                {
                    string strSQLOutDays = @"SELECT   tab_Order.PayStatus, tab_Order.UserID, tab_Orderdetails.Over7DaysToBeans, 
                tab_Orderdetails.isdeleted, tab_Orderdetails.ShopClient_ID, tab_Orderdetails.GoodType, 
                tab_Orderdetails.GoodTypeId, tab_Orderdetails.GoodTypeIdBuyInfo, tab_Orderdetails.ID, 
                tab_Orderdetails.CreatTime
FROM      tab_Order RIGHT OUTER JOIN
                tab_Orderdetails ON tab_Order.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND 
                tab_Order.ID = tab_Orderdetails.OrderID
                WHERE (tab_Orderdetails.ShopClient_ID = 21) 
                and (tab_Orderdetails.Over7DaysToBeans  = 1) 
                AND (tab_Order.UserID = " + Model_tab_UserAsk.ID + @")
                AND (tab_Orderdetails.GoodType = '6')
                ";

                    string strSQLIn7Days = @"SELECT   tab_Order.PayStatus, tab_Order.UserID, tab_Orderdetails.Over7DaysToBeans, 
                tab_Orderdetails.isdeleted, tab_Orderdetails.ShopClient_ID, tab_Orderdetails.GoodType, 
                tab_Orderdetails.GoodTypeId, tab_Orderdetails.GoodTypeIdBuyInfo, tab_Orderdetails.ID, 
                tab_Orderdetails.CreatTime
FROM      tab_Order RIGHT OUTER JOIN
                tab_Orderdetails ON tab_Order.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND 
                tab_Order.ID = tab_Orderdetails.OrderID
                WHERE (tab_Orderdetails.ShopClient_ID = 21) 
                and (tab_Orderdetails.Over7DaysToBeans  = 0) 
                AND (tab_Order.UserID = " + Model_tab_UserAsk.ID + @")
                AND (tab_Orderdetails.GoodType = '6')
                ";

                    System.Data.DataTable DataDataTableSQLIn7Days = blltab_Order.SelectList(strSQLOutDays).Tables[0];
                    if (DataDataTableSQLIn7Days.Rows.Count > 0)
                    {
                        intErrorCode = -68;///只能申请首次下单的客户，当前用户已经下过单
                    }
                    else
                    {
                        System.Data.DataTable DataSQLIn7Days = blltab_Order.SelectList(strSQLIn7Days).Tables[0];
                        if (DataSQLIn7Days.Rows.Count > 0)////符合申请条件 。看是否能自动带出数据
                        {
                            EggsoftWX.BLL.b005_UserID_Operation_ID bllb005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                            EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = bllb005_UserID_Operation_ID.GetModel("UserID=@UserID AND  ShopClientID=@ShopClientID AND OperationCenterID_UserID=@OperationCenterID_UserID and IsDeleted=0", Model_tab_UserAsk.ID, strShopClientID.toInt32(), strUserID.toInt32());
                            if (Model_b005_UserID_Operation_ID == null)
                            {
                                intErrorCode = 5;///不是本运营中心的 不能自动带出数据
                            }
                            else
                            {
                                int intUserParentID = Model_b005_UserID_Operation_ID.UserParentID.toInt32();
                                int intUserGrandParentID = 0;
                                if (intUserParentID > 0)
                                {
                                    EggsoftWX.Model.b005_UserID_Operation_ID ParentModel_b005_UserID_Operation_ID = bllb005_UserID_Operation_ID.GetModel("UserID=@UserID AND  ShopClientID=@ShopClientID  and IsDeleted=0", intUserParentID, strShopClientID.toInt32());
                                    if (ParentModel_b005_UserID_Operation_ID != null)
                                    {
                                        intUserGrandParentID = ParentModel_b005_UserID_Operation_ID.UserParentID.toInt32();
                                    }
                                }
                                string strUserParentIDNickname = "";
                                string strUserParentIDRealname = "";
                                string strUserParentIDShopUserID = "";
                                string strUserGrandParentIDNickname = "";
                                string strUserGrandParentIDRealname = "";
                                string strUserGrandParentIDShopUserID = "";


                                Model_tab_UserAsk = blltab_User.GetModel("ID=@ID and ShopClientID=@ShopClientID", intUserParentID, strShopClientID);
                                if (Model_tab_UserAsk != null)
                                {
                                    strUserParentIDShopUserID = Model_tab_UserAsk.ShopUserID.ToString();
                                    strUserParentIDNickname = Model_tab_UserAsk.NickName;
                                    strUserParentIDRealname = Model_tab_UserAsk.UserRealName;
                                }
                                Model_tab_UserAsk = blltab_User.GetModel("ID=@ID and ShopClientID=@ShopClientID", intUserGrandParentID, strShopClientID);
                                if (Model_tab_UserAsk != null)
                                {
                                    strUserGrandParentIDShopUserID = Model_tab_UserAsk.ShopUserID.ToString();
                                    strUserGrandParentIDNickname = Model_tab_UserAsk.NickName;
                                    strUserGrandParentIDRealname = Model_tab_UserAsk.UserRealName;
                                }

                                intErrorCode = 1;

                                strShowInfo = "{\"UserIDRealname\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserIDRealname) + "\"";

                                strShowInfo += ",\"UserParentIDShopUserID\":\"" + strUserParentIDShopUserID + "\"";
                                strShowInfo += ",\"UserParentIDNickname\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserParentIDNickname) + "\"";
                                strShowInfo += ",\"UserParentIDRealname\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserParentIDRealname) + "\"";

                                strShowInfo += ",\"UserGrandParentIDShopUserID\":\"" + strUserGrandParentIDShopUserID + "\"";
                                strShowInfo += ",\"UserGrandParentIDNickname\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserGrandParentIDNickname) + "\"";
                                strShowInfo += ",\"UserGrandParentIDRealname\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserGrandParentIDRealname) + "\"}";

                            }
                        }
                        else
                        {
                            intErrorCode = -58;///用户下过单后，才能申请调整上下级关系
                        }
                    }
                }

                #endregion 前端录入合法性检查



                str = "{\"ErrorCode\":\"" + intErrorCode + "\",\"ShowInfo\":" + strShowInfo + "}";
                Eggsoft.Common.debug_Log.Call_WriteLog(str, "运营中心服务修改上下级关系", "返回数据");


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
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "运营中心修改上下级关系", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "运营中心修改上下级关系");
            }
            finally
            {

            }

            return str;
        }



        /// <summary>
        ///运营中心录单系统 。检查录入的ID是否存在  并是否是本运营中心的。如果 存在  就自动 带出上级。     
        /// </summary>
        /// <returns></returns>
        [WebMethod]

        public String _Service_Check_ShopUserID_OperationCenter()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            string strShowInfo = "0";
            try
            {
                String strBuyOrderShopUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["BuyOrderUserID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strJSUserSign = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["JSUserSign"]);
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);




                int intErrorCode = 0;

                EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                string strUserIDRealname = "";
                #region 前端录入合法性检查
                EggsoftWX.Model.tab_User Model_tab_UserAsk = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strBuyOrderShopUserID.toInt32(), strShopClientID);
                if (Model_tab_UserAsk == null)
                {
                    intErrorCode = -78;///用户微店ID不存在
                }
                else
                {
                    strUserIDRealname = Model_tab_UserAsk.UserRealName;
                }

                #region 检查签名
                EggsoftWX.Model.tab_User curModel_tab_User = blltab_User.GetModel(strUserID.toInt32());
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strBuyOrderShopUserID + strShopClientID + strUserID + Eggsoft.Common.DESCrypt.hex_md5_2(curModel_tab_User.SafeCode));
                if (strJSUserSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("" + curModel_tab_User.SafeCode, "运营中心录单系统签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    intErrorCode = -88;///签名失败  
                }
                #endregion 检查签名



                if (intErrorCode == 0)
                {






                    ///本运营中心 才自动带出信息
                    EggsoftWX.BLL.b005_UserID_Operation_ID bllb005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                    EggsoftWX.Model.b005_UserID_Operation_ID Modelb005_UserID_Operation_ID = bllb005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID and OperationCenterID_UserID=@OperationCenterID_UserID", Model_tab_UserAsk.ID, strShopClientID.toInt32(), strUserID.toInt32());
                    if (Modelb005_UserID_Operation_ID == null)
                    {///不是本运营中心的  不能带出信息
                        intErrorCode = 12;
                    }
                    else if (Modelb005_UserID_Operation_ID.UserParentID == Model_tab_UserAsk.ParentID)
                    {///2个表的信息 完全吻合
                        intErrorCode = 1;

                        strShowInfo = "{\"BuyOrderShopUserIDRealName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserIDRealname) + "\"";
                        strShowInfo += ",\"BuyOrderShopUserIDIDCard\":\"" + (Model_tab_UserAsk.IDCard.toString()) + "\"";
                        strShowInfo += ",\"BuyOrderShopUserIDContactPhone\":\"" + (Model_tab_UserAsk.ContactPhone.toString()) + "\"";
                        strShowInfo += ",\"BuyParentShopUserID\":\"" + Eggsoft_Public_CL.Pub.GetMyShopUserID(Model_tab_UserAsk.ParentID.toString()) + "\"";
                        strShowInfo += "}";
                    }
                    else
                    {////信息不吻合  也不带出。
                        intErrorCode = 13;
                        Eggsoft.Common.debug_Log.Call_WriteLog("Operation_ID.UserParentID=" + Modelb005_UserID_Operation_ID.UserParentID + "  tab_UserAsk.ParentID-" + Model_tab_UserAsk.ParentID, "运营中心录单系统", "信息不吻合pub_Int_Session_CurUserID=" + strUserID);


                    }


                }

                #endregion 前端录入合法性检查



                str = "{\"ErrorCode\":\"" + intErrorCode + "\",\"ShowInfo\":" + strShowInfo + "}";
                Eggsoft.Common.debug_Log.Call_WriteLog(str, "运营中心录单系统", "返回数据");


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
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "运营中心录单系统", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "运营中心录单系统");
            }
            finally
            {

            }

            return str;
        }

        /// <summary>
        ///运营中心录单系统 。检查录入的上级ID是否合法  有代理资质。     
        /// </summary>
        /// <returns></returns>
        [WebMethod]

        public String _Service_Check_ShopUserID_OperationCenter_BuyParentShopUserID()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            string strShowInfo = "0";
            try
            {
                String strBuyParentShopUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["BuyParentShopUserID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strJSUserSign = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["JSUserSign"]);
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);




                int intErrorCode = 0;



                #region 检查签名
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User curModel_tab_User = BLL_tab_User.GetModel(strUserID.toInt32());

                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strBuyParentShopUserID + strShopClientID + strUserID + Eggsoft.Common.DESCrypt.hex_md5_2(curModel_tab_User.SafeCode));
                if (strJSUserSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("" + curModel_tab_User.SafeCode, "运营中心录单系统录入上级签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    intErrorCode = -88;///签名失败  
                }
                #endregion 检查签名




                if (intErrorCode == 0)
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ curtab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("ShopClientID=@ShopClientID and UserID=@UserID  and IsDeleted=0 ", strShopClientID.toInt32(), Eggsoft_Public_CL.Pub.GetMyUserIDFromShopUserID(strBuyParentShopUserID.toInt32(), strShopClientID.toInt32()));




                    if (curtab_ShopClient_Agent_ != null)
                    {///2个表的信息 完全吻合
                        intErrorCode = 1;


                    }
                    else
                    {////信息不吻合  也不带出。
                        intErrorCode = -1;

                    }


                }





                str = "{\"ErrorCode\":" + intErrorCode + "}";
                Eggsoft.Common.debug_Log.Call_WriteLog(str, "运营中心录单系统", "返回数据");


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
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "运营中心录单系统", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "运营中心录单系统");
            }
            finally
            {

            }

            return str;
        }

        /// <summary>
        ///检查输入的支付流水号 
        /// </summary>
        /// <returns></returns>
        [WebMethod]

        public String _Service_Check_BuyOrderPaySerialNumber()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            string strShowInfo = "0";
            try
            {
                String strBuyOrderPaySerialNumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["BuyOrderPaySerialNumber"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strJSUserSign = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["JSUserSign"]);
                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);




                int intErrorCode = 0;



                #region 检查签名
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User curModel_tab_User = BLL_tab_User.GetModel(strUserID.toInt32());

                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strBuyOrderPaySerialNumber + strShopClientID + strUserID + Eggsoft.Common.DESCrypt.hex_md5_2(curModel_tab_User.SafeCode));
                if (strJSUserSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("" + curModel_tab_User.SafeCode, "运营中心录单系统录入支付流水号签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    intErrorCode = -88;///签名失败  
                }
                #endregion 检查签名




                if (intErrorCode == 0)
                {
                    //[ID]
                    // ,[PayStatus]
                    // ,[isReceipt]
                    // ,[CreateDateTime]
                    // ,[UserID]
                    // ,[DeliveryText]
                    // ,[TotalMoney]
                    // ,[OrderNum]
                    // ,[ShopClient_ID]
                    // ,[OrderName]
                    // ,[User_Address]
                    // ,[PayWay]
                    // ,[PaywayOrderNum]
                    // ,[PayDateTime]
                    // ,[O2OTakedID]

                    StringBuilder mySQL = new StringBuilder();
                    mySQL.Append("ShopClient_ID=");
                    mySQL.Append(strShopClientID);
                    mySQL.Append(" and PaywayOrderNum=");
                    mySQL.Append("'");
                    mySQL.Append(strBuyOrderPaySerialNumber);
                    mySQL.Append("'");
                    mySQL.Append(" and isnull(IsDeleted,0)=0");
                    EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    Boolean BooleanOrder = BLL_tab_Order.Exists(mySQL.ToString());

                    if (BooleanOrder == false)
                    {///可以录单
                        intErrorCode = 1;
                    }
                    else
                    {////订单表中已经存在数据
                        intErrorCode = 2;
                    }
                }


                str = "{\"ErrorCode\":" + intErrorCode + "}";
                Eggsoft.Common.debug_Log.Call_WriteLog(str, "运营中心录单系统", "录入支付流水号返回数据");


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
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "运营中心录单系统", "录入支付流水号线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "运营中心录单系统", "录入支付流水号程序报错");
            }
            finally
            {

            }

            return str;
        }


    }
}
