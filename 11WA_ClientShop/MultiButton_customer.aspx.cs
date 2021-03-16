using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_customer : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_customer_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = InitOpenShopAsk(strTemplet);

                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "直接代理收入"));

                    strTemplet = strTemplet.Replace("###Header###", "");
                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());
                    
                    Response.Write(strTemplet);

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
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }
        private String InitOpenShopAsk(string strTemplet)
        {
            string strLookmySonID = Request.QueryString["LookmySonID"];
            int intLookmySonID = 0;
            int.TryParse(strLookmySonID, out intLookmySonID);

            string strAgentLevelNum = Request.QueryString["AgentLevelNum"];
            int intAgentLevelNum = 0;
            int.TryParse(strAgentLevelNum, out intAgentLevelNum);

            int intGetMySon_AgentMoney = pub_Int_Session_CurUserID;
            if (intLookmySonID > 0)
            {
                intGetMySon_AgentMoney = intLookmySonID;
            }
            //EggsoftWX.BLL.View_ShopClient_Agent_SalesOrderBy BLL_View_ShopClient_Agent_SalesOrderBy = new EggsoftWX.BLL.View_ShopClient_Agent_SalesOrderBy();
            //EggsoftWX.Model.View_ShopClient_Agent_SalesOrderBy Model_View_ShopClient_Agent_SalesOrderBy = BLL_View_ShopClient_Agent_SalesOrderBy.GetModel("userid=" + pub_Int_Session_CurUserID);
            //Decimal myAllFenXiaoMoney = 0;
            //if (Model_View_ShopClient_Agent_SalesOrderBy != null)
            //{
            //    myAllFenXiaoMoney = Convert.ToDecimal(Model_View_ShopClient_Agent_SalesOrderBy.AllFenXiaoMoney);
            //}

            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            Decimal myAllFenXiaoMoney = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList("select sum(ConsumeOrRechargeMoney) from tab_TotalCredits_Consume_Or_Recharge where UserID=@UserID and ShopClient_ID=@ShopClient_ID and (ConsumeOrRechargeType=10 or ConsumeOrRechargeType=12) and Bool_ConsumeOrRecharge=1", pub_Int_Session_CurUserID, pub_Int_ShopClientID).Tables[0].Rows[0][0].toDecimal();


            strTemplet = strTemplet.Replace("###CountMoney###", "￥" + Eggsoft_Public_CL.Pub.getPubMoney(myAllFenXiaoMoney));

            strTemplet = strTemplet.Replace("###comment_list###", GetMoneySList(pub_Int_Session_CurUserID));

            strTemplet = strTemplet.Replace("###View_ShopClient_Agent_SalesOrderBy_StatusDateTime###", GetMaxUpdagteTimeMoneySList(pub_Int_Session_CurUserID));


            return strTemplet;

        }
        public String GetMaxUpdagteTimeMoneySList(int intArgUserID)
        {
            string strReturn = "";

            EggsoftWX.BLL.tab_tab_ShopClient_myGetFenXiaoMoneyHistory blltab_tab_ShopClient_myGetFenXiaoMoneyHistory = new EggsoftWX.BLL.tab_tab_ShopClient_myGetFenXiaoMoneyHistory();
            string StrWhere = " SELECT max([CreatTime]) as maxCreatTime FROM [tab_tab_ShopClient_myGetFenXiaoMoneyHistory] where UserID=" + intArgUserID;
            System.Data.DataTable myDataTable = blltab_tab_ShopClient_myGetFenXiaoMoneyHistory.SelectList(StrWhere).Tables[0];
            if (myDataTable.Rows.Count > 0)
            {
                string strCreatTime = myDataTable.Rows[0]["maxCreatTime"].ToString();
                if (String.IsNullOrEmpty(strCreatTime))
                {
                    strCreatTime = DateTime.Now.ToString();
                }
                strReturn = DateTime.Parse(strCreatTime).ToString("d日h时");
            }
            return strReturn;
        }

        public String GetMoneySList(int intArgUserID)
        {
            EggsoftWX.BLL.tab_tab_ShopClient_myGetFenXiaoMoneyHistory blltab_tab_ShopClient_myGetFenXiaoMoneyHistory = new EggsoftWX.BLL.tab_tab_ShopClient_myGetFenXiaoMoneyHistory();

            //EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLLtab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            //EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Modeltab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();

            String strBody = "";
            strBody += "<li style=\"height:auto;border:none;\" ms-repeat=\"items\">\n";
            strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

            strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>昵称</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>订单时间</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>所得</strong>¥" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>收入描述</strong>¥" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>转化</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "			</div>\n";
            strBody += "		</li>\n";



            System.Data.DataTable myDataTable = blltab_tab_ShopClient_myGetFenXiaoMoneyHistory.GetDataTable("50", "*", "UserID=" + intArgUserID + " order by CreatDateTime desc");
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {


                string strBuyUserIDNickName = myDataTable.Rows[i]["BuyUserIDNickName"].ToString();
                string strCreatDateTime = myDataTable.Rows[i]["CreatDateTime"].ToString();
                string strMyWillGetMoney = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["MyWillGetMoney"].ToString()));
                string strIFGetToMoneyDesc = myDataTable.Rows[i]["IFGetToMoneyDesc"].ToString();
                string strBool_IFGetToMoney = myDataTable.Rows[i]["IFGetToMoney"].ToString();
                bool Bool_IFGetToMoney = false;
                bool.TryParse(strBool_IFGetToMoney, out Bool_IFGetToMoney);
                if (Bool_IFGetToMoney)
                {
                    strBool_IFGetToMoney = "是";
                }
                else
                {
                    strBool_IFGetToMoney = "否";
                }

                string strOrderTime = DateTime.Parse(strCreatDateTime).ToString("M月d日H时");




                strBody += "<li style=\"height:auto; border:none;\" ms-repeat=\"items\">\n";
                strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

                strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strBuyUserIDNickName + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strOrderTime + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + strMyWillGetMoney + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strIFGetToMoneyDesc + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strBool_IFGetToMoney + "</div>\n";
                strBody += "				</div>\n";
                strBody += "			</div>\n";
                strBody += "		</li>\n";

            }

            return strBody;
        }

    }
}