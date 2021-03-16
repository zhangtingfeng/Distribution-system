using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WriteHtml
{
    public partial class writeHtml_StyleModel3 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private static object objLock = new object();

        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label_Memory.Text = "0";
            }
        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string strpShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                int intShopClient = Int32.Parse(strpShopClientID);
                //int intShopClient = 5;

                int intStartInt = Int32.Parse(Label_Memory.Text);


                intStartInt++;
                Label_Memory.Text = intStartInt.ToString();
                lResult0_Show.Text = "<br />" + (intStartInt * 10).ToString() + "%";
                //lResult.Text += "<br />" + (intStartInt*10).ToString()+"%";

                if (intStartInt == 1)
                {
                    lock (objLock)
                    { makebanner(intShopClient); }
                }
                else if (intStartInt == 2)
                {
                    //maketopbar(intShopClient);
                }
                else if (intStartInt == 3)
                {
                    lock (objLock)
                    { Class_Pub.makeFooter(intShopClient); }
                    //                makeFooter(intShopClient);
                }
                else if (intStartInt == 4)
                {
                    lock (objLock)
                    { makeIndex(intShopClient); }
                }
                else if (intStartInt == 8)
                {

                    //JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/18tab_GoodClass/Board_Good.aspx");

                    //Eggsoft.Common.JsUtil.TipAndRedirect("生成完成", strClientAdminURL + "/ClientAdmin/right.aspx","1");

                }
                else if (intStartInt == 10)
                {
                    lResult0_Show.Text += "<br />生成完成";

                    Timer1.Enabled = false;
                    //                Response.End();
                }


            }

            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally
            {

            }
        }

        protected void makebanner(int intShopClient)
        {
            string strHTML = "";





            string strLabel1 = "";
            string strLabel2 = "";
            EggsoftWX.BLL.tab_AnnouncePic BLL_tab_AnnouncePic = new EggsoftWX.BLL.tab_AnnouncePic();
            System.Data.DataTable myDataTable = BLL_tab_AnnouncePic.GetList("UserID=" + intShopClient + " order by pos asc,id asc").Tables[0];

            //strLabel1 += "<ul class=\"pic_list\" data-tag=\"slider-list\" style=\"width: " + myDataTable.Rows.Count * 640 + "px;\">\n";

            strLabel1 += "            <ul style=\"list-style: none outside none; width: " + myDataTable.Rows.Count * 640 + "px; transition-duration: 500ms;transform: translate3d(0px, 0px, 0px);\">\n";

            strHTML += "<div id=\"Anounce\">\n";
            strHTML += "    <div style=\"-webkit-transform: translate3d(0,0,0);\">\n";
            strHTML += "        <div style=\"visibility: visible;\" id=\"banner_box\" class=\"box_swipe\">\n";
            strHTML += "            ####<%=strLabel1%>####\n";

            if (myDataTable.Rows.Count > 1)//有2个才出现轮播图下面的位置示意图
            {
                strHTML += "            <ol>\n";
                strHTML += "             ####<%=strLabel2%>####\n";
                strHTML += "            </ol>\n";
            }
            strHTML += "        </div>\n";
            strHTML += "    </div>\n";
            strHTML += "    <script>\n";
            strHTML += "        $(function () {\n";
            strHTML += "            new Swipe(document.getElementById('banner_box'), {\n";
            strHTML += "                speed: 500,\n";
            strHTML += "                auto: 3000,\n";
            strHTML += "                callback: function () {\n";
            strHTML += "                    var lis = $(this.element).next(\"ol\").children();\n";
            strHTML += "                    lis.removeClass(\"on\").eq(this.index).addClass(\"on\");\n";
            strHTML += "                }\n";
            strHTML += "            });\n";
            strHTML += "        });\n";
            strHTML += "    </script>\n";
            strHTML += "</div>\n";
            //strHTML += "</div>\n";


            for (int inti = 0; inti < myDataTable.Rows.Count; inti++)
            {

                String strPicUrl = myDataTable.Rows[inti]["PicUrl"].ToString();
                String strShowText = myDataTable.Rows[inti]["ShowText"].ToString();
                String strLinkURL = myDataTable.Rows[inti]["LinkURL"].ToString();

                strLabel1 += "                <li style=\"width: 640px; display: table-cell; vertical-align: top;\"><a href=\"" + strLinkURL + "\"><img src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPicUrl + "\" alt=\"" + strShowText + "\" style=\"width:100%;\"></a></li>\n";

                if (inti == myDataTable.Rows.Count - 1)
                {
                    strLabel2 += " <li class=\"on\"></li>\n";
                }
                else
                {
                    strLabel2 += "<li class=\"\"></li>\n";
                }

            }

            strLabel1 += "</ul>\n";


            strHTML = strHTML.Replace("####<%=strLabel1%>####", strLabel1);
            strHTML = strHTML.Replace("####<%=strLabel2%>####", strLabel2);

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClient);
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";
            string strpath = System.Web.HttpContext.Current.Server.MapPath(STR_tab_ShopClient_ModelUpLoadPath);
            Eggsoft.Common.FileFolder.makeFolder(strpath);
            STR_tab_ShopClient_ModelUpLoadPath += "/01Banner.html";

            Eggsoft.Common.FileFolder.WriteFile(STR_tab_ShopClient_ModelUpLoadPath, strHTML);

        }


        protected void makeIndex(int intShopClient)
        {

            string strpShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Int32.Parse(strpShopClientID));
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";

            //string STR_02Topbar_html = STR_tab_ShopClient_ModelUpLoadPath + "/02Topbar.html";
            //string strTopbar = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_02Topbar_html);


            string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/03Footer.html";
            string strFooter = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_03Footer_html);



            string strHTML = "";

            strHTML += "<!DOCTYPE html>\n";
            strHTML += "<html>\n";
            strHTML += "<head>\n";
            strHTML += "    <meta http-equiv=\"Content-type\" content=\"text/html; charset=utf-8\">\n";
            strHTML += "    <title>###SAgent__Pub_dBody_Title###</title>\n";
            strHTML += "    <meta name=\"viewport\" content=\"width=640,target-densitydpi=device-dpi,user-scalable=no\" />\n";
            //strHTML += "    <meta name=\"viewport\" content=\"width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;\" />\n";
            strHTML += "    <!-- Mobile Devices Support @begin -->\n";
            strHTML += "    <meta content=\"no-cache,must-revalidate\" http-equiv=\"Cache-Control\" />\n";
            strHTML += "    <meta content=\"no-cache\" http-equiv=\"pragma\" />\n";
            strHTML += "    <meta content=\"0\" http-equiv=\"expires\" />\n";
            strHTML += "    <meta content=\"telephone=no, address=no\" name=\"format-detection\" />\n";
            strHTML += "    <meta name=\"apple-mobile-web-app-capable\" content=\"yes\" />\n";
            strHTML += "    <!-- apple devices fullscreen -->\n";
            strHTML += "    <meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black-translucent\" />\n";
            strHTML += "    <!-- Mobile Devices Support @end -->\n";
            strHTML += "    <script src=\"/Templet/02ShiYi/js/jquery-1.8.3.js?version=js201709121928\" type=\"text/javascript\"></script>\n";
            strHTML += "    <link href=\"/Templet/02ShiYi/skin/ys.css?version=css201709121928\" rel=\"stylesheet\" type=\"text/css\">\n";
            strHTML += "    <link href=\"/Templet/02ShiYi/skin/foot.css?version=css201709121928\" rel=\"stylesheet\" type=\"text/css\">\n";
            strHTML += "    <script src=\"/Templet/02ShiYi/js/Footer_common.js?version=js201709121928\"></script>\n";
            strHTML += "    <link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/02ShiYi/skin/box_swipe.css?version=css201709121928\" media=\"all\">\n";
            strHTML += "    <script type=\"text/javascript\" src=\"/Templet/02ShiYi/js/swipe.js?version=js201709121928\"></script>\n";
            strHTML += "    <script type=\"text/javascript\" src=\"/Templet/01WYJS/js/func_Image.js?version=js201709121928\"></script>\n";
            strHTML += "    <link href=\"/Templet/01WYJS/Css/mall_Templet1.css?version=css201709121928\" type=\"text/css\" rel=\"stylesheet\">\n";
            strHTML += "    <link href=\"/Templet/01WYJS/Css/base.css?version=css201709121928\" type=\"text/css\" rel=\"stylesheet\">\n";
            strHTML += "    <link href=\"/Templet/01WYJS/Css/font-awesome.css?version=css201709121928\" type=\"text/css\" rel=\"stylesheet\">\n";
            strHTML += "    <link href=\"/Templet/01WYJS/Css/itemListTemplate.css?version=css201709121928\" type=\"text/css\" rel=\"stylesheet\">\n";
            strHTML += "    <link href=\"/Templet/03LiuShenMa/Css/02Templet01.css?version=css201709121928\" type=\"text/css\" rel=\"stylesheet\">\n";
            strHTML += "    <link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/01WYJS/Css/SearchBar-mobile-template.css?version=css201709121928\">\n";
            strHTML += "    <link href=\"/Templet/04StyleModel/Css/StyleSheet4.css?version=css201709121928\" rel=\"stylesheet\" />";
            strHTML += "    ###WeiXin__o2o_FootMarker_Location_###\n";
            strHTML += "    <script src=\"/Templet/01WYJS/js/Base.js?version=js201709121928\"></script>\n";
            strHTML += " <script type=\"text/javascript\">\n";
            strHTML += " var varUserID = \"###UserID###\";///传递给 product.js   传递变量\n";
            strHTML += " var varServiceURL = \"###ServiceURL###\";///传递给 product.js   传递变量\n";
            strHTML += " var varShopClientID = \"###ShopClientID###\";///传递给 product.js   传递变量\n";

            strHTML += " function load_Multi() {\n";
            strHTML += "     doVisitThisPage();";
            strHTML += "     doDefault_Banner();";
            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
            bool boolExsit_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + strpShopClientID);
            if (boolExsit_O2O_ShopInfo)
            {
                strHTML += "doDefault_Nearest_ShopName();";
            }
            strHTML += "     doDefault_SAgent_ProductGoodClass();";
            strHTML += "     doDefault_SAgent_ProductNewGood();";
            strHTML += "     doInfoAlert_Msg(###UserID###);";
            strHTML += "  }\n";

            strHTML += "    function btnSearch() {\n";
            strHTML += "       var varSearchKey= document.getElementById(\"oliversearch-input_s_1447079356776\").value;\n";
            strHTML += "       var varSearchURL= encodeURI('###SAgentPath###/productsearchgoods.aspx?searchkey=' + varSearchKey.toLowerCase());\n";
            strHTML += "      window.location.href = varSearchURL;\n";
            strHTML += " }\n";

            strHTML += "    function doVisitThisPage() {\n";
            strHTML += "   var url=\"###ServiceURL###/Pub/doVisiDefault.asmx/doVisitDefaultAction?strpInt_QueryString_ParentID=###DBParentID###&strpub_Int_Session_CurUserID=###UserID###&strpub_Int_ShopClientID=###ToShopCilentID###\";\n";
            strHTML += "\n";
            strHTML += "    var result=-1;\n";
            strHTML += "   $.ajax({\n";
            strHTML += "      type: \"POST\",\n";
            strHTML += "       url: url,\n";
            strHTML += "      dataType: \"jsonp\",\n";
            strHTML += "      jsonp: \"jsonp\", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)\n";
            strHTML += "      jsonpCallback: \"jsonp1Callback\", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名\n";
            strHTML += "      contentType: \"application/json; charset=utf-8\",\n";
            strHTML += "      success: function (json) {\n";
            strHTML += "          result = parseInt(json.ErrorCode);\n";
            strHTML += "          return;\n";
            strHTML += "      },\n";
            strHTML += "      error: function () {\n";
            strHTML += "      }\n";
            strHTML += "  });\n";
            strHTML += "   return result;\n";
            strHTML += " }\n";

            strHTML += "   function doDefault_Banner() {\n";
            strHTML += "    $.ajax({\n";
            strHTML += "         type:'GET',\n";
            strHTML += "         url:'/Handler/Default_Banner.ashx',\n";
            strHTML += "         dataType: 'text',\n";
            strHTML += "        data:'strpub_Int_ShopClientID=###ToShopCilentID###',\n";
            strHTML += "        beforeSend:function(){\n";
            strHTML += "            $(\"#Anounce_spro_con\").append('<img style=\"margin: 0px auto;display:block;\" src=\"/images/loading.gif\"/>');\n";
            strHTML += "        },\n";
            strHTML += "         success:function(msg)\n";
            strHTML += "        {\n";
            strHTML += "            //alert(msg);\n";
            strHTML += "             $(\"#Anounce_spro_con\").html(msg);\n";
            strHTML += "         },\n";
            strHTML += "        error: function(data){\n";
            strHTML += "          }\n";
            strHTML += "     })\n";
            strHTML += "  }\n";

            if (boolExsit_O2O_ShopInfo)
            {
                strHTML += "   function doDefault_Nearest_ShopName() {\n";
                strHTML += "    $.ajax({\n";
                strHTML += "         type:'GET',\n";
                strHTML += "         url:'/Handler/Default_Get_NestShopName.ashx',\n";
                strHTML += "         dataType: 'text',\n";
                strHTML += "        data:'strpub_Int_Session_CurUserID=###UserID###',\n";
                strHTML += "        beforeSend:function(){\n";
                strHTML += "        },\n";
                strHTML += "         success:function(msg)\n";
                strHTML += "        {\n";
                strHTML += "             $(\"#Nearest_ShopName\").html(msg);\n";
                strHTML += "         },\n";
                strHTML += "        error: function(data){\n";
                strHTML += "          }\n";
                strHTML += "     })\n";
                strHTML += "  }\n";
            }
            //data:'strGoodID='+'###GoodID###'+'&Int_ShopClientID=###ToShopCilentID###'+'&pub_Int_Session_CurUserID=###UserID###',

            strHTML += "   function doDefault_SAgent_ProductGoodClass() {\n";
            strHTML += "    $.ajax({\n";
            strHTML += "         type:'GET',\n";
            strHTML += "         url:'/Handler/Default_SAgent_ProductGoodClass.ashx',\n";
            strHTML += "         dataType: 'text',\n";
            strHTML += "        data:'pClassGoodType=0&strpub_Int_ShopClientID=###ToShopCilentID###&strpub_Int_Session_CurUserID=###UserID###',\n";
            strHTML += "        beforeSend:function(){\n";
            strHTML += "            $(\"#GoodClassStyle4\").append('<img style=\"margin: 0px auto;display:block;\" src=\"/images/loading.gif\"/>');\n";
            strHTML += "        },\n";
            strHTML += "         success:function(msg)\n";
            strHTML += "        {\n";
            strHTML += "             var varmsgList=msg.split('######');\n";
            strHTML += "             $(\"#ShopNavIcon\").html(varmsgList[0]);\n";
            strHTML += "             $(\"#GoodClassStyle4\").html(varmsgList[1]);\n";
            strHTML += "             adjustShopNavIcon();\n";
            strHTML += "         },\n";
            strHTML += "        error: function(data){\n";
            strHTML += "          }\n";
            strHTML += "     })\n";
            strHTML += "  }\n";

            strHTML += "   function doDefault_SAgent_ProductNewGood() {\n";
            strHTML += "    $.ajax({\n";
            strHTML += "         type:'GET',\n";
            strHTML += "         url:'/Handler/Default_SAgent_ProductNewGood.ashx',\n";
            strHTML += "         dataType: 'text',\n";
            strHTML += "        data:'strpub_Int_ShopClientID='+'###ToShopCilentID###'+'&strpub_Int_Session_CurUserID=###UserID###'+'&strpInt_QueryString_ParentID=###ParentID###',\n";
            strHTML += "        beforeSend:function(){\n";
            strHTML += "            $(\"#SAgent_ProductNewGoodClass\").append('<img style=\"margin: 0px auto;display:block;\" src=\"/images/loading.gif\"/>');\n";
            strHTML += "        },\n";
            strHTML += "         success:function(msg)\n";
            strHTML += "        {\n";
            strHTML += "             $(\"#SAgent_ProductNewGoodClass\").html(msg);\n";
            strHTML += "         },\n";
            strHTML += "        error: function(data){\n";
            strHTML += "          }\n";
            strHTML += "     })\n";
            strHTML += "  }\n";

            strHTML += "  </script>\n";
            strHTML += "    ###WeiXin__Share###\n";
            strHTML += "  <script src=\"/Templet/04StyleModel/Js/StyleSheet4.js\"></script>\n";
            strHTML += "</head>\n";
            strHTML += "<body onload=\"load_Multi();\" onresize=\"AutoResizeThisImage()\">\n";


            if (boolExsit_O2O_ShopInfo)
            {
                strHTML += "     <section class=\"box\" id=\"myorder\" style=\"display:block; height: auto;\">\n";
                strHTML += "    <div class=\"user_index\">\n";
                strHTML += "        <span class=\"user_header\" style=\"float:left;background:none;\" id=\"actionNavBar\">\n";
                strHTML += "            <a href=\"###SAgentPath###/offlineshop.aspx\"><span id=\"Nearest_ShopName\"></span>\n";
                strHTML += "                <span id=\"DownNav\"></span></a>\n";
                strHTML += "        </span>\n";
                strHTML += "       <span class=\"user_header\" style=\"float: right;background:none;\" id=\"actionBar\">\n";
                strHTML += "          <span style=\"background:none;\" >\n";
                strHTML += "               <a href=\"/cart.aspx\" style=\"background:none;\" class=\"shopping-cart\">\n";
                strHTML += "                  <i class=\"fa fa-shopping-cart\"></i>\n";
                strHTML += "              </a>\n";
                strHTML += "          </span>\n";
                strHTML += "         <span style=\"background:none;\"><a href=\"###SAgentPath###/historygoodslist.aspx\">我的记录</a></span>\n";
                strHTML += "     </span>\n";
                strHTML += "  </div>\n";
                strHTML += " </section>\n";
            }

            strHTML += "<div id=\"Anounce_spro_con\" class=\"spro_con\" style=\"background-color:black;\">\n";
            //strHTML += "    " + strBanner + " \n";
            strHTML += "</div>\n";

            strHTML += "    ###demo_MoblieRoll###\n";

            strHTML += "    <div style=\"clear: both;\"></div>\n";
            strHTML += "    <div class=\"oliversearch-bar\" style=\"background: #ffffff;display:block;\">\n";
            strHTML += "                     <div class=\"oliversearch-inner\">\n";
            strHTML += "              <i class=\"font-icon font-icon-oliversearch\"></i>\n";
            strHTML += "              <input class=\"oliversearch-input\" style=\"background-color: rgb(255, 255, 255); border-color: rgb(175, 175, 175); color: rgb(127, 127, 127); width: 80%; background-position: initial initial; background-repeat: initial initial;\"\n";
            strHTML += "                    id =\"oliversearch-input_s_1447079356776\" type=\"text\"\n";
            strHTML += "                    placeholder =\"搜索商品名/分类/种类/描述/全部\" onFocus=\"if((this.value == '搜索商品名/分类/种类/描述/全部' ) || this.value == '') {this.value = '';this.style.width='80%';document.getElementById('oliversearch-btn_s_1447079356706').style.display='inline-block'}\" onBlur=\"if(this.value =='') {this.value = '搜索商品名/分类/种类/描述/全部';this.style.width='100%';document.getElementById('oliversearch-btn_s_1447079356706').style.display='none'}\">\n";
            strHTML += "             <button class=\"oliversearch-btn btnSearch\" style=\"background-color: rgb(255, 0, 6); border: rgb(189, 0, 4); color: rgb(255, 255, 255); display:inline-block; background-position: initial initial; background-repeat: initial initial;\"\n";
            strHTML += "                    onclick =\"btnSearch();\" id=\"oliversearch-btn_s_1447079356706\">\n";
            strHTML += "                 搜索\n";
            strHTML += "             </button>\n";
            strHTML += "         </div>\n";
            strHTML += "     </div>\n";
            strHTML += "    <div style=\"clear: both;\"></div>\n";
            #region  模板4新增的
            strHTML += "   <div id=\"ShopNavIcon\">\n";           
           
            strHTML += "    </div>\n";
            strHTML += "     <div style=\"clear: both;\"></div>\n";
            strHTML += "    <div id=\"GoodClassStyle4\">\n";
           
            strHTML += "   </div>\n";
            #endregion  模板4新增的


            //strHTML += "<div id=\"ShopMakeOwnerEdit\" style=\"display:block;width:100%;\">";
            //strHTML += "###ShopMakeOwnerEdit###";
            //strHTML += "</div>";
            strHTML += "    <div style=\"clear: both;\"></div>\n";

            strHTML += "<div id=\"GoodAgentClass\" class=\"jx_map\"></div>\n";

            //strHTML += "            ###SAgent_ProductGoodClass###\n";
            strHTML += "<div class=\"SAgent_ChoiceGood\" style=\"margin-bottom:10px;\"></div>\n";
            strHTML += "<div id=\"SAgent_ProductNewGoodClass\" style=\"display:inline-block;\"></div>\n";
            //strHTML += "                ###SAgent_ProductNewGood###\n";
            strHTML += "    " + strFooter + " \n";
            strHTML += "    ###WeiXin__o2o_FootMarker_Location_###\n";
            strHTML += "</body>\n";
            strHTML += "</html>\n";



            //string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClient);
            // STR_tab_ShopClient_ModelUpLoadPath += "/Html";
            STR_tab_ShopClient_ModelUpLoadPath += "/04Index.html";
            //   strFooter = strFooter.Replace("###Pub_ShowAgent_SubMix_Text###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text());

            Eggsoft.Common.FileFolder.WriteFile(STR_tab_ShopClient_ModelUpLoadPath, strHTML);
        }
    }
}