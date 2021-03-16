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
    public partial class ConsumerWealthDrawMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.b004_OperationGoods bll_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
        //private EggsoftWX.Model.tab_ShopClient_Model Model = new EggsoftWX.Model.tab_ShopClient_Model();
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];


        private int pSTR_webuy8_ClientAdmin_Users_ID = 0;
        private int pSTR_webuy8_ClientAdmin_Usersb004_OperationGoodsID = 0;
        private string STR_tab_ShopClient_ModelUpLoadPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ini_Var(sender, e);
                setConsumerWealthAgreement("Show");
            }
        }


        protected void ini_Var(object sender, EventArgs e)
        {
            string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            pSTR_webuy8_ClientAdmin_Users_ID = Convert.ToInt32(strID);
            pSTR_webuy8_ClientAdmin_Usersb004_OperationGoodsID = Request.QueryString["Usersb004_OperationGoodsID"].toInt32();
            STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pSTR_webuy8_ClientAdmin_Users_ID);
        }

        private void setConsumerWealthAgreement(string strWay)
        {
            EggsoftWX.Model.b004_OperationGoods Model = bll_b004_OperationGoods.GetModel("ID=" + pSTR_webuy8_ClientAdmin_Usersb004_OperationGoodsID + " and ShopClient_ID=" + pSTR_webuy8_ClientAdmin_Users_ID);

            if (strWay == "Show")
            {
                if (Model != null)
                {
                    string Server_Str = Model.ConsumerWealthDrawMoney;

                    Text_setContactUs.Text = Server.HtmlDecode(Server_Str);
                }
            }
            else if (strWay == "Save")
            {
                string strHtmlSave = Server.HtmlEncode(Text_setContactUs.Text);

                Model.ConsumerWealthDrawMoney = strHtmlSave;
                Model.UpdateTime = DateTime.Now;
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                Model.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                bll_b004_OperationGoods.Update(Model);
                if (String.IsNullOrEmpty(Model.ConsumerWealthDrawMoney)) {
                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql("update b004_OperationGoods set ConsumerWealthDrawMoney=null where id=" + Model.ID);
                }
            }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ini_Var(sender, e);

            //string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            //string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

            setConsumerWealthAgreement("Save");
            pSTR_webuy8_ClientAdmin_Usersb004_OperationGoodsID = Request.QueryString["Usersb004_OperationGoodsID"].toInt32();

            JsUtil.ShowMsg("保存成功!", "ConsumerWealthDrawMoney.aspx?Usersb004_OperationGoodsID=" + pSTR_webuy8_ClientAdmin_Usersb004_OperationGoodsID);

            //    }       
        }

    }
}