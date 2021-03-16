using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._16SendMoney
{
    public partial class Net16SendMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strTextbox_Timer0_Text = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
        public string strTextbox_Timer1_Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");

        protected string pub__addGoodAndGoodClassShortUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetClass();
                EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag BLL_tab_ShopClient_SendMoneyByRedBag = new EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag();

                string type = Request.QueryString["type"];
                if (type == "Add")
                {

                }
                else if ((type == "Modify") || (type == "ModifyCopy"))
                {
                    string ModifyID = Request.QueryString["SendBagID"];
                    EggsoftWX.Model.tab_ShopClient_SendMoneyByRedBag Model_tab_ShopClient_SendMoneyByRedBag = BLL_tab_ShopClient_SendMoneyByRedBag.GetModel(Int32.Parse(ModifyID));
                    Textbox_Content.Text = Model_tab_ShopClient_SendMoneyByRedBag.MsgTypeNewsDescription;
                    MsgTypeNewsTitle.Text = Model_tab_ShopClient_SendMoneyByRedBag.MsgTypeNewsTitle;
                    RadioButtonList_SendToType.SelectedValue = Model_tab_ShopClient_SendMoneyByRedBag.SendToType;
                    strTextbox_Timer0_Text = Model_tab_ShopClient_SendMoneyByRedBag.ValidStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                    strTextbox_Timer1_Text = Model_tab_ShopClient_SendMoneyByRedBag.ValidEndTime.ToString("yyyy-MM-dd HH:mm:ss");

                    EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers BLL_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                    EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers Model_tab_RedWallet_Money_Credits_Vouchers = BLL_tab_RedWallet_Money_Credits_Vouchers.GetModel("SendMoneyByRedBagID=" + ModifyID);

                    RadioButtonList_RedType.SelectedValue = Model_tab_RedWallet_Money_Credits_Vouchers.Type_Or_Money_Credits_Vouchers.ToString();
                    Textbox_Price.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_RedWallet_Money_Credits_Vouchers.Money.toDecimal());
                    Textbox_HowMany.Text = Model_tab_RedWallet_Money_Credits_Vouchers.HowmanyPeople.ToString();


                }
                else if (type.ToLower() == "delete")
                {
                    string strSendBagID = Request.QueryString["SendBagID"];
                    if (!CommUtil.IsNumStr(strSendBagID))
                        MyError.ThrowException("传递参数错误!");
                    BLL_tab_ShopClient_SendMoneyByRedBag.Delete("ID=" + strSendBagID);

                    EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers BLL_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                    BLL_tab_RedWallet_Money_Credits_Vouchers.Delete("SendMoneyByRedBagID=" + strSendBagID);

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("删除成功!", strCallBackUrl);
                }
            }
        }


        private void SetClass()
        {

            ///  先列出 所有的 关注的 。扣除 代理商（必须关注状态）   扣除 付过钱的（必须关注状态） 就是 不是的

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            ArrayList ArrayListA; ArrayList ArrayListB; ArrayList ArrayListC;
            Eggsoft_Public_CL.Pub.SendRed_Get_ABC___Task(strShopClientID, out ArrayListA, out ArrayListB, out ArrayListC);

            ListItem myListItem = new ListItem();
            myListItem.Text = "代理商(" + ArrayListA.Count + ")";
            myListItem.Value = "A";
            RadioButtonList_SendToType.Items.Add(myListItem);

            ListItem myListItem1 = new ListItem();
            myListItem1.Text = "已购买(" + ArrayListB.Count + ")";
            myListItem1.Value = "B";
            RadioButtonList_SendToType.Items.Add(myListItem1);

            ListItem myListItem2 = new ListItem();
            myListItem2.Text = "已关注(" + ArrayListC.Count + ")"; ;
            myListItem2.Value = "C";
            RadioButtonList_SendToType.Items.Add(myListItem2);

            RadioButtonList_SendToType.SelectedIndex = 0;
        }


        protected void Button_Save_Click(object sender, EventArgs e)
        {
            int intThisShowID = 0;
            string strMsgTypeNewsTitle = "";
            string strMsgTypeNewsDescription = "";

            save(false, out intThisShowID, out strMsgTypeNewsTitle, out strMsgTypeNewsDescription);

            string strCallBackUrl = Request.QueryString["CallBackUrl"];
            strCallBackUrl = strCallBackUrl.Replace("*", "?");
            JsUtil.ShowMsg("保存成功!", strCallBackUrl);

        }
        protected void Button_Save_Send_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            EggsoftWX.BLL.tab_ShopClient_EngineerMode bll_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = bll_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);
            string strLiBaoLingQuTongZhi = Model_tab_ShopClient_EngineerMode.LiBaoLingQuTongZhi;
            if (String.IsNullOrEmpty(strLiBaoLingQuTongZhi))
            {
                Eggsoft.Common.JsUtil.ShowMsg("不存在行业IT科技 - 互联网|电子商务 ，模板ID 礼包领取通知", -1);

                return;
            }
            int intThisShowID = 0;
            string strMsgTypeNewsTitle = "";
            string strMsgTypeNewsDescription = "";

            int intSendMoneyByRedBag = save(true, out intThisShowID, out strMsgTypeNewsTitle, out strMsgTypeNewsDescription);
            if (intSendMoneyByRedBag > 0)
            {
                string strACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(Int32.Parse(strShopClientID), true);



                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(strShopClientID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                string strHref = "https://" + strErJiYuMing + "/midsmf.aspx?thisshowid=" + intThisShowID + "&onlymegetuserid=###userid###";


                //for (int i = 0; i < 10000; i++)
                //{
                String strSendURL = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + strACCESS_TOKEN;
                string strJSON = "";
                strJSON += "        {\n";
                strJSON += "\"touser\":\"###openid###\",\n";
                strJSON += "\"template_id\":\"" + strLiBaoLingQuTongZhi + "\",\n";
                strJSON += "\"url\":\"" + strHref + "\",\n";
                strJSON += "\"topcolor\":\"#FF0000\",\n";
                strJSON += "\"data\":{\n";
                strJSON += "\"first\": {\n";
                strJSON += "\"value\":\"" + strMsgTypeNewsTitle + "\",\n";
                strJSON += "\"color\":\"#173177\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword1\":{\n";
                strJSON += "\"value\":\"礼包编号" + intSendMoneyByRedBag + "\",\n";
                strJSON += "\"color\":\"#173177\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword2\":{\n";
                strJSON += "\"value\":\"###NickName###\",\n";
                strJSON += "\"color\":\"#173177\"\n";
                strJSON += "},\n";
                strJSON += "\"remark\":{\n";
                strJSON += "\"value\":\"" + strMsgTypeNewsDescription + "\",\n";
                strJSON += "\"color\":\"#173177\"\n";
                strJSON += "}\n";
                strJSON += "}\n";
                strJSON += "}\n";



                ///String strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);
                //}
                string strArrayListABC = "";
                if (CheckBox_Test.Checked)
                {
                    EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();

                    string strShopID = TextBox_Test.Text.Trim();
                    int intShopID = 0;
                    int.TryParse(strShopID, out intShopID);
                    if (intShopID > 0)
                    {
                        EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel("ShopClientID=" + strShopClientID + " and ShopUserID=" + intShopID);
                        if (Model_tab_User != null)
                        {
                            strArrayListABC = Model_tab_User.ID.ToString();
                        }
                    }

                }
                else
                {
                    ArrayList ArrayListA; ArrayList ArrayListB; ArrayList ArrayListC;
                    Eggsoft_Public_CL.Pub.SendRed_Get_ABC___Task(strShopClientID, out ArrayListA, out ArrayListB, out ArrayListC);
                    ArrayList ArrayListABC = new ArrayList(); ;
                    if (RadioButtonList_SendToType.SelectedValue == "A")
                    {
                        ArrayListABC = ArrayListA;
                    }
                    else if (RadioButtonList_SendToType.SelectedValue == "B")
                    {
                        ArrayListABC = ArrayListB;
                    }
                    else if (RadioButtonList_SendToType.SelectedValue == "C")
                    {
                        ArrayListABC = ArrayListC;
                    }
                    for (int i = 0; i < ArrayListABC.Count; i++)
                    {
                        strArrayListABC += ArrayListABC[i] + ",";
                    }
                    strArrayListABC = strArrayListABC.Remove(strArrayListABC.Length - 1);
                }


                Eggsoft.Common.ClassWeiXin_Task myXML_Class = new Eggsoft.Common.ClassWeiXin_Task();
                myXML_Class.SendURL = strSendURL;
                myXML_Class.JSON = strJSON;
                string strAndOther = Eggsoft.Common.XmlHelper.XmlSerialize(myXML_Class, System.Text.Encoding.UTF8);


                EggsoftWX.BLL.tab_DoTask_Services BLL_tab_DoTask_Services = new EggsoftWX.BLL.tab_DoTask_Services();
                EggsoftWX.Model.tab_DoTask_Services Model_tab_DoTask_Services = new EggsoftWX.Model.tab_DoTask_Services();
                Model_tab_DoTask_Services.InsertTime = DateTime.Now;
                Model_tab_DoTask_Services.TaskIfDone = false;
                Model_tab_DoTask_Services.TaskType = "SendWeiXin_Template";
                Model_tab_DoTask_Services.TaskXML = strAndOther;
                Model_tab_DoTask_Services.TaskMemo = strArrayListABC;
                BLL_tab_DoTask_Services.Add(Model_tab_DoTask_Services);

                string strCallBackUrl = Request.QueryString["CallBackUrl"];
                strCallBackUrl = strCallBackUrl.Replace("*", "?");
                JsUtil.ShowMsg("保存成功,并且你的任务已放入发送队列!", strCallBackUrl);
            }
        }


        private int save(bool boolSend, out int intThisShowID, out string strMsgTypeNewsTitle, out string strMsgTypeNewsDescription)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            if (CheckBox_Test.Checked)
            {
                string strTestUserID = TextBox_Test.Text;
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                bool boolExsit = BLL_tab_User.Exists("ShopUserID=" + strTestUserID + " and ShopClientID=" + strShopClientID);
                if (boolExsit == false)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("已经选择测试模式，但是用户ID不存在", -1);

                    intThisShowID = 0;
                    strMsgTypeNewsTitle = "";
                    strMsgTypeNewsDescription = "";
                    return 0;
                }
            }

            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/images/";


            string strText_SecondBuyStart = Request.Form["Text-SecondBuyStart"];
            string strText_SecondBuyEnd = Request.Form["Text-SecondBuyEnd"];

            DateTime my1datetime = DateTime.ParseExact(strText_SecondBuyStart, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime my2datetime = DateTime.ParseExact(strText_SecondBuyEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            strMsgTypeNewsTitle = MsgTypeNewsTitle.Text.Trim() + " 开抢时间:" + strText_SecondBuyStart + " 结束时间:" + strText_SecondBuyEnd;
            strMsgTypeNewsDescription = Textbox_Content.Text.Trim();

            intThisShowID = 0;
            int intSendMoneyByRedBag = 0;
            string type = Request.QueryString["type"];
            if ((type == "Add") || (type == "ModifyCopy"))
            {
                EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag BLL_tab_ShopClient_SendMoneyByRedBag = new EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag();
                EggsoftWX.Model.tab_ShopClient_SendMoneyByRedBag Model_tab_ShopClient_SendMoneyByRedBag = new EggsoftWX.Model.tab_ShopClient_SendMoneyByRedBag();

                Model_tab_ShopClient_SendMoneyByRedBag.SendToType = RadioButtonList_SendToType.SelectedValue;
                Model_tab_ShopClient_SendMoneyByRedBag.MsgTypeNewsDescription = Textbox_Content.Text.Trim();
                Model_tab_ShopClient_SendMoneyByRedBag.MsgTypeNewsTitle = MsgTypeNewsTitle.Text.Trim();
                Model_tab_ShopClient_SendMoneyByRedBag.SendedStatus = boolSend;
                Model_tab_ShopClient_SendMoneyByRedBag.ShopClient_ID = Int32.Parse(strShopClientID);
                Model_tab_ShopClient_SendMoneyByRedBag.ValidStartTime = my1datetime;
                Model_tab_ShopClient_SendMoneyByRedBag.ValidEndTime = my2datetime;

                intSendMoneyByRedBag = BLL_tab_ShopClient_SendMoneyByRedBag.Add(Model_tab_ShopClient_SendMoneyByRedBag);
                int intHowmanyPeople = 0;
                int.TryParse(Textbox_HowMany.Text.Trim(), out intHowmanyPeople);
                Decimal DecimalMoney = 0;
                Decimal.TryParse(Textbox_Price.Text.Trim(), out DecimalMoney);

                EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers BLL_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers Model_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers();
                Model_tab_RedWallet_Money_Credits_Vouchers.ShopClientID = Int32.Parse(strShopClientID);
                Model_tab_RedWallet_Money_Credits_Vouchers.SendMoneyByRedBagID = intSendMoneyByRedBag;
                Model_tab_RedWallet_Money_Credits_Vouchers.HowmanyPeople = intHowmanyPeople;
                Model_tab_RedWallet_Money_Credits_Vouchers.Money = DecimalMoney;
                Model_tab_RedWallet_Money_Credits_Vouchers.Type_Or_Money_Credits_Vouchers = Int32.Parse(RadioButtonList_RedType.SelectedValue);
                intThisShowID = BLL_tab_RedWallet_Money_Credits_Vouchers.Add(Model_tab_RedWallet_Money_Credits_Vouchers);



            }
            else if (type == "Modify")
            {
                string ModifyID = Request.QueryString["SendBagID"];
                intSendMoneyByRedBag = Int32.Parse(ModifyID);
                EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag BLL_tab_ShopClient_SendMoneyByRedBag = new EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag();
                EggsoftWX.Model.tab_ShopClient_SendMoneyByRedBag Model_tab_ShopClient_SendMoneyByRedBag = BLL_tab_ShopClient_SendMoneyByRedBag.GetModel(Int32.Parse(ModifyID));

                Model_tab_ShopClient_SendMoneyByRedBag.SendToType = RadioButtonList_SendToType.SelectedValue;
                Model_tab_ShopClient_SendMoneyByRedBag.MsgTypeNewsDescription = Textbox_Content.Text.Trim();
                Model_tab_ShopClient_SendMoneyByRedBag.MsgTypeNewsTitle = MsgTypeNewsTitle.Text.Trim();
                Model_tab_ShopClient_SendMoneyByRedBag.ValidStartTime = my1datetime;
                Model_tab_ShopClient_SendMoneyByRedBag.ValidEndTime = my2datetime;
                Model_tab_ShopClient_SendMoneyByRedBag.SendedStatus = true;
                BLL_tab_ShopClient_SendMoneyByRedBag.Update(Model_tab_ShopClient_SendMoneyByRedBag);
                int intHowmanyPeople = 0;
                int.TryParse(Textbox_HowMany.Text.Trim(), out intHowmanyPeople);
                Decimal DecimalMoney = 0;
                Decimal.TryParse(Textbox_Price.Text.Trim(), out DecimalMoney);

                EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers BLL_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers Model_tab_RedWallet_Money_Credits_Vouchers = BLL_tab_RedWallet_Money_Credits_Vouchers.GetModel("SendMoneyByRedBagID=" + ModifyID);
                Model_tab_RedWallet_Money_Credits_Vouchers.ShopClientID = Int32.Parse(strShopClientID);
                Model_tab_RedWallet_Money_Credits_Vouchers.HowmanyPeople = intHowmanyPeople;
                Model_tab_RedWallet_Money_Credits_Vouchers.Money = DecimalMoney;
                Model_tab_RedWallet_Money_Credits_Vouchers.Type_Or_Money_Credits_Vouchers = Int32.Parse(RadioButtonList_RedType.SelectedValue);
                BLL_tab_RedWallet_Money_Credits_Vouchers.Update(Model_tab_RedWallet_Money_Credits_Vouchers);
                intThisShowID = Model_tab_RedWallet_Money_Credits_Vouchers.ID;
                //string strCallBackUrl = Request.QueryString["CallBackUrl"];
                //strCallBackUrl = strCallBackUrl.Replace("*", "?");
                //JsUtil.ShowMsg("保存成功!", strCallBackUrl);
            }
            return intSendMoneyByRedBag;
        }
        protected void CheckBox_Test_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonList_SendToType.Enabled = CheckBox_Test.Checked == false;
            TextBox_Test.Enabled = CheckBox_Test.Checked;
            RequiredFieldValidator4.Enabled = CheckBox_Test.Checked;
        }
    }
}