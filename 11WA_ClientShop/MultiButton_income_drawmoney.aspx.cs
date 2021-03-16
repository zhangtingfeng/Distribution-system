using Eggsoft.Common;
using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_income_drawmoney : System.Web.UI.Page
    {
        private static object myiwantdrawmoney = new Object();
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



                    string type = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(type) == false && type == "iwantdrawmoney")
                    {



                        lock (myiwantdrawmoney)
                        {
                            #region 节假日提现，该时间段才能提现，请注意商城公告
                            string infoMoneyTipInfo = "";
                            bool ifCanDrawMoney = false;
                            ifCanDrawMoney = ifFunctionCanDrawMoney(pub_Int_ShopClientID, out infoMoneyTipInfo);
                            if (!ifCanDrawMoney)
                            {
                                Eggsoft.Common.JsUtil.ShowMsg(infoMoneyTipInfo, "javascript:history.back()");
                                return;
                            }
                            #endregion

                            #region  是否有工作日限制
                            bool tContinue = false;
                            bool boolOnlyBankTime = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.toString(), "OnlyBankTime");//银行工作日
                            if (boolOnlyBankTime)
                            {
                                DateTime tDateTime = DateTime.Now;
                                Eggsoft.Common.HolidayHelper myHolidayHelper = Eggsoft.Common.HolidayHelper.GetInstance();
                                bool boolisWorkDay = (myHolidayHelper.isWorkDay(tDateTime));
                                if (boolisWorkDay)
                                {
                                    DateTime t0930 = new DateTime(tDateTime.Year, tDateTime.Month, tDateTime.Day, 9, 30, 0);
                                    DateTime t1630 = new DateTime(tDateTime.Year, tDateTime.Month, tDateTime.Day, 16, 30, 0);

                                    if ((tDateTime >= t0930) && (tDateTime <= t1630))
                                    {
                                        tContinue = true;
                                    }
                                }
                            }
                            else
                            {
                                tContinue = true;
                            }

                            if (tContinue == false)
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("规定的工作日9：30 到16：30才能提现", "javascript:history.back()");
                                return;
                            }
                            #endregion
                            #region 处理申请提现
                            if (ifCanDrawMoney)
                            {
                                EggsoftWX.BLL.tab_User_AskGetMoney my_BLL_tab_User_AskGetMoney = new EggsoftWX.BLL.tab_User_AskGetMoney();
                                EggsoftWX.Model.tab_User_AskGetMoney my_Model_tab_User_AskGetMoney = new EggsoftWX.Model.tab_User_AskGetMoney();

                                Decimal DecUserMoney = 0;


                                String strUserRealName = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserRealName"]);
                                String strUserMoney = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserMoney"]);
                                String UserPhone = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserPhone"]);
                                String UserExtraMemo = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserExtraMemo"]);
                                String JSUserSign = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["JSUserSign"]);

                                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User my_Model_tab_User = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                                #region 检查签名
                                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserRealName + strUserMoney + UserPhone + UserExtraMemo + Eggsoft.Common.DESCrypt.hex_md5_2(my_Model_tab_User.SafeCode));
                                if (JSUserSign != strNetSign)
                                {
                                    Eggsoft.Common.debug_Log.Call_WriteLog("Form UserRealName+ UserMoney+ UserPhone+ UserExtraMemo" + Request.Form["UserRealName"] + " " + Request.Form["UserMoney"] + " " + Request.Form["UserPhone"] + Request.Form["UserExtraMemo"] + " " + my_Model_tab_User.SafeCode, "提现签名失败", "pub_Int_Session_CurUserID=" + pub_Int_Session_CurUserID);
                                    Eggsoft.Common.debug_Log.Call_WriteLog("strUserRealName+strUserMoney+UserPhone+UserExtraMemo" + strUserRealName + " " + strUserMoney + " " + UserPhone + UserExtraMemo + " " + my_Model_tab_User.SafeCode, "提现签名失败", "pub_Int_Session_CurUserID=" + pub_Int_Session_CurUserID);
                                    Eggsoft.Common.JsUtil.ShowMsg("签名失败,微店将很快联系您！", "javascript:history.back()");
                                    return;
                                }
                                #endregion 检查签名

                                Decimal.TryParse(strUserMoney, out DecUserMoney);
                                EggsoftWX.BLL.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_bll = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                                EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = new EggsoftWX.Model.tab_ShopClient_ShopPar();
                                tab_ShopClient_ShopPar_Model = tab_ShopClient_ShopPar_bll.GetModel("ShopClientID=" + pub_Int_ShopClientID);


                                #region 检查21的提现权限
                                if (pub_Int_ShopClientID == 21)
                                {
                                    EggsoftWX.BLL.b005_UserID_Operation_ID BLLb005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                                    Boolean BooleanActiveAccount = BLLb005_UserID_Operation_ID.Exists("UserID=@UserID and ShopClientID=@ShopClientID and ActiveAccount=1", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                                    if (BooleanActiveAccount == false)
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("微店账户未激活", "javascript:history.back()");
                                        return;
                                    }
                                }
                                #endregion 检查21的提现权限

                                Decimal LimitMoney_AskMoney = 0;
                                if (tab_ShopClient_ShopPar_Model != null)
                                {
                                    LimitMoney_AskMoney = tab_ShopClient_ShopPar_Model.LimitMoney_AskMoney.toDecimal();
                                }

                                if (DecUserMoney < LimitMoney_AskMoney || DecUserMoney <= 0)
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("微店默认的最小申请提款额度是" + Eggsoft_Public_CL.Pub.getPubMoney(LimitMoney_AskMoney), "javascript:history.back()");
                                    return;
                                }
                                else
                                {

                                    string strWhere = "UserID=" + pub_Int_Session_CurUserID + " and AskMoney=" + DecUserMoney + " and CardName='" + UserPhone.Trim() + "' and isnull(AskMemo,'')='" + UserExtraMemo.Trim() + "' and isnull(IFSendMoney,0)=0 and DateDiff(dd,CreatTime,getdate())=0";
                                    if (my_BLL_tab_User_AskGetMoney.Exists(strWhere))
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("请勿重复提交,微店将很快联系您！", "javascript:history.back()");
                                        return;
                                    }
                                    else
                                    {



                                        #region 提现频率限制单位
                                        string strLimitMoney_PresentFrequency = Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.toString(), "LimitMoney_PresentFrequency").toString();///提现频率限制
                                        if (string.IsNullOrEmpty(strLimitMoney_PresentFrequency)) strLimitMoney_PresentFrequency = "Unlimited";///默认不限制 提现频率限制单位
                                        string strSQLShouldAdd = ""; string strSQLShouldAddAlertShow = "";
                                        switch (strLimitMoney_PresentFrequency)
                                        {
                                            case "Unlimited":
                                                break;
                                            case "Hourly":
                                                strSQLShouldAddAlertShow = "每时";
                                                strSQLShouldAdd = " and datediff(hh,[CreatTime],getdate())=0 ";
                                                break;
                                            case "Daily":
                                                strSQLShouldAddAlertShow = "每时";
                                                strSQLShouldAdd = " and datediff(dd,[CreatTime],getdate())=0 ";
                                                break;
                                            case "Weekly":
                                                strSQLShouldAddAlertShow = "每天";
                                                strSQLShouldAdd = " and datediff(wk,[CreatTime],getdate())=0 ";
                                                break;
                                            case "Monthly":
                                                strSQLShouldAddAlertShow = "每周";
                                                strSQLShouldAdd = " and datediff(mm,[CreatTime],getdate())=0 ";
                                                break;
                                            case "Quarterly":
                                                strSQLShouldAddAlertShow = "每季度";
                                                strSQLShouldAdd = " and datediff(qq,[CreatTime],getdate())=0 ";
                                                break;
                                            case "Annually":
                                                strSQLShouldAddAlertShow = "每年";
                                                strSQLShouldAdd = " and datediff(yyyy,[CreatTime],getdate())=0 ";
                                                break;

                                        }
                                        #endregion 提现频率限制单位


                                        #region 是否限制每天提款金额
                                        Boolean BooleanLimitMoney_MAX = false; Decimal DecimalHavedGOT = 0;
                                        Decimal LimitMoney_MAX_AskMoney = Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.toString(), "LimitMoney_MAX").toDecimal();
                                        if (LimitMoney_MAX_AskMoney > 0 && String.IsNullOrEmpty(strSQLShouldAdd) == false)
                                        {
                                            string strWhereLimitMaxMoney_OnceEveryDay = "SELECT sum(AskMoney) FROM [tab_User_AskGetMoney] WHERE  UserID = " + pub_Int_Session_CurUserID + strSQLShouldAdd + "    and isnull(IFSendMoney,0)=1";
                                            DecimalHavedGOT = my_BLL_tab_User_AskGetMoney.SelectList(strWhereLimitMaxMoney_OnceEveryDay).Tables[0].Rows[0][0].toDecimal();
                                            BooleanLimitMoney_MAX = (DecimalHavedGOT + DecUserMoney) > LimitMoney_MAX_AskMoney;
                                        }
                                        #endregion 是否限制每天提款金额

                                        #region 是否限制提现次数
                                        Boolean BooleanIFLimitEveryDay = false; int intCount = 0;
                                        Int32 LimitMoney_OnceEveryDay = Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.toString(), "LimitMoney_OnceEveryDay").toInt32();
                                        if (LimitMoney_OnceEveryDay > 0)
                                        {
                                            string strWhereLimitMoney_OnceEveryDay = "SELECT count(1) FROM [tab_User_AskGetMoney] WHERE  UserID = " + pub_Int_Session_CurUserID + strSQLShouldAdd + " and isnull(IFSendMoney,0)=1";
                                            intCount = my_BLL_tab_User_AskGetMoney.SelectList(strWhereLimitMoney_OnceEveryDay).Tables[0].Rows[0][0].toInt32();
                                            BooleanIFLimitEveryDay = intCount >= LimitMoney_OnceEveryDay;
                                        }
                                        #endregion 是否限制提现次数



                                        if (BooleanLimitMoney_MAX)
                                        {
                                            Eggsoft.Common.JsUtil.ShowMsg("微店默认的" + strSQLShouldAddAlertShow + "最大提款金额是" + Eggsoft_Public_CL.Pub.getPubMoney(LimitMoney_MAX_AskMoney) + ",已提" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalHavedGOT) + ",请下次再来", "javascript:history.back()");
                                            return;
                                        }
                                        else if (BooleanIFLimitEveryDay)
                                        {
                                            Eggsoft.Common.JsUtil.ShowMsg("微店默认的" + strSQLShouldAddAlertShow + "提现次数是" + LimitMoney_OnceEveryDay + "  已提" + intCount + ",请下次再来", "javascript:history.back()");
                                            return;
                                        }

                                        else
                                        {
                                            #region 多线程 检查余额
                                            Decimal myAllSonemoney = 0;
                                            Eggsoft_Public_CL.Pub_FenXiao.DoOver7daysCountMySonMoney_Then_CountyuEArgMoney(pub_Int_Session_CurUserID, out myAllSonemoney);
                                            if (DecUserMoney <= myAllSonemoney)
                                            {
                                                my_Model_tab_User.ContactPhone = UserPhone;
                                                my_Model_tab_User.UserRealName = strUserRealName;
                                                my_Model_tab_User.Updatetime = DateTime.Now;
                                                my_BLL_tab_User.Update(my_Model_tab_User);
                                                my_Model_tab_User_AskGetMoney.userRealName = strUserRealName;
                                                my_Model_tab_User_AskGetMoney.UserID = pub_Int_Session_CurUserID;
                                                my_Model_tab_User_AskGetMoney.AskMoney = DecUserMoney;
                                                my_Model_tab_User_AskGetMoney.CardName = UserPhone;
                                                my_Model_tab_User_AskGetMoney.AskMemo = UserExtraMemo;
                                                int intAddMoney = my_BLL_tab_User_AskGetMoney.Add(my_Model_tab_User_AskGetMoney);





                                                bool boolIfSystemDone = false;///系统是否已经处理过
                                                EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                                                EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + pub_Int_ShopClientID);
                                                if (Model_tab_ShopClient_ShopPar.GiveMoneyAfterOntime.toBoolean())///马上转账
                                                {
                                                    string strTransNo = DateTime.Now.ToString("yyyyMMdd") + Eggsoft.Common.StringNum.Add000000Num(intAddMoney, 8);
                                                    Eggsoft_Public_CL.WXRed myWXRed = new Eggsoft_Public_CL.WXRed();
                                                    string strresult_code = ""; string strpayment_no = ""; string strerr_code_des = "";
                                                    myWXRed.sendMoney(pub_Int_Session_CurUserID, strTransNo, DecUserMoney, out strresult_code, out strpayment_no, out strerr_code_des);////调用立即转账的程序
                                                    if (strresult_code == "success" && String.IsNullOrEmpty(strpayment_no) == false)
                                                    {
                                                        boolIfSystemDone = true;
                                                        my_BLL_tab_User_AskGetMoney.Update("IFSendMoney=1,payment_no=@payment_no,UpdateTime=@UpdateTime,ResultCode=@ResultCode", "ID=@ID", strpayment_no, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "成功", intAddMoney);

                                                        if (Decimal.Round(DecUserMoney, 2) > 0)
                                                        {
                                                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                                                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 130;
                                                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecUserMoney;
                                                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "官方微信现金零钱转账" + strpayment_no;
                                                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
                                                            int intADD = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                                                            if (!(intADD > 0))
                                                            {
                                                                Eggsoft.Common.debug_Log.Call_WriteLog("strUserRealName+strUserMoney+UserPhone+UserExtraMemo" + Request.Form["UserRealName"] + " " + Request.Form["UserMoney"] + " " + Request.Form["UserPhone"] + Request.Form["UserExtraMemo"] + " " + my_Model_tab_User.SafeCode, "提现发放成功,记录失败,现金严重错误", "pub_Int_Session_CurUserID=" + pub_Int_Session_CurUserID);
                                                                Eggsoft.Common.JsUtil.ShowMsg("发放成功,记录失败" + strpayment_no, "javascript:history.back()");
                                                            }
                                                        }
                                                        Eggsoft.Common.JsUtil.ShowMsg("已发放现金零钱，请查收！流水号为" + strpayment_no, "/mywebuy.aspx");
                                                    }
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(strerr_code_des)) { strerr_code_des = "可能是微信支付权限不足"; }
                                                        else
                                                        {
                                                            strerr_code_des += "  平台已记录你的数据，将人工继续处理，请稍待";
                                                        }
                                                        my_BLL_tab_User_AskGetMoney.Update("IFSendMoney=0,payment_no=@payment_no,UpdateTime=@UpdateTime,ResultCode=@ResultCode", "ID=@ID", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strerr_code_des, intAddMoney);
                                                        Eggsoft.Common.JsUtil.ShowMsg(strerr_code_des, "javascript:history.back()");
                                                    }
                                                }

                                                if (boolIfSystemDone == false)////移动到这里 提款成功 就不发送了 否则发送邮件太多
                                                {
                                                    string strInfoInfo = strUserRealName + "，昵称：" + Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "";
                                                    string strSubject = "微店" + " 用户提款通知！" + strInfoInfo;
                                                    string strBody = "你好，我们给你发信，是因为" + strInfoInfo + "通知引起！" + "\n";
                                                    strBody += "申请额度：" + strUserMoney + "  \n";
                                                    strBody += "用户电话：" + UserPhone + "  \n";
                                                    strBody += "附加信息：" + UserExtraMemo + "  \n";

                                                    EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                                                    EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_User.ShopClientID));
                                                    if (String.IsNullOrEmpty(my_Model_tab_ShopClient.XML) == false)
                                                    {
                                                        XML__Class_Shop_Client myXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(my_Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);

                                                        if (myXML__Class_Shop_Client.CheckEmail == true)
                                                        {
                                                            string strTo = myXML__Class_Shop_Client.Email;
                                                            string strClientAdminURL = System.Configuration.ConfigurationManager.AppSettings["ClientAdminURL"];
                                                            strBody += "请点击如下的连接进行处理。如果不能点击，请复制如下连接到浏览器地址栏！" + "\n";
                                                            strBody += strClientAdminURL + "/ClientAdmin/12UserAskMoney/IS_UserFinance_check_DrawMoney.aspx";
                                                            Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName + "微店", strTo, strSubject, strBody);
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion 多线程 检查余额；///////////////////////////////////////////////////////////////

                                        }
                                        Eggsoft.Common.JsUtil.ShowMsg("已成功提交，微店将很快联系您！", "/mywebuy.aspx");
                                    }
                                    ////////             }
                                }
                            }
                            #endregion 处理申请提现
                        }
                    }

                    else
                    {
                        string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_income_drawmoney_Templet.html");
                        strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                        strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "AfterShareContinuesAskDrawMoney");//申请提现的 回调事件
                        strTemplet = strTemplet.Replace("###Header###", "");
                        strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "申请提款"));

                        EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                        string strShowText = Eggsoft_Public_CL.Pub.GetstringShowPower_AgentShopTextDesc(pub_Int_ShopClientID.ToString()) + " " + Model_tab_ShopClient.ShopClientName + "  申请提款";
                        strTemplet = strTemplet.Replace("###WeiXin_descAppPageContent###", strShowText);
                        strTemplet = strTemplet.Replace("###WeiXin_shareAppAllPageTitle###", strShowText);


                        #region 分享朋友圈描述



                        #endregion 分享朋友圈描述

                        #region 查看单位订单 是否需要分享朋友圈 才能完成提现
                        string strSQLShow = @"SELECT UserID,ID
  FROM [b008_OpterationUserActiveReturnMoneyOrderNum] where  ShopClient_ID=@ShopClient_ID and UserID=@UserID and isnull(OrderDetailID,0)>0 and isnull(ActiveOrderNum,0)=0 
