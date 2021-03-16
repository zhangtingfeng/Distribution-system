using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.AddFunction._06coupons
{

    public partial class IndexList : System.Web.UI.Page
    {
        protected string _Pub_03Footer_html = "";

        protected string pub_ShowList = "";
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
                    doGetList();
                    InitWeiXinShareLink();
                    _Pub_03Footer_html = Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);

                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "优惠券", "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "优惠券");
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
                string pub_strDESFull = "领用优惠券";

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/addFunction/06coupons/indexlist.aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", GetNickName + "邀请你领用优惠券");

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "邀请你领用优惠券");
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
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
            pInt_DB_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(pub_Int_Session_CurUserID);


            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();

            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
            Pub_IntStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);
        }
        private void doGetList()
        {
            EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            Int32 Int32HowManyGoods = bll_tab_Goods.ExistsCount("IsDeleted=0 and isSaled=1 and Shopping_Vouchers_Percent>0 and ShopClient_ID=" + pub_Int_ShopClientID + " and Shopping_Vouchers_Percent>0");////判断全场可用的字符串使用
            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme bll_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();


            System.Data.DataTable Data_DataTable = bll_VouchersScheme.GetList(@"[ID],[Vouchers_Title]
      ,[Money]
      ,[AllCount]
      ,[ShopClientID]
      ,[GoodList]
      ,[HowToUse]
      ,[HowToUseLimitMaxMoney]
      ,[LimitHowMany]
      ,[ValidateStartTime]
      ,[ValidateEndTime]
      ,[HowToGet]", "ShopClientID=" + pub_Int_ShopClientID + " and ValidateEndTime>getdate() and HowToGet=0 order by id desc").Tables[0];
            if (Data_DataTable.Rows.Count > 0)
            {
                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                {
                    string strVouchers_Title = Data_DataTable.Rows[i]["Vouchers_Title"].toString();
                    string strID = Data_DataTable.Rows[i]["ID"].toString();
                    string strAllCount = Data_DataTable.Rows[i]["AllCount"].toString();
                    string strGoodList = Data_DataTable.Rows[i]["GoodList"].toString();
                    Decimal DecimalMoney = Data_DataTable.Rows[i]["Money"].toDecimal();
                    string strHowToUse = Data_DataTable.Rows[i]["HowToUse"].toString();
                    string strHowToUseLimitMaxMoney = Data_DataTable.Rows[i]["HowToUseLimitMaxMoney"].toString();
                    DateTime strValidateStartTime = Convert.ToDateTime(Data_DataTable.Rows[i]["ValidateStartTime"].toDateTime());
                    DateTime strValidateEndTime = Convert.ToDateTime(Data_DataTable.Rows[i]["ValidateEndTime"].toDateTime());
                    string strLimitHowMany = Data_DataTable.Rows[i]["LimitHowMany"].toString();



                    string strList = "<li style=\"width: 100 % !important\">";
                    strList += "<span class=\"coupons_price\"><i>￥</i>{0}<em>{1}</em></span><div class=\"coupons_tips\">";
                    strList += "            <div class=\"coupons_tips_left\"><span><i class=\"icon_tips icon-icon_warning\"></i>&nbsp;{2} </span><span><i class=\"icon_clock icon-icon_time\"></i>&nbsp; {3}-{4}</span></div>";
                    //strList += "            <a class='btn_receive' style=\"float:right; margin-right:4.5rem\" href='javascript:goToLook(\"" + strID + "\")'>查看</a>";
                    strList += "            <a class='btn_receive' href='javascript:goToUse(\"" + strID + "\")'>立即领取</a>";
                    strList += "        </div>";
                    strList += "    </li>";


                    #region 特定商品可用
                    string strSalesQuan = "";
                    if (strGoodList.Split(',').Length >= Int32HowManyGoods)
                    {
                        strSalesQuan = "全场商品可用";
                    }
                    else
                    {
                        string[] strCharGoodList = strGoodList.Split(',');
                        for (int k = 0; k < Math.Min(strCharGoodList.Length, 4); k++)
                        {
                            string strName = bll_tab_Goods.GetList("Name", "id=" + strCharGoodList[k]).Tables[0].Rows[0]["Name"].toString();
                            strSalesQuan += strName + " ";
                        }
                        if (strCharGoodList.Length > 4) strSalesQuan += "等";

                        strSalesQuan = Eggsoft.Common.StringNum.MaxLengthString(strSalesQuan, 20);
                    }
                    //                        ((strGoodList.Split(',').Length >= Int32HowManyGoods) ? "全场商品可用" : "特定商品可用");
                    #endregion

                    #region 订单无限制
                    string strOrderLimitShow = "";
                    if (strHowToUse == "0")
                    {
                        strOrderLimitShow = "限制面额使用,余额由系统回收";
                    }
                    else if (strHowToUse == "1")
                    {
                        strOrderLimitShow = "商品满￥" + strHowToUseLimitMaxMoney + "才能使用";
                    }
                    #endregion
                    strList = String.Format(strList,
                        Eggsoft_Public_CL.Pub.getBankPubMoney(Convert.ToDecimal(DecimalMoney)),
                        strVouchers_Title + strSalesQuan,
                        strOrderLimitShow,
                        strValidateStartTime.ToString("yyyy.MM.dd"),
                         strValidateEndTime.ToString("yyyy.MM.dd")
                        );
                    pub_ShowList += strList;
                }
            }

        }


    }
}