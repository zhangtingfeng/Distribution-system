using Eggsoft_Public_CL;
using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange
{
    public partial class Manage_28Member : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strBoardAsx = "Board_28Member.aspx";
        private static Object ObjectDelete = new Object();
        private static Object ObjectAddPhone = new Object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    lock (ObjectDelete)
                    {
                        EggsoftWX.BLL.tab_ShopClient_MemberCard bll = new EggsoftWX.BLL.tab_ShopClient_MemberCard();
                        EggsoftWX.Model.tab_ShopClient_MemberCard Model = bll.GetModel(int.Parse(ID));
                        if (Model.IsDeleted != 1)
                        {
                            Model.IsDeleted = 1;
                            Model.UpdateTime = DateTime.Now;
                            Model.UpdateBy = "删除操作" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                            bll.Update(Model);
                            //bll.Delete(Int32.Parse(ID));
                        }
                    }
                    JsUtil.ShowMsg("删除成功!", strBoardAsx);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass(sender, e);
                }
            }
        }

        private void SetClass(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                //string ID = Request.QueryString["ID"];// 修改ID
                //EggsoftWX.BLL.tab_MemberCard bll = new EggsoftWX.BLL.tab_MemberCard();
                //EggsoftWX.Model.tab_MemberCard Model = bll.GetModel(Int32.Parse(ID));

                //txtInputMoney.Text = Model.InputMoney.ToString();

                //btnAdd.Text = "保 存";
                btnAdd.Visible = false;

                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                // RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lock (ObjectAddPhone)
            {

                if (!Eggsoft.Common.IPPhone.IsHandset(TextBox_MemberMobile.Text))
                {
                    JsUtil.ShowMsg("手机号码错误!", -1);
                }

                if (String.IsNullOrEmpty(TextBox_YanZhengMaHide.Text))
                {
                    JsUtil.ShowMsg("请先点击发送验证码，并录入收到的验证码!", -1);
                }

                if (string.IsNullOrEmpty(TextBox_YanZhengMa.Text))
                {
                    JsUtil.ShowMsg("手机验证码必须录入!", -1);
                }



                string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                if (TextBox_YanZhengMa.Text == TextBox_YanZhengMaHide.Text)
                {

                    string type = Request.QueryString["type"];
                    if (type.ToLower() == "modify")
                    {
                        return;
                        //string ID = Request.QueryString["ID"];// 修改ID
                        //EggsoftWX.BLL.tab_MemberCard bll = new EggsoftWX.BLL.tab_MemberCard();
                        //EggsoftWX.Model.tab_MemberCard Model = bll.GetModel(Int32.Parse(ID));
                        //Model.PhoneNum = TextBox_MemberMobile.Text;
                        //Model.InputMoney = txtInputMoney.Text.toDecimal();
                        //EggsoftWX.Model.tab_MemberCardBonus Model_tab_MemberCardBonus = Eggsoft_Public_CL.PubMember.getBonusPolicy(strINCID.toInt32(), Model.InputMoney);
                        //if (Model_tab_MemberCardBonus != null)
                        //{
                        //    Model.BonusMoney = Model_tab_MemberCardBonus.BonusMoney;
                        //    Model.BonusGouWuQuan = Model_tab_MemberCardBonus.BonusGouWuQuan;
                        //    Model.BonusDesc = Model_tab_MemberCardBonus.BonusDesc + ",充值政策" + Model_tab_MemberCardBonus.ID;
                        //}
                        //Model.UpdateBy = Eggsoft_Public_CL.Pub.geLoginShow();


                        //Model.UpdateTime = DateTime.Now;

                        //bll.Update(Model);
                        //JsUtil.ShowMsg("修改成功!", strBoardAsx);

                    }
                    else if (type.ToLower() == "add")
                    {
                        #region 后端充值

                        EggsoftWX.BLL.tab_ShopClient_MemberCard bll = new EggsoftWX.BLL.tab_ShopClient_MemberCard();
                        EggsoftWX.Model.tab_ShopClient_MemberCard Model = new EggsoftWX.Model.tab_ShopClient_MemberCard();
                        Model.ShopClientID = Int32.Parse(strINCID);
                        Model.PhoneNum = TextBox_MemberMobile.Text;
                        Model.InputMoney = Decimal.Parse(txtInputMoney.Text);
                        Model.BankSeraillnum = TextBoxBankSeraillnum.Text;
                        EggsoftWX.Model.tab_ShopClient_MemberCardBonus Model_tab_MemberCardBonus = Eggsoft_Public_CL.PubMember.getBonusPolicy(Int32.Parse(strINCID), Model.InputMoney);

                        if (Model_tab_MemberCardBonus != null)
                        {
                            Model.BonusMoney = Model_tab_MemberCardBonus.BonusMoney;
                            Model.BonusGouWuQuan = Model_tab_MemberCardBonus.BonusGouWuQuan;
                            Model.BonusDesc = Model_tab_MemberCardBonus.BonusDesc + ",充值政策" + Model_tab_MemberCardBonus.ID;
                        }
                        else
                        {
                            Model.BonusDesc = "充值政策没有匹配成功";
                        }
                        Model.CreateBy = Eggsoft_Public_CL.Pub.geLoginShow();
                        Model.UpdateBy = Eggsoft_Public_CL.Pub.geLoginShow();
                        Model.UpdateTime = DateTime.Now;
                        bool boolIFSend = Eggsoft_Public_CL.PubMember.CardBonusChangeToUserAccount(Model);
                        Model.IfChangToWeiXinBonus = boolIFSend;

                        string strDEC = "添加成功!";
                        if (boolIFSend)
                        {
                            strDEC += "，已转化到账户";
                            Model.BonusDesc += "，已转化到账户";
                        }
                        else
                        {
                            strDEC += "，尚未转化，请提醒顾客绑定手机号";
                        }
                        bll.Add(Model);
                        #endregion 后端充值
                        JsUtil.ShowMsg(strDEC, strBoardAsx);
                    }
                }
                else
                {
                    JsUtil.ShowMsg("验证码错误需纠正,请重新录入你刚才收到的验证码!", -1);
                }
            }

        }



        protected void ButtonSendCode_Click(object sender, EventArgs e)
        {
            ButtonSendCode.Enabled = false;
            lock (ObjectAddPhone)
            {
                if (!Eggsoft.Common.IPPhone.IsHandset(TextBox_MemberMobile.Text))
                {
                    JsUtil.ShowMsg("手机号码错误!", -1);
                }



                string strvarGetAppConfiugServicesURL = Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL();
                string strvaruserMobilePhone = TextBox_MemberMobile.Text;
                string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                var urlasmx = strvarGetAppConfiugServicesURL + "/Other/CheckCode/WSCheck.asmx/doGameInfo_SendPhoneCode?PhoneNum=" + strvaruserMobilePhone + "&strShopClientID=" + strINCID;
                string strresult = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(urlasmx);
                Eggsoft.Common.debug_Log.Call_WriteLog(strresult, "验证码", "收到验证码");
                strresult = strresult.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "").Replace("<string xmlns=\"http://tempuri.org/\">", "").Replace("</string>", "");

                //string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/Other/WSCheck.asmx";
                //string[] args = new string[0];
                //object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "doGameInfo_SendPhoneCode", args);
                //string strresult = result.ToString();
                ErrorCodeClass ErrorCodeClassresult = Eggsoft.Common.JsonHelper.JsonDeserialize<ErrorCodeClass>(strresult);
                if (ErrorCodeClassresult.ErrorCode == "1" || ErrorCodeClassresult.ErrorCode == "2")
                {
                    Timer1SendCode.Enabled = true;
                    ButtonSendCode.Text = "验证码已发送60";
                    ButtonSendCode.Enabled = false;
                    TextBox_YanZhengMaHide.Text = ErrorCodeClassresult.SendCheckCode;
                    TextBox_YanZhengMa.Focus();
                }
                else if (ErrorCodeClassresult.ErrorCode == "-5")
                {
                    ButtonSendCode.Text = "余额不足,请联系1273844711@qq.com进行充值";
                    ButtonSendCode.Enabled = false; ;
                    TextBox_YanZhengMaHide.Text = ErrorCodeClassresult.ErrorCode + "发送失败";
                    //JsUtil.ShowMsg("余额不足,请联系1273844711@qq.com进行充值!", -1);
                }
                else if (ErrorCodeClassresult.ErrorCode == "-6")
                {
                    ButtonSendCode.Text = "可能是短信模板尚未配置,请联系1273844711@qq.com进行配置";
                    ButtonSendCode.Enabled = false; ;
                    TextBox_YanZhengMaHide.Text = ErrorCodeClassresult.ErrorCode + "发送失败";
                    //JsUtil.ShowMsg("余额不足,请联系1273844711@qq.com进行充值!", -1);
                }
                else
                {
                    ButtonSendCode.Text = "发送失败";
                    ButtonSendCode.Enabled = true; ;
                    TextBox_YanZhengMaHide.Text = ErrorCodeClassresult.ErrorCode + "发送失败";
                }
            }
        }

        protected void Timer1SendCode_Tick(object sender, EventArgs e)
        {
            int AllCount = 60;

            AllCount = int.Parse(ButtonSendCode.Text.Replace("验证码已发送", ""));
            AllCount--;
            if (AllCount > 0)
            {
                ButtonSendCode.Text = "验证码已发送" + AllCount.ToString();
            }
            if (AllCount <= 0)
            {
                Timer1SendCode.Enabled = false;
                ButtonSendCode.Text = "发送验证码";
                ButtonSendCode.Enabled = true;
            }
        }
    }

    public class ErrorCodeClass
    {
        public string ErrorCode { get; set; }
        public string SendCheckCode
        {
            get; set;
        }

    }
}