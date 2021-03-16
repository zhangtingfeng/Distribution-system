using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_YunYinZhongXinAllMember : System.Web.UI.Page
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
                    #region 检查访问权限
                    EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("UserID=" + pub_Int_Session_CurUserID+ " and RunningState=1 and IsDeleted=0");
                    if (Model_b002_OperationCenter != null)
                    {
                        ///继续访问
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.TipAndRedirect("权限不足", "/mywebuy.aspx", 2);

                        Response.End();
                    }
                    #endregion 检查访问权限

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_YunYinZhongXinAllMember.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = InitOpenShopAsk(strTemplet);


                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "微店首页"));

                    strTemplet = strTemplet.Replace("###Header###", "");

                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                    #region 注销我的运营中心会员未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_myYunYingAllMember' and Readed=0", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    #endregion 注销我的运营中心会员未读消息

                    Response.Write(strTemplet);
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "前端运营中心会员");
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
            string strBody = GetYunYinZhongXinOrder_listSList(pub_Int_Session_CurUserID);


            //Decimal myCountWealth = 0;
            //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(pub_Int_Session_CurUserID, out myCountWealth);
            //strTemplet = strTemplet.Replace("###TotalOrderCredits_OperationCenter###", "" + Eggsoft_Public_CL.Pub.getPubMoney(myCountWealth));
            strTemplet = strTemplet.Replace("###YunYinZhongXinMember_list###", strBody);
            return strTemplet;
        }



        public static String GetYunYinZhongXinOrder_listSList(int intArgUserID)
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            String strBody = "";
            EggsoftWX.BLL.b002_OperationCenter BLLb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
            EggsoftWX.Model.b002_OperationCenter Modelb002_OperationCenter = BLLb002_OperationCenter.GetModel("ShopClient_ID=@ShopClient_ID and UserID=@UserID and IsDeleted=0", strShopClientID.toInt32(), intArgUserID);
            if (Modelb002_OperationCenter == null)
            {
                strBody = "";
            }
            else
            {
                strBody += "<li ms-repeat=\"items\">\n";
                strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";
                strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>会员ID</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>会员昵称</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>上级ID</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>上级昵称</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_25_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\" style=\"line-height:15px;\"><strong>订单统计<br />(活动数/已生效/已支付)</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "			</div>\n";
                strBody += "</li>\n";

                string strSQL = "";
               

                strSQL = @"SELECT tab_User.NickName,tab_User.ShopUserID,
 b005_UserID_Operation_ID.ID,
 b005_UserID_Operation_ID.UserID,
 b005_UserID_Operation_ID.ShopClientID,
 b005_UserID_Operation_ID.OperationCenterID,
 b005_UserID_Operation_ID.OperationCenterID_UserID,
 b005_UserID_Operation_ID.UserParentID,
 b005_UserID_Operation_ID.CreateBy,
 b005_UserID_Operation_ID.UpdateTime,
 b005_UserID_Operation_ID.UpdateBy,
 b005_UserID_Operation_ID.CreatTime,
 b005_UserID_Operation_ID.IsDeleted,
 tab_User_1Parent.ShopUserID AS ParentShopUserID,
 tab_User_1Parent.NickName AS ParentNickName,
 b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum,
 V7.Over7DaysToBeansCount,
 V8.BuyAllCount
FROM b005_UserID_Operation_ID
 LEFT OUTER JOIN (SELECT UserID,
 Sum(OrderCount) AS BuyAllCount
 FROM (SELECT dbo.tab_Order.PayStatus,
 dbo.tab_Order.UserID,
 dbo.tab_Order.IsDeleted,
 dbo.tab_Orderdetails.OrderCount,
 dbo.tab_Orderdetails.Over7DaysToBeans,
 dbo.tab_Orderdetails.GoodType,
 dbo.tab_Orderdetails.GoodTypeId,
 dbo.tab_Orderdetails.GoodTypeIdBuyInfo,
 dbo.tab_Orderdetails.ShopClient_ID 
 FROM dbo.tab_Orderdetails
 LEFT OUTER JOIN dbo.tab_Order
 ON dbo.tab_Orderdetails.OrderID = dbo.tab_Order.ID
 WHERE ( dbo.tab_Orderdetails.ShopClient_ID = " + strShopClientID + @" )
 AND ( dbo.tab_Orderdetails.isdeleted = 0 )
 AND ( dbo.tab_Orderdetails.GoodType = 6 )
 AND ( dbo.tab_Orderdetails.GoodTypeId = " + Modelb002_OperationCenter.ID + @" )
 AND ( dbo.tab_Order.IsDeleted = 0 )
 AND ( dbo.tab_Order.PayStatus = 1 )
 AND ( dbo.tab_Orderdetails.Over7DaysToBeans = 1 )) AS v6
 GROUP BY UserID) V8
 ON b005_UserID_Operation_ID.UserID = V8.UserID
 LEFT OUTER JOIN (SELECT UserID,
 Sum(OrderCount) AS Over7DaysToBeansCount
 FROM (SELECT dbo.tab_Order.PayStatus,
 dbo.tab_Order.UserID,
 dbo.tab_Order.IsDeleted,
 dbo.tab_Orderdetails.OrderCount,
 dbo.tab_Orderdetails.Over7DaysToBeans,
 dbo.tab_Orderdetails.GoodType,
 dbo.tab_Orderdetails.GoodTypeId,
 dbo.tab_Orderdetails.GoodTypeIdBuyInfo,
 dbo.tab_Orderdetails.ShopClient_ID 
 FROM dbo.tab_Orderdetails
 LEFT OUTER JOIN dbo.tab_Order
 ON dbo.tab_Orderdetails.OrderID = dbo.tab_Order.ID
 WHERE ( dbo.tab_Orderdetails.ShopClient_ID = " + strShopClientID + @" )
 AND ( dbo.tab_Orderdetails.isdeleted = 0 )
 AND ( dbo.tab_Orderdetails.GoodType = 6 )
 AND ( dbo.tab_Orderdetails.GoodTypeId = " + Modelb002_OperationCenter.ID + @" )
 AND ( dbo.tab_Order.IsDeleted = 0 )
 AND ( dbo.tab_Order.PayStatus = 1 )
 AND ( dbo.tab_Orderdetails.Over7DaysToBeans = 1 )) AS v6
 GROUP BY UserID) V7
 ON b005_UserID_Operation_ID.UserID = V7.UserID
 LEFT OUTER JOIN tab_User AS tab_User_1Parent
 ON b005_UserID_Operation_ID.UserParentID = tab_User_1Parent.ID
 LEFT OUTER JOIN tab_User
 ON b005_UserID_Operation_ID.UserID = tab_User.ID
 LEFT OUTER JOIN b008_OpterationUserActiveReturnMoneyOrderNum
 ON b005_UserID_Operation_ID.UserID = b008_OpterationUserActiveReturnMoneyOrderNum.UserID
WHERE ( b005_UserID_Operation_ID.ShopClientID = " + strShopClientID + @" )
 AND ( b005_UserID_Operation_ID.OperationCenterID_UserID = " + intArgUserID + @" )
ORDER BY b005_UserID_Operation_ID.id DESC
";

                //and ( tab_Orderdetails.GoodTypeID = " + Modelb002_OperationCenter.ID + @" )



                System.Data.DataTable myDataTable = BLLb002_OperationCenter.SelectList(strSQL).Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {

                    string strShopUserID = myDataTable.Rows[i]["ShopUserID"].ToString();
                    string strNickname = myDataTable.Rows[i]["NickName"].ToString();
                    string strParentShopUserID = myDataTable.Rows[i]["ParentShopUserID"].ToString();
                    string strParentNickName = myDataTable.Rows[i]["ParentNickName"].ToString();

                    string strActiveOrderNum = myDataTable.Rows[i]["ActiveOrderNum"].ToString();
                    string strOver7DaysToBeansCount = myDataTable.Rows[i]["Over7DaysToBeansCount"].ToString();
                    string strBuyAllCount = myDataTable.Rows[i]["BuyAllCount"].ToString();



                    strBody += "<li ms-repeat=\"items\">\n";
                    strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\" style=\"clear:both\">\n";

                    strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strShopUserID + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strNickname + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strParentShopUserID + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Beans_18_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strParentNickName + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Beans_25_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strActiveOrderNum.toInt32() + "/" + strOver7DaysToBeansCount.toInt32() + "/" + strBuyAllCount.toInt32() + "</div>\n";
                    strBody += "				</div>\n";

                  

                    strBody += "			</div>\n";
                    strBody += "		</li>\n";


                  
                }

            }

            //myArgMoney = CountmyArgMoney;
            return strBody;
        }


    }
}