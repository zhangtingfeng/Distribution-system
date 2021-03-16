using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_comments : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_comments_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码

                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "评论"));

                    strTemplet = strTemplet.Replace("###Header###", "");
                    if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "PC")
                    {
                        //strTemplet = strTemplet.Replace("###Header###", "");

                        strTemplet = strTemplet.Replace("###Webuy8Footer###", "");
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    }


                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                    strTemplet = InitOpenShopAsk(strTemplet);

                    Response.Write(strTemplet);

                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }
            }
        }

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }
        private String InitOpenShopAsk(string strTemplet)
        {
            String strBody = "暂无数据";


            string strView_SalesGoods = "";
            strView_SalesGoods += @" SELECT   tab_Orderdetails.OrderCount, tab_Order.ID AS OrderID, tab_Order.PayStatus, tab_Orderdetails.GoodID, 
                tab_Order.UserID, tab_User.NickName, tab_Order.OrderNum, tab_Goods.PromotePrice, 
                tab_Orderdetails.GoodPrice, tab_Goods.Name AS GoodName, tab_Orderdetails.ID AS ID_Orderdetails, 
                tab_Orderdetails.Pinglun, tab_Orderdetails.CreatDateTime, tab_Order.PayDateTime, 
                tab_Orderdetails.ParentID, tab_Orderdetails.Over7DaysToBeans, tab_Goods.IsDeleted, 
                tab_Order.TotalMoney, tab_Goods.IS_Admin_check, 
                tab_ShopClient_Agent__ProductClassID.Empowered AS ParentID_Empowered, 
                tab_ShopClient_Agent_.Empowered AS GrandParentID_Empowered, tab_Order.DeliveryText, 
                tab_Order.isReceipt, tab_Orderdetails.GrandParentID, tab_Orderdetails.GreatParentID, 
                tab_Order.UpdateDateTime, tab_Order.ShopClient_ID, tab_Goods.Send_Money_IfBuy, 
                tab_Goods.Send_Vouchers_IfBuy, tab_Orderdetails.GoodType, tab_Orderdetails.GoodTypeId, 
                tab_Orderdetails.GoodTypeIdBuyInfo ";
            strView_SalesGoods += " FROM      tab_ShopClient_Agent_ with(nolock) RIGHT OUTER JOIN ";
            strView_SalesGoods += "  tab_ShopClient_Agent__ProductClassID with(nolock) ON ";
            strView_SalesGoods += "   tab_ShopClient_Agent_.UserID = tab_ShopClient_Agent__ProductClassID.UserID RIGHT OUTER JOIN ";
            strView_SalesGoods += "  tab_Order with(nolock) INNER JOIN ";
            strView_SalesGoods += "  tab_Orderdetails ON tab_Order.ID = tab_Orderdetails.OrderID INNER JOIN ";
            strView_SalesGoods += "  tab_Goods with(nolock) ON tab_Orderdetails.GoodID = tab_Goods.ID INNER JOIN ";
            strView_SalesGoods += " tab_User with(nolock) ON tab_Order.UserID = tab_User.ID ON ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.UserID = tab_Order.UserID AND ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.ProductID = tab_Orderdetails.GoodID ";
            string strwhere = "tab_ShopClient_Agent_.UserID=" + pub_Int_Session_CurUserID + " and PayStatus=1 ";
            strView_SalesGoods += " WHERE " + strwhere + " and (tab_Order.PayStatus = 1) AND (tab_Goods.IsDeleted = 0) AND (tab_Orderdetails.isdeleted = 0)";///AND (tab_Goods.IsDeleted = 0)
            strView_SalesGoods += " order by ID_Orderdetails";
           
            EggsoftWX.BLL.tab_Goods BLLtab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Modeltab_Goods = new EggsoftWX.Model.tab_Goods();

            System.Data.DataTable myDataTable = BLLtab_Goods.SelectList(strView_SalesGoods).Tables[0];
            if (myDataTable.Rows.Count > 0) strBody = "";
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                string strNickName = myDataTable.Rows[i]["NickName"].ToString();
                string strPinglun = myDataTable.Rows[i]["Pinglun"].ToString();

                if (string.IsNullOrEmpty(strPinglun)) strPinglun = strNickName + ":不能太懒啊，点击,找到下方的商品评价，输入评论，分发朋友圈，创造致富神话！";
                string strID_Orderdetails = myDataTable.Rows[i]["ID_Orderdetails"].ToString();

                Modeltab_Goods = BLLtab_Goods.GetModel(Int32.Parse(strGoodID));

                strBody += "<a href=\"" + Pub_Agent_Path + "/product-" + strGoodID + ".aspx#LableSay\"><li>\n";
                strBody += "			<div class=\"ul_li\">\n";
                strBody += "				<div class=\"ul_li_div_img\">\n";
                strBody += "					<img style=\"float:left;\" alt=\"图片\" width=\"100%\" height=\"80px\"\n";
                strBody += "						src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(Modeltab_Goods.Icon, 100) + "\">\n";
                strBody += "					<div class=\"ul_li_div_name\" style=\"bottom:46px;top:auto;\">" + Modeltab_Goods.Name + "</div>\n";
                strBody += "					<div class=\"item_info\">\n";
                strBody += "						<div class=\"item_price\">\n";
                strBody += "							<del class=\"price_\">￥" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Modeltab_Goods.PromotePrice)) + "</del>\n";
                strBody += "							<div class=\"price red\">￥" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Modeltab_Goods.Price)) + "</div>\n";
                strBody += "						</div>\n";
                strBody += "						<div class=\"item_good\">\n";
                strBody += "							<div class=\"vol\">销量: " + Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(strGoodID)) + "</div>\n";
                strBody += "							<div class=\"vgood\"><div style=\"float:right;max-width:30px;overflow: hidden;\">" + Eggsoft_Public_CL.GoodP.ByShareAskCount(Int32.Parse(strGoodID)) + "</div><div class=\"vg\"></div></div>\n";
                strBody += "						</div>\n";
                strBody += "					</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_comments\">\n";
                //strBody += "					<div class=\"ul_li_title\">" + strNickName + "</div>\n";
                strBody += "					<div class=\"ul_li_content\">" + strPinglun + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				\n";
                strBody += "			</div>\n";
                strBody += "		</li></a>\n";
            }
            strTemplet = strTemplet.Replace("###comment_list###", strBody);

            //

            return strTemplet;

        }
    }
}