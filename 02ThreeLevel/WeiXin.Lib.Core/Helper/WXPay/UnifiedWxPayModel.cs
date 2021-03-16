using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXin.Lib.Core.Consts;

namespace WeiXin.Lib.Core.Helper.WXPay
{
    public class UnifiedWxPayModel
    {
        #region 构造函数、私有变量
        private UnifiedWxPayModel()
        {
            this.parameters = new Dictionary<string, string>();
        }

        private Dictionary<string, string> parameters;
        public string AppId;
        private string Key;
        public string PartnerId;

        #endregion

        #region 创建UnifiedPrePayModel
        public static UnifiedWxPayModel CreateUnifiedModel(string appId, string partnerId, string key)
        {
            UnifiedWxPayModel wxPayModel = new UnifiedWxPayModel();
            //先设置基本信息
            wxPayModel.SetAppId(appId);
            wxPayModel.SetKey(key);
            wxPayModel.SetPartnerId(partnerId);

            return wxPayModel;
        }
        #endregion

        #region 参数操作相关
        public void SetAppId(string str)
        {
            AppId = str;
        }

        public void SetPartnerId(string str)
        {
            PartnerId = str;
        }

        public void SetKey(string str)
        {
            Key = str;
        }

        #endregion

        #region 生成订单详情package、支付签名

        /// <summary>
        /// MD5签名
        /// </summary>
        /// <returns></returns>
        public string GetCftPackage(Dictionary<string, string> bizObj)
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new Exception("Key为空！");
            }

            string unSignParaString = CommonUtil.FormatBizQueryParaMapForUnifiedPay(bizObj);

