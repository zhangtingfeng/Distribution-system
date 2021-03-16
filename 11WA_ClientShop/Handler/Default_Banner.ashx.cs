using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Default_Banner 的摘要说明
    /// </summary>
    public class Default_Banner : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            try
            {
                context.Response.ContentType = "text/plain";
                string strpub_Int_ShopClientID = context.Request.QueryString["strpub_Int_ShopClientID"];
                //
                //string strContext=
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();

                string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
                STR_tab_ShopClient_ModelUpLoadPath += "/Html";



                string STR_01Banner_html = STR_tab_ShopClient_ModelUpLoadPath + "/01Banner.html";
                string strBanner = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_01Banner_html);




                context.Response.Write(strBanner);
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