union 
SELECT UserID,ID
  FROM [b008_OpterationUserActiveReturnMoneyOrderNum] where ShopClient_ID=@ShopClient_ID and UserID=@UserID and ActiveOrderNum>0 and isnull(OrderDetailID,0)>0 and ReturnMoneyUnit/ActiveOrderNum <1800 ";
                        EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        int intShow = BLL_tab_ShopClient_Agent_.SelectList(strSQLShow, pub_Int_ShopClientID, pub_Int_Session_CurUserID).Tables[0].Rows.Count;

                        string strSQLNotShow = @"
SELECT UserID,ID
  FROM [b008_OpterationUserActiveReturnMoneyOrderNum] where ShopClient_ID=@ShopClient_ID and UserID=@UserID and isnull(OrderDetailID,0)>0 and ActiveOrderNum>0 and ReturnMoneyUnit/ActiveOrderNum >1800";
                        int intNotShow = BLL_tab_ShopClient_Agent_.SelectList(strSQLNotShow, pub_Int_ShopClientID, pub_Int_Session_CurUserID).Tables[0].Rows.Count;


                        if (intShow > 0 && intNotShow == 0)
                        {
                            strTemplet = strTemplet.Replace("###UserDrawMoneyShareFriend###", "1");
                        }
                        else
                        {
                            strTemplet = strTemplet.Replace("###UserDrawMoneyShareFriend###", (Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "UserDrawMoneyShareFriend") ? "1" : "0"));
                        }
                        #endregion 查看单位订单 是否需要分享朋友圈 才能完成提现


                        #region ###Now_Draw_Money_AgentInfo###
                        EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID);
                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);

                        string strPhone = "";
                        if (string.IsNullOrEmpty(Model_tab_User.ContactPhone))
                        {

                        }
                        else
                        {
                            if (Model_tab_User.ContactPhone.ToLower() == "undefined")
                            {
                                strPhone = "";
                            }
                            else
                            {
                                strPhone = Model_tab_User.ContactPhone;
                            }
                        }

                        strTemplet = strTemplet.Replace("###Now_Draw_Money_AgentInfo###", strPhone);
                        #endregion


                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                        string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                        strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);

                        #region 请输入微信真实用户名，银行需要
                        strTemplet = strTemplet.Replace("###Now_Draw_MoneyUserRealName###", Model_tab_User.UserRealName);
                        #endregion 请输入微信真实用户名，银行需要

                        strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());
                        strTemplet = strTemplet.Replace("###NetUserSafeCode###", Eggsoft.Common.DESCrypt.hex_md5_2(Model_tab_User.SafeCode));

                        strTemplet = Init_maxMoney(strTemplet);

                        EggsoftWX.BLL.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_bll = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                        EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = new EggsoftWX.Model.tab_ShopClient_ShopPar();
                        tab_ShopClient_ShopPar_Model = tab_ShopClient_ShopPar_bll.GetModel("ShopClientID=" + pub_Int_ShopClientID);
                        Decimal LimitMoney_AskMoney = 0;
                        if (tab_ShopClient_ShopPar_Model != null)
                        {
                            LimitMoney_AskMoney = tab_ShopClient_ShopPar_Model.LimitMoney_AskMoney.toDecimal();
                        }
                        strTemplet = strTemplet.Replace("###Min_Draw_Money###", Eggsoft_Public_CL.Pub.getPubMoney(LimitMoney_AskMoney));


                        #region 消费财富协议
                        if (pub_Int_ShopClientID == 21)
                        {
                            ////如果用户成功提现过那也不显示
                            //EggsoftWX.BLL.tab_User_AskGetMoney BLL_tab_User_AskGetMoney = new EggsoftWX.BLL.tab_User_AskGetMoney();
                            //bool boolTenMonth = BLL_tab_User_AskGetMoney.Exists("UserID=@UserID and IFSendMoney = 1 and creattime>= '2017-10-01'", pub_Int_Session_CurUserID);
                            //if (boolTenMonth)////10月份以来的成功提现过的  肯定都出现过 就不显示了
                            //{
                            //    strTemplet = strTemplet.Replace("###boolShowConsumerWealthAgreement###", "0");
                            //}
                            //else
                            //{
                            EggsoftWX.BLL.b004_OperationGoods BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                            EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = BLL_b004_OperationGoods.GetModel(1);


                            strTemplet = strTemplet.Replace("###ConsumerWealthNeedToKnow###", Server.HtmlDecode(Model_b004_OperationGoods.ConsumerWealthDrawMoney.ToString()));
                            strTemplet = strTemplet.Replace("###ConsumerWealthAgreementGoodID###", Model_b004_OperationGoods.DiscountGoodID.toString());

                            EggsoftWX.BLL.b005_UserID_Operation_ID BLL_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                            EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=21", pub_Int_Session_CurUserID);
                            if (Model_b005_UserID_Operation_ID != null)
                            {
                                strTemplet = strTemplet.Replace("###boolShowConsumerWealthAgreement###", (!String.IsNullOrEmpty(Model_b004_OperationGoods.ConsumerWealthDrawMoney.toString())).toBoolean().toInt32().toString());
                            }
                            else
                            {
                                strTemplet = strTemplet.Replace("###boolShowConsumerWealthAgreement###", "0");
                            }
                            //}
                        }
                        else
                        {
                            strTemplet = strTemplet.Replace("###boolShowConsumerWealthAgreement###", "0");
                        }
                        #endregion 消费财富协议


                        Response.Write(strTemplet);
                    }
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "提现");
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
        private String Init_maxMoney(String strargBody)
        {

            string strMaxMoneyInfo = "";
            string typemaxMoney = Request.QueryString["maxMoney"];
            strMaxMoneyInfo += "微店号：" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString()) + "<br />昵称：" + Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "<br /> 亲：你好，你的可提现额度是¥<span id=\"myCanDrawMoneyVar\">" + typemaxMoney + "</span><br/><br/>";
            strMaxMoneyInfo += "<span>提现操作步骤！</span><br/>";
            strMaxMoneyInfo += "<span>1.亲，输入如下信息，提交即可，您只需坐等。<br/>";
            strMaxMoneyInfo += "<span>2.微店将会努力工作，加紧处理，并会主动联系您。<br/>";
            strMaxMoneyInfo += "<span>3.微店官方微信会在2小时内转账给你.<br/>";
            strMaxMoneyInfo += "<span>4.如涉及到国家税务政策，将代扣税点.<br/>";
            if (String.IsNullOrEmpty(typemaxMoney)) typemaxMoney = "0";

            strargBody = strargBody.Replace("###MaxMoneyInfo###", strMaxMoneyInfo);
            string strCanWriteMoney = "";
            Decimal mytypemaxMoney = Decimal.Parse(typemaxMoney);
            if (mytypemaxMoney >= 200)
            {
                strCanWriteMoney = "200";
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
            return strargBody;

        }


        private bool ifFunctionCanDrawMoney(int intShopClientID, out string infoMoneyTipInfo)
        {

            infoMoneyTipInfo = "";

            try
            {

                string strShopClientIDShow = "ShopClient" + intShopClientID.toString();

                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                string strReadjson1 = Eggsoft.Common.JsonHelper.GetFileJson("~/MultiButton_income_drawmoney.json");
                dynamic modelDy = js.Deserialize<dynamic>(strReadjson1);
                bool boolExsit = ((IDictionary<string, object>)modelDy).ContainsKey(strShopClientIDShow);
                if (boolExsit)////说明存在限制条件
                {
                    string strState = modelDy[strShopClientIDShow]["State"];
                    DateTime? DateTimeTimeStart = ((String)modelDy[strShopClientIDShow]["TimeStart"]).toDateTime();
                    DateTime? DateTimeTimeEnd = ((String)modelDy[strShopClientIDShow]["TimeEnd"]).toDateTime();
                    String strInfo = modelDy[strShopClientIDShow]["Info"];
                    infoMoneyTipInfo = strInfo + " 开始时间:" + modelDy[strShopClientIDShow]["TimeStart"] + " 结束时间:" + modelDy[strShopClientIDShow]["TimeEnd"];


                    if (strState == "Pause")///维护期间，该时间段暂停提现，请注意商城公告
                    {////Normal  表示不暂停

                        if ((DateTimeTimeStart <= DateTime.Now && DateTimeTimeEnd >= DateTime.Now))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (strState == "Normal")
                    {////表示这个时间段才能提现
                        if (!(DateTimeTimeStart <= DateTime.Now && DateTimeTimeEnd >= DateTime.Now))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
            }
            catch (Exception ddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddd, "提现文件读取错误");
                infoMoneyTipInfo = "出现传输错误，请稍后重拾";
                return false;
            }
            return true;
        }

    }
}