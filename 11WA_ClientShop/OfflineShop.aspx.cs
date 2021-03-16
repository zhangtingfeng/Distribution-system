using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class OfflineShop : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected string pub_WeiXin__o2o_FootMarker_Location___ = "";

        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        private int pInt_QueryString_ParentID = 0;//；

        protected string _Pub_03Footer_html = "";


        protected string protected_stro2oHead = "";
        protected string protected_stro2oShopInfo = "";
        protected String pub_GetNickName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    _Pub_03Footer_html = Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);
                    InitWeiXinShareLink();
                    reado2oHead();
                    reado2oShop();

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
                string pub_strDESFull = "o2o,线下批发,代理联营等多种销售模式";

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;


                pub_GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/offlineshop.aspx";

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", pub_GetNickName + "o2o,线下批发,代理联营等多种销售模式");

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
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
        }


        private void reado2oHead()
        {
            pub_WeiXin__o2o_FootMarker_Location___ = Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "线下商铺");


            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

            string strXML = Model_tab_ShopClient.XML;
            Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
            string strShopLogoImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + XML__Class_Shop_Client.ShopLogoImage;


            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + pub_Int_ShopClientID);


            protected_stro2oHead += "    <div class=\"block block-border-top-none block-top-0 block-store-top\">\n";
            protected_stro2oHead += "      <div class=\"name-card name-card-store-top\">\n";
            protected_stro2oHead += "          <a href=\"https://" + Model_tab_ShopClient.ErJiYuMing + "\" class=\"thumb\">\n";
            protected_stro2oHead += "              <img src=\"" + strShopLogoImage + "\">\n";
            protected_stro2oHead += "          </a>\n";
            protected_stro2oHead += "          <a href=\"" + Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD + "\" class=\"btn btn-follow pull-right\">+ 关注微信</a>\n";
            protected_stro2oHead += "          <div class=\"detail\">\n";
            protected_stro2oHead += "              <a href=\"https://" + Model_tab_ShopClient.ErJiYuMing + "\">\n";
            protected_stro2oHead += "                  <h2>" + Model_tab_ShopClient.ShopClientName + "</h2>\n";
            protected_stro2oHead += "              </a>\n";
            protected_stro2oHead += "          </div>\n";
            protected_stro2oHead += "      </div>\n";
            protected_stro2oHead += "  </div>\n";

        }


        public class ArrayList_Shop_Distance
        {
            public string strShop_ID;
            public double DecimalDistance;
        }


        private void reado2oShop()
        {
            try
            {
                protected_stro2oShopInfo = "";

                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo();

                System.Data.DataTable ShopData_DataTable = BLL_tab_ShopClient_O2O_ShopInfo.GetList("ShopClientID=" + pub_Int_ShopClientID).Tables[0];

                Eggsoft.Common.debug_Log.Call_WriteLog("reado2oShop pub_Int_Session_CurUserID=" + pub_Int_Session_CurUserID, "Get_NestShopName");

                string strUserIdAdress = ""; double doubleBaiDulng = 0; double doubleBaiDulat = 0; Eggsoft_Public_CL.Pub.Get_o2o_NestUserID__(pub_Int_Session_CurUserID, out doubleBaiDulng, out doubleBaiDulat, out strUserIdAdress);
                List<ArrayList_Shop_Distance> mList = new List<ArrayList_Shop_Distance>();

                for (int inti = 0; inti < ShopData_DataTable.Rows.Count; inti++)
                {
                    string strShop_ID = ShopData_DataTable.Rows[inti]["ID"].ToString();
                    string str_Shop_BaiDulng = ShopData_DataTable.Rows[inti]["BaiDulng"].ToString();
                    string str_Shop_BaiDulat = ShopData_DataTable.Rows[inti]["BaiDulat"].ToString();

                    double mLat1 = 39.90923; // point1纬度
                    double mLng1 = 116.357428; // point1经度

                    double.TryParse(str_Shop_BaiDulng, out mLng1);
                    double.TryParse(str_Shop_BaiDulat, out mLat1);

                    double distance = Eggsoft.Common.GPS.GetShortDistance(mLng1, mLat1, doubleBaiDulng, doubleBaiDulat);

                    ArrayList_Shop_Distance cur = new ArrayList_Shop_Distance();
                    cur.strShop_ID = strShop_ID;
                    cur.DecimalDistance = distance;
                    mList.Add(cur);
                }

                ArrayList_Shop_Distance temp = new ArrayList_Shop_Distance();
                for (int i = mList.Count; i > 0; i--)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        if (mList[j].DecimalDistance > mList[j + 1].DecimalDistance)
                        {
                            temp = mList[j];
                            mList[j] = mList[j + 1];
                            mList[j + 1] = temp;
                        }
                    }

                }


                string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();


                for (int inti = 0; inti < mList.Count; inti++)
                {
                    string strShop_ID = mList[inti].strShop_ID;
                    double DecimalDistance = mList[inti].DecimalDistance / 1000;

                    Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(Int32.Parse(strShop_ID));

                    string strShopName = Model_tab_ShopClient_O2O_ShopInfo.ShopName;
                    string strShopAdMsg = Model_tab_ShopClient_O2O_ShopInfo.ShopAdMsg;
                    string strTel = Model_tab_ShopClient_O2O_ShopInfo.Tel;
                    string strShopDayTime = Model_tab_ShopClient_O2O_ShopInfo.ShopDayTime;
                    string strLng = Model_tab_ShopClient_O2O_ShopInfo.BaiDulng;
                    string strLat = Model_tab_ShopClient_O2O_ShopInfo.BaiDulat;

                    string strShopAdress = "";


                    int intAdddressProvince = Model_tab_ShopClient_O2O_ShopInfo.AdddressProvince;
                    int intAdddressCity = Model_tab_ShopClient_O2O_ShopInfo.AdddressCity;
                    int intAdddressCountry = Model_tab_ShopClient_O2O_ShopInfo.AdddressCountry;
                    strShopAdress += Eggsoft_Public_CL.ProvinceCityCountry.getProvinceCityCountry(intAdddressProvince, "Province");
                    strShopAdress += Eggsoft_Public_CL.ProvinceCityCountry.getProvinceCityCountry(intAdddressCity, "City");
                    strShopAdress += Eggsoft_Public_CL.ProvinceCityCountry.getProvinceCityCountry(intAdddressCountry, "Area");
                    strShopAdress += Model_tab_ShopClient_O2O_ShopInfo.ShopAdress;

                    string strShopImage = "";
                    string strXML = Model_tab_ShopClient_O2O_ShopInfo.XML;

                    try
                    {
                        if (string.IsNullOrEmpty(strXML))
                        {
                        }
                        else
                        {
                            Eggsoft_Public_CL.XML__Class_Shop_O2o XML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_O2o>(strXML, System.Text.Encoding.UTF8);
                            strShopImage = XML__Class_Shop_O2o.ShopLogoo2oImage;
                        }
                    }
                    catch { }
                    finally { }

                    string strBaiDuLink = "https://api.map.baidu.com/marker?zoom=10&location=" + strLat + "," + strLng + "&amp;title=" + strShopName + "&amp;content=" + strShopName + "&amp;output=html";

                    protected_stro2oShopInfo += "  <div class=\"block block-order\">\n";
                    protected_stro2oShopInfo += "     <div class=\"store-header header\">\n";
                    protected_stro2oShopInfo += "         <span>店铺：" + strShopName + "</span><span style=\"float:right;\">距离：" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(DecimalDistance)) + "公里</span>\n";
                    protected_stro2oShopInfo += "     </div>\n";
                    protected_stro2oShopInfo += "     <hr class=\"margin-0 left-10\">\n";
                    protected_stro2oShopInfo += "     <div class=\"name-card name-card-3col name-card-store clearfix\">\n";
                    protected_stro2oShopInfo += "         <a href=\"" + strgetwebHttp + "\" class=\"thumb js-view-image-list\">\n";
                    protected_stro2oShopInfo += "             <img class=\"js-view-image-item\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strShopImage + "\">\n";
                    protected_stro2oShopInfo += "         </a>\n";

                    protected_stro2oShopInfo += "     <div id=\"Nav_My\">    <a href=\"tel:" + strTel + "\">\n";
                    protected_stro2oShopInfo += "             <div class=\"phone\"></div>\n";
                    protected_stro2oShopInfo += "         </a>\n";
                    protected_stro2oShopInfo += "         <a href=\"" + strBaiDuLink + "\">\n";
                    protected_stro2oShopInfo += "             <div class=\"nav\"></div>\n";
                    protected_stro2oShopInfo += "         </a></div>\n";

                    protected_stro2oShopInfo += "         <a class=\"detail\" href=\"" + strBaiDuLink + "\">\n";
                    protected_stro2oShopInfo += "             <h3>" + strShopAdress + "</h3>\n";
                    protected_stro2oShopInfo += "             <p class=\"c-gray-dark ellipsis\" style=\"margin-top: 5px\">\n";
                    protected_stro2oShopInfo += "                 营业时间：" + strShopDayTime + "               \n";
                    protected_stro2oShopInfo += "             </p>\n";
                    protected_stro2oShopInfo += "         </a>\n";
                    protected_stro2oShopInfo += "            </div>\n";
                    protected_stro2oShopInfo += "           <hr>\n";
                    protected_stro2oShopInfo += "           <div class=\"name-card-bottom c-gray-dark\">商家推荐：" + strShopAdMsg + "</div>\n";
                    protected_stro2oShopInfo += "       </div>\n";

                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally { }


        }
    }
}