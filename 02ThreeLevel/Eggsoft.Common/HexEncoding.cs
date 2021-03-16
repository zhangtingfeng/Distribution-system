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
    public class HexEncoding 
    {
        public HexEncoding()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 将十六进行字节数组转换成字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetString(byte[] data)
        {
            StringBuilder Results = new StringBuilder();
            foreach (byte b in data)
            {
                Results.Append(b.ToString("X2"));
            }
            return Results.ToString();
        }
        /// <summary>
        /// 将字符串转换为十六进行字节数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string data)
        {
            byte[] Results = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
            {
                Results[i / 2] = Convert.ToByte(data.Substring(i, 2), 16);
            }
            return Results;
        }
    } 
}
