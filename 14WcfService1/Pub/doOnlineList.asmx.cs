using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Pub
{
    /// <summary>
    /// doOnlineList 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doOnlineList : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        /// <summary>
        /// 加载在线报名列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doAjaxLoad05OnlineList()
        {

           // System.Threading.Thread.Sleep(100000);
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
                    //sql.addOrderField("tab_TuanGou.sort", "asc");//第一排序字段  
                    sql.addOrderField("id", "desc");//第二排序字段  

                    //string strTablename = "tab_TuanGou LEFT OUTER JOIN  tab_Goods ON tab_TuanGou.ShopClientID = tab_Goods.ShopClient_ID AND tab_TuanGou.SourceGoodID = tab_Goods.ID";
                    string strTablename = " tab_ShopClient_OlineContent";
                    sql.table = strTablename;

                    string stroutfields = "[ID]";
                    stroutfields += ",[ShopClient_ID]";
                    stroutfields += ",[Oline_Content]";
                    stroutfields += ",[Title]";
                    stroutfields += ",[XML]";
                    stroutfields += ",[AddExpListTextShow]";
                    stroutfields += ",[AddExpListText]";
                    stroutfields += ",[CreateTime]";
                    stroutfields += ",[Createby]";
                    stroutfields += ",[UpdateTime]";
                    stroutfields += ",[Updateby]";
                    stroutfields += ",[IsDeleted]";
                    sql.outfields = stroutfields;
                    sql.nowPageIndex = 1;
                    sql.pagesize = 1000;

                    string strOnLineJointWhere = "";
                    strOnLineJointWhere += "  ShopClient_ID = " + strShopClientID + " and isnull(IsDeleted,0)=0";
                    string strWhere = " ";
                    string strwhere = strOnLineJointWhere + strWhere;
                    EggsoftWX.BLL.tab_ShopClient_OlineContent BLL_tab_ShopClient_OlineContent = new EggsoftWX.BLL.tab_ShopClient_OlineContent();
                    EggsoftWX.BLL.tab_ShopClient_OnlineRegistration BLL_tab_ShopClient_OnlineRegistration = new EggsoftWX.BLL.tab_ShopClient_OnlineRegistration();

                    sql.where = strwhere;
                    string strSQLRecordCount = String.Format("SELECT {0} FROM " + strTablename + " WHERE" + strOnLineJointWhere, "count(*) as RecordCount") + strWhere;
                    string strRecordCount = BLL_tab_ShopClient_OlineContent.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();
                    string strSql = sql.getSQL(Int32.Parse(strRecordCount));
                    System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_OlineContent.SelectList(strSql).Tables[0];

                    string strThisOnlineInfoAddList = "[";
                    for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                    {
                        string strThisItemInfo = "{";
                        string strOnLineID = Data_DataTable.Rows[i]["ID"].ToString();
                        string strOline_Content = Data_DataTable.Rows[i]["Oline_Content"].ToString();
                        string strTitle = Data_DataTable.Rows[i]["Title"].ToString();
                        string strXML = Data_DataTable.Rows[i]["XML"].ToString();
                        string strDeadLine = "";
                        if (string.IsNullOrEmpty(strXML) == false)
                        {
                            Eggsoft_Public_CL.XmlHelper_Instance_Online myXML = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XmlHelper_Instance_Online>(strXML, System.Text.Encoding.UTF8);
                            ///CheckBox_NeedCheck.Checked = myXML.BoolNeedCheck;
                            //CheckBox_Need_WriteData.Checked = myXML.NeedWrite_NotChoiceShengFeng;
                            strDeadLine = myXML.NeedWrite_DeadlineTime.ToString("yyyy-MM-dd");
                        }
                        string pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetFirstHtmlImageUrlByShopClientID(Server.HtmlDecode(strOline_Content), strShopClientID.toInt32());

                        #region 检查用户是否参与报名
                        bool boolIfUserOnline = BLL_tab_ShopClient_OnlineRegistration.Exists("ShopClient_ID=" + strShopClientID + " and OnlineID="+ strOnLineID + " and UserID=" + strUseid + " and isnull(IsDeleted,0)=0");

                        #endregion 检查用户是否参与报名


                        strThisItemInfo += "\"OnlineID\":\"" + strOnLineID + "\"";
                        strThisItemInfo += ",\"boolIfUserOnline\":\"" + boolIfUserOnline.toInt32() + "\"";
                        strThisItemInfo += ",\"DeadLine\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strDeadLine) + "\"";
                        strThisItemInfo += ",\"Title\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strTitle) + "\"";
                        strThisItemInfo += ",\"ImageFull\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(pub_str_FirstImageFull) + "\"";
                        strThisItemInfo += "}";
                        strThisOnlineInfoAddList += strThisItemInfo;
                        if (i != Data_DataTable.Rows.Count - 1) strThisOnlineInfoAddList += ",";
                    }
                    strThisOnlineInfoAddList += "]";

                    str = "{\"ErrorCode\":0,\"RecordCount\":" + Data_DataTable.Rows.Count + ",\"UserOnlineList\":" + strThisOnlineInfoAddList + "}";

                    if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                    {
                        HttpRequest Request = HttpContext.Current.Request;
                        HttpResponse Response = HttpContext.Current.Response;
                        string callback = Request["jsonp"];
                        Response.Write(callback + "(" + str + ")");
                        Response.End();//结束后续的操作，直接返回所需要的字符串
                    }
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "在线报名列表", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "在线报名列表", "程序报错");
            }
            finally
            {

            }
            return str;
        }
    }
}