using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// MultiButton_Cart 的摘要说明
    /// </summary>
    public class MultiButton_Cart : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            string strCartGoods = "";

            try
            {
                string strQureyUserID = context.Request.QueryString["strQureyUserID"];
                string strLock = strQureyUserID + "09348925u3495u23948Lock";
                lock (strLock)
                {
                    string strQureyID_ShopingCart = Eggsoft.Common.CommUtil.SafeFilter(context.Request.QueryString["strQureyID_ShopingCart"]);
                    string strQureyGoodCount = Eggsoft.Common.CommUtil.SafeFilter(context.Request.QueryString["strQureyGoodCount"]);
                    ClassCart myCodeCart = new ClassCart();
                    strCartGoods = myCodeCart.getClassCart(strQureyID_ShopingCart, strQureyGoodCount, strQureyUserID);
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {


            }


            context.Response.Write(strCartGoods);
        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}