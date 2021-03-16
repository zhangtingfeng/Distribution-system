using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace _14WcfS.PCService
{
    /// <summary>
    /// WS_User 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    //[System.Web.Script.Services.ScriptService]
    public class WS_User : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 视图实现二维码 扫描登陆的类似  Socket功能
        /// </summary>
        /// <returns></returns>

        [WebMethod]
        public string UserSaoYiSaoErWeiMa(string strargUserOpenID, string strargUserSessionID)
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            string strUserOpenID = context.QueryString["strargUserOpenID"];
            string strUserSessionID = context.QueryString["strargUserSessionID"];

            Table_SaoYiSaoErWeiMa newTable_SaoYiSaoErWeiMa = new Table_SaoYiSaoErWeiMa();
            newTable_SaoYiSaoErWeiMa.Table_SaoYiSaoErWeiMa_add(strUserOpenID, strUserSessionID);

            string str = "{\"ErrorCode\":\"0\"}";
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return str;
        }

        [WebMethod]
        public string CheckUserSaoYiSaoErWeiMa()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            string strUserSessionID = context.QueryString["strUserSessionID"];

            Table_SaoYiSaoErWeiMa newTable_SaoYiSaoErWeiMa = new Table_SaoYiSaoErWeiMa();
            string strUserOpenID = newTable_SaoYiSaoErWeiMa.Table_SaoYiSaoErWeiMa_Check(strUserSessionID);

            string strReturnError = "";
            if (String.IsNullOrEmpty(strUserOpenID))
            {
                strReturnError = "{\"ErrorCode\":\"-1\"}";
            }
            else
            {
                string strUserID = Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(strUserOpenID);
                string strPub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(Int32.Parse(strUserID));
                strReturnError = "{\"ErrorCode\":\"0\",\"UserOpenID\":\"" + strUserOpenID + "\",\"AgentPath\":\"" + strPub_Agent_Path + "\"}";
            }

            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturnError + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return strReturnError;
        }

        /// <summary>
        /// PC注册后  得到当前用户的二维码
        /// </summary>
        /// <param name="strThisOpenID"></param>
        [WebMethod]
        public string Image_MarkerGetErWeiMa()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string strUserOpenID = context.QueryString["strThisOpenID"];


            string strpub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(strUserOpenID);

            String strPath = "";
            String strAgentDesc = "";
            try
            {


                ///检查 当前人是否是代理 是的话 取路径 1  取代理路径 2 检查文件是否存在 3不存在再做一次 还是没有只好作罢
                //不是的话 去其父的 
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolExsitAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + strpub_Int_Session_CurUserID + " and isnull(Empowered, 0) = 1 and IsDeleted=0");
                //Image_MarkerErWeiMa.Visible = false;
                if (boolExsitAgent == false)
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("ID=" + strpub_Int_Session_CurUserID);
                    int pInt_QueryString_ParentID = Convert.ToInt32(Model_tab_User.ParentID);
                    if (pInt_QueryString_ParentID > 0)
                    {
                        Eggsoft_Public_CL.Pub_Agent.Pub_Agent_GetAgent_WeiXinErWeiMaPath(pInt_QueryString_ParentID, out strPath);////取其父亲的代理二维码
                        strAgentDesc = "代理证书:" + Model_tab_User.NickName;
                        if (String.IsNullOrEmpty(strPath))
                        {
                            ///nothing   空了 没有任何需要处理的路径
                        }
                        else if (Eggsoft.Common.FileFolder.RemoteFileExists(strPath))
                        {
                            ////已经存在了
                        }
                        else
                        {
                            #region 重跑一下代理证书  会自动生成二维码
                            string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_UserAgentCertification.asmx";
                            string[] args = new string[1];
                            args[0] = pInt_QueryString_ParentID.ToString();// "/UpLoad/images/";
                            object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WebMethod_APPCODE_getImage_UserAgentCertification", args);
                            string strresult = result.ToString();
                            #endregion
                        }
                    }

                }
                else
                {
                    Eggsoft_Public_CL.Pub_Agent.Pub_Agent_GetAgent_WeiXinErWeiMaPath(Int32.Parse(strpub_Int_Session_CurUserID), out strPath);
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("ID=" + strpub_Int_Session_CurUserID);

                    strAgentDesc = "代理证书:" + Model_tab_User.NickName;
                    if (Eggsoft.Common.FileFolder.RemoteFileExists(strPath))
                    {

                    }
                    else
                    {
                        #region 重跑一下代理证书  会自动生成二维码
                        string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_UserAgentCertification.asmx";
                        string[] args = new string[1];
                        args[0] = strpub_Int_Session_CurUserID;// "/UpLoad/images/";
                        object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WebMethod_APPCODE_getImage_UserAgentCertification", args);
                        string strresult = result.ToString();
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e);
            }
            string strResult = "";
            if (String.IsNullOrEmpty(strPath))
            {
                strResult = "{\"ErrorCode\":\"-1\"}";
            }
            else
            {
                strResult = "{\"ErrorCode\":\"0\",\"WeiXinErWeiMaPath\":\"" + strPath + "\",\"WeiXinNickName\":\"" + strAgentDesc + "\"}";
            }
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strResult + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return strResult;
        }

    }
    /// <summary>
    /// 参考文件  http://blog.csdn.net/hcw_peter/article/details/3980723
    /// </summary>

    public class Table_SaoYiSaoErWeiMa
    {
        static bool boolNewcolumns = false;
        static DataTable staticTable_SaoYiSaoErWeiMa = null;
        public Table_SaoYiSaoErWeiMa()
        {
            if (boolNewcolumns == false)
            {
                staticTable_SaoYiSaoErWeiMa = new DataTable("Table_SaoYiSaoErWeiMa");

                DataColumn dcUserOpenID = new DataColumn("UserOpenID", System.Type.GetType("System.String"));
                staticTable_SaoYiSaoErWeiMa.Columns.Add(dcUserOpenID);

                DataColumn dcUserSessionID = new DataColumn("UserSessionID", System.Type.GetType("System.String"));
                staticTable_SaoYiSaoErWeiMa.Columns.Add(dcUserSessionID);

                DataColumn dcUpdateTime = new DataColumn("UpdateTime", System.Type.GetType("System.DateTime"));
                staticTable_SaoYiSaoErWeiMa.Columns.Add(dcUpdateTime);
                boolNewcolumns = true;
            }
        }

        /// <summary>
        /// 两个都要UserOpenID  UserSessionID 因为 可能是 2个不同的浏览器 或者 不同的电脑 用同一个微信账户扫描登录了
        /// </summary>
        /// <param name="strUserOpenID"></param>
        /// <param name="strUserSessionID"></param>
        public void Table_SaoYiSaoErWeiMa_add(string strUserOpenID, string strUserSessionID)
        {
            DataRow[] drssDataRow = staticTable_SaoYiSaoErWeiMa.Select("UserOpenID = '" + strUserOpenID + "' and UserSessionID='" + strUserSessionID + "'");
            if (drssDataRow.GetLength(0) == 0)
            {
                DataRow dr = staticTable_SaoYiSaoErWeiMa.NewRow();
                dr["UserOpenID"] = strUserOpenID;
                dr["UserSessionID"] = strUserSessionID;
                dr["UpdateTime"] = DateTime.Now;
                staticTable_SaoYiSaoErWeiMa.Rows.Add(dr);
            }
        }

        public static string DataTableToString(DataTable dt)
        {
            string dtstring = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dtstring = dtstring + dt.Columns[i].ColumnName + "\t";
            }
            dtstring = dtstring + "\r\n";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dtstring = dtstring + dt.Rows[i][j] + "\t";
                }
                dtstring = dtstring + "\r\n";
            }
            return dtstring;
        }


        public String Table_SaoYiSaoErWeiMa_Check(string strUserSessionID)
        {


            DataRow[] drssDataRow = staticTable_SaoYiSaoErWeiMa.Select("UserSessionID = '" + strUserSessionID + "'");
            if (drssDataRow.GetLength(0) == 0)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(DataTableToString(staticTable_SaoYiSaoErWeiMa));


                return "";
            }
            else
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(DataTableToString(staticTable_SaoYiSaoErWeiMa));
                string strUserOpenID = drssDataRow[0]["UserOpenID"].ToString();
                staticTable_SaoYiSaoErWeiMa.Rows.Remove(drssDataRow[0]);//而Remove方法则是直接删除.   暂时不删除
                return strUserOpenID;
            }
        }


    }
}
