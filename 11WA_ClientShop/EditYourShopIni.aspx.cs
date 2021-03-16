using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class EditYourShopIni : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected string pub_WeiXin__o2o_FootMarker_Location___ = "";

        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected string _Pub_03Footer_html = "";

        protected string _pub_GetAgentpolText = "";
        protected string _pub_GetAgentAdpolText = "";

        protected string pub_StrUserIDSelectProductList = "";//请选择分销商品 已选数组


        protected string[] pub_stringEditShopList;//店铺名称等等

        protected string pub_stringChoiceProductClsssList = "";//挑选分类商品  待选  待触摸

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    setAllNeedID();
                    InitWeiXinShareLink();

                    pub_WeiXin__o2o_FootMarker_Location___ = Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "代理分销选择");

                    #region  ///大道质监  判断跳转方向      1 是否申请批准过代理 如有 直接跳 。
                    //没有的话 看看 是不是父代理是最低的  是的话 跳到分销商中去
                    //不是的话 判断 是否启用了  代理模式 
                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID + " and Empowered=1" + " and ShopClientID=" + pub_Int_ShopClientID + "   and IsDeleted=0 ");
                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        int intAgentLevelSelect = Model_tab_ShopClient_Agent_.AgentLevelSelect.toInt32();
                        if (intAgentLevelSelect > 0)
                        {
                            HttpContext.Current.Response.Redirect("edityourshop_ad.aspx");///直接跳回c

                            //Eggsoft.Common.JsUtil.TipAndRedirect("","EditYourShop_Ad.aspx","0");
                            Response.End();
                            //return;
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("edityourshop.aspx");///直接跳回c
                            //Eggsoft.Common.JsUtil.LocationNewHref("EditYourShop.aspx");
                            Response.End();//;
                        }
                    }
                    else
                    {
                        string strparentagentadid = Request["parentagentadid"];
                        int intparentagentadid = 0;
                        int.TryParse(strparentagentadid, out intparentagentadid);
                        if (intparentagentadid > 0)
                        {
                            #region  intparentagentadid > 0
                            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_PID_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intparentagentadid + " and Empowered=1");
                            if (Model_tab_ShopClient_PID_Agent_ != null)///肯定 存在 折磨些  只为纠错
                            {
                                if (Model_tab_ShopClient_PID_Agent_.AgentLevelSelect > 0)
                                {
                                    EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                                    DataSet myds = null;
                                    myds = BLL_tab_ShopClient_Agent_Level.GetList("ID,AgentLevelName", " ShopClientID=" + pub_Int_ShopClientID + " order by Sort asc,id asc");

                                    int intPos = 0;
                                    int[] myLevelIDList = new int[myds.Tables[0].Rows.Count];
                                    for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                                    {
                                        string strID = myds.Tables[0].Rows[i]["ID"].ToString();

                                        if (intparentagentadid == Int32.Parse(strID)) intPos = i;
                                        myLevelIDList[i] = Int32.Parse(strID);
                                    }
                                    if (intPos == myds.Tables[0].Rows.Count - 1)
                                    {
                                        ///已经是最低级了  下面不能油料
                                        ///
                                        HttpContext.Current.Response.Redirect("edityourshop.aspx");///直接跳回c

                                        //Eggsoft.Common.JsUtil.LocationNewHref("EditYourShop.aspx");
                                        return;
                                        // Eggsoft.Common.JsUtil.ShowMsg("只能申请分销商", "EditYourShop.aspx");
                                    }
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            //不是的话 判断 是否启用了  代理模式 
                            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                            bool boolAgent_Level = BLL_tab_ShopClient_Agent_Level.ExistsCount("ShopClientID=" + pub_Int_ShopClientID + " and IsDeleted=0") > 1;
                            if (boolAgent_Level == false)
                            {
                                ///直接跳到分销商模式中去
                                ///  
                                HttpContext.Current.Response.Redirect("edityourshop.aspx");///直接跳回c

                            }

                        }
                    }
                    #endregion
                    _Pub_03Footer_html = Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);

                    init_GetAgentpolText();
                    init_GetAgentAdpolText();

                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }
            }
        }

        private void InitWeiXinShareLink()
        {

            string strShareLink = "";
            strShareLink += " <script type=\"text/javascript\">\n";
            strShareLink += "        var WeiXin_imgAllPageUrl = '###_PulicChageWeiXin_Small_Goods_Picture###';\n";
            strShareLink += "        var WeiXin_lineAllPageLink = '###_PulicChageWeiXin_MySonWillOpenLind_Goods###';\n";
            strShareLink += "        var WeiXin_descAppPageContent = '###_PulicChageWeiXin_Des_Goods###';\n";
            strShareLink += "        var WeiXin_shareAppAllPageTitle = '###_PulicChageWeiXin_Title_Goods###';\n";
            strShareLink += "        var WeiXin_appidAllPage = '';\n";
            strShareLink += "        var path=WeiXin_lineAllPageLink;\n";
            strShareLink += "    </script>\n";
            strShareLink += "    ###_PulicChageWeiXin###\n";


            //string cssFrageMent = "";
            try
            {
                string pub_strDESFull = "选择做代理商，选择做代理商,微信代理朋友圈代销,线下批发,代理联营等多种销售模式";

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/edityourshopini.aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", GetNickName + "浏览选择做代理商，选择做代理分销商");

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            strShareLink = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strShareLink, "ShareShopFunction");//

            Literal_ShareFriend.Text = strShareLink;

        }



        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
            pub_Int_CurParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);

        }


        private void init_GetAgentAdpolText()
        {
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
            string Server_Str = Model_tab_ShopClient.AgentMustReadAd;
            if (String.IsNullOrEmpty(Server_Str))
            {

                Server_Str += Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgentAd();
            }
            else
            {
                /*正则表达式替换求助
    如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
    例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                Server_Str = Server.HtmlDecode(Server_Str);

                Server_Str = Regex.Replace(Server_Str, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                string strSearch = "src=\"/upload/";
                Server_Str = Server_Str.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/upload/");
                strSearch = "src=\"/Upload/";
                Server_Str = Server_Str.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/upload/");


            }
            _pub_GetAgentAdpolText = Server_Str;


        }



        private void init_GetAgentpolText()
        {
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
            string strAgentMustRead = Model_tab_ShopClient.AgentMustRead;
            if (String.IsNullOrEmpty(strAgentMustRead))
            {
                strAgentMustRead = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent();
            }
            else
            {
                /*正则表达式替换求助
    如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
    例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                strAgentMustRead = Server.HtmlDecode(strAgentMustRead);

                strAgentMustRead = Regex.Replace(strAgentMustRead, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                string strSearch = "src=\"/upload/";
                strAgentMustRead = strAgentMustRead.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/upload/");
                strSearch = "src=\"/Upload/";
                strAgentMustRead = strAgentMustRead.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/upload/");


            }
            _pub_GetAgentpolText = strAgentMustRead;


        }
    }
}