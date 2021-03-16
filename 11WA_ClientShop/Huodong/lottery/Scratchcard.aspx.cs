using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.Huodong.lottery
{
    public partial class Scratchcard : System.Web.UI.Page
    {
        public String strPubJPGShow = "";
        public String strPubBonus = "今天抽奖结束！";

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected int pInt_QueryString_ParentID = 0;
        protected string Pub_Agent_Path = "";
        protected int Pub_IntStyle_Model = 0;
        protected string _Pub_03Footer_html = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setAllNeedID();

                loadBonus();
                InitWeiXinShareLink();
                DoFooterHTML();
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


            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
            Pub_IntStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);

        }
        protected void DoFooterHTML()
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
                string pub_strDESFull = "刮刮乐,微信代理分销,朋友圈代理分销,线下批发,代理联营等多种销售模式";

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                //string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = "https://" + Model_tab_ShopClient.ErJiYuMing + "/huodong/resources/lottery/images/activity-scratch-card-bannerbg.png";


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/huodong/lottery/scratchcard.aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", GetNickName + pub_strDESFull);

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



        private void loadBonus()
        {
            Localize_ShowBonus.Text = "               \n";

            Localize_ShowBonus.Text += "<p>\n";
            Localize_ShowBonus.Text += "						特等奖： 。奖品数量：1\n";
            Localize_ShowBonus.Text += "					</p>\n";
            Localize_ShowBonus.Text += "					<p>\n";
            Localize_ShowBonus.Text += "						一等奖：。奖品数量：3\n";
            Localize_ShowBonus.Text += "					</p>\n";
            Localize_ShowBonus.Text += "					<p>\n";
            Localize_ShowBonus.Text += "						二等奖：  。奖品数量：5\n";
            Localize_ShowBonus.Text += "					</p>\n";
            Localize_ShowBonus.Text += "					<p>\n";
            Localize_ShowBonus.Text += "						三等奖： 。奖品数量：5\n";
            Localize_ShowBonus.Text += "					</p>\n";
            Localize_ShowBonus.Text += "					<p>\n";
            Localize_ShowBonus.Text += "						幸运奖： 。奖品数量：200\n";
            Localize_ShowBonus.Text += "					</p>\n";

            EggsoftWX.BLL.tab_ShopClient_HuoDong blltab_ShopClient_HuoDong = new EggsoftWX.BLL.tab_ShopClient_HuoDong();
            EggsoftWX.Model.tab_ShopClient_HuoDong Modeltab_ShopClient_HuoDong = blltab_ShopClient_HuoDong.GetModel("type='guaguaka' and ShopClientID=" + pub_Int_ShopClientID);

            if (Modeltab_ShopClient_HuoDong == null)
            {
                Eggsoft.Common.JsUtil.ShowMsg("找不到该项");
                Response.End();
                return;
            }

            string strXML = Modeltab_ShopClient_HuoDong.XML;


            int intJang0 = 1;
            int intJang1 = 10;
            int intJang2 = 50;
            int intJang3 = 100;
            int intJang4 = 1000;
            int intBase = 10000;
            int intSecondGetBonusTime = 1440;




            if (string.IsNullOrEmpty(strXML) == false)
            {
                Eggsoft_Public_CL.XmlHelper_Instance_Custom_XMLAdvance_ZhuanZhuanChe myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XmlHelper_Instance_Custom_XMLAdvance_ZhuanZhuanChe>(strXML, System.Text.Encoding.UTF8);
                Localize_ShowBonus.Text = "               \n";
                Localize_ShowBonus.Text += "<p>\n";
                Localize_ShowBonus.Text += "						特等奖： " + myFahuoDan.stringValue0 + "。奖品数量：" + myFahuoDan.intValue0.ToString() + "\n";
                Localize_ShowBonus.Text += "					</p>\n";
                Localize_ShowBonus.Text += "					<p>\n";
                Localize_ShowBonus.Text += "						一等奖：" + myFahuoDan.stringValue1 + "。奖品数量：" + myFahuoDan.intValue1.ToString() + "\n";
                Localize_ShowBonus.Text += "					</p>\n";
                Localize_ShowBonus.Text += "					<p>\n";
                Localize_ShowBonus.Text += "						二等奖： " + myFahuoDan.stringValue2 + " 。奖品数量：" + myFahuoDan.intValue2.ToString() + "\n";
                Localize_ShowBonus.Text += "					</p>\n";
                Localize_ShowBonus.Text += "					<p>\n";
                Localize_ShowBonus.Text += "						三等奖：" + myFahuoDan.stringValue3 + " 。奖品数量：" + myFahuoDan.intValue3.ToString() + "\n";
                Localize_ShowBonus.Text += "					</p>\n";
                Localize_ShowBonus.Text += "					<p>\n";
                Localize_ShowBonus.Text += "						幸运奖：" + myFahuoDan.stringValue4 + " 。奖品数量：" + myFahuoDan.intValue4.ToString() + "\n";
                Localize_ShowBonus.Text += "					</p>\n";


                intSecondGetBonusTime = myFahuoDan.intSecondGetBonusTime;
                intJang0 = myFahuoDan.intValue0;
                intJang1 = myFahuoDan.intValue1;
                intJang2 = myFahuoDan.intValue2;
                intJang3 = myFahuoDan.intValue3;
                intJang4 = myFahuoDan.intValue4;

                intBase = myFahuoDan.intALLCountValue;
                //txtTypeAllCount.Text = myFahuoDan.intALLCountValue.ToString();

                if (myFahuoDan.boolEnable == false)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("本活动已终止!", "javascript:history.back()");
                }
            }

            //        你可以把输入的比率都转换成n/10000
            //然后按n来抽奖
            //特等奖
            //比如1等奖1/1000 即 10/10000
            //2等奖 30/10000
            //3等奖 200/10000
            //4等奖3000/10000
            //那么10+30+200+3000=3240；
            //取随机数 r.Next（1，10001）
            //  当0<r<=10 则为1等奖 
            //当 10<r<=10+30 则中2等奖
            //当 10+30<r<=10+30+200 则中3等奖依次 
            string SessionBonus = "SessionBonus";

            bool boolExsitValue = Eggsoft.Common.DotCookie.Exists(SessionBonus);
            string strBonusValue = Eggsoft.Common.DotCookie.Read(SessionBonus);
            //Eggsoft.Common.DotCookie.Delete(SessionBonus);//debug

            //if (strBonusValue == "-1") boolExsitValue = false;
            int intlevel = -1;

            if ((boolExsitValue == false) || String.IsNullOrEmpty(strBonusValue))
            {
                //int intJang0 = 1;
                //int intJang1 = 10;
                //int intJang2 = 50;
                //int intJang3 = 100;
                //int intJang4 = 1000;

                int intAll = intJang0 + intJang1 + intJang2 + intJang3 + intJang4;
                //int intBase = 10000;


                //函数是这样用,比如100至999的随机数
                Random ran = new Random();
                int RandKey = ran.Next(1, intBase);
                if ((RandKey > 0) && (RandKey <= intJang0))
                {
                    intlevel = 0;
                }
                else if ((RandKey > intJang0) && (RandKey <= intJang1))
                {
                    intlevel = 1;
                }
                else if ((RandKey > intJang1) && (RandKey <= intJang2))
                {
                    intlevel = 2;
                }
                else if ((RandKey > intJang2) && (RandKey <= intJang3))
                {
                    intlevel = 3;
                }
                else if ((RandKey > intJang3) && (RandKey <= intJang4))
                {
                    intlevel = 4;
                }


                Eggsoft.Common.DotCookie.Add(SessionBonus, intlevel.ToString(), intSecondGetBonusTime);
            }
            else
            {
                intlevel = Int32.Parse(strBonusValue);
                //if ((intlevel < 0) || (intlevel > 4))//error  go out and stop
                //{
                //    Eggsoft.Common.DotCookie.Delete(SessionBonus);
                //    loadBonus();
                //    return;
                //}
            }

            int[] intgetLevelList = { 60, 0, 120, 240, 180 };
            string[] strGetLevelList = { "000.png", "100.png", "200.png", "300.png", "400.png" };
            string[] stringGetNoneLevelList = { "slide0.png", "slide1.png", "slide2.png", "slide3.png", "slide4.png", "slide5.png", "slide7.png" };

            if (intlevel > -1)
            {
                strPubJPGShow = strGetLevelList[intlevel].ToString();
                //strPubBonus = strGetLevelList[intlevel] + "," + strPubBonus;
            }
            else
            {
                //strPubBonus = "再接再厉，什么也没中！" + strPubBonus + "<br />" + "今天抽奖结束！";
                Random ranKey = new Random();
                int RandKeyNo = ranKey.Next(0, stringGetNoneLevelList.Length - 1);
                strPubJPGShow = stringGetNoneLevelList[RandKeyNo].ToString();

                //strPubAngle = intgetNoneLevelList[RandKeyNo].ToString();
            }


            //strPubAngle = (3600 + Int16.Parse(strPubAngle)).ToString();

        }

    }
}