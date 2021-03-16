using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class Long2ShortUrl : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected string pub__addGoodAndGoodClassShortUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetClass();
                //addGoodAndGoodClassShortUrl();
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


            EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            System.Data.DataTable myBLL_tab_Goods_DataTable = myBLL_tab_Goods.GetList("ShopClient_ID=" + strShopClientID + " and isSaled=1 and IsDeleted=0 order by sort,id asc").Tables[0];

            for (int inti = 0; inti < myBLL_tab_Goods_DataTable.Rows.Count; inti++)
            {
                string strProductName = myBLL_tab_Goods_DataTable.Rows[inti]["Name"].ToString();
                string strProductID = myBLL_tab_Goods_DataTable.Rows[inti]["ID"].ToString();
                string strLongText = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=https://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/product-" + strProductID + ".aspx@subscribe=1#wechat_redirect";

                str_addGoodAndGoodClassShortUrl += "  <tr class=\"tdbg\" bgcolor=\"#e3e3e3\">\n";
                str_addGoodAndGoodClassShortUrl += "      <td align=\"right\" class=\"auto-style1\">\n";
                str_addGoodAndGoodClassShortUrl += "           <font face=\"宋体\">\n";
                str_addGoodAndGoodClassShortUrl += "               <strong>商品名称：" + strProductName + "</strong></font></td>\n";
                str_addGoodAndGoodClassShortUrl += "       <td bgcolor=\"#ecf5ff\" class=\"auto-style1\">\n";
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


            //Label_MyShop.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&amp;redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&amp;response_type=code&amp;scope=snsapi_base&amp;state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/default.aspx@subscribe=1/#wechat_redirect";
            //Label2_IWillOpen.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/edityourshop.aspx@subscribe=1#wechat_redirect";
            //Label_o2oShopLink.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/offlineshop.aspx@subscribe=1#wechat_redirect";
            //Label_Cart.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/cart.aspx@subscribe=1#wechat_redirect";
            //Label_MyWeBuy.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/mywebuy.aspx@subscribe=1#wechat_redirect";
            //Label_Multibutton_Customer.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/multibutton_customer.aspx@subscribe=1#wechat_redirect";
            //Label_Multibutton_showmoneydata.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/multibutton_showmoneydata.aspx@subscribe=1#wechat_redirect";
            //Label_MarkerErWeiMaPath.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/showagentbookmark.aspx@subscribe=1#wechat_redirect";
            //Label_SalesOrder.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/addfunction/salesorder.html@subscribe=1#wechat_redirect";
            //HyperLink_LinkWeiXin.Text = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Model_tab_ShopClient_EngineerMode.WeiXinAppId + "&redirect_uri=http%3a%2f%2fthirdplatform.eggsoft.cn%2fwxurl%2fmyoauth1-" + strShopClientID + ".aspx&response_type=code&scope=snsapi_base&state=http://" + Model_tab_ShopClient.ErJiYuMing.ToLower() + "/weixinerweima_tuiguang.aspx@subscribe=1#wechat_redirect";
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            ;


            text_Short.Text = Eggsoft_Public_CL.Pub_Agent.LongUrlToShortUrl(Int32.Parse(strShopClientID), text_WeiXinUserName_Long.Text.Trim());

            //JsUtil.ShowMsg("修改成功!",0);
        }
    }
}