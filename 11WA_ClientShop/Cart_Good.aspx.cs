using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft.Common;

namespace _11WA_ClientShop
{
    public partial class Cart_Good : System.Web.UI.Page
    {
        //    select * from [eggsoft.cn].[dbo].[tab_Order] where userid in (
        //select id=Pid from AccountsRelation where Wid=4
        //  union
        //  select id=Wid from AccountsRelation where Wid=4
        //  union
        //  select id=isnull(Yid,0) from AccountsRelation where Wid=4)
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";

        private Object thisLock = new Object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    setAllNeedID();

                    string type = Request.QueryString["type"];
                    if(string.IsNullOrEmpty(type) == false)
                    {
                        if(type == "cancelthis")
                        {
                            string strOrderINT = Request.QueryString["OrderINT"];//订单记录的ID
                            Eggsoft_Public_CL.GoodP.DeleteOrder(strOrderINT, pub_Int_Session_CurUserID.toString() + "用户删除");
                        }
                    }

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_CartGood_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "待付款"));
                    strTemplet = strTemplet.Replace("###pub_Int_ShopClientID###", pub_Int_ShopClientID.ToString());
                    strTemplet = strTemplet.Replace("###ServicesURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL());
                    strTemplet = strTemplet.Replace("###varglobalcheckPayWay###", Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "V3_js_API") ? "V3_js_API" : "Oldjs_APIPay");

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);

                    strTemplet = strTemplet.Replace("###Header###", "");


                    if(Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "PC")
                    {
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    }
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());



                    #region 注销待付款未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart_good' and Readed=0", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    #endregion 注销待付款未读消息



                    strTemplet = InitOrders(strTemplet);

                    Response.Write(strTemplet);
                }
                catch(System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch(Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }

                //InitOrders();   
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
        private string InitOrders(string strTemplet)
        {
            #region background

            try
            {
                EggsoftWX.BLL.tab_Order my_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.BLL.tab_Orderdetails my_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                EggsoftWX.BLL.tab_User_Address my_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();
                EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods BLL_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods();
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();

                System.Data.DataTable myDataTable = my_tab_Order.GetList("UserID=" + pub_Int_Session_CurUserID + " and PayStatus=0  and isdeleted<>1 order by id desc").Tables[0];

                string strOrderCartGoods = "";
                if(myDataTable.Rows.Count == 0)
                {
                    strOrderCartGoods = "&nbsp;&nbsp;暂无待付款信息！";
                }
                else
                {
                    strOrderCartGoods += "<ul id=\"thelist\" class=\"experience_list\">\n";

                    for(int inti = 0; inti < myDataTable.Rows.Count; inti++)
                    {

                        String strShopClient_ID = myDataTable.Rows[inti]["ShopClient_ID"].ToString();

                        String strOrderID = myDataTable.Rows[inti]["ID"].ToString();
                        String strOrderNum = myDataTable.Rows[inti]["OrderNum"].ToString();
                        Decimal allMoney = Decimal.Parse(myDataTable.Rows[inti]["TotalMoney"].ToString());
                        String strTotalMoney = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(allMoney);
                        String strOrderName = myDataTable.Rows[inti]["OrderName"].ToString();
                        DateTime DateTimeCreateDatetime = string.IsNullOrEmpty(myDataTable.Rows[inti]["CreateDatetime"].ToString()) ? DateTime.Now : Convert.ToDateTime(myDataTable.Rows[inti]["CreateDatetime"].ToString());



                        strOrderCartGoods += "<li  class=\"ul_li experience_items\">" + "\n";
                        strOrderCartGoods += "                  <div class=\"OrdertitleBig\">     <div class=\"Ordertitle\">" + "  订单号：" + strOrderNum + "<br />  \n";
                        strOrderCartGoods += "                  订单金额：<strong class='price'>" + strTotalMoney + "</strong></div>\n";
                        strOrderCartGoods += "     <div style=\"margin-right: 16px;\" class=\"pro_wBuy_Button_\">\n";
                        strOrderCartGoods += "             <a id=\"LinkButton_isSale\" class=\"modBtnColor modBtnColor_Red\" href=\"javascript: buyThis(" + strOrderID + ");\">支付</a>\n";
                        strOrderCartGoods += "     </div>\n";
                        strOrderCartGoods += "   </div>\n";
                        strOrderCartGoods += "    <div class=\"OrdertitleBig\">\n";


                        System.Data.DataTable myDataTable_Orderdetails = my_tab_Orderdetails.GetList("OrderID=" + strOrderID + "").Tables[0];
                        for(int intj = 0; intj < myDataTable_Orderdetails.Rows.Count; intj++)
                        {

                            String strOrderDetailID = myDataTable_Orderdetails.Rows[intj]["ID"].ToString();
                            String strGoodID = myDataTable_Orderdetails.Rows[intj]["GoodID"].ToString();

                            #region 检查上级是否改变过
                            Int32 ParentID = myDataTable_Orderdetails.Rows[intj]["ParentID"].toInt32();
                            Int32 GrandParentID = myDataTable_Orderdetails.Rows[intj]["GrandParentID"].toInt32();
                            Int32 GreatParentID = myDataTable_Orderdetails.Rows[intj]["GreatParentID"].toInt32();
                            Int32 TeamID = myDataTable_Orderdetails.Rows[intj]["TeamID"].toInt32();

                            EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(strShopClient_ID.toInt32(), pub_Int_Session_CurUserID, strGoodID.toInt32());
                            int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();
                            //Int32 NowParentID = myDataTable_Orderdetails.Rows[intj]["ParentID"].toInt32();
                            //Int32 NowGrandParentID = myDataTable_Orderdetails.Rows[intj]["GrandParentID"].toInt32();
                            //Int32 NowGreatParentID = myDataTable_Orderdetails.Rows[intj]["GreatParentID"].toInt32();
                            //Int32 NowTeamID = myDataTable_Orderdetails.Rows[intj]["TeamID"].toInt32();

                            Int32 NowParentID = (intLength > 0) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(pub_Int_Session_CurUserID).toInt32() : 0;
                            Int32 NowGrandParentID = (intLength > 1) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(NowParentID).toInt32() : 0;
                            Int32 NowGreatParentID = (intLength > 2) ? Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(NowGrandParentID).toInt32() : 0;
                            Int32 NowTeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(pub_Int_Session_CurUserID.toString()).toInt32();
                            if (ParentID != NowParentID || GrandParentID != NowGrandParentID || GreatParentID != NowGreatParentID || TeamID != NowTeamID)
                            {
                                EggsoftWX.Model.tab_Orderdetails mytab_Orderdetails = my_tab_Orderdetails.GetModel(strOrderDetailID.toInt32());
                                mytab_Orderdetails.TeamID = NowTeamID;
                                mytab_Orderdetails.ParentID = NowParentID;
                                mytab_Orderdetails.GrandParentID = NowGrandParentID;
                                mytab_Orderdetails.GreatParentID = NowGreatParentID;
                                mytab_Orderdetails.UpdateDateTime = DateTime.Now;
                                mytab_Orderdetails.UpdateBy = "支付前发现上级更改过了";
                                my_tab_Orderdetails.Update(mytab_Orderdetails);
                            }
                            #endregion 检查上级是否改变过


                            String strOrderCount = myDataTable_Orderdetails.Rows[intj]["OrderCount"].ToString();
                            String strVouchersNum_List = myDataTable_Orderdetails.Rows[intj]["VouchersNum_List"].ToString();
                            String strBeans = myDataTable_Orderdetails.Rows[intj]["Beans"].ToString();
                            String strGoodType = myDataTable_Orderdetails.Rows[intj]["GoodType"].ToString();
                            String strGoodTypeId = myDataTable_Orderdetails.Rows[intj]["GoodTypeId"].ToString();
                            String strGoodTypeIdBuyInfo = myDataTable_Orderdetails.Rows[intj]["GoodTypeIdBuyInfo"].ToString();
                            String strFreight = myDataTable_Orderdetails.Rows[intj]["Freight"].ToString();
                            String strMoneyCredits = myDataTable_Orderdetails.Rows[intj]["MoneyCredits"].ToString();
                            String strMoneyWeBuy8Credits = myDataTable_Orderdetails.Rows[intj]["MoneyWeBuy8Credits"].ToString();
                            if(strBeans == "0") strBeans = "";//这里清空 有利于后面检查
                            if(strMoneyCredits == "0.00") strMoneyCredits = "";//这里清空 有利于后面检查
                            if(strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";//这里清空 有利于后面检查
                            int intOrderCount = Int32.Parse(strOrderCount);




                            String strGoodName = Eggsoft_Public_CL.GoodP.GetGoodType(Int32.Parse(strGoodType)) + myDataTable_Orderdetails.Rows[intj]["GoodName"].ToString();
                            Decimal GoodPrice = Decimal.Parse(myDataTable_Orderdetails.Rows[intj]["GoodPrice"].ToString());
                            String strGoodPrice = "" + Eggsoft_Public_CL.Pub.getPubMoney(GoodPrice);
                            if((strGoodType == "1") || (strGoodType == "2") || (strGoodType == "3"))
                            {
                                string strReturnGoodPrice = ""; Decimal dec_Good_Money = 0;
                                Eggsoft_Public_CL.ShoppingCart.getGoodPrice(Int32.Parse(strGoodType), Int32.Parse(strGoodTypeId), intOrderCount, strGoodTypeIdBuyInfo, out strReturnGoodPrice, out dec_Good_Money);
                                Decimal DecimalReturnGoodPrice = 0;
                                Decimal.TryParse(strReturnGoodPrice, out DecimalReturnGoodPrice);
                                strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(DecimalReturnGoodPrice);
                            }




                            strOrderCount = intOrderCount.ToString();
                            //}
                            //    bool boolIFSecondBuy = Eggsoft_Public_CL.GoodP.get_Bool_LimitTimer_FromGoodID(Int32.Parse(strGoodID));

                            //---计算购物券
                            Decimal allVouchersMoneyMoney = 0; Decimal allMoneyMoney = 0;
                            if(String.IsNullOrEmpty(strVouchersNum_List) == false)
                            {
                                string[] strEachList = strVouchersNum_List.Split(',');
                                for(int k = 0; k < strEachList.Length; k++)
                                {
                                    if(String.IsNullOrEmpty(strEachList[k]) == false)
                                    {
                                        string[] strEachListString = strEachList[k].Split('#');
                                        String strVouchersMoney = strEachListString[1];
                                        allVouchersMoneyMoney += Decimal.Parse(strVouchersMoney);
                                    }
                                }

                            }
                            else if(String.IsNullOrEmpty(strBeans) == false)
                            {
                                allVouchersMoneyMoney = Decimal.Multiply(Decimal.Parse(strBeans), (Decimal)0.01);
                            }

                            else if(String.IsNullOrEmpty(strMoneyWeBuy8Credits) == false)
                            {
                                allVouchersMoneyMoney = Decimal.Parse(strMoneyWeBuy8Credits);
                            }

                            if(String.IsNullOrEmpty(strMoneyCredits) == false)
                            {
                                allMoneyMoney = Decimal.Parse(strMoneyCredits);
                            }

                            //---计算运费
                            Decimal allFreightMoney = 0;
                            if(String.IsNullOrEmpty(strFreight) == false)
                            {
                                allFreightMoney = Decimal.Parse(strFreight);
                            }


                            String strGoodTotalMoney = "" + Eggsoft_Public_CL.Pub.getPubMoney(GoodPrice * intOrderCount - allVouchersMoneyMoney - allMoneyMoney + allFreightMoney);

                            string strGoodLink = "";
                            if(strGoodType == "0")
                            {
                                strGoodLink = Pub_Agent_Path + "/product-" + strGoodID + ".aspx";
                            }
                            else if(strGoodType == "1")
                            {
                                strGoodLink = "/Huodong/WeiKanJia/default.html?kanjiaid=" + strGoodTypeId;
                            }
                            else if(strGoodType == "2")
                            {
                                strGoodLink = "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId;
                            }
                            else if(strGoodType == "3")
                            {
                                strGoodLink = "/addfunction/04zc_project/03zc.html?zcid=" + strGoodTypeId;
                            }
                            else if(strGoodType == "6")
                            {
                                strGoodLink = "/op-" + strGoodTypeId + "-" + strGoodTypeIdBuyInfo + ".aspx";
                            }
                            strOrderCartGoods += "     <a href=\"" + strGoodLink + "\">\n";

                            strOrderCartGoods += "		<div class=\"pro_list_name_EachGoods\" ms-repeat=\"items\">\n";
                            strOrderCartGoods += "			<div class=\"pro_list_name left\">\n";
                            strOrderCartGoods += "				" + strGoodName + "\n";
                            strOrderCartGoods += "			</div>\n";
                            strOrderCartGoods += "			<div class=\"pro_list_price left\">\n";
                            strOrderCartGoods += "				￥" + strGoodPrice + "\n";
                            strOrderCartGoods += "			</div>\n";
                            strOrderCartGoods += "			<div class=\"pro_list_count left\">\n";
                            strOrderCartGoods += "				" + strOrderCount + "\n";
                            strOrderCartGoods += "			</div>\n";
                            strOrderCartGoods += "			<div class=\"pro_list_sum left\">\n";
                            strOrderCartGoods += "				￥" + strGoodTotalMoney + "\n";

                            strOrderCartGoods += "			</div>\n";
                            strOrderCartGoods += "		</div>\n";
                            strOrderCartGoods += "      </a>\n";
                        }
                        strOrderCartGoods += "		</div>\n";

                        #region 收货地址

                        String str_ID_User_Address = myDataTable.Rows[inti]["User_Address"].ToString();
                        String str_ID_User_O2OTakedID = myDataTable.Rows[inti]["O2OTakedID"].ToString();
                        if(String.IsNullOrEmpty(str_ID_User_Address) == false)
                        {
                            if(my_tab_User_Address.Exists("ID=" + str_ID_User_Address + " and IsDeleted=0"))
                            {
                                strOrderCartGoods += "<div style=\"width:98%;margin: 0; padding: 0;padding-right: 10px;\" class=\"addrtit_CancelOrder\" >收货地址：" + my_tab_User_Address.GetList("XiangXiDiZhi", "ID=" + str_ID_User_Address).Tables[0].Rows[0]["XiangXiDiZhi"].ToString() + "</div>  \n";
                            }
                        }
                        if(String.IsNullOrEmpty(str_ID_User_O2OTakedID) == false)
                        {
                            if(BLL_tab_ShopClient_O2O_TakeGoods.Exists("ID=" + str_ID_User_O2OTakedID + " and ISDeleted=0"))
                            {
                                EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods Model_tab_ShopClient_O2O_TakeGoods = BLL_tab_ShopClient_O2O_TakeGoods.GetModel(Int32.Parse(str_ID_User_O2OTakedID));
                                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(Model_tab_ShopClient_O2O_TakeGoods.TakeO2OShopID);
                                if(Model_tab_ShopClient_O2O_ShopInfo != null)
                                {
                                    strOrderCartGoods += "<div style=\"width:98%;margin: 0; padding: 0;padding-right: 10px;\" class=\"addrtit_CancelOrder\" >上门自取：" + Model_tab_ShopClient_O2O_ShopInfo.ShopName + " " + Model_tab_ShopClient_O2O_ShopInfo.ShopAdress + " 营业时间：" + Model_tab_ShopClient_O2O_ShopInfo.ShopDayTime + "  <br /> 自取时间：" + Model_tab_ShopClient_O2O_TakeGoods.TakeDateTime.ToString("yyyy-MM-dd HH:mm") + "</div>\n";
                                }
                            }
                        }
                        else
                        {
                            strOrderCartGoods += "<div class=\"addrtit_CancelOrder\" ><div class='addrtit'><a id=\"HyperLink_Write_Address\" class=\"title_Link\" href=\"" + Pub_Agent_Path + "/cart_self.aspx?type=modifyorder&modifyorderid=" + strOrderID + "\">管理收货地址</a></div>";
                        }
                        strOrderCartGoods += "<div class=\"CancelOrder\" style=\"overflow:none;margin-right: 16px;\"><a href=\"javascript: CancelThis(" + strOrderID + ");\" id=\"LinkButton_isSale\">取消</a></div>\n";
                        strOrderCartGoods += "</li>" + "\n";
                        #endregion



                        strOrderCartGoods += "             \n";
                        //strOrderCartGoods += "        </div>\n";
                        //strOrderCartGoods += "	</div>\n";
                        //strOrderCartGoods +=inti.ToString() + "</li>	\n";



                    }


                    strOrderCartGoods += "</ul>\n";

                    //Literal_GetCart.Text = strOrderCartGoods;

                }



                strTemplet = strTemplet.Replace("###WaitPayList###", strOrderCartGoods);

            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }

            finally
            {

            }
            return strTemplet;


            #endregion
        }

    }
}