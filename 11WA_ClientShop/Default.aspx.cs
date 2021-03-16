using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class _Default : Page
    {



        protected string _Pub_01Banner_html = "1";
        protected string _Pub_02Topbar_html = "2";
        protected string _Pub_03Footer_html = "3";


        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        /// <summary>
        /// 该变量自己页面会使用
        /// </summary>
        protected int pInt_QueryString_ParentID = 0;//；
        /// <summary>
        /// 该变量展示自己的上级
        /// </summary>
        protected int pInt_DB_ParentID = 0;//；数据库上级 真正的上级

        protected string Pub_Agent_Path = "";
        protected int Pub_IntStyle_Model = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    Do_Index_HTML();
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "clientDefault", "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "clientDefault");
                }
                finally
                {

                }
            }
        }

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
            pInt_DB_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(pub_Int_Session_CurUserID);
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
            Pub_IntStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);
            //#region 初始化所有运营中心数据  粉丝数据
            //System.Threading.Tasks.Task.Factory.StartNew(() =>
            //{
            //    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strShopClientID.toInt32()))
            //    {
            //        Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            //    }
            //});
            //#endregion 初始化所有运营中心数据        
        }

        /// <summary>
        /// 可设置加载自定义模版
        /// </summary>
        protected void Do_Index_HTML()
        {

            if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "StyleModeDoSelfCheck", true))
            {
                
                string Server_Str = "";
                Server_Str += "<div id=\"ShopMakeOwner\" style=\"display:block;margin-top:10px;margin-bottom:10px;\" > ";
                Server_Str += Server.HtmlDecode(Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "StyleModeDoSelfHtml"));
                Server_Str += "</div>";

                Eggsoft.Common.debug_Log.Call_WriteLog(Server_Str, "商铺号加载自定义模板页" + pub_Int_ShopClientID.ToString(), "疑似程序报错");
                //str04Index = str04Index.Replace("###ShopMakeOwnerEdit###", Server_Str);
                Response.Write(Server.HtmlDecode(Server_Str));
            }
            else
            {
                string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();
                string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
                STR_tab_ShopClient_ModelUpLoadPath += "/Html";

                string STR_04Index = STR_tab_ShopClient_ModelUpLoadPath + "/04Index.html";
                string str04Index = Eggsoft.Common.FileFolder.ReadRemoteTempleToCacheKey_ShopClientID(strGetAppConfiugUplaod + STR_04Index, pub_Int_ShopClientID);
                str04Index = str04Index.Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL());
                str04Index = str04Index.Replace("###ParentID###", pInt_QueryString_ParentID.ToString()).Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ToShopCilentID###", pub_Int_ShopClientID.ToString());
                str04Index = str04Index.Replace("###DBParentID###", pInt_DB_ParentID.ToString());
                str04Index = str04Index.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


                str04Index = str04Index.Replace("###_offlineshopShopName_###", ""); ;


                str04Index = str04Index.Replace("###SAgentPath###", Pub_Agent_Path);
                str04Index = str04Index.Replace("###SAgent__Pub_dBody_Title###", Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID));
                str04Index = str04Index.Replace("###WeiXin__Share###", setLiteral_WeiXinShare());
                str04Index = str04Index.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "微店首页"));
                str04Index = str04Index.Replace("###demo_MoblieRoll###", replaceRoolShow());


                if (Pub_IntStyle_Model == 1)
                {
                    str04Index = str04Index.Replace("###Pub_ShowAgent_SubMix_Text###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID));

                    Response.Write(str04Index);
                }
                else if (Pub_IntStyle_Model == 0)
                {
                    str04Index = str04Index.Replace("background-image: url(/Templet/02ShiYi/skin/images/h02.jpg);\">申请代理</li>", "background-image: url(/Templet/02ShiYi/skin/images/h02.jpg);\">" + Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID) + "</li>");
                    Response.Write(str04Index);
                }
                else if (Pub_IntStyle_Model == 2)
                {
                    Response.Write(str04Index);
                }
                else if (Pub_IntStyle_Model == 3)
                {
                    str04Index = str04Index.Replace("###ShopNavIconList###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID));

                    Response.Write(str04Index);
                }
            }



        }


        protected String setLiteral_WeiXinShare()///微信转发首页
        {

            string strFirstImageFullName = "";
            string strURL = "";
            string strDESFullName = "";
            string strTitle = "";


            #region  strFirstImageFullName
            /*显示头像步骤 代理的头像 店铺的logo  店铺的轮播图片  店铺的商品 都没与 就默认*/
            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID + "   and IsDeleted=0 and (isnull(Empowered, 0) = 1 or OnlyIsAngel=1)");///有代理啊
            if (boolAgent)
            {
                strFirstImageFullName = Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(pub_Int_Session_CurUserID);

                string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
                strURL = strgetwebHttp + "/sagent-" + pub_Int_Session_CurUserID + "/default.aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog(pub_Int_Session_CurUserID + "是代理 strURL=" + strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                strTitle = pub_GetAgentShopName_From_Visit__;
                if (strTitle.IndexOf(Model_tab_ShopClient.ShopClientName) < 0) strTitle = strTitle + " " + Model_tab_ShopClient.ShopClientName;//不知道是否包含啊

                strDESFullName += strTitle + ",微店推荐 ";
                EggsoftWX.BLL.View_Goods_Class_Agent bll_View_Goods_Class_Agent = new EggsoftWX.BLL.View_Goods_Class_Agent();
                DataSet myds = bll_View_Goods_Class_Agent.GetList("ClassName", " UserID=" + pub_Int_Session_CurUserID + "  order by sort asc,updatetime desc");
                for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                {
                    string GoodName = myds.Tables[0].Rows[i]["ClassName"].ToString();
                    strDESFullName += GoodName;
                }

            }
            else
            {
                ///是不是别家的商品
                ///
                int intParentAgentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);//是不是访问别人网页；
                if (intParentAgentID > 0) //是访问别人代理店铺；
                {
                    strFirstImageFullName = Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(intParentAgentID);
                    string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
                    strURL = strgetwebHttp + "/sagent-" + intParentAgentID + "/default.aspx";
                    Eggsoft.Common.debug_Log.Call_WriteLog(pub_Int_Session_CurUserID + "是访问 代理" + intParentAgentID + " strURL=" + strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                    EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                    strTitle = pub_GetAgentShopName_From_Visit__;
                    if (strTitle.IndexOf(Model_tab_ShopClient.ShopClientName) < 0) strTitle = strTitle + " " + Model_tab_ShopClient.ShopClientName;//不知道是否包含啊

                    strDESFullName += strTitle + ",微店推荐 ";
                    EggsoftWX.BLL.View_Goods_Class_Agent bll_View_Goods_Class_Agent = new EggsoftWX.BLL.View_Goods_Class_Agent();
                    DataSet myds = bll_View_Goods_Class_Agent.GetList("ClassName", " UserID=" + intParentAgentID + "  order by sort asc,updatetime desc");
                    for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                    {
                        string GoodName = myds.Tables[0].Rows[i]["ClassName"].ToString();
                        strDESFullName += GoodName;
                    }
                }
                else
                {

                    EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                    Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);
                    string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                    string strShopButtonImage = Model_tab_ShopClient.ShopButton;

                    if (String.IsNullOrEmpty(strShopLogoImage) == false)
                    {
                        strFirstImageFullName = strShopLogoImage;
                    }
                    else if (String.IsNullOrEmpty(strShopButtonImage) == false)
                    {
                        strFirstImageFullName = strShopButtonImage;
                    }
                    else
                    {
                        strFirstImageFullName = "/UpLoad/images/lb20141228103044118533376.jpg";//默认微云基石的
                    }

                    string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
                    strURL = strgetwebHttp + "/default.aspx";
                    Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 strURL=" + strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                    EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(pub_Int_Session_CurUserID);


                    strTitle = Model_tab_User.NickName + " " + pub_GetAgentShopName_From_Visit__;


                    strDESFullName += strTitle + ",微店推荐 ";

                }
            }
            strFirstImageFullName = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strFirstImageFullName;
            #endregion


            string cssFrageMent = "";

            cssFrageMent += " <script type=\"text/javascript\">\n";
            cssFrageMent += "var WeiXin_imgAllPageUrl = '" + strFirstImageFullName + "';\n";
            cssFrageMent += "var WeiXin_lineAllPageLink = '" + strURL + "';\n";
            cssFrageMent += "var WeiXin_descAppPageContent = '" + Eggsoft.Common.CommUtil.getShortText(strDESFullName, 80) + "';\n";
            cssFrageMent += "var WeiXin_shareAppAllPageTitle = '" + strTitle + "';\n";
            cssFrageMent += "var WeiXin_appidAllPage = '';\n";
            cssFrageMent += "var path=WeiXin_lineAllPageLink;\n";
            cssFrageMent += "</script>\n";
            cssFrageMent += "###_PulicChageWeiXin###\n";
            cssFrageMent = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(cssFrageMent, "ShareShopFunction");


            return cssFrageMent;
            //Literal_WeiXinShare.Text = cssFrageMent;
        }



        /// <summary>
        /// 替换公告栏
        /// </summary>
        /// <returns></returns>
        private String replaceRoolShow()
        {
            string strShowText = "";

            EggsoftWX.BLL.b016_NoticeGuidePages BLL_b016_NoticeGuidePages = new EggsoftWX.BLL.b016_NoticeGuidePages();
            System.Data.DataTable Data_DataTable = BLL_b016_NoticeGuidePages.SelectList("select Title,Linkurl from b016_NoticeGuidePages where Active=1 and IsDeleted=0 and ShopClientID=@ShopClientID order by pos asc,id desc", pub_Int_ShopClientID).Tables[0];
            if (Data_DataTable.Rows.Count > 0)
            {
                string strReplaceText = "";

                strReplaceText += "  <style type=\"text/css\">\n";
                strReplaceText += " .qimo8MoblieRoll {\n";
                strReplaceText += "     overflow: hidden;\n";
                strReplaceText += "     width: 98%;\n";
                strReplaceText += "     margin: 0px auto;\n";
                strReplaceText += " }\n";
                strReplaceText += " \n";
                strReplaceText += "     .qimo8MoblieRoll .qimo_MoblieRoll { /*width:99999999px;*/\n";
                strReplaceText += "        width: 8000%;\n";
                strReplaceText += "        height: 25px;\n";
                strReplaceText += "    }\n";
                strReplaceText += " \n";
                strReplaceText += "        .qimo8MoblieRoll .qimo_MoblieRoll div {\n";
                strReplaceText += "            float: left;\n";
                strReplaceText += "        }\n";
                strReplaceText += " \n";
                strReplaceText += "        .qimo8MoblieRoll .qimo_MoblieRoll ul {\n";
                strReplaceText += "          float: left;\n";
                strReplaceText += "          height: 25px;\n";
                strReplaceText += "           overflow: hidden;\n";
                strReplaceText += "          zoom: 1;\n";
                strReplaceText += "      }\n";
                strReplaceText += " \n";
                strReplaceText += "          .qimo8MoblieRoll .qimo_MoblieRoll ul li {\n";
                strReplaceText += "             float: left;\n";
                strReplaceText += "              line-height: 25px;\n";
                strReplaceText += "             list-style: none;\n";
                strReplaceText += "         }\n";
                strReplaceText += " \n";
                strReplaceText += "   .qimo8MoblieRoll li a {\n";
                strReplaceText += "      margin-right: 10px;\n";
                strReplaceText += "      color:blue;\n";
                strReplaceText += "      text-decoration:underline;\n";
                strReplaceText += " \n";
                strReplaceText += "     }\n";
                strReplaceText += "  </style>\n";
                strReplaceText += "  <div id=\"demo_MoblieRoll\" class=\"qimo8MoblieRoll\">\n";
                strReplaceText += "    <div class=\"qimo_MoblieRoll\">\n";
                strReplaceText += "       <div id=\"demo1_MoblieRoll\">\n";
                strReplaceText += "          <ul>\n";
                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                {
                    string strTitle = Data_DataTable.Rows[i]["Title"].toString();
                    string strLinkurl = Data_DataTable.Rows[i]["Linkurl"].toString();
                    strReplaceText += "       <li><a target=\"_blank\" href='" + strLinkurl + "'>" + strTitle + "</a></li>\n";
                }
                strReplaceText += "     </ul>\n";
                strReplaceText += "   </div>\n";
                strReplaceText += "   <div id=\"demo2_MoblieRoll\"></div>\n";
                strReplaceText += "  </div>\n";
                strReplaceText += " </div>\n";
                strReplaceText += " <script type=\"text/javascript\">\n";
                strReplaceText += "   var demo_MoblieRoll = document.getElementById(\"demo_MoblieRoll\");\n";
                strReplaceText += "   var demo1_MoblieRoll = document.getElementById(\"demo1_MoblieRoll\");\n";
                strReplaceText += "   var demo2_MoblieRoll = document.getElementById(\"demo2_MoblieRoll\");\n";
                strReplaceText += "   demo2_MoblieRoll.innerHTML = document.getElementById(\"demo1_MoblieRoll\").innerHTML;\n";
                strReplaceText += "   function Marquee() {\n";
                strReplaceText += "       if (demo_MoblieRoll.scrollLeft - demo2_MoblieRoll.offsetWidth >= 0) {\n";
                strReplaceText += "           demo_MoblieRoll.scrollLeft -= demo1_MoblieRoll.offsetWidth;\n";
                strReplaceText += "      }\n";
                strReplaceText += "      else {\n";
                strReplaceText += "          demo_MoblieRoll.scrollLeft++;\n";
                strReplaceText += "     }\n";
                strReplaceText += "  }\n";
                strReplaceText += "  var myvar = setInterval(Marquee, 60);\n";
                strReplaceText += "  demo_MoblieRoll.onmouseout = function () { myvar = setInterval(Marquee, 30); }\n";
                strReplaceText += "  demo_MoblieRoll.onmouseover = function () { clearInterval(myvar); }\n";
                strReplaceText += " </script>\n";
                return strReplaceText;
            }
            else
            {
                return "";
            }





        }
    }
}