using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._11RootMenu
{
    public partial class URLShow : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected string pub__addGoodAndGoodClassShortUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_URLShow")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                SetClass();
                addGoodAndGoodClassShortUrl();
            }
        }


        private void addGoodAndGoodClassShortUrl()
        {
            string str_addGoodAndGoodClassShortUrl = "";
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);


            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("id=" + strShopClientID);


            //EggsoftWX.BLL.tab_Goods_Class myBLL_tab_Goods_Class = new EggsoftWX.BLL.tab_Goods_Class();
            //System.Data.DataTable my_tab_Goods_Class = myBLL_tab_Goods_Class.GetList("userID=" + strShopClientID+" order by sort,id asc").Tables[0];

            //for (int inti = 0; inti < my_tab_Goods_Class.Rows.Count; inti++)
            //{
            //    string strClassName = my_tab_Goods_Class.Rows[inti]["ClassName"].ToString();
            //    string strClassID = my_tab_Goods_Class.Rows[inti]["ID"].ToString();
            //    string strLongText = "http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/productclass-" + strClassID + ".aspx";

            //    str_addGoodAndGoodClassShortUrl += "  <tr class=\"tdbg\" bgcolor=\"#e3e3e3\">\n";
            //    str_addGoodAndGoodClassShortUrl += "      <td>\n";
            //    str_addGoodAndGoodClassShortUrl += "           <font face=\"宋体\">\n";
            //    str_addGoodAndGoodClassShortUrl += "               <strong>商品分类：" + strClassName + "</strong></font></td>\n";
            //    str_addGoodAndGoodClassShortUrl += "       <td bgcolor=\"#ecf5ff\">\n";
            //    str_addGoodAndGoodClassShortUrl += "           " + strLongText + "\n";
            //    str_addGoodAndGoodClassShortUrl += "       </td>\n";
            //    str_addGoodAndGoodClassShortUrl += "   </tr>\n";
            //}

            EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            System.Data.DataTable myBLL_tab_Goods_DataTable = myBLL_tab_Goods.GetList("ShopClient_ID=" + strShopClientID + " and isSaled=1 and IsDeleted=0 order by sort,id asc").Tables[0];

            for (int inti = 0; inti < myBLL_tab_Goods_DataTable.Rows.Count; inti++)
            {
                string strProductName = myBLL_tab_Goods_DataTable.Rows[inti]["Name"].ToString();
                string strProductID = myBLL_tab_Goods_DataTable.Rows[inti]["ID"].ToString();
                string strLongText = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/product-" + strProductID + ".aspx";

                str_addGoodAndGoodClassShortUrl += "  <tr class=\"tdbg\" bgcolor=\"#e3e3e3\">\n";
                str_addGoodAndGoodClassShortUrl += "      <td>\n";
                str_addGoodAndGoodClassShortUrl += "           <font face=\"宋体\">\n";
                str_addGoodAndGoodClassShortUrl += "               <strong>商品名称：" + strProductName + "</strong></font></td>\n";
                str_addGoodAndGoodClassShortUrl += "       <td bgcolor=\"#ecf5ff\">\n";
                str_addGoodAndGoodClassShortUrl += "           " + strLongText + "\n";
                str_addGoodAndGoodClassShortUrl += "       </td>\n";
                str_addGoodAndGoodClassShortUrl += "   </tr>\n";
            }

            EggsoftWX.BLL.tab_ShopClient_GuidePages myBLLtab_ShopClient_GuidePages = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
            System.Data.DataTable myBLL_tab_ShopClient_GuidePages = myBLLtab_ShopClient_GuidePages.GetList("ShopClientID=" + strShopClientID + " and ParentID=0 and isnull(IsDeleted,0)=0 order by MenuPos,id asc").Tables[0];

            for (int inti = 0; inti < myBLL_tab_ShopClient_GuidePages.Rows.Count; inti++)
            {
                string strMenuName = myBLL_tab_ShopClient_GuidePages.Rows[inti]["MenuName"].ToString();
                string strguidepageID = myBLL_tab_ShopClient_GuidePages.Rows[inti]["ID"].ToString();
                string strLongText = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/guidepage-" + strguidepageID + ".aspx";

                str_addGoodAndGoodClassShortUrl += "  <tr class=\"tdbg\" bgcolor=\"#e3e3e3\">\n";
                str_addGoodAndGoodClassShortUrl += "      <td>\n";
                str_addGoodAndGoodClassShortUrl += "           <font face=\"宋体\">\n";
                str_addGoodAndGoodClassShortUrl += "               <strong>引导页名称：" + strMenuName + "</strong></font></td>\n";
                str_addGoodAndGoodClassShortUrl += "       <td bgcolor=\"#ecf5ff\">\n";
                str_addGoodAndGoodClassShortUrl += "           " + strLongText + "\n";
                str_addGoodAndGoodClassShortUrl += "       </td>\n";
                str_addGoodAndGoodClassShortUrl += "   </tr>\n";
            }


            pub__addGoodAndGoodClassShortUrl = str_addGoodAndGoodClassShortUrl;

        }


        private void SetClass()
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);


            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("id=" + strShopClientID);


            Label_MyShop.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/default.aspx";
            //Label_MyShop.Text =  Label_MyShop.Text;

            Label2_IWillOpen.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/edityourshopini.aspx";
            //Label2_IWillOpen.Text =  Label2_IWillOpen.Text;
            Label_o2oShopLink.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/offlineshop.aspx";

            Label_Cart.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/cart.aspx";
            //Label_Cart.Text =  Label_Cart.Text;

            Label_MyWeBuy.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/mywebuy.aspx";
            //Label_MyWeBuy.Text =  Label_MyWeBuy.Text;

            Label_Multibutton_Customer.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/multibutton_customer.aspx";
            //Label_Multibutton_Customer.Text =  Label_Multibutton_Customer.Text;

            Label_Multibutton_showmoneydata.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/multibutton_showmoneydata.aspx";
            Label1Poster.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/poster.aspx";//海报地址

            Label_MarkerErWeiMaShow.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/showagentbookmark.aspx";//代理证书地址

            Label_DialTel.Text = "tel:" + Model_tab_ShopClient.ContactPhone;
            Label_DialSMS.Text = "sms:" + Model_tab_ShopClient.ContactPhone;
            Label_SalesOrder.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/addfunction/salesorder.html";



            #region 验证 QQ 关联
            string strLabel_LinQQ = Model_tab_ShopClient.QM_QQ_COM_QM_K_32;
            if (String.IsNullOrEmpty(strLabel_LinQQ) == false)
            {
                Label_LinQQ.Text = "已经关联，链接地址是" + strLabel_LinQQ;
            }
            #endregion 验证 QQ 关联

            string strLinkWeiXinLink = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/weixinerweima_tuiguang.aspx";
            HyperLink_LinkWeiXin.NavigateUrl = strLinkWeiXinLink;
            HyperLink_LinkWeiXin.Text = strLinkWeiXinLink;


            string strMultiButton_GouWuQuanChange = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/multibutton_gouwuquanchange.aspx";
            HyperLink_GouWuQuanChange.NavigateUrl = strMultiButton_GouWuQuanChange;
            HyperLink_GouWuQuanChange.Text = strMultiButton_GouWuQuanChange;


            HyperLink101Shake_Parter.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/AddFunction/01Shake_Parter/01Shake_Parter.aspx";
            HyperLink101Shake_Parter.NavigateUrl = HyperLink101Shake_Parter.Text;

            HyperLink_PaySelf.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/03custompay.aspx";
            HyperLink_PaySelf.NavigateUrl = HyperLink_PaySelf.Text;
            string str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/QRCodeImage/";
            string strImageURL = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(HyperLink_PaySelf.Text, str_Pub_upLoadpath, "M");
            Image1_PaySelf.ImageUrl = strImageURL;
                  


            HyperLink_WeiTuanGou.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/AddFunction/02PingTuan/02PingTuan.html";
            HyperLink_WeiTuanGou.NavigateUrl = HyperLink_WeiTuanGou.Text;

            HyperLink1_InputMoney.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/multibutton_income_givemoney.aspx";
            HyperLink1_InputMoney.NavigateUrl = HyperLink1_InputMoney.Text;

            HyperLink_OnlineList.Text = "https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/Huodong/05OnlineList/index.html";
            HyperLink_OnlineList.NavigateUrl = HyperLink_OnlineList.Text;


        }
    }
}