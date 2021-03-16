using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft.Common;

namespace _11WA_ClientShop
{
    public partial class Cart_Good3 : System.Web.UI.Page
    {

        private EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
        private EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


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

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_CartGood3_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "已完成"));

                    strTemplet = strTemplet.Replace("###Header###", "");

                    if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "PC")
                    {
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    }
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


                    strTemplet = InitOrders(strTemplet);
                    #region 注销已完成未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart_good3' and Readed=0", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    #endregion 注销已完成未读消息

                    Response.Write(strTemplet);
                    //InitOrders();

                    //#region 底部快速按钮
                    //Control Control_WUC_Bottom = Page.LoadControl("/Control/" + Eggsoft_Public_CL.tab_System_And_.getTab_System("CityTemplet") + "/WUC_Bottom.ascx");
                    //WUC_Bottom.Controls.Add(Control_WUC_Bottom);
                    //#endregion
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
        private String InitOrders(string strTemplet)
        {
            #region background

            //商户OrderNum  你确定要确认收货吗？ 调试语句  http://localhost:8011/cart_good3.aspx?getgoodsordernum=11939
            string GetGoodsOrderNum = Request.QueryString["GetGoodsOrderNum"];
            if (GetGoodsOrderNum != null)
            {
                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
               
                my_BLL_tab_Order.Update("isReceipt=1,UpdateDateTime=getdate(),updateby=@updateby", "id=" + GetGoodsOrderNum.ToString(), "用户确认收货");
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {///立即收获 就理解结算
                    try
                    {

                        Eggsoft_Public_CL.Pub_FenXiao.DoOver7daysCountMySonMoney_UpdateEvertDay(pub_Int_ShopClientID, GetGoodsOrderNum.toInt32());
                    }
                    catch (System.Threading.ThreadAbortException ettt)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                    }
                    catch (Exception Exceptione)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "用户确认收货");
                    }
                    finally
                    {

                    }
                });

                EggsoftWX.Model.tab_Order Modeltab_Order = my_BLL_tab_Order.GetModel(GetGoodsOrderNum.toInt32());

                #region 注销待付款等完成未处理信息
                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and (Type='Info_cart_good2' or Type='Info_cart_good') and Readed=0", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                #endregion 注销待付款等完成未处理信息  

                //#region 增加已完成未处理信息
                //EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                //Model_b011_InfoAlertMessage.InfoTip = "用户手动确认收货";
                //Model_b011_InfoAlertMessage.CreateBy = "用户手动确认收货";
                //Model_b011_InfoAlertMessage.UpdateBy = "用户手动确认收货";
                //Model_b011_InfoAlertMessage.UserID = pub_Int_Session_CurUserID;
                //Model_b011_InfoAlertMessage.ShopClient_ID = pub_Int_ShopClientID;
                //Model_b011_InfoAlertMessage.Type = "Info_cart_good3";
                //Model_b011_InfoAlertMessage.TypeTableID = Modeltab_Order.ID;
                //bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                //#endregion 增加已完成未处理信息  



            }

            #region background

            EggsoftWX.BLL.tab_Order my_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.BLL.tab_Orderdetails my_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            //int userID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();

            System.Data.DataTable myDataTable = my_tab_Order.GetList("UserID=" + pub_Int_Session_CurUserID + " and PayStatus=1 and isReceipt=1  and isdeleted<>1 order by id desc").Tables[0];

            string strOrderCartGoods = "";
            strOrderCartGoods += "<ul id=\"thelist\" class=\"experience_list\">\n";


            if (myDataTable.Rows.Count > 0)
            {
                for (int inti = 0; inti < myDataTable.Rows.Count; inti++)
                {

                    String strOrderID = myDataTable.Rows[inti]["ID"].ToString();
                    String strOrderNum = myDataTable.Rows[inti]["OrderNum"].ToString();
                    Decimal allMoney = Decimal.Parse(myDataTable.Rows[inti]["TotalMoney"].ToString());
                    String strTotalMoney = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(allMoney);
                    String strOrderName = myDataTable.Rows[inti]["OrderName"].ToString();
                    DateTime DateTimeCreateDatetime = Convert.ToDateTime(myDataTable.Rows[inti]["CreateDatetime"].ToString());


                    strOrderCartGoods += "<div class=\"pro_w\">\n";
                    strOrderCartGoods += "                    <div class=\"tpLeft_txt\">\n";
                    strOrderCartGoods += "                       <div class=\"Ordertitle\">" + "  订单号：" + strOrderNum + "<br /> \n";
                    //strOrderCartGoods += "                       订单名称：<strong class='tit'>" + strOrderName + "</strong><br /> \n";
                    strOrderCartGoods += "                       订单金额：<strong class='price'>" + strTotalMoney + "</strong></div>\n";
                    strOrderCartGoods += "    </div>\n";

                    strOrderCartGoods += "<div class=\"OrderGoodDetail\">\n";


                    System.Data.DataTable myDataTable_Orderdetails = my_tab_Orderdetails.GetList("OrderID=" + strOrderID + " and isdeleted<>1").Tables[0];
                    for (int intj = 0; intj < myDataTable_Orderdetails.Rows.Count; intj++)
                    {

                        String strOrderDetailID = myDataTable_Orderdetails.Rows[intj]["ID"].ToString();
                        String strGoodID = myDataTable_Orderdetails.Rows[intj]["GoodID"].ToString();
                        Decimal GoodPrice = Decimal.Parse(myDataTable_Orderdetails.Rows[intj]["GoodPrice"].ToString());
                        String strVouchersNum_List = myDataTable_Orderdetails.Rows[intj]["VouchersNum_List"].ToString();

                        String strBeans = myDataTable_Orderdetails.Rows[intj]["Beans"].ToString();
                        String strFreight = myDataTable_Orderdetails.Rows[intj]["Freight"].ToString();
                        String strGoodType = myDataTable_Orderdetails.Rows[intj]["GoodType"].ToString();
                        String strGoodTypeId = myDataTable_Orderdetails.Rows[intj]["GoodTypeId"].ToString();
                        String strGoodTypeIdBuyInfo = myDataTable_Orderdetails.Rows[intj]["GoodTypeIdBuyInfo"].ToString();
                        String strGoodName = Eggsoft_Public_CL.GoodP.GetGoodType(Int32.Parse(strGoodType)) + myDataTable_Orderdetails.Rows[intj]["GoodName"].ToString();

                        String strMoneyCredits = myDataTable_Orderdetails.Rows[intj]["MoneyCredits"].ToString();
                        String strMoneyWeBuy8Credits = myDataTable_Orderdetails.Rows[intj]["MoneyWeBuy8Credits"].ToString();
                        if (strBeans == "0") strBeans = "";//这里清空 有利于后面检查
                        if (strMoneyCredits == "0.00") strMoneyCredits = "";//这里清空 有利于后面检查
                        if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";//这里清空 有利于后面检查
                        int intOrderCount = Int32.Parse(myDataTable_Orderdetails.Rows[intj]["OrderCount"].ToString());

                        String strGoodPrice = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(GoodPrice);
                        if ((strGoodType == "1") || (strGoodType == "2") || (strGoodType == "3"))
                        {
                            string strReturnGoodPrice = ""; Decimal dec_Good_Money = 0;
                            Eggsoft_Public_CL.ShoppingCart.getGoodPrice(Int32.Parse(strGoodType), Int32.Parse(strGoodTypeId), intOrderCount, strGoodTypeIdBuyInfo, out strReturnGoodPrice, out dec_Good_Money);
                            strGoodPrice = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strReturnGoodPrice));
                        }

                        //---计算购物券
                        Decimal allVouchersMoneyMoney = 0; Decimal allMoneyMoney = 0;
                        if (String.IsNullOrEmpty(strVouchersNum_List) == false)
                        {
                            string[] strEachList = strVouchersNum_List.Split(',');
                            for (int k = 0; k < strEachList.Length; k++)
                            {
                                if (String.IsNullOrEmpty(strEachList[k]) == false)
                                {
                                    string[] strEachListString = strEachList[k].Split('#');
                                    String strVouchersMoney = strEachListString[1];
                                    allVouchersMoneyMoney += Decimal.Parse(strVouchersMoney);
                                }
                            }

                        }
                        else if (String.IsNullOrEmpty(strBeans) == false)
                        {
                            allVouchersMoneyMoney = Decimal.Multiply(Decimal.Parse(strBeans), (Decimal)0.01);
                        }
                        //else if (String.IsNullOrEmpty(strMoneyCredits) == false)
                        //{
                        //    allVouchersMoneyMoney = Decimal.Parse(strMoneyCredits);
                        //}
                        else if (String.IsNullOrEmpty(strMoneyWeBuy8Credits) == false)
                        {
                            allVouchersMoneyMoney = Decimal.Parse(strMoneyWeBuy8Credits);
                        }
                        if (String.IsNullOrEmpty(strMoneyCredits) == false)
                        {
                            allMoneyMoney = Decimal.Parse(strMoneyCredits);
                        }
                        //---计算运费
                        Decimal allFreightMoney = 0;
                        if (String.IsNullOrEmpty(strFreight) == false)
                        {
                            allFreightMoney = Decimal.Parse(strFreight);
                        }
                        String strGoodTotalMoney = "￥" + Eggsoft_Public_CL.Pub.getPubMoney(GoodPrice * intOrderCount - allVouchersMoneyMoney - allFreightMoney + allFreightMoney);
                        string strGoodLink = "";
                        if (strGoodType == "0")
                        {
                            strGoodLink = Pub_Agent_Path + "/product-" + strGoodID + ".aspx";
                        }
                        else if (strGoodType == "1")
                        {
                            strGoodLink = "/Huodong/WeiKanJia/default.html?kanjiaid=" + strGoodTypeId;
                        }
                        else if (strGoodType == "2")
                        {
                            strGoodLink = "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId;
                        }
                        else if (strGoodType == "3")
                        {
                            strGoodLink = "/addfunction/04zc_project/03zc.html?zcid=" + strGoodTypeId;
                        }
                        strOrderCartGoods += "        <a href=\"" + strGoodLink + "\"  title=\"" + strOrderDetailID + "\">        <div class=\"eachLine\">\n";
                        strOrderCartGoods += "                    <div class=\"eachItemName\">" + strGoodName + "</div>\n";
                        strOrderCartGoods += "                    <div class=\"eachItem\">" + strGoodPrice + "</div>\n";
                        strOrderCartGoods += "                    <div class=\"eachItemShort\">" + intOrderCount.ToString() + "</div>\n";
                        strOrderCartGoods += "                    <div class=\"eachItem\">" + strGoodTotalMoney + "\n";
                        strOrderCartGoods += "                                                                     </div>\n";
                        strOrderCartGoods += "                </div></a>\n";
                    }
                    strOrderCartGoods += "                </div>\n";
                    strOrderCartGoods += "                </div>\n";

                }
            }
            else
            {
                strOrderCartGoods += "暂无信息\n";
            }
            #endregion
            strOrderCartGoods += "</ul>\n";

            strTemplet = strTemplet.Replace("###WaitPayList###", strOrderCartGoods);

            return strTemplet;
            #endregion
        }

    }
}