using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Default_SAgent_ProductNewGood 的摘要说明
    /// </summary>
    public class Default_SAgent_ProductNewGood : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            try
            {
                context.Response.ContentType = "text/plain";
                string strpub_Int_Session_CurUserID = context.Request.QueryString["strpub_Int_Session_CurUserID"];
                string strInt_ShopClientID = context.Request.QueryString["strpub_Int_ShopClientID"];
                string strpInt_QueryString_ParentID = context.Request.QueryString["strpInt_QueryString_ParentID"];
                //
                //string strContext=
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strInt_ShopClientID, out pub_Int_ShopClientID);
                int pInt_QueryString_ParentID = 0;
                int.TryParse(strpInt_QueryString_ParentID, out pInt_QueryString_ParentID);

                string strpClassGoodType = context.Request.QueryString["pClassGoodType"];
                int pub_Int_ClassGoodType = 0;
                int.TryParse(strpClassGoodType, out pub_Int_ClassGoodType);

                string strpClassID = context.Request.QueryString["pClassID"];
                int pub_pClassID = 0;
                int.TryParse(strpClassID, out pub_pClassID);

                string strType = context.Request.QueryString["Type"];

                // context.Response.Write(strVisitUserListImgeAndName);
                EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                int Pub_IntStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);

                string strSAgent_ProductNewGood = _GetNewestGoods(pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID, Pub_IntStyle_Model, strType, pub_Int_ClassGoodType, pub_pClassID);

                context.Response.Write(strSAgent_ProductNewGood);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string _GetNewestGoods(int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID, int Pub_IntStyle_Model, string strType, int pub_Int_ClassGoodType, int pub_pClassID)
        {

            //return "";
            string _NewstrGoodBodyest = "";
            string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);


            string strSQLView_Goods_Agent = @"SELECT   tab_ShopClient_Agent__ProductClassID.UserID, tab_Goods.ShopClient_ID, tab_Goods.Class1_ID, 
                tab_Goods.Class2_ID, tab_Goods.Class3_ID, tab_Goods.isSaled, tab_Goods.Name, 
                tab_Goods.Icon, tab_Goods.ShortInfo, tab_Goods.KuCunCount, 
                tab_Goods.Unit, tab_Goods.Price, tab_Goods.MemberPrice, tab_Goods.PromotePrice, 
                tab_Goods.IsCommend, tab_Goods.HitCount, tab_Goods.PromoteCount, tab_Goods.UpTime, 
                tab_Goods.UpdateTime, tab_Goods.ContactMan, tab_Goods.Sort, tab_Goods.IsDeleted, 
                tab_Goods.Good_Class, tab_Goods.SalesCount, tab_Goods.LimitTimerBuy_StartTime, 
                tab_Goods.LimitTimerBuy_EndTime, tab_Goods.LimitTimerBuy_TimePrice, 
                tab_Goods.LimitTimerBuy_Bool, tab_Goods.MinOrderNum, tab_Goods.MaxOrderNum, 
                tab_Goods.LimitTimerBuy_MaxSalesCount, tab_Goods.Shopping_Vouchers, tab_Goods.IS_Admin_check, 
                tab_Goods.CheckBox_WeiBai_RedMoney, tab_Goods.Webuy8_DistributionMoney_Value, 
                tab_Goods.FreightTemplate_ID, tab_Goods.ID, 
                tab_ShopClient_Agent__ProductClassID.Empowered
FROM      tab_Goods RIGHT OUTER JOIN
                tab_ShopClient_Agent__ProductClassID ON 
                tab_Goods.ID = tab_ShopClient_Agent__ProductClassID.ProductID and tab_Goods.ShopClient_ID = tab_ShopClient_Agent__ProductClassID.ShopClientID where tab_ShopClient_Agent__ProductClassID.UserID={0} and tab_Goods.ShopClient_ID={1} {2} and tab_Goods.isSaled=1 and tab_Goods.IsDeleted=0 and tab_Goods.IS_Admin_check=1 order by tab_Goods.sort asc,tab_Goods.updatetime desc		
