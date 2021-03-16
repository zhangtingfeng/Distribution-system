using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// WS_WeiKanJia 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_WeiKanJia : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        /// <summary>
        /// 原商品详情
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_KanJiaTopicDescContent()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strQueryStringWeiKanJiaID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strWeiKanJiaID"]);
                int intQueryStringWeiKanJiaID = 0;
                int.TryParse(strQueryStringWeiKanJiaID, out intQueryStringWeiKanJiaID);
                EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia Model_tab_WeiKanJia = BLL_tab_WeiKanJia.GetModel("ID=" + intQueryStringWeiKanJiaID + " and isSaled=1");

                string strReturnError = "-1";
                if ((Model_tab_WeiKanJia != null))
                {
                    strReturnError = "0";
                }
                if (strReturnError == "0")
                {
                    #region pub_strKanJiaRule
                    string pub_strKanJiaRule = HttpContext.Current.Server.HtmlDecode(Model_tab_WeiKanJia.KanJiaRule);
                    /*正则表达式替换求助
               如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
               例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                    pub_strKanJiaRule = System.Text.RegularExpressions.Regex.Replace(pub_strKanJiaRule, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                    string strSearch = "src=\"/upload/";
                    pub_strKanJiaRule = pub_strKanJiaRule.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                    strSearch = "src=\"/Upload/";
                    pub_strKanJiaRule = pub_strKanJiaRule.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                    string strKanJiaRule = Microsoft.JScript.GlobalObject.encodeURIComponent(pub_strKanJiaRule);
                    #endregion

                    #region pub_strKanJiaTopicDescContent
                    string pub_strKanJiaTopicDescContent = HttpContext.Current.Server.HtmlDecode(Model_tab_WeiKanJia.KanJiaTopicDescContent);
                    /*正则表达式替换求助
               如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
               例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
                    pub_strKanJiaTopicDescContent = System.Text.RegularExpressions.Regex.Replace(pub_strKanJiaTopicDescContent, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
                    strSearch = "src=\"/upload/";
                    pub_strKanJiaTopicDescContent = pub_strKanJiaTopicDescContent.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
                    strSearch = "src=\"/Upload/";
                    pub_strKanJiaTopicDescContent = pub_strKanJiaTopicDescContent.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");

                    string strKanJiaTopicDescContent = Microsoft.JScript.GlobalObject.encodeURIComponent(pub_strKanJiaTopicDescContent);
                    #endregion

                    #region get kuncun
                    int intGoodID = Convert.ToInt32(Model_tab_WeiKanJia.GoodID);
                    EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods Model_tab_Goods = BLL_tab_Goods.GetModel(intGoodID);
                    #endregion

                    TimeSpan span = (TimeSpan)(Model_tab_WeiKanJia.EndTime - DateTime.Now);
                    str = "{\"ErrorCode\":\"0\"";
                    str += ",\"EndTimeInt\":\"" + span.TotalSeconds + "\"";
                    str += ",\"GoodIDint\":" + Model_tab_WeiKanJia.GoodID + "";
                    str += ",\"Topic\":\"" + Model_tab_WeiKanJia.Topic + "\"";
                    str += ",\"KanJiaRule\":\"" + strKanJiaRule + "\"";
                    str += ",\"KuCunCount\":" + Model_tab_Goods.KuCunCount + "";
                    str += ",\"StartPrice\":\"" + Model_tab_WeiKanJia.StartPrice + "\"";
                    str += ",\"EndPrice\":\"" + Model_tab_WeiKanJia.EndPrice + "\"";
                    str += ",\"EachAction_LowPrice\":\"" + Model_tab_WeiKanJia.EachAction_LowPrice + "\"";
                    str += ",\"EachAction_HighPrice\":\"" + Model_tab_WeiKanJia.EachAction_HighPrice + "\"";
                    str += ",\"MustSubscribe_Master\":\"" + Model_tab_WeiKanJia.MustSubscribe_Master + "\"";
                    str += ",\"MustAddress_Master\":\"" + Model_tab_WeiKanJia.MustAddress_Master + "\"";
                    str += ",\"KanJiaTopicDescContent\":\"" + strKanJiaTopicDescContent + "\"";
                    str += "}";
                }
                else
                {
                    str = "{\"ErrorCode\":\"" + strReturnError + "\"}";
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


        [WebMethod]
        public string doGameInfo_KanJia_myProperty()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strQueryStringWeiKanJiaID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strWeiKanJiaID"]);
                int intQueryStringWeiKanJiaID = 0;
                int.TryParse(strQueryStringWeiKanJiaID, out intQueryStringWeiKanJiaID);

                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                int intUserID = 0;
                int.TryParse(strUserID, out intUserID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);

                string strQMasterUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strMasterUserID"]);
                int intMasterUserID = 0;
                int.TryParse(strQMasterUserID, out intMasterUserID);
                if (intMasterUserID == 0) return "-1";////前台不允许这样传递
                #region 设置分销功能
                Int32 IFModifyParent_Parent = 0;//消息处理使用 是否改写上级

                string strvarParentID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["varParentID"]);
                int intvarParentID = 0;
                int.TryParse(strvarParentID, out intvarParentID);
                //分销所得优先给予第一人还是给予最新的转发人。（举例：A转发给B，2天后C也转发给B。然后B购买了商品，A的上线是A还是C？选择表示上线是A，不选择表示上线是C。）
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();

                bool boolDoCheckIfPayMoney = Eggsoft_Public_CL.Pub_FenXiao.DoCheckIfPayedMoney(intShopClientID, intUserID);//.getp Eggsoft_Public_CL.Pub.boolShowPower(Model_tab_User.ShopClientID.ToString(), "ShareFirstManORLastMan");


                //bool boolShareFirstManORLastMan = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ShareFirstManORLastMan");
                if (boolDoCheckIfPayMoney)
                {
                    if ((intvarParentID > 0) && (intUserID != intvarParentID))//自己不能当自己的父亲
                    {
                        Model_tab_User = BLL_tab_User.GetModel(intUserID);
                        if (Model_tab_User.ParentID <= 0)////自己没有上线  才给她认为是上线
                        {
                            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            bool boolAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + intUserID + " and ShopClientID=" + intShopClientID + " and isnull(Empowered,0)=1 and IsDeleted=0");///有代理啊

                            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            bool boolModel_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent.Exists("UserID=" + intvarParentID + " and ShopClientID=" + intShopClientID + " and Empowered=1 and IsDeleted=0");
                            if (boolModel_tab_ShopClient_Agent_ && !boolAgent)
                            {
                                IFModifyParent_Parent = intvarParentID;//消息处理使用 是否改写上级
                                Model_tab_User.ParentID = intvarParentID;
                                //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                Model_tab_User.Updatetime = DateTime.Now;
                                Model_tab_User.UpdateBy = "自己没有上线  才给她认为是上线";
                                BLL_tab_User.Update(Model_tab_User);
                            }
                        }
                    }
                }
                else
                {////只要有资质 就认为是 上线
                    if ((intvarParentID > 0) && (intUserID != intvarParentID))//自己不能当自己的父亲
                    {
                        Model_tab_User = BLL_tab_User.GetModel(intUserID);

                        EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        bool boolModel_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent.Exists("UserID=" + intvarParentID + " and ShopClientID=" + intShopClientID + " and Empowered=1 and IsDeleted=0");
                        if (boolModel_tab_ShopClient_Agent_)
                        {
                            IFModifyParent_Parent = intvarParentID;//消息处理使用 是否改写上级
                            Model_tab_User.ParentID = intvarParentID;
                            //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                            Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                            if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                            Model_tab_User.Updatetime = DateTime.Now;
                            Model_tab_User.UpdateBy = "只要有资质 就认为是 上线";
                            BLL_tab_User.Update(Model_tab_User);
                        }
                    }
                }
                #endregion



                #region 增加直推未处理信息
                if (IFModifyParent_Parent > 0)
                {
                    string strwebuy8_ClientAdmin_Users_ClientUserAccount = "改写上级微砍价服务";
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "改写上级微砍价";
                    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UserID = IFModifyParent_Parent;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(IFModifyParent_Parent.toString());
                    Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                    Model_b011_InfoAlertMessage.TypeTableID = IFModifyParent_Parent;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                }
                #endregion 增加直推未处理信息  

                #region 增加间推未处理信息
                if (Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(IFModifyParent_Parent) > 0)
                {
                    string strwebuy8_ClientAdmin_Users_ClientUserAccount = "改写上上级微砍价服务";
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "改写上上级微砍价";
                    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(IFModifyParent_Parent);
                    Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(IFModifyParent_Parent.toString());
                    Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                    Model_b011_InfoAlertMessage.TypeTableID = IFModifyParent_Parent;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                }
                #endregion 增加直推未处理信息  


                #region 加入被转发人的运营中心数据
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(Model_tab_User.ShopClientID.toInt32()))
                    {
                        Eggsoft_Public_CL.OperationCenter.update_Only_One_UserID_Operation_ID(Model_tab_User.ID, Model_tab_User.ShopClientID.toInt32(), Model_tab_User.ParentID.toInt32());
                    }
                });
                #endregion 初始化所有运营中心数据

                EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                bool boolExsit_WeiKanJiaID = BLL_tab_WeiKanJia.Exists("ID=" + intQueryStringWeiKanJiaID + " and isSaled=1");


                if (boolExsit_WeiKanJiaID)
                {



                    EggsoftWX.BLL.tab_WeiKanJia_Master BLL_tab_WeiKanJia_Master = new EggsoftWX.BLL.tab_WeiKanJia_Master();
                    bool booltab_WeiKanJia_MasterExsit = BLL_tab_WeiKanJia_Master.Exists("WeikanJiaID=" + intQueryStringWeiKanJiaID + " and UserID=" + intMasterUserID + " and ShopClientID=" + strShopClientID + " and IsDeleted<>1 and IsBuyed<>1");
                    #region 显示现在 能够购买的 立即价格   第一步  开始砍得价格
                    Decimal DecimalMyCanBuy = 0;
                    EggsoftWX.Model.tab_WeiKanJia Model_tab_WeiKanJia = BLL_tab_WeiKanJia.GetModel(intQueryStringWeiKanJiaID);

                    String pub_strMenuContent = Server.HtmlDecode(Model_tab_WeiKanJia.KanJiaTopicDescContent);///供微信分析使用
                    String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));
                    string strWeiXinDes = Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 80);

                    DecimalMyCanBuy = Convert.ToDecimal(Model_tab_WeiKanJia.StartPrice);
                    #endregion
                    #region get kuncun
                    int intGoodID = Convert.ToInt32(Model_tab_WeiKanJia.GoodID);
                    EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods Model_tab_Goods = BLL_tab_Goods.GetModel(intGoodID);
                    #endregion

                    if (booltab_WeiKanJia_MasterExsit)
                    {
                        #region 显示现在 能够购买的 立即价格  第二步  覆盖开始砍得价格
                        Decimal DecimalMasterCanBuy = 0;
                        EggsoftWX.Model.tab_WeiKanJia_Master Model_tab_WeiKanJia_Master = BLL_tab_WeiKanJia_Master.GetModel("WeikanJiaID=" + intQueryStringWeiKanJiaID + " and UserID=" + intMasterUserID + " and IsDeleted<>1 and IsBuyed<>1");
                        if ((intMasterUserID == intUserID) && (intMasterUserID != 0) && (boolExsit_WeiKanJiaID) && booltab_WeiKanJia_MasterExsit)
                        {
                            DecimalMasterCanBuy = Convert.ToDecimal(Model_tab_WeiKanJia_Master.NowPrice);
                            DecimalMyCanBuy = DecimalMasterCanBuy;
                        }
                        else if ((boolExsit_WeiKanJiaID) && (booltab_WeiKanJia_MasterExsit) && (intMasterUserID != intUserID))
                        {
                            DecimalMasterCanBuy = Convert.ToDecimal(Model_tab_WeiKanJia_Master.NowPrice);
                            DecimalMyCanBuy = Convert.ToDecimal(Model_tab_WeiKanJia.StartPrice);
                        }
                        #endregion

                        str = "{\"ErrorCode\":0,\"DecimalMyCanBuy\":" + DecimalMyCanBuy + ",\"KuCunCount\":" + Model_tab_Goods.KuCunCount;
                        if (booltab_WeiKanJia_MasterExsit)
                        {
                            #region JsonList
                            string strJsonList = "[";
                            EggsoftWX.BLL.tab_WeiKanJia_Master_Helper BLL_tab_WeiKanJia_Master_Helper = new EggsoftWX.BLL.tab_WeiKanJia_Master_Helper();
                            System.Data.DataTable DataTableMy = BLL_tab_WeiKanJia_Master_Helper.GetList("*", "WeiKanJiaMasterID=" + intQueryStringWeiKanJiaID + " and WeiKanJiaMasterUserID=" + intMasterUserID + " order by id desc").Tables[0];

                            for (int i = 0; i < DataTableMy.Rows.Count; i++)
                            {


                                string strHelperUserID = DataTableMy.Rows[i]["HelperUserID"].ToString();
                                string strUserIMG = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(Int32.Parse(strHelperUserID));
                                ///string strUserIMG = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(Int32.Parse(strHelperUserID));
                                string strUserNickName = Microsoft.JScript.GlobalObject.encodeURIComponent(Eggsoft_Public_CL.Pub.GetNickName(strHelperUserID));

                                string strMyHelperPrice = DataTableMy.Rows[i]["MyHelperPrice"].ToString();
                                string strAfterMyHelperPrice = DataTableMy.Rows[i]["AfterMyHelperPrice"].ToString();

                                string strCurList = "{\"UserIMG\":\"" + strUserIMG + "\",\"UserNickName\":\"" + strUserNickName + "\",\"MyHelperPrice\":" + strMyHelperPrice + ",\"AfterMyHelperPrice\":" + strAfterMyHelperPrice + "}";
                                if (i > 0) strJsonList += ",";
                                strJsonList += strCurList;
                            }
                            strJsonList += "]";
                            str += ",\"Exsit_WeiKanJia_Master\":" + "0" + ",\"JsonList\":" + strJsonList + "";
                            #endregion JsonList

                            #region MyKanJiaInfo
                            string strCurUserNickName = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                            string strMasterUserNickName = Eggsoft_Public_CL.Pub.GetNickName(intMasterUserID.ToString());


                            //EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                            Decimal? DecimalStartPrice = BLL_tab_WeiKanJia.GetModel(intQueryStringWeiKanJiaID).StartPrice;
                            string strOurHelperPrice = BLL_tab_WeiKanJia_Master_Helper.SelectList("select sum(MyHelperPrice) as OurHelperPrice from tab_WeiKanJia_Master_Helper where WeiKanJiaMasterID=" + intQueryStringWeiKanJiaID + " and WeiKanJiaMasterUserID=" + intMasterUserID).Tables[0].Rows[0]["OurHelperPrice"].ToString();
                            Decimal myDecimalstr_HelperPrice = 0;
                            Decimal.TryParse(strOurHelperPrice, out myDecimalstr_HelperPrice);


                            string strMyKanJiaInfo = "";
                            strMyKanJiaInfo += "<h1>亲爱的：" + strCurUserNickName + "</h1>";
                            strMyKanJiaInfo += "已有<span>" + DataTableMy.Rows.Count + "</span>位亲友，帮助<span>" + strMasterUserNickName + "</span>砍价了，共砍掉金额<span>" + Eggsoft_Public_CL.Pub.getPubMoney(myDecimalstr_HelperPrice) + "</span>元，当前价格为<span>" + DecimalMasterCanBuy + "</span>元，你也来帮他再砍一刀吧！";
                            strMyKanJiaInfo += "<h2>原价：<b style=\"text-decoration:line-through;\">￥" + DecimalStartPrice + "</b>&nbsp;&nbsp;&nbsp;&nbsp;现价：<b><span>￥" + DecimalMasterCanBuy + "</span></b></h2>";

                            str += ",\"MyKanJiaInfo\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strMyKanJiaInfo) + "\"";

                            str += ",\"MasterUserIDContactMan\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Model_tab_WeiKanJia_Master.MasterContactMan) + "\"";
                            string strMasterUserIDHeadIMG = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(intMasterUserID);
                            str += ",\"MasterUserIDHeadIMG\":\"" + strMasterUserIDHeadIMG + "\"";
                            str += ",\"Topic\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Model_tab_WeiKanJia.Topic) + "\"";
                            str += ",\"EndPrice\":\"" + Model_tab_WeiKanJia.EndPrice + "\"";
                            str += ",\"WeiXinDes\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strWeiXinDes) + "\"";


                            #endregion MyKanJiaInfo
                        }

                        str += "}";
                    }
                    else
                    {
                        str = "{\"ErrorCode\":-1,\"DecimalMyCanBuy\":" + DecimalMyCanBuy + ",\"KuCunCount\":" + Model_tab_Goods.KuCunCount + "";
                        str += ",\"Topic\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Model_tab_WeiKanJia.Topic) + "\"";
                        str += ",\"EndPrice\":\"" + Model_tab_WeiKanJia.EndPrice + "\"";
                        str += ",\"WeiXinDes\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strWeiXinDes) + "\"";

                        str += "}";
                    }

                }
                else
                {
                    str = "{\"ErrorCode\":-1}";
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



        [WebMethod]
        public string IWantDoKanJia_Submit()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strQueryStringWeiKanJiaID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strWeiKanJiaID"]);
                int intQueryStringWeiKanJiaID = 0;
                int.TryParse(strQueryStringWeiKanJiaID, out intQueryStringWeiKanJiaID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);

                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);
                int intUserID = 0;
                int.TryParse(strUserID, out intUserID);

                string strvarusername = Eggsoft.Common.CommUtil.SafeFilter(HttpUtility.UrlDecode(context.QueryString["varusername"]));
                string strvartel = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["vartel"]);

                #region 如果tab_User 姓名 等为空  补充他一下
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);


                if (String.IsNullOrEmpty(Model_tab_User.UserRealName))
                {
                    Model_tab_User.UserRealName = strvarusername;
                }
                if (String.IsNullOrEmpty(Model_tab_User.ContactMan))
                {
                    Model_tab_User.ContactMan = strvarusername;
                }
                if (String.IsNullOrEmpty(Model_tab_User.ContactPhone))
                {
                    Model_tab_User.ContactPhone = strvartel;
                }
                BLL_tab_User.Update(Model_tab_User);
                #endregion

                #region 是否关注
                EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia Model_tab_WeiKanJia = BLL_tab_WeiKanJia.GetModel("ID=" + intQueryStringWeiKanJiaID + " and isSaled=1");

                EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);
                string strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;


                if (Model_tab_User != null)
                {
                    if ((Model_tab_WeiKanJia.MustSubscribe_Master == true) && (Model_tab_User.Subscribe == false))
                    {
                        str = "{\"ErrorCode\":1,\"GuideSubscribe\":\"" + strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ + "\"}";////必须关注
                    }
                    else if ((Model_tab_WeiKanJia.MustAddress_Master == true) && (Model_tab_User.Default_Address == 0))
                    {
                        str = "{\"ErrorCode\":5}";////发起砍价是否必须输入收获地址
                    }
                    else if ((Model_tab_WeiKanJia.MustSubscribe_Agent == true) && (Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(intUserID) < 1))
                    {
                        string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intUserID);

                        str = "{\"ErrorCode\":4,\"AgentSubscribe\":\"" + Pub_Agent_Path + "/edityourshopini.aspx\"}";////必须代理
                    }
                    else
                    {
                        #region 加入微砍价表
                        if (Convert.ToBoolean(Model_tab_WeiKanJia.MustSubscribe_Master) && (Model_tab_User.Subscribe == false))
                        {
                            str = "{\"ErrorCode\":2}";////表示必须关注
                        }
                        else
                        {
                            EggsoftWX.BLL.tab_WeiKanJia_Master BLL_tab_WeiKanJia_Master = new EggsoftWX.BLL.tab_WeiKanJia_Master();
                            if (BLL_tab_WeiKanJia_Master.Exists("WeikanJiaID=" + intQueryStringWeiKanJiaID + " and UserID=" + intUserID) == false)
                            {
                                EggsoftWX.Model.tab_WeiKanJia_Master Model_tab_WeiKanJia_Master = new EggsoftWX.Model.tab_WeiKanJia_Master();
                                Model_tab_WeiKanJia_Master.UserID = intUserID;
                                Model_tab_WeiKanJia_Master.ShopClientID = intShopClientID;
                                Model_tab_WeiKanJia_Master.WeikanJiaID = intQueryStringWeiKanJiaID;
                                Model_tab_WeiKanJia_Master.NowPrice = Model_tab_WeiKanJia.StartPrice;
                                Model_tab_WeiKanJia_Master.MasterContactMan = strvarusername;
                                Model_tab_WeiKanJia_Master.MasteContactPhone = strvartel;
                                BLL_tab_WeiKanJia_Master.Add(Model_tab_WeiKanJia_Master);
                            }

                            #region make strPub_Agent_Path  如果 本人不是代理 就看看 他的父亲是不是
                            string parentagentadid = "0";
                            string parentagentid = "0";

                            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intUserID + " and ShopClientID=" + strShopClientID + " and Empowered=1 and IsDeleted=0");
                            if (Model_tab_ShopClient_Agent_ != null)
                            {

                                if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)
                                {
                                    parentagentadid = intUserID.ToString();///还是高级代理那
                                }
                                else
                                {
                                    parentagentid = intUserID.ToString();
                                }

                            }
                            else
                            {
                                ///就看看 他的父亲是不是
                                int intParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intUserID);
                                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ParentID = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intParentID + " and ShopClientID=" + strShopClientID + " and Empowered=1");
                                if (Model_tab_ShopClient_Agent_ != null)
                                {
                                    if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)
                                    {
                                        parentagentadid = intParentID.ToString();///还是高级代理那
                                    }
                                    else
                                    {
                                        parentagentid = intParentID.ToString();
                                    }
                                }
                            }
                            #endregion


                            #region get kuncun
                            int intGoodID = Convert.ToInt32(Model_tab_WeiKanJia.GoodID);
                            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                            EggsoftWX.Model.tab_Goods Model_tab_Goods = BLL_tab_Goods.GetModel(intGoodID);
                            #endregion
                            str = "{\"ErrorCode\":0,\"KuCunCount\":" + Model_tab_Goods.KuCunCount + ",\"parentagentadid\":" + parentagentadid + ",\"parentagentid\":" + parentagentid + "}";////添加成功
                        }

                        #endregion
                    }
                }
                else
                {
                    str = "{\"ErrorCode\":3,\"GuideSubscribe\":\"" + strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ + "\"}";////必须关注

                }
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }

                #endregion

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "微砍价");
            }
            return str;
        }

        /// <summary>
        /// 帮助别人砍价
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string IWantDoKanJia_Help()////帮助别人砍价
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strQueryStringWeiKanJiaID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strWeiKanJiaID"]);
                int intQueryStringWeiKanJiaID = 0;
                int.TryParse(strQueryStringWeiKanJiaID, out intQueryStringWeiKanJiaID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);

                string strMasterUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["MasterUserID"]);
                int intMasterUserID = 0;
                int.TryParse(strMasterUserID, out intMasterUserID);

                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);
                int intUserID = 0;
                int.TryParse(strUserID, out intUserID);
                str = "{\"ErrorCode\":-1}";////表示砍价失败

                EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia Model_tab_WeiKanJia = BLL_tab_WeiKanJia.GetModel(intQueryStringWeiKanJiaID);
                if (Model_tab_WeiKanJia != null)
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
                    if (Model_tab_User != null)
                    {
                        if ((Model_tab_WeiKanJia.MustSubscribe_Helper == true) && (Model_tab_User.Subscribe == false))
                        {
                            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);
                            string strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;

                            str = "{\"ErrorCode\":1,\"GuideSubscribe\":\"" + strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ + "\"}";////必须关注
                        }
                        else
                        {

                            EggsoftWX.BLL.tab_WeiKanJia_Master BLL_tab_WeiKanJia_Master = new EggsoftWX.BLL.tab_WeiKanJia_Master();
                            EggsoftWX.Model.tab_WeiKanJia_Master Model_tab_WeiKanJia_Master = BLL_tab_WeiKanJia_Master.GetModel("UserID=" + intMasterUserID + " and WeikanJiaID=" + intQueryStringWeiKanJiaID + " and ShopClientID=" + intShopClientID);
                            if (Model_tab_WeiKanJia_Master != null)
                            {
                                EggsoftWX.BLL.tab_WeiKanJia_Master_Helper BLL_tab_WeiKanJia_Master_Helper = new EggsoftWX.BLL.tab_WeiKanJia_Master_Helper();
                                if (BLL_tab_WeiKanJia_Master_Helper.Exists("WeiKanJiaMasterID=" + intQueryStringWeiKanJiaID + " and WeiKanJiaMasterUserID=" + intMasterUserID + " and ShopClientID=" + intShopClientID + " and HelperUserID=" + intUserID) == false)
                                {
                                    EggsoftWX.Model.tab_WeiKanJia_Master_Helper Model_tab_WeiKanJia_Master_Helper = new EggsoftWX.Model.tab_WeiKanJia_Master_Helper();
                                    Model_tab_WeiKanJia_Master_Helper.ShopClientID = intShopClientID;
                                    Model_tab_WeiKanJia_Master_Helper.WeiKanJiaMasterID = intQueryStringWeiKanJiaID;
                                    Model_tab_WeiKanJia_Master_Helper.WeiKanJiaMasterUserID = intMasterUserID;
                                    Model_tab_WeiKanJia_Master_Helper.HelperUserID = intUserID;

                                    Decimal EachAction_LowPrice = (Decimal)Model_tab_WeiKanJia.EachAction_LowPrice;
                                    Decimal EachAction_HighPrice = (Decimal)Model_tab_WeiKanJia.EachAction_HighPrice;
                                    Random Random1 = new Random();
                                    ///C#如何在两个float类型之间取随机数   rand.NextDouble() * (上限 - 下限) + 下限;
                                    Decimal myDecimal = (Decimal)Random1.NextDouble() * (EachAction_HighPrice - EachAction_LowPrice) + EachAction_LowPrice;
                                    ///砍价不能小于低价
                                    if ((Model_tab_WeiKanJia_Master.NowPrice - myDecimal) >= Model_tab_WeiKanJia.EndPrice)
                                    {
                                        Model_tab_WeiKanJia_Master_Helper.MyHelperPrice = myDecimal;
                                        Model_tab_WeiKanJia_Master_Helper.AfterMyHelperPrice = Model_tab_WeiKanJia_Master.NowPrice - myDecimal;
                                    }
                                    else
                                    {
                                        Model_tab_WeiKanJia_Master_Helper.MyHelperPrice = Model_tab_WeiKanJia_Master.NowPrice - Model_tab_WeiKanJia.EndPrice;
                                        Model_tab_WeiKanJia_Master_Helper.AfterMyHelperPrice = Model_tab_WeiKanJia.EndPrice;
                                    }
                                    BLL_tab_WeiKanJia_Master_Helper.Add(Model_tab_WeiKanJia_Master_Helper);

                                    Model_tab_WeiKanJia_Master.NowPrice = Model_tab_WeiKanJia_Master_Helper.AfterMyHelperPrice;
                                    Model_tab_WeiKanJia_Master.UpdateTime = DateTime.Now;
                                    BLL_tab_WeiKanJia_Master.Update(Model_tab_WeiKanJia_Master);

                                    str = "{\"ErrorCode\":0,\"MyHelperPrice\":" + Eggsoft_Public_CL.Pub.getBankPubMoney(Convert.ToDecimal(Model_tab_WeiKanJia_Master_Helper.MyHelperPrice)) + "}";////表示砍价成功

                                }
                                else
                                {
                                    EggsoftWX.Model.tab_WeiKanJia_Master_Helper Model_tab_WeiKanJia_Master_Helper = BLL_tab_WeiKanJia_Master_Helper.GetModel("WeiKanJiaMasterID=" + intQueryStringWeiKanJiaID + " and WeiKanJiaMasterUserID=" + intMasterUserID + " and ShopClientID=" + intShopClientID + " and HelperUserID=" + intUserID);

                                    str = "{\"ErrorCode\":\"2\",\"MyHelperPrice\":" + Eggsoft_Public_CL.Pub.getBankPubMoney(Convert.ToDecimal(Model_tab_WeiKanJia_Master_Helper.MyHelperPrice)) + "}";////表示已经砍过价了
                                }
                                //}
                            }
                        }
                    }
                    else
                    {
                        EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                        EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);
                        string strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;

                        str = "{\"ErrorCode\":3,\"GuideSubscribe\":\"" + strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ + "\"}";////必须关注
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
            return str;
        }

        #region 微kanjia 访问事件
        /// <summary>
        ///微kanjia 访问事件
        /// </summary>
        /// <returns></returns>

        [WebMethod]
        public string doVisitWeiKanJiaAction()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;


                string strmasteruserid = context.QueryString["strMasterUserID"];
                int pIntMasterUserid = 0;
                int.TryParse(strmasteruserid, out pIntMasterUserid);

                string strWeiKanJiaID = context.QueryString["strWeiKanJiaID"];
                int pIntWeiKanJiaID = 0;
                int.TryParse(strWeiKanJiaID, out pIntWeiKanJiaID);

                string strpInt_QueryString_ParentID = context.QueryString["varParentID"];
                int pInt_QueryString_ParentID = 0;
                int.TryParse(strpInt_QueryString_ParentID, out pInt_QueryString_ParentID);

                string strpub_Int_Session_CurUserID = context.QueryString["strUserID"];
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);

                string strpub_Int_ShopClientID = context.QueryString["ShopClientID"];
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_Int_Session_CurUserID);///保证同源
                int intParentID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpInt_QueryString_ParentID);///保证同源
                int intGoodID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromWeiKanJiaID(pIntWeiKanJiaID);///保证同源
                if ((pInt_QueryString_ParentID == 0) || ((intUserID_ShopClientID == pub_Int_ShopClientID) && (intParentID_ShopClientID == pub_Int_ShopClientID) && (intGoodID_ShopClientID == pub_Int_ShopClientID)))///保证同源
                {
                    sendSNSToShopClientWeiXin(pIntWeiKanJiaID, pIntMasterUserid, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToMasterUseridWeiXin(pIntWeiKanJiaID, pIntMasterUserid, pub_Int_ShopClientID, pub_Int_Session_CurUserID);

                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);
                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID + ",strWeiKanJiaID=" + strWeiKanJiaID, "临界区突破strWeiKanJiaID");

                }

                str = "{\"ErrorCode\":0}";////表示ok

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                str = "{\"ErrorCode\":-1}";////表示ok
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }

        private void sendSNSToShopClientWeiXin(int pIntWeiKanJiaID, int pIntMasterUserid, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();

                my_Model_tab_WeiKanJia = my_BLL_tab_WeiKanJia.GetModel(pIntWeiKanJiaID);

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "正在浏览" + my_Model_tab_WeiKanJia.Topic + ",由" + Eggsoft_Public_CL.Pub.GetNickName(pIntMasterUserid.ToString()) + "发起";

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_WeiKanJia.GoodID));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                //String pub_strMenuContent = Server.HtmlDecode(my_Model_tab_WeiKanJia.KanJiaTopicDescContent);///供微信分析使用
                //String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));
                String pub_strMenuContent = Server.HtmlDecode(my_Model_tab_WeiKanJia.KanJiaTopicDescContent);///供微信分析使用
                String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));
                string strWeiXinDes = Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 180);

                strDescription += strWeiXinDes + "。";

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + pIntWeiKanJiaID.ToString() + "&masteruserid=" + pIntMasterUserid;
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);


                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WeiKanJiaLook"))
                {
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
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

        private void sendSNSToMasterUseridWeiXin(int pIntWeiKanJiaID, int pIntMasterUserid, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();

                my_Model_tab_WeiKanJia = my_BLL_tab_WeiKanJia.GetModel(pIntWeiKanJiaID);

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "正在浏览" + my_Model_tab_WeiKanJia.Topic + ",由" + Eggsoft_Public_CL.Pub.GetNickName(pIntMasterUserid.ToString()) + "发起";

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_WeiKanJia.GoodID));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                String pub_strMenuContent = Server.HtmlDecode(my_Model_tab_WeiKanJia.KanJiaTopicDescContent);///供微信分析使用
                String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));
                string strWeiXinDes = Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 180);

                strDescription += strWeiXinDes + "。";

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + pIntWeiKanJiaID.ToString() + "&masteruserid=" + pIntMasterUserid;
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);


                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WeiKanJiaLook"))
                {

                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pIntMasterUserid, pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);
                    string[] strCheckReSendList = { "45015", "45047" };
                    bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                    if (exists)
                    {
                        if (boolTempletVisitMessage)
                        {
                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pIntMasterUserid, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                        }
                    }

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
        #endregion
        #region 微kanjia 帮砍事件 发送消息
        /// <summary>
        ///微kanjia 帮砍事件 发送消息
        /// </summary>
        /// <returns></returns>

        [WebMethod]
        public string doHelpWeiKanJiaAction()
        {
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;


                string strmasteruserid = context.QueryString["strMasterUserID"];
                int pIntMasterUserid = 0;
                int.TryParse(strmasteruserid, out pIntMasterUserid);

                string strWeiKanJiaID = context.QueryString["strWeiKanJiaID"];
                int pIntWeiKanJiaID = 0;
                int.TryParse(strWeiKanJiaID, out pIntWeiKanJiaID);

                string strpInt_QueryString_ParentID = context.QueryString["strpInt_QueryString_ParentID"];
                int pInt_QueryString_ParentID = 0;
                int.TryParse(strpInt_QueryString_ParentID, out pInt_QueryString_ParentID);

                string strpub_Int_Session_CurUserID = context.QueryString["strUserID"];
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);

                string strpub_Int_ShopClientID = context.QueryString["ShopClientID"];
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_Int_Session_CurUserID);///保证同源
                int intParentID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpInt_QueryString_ParentID);///保证同源
                int intGoodID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromWeiKanJiaID(pIntWeiKanJiaID);///保证同源
                if ((pInt_QueryString_ParentID == 0) || ((intUserID_ShopClientID == pub_Int_ShopClientID) && (intParentID_ShopClientID == pub_Int_ShopClientID) && (intGoodID_ShopClientID == pub_Int_ShopClientID)))///保证同源
                {
                    sendSNSToShopClientWeiXin_Help(pIntWeiKanJiaID, pIntMasterUserid, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToMasterUseridWeiXin_Help(pIntWeiKanJiaID, pIntMasterUserid, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID + ",strWeiKanJiaID=" + strWeiKanJiaID, "临界区突破strWeiKanJiaID");

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
            return "1";
        }

        private void sendSNSToShopClientWeiXin_Help(int pIntWeiKanJiaID, int pIntMasterUserid, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();

                my_Model_tab_WeiKanJia = my_BLL_tab_WeiKanJia.GetModel(pIntWeiKanJiaID);

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                EggsoftWX.BLL.tab_WeiKanJia_Master_Helper bll_tab_WeiKanJia_Master_Helper = new EggsoftWX.BLL.tab_WeiKanJia_Master_Helper();
                EggsoftWX.Model.tab_WeiKanJia_Master_Helper Model_tab_WeiKanJia_Master_Helper = bll_tab_WeiKanJia_Master_Helper.GetModel("WeiKanJiaMasterID=" + pIntWeiKanJiaID + " and   WeiKanJiaMasterUserID=" + pIntMasterUserid + " and  HelperUserID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID);



                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "帮助砍价" + my_Model_tab_WeiKanJia.Topic + ",由" + Eggsoft_Public_CL.Pub.GetNickName(pIntMasterUserid.ToString()) + "发起,成功砍掉" + Model_tab_WeiKanJia_Master_Helper.MyHelperPrice + "元,砍后价格" + Model_tab_WeiKanJia_Master_Helper.AfterMyHelperPrice + "元";
                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_WeiKanJia.GoodID));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                String pub_strMenuContent = Server.HtmlDecode(my_Model_tab_WeiKanJia.KanJiaTopicDescContent);///供微信分析使用
                String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));
                string strWeiXinDes = Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 180);


                strDescription += strWeiXinDes + "。";

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + pIntWeiKanJiaID.ToString() + "&masteruserid=" + pIntMasterUserid;
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);


                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WeiKanJiaLook"))
                {
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
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

        private void sendSNSToMasterUseridWeiXin_Help(int pIntWeiKanJiaID, int pIntMasterUserid, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();

                my_Model_tab_WeiKanJia = my_BLL_tab_WeiKanJia.GetModel(pIntWeiKanJiaID);

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                EggsoftWX.BLL.tab_WeiKanJia_Master_Helper bll_tab_WeiKanJia_Master_Helper = new EggsoftWX.BLL.tab_WeiKanJia_Master_Helper();
                EggsoftWX.Model.tab_WeiKanJia_Master_Helper Model_tab_WeiKanJia_Master_Helper = bll_tab_WeiKanJia_Master_Helper.GetModel("WeiKanJiaMasterID=" + pIntWeiKanJiaID + " and   WeiKanJiaMasterUserID=" + pIntMasterUserid + " and  HelperUserID=" + pub_Int_Session_CurUserID + " and ShopClientID=" + pub_Int_ShopClientID);



                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  
                //string strTitle = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "正在浏览" + my_Model_tab_WeiKanJia.Topic + ",由" + Eggsoft_Public_CL.Pub.GetNickName(pIntMasterUserid.ToString()) + "发起";
                string strTitle = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "帮助砍价" + my_Model_tab_WeiKanJia.Topic + ",帮您成功砍掉" + Model_tab_WeiKanJia_Master_Helper.MyHelperPrice + "元,砍后价格" + Model_tab_WeiKanJia_Master_Helper.AfterMyHelperPrice + "元";

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_WeiKanJia.GoodID));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                String pub_strMenuContent = Server.HtmlDecode(my_Model_tab_WeiKanJia.KanJiaTopicDescContent);///供微信分析使用
                String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));
                string strWeiXinDes = Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 180);

                strDescription += strWeiXinDes + "。";

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + pIntWeiKanJiaID.ToString() + "&masteruserid=" + pIntMasterUserid;
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);


                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WeiKanJiaLook"))
                {

                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pIntMasterUserid, pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);
                    string[] strCheckReSendList = { "45015", "45047" };
                    bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                    if (exists)
                    {
                        if (boolTempletVisitMessage)
                        {
                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pIntMasterUserid, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                        }
                    }

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
        #endregion
    }

}
