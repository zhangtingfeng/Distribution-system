using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class DESCrypt
    {
        #region 前端加密使用的签名算法
        public static String hex_md5_10(string s)
        {
            var var8 = 0;

            while (var8 < 10)
            {
                var8++;
                s = GetMd5Str32(s);
            }
            return s;
        }

        public static String hex_md5_2(string s)
        {
            var var8 = 0;

            while (var8 < 2)
            {
                var8++;
                s = GetMd5Str32(s);
            }
            return s;
        }
        public static String hex_md5_8(string s)
        {
            var var8 = 0;

            while (var8 < 8)
            {
                var8++;
                s = GetMd5Str32(s);
            }
            return s;
        }
        #endregion 前端加密使用的签名算法



        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5Str32(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            // Convert the input string to a byte array and compute the hash.  
            char[] temp = str.ToCharArray();
            byte[] buf = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                buf[i] = (byte)temp[i];
            }
            byte[] data = md5Hasher.ComputeHash(buf);
            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data   
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.  
            return sBuilder.ToString();
        }



         public DESCrypt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //0-255范围的字节数
        
        static private byte[] Key24 = new byte[] { 25, 79, 36, 99, 167, 63, 42, 86, 246, 79, 36, 99, 167, 93, 42, 106, 93, 156, 78, 65, 218, 32, 55, 99 };
        static private byte[] IV24 = new byte[] { 96, 208, 32, 55, 103, 206, 79, 36, 99, 107, 210, 42, 56, 216, 79, 36, 99, 167, 203, 42, 86, 93, 126, 78 };

        static public string Crypt(string p)
        {
            if (p != "")
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(Key24, IV24), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(p);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length)).Replace("/","@@@XXX").Replace("=","@@@DDD").Replace("*","###XXX");
            }
            else
            {
                return "";
            }
        }

        static public string DeCrypt(string p)
        {
            if (p != "")
            {
                p = p.Replace("@@@XXX","/").Replace("@@@DDD","=").Replace("###XXX","*");
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                byte[] buffer = Convert.FromBase64String(p);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(Key24, IV24), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            else
            {
                return "";
            }
        }


       
         /// <summary>
        /// C#将字符串加密成数字，可逆解密，能实现不？
    //比如abc加密成123，字符串没有长度限制，但是必须把字符串加密成数字并且可逆，而且数据量非常大，还要考虑查询速度，各位大大们能帮帮我不？
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string NumberEncrypts(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
                sb.AppendFormat("{0:D5}", str[i] ^ 12345);
            string r1 = sb.ToString();
            
//Response.Write("加密后：" + r1 + "<br/>");
            return r1;
        }
         /// <summary>
        /// C#将字符串加密成数字，可逆解密，能实现不？
    //比如abc加密成123，字符串没有长度限制，但是必须把字符串加密成数字并且可逆，而且数据量非常大，还要考虑查询速度，各位大大们能帮帮我不？
      // string s = "中国人和日本人。";
//Response.Write("加密前：" + s + "<br/>");

//Response.Write("加密后：" + r1 + "<br/>");
//MatchCollection matches = Regex.Matches(r1, @"\d{5}");
//sb = new StringBuilder();
//for (int i = 0; i < matches.Count; i++)
//    sb.Append((char)(Int32.Parse(matches[i].Value) ^ 12345));
//string r2 = sb.ToString();
//Response.Write("解密后：" + r2);

//        加密前：中国人和日本人。
//加密后：3227626308323872578121980222933238700059
//解密后：中国人和日本人。

        /// </summary>
        /// <param name="str"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string NumberDencrypts(string str)
        {
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(str, @"\d{5}");
            StringBuilder  sb = new StringBuilder();
            for (int i = 0; i < matches.Count; i++)
                sb.Append((char)(Int32.Parse(matches[i].Value) ^ 12345));
            string r2 = sb.ToString();
            //Response.Write("解密后：" + r2);
            return r2;
        }

        /// <summary>
        /// 使用对称算法加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string SymmetricEncrypts(string str, string encryptKey)
        {
            string result = string.Empty;
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] IV = { 0x77, 0x70, 0x50, 0xD9, 0xE1, 0x7F, 0x23, 0x13, 0x7A, 0xB3, 0xC7, 0xA7, 0x48, 0x2A, 0x4B, 0x39 };
            try
            {
                byte[] byKey = System.Text.Encoding.UTF8.GetBytes(encryptKey);
                //如需指定加密算法，可在Create()参数中指定字符串
                //Create()方法中的参数可以是：DES、RC2 System、Rijndael、TripleDES 
                //采用不同的实现类对IV向量的要求不一样(可以用GenerateIV()方法生成)，无参数表示用Rijndael
                SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create();//产生一种加密算法
                MemoryStream msTarget = new MemoryStream();
                //定义将数据流链接到加密转换的流。
                CryptoStream encStream = new CryptoStream(msTarget, Algorithm.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                encStream.Write(inputData, 0, inputData.Length);
                encStream.FlushFinalBlock();
                result = Convert.ToBase64String(msTarget.ToArray());
                Algorithm.Clear();
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }


        /// <summary>
        /// 使用对称算法解密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string SymmectricDecrypts(string encryptStr, string encryptKey)
        {
            string result = string.Empty;
            //加密时使用的是Convert.ToBase64String(),解密时必须使用Convert.FromBase64String()
            try
            {
                byte[] encryptData = Convert.FromBase64String(encryptStr);
                byte[] byKey = System.Text.Encoding.UTF8.GetBytes(encryptKey);
                byte[] IV = { 0x77, 0x70, 0x50, 0xD9, 0xE1, 0x7F, 0x23, 0x13, 0x7A, 0xB3, 0xC7, 0xA7, 0x48, 0x2A, 0x4B, 0x39 };
                SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create();
                MemoryStream msTarget = new MemoryStream();
                CryptoStream decStream = new CryptoStream(msTarget, Algorithm.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                decStream.Write(encryptData, 0, encryptData.Length);
                decStream.FlushFinalBlock();
                result = System.Text.Encoding.Default.GetString(msTarget.ToArray());
                Algorithm.Clear();
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }


    }
}
