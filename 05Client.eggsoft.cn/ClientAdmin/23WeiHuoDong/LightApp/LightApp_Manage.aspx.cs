using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.LightApp
{
    public partial class LightApp_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];


        public String MenuLink = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string type = Request.QueryString["Type"];

                if (string.IsNullOrEmpty(type) == false)
                {
                    if (type.ToLower() == "delete")
                    {
                        string LightApp_ID = Request.QueryString["ID"];
                        if (!CommUtil.IsNumStr(LightApp_ID))
                            MyError.ThrowException("传递参数错误!");
                        EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage bll = new EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage();
                        bll.Delete("LightApp_ID=" + LightApp_ID);

                        EggsoftWX.BLL.tab_ShopClient_LightApp bll_tab_LightApp = new EggsoftWX.BLL.tab_ShopClient_LightApp();
                        bll_tab_LightApp.Delete(Int32.Parse(LightApp_ID));

                        JsUtil.ShowMsg("删除成功!", "LightApp_Boad.aspx");
                    }
                    else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                    {
                        SetClass();
                    }
                }
            }
        }

        private void SetClass()
        {
            EggsoftWX.BLL.tab_ShopClient_LightApp bll_tab_LightApp = new EggsoftWX.BLL.tab_ShopClient_LightApp();


            string type = Request.QueryString["Type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model.tab_ShopClient_LightApp Model = bll_tab_LightApp.GetModel(Int32.Parse(ID));

                txtTitle.Text = Model.Title;

                HyperLink__Mp3path.Text = Model.Mp3Path;
                HyperLink__Mp3path.NavigateUrl = strUpLoadURL + Model.Mp3Path;

                Model.Title = txtTitle.Text;
                txtTitleDesc.Text = Model.Description;


                //string strURL = Eggsoft.Common.Application.getwebHttp() + "/LightAppCN/D-" + Eggsoft.Common.Session.Read("INCID").ToString() + "-" + ID + ".aspx";
                //string strAPPCODE_get_INC_Upload = Pub.APPCODE_get_INC_QRImages_Upload();
                //ztfLabel_ErWeiMa.Text = "<img width=\"181px;\" src=\"" + Eggsoft.Common.Image.creatQRCodeImage(strURL, strAPPCODE_get_INC_Upload) + "\">";


                btnAdd.Text = "保 存";
            }
            else if (type.ToLower() == "add")
            {

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];
            string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strFileUpload_Logo = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/MP3/";

            if (FileUpload_Mp3.HasFile == true)
            {
                if (Eggsoft.Common.FileFolder.IsAllowedExtension(FileUpload_Mp3,"mp3"))
                {
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".mp3";

                    SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                    string eMsg = "";
                    string strRemoveUpload = strFileUpload_Logo.Substring("/upload/".Length, strFileUpload_Logo.Length - 1 - "/upload/".Length);
                    upl.UploadFile(FileUpload_Mp3.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                    System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
                    strFileUpload_Logo += saveName;
                }
                else
                {
                    Response.Write("<script>alert('您只能上传mp3文件');</script>");
                }


            }

            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_LightApp bll = new EggsoftWX.BLL.tab_ShopClient_LightApp();
                EggsoftWX.Model.tab_ShopClient_LightApp Model = bll.GetModel(Int32.Parse(ID));



                Model.Title = txtTitle.Text;
                Model.Description = txtTitleDesc.Text;
                if (String.IsNullOrEmpty(strFileUpload_Logo) == false) Model.Mp3Path = strFileUpload_Logo;

                //TextBox dddTextBox = Upload_MultiSeclect1.FindControl("TextBox_txtReturnValue") as TextBox;

                //Model.PicList = dddTextBox.Text;


                //XmlHelper_Instance_Custom_XMLAdvance_LightApp myLightAppXML = new XmlHelper_Instance_Custom_XMLAdvance_LightApp();
                //myLightAppXML.LastPageButtonName=txtTitleNavName.Text;
                //myLightAppXML.LastPageButtonHttp = Textbox_Address.Text;
                //string strXML = Eggsoft.Common.XmlHelper.XmlSerialize(myLightAppXML, System.Text.Encoding.UTF8);
                //Model.AddMemo = strXML;


                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "LightApp_Boad.aspx");
            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_ShopClient_LightApp bll_tab_LightApp = new EggsoftWX.BLL.tab_ShopClient_LightApp();
                    EggsoftWX.Model.tab_ShopClient_LightApp Model = new EggsoftWX.Model.tab_ShopClient_LightApp();

                    Model.Title = txtTitle.Text;
                    Model.Description = txtTitleDesc.Text;
                    if (String.IsNullOrEmpty(strFileUpload_Logo) == false) Model.Mp3Path = strFileUpload_Logo;
                    //TextBox dddTextBox = Upload_MultiSeclect1.FindControl("TextBox_txtReturnValue") as TextBox;

                    Model.ShopClientID = Int32.Parse(str_Pub_ShopClientID);


                    //Model.PicList = dddTextBox.Text;

                    //XmlHelper_Instance_Custom_XMLAdvance_LightApp myLightAppXML = new XmlHelper_Instance_Custom_XMLAdvance_LightApp();
                    //myLightAppXML.LastPageButtonName = txtTitleNavName.Text;
                    //myLightAppXML.LastPageButtonHttp = Textbox_Address.Text;
                    //string strXML = Eggsoft.Common.XmlHelper.XmlSerialize(myLightAppXML, System.Text.Encoding.UTF8);
                    //Model.AddMemo = strXML;

                    bll_tab_LightApp.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "LightApp_Boad.aspx");

                }



        }

    }
}