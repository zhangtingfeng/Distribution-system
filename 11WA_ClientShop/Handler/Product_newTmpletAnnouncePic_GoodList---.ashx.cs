using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Product_newTmpletAnnouncePic_GoodList 的摘要说明
    /// </summary>
    public class Product_newTmpletAnnouncePic_GoodList : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {

            try
            {
                context.Response.ContentType = "text/plain";
                string strGoodID = context.Request.QueryString["strGoodID"];
                //
                //string strContext=
                int pIntGoodID = 0;
                int.TryParse(strGoodID, out pIntGoodID);




                // string strVisitUserListImgeAndName = Eggsoft_Public_CL.GoodP_MakeHtml.VisitUserListImgeAndName(pIntGoodID);
                string strMakeHtml_AnnouncePic_GoodList = "";
                strMakeHtml_AnnouncePic_GoodList += "    <div style=\"-webkit-transform: translate3d(0,0,0);\">\n";
                strMakeHtml_AnnouncePic_GoodList += "        <div style=\"visibility: visible;\" id=\"banner_box\" class=\"box_swipe\">\n";



                strMakeHtml_AnnouncePic_GoodList += Eggsoft_Public_CL.GoodP_MakeHtml.MakeHtml_AnnouncePic_GoodList(pIntGoodID);

                strMakeHtml_AnnouncePic_GoodList += " </div>\n";
                strMakeHtml_AnnouncePic_GoodList += "     </div>\n";
                strMakeHtml_AnnouncePic_GoodList += "     <script>\n";
                strMakeHtml_AnnouncePic_GoodList += "         $(function () {\n";
                strMakeHtml_AnnouncePic_GoodList += "             new Swipe(document.getElementById('banner_box'), {\n";
                strMakeHtml_AnnouncePic_GoodList += "                 speed: 500,\n";
                strMakeHtml_AnnouncePic_GoodList += "                 auto: 3000,\n";
                strMakeHtml_AnnouncePic_GoodList += "                 callback: function () {\n";
                strMakeHtml_AnnouncePic_GoodList += "                     var lis = $(this.element).next(\"ol\").children();\n";
                strMakeHtml_AnnouncePic_GoodList += "                    lis.removeClass(\"on\").eq(this.index).addClass(\"on\");\n";
                strMakeHtml_AnnouncePic_GoodList += "                 }\n";
                strMakeHtml_AnnouncePic_GoodList += "             });\n";
                strMakeHtml_AnnouncePic_GoodList += "        });\n";
                strMakeHtml_AnnouncePic_GoodList += "     </script>   \n";


                context.Response.Write(strMakeHtml_AnnouncePic_GoodList);
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