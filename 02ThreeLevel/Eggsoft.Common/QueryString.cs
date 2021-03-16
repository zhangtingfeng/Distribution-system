using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;



//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class QueryString_EggSoft :  System.Collections.Specialized.StringDictionary
    {

        /// <summary>
        /// 接收查询参数，返回字符串值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static string QueryString(HttpRequest request, string param, string defValue)
        {
            if (request.Params[param] != null && !string.IsNullOrEmpty(request.Params[param].ToString()))
            {
                return request.Params[param].ToString();
            }
            //if (request.QueryString[param] != null)
            //{
            //    return request.QueryString[param].ToString();
            //}
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 接收查询参数，返回整型值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int QueryString(HttpRequest request, string param, int defValue)
        {
            if (request.Params[param] != null && !string.IsNullOrEmpty(request.Params[param].ToString()))
            {
                return  Eggsoft.Common.CommUtil.ToInt(request.Params[param].ToString());
            }
            //if (request.QueryString[param] != null)
            //{
            //    try
            //    {
            //        return ToInt(request.QueryString[param].ToString());
            //    }
            //    catch
            //    {
            //        return defValue;
            //    }
            //}
            else
            {
                return defValue;
            }
        }


        public QueryString_EggSoft()
        {
            // TODO: 在此处添加构造函数逻辑
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="encryptedData"></param>
        public QueryString_EggSoft(string encryptedData)
        {

            byte[] RawData = HexEncoding.GetBytes(encryptedData);
            byte[] ClearRawData = System.Security.Cryptography.ProtectedData.Unprotect(
                        RawData, null, DataProtectionScope.LocalMachine);
            string StringData = Encoding.UTF8.GetString(ClearRawData);
            int Index;
            string[] SplittedData = StringData.Split(new char[] { '&' });
            foreach (string SingleData in SplittedData)
            {
                Index = SingleData.IndexOf('=');
                base.Add(
                    HttpUtility.UrlDecode(SingleData.Substring(0, Index)),
                    HttpUtility.UrlDecode(SingleData.Substring(Index + 1))
                );
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder Content = new StringBuilder();
            foreach (string key in base.Keys)
            {
                Content.Append(HttpUtility.UrlEncode(key));
                Content.Append("=");
                Content.Append(HttpUtility.UrlEncode(base[key]));
                Content.Append("&");
            }
            Content.Remove(Content.Length - 1, 1);
            byte[] EncryptedData = ProtectedData.Protect(
                        Encoding.UTF8.GetBytes(Content.ToString()),
                        null, DataProtectionScope.LocalMachine);
            return HexEncoding.GetString(EncryptedData);
        }



    }

    


}
