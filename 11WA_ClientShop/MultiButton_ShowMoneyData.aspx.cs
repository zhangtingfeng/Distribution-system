using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_ShowMoneyData : System.Web.UI.Page
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
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_ShowMoneyData_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = InitOpenShopAsk(strTemplet);


                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "微店首页"));

                    strTemplet = strTemplet.Replace("###Header###", "");

                    string strCloseCheckBox_ShareXianJinHongBao = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareXianJinHongBao") ? "style=\"display:none; \"" : "";
                    strTemplet = strTemplet.Replace("###CheckBox1_CloseShareXianJinHongBao###", strCloseCheckBox_ShareXianJinHongBao);
                    
                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    //strHeader888 = strHeader888.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                    #region 注销未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_ZhangHuYuE'", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    #endregion 注销未读消息

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



            //userID = 38;
            string strBody = GetMoneySList(pub_Int_Session_CurUserID);


            Decimal myCountMoney = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(pub_Int_Session_CurUserID, out myCountMoney);
            strTemplet = strTemplet.Replace("###CountMoney###", "" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney));

            strTemplet = strTemplet.Replace("###Money_list###", strBody);


            return strTemplet;

        }



        public static String GetMoneySList(int intArgUserID)
        {

            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLLtab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Modeltab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();

            String strBody = "";
            strBody += "<li ms-repeat=\"items\">\n";
            strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

            strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>类型</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>说明</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>额度</strong>¥" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>余数</strong>¥" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>时间</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "			</div>\n";
            strBody += "		</li>\n";

            string strSQL = @"
    	select top 60 * from (select * from (SELECT top 60  ID,Bool_ConsumeOrRecharge,UpdateTime,ConsumeOrRechargeMoney,RemainingSum,ConsumeTypeOrRecharge FROM tab_TotalCredits_Consume_Or_Recharge where userid=@userid and RemainingSum is not null and [updatetime]<'2017-10-01 00:00:13.370'  order by id desc) bbbb
	  union	 
	  SELECT max(ID) ID,Bool_ConsumeOrRecharge,max([updatetime])    [updatetime] 
      ,sum(ConsumeOrRechargeMoney) ConsumeOrRechargeMoney
      ,max([RemainingSum]) RemainingSum
	  ,max(ConsumeTypeOrRecharge) ConsumeTypeOrRecharge
FROM tab_TotalCredits_Consume_Or_Recharge
where  userid=@userid and [updatetime]>'2017-10-01 00:00:13.370'  and charindex('WealthID',ConsumeTypeOrRecharge)<>0  
group by convert(varchar(10),[updatetime],120),Bool_ConsumeOrRecharge 
 union	 
	  select * from (SELECT ID,Bool_ConsumeOrRecharge,UpdateTime,ConsumeOrRechargeMoney,RemainingSum,ConsumeTypeOrRecharge FROM tab_TotalCredits_Consume_Or_Recharge where userid=@userid and RemainingSum is not null and [updatetime]>'2017-10-01 00:00:13.370' and  charindex('WealthID',ConsumeTypeOrRecharge)=0) CCCC
) ffffff order by id desc


";
            System.Data.DataTable myDataTable = BLLtab_TotalCredits_Consume_Or_Recharge.SelectList(strSQL, intArgUserID).Tables[0];


           // System.Data.DataTable myDataTable = BLLtab_TotalCredits_Consume_Or_Recharge.GetDataTable("120", "*", "UserID=" + intArgUserID + " order by id desc");
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {

                //        ,[UserID]
                //,[UpdateTime]
                //,[ConsumeOrRechargeBean]
                //,[ConsumeTypeOrRecharge]
                //,[RemainingSumBean]
                //,[Bool_ConsumeOrRecharge]
                //,[BoolIfOnlyonceUpdate]

                string strConsumeOrRechargeMoney = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["ConsumeOrRechargeMoney"].ToString()));
                string strConsumeTypeOrRecharge = myDataTable.Rows[i]["ConsumeTypeOrRecharge"].ToString();
                string strRemainingSum = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["RemainingSum"].toString()));
                string strBool_ConsumeOrRecharge = myDataTable.Rows[i]["Bool_ConsumeOrRecharge"].ToString();
                bool Bool_ConsumeOrRecharge = false;
                bool.TryParse(strBool_ConsumeOrRecharge, out Bool_ConsumeOrRecharge);
                if (Bool_ConsumeOrRecharge)
                {
                    strBool_ConsumeOrRecharge = "充值";
                }
                else
                {
                    strBool_ConsumeOrRecharge = "消费";
                }

                string strUpdateTime = DateTime.Parse(myDataTable.Rows[i]["UpdateTime"].ToString()).ToString("M月d日H时");




                strBody += "<li ms-repeat=\"items\">\n";
                strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

                strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strBool_ConsumeOrRecharge + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strConsumeTypeOrRecharge + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + strConsumeOrRechargeMoney + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + strRemainingSum + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strUpdateTime + "</div>\n";
                strBody += "				</div>\n";
                strBody += "			</div>\n";
                strBody += "		</li>\n";


                #region
                //找他的儿子
                //if (intLevel == 1)
                //{
                //    Decimal mySonemoney = 0;
                //    strBody = strBody + GetCountMySonMoney(Int32.Parse(strUserID), 2, out  mySonemoney);
                //    CountmyArgMoney+=mySonemoney;
                //}
                //else
                //{


                //}
                #endregion
            }
            //myArgMoney = CountmyArgMoney;
            return strBody;
        }


    }
}