";



            if ((pub_Int_ClassGoodType == 0) && (String.IsNullOrEmpty(strType) && pub_pClassID == 0))///这是首页
            {
                #region  ///这是首页
                EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + pub_Int_ShopClientID);

                if (Model_tab_ShopClient_ShopPar.DeafaultOnlyShowAnounceBitmap.toBoolean()) return "";//这家公司比较特殊 首页只要三张大的轮播图，满足她们把

                // Eggsoft.Common.debug_Log.Call_WriteLog("Time21=" + DateTime.Now);
                //检查是否是代理
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + pub_Int_Session_CurUserID + " and Empowered=1" + "  and IsDeleted=0  and ShopClientID=" + pub_Int_ShopClientID);///有代理啊





                System.Data.DataSet myds = null;
                if (boolAgent)
                {
                    myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, pub_Int_Session_CurUserID, pub_Int_ShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 "));

                    //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                    //myds = bll_bll_View_Goods_Agent.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + pub_Int_ShopClientID + " and UserID=" + pub_Int_Session_CurUserID + " and Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime desc");
                }
                else
                {
                    ///是不是别家的商品
                    ///

                    if (pInt_QueryString_ParentID > 0) //是访问别人代理店铺；
                    {
                        myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, pInt_QueryString_ParentID, pub_Int_ShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 "));

                        //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                        //myds = bll_bll_View_Goods_Agent.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + pub_Int_ShopClientID + " and UserID=" + pInt_QueryString_ParentID + " and  Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime desc");
                        if (myds.Tables[0].Rows.Count == 0)//父代理不存在了
                        {
                            Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_DeleteCookies();
                            EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                            myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + pub_Int_ShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime desc");
                        }
                    }
                    else
                    {
                        EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + pub_Int_ShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime desc");
                    }

                }

                int intShowCount = myds.Tables[0].Rows.Count;

                if (Pub_IntStyle_Model == 1)
                {
                    #region for
                    _NewstrGoodBodyest += "<section id=\"search-content\">\n";
                    _NewstrGoodBodyest += " <div class=\"i_wrap margin_auto rel hide\" id=\"item_classes_list_wrap\" style=\"display: block;\">\n";
                    _NewstrGoodBodyest += "     <ul class=\"i_ul rel\" id=\"hot_ul\">\n";


                    for (int i = 0; i < (intShowCount > 80 ? 80 : intShowCount); i++)
                    {
                        string GoodID = myds.Tables[0].Rows[i]["ID"].ToString();
                        string GoodName = myds.Tables[0].Rows[i]["Name"].ToString();
                        string GoodIconOOO = myds.Tables[0].Rows[i]["Icon"].ToString();
                        string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(GoodIconOOO, 200);


                        int SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        string Price = myds.Tables[0].Rows[i]["Price"].ToString();
                        string PromotePrice = myds.Tables[0].Rows[i]["PromotePrice"].ToString();
                        //string Price = myds.Tables[0].Rows[i]["PromotePrice"].ToString();
                        string ShortInfo = myds.Tables[0].Rows[i]["ShortInfo"].ToString();
                        int intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        int intByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(GoodID));


                        _NewstrGoodBodyest += "  <li class=\"i_li left\">\n";
                        _NewstrGoodBodyest += " <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\">\n";
                        _NewstrGoodBodyest += "<div  class=\"box\">\n";
                        _NewstrGoodBodyest += "<img  onload=\"AutoResizeImage(this)\" src=\"" + GoodIcon + "\">\n";
                        _NewstrGoodBodyest += "   </div>\n";
                        _NewstrGoodBodyest += "<p class=\"i_txt\">" + ShortInfo + " </p>";
                        _NewstrGoodBodyest += "<p class=\"i_pri_wrap\"><span style=\"padding-right:2px;\"  class=\"i_pri\">¥\n";
                        _NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice));
                        _NewstrGoodBodyest += "</span>";
                        if (Decimal.Parse(PromotePrice) != Decimal.Parse(Price))
                        {
                            _NewstrGoodBodyest += "<span style=\"text-decoration:line-through;color: #666666;font-size: 12px;\" class=\"i_pri\">¥\n";
                            _NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price));
                            _NewstrGoodBodyest += "</span>";
                            _NewstrGoodBodyest += "<span style=\"color:#666666;font-size:12px;padding-left:0px;\" class=\"i_pri\">\n";
                            _NewstrGoodBodyest += Eggsoft.Common.StringNum.MaxLengthString(GoodName, 5);
                            _NewstrGoodBodyest += "</span>";
                        }
                        else
                        {
                            _NewstrGoodBodyest += "<span style=\"color:#666666;font-size:12px;padding-left:0px;\" class=\"i_pri\">\n";
                            _NewstrGoodBodyest += Eggsoft.Common.StringNum.MaxLengthString(GoodName, 8);
                            _NewstrGoodBodyest += "</span>";
                        }

                        _NewstrGoodBodyest += "</p></a></li>\n";



                    }

                    _NewstrGoodBodyest += "</ul><div class=\"clear\"></div>\n";
                    _NewstrGoodBodyest += "</div>\n";
                    _NewstrGoodBodyest += " </section>\n";


                    #endregion for
                }
                else if (Pub_IntStyle_Model == 0)
                {
                    #region for
                    for (int i = 0; i < (intShowCount > 80 ? 80 : intShowCount); i++)
                    {
                        string GoodID = myds.Tables[0].Rows[i]["ID"].ToString();
                        string GoodName = myds.Tables[0].Rows[i]["Name"].ToString();
                        string GoodIconOOO = myds.Tables[0].Rows[i]["Icon"].ToString();
                        string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(GoodIconOOO, 640);


                        int SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        string Price = myds.Tables[0].Rows[i]["Price"].ToString();
                        string PromotePrice = myds.Tables[0].Rows[i]["PromotePrice"].ToString();
                        string ShortInfo = myds.Tables[0].Rows[i]["ShortInfo"].ToString();
                        int intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        int intByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(GoodID));




                        _NewstrGoodBodyest += "      <div class=\"jx_g\">\n";
                        _NewstrGoodBodyest += "          <div class=\"jx_g_img\">\n";
                        _NewstrGoodBodyest += "              <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\" class=\"J_bi_product_item J_ytag\" data-dap=\"\" data-pid=\"" + GoodID + "\">\n";
                        _NewstrGoodBodyest += "                  <img class=\"\" src=\"" + GoodIcon + "\">\n";
                        _NewstrGoodBodyest += "              </a>\n";
                        _NewstrGoodBodyest += "          </div>\n";
                        _NewstrGoodBodyest += "          <div class=\"jx_g_info\">\n";
                        _NewstrGoodBodyest += "              <p class=\"jx_g_title\">\n";
                        _NewstrGoodBodyest += "                  <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\" class=\"J_bi_product_item J_ytag\" data-dap=\"\" data-pid=\"" + GoodID + "\">" + ShortInfo + "</a>\n";
                        _NewstrGoodBodyest += "              </p>\n";
                        _NewstrGoodBodyest += "              <p class=\"jx_g_price\">\n";
                        _NewstrGoodBodyest += "                  <span class=\"jx_g_price_wx\">惊爆价<span><i>￥</i>" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice)) + "</span>\n";
                        _NewstrGoodBodyest += "                  </span><del class=\"jx_g_price_market\">市场价￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price)) + "</del>\n";
                        _NewstrGoodBodyest += "              </p>\n";
                        _NewstrGoodBodyest += "              <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\" class=\"WX_btn jx_g_btn J_bi_product_item J_ytag\" data-dap=\"\" data-pid=\"" + GoodID + "\">" + Eggsoft_Public_CL.GoodP.APPCODE_getBuyNow_String(Int32.Parse(GoodID)) + "</a>\n";
                        _NewstrGoodBodyest += "          </div>\n";
                        _NewstrGoodBodyest += "      </div>\n";

                    }
                    #endregion for
                }
                else if (Pub_IntStyle_Model == 2)
                {
                    #region for
                    for (int i = 0; i < (intShowCount > 80 ? 80 : intShowCount); i++)
                    {
                        string GoodID = myds.Tables[0].Rows[i]["ID"].ToString();
                        string GoodName = myds.Tables[0].Rows[i]["Name"].ToString();
                        string strSort = myds.Tables[0].Rows[i]["Sort"].ToString();
                        string GoodIconOOO = myds.Tables[0].Rows[i]["Icon"].ToString();
                        string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(GoodIconOOO, 200);


                        int SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        string Price = myds.Tables[0].Rows[i]["Price"].ToString();
                        string PromotePrice = myds.Tables[0].Rows[i]["PromotePrice"].ToString();
                        string ShortInfo = myds.Tables[0].Rows[i]["ShortInfo"].ToString();
                        int intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        int intByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(GoodID));




                        _NewstrGoodBodyest += "     <div class=\"classOneGood\" onclick=\"javascript:window.location.href='" + Pub_Agent_Path + "/product-" + GoodID + ".aspx'\">\n";
                        _NewstrGoodBodyest += " <div class=\"thisboard\">\n";
                        _NewstrGoodBodyest += "     <div class=\"box\">\n";
                        _NewstrGoodBodyest += "         <img alt=\"" + GoodName + "(" + strSort + ")\" onload=\"AutoResizeImage(this)\" src=\"" + GoodIcon + "\" />\n";
                        _NewstrGoodBodyest += "     </div>\n";
                        _NewstrGoodBodyest += "     <div class=\"RightDataGoods\">\n";
                        _NewstrGoodBodyest += "      <div class=\"gameCenterthisBlock\">  <div class=\"gameCenter\">\n";
                        _NewstrGoodBodyest += "        <div class=\"RightDataGoods_GoodName\">\n";
                        _NewstrGoodBodyest += "             " + GoodName + "\n";
                        _NewstrGoodBodyest += "         </div>\n";
                        _NewstrGoodBodyest += "         </div></div>\n";
                        _NewstrGoodBodyest += "         <div class=\"RightDataGoods_Goodline01\">\n";
                        _NewstrGoodBodyest += "             <span class=\"RedPrice\">¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice)) + "\n";
                        _NewstrGoodBodyest += "            </span>\n";
                        _NewstrGoodBodyest += "           <span class=\"BlueSend\">" + Eggsoft_Public_CL.Pub.getPubPromotePrice_ZheKou(Decimal.Parse(PromotePrice), Decimal.Parse(Price)) + "折</span>\n";
                        _NewstrGoodBodyest += "            <span class=\"BlueSend\">\n";
                        _NewstrGoodBodyest += "            </span>\n";
                        _NewstrGoodBodyest += "           <span class=\"okImg\"></span>\n";
                        _NewstrGoodBodyest += "            <span class=\"okText\">" + intByHitCount + "</span>\n";
                        _NewstrGoodBodyest += "\n";
                        _NewstrGoodBodyest += "       </div>\n";
                        _NewstrGoodBodyest += "        <div class=\"RightDataGoods_Goodline01\">\n";
                        _NewstrGoodBodyest += "             <span class=\"oldPrice\">原价：¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price)) + "\n";
                        _NewstrGoodBodyest += "           </span>\n";
                        _NewstrGoodBodyest += "\n";
                        _NewstrGoodBodyest += "             <span class=\"BtnBuyNow\">立即购买</span>\n";
                        _NewstrGoodBodyest += "\n";
                        _NewstrGoodBodyest += "         </div>\n";
                        _NewstrGoodBodyest += "     </div>\n";
                        _NewstrGoodBodyest += "\n";
                        _NewstrGoodBodyest += "   </div>\n";
                        _NewstrGoodBodyest += " </div>\n";



                    }
                    #endregion for
                }
                else if (Pub_IntStyle_Model == 3)
                {
                    #region for
                    _NewstrGoodBodyest += " <div class=\"GoodClassStyle4NavTwoGood\">\n";
                    _NewstrGoodBodyest += "     <ul>\n";


                    for (int i = 0; i < (intShowCount > 80 ? 80 : intShowCount); i++)
                    {
                        string GoodID = myds.Tables[0].Rows[i]["ID"].ToString();
                        string GoodName = myds.Tables[0].Rows[i]["Name"].ToString();
                        string GoodIconOOO = myds.Tables[0].Rows[i]["Icon"].ToString();
                        string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(GoodIconOOO, 400);


                        int SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        string Price = myds.Tables[0].Rows[i]["Price"].ToString();
                        string PromotePrice = myds.Tables[0].Rows[i]["PromotePrice"].ToString();
                        //string Price = myds.Tables[0].Rows[i]["PromotePrice"].ToString();
                        string ShortInfo = myds.Tables[0].Rows[i]["ShortInfo"].ToString();
                        int intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        int intByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(GoodID));


                        _NewstrGoodBodyest += "    <li>\n";
                        _NewstrGoodBodyest += "       <div onclick=\"javascript:window.location.href='" + Pub_Agent_Path + "/product-" + GoodID + ".aspx'\" class=\"goodHotClass\">\n";
                        _NewstrGoodBodyest += "           <div class='ImgParent'><img onload=\"AutoResizeImage(this)\" src=\"" + GoodIcon + "\" onerror=\"javascript:this.src='/Templet/04StyleModel/Images/nopic_loading.gif'\"/></div>\n";
                        _NewstrGoodBodyest += "          <div class=\"goodHotGoodName\">\n";
                        _NewstrGoodBodyest += "             " + GoodName + "\n";
                        _NewstrGoodBodyest += "         </div>\n";
                        _NewstrGoodBodyest += "        <div class=\"goodHotGoodPriceAndBuy\">\n";
                        _NewstrGoodBodyest += "            <div class=\"leftPrice\">\n";
                        _NewstrGoodBodyest += "                 惊爆价<span class=\"Moneypay\">￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice)) + "</span><br />\n";
                        _NewstrGoodBodyest += "                <span class=\"MarketMoney\">市场价 " + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price)) + "</span>\n";
                        _NewstrGoodBodyest += "             </div>\n";
                        _NewstrGoodBodyest += "              <div class=\"rightButton\">立即购买</div>\n";
                        _NewstrGoodBodyest += "         </div>\n";
                        _NewstrGoodBodyest += "       </div>\n";
                        _NewstrGoodBodyest += "   </li>\n";





                    }

                    _NewstrGoodBodyest += "</ul>\n";
                    _NewstrGoodBodyest += "</div>\n";

                    #endregion for
                }
                #endregion  ///这是首页
            }
            else
            {

                ///先识别身份。  总是有用的 任何一页都是如此啊
                //检查是否是代理
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + pub_Int_Session_CurUserID + "  and IsDeleted=0  and Empowered=1");///有代理啊
                System.Data.DataTable mydsDataTable = null;

                if (String.IsNullOrEmpty(strType) == false)
                {
                    #region 有类型 最热等
                    strType = strType.ToLower();
                    if (strType == "newest")
                    {
                        string strWhere = " and ShopClient_ID=" + pub_Int_ShopClientID;

                        if (boolAgent)
                        {
                            mydsDataTable = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, pub_Int_Session_CurUserID, pub_Int_ShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ")).Tables[0];

                            //EggsoftWX.BLL.View_Goods_Agent bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                            //mydsDataTable = bll_View_Goods_Agent.GetDataTable("40", "ID,Name,Icon,Price,PromotePrice,ShortInfo", strWhere + " and UserID=" + pub_Int_Session_CurUserID + " and Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime desc");
                        }
                        else
                        {
                            int intParentAgentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);//是不是访问别人网页；
                            if (intParentAgentID > 0) //是访问别人代理店铺；
                            {
                                mydsDataTable = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, intParentAgentID, pub_Int_ShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ")).Tables[0];

                                //EggsoftWX.BLL.View_Goods_Agent bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                                //mydsDataTable = bll_View_Goods_Agent.GetDataTable("40", "ID,Name,Icon,Price,PromotePrice,ShortInfo", strWhere + " and Empowered=1 and UserID=" + intParentAgentID + "  and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime desc");

                            }
                            else
                            {
                                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                mydsDataTable = bll_tab_Goods.GetDataTable("40", "ID,Name,Icon,Price,PromotePrice,ShortInfo", strWhere + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime desc");
                            }
                        }
                    }
                    else if (strType == "salsed")
                    {
                        //strWhere = "";//精选商品；View_SalesCount03_All_SalesCount
                        string strWhere = " and ShopClient_ID=" + pub_Int_ShopClientID;

                        if (boolAgent)
                        {
                            EggsoftWX.BLL.View_SalesCount03_All_SalesCount_Agent View_SalesCount03_All_SalesCount_Agent = new EggsoftWX.BLL.View_SalesCount03_All_SalesCount_Agent();
                            mydsDataTable = View_SalesCount03_All_SalesCount_Agent.GetDataTable("40", "GoodID as ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strWhere + " and Empowered=1 and UserID=" + pub_Int_Session_CurUserID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by SalesCount desc,ShareAskCount desc,sort asc");
                        }
                        else
                        {
                            int intParentAgentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);//是不是访问别人网页；
                            if (intParentAgentID > 0) //是访问别人代理店铺；
                            {
                                EggsoftWX.BLL.View_SalesCount03_All_SalesCount_Agent View_SalesCount03_All_SalesCount_Agent = new EggsoftWX.BLL.View_SalesCount03_All_SalesCount_Agent();
                                mydsDataTable = View_SalesCount03_All_SalesCount_Agent.GetDataTable("40", "GoodID as ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strWhere + " and Empowered=1 and UserID=" + intParentAgentID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by SalesCount desc,ShareAskCount desc,sort asc");
                            }
                            else
                            {
                                EggsoftWX.BLL.View_SalesCount03_All_SalesCount View_SalesCount03_All_SalesCount = new EggsoftWX.BLL.View_SalesCount03_All_SalesCount();
                                mydsDataTable = View_SalesCount03_All_SalesCount.GetDataTable("40", "GoodID as ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strWhere + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by SalesCount desc,ShareAskCount desc,sort asc");
                            }
                        }


                    }
                    else if (strType == "cheapest")
                    {
                        string strWhere = " and ShopClient_ID=" + pub_Int_ShopClientID + " ";

                        if (boolAgent)
                        {
                            EggsoftWX.BLL.View_Today_Cheapest_Agent View_Today_Cheapest_Agent = new EggsoftWX.BLL.View_Today_Cheapest_Agent();
                            mydsDataTable = View_Today_Cheapest_Agent.GetDataTable("40", "ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strWhere + " and Empowered=1  and UserID=" + pub_Int_Session_CurUserID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by CheapPrice asc,updatetime desc,sort asc");
                        }
                        else
                        {
                            int intParentAgentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);//是不是访问别人网页；
                            if (intParentAgentID > 0) //是访问别人代理店铺；
                            {
                                EggsoftWX.BLL.View_Today_Cheapest_Agent View_Today_Cheapest_Agent = new EggsoftWX.BLL.View_Today_Cheapest_Agent();
                                mydsDataTable = View_Today_Cheapest_Agent.GetDataTable("40", "ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strWhere + " and Empowered=1  and UserID=" + intParentAgentID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by CheapPrice asc,updatetime desc,sort asc");
                            }
                            else
                            {
                                EggsoftWX.BLL.View_Today_Cheapest View_Today_Cheapest = new EggsoftWX.BLL.View_Today_Cheapest();
                                mydsDataTable = View_Today_Cheapest.GetDataTable("40", "ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strWhere + "  and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by CheapPrice asc,updatetime desc,sort asc");
                            }
                        }
                    }
                    #endregion 有类型 最热等

                }
                else if (pub_pClassID > 0)
                {
                    #region pub_pClassID > 0

                    string strWhere = "";

                    strWhere = " ShopClient_ID=" + pub_Int_ShopClientID;
                    EggsoftWX.BLL.tab_Class1 bll_tab_Class1 = new EggsoftWX.BLL.tab_Class1();
                    EggsoftWX.BLL.tab_Class2 bll_tab_Class2 = new EggsoftWX.BLL.tab_Class2();
                    EggsoftWX.BLL.tab_Class3 bll_tab_Class3 = new EggsoftWX.BLL.tab_Class3();



                    //Model_tab_Goods_Class = bll_tab_Goods_Class.GetModel(Int32.Parse(strClassID));
                    //_Pub_strGoodBody_Title = Model_tab_Goods_Class.ClassName;
                    //_Pub_dBody_Title = _Pub_strGoodBody_Title;//分销商名称

                    if (boolAgent)
                    {
                        //EggsoftWX.BLL.View_Goods_Agent bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                        string strClassWhere = "";
                        if (pub_Int_ClassGoodType == 1)
                        {
                            strClassWhere = " and tab_Goods.Class1_ID=" + pub_pClassID;
                        }
                        else if (pub_Int_ClassGoodType == 2)
                        {
                            strClassWhere = " and tab_Goods.Class2_ID=" + pub_pClassID;
                        }
                        else if (pub_Int_ClassGoodType == 3)
                        {
                            strClassWhere = " and tab_Goods.Class3_ID=" + pub_pClassID;
                        }
                        //mydsDataTable = bll_View_Goods_Agent.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strClassWhere).Tables[0];

                        mydsDataTable = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, pub_Int_Session_CurUserID, pub_Int_ShopClientID, (" and tab_ShopClient_Agent__ProductClassID.Empowered=1 " + strClassWhere))).Tables[0];


                    }
                    else
                    {
                        int intParentAgentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);//是不是访问别人网页；
                        if (intParentAgentID > 0) //是访问别人代理店铺；
                        {
                            //EggsoftWX.BLL.View_Goods_Agent bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                            string strClassWhere = "";
                            if (pub_Int_ClassGoodType == 1)
                            {
                                strClassWhere = " and tab_Goods.Class1_ID=" + pub_pClassID;
                            }
                            else if (pub_Int_ClassGoodType == 2)
                            {
                                strClassWhere = " and tab_Goods.Class2_ID=" + pub_pClassID;
                            }
                            else if (pub_Int_ClassGoodType == 3)
                            {
                                strClassWhere = " and tab_Goods.Class3_ID=" + pub_pClassID;
                            }
                            mydsDataTable = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, intParentAgentID, pub_Int_ShopClientID, (" and tab_ShopClient_Agent__ProductClassID.Empowered=1 " + strClassWhere))).Tables[0];

                        }

                        if (!(mydsDataTable != null && mydsDataTable.Rows.Count > 0))
                        {
                            EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                            string strClassWhere = "";
                            if (pub_Int_ClassGoodType == 1)
                            {
                                strClassWhere = strWhere + " and Class1_ID=" + pub_pClassID + "   and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime asc";
                            }
                            else if (pub_Int_ClassGoodType == 2)
                            {
                                strClassWhere = strWhere + " and Class2_ID=" + pub_pClassID + "   and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime asc";
                            }
                            else if (pub_Int_ClassGoodType == 3)
                            {
                                strClassWhere = strWhere + "  and Class3_ID=" + pub_pClassID + "   and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 order by sort asc,updatetime asc";
                            }
                            mydsDataTable = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", strClassWhere).Tables[0];
                        }
                    }
                    #endregion pub_pClassID > 0
                }

                int intShowCount = mydsDataTable.Rows.Count;
                intShowCount = intShowCount > 40 ? 40 : intShowCount;
                if (intShowCount == 0)
                {
                    _NewstrGoodBodyest = "暂无商品上传！";
                }
                else
                {
                    #region 循环显示
                    string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL();

                    for (int i = 0; i < intShowCount; i++)
                    {
                        #region mydsDataTable.Rows[i]
                        string GoodID = mydsDataTable.Rows[i]["ID"].ToString();
                        string GoodName = mydsDataTable.Rows[i]["Name"].ToString();
                        string GoodIconOOO = mydsDataTable.Rows[i]["Icon"].ToString();
                        string GoodIcon = strGetAppConfiugUplaod + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(GoodIconOOO, 640);
                        string GoodIcon400 = strGetAppConfiugUplaod + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(GoodIconOOO, 400);


                        int SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        string Price = mydsDataTable.Rows[i]["Price"].ToString();
                        string PromotePrice = mydsDataTable.Rows[i]["PromotePrice"].ToString();
                        string ShortInfo = mydsDataTable.Rows[i]["ShortInfo"].ToString();
                        int intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                        int intByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(GoodID));
                        #endregion

                        if (Pub_IntStyle_Model == 0)
                        {
                            #region Pub_IntStyle_Model == 0
                            _NewstrGoodBodyest += "      <div class=\"jx_g\">\n";
                            _NewstrGoodBodyest += "          <div class=\"jx_g_img\">\n";
                            _NewstrGoodBodyest += "              <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\" class=\"J_bi_product_item J_ytag\" data-dap=\"\" data-pid=\"" + GoodID + "\">\n";
                            _NewstrGoodBodyest += "                  <img class=\"\" src=\"" + GoodIcon + "\">\n";
                            _NewstrGoodBodyest += "              </a>\n";
                            _NewstrGoodBodyest += "          </div>\n";
                            _NewstrGoodBodyest += "          <div class=\"jx_g_info\">\n";
                            _NewstrGoodBodyest += "              <p class=\"jx_g_title\">\n";
                            _NewstrGoodBodyest += "                  <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\" class=\"J_bi_product_item J_ytag\" data-dap=\"\" data-pid=\"" + GoodID + "\">" + ShortInfo + "</a>\n";
                            _NewstrGoodBodyest += "              </p>\n";
                            _NewstrGoodBodyest += "              <p class=\"jx_g_price\">\n";
                            _NewstrGoodBodyest += "                  <span class=\"jx_g_price_wx\">惊爆价<span><i>￥</i>" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice)) + "</span>\n";
                            _NewstrGoodBodyest += "                  </span><del class=\"jx_g_price_market\">市场价￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price)) + "</del>\n";
                            _NewstrGoodBodyest += "              </p>\n";
                            _NewstrGoodBodyest += "              <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\" class=\"WX_btn jx_g_btn J_bi_product_item J_ytag\" data-dap=\"\" data-pid=\"" + GoodID + "\">" + Eggsoft_Public_CL.GoodP.APPCODE_getBuyNow_String(Int32.Parse(GoodID)) + "</a>\n";
                            _NewstrGoodBodyest += "          </div>\n";
                            _NewstrGoodBodyest += "      </div>\n";
                            #endregion
                        }
                        else if (Pub_IntStyle_Model == 1)
                        {
                            #region Pub_IntStyle_Model == 1
                            _NewstrGoodBodyest += "  <li class=\"i_li left\">\n";
                            _NewstrGoodBodyest += " <a href=\"" + Pub_Agent_Path + "/product-" + GoodID + ".aspx\">\n";
                            //_NewstrGoodBodyest += "img src=\"/Templet/01WYJS/images/placeholder_list.png\">\n";
                            _NewstrGoodBodyest += "<div class=\"box\">\n";
                            _NewstrGoodBodyest += "<img  onload=\"AutoResizeImage(this)\" src=\"" + GoodIcon + "\">\n";
                            _NewstrGoodBodyest += "   </div>\n";
                            _NewstrGoodBodyest += "<p class=\"i_txt\">" + ShortInfo + " </p><p class=\"i_pri_wrap\"><span style=\"padding-right:2px;\" class=\"i_pri\">¥\n";
                            _NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice));
                            _NewstrGoodBodyest += "</span>";


                            if (Decimal.Parse(PromotePrice) != Decimal.Parse(Price))
                            {
                                _NewstrGoodBodyest += "<span style=\"text-decoration:line-through;color: #666666;font-size: 12px;padding-left:0px;\" class=\"i_pri\">¥\n";
                                _NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price));
                                _NewstrGoodBodyest += "</span>";
                                _NewstrGoodBodyest += "<span style=\"color:#666666;font-size:12px;padding-left:0px;padding-right:0px;\" class=\"i_pri\">\n";
                                _NewstrGoodBodyest += Eggsoft.Common.StringNum.MaxLengthString(GoodName, 5);
                                _NewstrGoodBodyest += "</span>";
                            }
                            else
                            {
                                _NewstrGoodBodyest += "<span style=\"color:#666666;font-size:12px;padding-left:0px;\" class=\"i_pri\">\n";
                                _NewstrGoodBodyest += Eggsoft.Common.StringNum.MaxLengthString(GoodName, 8);
                                _NewstrGoodBodyest += "</span>";
                            }

                            //_NewstrGoodBodyest += "<span style=\"text-decoration:line-through;color: #666666;font-size: 12px;\" class=\"i_pri\">¥\n";
                            //_NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price));
                            //_NewstrGoodBodyest += "</span>";
                            _NewstrGoodBodyest += "</p></a></li>\n";
                            #endregion
                        }
                        else if (Pub_IntStyle_Model == 2)
                        {
                            #region Pub_IntStyle_Model == 2
                            _NewstrGoodBodyest += "     <div class=\"classOneGood\" onclick=\"javascript:window.location.href='" + Pub_Agent_Path + "/product-" + GoodID + ".aspx'\">\n";
                            _NewstrGoodBodyest += " <div class=\"thisboard\">\n";
                            _NewstrGoodBodyest += "     <div class=\"box\">\n";
                            _NewstrGoodBodyest += "         <img onload=\"AutoResizeImage(this)\" src=\"" + GoodIcon + "\" />\n";
                            _NewstrGoodBodyest += "     </div>\n";
                            _NewstrGoodBodyest += "     <div class=\"RightDataGoods\">\n";
                            _NewstrGoodBodyest += "      <div class=\"gameCenterthisBlock\">  <div class=\"gameCenter\">\n";
                            _NewstrGoodBodyest += "        <div class=\"RightDataGoods_GoodName\">\n";
                            _NewstrGoodBodyest += "             " + GoodName + "\n";
                            _NewstrGoodBodyest += "         </div>\n";
                            _NewstrGoodBodyest += "         </div></div>\n";
                            _NewstrGoodBodyest += "         <div class=\"RightDataGoods_Goodline01\">\n";
                            _NewstrGoodBodyest += "             <span class=\"RedPrice\">¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice)) + "\n";
                            _NewstrGoodBodyest += "            </span>\n";
                            _NewstrGoodBodyest += "           <span class=\"BlueSend\">" + Eggsoft_Public_CL.Pub.getPubPromotePrice_ZheKou(Decimal.Parse(PromotePrice), Decimal.Parse(Price)) + "折</span>\n";
                            _NewstrGoodBodyest += "            <span class=\"BlueSend\">包邮\n";
                            _NewstrGoodBodyest += "            </span>\n";
                            _NewstrGoodBodyest += "           <span class=\"okImg\"></span>\n";
                            _NewstrGoodBodyest += "            <span class=\"okText\">" + intByHitCount + "</span>\n";
                            _NewstrGoodBodyest += "\n";
                            _NewstrGoodBodyest += "       </div>\n";
                            _NewstrGoodBodyest += "        <div class=\"RightDataGoods_Goodline01\">\n";
                            _NewstrGoodBodyest += "             <span class=\"oldPrice\">原价：¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price)) + "\n";
                            _NewstrGoodBodyest += "           </span>\n";
                            _NewstrGoodBodyest += "\n";
                            _NewstrGoodBodyest += "             <span class=\"BtnBuyNow\">立即购买</span>\n";
                            _NewstrGoodBodyest += "\n";
                            _NewstrGoodBodyest += "         </div>\n";
                            _NewstrGoodBodyest += "     </div>\n";
                            _NewstrGoodBodyest += "\n";
                            _NewstrGoodBodyest += "   </div>\n";
                            _NewstrGoodBodyest += " </div>\n";
                            #endregion
                        }
                        else if (Pub_IntStyle_Model == 3)
                        {
                            #region Pub_IntStyle_Model == 3
                            _NewstrGoodBodyest += "    <li>\n";
                            _NewstrGoodBodyest += "       <div onclick=\"javascript:window.location.href='" + Pub_Agent_Path + "/product-" + GoodID + ".aspx'\" class=\"goodHotClass\">\n";
                            _NewstrGoodBodyest += "           <div class=\"ImgParent\"><img onload=\"AutoResizeImage(this)\" src=\"" + GoodIcon400 + "\" onerror=\"javascript:this.src='/Templet/04StyleModel/Images/nopic_loading.gif'\"/></div>\n";
                            _NewstrGoodBodyest += "          <div class=\"goodHotGoodName\">\n";
                            _NewstrGoodBodyest += "             " + GoodName + "\n";
                            _NewstrGoodBodyest += "         </div>\n";
                            _NewstrGoodBodyest += "        <div class=\"goodHotGoodPriceAndBuy\">\n";
                            _NewstrGoodBodyest += "            <div class=\"leftPrice\">\n";
                            _NewstrGoodBodyest += "                 惊爆价<span class=\"Moneypay\">￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice)) + "</span><br />\n";
                            _NewstrGoodBodyest += "                <span class=\"MarketMoney\">市场价 " + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price)) + "</span>\n";
                            _NewstrGoodBodyest += "             </div>\n";
                            _NewstrGoodBodyest += "              <div class=\"rightButton\">立即购买</div>\n";
                            _NewstrGoodBodyest += "         </div>\n";
                            _NewstrGoodBodyest += "       </div>\n";
                            _NewstrGoodBodyest += "   </li>\n";
                            #endregion
                        }
                    }
                    #endregion for
                }

            }

            return _NewstrGoodBodyest;
        }
    }
}