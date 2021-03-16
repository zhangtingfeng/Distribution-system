using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class multibutton_agentAd : System.Web.UI.Page
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
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_AgentAD_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = InitOpenShopAsk(strTemplet);

                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "下级代理"));

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



            //int userID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            //userID = 38;
            // Decimal mySonemoney = 0;


            string strBody = GetMySon_AgentMoney(pub_Int_Session_CurUserID);


            //strTemplet = strTemplet.Replace("###AllMyMoney###", "" + Eggsoft_Public_CL.Pub.getPubMoney(mySonemoney));
            string strmysonlevelid = Request["mysonlevelid"];
            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLLtab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
            EggsoftWX.Model.tab_ShopClient_Agent_Level Modeltab_ShopClient_Agent_Level = BLLtab_ShopClient_Agent_Level.GetModel(Int32.Parse(strmysonlevelid));

            strTemplet = strTemplet.Replace("###Mylevelname###", Modeltab_ShopClient_Agent_Level.AgentLevelName);


            strTemplet = strTemplet.Replace("###Agents_list###", strBody);

            return strTemplet;

        }


        private String GetMySon_AgentMoney(int intArgUserID)
        {

            string strmysonlevelid = Request["mysonlevelid"];


            string strPub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intArgUserID);


            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.Model.tab_User();

            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

            EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLLtab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
            String strBody = "";
            //Decimal CountmyArgMoney = 0;
            System.Data.DataTable myDataTable = BLLtab_ShopClient_Agent_.GetList("(ParentID=" + intArgUserID + " and Empowered=1   and IsDeleted=0  and AgentLevelSelect=" + strmysonlevelid + ")  order by id asc").Tables[0];
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                //string strPartnerWeiXinID = myDataTable.Rows[i]["PartnerWeiXinID"].ToString();
                //string strRecommendWeiXinID = myDataTable.Rows[i]["RecommendWeiXinID"].ToString();
                //string strGreatParentID = myDataTable.Rows[i]["GreatParentID"].ToString();
                string strUserID = myDataTable.Rows[i]["UserID"].ToString();
                string strShopName = myDataTable.Rows[i]["ShopName"].ToString();
                string strID = myDataTable.Rows[i]["ID"].ToString();
                string strUpdateTime = myDataTable.Rows[i]["UpdateTime"].ToString();

                string strHeadIMG = Eggsoft_Public_CL.Pub.Get_HeadImage(strUserID);
                string strNickname = Eggsoft_Public_CL.Pub.GetNickName(strUserID);


                Modeltab_User = BLLtab_User.GetModel(Int32.Parse(strUserID));


                Decimal myDecimal = 0;
                //从 0 到 255 的整型数据。存储大小为 1 字节。  消费类型或者收入类型。1-100 表示 收入类型（1表示充值收入 10表示分销收入，11表示团队收入，12表示真情收入13组团收入 20表示下级访问商品收入，21购买赠送，22游戏赠送，30表示平台增加商家调节收入 40表示商品访问收入 41表示扫描代理赠送42关注赠送43首次访问赠送44签到赠送 50表示咨询访问收入 60表示签到收入 70表示红包收入 71红包退回72取消提现 80表示清除购物车81自动兑现现金 90表示待支付订单取消 91已支付取消92市场调查推广费发放）     101-200表示消费类型。（110表示购物车消耗 120表示红包消耗  130提现 140平台减少）
                //int myAgentSales = 0;
                string strSum = "select ConsumeOrRechargeMoney from tab_TotalCredits_Consume_Or_Recharge where UserID=@UserID and ConsumeOrRechargeType=10 and Bool_ConsumeOrRecharge=1";
                myDecimal = BLLtab_ShopClient_Agent_.SelectList(strSum, strUserID).Tables[0].Rows[0][0].toDecimal();
                
                //int int_Agent__ProductClassID_Count = BLLtab_ShopClient_Agent__ProductClassID.ExistsCount("UserID=" + strUserID + " and Empowered=1");
                //System.Data.DataTable myDataTable66 = BLLtab_ShopClient_Agent__ProductClassID.GetList("UserID=" + strUserID + " and Empowered=1  order by id asc").Tables[0];
                //for (int j = 0; j < myDataTable66.Rows.Count; j++)
                //{
                //    string strProductID = myDataTable66.Rows[j]["ProductID"].ToString();
                //    //string strPrice_Percent1 = myDataTable66.Rows[j]["Price_Percent1"].ToString();///存在 这个 就是我的收入
                //    ///

                //    Decimal Decimal_ProductID = 0;
                //    Decimal.TryParse(strPrice_Percent1, out Decimal_ProductID);
                //    if (Decimal_ProductID > 0)
                //    {
                //        System.Data.DataTable myDataTable77 = BLLtab_Orderdetails.GetList("ParentID=" + strUserID + " and Over7DaysToBeans=1 and isdeleted=0").Tables[0];
                //        for (int m = 0; m < myDataTable77.Rows.Count; m++)
                //        {
                //            string strGoodPrice = myDataTable77.Rows[m]["GoodPrice"].ToString();
                //            string strOrderCount = myDataTable77.Rows[m]["OrderCount"].ToString();
                //            myAgentSales += Int32.Parse(strOrderCount);
                //            Decimal DecimalGoodPrice = 0;
                //            Decimal.TryParse(strGoodPrice, out DecimalGoodPrice);
                //            myDecimal += decimal.Multiply(decimal.Multiply(decimal.Multiply(DecimalGoodPrice, Int32.Parse(strOrderCount)), Decimal_ProductID), (decimal)0.01);
                //        }
                //    }

                //}



                strBody += "<li ms-repeat=\"items\">\n";
                strBody += "			<div class=\"ul_li\">\n";
                strBody += "				<div class=\"ul_li_div_img\">\n";
                strBody += "					<div class=\"div_img_center\"><img alt=\"图片\" width=\"90px\" height=\"90px\"\n";
                strBody += "						src=\"" + strHeadIMG + "\"></div>\n";
                strBody += "					<div class=\"ul_li_div_name\">" + strNickname + "</div>\n";

                strBody += "				</div>\n";

                strBody += "				<div class=\"ul_li_trade\"><ul class=\"OliverModi\">\n";
                strBody += "					<li>代理店铺名:" + strShopName + "</li>\n";

                strBody += "						<li>联系人:" + Modeltab_User.UserRealName + "</li>\n";
                strBody += "						<li>电话:<a href=\"tel:" + Modeltab_User.ContactPhone + "\">" + Modeltab_User.ContactPhone + "</a></li>\n";
                strBody += "						<li>店铺更新时间:" + strUpdateTime + "</li>\n";
                strBody += "						<li>2018年分销收入:" + Eggsoft_Public_CL.Pub.getBankPubMoney(myDecimal) + "</li>\n";
                //strBody += "				<div class=\"ul_li_money_Percent\"><ul class=\"OliverModi\">\n";

                //strBody += "						<li>代理销售商品数:" + myAgentSales + "</li>\n"; ;

                strBody += "				</ul></div>\n";

                strBody += "			</div>\n";
                strBody += "		</li>\n";



            }
            return strBody;
        }


    }
}