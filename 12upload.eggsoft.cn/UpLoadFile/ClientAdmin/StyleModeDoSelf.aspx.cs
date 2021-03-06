using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin
{
    public partial class StyleModeDoSelf : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
        //private EggsoftWX.Model.tab_ShopClient_Model Model = new EggsoftWX.Model.tab_ShopClient_Model();
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];


        private int pSTR_webuy8_ClientAdmin_Users_ID = 0;
        private string STR_tab_ShopClient_ModelUpLoadPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ini_Var(sender, e);
                setStyleModeDoSelf("Show");
            }
        }


        protected void ini_Var(object sender, EventArgs e)
        {
            string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            pSTR_webuy8_ClientAdmin_Users_ID = Convert.ToInt32(strID);
            STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pSTR_webuy8_ClientAdmin_Users_ID);
        }

        private void setStyleModeDoSelf(string strWay)
        {
            EggsoftWX.Model.tab_ShopClient Model = bll_tab_ShopClient.GetModel("ID=" + pSTR_webuy8_ClientAdmin_Users_ID);

            if (strWay == "Show")
            {
                if (Model != null)
                {
                    string Server_Str = Eggsoft_Public_CL.Pub.stringShowPower(pSTR_webuy8_ClientAdmin_Users_ID.ToString(), "StyleModeDoSelfHtml");

                    Text_setContactUs.Text = Server.HtmlDecode(Server_Str);
                }
            }
            else if (strWay == "Save")
            {
                string strHtmlSave = Server.HtmlEncode(Text_setContactUs.Text);

                Eggsoft_Public_CL.Pub.boolSaveShowPower(pSTR_webuy8_ClientAdmin_Users_ID.ToString(), "StyleModeDoSelfHtml", strHtmlSave);///源码显示
         

                bll_tab_ShopClient.Update(Model);
            }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ini_Var(sender, e);

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

            setStyleModeDoSelf("Save");

            JsUtil.ShowMsg("保存成功!", "StyleModeDoSelf.aspx");

            //    }       
        }

    }
}