using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class WeiXinErweima_Tuiguang : System.Web.UI.Page
    {
        protected string pub_WeiXin__o2o_FootMarker_Location___ = "";

        protected string _Pub_strGoodBodyest = "";
        protected string _Pub_strGoodBody_Title = "";
        protected string _Pub_dBody_Title = "";
        protected string _Pub_strstrFooter = "";
        protected string pub_GetAgentShopName_From_Visit__ = "";
        private int pInt_QueryString_ParentID = 0;//；

        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";

        protected string _Pub_02Topbar_html = "";
        protected string _Pub_03Footer_html = "";

        protected string _Pub_ProductGoodClass_ = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    Image_MarkerErWeiMaThisBook();
                    InitWeiXinShareLink();
                    DoHTML();
                    //_Pub_ProductGoodClass_ = Eggsoft_Public_CL.Pub_Agent.strGet_SAgent_ProductGoodClass(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);
                    _Pub_strstrFooter = Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);
                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);


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
                string pub_strDESFull = "联系我们";

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/weixinerweima_tuiguang.aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", "微商客服,微商客服二维码");
                pub_WeiXin__o2o_FootMarker_Location___ = Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "微商客服,微商客服二维码");

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            strShareLink = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strShareLink, "ShareShopFunction");//

            Literal_WeiXinShare.Text = strShareLink;

        }


        private void Image_MarkerErWeiMaThisBook()
        {
            ///检查 当前人是否是代理 是的话 取路径 1  取代理路径 2 检查文件是否存在 3不存在再做一次 还是没有只好作罢
            //不是的话 去其父的 

            string STR_tab_ShopClient_ModelUpLoadPath = System.Configuration.ConfigurationManager.AppSettings["UpLoadResourceURL"] + Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
            STR_tab_ShopClient_ModelUpLoadPath += "/SystemDevelopment/weixinerweima_tuiguang.jpg";


            Image_MarkerErWeiMa.Visible = false;



            if (Eggsoft.Common.FileFolder.RemoteFileExists(STR_tab_ShopClient_ModelUpLoadPath))
            {
                Image_MarkerErWeiMa.ImageUrl = STR_tab_ShopClient_ModelUpLoadPath;
                Image_MarkerErWeiMa.Visible = true;

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

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";



            string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/03Footer.html";
            string strFooter = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_03Footer_html);
            _Pub_03Footer_html = strFooter.Replace("###SAgentPath###", Pub_Agent_Path);
            _Pub_03Footer_html = _Pub_03Footer_html.Replace("background-image: url(/Templet/02ShiYi/skin/images/h02.jpg);\">申请代理</li>", "background-image: url(/Templet/02ShiYi/skin/images/h02.jpg);\">" + Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID) + "</li>");

        }
    }
}