using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.LightApp
{
    public partial class LightApp_EachPage_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

        public String MenuLink = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string LightApp_Boad_ID = Request.QueryString["LightApp_Boad_ID"];


                string type = Request.QueryString["Type"];

                if (string.IsNullOrEmpty(type) == false)
                {
                    if (type.ToLower() == "delete")
                    {
                        string ID = Request.QueryString["ID"];
                        if (!CommUtil.IsNumStr(ID))
                            MyError.ThrowException("传递参数错误!");
                        EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage bll = new EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage();
                        bll.Delete(Int32.Parse(ID));
                        //JsUtil.ShowMsg("删除成功!", "LightApp_EachPage_Boad.aspx?LightApp_Boad_ID=" + LightApp_Boad_ID);

                        string strCallBackUrl = Request.QueryString["CallBackUrl"];
                        strCallBackUrl = strCallBackUrl.Replace("*", "?");
                        strCallBackUrl = strCallBackUrl.Replace("^", "&");
                        JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/23WeiHuoDong/LightApp/" + strCallBackUrl);
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
            EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage bll = new EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage();
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";


            string type = Request.QueryString["Type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model.tab_ShopClient_LightApp_EachPage Model = bll.GetModel(Int32.Parse(ID));

                Upload_MultiSeclect.OnInit(Model.PicPath, upLoadpath);
                CheckBox_Nav_Bool.Checked = Model.ShowNav;
                RequiredFieldValidator_txtTitleNavName.Enabled = CheckBox_Nav_Bool.Checked;
                RegularExpressionValidator_Textbox_Address.Enabled = CheckBox_Nav_Bool.Checked;
                txtTitleNavName.Text = Model.NavName;
                Textbox_Address.Text = Model.NavPath;
                txtMenuPos.Text = Model.ShowPos.ToString();

                btnAdd.Text = "保 存";
            }
            else if (type.ToLower() == "add")
            {
                Upload_MultiSeclect.OnInit("", upLoadpath);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";


            string LightApp_Boad_ID = Request.QueryString["LightApp_Boad_ID"];

            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage bll = new EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage();
                EggsoftWX.Model.tab_ShopClient_LightApp_EachPage Model = bll.GetModel(Int32.Parse(ID));

                Upload_MultiSeclect.OnInit(Model.PicPath, upLoadpath);
                Model.ShowNav = CheckBox_Nav_Bool.Checked;
                Model.NavName = txtTitleNavName.Text;
                Model.NavPath = Textbox_Address.Text;
                Model.ShowPos = Int32.Parse(txtMenuPos.Text);

                TextBox dddTextBox = Upload_MultiSeclect.FindControl("TextBox_txtReturnValue") as TextBox;

                Model.PicPath = dddTextBox.Text;
                if (String.IsNullOrEmpty(Model.PicPath)) JsUtil.ShowMsg("修改失败!图片不能空", -1);



                bll.Update(Model);

                string strCallBackUrl = Request.QueryString["CallBackUrl"];
                strCallBackUrl = strCallBackUrl.Replace("*", "?"); strCallBackUrl = strCallBackUrl.Replace("^", "&");
                JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/23WeiHuoDong/LightApp/" + strCallBackUrl);


                //JsUtil.ShowMsg("修改成功!", "LightApp_EachPage_Boad.aspx?LightApp_Boad_ID=" + LightApp_Boad_ID);
            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage bll = new EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage();
                    EggsoftWX.Model.tab_ShopClient_LightApp_EachPage Model = new EggsoftWX.Model.tab_ShopClient_LightApp_EachPage();

                    string ID = Request.QueryString["ID"];// 修改ID
                    Upload_MultiSeclect.OnInit(Model.PicPath, upLoadpath);

                    //Upload_MultiSeclect.OnInit(Model.PicPath, false);
                    Model.ShowNav = CheckBox_Nav_Bool.Checked;
                    Model.NavName = txtTitleNavName.Text;
                    Model.NavPath = Textbox_Address.Text;
                    Model.ShowPos = Int32.Parse(txtMenuPos.Text);
                    Model.LightApp_ID = Int32.Parse(LightApp_Boad_ID);

                    TextBox dddTextBox = Upload_MultiSeclect.FindControl("TextBox_txtReturnValue") as TextBox;
                    Model.PicPath = dddTextBox.Text;
                    if (String.IsNullOrEmpty(Model.PicPath)) JsUtil.ShowMsg("修改失败!图片不能空", -1);

                    bll.Add(Model);
                
                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    if (String.IsNullOrEmpty(strCallBackUrl) == true)
                    {
                        strCallBackUrl = "LightApp_EachPage_Boad.aspx?LightApp_Boad_ID=" + LightApp_Boad_ID;
                    }
                    else
                    {
                        strCallBackUrl = strCallBackUrl.Replace("*", "?"); strCallBackUrl = strCallBackUrl.Replace("^", "&");
                    }
                    JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/23WeiHuoDong/LightApp/" + strCallBackUrl);
                }
        }
        protected void CheckBox_Nav_Bool_CheckedChanged(object sender, EventArgs e)
        {
            RequiredFieldValidator_txtTitleNavName.Enabled = CheckBox_Nav_Bool.Checked;
            RegularExpressionValidator_Textbox_Address.Enabled = CheckBox_Nav_Bool.Checked;
        }
    }
}