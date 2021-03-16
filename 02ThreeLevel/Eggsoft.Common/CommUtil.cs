using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Net;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{

    /// <summary>
    /// 常用方法
    /// </summary>
    public class CommUtil
    {
        /// <summary> 
        /// 判断是否 = null 或 = ""
        /// </summary>
        public static bool isEmpty(params object[] args)
        {
            try
            {
                if (args == null) return true;
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == null) return true;
                    if ("".Equals(args[i].ToString())) return true;
                }
            }
            catch (Exception ex)
            {
                //LogController.writeErrorLog(ex, "Check isEmpty");
            }
            return false;
        }

        /// <summary>
        /// 转换成整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(string str)
        {
            int i = 0;
            if (int.TryParse(str, out i))
                return i;
            else return 0;

        }

        /// <summary>
        /// 转换成Bool值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(string str)
        {
            bool i = false;
            Boolean.TryParse(str, out i);
            return i;
        }

        /// <summary>
        /// 转换成时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string str)
        {
            DateTime dt = DateTime.Now;
            if (string.IsNullOrEmpty(str))
                return dt;
            DateTime.TryParse(str, out dt);
            return dt;
        }

        /// <summary>
        /// 转换成数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(string str)
        {
            Decimal d = 0;
            if (string.IsNullOrEmpty(str))
                return d;
            Decimal.TryParse(str, out d);
            return d;
        }

        //获6位的用户   编   码 20120824
        //--功能：add 0 to number,000001.bmp,000023.bmp
        //--输入 输出：000001.bmp
        public static string add0000Number(int argInteger, int argBit)//---add 0 to number,000001.bmp,000023.bmp
        {
            string tStr = argInteger.ToString();
            int tCount = tStr.Length;
            string t000Char = "";
            for (int i = 0; i < (argBit - tCount); i++)
            {
                t000Char = t000Char + "0";
            }

            string str_add0000Number = t000Char + tStr;
            return str_add0000Number;
        }



        /// <summary>
        /// 获取一个小数的整数部分
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetInt(int num)
        {
            string[] arr = num.ToString().Split('.');
            return Int32.Parse(arr[0]);
        }

        /// <summary>
        /// 过滤非法字符   防止攻击
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="exclueStr">排除的字符串，＂，＂分割如 ".,-,"</param>
        /// <returns></returns>
        public static string SafeFilter(string Htmlstring)
        {

            if (Htmlstring == null)
            {
                return "";
            }
            else
            {
                //删除脚本
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                ////删除HTML
                //Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);////帐号:yuanshengsp,密码sh51995669@
                //Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

                //Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);

                //删除与数据库相关的词
                Htmlstring = Regex.Replace(Htmlstring, "'", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "count", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, ">", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "<", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "=", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "cursor", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "delect", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "exec", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "master", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "varchar", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "declare", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "chr", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
            }
            return Htmlstring;
        }


        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string getShortText(string getLongShortText, int intMaxLong)
        {
            string strGetShortText = "";
            if (getLongShortText.Length > intMaxLong)
            {
                strGetShortText = getLongShortText.Substring(0, intMaxLong - 1) + ".";
            }
            else
            {
                strGetShortText = getLongShortText;
            }
            return strGetShortText;
        }


        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// 对md5改造的加密方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DotEnCrypt(string str, int safeStart)
        {
            string strPre = "", strEnd = "";
            int startIndex = safeStart;
            str = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            strEnd = DotReverse(str.Substring(startIndex, str.Length - startIndex));
            strPre = DotReverse(str.Substring(0, startIndex));
            return strEnd + strPre;
        }

        /// <summary>
        /// 字符串反转算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DotReverse(string str)
        {
            string ret = "";
            for (int i = str.Length - 1; i > -1; i--)
            {
                ret += str.Substring(i, 1);
            }
            return ret;
        }

        public static void Debug(string str)
        {
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 获取应用程序路径
        /// </summary>
        /// <param name="type">1 获取根目录，2　获取当前目录</param>
        /// <returns></returns>
        public static string GetAppPath(int type)
        {
            string retValue = "";
            if (type == 1)
            {
                retValue = HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
            }
            else
            {
                string str = HttpContext.Current.Request.ServerVariables["path_translated"];
                retValue = str.Substring(0, str.LastIndexOf("\\") + 1);
            }
            return retValue;
        }

        /// <summary>
        /// 判断是否为数字类型的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumStr(string str)
        {
            bool b = true;
            if (string.IsNullOrEmpty(str))
            {
                b = false;
            }
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    b = false;
                    break;
                }
            }
            return b;
        }
        /// <summary>
        /// 判断是否为数字类型的字符串
        /// </summary>
        public static bool IsNumber(string num)
        {
            if (num == null)
            {
                return false;
            }
            if (num == string.Empty)
            {
                return false;
            }
            if (num == "0")
            {
                return true;
            }
            Regex rg = new Regex("^[0-9]*[1-9][0-9]*$");
            if (rg.IsMatch(num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?/d*[.]?/d*$");
        }
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?/d*$");
        }
        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^/d*[.]?/d*$");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDataTimeRandomFileName()
        {
            Random r = new Random(1000);
            return DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + r.Next().ToString();
        }

        public static string GenRndString(int len)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();
            char[] charArray = str.ToCharArray();
            int number;
            char code;
            string rndCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                code = charArray[number % 50];

                rndCode += code.ToString();
            }

            return rndCode;
        }



        public static void ArrRandomList(ref string[] arr)
        {
            Random ran = new Random();
            int k = 0;
            string strtmp = "";
            for (int i = 0; i < arr.Length; i++)
            {

                k = ran.Next(0, 3);
                if (k != i)
                {
                    strtmp = arr[i];
                    arr[i] = arr[k];
                    arr[k] = strtmp;
                }
            }

        }

        public static string GenRndNum(int len)
        {
            string str = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15";
            string[] strArray = str.Split(new char[] { ',' });
            int number;
            string s = string.Empty;
            string strCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                s = strArray[number % 15];
                if (s == "0")
                    s = "2";
                strCode += s;
            }

            return strCode;
        }

        /// <summary>
        /// 判断参数是否为空，是否是数字类型字符
        /// </summary>
        /// <param name="ID"></param>
        public static void ValidID(string ID)
        {
            if (ID.Trim() == "" || !IsNumStr(ID))
                throw new Exception("参数传递错误!");
        }

        public static int DateDiff(string unit, DateTime startDate, DateTime endDate)
        {
            int retValue = 0;
            int diffYear = endDate.Year - startDate.Year;
            int diffMonth = endDate.Month - startDate.Month;
            int diffDay = endDate.Day - startDate.Day;
            int diffHour = endDate.Hour - startDate.Hour;
            int diffMinute = endDate.Minute - startDate.Minute;
            int diffSecond = endDate.Second - startDate.Second;
            if (unit == "year")
            {
                retValue = diffYear;
            }
            if (unit == "month")
            {
                retValue = diffYear * 12 + diffMonth;
            }
            if (unit == "day")
            {
                retValue = (diffYear * 12 + diffMonth) * 30 + diffDay;
            }
            if (unit == "hour")
            {
                retValue = ((diffYear * 12 + diffMonth) * 30 + diffDay) * 24 + diffHour;
            }
            if (unit == "minute")
            {
                retValue = (((diffYear * 12 + diffMonth) * 30 + diffDay) * 24 + diffHour) * 60 + diffMinute;
            }
            if (unit == "second")
            {
                retValue = ((((diffYear * 12 + diffMonth) * 30 + diffDay) * 24 + diffHour) * 60 + diffMinute) * 60;
            }
            return retValue;
        }
        /// <summary>
        /// 精确计算两次时间之间的差值
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        private string DateDiffs(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            float nYear = ((float)ts.Days) / 365;
            float Minutes = ((float)ts.Minutes) * 60;
            float Seconds = ((float)ts.Seconds);
            float s = Minutes + Seconds;
            return s.ToString();
        }
        public static string GetClientIP()
        {

            string lastIP = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                lastIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                lastIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            if (lastIP == "::1") lastIP = "127.0.0.1";
            return lastIP;
        }

        /// <summary>
        /// 按指定长度截取字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSpeStr(string str, int slen, bool showDot)
        {
            if (str.Length > slen)
            {
                if (showDot)
                    str = str.Substring(0, slen) + "...";
                else
                    str = str.Substring(0, slen);
            }
            return str;
        }

        public static string JsFilter(string str, string exclueStr)
        {
            if (exclueStr.IndexOf("<script>,") < 0)
            {
                str = str.Replace("<script>", "");
            }
            if (exclueStr.IndexOf("</script>,") < 0)
            {
                str = str.Replace("</script>", "");
            }
            if (exclueStr.IndexOf("javascript,") < 0)
            {
                str = str.Replace("javascript", "");
            }
            if (exclueStr.IndexOf("/,") < 0)
            {
                str = str.Replace("/", "");
            }
            if (exclueStr.IndexOf("',") < 0)
            {
                str = str.Replace("'", "");
            }
            if (exclueStr.IndexOf(";,") < 0)
            {
                str = str.Replace(";", "");
            }
            if (exclueStr.IndexOf("&,") < 0)
            {
                str = str.Replace("&", "");
            }
            if (exclueStr.IndexOf("#,") < 0)
            {
                str = str.Replace("#", "");
            }
            return str;
        }

        /// 转全角的函数(SBC case)
        ///
        ///任意字符串
        ///全角字符串
        ///
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///
        private static String ToSBC(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }

        /**/
        // /
        // / 转半角的函数(DBC case)
        // /
        // /任意字符串
        // /半角字符串
        // /
        // /全角空格为12288，半角空格为32
        // /其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        // /
        private static String ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }


        /// <summary>
        /// 特殊字符全角编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TSEncode(string input, string exclueStr)
        {
            string ret = input;
            if (exclueStr.IndexOf("',") < 0)
            {
                ret = ret.Replace("'", ToSBC("'"));
            }
            if (exclueStr.IndexOf("%,") < 0)
            {
                ret = ret.Replace("%", ToSBC("%"));
            }
            if (exclueStr.IndexOf(" ,") < 0)
            {
                ret = ret.Replace(" ", ToSBC(" "));
            }
            if (exclueStr.IndexOf("-,") < 0)
            {
                ret = ret.Replace("-", ToSBC("-"));
            }
            if (exclueStr.IndexOf("+,") < 0)
            {
                ret = ret.Replace("+", ToSBC("+"));
            }
            if (exclueStr.IndexOf("&,") < 0)
            {
                ret = ret.Replace("&", ToSBC("&"));
            }
            if (exclueStr.IndexOf("$,") < 0)
            {
                ret = ret.Replace("$", ToSBC("$"));
            }
            if (exclueStr.IndexOf(".,") < 0)
            {
                ret = ret.Replace(".", ToSBC("."));
            }
            if (exclueStr.IndexOf("=,") < 0)
            {
                ret = ret.Replace("=", ToSBC("="));
            }
            if (exclueStr.IndexOf("(,") < 0)
            {
                ret = ret.Replace("(", ToSBC("("));
            }
            if (exclueStr.IndexOf("),") < 0)
            {
                ret = ret.Replace(")", ToSBC(")"));
            }
            if (exclueStr.IndexOf("<,") < 0)
            {
                ret = ret.Replace("<", ToSBC("<"));
            }
            if (exclueStr.IndexOf(">,") < 0)
            {
                ret = ret.Replace(">", ToSBC(">"));
            }
            if (exclueStr.IndexOf("@,") < 0)
            {
                ret = ret.Replace("@", ToSBC("@"));
            }
            if (exclueStr.IndexOf("#,") < 0)
            {
                ret = ret.Replace("#", ToSBC("#"));
            }
            if (exclueStr.IndexOf(",,") < 0)
            {
                ret = ret.Replace(",", ToSBC(","));
            }
            if (exclueStr.IndexOf(":,") < 0)
            {
                ret = ret.Replace(":", ToSBC(":"));
            }
            if (exclueStr.IndexOf(";,") < 0)
            {
                ret = ret.Replace(";", ToSBC(";"));
            }
            if (exclueStr.IndexOf("!,") < 0)
            {
                ret = ret.Replace("!", ToSBC("!"));
            }
            if (exclueStr.IndexOf("|,") < 0)
            {
                ret = ret.Replace("|", ToSBC("|"));
            }
            return ret;
        }


        /// <summary>
        /// 过滤html字符
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="exclueStr">"</param>
        /// <returns></returns>

        public static string checkStrhtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = regex7.Replace(html, ""); //过滤frameset 
            html = regex8.Replace(html, ""); //过滤frameset 
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }




        public static bool eMail_Validate(string emailString)
        {
            string emailReg = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            return Regex.IsMatch(emailString, emailReg);
        }


        public static string HttpWebRequest_WebRequest_Post_JSON(string strURL, string strJSON)
        {
            WebRequest httpWebRequest = WebRequest.Create(strURL);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

            streamWriter.Write(strJSON);
            streamWriter.Flush();
            streamWriter.Close();

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            string resultstring = streamReader.ReadToEnd();
            streamReader.Close();
            //Eggsoft.Common.JsUtil.ShowMsg(resultstring);
            return resultstring;

        }


        public static string HttpWebRequest_WebRequest_GET_WeiXin_YiXin_Json(string strURL)
        {
            HttpWebRequest webRequest2 = (HttpWebRequest)WebRequest.Create(strURL);
            webRequest2.ContentType = "text/html; charset=UTF-8";
            webRequest2.Method = "GET";
            webRequest2.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
            webRequest2.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse response2 = (HttpWebResponse)webRequest2.GetResponse();


            StreamReader sr2 = new StreamReader(response2.GetResponseStream(), Encoding.Default);
            string text2 = sr2.ReadToEnd();
            sr2.Close();
            return text2;
        }


        public static string HttpWebRequest_WebRequest_GET_JSON(string strURL)
        {

            // debug_Log.Call_WriteLog("HttpWebRequest_WebRequest_GET_JSON(string strURL)=" + strURL);

            WebRequest req = WebRequest.Create(strURL);
            WebResponse res = req.GetResponse();    // GetResponse blocks until the response arrives
            Stream ReceiveStream = res.GetResponseStream();    // Read the stream into a string
            StreamReader sr = new StreamReader(ReceiveStream);
            string resultstring = sr.ReadToEnd();
            sr.Close();
            return resultstring;
        }



        public static string HttpWebRequest_WebRequest_Post(string strURL)
        {
            HttpWebRequest webRequest2 = (HttpWebRequest)WebRequest.Create(strURL);
            webRequest2.ContentType = "text/html; charset=UTF-8";
            webRequest2.Method = "POST";
            webRequest2.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
            webRequest2.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse response2 = (HttpWebResponse)webRequest2.GetResponse();


            StreamReader sr2 = new StreamReader(response2.GetResponseStream(), Encoding.Default);
            string text2 = sr2.ReadToEnd();
            sr2.Close();
            return text2;
        }

        public static string RangerUsControl(string controlName, string strAllParam)
        {

            string result = "";

            StringBuilder build = new StringBuilder();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(new StringWriter(build));
            UserControl uc = new UserControl();
            Control ctrl = uc.LoadControl(controlName + "");//加载用户定义控件 TextBox txtBox1 = ctrl.FindControl("TextBox1") as TextBox;//获得id为“TextBox1”的控件 



            try
            {
                ctrl.ID = strAllParam;
                ctrl.RenderControl(htmlWriter);

            }
            catch
            { }
            finally
            {
                htmlWriter.Flush();
                result = build.ToString();
            }
            return result;//返回控件的HTML代码 
        }

        /*接收post数据
         string url=http://liuleiceshi.hexun.com/payment/WebForm1.aspx;
        string stext = HttpRequestFromPost(url, "s=1213213&t=1111");
    */
        private string HttpRequestFromPost(string maiurl, string paramurl)
        {
            string strHtmlContent = "";
            HttpWebRequest request;
            try
            {
                Encoding encoding = Encoding.GetEncoding("GB2312");
                //声明一个HttpWebRequest请求
                request = (HttpWebRequest)WebRequest.Create(maiurl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = true;
                byte[] Postbyte = Encoding.ASCII.GetBytes(paramurl);
                request.ContentLength = Postbyte.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(Postbyte, 0, Postbyte.Length);//把参数用流对象写入request对象中   
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获得服务器响应对象  
                Stream resStream = response.GetResponseStream();//转成流对象   
                StreamReader sr = new StreamReader(resStream, encoding);
                strHtmlContent = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                //help.log("网支中连接接口出现问题,错误信息：" + help.SetErrorInfo(ex.Message));
            }
            finally
            {
                request = null;
            }
            if (strHtmlContent == null)
                strHtmlContent = "";
            return strHtmlContent;
        }
        /*
        接收get数据
              string url="http://liuleiceshi.hexun.com/payment/WebForm1.aspx";
              string stext = help.GetHtmlContent(url);
        */
        public string GetHtmlContent(string Url)
        {
            string strHtmlContent = "";
            HttpWebRequest request;
            try
            {
                //声明一个HttpWebRequest请求
                request = (HttpWebRequest)WebRequest.Create(Url);
                //连接超时时间
                request.Timeout = 20000;
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamHtmlCode = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamHtmlCode, encoding);
                strHtmlContent = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                //help.log("网支中连接接口出现问题,错误信息：" + SetErrorInfo(ex.Message));
            }
            finally
            {
                request = null;
            }
            if (strHtmlContent == null)
                strHtmlContent = "";
            return strHtmlContent;
        }


        ///<summary>
        /// 从一段网页源码中获取正文
        ///</summary>
        ///<param name="input"></param>
        ///<returns></returns>
        public static string GetMainContent(string strinput)
        {
            string pattern = @"^[\u300a\u300b]|[\u4e00-\u9fa5]|[\uFF00-\uFFEF]";
            //string str = "dfa#445发了,. 。*(*&*^e4444";
            string v = "";
            if (System.Text.RegularExpressions.Regex.IsMatch(strinput, pattern))
            {
                //提示的代码在这里写
                System.Text.RegularExpressions.Match m =
                System.Text.RegularExpressions.Regex.Match(strinput, pattern);
                while (m.Success)
                {
                    if (m.Value == ",")
                    {
                        v += m.Value;
                        continue;
                    }
                    v += m.Value;
                    m = m.NextMatch();
                }
                //Response.Write(v);
            }
            v = v.Replace("微软雅黑", "").Replace("宋体", "").Replace("黑体", "");
            return v;
        }
        //}

    }
}
