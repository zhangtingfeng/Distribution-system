using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS
{
    /// <summary>
    /// WbS_User 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WbS_User : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public int SubLogin(string strUserName, string strPwd)
        {
            strPwd = Eggsoft.Common.DESCrypt.GetMd5Str32(strPwd);

            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();

            int result = BLL_tab_User.ExistsCount("[pc_username] = '" + strUserName + "' AND [pc_password] ='" + strPwd + "'");
            if (result > 0)
            {

                EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                Model_tab_User = BLL_tab_User.GetModel("[pc_username] = '" + strUserName + "' AND [pc_password] ='" + strPwd + "'");
                Model_tab_User.Updatetime = DateTime.Now;
                BLL_tab_User.Update(Model_tab_User);

            }
            //int result = TwoD.ValidateLogin(strUserName, strPwd);


            return result;
        }

        [WebMethod]
        /// <summary>
        /// 免费注册
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPwd">密码</param>
        /// <returns></returns>
        public int FreeRegister(string strUserName, string strPwd)
        {
            strPwd = Eggsoft.Common.DESCrypt.GetMd5Str32(strPwd);
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();

            int result = new EggsoftWX.BLL.tab_User().ExistsCount("[pc_username] = '" + strUserName + "' AND [pc_password] ='" + strPwd + "'");
            if (result > 0)
            {
                return 2;///表示已注册
            }
            else
            {
                /*   pc端暂时不用 20150204
                Model_tab_User.pc_username = strUserName;
                Model_tab_User.pc_password = strPwd;
                Model_tab_User.pc_userstate = "1";
                Model_tab_User.SocialPlatform = "PC";
                BLL_tab_User.Add(Model_tab_User);
                 * */
                return 1;
            }
            //strPwd = WeBuyOperate.MD5(strPwd, 32);
            //return TwoD.FreeRegister(strUserName, strPwd);
        }

        [WebMethod]
        /// <summary>
        /// 免费注册
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPwd">密码</param>
        /// <returns></returns>
        public int UpdatePassWord(string strUserName, string strPwd)
        {
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();

            int result = new EggsoftWX.BLL.tab_User().ExistsCount("[pc_username] = '" + strUserName + "'");
            if (result > 0)
            {
                strPwd = Eggsoft.Common.DESCrypt.GetMd5Str32(strPwd);

                //  string strsql = string.Format("update tab_User set pc_password='{0}' where pc_username='{1}'", strPwd, strUserName);
                BLL_tab_User.Update("pc_password='" + strPwd + "'", "pc_username='" + strUserName + "'");
            }
            return result;

            //strPwd = WeBuyOperate.MD5(strPwd, 32);
            //return TwoD.UpdatePassWord(strUserName, strPwd);
        }

        [WebMethod]
        /// <summary>
        /// 免费注册
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPwd">密码</param>
        /// <returns></returns>
        public int Update_UserName_Only_Test(string strUserName, string strPwd, string strToUserName)
        {
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            strPwd = Eggsoft.Common.DESCrypt.GetMd5Str32(strPwd);
            int result = 0;
            if ((String.IsNullOrEmpty(strUserName)) || (String.IsNullOrEmpty(strPwd)) || (String.IsNullOrEmpty(strToUserName)))
            {
                result = 0;
            }
            else
            {
                string strSQL = "[pc_username] = '" + strUserName + "' and pc_password='" + strPwd + "'";
                result = new EggsoftWX.BLL.tab_User().ExistsCount(strSQL);
                if (result > 0)
                {

                    //  string strsql = string.Format("update tab_User set pc_password='{0}' where pc_username='{1}'", strPwd, strUserName);
                    BLL_tab_User.Update("pc_username='" + strToUserName + "'", strSQL);
                }
            }
            return result;

            //strPwd = WeBuyOperate.MD5(strPwd, 32);
            //return TwoD.UpdatePassWord(strUserName, strPwd);
        }


        [WebMethod]
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <returns></returns>
        public string UserIsExist(string strUserName)
        {
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();

            int result = new EggsoftWX.BLL.tab_User().ExistsCount("[pc_username] = '" + strUserName + "'");

            //
            return result.ToString();
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="strMobile">手机号码</param>
        /// <param name="strType">0：注册 1：找回</param>
        /// <returns></returns>
        [WebMethod]
        public string SendValidCode(string strMobile, string strType)
        {
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();


            string strResultValue = "";
            switch (strType)
            {
                case "0":
                    int result = new EggsoftWX.BLL.tab_User().ExistsCount("[pc_username] = '" + strMobile + "'");

                    if (result > 0)
                    {
                        strResultValue = "001";
                    }
                    else
                    {
                        strResultValue = SendValidCode(strMobile);
                    }
                    break;
                case "1":
                    strResultValue = SendValidCode(strMobile);
                    break;
                default:
                    strResultValue = "000";
                    break;
            }
            return strResultValue;
        }

        public string SendValidCode(string strMobile)
        {


            Eggsoft_Public_CL.HttpPost client = new Eggsoft_Public_CL.HttpPost();
            string strusername = ConfigurationManager.AppSettings["SendUname"];
            string strscode = ConfigurationManager.AppSettings["SendPwd"];
            string strValidCode = Eggsoft.Common.StringNum.GenerateRandomNumber(6);
            //string sms = HttpUtility.UrlEncode(txtcontent.Text, Encoding.GetEncoding("gbk"));//若参数中有中文的话，请先用此方法转成GBK编码
            string url = "http://mssms.cn:8000/msm/sdk/http/sendsms.jsp?username=" + strusername + "&scode=" + strscode + "&content=@1@=" + strValidCode + "&tempid=MB-2013102300&mobile=" + strMobile;
            client = new Eggsoft_Public_CL.HttpPost(url);
            //client.PostingData.Add("username", "JSMB260648");
            //client.PostingData.Add("scode", "179933");
            //client.PostingData.Add("mobile", "13418937025");
            //client.PostingData.Add("content", "你好吗");
            //client.PostingData.Add("extcode", "888");
            string result = client.GetString();
            if (!result.Trim().Equals("0#1#1"))
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("邮件返回信息为：" + result);
                //LogHelper.WriteLog("邮件返回信息为：" + result);

            }
            return strValidCode;
        }



    }
}
