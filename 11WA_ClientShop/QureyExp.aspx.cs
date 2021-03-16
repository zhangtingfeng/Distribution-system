using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class QureyExp : System.Web.UI.Page
    {

        //public String pub_strMenuName = "", pub_strMenuContent = "";
        //public String pub_strMenuListContent = "";
        public String pub_strOrderID = "";
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected string pub_WeiXin__o2o_FootMarker_Location___ = "";

        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected int pInt_QueryString_ParentID = 0;

        //protected string pub_str_FirstImageFull = "";
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
                pub_strOrderID = Request.QueryString["QueryOrderID"];
                pub_WeiXin__o2o_FootMarker_Location___ = Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "物流查询");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
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
                pub_strDESFull = "物流查询";

                String pub_str_FirstImageFull = "http://qiniu.eggsoft.cn/UpLoad/000001_sh/images/20150222155447765625410.jpg";///微云基石基石的  公众号二维码


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/qureyexp-" + pub_strOrderID + ".aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", GetNickName + "物流查询,微云基石技术支持,上海时仪电子有限公司");

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