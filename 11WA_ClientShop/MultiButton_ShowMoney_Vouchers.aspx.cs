using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_ShowMoney_Vouchers : System.Web.UI.Page
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
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_ShowMoney_Vouchers_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = strTemplet.Replace("###ShowPower_ShopName###", Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()));
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//
                    strTemplet = InitOpenShopAsk(strTemplet);



                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "余额详情"));

                    strTemplet = strTemplet.Replace("###Header###", "");

                    string strCloseCheckBox_ShareGouWuQuan = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareGouWuQuan") ? "style=\"display:none; \"" : "";
                    strTemplet = strTemplet.Replace("###CheckBox_CloseShareGouWuQuan###", strCloseCheckBox_ShareGouWuQuan);

                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));


                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                    #region 注销未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_GouWuHongBao'", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
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

            string strBody = GetMoneySList(pub_Int_Session_CurUserID);

            Decimal myCountMoney = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(pub_Int_Session_CurUserID, out myCountMoney);
            strTemplet = strTemplet.Replace("###CountMoney###", "¥" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney));

            strTemplet = strTemplet.Replace("###Money_list###", strBody);


            return strTemplet;

        }



        public static String GetMoneySList(int intArgUserID)
        {

            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();

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



            System.Data.DataTable myDataTable = BLL_tab_Total_Vouchers_Consume_Or_Recharge.GetDataTable("60", "*", "UserID=" + intArgUserID + " order by id desc");
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {

                //        ,[UserID]
                //,[UpdateTime]
                //,[ConsumeOrRechargeBean]
                //,[ConsumeTypeOrRecharge]
                //,[RemainingSumBean]
                //,[Bool_ConsumeOrRecharge]
                //,[BoolIfOnlyonceUpdate]

                string strConsumeOrRecharge_Vouchers = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["ConsumeOrRecharge_Vouchers"].ToString()));
                string strConsumeTypeOrRecharge = myDataTable.Rows[i]["ConsumeTypeOrRecharge"].ToString();
                string strRemainingSum_Vouchers = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["RemainingSum_Vouchers"].ToString()));
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
                //"yyyy年MM月HH日
                string strDate = myDataTable.Rows[i]["UpdateTime"].ToString();
                DateTime myDateTime = DateTime.Now;
                DateTime.TryParse(strDate, out myDateTime);
                string strUpdateTime = myDateTime.ToString("M月d日H时");




                strBody += "<li ms-repeat=\"items\">\n";
                strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

                strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strBool_ConsumeOrRecharge + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strConsumeTypeOrRecharge + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + strConsumeOrRecharge_Vouchers + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + strRemainingSum_Vouchers + "</div>\n";
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