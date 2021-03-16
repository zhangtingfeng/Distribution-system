using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.User
{
    /// <summary>
    /// WebS_User 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebS_User : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        /// <summary>
        /// 计算TotalCredits
        /// </summary>

        /// <returns></returns>


        public string Serv_Get_User_TotalCredits()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            string strUserID = context.QueryString["strUserID"];
            int pIntUserID = 0;
            int.TryParse(strUserID, out pIntUserID);



            Decimal myTotalCreditsMoney = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(pIntUserID, out myTotalCreditsMoney);

            Decimal myTotal_VouchersMoney_RedMoney = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(pIntUserID, out myTotal_VouchersMoney_RedMoney);

            string str = "{\"TotalCredits\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(myTotalCreditsMoney) + "\",\"Total_Vouchers_RedMoney\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(myTotal_VouchersMoney_RedMoney) + "\"}";
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



        [WebMethod]

        public string Serv_Get_GoodIDTotalCredits_()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            string strGoodID = context.QueryString["strGoodID"];

            int pIntGoodID = 0;
            int.TryParse(strGoodID, out pIntGoodID);

            Decimal goodTotalCreditsMoney = 0;
            //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuArg_Shopping_Vouchers(pIntGoodID, out goodTotalCreditsMoney);


            string str = "{\"GoodIDTotalCredits\":\"" + Eggsoft_Public_CL.Pub.getPubMoney(goodTotalCreditsMoney) + "\"}";
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

    }
}
