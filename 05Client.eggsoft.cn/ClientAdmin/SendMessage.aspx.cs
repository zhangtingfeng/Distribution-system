using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin
{
    public partial class SendMessage : System.Web.UI.Page
    {
        private EggsoftWX.BLL.tab_ShopClient_Model bll = new EggsoftWX.BLL.tab_ShopClient_Model();
        //private EggsoftWX.Model.tab_ShopClient_Model Model = new EggsoftWX.Model.tab_ShopClient_Model();


        private int pSTR_webuy8_ClientAdmin_Users_ID = 0;
        private string STR_tab_ShopClient_ModelUpLoadPath = "";
        public string strShopClientID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ini_Var(sender, e);
            }
        }


        protected void ini_Var(object sender, EventArgs e)
        {
            string strUserID = Request.QueryString["UserID"];

            EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User myModel_tab_User = new EggsoftWX.Model.tab_User();
            myModel_tab_User = myBLL_tab_User.GetModel("id=" + strUserID);


            Literal_SendMessage.Text = myModel_tab_User.NickName + "  " + myModel_tab_User.OpenID;
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strUserID = Request.QueryString["UserID"];
            strShopClientID = Request.QueryString["ShopClientID"];

            EggsoftWX.BLL.tab_ShopClient myBLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient myModel_tab_ShopClient = myBLL_tab_ShopClient.GetModel("id=" + strShopClientID);

            //string strOpenID = myModel_tab_ShopClient.OpenID;
            //if (String.IsNullOrEmpty(strOpenID))
            //{
            //    Eggsoft.Common.JsUtil.ShowMsg("亲，你还没有关联你的微信号，关联后才能使用本功能哦！");
            //}
            //else
            //{
            //EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
            //EggsoftWX.Model.tab_User myModel_tab_User = myBLL_tab_User.GetModel("OpenID='" + strOpenID + "'");

            string strDescription = Text_Contact.Text + "\nby " + myModel_tab_ShopClient.ShopClientName;
            //strDescription += "\n卖家微店号：" + myModel_tab_User.ID + ",回复" + myModel_tab_User.ID + "#文字内容可与卖家直接对话！" + DateTime.Now;
            strDescription += DateTime.Now + "\n";

            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strUserID), 0, strDescription);




            Eggsoft_Public_CL.Pub.insert_tab_User_Question("0", strUserID, strDescription, "商家客服消息");
            Eggsoft.Common.JsUtil.ShowMsg("微信消息已送入发送队列！");

            //}
        }

    }
}