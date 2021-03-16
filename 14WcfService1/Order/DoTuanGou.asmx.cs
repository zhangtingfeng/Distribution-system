using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Order
{
    /// <summary>
    /// DoTuanGou 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DoTuanGou : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        /// <summary>
        /// 添加团购商品到购物车。完成支付才算是参团成功。
        /// </summary>
        /// <param name="strGoodID">商品号</param>
        /// <param name="strParentID">分享人</param>
        /// <returns></returns>
        public String _Service_AddToCart_TuanGou()
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
                String strTuanGouID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["TuanGouID"]);////商品种类
                String strTuanGouIDNumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["TuanGouIDNumber"]);

                int intErrorCode = 0;

                if (strGoodID == null) { intErrorCode = 6; };
                int pIntUserID = Convert.ToInt32(strUserID);
                int pIntGoodID = Convert.ToInt32(strGoodID);
                int intParentID = Convert.ToInt32(strParentID);
                int intTuanGouID = Convert.ToInt32(strTuanGouID);///0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                                                                 ///商品 来源 类型  表的 主键   正常订单 这个 为空  .微砍价 团购 会出现相关主键 商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                int intTuanGouIDNumber = Convert.ToInt32(strTuanGouIDNumber);

                intErrorCode = Eggsoft_Public_CL.ShoppingCart.AddToShoppingCart(pIntUserID, pIntGoodID, Int32.Parse(strbuyCount), 0, new string[] { "0" }, new string[] { "0" }, new string[] { "0" }, 2, intTuanGouID, intTuanGouIDNumber,"");


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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return str;
        }
    }
}
