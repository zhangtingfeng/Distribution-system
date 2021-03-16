using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Handler_CheckQuan 的摘要说明
    /// </summary>
    public class Handler_CheckQuan : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
         
            try
            {
                string json = GetQuanWish(context.Request["strNum"], context.Request["strUserID"], context.Request["strvarPromotePriceWillPay"], context.Request["ToShopCilentID"]);
                context.Response.Write(json);
                context.Response.ContentType = "text/plain";
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione,"商品页面点击优惠券");
            }
            finally
            {

            }
            //context.Response.Write("123456");
        }

        public static string GetQuanWish(string strNum, string strUserID, string strvarPromotePriceWillPay, string strToShopCilentID)
        {
            string data = "";
            if ((String.IsNullOrEmpty(strNum)) || (String.IsNullOrEmpty(strUserID)) || (String.IsNullOrEmpty(strvarPromotePriceWillPay)))
            {
                if (String.IsNullOrEmpty(strNum)) { data = "{\"ReturnCode\":\"-1\",\"ReturnDes\":\"\"}"; };
                if (String.IsNullOrEmpty(strUserID)) { data = "{\"ReturnCode\":\"-3\",\"ReturnDes\":\"\"}"; };
                if (String.IsNullOrEmpty(strvarPromotePriceWillPay)) { data = "{\"ReturnCode\":\"-4\",\"ReturnDes\":\"\"}"; };
                return data;
            }
            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail EggsoftWX_BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
            EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail EggsoftWX_Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

            if (false == EggsoftWX_BLL_tab_Shopping_Vouchers.Exists("VouchersNum='" + strNum + "'"))
            {
                data = "{\"ReturnCode\":\"-1\",\"ReturnDes\":\"\"}";
                return data;
            }

            EggsoftWX_Model_tab_Shopping_Vouchers = EggsoftWX_BLL_tab_Shopping_Vouchers.GetModel("VouchersNum='" + strNum + "'");

            #region 检查是否要求合法使用人
            String strDB_UserID = EggsoftWX_Model_tab_Shopping_Vouchers.UserID.ToString();

            if ((String.IsNullOrEmpty(strDB_UserID) == false) && (strDB_UserID != "0") && (strDB_UserID != strUserID))
            {
                data = "{\"ReturnCode\":\"-5\",\"ReturnMoney\":\"" + "" + "\"}";
                return data;
            }
            #endregion
            #region 检查是否消费过
            if (EggsoftWX_Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID.toInt32() > 0)
            {
                data = "{\"ReturnCode\":\"-2\",\"ReturnDes\":\"\"}";
                return data;
            }
            #endregion

            #region 检查是否有效期  合法期
            if (EggsoftWX_Model_tab_Shopping_Vouchers.ValidateEndTime.toDateTime() < DateTime.Now)
            {
                data = "{\"ReturnCode\":\"-3\",\"ReturnDes\":\"\"}";
                return data;
            }
            #endregion

            #region 检查是否有 合法期
            DateTime DateTimeNow = DateTime.Now;
            if ((DateTimeNow < EggsoftWX_Model_tab_Shopping_Vouchers.ValidateStartTime))
            {
                data = "{\"ReturnCode\":\"-6\",\"ReturnDes\":\"\"}";
                return data;
            }
            #endregion

            #region 检查是否有效期  合法期
            if (DateTimeNow > EggsoftWX_Model_tab_Shopping_Vouchers.ValidateEndTime)
            {
                data = "{\"ReturnCode\":\"-7\",\"ReturnDes\":\"\"}";
                return data;
            }
            #endregion


            #region 检查购物券是否属于该店铺 strToShopCilentID
            if ((strToShopCilentID != EggsoftWX_Model_tab_Shopping_Vouchers.ShopClientID.ToString()) && EggsoftWX_Model_tab_Shopping_Vouchers.ShopClientID != 0)
            {
                data = "{\"ReturnCode\":\"-8\",\"ReturnDes\":\"\"}";
                return data;
            }
            EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(Int32.Parse(strToShopCilentID));
            if ((Model_ShopClient.ShenMaShopping == false) && (Model_ShopClient.Shopping_Vouchers == false))
            {
                data = "{\"ReturnCode\":\"-9\",\"ReturnDes\":\"\"}";
                return data;
            }

            #endregion





            #region ok

            Decimal Shopping_Vouchers_decimalMoney = EggsoftWX_Model_tab_Shopping_Vouchers.Money.toDecimal();
            Decimal Decimal_strvarPromotePriceWillPay = 0;
            Decimal.TryParse(strvarPromotePriceWillPay, out Decimal_strvarPromotePriceWillPay);

            if (Shopping_Vouchers_decimalMoney <= Decimal_strvarPromotePriceWillPay)
            {
                data = "{\"ReturnCode\":\"1\",\"ReturnMoney\":\"" + Shopping_Vouchers_decimalMoney + "\"}";
                return data;
            }
            else
            {
                data = "{\"ReturnCode\":\"1\",\"ReturnMoney\":\"" + Decimal_strvarPromotePriceWillPay + "\"}";
                return data;
            }


            #endregion



            /*
        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        context.Response.ContentType = "text/plain";
        System.Collections.Generic.Dictionary<string, string> drow = new System.Collections.Generic.Dictionary<string, string>();
        drow.Add("name", "Wang");
        drow.Add("age", "24");
        context.Response.Write(jss.Serialize(drow));


        string data = "{\"name\":\"wang\",\"age\":25}";


        string data = "{\"name\":\"wang\",\"age\":25}";

        String String_OpenID = GetOpenID_FromDateBase(strID);
        if (String_OpenID != strNowID)
        {
            // string strID = Eggsoft.Common.Cookie.Read("webuy8_ClientAdmin_Users").ToString();

            strResult = String_OpenID;
        }
        else
        {
            strResult = "0";
        }


        //string json = "[{'OpenID':'" + strResult + "'}]";
        string json = strResult;
        context.Response.Write(json);
        
        return string.Format("祝您在{3}年里 {0}、{1}、{2}", value1, value2, value3, value4);*/
        }

        /*
        private static String GetOpenID_FromDateBase(String strID)
        {

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("ID=" + strID + "");

            string strOpenID = "0";
            if (Model_tab_ShopClient != null)
            {


                //strOpenID = Model_tab_ShopClient.OpenID;
                if (Model_tab_ShopClient.OpenID != "")
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();


                    strOpenID = BLL_tab_User.GetList("ID", "OpenID='" + Model_tab_ShopClient.OpenID + "'").Tables[0].Rows[0][0].ToString();
                }
                else
                {

                }

            }
            else
            {

            }
            return strOpenID;
        }

        */
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}