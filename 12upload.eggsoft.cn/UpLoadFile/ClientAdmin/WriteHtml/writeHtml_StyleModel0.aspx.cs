using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WriteHtml
{
    public partial class writeHtml_StyleModel0 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private static object objLock = new object();
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
        protected void Page_Load(object sender, EventArgs e)
        {
            //Eggsoft.Common.debug_Log.Call_WriteLog("Index888");

            //Eggsoft.Common.Session.Add("WriteHtml_write", "0");
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
                    {
                        makebanner(intShopClient);
                    }
                }
                else if (intStartInt == 2)
                {
                    lock (objLock)
                    {
                        maketopbar(intShopClient);
                    }
                }
                else if (intStartInt == 3)
                {
                    lock (objLock)
                    { Class_Pub.makeFooter(intShopClient); }
                    //makeFooter(intShopClient);
                }
                else if (intStartInt == 4)
                {
                    makeIndex(intShopClient);
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
            strHTML += "<section class=\"box\" id=\"banner\">\n";
            strHTML += "<link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/01WYJS/Css/scroll.css\">\n";
            strHTML += "<script language=\"javascript\" src=\"/Templet/01WYJS/js/scroll.js\" type=\"text/javascript\"></script>\n";
            strHTML += "<div class=\"pfhlkd_frame1\">\n";
            strHTML += "     <div class=\"pfhlkd_mode0  pfhlkd_mf10001000\"></div>\n";
            strHTML += "     <div class=\"pfhlkd_mode0  pfhlkd_mf10001005\"></div>\n";
            strHTML += "      <div class=\"pfhlkd_mode0  pfhlkd_mf10001001\">\n";
            strHTML += "       <div class=\"bd pfhlkd_bd_10\">\n";
            strHTML += "           <div class=\"shop_slider shop_slider_auto mod_pic_mt_1\" data-tag=\"module-slider\" data-height=\"320\">\n";
            strHTML += "              <div class=\"inner\" data-tag=\"slider-warp\" style=\"visibility: visible;\">\n";
            strHTML += "                   ####<%=strLabel1%>####\n";
            strHTML += "                 </div>\n";
            strHTML += "             <div class=\"bar_wrap\">\n";
            strHTML += "               <ul class=\"bar\" data-tag=\"slider-page\">\n";
            strHTML += "                     ####<%=strLabel2%>####\n";
            strHTML += "                   </ul>\n";
            strHTML += "                </div>\n";
            strHTML += "             </div>\n";
            strHTML += "        </div>\n";
            strHTML += "     </div>\n";
            strHTML += "  </div>\n";
            strHTML += "</section>\n";


            string strAnnouncePic_Height = "";
            int intAnnouncePic_Height = Eggsoft_Public_CL.Pub.stringShowPower(intShopClient.ToString(), "AnnouncePic_Height").toInt32();
            if (intAnnouncePic_Height > 0) strAnnouncePic_Height = "height =\"" + intAnnouncePic_Height.toString() + "px;\"";
            string strLabel1 = "";
            string strLabel2 = "";
            EggsoftWX.BLL.tab_AnnouncePic BLL_tab_AnnouncePic = new EggsoftWX.BLL.tab_AnnouncePic();
            System.Data.DataTable myDataTable = BLL_tab_AnnouncePic.GetList("UserID=" + intShopClient + " order by pos asc,id asc").Tables[0];

            strLabel1 += "<ul class=\"pic_list\" data-tag=\"slider-list\" style=\"width: " + myDataTable.Rows.Count * 640 + "px;\">\n";


            for (int inti = 0; inti < myDataTable.Rows.Count; inti++)
            {

                String strPicUrl = myDataTable.Rows[inti]["PicUrl"].ToString();
                String strShowText = myDataTable.Rows[inti]["ShowText"].ToString();
                String strLinkURL = myDataTable.Rows[inti]["LinkURL"].ToString();

                strLabel1 += "<li data-index=\"" + inti + "\" style=\"width: 640px;left: -" + inti * 640 + "px; transition: all 0ms ease 0s; transform: translateX(640px);\">\n";
                strLabel1 += "                  <a href=\"" + strLinkURL + "\" target=\"_blank\" title=\"" + strShowText + "\">\n";
                strLabel1 += "                      <img alt=\"" + strShowText + "\" " + strAnnouncePic_Height + " class=\"pp_init_img\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPicUrl + "\"></a></li>\n";


                if (inti == myDataTable.Rows.Count - 1)
                {
                    strLabel2 += " <li class=\"cur\"></li>\n";
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



        protected void maketopbar(int intShopClient)
        {
            string strHTML = "";


            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClient);
            string strContactPhone_ = Model_tab_ShopClient.ContactPhone;


            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClient);
            string strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;



            strHTML += "\n";
            strHTML += "      <!--topbar begin-->\n";
            strHTML += "  <section class=\"box\" id=\"myorder\" style=\"display: block;\">\n";
            strHTML += "       <div class=\"user_index\">\n";
            strHTML += "          <div class=\"user_header\" id=\"actionBar\">\n";
            strHTML += "              ####<%=pub_Str_HeadPage%>####\n";
            strHTML += "\n";
            strHTML += "              \n";
            strHTML += "            <span>\n";
            strHTML += "                <a href=\"/cart.aspx\" class=\"shopping-cart\">\n";
            strHTML += "                   <i class=\"fa fa-shopping-cart\"></i>\n";
            strHTML += "               </a>\n";
            strHTML += "           </span>\n";
            strHTML += "            <span><a href=\"###SAgentPath###/historygoodslist.aspx\">我的关注</a></span>\n";
            strHTML += "        </div>\n";
            strHTML += "     </div>\n";
            strHTML += " </section>\n";


            string strhead = "";





            EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
            string strCondition = " ShopClientID=" + intShopClient + " and parentID=0 order by MenuPos asc,id asc";///只有oliver 才能看到所有记录
            DataSet myds_AnnouncePic = bll.GetList(strCondition);

            int intRead = myds_AnnouncePic.Tables[0].Rows.Count > 3 ? 3 : myds_AnnouncePic.Tables[0].Rows.Count;
            for (int i = 0; i < intRead; i++)
            {
                string MenuName = myds_AnnouncePic.Tables[0].Rows[i]["MenuName"].ToString();
                string LinkOrText = myds_AnnouncePic.Tables[0].Rows[i]["LinkOrText"].ToString();
                string ID = myds_AnnouncePic.Tables[0].Rows[i]["ID"].ToString();
                string MenuLink = myds_AnnouncePic.Tables[0].Rows[i]["MenuLink"].ToString();

                bool boolfalse = false;
                bool.TryParse(LinkOrText, out boolfalse);

                if (boolfalse)
                {
                    strhead += "<span><a href=\"" + MenuLink + "\">" + MenuName + "</a></span>";
                }
                else
                {
                    strhead += "<span><a href=\"###SAgentPath###/guidepage-" + ID + ".aspx\">" + MenuName + "</a></span>";
                }
            }



            strHTML = strHTML.Replace("####<%=pub_Str_HeadPage%>####", strhead);

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClient);
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";
            STR_tab_ShopClient_ModelUpLoadPath += "/02Topbar.html";

            Eggsoft.Common.FileFolder.WriteFile(STR_tab_ShopClient_ModelUpLoadPath, strHTML);

        }


        protected void makeIndex(int intShopClient)
        {

            string strpShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Int32.Parse(strpShopClientID));
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";

            string STR_02Topbar_html = STR_tab_ShopClient_ModelUpLoadPath + "/02Topbar.html";
            string strTopbar = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_02Topbar_html);


            string STR_01Banner_html = STR_tab_ShopClient_ModelUpLoadPath + "/01Banner.html";
            string strBanner = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_01Banner_html);

            string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/03Footer.html";
            string strFooter = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_03Footer_html);



            string strHTML = "";

            strHTML += " <!DOCTYPE html>\n";
            strHTML += "<html>\n";
            strHTML += "<head>\n";
            strHTML += "    <meta charset=\"utf-8\">\n";
            strHTML += "    <title>###SAgent__Pub_dBody_Title###</title>\n";
            strHTML += "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=none\">\n";
            strHTML += "    <meta name=\"apple-mobile-web-app-capable\" content=\"yes\">\n";
            strHTML += "    <meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\">\n";
            strHTML += "    <meta name=\"format-detection\" content=\"telephone=no\">\n";
            strHTML += "    <meta http-equiv=\"x-dns-prefetch-control\" content=\"on\">\n";
            strHTML += "    <link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/01WYJS/Css/common.css?version=css201709121928\">\n";
            strHTML += "    <link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/01WYJS/Css/font-awesome.css?version=css201709121928\">\n";
            strHTML += "    <link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/01WYJS/Css/mall.css?version=css201709121928\">\n";
            strHTML += "    <link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/01WYJS/Css/PreFoot.css?version=css201709121928\">\n";
            strHTML += "    <script src=\"/Templet/01WYJS/js/jquery-1.js?version=js201709121928\" type=\"text/javascript\"></script>\n";
            strHTML += "    <script type=\"text/javascript\" src=\"/Templet/01WYJS/js/jscommon.js?version=js201709121928\" charset=\"gb2312\"></script>\n";
            strHTML += "    <script type=\"text/javascript\" src=\"/Templet/01WYJS/js/func_Image.js?version=js201709121928\"></script>\n";
            strHTML += "    <link href=\"/Templet/02ShiYi/skin/foot.css?version=css201709121928\" rel=\"stylesheet\" type=\"text/css\">\n";
            strHTML += "    <link rel=\"stylesheet\" type=\"text/css\" href=\"/Templet/01WYJS/Css/SearchBar-mobile-template.css?version=css201709121928\">\n";
            strHTML += "    <script src=\"/Templet/02ShiYi/js/Footer_common.js?version=js201709121928\"></script>\n";
            strHTML += "    ###WeiXin__o2o_FootMarker_Location_###\n";
            strHTML += "    <script src=\"/Templet/01WYJS/js/Base.js?version=js201709121928\"></script>\n";
            strHTML += " <script type=\"text/javascript\">\n";
            strHTML += " var varUserID = \"###UserID###\";///传递给 product.js   传递变量\n";
            strHTML += " var varServiceURL = \"###ServiceURL###\";///传递给 product.js   传递变量\n";
            strHTML += " var varShopClientID = \"###ShopClientID###\";///传递给 product.js   传递变量\n";


            strHTML += " function load_Multi() {\n";
            strHTML += "     doVisitThisPage();";
            strHTML += "     doDefault_Banner();";
            strHTML += "     doDefault_SAgent_ProductGoodClass();";
            strHTML += "     doDefault_SAgent_ProductNewGood();";
            strHTML += "     doInfoAlert_Msg(###UserID###);";
            strHTML += "  }\n";

            strHTML += "    function btnSearch() {\n";
            strHTML += "       var varSearchKey= document.getElementById(\"oliversearch-input_s_1447079356776\").value;\n";
            strHTML += "       window.location.href = '###SAgentPath###/productsearchgoods.aspx?searchkey=' + varSearchKey.toLowerCase();\n";
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
            strHTML += "        data:'strpub_Int_ShopClientID='+'###ToShopCilentID###',\n";
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
            //data:'strGoodID='+'###GoodID###'+'&Int_ShopClientID=###ToShopCilentID###'+'&pub_Int_Session_CurUserID=###UserID###',

            strHTML += "   function doDefault_SAgent_ProductGoodClass() {\n";
            strHTML += "    $.ajax({\n";
            strHTML += "         type:'GET',\n";
            strHTML += "         url:'/Handler/Default_SAgent_ProductGoodClass.ashx',\n";
            strHTML += "         dataType: 'text',\n";
            strHTML += "        data:'pClassGoodType=0&strpub_Int_ShopClientID='+'###ToShopCilentID###'+'&strpub_Int_Session_CurUserID=###UserID###',\n";
            strHTML += "        beforeSend:function(){\n";
            strHTML += "            $(\"#SAgent_ProductGoodClass\").append('<img style=\"margin: 0px auto;display:block;\" src=\"/images/loading.gif\"/>');\n";
            strHTML += "            $(\"#ShowSAgent_ProductGoodClass\").append('<img style=\"margin: 0px auto;display:block;\" src=\"/images/loading.gif\"/>');\n";
            strHTML += "        },\n";
            strHTML += "         success:function(msg)\n";
            strHTML += "        {\n";
            strHTML += "            //alert(msg);\n";
            strHTML += "             $(\"#SAgent_ProductGoodClass\").html(msg);\n";
            strHTML += "             $(\"#ShowSAgent_ProductGoodClass\").html(msg);\n";
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
            strHTML += "            $(\"#ShowSAgent_ProductNewGood\").append('<img style=\"margin: 0px auto;display:block;\" src=\"/images/loading.gif\"/>');\n";
            strHTML += "        },\n";
            strHTML += "         success:function(msg)\n";
            strHTML += "        {\n";
            strHTML += "            //alert(msg);\n";
            strHTML += "             $(\"#ShowSAgent_ProductNewGood\").html(msg);\n";
            strHTML += "         },\n";
            strHTML += "        error: function(data){\n";
            strHTML += "          }\n";
            strHTML += "     })\n";
            strHTML += "  }\n";

            strHTML += "  </script>\n";
            strHTML += "    ###WeiXin__Share###\n";
            strHTML += "</head>\n";
            strHTML += "<body onload=\"load_Multi();\">\n";
            strHTML += "    <!--mask begin-->\n";
            strHTML += "    <div class=\"mask\"></div>\n";
            strHTML += "    <!--mask end-->\n";
            strHTML += "    " + strTopbar + " \n";
            strHTML += "<div id=\"Anounce_spro_con\" class=\"spro_con\" style=\"background-color:black;\">\n";
            strHTML += "</div>\n";

            strHTML += "    ###demo_MoblieRoll###\n";


            //strHTML += "    " + strBanner + " \n";
            strHTML += "   <section class=\"box\" id=\"module\">\n";
            strHTML += "        <div>\n";
            strHTML += "            <div class=\"user_nav clearfix\">\n";
            strHTML += "                <ul class=\"user_nav_list\">\n";
            strHTML += "                    <li class=\"pro-class\">\n";
            strHTML += "                        <a href=\"javascript:void(0)\" id=\"menu\" class=\"icon-s1\"><span class=\"fa fa-th\"></span>所有商品</a>\n";
            strHTML += "                        <div class=\"WX_cat_pop WX_cat_list J_smenu_list\" id=\"menulist\" style=\"display: none;\">\n";
            strHTML += "                            <i class=\"WX_cat_btn-arrow\"></i>\n";
            strHTML += "                            <div id=\"SAgent_ProductGoodClass\">###SAgent_ProductGoodClass###</div>\n";
            strHTML += "                        </div>\n";
            strHTML += "                    </li>\n";
            strHTML += "\n";
            strHTML += "                    <li>\n";
            strHTML += "                        <a href=\"###SAgentPath###/productclass.aspx?type=newest\" class=\"icon-s1\"><span class=\"fa fa-clock-o\"></span>新品上架</a>\n";
            strHTML += "                    </li>\n";
            strHTML += "                    <li>\n";
            strHTML += "                        <a href=\"###SAgentPath###/productclass.aspx?type=salsed\" class=\"icon-s1\"><span class=\"fa fa-heart\"></span>精选商品</a>\n";
            strHTML += "                    </li>\n";
            strHTML += "                    <li>\n";
            strHTML += "                        <a href=\"###SAgentPath###/productclass.aspx?type=cheapest\" class=\"icon-s1\"><span class=\"fa fa-tags\"></span>特惠商品\n";
            strHTML += "                        </a>\n";
            strHTML += "                        &nbsp;</li>\n";
            strHTML += "\n";
            strHTML += "                </ul>\n";
            strHTML += "            </div>\n";
            strHTML += "        </div>\n";
            strHTML += "    </section>\n";
            strHTML += "    <div style=\"clear: both;\"></div>\n";



            strHTML += "    <div class=\"oliversearch-bar\" style=\"background: #ffffff;display:block;\">\n";
            strHTML += "    <div class=\"oliversearch-inner\">\n";
            strHTML += "        <i class=\"font-icon font-icon-oliversearch\"></i>\n";
            strHTML += "        <input class=\"oliversearch-input\" style=\"background-color: rgb(255, 255, 255); border-color: rgb(175, 175, 175); color: rgb(127, 127, 127); width: 80%; background-position: initial initial; background-repeat: initial initial;\"\n";
            strHTML += "            id=\"oliversearch-input_s_1447079356776\" type=\"text\"\n";
            strHTML += "            placeholder=\"搜索商品名/分类/种类/描述/全部\" onfocus=\"if((this.value == '搜索商品名/分类/种类/描述/全部' ) || this.value == '') {this.value = '';this.style.width='80%';document.getElementById('oliversearch-btn_s_1447079356706').style.display='inline-block'}\" onblur=\"if(this.value =='') {this.value = '搜索商品名/分类/种类/描述/全部';this.style.width='100%';document.getElementById('oliversearch-btn_s_1447079356706').style.display='none'}\">\n";
            strHTML += "        <button class=\"oliversearch-btn\" style=\"background-color: rgb(255, 0, 6); border: rgb(189, 0, 4); color: rgb(255, 255, 255); display:inline-block; background-position: initial initial; background-repeat: initial initial;\"\n";
            strHTML += "            onclick=\"btnSearch();\" id=\"oliversearch-btn_s_1447079356706\">\n";
            strHTML += "            搜索</button>\n";
            strHTML += "    </div>\n";
            strHTML += "    </div>\n";

            strHTML += "    <div style=\"clear: both;\"></div>\n";

            //strHTML += "<div id=\"ShopMakeOwnerEdit\" style=\"display:block;width:100%;\">";
            //strHTML += "###ShopMakeOwnerEdit###";
            //strHTML += "</div>";

            strHTML += "    <div style=\"clear: both;\"></div>\n";
            strHTML += "    <div class=\"user_itlist_nb\">\n";
            strHTML += "        <div class=\"title\">\n";
            strHTML += "            <h2>新品推荐</h2>\n";
            strHTML += "        </div>\n";
            strHTML += "    </div>\n";
            strHTML += "\n";
            strHTML += "\n";
            strHTML += "    <section class=\"main_title\" style=\"display: none\" id=\"top2\">\n";
            strHTML += "\n";
            strHTML += "        <h2 id=\"topname\"></h2>\n";
            strHTML += "        <a href=\"/\" data-type=\"back\" class=\"go-back\" id=\"backurl\"><span class=\"icons fa fa-angle-left\" data-icon=\"\"></span></a>\n";
            strHTML += "\n";
            strHTML += "    </section>\n";
            strHTML += "    <div class=\"h30\" id=\"h30\" style=\"display: none\"></div>\n";
            strHTML += "\n";
            strHTML += "    <div class=\"WX_con\" id=\"J_main\">\n";
            strHTML += "        <div class=\"jx\">\n";
            strHTML += "            <div id=\"ShowSAgent_ProductNewGood\" class=\"jx_list\">\n";
            //strHTML += "                ###SAgent_ProductNewGood###\n";
            strHTML += "            </div>\n";
            strHTML += "            <div class=\"jx_map\">\n";
            strHTML += "                <div id=\"ShowSAgent_ProductGoodClass\" class=\"jx_map_bd WX_cat_list\">\n";
            ///strHTML += "                    ###SAgent_ProductGoodClass###\n";
            strHTML += "                </div>\n";
            strHTML += "            </div>\n";
            strHTML += "        </div>\n";
            strHTML += "    </div>\n";
            strHTML += "    " + strFooter + " \n";
            strHTML += "</body>\n";
            strHTML += "</html>\n";


            //string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClient);
            // STR_tab_ShopClient_ModelUpLoadPath += "/Html";
            STR_tab_ShopClient_ModelUpLoadPath += "/04Index.html";
            //   strFooter = strFooter.Replace("###ShengQingDaiLiOrShowGuanliDaiLi###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text());

            Eggsoft.Common.FileFolder.WriteFile(STR_tab_ShopClient_ModelUpLoadPath, strHTML);
        }
    }
}