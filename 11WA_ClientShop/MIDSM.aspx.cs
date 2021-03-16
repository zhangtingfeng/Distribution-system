using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MIDSM : System.Web.UI.Page
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
                {///////
                    setAllNeedID();

                    Eggsoft_Public_CL.Pub_FenXiao.FileUploadRedJPGCheck(pub_Int_ShopClientID);

                    string type = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(type) == false)
                    {
                        //window.location.href = "?type=BeginSend&HowMuch=" + danZongMoney + "&HowMany=" + baoNUm;

                        if (type == "beginsend")
                        {


                            EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                            EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers Model_tab_RedWallet_Money_Credits = new EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers();

                            //int intArgUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();

                            String HowMuchGei = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["HowMuch"]);
                            String HowMany = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["HowMany"]);
                            String Money_Credits_Vouchers = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["Money_Credits_Vouchers"]);
                            String TSign = (Request.QueryString["TSign"]);

                            #region 验证签名
                            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                            string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(HowMuchGei + HowMany + Money_Credits_Vouchers + Eggsoft.Common.DESCrypt.hex_md5_2(Model_tab_User.SafeCode));
                            if (!(TSign == strNetSign))
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("签名失败，访问非法", -1);
                                return;
                            }
                            #endregion 验证签名

                            Decimal DecimalmyHowMuch = 0;
                            Decimal.TryParse(HowMuchGei, out DecimalmyHowMuch);
                            if (DecimalmyHowMuch > 3000)
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("最多3000元，需要发更多的请联系商家解决此问题", -1);
                                return;
                            }
                            if (DecimalmyHowMuch * 100 < HowMany.toInt32())
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("发放个数太多，无法分配", -1);
                                return;
                            }


                            Decimal myCountMoney = 0;
                            if (Money_Credits_Vouchers == "1")
                            {

                                if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareXianJinHongBao") == true)
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("功能关闭，请咨询客服", "/mywebuy.aspx");
                                    return;
                                }

                                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(pub_Int_Session_CurUserID, out myCountMoney);

                                if (DecimalmyHowMuch <= myCountMoney && DecimalmyHowMuch > 0)///被攻击ztf20170418
                                {

                                    #region  减去余额中的钱
                                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge Bll_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 120;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalmyHowMuch;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "发红包";
                                    int intADD = Bll_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                                    if (!(intADD > 0))
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("处理失败", -1);
                                        return;
                                    }
                                    #endregion 减去余额中的钱

                                    #region  红包中增加钱

                                    Model_tab_RedWallet_Money_Credits.ShopClientID = pub_Int_ShopClientID;
                                    Model_tab_RedWallet_Money_Credits.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_RedWallet_Money_Credits.ShopClientID = pub_Int_ShopClientID;
                                    Model_tab_RedWallet_Money_Credits.HowmanyPeople = Int32.Parse(HowMany);
                                    Model_tab_RedWallet_Money_Credits.Money = DecimalmyHowMuch;
                                    Model_tab_RedWallet_Money_Credits.UpdateTime = DateTime.Now;
                                    Model_tab_RedWallet_Money_Credits.Type_Or_Money_Credits_Vouchers = 1;//现金类型
                                    int myID = Bll_tab_RedWallet_Money_Credits.Add(Model_tab_RedWallet_Money_Credits);
                                    if (!(myID > 0))
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("处理失败", -1);
                                        return;
                                    }
                                    #endregion 红包中增加钱


                                    #region 加签名
                                    EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                                    string strUserIDSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(Modeltab_User.SafeCode);
                                    string strSign = Eggsoft.Common.DESCrypt.hex_md5_8(myID.toString() + strUserIDSafeCode);
                                    #endregion 加签名

                                    Eggsoft.Common.JsUtil.LocationNewHref(Pub_Agent_Path + "/midsmf.aspx?thisshowid=" + myID + "&type=beginopen&sign=" + strSign);
                                }
                                else
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("余额不足", -1);
                                }

                            }

                            else if (Money_Credits_Vouchers == "2")
                            {
                                if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareGouWuQuan") == true)
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("功能关闭，请咨询客服", "/mywebuy.aspx");
                                    return;
                                }

                                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(pub_Int_Session_CurUserID, out myCountMoney);


                                if (DecimalmyHowMuch <= myCountMoney && DecimalmyHowMuch > 0)
                                {



                                    #region  减去余额中的钱
                                    EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge Bll_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();

                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalmyHowMuch;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "发红包";
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                    int intADD = Bll_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                                    if (!(intADD > 0))
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("处理失败", -1);
                                        return;
                                    }
                                    #endregion 减去余额中的钱

                                    #region  红包中增加钱
                                    Model_tab_RedWallet_Money_Credits.ShopClientID = pub_Int_ShopClientID;
                                    Model_tab_RedWallet_Money_Credits.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_RedWallet_Money_Credits.HowmanyPeople = Int32.Parse(HowMany);
                                    Model_tab_RedWallet_Money_Credits.Money = DecimalmyHowMuch;
                                    Model_tab_RedWallet_Money_Credits.UpdateTime = DateTime.Now;
                                    Model_tab_RedWallet_Money_Credits.Type_Or_Money_Credits_Vouchers = 2;//购物券类型
                                    int myID = Bll_tab_RedWallet_Money_Credits.Add(Model_tab_RedWallet_Money_Credits);
                                    if (!(myID > 0))
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("处理失败", -1);
                                        return;
                                    }
                                    #endregion 红包中增加钱

                                    Eggsoft.Common.JsUtil.LocationNewHref(Pub_Agent_Path + "/midsmf.aspx?type=beginopen&thisshowid=" + myID);
                                }
                                else
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("余额不足", -1);
                                }
                            }
                        }
                    }
                    else
                    {
                        string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_income_drawmoney_ShareRedMoney_Temple.html");
                        strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                        strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                        strTemplet = strTemplet.Replace("###Header###", "");
                        String Money_Credits_Vouchers = (Request.QueryString["Money_Credits_Vouchers"]);

                        if (Money_Credits_Vouchers == "1")
                        {
                            if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareXianJinHongBao") == true)
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("功能关闭，请咨询客服", "/mywebuy.aspx");
                                return;
                            }

                            strTemplet = strTemplet.Replace("###_PulicTitleStyle###", "现金");
                            strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "现金红包设置"));

                        }

                        else if (Money_Credits_Vouchers == "2")
                        {
                            if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareGouWuQuan") == true)
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("功能关闭，请咨询客服", "/mywebuy.aspx");
                                return;
                            }

                            strTemplet = strTemplet.Replace("###_PulicTitleStyle###", Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()));
                            strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "红包设置"));

                        }



                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path
        ));
                        strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                        string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                        strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);

                        strTemplet = Init_maxMoney(strTemplet);

                        strTemplet = strTemplet.Replace("###draw_input_ShareMoney_History_List###", getdraw_input_ShareMoney_History_List());


                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                        strTemplet = strTemplet.Replace("###NetUserSafeCode###", Eggsoft.Common.DESCrypt.hex_md5_2(Model_tab_User.SafeCode));


                        Response.Write(strTemplet);
                    }

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
        private String getdraw_input_ShareMoney_History_List()
        {
            string strgetdraw_input_ShareMoney_History_List = "";

            String Money_Credits_Vouchers = (Request.QueryString["Money_Credits_Vouchers"]);

            int userID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();

            EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
            if (String.IsNullOrEmpty(Money_Credits_Vouchers)) Money_Credits_Vouchers = "2";
            string strSQL = "isnull(PID,0)=0 and UserID=" + userID + " and Type_Or_Money_Credits_Vouchers=" + Money_Credits_Vouchers + " order by id desc";
            System.Data.DataTable Data_DataTable = Bll_tab_RedWallet_Money_Credits.GetList(strSQL).Tables[0];

            if (Data_DataTable.Rows.Count > 0)
            {
                strgetdraw_input_ShareMoney_History_List += "<div class=\"draw_input_ShareMoney_History_List_Text\">已发红包</div>";
            }

            #region 加签名
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
            string strUserIDSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(Modeltab_User.SafeCode);
            #endregion 加签名
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                string strID = Data_DataTable.Rows[i]["ID"].ToString();
                string strMoney = Data_DataTable.Rows[i]["Money"].ToString();
                string strUpdateTime = Data_DataTable.Rows[i]["CreatTime"].ToString();

                string strSign = Eggsoft.Common.DESCrypt.hex_md5_8(strID.toString() + strUserIDSafeCode);

                strgetdraw_input_ShareMoney_History_List += "<br /><a  target=\"_blank\"  href=\"" + Pub_Agent_Path + "/midsmf.aspx?thisshowid=" + strID + "&sign=" + strSign + "\"><span class=\"draw_input_ShareMoney_HistoryAText_Money\">" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strMoney)) + "元</span><span class=\"draw_input_ShareMoney_HistoryATextYear\">" + DateTime.Parse(strUpdateTime).ToString("yyyy年MM月dd日") + "</span></a>";


            }

            return strgetdraw_input_ShareMoney_History_List;

        }
        private String Init_maxMoney(String strargBody)
        {





            Decimal myCountMoney = 0;
            String Money_Credits_Vouchers = (Request.QueryString["Money_Credits_Vouchers"]);

            if (Money_Credits_Vouchers == "1")
            {
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(pub_Int_Session_CurUserID, out myCountMoney);
            }

            else if (Money_Credits_Vouchers == "2")
            {
                Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(pub_Int_Session_CurUserID, out myCountMoney);
            }

            string strMaxMoneyInfo = "";
            string typemaxMoney = Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney);





            strMaxMoneyInfo += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "你好，你的可设置额度是¥<span id=\"typemaxMoney\">" + typemaxMoney + "</span><br/><br/>";
            strargBody = strargBody.Replace("###MaxMoneyInfo###", strMaxMoneyInfo);


            string strCanWriteMoney = "";
            Decimal mytypemaxMoney = Decimal.Parse(typemaxMoney);

            if (mytypemaxMoney >= 200)
            {
                strCanWriteMoney = "200";
                //int intMoney = Decimal.ToInt32(mytypemaxMoney);
                //intMoney = (intMoney / 100) * 100;
                //strCanWriteMoney = intMoney.ToString();
            }
            else if (mytypemaxMoney >= 100)
            {
                strCanWriteMoney = "100";
            }
            else if (mytypemaxMoney >= 20)
            {
                strCanWriteMoney = "20";
            }
            else if (mytypemaxMoney >= 10)
            {
                strCanWriteMoney = "10";
            }
            strargBody = strargBody.Replace("###Now_Draw_Money###", strCanWriteMoney);



            ///////////////////
            string strTextShow = "";
            if (Money_Credits_Vouchers == "1")
            {
                strTextShow = "塞钱进现金红包";
            }
            else if (Money_Credits_Vouchers == "2")
            {
                strTextShow = "塞钱进" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "红包";
            }
            /////////////////////////
            strargBody = strargBody.Replace("###Red_Text_Money_Credits_Vouchers###", strTextShow);

            return strargBody;

        }
    }
}