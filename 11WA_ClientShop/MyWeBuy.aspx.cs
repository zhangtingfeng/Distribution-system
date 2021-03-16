using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MyWeBuy : System.Web.UI.Page
    {
        //public String strMenuContent = "";
        //private String pstrClass1_ID = "";//三个值；
        //private String pstrClass2_ID = "";//
        //private String pstrClass3_ID = "";//

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        private int pInt_QueryString_ParentID = 0;//；

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("Time01=" + DateTime.Now);

                    setAllNeedID();



                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MyWeBuy_Templet.html");
                    strTemplet = strTemplet.Replace("###Header###", "");
                    strTemplet = strTemplet.Replace("###SAgent_AD_PathList###", SAgent_AD_PathList());
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = strTemplet.Replace("###ShowPower_ShopName###", Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()));
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = InitOpenMoney(strTemplet);
                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "个人中心"));


                    strTemplet = strTemplet.Replace("###pub_Int_Session_CurUserID###", pub_Int_Session_CurUserID.ToString());
                    strTemplet = strTemplet.Replace("###displaynone####", "none;");
                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                    #region 是否显示购物红包
                    EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                    Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                    if (Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers))
                    {
                        strTemplet = strTemplet.Replace("###DisPlayRedShoppingMoney###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###DisPlayRedShoppingMoney###", "display:none;");
                    }

                    if (Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers) && Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareGouWuQuan") == false)
                    {
                        strTemplet = strTemplet.Replace("###DisPlayShareRedShoppingMoney###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###DisPlayShareRedShoppingMoney###", "display:none;");
                    }

                    if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareXianJinHongBao") == false)
                    {
                        strTemplet = strTemplet.Replace("###DisPlayShareRedMoney###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###DisPlayShareRedMoney###", "display:none;");
                    }


                    #endregion 是否显示购物红包


                    #region 是否显示财富返还
                    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(pub_Int_ShopClientID.toInt32()))
                    {
                        strTemplet = strTemplet.Replace("###DisPlayTotalWealth_OperationUser###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###DisPlayTotalWealth_OperationUser###", "display:none;");
                    }
                    #endregion 是否显示财富返还

                    #region 是否显示普通分销的代理收入
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                    bool boolAdvanceAgent = BLL_tab_ShopClient_Agent_Level.Exists("ShopClientID=" + pub_Int_ShopClientID + " and isnull(IsDeleted,0)=0");

                    if (boolAdvanceAgent || Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(pub_Int_ShopClientID.toInt32()))
                    {
                        strTemplet = strTemplet.Replace("###DisPlayZongFenXiaoShorRu###", "display:none;");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###DisPlayZongFenXiaoShorRu###", "");
                    }
                    #endregion 是否显示普通分销的代理收入


                    #region 是否显示我的优惠券

                    if (Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers))
                    //   if (Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers) && Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareGouWuQuan") == false)
                    {
                        strTemplet = strTemplet.Replace("###DisPlayMemberCouponsShopping###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###DisPlayMemberCouponsShopping###", "display:none;");
                    }

                    string strShowCount = "";
                    Int32 Int32ShowCount = Eggsoft_Public_CL.GoodP_YouHuiQuan.GetMyCanUseCountYouHuiQuan(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    if (Int32ShowCount > 0)
                    {
                        strShowCount = Int32ShowCount + "张券可用";
                    }
                    else
                    {
                        strShowCount = "<a href=\"javascript:window.open('/addfunction/06coupons/indexlist.aspx', '_self'); \" onclick=\"\">查看可用</a>";
                    }

                    strTemplet = strTemplet.Replace("###DisPlayMemberCouponsShoppingText###", strShowCount);



                    #endregion 是否显示购物红包


                    #region 是否显示会员充值
                    EggsoftWX.BLL.tab_ShopClient_MemberCardBonus bll_tab_ShopClient_MemberCardBonus = new EggsoftWX.BLL.tab_ShopClient_MemberCardBonus();
                    int intCountMemberCardBonus = bll_tab_ShopClient_MemberCardBonus.ExistsCount("ShopClientID=" + pub_Int_ShopClientID + " and IsDeleted=0");
                    if (intCountMemberCardBonus > 0)
                    {
                        strTemplet = strTemplet.Replace("###DisPlayInputMoney###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###DisPlayInputMoney###", "display:none;");
                    }
                    #endregion 是否显示会员充值

                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                    #region 传递安全码
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("ID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID);
                    if (Model_tab_User == null) {
                        Eggsoft.Common.debug_Log.Call_WriteLog("ID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID, "传递安全码", "程序报错");
                        Response.Write("传递安全码程序报错看日志 可能用户不存在");
                        Response.End();
                    }
                    strTemplet = strTemplet.Replace("###NetUserSafeCode###", Eggsoft.Common.DESCrypt.hex_md5_2(Model_tab_User.SafeCode));
                    #endregion 传递安全码


                    #region  显示 购物车等数字
                    #region  显示 我的订单
                    EggsoftWX.BLL.tab_Order my_tab_Order = new EggsoftWX.BLL.tab_Order();
                    int intInfoCount_MySuccessOrder = my_tab_Order.ExistsCount("UserID=" + pub_Int_Session_CurUserID + " and PayStatus=1 and isReceipt=1  and isdeleted<>1");
                    strTemplet = strTemplet.Replace("###InfoCount_MySuccessOrder###", "(" + intInfoCount_MySuccessOrder.ToString() + ")");
                    #endregion  显示 我的订单
                    #region  显示 待付款
                    int intInfoCount_WaitGiveMoney = my_tab_Order.ExistsCount("UserID=" + pub_Int_Session_CurUserID + " and PayStatus=0  and isdeleted<>1");
                    strTemplet = strTemplet.Replace("###InfoCount_WaitGiveMoney###", "(" + intInfoCount_WaitGiveMoney.ToString() + ")");
                    #endregion  显示 待付款

                    #region  显示 待收货
                    int intInfoCount_WaitRicieveMoney = my_tab_Order.ExistsCount("UserID=" + pub_Int_Session_CurUserID + " and PayStatus=1 and isReceipt=0  and isdeleted<>1");
                    strTemplet = strTemplet.Replace("###InfoCount_WaitRicieveMoney###", "(" + intInfoCount_WaitRicieveMoney.ToString() + ")");
                    #endregion  显示 待收货

                    #region  显示 我的评论
                    string strView_SalesGoods = "";
                    strView_SalesGoods += " SELECT   count(1)";
                    strView_SalesGoods += " FROM      tab_ShopClient_Agent_ with(nolock) RIGHT OUTER JOIN ";
                    strView_SalesGoods += "  tab_ShopClient_Agent__ProductClassID with(nolock) ON ";
                    strView_SalesGoods += "   tab_ShopClient_Agent_.UserID = tab_ShopClient_Agent__ProductClassID.UserID RIGHT OUTER JOIN ";
                    strView_SalesGoods += "  tab_Order with(nolock) INNER JOIN ";
                    strView_SalesGoods += "  tab_Orderdetails ON tab_Order.ID = tab_Orderdetails.OrderID INNER JOIN ";
                    strView_SalesGoods += "  tab_Goods with(nolock) ON tab_Orderdetails.GoodID = tab_Goods.ID INNER JOIN ";
                    strView_SalesGoods += " tab_User with(nolock) ON tab_Order.UserID = tab_User.ID ON ";
                    strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.UserID = tab_Order.UserID AND ";
                    strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.ProductID = tab_Orderdetails.GoodID ";
                    string strwhere = "tab_ShopClient_Agent_.UserID=" + pub_Int_Session_CurUserID + " and PayStatus=1 ";
                    strView_SalesGoods += " WHERE " + strwhere + " and (tab_Order.PayStatus = 1) AND (tab_Goods.IsDeleted = 0) AND (tab_Orderdetails.isdeleted = 0)";///AND (tab_Goods.IsDeleted = 0)
                    System.Data.DataTable Data_DataTable = my_tab_Order.SelectList(strView_SalesGoods).Tables[0];


                    int intInfoCount_comments = Data_DataTable.Rows[0][0].toInt32();// BLLView_SalesGoods.ExistsCount("UserID=" + pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###InfoCount_comments###", "(" + intInfoCount_comments.ToString() + ")");
                    #endregion  显示 我的评论

                    #region  显示 浏览
                    string strView_WatchGoods = "";
                    strView_WatchGoods = @"
                    

                    SELECT  count(1) 
FROM      tab_Goods RIGHT OUTER JOIN
              (SELECT   MAX4444.ID, tab_User_Good_History.UserID, tab_User_Good_History.GoodID, 
                tab_User_Good_History.Parent_UserID, tab_User_Good_History.UpdateTime, 
                tab_User_Good_History.Count_Visit, tab_User_Good_History.GrandParentID, 
                tab_User_Good_History.GreatParentID, tab_User_Good_History.Type_Visit, 
                tab_User_Good_History.OperationID, tab_User_Good_History.CreatTime
FROM     (SELECT   MAX(ID) AS ID
FROM      tab_User_Good_History
WHERE   (UserID = " + pub_Int_Session_CurUserID + @")
GROUP BY GoodID) MAX4444 LEFT OUTER JOIN
                tab_User_Good_History ON MAX4444.ID = tab_User_Good_History.ID)  view_User_Good_History ON tab_Goods.ID = view_User_Good_History.GoodID
where view_User_Good_History.userID=" + pub_Int_Session_CurUserID + @"  and tab_Goods.ShopClient_ID=" + pub_Int_ShopClientID + @" 
";
                    System.Data.DataTable myHistoryDataTable = my_tab_Order.SelectList(strView_WatchGoods).Tables[0];

                    int intInfoCount_historygoodslist = myHistoryDataTable.Rows[0][0].toInt32();// BLLView_SalesGoods.ExistsCount("UserID=" + pub_Int_Session_CurUserID);




                    strTemplet = strTemplet.Replace("###InfoCount_historygoodslist###", "(" + intInfoCount_historygoodslist.ToString() + ")");
                    #endregion  显示 浏览

                    #region  显示 购物车
                    EggsoftWX.BLL.tab_Order_ShopingCart my_BLL_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                    int intInfoCount_Cart = my_BLL_tab_Order_ShopingCart.SelectList("select sum([GoodIDCount]) from [tab_Order_ShopingCart] where UserID = " + pub_Int_Session_CurUserID + " and IsDeleted <> 1").Tables[0].Rows[0][0].toInt32();

                    //  .ExistsCount("UserID=" + pub_Int_Session_CurUserID + " and IsDeleted<>1");
                    strTemplet = strTemplet.Replace("###InfoCount_Cart###", "(" + intInfoCount_Cart.ToString() + ")");
                    #endregion  显示 我的评论购物车

                    #endregion  显示 购物车等数字

                    Response.Write(strTemplet);
                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);
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
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);



        }


        private String SAgent_AD_PathList()
        {
            string strSAgent_AD_PathList = "";
            ///如果是省代  会出现下面的各种收入选择
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID + " and Empowered=1  and IsDeleted=0 ");
            if (Model_tab_ShopClient_Agent_ != null)
            {
                int intAgentLevelSelect = Model_tab_ShopClient_Agent_.AgentLevelSelect.toInt32();
                if (intAgentLevelSelect > 0)
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                    DataSet myds = null;
                    myds = BLL_tab_ShopClient_Agent_Level.GetList("ID,AgentLevelName", " ShopClientID=" + pub_Int_ShopClientID + " order by Sort asc,id asc");
                    int intPos = 0;
                    int[] myLevelIDList = new int[myds.Tables[0].Rows.Count];
                    for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                    {
                        string strID = myds.Tables[0].Rows[i]["ID"].ToString();

                        if (intAgentLevelSelect == Int32.Parse(strID)) intPos = i;
                        myLevelIDList[i] = Int32.Parse(strID);
                    }
                    for (int i = intPos + 1; i < (myds.Tables[0].Rows.Count); i++)
                    {
                        string strID = myds.Tables[0].Rows[i]["ID"].ToString();
                        string strAgentLevelName = myds.Tables[0].Rows[i]["AgentLevelName"].ToString();


                        int intCount = BLL_tab_ShopClient_Agent_.ExistsCount("ParentID=" + pub_Int_Session_CurUserID + " and Empowered=1  and AgentLevelSelect=" + strID);


                        strSAgent_AD_PathList += " <tr class=\"DivLineTR\" onclick=\"window.open('###SAgentPath###/multibutton_agentad.aspx?mysonlevelid=" + strID + "', '_self');\">\n";
                        strSAgent_AD_PathList += " <td align=\"center\" width=\"18%\">\n";
                        strSAgent_AD_PathList += "      <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                        strSAgent_AD_PathList += "  </td>\n";
                        strSAgent_AD_PathList += "   <td align=\"left\" width=\"33%\">\n";
                        strSAgent_AD_PathList += "        <span class=\"shouyi\">我的" + strAgentLevelName + "(" + intCount + "个)</span>\n";
                        strSAgent_AD_PathList += "    </td>\n";
                        strSAgent_AD_PathList += "   <td align=\"left\" width=\"35%\">\n";
                        strSAgent_AD_PathList += "        <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                        strSAgent_AD_PathList += "    </td>\n";
                        strSAgent_AD_PathList += "</tr>\n";
                    }
                }

                #region 显示代理证书
                strSAgent_AD_PathList += " <tr class=\"DivLineTR\" onclick=\"window.open('###SAgentPath###/showagentbookmark.aspx', '_self');\">\n";
                strSAgent_AD_PathList += " <td align=\"center\" width=\"18%\">\n";
                strSAgent_AD_PathList += "      <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/09.jpg\">\n";
                strSAgent_AD_PathList += "  </td>\n";
                strSAgent_AD_PathList += "   <td align=\"left\" width=\"33%\">\n";
                strSAgent_AD_PathList += "        <span class=\"shouyi\">我的代理证书</span>\n";
                strSAgent_AD_PathList += "    </td>\n";
                strSAgent_AD_PathList += "   <td align=\"left\" width=\"35%\">\n";
                strSAgent_AD_PathList += "        <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                strSAgent_AD_PathList += "    </td>\n";
                strSAgent_AD_PathList += "</tr>\n";
                #endregion 显示代理证书

                #region 是否显示运营中心收入及 本运营中心订单
                string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
                String strBody = "";
                EggsoftWX.BLL.b002_OperationCenter BLLb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                Boolean BooleanIFYunyinZhongXIN = BLLb002_OperationCenter.Exists("ShopClient_ID=@ShopClient_ID and UserID=@UserID and IsDeleted=0", strShopClientID.toInt32(), pub_Int_Session_CurUserID);
                if (BooleanIFYunyinZhongXIN)
                {
                    strSAgent_AD_PathList += " <tr class=\"DivLineTR\" onclick=\"window.open('###SAgentPath###/multibutton_operatecenter_order.aspx', '_self');\">\n";
                    strSAgent_AD_PathList += " <td align=\"center\" width=\"18%\">\n";
                    strSAgent_AD_PathList += "      <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/13.jpg\">\n";
                    strSAgent_AD_PathList += "  </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"33%\">\n";
                    strSAgent_AD_PathList += "        <span class=\"shouyi\">立即录单</span>\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"35%\">\n";
                    strSAgent_AD_PathList += "        <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "</tr>\n";


                    strSAgent_AD_PathList += " <tr class=\"DivLineTR\" onclick=\"window.open('###SAgentPath###/multibutton_showyunyinzhongxinorderdata.aspx', '_self');\">\n";
                    strSAgent_AD_PathList += " <td align=\"center\" width=\"18%\">\n";
                    strSAgent_AD_PathList += "      <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/11.jpg\">\n";
                    strSAgent_AD_PathList += "  </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"33%\">\n";
                    strSAgent_AD_PathList += "        <span class=\"shouyi\">我的运营中心订单</span>\n";
                    strSAgent_AD_PathList += " <div class=\"ShowFloatText\" style=\"display: none;\">\n";
                    strSAgent_AD_PathList += "     <div class=\"ShowFloatTextChild\" style=\"left:-12px;\">\n";
                    strSAgent_AD_PathList += "         <span id=\"Info_myYunYingOrder\" class=\"ShowFloatTextChildGouWuCheNumShow\">0</span>\n";
                    strSAgent_AD_PathList += "     </div>\n";
                    strSAgent_AD_PathList += " </div>\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"35%\">\n";
                    strSAgent_AD_PathList += "        <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "</tr>\n";


                    strSAgent_AD_PathList += " <tr class=\"DivLineTR\" onclick=\"window.open('###SAgentPath###/multibutton_showyunyinzhongxintotalcreditsdata.aspx', '_self');\">\n";
                    strSAgent_AD_PathList += " <td align=\"center\" width=\"18%\">\n";
                    strSAgent_AD_PathList += "      <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/12.jpg\">\n";
                    strSAgent_AD_PathList += "  </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"33%\">\n";
                    strSAgent_AD_PathList += "        <span class=\"shouyi\">我的运营中心余额</span>\n";
                    strSAgent_AD_PathList += "        <div class=\"ShowFloatText\" style=\"display: none;\">\n";
                    strSAgent_AD_PathList += "           <div class=\"ShowFloatTextChild\" style=\"left:-12px;\">\n";
                    strSAgent_AD_PathList += "           <span id=\"Info_myYunYingMoney\" class=\"ShowFloatTextChildGouWuCheNumShow\">0</span>\n";
                    strSAgent_AD_PathList += "         </div>\n";
                    strSAgent_AD_PathList += " </div>\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"35%\">\n";
                    strSAgent_AD_PathList += "        <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "</tr>\n";


                    strSAgent_AD_PathList += " <tr class=\"DivLineTR\" onclick=\"window.open('###SAgentPath###/multibutton_yunyinzhongxinallmember.aspx', '_self');\">\n";
                    strSAgent_AD_PathList += " <td align=\"center\" width=\"18%\">\n";
                    strSAgent_AD_PathList += "      <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                    strSAgent_AD_PathList += "  </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"33%\">\n";
                    strSAgent_AD_PathList += "        <span class=\"shouyi\">运营中心所有会员</span>\n";
                    strSAgent_AD_PathList += "      <div class=\"ShowFloatText\" style=\"display: none;\">\n";
                    strSAgent_AD_PathList += "        <div class=\"ShowFloatTextChild\" style=\"left:-12px;\">\n";
                    strSAgent_AD_PathList += "         <span id=\"Info_myYunYingAllMember\" class=\"ShowFloatTextChildGouWuCheNumShow\">0</span>\n";
                    strSAgent_AD_PathList += "        </div>\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "   <td align=\"left\" width=\"35%\">\n";
                    strSAgent_AD_PathList += "        <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                    strSAgent_AD_PathList += "    </td>\n";
                    strSAgent_AD_PathList += "</tr>\n";
                }
                #endregion 是否显示运营中心收入及 本运营中心订单

            }
            else
            {
                #region 我要代理，立即申请开店
                strSAgent_AD_PathList += " <tr class=\"DivLineTR\" onclick=\"window.open('###SAgentPath###/edityourshopini.aspx', '_self');\">\n";
                strSAgent_AD_PathList += " <td align=\"center\" width=\"18%\">\n";
                strSAgent_AD_PathList += "      <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/09.jpg\">\n";
                strSAgent_AD_PathList += "  </td>\n";
                strSAgent_AD_PathList += "   <td align=\"left\" width=\"33%\">\n";
                strSAgent_AD_PathList += "        <span class=\"shouyi\">申请代理</span>\n";
                strSAgent_AD_PathList += "    </td>\n";
                strSAgent_AD_PathList += "   <td align=\"left\" width=\"35%\">\n";
                strSAgent_AD_PathList += "        <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                strSAgent_AD_PathList += "    </td>\n";
                strSAgent_AD_PathList += "</tr>\n";
                #endregion 显示代理证书
            }

            return strSAgent_AD_PathList;
        }
        private String InitOpenMoney(string strargBody)
        {
            try
            {

                strargBody = strargBody.Replace("###HeadImageURL###", Eggsoft_Public_CL.Pub.Get_HeadImage(pub_Int_Session_CurUserID.ToString()));
                strargBody = strargBody.Replace("###userIDHeadImageURL###", Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(pub_Int_Session_CurUserID));


                strargBody = strargBody.Replace("###NickName###", Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()));


                Decimal myCountMoney = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(pub_Int_Session_CurUserID, out myCountMoney);
                strargBody = strargBody.Replace("###ZhangHuYuE###", Eggsoft_Public_CL.Pub.getBankPubMoney(myCountMoney));


                ///从 0 到 255 的整型数据。存储大小为 1 字节。  消费类型或者收入类型。1-100 表示 收入类型（1表示充值收入 10表示分销收入，11表示团队收入，12表示真情收入13组团收入 20表示下级访问商品收入，21购买赠送，22游戏赠送，30表示平台增加商家调节收入 40表示商品访问收入 41表示扫描代理赠送42关注赠送43首次访问赠送44签到赠送 50表示咨询访问收入 60表示签到收入 70表示红包收入 71红包退回72取消提现 80表示清除购物车81自动兑现现金 90表示待支付订单取消 91已支付取消92市场调查推广费发放）     101-200表示消费类型。（110表示购物车消耗 120表示红包消耗  130提现 140平台减少）
                //EggsoftWX.BLL.View_ShopClient_Agent_SalesOrderBy BLL_View_ShopClient_Agent_SalesOrderBy = new EggsoftWX.BLL.View_ShopClient_Agent_SalesOrderBy();
                //EggsoftWX.Model.View_ShopClient_Agent_SalesOrderBy Model_View_ShopClient_Agent_SalesOrderBy = BLL_View_ShopClient_Agent_SalesOrderBy.GetModel("userid=" + pub_Int_Session_CurUserID);
                //Decimal myAllFenXiaoMoney = 0;
                //if (Model_View_ShopClient_Agent_SalesOrderBy != null)
                //{
                //    myAllFenXiaoMoney = Convert.ToDecimal(Model_View_ShopClient_Agent_SalesOrderBy.AllFenXiaoMoney);
                //}
                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                Decimal myAllFenXiaoMoney = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList("select sum(ConsumeOrRechargeMoney) from tab_TotalCredits_Consume_Or_Recharge where UserID=@UserID and ShopClient_ID=@ShopClient_ID and (ConsumeOrRechargeType=10 or ConsumeOrRechargeType=12) and Bool_ConsumeOrRecharge=1", pub_Int_Session_CurUserID, pub_Int_ShopClientID).Tables[0].Rows[0][0].toDecimal();
                strargBody = strargBody.Replace("###ZongFenXiaoShorRu###", Eggsoft_Public_CL.Pub.getPubMoney(myAllFenXiaoMoney));


                Decimal myCountMoney_Vouchers = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(pub_Int_Session_CurUserID, out myCountMoney_Vouchers);
                strargBody = strargBody.Replace("###GouWuHongBao###", Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers));


                Decimal myCountTotalWealth_ = 0;
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(pub_Int_Session_CurUserID, out myCountTotalWealth_);
                strargBody = strargBody.Replace("###TotalWealth###", Eggsoft_Public_CL.Pub.getPubMoney(myCountTotalWealth_));

            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "MyWeBuy.aspx");
            }
            return strargBody;
        }

    }
}