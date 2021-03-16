using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MIDSMF : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        private int pInt_QueryString_ParentID = 0;//；

        private int intMoney_Credits_Vouchers = 0;

        private int intOnlyMeGetUserID = 0;///定向红包 只有我才能抢。转发分享无效

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                setAllNeedID();
                Init_Var();


                if (!IsPostBack)
                {
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_income_drawmoney_ShareRedMoney_ShareFriend_Temple.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString());
                    strTemplet = strTemplet.Replace("###ParentID###", pInt_QueryString_ParentID.ToString());
                    strTemplet = strTemplet.Replace("###ToShopCilentID###", pub_Int_ShopClientID.ToString());
                    strTemplet = strTemplet.Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//
                    //strTemplet = InitOpenShopAsk(strTemplet);

                    string type = Request.QueryString["type"];


                    string ThisShowID = Request.QueryString["thisshowid"];
                    EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                    EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers From_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(Int32.Parse(ThisShowID));
                    bool boolISMe = From_Model_tab_RedWallet_Money_Credits.UserID == pub_Int_Session_CurUserID;

                    if (intMoney_Credits_Vouchers == 1)
                    {
                        if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareXianJinHongBao") == true)
                        {
                            Eggsoft.Common.JsUtil.ShowMsg("功能关闭，请咨询客服", "/mywebuy.aspx");
                            return;
                        }
                    }

                    else if (intMoney_Credits_Vouchers == 2)
                    {
                        if (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseShareGouWuQuan") == true)
                        {
                            Eggsoft.Common.JsUtil.ShowMsg("功能关闭，请咨询客服", "/mywebuy.aspx");
                            return;
                        }
                    }

                    #region 验证签名  定向红包 不要验证
                    intOnlyMeGetUserID = 0;
                    string strintOnlyMeGetUserID = Request.QueryString["onlymegetuserid"];///定向红包 只有我才能抢。转发分享无效
                    int.TryParse(strintOnlyMeGetUserID, out intOnlyMeGetUserID);
                    if (intOnlyMeGetUserID == 0)
                    {
                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(From_Model_tab_RedWallet_Money_Credits.UserID.toInt32());
                        string strUserIDSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(Modeltab_User.SafeCode);
                        string strSign = Eggsoft.Common.DESCrypt.hex_md5_8(ThisShowID + strUserIDSafeCode);
                        string strQueryStringsign = Request.QueryString["sign"].toString();
                        if (strSign != strQueryStringsign)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("strQueryStringsign=" + strQueryStringsign + "strSign=" + strSign, "抢红包", "非法访问,签名失败");
                            Response.Write("非法访问,签名失败");
                            Response.End();
                        }
                    }
                    #endregion 验证签名

                    if (boolISMe == false)///不是我 就自动打开
                    {
                        strTemplet = strTemplet.Replace("###MaxMoneyInfo###", Init_maxMoney_BeginOpen());
                    }
                    else
                    {

                        strTemplet = strTemplet.Replace("###MaxMoneyInfo###", Init_maxMoney());
                    }


                    if (intMoney_Credits_Vouchers == 1)
                    {
                        strTemplet = strTemplet.Replace("###Literal_Title###", "现金红包分享");
                        strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "现金红包分享"));
                    }
                    else if (intMoney_Credits_Vouchers == 2)
                    {
                        strTemplet = strTemplet.Replace("###Literal_Title###", Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "红包分享");
                        strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "红包分享"));
                    }


                    strTemplet = strTemplet.Replace("###Header###", "");




                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());
                    strTemplet = InitWeiXinShareLinkRedWallet(strTemplet);

                    ///Eggsoft.Common.debug_Log.Call_WriteLog(strTemplet, "doVisiRedBag分销红包", "分析报错原因");

                    Response.Write(strTemplet);
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "抢红包", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "抢红包");
            }
            finally
            {

            }
        }
        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }
        private void Init_Var()
        {
            string ThisShowID = Request.QueryString["thisshowid"];
            EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
            EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers From_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(Int32.Parse(ThisShowID));


            intMoney_Credits_Vouchers = From_Model_tab_RedWallet_Money_Credits.Type_Or_Money_Credits_Vouchers.toInt32();

            string strintOnlyMeGetUserID = Request.QueryString["onlymegetuserid"];///定向红包 只有我才能抢。转发分享无效
            int.TryParse(strintOnlyMeGetUserID, out intOnlyMeGetUserID);
        }
        /// <summary>
        /// 处理自己打开别人的
        /// </summary>
        /// <returns></returns>
        private String Init_maxMoney_BeginOpen()
        {
            string strMoneyInfoadd = "";


            string ThisShowID = Request.QueryString["thisshowid"];
            EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
            EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers From_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(Int32.Parse(ThisShowID));
            #region 分析哪里来的钱 可能是后台发放
            ///
            ///
            String strGetFromMoneyDesc = "";
            if (intOnlyMeGetUserID > 0)
            {
                int intSendMoneyByRedBagID = From_Model_tab_RedWallet_Money_Credits.SendMoneyByRedBagID.toInt32();
                if (intSendMoneyByRedBagID > 0)
                {
                    EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag BLL_tab_ShopClient_SendMoneyByRedBag = new EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag();
                    EggsoftWX.Model.tab_ShopClient_SendMoneyByRedBag Model_tab_ShopClient_SendMoneyByRedBag = BLL_tab_ShopClient_SendMoneyByRedBag.GetModel(intSendMoneyByRedBagID);
                    if (Model_tab_ShopClient_SendMoneyByRedBag != null)
                    {
                        strGetFromMoneyDesc = Model_tab_ShopClient_SendMoneyByRedBag.MsgTypeNewsTitle;
                    }
                    if (DateTime.Now < Model_tab_ShopClient_SendMoneyByRedBag.ValidStartTime)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("还没有开始,请持续关注！", "/");//不能抢自己的红包额
                        return "";
                    }
                    if (DateTime.Now > Model_tab_ShopClient_SendMoneyByRedBag.ValidEndTime)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("已经结束，请关注下一轮！", "/");//不能抢自己的红包额
                        return "";
                    }



                }

                if (intOnlyMeGetUserID != pub_Int_Session_CurUserID)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("属于定向红包，别人不能抢，只能收到信息的本人抢！", "/");//不能抢自己的红包额
                    return "";
                }
            }
            else
            {
                strGetFromMoneyDesc = Eggsoft_Public_CL.Pub.GetNickName(From_Model_tab_RedWallet_Money_Credits.UserID.ToString()) + " 微店ID:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(From_Model_tab_RedWallet_Money_Credits.UserID.ToString()) + " 红包序号:" + ThisShowID;
            }
            #endregion
            //if (From_Model_tab_RedWallet_Money_Credits.UserID == pub_Int_Session_CurUserID)
            //{
            //    Eggsoft.Common.JsUtil.ShowMsg("自己不能抢自己的红包,请点击左上角的按钮发送给你的朋友");//不能抢自己的红包额
            //    return "";

            //}
            //else
            //{
            System.Data.DataTable Data_DataTable = Bll_tab_RedWallet_Money_Credits.GetList("PID=" + ThisShowID).Tables[0];
            int intHowManyPeopleGet = Data_DataTable.Rows.Count;


            if (intHowManyPeopleGet < From_Model_tab_RedWallet_Money_Credits.HowmanyPeople && From_Model_tab_RedWallet_Money_Credits.ISClosed.toBoolean() == false)
            {
                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers IGet_Model_tab_RedWallet_Money_Credits = new EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers();


                Decimal userGetMoney;
                bool boolIhaveGet = Bll_tab_RedWallet_Money_Credits.Exists("PID=" + ThisShowID + " and UserID=" + pub_Int_Session_CurUserID);
                if (boolIhaveGet)
                {
                    IGet_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel("PID=" + ThisShowID + " and UserID=" + pub_Int_Session_CurUserID);
                    userGetMoney = IGet_Model_tab_RedWallet_Money_Credits.Money.toDecimal();
                }
                else
                {
                    Decimal DecimalAll = From_Model_tab_RedWallet_Money_Credits.Money.toDecimal();
                    String strSQL = "select sum(Money) as sum_Money from tab_RedWallet_Money_Credits_Vouchers where PID=" + ThisShowID;
                    String sum_Money = Bll_tab_RedWallet_Money_Credits.SelectList(strSQL).Tables[0].Rows[0]["sum_Money"].ToString();
                    Decimal Decimal_sum_Money = 0;
                    Decimal.TryParse(sum_Money, out Decimal_sum_Money);
                    Decimal DecimalRandom = (DecimalAll - Decimal_sum_Money);


                    if (DecimalRandom > 0)
                    {
                        #region 分钱
                        //假如是最后一个人 全得吧
                        if (intHowManyPeopleGet == From_Model_tab_RedWallet_Money_Credits.HowmanyPeople - 1)
                        {
                            userGetMoney = DecimalRandom;


                            Bll_tab_RedWallet_Money_Credits.Update("ISClosed=1,UpdateTime=getdate(),UpdateBy='分完,自动关闭'", "ID=" + ThisShowID);

                        }
                        else
                        {
                            /*每次在剩余的平均数里面 随机  然后 生成数组  。随机提走一个*/
                            int intCount = From_Model_tab_RedWallet_Money_Credits.HowmanyPeople.toInt32() - intHowManyPeopleGet;

                            int[] intRadomMoneyList = new int[intCount];
                            DecimalRandom = Decimal.Multiply(DecimalRandom, 100);///转化成分

                            for (int i = 0; i < intCount; i++)
                            {
                                Decimal DecimalAve = DecimalRandom / (intCount - i);//剩余的的平均数
                                int intMax = (int)DecimalAve; ;
                                if (i == intCount - 1)
                                {
                                    intRadomMoneyList[i] = intMax;//最后一个
                                }
                                else
                                {
                                    Random rad = new Random();//实例化随机数产生器rad；
                                    int value = rad.Next(1, intMax);//用rad生成大于等于1000，小于等于9999的随机数；
                                                                    //除10000 转成钱
                                    intRadomMoneyList[i] = value;
                                    DecimalRandom = DecimalRandom - value;
                                }
                            }
                            ///最大数 最小数 平均一下

                            Random radLast = new Random();//实例化随机数产生器rad；
                            int userGetvalue = radLast.Next(1, intCount);//用rad生成大于等于1000，小于等于9999的随机数；

                            userGetMoney = Decimal.Multiply(intRadomMoneyList[userGetvalue], (decimal)0.01);

                        }
                        #region  红包中增加得到钱的记录
                        if (userGetMoney > 0)
                        {
                            IGet_Model_tab_RedWallet_Money_Credits.ShopClientID = pub_Int_ShopClientID;
                            IGet_Model_tab_RedWallet_Money_Credits.PID = Int32.Parse(ThisShowID);
                            IGet_Model_tab_RedWallet_Money_Credits.UserID = pub_Int_Session_CurUserID;
                            IGet_Model_tab_RedWallet_Money_Credits.Money = userGetMoney;
                            IGet_Model_tab_RedWallet_Money_Credits.UpdateTime = DateTime.Now;
                            IGet_Model_tab_RedWallet_Money_Credits.Type_Or_Money_Credits_Vouchers = intMoney_Credits_Vouchers;
                            int myID = Bll_tab_RedWallet_Money_Credits.Add(IGet_Model_tab_RedWallet_Money_Credits);
                            if (!(myID > 0))
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("处理失败", "/");//不能抢自己的红包额
                                return "";
                            }
                        }
                        #endregion 红包中增加钱


                        #region  增加余额中的钱

                        if (intMoney_Credits_Vouchers == 1)
                        {
                            if (Decimal.Round(userGetMoney, 2) > 0)
                            {
                                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge Bll_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 70;
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = userGetMoney;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "红包(" + strGetFromMoneyDesc + ")";
                                Model_tab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                int intmyID = Bll_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                                #region 增加账户余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intmyID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加账户余额未处理信息 


                                if (!(intmyID > 0))
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("处理失败", "/");//不能抢自己的红包额
                                    return "";
                                }
                            }
                        }
                        else if (intMoney_Credits_Vouchers == 2)
                        {
                            if (Decimal.Round(userGetMoney, 2) > 0)
                            {
                                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge Bll_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();

                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = userGetMoney;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "红包(" + strGetFromMoneyDesc + ")";
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                int intmyID = Bll_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                                #region 增加购物券未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                Model_b011_InfoAlertMessage.TypeTableID = intmyID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加购物券未处理信息 


                                if (!(intmyID > 0))
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("处理失败", "/");//不能抢自己的红包额
                                    return "";
                                }
                            }
                        }

                        #endregion 减去余额中的钱

                        #endregion 分钱
                    }

                    else
                    {
                        userGetMoney = 0;
                        ///不分钱
                        Bll_tab_RedWallet_Money_Credits.Update("ISClosed=1,UpdateTime=getdate(),UpdateBy='没钱了，关闭 userID=" + pub_Int_Session_CurUserID + "'", "ID=" + ThisShowID);
                    }
                }

                strMoneyInfoadd += "<br /><br /><br /><br /><br /><br /><br />";


                strMoneyInfoadd += "你已抢到" + strGetFromMoneyDesc;

                if (intMoney_Credits_Vouchers == 1)
                {
                    strMoneyInfoadd += "的现金红包！<br />" + "额度是" + Eggsoft_Public_CL.Pub.getPubMoney(userGetMoney);
                    strMoneyInfoadd += "<br /><br /><a style=\"Color:white;text-decoration:underline;\" href=\"multibutton_showmoneydata.aspx\">单击这里查看收益和使用方法</a>";
                    strMoneyInfoadd += "<br /><br /><br /><br /><br /><br /><br />¥" + Eggsoft_Public_CL.Pub.getPubMoney(userGetMoney);
                }

                else if (intMoney_Credits_Vouchers == 2)
                {
                    strMoneyInfoadd += "的" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "红包！<br />" + "额度是" + Eggsoft_Public_CL.Pub.getPubMoney(userGetMoney);
                    strMoneyInfoadd += "<br /><br /><a style=\"Color:white;text-decoration:underline;\" href=\"multibutton_showmoney_vouchers.aspx\">单击这里查看收益和使用方法</a>";
                    strMoneyInfoadd += "<br /><br /><br /><br /><br /><br /><br /><span class=\"MultiButton_ShowmoneyData_BigClassShow\">¥" + Eggsoft_Public_CL.Pub.getPubMoney(userGetMoney) + "</span>";
                }

            }
            else
            {
                strMoneyInfoadd = "<br /><br /><br /><br /><br /><br /><br />你来的太晚了，红包" + strGetFromMoneyDesc + "已抢完！";
            }
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

            string strIMGOpenOtherUrl = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MreadWallet" + (2 - intMoney_Credits_Vouchers) + "_Opened.jpg";


            string strMoneyInfo = "<div class=\"myIdClickIMG\"><div style=\"background-image:url('" + strIMGOpenOtherUrl + "');background-size:320px 380px;\"   class=\"IMGWallet_Open" + (intMoney_Credits_Vouchers) + "\">\n";
            strMoneyInfo += "<br />";
            strMoneyInfo += strMoneyInfoadd;
            strMoneyInfo += "<br />";
            strMoneyInfo += "</div></div>";
            return strMoneyInfo;
            //}


        }

        private String Init_maxMoney()
        {
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


            //userID = 38;
            string ThisShowID = Request.QueryString["thisshowid"];


            EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
            EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers Model_tab_RedWallet_Money_Credits = new EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers();

            Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(Int32.Parse(ThisShowID));


            string strMoneyInfo = "";


            System.Data.DataTable Data_DataTable = Bll_tab_RedWallet_Money_Credits.GetList("PID=" + ThisShowID + " order by id asc").Tables[0];
            string strIMGUrl = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MredWallet" + (2 - intMoney_Credits_Vouchers) + "_Share.jpg";
            strMoneyInfo += "<div style=\"background-image:url('" + strIMGUrl + "');background-size:320px 231px;\" class=\"IMGWallet" + intMoney_Credits_Vouchers + "\">\n";
            strMoneyInfo += "<br />";
            strMoneyInfo += Eggsoft_Public_CL.Pub.GetNickName(Model_tab_RedWallet_Money_Credits.UserID.ToString());
            strMoneyInfo += "<br />你发的红包总额是" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_RedWallet_Money_Credits.Money.toDecimal()) + "元";
            strMoneyInfo += "<br />你发的红包总个数是" + Model_tab_RedWallet_Money_Credits.HowmanyPeople + "个";
            strMoneyInfo += "<br />共有" + Data_DataTable.Rows.Count + "人收到你的红包";
            strMoneyInfo += "<br />亲，你可以发送给好友<br />或者分享到朋友圈。";
            strMoneyInfo += "</div>";

            int j = 0;
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                string strUserID = Data_DataTable.Rows[i]["UserID"].ToString();
                string strMoney = Data_DataTable.Rows[i]["Money"].ToString();

                if (String.IsNullOrEmpty(strUserID) == false)
                {

                    string strGetNickName = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                    if (String.IsNullOrEmpty(strGetNickName) == false)
                    {
                        if (strUserID == pub_Int_Session_CurUserID.toString())
                        {
                            strMoneyInfo += "<br />退回红包至账户 " + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strMoney)) + "元";
                        }
                        else
                        {
                            strMoneyInfo += "<br />" + strGetNickName + ",抢得" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strMoney)) + "元";
                        }
                        j++;
                    }
                }
            }
            if (j < Data_DataTable.Rows.Count)//有没有关注的啊  没办法 就这么表示吧
            {
                strMoneyInfo += "<br />...";
            }
            //}

            strMoneyInfo += "<div id=\"style_clorMMMMM\">\n";
            strMoneyInfo += "     <img src=\"/Images/mama.gif\">\n";
            strMoneyInfo += "</div>\n";



            return strMoneyInfo;

        }



        private string InitWeiXinShareLinkRedWallet(string strTemplet)
        {
            try
            {

                EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers Model_tab_RedWallet_Money_Credits = new EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers();
                string strThisShowID = Request.QueryString["thisshowid"];

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Modeltab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(Int32.Parse(strThisShowID));
                string strTitle = Modeltab_ShopClient.ShopClientName + ",";
                string strDes = "";
                string strFirstImageFullName = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Modeltab_ShopClient.UpLoadPath + "/QRCodeImage/MredWeBuy" + (2 - intMoney_Credits_Vouchers) + "_Icon.jpg";
                if (intMoney_Credits_Vouchers == 1)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "现金红包，总额" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_RedWallet_Money_Credits.Money.toDecimal()) + "元";
                    strDes = "微店现金红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "发放，亲，可以提取现金也可以转账";
                }

                else if (intMoney_Credits_Vouchers == 2)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "红包,总额" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_RedWallet_Money_Credits.Money.toDecimal()) + "元";
                    strDes = "微店" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "发放，亲，可以到" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "专区畅享购物";
                }
                #region 加签名
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(Model_tab_RedWallet_Money_Credits.UserID.toInt32());
                string strUserIDSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(Modeltab_User.SafeCode);
                string strSign = Eggsoft.Common.DESCrypt.hex_md5_8(strThisShowID + strUserIDSafeCode);
                strTemplet = strTemplet.Replace("###_sign###", strSign);
                #endregion 加签名

                strTemplet = strTemplet.Replace("###__ThisShowID###", strThisShowID);
                strTemplet = strTemplet.Replace("###__WeiXin_imgAllPageUrl###", strFirstImageFullName);
                strTemplet = strTemplet.Replace("###__WeiXin_descAppPageContent###", strDes);
                strTemplet = strTemplet.Replace("###__WeiXin_shareAppAllPageTitle###", strTitle);

                strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return strTemplet;

        }
    }
}