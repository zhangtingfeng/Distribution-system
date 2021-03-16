using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class HistoryGoodsList : System.Web.UI.Page
    {
        EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
        EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

        

        protected string strLinkGoodlist = "";

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
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/HistoryGoodsList_history_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码

                    strTemplet = strTemplet.Replace("###Header###", "");


                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "历史记录"));

                    strTemplet = InitLink(strTemplet);
                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


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

        private string InitLink(string strTemplet)
        {
            string strView_WatchGoods = "";
            strView_WatchGoods = @"
                    

                    SELECT  DISTINCT view_User_Good_History.GoodID,   tab_Goods.ShopClient_ID, tab_Goods.Class1_ID, tab_Goods.Class2_ID, tab_Goods.Class3_ID, 
                tab_Goods.Name, tab_Goods.Good_Class, view_User_Good_History.Parent_UserID, 
                view_User_Good_History.UserID, view_User_Good_History.UpdateTime, view_User_Good_History.Count_Visit, 
                tab_Goods.PromotePrice, tab_Goods.Price, view_User_Good_History.ID, tab_Goods.Sort, 
                tab_Goods.ID AS GoodID, tab_Goods.IsDeleted, tab_Goods.IS_Admin_check, 
                view_User_Good_History.GrandParentID, view_User_Good_History.GreatParentID
FROM      tab_Goods RIGHT OUTER JOIN
              (SELECT   MAX4444.ID, tab_User_Good_History.UserID, tab_User_Good_History.GoodID, 
                tab_User_Good_History.Parent_UserID, tab_User_Good_History.UpdateTime, 
                tab_User_Good_History.Count_Visit, tab_User_Good_History.GrandParentID, 
                tab_User_Good_History.GreatParentID, tab_User_Good_History.Type_Visit, 
                tab_User_Good_History.OperationID, tab_User_Good_History.CreatTime
FROM     (SELECT   MAX(ID) AS ID
FROM      tab_User_Good_History
WHERE   (UserID = "+ pub_Int_Session_CurUserID + @")
GROUP BY GoodID) MAX4444 LEFT OUTER JOIN
                tab_User_Good_History ON MAX4444.ID = tab_User_Good_History.ID)  view_User_Good_History ON tab_Goods.ID = view_User_Good_History.GoodID
where view_User_Good_History.userID="+ pub_Int_Session_CurUserID + @"  and tab_Goods.ShopClient_ID="+ pub_Int_ShopClientID + @" 
order by view_User_Good_History.UpdateTime desc
";
            System.Data.DataTable myHistoryDataTable = bll_tab_Goods.SelectList(strView_WatchGoods).Tables[0];


            //System.Data.DataTable myHistoryDataTable = new EggsoftWX.BLL.View_WatchGoods().GetList("userid=" + pub_Int_Session_CurUserID + "order by UpdateTime desc").Tables[0];

            string strHistory = "";
            int intMax = myHistoryDataTable.Rows.Count > 30 ? 30 : myHistoryDataTable.Rows.Count;
            for (int i = 0; i < intMax; i++)
            {
                String strName = myHistoryDataTable.Rows[i]["Name"].ToString();
                String Parent_UserID = myHistoryDataTable.Rows[i]["Parent_UserID"].ToString();
                String stGoodID = myHistoryDataTable.Rows[i]["GoodID"].ToString();
                String strTime = myHistoryDataTable.Rows[i]["UpdateTime"].ToString();
                String strPromotePrice = myHistoryDataTable.Rows[i]["PromotePrice"].ToString();

                Model_tab_Goods = bll_tab_Goods.GetModel(Convert.ToInt32(stGoodID));

                int intParent_UserID = 0;
                string strParentPath = "";
                int.TryParse(Parent_UserID, out intParent_UserID);
                if (intParent_UserID > 0) strParentPath = "/sagent-" + intParent_UserID;
                strHistory += "<a href=\"" + strParentPath + "/product-" + stGoodID + ".aspx\"><li >\n";
                strHistory += "		<div class=\"content_form\" >\n";

                strHistory += "<table style=\"width: 100%;overflow:hidden;table-layout:fixed;\" border=\"0\">\n";
                strHistory += "<tr>\n";
                strHistory += "<td style=\"width:30%;\"><img width=\"100px\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_getImage(Int16.Parse(stGoodID), 100) + "\"/></td>\n";
                strHistory += "<td style=\"width:68%;\"><ul class=\"ulKonl\">\n";
                strHistory += "				<li>商品编号:" + Eggsoft_Public_CL.GoodP.GetGood_Num_ID_From_Good_ID(Convert.ToInt32(stGoodID)).ToString() + "</li>\n";
                strHistory += "				<li>浏览时间:" + strTime + "</li>\n";
                strHistory += "				<li>" + strName + "￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strPromotePrice)) + "</li>\n";
                strHistory += "				<li>" + Model_tab_Goods.ShortInfo.Trim() + "</li>\n";

                strHistory += "</ul></td>\n";
                strHistory += "</tr>\n";
                strHistory += "</table>\n";


                //strHistory += "				<div class=\"his_num\">商品编号:" + Eggsoft_Public_CL.GoodP.GetGood_Num_ID_From_Good_ID(Convert.ToInt32(stGoodID)).ToString() + "</div>\n";
                //strHistory += "				<div class=\"his_date\">浏览时间:" + strTime + "</div>\n";
                //strHistory += "				<div class=\"his_pro_name\">" + strName + "&nbsp;&nbsp;<font color=\"red\">￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strPromotePrice)) + "</font></div>\n";
                //strHistory += "				<div class=\"his_pro_img_History\"><img alt=\"\" width=\"100px\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_getImage(Int16.Parse(stGoodID), 100) + "\"></div>\n";
                ////strHistory += "				<div class=\"his_pro_img_History\"><img alt=\"\" width=\"100px\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(Data_DataTable.Rows[i]["Icon"].ToString(), 100) + "\"></div>\n";
                //strHistory += "				<div class=\"his_pro_content_History\">" + Model_tab_Goods.ShortInfo.Trim() + "</div>\n";
                strHistory += "			\n";
                strHistory += "		</div>\n";
                strHistory += "	</li></a>\n";

            }

            strTemplet = strTemplet.Replace("###history_list###", strHistory);

            return strTemplet;
        }

    }
}