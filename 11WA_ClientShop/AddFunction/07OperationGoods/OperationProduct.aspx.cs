using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.AddFunction._07OperationGoods
{
    public partial class OperationProduct : System.Web.UI.Page
    {
        private EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
        private EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

        private int pOperationIDGoods = 0;//；  
        private int pIntGoodID = 0;//；  

        /// <summary>
        /// 该变量自己页面会使用
        /// </summary>
        private int pInt_QueryString_ParentID = 0;//；
        /// <summary>
        /// 该变量展示自己的上级
        /// </summary>
        private int pInt_DB_ParentID = 0;//；数据库上级 真正的上级
        //private EggsoftWX.Model.tab_DistributionMoney p_DistributionMoney_List_From_Good_ID = null;
        private bool boolSecondBuydGoodID = false;

        protected string pGoodName = "0";
        protected string pGoodImage = "0";
        protected string pGood_ShortText = "";
        protected string pGood_LongText = "";
        protected string strPubShopClient_BackGroundColor = "";
        protected string strPubShopClient_MenuBackGroundColor = "";
        protected string strPubPingLunWeiXinShare = "";
        protected Decimal pDecimalPromotePrice = 0;
        //protected Decimal pDecimalShopping_Vouchers_Percent = 0;//购物券最大允许金额

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        /// <summary>
        /// 运营中心的ID
        /// </summary>
        protected int pOperationCenterID = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //DateTime tStartTime = DateTime.Now;

                    setAllNeedID();
                    if (pub_Int_Session_CurUserID == 0) return;
                    string type = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(type) == false)
                    {
                        #region 购买 评论 等处理
                        if (type == "buynow")
                        {
                            #region 购买buynow
                            string buyCount = Request.QueryString["buyCount"];
                            string MultiType = Request.QueryString["MultiBuyType"];
                            if (String.IsNullOrEmpty(MultiType)) MultiType = "0";

                            int myIntbool = 0;


                            string strType_Money = Request.QueryString["Type_Money"];
                            string[] strMoney_List = new string[] { "0" };

                            if (String.IsNullOrEmpty(strType_Money) == false)
                            {
                                if (strType_Money == "1")
                                {
                                    string strMoney_ = Request.QueryString["_Money_"];
                                    strMoney_List = new string[] { "1", strMoney_ };
                                }
                            }

                            string strTypeNumber_Vouchers_Bean = Request.QueryString["TypeNumber_Vouchers_Bean"];
                            string[] strNumber_Vouchers_Bean_List = new string[] { "0" };
                            if (String.IsNullOrEmpty(strTypeNumber_Vouchers_Bean) == false)
                            {
                                if (strTypeNumber_Vouchers_Bean == "2")
                                {
                                    string strMoney_ = Request.QueryString["VouchersMoney_"];
                                    strNumber_Vouchers_Bean_List = new string[] { strTypeNumber_Vouchers_Bean, strMoney_ };
                                }
                                else if (strTypeNumber_Vouchers_Bean == "3")
                                {
                                    string strBean_ = Request.QueryString["Bean_"];
                                    strNumber_Vouchers_Bean_List = new string[] { strTypeNumber_Vouchers_Bean, strBean_ };
                                }
                                else if (strTypeNumber_Vouchers_Bean == "4")
                                {
                                    string VouchersNum_ = Request.QueryString["VouchersNum_"];
                                    string VouchersMoney_ = Request.QueryString["VouchersMoney_"];
                                    strNumber_Vouchers_Bean_List = new string[] { strTypeNumber_Vouchers_Bean, VouchersNum_, VouchersMoney_ };
                                }
                                else if (strTypeNumber_Vouchers_Bean == "0")
                                {
                                    //myIntbool = Eggsoft_Public_CL.ShoppingCart.AddToShoppingCart(pub_Int_Session_CurUserID, pIntGoodID, pInt_QueryString_ParentID, Int32.Parse(buyCount), Int32.Parse(MultiType), new string[] { "0" }, 0, 0, 0);
                                }
                            }
                            String strOperationID = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["OperationID"]);////
                            String strOperationIDGoods = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["OperationIDGoods"]);////

                            myIntbool = Eggsoft_Public_CL.ShoppingCart.AddToShoppingCart(pub_Int_Session_CurUserID, pIntGoodID, Int32.Parse(buyCount), Int32.Parse(MultiType), strMoney_List, new string[] { "0" }, strNumber_Vouchers_Bean_List, 6, strOperationID.toInt32(), strOperationIDGoods.toInt32(), "");


                            if (myIntbool != 1)
                            {
                                string strErro = "";
                                if (myIntbool == -1)
                                {
                                    strErro = "购物车或订单中已存在！";
                                }
                                else if (myIntbool == -2)
                                {
                                    strErro = "秒杀期间限制购买！";
                                }
                                else if (myIntbool == -4)
                                {
                                    strErro = "购物券已被使用！";
                                }
                                else if (myIntbool == -44)
                                {
                                    strErro = "库存不足！";
                                }
                                else
                                {
                                    strErro = "未知错误！";
                                }

                                Eggsoft.Common.JsUtil.ShowMsg("购物车添加失败！" + strErro, "/cart.aspx");//继续跳转
                            }
                            else
                            {
                                Eggsoft.Common.JsUtil.LocationNewHref("/cart.aspx?type=buynowfromgood");
                                Response.End();
                            }
                            #endregion 购买buynow
                        }
                        else if (type == "savepinglun")
                        {
                            Response.ContentEncoding = Encoding.GetEncoding("gb2312");

                            int ID_Orderdetails = Convert.ToInt32(Request.QueryString["ID_Orderdetails"]);
                            String strSavePingLun_textarea = (Request.Form["SavePingLun_textarea"]);
                            strSavePingLun_textarea = Eggsoft.Common.CommUtil.NoHTML(strSavePingLun_textarea);//过滤非法输入

                            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                            BLLtab_Orderdetails.Update("Pinglun='" + strSavePingLun_textarea + "'", "id=" + ID_Orderdetails.ToString());
                            Eggsoft.Common.JsUtil.ShowMsg("已成功保存单号" + ID_Orderdetails + "的评论!", "javascript:history.back();");
                            Response.End();
                        }
                        #endregion 购买 评论 等处理
                    }
                    else
                    {
                        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                        string strStyle_Model = "/AddFunction/07OperationGoods/OperationProduct-Goods_Templet.html";
                        string strTemplet = Eggsoft.Common.FileFolder.ReadTemple(strStyle_Model);
                        strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                        strTemplet = strTemplet.Replace("###GoodID###", pIntGoodID.ToString());

                        strTemplet = InitGoods(strTemplet);
                        strTemplet = SecondBuyInfo(strTemplet);
                        strTemplet = InitShopClient(strTemplet);
                        strTemplet = sayPingLun(strTemplet);
                        strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "商品-" + pGoodName));

                        #region 是否关闭
                        //商品页面的分享访问头像及统计可关闭，商户根据自身需要可选择勾选关闭
                        string strCloseCheckBox_ShareGouWuQuan = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "CloseGoodsShareAndStatus") ? "style=\"display:none; \"" : "";
                        strTemplet = strTemplet.Replace("###CheckBox_CloseGoodsShareAndStatus###", strCloseCheckBox_ShareGouWuQuan);
                        #endregion 是否关闭

                        //检查是否是代理
                        int intShareParnetID = 0;
                        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + pub_Int_Session_CurUserID + "   and IsDeleted=0 and (isnull(Empowered, 0) = 1 or OnlyIsAngel=1)");///有代理啊
                        if (boolAgent)
                        {
                            intShareParnetID = pub_Int_Session_CurUserID;
                        }
                        else
                        {
                            intShareParnetID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
                        }


                        strTemplet = strTemplet.Replace("###strhttpUrl###", Eggsoft.Common.Application.getwebHttp());//MyDistributionMoney_List_From_Good_ID()
                        strTemplet = strTemplet.Replace("###strUploadHttp###", Eggsoft_Public_CL.Pub.GetAppConfiugUplaod());//MyDistributionMoney_List_From_Good_ID()

                        strTemplet = InitWeiXinShareLink(strTemplet);


                        strTemplet = strTemplet.Replace("###DistributionMoney_List_From_Good_ID###", "");//MyDistributionMoney_List_From_Good_ID()

                        strTemplet = MyBossImage_MyBuyMarkerImage(strTemplet);

                        string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);

                        strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                        string InfoAlert_TalkMe_Page = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/InfoAlert_TalkMe_Page.html");
                        InfoAlert_TalkMe_Page = InfoAlert_TalkMe_Page.Replace("###SAgentPath###", Pub_Agent_Path);
                        InfoAlert_TalkMe_Page = InfoAlert_TalkMe_Page.Replace("###ToUserID###", "s" + pub_Int_ShopClientID.ToString());
                        strTemplet = strTemplet.Replace("###InfoAlert_TalkMe_Page###", InfoAlert_TalkMe_Page);

                        strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());
                        strTemplet = strTemplet.Replace("###pOperationCenterID###", pOperationCenterID.toString()).Replace("###OperationIDGoods###", pOperationIDGoods.toString());
                        strTemplet = MultiBuyPriceListBuyInfo(strTemplet);



                        Response.Write(strTemplet);

                    }


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
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
            pOperationIDGoods = (Request.QueryString["OperationGoods"]).toInt32();
            pInt_DB_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(pub_Int_Session_CurUserID);
            pOperationCenterID = (Request.QueryString["operationcenterid"]).toInt32();


            #region 设置运营中心默认值 
            if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strShopClientID.toInt32()))
            {
                #region 查找运营zhongxin
                #region 查找本人的运营中心
                EggsoftWX.BLL.b005_UserID_Operation_ID BLL_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                EggsoftWX.Model.b005_UserID_Operation_ID myselfModel = BLL_b005_UserID_Operation_ID.GetModel("userid=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID);
                if (myselfModel != null) pOperationCenterID = myselfModel.OperationCenterID.toInt32();
                #endregion 查找本人的运营中心

                #region 查找父亲的运营中心
                if (pOperationCenterID == 0)
                {
                    EggsoftWX.Model.b005_UserID_Operation_ID myModel = BLL_b005_UserID_Operation_ID.GetModel("userid=" + pInt_DB_ParentID + " and ShopClientID=" + pub_Int_ShopClientID);
                    if (myModel != null) pOperationCenterID = myModel.OperationCenterID.toInt32();
                }
                #endregion 查找父亲的运营中心


                if (pOperationCenterID == 0) pOperationCenterID = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "ConsumptionCapital_OperationCenterID").toInt32();///
                if (pOperationCenterID == 0) Eggsoft.Common.JsUtil.ShowMsg("请先设置默认的运营中心！", "/cart.aspx");//继续跳转
                #endregion 查找运营zhongxin

                EggsoftWX.BLL.b004_OperationGoods BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                if (pOperationIDGoods == 0)
                {
                    System.Data.DataTable Data_DataTable_OperationGoods = BLL_b004_OperationGoods.SelectList("select top 1 GoodID,ID from b004_OperationGoods where ShopClient_ID=" + strShopClientID + " and IsDeleted=0 order by id desc ").Tables[0];
                    if (Data_DataTable_OperationGoods.Rows.Count > 0)
                    {
                        pIntGoodID = Data_DataTable_OperationGoods.Rows[0]["GoodID"].toInt32();
                        pOperationIDGoods = Data_DataTable_OperationGoods.Rows[0]["ID"].toInt32();
                    }
                }
                else
                {
                    System.Data.DataTable Data_DataTable_OperationGoods = BLL_b004_OperationGoods.SelectList("select top 1 GoodID from b004_OperationGoods where id=" + pOperationIDGoods + " and ShopClient_ID=" + strShopClientID + " and IsDeleted=0 order by id desc ").Tables[0];
                    if (Data_DataTable_OperationGoods.Rows.Count > 0)
                    {
                        pIntGoodID = Data_DataTable_OperationGoods.Rows[0]["GoodID"].toInt32();
                    }
                }
            }
            else
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("无法定位商品=“" + pIntGoodID + "”  strShopClientID=" + strShopClientID, "运营中心模式检测", "运营中心模式不存在");
            }
            #endregion


            #region 初始化所有运营中心数据  粉丝数据
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strShopClientID.toInt32()))
                {
                    Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(pub_Int_Session_CurUserID, pub_Int_ShopClientID, pOperationCenterID);
                }
            });
            #endregion 初始化所有运营中心数据
        }


        private string MultiBuyPriceListBuyInfo(string strargBody)
        {
            string strMultiBuyPriceListBuyInfo = "";


            try
            {
                EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice BLL_tab_Goods_MultiSelectTypePrice = new EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice();
                EggsoftWX.Model.tab_Goods_MultiSelectTypePrice Model_tab_Goods_MultiSelectTypePrice = new EggsoftWX.Model.tab_Goods_MultiSelectTypePrice();



                System.Data.DataTable myDataTable = BLL_tab_Goods_MultiSelectTypePrice.GetList("GoodId=" + pIntGoodID + " order by id asc").Tables[0];


                if (myDataTable.Rows.Count > 0)
                {
                    strMultiBuyPriceListBuyInfo += "<div id=\"style_clor\" class=\"spro_c2\">\n";
                    strMultiBuyPriceListBuyInfo += "   <span style=\"float:left; line-height:34px; height:34px;\">类别选择：</span>\n";

                    for (int i = 0; i < myDataTable.Rows.Count; i++)
                    {
                        string MultiID = myDataTable.Rows[i]["id"].ToString();
                        string GoodMultiName = myDataTable.Rows[i]["GoodMultiName"].ToString();
                        string Price_Num = myDataTable.Rows[i]["GoodPrice"].ToString();
                        Price_Num = Decimal.Parse(Price_Num).ToString("###0.00");

                        strMultiBuyPriceListBuyInfo += "        <a href=\"javascript:void(0)\" title=\"" + MultiID + "\"><span title=\"&yen;" + Price_Num + "\">" + GoodMultiName + "</span></a> \n";
                    }
                    strMultiBuyPriceListBuyInfo += "</div>\n";
                }
                strargBody = strargBody.Replace("###ChildPriceList###", strMultiBuyPriceListBuyInfo);

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            return strargBody;
        }

        private string SecondBuyInfo(string strargBody)
        {
            try
            {
                string strSecondBuyInfoBody = "<div class=\"spro_c2\"></div>";

                EggsoftWX.BLL.View_SecondSalesGoodList bll_View_SecondSalesGoodList = new EggsoftWX.BLL.View_SecondSalesGoodList();
                EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = new EggsoftWX.Model.View_SecondSalesGoodList();

                boolSecondBuydGoodID = bll_View_SecondSalesGoodList.Exists("ID=" + pIntGoodID);

                strargBody = strargBody.Replace("###Get_UnitNameFromGoodID###", Eggsoft_Public_CL.GoodP.Get_UnitNameFromGoodID(pIntGoodID));

                if (boolSecondBuydGoodID)
                {
                    Model_View_SecondSalesGoodList = bll_View_SecondSalesGoodList.GetModel("ID=" + pIntGoodID);


                    strSecondBuyInfoBody = "<div class=\"spro_c2\" style=\"color: #E4272D;\"><span id=\"MemoryStatus\" style=\"display:none;\"></span>";

                    string strShowCountDown = "<script language=\"javascript\" type=\"text/javascript\">\n";
                    strShowCountDown += "var interval = 1000;\n";
                    strShowCountDown += "function ShowCountDown(year, month, day,hh,mm,ss, divname) {\n";
                    strShowCountDown += "    var now = new Date();\n";
                    strShowCountDown += "    var endDate = new Date(year,month-1 , day,hh,mm,ss);\n";
                    strShowCountDown += "   var leftTime = endDate.getTime() - now.getTime();\n";
                    strShowCountDown += "    var leftsecond = parseInt(leftTime / 1000);\n";
                    strShowCountDown += "    var day1 = Math.floor(leftsecond / (60 * 60 * 24));\n";
                    strShowCountDown += "    var hour = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);\n";
                    strShowCountDown += "    var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour * 3600) / 60);\n";
                    strShowCountDown += "    var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour * 3600 - minute * 60);\n";
                    strShowCountDown += "    var ccCountTime = document.getElementById(divname);\n";
                    strShowCountDown += "document.getElementById(\"MemoryStatus\").innerHTML=day1;\n";
                    strShowCountDown += "var cc = document.getElementById(\"MemoryStatus\").innerHTML;  var varCC=parseInt(cc);  \n";

                    if ((Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenSalesSecond) < 0) && (Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenEndSalesSecond) > 0))
                    {
                        strSecondBuyInfoBody += "本商品正在秒杀！" + "目前仅有库存量:" + Model_View_SecondSalesGoodList.KuCunCount + "<br />";
                        strSecondBuyInfoBody += "购买需马上支付！价格以实际支付时间为准!请抓住千钧一发的机会!" + "<br />";
                        strSecondBuyInfoBody += "秒杀价格:" + Eggsoft_Public_CL.Pub.getPubMoney(Model_View_SecondSalesGoodList.LimitTimerBuy_TimePrice) + "元" + "<br />";
                        strSecondBuyInfoBody += "秒杀限制数量:" + Model_View_SecondSalesGoodList.LimitTimerBuy_MaxSalesCount + Eggsoft_Public_CL.GoodP.Get_UnitNameFromGoodID(Model_View_SecondSalesGoodList.ID) + "<br />";


                        strShowCountDown += "if ((varCC=>0) && (day1<0))\n {\nlocation.reload();\n}\n";//重载当前页面；
                        strShowCountDown += "}\n";
                        strShowCountDown += "</script>\n";
                        strSecondBuyInfoBody += strShowCountDown;
                        strSecondBuyInfoBody += "秒杀到期时间:<span id=\"CountDownTime\">" + Model_View_SecondSalesGoodList.ShowWhenEndSales + "</span><br />";
                    }
                    else if (Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenSalesSecond) > 0)
                    {
                        strShowCountDown += "if ((varCC=>0) && (day1<0))\n {\nlocation.reload();\n}\n";//重载当前页面；
                        strShowCountDown += "}\n";
                        strShowCountDown += "</script>\n";
                        strSecondBuyInfoBody += strShowCountDown;
                        strSecondBuyInfoBody += "本商品还有" + "<span id=\"CountDownTime\">" + Model_View_SecondSalesGoodList.ShowWhenSales + "</span>开始秒杀！" + "<br />";
                        strSecondBuyInfoBody += "秒杀价格:" + Eggsoft_Public_CL.Pub.getPubMoney(Model_View_SecondSalesGoodList.LimitTimerBuy_TimePrice) + "元" + "<br />";
                        strSecondBuyInfoBody += "秒杀限制数量:" + Model_View_SecondSalesGoodList.LimitTimerBuy_MaxSalesCount + Eggsoft_Public_CL.GoodP.Get_UnitNameFromGoodID(Model_View_SecondSalesGoodList.ID) + "<br />";
                    }
                    else if (Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenEndSalesSecond) < 0)
                    {
                        /*
                         strSecondBuyInfoBody += "本商品秒杀已结束" + "<span id=\"CountDownTime\">" + Model_View_SecondSalesGoodList.ShowWhenEndSales + "</span>！" + "<br />";
                        strSecondBuyInfoBody += "秒杀价格:" + Eggsoft_Public_CL.Pub.getPubMoney(Model_View_SecondSalesGoodList.LimitTimerBuy_TimePrice) + "元" + "<br />";
                        strSecondBuyInfoBody += "秒杀限制数量:" + Model_View_SecondSalesGoodList.LimitTimerBuy_MaxSalesCount + Eggsoft_Public_CL.GoodP.Get_UnitNameFromGoodID(Model_View_SecondSalesGoodList.ID) + "<br />";
                        strSecondBuyInfoBody += "请等待下一轮秒杀." + "<br />";
                         */
                    }
                    strSecondBuyInfoBody += "</div>";
                }
                System.Data.DataSet myds = bll_View_SecondSalesGoodList.GetList("*", "1=1 order by ShowWhenEndSales asc");

                strargBody = strargBody.Replace("###SecondBuyInfo###", strSecondBuyInfoBody);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return strargBody;
        }


        private string MyBossImage_MyBuyMarkerImage(string strargBody)
        {
            try
            {
                //string strMyBossImage = "";
                //string strMyBuyMarkerImage = "";


                #region///运营中心及  上家的logo

                #region///运营中心
                string strBossOperationCenterNickNameShow = "";
                string strMyBossOperationCenterImage = "";
                if (pOperationCenterID > 0)
                {
                    EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = new EggsoftWX.Model.b002_OperationCenter();
                    Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel(pOperationCenterID);
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                    Model_tab_User = BLL_tab_User.GetModel(Model_b002_OperationCenter.UserID.toInt32());

                    if (Model_b002_OperationCenter != null && Model_b002_OperationCenter != null)

                        if (string.IsNullOrEmpty(Model_tab_User.HeadImageUrl) == false)
                        {
                            string strPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User.ID.ToString()));

                            //本地文件
                            string strHead_Center_Image = @"" + strPath + "/HeadImage/User" + Eggsoft.Common.StringNum.Add000000Num(Model_tab_User.ID, 6) + ".jpg";

                            //远程文件
                            string strGet_HeadImage_Center_UserID = Model_tab_User.HeadImageUrl;
                            if (Eggsoft.Common.FileFolder.RemoteFileExists(Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Center_Image))
                            {
                                strMyBossOperationCenterImage = "<a  target=\"_blank\" href=" + strGet_HeadImage_Center_UserID + "> <img width=\"40\" height=\"40\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Center_Image + "\"/></a>"; ///我share it 我是中心
                            }
                            else
                            {

                            }
                        }

                    if (string.IsNullOrEmpty(Model_tab_User.NickName) == false)//没有头像  那只写文字
                    {
                        strBossOperationCenterNickNameShow = Model_tab_User.NickName + "运营";
                    }
                    else //没有头像 文字  那只写微店号
                    {
                        strBossOperationCenterNickNameShow = "微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(Model_tab_User.ShopUserID.ToString()) + "运营";
                    }
                }


                strargBody = strargBody.Replace("###MyBossOperationCenterImage###", strMyBossOperationCenterImage);
                strargBody = strargBody.Replace("###BossOperationCenterNickNameShow###", strBossOperationCenterNickNameShow);
                #endregion

                #region 上家的logo
                string strBossNickNameShow = "";
                string strMyBossImage = "";

                if (pInt_DB_ParentID != 0)
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                    Model_tab_User = BLL_tab_User.GetModel(pInt_DB_ParentID);

                    if (string.IsNullOrEmpty(Model_tab_User.HeadImageUrl) == false)
                    {
                        string strPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(pInt_DB_ParentID.ToString()));

                        //本地文件
                        string strHead_Parent_Image = @"" + strPath + "/HeadImage/User" + Eggsoft.Common.StringNum.Add000000Num(pInt_DB_ParentID, 6) + ".jpg";

                        //远程文件
                        string strGet_HeadImage_Parent_UserID = Model_tab_User.HeadImageUrl;

                        if (Eggsoft.Common.FileFolder.RemoteFileExists(Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Parent_Image))
                        {
                            strMyBossImage = "<a  target=\"_blank\" href=" + strGet_HeadImage_Parent_UserID + "> <img width=\"40\" height=\"40\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Parent_Image + "\"/></a>"; ///我share it 我是老大

                        }
                        else
                        {


                        }
                    }

                    if (string.IsNullOrEmpty(Model_tab_User.NickName) == false)//没有头像  那只写文字
                    {
                        strBossNickNameShow = Eggsoft_Public_CL.Pub.GetNickName(pInt_DB_ParentID.ToString()) + "推荐";
                    }
                    else //没有头像 文字  那只写微店号
                    {
                        strBossNickNameShow = "微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pInt_DB_ParentID.ToString()) + "推荐";
                    }

                    //}
                }
                else
                {
                    //strMyBuyMarkerImage = "";
                }
                strargBody = strargBody.Replace("###MyBossImage###", strMyBossImage);

                strargBody = strargBody.Replace("###BossNickNameShow###", strBossNickNameShow);
                #endregion 上家的logo
                #endregion


                #region///检查是否购买过   已过期
                // strMyBossImage += " <li><div class=\"zan\"><span class=\"ys_zan123\">406</span></div></li>";
                // strMyBossImage += " <li><div class=\"sp_Zan_ro_c2\"><span class=\"SayOKOmage\">33145</span></div></li>";



                #endregion

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return strargBody;
        }



        private string InitWeiXinShareLink(string strTemplet)
        {
            //string cssFrageMent = "";
            try
            {
                string strDESFullName = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pGood_ShortText));
                string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(new EggsoftWX.BLL.tab_Goods().GetList("Icon", "ID=" + pIntGoodID).Tables[0].Rows[0]["Icon"].ToString(), 100);
                string strFirstImageFullName = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;
                #region///检查是否购买过  y加上我的id  不是的话 加上上家的ID  如果没有 那就写0  //已过期


                String GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                if (String.IsNullOrEmpty(GetNickName) == false)
                {
                    strDESFullName = GetNickName + ":" + strDESFullName;
                }
                else
                {
                    // strDESFullName = strDESFullName;
                }
                #endregion
                string strURL = Eggsoft.Common.Application.AppUrl + Pub_Agent_Path + "/op-" + pOperationCenterID + "-" + pOperationIDGoods + ".aspx";
                Eggsoft.Common.debug_Log.Call_WriteLog("strURL=" + strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对


                //Eggsoft.Common.JsUtil.ShowMsg(strURL);

                strPubPingLunWeiXinShare = Eggsoft.Common.CommUtil.GetMainContent(strPubPingLunWeiXinShare); ;
                string strDes = Eggsoft.Common.CommUtil.getShortText(strPubPingLunWeiXinShare + strDESFullName, 80);

                strTemplet = strTemplet.Replace("###_PulicChageWeiXin_Small_Goods_Picture###", strFirstImageFullName);
                strTemplet = strTemplet.Replace("###_PulicChageWeiXin_MySonWillOpenLind_Goods###", strURL);
                strTemplet = strTemplet.Replace("###_PulicChageWeiXin_Des_Goods###", strDes);
                strTemplet = strTemplet.Replace("###_PulicChageWeiXin_Title_Goods###", Eggsoft.Common.CommUtil.getShortText(pGoodName, 80));

                //Eggsoft.Common.debug_Log.Call_WriteLog("cssFrageMent=" + cssFrageMent);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareGoodsFunction");//
            return strTemplet;
        }




        private string sayPingLun(string strargBody)
        {

            try
            {
                string strOthersPeopleSaySomthing = "";
                string strIHaveBuyIwilSaySomething = "";


                EggsoftWX.BLL.View_SalesGoods BLLView_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();
                System.Data.DataTable myDataTable = BLLView_SalesGoods.GetList("GoodID=" + pIntGoodID + " order by ID_Orderdetails").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strPayDateTime = myDataTable.Rows[i]["PayDateTime"].ToString();
                    string strGoodName = myDataTable.Rows[i]["GoodName"].ToString();
                    string strNickName = myDataTable.Rows[i]["NickName"].ToString();
                    string strPinglun = myDataTable.Rows[i]["Pinglun"].ToString().Trim();
                    string strID_Orderdetails = myDataTable.Rows[i]["ID_Orderdetails"].ToString();
                    string strUserID = myDataTable.Rows[i]["UserID"].ToString();


                    if (Int32.Parse(strUserID) == pub_Int_Session_CurUserID)
                    {
                        if (strPubPingLunWeiXinShare.IndexOf(strNickName) == -1) strPubPingLunWeiXinShare += strNickName;

                        string strCanWriteForm = "";
                        strCanWriteForm += "<form id=\"SavePingLunform" + strID_Orderdetails + "\" name=\"SavePingLunform" + strID_Orderdetails + "\" method=\"post\" action=\"?type=savepinglun&id_orderdetails=" + strID_Orderdetails + "\">\n";
                        strCanWriteForm += "<div id=\"putpl\">  \n";
                        strCanWriteForm += " <textarea name=\"SavePingLun_textarea\"  class=\"pltext\" placeholder=\"我的评论（单号：" + strID_Orderdetails + "）：" + strPayDateTime + "购买" + strGoodName + "后请点击输入我的评论\"  cols=\"\" rows=\"\">\n";
                        strCanWriteForm += strPinglun + "";
                        strCanWriteForm += "</textarea>\n";
                        strCanWriteForm += "<div class=\"cl\"></div>\n";
                        strCanWriteForm += "</div> \n";
                        strCanWriteForm += "<div class=\"putplbtn cl\"> \n";
                        strCanWriteForm += "<input type=\"submit\" id=\"savebtn\"  class=\"spro_grenbtn\" value=\"保存\"> \n";
                        strCanWriteForm += "</div> \n";
                        strCanWriteForm += " </form> \n";


                        strIHaveBuyIwilSaySomething += strCanWriteForm;
                    }
                    else
                    {

                        if (strPinglun.Length == 0)
                        {
                            continue;
                        }

                        string strReadOnlyForm = "";
                        strReadOnlyForm += "<div id=\"putpl\">  \n";
                        strReadOnlyForm += Eggsoft_Public_CL.Pub.GetNickName(strUserID) + "评论：" + strPinglun + "";
                        strReadOnlyForm += "<div class=\"cl\"></div>\n";
                        strReadOnlyForm += "</div> \n";
                        strOthersPeopleSaySomthing += strReadOnlyForm;
                    }

                }
                strargBody = strargBody.Replace("###MeAndOthersPeopleSaySomthingCount###", myDataTable.Rows.Count.ToString());

                strargBody = strargBody.Replace("###OthersPeopleSaySomthing###", strOthersPeopleSaySomthing);

                strargBody = strargBody.Replace("###IHaveBuyIwilSaySomething###", strIHaveBuyIwilSaySomething);

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            return strargBody;
        }

        private string InitGoods(string strargBody)
        {

            try
            {


                #region background


                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);

                #endregion goodImageList



                #region 暂无库存  dendeng
                pGoodName = my_Model_tab_Goods.Name;
                strargBody = strargBody.Replace("###ShopGoodName###", pGoodName);
                strargBody = strargBody.Replace("###ShopTitleName###", pub_GetAgentShopName_From_Visit__);

                #region  处理字典 ShopClient_Dictionaries
                EggsoftWX.BLL.tab_Goods_XML_Goods_ID BLL_tab_Goods_XML_Goods_ID = new EggsoftWX.BLL.tab_Goods_XML_Goods_ID();
                bool bool_ShopClient_Dictionaries = BLL_tab_Goods_XML_Goods_ID.Exists("GoodID=" + pIntGoodID);
                Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries XML_Class_ShopClient_Dictionaries = new Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries();
                if (bool_ShopClient_Dictionaries == true)
                {
                    try
                    {
                        string strXMLName_ID = BLL_tab_Goods_XML_Goods_ID.GetList("XMLName_ID", "GoodID=" + pIntGoodID).Tables[0].Rows[0]["XMLName_ID"].ToString();
                        string strXML = new EggsoftWX.BLL.tab_Goods_XML().GetList("XMLContent", "ID=" + strXMLName_ID).Tables[0].Rows[0]["XMLContent"].ToString();
                        XML_Class_ShopClient_Dictionaries = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries>(strXML, System.Text.Encoding.UTF8);

                    }
                    catch (Exception Exceptione)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                    }
                    finally { }
                }
                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(pub_Int_Session_CurUserID);
                strargBody = strargBody.Replace("###NetUserSafeCode###", Eggsoft.Common.DESCrypt.hex_md5_2(Model_tab_User.SafeCode));



                #region 判断是否 省代 市代
                bool bool_Agent_Exsit = false;
                Decimal Decimal_ProductMoney = 0;
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID + "  and IsDeleted=0  and ShopClientID=" + pub_Int_ShopClientID);
                EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                if (Model_tab_ShopClient_Agent_ != null)
                {
                    if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)
                    {
                        bool_Agent_Exsit = BLL_tab_ShopClient_Agent__ProductClassID.Exists("UserID=" + pub_Int_Session_CurUserID + " and ProductID=" + pIntGoodID + " and Empowered=1" + " and ShopClientID=" + pub_Int_ShopClientID);
                    }
                }
                string strAgentLevelName = "";
                if (bool_Agent_Exsit)
                {
                    try
                    {
                        // EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID);

                        EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                        EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetModel(Model_tab_ShopClient_Agent_.AgentLevelSelect.toInt32());

                        strAgentLevelName = Model_tab_ShopClient_Agent_Level.AgentLevelName;

                        EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();
                        EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("shopClient_Agent_Level_ID=" + Model_tab_ShopClient_Agent_.AgentLevelSelect + " and ProductID=" + pIntGoodID);
                        if (Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID != null)
                        {
                            Decimal_ProductMoney = Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID.ProductPrice.toDecimal();
                        }
                    }
                    catch { }
                    finally { }
                }
                #endregion
                if (String.IsNullOrEmpty(strAgentLevelName))
                {
                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_PromotionalPrice###", XML_Class_ShopClient_Dictionaries.PromotionalPrice);
                }
                else
                {
                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_PromotionalPrice###", strAgentLevelName);
                }

                #region 包邮  邮件模版的处理
                if ((my_Model_tab_Goods.FreightTemplate_ID != null) && (my_Model_tab_Goods.FreightTemplate_ID != 0))
                {
                    bool boolGoodShow = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "GoodsShowYunFei");
                    if (boolGoodShow)
                    {
                        // strargBody = strargBody.Replace("###ShopClient_Dictionaries_Postage###", "含运费");
                        strargBody = strargBody.Replace("###ShopClient_Dictionaries_Postage###", "");///暂时删除  要多测试几个平台 否则 会出大问题
                    }
                    else
                    {
                        strargBody = strargBody.Replace("###ShopClient_Dictionaries_Postage###", "");
                    }
                }
                else
                {

                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_Postage###", "(" + XML_Class_ShopClient_Dictionaries.Postage + ")");

                }
                #endregion
                strargBody = strargBody.Replace("###ShopClient_Dictionaries_PriceText###", XML_Class_ShopClient_Dictionaries.PriceText);
                strargBody = strargBody.Replace("###ShopClient_Dictionaries_StockBalance###", XML_Class_ShopClient_Dictionaries.StockBalance);
                //strargBody = strargBody.Replace("###ShopClient_Dictionaries_Check_NeedRegIfBuy###", () ? "true" : "false");

                bool boolBuyIfNeedSub = (XML_Class_ShopClient_Dictionaries.NeedRegIfBuy == true) && (Model_tab_User.Subscribe == false);
                if (boolBuyIfNeedSub)
                {
                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_Check_NeedRegIfBuy###", "true");
                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + pub_Int_ShopClientID);
                    string strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;
                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_Check_NeedRegIfBuy___Link###", strGet_GuideSubscribePageFromWeiXinD_ShopClientID_);

                }
                else
                {
                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_Check_NeedRegIfBuy###", "false");
                }


                #endregion




                if (my_Model_tab_Goods.KuCunCount <= 0)
                {

                    strargBody = strargBody.Replace("###CanBuy###", XML_Class_ShopClient_Dictionaries.NoStock);
                    strargBody = strargBody.Replace("###CanAddCart###", XML_Class_ShopClient_Dictionaries.NoStock);
                    strargBody = strargBody.Replace("###Salesd###", "1");
                    strargBody = strargBody.Replace("###CanAddCartAninate###", "");///购物车动画 
                }
                else
                {
                    strargBody = strargBody.Replace("###CanBuy###", XML_Class_ShopClient_Dictionaries.BuyNow);
                    strargBody = strargBody.Replace("###CanAddCart###", XML_Class_ShopClient_Dictionaries.AddToCart);
                    strargBody = strargBody.Replace("###Salesd###", "0");
                    strargBody = strargBody.Replace("###CanAddCartAninate###", " Q-buy-btn");///购物车动画 
                }



                pGood_ShortText = my_Model_tab_Goods.ShortInfo.Trim();

                strargBody = strargBody.Replace("###ShortInfo###", pGood_ShortText);

                pGood_LongText = my_Model_tab_Goods.LongInfo.Trim();
                string strpGood_LongText_HtmlDecode = Server.HtmlDecode(pGood_LongText);

                /*正则表达式替换求助
        如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
        例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                strpGood_LongText_HtmlDecode = Regex.Replace(strpGood_LongText_HtmlDecode, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");

                string strSearch = "src=\"/upload/";
                string strpGood_LongText = strpGood_LongText_HtmlDecode.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                strSearch = "src=\"/Upload/";
                strpGood_LongText = strpGood_LongText.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");


                strargBody = strargBody.Replace("###LongInfo###", strpGood_LongText);

                //###Price###
                //###PromotePrice###

                strargBody = strargBody.Replace("###Price###", Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.Price)));

                pDecimalPromotePrice = Convert.ToDecimal(my_Model_tab_Goods.PromotePrice);

                if (bool_Agent_Exsit && Decimal_ProductMoney > 0)
                {

                    strargBody = strargBody.Replace("###PromotePriceTotalNoneShowDecimal###", Eggsoft_Public_CL.Pub.getPubMoney(Decimal_ProductMoney));
                    strargBody = strargBody.Replace("###PromotePrice###", Eggsoft_Public_CL.Pub.getPubMoney(Decimal_ProductMoney));
                    strargBody = strargBody.Replace("###PriceZheJou###", Eggsoft_Public_CL.Pub.getPubPromotePrice_ZheKou(Decimal_ProductMoney, Convert.ToDecimal(my_Model_tab_Goods.Price)));
                    //}
                }
                else
                {
                    strargBody = strargBody.Replace("###PromotePriceTotalNoneShowDecimal###", Eggsoft_Public_CL.Pub.getPubMoney(pDecimalPromotePrice));
                    strargBody = strargBody.Replace("###PromotePrice###", Eggsoft_Public_CL.Pub.getPubMoney(pDecimalPromotePrice));
                    strargBody = strargBody.Replace("###PriceZheJou###", Eggsoft_Public_CL.Pub.getPubPromotePrice_ZheKou(pDecimalPromotePrice, Convert.ToDecimal(my_Model_tab_Goods.Price)));
                }

                //---
                ///
                #region 买一送三 买一优惠三
                EggsoftWX.BLL.b004_OperationGoods BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = BLL_b004_OperationGoods.GetModel(pOperationIDGoods);
                string strReturnConsumerWealth = Model_b004_OperationGoods.ReturnConsumerWealth.toInt32().toString();
                strReturnConsumerWealth = strReturnConsumerWealth.Replace("0", "").Replace("1", "一").Replace("2", "二").Replace("3", "三").Replace("4", "四").Replace("5", "五").Replace("6", "六").Replace("7", "七").Replace("8", "八").Replace("9", "九");

                if (string.IsNullOrEmpty(strReturnConsumerWealth))
                {
                    strargBody = strargBody.Replace("###ReturnConsumerWealth###", "");
                }
                else
                {
                    strargBody = strargBody.Replace("###ReturnConsumerWealth###", "买一优惠" + strReturnConsumerWealth);
                }
                #endregion 买一送三  买一优惠三

                strargBody = strargBody.Replace("###GoodNumber###", Eggsoft_Public_CL.GoodP.GetGood_Num_ID_From_Good_ID(pIntGoodID).ToString());

                strargBody = strargBody.Replace("###GoodID###", pIntGoodID.ToString());
                strargBody = strargBody.Replace("###ParentID###", pInt_QueryString_ParentID.ToString());
                strargBody = strargBody.Replace("###DBParentID###", pInt_DB_ParentID.ToString());

                string strGaoDao_GoodShareBase = Eggsoft_Public_CL.Pub.stringShowPower(my_Model_tab_Goods.ShopClient_ID.ToString(), "GaoDao_GoodShareBase");///
                Int32 intGaoDao_GoodShareBase = 0;
                Int32.TryParse(strGaoDao_GoodShareBase, out intGaoDao_GoodShareBase);

                int intByShareAskCount = Eggsoft_Public_CL.GoodP.ByShareAskCount(pIntGoodID);
                if (intByShareAskCount > 0)
                {



                    strargBody = strargBody.Replace("###ByShareAskCount###", " 分享量:" + (intByShareAskCount + intGaoDao_GoodShareBase).ToString());
                }
                else
                {
                    if (intGaoDao_GoodShareBase > 0)
                    {
                        strargBody = strargBody.Replace("###ByShareAskCount###", " 分享量:" + (intGaoDao_GoodShareBase).ToString());
                    }
                    strargBody = strargBody.Replace("###ByShareAskCount###", "");
                }

                int intBySalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(pIntGoodID);
                if (intBySalesCount > 0)
                {
                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_SalesCount###", XML_Class_ShopClient_Dictionaries.SalesCount + ":");
                    strargBody = strargBody.Replace("###SalesCount###", "" + intBySalesCount.ToString() + "");
                }
                else
                {

                    strargBody = strargBody.Replace("###ShopClient_Dictionaries_SalesCount###", "");
                    strargBody = strargBody.Replace("###SalesCount###", "");

                }
                string strGaoDao_HitCount = Eggsoft_Public_CL.Pub.stringShowPower(my_Model_tab_Goods.ShopClient_ID.ToString(), "GaoDao_HitCount");///
                Int32 intGaoDao_HitCount = 0;
                Int32.TryParse(strGaoDao_HitCount, out intGaoDao_HitCount);


                strargBody = strargBody.Replace("###HitCount###", "点赞量:" + (Eggsoft_Public_CL.GoodP.ByHitCount(pIntGoodID) + intGaoDao_HitCount).ToString());



                strargBody = strargBody.Replace("###MinSalesdCount_MinOrderNum###", my_Model_tab_Goods.MinOrderNum.ToString());
                #endregion

                #region 消费财富协议
                strargBody = strargBody.Replace("###ConsumerWealthAgreement###", Server.HtmlDecode(Model_b004_OperationGoods.ConsumerWealthAgreement.ToString()));
                strargBody = strargBody.Replace("###boolShowConsumerWealthAgreement###", Model_b004_OperationGoods.ShowConsumerWealthAgreement.toString());
                strargBody = strargBody.Replace("###ConsumerWealthAgreementGoodID###", Model_b004_OperationGoods.DiscountGoodID.toString());


                EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = new EggsoftWX.Model.b002_OperationCenter();
                Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel(pOperationCenterID);
                strargBody = strargBody.Replace("###RepleaceCentgerName###", Model_b002_OperationCenter.MasterName.toString());


                #endregion 消费财富协议

                #region 购物车 财富车
                if (Model_b004_OperationGoods.ReturnConsumerWealth > 0)
                {
                    strargBody = strargBody.Replace("###button_AddShopCar###", "加入财富");
                    strargBody = strargBody.Replace("###button_ShopCar###", "财富车");

                }
                else
                {
                    strargBody = strargBody.Replace("###button_AddShopCar###", "加入购物车");
                    strargBody = strargBody.Replace("###button_ShopCar###", "购物车");
                }
                #endregion 购物车 财富车




                #region 最大购买数量 检查秒杀的情况
                int KuCunCount = Convert.ToInt32(my_Model_tab_Goods.KuCunCount);
                if (KuCunCount < 0)
                {
                    Response.Write("库存设置不足");
                    Response.End();
                }

                int MaxOrderNum = Convert.ToInt32(my_Model_tab_Goods.MaxOrderNum);
                MaxOrderNum = KuCunCount < MaxOrderNum ? KuCunCount : MaxOrderNum;

                int LimitTimerBuy_MaxSalesCount = 99999;

                EggsoftWX.BLL.View_SecondSalesGoodList bll_View_SecondSalesGoodList = new EggsoftWX.BLL.View_SecondSalesGoodList();
                EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = new EggsoftWX.Model.View_SecondSalesGoodList();
                bool boolGoodID = bll_View_SecondSalesGoodList.Exists("ID=" + pIntGoodID);


                if (boolGoodID)
                {
                    Model_View_SecondSalesGoodList = bll_View_SecondSalesGoodList.GetModel(pIntGoodID);

                    if ((Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenSalesSecond) < 0) && (Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenEndSalesSecond) > 0))///处于秒杀期间
                    {
                        LimitTimerBuy_MaxSalesCount = Model_View_SecondSalesGoodList.LimitTimerBuy_MaxSalesCount;
                    }
                }
                MaxOrderNum = LimitTimerBuy_MaxSalesCount < MaxOrderNum ? LimitTimerBuy_MaxSalesCount : MaxOrderNum;//取一个较小的值 用于限制购买            
                strargBody = strargBody.Replace("###MaxSalesdCount_MinOrderNum###", MaxOrderNum.ToString());
                #endregion


                #region  下架状态
                string strisSaled = "";
                if (Convert.ToBoolean(my_Model_tab_Goods.isSaled))
                {
                    if (my_Model_tab_Goods.KuCunCount > 0)
                    {
                        strisSaled = my_Model_tab_Goods.KuCunCount.ToString();
                    }
                    else
                    {
                        strisSaled = "无";
                    }
                }
                else
                {
                    strisSaled = "<span style=\"color:red\">下架状态！</span>";
                }
                strargBody = strargBody.Replace("###isSaled_KuCunLiang###", strisSaled);

                //strargBody = strargBody.Replace("###PreGoods###", "Goods-" + Eggsoft_Public_CL.GoodP.getPreID(pIntGoodID).ToString() + ".aspx");
                //strargBody = strargBody.Replace("###NextGoods###", "Goods-" + Eggsoft_Public_CL.GoodP.NextPreID(pIntGoodID).ToString() + ".aspx");

                #endregion


            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "消费财富协议", "程序报错pIntGoodID=" + pIntGoodID);
            }
            finally
            {

            }

            return strargBody;
        }

        private string getShopping_Vouchers_Beans_Money(EggsoftWX.Model.tab_ShopClient Model_ShopClient)
        {
            string Shopping_Vouchers_Beans_Money = "";//秒杀不支持 购物券
            Shopping_Vouchers_Beans_Money += "   <span id=\"SpanOnleyOneShow_Number_Money\" style=\"display: none;\"></span><span id=\"SpanOnleyOneShow_Number_Vouchers_Bean\" style=\"display: none;\"></span>\n";
            Decimal goodTotalCreditsMoney_Vouchers = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(pub_Int_Session_CurUserID, out goodTotalCreditsMoney_Vouchers);  //
            Decimal minCountMoney_Vouchers_Number = 0;



            if ((minCountMoney_Vouchers_Number > (Decimal)0.0001))//  没钱不显示
            {
                Shopping_Vouchers_Beans_Money += "<div class=\"buyInfo_WeBuy_Quan\" style=\"margin-top:5px;padding-top:2px;\">\n";
                Shopping_Vouchers_Beans_Money += "   <div class=\"buyInfo_Left\">\n";
                Shopping_Vouchers_Beans_Money += "       <div class=\"spro_C3_Money_Vouchers\">\n";
                Shopping_Vouchers_Beans_Money += "           <span id=\"Quaninfo_Money_Vouchers\"><a href=\"" + Pub_Agent_Path + "/multibutton_showmoney_vouchers.aspx\" target=\"_blank\">可用" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "</a>：¥<span id=\"CanCheckMoney2_Vouchers\">" + Eggsoft_Public_CL.Pub.getPubMoney(minCountMoney_Vouchers_Number) + "</span>&nbsp;&nbsp;<span style=\"display: none;\" id=\"NowCanUseMoney_Vouchers_ShowText\"></span><span style=\"display: none;\" id=\"NowCanUseMoney2_Vouchers\"></span></span>\n";
                Shopping_Vouchers_Beans_Money += "       </div>\n";
                Shopping_Vouchers_Beans_Money += "   </div>\n";
                Shopping_Vouchers_Beans_Money += "   <label class=\"iosCheck\" id=\"iosCheck_Shoping_Money_Vouchers\">\n";
                Shopping_Vouchers_Beans_Money += "       <input id=\"Span2ClickButton_WeiBaiQuan\" onclick=\"show_hidden_Money_Vouchers();\" type=\"checkbox\" /><i></i></label>\n";
                Shopping_Vouchers_Beans_Money += "</div>\n";
            }

            #region  余额购买 任何产品都支持  任何秒杀都支持  没钱不显示
            Decimal myCountMoney = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(pub_Int_Session_CurUserID, out myCountMoney);

            Decimal minCountMoney = pDecimalPromotePrice >= myCountMoney ? myCountMoney : pDecimalPromotePrice;//全部可用
            #region 优先使用购物券  购物券不用的 用现金补贴
            Decimal MaxMomey = pDecimalPromotePrice - minCountMoney_Vouchers_Number;
            if (minCountMoney > MaxMomey)
            {
                minCountMoney = MaxMomey;///用户肯定倾向于使用购物券。然后才是现金
            }
            #endregion
            if ((minCountMoney > (Decimal)0.0001))//  没钱不显示
            {
                Shopping_Vouchers_Beans_Money += "<div class=\"buyInfo_WeBuy_Quan\" style=\"margin-top:5px;padding-top:2px;\">\n";
                Shopping_Vouchers_Beans_Money += "   <div class=\"buyInfo_Left\">\n";
                Shopping_Vouchers_Beans_Money += "       <div class=\"spro_C3_Money\">\n";
                Shopping_Vouchers_Beans_Money += "           <span id=\"Quaninfo_Money\"><a href=\"/multibutton_showmoneydata.aspx\" target=\"_blank\">建议余额</a>：¥<span id=\"CanCheckMoney1\">" + Eggsoft_Public_CL.Pub.getPubMoney(minCountMoney) + "</span>&nbsp;&nbsp;<span style=\"display: none;\" id=\"NowCanUseMoneyShowText\"></span><span style=\"display: none;\" id=\"NowCanUseMoney1\"></span></span>\n";
                Shopping_Vouchers_Beans_Money += "       </div>\n";
                Shopping_Vouchers_Beans_Money += "   </div>\n";
                Shopping_Vouchers_Beans_Money += "   <label class=\"iosCheck\" id=\"iosCheck_Shoping_Money\">\n";
                Shopping_Vouchers_Beans_Money += "       <input id=\"Span1ClickButton_Shoping_Money\" onclick=\"show_hidden_Money();\" type=\"checkbox\" /><i></i></label>\n";
                Shopping_Vouchers_Beans_Money += "</div>\n";
            }
            #endregion
            //}
            ////}
            ///优惠券  和钱没有关系  有生成  就能输入
            ///
            #region  是否显示优惠券
            List<Eggsoft_Public_CL.GoodP_YouHuiQuan.ShowYouHuiQuanSearchBy> outShowYouHuiQuanSearchBy = null;
            int intStatus = Eggsoft_Public_CL.GoodP_YouHuiQuan.ShowYouHuiQuanSearchByGoodID(pIntGoodID, pub_Int_ShopClientID, pub_Int_Session_CurUserID, out outShowYouHuiQuanSearchBy);

            #endregion


            return Shopping_Vouchers_Beans_Money;
        }

        private string InitShopClient(string strargBody)
        {
            try
            {
                #region background
                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);
                if (Model_ShopClient == null)
                {
                    Eggsoft.Common.JsUtil.ShowMsgNew("店铺处于关闭状态！", "ClassWap_types.aspx");
                }
                strargBody = strargBody.Replace("###MobilePhone###", Model_ShopClient.ContactPhone);

                strargBody = strargBody.Replace("###ShopClientName###", Model_ShopClient.ShopClientName);

                strargBody = strargBody.Replace("###Shopping_Vouchers_Beans_Money###", getShopping_Vouchers_Beans_Money(Model_ShopClient));





                strargBody = strargBody.Replace("###ToShopCilentID###", pub_Int_ShopClientID.ToString());


                try
                {
                    string strQM_QQ_COM_QM_K_32 = Model_ShopClient.QM_QQ_COM_QM_K_32;
                    if (String.IsNullOrEmpty(strQM_QQ_COM_QM_K_32) == false)
                    {
                        string str____Button_ErWeiMa = "  <section class=\"lastPage_u-arrow2\">\n";
                        str____Button_ErWeiMa += "<a class=\"button orange\" target=\"_blank\" href=\"" + strQM_QQ_COM_QM_K_32 + "/\">在线沟通</a></section>";
                        str____Button_ErWeiMa += "";
                        strargBody = strargBody.Replace("###____Button_ErWeiMa###", str____Button_ErWeiMa);
                    }
                    else
                    {
                        strargBody = strargBody.Replace("###____Button_ErWeiMa###", "");
                    }
                }
                catch
                {
                    strargBody = strargBody.Replace("###____Button_ErWeiMa###", "");
                }


                try
                {
                    Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_ShopClient.XML, System.Text.Encoding.UTF8);
                    string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                    if (String.IsNullOrEmpty(strShopLogoImage) == false)
                    {
                        //strShopLogoImage = Eggsoft_Public_CL.GoodP.APPCODE_getImage_ForceGet(strShopLogoImage, 100, 50);
                        strargBody = strargBody.Replace("###ShopClientNameImgeOrText###", "<img id=\"Image1\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage + "\" style=\"height:50px;\" />");
                    }
                    else
                    {
                        strargBody = strargBody.Replace("###ShopClientNameImgeOrText###", Eggsoft.Common.CommUtil.getShortText(Model_ShopClient.ShopClientName, 10));
                    }
                }
                catch
                {
                    strargBody = strargBody.Replace("###ShopClientNameImgeOrText###", Model_ShopClient.ShopClientName);
                }
                finally { }
                try
                {
                    Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_ShopClient.XML, System.Text.Encoding.UTF8);
                    strPubShopClient_BackGroundColor = XML__Class_Shop_Client.Back_Color;
                    strPubShopClient_MenuBackGroundColor = XML__Class_Shop_Client.MenuBarColor;
                }
                catch { }
                finally { }
                EggsoftWX.BLL.tab_ShopClient_Model bll = new EggsoftWX.BLL.tab_ShopClient_Model();
                EggsoftWX.Model.tab_ShopClient_Model Model = bll.GetModel("UserID=" + pub_Int_ShopClientID + " and ModelName='" + "03_Logo" + "'");
                if (Model != null)
                {
                    strargBody = strargBody.Replace("###ShopClientIcon###", Model.ModelContent);
                }

                #endregion
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            return strargBody;

        }

    }
}