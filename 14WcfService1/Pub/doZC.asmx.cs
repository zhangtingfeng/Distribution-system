using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Pub
{
    /// <summary>
    /// doZC1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doZC1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        /// <summary>
        /// 加载项目信息及商品描述
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_ZC_Content()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strUseid = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int intUseid = 0;
                int.TryParse(strUseid, out intUseid);

                string strZCID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strZCID"]);
                int intZCID = 0;
                int.TryParse(strZCID, out intZCID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);



                if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUseid) != intShopClientID)
                {
                    str = "{\"ErrorCode\":-2}";
                }
                else
                {
                    String strSQL = "";
                    strSQL += "SELECT tab_Goods.Name AS GoodName ,tab_Goods.Icon AS GoodIcon ,  ";
                    strSQL += "tab_Goods.ShortInfo ,   ";
                    strSQL += "tab_Goods.LongInfo ,   ";
                    strSQL += "tab_Goods.Icon ,   ";
                    strSQL += "tab_ZC_01Product.ID ,   ";
                    strSQL += "tab_ZC_01Product.DestinationPrice ,   ";
                    strSQL += "tab_ZC_01Product.WhenEndAllGroup ,   ";
                    strSQL += "tab_ZC_01Product.ZCDescribe ,   ";
                    strSQL += "tab_ZC_01Product.ZCReason ,   ";
                    strSQL += "tab_ZC_01Product.ZCPromiseAndReturn ,   ";
                    strSQL += "tab_ZC_01Product.SourceGoodID ,   ";
                    strSQL += "tab_ZC_01Product.ShopClientID ,   ";
                    strSQL += "tab_ZC_01Product.IsSales ,   ";
                    strSQL += "tab_ZC_01Product.Sort ,   ";
                    strSQL += "tab_ZC_01Product.IsDeleted ,   ";
                    strSQL += "tab_ZC_01Product.CreateTime ,   ";
                    strSQL += "tab_ZC_01Product.UpdateTime  ";
                    strSQL += "FROM  ";
                    strSQL += "tab_ZC_01Product LEFT OUTER JOIN tab_Goods  ";
                    strSQL += " ON tab_ZC_01Product.ShopClientID  ";
                    strSQL += "  =   ";
                    strSQL += "  tab_Goods.ShopClient_ID  ";
                    strSQL += "AND tab_ZC_01Product.SourceGoodID  ";
                    strSQL += "=   ";
                    strSQL += " tab_Goods.ID  ";
                    strSQL += " WHERE tab_ZC_01Product.ShopClientID  ";
                    strSQL += "     =   ";
                    strSQL += "     " + strShopClientID + "  ";
                    strSQL += " AND tab_ZC_01Product.IsDeleted  ";
                    strSQL += "    =   ";
                    strSQL += "    0  ";
                    strSQL += " AND tab_Goods.ShopClient_ID  ";
                    strSQL += "   =   ";
                    strSQL += "   " + strShopClientID + "  ";
                    strSQL += " AND tab_ZC_01Product.IsSales  ";
                    strSQL += "    =   ";
                    strSQL += "   1  ";
                    strSQL += " and tab_ZC_01Product.ID=" + strZCID + "  ";


                    EggsoftWX.BLL.tab_ZC_01Product BLL_tab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                    System.Data.DataTable myDataTabletab_ZC_01Product = BLL_tab_ZC_01Product.SelectList(strSQL).Tables[0];
                    if (myDataTabletab_ZC_01Product.Rows.Count < 1)
                    {
                        str = "{\"ErrorCode\":-3}";
                    }
                    else
                    {
                        string strGoodname = myDataTabletab_ZC_01Product.Rows[0]["GoodName"].ToString();
                        string str2GoodIcon = myDataTabletab_ZC_01Product.Rows[0]["GoodIcon"].ToString();
                        string strSourceGoodID = myDataTabletab_ZC_01Product.Rows[0]["SourceGoodID"].ToString();
                        string strShortInfo = myDataTabletab_ZC_01Product.Rows[0]["ShortInfo"].ToString();
                        string strDBGoodNameLongInfo = myDataTabletab_ZC_01Product.Rows[0]["LongInfo"].ToString();
                        string strDBZCDescribe = myDataTabletab_ZC_01Product.Rows[0]["ZCDescribe"].ToString();
                        string strDBZCReason = myDataTabletab_ZC_01Product.Rows[0]["ZCReason"].ToString();
                        string strDBZCPromiseAndReturn = myDataTabletab_ZC_01Product.Rows[0]["ZCPromiseAndReturn"].ToString();
                        //string strWhenEndAllGroup = myDataTabletab_ZC_01Product.Rows[0]["WhenEndAllGroup"].ToString();


                        #region str_GoodNameLongInfo
                        string str_GoodNameLongInfo = HttpContext.Current.Server.HtmlDecode(strDBGoodNameLongInfo);
                        /*正则表达式替换求助
                   如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
                   例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                        str_GoodNameLongInfo = System.Text.RegularExpressions.Regex.Replace(str_GoodNameLongInfo, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                        string strSearch = "src=\"/upload/";
                        str_GoodNameLongInfo = str_GoodNameLongInfo.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                        strSearch = "src=\"/Upload/";
                        str_GoodNameLongInfo = str_GoodNameLongInfo.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                        #endregion

                        #region str_ZCDescribe
                        string str_ZCDescribe = HttpContext.Current.Server.HtmlDecode(strDBZCDescribe);
                        /*正则表达式替换求助
                   如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
                   例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                        str_ZCDescribe = System.Text.RegularExpressions.Regex.Replace(str_ZCDescribe, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                        strSearch = "src=\"/upload/";
                        str_ZCDescribe = str_ZCDescribe.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                        strSearch = "src=\"/Upload/";
                        str_ZCDescribe = str_ZCDescribe.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                        #endregion


                        #region str_ZCReason
                        string str_ZCReason = HttpContext.Current.Server.HtmlDecode(strDBZCReason);
                        /*正则表达式替换求助
                   如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
                   例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                        str_ZCReason = System.Text.RegularExpressions.Regex.Replace(str_ZCReason, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                        strSearch = "src=\"/upload/";
                        str_ZCReason = str_ZCReason.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                        strSearch = "src=\"/Upload/";
                        str_ZCReason = str_ZCReason.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                        #endregion

                        #region str_ZCPromiseAndReturn
                        string str_ZCPromiseAndReturn = HttpContext.Current.Server.HtmlDecode(strDBZCPromiseAndReturn);
                        /*正则表达式替换求助
                   如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
                   例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                        str_ZCPromiseAndReturn = System.Text.RegularExpressions.Regex.Replace(str_ZCPromiseAndReturn, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                        strSearch = "src=\"/upload/";
                        str_ZCPromiseAndReturn = str_ZCPromiseAndReturn.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                        strSearch = "src=\"/Upload/";
                        str_ZCPromiseAndReturn = str_ZCPromiseAndReturn.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                        #endregion


                        string strThisZhongChouGoodInfo = "{";
                        strThisZhongChouGoodInfo += "\"GoodName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strGoodname) + "\"";
                        strThisZhongChouGoodInfo += ",\"SourceGoodID\":\"" + strSourceGoodID + "\"";
                        strThisZhongChouGoodInfo += ",\"ZCDescribe\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(str_ZCDescribe) + "\"";
                        strThisZhongChouGoodInfo += ",\"ZCReason\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(str_ZCReason) + "\"";
                        strThisZhongChouGoodInfo += ",\"ZCPromiseAndReturn\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(str_ZCPromiseAndReturn) + "\"";
                        strThisZhongChouGoodInfo += ",\"ShortInfo\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strShortInfo) + "\"";
                        strThisZhongChouGoodInfo += ",\"LongInfo\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(str_GoodNameLongInfo) + "\"";
                        strThisZhongChouGoodInfo += ",\"GoodIcon\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Eggsoft_Public_CL.GoodP.doAnnouncePic_Good(str2GoodIcon)) + "\"";
                        strThisZhongChouGoodInfo += "}";
                        str = "{\"ErrorCode\":0,\"ThisZhongChouGoodInfo\":" + strThisZhongChouGoodInfo + "}";

                    }
                }




                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "微众筹");
            }
            finally
            {

            }
            return str;
        }



        /// <summary>
        /// 加载项目进度
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_ZC_SpeedBar()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strUseid = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int intUseid = 0;
                int.TryParse(strUseid, out intUseid);

                string strZCID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strZCID"]);
                int intZCID = 0;
                int.TryParse(strZCID, out intZCID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);



                if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUseid) != intShopClientID)
                {
                    str = "{\"ErrorCode\":-2}";
                }
                else
                {
                    EggsoftWX.BLL.tab_ZC_01Product BLL_tab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                    EggsoftWX.Model.tab_ZC_01Product Model_tab_ZC_01Product = BLL_tab_ZC_01Product.GetModel(intZCID);
                    Decimal DecimalDestinationPrice = Convert.ToDecimal(Model_tab_ZC_01Product.DestinationPrice);

                    EggsoftWX.BLL.tab_ZC_01Product_PartnerList BLL_tab_ZC_01Product_PartnerList = new EggsoftWX.BLL.tab_ZC_01Product_PartnerList();
                    string strWhereSalesAllMoney = "select sum(payPrice) as AllSalesMoney,count(*) as AllPeopleNum   from tab_ZC_01Product_PartnerList where Ispay=1 and IsDeleted=0 and ZC_01ProductID=" + intZCID + " and ShopClientID=" + intShopClientID;

                    System.Data.DataTable Data_DataTable = BLL_tab_ZC_01Product_PartnerList.SelectList(strWhereSalesAllMoney).Tables[0];


                    string strAllSalesMoney = Data_DataTable.Rows[0]["AllSalesMoney"].ToString();
                    string strAllPeopleNum = Data_DataTable.Rows[0]["AllPeopleNum"].ToString();

                    Decimal DecimalAllSalesMoney = 0;
                    Decimal.TryParse(strAllSalesMoney, out DecimalAllSalesMoney);

                    Decimal DecimalPercent = DecimalAllSalesMoney / DecimalDestinationPrice / (Decimal)0.01;
                    int intPercent = (int)(DecimalPercent + (Decimal)0.5);///四舍五入（全部加0.5，然后再取整（就是去除小数部分））

                    #region 检查众筹的终止时间
                    double doubleMaxLengthDay = 0;
                    double doubleMaxLengthHour = 0;
                    double doubleMaxLengthMinute = 0;
                    double doubleMaxLengthSeconds = 0;

                    TimeSpan span = (TimeSpan)(Model_tab_ZC_01Product.WhenEndAllGroup - DateTime.Now);
                    doubleMaxLengthDay = span.TotalDays;
                    doubleMaxLengthHour = span.TotalHours;
                    doubleMaxLengthMinute = span.TotalMinutes;
                    doubleMaxLengthSeconds = span.TotalSeconds;
                    #endregion



                    string strThisZhongChouGoodInfo = "{";
                    strThisZhongChouGoodInfo += "\"SpeendPercent\":" + intPercent.ToString() + "";
                    strThisZhongChouGoodInfo += ",\"AllSalesMoney\":\"" + DecimalAllSalesMoney.ToString() + "\"";
                    strThisZhongChouGoodInfo += ",\"AllPeopleNum\":\"" + strAllPeopleNum.ToString() + "\"";
                    strThisZhongChouGoodInfo += ",\"doubleMaxLengthDay\":" + (int)doubleMaxLengthDay + "";
                    strThisZhongChouGoodInfo += ",\"doubleMaxLengthHour\":" + (int)(doubleMaxLengthHour) + "";
                    strThisZhongChouGoodInfo += ",\"doubleMaxLengthMinute\":" + (int)(doubleMaxLengthMinute) + "";
                    strThisZhongChouGoodInfo += ",\"doubleMaxLengthSeconds\":" + (int)(doubleMaxLengthSeconds) + "";
                    strThisZhongChouGoodInfo += "}";
                    str = "{\"ErrorCode\":0,\"ThisZhongChouGoodInfo\":" + strThisZhongChouGoodInfo + "}";


                }
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "微众筹");
            }
            finally
            {

            }
            return str;
        }


        /// <summary>
        /// 加载档位
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_ZC_IWanttoSupportList()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strUseid = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int intUseid = 0;
                int.TryParse(strUseid, out intUseid);

                string strZCID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strZCID"]);
                int intZCID = 0;
                int.TryParse(strZCID, out intZCID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);




                if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUseid) != intShopClientID)
                {
                    str = "{\"ErrorCode\":-2}";
                }
                else
                {
                    EggsoftWX.BLL.tab_ZC_01Product_Support_GetBonus BLL_tab_ZC_01Product_Support_GetBonus = new EggsoftWX.BLL.tab_ZC_01Product_Support_GetBonus();


                    EggsoftWX.BLL.tab_ZC_01Product_PartnerList BLL_tab_ZC_01Product_PartnerList = new EggsoftWX.BLL.tab_ZC_01Product_PartnerList();


                    EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    string strWhereIcon = "";
                    strWhereIcon += "  SELECT   tab_Goods.ShortInfo, tab_Goods.Icon, tab_Goods.Name, tab_ZC_01Product.*";
                    strWhereIcon += "  FROM      tab_ZC_01Product LEFT OUTER JOIN";
                    strWhereIcon += "   tab_Goods ON tab_ZC_01Product.SourceGoodID = tab_Goods.ID where tab_ZC_01Product.id=" + intZCID + " and tab_Goods.ShopClient_ID=" + intShopClientID;
                    strWhereIcon += " order by tab_ZC_01Product.sort asc,tab_ZC_01Product.ID asc";
                    string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(BLL_tab_ZC_01Product_Support.SelectList(strWhereIcon).Tables[0].Rows[0]["Icon"].ToString(), 100);
                    string strFirstImageFullName = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + GoodIcon;


                    string strWheretab_ZC_01Product_SupportList = "";


                    System.Data.DataTable DataTable_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetList("*", "IsDeleted=0 and IsSales=1 and ShopClientID=" + intShopClientID + " and ZC_01ProductID=" + intZCID + " order by sort asc,id asc").Tables[0];
                    for (int i = 0; i < DataTable_ZC_01Product_Support.Rows.Count; i++)
                    {
                        string strSupportIDID = DataTable_ZC_01Product_Support.Rows[i]["ID"].ToString();
                        string strSalesPrice = DataTable_ZC_01Product_Support.Rows[i]["SalesPrice"].ToString();
                        string strSupportName = DataTable_ZC_01Product_Support.Rows[i]["Name"].ToString();
                        string strSalesLimit = DataTable_ZC_01Product_Support.Rows[i]["SalesLimit"].ToString();///档位名额限制 50 20 等等  0表示没有限制
                        string strSalesPricePromiseAndReturn = DataTable_ZC_01Product_Support.Rows[i]["SalesPricePromiseAndReturn"].ToString();
                        string strSupportWay = DataTable_ZC_01Product_Support.Rows[i]["SupportWay"].ToString();
                        string strSupportHowMany = DataTable_ZC_01Product_Support.Rows[i]["SupportHowMany"].ToString();
                        string strMustSubscribe = DataTable_ZC_01Product_Support.Rows[i]["MustSubscribe"].ToString();
                        string strOnlyBuyOneOnlyOneAccount = DataTable_ZC_01Product_Support.Rows[i]["OnlyBuyOneOnlyOneAccount"].ToString();

                        string strSelect = " select sum(PayPrice) as AllPayPrice,count(*) as AllPeople from tab_ZC_01Product_PartnerList where ZC_01ProductID=" + strZCID + " and SupportID=" + strSupportIDID + " and Ispay=1 and IsDeleted=0";
                        System.Data.DataTable DataTablePartnerList = BLL_tab_ZC_01Product_PartnerList.SelectList(strSelect).Tables[0];
                        string strAllPayPrice = DataTablePartnerList.Rows[0]["AllPayPrice"].ToString();
                        string strAllPeople = DataTablePartnerList.Rows[0]["AllPeople"].ToString();

                        #region 开奖期数显示
                        string strBonusShow = "";
                        System.Data.DataTable DataTableGetBonus = BLL_tab_ZC_01Product_Support_GetBonus.GetList("top 2 ID,SupportID,BonusContent,CreateTime", "SupportID=" + strSupportIDID + " and IsDeleted<>1 and ShopClientID=" + strShopClientID + " order by Sort desc,ID desc").Tables[0];
                        if (DataTableGetBonus.Rows.Count > 0)
                        {
                            for (int k = 0; k < DataTableGetBonus.Rows.Count; k++)
                            {
                                string strCreateTime = DataTableGetBonus.Rows[k]["CreateTime"].ToString();
                                string strBonusID = DataTableGetBonus.Rows[k]["ID"].ToString();
                                string strSupportID = DataTableGetBonus.Rows[k]["SupportID"].ToString();
                                string strBonusContent = DataTableGetBonus.Rows[k]["BonusContent"].ToString();

                                string strCurShow = "<a href=\"#\" title=\"" + strBonusContent + "\" class=\"ShowTitleInfo\">" + DateTime.Parse(strCreateTime).ToString("yyyy") + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strBonusID), 3) + "</a>&nbsp;";
                                if (String.IsNullOrEmpty(strBonusShow)) strBonusShow = "最近开奖";
                                strBonusShow += strCurShow;
                            }
                        }
                        #endregion

                        #region 档位说明
                        Decimal DecimalAllPayPrice = 0;
                        Decimal.TryParse(strAllPayPrice, out DecimalAllPayPrice);
                        string strDescMemo = "本档位<span style=\"font-weight:bold;\">" + strSupportName + "</span>,";

                        //int intGoodsCount=Int32.Parse(strGoodsCount);
                        //if (intGoodsCount == 0)
                        //{
                        //    strDescMemo += "属于无偿捐赠.";
                        //}

                        if (strSalesLimit == "0")
                        {
                            //strDescMemo += "无名额限制.";
                        }
                        else
                        {
                            strDescMemo += "限制名额" + strSalesLimit + "人.";
                        }
                        strDescMemo += "已支持人次" + strAllPeople + "次.";


                        if (strSupportWay == "1")
                        {
                            strDescMemo += "每满" + strSupportHowMany + "人,参考双色球数,在（每逢周二、四、日）22点开奖.不满则项目结束也开奖.";
                        }
                        else if (strSupportWay == "2")
                        {
                            strDescMemo += "每满" + strSupportHowMany + "人,参考福彩3D数据,在每日22点开奖.不满则项目结束也开奖.";
                        }
                        else if (strSupportWay == "0")
                        {
                            strDescMemo += "支付即发货.";
                        }
                        else if (strSupportWay == "3")
                        {
                            strDescMemo += "无偿赞助.";
                        }
                        else if (strSupportWay == "4")
                        {
                            strDescMemo += "后期回报.";
                        }
                        #endregion

                        string strThisSupportGoodInfo = "{";
                        strThisSupportGoodInfo += "\"SupportID\":" + strSupportIDID + "";
                        strThisSupportGoodInfo += ",\"SalesPrice\":" + strSalesPrice + "";
                        //strThisSupportGoodInfo += ",\"GoodsCount\":" + strGoodsCount + "";
                        strThisSupportGoodInfo += ",\"SalesLimitHowMany\":" + strSalesLimit + "";
                        strThisSupportGoodInfo += ",\"SalesPricePromiseAndReturn\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strSalesPricePromiseAndReturn) + "\"";
                        strThisSupportGoodInfo += ",\"SupportWay\":" + strSupportWay + "";
                        strThisSupportGoodInfo += ",\"DescMemo\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strBonusShow + strDescMemo) + "\"";
                        strThisSupportGoodInfo += ",\"strSupportHowMany\":" + strSupportHowMany + "";
                        strThisSupportGoodInfo += ",\"strMustSubscribe\":\"" + strMustSubscribe + "\"";
                        strThisSupportGoodInfo += ",\"OnlyBuyOneOnlyOneAccount\":\"" + strOnlyBuyOneOnlyOneAccount + "\"";
                        strThisSupportGoodInfo += "}";
                        if (String.IsNullOrEmpty(strWheretab_ZC_01Product_SupportList) == false) strWheretab_ZC_01Product_SupportList += ",";
                        strWheretab_ZC_01Product_SupportList += strThisSupportGoodInfo;
                    }


                    //string strThisZhongSupportListInfo = "{";
                    ////strThisZhongChouGoodInfo += "\"SpeendPercent\":" + intPercent.ToString() + "";
                    ////strThisZhongChouGoodInfo += ",\"AllSalesMoney\":\"" + DecimalAllSalesMoney.ToString() + "\"";
                    ////strThisZhongChouGoodInfo += ",\"AllPeopleNum\":\"" + strAllPeopleNum.ToString() + "\"";
                    ////strThisZhongChouGoodInfo += ",\"doubleMaxLengthDay\":" + (int)doubleMaxLengthDay + "";
                    ////strThisZhongChouGoodInfo += ",\"doubleMaxLengthHour\":" + (int)(doubleMaxLengthHour) + "";
                    ////strThisZhongChouGoodInfo += ",\"doubleMaxLengthMinute\":" + (int)(doubleMaxLengthMinute) + "";
                    ////strThisZhongChouGoodInfo += ",\"doubleMaxLengthSeconds\":" + (int)(doubleMaxLengthSeconds) + "";
                    //strThisZhongSupportListInfo += "}";
                    str = "{\"ErrorCode\":0,\"GoodICON\":\"" + strFirstImageFullName + "\",\"ThisZhongSupportListInfo\":[" + strWheretab_ZC_01Product_SupportList + "]}";


                }
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "微众筹");
            }
            finally
            {

            }
            return str;
        }
    }
}
