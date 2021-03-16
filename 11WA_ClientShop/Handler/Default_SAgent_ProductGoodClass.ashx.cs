using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Default_SAgent_ProductGoodClass 的摘要说明
    /// </summary>
    public class Default_SAgent_ProductGoodClass : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string strpub_Int_Session_CurUserID = context.Request.QueryString["strpub_Int_Session_CurUserID"];
                string strInt_ShopClientID = context.Request.QueryString["strpub_Int_ShopClientID"];
                string strpClassGoodType = context.Request.QueryString["pClassGoodType"];
                string strpClassID = context.Request.QueryString["pClassID"];

                int intClassGoodType = 0;
                int.TryParse(strpClassGoodType, out intClassGoodType);
                int intpClassID = 0;
                int.TryParse(strpClassID, out intpClassID);

                //
                //string strContext=
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strInt_ShopClientID, out pub_Int_ShopClientID);

                // context.Response.Write(strVisitUserListImgeAndName);
                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + pub_Int_ShopClientID);

                if (Model_tab_ShopClient_ShopPar.DeafaultOnlyShowAnounceBitmap.toBoolean()) //这家公司比较特殊 首页只要三张大的轮播图，满足她们把
                {
                    context.Response.Write("");
                }
                else
                {
                    string strGet_SAgent_ProductGoodClass = Eggsoft_Public_CL.Pub_Agent.strGet_SAgent_ProductGoodClass(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path, intClassGoodType, intpClassID);
                    context.Response.Write(strGet_SAgent_ProductGoodClass);
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
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