            return MD5SignUtil.Sign(unSignParaString, Key);
        }

        public bool ValidateMD5Signature(Dictionary<string, string> bizObj, string sign)
        {
            string signValue = GetCftPackage(bizObj);
            return signValue == sign;
        }

        #endregion

        #region 生成 预支付 请求参数（XML）
        /// <summary>
        /// 生成 预支付 请求参数（XML）
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tradeNo"></param>
        /// <param name="totalFee"></param>
        /// <param name="createIp"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string CreatePrePayPackage(string description, string tradeNo, string totalFee, string createIp, string notifyUrl, string openid)
        {
            Dictionary<string, string> nativeObj = new Dictionary<string, string>();

            nativeObj.Add("appid", AppId);
            nativeObj.Add("mch_id", PartnerId);
            nativeObj.Add("nonce_str", CommonUtil.CreateNoncestr());
            nativeObj.Add("body", description);
            nativeObj.Add("out_trade_no", tradeNo);
            nativeObj.Add("total_fee", totalFee); //todo:写死为1
            nativeObj.Add("spbill_create_ip", createIp);
            nativeObj.Add("notify_url", notifyUrl);
            nativeObj.Add("trade_type", "JSAPI");
            nativeObj.Add("openid", openid);
            nativeObj.Add("sign", GetCftPackage(nativeObj));

            string strnativeObj = DictionaryToXmlString(nativeObj);
            Eggsoft.Common.debug_Log.Call_WriteLog(strnativeObj, "预支付返回", "strnativeObj");

            return strnativeObj;
        }

        #endregion

        #region 创建订单查询 XML
        /// <summary>
        /// 创建订单查询 XML
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public string CreateOrderQueryXml(string orderNo)
        {
            Dictionary<string, string> nativeObj = new Dictionary<string, string>();

            nativeObj.Add("appid", AppId);
            nativeObj.Add("mch_id", PartnerId);
            nativeObj.Add("out_trade_no", orderNo);
            nativeObj.Add("nonce_str", CommonUtil.CreateNoncestr());
            nativeObj.Add("sign", GetCftPackage(nativeObj));

            return DictionaryToXmlString(nativeObj);
        }
        #endregion




        #region 创建订单退款 XML
        private string toMD5(string source)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        }


        /// <summary>
        /// 创建订单退款 XML
        /// </summary>
        /// <param name="orderNo">商户订单号</param>
        /// <param name="transactionId">微信订单号</param>
        /// <param name="totalFee">总金额</param>
        /// <param name="refundNo">退款订单号</param>
        /// <param name="refundFee">退款金额</param>
        /// <returns></returns>
        public string CreateOrderRefundXml(int intShopClientID, string orderNo, string transactionId, string totalFee, string refundNo, string refundFee, string strRefund_account)
        {
            WxPayModel wxPayModel = new WxPayModel(intShopClientID);

            string strnonce_str = CommonUtil.CreateNoncestr();

           
             //                 < op_user_id > " + PartnerId + @" </ op_user_id >
             //total_fee --这里必须是支付时候的总就价格，特别是分批次退款的时候注意这里

       
         
            string StringA = "";//拼接签名算法
            StringA += "appid=" + wxPayModel.AppId;
            StringA += "&mch_id=" + wxPayModel.PartnerID;
            StringA += "&nonce_str=" + strnonce_str;
            StringA += "&out_refund_no=" + refundNo;
            StringA += "&refund_account=" + strRefund_account;
            StringA += "&refund_fee=" + refundFee;
            StringA += "&total_fee=" + totalFee;
            StringA += "&transaction_id=" + transactionId;
            StringA += "&key=" + wxPayModel.PartnerKey; ;
           string signature = toMD5(StringA).ToUpper();

            string strXML = "<xml>";
            strXML += "      <appid>" + wxPayModel.AppId + "</appid>";
            strXML += "      <mch_id>" + wxPayModel.PartnerID + "</mch_id>";
            strXML += "      <nonce_str>" + strnonce_str + "</nonce_str> ";
            strXML += "      <out_refund_no>" + refundNo + "</out_refund_no>";
            // strXML += "      <out_trade_no>" + transactionId + "</out_trade_no>";///商户订单号
            strXML += "      <refund_account>" + strRefund_account + "</refund_account>";
            strXML += "      <refund_fee>" + refundFee + "</refund_fee>";
            strXML += "      <total_fee>" + totalFee + "</total_fee>";
            strXML += "      <transaction_id>" + transactionId + "</transaction_id>";///微信生成的订单号，在支付通知中有返回
            strXML += "      <sign>"+ signature + "</sign>";
            strXML += "    </xml>";

            return strXML;

//            Dictionary<string, string> nativeObj = new Dictionary<string, string>();

            //        nativeObj.Add("appid", wxPayModel.AppId);
            //            nativeObj.Add("mch_id", wxPayModel.PartnerID);
            //            nativeObj.Add("nonce_str", CommonUtil.CreateNoncestr());
            //            if (string.IsNullOrEmpty(transactionId))
            //            {
            //                if (string.IsNullOrEmpty(orderNo))
            //                    throw new Exception("缺少订单号！");
            //        nativeObj.Add("out_trade_no", orderNo);
            //            }
            //            else
            //            {
            //                nativeObj.Add("transaction_id", transactionId);
            //            }

            //nativeObj.Add("out_refund_no", refundNo);
            //            nativeObj.Add("total_fee", totalFee);
            //            nativeObj.Add("refund_fee", refundFee);
            //            nativeObj.Add("op_user_id", PartnerId); //todo:配置

            //            nativeObj.Add("sign", GetCftPackage(nativeObj));

            //            return DictionaryToXmlString(nativeObj);
        }

        #endregion

        #region dictionary与XmlDocument相互转换
        /// <summary>
        /// dictionary转为xml 字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string DictionaryToXmlString(Dictionary<string, string> dic)
{
    StringBuilder xmlString = new StringBuilder();
    xmlString.Append("<xml>");
    foreach (string key in dic.Keys)
    {
        xmlString.Append(string.Format("<{0}><![CDATA[{1}]]></{0}>", key, dic[key]));
    }
    xmlString.Append("</xml>");
    return xmlString.ToString();
}

/// <summary>
/// xml字符串 转换为  dictionary
/// </summary>
/// <param name="document"></param>
/// <returns></returns>
public static Dictionary<string, string> XmlToDictionary(string xmlString)
{
    System.Xml.XmlDocument document = new System.Xml.XmlDocument();
    document.LoadXml(xmlString);

    Dictionary<string, string> dic = new Dictionary<string, string>();

    var nodes = document.FirstChild.ChildNodes;

    foreach (System.Xml.XmlNode item in nodes)
    {
        dic.Add(item.Name, item.InnerText);
    }
    return dic;
}
        #endregion

    }
}
