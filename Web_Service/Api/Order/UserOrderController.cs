using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Transactions;
using System.Text;
using System.IO;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Order;
using InsureApi.Common.Common;
using InsureApi.BLL;

namespace Web_Service.Api.Order
{
    public class UserOrderController : BasicController
    {
        /// <summary>
        /// 查看日志（订单）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultAPIModel<UserOrdersAPIModel.GetViewLog> Query(string id)
        {
            string apiUrl = "get api/order/UserOrder";
            var result = new ResultAPIModel<UserOrdersAPIModel.GetViewLog>();
            try
            {
                result.data = new UserOrdersAPIModel.GetViewLog();

                #region 获取数据
                var data = new InsuranceOrderBC().SelectByPrimaryKey(id);
                if (Check.getIns().isEmpty(data))
                {
                    result.resultMessage.errorMessage = "没有查询到数据";
                    return result;
                }
                #endregion

                result.data = new UserOrdersAPIModel.GetViewLog()
                {
                    InsuranceOrderID = data.InsuranceOrderID,
                    InsuranceOrderXML = data.InsuranceOrderXML
                };
            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    message = ex.Message.ToString()
                };
            }
            return result;
        }

        #region 发送付款、出单消息
        /// <summary>
        /// 发送付款、出单消息
        /// </summary>
        /// <param name="InsuranceOrderID">订单号</param>
        /// <param name="RequestType">发送类型，205支付，230出单</param>
        /// <param name="CompanyCode">保险公司代码</param>
        public string Key = "B5784A34EE904A9BB123844D4E8B25C5";
        public void SendMessage(string InsuranceOrderID, string RequestType, string CompanyCode)
        {
            ExtractInfo_OrderRenBaoAndTaiPingYang Extract_Info = new ExtractInfo_OrderRenBaoAndTaiPingYang();

            #region 数据组织
            Extract_Info.InsuranceOrderID = InsuranceOrderID;
            Extract_Info.CompanyCode = CompanyCode;
            Extract_Info.RequestType = RequestType;

            #region 签名
            string strGetSHA1 = "";
            strGetSHA1 += Extract_Info.InsuranceOrderID;
            strGetSHA1 += Extract_Info.CompanyCode;
            strGetSHA1 += Extract_Info.RequestType;
            Extract_Info.SHA1Str = Secret.getIns().encryptSHA1(strGetSHA1 + Key).Trim().ToUpper();// Secret.getIns().GetSHA1(Extract_Info.intType + Extract_Info.WeiXinOPenID + Extract_Info.ExaractAmount.ToString("f2") + Extract_Info.StrDesc + Key).Trim().ToUpper().Trim();

            // Extract_Info.SHA1Str = Secret.getIns().GetSHA1(strGetSHA1 + Key).Trim().ToUpper().Trim();

            #endregion

            #endregion

            #region 接口调用
            string Baoxdd = System.Configuration.ConfigurationManager.AppSettings["BaoxddUrl"];
            WebRequest webRequest = WebRequest.Create(Baoxdd + "/callback/Insure_02RenBao_05TaiPingYang/01CallBack.ashx");
            webRequest.Timeout = 1000 * 20;
            HttpWebRequest req = webRequest as HttpWebRequest;
            req.Timeout = 1000 * 20;
            req.Method = "POST";
            req.ContentType = "application/json;charset=UTF-8";
            req.Timeout = 50000;// 5000;
            byte[] bytebuff = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(Extract_Info));
            req.ContentLength = bytebuff.Length;
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(bytebuff, 0, bytebuff.Length);
            requestStream.Close();

            //所请求页面响应
            WebResponse pos = req.GetResponse();
            StreamReader sr = new StreamReader(pos.GetResponseStream(), Encoding.UTF8);
            string strResult = sr.ReadToEnd().Trim();
            sr.Close();
            sr.Dispose();
            #endregion
        }

        #region 消息对像类
        /// <summary>
        /// 人保、太平洋回调
        /// </summary>
        public class ExtractInfo_OrderRenBaoAndTaiPingYang
        {
            /// <summary>
            /// 订单号
            /// </summary>
            public string InsuranceOrderID { get; set; }
            /// <summary>
            /// 保险公司
            /// </summary>
            public string CompanyCode { get; set; }

            /// <summary>
            /// 请求状态，205支付，230出单；
            /// </summary>
            public string RequestType { get; set; }
            /// <summary>
            /// SHA1签名认证
            /// </summary>
            public string SHA1Str { get; set; }
        }
        #endregion
        #endregion
    }
}