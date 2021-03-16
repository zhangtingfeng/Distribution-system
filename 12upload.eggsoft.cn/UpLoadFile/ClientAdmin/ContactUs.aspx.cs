using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin
{
    public partial class ContactUs : Eggsoft.Common.DotAdminPage_ClientAdmin//System.Web.UI.Page
    {
        private EggsoftWX.BLL.tab_ShopClient_Model bll = new EggsoftWX.BLL.tab_ShopClient_Model();
        //private EggsoftWX.Model.tab_ShopClient_Model Model = new EggsoftWX.Model.tab_ShopClient_Model();
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];


        private int pSTR_webuy8_ClientAdmin_Users_ID = 0;
        private string STR_tab_ShopClient_ModelUpLoadPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ini_Var(sender, e);
                setContactUs("Show");
            }
        }


        protected void ini_Var(object sender, EventArgs e)
        {

            string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            pSTR_webuy8_ClientAdmin_Users_ID = Convert.ToInt32(strID);

            STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pSTR_webuy8_ClientAdmin_Users_ID);

        }

        private void setContactUs(string strWay)
        {
            EggsoftWX.Model.tab_ShopClient_Model Model = bll.GetModel("UserID=" + pSTR_webuy8_ClientAdmin_Users_ID + " and ModelName='" + "02ContactUs" + "'");

            if (strWay == "Show")
            {
                if (Model != null)
                {
                    string Server_Str = Model.ModelContent;
                    if (Server_Str.ToLower().IndexOf("xml") != -1)
                    {
                        Eggsoft_Public_CL.XML_Class.ContactUS myXML_Class = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML_Class.ContactUS>(Server_Str, Encoding.UTF8);
                        Text_setContactUs.Text = Server.HtmlDecode(myXML_Class.ContactUSText);

                        CheckBoxIfShow.Checked = myXML_Class.IFShow;
                        RadioButtonList_Choice.SelectedValue = myXML_Class.ShowHeadType.ToString();
                        TextBox_LINK.Text = myXML_Class.LinkText;

                        ColorPicker_FontColor.Color = myXML_Class.Font_Color;
                        ColorPicker_BackgroundColor.Color = myXML_Class.Background_Color;
                    }
                    else
                    {
                        Text_setContactUs.Text = Server.HtmlDecode(Server_Str);
                    }
                }
            }
            else if (strWay == "Save")
            {
                if (Model != null)
                {
                    Eggsoft_Public_CL.XML_Class.ContactUS myXML_Class = new Eggsoft_Public_CL.XML_Class.ContactUS();
                    myXML_Class.ContactUSText = Server.HtmlEncode(Text_setContactUs.Text);
                    //myXML_Class.Font_Color = ColorPicker_FontColor.Color;
                    //myXML_Class.Background_Color = ColorPicker_BackgroundColor.Color;
                    myXML_Class.IFShow = CheckBoxIfShow.Checked;
                    myXML_Class.ShowHeadType = Int32.Parse(RadioButtonList_Choice.SelectedValue);
                    myXML_Class.LinkText = TextBox_LINK.Text;

                    string strModelContent = Eggsoft.Common.XmlHelper.XmlSerialize(myXML_Class, Encoding.UTF8);
                    Model.ModelContent = strModelContent;
                    bll.Update(Model);
                }
                else
                {
                    EggsoftWX.Model.tab_ShopClient_Model newModel = new EggsoftWX.Model.tab_ShopClient_Model();

                    Eggsoft_Public_CL.XML_Class.ContactUS myXML_Class = new Eggsoft_Public_CL.XML_Class.ContactUS();
                    myXML_Class.ContactUSText = Server.HtmlEncode(Text_setContactUs.Text);
                    myXML_Class.IFShow = CheckBoxIfShow.Checked;
                    myXML_Class.ShowHeadType = Int32.Parse(RadioButtonList_Choice.SelectedValue);
                    myXML_Class.LinkText = TextBox_LINK.Text;
                    //myXML_Class.Font_Color = ColorPicker_FontColor.Color;
                    //myXML_Class.Background_Color = ColorPicker_BackgroundColor.Color;
                    string strModelContent = Eggsoft.Common.XmlHelper.XmlSerialize(myXML_Class, Encoding.UTF8);
                    newModel.ModelContent = strModelContent;
                    newModel.ModelName = "02ContactUs";
                    newModel.ModelType = 1;
                    newModel.UserID = pSTR_webuy8_ClientAdmin_Users_ID;
                    newModel.ModelUpdateTime = DateTime.Now;
                    bll.Add(newModel);
                }
            }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ini_Var(sender, e);

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

            setContactUs("Save");

            JsUtil.ShowMsg("保存成功!", "ContactUs.aspx");

            //    }       
        }

    }
}