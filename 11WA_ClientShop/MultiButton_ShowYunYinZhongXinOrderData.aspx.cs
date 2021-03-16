using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_ShowYunYinZhongXinOrderData : System.Web.UI.Page
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
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("UserID=" + pub_Int_Session_CurUserID + " and RunningState=1 and IsDeleted=0");
                    if (Model_b002_OperationCenter != null && Model_b002_OperationCenter.RunningState.toBoolean())
                    {
                        ///继续访问
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.TipAndRedirect("权限不足", "/mywebuy.aspx", 2);

                        Response.End();
                    }
                    #endregion 检查访问权限

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_ShowYunYinZhongXinOrderData.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = InitOpenShopAsk(strTemplet);


                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "微店首页"));

                    strTemplet = strTemplet.Replace("###Header###", "");




                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    //strHeader888 = strHeader888.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());
                    #region 注销我的运营中心订单未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_myYunYingOrder' and Readed=0", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    #endregion 注销我的运营中心订单未读消息

                    Response.Write(strTemplet);
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "前端运营中心订单");
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
            strTemplet = strTemplet.Replace("###YunYinZhongXinOrder_list###", strBody);
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
                strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>顾客</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>支付时间</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>金额</strong>¥" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>发货</strong>¥" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>转化</strong>" + "</div>\n";
                strBody += "				</div>\n";
                strBody += "			</div>\n";
                strBody += "		</li>\n";

                string strSQL = @"
SELECT top 1000 tab_Orderdetails.ID,
 tab_Orderdetails.OrderID,
 tab_Orderdetails.GoodID,
 tab_Orderdetails.Discount,
 tab_Orderdetails.GoodName,
 tab_Orderdetails.GoodPrice,
 tab_Orderdetails.CreatDateTime,
 tab_Orderdetails.OrderCount,
 tab_Orderdetails.Pinglun,
 tab_Orderdetails.ParentID,
 tab_Orderdetails.GrandParentID,
 tab_Orderdetails.GreatParentID,
 tab_Orderdetails.Over7DaysToBeans,
 tab_Orderdetails.VouchersNum_List,
 tab_Orderdetails.Beans,
 tab_Orderdetails.MoneyCredits,
 tab_Orderdetails.MoneyWeBuy8Credits,
 tab_Orderdetails.isdeleted,
 tab_Orderdetails.Freight,
 tab_Orderdetails.FreightShowText,
 tab_Orderdetails.ModifyPriceUpdateDateTime,
 tab_Orderdetails.UpdateDateTime,
 tab_Orderdetails.CreatTime,
 tab_Orderdetails.ShopClient_ID,
 tab_Orderdetails.GoodType,
 tab_Orderdetails.GoodTypeId,
 tab_Orderdetails.GoodTypeIdBuyInfo,
 tab_Order.PayStatus,
 DeliveryBOOLEAN= CASE  isnull(tab_Order.DeliveryText,'') WHEN '' then 'false' else 'true' end,
 tab_User.ID as UserID,
 tab_User.ShopUserID,
 tab_User.NickName,
 tab_User.UserRealName,
 tab_User_1.NickName AS ParentNickName,
 tab_User_2.NickName AS GrandParentIDNickName,
 tab_User_1.ShopUserID AS ParentShopUserID,
 tab_User_2.ShopUserID AS GrandParentShopUserID,
 tab_User_1.UserRealName AS ParentUserRealName,
 tab_User_2.UserRealName AS GrandParentUserRealName,
 V5.ParentMasterName,
 V5.ParentCenterUserID,
 V5.GrandParentCenterUserID,
 V5.GrandParentMasterName,
 tab_Order.PayDateTime,tab_Order.TotalMoney 
FROM (SELECT b002_OperationCenter.ID,
 b002_OperationCenter.MasterName AS ParentMasterName,
 b002_OperationCenter.ShopClient_ID,
 b002_OperationCenter.UserID AS ParentCenterUserID,
 b002_OperationCenter_1.UserID AS GrandParentCenterUserID,
 b002_OperationCenter_1.MasterName AS GrandParentMasterName
 FROM b002_OperationCenter
 LEFT OUTER JOIN b002_OperationCenter AS b002_OperationCenter_1
 ON b002_OperationCenter.ShopClient_ID = b002_OperationCenter_1.ShopClient_ID
 AND b002_OperationCenter.ParentID = b002_OperationCenter_1.ID) AS V5
 RIGHT OUTER JOIN tab_Orderdetails
 ON V5.ShopClient_ID = tab_Orderdetails.ShopClient_ID
 AND V5.ID = tab_Orderdetails.GoodTypeId
 LEFT OUTER JOIN tab_User AS tab_User_2
 ON tab_Orderdetails.GrandParentID = tab_User_2.ID
 LEFT OUTER JOIN tab_User AS tab_User_1
 ON tab_Orderdetails.ParentID = tab_User_1.ID
 LEFT OUTER JOIN tab_User
 RIGHT OUTER JOIN tab_Order
 ON tab_User.ID = tab_Order.UserID
 ON tab_Orderdetails.ShopClient_ID = tab_Order.ShopClient_ID
 AND tab_Orderdetails.OrderID = tab_Order.ID
