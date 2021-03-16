using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net;


//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    /**/
    /// <summary>


    public class IPPhone {
        public static bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,8]+\d{9}");
        }

    }

    public class IPCheck
    {
        #region  根据访问IP 查找用户的省份
        private class result
        {
            public string area { get; set; }
            public string location { get; set; }
        }
        private class IP_JuHe_Info
        {
            public string resultcode { get; set; }
            public string reason { get; set; }
            public result result { get; set; }
            public string error_code { get; set; }

        }
        /// <summary>
        /// 开放平台 微信支付 api  密钥 ia9K3n2WfGHoRwl1bQNqB2H5dOicS3wl
        //聚合数据账号https://www.juhe.cn/my/certify zhangtingfeng  00000257
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        static string GetShengFromIP(string ip)
        {
            string strKeyJuHe = "0cbb700923450e9a52945490fb503fdb";
            string strHttp = "http://apis.juhe.cn/ip/ip2addr?ip=" + ip + "&key=" + strKeyJuHe;
            string strJSON = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strHttp);
            IP_JuHe_Info HereIP_JuHe_Info = JsonHelper.JsonDeserialize<IP_JuHe_Info>(strJSON);
            return HereIP_JuHe_Info.result.area;
        }

        #endregion

        #region 只允许 一些IP地址访问
        //定义允许的IP端，格式如下
        static string[] AllowIPRanges = { "58.21.0.0-58.21.255.255",
"58.155.128.0-58.155.167.255",
"58.244.0.0-58.245.255.255",
"59.72.0.0-59.73.255.255",
"61.138.128.0-61.139.191.255",
"61.161.1.0-61.161.63.255",
"61.232.169.0-61.232.174.255",
"61.235.48.0-61.235.63.255",
"61.237.12.0-61.237.20.255",
"61.243.224.0-61.243.255.255",
"118.88.0.0-118.88.255.255",
"118.244.64.0-118.245.66.255",
"121.69.0.0-121.70.63.255",
"122.10.0.0-122.10.127.255",
"122.12.140.0-122.12.255.255",
"122.15.32.0-122.15.62.255",
"122.69.82.0-122.69.221.255",
"122.136.0.0-122.143.255.255",
"122.248.48.0-122.248.63.255",
"123.57.157.0-123.57.157.255",
"123.172.0.0-123.173.255.255",
"123.190.85.0-123.190.85.255",
"124.200.224.0-124.200.235.255",
"124.234.0.0-124.235.255.255",
"125.32.0.0-125.32.255.255",
"125.222.192.0-125.223.255.255",
"159.226.122.0-159.226.165.255",
"202.38.164.0-202.38.167.255",
"202.97.38.0-202.98.31.255",
"202.111.160.0-202.111.191.255",
"202.181.112.0-202.181.127.255",
"202.198.0.0-202.198.255.255",
"203.93.111.31-203.93.187.63",
"210.12.11.0-210.13.74.255",
"210.47.0.0-210.47.63.255",
"210.52.51.0-210.52.191.255",
"210.82.166.0-210.83.56.255",
"211.79.141.41-211.79.141.42",
"211.93.64.0-211.93.79.255",
"211.98.84.0-211.98.191.255",
"211.137.208.0-211.137.223.255",
"211.140.209.0-211.141.79.255",
"211.146.124.0-211.146.124.255",
"211.149.0.0-211.149.255.255",
"211.165.37.0-211.165.37.255",
"218.27.0.0-218.27.255.255",
"218.62.0.0-218.62.127.255",
"219.131.221.66-219.131.221.76",
"219.149.192.0-219.150.31.255",
"219.217.0.0-219.217.127.255",
"219.243.254.0-219.243.254.255",
"220.192.240.0-220.192.247.255",
"220.201.153.0-220.201.191.255",
"221.8.0.0-221.9.255.255",
"221.122.128.0-221.122.185.255",
"222.27.0.0-222.27.159.255",
"222.34.0.0-222.34.255.255",
"222.39.80.0-222.39.80.152",
"222.62.224.0-222.62.224.255",
"222.160.0.0-222.163.255.255",
"222.168.0.0-222.169.255.255",
"122.136.0.0-122.143.255.255",
"119.48.0.0-119.55.255.255",
"175.16.0.0-175.23.255.255",
"123.172.0.0-123.173.255.255",
"124.234.0.0-124.235.255.255",
"111.116.0.0-111.117.255.255",
"175.30.0.0-175.31.255.255",
"36.48.0.0-36.49.255.255",
"222.168.0.0-222.169.255.255",
"221.8.0.0-221.9.255.255",
"59.72.0.0-59.73.255.255",
"58.244.0.0-58.245.255.255",
"222.160.0.0-222.161.255.255",
"49.140.0.0-49.141.255.255",
"125.32.0.0-125.32.255.255",
"222.162.0.0-222.162.255.255",
"218.27.0.0-218.27.255.255",
"58.21.0.0-58.21.255.255",
"42.97.0.0-42.97.255.255",
"218.62.0.0-218.62.127.255",
"113.213.0.0-113.213.127.255",
"222.163.128.0-222.163.255.255",
"222.163.64.0-222.163.127.255",
"61.138.128.0-61.138.191.255",
"219.149.192.0-219.149.255.255",
"61.139.128.0-61.139.191.255",
"61.161.0.0-61.161.63.255",
"210.12.192.0-210.12.255.255",
"114.110.64.0-114.110.127.255",
"202.111.160.0-202.111.191.255",
"118.88.32.0-118.88.63.255",
"114.111.0.0-114.111.31.255",
"219.150.0.0-219.150.31.255",
"222.163.32.0-222.163.63.255",
"222.163.0.0-222.163.31.255",
"202.181.112.0-202.181.127.255",
"122.248.48.0-122.248.63.255",
"202.98.16.0-202.98.31.255",
"202.98.0.0-202.98.7.255",
"202.98.8.0-202.98.15.255",
"103.22.112.0-103.22.115.255"};


        //static string[] AllowIPRanges = { "10.0.0.0-10.255.255.255", "172.16.0.0-172.31.255.255", "192.168.0.0-192.168.255.255" };

        //主函数，调用判断接口
        public static bool MainShowCheck(string ip)
        {

            //判断192.168.100.0这个ip是否在指定的IP范围段内
            //就这个范围而言，如果把IP转换成long型的 那么192.167.0.0这个IP 将在10.0.0.0-10.255.255.255这个范围内，但实际上这是错误的。还希望高手指点将ip转换为long的内幕
            //Console.WriteLine(TheIpIsRange("61.141.70.123", AllowIPRanges));
            //Console.WriteLine("Done");
            //Console.Read();

            bool myMainShowCheck = TheIpIsRange(ip, AllowIPRanges);
            return myMainShowCheck;
        }


        //接口函数 参数分别是你要判断的IP  和 你允许的IP范围
        //（已经重载）
        //（允许同时指定多个数组）
        static bool TheIpIsRange(string ip, params string[] ranges)
        {
            bool tmpRes = false;
            foreach (var item in ranges)
            {
                if (TheIpIsRange(ip, item))
                {
                    tmpRes = true; break;
                }
            }

            return tmpRes;
        }

        /// <summary>
        /// 判断指定的IP是否在指定的IP范围内   这里只能指定一个范围
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="ranges"></param>
        /// <returns></returns>
        static bool TheIpIsRange(string ip, string ranges)
        {
            bool result = false;

            int count;
            string start_ip, end_ip;
            //检测指定的IP范围 是否合法
            TryParseRanges(ranges, out count, out start_ip, out end_ip);//检测ip范围格式是否有效

            if (ip == "::1") ip = "127.0.0.1";

            try
            {
                IPAddress.Parse(ip);//判断指定要判断的IP是否合法
            }
            catch (Exception)
            {
                throw new ApplicationException("要检测的IP地址无效");
            }

            if (count == 1 && ip == start_ip) result = true;//如果指定的IP范围就是一个IP，那么直接匹配看是否相等
            else if (count == 2)//如果指定IP范围 是一个起始IP范围区间
            {
                byte[] start_ip_array = Get4Byte(start_ip);//将点分十进制 转换成 4个元素的字节数组
                byte[] end_ip_array = Get4Byte(end_ip);
                byte[] ip_array = Get4Byte(ip);

                bool tmpRes = true;
                for (int i = 0; i < 4; i++)
                {
                    //从左到右 依次比较 对应位置的 值的大小  ，一旦检测到不在对应的范围 那么说明IP不在指定的范围内 并将终止循环
                    if (ip_array[i] > end_ip_array[i] || ip_array[i] < start_ip_array[i])
                    {
                        tmpRes = false; break;
                    }
                }
                result = tmpRes;
            }




            return result;
        }

        //尝试解析IP范围  并获取闭区间的 起始IP   (包含)
        private static void TryParseRanges(string ranges, out int count, out string start_ip, out string end_ip)
        {
            string[] _r = ranges.Split('-');
            if (!(_r.Length == 2 || _r.Length == 1))
                throw new ApplicationException("IP范围指定格式不正确，可以指定一个IP，如果是一个范围请用“-”分隔");

            count = _r.Length;

            start_ip = _r[0];
            end_ip = "";
            try
            {
                IPAddress.Parse(_r[0]);
            }
            catch (Exception)
            {
                throw new ApplicationException("IP地址无效");
            }

            if (_r.Length == 2)
            {
                end_ip = _r[1];
                try
                {
                    IPAddress.Parse(_r[1]);
                }
                catch (Exception)
                {
                    throw new ApplicationException("IP地址无效");
                }
            }
        }


        /// <summary>
        /// 将IP四组值 转换成byte型
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        static byte[] Get4Byte(string ip)
        {
            string[] _i = ip.Split('.');

            List<byte> res = new List<byte>();
            foreach (var item in _i)
            {
                res.Add(Convert.ToByte(item));
            }

            return res.ToArray();
        }
        #endregion

    }
}
