using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Eggsoft.Common
{
    public class StringNum
    {

        /*
         URL中的域名即是“http://”与第一个“/”之间的部分，这样我们就可以用一下正表达式获取

(?<=http://).+?(?=/)

(?<=http://)即是匹配“http://”后面的部分，(?=/)即是匹配“/”前面的部分，因为一个URL中不只一个“/”，如http://www.yizu.org/bcxg/270.html，为了不匹配成www.yizu.org/bcxg，这里需要把匹配设置成非贪婪模式，即尽可能少地匹配，所以匹配域名的部分为.+?，后面的？就是设置成非贪婪模式（默认是贪婪模式）。为了能够匹配https、ftp等URL可以改成

(?<=://).+?(?=/)

用C#实现的代码如下：
         
         */
        static public string ym(string url)
        {
            Regex r = new Regex("(?<=://).+?(?=/)");
            Match m = r.Match(url);
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取条形码数字 
        /// </summary>
        /// <param name="Arg12code"></param>
        /// <returns></returns>
        public static String getBareanCode(String Arg12code)
        {
            int c1 = 0;
            int c2 = 0;
            for (int i = 0; i < Arg12code.Length; i += 2)
            {
                char c = Arg12code[i];
                //字符串code中第i个位置上的字符 
                int n = c - '0';
                c1 += n;//累加奇数位的数字和   
            }
            for (int i = 1; i < Arg12code.Length; i += 2)
            {
                char c = Arg12code[i];//字符串code中第i个位置上的字符
                int n = c - '0';
                c2 += n;//累加偶数位的数字和   
            }
            int cc = c1 + c2 * 3;
            int check = cc % 10;
            check = (10 - cc % 10) % 10;
            string Return = Arg12code + check + "";
            return Return;
        }



        static public string Add000000Num(int intNum, int intLength)
        {
            String strAdd000000Num = "";

            if (intNum.ToString().Length >= intLength)
            {
                strAdd000000Num = intNum.ToString();
            }
            else
            {
                for (int i = intLength; i > intNum.ToString().Length; i = (i - 1))
                {
                    strAdd000000Num += "0";
                }
                strAdd000000Num = strAdd000000Num + intNum.ToString();
            }


            return strAdd000000Num;

        }

        static public string[] OnlyGetOneList_NoRerpeat(string[] StringgetparentList)
        {
            String strAddNum = "";


            for (int k = 0; k < StringgetparentList.Length; k++)
            {
                if (string.IsNullOrEmpty(StringgetparentList[k])==false)
                {
                    if (strAddNum.IndexOf("," + StringgetparentList[k]) == -1)
                    {
                        strAddNum += "," + StringgetparentList[k];
                    }
                }
            }
            return strAddNum.Split(',');
        }

        static public string MaxLengthString(String strString, int intLength)
        {
            if (strString.Length > intLength)
            {
                strString = strString.Substring(0, intLength - 2) + "..";            
            }
            return strString;
        }

        private static char[] constant =   
        {   
            '0','1','2','3','4','5','6','7','8','9'
            //,  
            //'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
            //'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
        };
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }

    }
}
