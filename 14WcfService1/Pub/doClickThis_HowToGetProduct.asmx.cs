using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doClickThis_HowToGetProduct 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doClickThis_HowToGetProduct : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string doSelectThis()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            try
            {
                string strIntUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strIntUserID"]);
                int pIntUserID = 0;
                int.TryParse(strIntUserID, out pIntUserID);

                string strpInt_HowToGetProduct = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strpHowToGetProduct"]);
                int pInt_HowToGetProduct = 0;
                int.TryParse(strpInt_HowToGetProduct, out pInt_HowToGetProduct);

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pIntUserID);
                Model_tab_User.HowToGetProduct = pInt_HowToGetProduct;
                BLL_tab_User.Update(Model_tab_User);
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            string str = "";
            str = "{\"ErrorCode\":0}";////表示ok            
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }

            return "1";
        }


        [WebMethod]
        public string doGetProduct_newTmpletAnnouncePic_GoodList()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "{\"ErrorCode\":-1}";
            try
            {
                string strGoodID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strGoodID"]);
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


                str = "{\"ErrorCode\":0,\"msg\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strMakeHtml_AnnouncePic_GoodList) + "\"}"; ;
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }



            //str = "{\"ErrorCode\":0}";////表示ok            
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }

            return "1";
        }

        [WebMethod]
        public string doGet_Pub_03Footer()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "{\"ErrorCode\":-1}";
            try
            {

                string strvarShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int pub_Int_Session_ShopClientID = 0;
                int.TryParse(strvarShopClientID, out pub_Int_Session_ShopClientID);
        
                string strvarGetUseid = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strvarGetUseid"]);
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strvarGetUseid, out pub_Int_Session_CurUserID);
                if (pub_Int_Session_CurUserID > 0)
                {
                    string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();
                    string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
                    string strPub_Agent_Path = Pub_Agent_Path;

                    int pub_Int_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strvarGetUseid);
                    if (pub_Int_ShopClientID == pub_Int_Session_ShopClientID)
                    {
                        string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
                        STR_tab_ShopClient_ModelUpLoadPath += "/Html";
                        string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/03Footer.html";
                        string strFooter = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_03Footer_html);
                        String _Pub_03Footer_html = strFooter.Replace("###SAgentPath###", strPub_Agent_Path);
                        _Pub_03Footer_html = _Pub_03Footer_html.Replace("###Pub_ShowAgent_SubMix_Text###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID));

                        //context.Response.Write(_Pub_03Footer_html);
                        str = "{\"ErrorCode\":0,\"Pub_03Footer\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(_Pub_03Footer_html) + "\"}"; ;
                    }
                }


            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }



            //str = "{\"ErrorCode\":0}";////表示ok            
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }

            return "1";
        }


    }
}
