using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// UserIDSelectProductList 的摘要说明
    /// </summary>
    public class UserIDSelectProductList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string strUserID = context.Request.QueryString["strUserID"];
                //
                //string strContext=
                int pInt_Session_CurUserID = 0;
                int.TryParse(strUserID, out pInt_Session_CurUserID);

                string pub_stringChoiceProductClsssList = "";
                string pub_StrUserIDSelectProductList = "";


                initChoiceProductClsssList(pInt_Session_CurUserID, out pub_stringChoiceProductClsssList, out pub_StrUserIDSelectProductList);

                context.Response.Write(pub_stringChoiceProductClsssList + "######" + pub_StrUserIDSelectProductList);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }




        }

        private void initChoiceProductClsssList(int pub_Int_Session_CurUserID, out string pub_stringChoiceProductClsssList, out string pub_StrUserIDSelectProductList)
        {
            string stringEditShopList = "";

            //stringEditShopList += "<script src=\"/Templet/01WYJS/js/jquery_002.js?version=js201709121928\"></script>\n";
            //stringEditShopList += "<script src=\"/Templet/01WYJS/js/foundation.js?version=js201709121928\"></script>\n";
            //stringEditShopList += "<script src=\"/Templet/01WYJS/js/func.js?version=js201709121928\"></script>\n";
            //stringEditShopList += "<script src=\"/Templet/01WYJS/js/Common.js?version=js201709121928\"></script>\n";


           


            string stringUserIDSelectProductList = "";

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_BLL_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("userID=" + pub_Int_Session_CurUserID+ "  and IsDeleted=0 ");


            //EggsoftWX.BLL.tab_ShopClient_DistributionMoney BLL_tab_ShopClient_DistributionMoney = new EggsoftWX.BLL.tab_ShopClient_DistributionMoney();
            //EggsoftWX.Model.tab_ShopClient_DistributionMoney Model_tab_ShopClient_DistributionMoney = new EggsoftWX.Model.tab_ShopClient_DistributionMoney();


            EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
            EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = null;
            bool boolIfChoice = BLL_tab_ShopClient_Agent__ProductClassID.Exists("UserID=" + pub_Int_Session_CurUserID);//是否选择过


            System.Data.DataSet myds = null;


            //都是总店铺挑选
            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            myds = BLL_tab_Goods.GetList("ID,Name,Webuy8_DistributionMoney_Value,AgentPercent,PromotePrice", " ShopClient_ID=" + Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(pub_Int_Session_CurUserID.ToString()) + " and isSaled=1 and IsDeleted=0 order by Sort,id asc");
            //}
            for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
            {
                string strGoodID = myds.Tables[0].Rows[i]["ID"].ToString();
                string strName = myds.Tables[0].Rows[i]["Name"].ToString();
                string strAgentPercent = myds.Tables[0].Rows[i]["AgentPercent"].ToString();
                string strPromotePrice = myds.Tables[0].Rows[i]["PromotePrice"].ToString();

                //string strWebuy8_DistributionMoney_Value = myds.Tables[0].Rows[i]["Webuy8_DistributionMoney_Value"].ToString();
                //int intDistributionMoney_Value = 0;
                //int.TryParse(strWebuy8_DistributionMoney_Value, out intDistributionMoney_Value);
                //Model_tab_ShopClient_DistributionMoney = BLL_tab_ShopClient_DistributionMoney.GetModel(intDistributionMoney_Value);

                //string strPartner = "20.00"; string strPartner1 = "15.00"; string strPartner2 = "5.00";
                //if (Model_tab_ShopClient_DistributionMoney != null)
                //{
                //    strPartner = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_DistributionMoney.Partner);
                //    strPartner1 = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_DistributionMoney.Partner1);
                //    strPartner2 = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_DistributionMoney.Partner2);
                //}

                //bool boolExsit = BLL_tab_ShopClient_Agent__ProductClassID.Exists("UserID=" + pub_Int_Session_CurUserID + " and ProductID=" + strGoodID);//是否选中
                Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("UserID=" + pub_Int_Session_CurUserID + " and ProductID=" + strGoodID);
                bool boolExsit = Model_tab_ShopClient_Agent__ProductClassID != null;
                string strIFcurrent = "";
                if (boolExsit) strIFcurrent = " current";
                if (boolIfChoice == false) strIFcurrent = " current";//没选择过都选上

                if (strIFcurrent == " current")
                {
                    stringUserIDSelectProductList += strGoodID + ",";
                }


                if (i % 2 == 0) stringEditShopList += " <div style=\"width: 8%; padding-left: 10px; padding-bottom: 10px; float: left; box-sizing: border-box;\" id=\"item0eHnb5\" class=\"galcolumn\">\n";//判断奇偶数
                stringEditShopList += "      <div class=\"item" + strIFcurrent + "\" name=\"columns\" style=\"margin-bottom: 10px; opacity: 1;\" cid=\"" + strGoodID + "\">\n";
                stringEditShopList += "          <div>\n";
                stringEditShopList += "              <h5>" + Eggsoft.Common.StringNum.MaxLengthString(strName, 8) + "</h5>\n";
                stringEditShopList += "               <ul class=\"percent\">\n";
                if (boolExsit)//
                {
                    if (Model_tab_ShopClient_Agent__ProductClassID.Empowered.toBoolean())//存在 并且已授权 才显示
                    {
                        if (Model_BLL_tab_ShopClient_Agent_.Empowered.toBoolean())
                        {

                            Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                            myModel_MultiFenXiaoLevel.UserID = pub_Int_Session_CurUserID.toInt32();
                            myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                            Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel, false);
                            
                            
                            Decimal Decimal_Price_Percent = myModel_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * strAgentPercent.toDecimal() / strPromotePrice.toDecimal();

                            stringEditShopList += "                   <li>授权代理佣金:<strong>" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_Price_Percent.toDecimal()) + "%</strong></li></ul>\n";

                        }
                        else
                        {
                            stringEditShopList += "                   <li>尚未授权,请耐心等待</li></ul>\n";
                        }
                    }
                    else
                    {
                        stringEditShopList += "                   <li>尚未授权,请耐心等待</li></ul>\n";
                    }
                }
                //stringEditShopList += "                   <li>一级分店佣金:<strong>" + strPartner1 + "%</strong></li>\n";
                //stringEditShopList += "                   <li>二级分店佣金:<strong>" + strPartner2 + "%</strong></li>\n";
                stringEditShopList += "               \n";
                stringEditShopList += "            </div>\n";
                stringEditShopList += "       </div>\n";

                if ((i % 2 == 1) || ((i % 2 == 0) && (i == myds.Tables[0].Rows.Count - 1))) stringEditShopList += "</div>\n";//判断奇偶数   偶数并且是最后一个


            }
            pub_stringChoiceProductClsssList = stringEditShopList + "<div id=\"cleareHnb5\" style=\"clear: both; height: 0px; width: 0px; display: block;\"></div>";

            if (stringUserIDSelectProductList.IndexOf(",") > -1)
            {
                pub_StrUserIDSelectProductList = stringUserIDSelectProductList.Substring(0, stringUserIDSelectProductList.Length - 1);//去掉最后一个逗号； }

            }
            else
            {
                pub_StrUserIDSelectProductList = "";
            }
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}