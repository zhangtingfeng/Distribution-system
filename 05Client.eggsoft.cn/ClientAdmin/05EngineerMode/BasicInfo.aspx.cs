using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class BasicInfo : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_BasicInfo")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strLink = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/WxURL/ModeD-" + strShopClientID + ".aspx";
                HyperLink_WeiXin_Developmebt_URL.NavigateUrl = strLink;
                HyperLink_WeiXin_Developmebt_URL.Text = strLink;
                SetClass();
            }
        }



        private void SetClass()
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
            Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);

            if (Model_tab_ShopClient_EngineerMode != null)
            {
                text_WeiXinUserName.Text = Model_tab_ShopClient_EngineerMode.WeiXinUserName;
                text_WeiXinUserPassword.Text = Model_tab_ShopClient_EngineerMode.WeiXinUserPassword;
                Textbox_Token.Text = Model_tab_ShopClient_EngineerMode.Token;
                txtTitle_WeiXinAppId.Text = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                Textbox_WeiXinAppSecret.Text = Model_tab_ShopClient_EngineerMode.WeiXinAppSecret;
                Textbox_EncodingAESKey.Text = Model_tab_ShopClient_EngineerMode.EncodingAESKey;
                Textbox_LiBaoLingQuTongZhi.Text = Model_tab_ShopClient_EngineerMode.LiBaoLingQuTongZhi;

                Textbox_GuideSubscribePageFromWeiXinD.Text = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;
                Textbox_WeiXinPayID.Text = Model_tab_ShopClient_EngineerMode.WeiXinPayID;
                Textbox_PartnerKey.Text = Model_tab_ShopClient_EngineerMode.PartnerKey;
                Label_Apiclient_cert_Pem.Text = Model_tab_ShopClient_EngineerMode.Apiclient_cert_Pem;//表示有文件
                Textbox_Apiclient_cert_Pem_Password.Text = Model_tab_ShopClient_EngineerMode.Apiclient_cert_Pem_Password;

            }


            Textbox_VisitMessage.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "TempleVisitMessage");//访客消息通知 模板ID：
            Textbox1_WisdomVisitMessage.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "TempleWisdomVisitMessage");//智能访客消息通知 模板ID：
            Textbox_PayMessage.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "TemplePayMessage");//成功付款通知 模板ID：
            TextBox_WeiXinHao.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "WeiXinHao");//微信号   微现场可用：
            Textbox_TempleInputMoneyMessage.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "TempleInputMoneyMessage");//会员充值通知
            Textbox1AccountNotice.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "TempleAccountNotice");//到账通知

            TextBox_SmallProgram_name.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "SmallProgram_name");//到账通知
            TextBox_SmallProgram_UsersName.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "SmallProgram_UsersName");//到账通知
            TextBox_SmallProgram_PWD.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "SmallProgram_PWD");//到账通知
            TextBox_SmallProgram_APPID.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "SmallProgram_APPID");//到账通知
            TextBox_SmallProgram_Secret.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "mallProgram_Secret");//到账通知
           
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);


            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/";
            String strFileUpload_Button = "";

            if (FileUpload_Apiclient_cert_Pem.HasFile == true)
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".cert.shiyi";
                strFileUpload_Button = upLoadpath + saveName;
                string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                upl.UploadFile(FileUpload_Apiclient_cert_Pem.FileBytes, saveName, strRemoveUpload, ref eMsg, "");

                // if (String.IsNullOrEmpty(strFileUpload_Button) == false) tab_ShopClient_Model.ShopButton = strFileUpload_Button;
            }

            if (Model_tab_ShopClient_EngineerMode != null)
            {
                Model_tab_ShopClient_EngineerMode.WeiXinUserName = text_WeiXinUserName.Text.Trim();
                Model_tab_ShopClient_EngineerMode.WeiXinUserPassword = text_WeiXinUserPassword.Text.Trim();
                Model_tab_ShopClient_EngineerMode.Token = Textbox_Token.Text.Trim();
                Model_tab_ShopClient_EngineerMode.WeiXinAppId = txtTitle_WeiXinAppId.Text.Trim();
                Model_tab_ShopClient_EngineerMode.WeiXinAppSecret = Textbox_WeiXinAppSecret.Text.Trim();
                Model_tab_ShopClient_EngineerMode.EncodingAESKey = Textbox_EncodingAESKey.Text.Trim();
                Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD = Textbox_GuideSubscribePageFromWeiXinD.Text.Trim();

                Model_tab_ShopClient_EngineerMode.LiBaoLingQuTongZhi = Textbox_LiBaoLingQuTongZhi.Text.Trim();

                Model_tab_ShopClient_EngineerMode.WeiXinPayID = Textbox_WeiXinPayID.Text.Trim();

                Model_tab_ShopClient_EngineerMode.PartnerKey = Textbox_PartnerKey.Text.Trim();
                if (String.IsNullOrEmpty(strFileUpload_Button) == false) Model_tab_ShopClient_EngineerMode.Apiclient_cert_Pem = strFileUpload_Button;//表示有文件
                Model_tab_ShopClient_EngineerMode.Apiclient_cert_Pem_Password = Textbox_Apiclient_cert_Pem_Password.Text.Trim();


                BLL_tab_ShopClient_EngineerMode.Update(Model_tab_ShopClient_EngineerMode);
            }
            else
            {
                Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                Model_tab_ShopClient_EngineerMode.WeiXinUserName = text_WeiXinUserName.Text.Trim();
                Model_tab_ShopClient_EngineerMode.WeiXinUserPassword = text_WeiXinUserPassword.Text.Trim();
                Model_tab_ShopClient_EngineerMode.Token = Textbox_Token.Text.Trim();
                Model_tab_ShopClient_EngineerMode.WeiXinAppId = txtTitle_WeiXinAppId.Text.Trim();
                Model_tab_ShopClient_EngineerMode.WeiXinAppSecret = Textbox_WeiXinAppSecret.Text.Trim();
                Model_tab_ShopClient_EngineerMode.ShopClientID = Int32.Parse(strShopClientID);
                Model_tab_ShopClient_EngineerMode.EncodingAESKey = Textbox_EncodingAESKey.Text.Trim();
                Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD = Textbox_GuideSubscribePageFromWeiXinD.Text.Trim();
                Model_tab_ShopClient_EngineerMode.WeiXinPayID = Textbox_WeiXinPayID.Text.Trim();
                Model_tab_ShopClient_EngineerMode.PartnerKey = Textbox_PartnerKey.Text.Trim();
                if (String.IsNullOrEmpty(strFileUpload_Button) == false) Model_tab_ShopClient_EngineerMode.Apiclient_cert_Pem = strFileUpload_Button;//表示有文件
                Model_tab_ShopClient_EngineerMode.Apiclient_cert_Pem_Password = Textbox_Apiclient_cert_Pem_Password.Text.Trim();
                Model_tab_ShopClient_EngineerMode.LiBaoLingQuTongZhi = Textbox_LiBaoLingQuTongZhi.Text.Trim();


                BLL_tab_ShopClient_EngineerMode.Add(Model_tab_ShopClient_EngineerMode);
            }

            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "TempleVisitMessage", Textbox_VisitMessage.Text);///访客消息通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "TempleWisdomVisitMessage", Textbox1_WisdomVisitMessage.Text);///智能访客消息通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "TemplePayMessage", Textbox_PayMessage.Text);///成功付款通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "WeiXinHao", TextBox_WeiXinHao.Text);///微信号   微现场可用：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "TempleInputMoneyMessage", Textbox_TempleInputMoneyMessage.Text);///访客消息通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "TempleAccountNotice", Textbox1AccountNotice.Text);///到账通知 模板ID：

            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "SmallProgram_name", TextBox_SmallProgram_name.Text);///到账通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "SmallProgram_UsersName", TextBox_SmallProgram_UsersName.Text);///到账通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "SmallProgram_PWD", TextBox_SmallProgram_PWD.Text);///到账通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "SmallProgram_APPID", TextBox_SmallProgram_APPID.Text);///到账通知 模板ID：
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "mallProgram_Secret", TextBox_SmallProgram_Secret.Text);///到账通知 模板ID：
          


            JsUtil.ShowMsg("修改成功!", -1);
        }

    }
}