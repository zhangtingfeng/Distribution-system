using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.AddFunction._06coupons
{
    public partial class Index : System.Web.UI.Page
    {
        protected string _Pub_03Footer_html = "";

        protected string pub_MyShowList = "";
        protected string pub_GetNickName_From_Share__ = "";

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

                    doMyGetList();
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
                string pub_strDESFull = "查看可用优惠券";

                string pub_str_FirstImageFull = "";

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;


                pub_GetNickName_From_Share__ = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                string pub_strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/addfunction/06coupons/index.aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", pub_str_FirstImageFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", pub_strURL);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Des_Goods###", pub_strDESFull);
                strShareLink = strShareLink.Replace("###_PulicChageWeiXin_Title_Goods###", pub_GetNickName_From_Share__ + "邀请你查看可用优惠券");

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "邀请你查看可用优惠券");
            }
            finally
            {

            }

            strShareLink = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strShareLink);//

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



        private void doMyGetList()
        {
            EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail bll_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();

            Int32 Int32usedType = Request.QueryString["usedType"].toInt32(); ///1  2   3


            string strSELECTWhere = @"SELECT   tab_ShopClient_Shopping_VouchersScheme.Vouchers_Title, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.UserID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.UserID_Old, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.ShopClientID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.GuWuCheIDOrOrderDetailID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.VouchersNum, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.ID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.Money, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.MoneyUsed, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.CreatTime, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.UpdateTime, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateStartTime, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime, 
                tab_ShopClient_Shopping_VouchersScheme.GoodList, tab_ShopClient_Shopping_VouchersScheme.HowToUse, 
                tab_ShopClient_Shopping_VouchersScheme.HowToUseLimitMaxMoney, 
                tab_ShopClient_Shopping_VouchersScheme.LimitHowMany, 
                tab_ShopClient_Shopping_VouchersScheme.HowToGet
                  FROM      tab_ShopClient_Shopping_VouchersScheme RIGHT OUTER JOIN
                tab_ShopClient_Shopping_VouchersScheme_Detail ON 
                tab_ShopClient_Shopping_VouchersScheme.ID = tab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID";


            if ((Int32usedType == 0) || (Int32usedType == 1))
            {
                strSELECTWhere += " where isnull(tab_ShopClient_Shopping_VouchersScheme_Detail.GuWuCheIDOrOrderDetailID,0)=0 and tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime>getdate() ";
            }
            else if ((Int32usedType == 2))
            {
                strSELECTWhere += " where tab_ShopClient_Shopping_VouchersScheme_Detail.GuWuCheIDOrOrderDetailID>0";
            }
            else if ((Int32usedType == 3))
            {
                strSELECTWhere += " where isnull(tab_ShopClient_Shopping_VouchersScheme_Detail.GuWuCheIDOrOrderDetailID,0)=0 and tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime<=getdate() ";
            }
            strSELECTWhere += " and tab_ShopClient_Shopping_VouchersScheme_Detail.ShopClientID = " + pub_Int_ShopClientID + " and tab_ShopClient_Shopping_VouchersScheme_Detail.UserID = " + pub_Int_Session_CurUserID;
            strSELECTWhere += " order by tab_ShopClient_Shopping_VouchersScheme_Detail.ID desc";

            Int32 Int32ShopClient_IDHowManyGoods = bll_tab_Goods.ExistsCount("IsDeleted=0  and Shopping_Vouchers_Percent>0 and isSaled=1 and ShopClient_ID=" + pub_Int_ShopClientID + " ");////判断全场可用的字符串使用

            Eggsoft.Common.debug_Log.Call_WriteLog(strSELECTWhere, "优惠券", "SQL执行语句");
            System.Data.DataTable Data_DataTable = bll_VouchersScheme_Detail.SelectList(strSELECTWhere).Tables[0];
            if (Data_DataTable.Rows.Count > 0)
            {
                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                {
                    string strUserID_Old = Data_DataTable.Rows[i]["UserID_Old"].toString();
                    string strVouchers_Title = Data_DataTable.Rows[i]["Vouchers_Title"].toString();
                    string strVouchersNum = Data_DataTable.Rows[i]["VouchersNum"].toString();
                    string strID = Data_DataTable.Rows[i]["ID"].toString();
                    Int32 Int32GuWuCheIDOrOrderDetailID = Data_DataTable.Rows[i]["GuWuCheIDOrOrderDetailID"].toInt32();
                    string strGoodList = Data_DataTable.Rows[i]["GoodList"].toString();
                    Decimal DecimalMoney = Data_DataTable.Rows[i]["Money"].toDecimal();
                    string strHowToUse = Data_DataTable.Rows[i]["HowToUse"].toString();
                    string strHowToUseLimitMaxMoney = Data_DataTable.Rows[i]["HowToUseLimitMaxMoney"].toString();
                    DateTime strValidateStartTime = Convert.ToDateTime(Data_DataTable.Rows[i]["ValidateStartTime"].toDateTime());
                    DateTime strValidateEndTime = Convert.ToDateTime(Data_DataTable.Rows[i]["ValidateEndTime"].toDateTime());
                    string strLimitHowMany = Data_DataTable.Rows[i]["LimitHowMany"].toString();

                    string StringGoodID = "0";


                    #region 未使用
                    string strList = "<li>";
                    strList += " <span class=\"coupons_price\"><i>￥</i>{0}<em>{1}</em></span><div class=\"coupons_tips\">";///<span style=\"font-size: 0.2rem;line-height: 0.2rem;\">(" + strVouchersNum + ")<span>
                    strList += "            <div class=\"coupons_tips_left\"><span><i class=\"icon_tips icon-icon_warning\"></i>&nbsp;{2}</span><span><i class=\"icon_clock icon-icon_time\"></i>&nbsp; {3}-{4}</span></div>";
                    strList += "             <a class='btn_share' style=\"font-size: 0.6rem;line-height: 0.6rem;{5}\" href='javascript:goToShare(\"{6}\",\"{0}\",\""+ strVouchers_Title + "\")'>{7}</a>";
                    strList += "             <a class='btn_receive' style=\"font-size: 0.6rem;line-height: 0.6rem;{8}\" href='javascript:goToUse(\"{9}\")'>{10}</a>";
                    strList += "       </div>";
                    strList += "   </li>";

                    #region 特定商品可用
                    string strSalesQuan = "";
                    if ((string.IsNullOrEmpty(strGoodList)) || (strGoodList.Split(',').Length >= Int32ShopClient_IDHowManyGoods))
                    {
                        strSalesQuan = "全场商品可用";
                    }
                    else
                    {
                        string[] strCharGoodList = strGoodList.Split(',');

                        if (strCharGoodList.Length > 1)
                        {
                            strSalesQuan = "特定商品可用";
                        }
                        else if (strCharGoodList.Length == 1)
                        {
                            string strName = bll_tab_Goods.GetList("Name", "id=" + strCharGoodList[0]).Tables[0].Rows[0]["Name"].toString();
                            strSalesQuan += strName + " ";
                            strSalesQuan = Eggsoft.Common.StringNum.MaxLengthString(strSalesQuan, 20);
                            StringGoodID = strCharGoodList[0];
                        }
                    }
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
                      Eggsoft.Common.StringNum.MaxLengthString(strVouchers_Title + strSalesQuan, 15),
                        strOrderLimitShow,
                        strValidateStartTime.ToString("yyyy.MM.dd"),
                         strValidateEndTime.ToString("yyyy.MM.dd"),
                           ((strUserID_Old.toInt32() > 0|| (Int32usedType == 2 || Int32usedType == 3)) ? "display:none" : ""),///去分享
                         strVouchersNum,
                         ((strUserID_Old.toInt32() > 0) ? "" : "分享该券"),///去分享
                         ((Int32usedType == 2 || Int32usedType == 3) ? "display:none" : ""),///去使用
                         StringGoodID,
                         ((Int32usedType == 2 || Int32usedType == 3) ? "" : "去使用")///去使用
                        );
                    pub_MyShowList += strList; ///已使用
                    #endregion 未使用
                }
            }

        }
    }
}