using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using Xfrog.Net;

namespace _14WcfService1.Other.CheckCode
{
    /// <summary>
    /// WSCheck 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WSCheck : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region 发送短信验证码 
        private static object GameInfo_SendPhoneCode = new Object();
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_SendPhoneCode()
        {
            string str = "";

            try
            {
                lock (GameInfo_SendPhoneCode)
                {
                    var response = HttpContext.Current.Response;
                    var context = HttpContext.Current.Request;
                    bool boolIFSend = false;
                    //string strCheckCodeGuid = (context.QueryString["CheckCodeGuid"]);
                    string strPhoneNum = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["PhoneNum"]);
                    string strinnerIP = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["innerIP"]);


                    string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                    Eggsoft.Common.debug_Log.Call_WriteLog("开始 strPhoneNum=" + strPhoneNum + " strShopClientID=" + strShopClientID, "验证码", "收到验证码请求");


                    #region 发送短信
                    int intShopClientID = 0;
                    int.TryParse(strShopClientID, out intShopClientID);
                    string strIP = context.UserHostAddress;///System.Web.HttpContext.Current.Request.UserHostAddress; 这样获取IP是没问题的。   一个IP限制每分钟100个

                    EggsoftWX.BLL.b001_Phone_Message__CheckCode BLL_b001_Phone_Message__CheckCode = new EggsoftWX.BLL.b001_Phone_Message__CheckCode();
                    //EggsoftWX.Model.b001_Phone_Message__CheckCode Model_b001_Phone_Message__CheckCode = BLL_b001_Phone_Message__CheckCode.GetModel("CheckCodeGuid='" + strCheckCodeGuid + "'");

                    ////总发送次数 每分钟 10次。   每天不超过 总数 50次。每个IP 限制 总数50次 每天。
                    int intCountEverySecond = 10;//总发送次数 每秒 10次。
                    int intCountEveryDay = 100;///每天不超过 总数 100次。
                    int intCountEveryIPEveryHour = 100;//每个IP 限制 总数100次 每小时。
                    int intCountEveryPhoneEveryThreeHour = 1;//每个手机号 60*3 每3分钟限制一个数 每3小时限制个数 提示三小时都有效   3600*3 3小时   

                    string strSQLCountEverySecond = "select count(*) from b001_Phone_Message__CheckCode where DATEDIFF(ss, CreatTime, getdate()) <= 1";
                    string strSQLCountEveryDay = "select count(*) from b001_Phone_Message__CheckCode where DATEDIFF(ss, CreatTime, getdate()) <= 3600*24";
                    string strSQLCountEveryIP = "select count(*) from b001_Phone_Message__CheckCode where ip='" + strIP + "' and  DATEDIFF(ss, CreatTime, getdate()) <= 3600";
                    string strSQLCountEveryPhoneEveryThreeHour = "select count(*) from b001_Phone_Message__CheckCode where SendPhoneNum='" + strPhoneNum + "' and  DATEDIFF(ss, CreatTime, getdate()) <= 60*3";//每三分钟

                    string stringCountEverySecond = BLL_b001_Phone_Message__CheckCode.SelectList(strSQLCountEverySecond).Tables[0].Rows[0][0].ToString();
                    string stringCountEveryDay = BLL_b001_Phone_Message__CheckCode.SelectList(strSQLCountEveryDay).Tables[0].Rows[0][0].ToString();
                    string stringCountEveryIPEveryHour = BLL_b001_Phone_Message__CheckCode.SelectList(strSQLCountEveryIP).Tables[0].Rows[0][0].ToString();
                    string stringCountEveryPhoneEveryThreeHour = BLL_b001_Phone_Message__CheckCode.SelectList(strSQLCountEveryPhoneEveryThreeHour).Tables[0].Rows[0][0].ToString();

                    bool boolCountEverySecond = Int16.Parse(stringCountEverySecond) <= intCountEverySecond;
                    bool boolCountEveryDay = Int16.Parse(stringCountEveryDay) <= intCountEveryDay;
                    bool boolCountEveryIPEveryHour = Int16.Parse(stringCountEveryIPEveryHour) <= intCountEveryIPEveryHour;
                    bool boolCountEveryPhoneEveryHour = Int16.Parse(stringCountEveryPhoneEveryThreeHour) <= intCountEveryPhoneEveryThreeHour;
                    #region 判断3分钟今天是否发送过                 
                    string strOldCheckCode = "";
                    if (boolCountEveryPhoneEveryHour == false)
                    {
                        string strSQLCheckCode = "select CheckCode from b001_Phone_Message__CheckCode where SendPhoneNum='" + strPhoneNum + "' and  DATEDIFF(ss, CreatTime, getdate()) <= 3600*3 order by SendTime desc ";
                        System.Data.DataTable DataTable_CheckCode = BLL_b001_Phone_Message__CheckCode.SelectList(strSQLCheckCode).Tables[0];
                        if (DataTable_CheckCode.Rows.Count > 0)
                        {
                            strOldCheckCode = DataTable_CheckCode.Rows[0]["CheckCode"].ToString();
                        }
                    }
                    #endregion

                    if ((boolCountEveryPhoneEveryHour == false) && (string.IsNullOrEmpty(strOldCheckCode) == false))
                    {
                        str = "{\"ErrorCode\":2,\"SendCheckCode\":\"" + strOldCheckCode + "\"}";////短信验证码 已经发送过了
                    }
                    else if (boolCountEverySecond && boolCountEveryDay && boolCountEveryIPEveryHour)
                    {//可以发送 
                        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
                        if (string.IsNullOrEmpty(Model_tab_ShopClient.TPL_ID))
                        {
                            str = "{\"ErrorCode\":-6}";////成功失败   可能是短信模板尚未配置
                        }
                        else if (Convert.ToDouble(Model_tab_ShopClient.AuthorMoney) >= 0.1)
                        {
                            string strSendCode = GenerateCheckCode();
                            EggsoftWX.Model.b001_Phone_Message__CheckCode Model_b001_Phone_Message__CheckCode = new EggsoftWX.Model.b001_Phone_Message__CheckCode();
                            Model_b001_Phone_Message__CheckCode.CreatTime = DateTime.Now;
                            Model_b001_Phone_Message__CheckCode.CheckCode = strSendCode;
                            Model_b001_Phone_Message__CheckCode.IP = strIP;
                            Model_b001_Phone_Message__CheckCode.innerIP = strinnerIP;
                            Model_b001_Phone_Message__CheckCode.IPDetailDesc = getIpDetailDesc(strIP);
                            Model_b001_Phone_Message__CheckCode.SendPhoneNum = strPhoneNum;
                            string sendConent = ""; string strErrorContent = "";
                            boolIFSend = SendAction(Model_tab_ShopClient.TPL_ID, strPhoneNum, strSendCode, Model_tab_ShopClient.ShopClientName, out sendConent, out strErrorContent); ///正常运营
                            //boolIFSend = true;///调试
                            if (boolIFSend)
                            {
                                Model_b001_Phone_Message__CheckCode.consumeMoney = (Decimal)0.1;
                                Model_tab_ShopClient.AuthorMoney = Model_tab_ShopClient.AuthorMoney - Model_b001_Phone_Message__CheckCode.consumeMoney;
                                BLL_tab_ShopClient.Update(Model_tab_ShopClient);
                                Model_b001_Phone_Message__CheckCode.AuthorMoney = Model_tab_ShopClient.AuthorMoney;
                                Model_b001_Phone_Message__CheckCode.MessageContent = sendConent + strErrorContent;
                                Model_b001_Phone_Message__CheckCode.SendTime = DateTime.Now;
                                Model_b001_Phone_Message__CheckCode.SendType = 1;
                                Model_b001_Phone_Message__CheckCode.ShopClientID = intShopClientID;
                                Int32 int32Num = BLL_b001_Phone_Message__CheckCode.Add(Model_b001_Phone_Message__CheckCode);
                                Eggsoft.Common.debug_Log.Call_WriteLog(Model_b001_Phone_Message__CheckCode.IPDetailDesc, "验证码", strPhoneNum);
                                Eggsoft.Common.debug_Log.Call_WriteLog(sendConent + strErrorContent, "验证码", strPhoneNum);
                                str = "{\"ErrorCode\":1,\"SendCheckCode\":\"" + strSendCode + "\"}";////成功发送
                            }
                            else
                            {
                                str = "{\"ErrorCode\":-3}";////成功失败   可能是短信模板尚未配置
                            }
                        }
                        else
                        {
                            str = "{\"ErrorCode\":-5}";////余额不足
                        }
                    }
                    else
                    {
                        if (boolCountEverySecond == false) Eggsoft.Common.debug_Log.Call_WriteLog("攻击性" + strSQLCountEverySecond, "攻击性访问");
                        if (boolCountEveryDay == false) Eggsoft.Common.debug_Log.Call_WriteLog("攻击性" + strSQLCountEveryDay, "攻击性访问");
                        if (boolCountEveryIPEveryHour == false) Eggsoft.Common.debug_Log.Call_WriteLog("攻击性" + strSQLCountEveryIP, "攻击性访问");
                        Eggsoft.Common.debug_Log.Call_WriteLog("攻击性", "攻击性访问");

                        str = "{\"ErrorCode\":-4,\"ReturnSendCode\":false}";////攻击 拒绝服务
                    }



                    if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(str, "验证码", "验证码服务返回");

                        HttpRequest Request = HttpContext.Current.Request;
                        HttpResponse Response = HttpContext.Current.Response;
                        string callback = Request["jsonp"];
                        Response.Write(callback + "(" + str + ")");
                        Response.End();//结束后续的操作，直接返回所需要的字符串
                    }
                    #endregion 发送短信
                }

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "验证码线程");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "验证码");
            }
            finally
            {

            }
            Eggsoft.Common.debug_Log.Call_WriteLog(str, "验证码", "返回数据");
            return str;
        }

        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                code = (char)('0' + (char)(number % 10));

                checkCode += code.ToString();
            }

            return checkCode;
        }

        #region 发送短信 Action
        private bool SendAction(string strTPL_ID, string strPhoneNum, string strSendPhoneCode, string strCompany, out string sendConent, out string strContentError)
        {
            if (string.IsNullOrEmpty(strTPL_ID))
            {
                sendConent = "短信模板尚未配置";
                strContentError = "短信模板尚未配置";
                return false;
            }

            string appkey = "979a7fda49a4436aaec66cc1d1664613"; //配置您申请的appkey
                                                                ////1.屏蔽词检查测
                                                                //string url1 = "http://v.juhe.cn/sms/black";

            //var parameters1 = new Dictionary<string, string>();

            //parameters1.Add("word", "【企业移动平台】欢迎中奖彩票政府警察使用#app#，您的手机验证码是1224。本条信息无需回复"); //需要检测的短信内容，需要UTF8 URLENCODE
            //parameters1.Add("key", appkey);//你申请的key

            //string result1 = sendPost(url1, parameters1, "get");

            //JsonObject newObj1 = new JsonObject(result1);
            //String errorCode1 = newObj1["error_code"].Value;

            //if (errorCode1 == "0")
            //{
            //    //Debug.WriteLine("成功");
            //    //Debug.WriteLine(newObj1);
            //}
            //else
            //{
            //    //Debug.WriteLine("失败");
            //    //Debug.WriteLine(newObj1["error_code"].Value + ":" + newObj1["reason"].Value);
            //}


            //2.发送短信
            string url2 = "http://v.juhe.cn/sms/send";

            var parameters2 = new Dictionary<string, string>();

            parameters2.Add("mobile", strPhoneNum); //接收短信的手机号码
            parameters2.Add("tpl_id", strTPL_ID); //短信模板ID，请参考个人中心短信模板设置
            parameters2.Add("tpl_value", "#code#=" + strSendPhoneCode + "&#unitcom#=" + strCompany + ""); //变量名和变量值对。如果你的变量名或者变量值中带有#&amp;=中的任意一个特殊符号，请先分别进行urlencode编码后再传递，&lt;a href=&quot;http://www.juhe.cn/news/index/id/50&quot; target=&quot;_blank&quot;&gt;详细说明&gt;&lt;/a&gt;
            parameters2.Add("key", appkey);//你申请的key
            parameters2.Add("dtype", "json"); //返回数据的格式,xml或json，默认json

            string result2 = sendPost(url2, parameters2, "get");

            JsonObject newObj2 = new JsonObject(result2);
            String errorCode2 = newObj2["error_code"].Value;

            sendConent = result2;
            strContentError = errorCode2;
            if (errorCode2 == "0")
            {
                return true;
                //strContentError = errorCode2;
                //Debug.WriteLine("成功");
                //Debug.WriteLine(newObj2);
            }
            else
            {
                strContentError = newObj2["error_code"].Value + ":" + newObj2["reason"].Value;
                return false;
                //Debug.WriteLine("失败");
                //Debug.WriteLine(newObj2["error_code"].Value + ":" + newObj2["reason"].Value);
            }


        }

        /// <summary>
                /// Http (GET/POST)
                /// </summary>
                /// <param name="url">请求URL</param>
                /// <param name="parameters">请求参数</param>
                /// <param name="method">请求方法</param>
                /// <returns>响应内容</returns>
        string sendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 5000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }

        /// <summary>
                /// 组装普通文本请求参数。
                /// </summary>
                /// <param name="parameters">Key-Value形式请求参数字典</param>
                /// <returns>URL编码后的请求数据</returns>
        string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
                /// 把响应流转换为文本。
                /// </summary>
                /// <param name="rsp">响应流对象</param>
                /// <param name="encoding">编码方式</param>
                /// <returns>响应文本</returns>
        string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }
        #endregion 发送短信 Action


        #region 查询用户的IP地址
        private String getIpDetailDesc(string strIP)
        {
            string appkey = "6f238b1adcd250e5bb2d780604c1d29d"; //配置您申请的appkey


            //1.根据IP/域名查询地址
            string url1 = "http://apis.juhe.cn/ip/ip2addr";

            var parameters1 = new Dictionary<string, string>();

            parameters1.Add("ip", strIP); //需要查询的IP地址或域名
            parameters1.Add("key", appkey);//你申请的key
            parameters1.Add("dtype", "json"); //返回数据的格式,xml或json，默认json
            string result1 = sendPost(url1, parameters1, "get");
            JsonObject newObj1 = new JsonObject(result1);
            //String errorCode1 = newObj1["error_code"].Value;
            return result1;
        }
        #endregion
        #endregion 发送短信验证码 
        #region 注册用户
        private static object mydddCheckPhoneCode_Register = new object();
        /// <summary>
        /// 核对短信验证码 并注册用户
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_CheckPhoneCode_Register()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                string strUserCheckCode = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["RegsiterCode"]);
                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                string strPhoneNum = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["PhoneNum"]);
                string strPassword = (context.QueryString["userpwd"]);
                string strRecommandPhone = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["RecommandPhone"]);
                //string strOpenID = (context.QueryString["OpenID"]);

                Eggsoft.Common.debug_Log.Call_WriteLog("开始 strUserID=" + strUserID + " strPhoneNum=" + strPhoneNum + " strShopClientID=" + strShopClientID + " strRecommandPhone=" + strRecommandPhone + " strPassword=" + strPassword + " strUserCheckCode=" + strUserCheckCode, "注册用户");

                EggsoftWX.BLL.b001_Phone_Message__CheckCode BLL_b001_Phone_Message__CheckCode = new EggsoftWX.BLL.b001_Phone_Message__CheckCode();
                string strSelectWhere = "select CheckCode from b001_Phone_Message__CheckCode where SendPhoneNum='" + strPhoneNum + "' and CheckCode='" + strUserCheckCode + "' and ShopClientID=" + strShopClientID + " and DATEDIFF(ss,SendTime,getdate())<=24*60*60 order by SendTime desc";
                System.Data.DataTable Data_DataTableCheckCode = BLL_b001_Phone_Message__CheckCode.SelectList(strSelectWhere).Tables[0];
                if (Data_DataTableCheckCode.Rows.Count > 0)
                {
                    #region 注册用户
                    lock (mydddCheckPhoneCode_Register)
                    {
                        int? intIFModifyParent = 0;///分销所得优先给予第一人还是给予最新的转发人
                        int intRecommandPhone = 3;
                        int intUserID = 0;
                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Convert.ToInt32(strUserID));
                        if (Model_tab_User == null)
                        {
                            bool boolExsit = BLL_tab_User.Exists("UserAccount='" + strPhoneNum + "' and ID=" + strUserID + " and ShopClientID=" + strShopClientID);
                            if (boolExsit)
                            {
                                Eggsoft.Common.debug_Log.Call_WriteLog("开始 strPhoneNum=" + strPhoneNum + "已是注册 用户 。密码 已重置", "注册用户");


                                ////已是注册 用户 。密码 已重置。
                                str = "{\"ErrorCode\":2}";////
                                Model_tab_User = BLL_tab_User.GetModel("UserAccount='" + strPhoneNum + "'");
                                intUserID = Model_tab_User.ID;
                                Model_tab_User.Password = strPassword;
                                Model_tab_User.Updatetime = DateTime.Now;
                                #region 设置 推荐人的手机号码
                                if (Model_tab_User.ParentID == 0 || Model_tab_User.ParentID == null)
                                {
                                    EggsoftWX.Model.tab_User Model_tab_UserRecommandPhone = BLL_tab_User.GetModel("UserAccount='" + strRecommandPhone + "'");
                                    if (Model_tab_UserRecommandPhone != null)
                                    {
                                        Model_tab_User.ParentID = Model_tab_UserRecommandPhone.ID;////设置 推荐人的手机号码
                                        //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                        Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                        if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                        intIFModifyParent = Model_tab_User.ParentID;
                                    }
                                    else
                                    {
                                        intRecommandPhone = 55;
                                    }
                                }
                                if (String.IsNullOrEmpty(Model_tab_User.ContactPhone))
                                {
                                    Model_tab_User.ContactPhone = strPhoneNum;
                                }
                                if (intRecommandPhone != 55) intRecommandPhone = 5;///提示用户 推荐人不存在 以后来注册也是可以的
                                #endregion 设置 推荐人的手机号码
                                BLL_tab_User.Update(Model_tab_User);
                            }
                            else
                            {///不存在 这个 账户 。那就 加入账户
                                Eggsoft.Common.debug_Log.Call_WriteLog("开始 strPhoneNum=" + strPhoneNum + "不存在 这个 账户 。那就 创建账户", "注册用户");

                                if (BLL_tab_User.Exists("UserAccount='" + strPhoneNum + "' and ShopClientID=" + strShopClientID))
                                {
                                    intRecommandPhone = -3;////手机号码已被其他微信号捆绑
                                }
                                else
                                {
                                    //Model_tab_User.NickName = strPhoneNum;
                                    Model_tab_User.Updatetime = DateTime.Now;
                                    Model_tab_User.UpdateBy = "绑定手机号";
                                    Model_tab_User.ShopClientID = int.Parse(strShopClientID);
                                    Model_tab_User.ContactPhone = strPhoneNum;
                                    Model_tab_User.UserAccount = strPhoneNum;
                                    #region 设置 推荐人的手机号码
                                    if (Model_tab_User.ParentID == 0 || Model_tab_User.ParentID == null)
                                    {
                                        EggsoftWX.Model.tab_User Model_tab_UserRecommandPhone = BLL_tab_User.GetModel("UserAccount='" + strRecommandPhone + "'");
                                        if (Model_tab_UserRecommandPhone != null)
                                        {
                                            Model_tab_User.ParentID = Model_tab_UserRecommandPhone.ID;////设置 推荐人的手机号码
                                            //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                            Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                            if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                        }
                                        else
                                        {
                                            intRecommandPhone = 66;///提示用户 推荐人不存在 以后来注册也是可以的
                                        }
                                    }
                                    #endregion 设置 推荐人的手机号码
                                    if (intRecommandPhone != 66) intRecommandPhone = 6;///提示用户 推荐人不存在 以后来注册也是可以的
                                    BLL_tab_User.Update(Model_tab_User);
                                }
                            }
                        }
                        else
                        {
                            Model_tab_User.UserAccount = strPhoneNum;
                            Model_tab_User.Password = strPassword;
                            Model_tab_User.Updatetime = DateTime.Now;
                            #region 设置 推荐人的手机号码
                            if (Model_tab_User.ParentID == 0 || Model_tab_User.ParentID == null)
                            {
                                EggsoftWX.Model.tab_User Model_tab_UserRecommandPhone = BLL_tab_User.GetModel("UserAccount='" + strRecommandPhone + "'");
                                if (Model_tab_UserRecommandPhone != null)
                                {
                                    Model_tab_User.ParentID = Model_tab_UserRecommandPhone.ID;////设置 推荐人的手机号码
                                    //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                    intIFModifyParent = Model_tab_User.ParentID;


                                }
                                else
                                {
                                    intRecommandPhone = 55;
                                }
                            }
                            if (String.IsNullOrEmpty(Model_tab_User.ContactPhone))
                            {
                                Model_tab_User.ContactPhone = strPhoneNum;
                            }
                            if (intRecommandPhone != 55) intRecommandPhone = 5;///提示用户 推荐人不存在 以后来注册也是可以的
                            #endregion 设置 推荐人的手机号码
                            BLL_tab_User.Update(Model_tab_User);

                            #region 检查有没有 官方给我充过钱

                            EggsoftWX.BLL.tab_ShopClient_MemberCard bll_MemberCard = new EggsoftWX.BLL.tab_ShopClient_MemberCard();
                            System.Data.DataTable BLLMemberCard = bll_MemberCard.GetList("ID", "ShopClientID=" + strShopClientID + " and PhoneNum='" + strPhoneNum + "' and isnull(IfChangToWeiXinBonus,0)=0").Tables[0];
                            EggsoftWX.Model.tab_ShopClient_MemberCard Model = new EggsoftWX.Model.tab_ShopClient_MemberCard();
                            for (int i = 0; i < BLLMemberCard.Rows.Count; i++)
                            {
                                string strID = BLLMemberCard.Rows[i]["ID"].ToString();
                                Model = bll_MemberCard.GetModel(int.Parse(strID));
                                bool boolSendMoney = Eggsoft_Public_CL.PubMember.CardBonusChangeToUserAccount(Model);
                                if (boolSendMoney)
                                {
                                    Model.IfChangToWeiXinBonus = true;
                                    Model.BonusDesc = Model.BonusDesc + " 手机端绑定账号，注册成功";
                                    Model.UpdateTime = DateTime.Now;
                                    Model.UpdateBy = "手机端绑定账号";
                                    Model.CreateBy = "手机端绑定账号";
                                    bll_MemberCard.Update(Model);///更新状态 预充值状态
                                    intRecommandPhone = 7;//提示充值成功 可以登陆会员中心查看
                                }
                            }




                            #endregion 检查有没有 官方给我充过钱
                        }



                        #region 增加直推未处理信息
                        if (Model_tab_User != null && intIFModifyParent > 0)
                        {
                            //string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = "改写上级推荐人的手机号码";
                            Model_b011_InfoAlertMessage.CreateBy = "核对短信验证码";
                            Model_b011_InfoAlertMessage.UpdateBy = "核对短信验证码";
                            Model_b011_InfoAlertMessage.UserID = intIFModifyParent;
                            Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intIFModifyParent.toString());
                            Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                            Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        }
                        #endregion 增加直推未处理信息  

                        #region 增加间推未处理信息
                        if (Model_tab_User != null && Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent.toInt32()) > 0)
                        {
                            //string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = "改写上上级推荐人的手机号码";
                            Model_b011_InfoAlertMessage.CreateBy = "核对短信验证码";
                            Model_b011_InfoAlertMessage.UpdateBy = "核对短信验证码";
                            Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent.toInt32());
                            Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intIFModifyParent.toString());
                            Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                            Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        }
                        #endregion 增加直推未处理信息  


                        str = "{\"ErrorCode\":" + intRecommandPhone + ",\"UserID\":" + intUserID + "}";////添加用户 成功
                    }
                    #endregion
                }
                else
                {
                    str = "{\"ErrorCode\":-2}";////验证码错误   提示用户 一天 内 都有效
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
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "短信验证线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "短信验证");
            }
            finally
            {

            }
            //不存在 这个 账户 。那就 创建账户
            return str;
        }
        #endregion 注册用户


        #region 登陆用户
        private static object mydoGameInfo_CheckPhonePWD_Login = new object();
        /// <summary>
        /// 核对短信验证码 并注册用户
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doGameInfo_CheckPhonePWD_Login()
        {
            string str = "";
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
            string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
            string strPhoneNum = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["PhoneNum"]);
            string strPassword = (context.QueryString["userpwd"]);

            try
            {


                Eggsoft.Common.debug_Log.Call_WriteLog("开始 strPhoneNum=" + strPhoneNum + " strShopClientID=" + strShopClientID + " strUserID=" + strUserID + " strPassword=" + strPassword, "登陆用户");
                lock (mydoGameInfo_CheckPhonePWD_Login)
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    string strSelectWhere = "ShopClientID=" + strShopClientID + " and UserAccount='" + strPhoneNum + "' and Password='" + strPassword + "'";
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(strSelectWhere);
                    if (Model_tab_User != null)
                    {
                        if (Model_tab_User.ID == Convert.ToInt32(strUserID))
                        {
                            str = "{\"ErrorCode\":1,\"UserID\":" + Model_tab_User.ID + "}";////添加用户 成功
                        }
                        else
                        {
                            str = "{\"ErrorCode\":2}";////手机号码已被其他微信号绑定
                        }
                    }
                    else
                    {
                        strSelectWhere = "ShopClientID=" + strShopClientID + " and UserAccount='" + strPhoneNum + "'";
                        Model_tab_User = BLL_tab_User.GetModel(strSelectWhere);
                        if (Model_tab_User != null)
                        {
                            str = "{\"ErrorCode\":-2}";////密码错误
                        }
                        else
                        {
                            str = "{\"ErrorCode\":-3}";////收入的手机号码不存在
                        }
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
                Eggsoft.Common.debug_Log.Call_WriteLog("strShopClientID=" + strShopClientID + "   strPhoneNum=" + strPhoneNum + " userpwd=" + strPassword, "登陆线程异常");
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "登陆线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("strShopClientID=" + strShopClientID + "   strPhoneNum=" + strPhoneNum + " userpwd=" + strPassword, "登陆线程异常");
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "登陆验证");
            }
            finally
            {

            }
            //不存在 这个 账户 。那就 创建账户
            return str;
        }
        #endregion 登陆用户
    }
}
