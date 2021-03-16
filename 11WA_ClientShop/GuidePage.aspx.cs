using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class GuidePage : System.Web.UI.Page
    {
        public String pub_strMenuName = "", pub_strMenuContent = "";
        public String pub_strMenuListContent = "";
        public String pub_strMenuID = "";
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected string pub_WeiXin__o2o_FootMarker_Location___ = "";

        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        /// <summary>
        /// 该变量自己页面会使用
        /// </summary>
        protected int pInt_QueryString_ParentID = 0;//；
        /// <summary>
        /// 该变量展示自己的上级
        /// </summary>
        protected int pInt_DB_ParentID = 0;//；数据库上级 真正的上级
        protected string pub_str_FirstImageFull = "";
        protected string pub_strURL = "";
        protected string pub_strDESFull = "";

        protected string _Pub_03Footer_html = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    DoHTML();
                    InitContent();

                    InitWeiXinShareLink();
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

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
            pInt_DB_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(pub_Int_Session_CurUserID);

        }
        protected void DoHTML()
        {
            string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();
            string strPub_Agent_Path = Pub_Agent_Path;

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";
            string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/03Footer.html";
            string strFooter = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_03Footer_html);
            _Pub_03Footer_html = strFooter.Replace("###SAgentPath###", strPub_Agent_Path);
            _Pub_03Footer_html = _Pub_03Footer_html.Replace("###Pub_ShowAgent_SubMix_Text###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID));
        }

        private void InitContent()
        {
            try
            {
                pub_strMenuID = Request["guidepageid"];
                int intMenuID = 0;
                int.TryParse(pub_strMenuID, out intMenuID);
                // INC_User_ID=1&MenuID=2
                EggsoftWX.BLL.tab_ShopClient_GuidePages tab_ShopClient_GuidePages_bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                EggsoftWX.Model.tab_ShopClient_GuidePages tab_ShopClient_GuidePages_Model = new EggsoftWX.Model.tab_ShopClient_GuidePages();
                tab_ShopClient_GuidePages_Model = tab_ShopClient_GuidePages_bll.GetModel(intMenuID);

                if (tab_ShopClient_GuidePages_Model != null && tab_ShopClient_GuidePages_Model.IsDeleted != true)
                {
                    pub_strMenuName = tab_ShopClient_GuidePages_Model.MenuName;
                    pub_strMenuContent = Server.HtmlDecode(tab_ShopClient_GuidePages_Model.MenuText);///供微信分析使用
                }
                pub_WeiXin__o2o_FootMarker_Location___ = Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "资讯页-" + pub_strMenuName);





                DataSet myds = tab_ShopClient_GuidePages_bll.GetList("ParentID=" + intMenuID + " and isnull(IsDeleted,0)=0 order by MenuPos asc,id desc");

                if (myds.Tables[0].Rows.Count > 0)
                {
                    pub_strMenuListContent += "<section>\n";
                    pub_strMenuListContent += "<ul class=\"list_ul_news\">\n";

                    //string BigID,BigClassName;
                    for (int i = 0; i < myds.Tables[0].Rows.Count; i++)
                    {
                        string MenuName = myds.Tables[0].Rows[i]["MenuName"].ToString();
                        string strID = myds.Tables[0].Rows[i]["ID"].ToString();
                        string MenuIcon = myds.Tables[0].Rows[i]["MenuIcon"].ToString();
                        string MenuLink = myds.Tables[0].Rows[i]["MenuLink"].ToString();
                        MenuIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + MenuIcon;
                        string LinkOrText = myds.Tables[0].Rows[i]["LinkOrText"].ToString();
                        bool boolLinkOrText = Convert.ToBoolean(LinkOrText);
                        if (!boolLinkOrText)
                        {
                            MenuLink = Pub_Agent_Path + "/guidepage-" + strID + ".aspx";
                        }
                        Eggsoft.Common.debug_Log.Call_WriteLog("5");

                        string UpdateTime = myds.Tables[0].Rows[i]["UpdateTime"].ToString();

                        DateTime dt = Convert.ToDateTime(UpdateTime);
                        UpdateTime = string.Format("{0:yyyy-MM-dd}", dt);
                        Eggsoft.Common.debug_Log.Call_WriteLog("6");

                        pub_strMenuListContent += "<li class=\"newLine\">\n";
                        pub_strMenuListContent += "			<a class=\"tbox\" href=\"" + MenuLink + "\">\n";
                        pub_strMenuListContent += "			  <div style=\"clear:both;\">\n";
                        pub_strMenuListContent += "				<div style=\"float:left;\">\n";
                        pub_strMenuListContent += "					<img src=\"" + MenuIcon + "\" style=\"width:60px!important; height:60px;\">\n";
                        pub_strMenuListContent += "				</div>\n";
                        pub_strMenuListContent += "				<div style=\"float:left;margin-left:10px;\">\n";
                        pub_strMenuListContent += "					<p>" + Eggsoft.Common.CommUtil.getShortText(MenuName, 16) + "</p>\n";
                        pub_strMenuListContent += "					<p>" + UpdateTime + "</p>\n";
                        pub_strMenuListContent += "				</div>\n";
                        pub_strMenuListContent += "			  </div>\n";
                        pub_strMenuListContent += "			</a>\n";
                        pub_strMenuListContent += "		</li>\n";

                    }

                    pub_strMenuListContent += "</ul>\n";
                    pub_strMenuListContent += "</section>\n";
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "前端咨询管理");
            }
            finally
            {

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
                pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetFirstHtmlImageUrlByShopClientID(pub_strMenuContent, pub_Int_ShopClientID);

                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/guidepage-" + pub_strMenuID + ".aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", GetNickName + "浏览" + Eggsoft.Common.CommUtil.getShortText(pub_strMenuName, 80));

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


    }
}