WHERE (tab_Orderdetails.GoodType = 6 ) 
and ( tab_Orderdetails.GoodTypeID = " + Modelb002_OperationCenter.ID + @" )
 AND (tab_Orderdetails.ShopClient_ID = " + strShopClientID + @" )
 AND 
                (tab_Orderdetails.isdeleted = 0) AND (tab_Order.IsDeleted = 0)  AND (tab_Order.PayStatus = 1) ";
                strSQL += " order by id desc";

                //and ( tab_Orderdetails.GoodTypeID = " + Modelb002_OperationCenter.ID + @" )



                System.Data.DataTable myDataTable = BLLb002_OperationCenter.SelectList(strSQL).Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {

                    string strNickname = myDataTable.Rows[i]["Nickname"].ToString();
                    string strPayDateTime = myDataTable.Rows[i]["PayDateTime"].ToString();
                    string strTotalMoney = myDataTable.Rows[i]["TotalMoney"].ToString();
                    string strDeliveryBOOLEAN = myDataTable.Rows[i]["DeliveryBOOLEAN"].ToString();
                    string strOver7DaysToBeans = myDataTable.Rows[i]["Over7DaysToBeans"].ToString();

                    strTotalMoney = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strTotalMoney));

                    string strShowstrPayDateTime = "";
                    if (String.IsNullOrEmpty(strPayDateTime))
                    {
                        strShowstrPayDateTime = "未支付";
                    }
                    else
                    {
                        strShowstrPayDateTime = Convert.ToDateTime(strPayDateTime.toDateTime()).ToString("M月d日H时");
                    }



                    strBody += "<li ms-repeat=\"items\">\n";
                    strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

                    strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strNickname + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strShowstrPayDateTime + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + strTotalMoney + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + (strDeliveryBOOLEAN.toBoolean() ? "已发" : "未发") + "</div>\n";
                    strBody += "				</div>\n";
                    strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                    strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + (strOver7DaysToBeans.toBoolean() ? "已转化" : "未转化") + "</div>\n";
                    strBody += "				</div>\n";

                    #region 下单用户信息提示
                    string strShopUserID = myDataTable.Rows[i]["ShopUserID"].ToString();
                    string strParentNickName = myDataTable.Rows[i]["ParentNickName"].ToString();
                    string strGrandParentNickName = myDataTable.Rows[i]["GrandParentIDNickName"].ToString();
                    string strParentShopUserID = myDataTable.Rows[i]["ParentShopUserID"].ToString();
                    string strGrandParentShopUserID = myDataTable.Rows[i]["GrandParentShopUserID"].ToString();
                    string strUserRealName = myDataTable.Rows[i]["UserRealName"].ToString();
                    string strParentUserRealName = myDataTable.Rows[i]["ParentUserRealName"].ToString();
                    string strGrandParentUserRealName = myDataTable.Rows[i]["GrandParentUserRealName"].ToString();

                    #region 检查是否已经下过单
                    Boolean BooleanIFHaveOrder = true;


                    string strUserID = myDataTable.Rows[i]["UserID"].ToString();

                    string strSQLOutDays = @"SELECT   tab_Order.PayStatus, tab_Order.UserID, tab_Orderdetails.Over7DaysToBeans, 
                tab_Orderdetails.isdeleted, tab_Orderdetails.ShopClient_ID, tab_Orderdetails.GoodType, 
                tab_Orderdetails.GoodTypeId, tab_Orderdetails.GoodTypeIdBuyInfo, tab_Orderdetails.ID, 
                tab_Orderdetails.CreatTime
FROM      tab_Order RIGHT OUTER JOIN
                tab_Orderdetails ON tab_Order.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND 
                tab_Order.ID = tab_Orderdetails.OrderID
                WHERE (tab_Orderdetails.ShopClient_ID = 21) 
                and (tab_Orderdetails.Over7DaysToBeans  = 1) 
                AND (tab_Order.UserID = " + strUserID + @")
                AND (tab_Orderdetails.GoodType = '6')
                ";
                    System.Data.DataTable DataDataTableSQLIn7Days = BLLb002_OperationCenter.SelectList(strSQLOutDays).Tables[0];
                    if (DataDataTableSQLIn7Days.Rows.Count > 0)
                    {
                        BooleanIFHaveOrder = false;///只能申请首次下单的客户，当前用户已经下过单
                    }
                    #endregion 检查是否已经下过单


                    strBody += "				<div class=\"Parent_TipInfo\">\n";
                    if (strShopUserID.toInt32() > 0)
                    {
                        if (BooleanIFHaveOrder)
                        {
                            strBody += "<span style=\"color:blue;\" >";
                        }
                        strBody += "下单用户ID:" + strShopUserID + ",\n";
                        strBody += " 昵称:" + strNickname + "<br />";
                        if (BooleanIFHaveOrder)
                        {
                            strBody += "</span>";
                        }
                        //strBody += " 姓名(" + strUserRealName + ")<br/>";
                    }
                    if (strParentShopUserID.toInt32() > 0)
                    {

                        strBody += " 上级ID:" + strParentShopUserID + ",\n";
                        strBody += " 昵称:" + strParentNickName + "<br />";

                        //strBody += " 上级姓名(" + strParentUserRealName + ")<br/>";
                    }
                    if (strGrandParentShopUserID.toInt32() > 0)
                    {
                        strBody += " 上上级ID:" + strGrandParentShopUserID + ",\n";
                        strBody += " 昵称:" + strGrandParentNickName + "<br />";
                        //strBody += " 上上级姓名(" + strGrandParentUserRealName + ")";
                    }
                    strBody += "				</div>\n";
                    #endregion 下单用户信息提示

                    strBody += "			</div>\n";
                    strBody += "		</li>\n";


                    #region

                    #endregion
                }

            }

            //myArgMoney = CountmyArgMoney;
            return strBody;
        }


    }
}