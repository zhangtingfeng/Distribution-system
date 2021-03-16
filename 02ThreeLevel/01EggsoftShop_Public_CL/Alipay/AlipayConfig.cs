using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using Eggsoft_Public_CL;

namespace Com.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        #region 字段
        private static string partner = "";
        private static string key = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        #endregion

        static Config()
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            //partner = "2088211287992429";
            partner = tab_System_And_.getTab_System("Textbox_Alipay_partner");

            //交易安全检验码，由数字和字母组成的32位字符串
            //如果签名方式设置为“MD5”时，请设置该参数
            //key = "cyw7housqtggm23c77q02h9bhedbswxt";
            key = tab_System_And_.getTab_System("Textbox_Alipay_key");
            //商户的私钥
            //如果签名方式设置为“0001”时，请设置该参数
            //private_key = @"MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBALFuxTc4RjZaqMQwJjtkL6lLk/iX31/uqc2ccnrHcVZNC8q2JKjp/ZrEo/BR+xH/bwaMXWR/coiJEwVImOixqdGuW54my8r2N4SUHLA5tYJtQyQgnGPc4MZe8yjWpqI1b7S0djGbUv+rjS++rtYk9Y3D3d9SC37l2ME7d9fpfQibAgMBAAECgYB9zlC/aoM+HuHy2UECc3Ln0tLEPMsBNjPnubniHG/cBR0LSkKMEfzjM/IZf8dJZ5fNSNEfZM5MyQRXhrYEp5QEDP6daxviEhnuj6Xn0zhOxifXyFvXWyIluUXRSmmew+8w/akol1etj7+f+ebDmkqKvDE6GRjWUFAWcjAmP4+pIQJBAOiNWBcqBJLP/oAcDeIXIkfW89cMXhOO2jrpvAWt5IqnClsKgQ452iZYRfizEn/UylkRru3DLshh+YFNQS9yDA0CQQDDUq3BkiT7FTDrc9KeT8KBGk/MdmVBQeAAczsk9Zrtw7lfpSvBY8egEBdnJQIzigbg3swiVSQ71t3tebhVkTVHAkAO2ohX4m0sW3CsCh6w5D1iTU6B295ebW9u9+L0kejZGlZE/mTD3dobPOQrQHTcCWFrUv/TW/YvAmMHaUHSn/w5AkBLjsg4gVhc6K2r53oqU6BiYNNNvN8eh2Unx2uxuHDeWUB0h2iNvxOSD6d99wsK9PIEOyusfFMv8saW/ucX8rwxAkEAii6WWo+VERnD92V5p7qwycl76zjtCuauGa9kGhbv1Ns/ssyvc+CPKeoWa8oRWyhRXlyfUi1hzYADVyZ5cvmVVw==";
            private_key = tab_System_And_.getTab_System("Textbox_Alipay_private_key");
            //支付宝的公钥
            //如果签名方式设置为“0001”时，请设置该参数
            //public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCVTsI3CYteVEnSj6tRrv2I/a4LE1k/iYB+xGVn a0Z0lM+OlziQyPgPp2Z3g1kfSk6XyZrhjJ24kGzMxTxKC66Nwy+ZPF+uZ7j3dR5EQavDTumg5EC2 AphPitvGum+uesW9TrYp2N0h2ZbDz8iJgBAlsbGdTxMlp76AaY9N0CZr+wIDAQAB";
            public_key=tab_System_And_.getTab_System("Textbox_Alipay_public_key");
            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑



            //字符编码格式 目前支持 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：0001(RSA)、MD5
            //sign_type = "0001";
            sign_type = "0001";
            //无线的产品中，签名方式为rsa时，sign_type需赋值为0001而不是RSA
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设交易安全校验码
        /// </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        #endregion
    }
}