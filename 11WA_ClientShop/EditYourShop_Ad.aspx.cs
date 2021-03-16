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
    public partial class EditYourShop_Ad : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected string pub_WeiXin__o2o_FootMarker_Location___ = "";

        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected string _Pub_03Footer_html = "";

        protected string _pub_GetAgentAdpolText = "";
        protected string pub_AddExpListTextShowString = "";

        protected string pub_StrUserIDSelectProductList = "";//请选择分销商品 已选数组


        protected string[] pub_stringEditShopList;//店铺名称等等

        protected string pub_stringChoiceProductClsssList = "";//挑选分类商品  待选  待触摸

        protected string pub_AgentLevelList = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    InitWeiXinShareLink();
                    initEditShopList();
                    initChoiceProductClsssList();///挑选商品
                    _Pub_03Footer_html = Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);
                    pub_WeiXin__o2o_FootMarker_Location___ = Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "管理代理");

                    init_GetAgentADpolText();
                    init_GetAgentAD_LevelName();

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
                string pub_strDESFull = "管理代理商,微信代理,朋友圈代理,线下批发,代理联营等多种销售模式";

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/edityourshop_ad.aspx";

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", GetNickName + "管理代理商,微信代理,朋友圈代理");

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
        private void initEditShopList()
        {
            string stringEditShopList = "";

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID+ "  and IsDeleted=0 ");
            if (Model_tab_ShopClient_Agent_ != null)
            {
                stringEditShopList = Model_tab_ShopClient_Agent_.ShopName;
            }

            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
            if (stringEditShopList == "")///如果 没有 就默认 昵称
            {
                stringEditShopList = Model_tab_User.NickName;
            }
            stringEditShopList += ("," + Model_tab_User.UserRealName);
            stringEditShopList += ("," + Model_tab_User.ContactPhone);
            stringEditShopList += ("," + Model_tab_User.AlipayNumOrWeiXinPay);
            stringEditShopList=stringEditShopList.Replace("undefined", "").Replace("null", "");////页面上不要显示undefined
            pub_stringEditShopList = stringEditShopList.Split(',');


            #region 省份证等
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
            EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + strShopClientID);
            string strAddExpListTextShow = Model_tab_ShopClient_ShopPar.AddExpListTextShow;
            if ((String.IsNullOrEmpty(strAddExpListTextShow) == false))
            {
                string strAddExp = "";


                string[] strAddExpListTextList = strAddExpListTextShow.Split('#');
                for (int i = 0; i < strAddExpListTextList.Length; i++)
                {
                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        if (i == 0) strAddExp = Model_tab_ShopClient_Agent_.AddExp0;
                        if (i == 1) strAddExp = Model_tab_ShopClient_Agent_.AddExp1;
                        if (i == 2) strAddExp = Model_tab_ShopClient_Agent_.AddExp2;
                        if (i == 3) strAddExp = Model_tab_ShopClient_Agent_.AddExp3;
                        if (i == 4) strAddExp = Model_tab_ShopClient_Agent_.AddExp4;
                        if (i == 5) strAddExp = Model_tab_ShopClient_Agent_.AddExp5;
                        if (i == 6) strAddExp = Model_tab_ShopClient_Agent_.AddExp6;
                        if (i == 7) strAddExp = Model_tab_ShopClient_Agent_.AddExp7;
                        if (i == 8) strAddExp = Model_tab_ShopClient_Agent_.AddExp8;
                    }
                    pub_AddExpListTextShowString += "  <div class=\"row\">\n";
                    pub_AddExpListTextShowString += "    <div class=\"large-12 columns\">\n";
                    pub_AddExpListTextShowString += "      <label>" + strAddExpListTextList[i] + "</label>\n";
                    pub_AddExpListTextShowString += "     </div>\n";
                    pub_AddExpListTextShowString += "    <div class=\"small-8 columns input-col\">\n";
                    pub_AddExpListTextShowString += "      <input name=\"AddExp" + i + "\"  id=\"AddExp" + i + "\" value=\"" + strAddExp + "\" placeholder=\"必须输入" + strAddExpListTextList[i] + "\" type=\"text\">\n";
                    pub_AddExpListTextShowString += "    </div>\n";
                    pub_AddExpListTextShowString += " </div>\n";
                }
            }
            #endregion
        }



        private void init_GetAgentAD_LevelName()
        {
            pub_AgentLevelList = "";
            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
            DataSet myds_Agent_Level = null;
            myds_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetList("ID,AgentLevelName", " ShopClientID=" + pub_Int_ShopClientID + " order by Sort asc,id asc");
            //}
            int intAgentLevelSelect = 0;
            bool booEmpowered = false;
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID+ "  and IsDeleted=0 ");
            if (Model_tab_ShopClient_Agent_ != null)
            {
                intAgentLevelSelect = Model_tab_ShopClient_Agent_.AgentLevelSelect.toInt32();
                booEmpowered = Model_tab_ShopClient_Agent_.Empowered.toBoolean();
            }
            ///看看上级家 是什么代理 肯定不能比上家高
            string strparentagentadid = Request["parentagentadid"];
            int intparentagentadid = 0;
            int.TryParse(strparentagentadid, out intparentagentadid);
            if (intparentagentadid > 0)
            {
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_Parent = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intparentagentadid);
                int intAgentLevelSelect_Parent = Model_tab_ShopClient_Agent_Parent.AgentLevelSelect.toInt32();

                int intPos = 0;
                int[] myLevelIDList = new int[myds_Agent_Level.Tables[0].Rows.Count];
                for (int i = 0; i < (myds_Agent_Level.Tables[0].Rows.Count); i++)
                {
                    string strID = myds_Agent_Level.Tables[0].Rows[i]["ID"].ToString();

                    if (intAgentLevelSelect_Parent == Int32.Parse(strID)) intPos = i;
                    myLevelIDList[i] = Int32.Parse(strID);
                }
                if (intPos == myds_Agent_Level.Tables[0].Rows.Count - 1)
                {
                    ///已经是最低级了  下面不能油料
                    ///
                    Eggsoft.Common.JsUtil.ShowMsg("只能申请代理分销商", Pub_Agent_Path + "/edityourshop.aspx");
                }

                for (int i = intPos + 1; i < (myds_Agent_Level.Tables[0].Rows.Count); i++)
                {
                    string strID = myds_Agent_Level.Tables[0].Rows[i]["ID"].ToString();
                    string strAgentLevelName = myds_Agent_Level.Tables[0].Rows[i]["AgentLevelName"].ToString();
                    if ((intAgentLevelSelect > 0) && (booEmpowered))
                    {
                        if (intAgentLevelSelect.ToString() == strID)
                        {
                            pub_AgentLevelList += "<option value=\"" + strID + "\">" + strAgentLevelName + "</option>";
                        }
                    }
                    else
                    { pub_AgentLevelList += "<option value=\"" + strID + "\">" + strAgentLevelName + "</option>"; }

                }
            }
            else
            {
                for (int i = 0; i < (myds_Agent_Level.Tables[0].Rows.Count); i++)
                {
                    string strID = myds_Agent_Level.Tables[0].Rows[i]["ID"].ToString();
                    string strAgentLevelName = myds_Agent_Level.Tables[0].Rows[i]["AgentLevelName"].ToString();
                    if ((intAgentLevelSelect > 0) && (booEmpowered))
                    {
                        if (intAgentLevelSelect.ToString() == strID)
                        {
                            pub_AgentLevelList += "<option value=\"" + strID + "\">" + strAgentLevelName + "</option>";
                        }
                    }
                    else
                    { pub_AgentLevelList += "<option value=\"" + strID + "\">" + strAgentLevelName + "</option>"; }
                }
            }

        }

        private void init_GetAgentADpolText()
        {
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
            string strAgentMustReadAd = Model_tab_ShopClient.AgentMustReadAd;
            if (String.IsNullOrEmpty(strAgentMustReadAd))
            {
                strAgentMustReadAd = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgentAd();

            }
            else
            {
                /*正则表达式替换求助
    如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
    例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                strAgentMustReadAd = Server.HtmlDecode(strAgentMustReadAd);

                strAgentMustReadAd = Regex.Replace(strAgentMustReadAd, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                string strSearch = "src=\"/upload/";
                strAgentMustReadAd = strAgentMustReadAd.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/upload/");
                strSearch = "src=\"/Upload/";
                strAgentMustReadAd = strAgentMustReadAd.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/upload/");


            }
            _pub_GetAgentAdpolText = strAgentMustReadAd;


        }


        private void initChoiceProductClsssList()
        {
            string stringEditShopList = "";
            string stringUserIDSelectProductList = "";

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_BLL_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("userID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID+ "  and IsDeleted=0 ");


            EggsoftWX.BLL.tab_ShopClient_DistributionMoney BLL_tab_ShopClient_DistributionMoney = new EggsoftWX.BLL.tab_ShopClient_DistributionMoney();
            EggsoftWX.Model.tab_ShopClient_DistributionMoney Model_tab_ShopClient_DistributionMoney = new EggsoftWX.Model.tab_ShopClient_DistributionMoney();

            EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();

            EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
            EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = null;
            bool boolIfChoice = BLL_tab_ShopClient_Agent__ProductClassID.Exists("UserID=" + pub_Int_Session_CurUserID);//是否选择过

            String strAgentLevelName = "";

            if (Model_BLL_tab_ShopClient_Agent_ != null)
            {
                if (Model_BLL_tab_ShopClient_Agent_.AgentLevelSelect > 0 && Model_BLL_tab_ShopClient_Agent_.AgentLevelSelect < 99999)
                {

                    EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                    EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetModel("id=" + Model_BLL_tab_ShopClient_Agent_.AgentLevelSelect);
                    if (Model_tab_ShopClient_Agent_Level != null)
                    {
                        strAgentLevelName = Model_tab_ShopClient_Agent_Level.AgentLevelName.Trim() + "";
                    }
                 }
            }



            ///是否级店铺
            ///    
            DataSet myds = null;


            //都是总店铺挑选
            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            myds = BLL_tab_Goods.GetList("ID,Name,Webuy8_DistributionMoney_Value", " ShopClient_ID=" + pub_Int_ShopClientID + " and isSaled=1 and IsDeleted=0 order by Sort asc");
            //}
            for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
            {
                string strGoodID = myds.Tables[0].Rows[i]["ID"].ToString();
                string strName = myds.Tables[0].Rows[i]["Name"].ToString();
                string strWebuy8_DistributionMoney_Value = myds.Tables[0].Rows[i]["Webuy8_DistributionMoney_Value"].ToString();
                int intDistributionMoney_Value = 0;
                int.TryParse(strWebuy8_DistributionMoney_Value, out intDistributionMoney_Value);
                Model_tab_ShopClient_DistributionMoney = BLL_tab_ShopClient_DistributionMoney.GetModel(intDistributionMoney_Value);

                string strPartner = "20.00"; string strPartner1 = "15.00"; string strPartner2 = "5.00";
                if (Model_tab_ShopClient_DistributionMoney != null)
                {
                    strPartner = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_DistributionMoney.Partner);
                    strPartner1 = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_DistributionMoney.Partner1);
                    strPartner2 = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_DistributionMoney.Partner2);
                }

                //bool boolExsit = BLL_tab_ShopClient_Agent__ProductClassID.Exists("UserID=" + pub_Int_Session_CurUserID + " and ProductID=" + strGoodID);//是否选中
                Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("UserID=" + pub_Int_Session_CurUserID + " and ProductID=" + strGoodID);
                bool boolExsit = Model_tab_ShopClient_Agent__ProductClassID != null;
                string strIFcurrent = "";
                if (boolExsit) strIFcurrent = " current";
                if (boolIfChoice == false) strIFcurrent = " current";//没选择过都选上

                if (strIFcurrent == " current")
                {
                    stringUserIDSelectProductList += strGoodID + ",";
                }


                if (i % 2 == 0) stringEditShopList += " <div style=\"width: 9.93631%; padding-left: 10px; padding-bottom: 10px; float: left; box-sizing: border-box;\" id=\"item0eHnb5\" class=\"galcolumn\">\n";//判断奇偶数
                stringEditShopList += "      <div class=\"item" + strIFcurrent + "\" name=\"columns\" style=\"margin-bottom: 10px; opacity: 1;\" cid=\"" + strGoodID + "\">\n";
                stringEditShopList += "          <div>\n";
                stringEditShopList += "              <h5>" + Eggsoft.Common.StringNum.MaxLengthString(strName, 8) + "</h5>\n";
                stringEditShopList += "               <ul class=\"percent\">\n";
                if (boolExsit)//
                {
                    if (Model_tab_ShopClient_Agent__ProductClassID.Empowered.toBoolean())//存在 并且已授权 才显示
                    {
                        if (Model_BLL_tab_ShopClient_Agent_.Empowered.toBoolean())
                        {
                            if (String.IsNullOrEmpty(strAgentLevelName) == false)
                            {

                                Decimal ProductPrice = 0;
                                if (Model_BLL_tab_ShopClient_Agent_ != null)
                                {
                                    EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("shopClient_Agent_Level_ID=" + Model_BLL_tab_ShopClient_Agent_.AgentLevelSelect + " and ProductID=" + strGoodID);
                                    if (Model_tab_ShopClient_Agent_Level_ProductInfo != null)
                                    {
                                        ProductPrice = Model_tab_ShopClient_Agent_Level_ProductInfo.ProductPrice.toDecimal();
                                    }
                                }


                                stringEditShopList += "                   <li>" + strAgentLevelName + "价格:<strong>" + Eggsoft_Public_CL.Pub.getPubMoney(ProductPrice) + "￥</strong></li></ul>\n";
                            }
                        }
                        else
                        {
                            stringEditShopList += "                   <li>尚未授权,请耐心等待</li></ul>\n";
                        }
                    }
                    else
                    {
                        stringEditShopList += "                   <li>尚未授权,请耐心等待</li></ul>\n";
                    }
                }
                //stringEditShopList += "                   <li>一级分店佣金:<strong>" + strPartner1 + "%</strong></li>\n";
                //stringEditShopList += "                   <li>二级分店佣金:<strong>" + strPartner2 + "%</strong></li>\n";
                stringEditShopList += "               \n";
                stringEditShopList += "            </div>\n";
                stringEditShopList += "       </div>\n";

                if ((i % 2 == 1) || ((i % 2 == 0) && (i == myds.Tables[0].Rows.Count - 1))) stringEditShopList += "</div>\n";//判断奇偶数   偶数并且是最后一个


            }
            pub_stringChoiceProductClsssList = stringEditShopList;

            if (stringUserIDSelectProductList.IndexOf(",") > -1)
            {
                pub_StrUserIDSelectProductList = stringUserIDSelectProductList.Substring(0, stringUserIDSelectProductList.Length - 1);//去掉最后一个逗号； }

            }
        }





        //<div class="row">
        //            <div class="large-12 columns">
        //                <label>店铺名称</label>
        //            </div>
        //            <div class="small-8 columns input-col">
        //                 <input id="ShopName" value="张家界" placeholder="店铺名称" type="text">
        //            </div>
        //        </div>
        //        <div class="row">
        //            <div class="large-12 columns">
        //                <label>联系人姓名</label>
        //            </div>
        //            <div class="small-8 columns input-col">
        //                <input id="ContactName" value="" placeholder="真实姓名(向您转账信息)" type="text">
        //            </div>
        //        </div>
        //        <div class="row">
        //            <div class="large-12 columns">
        //                <label>手机号码</label>
        //            </div>
        //            <div class="small-8 columns input-col">
        //                <input id="ContactMobileName" value="" placeholder="联系人手机" type="text">
        //            </div>
        //        </div>
        //        <div class="row">
        //            <div class="large-12 columns">
        //                <label>支付宝或微信</label>
        //            </div>
        //            <div class="small-8 columns input-col">
        //                <input id="Alipay" value="" placeholder="您的支付宝或者微信号" type="text">
        //            </div>
        //        </div>
    }
}