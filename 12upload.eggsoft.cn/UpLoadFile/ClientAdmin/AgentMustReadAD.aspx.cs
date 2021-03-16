using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft.Common;
namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin
{
    public partial class AgentMustReadAD : Eggsoft.Common.DotAdminPage_ClientAdmin//System.Web.UI.Page
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
            EggsoftWX.Model.tab_ShopClient Model = bll_tab_ShopClient.GetModel("ID=" + pSTR_webuy8_ClientAdmin_Users_ID);

            if (strWay == "Show")
            {
                if (Model != null)
                {
                    string Server_Str = Model.AgentMustReadAd;

                    if (String.IsNullOrEmpty(Server_Str))
                    {
                        Server_Str = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgentAd();





                        //Server_Str += " <h2 class=\"tip-means-title\"><span>温馨提示</span><i class=\"icon-close\" onclick=\"tip_means_close(this)\"></i></h2>\n";
                        //Server_Str += "    <div class=\"tip-means-c\">\n";
                        //Server_Str += "  <span>亲，您的代理佣金由您的微店销售所得：</span>\n";
                        //Server_Str += "         <ol class=\"tip-means-ol\">\n";
                        //Server_Str += "             <li>销售的商品，我所获得佣金（即本店代理销售佣金）。</li>\n";
                        //Server_Str += "             <li>每款销售商品，客户无异议，无退货后，佣金自动转入你的账户（一般T+7）。</li>\n";
                        //Server_Str += "             <li>您的账户的现金可随时提现。</li>\n";
                        //Server_Str += "             <li>本系统挑选商品后即可一键生成您的微店。</li>\n";
                        //Server_Str += "             <li>下级分店发展的分店所销售的商品，我所获得的佣金（即一级分店佣金）。</li>\n";
                        //Server_Str += "         </ol>\n";
                        //Server_Str += " </div>\n";
                    }

                    Text_setContactUs.Text = Server.HtmlDecode(Server_Str);

                }
            }
            else if (strWay == "Save")
            {
                Model.AgentMustReadAd = Server.HtmlEncode(Text_setContactUs.Text);
                bll_tab_ShopClient.Update(Model);
            }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ini_Var(sender, e);

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

            setContactUs("Save");

            JsUtil.ShowMsg("保存成功!", "AgentMustReadAD.aspx");

            //    }       
        }

    }
}