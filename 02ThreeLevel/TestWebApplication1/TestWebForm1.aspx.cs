using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace TestWebApplication1
{
    public partial class TestWebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<string xmlns=\"http://tempuri.org/\">SUCCESS</string>";
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXML);
            XmlNamespaceManager xnm = new XmlNamespaceManager(doc.NameTable);
            xnm.AddNamespace("mxh", "http://tempuri.org/");

            XmlNode dddd= doc.SelectSingleNode(@"/mxh:string", xnm);
            if (dddd != null) {
                string STRtEXT = dddd.InnerText;
            }


            //匿名对象解析
            // var tempEntity = new { ID = 0, Name = string.Empty };
            // string json5 = JsonHelper.SerializeObject(tempEntity);
            // //json5 : {"ID":0,"Name":""}
            // tempEntity = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempEntity);
            // //var tempStudent = new Student();
            // //tempStudent = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempStudent);


            // //int intddd = 0;
            // //intddd = 9869554;

            // Int64 Int64HaoMa = Int64.Parse("15");
            // int fffff = 7;
            ////-- int dddd = Int64HaoMa % fffff;

            // Comput mmmmComput = new Comput();
            // string[] strrrList = mmmmComput.BigNumDiv("15508101120212711", "7");

            // int iiii = (int)Int64HaoMa % 7;
        }
    }


    class Comput
    {
        //64位机字长表示的最大十进制数长度为19
        private readonly int MAX_LEN = 19;

        /// <summary>
        /// 大数(整型)相加
        /// </summary>
        /// <param name="strNum1">被加数</param>
        /// <param name="strNum2">加数</param>
        /// <returns>返回相加结果</returns>
        public string BigNumAdd(string strNum1, string strNum2)
        {
            if (null == strNum1 || null == strNum2)
            {
                return string.Empty;
            }

            bool _isNum1 = Regex.IsMatch(strNum1, @"^[0-9]|^-[0-9]");
            bool _isNum2 = Regex.IsMatch(strNum2, @"^[0-9]|^-[0-9]");
            if (!_isNum1 || !_isNum2)
            {
                return "输入的参数不全是数字";
            }

            bool _is_Negative_Add = false;
            if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1和strnum2都是负数
                _is_Negative_Add = true;
                strNum1 = strNum1.Substring(1, strNum1.Length - 1);
                strNum2 = strNum2.Substring(1, strNum2.Length - 1);
            }
            else if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 != string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是负数,strnum2是正数
                return BigNumSub(strNum2, strNum1.Substring(1, strNum1.Length - 1));
            }
            if (0 != string.Compare(strNum1.Substring(0, 1), "-") &&
                0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是正数,strnum2是负数
                return BigNumSub(strNum1, strNum2.Substring(1, strNum2.Length - 1));
            }

            if (1 < strNum1.Length)
            {
                strNum1 = strNum1.TrimStart('0');
            }
            if (1 < strNum2.Length)
            {
                strNum2 = strNum2.TrimStart('0');
            }

            long _lngNum1 = 0;
            long _lngNum2 = 0;
            int _intlen1 = strNum1.Length;
            int _intlen2 = strNum2.Length;
            if (_intlen1 < MAX_LEN && _intlen2 < MAX_LEN)
            {
                _lngNum1 = Convert.ToInt64(strNum1);
                _lngNum2 = Convert.ToInt64(strNum2);
                if (_is_Negative_Add)
                {
                    return string.Format("-{0}", (_lngNum1 + _lngNum2).ToString());
                }
                else
                {

                    return (_lngNum1 + _lngNum2).ToString();
                }
            }

            char[] _chrs1 = strNum1.ToArray();
            char[] _chrs2 = strNum2.ToArray();
            int k1 = _chrs1.Length, k2 = _chrs2.Length, k = k1 >= k2 ? k1 + 1 : k2 + 1;

            int[] num1 = new int[k];
            int[] num2 = new int[k];

            if (k1 == k2)
            {
                for (int i = k - 1; i >= 1; i--)
                {
                    num1[i] = Convert.ToInt32(_chrs1[i - 1]) - 48;
                    num2[i] = Convert.ToInt32(_chrs2[i - 1]) - 48;
                }
            }
            else if (k1 > k2)
            {
                for (int i = k - 1; i >= 1; i--)
                {
                    num1[i] = Convert.ToInt32(_chrs1[i - 1]) - 48;
                    if (i - (k1 - k2) - 1 >= 0)
                    {
                        num2[i] = Convert.ToInt32(_chrs2[i - (k1 - k2) - 1]) - 48;
                    }
                }
            }
            else if (k1 < k2)
            {
                for (int i = k - 1; i >= 1; i--)
                {
                    num1[i] = Convert.ToInt32(_chrs2[i - 1]) - 48;
                    if (i - (k2 - k1) - 1 >= 0)
                    {
                        num2[i] = Convert.ToInt32(_chrs1[i - (k2 - k1) - 1]) - 48;
                    }
                }
            }

            string t = string.Empty;
            int carry = 0;
            int value = 0;
            for (int i = k - 1; i >= 0; i--)
            {
                t = (num1[i] + num2[i] + carry).ToString();
                if (t.Length > 1)
                {
                    carry = Convert.ToInt32(t[0]) - 48;
                    value = Convert.ToInt32(t[1]) - 48;
                }
                else
                {
                    carry = 0;
                    value = Convert.ToInt32(t[0]) - 48;
                }
                num1[i] = value;
            }
            StringBuilder _strR = new StringBuilder();
            for (int i = 0; i < k; i++)
            {
                _strR.Append(num1[i]);
            }
            if (_is_Negative_Add)
            {
                return string.Format("-{0}", _strR.ToString().TrimStart('0'));
            }
            return _strR.ToString().TrimStart('0');
        }

        /// <summary>
        /// 大数（整型）相减
        /// </summary>
        /// <param name="strNum1">被减数</param>
        /// <param name="strNum2">减数</param>
        /// <returns>返回相减结果</returns>
        public string BigNumSub(string strNum1, string strNum2)
        {
            if (null == strNum1 || null == strNum2)
            {
                return string.Empty;
            }

            bool _isNum1 = Regex.IsMatch(strNum1, @"^[0-9]|^-[0-9]");
            bool _isNum2 = Regex.IsMatch(strNum2, @"^[0-9]|^-[0-9]");
            if (!_isNum1 || !_isNum2)
            {
                return "输入的参数不全是数字";
            }

            if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strNum1和strNum2都是负数
                string _t = strNum1.Substring(1, strNum1.Length - 1);
                strNum1 = strNum2.Substring(1, strNum2.Length - 1);
                strNum2 = _t;
            }
            else if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 != string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是负数,strnum2是正数
                return string.Format("-{0}",
                    BigNumAdd(strNum1.Substring(1, strNum1.Length - 1), strNum2));
            }
            else if (0 != string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是正数,strnum2是负数
                return BigNumAdd(strNum1, strNum2.Substring(1, strNum2.Length - 1));
            }

            long _lngNum1 = 0;
            long _lngNum2 = 0;
            int _intlen1 = strNum1.Length;
            int _intlen2 = strNum2.Length;
            if (_intlen1 < MAX_LEN && _intlen2 < MAX_LEN)
            {
                _lngNum1 = Convert.ToInt64(strNum1);
                _lngNum2 = Convert.ToInt64(strNum2);
                return (_lngNum1 - _lngNum2).ToString();

            }

            if (1 < strNum1.Length)
            {
                strNum1 = strNum1.TrimStart('0');
            }
            if (1 < strNum2.Length)
            {
                strNum2 = strNum2.TrimStart('0');
            }
            if (0 == string.Compare(strNum1, strNum2))
            {
                return "0";
            }
            char[] _chrs1 = strNum1.ToArray();
            char[] _chrs2 = strNum2.ToArray();
            int k1 = _chrs1.Length, k2 = _chrs2.Length, k = k1 > k2 ? k1 : k2;

            int[] num1 = new int[k];
            int[] num2 = new int[k];

            bool _isSwap = false;

            if (k1 == k2)
            {
                for (int i = 0; i < k; i++)
                {
                    if (Convert.ToInt32(_chrs1[i]) - 48 < Convert.ToInt32(_chrs2[i]) - 48)
                    {
                        _isSwap = true;
                        break;
                    }
                }

                if (!_isSwap)
                {
                    for (int i = k - 1; i >= 0; i--)
                    {
                        num1[i] = Convert.ToInt32(_chrs1[i]) - 48;
                        num2[i] = Convert.ToInt32(_chrs2[i]) - 48;
                    }
                }
                else
                {
                    for (int i = k - 1; i >= 0; i--)
                    {
                        num1[i] = Convert.ToInt32(_chrs2[i]) - 48;
                        num2[i] = Convert.ToInt32(_chrs1[i]) - 48;
                    }
                }
            }
            else if (k1 > k2)
            {
                for (int i = k - 1; i >= 0; i--)
                {
                    num1[i] = Convert.ToInt32(_chrs1[i]) - 48;
                    if (i - (k1 - k2) >= 0)
                    {
                        num2[i] = Convert.ToInt32(_chrs2[i - (k1 - k2)]) - 48;
                    }
                }
            }
            else if (k1 < k2)
            {
                _isSwap = true;
                for (int i = k - 1; i >= 0; i--)
                {
                    if (i - (k2 - k1) >= 0)
                    {
                        num2[i] = Convert.ToInt32(_chrs1[i - (k2 - k1)]) - 48;
                    }
                    num1[i] = Convert.ToInt32(_chrs2[i]) - 48;
                }
            }

            int d = 0;
            int borrow = 0;
            int value = 0;
            int index = 0;
            for (int i = k - 1; i >= 0; i--)
            {
                d = num1[i] - num2[i];
                if (d < 0)
                {
                    borrow = 1;

                    index = i - 1;
                    //处理借位
                    while (index >= 0)
                    {
                        if (num1[index] != 0)
                        {
                            num1[index] -= borrow;
                            break;
                        }
                        else
                        {
                            num1[index] = 9;
                        }
                        index--;
                    }

                    value = 10 + num1[i] - num2[i];
                }
                else
                {
                    value = d;
                }
                num1[i] = value;
            }
            StringBuilder _strR = new StringBuilder();
            for (int i = 0; i < k; i++)
            {
                _strR.Append(num1[i]);
            }
            return _isSwap
                ? string.Format("-{0}", _strR.ToString().TrimStart('0'))
                : _strR.ToString().TrimStart('0');
        }

        /// <summary>
        /// 大数（整数）相乘
        /// </summary>
        /// <param name="strNum1">被乘数</param>
        /// <param name="strNum2">乘数</param>
        /// <returns>返回相乘结果</returns>
        public string BigNumMul(string strNum1, string strNum2)
        {
            if (null == strNum1 || null == strNum2)
            {
                return string.Empty;
            }

            bool _isNum1 = Regex.IsMatch(strNum1, @"^[0-9]|^-[0-9]");
            bool _isNum2 = Regex.IsMatch(strNum2, @"^[0-9]|^-[0-9]");
            if (!_isNum1 || !_isNum2)
            {
                return "输入的参数不全是数字";
            }

            strNum1 = strNum1.TrimStart('0');

            if (string.IsNullOrEmpty(strNum1.Trim()))
            {
                return "0";
            }

            bool _hasFlag = false;
            if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strNum1和strNum2都是负数
                strNum1 = strNum1.Substring(1, strNum1.Length - 1);
                strNum2 = strNum2.Substring(1, strNum2.Length - 1);
            }
            else if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 != string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是负数,strnum2是正数
                _hasFlag = true;
                strNum1 = strNum1.Substring(1, strNum1.Length - 1);
            }
            else if (0 != string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是正数,strnum2是负数
                _hasFlag = true;
                strNum2 = strNum2.Substring(1, strNum2.Length - 1);
            }

            long _lngNum1 = 0;
            long _lngNum2 = 0;
            int _intlen1 = strNum1.Length;
            int _intlen2 = strNum2.Length;
            if (_intlen1 + _intlen2 <= MAX_LEN)
            {
                _lngNum1 = Convert.ToInt64(strNum1);
                _lngNum2 = Convert.ToInt64(strNum2);
                if (_hasFlag)
                {
                    return string.Format("-{0}", (_lngNum1 * _lngNum2).ToString());
                }
                else
                {
                    return (_lngNum1 * _lngNum2).ToString();
                }
            }

            if (1 < strNum1.Length)
            {
                strNum1 = strNum1.TrimStart('0');
            }
            if (1 < strNum2.Length)
            {
                strNum2 = strNum2.TrimStart('0');
            }
            //转换参数类型
            char[] chrs1 = strNum1.ToArray();
            int[] nums1 = new int[chrs1.Length];
            for (int i = 0; i < chrs1.Length; i++)
            {
                nums1[i] = Convert.ToInt32(chrs1[i]) - 48;
            }

            char[] chrs2 = strNum2.ToArray();
            int[] nums2 = new int[chrs2.Length];
            for (int i = 0; i < chrs2.Length; i++)
            {
                nums2[i] = Convert.ToInt32(chrs2[i]) - 48;
            }

            int k1 = 0, k2 = 0;
            if (nums1.Length >= nums2.Length)
            {
                k1 = nums1.Length;
                k2 = nums2.Length;
            }
            else
            {
                k1 = nums2.Length;
                k2 = nums1.Length;
            }

            //初始化相乘项积的长度数组
            IList<int[]> _result = new List<int[]>();
            for (int i = 1; i <= k2; i++)
            {
                _result.Add(new int[k1 + 1 + k2]);
            }

            //计算相乘结果
            string t = string.Empty;
            int carry = 0;
            int value = 0;
            int k = 0;
            for (int i = k2 - 1; i >= 0; i--)
            {
                for (int j = k1 - 1; j >= 0; j--)
                {
                    t = (nums2[i] * nums1[j] + carry).ToString();
                    if (t.Length > 1)
                    {
                        carry = Convert.ToInt32(t[0]) - 48;
                        value = Convert.ToInt32(t[1]) - 48;
                    }
                    else
                    {
                        carry = 0;
                        value = Convert.ToInt32(t[0]) - 48;
                    }
                    _result[k2 - 1 - i][j + 1 + k2 - k] = value;
                    if (j == 0)
                    {
                        //最后一个进位须保留
                        _result[k2 - 1 - i][j + 1 + k2 - k - 1] = carry;
                    }
                }
                carry = 0;
                k++;
            }

            //for (int i = 0; i <= _result.Count - 1; i++)
            //{
            //    for (int j = 0; j < _result[i].Length; j++)
            //    {
            //        Console.Write(_result[i][j]);
            //    }
            //    Console.WriteLine();
            //}
            //将相乘结果进行求和
            carry = 0;
            k = k1 + k2 + 1;
            int[] _tmp = _result[0];

            for (int i = 1; i <= _result.Count - 1; i++)
            {
                for (int j = k - 1; j >= 0; j--)
                {
                    t = (_tmp[j] + _result[i][j] + carry).ToString();
                    if (t.Length > 1)
                    {
                        carry = Convert.ToInt32(t[0]) - 48;
                        value = Convert.ToInt32(t[1]) - 48;
                    }
                    else
                    {
                        carry = 0;
                        value = Convert.ToInt32(t[0]) - 48;
                    }
                    _tmp[j] = value;
                }
            }

            StringBuilder _str = new StringBuilder();
            for (int i = 0; i < k; i++)
            {
                _str.Append(_tmp[i]);
                //    Console.Write(_tmp[i]);
            }
            //Console.WriteLine();
            if (_hasFlag)
            {
                return string.Format("-{0}", _str.ToString().TrimStart('0'));
            }
            return _str.ToString().TrimStart('0');
        }

        /// <summary>
        /// 大数（整数）相除
        /// </summary>
        /// <param name="strNum1">被除数</param>
        /// <param name="strNum2">除数</param>
        /// <returns>返回相除结果（商和余）</returns>
        public string[] BigNumDiv(string strNum1, string strNum2)
        {
            string[] _r = new string[2];

            if (null == strNum1 || null == strNum2)
            {
                _r[0] = string.Empty;
                _r[1] = string.Empty;
                return _r;
            }

            bool _isNum1 = Regex.IsMatch(strNum1, @"^[0-9]|^-[0-9]");
            bool _isNum2 = Regex.IsMatch(strNum2, @"^[0-9]|^-[0-9]");
            if (!_isNum1 || !_isNum2)
            {
                _r[0] = "输入的参数不全是数字";
                _r[1] = "";
                return _r;
            }

            bool _hasFlag = false;
            if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strNum1和strNum2都是负数
                strNum1 = strNum1.Substring(1, strNum1.Length - 1);
                strNum2 = strNum2.Substring(1, strNum2.Length - 1);
            }
            else if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 != string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是负数,strnum2是正数
                _hasFlag = true;
                strNum1 = strNum1.Substring(1, strNum1.Length - 1);
            }
            else if (0 != string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是正数,strnum2是负数
                _hasFlag = true;
                strNum2 = strNum2.Substring(1, strNum2.Length - 1);
            }

            _r = BigNumDiv_(strNum1, strNum2);

            if (_hasFlag)
            {
                _r[0] = string.Format("-{0}", _r[0]);
            }

            return _r;
        }

        /// <summary>
        /// 大数（整数）相除
        /// </summary>
        /// <param name="strNum1">被除数</param>
        /// <param name="strNum2">除数</param>
        /// <returns>返回相除结果（商和余）</returns>
        private string[] BigNumDiv_(string strNum1, string strNum2)
        {
            string[] _r = new string[2];

            if (1 < strNum1.Length)
            {
                strNum1 = strNum1.TrimStart('0');
            }
            if (1 < strNum2.Length)
            {
                strNum2 = strNum2.TrimStart('0');
            }

            strNum1 = strNum1.TrimStart('0');

            if (string.IsNullOrEmpty(strNum1.Trim()))
            {
                _r[0] = "0";
                _r[1] = "0";
                return _r;
            }

            int _intLen1 = strNum1.Length;
            int _intLen2 = strNum2.Length;
            string _strTemp = string.Empty;
            if (_intLen1 < _intLen2)
            {
                _strTemp = strNum1;
                strNum1 = strNum2;
                strNum2 = _strTemp;
                _intLen1 = strNum1.Length;
                _intLen2 = strNum2.Length;
            }
            else if (_intLen1 == _intLen2)
            {
                //如果两数完全相同
                if (0 == string.Compare(strNum1, strNum2))
                {
                    _r[0] = "1";
                    _r[1] = "0";
                    return _r;
                }
                Int32 _firstNum1 = 0;
                Int32 _firstNum2 = 0;
                int i = 0;
                bool _isSwap = true;
                while (i + 1 <= _intLen1)
                {
                    _firstNum1 = Convert.ToInt32(strNum1.Substring(i, 1));
                    _firstNum2 = Convert.ToInt32(strNum2.Substring(i, 1));
                    if (_firstNum1 > _firstNum2)
                    {
                        _isSwap = false;
                        break;
                    }
                    i++;
                }
                if (_isSwap)
                {
                    _strTemp = strNum1;
                    strNum1 = strNum2;
                    strNum2 = _strTemp;
                    _intLen1 = strNum2.Length;
                    _intLen2 = strNum1.Length;
                }
            }

            strNum2 = strNum2.TrimStart('0');

            if (string.IsNullOrEmpty(strNum2.Trim()))
            {
                _r[0] = "除数不能为0";
                _r[1] = "无法计算出余数";
                return _r;
            }

            long num1 = 0;
            long num2 = 0;
            if (_intLen1 <= MAX_LEN - 2 && _intLen2 <= MAX_LEN - 2)
            {
                num1 = Convert.ToInt64(strNum1);
                num2 = Convert.ToInt64(strNum2);
                _r[0] = (num1 / num2).ToString();
                _r[1] = (num1 % num2).ToString();
                return _r;
            }

            //设置试算的范围
            if (_intLen1 >= MAX_LEN)
            {
                _intLen1 = MAX_LEN - 2;
            }
            else if (_intLen1 == 1)
            {
                _intLen1 = 1;
            }

            if (_intLen2 > MAX_LEN)
            {
                _intLen2 = MAX_LEN - 3;
            }
            else if (_intLen2 == MAX_LEN)
            {
                _intLen2 = MAX_LEN - 2;
            }
            else if (_intLen2 == 1)
            {
                _intLen2 = 1;
            }

            //定义ax+b=c中的四个变量
            string a = string.Empty;
            string x = string.Empty;
            string b = string.Empty;
            string c = string.Empty;


            //试算过程
            num1 = Convert.ToInt64(strNum1.Substring(0, _intLen1));
            num2 = Convert.ToInt64(strNum2.Substring(0, _intLen2));
            x = (num1 / num2).ToString();
            a = num2.ToString();
            b = (num1 % num2).ToString();
            c = num1.ToString();

            int k = strNum2.Length - _intLen2;
            StringBuilder strnum2_ = new StringBuilder("1");
            for (int i = 1; i <= k; i++)
            {
                strnum2_.Append("0");
            }

            if (1 < strnum2_.Length)
            {
                a = BigNumMul(a, strnum2_.ToString());
                b = BigNumMul(b, strnum2_.ToString());

                c = BigNumMul(c, strnum2_.ToString());

                //计算“试算的除数”与“实际除数”的差值
                string num2__ = BigNumSub(strNum2, a);
                //赋于a变量为“真实除数”
                a = strNum2;
                //赋于b为“试算的余数”与“试算倍数*差值”的差值
                string b_ = BigNumMul(x, num2__);
                b = BigNumSub(b, b_);
            }

            k = strNum1.Length - c.Length;
            StringBuilder strnum1_ = new StringBuilder("1");
            for (int i = 1; i <= k; i++)
            {
                strnum1_.Append("0");
            }

            if (1 < strnum1_.Length)
            {
                x = BigNumMul(x, strnum1_.ToString());
                b = BigNumMul(b, strnum1_.ToString());
                c = BigNumMul(c, strnum1_.ToString());
            }

            //计算“试算的被除数”与“实际除数”的差值
            string num1__ = BigNumSub(strNum1, c);
            //赋于c变量为“真实被除数”
            c = strNum1;
            //赋于b值为在b值基出上累加的变量num1__的值
            b = BigNumAdd(b, num1__);


            //判断b值（试算余数）是否小于等于a(实际除数)
            if (2 == BigNumCompare(a, b))
            {
                //如果余数大于商，则进行递归计算
                _r = BigNumDiv_(b, a);
                //累加商值
                _r[0] = BigNumAdd(x, _r[0]);
            }
            else
            {
                _r[0] = x;
                _r[1] = b;
            }
            return _r;
        }

        /// <summary>
        /// 大数（整数）比较
        /// </summary>
        /// <param name="strNum1">大数1</param>
        /// <param name="strNum2">大数2</param>
        /// <returns>返回比较状态 0 两数相等 1=>大数1大于大数2 2=>大数1小于大数2 -1=>出错</returns>
        public int BigNumCompare(string strNum1, string strNum2)
        {
            if (null == strNum1 || null == strNum2)
            {
                return -1;
            }
            int flag = 0;
            bool _is_Negative_Compare = false;
            if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1和strnum2都是负数
                _is_Negative_Compare = true;
                strNum1 = strNum1.Substring(1, strNum1.Length - 1);
                strNum2 = strNum2.Substring(1, strNum2.Length - 1);
            }
            else if (0 == string.Compare(strNum1.Substring(0, 1), "-") &&
                     0 != string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是负数,strnum2是正数
                flag = 1;
            }
            if (0 != string.Compare(strNum1.Substring(0, 1), "-") &&
                0 == string.Compare(strNum2.Substring(0, 1), "-"))
            {
                //strnum1是正数,strnum2是负数
                flag = 2;
            }

            if (1 < strNum1.Length)
            {
                strNum1 = strNum1.TrimStart('0');
            }
            if (1 < strNum2.Length)
            {
                strNum2 = strNum2.TrimStart('0');
            }

            long _lngNum1 = 0;
            long _lngNum2 = 0;
            int _intlen1 = strNum1.Length;
            int _intlen2 = strNum2.Length;
            if (_intlen1 <= MAX_LEN - 2 && _intlen2 <= MAX_LEN - 2)
            {
                _lngNum1 = Convert.ToInt64(strNum1);
                _lngNum2 = Convert.ToInt64(strNum2);
                if (_lngNum1 > _lngNum2)
                {
                    flag = 1;
                }
                else if (_lngNum1 < _lngNum2)
                {
                    flag = 2;
                }
                if (_lngNum1 == _lngNum2)
                {
                    flag = 0;
                }
                return flag;
            }

            if (_intlen1 > _intlen2)
            {
                if (_is_Negative_Compare)
                {
                    flag = 2;
                }
                else
                {
                    flag = 1;
                }
            }
            else if (_intlen1 < _intlen2)
            {
                if (_is_Negative_Compare)
                {
                    flag = 1;
                }
                else
                {
                    flag = 2;
                }
            }
            else if (_intlen2 == _intlen1)
            {
                flag = 0;
            }
            return flag;
        }

        /// <summary>
        /// 判断两数是否互质
        /// </summary>
        /// <param name="strNum1">输入数1</param>
        /// <param name="strNum2">输入数2</param>
        /// <returns>true 互质 false 非互质</returns>
        public bool IsEachPrime(string strNum1, string strNum2)
        {
            bool flag = false;
            string divisor = string.Empty;
            string dividend = string.Empty;
            string str1 = "0";
            string str2 = "1";
            string[] _r = new string[2];
            while (true)
            {
                _r = BigNumDiv(dividend, divisor);
                if (0 == string.Compare(str1, _r[1]))
                {
                    flag = false;
                    break;
                }
                else if (0 == string.Compare(str2, _r[1]))
                {
                    flag = true;
                    break;
                }
                dividend = divisor;
                divisor = _r[1];
            }
            return flag;
        }

        /// <summary>
        /// 计算出两个数的最大公因子
        /// </summary>
        /// <param name="strNum1">整数1</param>
        /// <param name="strNum2">整数2</param>
        /// <returns>返回公因子</returns>
        public string gcd(string strNum1, string strNum2)
        {
            if (null == strNum1 || null == strNum2)
            {
                return string.Empty;
            }

            if (1 < strNum1.Length)
            {
                strNum1 = strNum1.TrimStart('0');
            }
            if (1 < strNum2.Length)
            {
                strNum2 = strNum2.TrimStart('0');
            }

            bool _isNum1 = Regex.IsMatch(strNum1, @"^[0-9]|^-[0-9]");
            bool _isNum2 = Regex.IsMatch(strNum2, @"^[0-9]|^-[0-9]");
            if (!_isNum1 || !_isNum2)
            {
                return "输入的参数不全是数字";
            }

            string _result = string.Empty;

            string divisor = string.Empty;
            string dividend = string.Empty;

            int _flag = BigNumCompare(strNum1, strNum2);

            if (1 == _flag)
            {
                dividend = strNum1;
                divisor = strNum2;
            }
            else if (2 == _flag)
            {
                dividend = strNum2;
                divisor = strNum1;
            }
            else if (-1 == _flag)
            {
                return "比较出错";
            }
            else if (0 == _flag)
            {
                return strNum1;
            }

            string str1 = "0";
            string str2 = "1";
            string[] _r = new string[2];
            while (true)
            {
                _r = BigNumDiv(dividend, divisor);
                if (0 == string.Compare(str1, _r[1]))
                {
                    _result = divisor;
                    break;
                }
                else if (0 == string.Compare(str2, _r[1]))
                {
                    _result = str2;
                    break;
                }
                dividend = divisor;
                divisor = _r[1];
            }

            return _result;
        }
    }

}