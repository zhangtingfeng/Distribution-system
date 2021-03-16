using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doWeiXianChang 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doWeiXianChang : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }





        #region 摇动结束聊  收到辅助主机发送来的 数据
        public class pShakePersonList
        {
            /// <summary>
            /// 用户的ID
            /// </summary>
            public int? UserID { get; set; }

            public String UserNickName { get; set; }

            /// <summary>
            /// 大头像
            /// </summary>
            public String HeadImageUrl { get; set; }

            /// <summary>
            /// 用户 摇动的次数
            /// </summary>
            public int? ThisAllScoreShakeCount { get; set; }
        }

        //public class pReturnObj
        //{
        //    public int? SceenXianChangHuoDongNumber { get; set; }
        //    public int? shopclientID { get; set; }
        //    public DateTime? XianChangHuoDongNum_BeginTime { get; set; }
        //    public DateTime? XianChangHuoDongNum_EndTime { get; set; }
        //    /// <summary>
        //    /// 摇动的 所有人人的
        //    /// </summary>
        //    public pShakePersonList[] allShakePersonList { get; set; }

        //}
        //public static object isEnableLock_CopyData = new object();

        //[WebMethod]
        ///// <summary>
        ///// 摇动结束聊  收到辅助主机发送来的 数据
        ///// </summary>
        ///// <returns></returns>
        //public String doGetHuoDongNumberStatusIsEnd_SendDate(string strSendData)
        //{
        //    string strReturn = "-1";
        //    try
        //    {
        //        lock (isEnableLock_CopyData)
        //        {


        //            pReturnObj mypReturnObj = Eggsoft.Common.JsonHelper.JsonDeserialize<pReturnObj>(strSendData);

        //            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
        //            EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel("XianChangHuoDongNumberbyShopClientID=" + mypReturnObj.SceenXianChangHuoDongNumber + " and ShopClientID=" + mypReturnObj.shopclientID);


        //            if (Model_tab_ShopClient_XianChangHuoDong_Number == null)
        //            {
        //                strReturn = "-1";
        //            }
        //            else
        //            {

        //                System.Data.DataTable dt_TaskDetail = new System.Data.DataTable();
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("ID", typeof(int)));
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("XianChangHuoDongNumberbyShopClientID", typeof(Int16)));
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserID", typeof(Int16)));
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserNickName", typeof(string)));
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserShopClientID", typeof(int)));
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserShakeNumber", typeof(string)));
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("CreateTime", typeof(DateTime)));
        //                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UpdateTime", typeof(DateTime)));

        //                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum BLL_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum();
        //                int intMAXID = BLL_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.GetMaxId();
        //                System.Data.DataRow dr;

        //                for (int i = 0; i < mypReturnObj.allShakePersonList.Length; i++)
        //                {
        //                    dr = dt_TaskDetail.NewRow();
        //                    dr["ID"] = intMAXID + i;
        //                    dr["UserID"] = mypReturnObj.allShakePersonList[i].UserID;
        //                    dr["UserNickName"] = mypReturnObj.allShakePersonList[i].UserNickName;
        //                    dr["UserShakeNumber"] = mypReturnObj.allShakePersonList[i].ThisAllScoreShakeCount;
        //                    dr["XianChangHuoDongNumberbyShopClientID"] = mypReturnObj.SceenXianChangHuoDongNumber;
        //                    dr["UserShopClientID"] = mypReturnObj.shopclientID;
        //                    dr["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //                    dr["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //                    dt_TaskDetail.Rows.Add(dr);
        //                }
        //                #region 批量插入数据 memo 0021
        //                System.Data.SqlClient.SqlBulkCopyColumnMapping[] mapp = new System.Data.SqlClient.SqlBulkCopyColumnMapping[dt_TaskDetail.Columns.Count];
        //                for (int j = 0; j < dt_TaskDetail.Columns.Count; j++)
        //                {
        //                    mapp[j] = new System.Data.SqlClient.SqlBulkCopyColumnMapping(dt_TaskDetail.Columns[j].ColumnName, dt_TaskDetail.Columns[j].ColumnName);
        //                }
        //                EggsoftWX.SQLServerDAL.DbHelperSQL.MySqlBulkCopy(dt_TaskDetail, mapp, "[tab_ShopClient_XianChangHuoDong_Number_UserShakeNum]");
        //                #endregion 批量插入数据 memo 0021


        //                Model_tab_ShopClient_XianChangHuoDong_Number.BeginTime = mypReturnObj.XianChangHuoDongNum_BeginTime;
        //                Model_tab_ShopClient_XianChangHuoDong_Number.EndTime = mypReturnObj.XianChangHuoDongNum_EndTime;
        //                Model_tab_ShopClient_XianChangHuoDong_Number.UpdateTime = DateTime.Now;
        //                Model_tab_ShopClient_XianChangHuoDong_Number.IsDoing = 0;
        //                BLL_tab_ShopClient_XianChangHuoDong_Number.Update(Model_tab_ShopClient_XianChangHuoDong_Number);

        //                strReturn = "0";

        //                #region 防止 手机端的 新编号 请求 先发生
        //                doGetHuoDongNumberStatusIsTrue(mypReturnObj.shopclientID.ToString());
        //                /// 111111/////
        //                #endregion
        //            }
        //        }///end isEnableLock_CopyData
        //    }
        //    catch (System.Threading.ThreadAbortException e)
        //    {
        //        Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
        //    }
        //    catch (Exception Exceptione)
        //    {
        //        Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
        //    }
        //    finally
        //    {

        //    }
        //    return strReturn;
        //    //return intErrorCode.ToString();
        //}
        #endregion 摇动结束聊  收到辅助主机发送来的 数据



        #region 校验摇一摇 校验用户名 密码
        [WebMethod]
        /// <summary>
        /// 校验用户名 密码 
        /// </summary>
        /// <returns></returns>
        public String doverify()
        {
            string str = "";
            try
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("调用频度检测doverify摇一摇 用户名 密码 登陆", "05XianChangHuoDong");

                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strSceenXianChangHuoDongNumber = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["SceenXianChangHuoDongNumber"]);
                String strpassword = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["password"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                string strReturnData = "";
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel("XianChangHuoDongNumberbyShopClientID=" + strSceenXianChangHuoDongNumber + " and ShopClientID=" + strShopClientID);
                if (Model_tab_ShopClient_XianChangHuoDong_Number != null)
                {
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ID=" + Model_tab_ShopClient_XianChangHuoDong_Number.XianChangHuoDongID + " and ShopClientID=" + strShopClientID);

                    if (Model_tab_ShopClient_XianChangHuoDong != null)
                    {
                        if (Model_tab_ShopClient_XianChangHuoDong.ActivityState != true)
                        {
                            strReturnData = "-3";///当前 活动已经停止
                        }
                        else if (Model_tab_ShopClient_XianChangHuoDong.PassWord != strpassword)
                        {
                            strReturnData = "-4";///大屏幕 密码错误
                        }
                        else
                        {
                            strReturnData = "0";/// 可以登录
                        }
                    }
                    else
                    {
                        strReturnData = "-2";
                    }
                }
                else
                {
                    strReturnData = "-2";
                }



                str = "{\"ret\":0,\"data\":" + strReturnData + "";
                str += "}";
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
        #endregion



        #region 校验抽奖 校验用户名 密码
        [WebMethod]
        /// <summary>
        /// 校验用户名 密码 
        /// </summary>
        /// <returns></returns>
        public String doverify_ChouJiang()
        {
            string str = "";
            try
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("调用频度检测doverify_ChouJiang 用户名 密码 登陆", "05XianChangHuoDong");

                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strShopClient_XianChangHuoDongID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClient_XianChangHuoDongID"]);
                String strpassword = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["password"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                string strReturnData = "";
                //EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                //EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel("XianChangHuoDongNumberbyShopClientID=" + strSceenXianChangHuoDongNumber + " and ShopClientID=" + strShopClientID);
                //if (Model_tab_ShopClient_XianChangHuoDong_Number != null)
                //{
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ID=" + strShopClient_XianChangHuoDongID + " and ShopClientID=" + strShopClientID);

                if (Model_tab_ShopClient_XianChangHuoDong != null)
                {
                    if (Model_tab_ShopClient_XianChangHuoDong.ActivityState != true)
                    {
                        strReturnData = "-3";///当前 活动已经停止
                    }
                    else if (Model_tab_ShopClient_XianChangHuoDong.PassWord != strpassword)
                    {
                        strReturnData = "-4";///大屏幕 密码错误
                    }
                    else
                    {
                        strReturnData = "0";/// 可以登录
                    }
                }
                else
                {
                    strReturnData = "-2";
                }
                //}
                //else
                //{
                //    strReturnData = "-2";
                //}



                str = "{\"ret\":0,\"data\":" + strReturnData + "";
                str += "}";
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
        #endregion



        #region 抽奖活动即将开始，收到辅助主机 送来 可以抽奖的名单。返回 本次抽奖编号
        /// <summary>
        /// 批量插入数据 tab_ShopClient_XianChangHuoDong_Bonus_User
        /// </summary>
        /// <param name="mypReturnObj"></param>
        /// <param name="intWillReturnXianChangHuoDongBonusNumberbyShopClientID"></param>
        /// <param name="intThistab_ShopClient_XianChangHuoDongID"></param>
        /// <returns></returns>
        /*
        private bool insertBonus_User(pReturnObj mypReturnObj, int? intWillReturnXianChangHuoDongBonusNumberbyShopClientID, int intThistab_ShopClient_XianChangHuoDongID)
        {
            bool boolReturn = false;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User BLL_tab_ShopClient_XianChangHuoDong_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();

                #region 批量插入数据 memo 0021
                System.Data.DataTable dt_TaskDetail = new System.Data.DataTable();
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("ID", typeof(int)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("XianChangHuoDongBonusNumberbyShopClientID", typeof(Int16)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("XianChangHuoDongID", typeof(Int16)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("ShopClientID", typeof(int)));
                dt_TaskDetail.Columns.Add(new System.Data.DataColumn("UserID", typeof(Int16)));
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
        }*/
        [WebMethod]
       

        #endregion


        #region 处理主机的 抽奖 交互
        private pShakePersonList[] doGet_ValidUserList_pShakePersonList(string strShopClient_XianChangHuoDongID, string strShopClientID, string strBonusNumberByShopClientID)
        {
            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
            EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ID=" + strShopClient_XianChangHuoDongID + " and ShopClientID=" + strShopClientID);



            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User BLL_tab_ShopClient_XianChangHuoDong_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();
            string strWhere = "";
            strWhere += " SELECT   tab_User.ID, tab_User.NickName, tab_User.HeadImageUrl, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.XianChangHuoDongBonusNumberbyShopClientID, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.XianChangHuoDongID, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.ShopClientID, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.GetBonusName, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.UserID, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.ISDoing, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.CreateTime, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.UpdateTime, ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.ID AS XianChangHuoDong_Bonus_User_ID";
            strWhere += " FROM      tab_ShopClient_XianChangHuoDong_Bonus_User LEFT OUTER JOIN";
            strWhere += " tab_User ON tab_ShopClient_XianChangHuoDong_Bonus_User.ShopClientID = tab_User.ShopClientID AND ";
            strWhere += " tab_ShopClient_XianChangHuoDong_Bonus_User.UserID = tab_User.ID";


            if (Model_tab_ShopClient_XianChangHuoDong.GetBonusRepeat == true)////直接返回 列表
            {
                strWhere += " WHERE   (tab_ShopClient_XianChangHuoDong_Bonus_User.ShopClientID = " + strShopClientID + " and XianChangHuoDongBonusNumberbyShopClientID=" + strBonusNumberByShopClientID + " and XianChangHuoDongID=" + strShopClient_XianChangHuoDongID + " and ISDoing=1)";

            }
            else if (Model_tab_ShopClient_XianChangHuoDong.GetBonusRepeat == false)////不允许重复 中奖
            {

                strWhere += " WHERE   (tab_ShopClient_XianChangHuoDong_Bonus_User.ShopClientID = " + strShopClientID + " and XianChangHuoDongBonusNumberbyShopClientID=" + strBonusNumberByShopClientID + " and XianChangHuoDongID=" + strShopClient_XianChangHuoDongID + " and GetBonusName is  null  and ISDoing=1)";
            }

            strWhere += " order by tab_ShopClient_XianChangHuoDong_Bonus_User.ID asc";

            System.Data.DataTable XianChangHuoDong_Bonus_UserList = BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.SelectList(strWhere).Tables[0];

            pShakePersonList[] allShakePersonList = new pShakePersonList[XianChangHuoDong_Bonus_UserList.Rows.Count];
            for (int i = 0; i < XianChangHuoDong_Bonus_UserList.Rows.Count; i++)
            {
                pShakePersonList mypShakePersonList = new pShakePersonList();
                mypShakePersonList.UserID = Int32.Parse(XianChangHuoDong_Bonus_UserList.Rows[i]["ID"].ToString());
                mypShakePersonList.HeadImageUrl = XianChangHuoDong_Bonus_UserList.Rows[i]["HeadImageUrl"].ToString();
                mypShakePersonList.UserNickName = XianChangHuoDong_Bonus_UserList.Rows[i]["NickName"].ToString();
                allShakePersonList[i] = mypShakePersonList;
            }

            return allShakePersonList;
        }
        [WebMethod]
        /// <summary>
        /// 获取可抽奖 列表  ///和主机 交互 获取 可参与 人数   扣除 重复中奖的 名单、、、根本就 不能 参与抽奖
        /// </summary>
        /// <returns></returns>
        public String doGet_ValidUserList()
        {
            string str = "";
            try
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("调用频度检测doverify_ChouJiang 用户名 密码 登陆", "05XianChangHuoDong");

                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strBonusNumberByShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["BonusNumberByShopClientID"]);
                String strShopClient_XianChangHuoDongID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClient_XianChangHuoDongID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                string strReturnData = "";
               
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ID=" + strShopClient_XianChangHuoDongID + " and ShopClientID=" + strShopClientID);

                if (Model_tab_ShopClient_XianChangHuoDong != null)
                {
                    pShakePersonList[] allShakePersonList = doGet_ValidUserList_pShakePersonList(strShopClient_XianChangHuoDongID, strShopClientID, strBonusNumberByShopClientID);
                    strReturnData = Eggsoft.Common.JsonHelper.JsonSerializer<pShakePersonList[]>(allShakePersonList);
                    str = "{\"ErrorCode\":0,\"data\":" + strReturnData + "";
                    str += "}";
                }
                else
                {
                    str = "{\"ErrorCode\":-2";
                    str += "}";
                }

                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    Response.Charset = "utf-8";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    Response.ContentType = ("application/json;charset=UTF-8");
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


        }



        [WebMethod]
        /// <summary>
        /// 预先设定中奖用户，前端只是随便 转转 中奖用户
        /// <returns></returns>
        public String doGet_ValidUser_lottery_luckyList()
        {
            string str = "";
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strBonusNumberByShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["BonusNumberByShopClientID"]);
                String strShopClient_XianChangHuoDongID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClient_XianChangHuoDongID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strwiner_count = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["winer_countnum"]);

                pShakePersonList[] allShakePersonList = doGet_ValidUserList_pShakePersonList(strShopClient_XianChangHuoDongID, strShopClientID, strBonusNumberByShopClientID);
                #region 取随机数组
                Random ran = new Random();
                int k = 0;
                pShakePersonList strtmp = null;
                for (int i = 0; i < allShakePersonList.Length; i++)
                {

                    k = ran.Next(0, allShakePersonList.Length);
                    if (k != i)
                    {
                        strtmp = allShakePersonList[i];
                        allShakePersonList[i] = allShakePersonList[k];
                        allShakePersonList[k] = strtmp;
                    }
                }

                pShakePersonList[] ReturnShakePersonList = new pShakePersonList[Int32.Parse(strwiner_count)];
                for (int h = 0; h < Int32.Parse(strwiner_count); h++)
                {
                    ReturnShakePersonList[h] = allShakePersonList[h];
                }
                #endregion 取随机数组

                string strReturnData = "";
                strReturnData = Eggsoft.Common.JsonHelper.JsonSerializer<pShakePersonList[]>(ReturnShakePersonList);
                str = "{\"ErrorCode\":0,\"data\":" + strReturnData + "";
                str += "}";

                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    Response.Charset = "utf-8";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    Response.ContentType = ("application/json;charset=UTF-8");
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
        }

        /// String strShopName = System.Web.HttpUtility.UrlDecode(context.QueryString["ShopName"], System.Text.UTF8Encoding.UTF8);

        [WebMethod]
        /// <summary>
        /// 得到真正的 中奖 用户
        /// <returns></returns>
        public String dotellServerWiner_luckyList()
        {
            string str = "";
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;
                context.ContentEncoding = System.Text.Encoding.UTF8;

                String strBonusNumberByShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["BonusNumberByShopClientID"]);
                String strShopClient_XianChangHuoDongID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClient_XianChangHuoDongID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String stringUserIDList = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserIDList"]);
                String strselectedIndex = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["selectedIndex"]);
                //String strselectedtext = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["selectedtext"]);
                //String strselectedtext = System.Web.HttpUtility.UrlDecode(context.QueryString["selectedtext"], System.Text.UTF8Encoding.UTF8);
                String strselectedtext = (context.QueryString["selectedtext"]);

                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User BLL_tab_ShopClient_XianChangHuoDong_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();

                string[] strBonusList = stringUserIDList.Split('#');
                for (int i = 0; i < strBonusList.Length; i++)
                {
                    string struserID = strBonusList[i];

                    String strUpdateWhereItem = "GetBonusName='" + strselectedtext + "',UpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                    String strUpdateWhere = "XianChangHuoDongBonusNumberbyShopClientID=" + strBonusNumberByShopClientID + "";
                    strUpdateWhere += " and XianChangHuoDongID=" + strShopClient_XianChangHuoDongID;
                    strUpdateWhere += " and ShopClientID=" + strShopClientID;
                    strUpdateWhere += " and UserID=" + struserID;

                    BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.Update(strUpdateWhereItem, strUpdateWhere);
                }

                str = "{\"ErrorCode\":0";
                str += "}";

                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    Response.Charset = "utf-8";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    Response.ContentType = ("application/json;charset=UTF-8");
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
        }


        #endregion
    }
}
