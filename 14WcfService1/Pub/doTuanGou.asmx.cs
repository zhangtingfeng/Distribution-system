using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Pub
{
    /// <summary>
    /// doTuanGou 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doTuanGou : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 加载团购列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_TuanGoulist()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strUseid = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int intUseid = 0;
                int.TryParse(strUseid, out intUseid);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);

                if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUseid) != intShopClientID)
                {
                    str = "{\"ErrorCode\":-1}";
                }
                else
                {
                    web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                    sql.addOrderField("tab_TuanGou.sort", "asc");//第一排序字段  
                    sql.addOrderField("tab_TuanGou.id", "asc");//第二排序字段  

                    string strTablename = "tab_TuanGou LEFT OUTER JOIN  tab_Goods ON tab_TuanGou.ShopClientID = tab_Goods.ShopClient_ID AND tab_TuanGou.SourceGoodID = tab_Goods.ID";
                    sql.table = strTablename;

                    string stroutfields = "tab_Goods.Name as GoodName,tab_Goods.PromotePrice as PromotePrice, tab_Goods.ShortInfo, tab_Goods.Icon, tab_TuanGou.ID, ";
                    stroutfields += "    tab_TuanGou.HowManyPeople, tab_TuanGou.EachPeoplePrice, ";
                    stroutfields += "    tab_TuanGou.AgentPrice, tab_TuanGou.ShopClientID, tab_TuanGou.SourceGoodID, ";
                    stroutfields += "   tab_TuanGou.IsDeleted, tab_TuanGou.CreateTime, tab_TuanGou.UpdateTime, tab_TuanGou.IsSales, ";
                    stroutfields += "   tab_TuanGou.Sort, tab_TuanGou.MustSubscribe_Master, tab_TuanGou.MustSubscribe_Helper, ";
                    stroutfields += "   tab_TuanGou.MustAddress_Master, tab_TuanGou.MustAgent_Master";
                    sql.outfields = stroutfields;
                    sql.nowPageIndex = 1;
                    sql.pagesize = 1000;

                    string strTitlename = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(intUseid, intShopClientID);


                    string strTuanGouJointWhere = "";
                    strTuanGouJointWhere += "  (tab_TuanGou.ShopClientID = " + strShopClientID + " and tab_TuanGou.IsDeleted=0)  and (tab_Goods.ShopClient_ID = " + strShopClientID + ")";
                    string strWhere = " and (tab_TuanGou.IsSales=1)";
                    string strwhere = strTuanGouJointWhere + strWhere;
                    sql.where = strwhere;

                    EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();

                    string strSQLRecordCount = String.Format("SELECT {0} FROM " + strTablename + " WHERE" + strTuanGouJointWhere, "count(*) as RecordCount") + strWhere;
                    string strRecordCount = BLL_tab_TuanGou.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

                    string strSql = sql.getSQL(Int32.Parse(strRecordCount));

                    str = "{\"ErrorCode\":0,\"Title\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strTitlename) + "\"";
                    if (Int32.Parse(strRecordCount) == 0)
                    {
                        str += ",\"RecordCount\":0";
                        str += ",\"ThisTuanGouGoodInfoList\":[]}";
                    }
                    else
                    {
                        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
                        string strShortMemo = Model_tab_ShopClient.ShopMemo;

                        System.Data.DataTable myDataTableTuanGou = BLL_tab_TuanGou.SelectList(strSql).Tables[0];
                        str += ",\"RecordCount\":" + myDataTableTuanGou.Rows.Count + "";

                        string strThisTuanGouGoodInfoAdd = "";
                        for (int i = 0; i < myDataTableTuanGou.Rows.Count; i++)
                        {
                            string strThisTuanGouGoodInfo = "{";

                            string strTuanGouID = myDataTableTuanGou.Rows[i]["ID"].ToString();
                            string strGoodname = myDataTableTuanGou.Rows[i]["GoodName"].ToString();
                            string strShortInfo = myDataTableTuanGou.Rows[i]["ShortInfo"].ToString();
                            string strIcon = myDataTableTuanGou.Rows[i]["Icon"].ToString();
                            string strHowManyPeople = myDataTableTuanGou.Rows[i]["HowManyPeople"].ToString();
                            string strEachPeoplePrice = myDataTableTuanGou.Rows[i]["EachPeoplePrice"].ToString();
                            string strPromotePrice = myDataTableTuanGou.Rows[i]["PromotePrice"].ToString();
                            string str2GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(strIcon, 200);


                            strThisTuanGouGoodInfo += "\"GoodName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strGoodname) + "\"";
                            strThisTuanGouGoodInfo += ",\"TuanGouID\":\"" + strTuanGouID + "\"";
                            strThisTuanGouGoodInfo += ",\"CompanyShortDesc\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strShortMemo) + "\"";
                            strThisTuanGouGoodInfo += ",\"ShortInfo\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strShortInfo) + "\"";
                            strThisTuanGouGoodInfo += ",\"GoodIcon\":\"" + str2GoodIcon + "\"";
                            strThisTuanGouGoodInfo += ",\"HowManyPeople\":\"" + strHowManyPeople + "\"";
                            strThisTuanGouGoodInfo += ",\"EachPeoplePrice\":\"" + strEachPeoplePrice + "\"";
                            strThisTuanGouGoodInfo += ",\"PromotePrice\":\"" + strPromotePrice + "\"";
                            strThisTuanGouGoodInfo += "}";
                            if (String.IsNullOrEmpty(strThisTuanGouGoodInfoAdd) == false) strThisTuanGouGoodInfoAdd += ",";
                            strThisTuanGouGoodInfoAdd += strThisTuanGouGoodInfo;
                        }
                        str += ",\"ThisTuanGouGoodInfoList\":[" + strThisTuanGouGoodInfoAdd + "]}";
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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "微砍价");
            }
            finally
            {

            }
            return str;
        }



        /// <summary>
        /// 加载团购描述
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_TuanGou()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strUseid = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int intUseid = 0;
                int.TryParse(strUseid, out intUseid);

                string strTuanGouID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strTuanGouID"]);
                int intTuanGouID = 0;
                int.TryParse(strTuanGouID, out intTuanGouID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);

                string strtuangouidnumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strtuangouidnumber"]);
                int intTuanGouidnumber = 0;
                int.TryParse(strtuangouidnumber, out intTuanGouidnumber);////有可能是参与别人的团购



                if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUseid) != intShopClientID)
                {
                    str = "{\"ErrorCode\":-2}";
                }
                else
                {
                    #region 联合查询 用一下
                    web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                    sql.addOrderField("tab_TuanGou.sort", "asc");//第一排序字段  
                    sql.addOrderField("tab_TuanGou.id", "asc");//第二排序字段  

                    string strTablename = "tab_TuanGou LEFT OUTER JOIN  tab_Goods ON tab_TuanGou.ShopClientID = tab_Goods.ShopClient_ID AND tab_TuanGou.SourceGoodID = tab_Goods.ID";
                    sql.table = strTablename;

                    string stroutfields = "tab_Goods.Name as GoodName,tab_Goods.PromotePrice as PromotePrice, tab_Goods.ShortInfo, tab_Goods.LongInfo, tab_Goods.Icon, tab_TuanGou.ID, ";
                    stroutfields += "    tab_TuanGou.TuanFouRule, tab_TuanGou.HowManyPeople, tab_TuanGou.EachPeoplePrice, ";
                    stroutfields += "    tab_TuanGou.WhenEndAllGroup, tab_TuanGou.MaxTimeLengthDoGroup, tab_TuanGou.ChoiceWhenEndAllGroup,tab_TuanGou.ChoiceMaxTimeLengthDoGroup, ";
                    stroutfields += "    tab_TuanGou.AgentPrice, tab_TuanGou.ShopClientID, tab_TuanGou.SourceGoodID, ";
                    stroutfields += "   tab_TuanGou.IsDeleted, tab_TuanGou.CreateTime, tab_TuanGou.UpdateTime, tab_TuanGou.IsSales, ";
                    stroutfields += "   tab_TuanGou.Sort, tab_TuanGou.MustSubscribe_Master, tab_TuanGou.MustSubscribe_Helper, ";
                    stroutfields += "   tab_TuanGou.MustAddress_Master, tab_TuanGou.MustAgent_Master";
                    sql.outfields = stroutfields;
                    sql.nowPageIndex = 1;
                    sql.pagesize = 1;
                    string strTuanGouJointWhere = "";
                    strTuanGouJointWhere += "  (tab_TuanGou.ID=" + strTuanGouID + " and tab_TuanGou.ShopClientID = " + strShopClientID + " and tab_TuanGou.IsDeleted=0)  and (tab_Goods.ShopClient_ID = " + strShopClientID + ")";
                    string strWhere = " and (tab_TuanGou.IsSales=1)";
                    string strwhere = strTuanGouJointWhere + strWhere;
                    sql.where = strwhere;
                    string strSQLRecordCount = String.Format("SELECT {0} FROM " + strTablename + " WHERE" + strTuanGouJointWhere, "count(*) as RecordCount") + strWhere;
                    string strRecordCount = "1";
                    string strSql = sql.getSQL(Int32.Parse(strRecordCount));
                    #endregion
                    EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                    System.Data.DataTable myDataTableTuanGou = BLL_tab_TuanGou.SelectList(strSql).Tables[0];
                    if (myDataTableTuanGou.Rows.Count == 0) Eggsoft.Common.debug_Log.Call_WriteLog(strSql, "微团购", "程序出错");
                    if (myDataTableTuanGou.Rows.Count > 0)
                    {
                        string strGoodname = myDataTableTuanGou.Rows[0]["GoodName"].ToString();
                        string strSourceGoodID = myDataTableTuanGou.Rows[0]["SourceGoodID"].ToString();
                        string strShortInfo = myDataTableTuanGou.Rows[0]["ShortInfo"].ToString();
                        string strDBLongInfo = myDataTableTuanGou.Rows[0]["LongInfo"].ToString();
                        string strDBTuanFouRule = myDataTableTuanGou.Rows[0]["TuanFouRule"].ToString();
                        string strMustSubscribe_Master = myDataTableTuanGou.Rows[0]["MustSubscribe_Master"].ToString();
                        string strMustSubscribe_Helper = myDataTableTuanGou.Rows[0]["MustSubscribe_Helper"].ToString();
                        string strMustAddress_Master = myDataTableTuanGou.Rows[0]["MustAddress_Master"].ToString();
                        string strMustAgent_Master = myDataTableTuanGou.Rows[0]["MustAgent_Master"].ToString();
                        string strWhenEndAllGroup = myDataTableTuanGou.Rows[0]["WhenEndAllGroup"].ToString();
                        string strMaxTimeLengthDoGroup = myDataTableTuanGou.Rows[0]["MaxTimeLengthDoGroup"].ToString();
                        string strChoiceWhenEndAllGroup = myDataTableTuanGou.Rows[0]["ChoiceWhenEndAllGroup"].ToString();
                        string strChoiceMaxTimeLengthDoGroup = myDataTableTuanGou.Rows[0]["ChoiceMaxTimeLengthDoGroup"].ToString();

                        string strIcon = myDataTableTuanGou.Rows[0]["Icon"].ToString();
                        string strHowManyPeople = myDataTableTuanGou.Rows[0]["HowManyPeople"].ToString();
                        string strEachPeoplePrice = myDataTableTuanGou.Rows[0]["EachPeoplePrice"].ToString();
                        string strPromotePrice = myDataTableTuanGou.Rows[0]["PromotePrice"].ToString();
                        string str2GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(strIcon, 100);

                        #region 检查团的终止时间
                        double doubleMaxLengthTime = 0;
                        if (strChoiceWhenEndAllGroup == "True")
                        {
                            DateTime DateTimeModel = DateTime.Now;
                            DateTime.TryParse(strWhenEndAllGroup, out DateTimeModel);
                            TimeSpan span = (TimeSpan)(DateTimeModel - DateTime.Now);
                            doubleMaxLengthTime = span.TotalSeconds;
                        }
                        #endregion

                        #region str_TuanFouRule
                        string str_TuanFouRule = HttpContext.Current.Server.HtmlDecode(strDBTuanFouRule);
                        /*正则表达式替换求助
                   如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
                   例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                        str_TuanFouRule = System.Text.RegularExpressions.Regex.Replace(str_TuanFouRule, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                        string strSearch = "src=\"/upload/";
                        str_TuanFouRule = str_TuanFouRule.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                        strSearch = "src=\"/Upload/";
                        str_TuanFouRule = str_TuanFouRule.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                        #endregion

                        #region str_LongInfo
                        string str_LongInfo = HttpContext.Current.Server.HtmlDecode(strDBLongInfo);
                        /*正则表达式替换求助
                   如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
                   例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                        str_LongInfo = System.Text.RegularExpressions.Regex.Replace(str_LongInfo, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                        strSearch = "src=\"/upload/";
                        str_LongInfo = str_LongInfo.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                        strSearch = "src=\"/Upload/";
                        str_LongInfo = str_LongInfo.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                        #endregion

                        #region 检查组团情况   如果 没有 购买过。出空白。。如果 自己 是团长 。显示 团员情况

                        #region ///检查是否团长
                        String strParterRole = "-1";///预先假设什么都不是 什么都没有买过
                        String strTuanGouIDNumber = "0";

                        string strIfTuanZhang = "SELECT   tab_TuanGou_Number.IFFinshedCurMemberShip, tab_TuanGou_Partner.ParterRole AS TuanGou_PartnerParterRole, ";
                        strIfTuanZhang += " tab_TuanGou_Partner.TuanGouIDNumber as TuanGou_Partner_TuanGouIDNumber";
                        strIfTuanZhang += " FROM      tab_TuanGou_Partner LEFT OUTER JOIN";
                        strIfTuanZhang += " tab_TuanGou_Number ON tab_TuanGou_Partner.TuanGouID = tab_TuanGou_Number.TuanGouID AND ";
                        strIfTuanZhang += " tab_TuanGou_Partner.TuanGouIDNumber = tab_TuanGou_Number.ID AND ";
                        strIfTuanZhang += " tab_TuanGou_Partner.ShopClientID = tab_TuanGou_Number.ShopClientID";
                        strIfTuanZhang += " WHERE   (tab_TuanGou_Partner.UserID = " + strUseid + " and tab_TuanGou_Partner.ShopClientID=" + strShopClientID + " and tab_TuanGou_Partner.TuanGouID=" + strTuanGouID + "";
                        strIfTuanZhang += " and tab_TuanGou_Number.IFFinshedCurMemberShip=0) order by tab_TuanGou_Partner.CreateTime desc";

                        System.Data.DataTable myDataTableTuanGouTuanZhang = BLL_tab_TuanGou.SelectList(strIfTuanZhang).Tables[0];
                        if (myDataTableTuanGouTuanZhang.Rows.Count > 0)
                        {
                            strParterRole = myDataTableTuanGouTuanZhang.Rows[0]["TuanGou_PartnerParterRole"].ToString();
                            strTuanGouIDNumber = myDataTableTuanGouTuanZhang.Rows[0]["TuanGou_Partner_TuanGouIDNumber"].ToString();
                        }
                        #endregion
                        EggsoftWX.BLL.tab_TuanGou_Partner BLL_tab_TuanGou_Partner = new EggsoftWX.BLL.tab_TuanGou_Partner();

                        String strParterCount = "0";
                        ///如果 是 团长 。并且 已经 完成 组团。出空白

                        if (intTuanGouidnumber > 0)
                        {////打开别人链接的团
                            strTuanGouIDNumber = intTuanGouidnumber.ToString();
                        }
                        else
                        {
                            ///本人是 团员。。显示团员及 各团员 购买情况。
                            string strWheretab_TuanGou_Partner = "select tab_TuanGou_Partner.TuanGouIDNumber ";
                            strWheretab_TuanGou_Partner += "from  tab_TuanGou_Partner  LEFT OUTER JOIN tab_TuanGou_Number on  (tab_TuanGou_Number.ID=tab_TuanGou_Partner.TuanGouIDNumber)";
                            strWheretab_TuanGou_Partner += " where ";
                            strWheretab_TuanGou_Partner += " (tab_TuanGou_Partner.UserID=" + strUseid + " and tab_TuanGou_Partner.ShopClientID=" + strShopClientID + " and tab_TuanGou_Number.IFFinshedCurMemberShip=0 and tab_TuanGou_Number.TuanGouID=" + strTuanGouID + " and tab_TuanGou_Partner.TuanGouID=" + strTuanGouID + ")";
                            strWheretab_TuanGou_Partner += " order by tab_TuanGou_Partner.CreateTime desc";
                            System.Data.DataTable TuanGou_PartnerDataTable = BLL_tab_TuanGou_Partner.SelectList(strWheretab_TuanGou_Partner).Tables[0];

                            if (TuanGou_PartnerDataTable.Rows.Count > 0)///可能购买过
                            {
                                strtuangouidnumber = TuanGou_PartnerDataTable.Rows[0]["TuanGouIDNumber"].ToString();
                                int.TryParse(strtuangouidnumber, out intTuanGouidnumber);////自己作为团圆 可能购买过 也可能 没有购买过  有可能是参与别人的团购

                            }
                        }

                        string strParterRoleList = "[]";
                        if (intTuanGouidnumber > 0)
                        {
                            string strEveryTuanYuan = "";
                            strEveryTuanYuan += "SELECT     tab_TuanGou_Partner.UserID, tab_TuanGou_Partner.ShopClientID, tab_TuanGou_Partner.BuyPrice, tab_TuanGou_Partner.TuanGouID, tab_User.NickName,tab_TuanGou_Partner.ParterRole, ";
                            strEveryTuanYuan += "  tab_User.HeadImageUrl, tab_TuanGou_Partner.CreateTime as PayTime";
                            strEveryTuanYuan += " FROM         tab_TuanGou_Partner LEFT OUTER JOIN ";
                            strEveryTuanYuan += "  tab_User ON tab_TuanGou_Partner.ShopClientID = tab_User.ShopClientID AND tab_TuanGou_Partner.UserID = tab_User.ID ";
                            strEveryTuanYuan += "    WHERE     (tab_TuanGou_Partner.TuanGouIDNumber = " + intTuanGouidnumber + ") and (tab_TuanGou_Partner.TuanGouID = " + strTuanGouID + ")";
                            strEveryTuanYuan += "  ORDER BY tab_TuanGou_Partner.CreateTime asc";
                            System.Data.DataTable Data_DataTable = BLL_tab_TuanGou_Partner.SelectList(strEveryTuanYuan).Tables[0];


                            strParterCount = Data_DataTable.Rows.Count.ToString();

                            #region 团长的支付时间  页面倒计时使用   string strTuanZhangPayTime = "";////团长的支付时间
                            if (Data_DataTable.Rows.Count > 0)
                            {
                                string strTuanZhangPayTime = Data_DataTable.Rows[0]["PayTime"].ToString();///参与人角色  1 表示发起人  2  表示 参与人

                                if (strChoiceMaxTimeLengthDoGroup == "True")
                                {
                                    DateTime TuanZhangPayTime = DateTime.Now;
                                    DateTime.TryParse(strTuanZhangPayTime, out TuanZhangPayTime);

                                    TimeSpan span = (TimeSpan)(TuanZhangPayTime.AddHours(Int32.Parse(strMaxTimeLengthDoGroup)) - DateTime.Now);
                                    if ((doubleMaxLengthTime > span.TotalSeconds) && (strChoiceWhenEndAllGroup == "True"))
                                    {
                                        doubleMaxLengthTime = span.TotalSeconds;
                                    }
                                    else
                                    {
                                        if ((span.TotalSeconds > 0) && (doubleMaxLengthTime < 1))
                                        {
                                            doubleMaxLengthTime = span.TotalSeconds;
                                        }
                                    }


                                }
                            }
                            #endregion

                            strParterRoleList = "[";
                            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                            {
                                string strEachHeadImageUrl = Data_DataTable.Rows[i]["HeadImageUrl"].ToString();///参与人角色  1 表示发起人  2  表示 参与人
                                string strEachNickName = Data_DataTable.Rows[i]["NickName"].ToString();///参与人角色  1 表示发起人  2  表示 参与人
                                string strEachParterRole = Data_DataTable.Rows[i]["ParterRole"].ToString();///参与人角色  1 表示发起人  2  表示 参与人
                                string strEachPayTime = Data_DataTable.Rows[i]["PayTime"].ToString();///参与人角色  1 表示发起人  2  表示 参与人
                                if (strEachPayTime == "") strEachPayTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");////程序 报错时  这里 会出现空的 ，这样写 方便调试  临时措施
                                                                                                                            //strEachPayTime = DateTime.Parse(strEachPayTime).ToString("yyyy-MM-dd HH:mm");
                                DateTime dt2 = DateTime.Parse(strEachPayTime);                                                    ///


                                String strCurParter = "{\"EachHeadImageUrl\":\"" + strEachHeadImageUrl + "\",";
                                strCurParter += "\"EachNickName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strEachNickName) + "\",";
                                strCurParter += "\"EachParterRole\":\"" + strEachParterRole + "\",";
                                strCurParter += "\"EachParterRoleDesc\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strEachParterRole == "1" ? "团长" : "团员") + "\",";
                                strCurParter += "\"EachPayTime\":\"" + dt2.ToString("yyyy-MM-dd") + "\"";
                                strCurParter += "}";
                                strParterRoleList += strCurParter;
                                if (i != Data_DataTable.Rows.Count - 1) strParterRoleList += ",";
                            }
                            strParterRoleList += "]";
                        }
                        #endregion




                        string strThisTuanGouGoodInfo = "{";
                        strThisTuanGouGoodInfo += "\"GoodName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strGoodname) + "\"";
                        strThisTuanGouGoodInfo += ",\"SourceGoodID\":\"" + strSourceGoodID + "\"";
                        strThisTuanGouGoodInfo += ",\"TuanFouRule\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(str_TuanFouRule) + "\"";
                        strThisTuanGouGoodInfo += ",\"MustSubscribe_Master\":\"" + strMustSubscribe_Master + "\"";
                        strThisTuanGouGoodInfo += ",\"MustSubscribe_Helper\":\"" + strMustSubscribe_Helper + "\"";
                        strThisTuanGouGoodInfo += ",\"MustAddress_Master\":\"" + strMustAddress_Master + "\"";
                        strThisTuanGouGoodInfo += ",\"MustAgent_Master\":\"" + strMustAgent_Master + "\"";
                        strThisTuanGouGoodInfo += ",\"ShortInfo\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strShortInfo) + "\"";
                        strThisTuanGouGoodInfo += ",\"LongInfo\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(str_LongInfo) + "\"";
                        strThisTuanGouGoodInfo += ",\"GoodIcon\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Eggsoft_Public_CL.GoodP.doAnnouncePic_Good(str2GoodIcon)) + "\"";
                        strThisTuanGouGoodInfo += ",\"HowManyPeople\":\"" + strHowManyPeople + "\"";
                        strThisTuanGouGoodInfo += ",\"ParterRole\":" + strParterRole + "";
                        strThisTuanGouGoodInfo += ",\"TuanGouIDNumber\":" + strTuanGouIDNumber + "";
                        strThisTuanGouGoodInfo += ",\"ParterCount\":" + strParterCount + "";
                        strThisTuanGouGoodInfo += ",\"ParterRoleList\":" + (strParterRoleList) + "";
                        strThisTuanGouGoodInfo += ",\"EachPeoplePrice\":\"" + strEachPeoplePrice + "\"";
                        strThisTuanGouGoodInfo += ",\"PromotePrice\":\"" + strPromotePrice + "\"";
                        strThisTuanGouGoodInfo += ",\"doubleMaxLengthTime\":" + doubleMaxLengthTime + "";
                        strThisTuanGouGoodInfo += "}";
                        str = "{\"ErrorCode\":0,\"ThisTuanGouGoodInfo\":" + strThisTuanGouGoodInfo + "}";
                    }
                    else
                    {
                        str = "{\"ErrorCode\":-3}";////产品已下线
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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "微团购");
            }
            finally
            {

            }
            return str;
        }





        /// <summary>
        /// 修改该团购是否完成
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doModifyStatus_TuanGou_Number()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;


                string strTuanGouID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strTuanGouID"]);
                int intTuanGouID = 0;
                int.TryParse(strTuanGouID, out intTuanGouID);


                string strtuangouidnumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strtuangouidnumber"]);
                int intTuanGouidnumber = 0;
                int.TryParse(strtuangouidnumber, out intTuanGouidnumber);////有可能是参与别人的团购

                string strModifyStatus = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strModifyStatus"]);
                int intModifyStatus = 0;
                int.TryParse(strModifyStatus, out intModifyStatus);

                EggsoftWX.BLL.tab_TuanGou_Number BLL_tab_TuanGou_Number = new EggsoftWX.BLL.tab_TuanGou_Number();
                BLL_tab_TuanGou_Number.Update("IFFinshedCurMemberShip=" + intModifyStatus + ",UpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'", "ID=" + strtuangouidnumber);



                str = "{\"ErrorCode\":0}";


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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "微团购刷新");
            }
            finally
            {

            }
            return str;
        }

    }
}
