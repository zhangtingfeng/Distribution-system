using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Eggsoft_Public_CL
{




    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class WXRed
    {
        ////Appid
        //public static string Weixin_Appid = "wxb97da79b8bad5e74";
        ////Appsecret 
        //public static string Weixin_Appsecret = "058ac75367f939b4ce10c242788080d6";

        ////微信支付分配的商户号
        //public static string Weixin_Pay_MCHID = "10092403";

        ////商户支付密钥  不知道在哪获取可以登录商户平台：https://pay.weixin.qq.com，然后点击账户设置 => 安全设置 => API安全 点击“设置密钥”按钮
        //public static string Weixin_Pay_MerchantKey = "123456789012345678901234567890qw";

        ////证书地址
        //public static string cert_url = @"F:\NetHttp\0034eggsoft.cnvs2015_GaoLaoShi\12Eggsoft_Service_Upload\UpLoad\000001_sh\Cert\apiclient_cert.p12";



        private string private_Weixin_Appid = "";
        private string private_Weixin_Pay_MCHID = "";
        private string private_Weixin_Pay_MerchantKey = ""; ///拼接API密钥
        private string private_cert_url = "";
        private string private_Weixin_AppShopClientName = "";


        public void sendMoney(int intUserID, string strTransNo, Decimal decSendMoney, out string strresult_code, out string strpayment_no, out string strerr_code_des)
        {
            strresult_code = ""; strpayment_no = ""; strerr_code_des = "";

            try
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("intUserID=" + intUserID + " decSendMoney=" + decSendMoney, "微信现金零钱");

                int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                EggsoftWX.Model.tab_ShopClient_EngineerMode Model_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                Model_EngineerMode = BLL_EngineerMode.GetModel("ShopClientID=" + intShopClientID);

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                Model_tab_User = BLL_tab_User.GetModel(intUserID);



                string strCertURL = Model_EngineerMode.Apiclient_cert_Pem;
             
                WXRed myWXRed = new WXRed();
                myWXRed.WXRed_SetBasicInfo(Model_EngineerMode.WeiXinAppId, Model_EngineerMode.WeiXinPayID, Model_EngineerMode.PartnerKey, strCertURL, Model_tab_ShopClient.ShopClientName.Replace("(", "（").Replace(")", "）"));

                string stringmyWXRed_Debug = myWXRed.GetCachred_02CashChange(Model_tab_User.UserRealName, strTransNo, Model_tab_User.OpenID, decSendMoney);



                Eggsoft.Common.debug_Log.Call_WriteLog(stringmyWXRed_Debug, "微信现金零钱", "转账成功日志");
                getifSucess(stringmyWXRed_Debug, out strresult_code, out strpayment_no, out strerr_code_des);
                strresult_code = strresult_code.ToLower();
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex.Message, "微信现金零钱");
            }
            finally { }
        }


        private void getifSucess(string strRequest_str, out string strresult_code, out string strpayment_no, out string strerr_code_des)
        {



            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.LoadXml(strRequest_str);


            strpayment_no = "";
            strerr_code_des = "";

            System.Xml.XmlNode myxmlNode_Response = xDoc.SelectSingleNode("/xml/result_code");
            strresult_code = myxmlNode_Response.InnerText;
            if (strresult_code.ToLower() == "success")
            {
                myxmlNode_Response = xDoc.SelectSingleNode("/xml/payment_no");
                strpayment_no = myxmlNode_Response.InnerText;
            }
            else
            {
                myxmlNode_Response = xDoc.SelectSingleNode("/xml/err_code_des");
                strerr_code_des = myxmlNode_Response.InnerText;
            }

        }



        public WXRed()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        private void WXRed_SetBasicInfo(string strWeixin_Appid, string strWeixin_Pay_MCHID, string strWeixin_Pay_MerchantKey, string strcert_url, string str_Weixin_AppShopClientName)
        {
            private_Weixin_Appid = strWeixin_Appid;
            private_Weixin_Pay_MCHID = strWeixin_Pay_MCHID;
            private_Weixin_Pay_MerchantKey = strWeixin_Pay_MerchantKey;///拼接API密钥
            private_cert_url = strcert_url;
            private_Weixin_AppShopClientName = str_Weixin_AppShopClientName;

            //



            //TODO: 在此处添加构造函数逻辑
            //
        }



        private long GenerateTimeStamp(DateTime dt)
        {
            // Default implementation of UNIX time of the current UTC time  
            TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        //生成随机字符
        private string GenNoncestr(int length)
        {
            Random r = new Random();
            string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string noncestr = "";
            for (int i = 0; i < length; i++)
            {
                noncestr += str[r.Next(str.Length)];
            }
            return noncestr;
        }




        #region 微信红包
        private string toMD5(string source)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        }
        private string GetSignature_01RedSign(string act_name, int max_value, string mch_billno, string mch_id, int min_value, string nick_name, string nonce_str, string remark, string re_openid, string send_name, int total_amount, int total_num, string wishing, string sign = "")
        {
            string signature = "";
            string signature_str = "";
            string client_ip = HttpContext.Current.Request.UserHostAddress;
            //client_ip = "120.24.81.198";
            string StringA = "";
            //拼接签名算法
            string wxappid = private_Weixin_Appid;
            StringA += "act_name=" + act_name;
            StringA += "&client_ip=" + client_ip;
            StringA += "&max_value=" + max_value;
            StringA += "&mch_billno=" + mch_billno;
            StringA += "&mch_id=" + mch_id;
            StringA += "&min_value=" + min_value;
            StringA += "&nick_name=" + nick_name;
            StringA += "&nonce_str=" + nonce_str;
            StringA += "&re_openid=" + re_openid;
            StringA += "&remark=" + remark;
            StringA += "&send_name=" + send_name;
            if (sign != "")
                StringA += "&sign=" + sign;
            StringA += "&total_amount=" + total_amount;
            StringA += "&total_num=" + total_num;
            StringA += "&wishing=" + wishing;
            StringA += "&wxappid=" + wxappid;
            signature_str = StringA + "&key=" + private_Weixin_Pay_MerchantKey;
            signature = toMD5(signature_str).ToUpper();
            return signature;
        }


        /*CheckValidationResult的定义*/
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }
        private string Web_Post(string url, string xmldata)
        {
            string resp = string.Empty;
            try
            {
                //证书地址,并且双击apiclient_cert.p12安装证书
                string cert = private_cert_url;
                //Eggsoft.Common.debug_Log.Call_WriteLog("cert=" + cert, "WXRed_Debug");

                string password = private_Weixin_Pay_MCHID;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //Eggsoft.Common.debug_Log.Call_WriteLog("222", "WXRed_Debug");

                //****************注意*********************
                //这里添加 证书要注意，在本地调试的时候
                //本地调试用这个
                //X509Certificate cer = new X509Certificate(cert, password);
                //上传服务器上用这个
                X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                //Eggsoft.Common.debug_Log.Call_WriteLog("url=" + url, "WXRed_Debug");
                //Eggsoft.Common.debug_Log.Call_WriteLog("3333=", "WXRed_Debug");

                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.ClientCertificates.Add(cer);
                webrequest.Method = "post";
                webrequest.UseDefaultCredentials = true;
                //添加xml数据
                StreamWriter swMessages = new StreamWriter(webrequest.GetRequestStream());
                //写入的流以XMl格式写入
                swMessages.Write(xmldata);
                //关闭写入流
                swMessages.Close();

                HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
                Stream stream = webreponse.GetResponseStream();

                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex.Message, "WXRed_Debug");

            }
            finally { }
            return resp;
        }

        /// <summary>
        /// 红包
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="decMoney"></param>
        /// <returns></returns>
        private string GetCachred_01RedSign(string openid, Decimal decMoney)
        {

            string nonce_str = GenNoncestr(16);
            string mch_id = private_Weixin_Pay_MCHID;
            Random r = new Random();
            string mch_billno = mch_id + DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(0, 9);
            string nick_name = private_Weixin_AppShopClientName;

            decMoney *= 100;//转化为分
            int intdecMoney = Convert.ToInt32(decMoney);
            //total_amount,min_value,max_value3个值必须是相等的
            int total_amount = intdecMoney;
            int min_value = intdecMoney;
            int max_value = intdecMoney;

            int total_num = 1;
            string wishing = "一键微商";
            string send_name = private_Weixin_AppShopClientName;//商户名称
            string client_ip = HttpContext.Current.Request.UserHostAddress;
            //client_ip = "120.24.81.198";
            string act_name = "现金分红";
            string remark = "代理提成";
            string wxappid = private_Weixin_Appid;

            //获得签名
            string signValue = GetSignature_01RedSign(act_name, max_value, mch_billno, mch_id, min_value, nick_name, nonce_str, remark, openid, send_name, total_amount, total_num, wishing);
            string post_data = "";
            post_data += "<xml>";
            post_data += "<sign><![CDATA[" + signValue + "]]></sign>";
            post_data += "<mch_billno><![CDATA[" + mch_billno + "]]></mch_billno>";
            post_data += "<mch_id>" + mch_id + "</mch_id>";
            post_data += "<wxappid>" + wxappid + "</wxappid>";
            post_data += "<nick_name><![CDATA[" + nick_name + "]]></nick_name>";
            post_data += "<send_name><![CDATA[" + send_name + "]]></send_name>";
            post_data += "<re_openid>" + openid + "</re_openid>";
            post_data += "<total_amount>" + total_amount + "</total_amount>";
            post_data += "<min_value>" + min_value + "</min_value>";
            post_data += "<max_value>" + max_value + "</max_value>";
            post_data += "<total_num>" + total_num + "</total_num>";
            post_data += "<wishing><![CDATA[" + wishing + "]]></wishing>";
            post_data += "<client_ip><![CDATA[" + client_ip + "]]></client_ip>";
            post_data += "<act_name><![CDATA[" + act_name + "]]></act_name>";
            post_data += "<remark><![CDATA[" + remark + "]]></remark>";
            post_data += "<nonce_str><![CDATA[" + nonce_str + "]]></nonce_str>";
            post_data += "</xml>";
            Eggsoft.Common.debug_Log.Call_WriteLog(post_data, "微信红包", "发送消息");

            string post_url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
            string request_str = Web_Post(post_url, post_data);
            Eggsoft.Common.debug_Log.Call_WriteLog(request_str, "微信红包", "收到消息");

            return request_str;
        }

        /// <summary>
        /// 现金零钱Cash change
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="decMoney"></param>
        /// <returns></returns>
        private string GetCachred_02CashChange(string UserRealName, string partner_trade_no, string openid, Decimal decMoney)
        {

            string nonce_str = GenNoncestr(16);
            string mch_id = private_Weixin_Pay_MCHID;
            Random r = new Random();
            string mch_billno = String.IsNullOrEmpty(partner_trade_no) ? (mch_id + DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(0, 9)) : partner_trade_no;
            string nick_name = private_Weixin_AppShopClientName;

            decMoney *= 100;//转化为分
            int intdecMoney = Convert.ToInt32(decMoney);
            //total_amount,min_value,max_value3个值必须是相等的
            int total_amount = intdecMoney;
            int min_value = intdecMoney;
            int max_value = intdecMoney;

            string send_name = private_Weixin_AppShopClientName;//商户名称
            string client_ip = HttpContext.Current.Request.UserHostAddress;
            //client_ip = "120.24.81.198";
            string act_name = "市场调查劳务费";
            string wxappid = private_Weixin_Appid;///

            //获得签名
            #region 获得签名          
            string signature = "";
            string signature_str = "";
            string StringA = "";
            //拼接签名算法
            StringA += "amount=" + total_amount;
            StringA += "&check_name=" + "FORCE_CHECK";
            StringA += "&desc=" + act_name;
            StringA += "&mch_appid=" + wxappid;
            StringA += "&mchid=" + mch_id;
            StringA += "&nonce_str=" + nonce_str;
            StringA += "&openid=" + openid;
            StringA += "&partner_trade_no=" + mch_billno;
            StringA += "&re_user_name=" + UserRealName;
            StringA += "&spbill_create_ip=" + client_ip;

            signature_str = StringA + "&key=" + private_Weixin_Pay_MerchantKey;
            signature = toMD5(signature_str).ToUpper();
            #endregion
            string post_data = "";

            post_data += "<xml> ";
            post_data += "<mch_appid>" + wxappid + "</mch_appid>";///公众账号appid mch_appid 是 wx8888888888888888 String 微信分配的公众账号ID（企业号corpid即为此appId） 
            post_data += "<mchid>" + mch_id + "</mchid>";//商户号 mchid 是 1900000109 String(32) 微信支付分配的商户号 
            post_data += "<nonce_str>" + nonce_str + "</nonce_str>";//随机字符串 nonce_str 是 5K8264ILTKCH16CQ2502SI8ZNMTM67VS String(32) 随机字符串，不长于32位 
            post_data += "<partner_trade_no>" + mch_billno + "</partner_trade_no>";///商户订单号 partner_trade_no 是 10000098201411111234567890 String 商户订单号，需保持唯一性(只能是字母或者数字，不能包含有符号) 
            post_data += "<openid>" + openid + "</openid>";///用户openid openid 是 oxTWIuGaIt6gTKsQRLau2M0yL16E String 商户appid下，某用户的openid 
            post_data += "<check_name>FORCE_CHECK</check_name>";///校验用户姓名选项 check_name 是 OPTION_CHECK String NO_CHECK：不校验真实姓名          FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功） 
            post_data += "<re_user_name>" + UserRealName + "</re_user_name>";///收款用户姓名 re_user_name 可选 马花花 String 收款用户真实姓名。  如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名
            post_data += "<amount>" + total_amount + "</amount>";//金额 amount 是 10099 int 企业付款金额，单位为分 
            post_data += "<desc>" + act_name + "</desc>";///企业付款描述信息 desc 是 理赔 String 企业付款操作说明信息。必填。 
            post_data += "<spbill_create_ip>" + client_ip + "</spbill_create_ip>";///Ip地址 spbill_create_ip 是 192.168.0.1 String(32) 调用接口的机器Ip地址 
            post_data += "<sign>" + signature + "</sign>";
            post_data += "</xml>";

            Eggsoft.Common.debug_Log.Call_WriteLog(post_data, "现金零钱", "发送消息");
            string post_url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
            string request_str = Web_Post(post_url, post_data);
            Eggsoft.Common.debug_Log.Call_WriteLog(request_str, "现金零钱", "收到消息");

            return request_str;
        }

        #endregion



    }
}