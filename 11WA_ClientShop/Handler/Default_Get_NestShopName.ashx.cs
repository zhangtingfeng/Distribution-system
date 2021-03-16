using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Default_Get_NestShopName 的摘要说明
    /// </summary>
    public class Default_Get_NestShopName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strpub_UserID = context.Request.QueryString["strpub_Int_Session_CurUserID"];
            string strNearestShopName = "";
            try
            {
                int intOutShop = Int32.Parse(strpub_UserID);
                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                //
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo();

                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(intOutShop, out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);

                strNearestShopName = Model_tab_ShopClient_O2O_ShopInfo.ShopName;
                strNearestShopName = Eggsoft.Common.CommUtil.getShortText(strNearestShopName, 8);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally { }




            context.Response.Write(strNearestShopName);
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