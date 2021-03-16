using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.AddFunction._01Shake_Parter
{
    public partial class _01Shake_Parter : System.Web.UI.Page
    {
        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected string pub_GetAgentShopName_From_Visit__ = "";
        private int pInt_QueryString_ParentID = 0;//；
        //protected string pub_ServicesURL_HelpMachine = "";


        protected string Pub_Get_MyDisk_HeadImage = "";
        protected string Pub_Get_MyWeiXin_HeadImage = "";
        protected string pub_varUserIDNickName = "";

        protected string pub_varUserHostAddress = "";
   
        protected string pub_NeedAlertShallSubscribe = "0";////1提示 应该关注
        protected string pub_strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = "";////1提示 应该关注
        protected string pub_NeedAlertShallAddress = "0";///1提示 应有收获地址

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    setInfo();
                    setLiteral_WeiXinShare();///微信转发首页
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


        private void setLiteral_WeiXinShare()///微信转发首页
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
                string pub_strDESFull = this.Title;

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/addfunction/01shake_parter/01shake_parter.aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", GetNickName + "现场活动，摇大奖");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "摇一摇");
            }
            finally
            {

            }

            strShareLink = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strShareLink, "ShareShopFunction");//

            Literal_WeiXinShare.Text = strShareLink;
        }


        private void setInfo()
        {

            pub_varUserHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
        
            this.Title = pub_GetAgentShopName_From_Visit__ + "_摇一摇";////设置 店铺名字
            //pub_ServicesURL_HelpMachine = Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL_HelpMachine();///辅助 机器的路径
            ///

            pub_varUserIDNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
            Pub_Get_MyDisk_HeadImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(pub_Int_Session_CurUserID);
            Pub_Get_MyWeiXin_HeadImage = Eggsoft_Public_CL.Pub.Get_HeadImage(pub_Int_Session_CurUserID.ToString());
            Imagewx_qlogo_cn_mmopen.ImageUrl = Pub_Get_MyWeiXin_HeadImage;

            #region 检测 收获地址 是否关注等 由前段 JS  进行 跳转
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);

            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
            EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ActivityState=1 and ShopClientID=" + pub_Int_ShopClientID);

            if (Model_tab_ShopClient_XianChangHuoDong != null)
            {
                if ((Convert.ToBoolean(Model_tab_ShopClient_XianChangHuoDong.Subscribe_Must)) && (Convert.ToBoolean(Model_tab_User.Subscribe) == false))
                {
                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + pub_Int_ShopClientID);
                    pub_strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;

                    pub_NeedAlertShallSubscribe = "1";
                }

                if ((Convert.ToBoolean(Model_tab_ShopClient_XianChangHuoDong.Address_Must)) && (Convert.ToInt32(Model_tab_User.Default_Address) == 0))
                {
                    pub_NeedAlertShallAddress = "1";
                }
            }


            #endregion
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
    }
}