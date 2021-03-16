using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._08Style_Model
{
    public partial class Style_Model : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        public String strMange = "";
        private int ShopClientID = 0;
        EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting_Style_Model")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

            if (!IsPostBack)
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                ShopClientID = Int32.Parse(strShopClientID);

                InitGoPage();
            }
        }

        private void InitGoPage()
        {
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(ShopClientID);
            int intStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);
            
            if (intStyle_Model == 0)
            {
                RadioButton0.Checked = true;
            }
            else if (intStyle_Model == 1)
            {
                RadioButton1.Checked = true;
            }
            else if (intStyle_Model == 2)
            {
                RadioButton2.Checked = true;
            }
            else if (intStyle_Model == 3)
            {
                RadioButton3.Checked = true;
            }
            CheckBox_StyleModeDoSelf.Checked = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID.ToString(), "StyleModeDoSelfCheck", false);

            setTitle();

        }

        protected void RadioButton0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;
            saveAction();
        }
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton0.Checked = false;
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;
            saveAction();
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton0.Checked = false;
            RadioButton1.Checked = false;
            RadioButton3.Checked = false;
            saveAction();
        }
        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton0.Checked = false;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            saveAction();
        }
        protected void saveAction()
        {
            int intstyle_Model = 0;
            if (RadioButton0.Checked == true)
            {
                intstyle_Model = 0;
            }
            else if (RadioButton1.Checked == true)
            {
                intstyle_Model = 1;
            }

            else if (RadioButton2.Checked == true)
            {
                intstyle_Model = 2;
            }
            else if (RadioButton3.Checked == true)
            {
                intstyle_Model = 3;
            }

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            ShopClientID = Int32.Parse(strShopClientID);
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(ShopClientID);
            Model_tab_ShopClient.Style_Model = intstyle_Model;
            bll_tab_ShopClient.Update(Model_tab_ShopClient);
            Eggsoft.Common.JsUtil.ShowMsg("模板设置成功！");
        }



        protected void CheckBox_StyleMode_CheckedChanged(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "StyleModeDoSelfCheck", CheckBox_StyleModeDoSelf.Checked);

            if (CheckBox_StyleModeDoSelf.Checked)
            {
                string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
                string strHyperLink_MakeHtml = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClientID + "&GoToUrl=";
                string strNavigateUrl = strHyperLink_MakeHtml + Server.UrlEncode("StyleModeDoSelf.aspx");
                Response.Redirect(strNavigateUrl);
            }
            setTitle();
        }

        private void setTitle() {
            if (CheckBox_StyleModeDoSelf.Checked)
            {
                CheckBox_StyleModeDoSelf.Text = "如果不使用商城首页自定义功能，请点击取消";
            }
            else
            {
                CheckBox_StyleModeDoSelf.Text = "商城首页自定义，点击选择该功能，并自动进入设置";
            }
        }
    }
}
