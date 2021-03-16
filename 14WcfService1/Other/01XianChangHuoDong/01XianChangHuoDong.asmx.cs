using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Other._01XianChangHuoDong
{
    /// <summary>
    /// _01XianChangHuoDong 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class _01XianChangHuoDong : System.Web.Services.WebService
    {

        #region 摇一摇
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        //  检测 缓存 是否 存在 说 摇动 已经开始
        // 缓存 说 是 timer 单独 控制 。一秒  去 请求一次 主服务器 。看看 活动 开始 没有。用一个 锁  进程 比较 简单
        /// 如果 开始 。就 通知（返回） 客户端，活动 开始了。活动 进入 统计 次数 阶段。就是 不断 的  更新 本 数据库 219.235.0.112

        #region 1100 检测 缓存 是否 存在 说 摇动 已经开始
        private String doGetHuoDongNumberStatusIsTrue(string strintShopClientID)
        {
            string strIP = this.Context.Request.UserHostAddress;
            string strReturn = "-1";
            try
            {

                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ActivityState=1 and ShopClientID=" + strintShopClientID);



                if (Model_tab_ShopClient_XianChangHuoDong == null)
                {
                    strReturn = "-1";
                }
                else
                {
                    string strXianChangHuoDongID = "";
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel("IsDoing=1 and ShopClientID=" + strintShopClientID);
                    if (Model_tab_ShopClient_XianChangHuoDong_Number == null)
                    {
                        Model_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number();
                        Model_tab_ShopClient_XianChangHuoDong_Number.ShopClientID = Int32.Parse(strintShopClientID);
                        Model_tab_ShopClient_XianChangHuoDong_Number.XianChangHuoDongID = Model_tab_ShopClient_XianChangHuoDong.ID;
                        Model_tab_ShopClient_XianChangHuoDong_Number.IsDoing = 1;
                        int intID = BLL_tab_ShopClient_XianChangHuoDong_Number.Add(Model_tab_ShopClient_XianChangHuoDong_Number);

                        Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel(intID);
                        strXianChangHuoDongID = Model_tab_ShopClient_XianChangHuoDong_Number.XianChangHuoDongNumberbyShopClientID.ToString();/// 可以登录
                    }
                    else
                    {
                        strXianChangHuoDongID = Model_tab_ShopClient_XianChangHuoDong_Number.XianChangHuoDongNumberbyShopClientID.ToString();/// 可以登录
                    }
                    strReturn = strXianChangHuoDongID + "#" + Model_tab_ShopClient_XianChangHuoDong.CountHowMany + "#" + Model_tab_ShopClient_XianChangHuoDong.LongShakeTime + "#" + Model_tab_ShopClient_XianChangHuoDong.MaxTracks;
                    Eggsoft.Common.debug_Log.Call_WriteLog("strReturn=" + strReturn + " strIP=" + strIP, "doGetHuoDongNumberStatusIsTrue");
                }

            }
            catch (System.Threading.ThreadAbortException e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return strReturn;
            //return intErrorCode.ToString();
        }




        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
        public class pHelp_01XianChangHuoDong : Attribute
        {///现场活动序号 每个商家 的 相应递增
            public int? XianChangHuoDongNum { get; set; }
            /// <summary>
            /// 当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束
            /// </summary>
            public int? XianChangHuoDongStatus { get; set; }
        }

        private static object Lockinsert_UserShake = new object();////复制 数据期间 不能 检测编号

        /// <summary>
        /// 插入 大头像 显示的表
        /// </summary>
        private void insertHelp_01XianChangHuoDong_UserShake(string strpub_varUserHostAddress, string strUserIDNickName, string strUserIDHeadURL, string strUserIDWeiXinHeadURL, int pInt_QueryString_UserID, int pub_Int_ShopClientID, int XianChangHuoDongNum)
        {
            EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake BLL_Help_01XianChangHuoDong_UserShake = new EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake();
            String strExsitWhere = "ShopClientID=" + pub_Int_ShopClientID + " and UserID=" + pInt_QueryString_UserID + " and XianChangHuoDongNum=" + XianChangHuoDongNum;
            lock (Lockinsert_UserShake)
            {
                bool boolExsitMineHeadImg = BLL_Help_01XianChangHuoDong_UserShake.Exists(strExsitWhere);
                if (boolExsitMineHeadImg)
                {

                }
                else
                {
                    EggsoftWX.Model.b018Help_01XianChangHuoDong_UserShake Model_Help_01XianChangHuoDong_UserShake = new EggsoftWX.Model.b018Help_01XianChangHuoDong_UserShake();
                    Model_Help_01XianChangHuoDong_UserShake.ShopClientID = pub_Int_ShopClientID;
                    Model_Help_01XianChangHuoDong_UserShake.UserID = pInt_QueryString_UserID;
                    Model_Help_01XianChangHuoDong_UserShake.XianChangHuoDongNum = XianChangHuoDongNum;
                    Model_Help_01XianChangHuoDong_UserShake.isReturnedToBigScreenHeadIMG = false;
                    Model_Help_01XianChangHuoDong_UserShake.UserIDHeadURL = strUserIDHeadURL;
                    Model_Help_01XianChangHuoDong_UserShake.UserIDWeiXinHeadURL = strUserIDWeiXinHeadURL;
                    Model_Help_01XianChangHuoDong_UserShake.isDoing = true;
                    Model_Help_01XianChangHuoDong_UserShake.UserIP = strpub_varUserHostAddress;
                    Model_Help_01XianChangHuoDong_UserShake.UserIDNickName = strUserIDNickName;

                    BLL_Help_01XianChangHuoDong_UserShake.Add(Model_Help_01XianChangHuoDong_UserShake);
                }
            }
        }
        private static string isEnableLock_CopyData = "isEnableLock_CopyData20160505";////复制 数据期间 不能 检测编号

        private static bool isboolCopyDataing = false;//复制 数据期间 不能 检测编号
        private static Object thisLockdoGetStatue_XianChangHuoDongAction = new Object();
        /// <summary>
        ///1100 手机端来的请求  检测 缓存 是否 存在 说 摇动 是否已经开始
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGetStatue_XianChangHuoDongAction()
        {
            string strReturn = "";
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;
                ;

                string strUserIDNickName = Eggsoft.Common.CommUtil.SafeFilter(System.Web.HttpUtility.UrlDecode(context.QueryString["UserIDNickName"], System.Text.UTF8Encoding.UTF8));
                string strUserIDHeadURL = System.Web.HttpUtility.UrlDecode(context.QueryString["UserIDHeadURL"], System.Text.UTF8Encoding.UTF8);
                string strUserIDWeiXinHeadURL = System.Web.HttpUtility.UrlDecode(context.QueryString["UserIDWeiXinHeadURL"], System.Text.UTF8Encoding.UTF8);


                string strpInt_QueryString_UserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int pInt_QueryString_UserID = 0;
                int.TryParse(strpInt_QueryString_UserID, out pInt_QueryString_UserID);

                string strpub_strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_strShopClientID, out pub_Int_ShopClientID);


                string strpub_varUserHostAddress = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["pub_varUserHostAddress"]);

                while (isboolCopyDataing)///复制 数据期间 不能 检测编号
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("doGetStatue_XianChangHuoDongAction 复制 数据期间 不能 检测编号 strUserIDNickName=" + strUserIDNickName, "01XianChangHuoDong_asmx");
                    System.Threading.Thread.Sleep(1000);
                }


                //  检测 缓存 是否 存在 说 摇动 已经开始
                // 缓存 说 是 timer 单独 控制 。一秒  去 请求一次 主服务器 。看看 活动 开始 没有。用一个 锁  进程 比较 简单
                /// 如果 开始 。就 通知（返回） 客户端，活动 开始了。活动 进入 统计 次数 阶段。就是 不断 的  更新 本 数据库 219.235.0.112
                #region 检测 缓存 是否 存在 说 摇动 已经开始
                string strJsonDataCacheHelp_01XianChangHuoDong = "";
                pHelp_01XianChangHuoDong ppHelp_01XianChangHuoDong = new pHelp_01XianChangHuoDong();

                #region  检查现在的活动编号
                string strAssemblyPath = "XianChangHuoDong" + strpub_strShopClientID;
                string CacheKey = strAssemblyPath + ".XianChangHuoDongNum";
                object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
                if (objType == null)
                {
                    try
                    {
                        lock (thisLockdoGetStatue_XianChangHuoDongAction)
                        {
                            EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_Help_01XianChangHuoDong_Main = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
                            EggsoftWX.Model.b017Help_01XianChangHuoDong_Main Model_Help_01XianChangHuoDong_Main = BLL_Help_01XianChangHuoDong_Main.GetModel("ShopClientID=" + strpub_strShopClientID + " and (XianChangHuoDongStatus=1 or XianChangHuoDongStatus=2)");//当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束
                            if (Model_Help_01XianChangHuoDong_Main != null)
                            {
                                ppHelp_01XianChangHuoDong.XianChangHuoDongNum = Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum;
                                ppHelp_01XianChangHuoDong.XianChangHuoDongStatus = Model_Help_01XianChangHuoDong_Main.XianChangHuoDongStatus;
                                strJsonDataCacheHelp_01XianChangHuoDong = Eggsoft.Common.JsonHelper.JsonSerializer<pHelp_01XianChangHuoDong>(ppHelp_01XianChangHuoDong);
                                Eggsoft.Common.DataCache.SetCache(CacheKey, strJsonDataCacheHelp_01XianChangHuoDong, System.DateTime.Now.AddSeconds(3), System.Web.Caching.Cache.NoSlidingExpiration);// 写入缓存
                            }
                            else
                            {
                              
                                string strReturnList = doGetHuoDongNumberStatusIsTrue(strpub_strShopClientID);// result.ToString();
                                Eggsoft.Common.debug_Log.Call_WriteLog("strReturnList=" + strReturnList, "01XianChangHuoDong_asmx");


                                if (strReturnList != "-1")
                                {
                                    String[] strCurReturnList = strReturnList.Split('#');
                                    ////插入 一行 数据
                                    Model_Help_01XianChangHuoDong_Main = new EggsoftWX.Model.b017Help_01XianChangHuoDong_Main();
                                    Model_Help_01XianChangHuoDong_Main.ShopClientID = int.Parse(strpub_strShopClientID);
                                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum = int.Parse(strCurReturnList[0]);
                                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_CountHowMany = int.Parse(strCurReturnList[1]);
                                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_LongTime = int.Parse(strCurReturnList[2]);
                                    Model_Help_01XianChangHuoDong_Main.MaxTracks = int.Parse(strCurReturnList[3]);
                                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongStatus = 1;
                                    BLL_Help_01XianChangHuoDong_Main.Add(Model_Help_01XianChangHuoDong_Main);

                                    Eggsoft.Common.debug_Log.Call_WriteLog("Model_Help_01XianChangHuoDong_MainJsonSerializer=" + Eggsoft.Common.JsonHelper.JsonSerializer<EggsoftWX.Model.b017Help_01XianChangHuoDong_Main>(Model_Help_01XianChangHuoDong_Main), "01XianChangHuoDong_asmx");

                                    ppHelp_01XianChangHuoDong.XianChangHuoDongNum = int.Parse(strCurReturnList[0]);
                                    ppHelp_01XianChangHuoDong.XianChangHuoDongStatus = 1;
                                    strJsonDataCacheHelp_01XianChangHuoDong = Eggsoft.Common.JsonHelper.JsonSerializer<pHelp_01XianChangHuoDong>(ppHelp_01XianChangHuoDong);
                                    Eggsoft.Common.DataCache.SetCache(CacheKey, strJsonDataCacheHelp_01XianChangHuoDong, System.DateTime.Now.AddSeconds(3), System.Web.Caching.Cache.NoSlidingExpiration);// 写入缓存
                                }
                                else
                                {
                                    strJsonDataCacheHelp_01XianChangHuoDong = "";///目前灭有打开的额活动
                                }
                            }
                        }
                    }
                    catch (Exception Exceptione)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "XianChangHuoDong");
                        strReturn = "{\"ErrorCode\":-2}";////暂无活动
                    }
                    finally
                    {

                    }
                }
                else
                {
                    strJsonDataCacheHelp_01XianChangHuoDong = (string)objType;
                }
                #endregion


                if (String.IsNullOrEmpty(strJsonDataCacheHelp_01XianChangHuoDong) == false)
                {
                    ppHelp_01XianChangHuoDong = Eggsoft.Common.JsonHelper.JsonDeserialize<pHelp_01XianChangHuoDong>(strJsonDataCacheHelp_01XianChangHuoDong);

                    insertHelp_01XianChangHuoDong_UserShake(strpub_varUserHostAddress, strUserIDNickName, strUserIDHeadURL, strUserIDWeiXinHeadURL, pInt_QueryString_UserID, pub_Int_ShopClientID, Convert.ToInt16(ppHelp_01XianChangHuoDong.XianChangHuoDongNum));

                    strReturn = "{\"ErrorCode\":0,\"XianChangHuoDongNum\":" + ppHelp_01XianChangHuoDong.XianChangHuoDongNum + ",\"XianChangHuoDongStatus\":" + ppHelp_01XianChangHuoDong.XianChangHuoDongStatus + "}";////表示ok  一旦 开始 这个 就不再调用
                }
                else
                {
                    strReturn = "{\"ErrorCode\":-2}";////暂无活动
                }
                #endregion

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "XianChangHuoDong");
                strReturn = "{\"ErrorCode\":-2}";////暂无活动
            }
            finally
            {

            }


            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }
        #endregion


        #region 圆环上加载 参与者
        private static object Lockinsert_Userdoshake_parter = new object();////复制 数据期间 不能 检测编号

        [WebMethod]
        /// <summary>
        ///1110 圆环上加载 参与者
        /// </summary>
        /// <returns></returns>
        public String doshake_parter()
        {
            string str = "";
            try
            {
                lock (Lockinsert_Userdoshake_parter)
                {
                    var response = HttpContext.Current.Response;
                    var context = HttpContext.Current.Request;

                    String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                    String strSceenXianChangHuoDongNumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["SceenXianChangHuoDongNumber"]);

                    System.Data.IDataParameter[] iData = new System.Data.SqlClient.SqlParameter[6];
                    iData[0] = new System.Data.SqlClient.SqlParameter("@ShopClientID", strShopClientID);
                    iData[1] = new System.Data.SqlClient.SqlParameter("@XianChangHuoDongNum", strSceenXianChangHuoDongNumber);
                    iData[2] = new System.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.VarChar, 400, System.Data.ParameterDirection.Output, false, 0, 0, string.Empty, System.Data.DataRowVersion.Default, null);
                    iData[3] = new System.Data.SqlClient.SqlParameter("@UserIDNickName", System.Data.SqlDbType.VarChar, 400, System.Data.ParameterDirection.Output, false, 0, 0, string.Empty, System.Data.DataRowVersion.Default, null);
                    iData[4] = new System.Data.SqlClient.SqlParameter("@UserIDHeadURL", System.Data.SqlDbType.VarChar, 400, System.Data.ParameterDirection.Output, false, 0, 0, string.Empty, System.Data.DataRowVersion.Default, null);
                    iData[5] = new System.Data.SqlClient.SqlParameter("@CountAllParterRecords", System.Data.SqlDbType.VarChar, 400, System.Data.ParameterDirection.Output, false, 0, 0, string.Empty, System.Data.DataRowVersion.Default, null);
                    string strReturn = EggsoftWX.SQLServerDAL.DbHelperSQL.RunProcedure("sp_GetHelp_01XianChangHuoDong_UserShake_RandomUser", iData).ToString();
                    string strUserID = iData[2].Value.ToString();
                    string strUserIDNickName = iData[3].Value.ToString();
                    string strUserIDHeadURL = iData[4].Value.ToString();
                    string strCountAllParterRecords = iData[5].Value.ToString();

                    int intCount = 0;
                    int.TryParse(strCountAllParterRecords, out intCount);
                    Eggsoft.Common.debug_Log.Call_WriteLog("圆环上加载 参与者 调用频度检测doshake_parter", "05XianChangHuoDong");
                    str = "{\"ret\":0,\"data\":{\"count\":" + strCountAllParterRecords + ",";

                    string strparters = "[";
                    if (intCount > 0)
                    {
                        strparters += "{\"UserID\":" + strUserID + ",\"avatar\":\"" + strUserIDHeadURL + "\",\"nick_name\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserIDNickName) + "\"}";//////js decodeURIComponent
                    }
                    strparters += "]";
                    str += "\"parters\":" + strparters;
                    str += "}}";
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
            catch (System.Threading.ThreadAbortException e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return str;
            //return intErrorCode.ToString();
        }
        #endregion 圆环上加载 参与者

        /// <summary>
        ///1120 大屏幕通知这里 活动开始聊  这里是 活动中心吗!
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doShakePhone_doshake_status_StartAction()
        {
            string strReturn = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strSceenXianChangHuoDongNumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["SceenXianChangHuoDongNumber"]);
                int pSceenXianChangHuoDongNumber = 0;
                int.TryParse(strSceenXianChangHuoDongNumber, out pSceenXianChangHuoDongNumber);

                string strstrShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strstrShopClientID, out pub_Int_ShopClientID);

                string strCountHowMany = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["CountHowMany"]);
                int pub_Int_CountHowMany = 0;
                int.TryParse(strCountHowMany, out pub_Int_CountHowMany);

                string strLongShakeTime = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["LongShakeTime"]);
                int pub_Int_LongShakeTime = 0;
                int.TryParse(strLongShakeTime, out pub_Int_LongShakeTime);

                string strMaxTracks = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["MaxTracks"]);
                int pub_Int_MaxTracks = 0;
                int.TryParse(strMaxTracks, out pub_Int_MaxTracks);

                string strvaraction = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["action"]);////正在摇动的编号

                if (strvaraction == "start")
                {
                    EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_Help_01XianChangHuoDong_Main = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
                    EggsoftWX.Model.b017Help_01XianChangHuoDong_Main Model_Help_01XianChangHuoDong_Main = BLL_Help_01XianChangHuoDong_Main.GetModel("ShopClientID=" + strstrShopClientID + " and XianChangHuoDongNum=" + strSceenXianChangHuoDongNumber);
                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongStatus = 2;
                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_BeginTime = DateTime.Now;
                    Model_Help_01XianChangHuoDong_Main.UpdateTime = DateTime.Now;
                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_CountHowMany = pub_Int_CountHowMany;
                    Model_Help_01XianChangHuoDong_Main.MaxTracks = pub_Int_MaxTracks;

                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_LongTime = pub_Int_LongShakeTime;
                    BLL_Help_01XianChangHuoDong_Main.Update(Model_Help_01XianChangHuoDong_Main);
                    strReturn = "{\"ErrorCode\":2}";////2 表示 kaishi
                }

                else
                {
                    strReturn = "{\"ErrorCode\":-1}";////2 表示 结束了
                }





            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                strReturn = "{\"ErrorCode\":-1}";////-1 表示 出错了
            }
            finally
            {

            }

            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }



        [WebMethod]
        /// <summary>
        ///1130 正在 摇的 进行排序  读取数据
        /// </summary>
        /// <returns></returns>
        public String doShakePhone_doshake_sort()
        {
            string str = "";
            try
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("调用频度检测d页面排序oshake_sort", "05XianChangHuoDong");



                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strSceenXianChangHuoDongNumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["SceenXianChangHuoDongNumber"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);

                while (isboolCopyDataing)///复制 数据期间 不能 1130 正在 摇的 进行排序  读取数据
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("doShakePhone_doshake_sort 复制 数据期间 不能 检测编号 strSceenXianChangHuoDongNumber=" + strSceenXianChangHuoDongNumber, "01XianChangHuoDong_asmx");
                    System.Threading.Thread.Sleep(1000);
                }

                EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_Help_01XianChangHuoDong_Main = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
                EggsoftWX.Model.b017Help_01XianChangHuoDong_Main Model_Help_01XianChangHuoDong_Main = BLL_Help_01XianChangHuoDong_Main.GetModel("ShopClientID=" + strShopClientID + " and XianChangHuoDongNum=" + strSceenXianChangHuoDongNumber);
                if (Model_Help_01XianChangHuoDong_Main == null)
                {
                    str = "{\"ErrorCode\":-1}";
                }
                else
                {
                    #region
                    int intMaxTracck = Convert.ToInt16(Model_Help_01XianChangHuoDong_Main.MaxTracks);

                    EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake BLL_Help_01XianChangHuoDong_UserShake = new EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake();
                    System.Data.DataTable tabUserShake = BLL_Help_01XianChangHuoDong_UserShake.GetList("top " + intMaxTracck + " UserID,UserIDNickName,UserIDHeadURL,ThisScore,ThisAllScoreShakeCount,UserIDHeadURL,UserIDWeiXinHeadURL", "ShopClientID=" + strShopClientID + " and XianChangHuoDongNum=" + strSceenXianChangHuoDongNumber + " and isDoing=1 order by ThisAllScoreShakeCount desc").Tables[0];

                    str = "{\"count\":" + tabUserShake.Rows.Count + ",";
                    bool boolUserIfuserShake600Counts = false;
                    string strplayers = "[";
                    for (int inti = 0; inti < tabUserShake.Rows.Count; inti++)
                    {
                        string strUserID = tabUserShake.Rows[inti]["UserID"].ToString();
                        string strUserIDNickName = tabUserShake.Rows[inti]["UserIDNickName"].ToString();
                        string strThisAllScoreShakeCount = tabUserShake.Rows[inti]["ThisAllScoreShakeCount"].ToString();
                        string strUserIDHeadURL = tabUserShake.Rows[inti]["UserIDHeadURL"].ToString();
                        string strUserIDWeiXinHeadURL = tabUserShake.Rows[inti]["UserIDWeiXinHeadURL"].ToString();

                        int intIfuserShake600Counts = 0;
                        int.TryParse(strThisAllScoreShakeCount, out intIfuserShake600Counts);

                        strplayers += "{\"idNum\":" + inti + ",\"UserIDid\":" + strUserID + ",\"ThisUserAllScoreShakeCount\":" + intIfuserShake600Counts + ",\"avatar\":\"" + strUserIDHeadURL + "\",\"UserIDWeiXinHeadURL\":\"" + strUserIDWeiXinHeadURL + "\",\"nick_name\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strUserIDNickName) + "\"}";///js decodeURIComponent
                        if (inti != tabUserShake.Rows.Count - 1) strplayers += ",";

                        if (intIfuserShake600Counts >= Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_CountHowMany)
                        {
                            if (boolUserIfuserShake600Counts == false)
                            {
                                if (ShakeEnd(strSceenXianChangHuoDongNumber, strShopClientID))///成功 复制 数据了。。。大屏幕 开始 和 主机交互
                                {
                                    boolUserIfuserShake600Counts = true;
                                }
                            }
                        }
                    }
                    strplayers += "]";
                    str += "\"players\":" + strplayers;
                    #endregion

                    DateTime tDateTimeNow = DateTime.Now;
                    TimeSpan ts = tDateTimeNow.Subtract(Convert.ToDateTime(Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_BeginTime)).Duration();


                    if (boolUserIfuserShake600Counts)///成功600次数到了 复制 数据了。。。大屏幕 开始 和 主机交互
                    {
                        str += ",\"ErrorCode\":" + "3";////有人 摇动聊600次
                    }
                    else if (ts.TotalSeconds >= Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_LongTime)///超时时间到辽
                    {
                        if (ShakeEnd(strSceenXianChangHuoDongNumber, strShopClientID))///成功 复制 数据了。。。大屏幕 开始 和 主机交互
                        {
                            str += ",\"ErrorCode\":" + "2";///游戏 超时
                        }
                    }
                    else
                    {
                        str += ",\"ErrorCode\":" + "0";///游戏 正常进行
                    }
                    str += "}";
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
            catch (System.Threading.ThreadAbortException e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return str;
            //return intErrorCode.ToString();
        }



        #region 返回数据到主数据库
        public class pShakePersonList
        {
            /// <summary>
            /// 用户的ID
            /// </summary>
            public int? UserID { get; set; }
            public String UserNickName { get; set; }
            /// <summary>
            /// 微信  Head  URL
            /// </summary>
            public String UserIDWeiXinHeadURL { get; set; }
            /// <summary>
            /// 用户 摇动的次数
            /// </summary>
            public int? ThisAllScoreShakeCount { get; set; }
        }

        public class pReturnObj
        {
            public int? SceenXianChangHuoDongNumber { get; set; }
            public int? shopclientID { get; set; }
            public DateTime? XianChangHuoDongNum_BeginTime { get; set; }
            public DateTime? XianChangHuoDongNum_EndTime { get; set; }
            /// <summary>
            /// 摇动的 所有人人的
            /// </summary>
            public pShakePersonList[] allShakePersonList { get; set; }

        }
        private String doGetHuoDongNumberStatusIsEnd_SendDate(string strSendData)
        {
            string strReturn = "-1";
            try
            {
                lock (isEnableLock_CopyData)
                {


                    pReturnObj mypReturnObj = Eggsoft.Common.JsonHelper.JsonDeserialize<pReturnObj>(strSendData);

                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel("XianChangHuoDongNumberbyShopClientID=" + mypReturnObj.SceenXianChangHuoDongNumber + " and ShopClientID=" + mypReturnObj.shopclientID);


                    if (Model_tab_ShopClient_XianChangHuoDong_Number == null)
                    {
                        strReturn = "-1";
                    }
                    else
                    {

                        System.Data.DataTable dt_TaskDetail = new System.Data.DataTable();
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("ID", typeof(Int32)));
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("XianChangHuoDongNumberbyShopClientID", typeof(Int32)));
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserID", typeof(Int32)));
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserNickName", typeof(string)));
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserShopClientID", typeof(Int32)));
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserShakeNumber", typeof(string)));
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("CreateTime", typeof(DateTime)));
                        dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UpdateTime", typeof(DateTime)));

                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum BLL_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum();
                        int intMAXID = BLL_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.GetMaxId();
                        System.Data.DataRow dr;

                        for (Int32 i = 0; i < mypReturnObj.allShakePersonList.Length; i++)
                        {
                            dr = dt_TaskDetail.NewRow();
                            dr["ID"] = intMAXID + i;
                            dr["UserID"] = mypReturnObj.allShakePersonList[i].UserID;
                            dr["UserNickName"] = mypReturnObj.allShakePersonList[i].UserNickName;
                            dr["UserShakeNumber"] = mypReturnObj.allShakePersonList[i].ThisAllScoreShakeCount;
                            dr["XianChangHuoDongNumberbyShopClientID"] = mypReturnObj.SceenXianChangHuoDongNumber;
                            dr["UserShopClientID"] = mypReturnObj.shopclientID;
                            dr["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            dr["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            dt_TaskDetail.Rows.Add(dr);
                        }
                        #region 批量插入数据 memo 0021
                        System.Data.SqlClient.SqlBulkCopyColumnMapping[] mapp = new System.Data.SqlClient.SqlBulkCopyColumnMapping[dt_TaskDetail.Columns.Count];
                        for (int j = 0; j < dt_TaskDetail.Columns.Count; j++)
                        {
                            mapp[j] = new System.Data.SqlClient.SqlBulkCopyColumnMapping(dt_TaskDetail.Columns[j].ColumnName, dt_TaskDetail.Columns[j].ColumnName);
                        }
                        EggsoftWX.SQLServerDAL.DbHelperSQL.MySqlBulkCopy(dt_TaskDetail, mapp, "[tab_ShopClient_XianChangHuoDong_Number_UserShakeNum]");
                        #endregion 批量插入数据 memo 0021


                        Model_tab_ShopClient_XianChangHuoDong_Number.BeginTime = mypReturnObj.XianChangHuoDongNum_BeginTime;
                        Model_tab_ShopClient_XianChangHuoDong_Number.EndTime = mypReturnObj.XianChangHuoDongNum_EndTime;
                        Model_tab_ShopClient_XianChangHuoDong_Number.UpdateTime = DateTime.Now;
                        Model_tab_ShopClient_XianChangHuoDong_Number.IsDoing = 0;
                        BLL_tab_ShopClient_XianChangHuoDong_Number.Update(Model_tab_ShopClient_XianChangHuoDong_Number);

                        strReturn = "0";

                        #region 防止 手机端的 新编号 请求 先发生
                        doGetHuoDongNumberStatusIsTrue(mypReturnObj.shopclientID.ToString());
                        /// 111111/////
                        #endregion
                    }
                }///end isEnableLock_CopyData
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return strReturn;
            //return intErrorCode.ToString();
        }

        /// <summary>
        /// 结束摇动了  复制 所有 数据 到 主机
        /// </summary>
        private bool ShakeEnd(string strSceenXianChangHuoDongNumber, string strShopClientID)
        {
            bool sendSuccess = false;//是否发送数据成功
            try
            {
                lock (isEnableLock_CopyData)
                {
                    isboolCopyDataing = true;////复制数据期间不能检测编号
                    pReturnObj myReturnObj = new pReturnObj();

                    myReturnObj.shopclientID = int.Parse(strShopClientID);
                    myReturnObj.SceenXianChangHuoDongNumber = int.Parse(strSceenXianChangHuoDongNumber);

                    EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_Help_01XianChangHuoDong_Main = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
                    EggsoftWX.Model.b017Help_01XianChangHuoDong_Main Model_Help_01XianChangHuoDong_Main = BLL_Help_01XianChangHuoDong_Main.GetModel("ShopClientID=" + strShopClientID + " and XianChangHuoDongNum=" + strSceenXianChangHuoDongNumber);

                    myReturnObj.XianChangHuoDongNum_BeginTime = Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_BeginTime;
                    myReturnObj.XianChangHuoDongNum_EndTime = DateTime.Now;
                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongNum_EndTime = myReturnObj.XianChangHuoDongNum_EndTime;
                    Model_Help_01XianChangHuoDong_Main.XianChangHuoDongStatus = 3;/////用户端的请求 交互 会进入 doGetStatue_XianChangHuoDongAction()   自动 生成新的 编号
                    BLL_Help_01XianChangHuoDong_Main.Update(Model_Help_01XianChangHuoDong_Main);

                    EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake BLL_Help_01XianChangHuoDong_UserShake = new EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake();

                    //#region 批量数据测试
                    //for (int i = 0; i < 2000; i++)
                    //{
                    //    EggsoftWX.Model.Help_01XianChangHuoDong_UserShake Model_Help_01XianChangHuoDong_UserShake = new EggsoftWX.Model.Help_01XianChangHuoDong_UserShake();
                    //    Model_Help_01XianChangHuoDong_UserShake.ShopClientID = int.Parse(strShopClientID);
                    //    Model_Help_01XianChangHuoDong_UserShake.UserID = (i + 60);
                    //    Model_Help_01XianChangHuoDong_UserShake.XianChangHuoDongNum = int.Parse(strSceenXianChangHuoDongNumber);
                    //    Model_Help_01XianChangHuoDong_UserShake.ThisAllScoreShakeCount = (i + 66);
                    //    Model_Help_01XianChangHuoDong_UserShake.UserIDNickName = i.ToString();
                    //    BLL_Help_01XianChangHuoDong_UserShake.Add(Model_Help_01XianChangHuoDong_UserShake);
                    //}
                    //#endregion 批量数据测试
                    System.Collections.ArrayList ArrayListSQL = new System.Collections.ArrayList();////update  isdoing=1


                    System.Data.DataTable tabUserShake = BLL_Help_01XianChangHuoDong_UserShake.GetList("id,UserID,ThisAllScoreShakeCount,UserIDNickName", "ShopClientID=" + strShopClientID + " and XianChangHuoDongNum=" + strSceenXianChangHuoDongNumber + " and isDoing=1 order by ID desc").Tables[0];
                    pShakePersonList[] mywillAddPersonList = new pShakePersonList[tabUserShake.Rows.Count];
                    for (int inti = 0; inti < tabUserShake.Rows.Count; inti++)
                    {
                        string strid = tabUserShake.Rows[inti]["id"].ToString();
                        string strUserID = tabUserShake.Rows[inti]["UserID"].ToString();
                        string strThisAllScoreShakeCount = tabUserShake.Rows[inti]["ThisAllScoreShakeCount"].ToString();
                        string strUserIDNickName = tabUserShake.Rows[inti]["UserIDNickName"].ToString();

                        pShakePersonList tempPerson = new pShakePersonList();
                        tempPerson.UserNickName = strUserIDNickName;
                        tempPerson.UserID = int.Parse(strUserID);
                        if (String.IsNullOrWhiteSpace(strThisAllScoreShakeCount) == false)
                        {
                            if (int.Parse(strThisAllScoreShakeCount) > 0)
                            {
                                tempPerson.ThisAllScoreShakeCount = int.Parse(strThisAllScoreShakeCount);
                            }
                        }
                        mywillAddPersonList[inti] = tempPerson;
                        string strstrSql = "update b018Help_01XianChangHuoDong_UserShake set isDoing=0 where id=" + strid;////更新状态 有利于调试
                        ArrayListSQL.Add(strstrSql.ToString());
                    }
                    myReturnObj.allShakePersonList = mywillAddPersonList;
                    string strJSON = Eggsoft.Common.JsonHelper.JsonSerializer<pReturnObj>(myReturnObj);
                    //string urlasmx = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"] + "/Pub/doWeiXianChang.asmx";
                    //string[] args = new string[1];
                    //args[0] = strJSON;// "/UpLoad/images/";
                    //object result = Eggsoft.Common.WebServiceHelper.InvokeWebService(urlasmx, "doGetHuoDongNumberStatusIsEnd_SendDate", args);
                    string strReturn = doGetHuoDongNumberStatusIsEnd_SendDate(strJSON);// result.ToString();

                    if (strReturn == "0")
                    { ///插入成功
                        EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(ArrayListSQL);///更新 当前的 状态 意味着 已经全部 转移聊
                        sendSuccess = true;
                    }
                    isboolCopyDataing = false;////复制结束（复制数据期间不能检测编号）
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);

            }
            finally
            {

            }
            return sendSuccess;
        }
        #endregion
        /// <summary>
        /// 如果 开始 。就 通知（返回） 客户端，活动 开始了。活动 进入 统计 次数 阶段。就是 不断 的  更新 本 数据库 219.235.0.112
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doShakePhone_XianChangHuoDongAction()
        {
            string strReturn = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                //http://localhost/02Wcf_HelpMachine/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_XianChangHuoDongAction?strUseid=8568&strShopClientID=1&strvarNumberISDoing=4&shakeNumsBySend=3
                //http://localhost/02Wcf_HelpMachine/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_XianChangHuoDongAction?strUseid=8568&strShopClientID=1&strvarNumberISDoing=4&shakeNumsBySend=3
                //http://localhost/02Wcf_HelpMachine/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_XianChangHuoDongAction?jsonp=0000&strUseid=8568&strShopClientID=1&strvarNumberISDoing=4&shakeNumsBySend=3
                //http://helpmachine.o2o10000.cn/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_XianChangHuoDongAction?jsonp=0000&strUseid=8568&strShopClientID=1&strvarNumberISDoing=4&shakeNumsBySend=3
                //http://localhost/02Wcf_HelpMachine/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_XianChangHuoDongAction?jsonp=0000&strUseid=8568&strShopClientID=1&strvarNumberISDoing=4&shakeNumsBySend=9999999993
                string strpInt_QueryString_UseID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int pInt_QueryString_UseID = 0;
                int.TryParse(strpInt_QueryString_UseID, out pInt_QueryString_UseID);

                string strstrShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strstrShopClientID, out pub_Int_ShopClientID);

                string strvarNumberISDoing = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strvarNumberISDoing"]);////正在摇动的编号
                int pub_Int_Number = 0;
                int.TryParse(strvarNumberISDoing, out pub_Int_Number);

                string strshakeNumsBySend = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["shakeNumsBySend"]);////手机端每0.5秒 送来一次用户摇聊多少次。0 就 不送来料
                int pub_Int_shakeNumsBySend = 0;
                int.TryParse(strshakeNumsBySend, out pub_Int_shakeNumsBySend);

                while (isboolCopyDataing)///复制 数据期间 不能 1130 正在 摇的 进行排序  读取数据
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("doShakePhone_XianChangHuoDongAction 复制 数据期间 不能 检测编号 strvarNumberISDoing=" + strvarNumberISDoing, "01XianChangHuoDong_asmx");
                    System.Threading.Thread.Sleep(1000);
                }

                //Eggsoft.Common.debug_Log.Call_WriteLog("strUseid=" + context.QueryString["strUseid"] + "  strShopClientID=" + context.QueryString["strShopClientID"] + "  strvarNumberISDoing=" + context.QueryString["strvarNumberISDoing"] + "  shakeNumsBySend=" + context.QueryString["shakeNumsBySend"] + "");

                int intXianChangHuoDongStatusStatus = getShakingNum(pub_Int_ShopClientID, pub_Int_Number);///当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束


                if (intXianChangHuoDongStatusStatus == 2)///2 表示 正在摇
                {
                    EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake BLL_Help_01XianChangHuoDong_UserShake = new EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake();
                    string strWhere = "ShopClientID=" + pub_Int_ShopClientID + " and UserID=" + pInt_QueryString_UseID + " and XianChangHuoDongNum=" + pub_Int_Number + " and isDoing=1";
                    string strThisMAXScore = "-1";///-1  就是这次没有累加数据
                    if (pub_Int_shakeNumsBySend > 0)
                    {
                        EggsoftWX.Model.b018Help_01XianChangHuoDong_UserShake Model_Help_01XianChangHuoDong_UserShake = BLL_Help_01XianChangHuoDong_UserShake.GetModel(strWhere);
                        if (Model_Help_01XianChangHuoDong_UserShake != null)
                        {
                            int? intOldCount = Model_Help_01XianChangHuoDong_UserShake.ThisAllScoreShakeCount;
                            if (intOldCount == null) intOldCount = 0;
                            Model_Help_01XianChangHuoDong_UserShake.ThisAllScoreShakeCount = intOldCount + pub_Int_shakeNumsBySend;
                            strThisMAXScore = Model_Help_01XianChangHuoDong_UserShake.ThisAllScoreShakeCount.ToString();
                            Model_Help_01XianChangHuoDong_UserShake.UpdateTime = DateTime.Now;
                            BLL_Help_01XianChangHuoDong_UserShake.Update(Model_Help_01XianChangHuoDong_UserShake);/////
                        }
                        else
                        {
                            Model_Help_01XianChangHuoDong_UserShake = new EggsoftWX.Model.b018Help_01XianChangHuoDong_UserShake();
                            strThisMAXScore = pub_Int_shakeNumsBySend.ToString();
                            Model_Help_01XianChangHuoDong_UserShake.ThisAllScoreShakeCount = pub_Int_shakeNumsBySend;
                            Model_Help_01XianChangHuoDong_UserShake.UpdateTime = DateTime.Now;
                            Model_Help_01XianChangHuoDong_UserShake.ShopClientID = pub_Int_ShopClientID;
                            Model_Help_01XianChangHuoDong_UserShake.UserID = pInt_QueryString_UseID;
                            Model_Help_01XianChangHuoDong_UserShake.isDoing = true;
                            Model_Help_01XianChangHuoDong_UserShake.XianChangHuoDongNum = pub_Int_Number;
                            BLL_Help_01XianChangHuoDong_UserShake.Add(Model_Help_01XianChangHuoDong_UserShake);////
                        }
                    }
                    strReturn = "{\"ErrorCode\":2,\"TotalShake\":" + strThisMAXScore + "}";////正在摇  计数
                    //  检测 缓存 是否 存在 说 摇动 已经开始
                    // 缓存 说 是 timer 单独 控制 。一秒  去 请求一次 主服务器 。看看 活动 开始 没有。用一个 锁  进程 比较 简单
                    /// 如果 开始 。就 通知（返回） 客户端，活动 开始了。活动 进入 统计 次数 阶段。就是 不断 的  更新 本 数据库 219.235.0.112

                }
                else
                {
                    strReturn = "{\"ErrorCode\":3}";////3 表示 结束了
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                strReturn = "{\"ErrorCode\":-1}";////-1 表示 出错了
            }
            finally
            {

            }

            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }


        /// <summary>
        ////////检测 正在摇的活动 是否 结束聊 。用 缓存 是为防止 频繁的 访问 数据库
        /// </summary>
        /// <param name="shopClientID"></param>
        /// <param name="intShakeNum"></param>
        /// <returns></returns>
        private int getShakingNum(int shopClientID, int intShakeNum)
        {
            string strNumberISDoingXianChangHuoDongStatus = "";
            //当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束
            string strAssemblyPath = "XianChangHuoDonging" + shopClientID;
            string CacheKey = strAssemblyPath + ".XianChangHuoDongNum";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_Help_01XianChangHuoDong_Main = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
                    EggsoftWX.Model.b017Help_01XianChangHuoDong_Main Model_Help_01XianChangHuoDong_Main = BLL_Help_01XianChangHuoDong_Main.GetModel("ShopClientID=" + shopClientID + " and XianChangHuoDongNum=" + intShakeNum);//当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束
                    if (Model_Help_01XianChangHuoDong_Main != null)
                    {
                        strNumberISDoingXianChangHuoDongStatus = Convert.ToInt32(Model_Help_01XianChangHuoDong_Main.XianChangHuoDongStatus).ToString();
                        Eggsoft.Common.DataCache.SetCache(CacheKey, strNumberISDoingXianChangHuoDongStatus, System.DateTime.Now.AddSeconds(2), System.Web.Caching.Cache.NoSlidingExpiration);// 写入缓存
                    }
                }
                catch { }
                // strReturn = "{\"ErrorCode\":0,\"NumberISDoing\":\"" + strNumberISDoing + "\"}";////表示ok  
            }
            else
            {//当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束
                strNumberISDoingXianChangHuoDongStatus = (string)objType;
            }
            Eggsoft.Common.debug_Log.Call_WriteLog("strNumberISDoingXianChangHuoDongStatus=" + strNumberISDoingXianChangHuoDongStatus);

            return int.Parse(strNumberISDoingXianChangHuoDongStatus);
        }
        #endregion 摇一摇


        #region 抽奖
        /// <summary>
        /// 批量插入数据 tab_ShopClient_XianChangHuoDong_Bonus_User
        /// </summary>
        /// <param name="mypReturnObj"></param>
        /// <param name="intWillReturnXianChangHuoDongBonusNumberbyShopClientID"></param>
        /// <param name="intThistab_ShopClient_XianChangHuoDongID"></param>
        /// <returns></returns>
        private bool insertBonus_User(pReturnObj mypReturnObj, int? intWillReturnXianChangHuoDongBonusNumberbyShopClientID, int intThistab_ShopClient_XianChangHuoDongID)
        {
            bool boolReturn = false;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User BLL_tab_ShopClient_XianChangHuoDong_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();

                #region 批量插入数据 memo 0021
                System.Data.DataTable dt_TaskDetail = new System.Data.DataTable();
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("ID", typeof(Int32)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("XianChangHuoDongBonusNumberbyShopClientID", typeof(Int32)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("XianChangHuoDongID", typeof(Int32)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("ShopClientID", typeof(Int32)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserID", typeof(Int32)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("ISDoing", typeof(bool)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("CreateTime", typeof(DateTime)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UpdateTime", typeof(DateTime)));

                int intMAXID = BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.GetMaxId();
                System.Data.DataRow dr;

                for (int i = 0; i < mypReturnObj.allShakePersonList.Length; i++)
                {
                    dr = dt_TaskDetail.NewRow();
                    dr["ID"] = intMAXID + i;
                    dr["XianChangHuoDongBonusNumberbyShopClientID"] = intWillReturnXianChangHuoDongBonusNumberbyShopClientID;
                    dr["XianChangHuoDongID"] = intThistab_ShopClient_XianChangHuoDongID;
                    dr["ShopClientID"] = mypReturnObj.shopclientID;
                    dr["ISDoing"] = "true";
                    dr["UserID"] = mypReturnObj.allShakePersonList[i].UserID;
                    dr["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    dr["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    dt_TaskDetail.Rows.Add(dr);
                }
                System.Data.SqlClient.SqlBulkCopyColumnMapping[] mapp = new System.Data.SqlClient.SqlBulkCopyColumnMapping[dt_TaskDetail.Columns.Count];
                for (int j = 0; j < dt_TaskDetail.Columns.Count; j++)
                {
                    mapp[j] = new System.Data.SqlClient.SqlBulkCopyColumnMapping(dt_TaskDetail.Columns[j].ColumnName, dt_TaskDetail.Columns[j].ColumnName);
                }
                EggsoftWX.SQLServerDAL.DbHelperSQL.MySqlBulkCopy(dt_TaskDetail, mapp, "[tab_ShopClient_XianChangHuoDong_Bonus_User]");
                #endregion 批量插入数据 memo 0021

                boolReturn = true;
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
                boolReturn = false;
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                boolReturn = false;
            }
            finally
            {

            }
            return boolReturn;
        }



        /// <summary>
        /// 抽奖活动即将开始，收到辅助表 送来 可以抽奖的名单。返回 本次抽奖编号
        /// </summary>
        /// <returns></returns>
        private String doGet_SendThisBonusNumber(pReturnObj mypReturnObj)
        {
            string strReturn = "-1";

            try
            {

                int? intWillReturnXianChangHuoDongBonusNumberbyShopClientID = 0;
                lock (isEnableLock_CopyData)
                {


                   /// pReturnObj mypReturnObj = Eggsoft.Common.JsonHelper.JsonDeserialize<pReturnObj>(strSendData);

                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ActivityState=1 and ShopClientID=" + mypReturnObj.shopclientID);

                    int intThistab_ShopClient_XianChangHuoDongID = 0;
                    if (Model_tab_ShopClient_XianChangHuoDong != null)////主编号 是否 还有效
                    {
                        intThistab_ShopClient_XianChangHuoDongID = Model_tab_ShopClient_XianChangHuoDong.ID;

                        ////找到 本次 抽奖 活动的 编号。
                        /// 1  如果 以前 存在 。看是否 空数据  。。
                        /// 2 空 数据 。删除 无用 的 用户 列表 。插入 本次的。返回 本数据 编号
                        /// 3  有数据 。就 新开 一个 编号 。插入 本次的。
                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus BLL_tab_ShopClient_XianChangHuoDong_Bonus = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus();
                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User BLL_tab_ShopClient_XianChangHuoDong_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();

                        System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_XianChangHuoDong_Bonus.SelectList("select top 1 id from tab_ShopClient_XianChangHuoDong_Bonus where XianChangHuoDongID=" + intThistab_ShopClient_XianChangHuoDongID + " and ShopClientID=" + mypReturnObj.shopclientID + " and ISDoing=1 order by id desc").Tables[0];
                        EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Bonus Model_tab_ShopClient_XianChangHuoDong_Bonus = null;
                        if (Data_DataTable.Rows.Count > 0)
                        {
                            string strMAxTopXianChangHuoDong_BonusID = Data_DataTable.Rows[0]["ID"].ToString();
                            Model_tab_ShopClient_XianChangHuoDong_Bonus = BLL_tab_ShopClient_XianChangHuoDong_Bonus.GetModel(Int32.Parse(strMAxTopXianChangHuoDong_BonusID));
                        }
                        if (Model_tab_ShopClient_XianChangHuoDong_Bonus == null)///2 空 数据 
                        {
                            Model_tab_ShopClient_XianChangHuoDong_Bonus = new EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Bonus();
                            Model_tab_ShopClient_XianChangHuoDong_Bonus.ShopClientID = mypReturnObj.shopclientID;
                            Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongID = intThistab_ShopClient_XianChangHuoDongID;
                            Model_tab_ShopClient_XianChangHuoDong_Bonus.CreateTime = DateTime.Now;
                            Model_tab_ShopClient_XianChangHuoDong_Bonus.ISDoing = true;
                            int inttab_ShopClient_XianChangHuoDong_BonusID = BLL_tab_ShopClient_XianChangHuoDong_Bonus.Add(Model_tab_ShopClient_XianChangHuoDong_Bonus);
                            Model_tab_ShopClient_XianChangHuoDong_Bonus = BLL_tab_ShopClient_XianChangHuoDong_Bonus.GetModel(inttab_ShopClient_XianChangHuoDong_BonusID);
                            intWillReturnXianChangHuoDongBonusNumberbyShopClientID = Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID;
                            BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.Delete("XianChangHuoDongID=" + intWillReturnXianChangHuoDongBonusNumberbyShopClientID + " and ShopClientID=" + mypReturnObj.shopclientID + " and GetBonusName is not null and ISDoing<>1");
                            #region 批量插入数据 memo 0021
                            insertBonus_User(mypReturnObj, intWillReturnXianChangHuoDongBonusNumberbyShopClientID, intThistab_ShopClient_XianChangHuoDongID);
                            #endregion 批量插入数据 memo 0021

                        }
                        else //3 4 有数据 
                        {
                            ///3   有数据 。看看 是否 有效 数据 。A 无效的 数据 都删除 。并且 使用 当前 标号
                            ///4   有数据  。                     B 数据 也有效 。要 保存 以前的数据 。。现在的 数据 都 新开
                            //string max
                            bool boolPreData = BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.Exists("GetBonusName is not null and ShopClientID=" + mypReturnObj.shopclientID + " and XianChangHuoDongBonusNumberbyShopClientID=" + Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID);
                            if (boolPreData)
                            { ///3
                                //3.1 完结以前的编号
                                //3.2 生成新的  编号 。后 插入 新的 数据

                                //第一步 3.1 完结以前的编号
                                BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.Update("ISDoing=0,UpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'", "ShopClientID=" + mypReturnObj.shopclientID + " and XianChangHuoDongBonusNumberbyShopClientID=" + Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID);
                                //第二步 3.2 生成新的  编号
                                Model_tab_ShopClient_XianChangHuoDong_Bonus = new EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Bonus();
                                Model_tab_ShopClient_XianChangHuoDong_Bonus.ShopClientID = mypReturnObj.shopclientID;
                                Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongID = intThistab_ShopClient_XianChangHuoDongID;
                                Model_tab_ShopClient_XianChangHuoDong_Bonus.CreateTime = DateTime.Now;
                                Model_tab_ShopClient_XianChangHuoDong_Bonus.ISDoing = true;
                                int inttab_ShopClient_XianChangHuoDong_BonusID = BLL_tab_ShopClient_XianChangHuoDong_Bonus.Add(Model_tab_ShopClient_XianChangHuoDong_Bonus);
                                Model_tab_ShopClient_XianChangHuoDong_Bonus = BLL_tab_ShopClient_XianChangHuoDong_Bonus.GetModel(inttab_ShopClient_XianChangHuoDong_BonusID);
                                intWillReturnXianChangHuoDongBonusNumberbyShopClientID = Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID;
                                #region 批量插入数据 memo 0021
                                ///BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.Delete("XianChangHuoDongBonusNumberbyShopClientID=" + intWillReturnXianChangHuoDongBonusNumberbyShopClientID + " and ShopClientID=" + mypReturnObj.shopclientID + " and GetBonusName is not null and ISDoing<>1");

                                insertBonus_User(mypReturnObj, intWillReturnXianChangHuoDongBonusNumberbyShopClientID, intThistab_ShopClient_XianChangHuoDongID);
                                #endregion 批量插入数据 memo 0021


                            }
                            else
                            {//4
                                BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.Delete("XianChangHuoDongBonusNumberbyShopClientID=" + Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID + " and ShopClientID=" + mypReturnObj.shopclientID);
                                //覆盖 使用 以前的数据
                                intWillReturnXianChangHuoDongBonusNumberbyShopClientID = Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID;

                                #region 批量插入数据 memo 0021
                                insertBonus_User(mypReturnObj, Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID, Convert.ToInt32(Model_tab_ShopClient_XianChangHuoDong_Bonus.XianChangHuoDongID));
                                #endregion 批量插入数据 memo 0021
                            }
                        }
                        strReturn = intWillReturnXianChangHuoDongBonusNumberbyShopClientID.ToString();
                    }
                    else
                    {
                        strReturn = "-1";
                    }


                }///end isEnableLock_CopyData
            }
            catch (System.Threading.ThreadAbortException e)
            {
                strReturn = "-2";
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
            }
            catch (Exception Exceptione)
            {
                strReturn = "-3";
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return strReturn;
        }



        [WebMethod]
        /// <summary>
        ///复制所有数据到主机，让客户端以后和主机交互。 正在 读取抽奖的成员  读取数据
        /// </summary>
        /// <returns></returns>
        public String doShakePhone_doAllLotMember()
        {
            string strReturn = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;


                string strstrShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strstrShopClientID, out pub_Int_ShopClientID);

                string strShopClient_XianChangHuoDongID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClient_XianChangHuoDongID"]);
                int pub_Int_ShopClient_XianChangHuoDongID = 0;
                int.TryParse(strShopClient_XianChangHuoDongID, out pub_Int_ShopClient_XianChangHuoDongID);

                string strSceenXianChangHuoDongNumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["SceenXianChangHuoDongNumber"]);
                int pSceenXianChangHuoDongNumber = 0;
                int.TryParse(strSceenXianChangHuoDongNumber, out pSceenXianChangHuoDongNumber);

                #region 取所有数据 到客户端   大屏幕的抽奖 是和主机交互的
                EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake BLL_Help_01XianChangHuoDong_UserShake = new EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake();
                System.Data.DataTable tabUserShake = BLL_Help_01XianChangHuoDong_UserShake.GetList("ShopClientID=" + pub_Int_ShopClientID + " and XianChangHuoDongNum=" + pSceenXianChangHuoDongNumber + " and isDoing=1").Tables[0];

                if (tabUserShake.Rows.Count == 0)
                {
                    strReturn = "{\"ErrorCode\":-4}";////人数不足 无法 启动
                }
                else
                {
                    pShakePersonList[] mywillAddPersonList = new pShakePersonList[tabUserShake.Rows.Count];
                    for (int inti = 0; inti < tabUserShake.Rows.Count; inti++)
                    {
                        string strid = tabUserShake.Rows[inti]["id"].ToString();
                        string strUserID = tabUserShake.Rows[inti]["UserID"].ToString();
                        string strUserIDNickName = tabUserShake.Rows[inti]["UserIDNickName"].ToString();
                        string strUserIDWeiXinHeadURL = tabUserShake.Rows[inti]["UserIDWeiXinHeadURL"].ToString();

                        pShakePersonList tempPerson = new pShakePersonList();
                        tempPerson.UserNickName = strUserIDNickName;
                        tempPerson.UserID = int.Parse(strUserID);
                        tempPerson.UserIDWeiXinHeadURL = strUserIDWeiXinHeadURL;
                        mywillAddPersonList[inti] = tempPerson;
                    }
                    pReturnObj mypReturnObj = new pReturnObj();
                    mypReturnObj.allShakePersonList = mywillAddPersonList;
                    mypReturnObj.shopclientID = pub_Int_ShopClientID;

                    //string strJSON = Eggsoft.Common.JsonHelper.JsonSerializer<pReturnObj>(mypReturnObj);
                    string returnstrReturn = doGet_SendThisBonusNumber(mypReturnObj);


                    //string urlasmx = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"] + "/Pub/doWeiXianChang.asmx";
                    //string[] args = new string[1];
                    //args[0] = strJSON;//
                    //object result = Eggsoft.Common.WebServiceHelper.InvokeWebService(urlasmx, "doGet_SendThisBonusNumber", args);
                    string strdoGet_SendThisBonusNumber = returnstrReturn;

                    if (strdoGet_SendThisBonusNumber != "-1" && strdoGet_SendThisBonusNumber != "-2" && strdoGet_SendThisBonusNumber != "-3")
                    {
                        strReturn = "{\"ErrorCode\":0,\"BonusNumber\":" + strdoGet_SendThisBonusNumber + "}";////
                    }
                    else
                    {
                        strReturn = "{\"ErrorCode\":" + strdoGet_SendThisBonusNumber + "}";////2 表示 kaishi
                    }
                }
                #endregion
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                strReturn = "{\"ErrorCode\":-1}";////-1 表示 出错了
            }
            finally
            {

            }

            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";

        }

        #endregion 抽奖




    }
}
