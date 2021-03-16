using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_GouWuQuanChange : System.Web.UI.Page
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
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_GouWuQuanChange_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "AfterShareContinuesAskChangeMoney");//微信分享代码
                    strTemplet = InitOpenShopAsk(strTemplet);

                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "直接代理收入"));
                    strTemplet = strTemplet.Replace("###ShowPower_ShopName###", Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()));

                    strTemplet = strTemplet.Replace("###Header###", "");



                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


                    Decimal myAllSonemoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.DoOver7daysCountMySonMoney_Then_CountyuEArgMoney(pub_Int_Session_CurUserID, out myAllSonemoney);
                    strTemplet = strTemplet.Replace("###ZhangHuYuE###", Eggsoft_Public_CL.Pub.getBankPubMoney(myAllSonemoney));

                    Decimal myCountMoney_Vouchers = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(pub_Int_Session_CurUserID, out myCountMoney_Vouchers);
                    strTemplet = strTemplet.Replace("###GouWuHongBao###", Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers));



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

            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            Decimal myAllFenXiaoMoney = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList("select sum(ConsumeOrRechargeMoney) from tab_TotalCredits_Consume_Or_Recharge where UserID=@UserID and ShopClient_ID=@ShopClient_ID and (ConsumeOrRechargeType=10 or ConsumeOrRechargeType=12) and Bool_ConsumeOrRecharge=1", pub_Int_Session_CurUserID, pub_Int_ShopClientID).Tables[0].Rows[0][0].toDecimal();

            //if (Model_View_ShopClient_Agent_SalesOrderBy != null)
            //{
            //Decimal mySonemoney = Convert.ToDecimal(Model_View_ShopClient_Agent_SalesOrderBy.AllFenXiaoMoney_my_AND_myAllSon);
            strTemplet = strTemplet.Replace("###CountMoney###", "￥" + Eggsoft_Public_CL.Pub.getPubMoney(myAllFenXiaoMoney));
            //}
            //else
            //{
            //    strTemplet = strTemplet.Replace("###CountMoney###", "￥0");
            //}
            strTemplet = strTemplet.Replace("###GouWuQuanChang_list###", GetGouWuQuanChanglist());
            return strTemplet;
        }


        private String GetGouWuQuanChanglist()
        {
            EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc blltab_GouWuQuan2XianJInEtc = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();

            String strBody = "";

            System.Data.DataTable myDataTable = blltab_GouWuQuan2XianJInEtc.GetDataTable("200", "*", " and ShopClientID=" + pub_Int_ShopClientID + " and IsDeleted=0 order by id asc");
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {

                string strChangeAutoID = myDataTable.Rows[i]["ID"].ToString();

                string strUserGouWuQuan = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["UserGouWuQuan"].ToString()));
                string strChangeAuto = myDataTable.Rows[i]["ChangeAuto"].ToString();
                string strShortDesc = myDataTable.Rows[i]["ShortDesc"].ToString();
                string strChangeDestination = myDataTable.Rows[i]["ChangeDestination"].ToString();

                string strDESC = Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "￥" + strUserGouWuQuan + "元";
                strDESC += "<span style=\"color:red;font-size:20px;font-weight:bold;\">=</span>";
                if (strChangeDestination == "1")
                {
                    string strXianJinMoney = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["XianJinMoney"].ToString()));

                    strDESC += "现金￥" + strXianJinMoney + "元";
                }
                else
                {
                    strDESC += strShortDesc;
                }
                string strButtonShow = "";
                if (strChangeAuto == "Auto")
                {
                    strButtonShow += "立即兑换";
                }
                else
                {
                    strButtonShow += "申请兑换";
                }


                strBody += "<li  onclick=\"ChangeMoney(" + strChangeAutoID + ");\" style=\"height:auto;margin-top:5px;padding-top:5px;margin-bottom:5px;\" ms-repeat=\"items\">\n";
                strBody += "			<div style=\"background-color:white;display:block;height:50px;\" class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

                strBody += "				<div class=\"ul_li_Classs_75_Percent\">\n";
                strBody += "					<div style=\"font-size:14px;display:block; \" class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strDESC + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div style=\"width: 25%;\" class=\"ul_li_Beans_18_Percent\">\n";
                strBody += "					<div style=\"font-size:14px;display:block; border: 2px solid #c0c0c0;\" class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strButtonShow + "</div>\n";
                strBody += "				</div>\n";
                strBody += "			</div>\n";
                strBody += "		</li>\n";

            }

            return strBody;
        }



    }
}