using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace Eggsoft.Common
{

    public class BaiduLocation
    {
        public string gpsx, gpsy;
        public string baidux, baiduy;
        public bool ok = false;
    }


    public class GPS
    {

        private static string ConvertBase64(String str)
        {
            //进行Base64解码
            byte[] xBuffer = Convert.FromBase64String(str);
            string strX = Encoding.UTF8.GetString(xBuffer, 0, xBuffer.Length);

            //byte[] yBuffer = Convert.FromBase64String(lat);
            //string strY = Encoding.UTF8.GetString(yBuffer, 0, xBuffer.Length);

            return strX;
            //byte[] bs = Convert.FromBase64String(str);
            ////byte[] bs = Base64.decode(str);
            //float ffffloat = BitConverter.ToSingle(bs, 0);
            //return ffffloat;
        }

        public static string __GetLBaidu_Location(string x, string y)
        {
            string url = string.Format("https://api.map.baidu.com/ag/coord/convert?from=0&to=4&x={0:F}&y={1:F}", x, y);
            string strhtml = Eggsoft.Common.FileFolder.ReadRemoteHTML(url);
            return strhtml;
        }

        public static bool Get___BaiduLocationbl(BaiduLocation bl)
        {
            try
            {
                bl.ok = false;
                string res = __GetLBaidu_Location(bl.gpsx, bl.gpsy);
                if (res.StartsWith("{", StringComparison.Ordinal) && res.EndsWith("}", StringComparison.Ordinal))
                {
                    res = res.Substring(1, res.Length - 2 - 1).Replace("\"", "");
                    string[] lines = res.Split(',');
                    foreach (string line in lines)
                    {
                        string[] items = line.Split(':');
                        if (items.Length == 2)
                        {
                            if ("error".Equals(items[0]))
                            {
                                bl.ok = "0".Equals(items[1]);
                            }
                            if ("x".Equals(items[0]))
                            {
                                bl.baidux = ConvertBase64(items[1]);
                            }
                            if ("y".Equals(items[0]))
                            {
                                bl.baiduy = ConvertBase64(items[1]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                bl.ok = false;
            }
            return bl.ok;
        }


        /// <summary>
        /// gps转换成百度坐标
        /// </summary>
        /// 


        public static void getBaiDugps(string strlats, string strlngs, out string strBaiDulats, out string strBaiDulon)
        {
            strBaiDulats = "";
            strBaiDulon = "";

            try
            {
                BaiduLocation bl = new BaiduLocation();
                bl.gpsx = strlngs; //经度
                bl.gpsy = strlats; //纬度
                Get___BaiduLocationbl(bl);
                if (bl.ok)
                {
                    strBaiDulon = bl.baidux;
                    strBaiDulats = bl.baiduy;

                    //int baidux = (int)(bl.baidux * 1E6);
                    //int baiduy = (int)(bl.baiduy * 1E6);
                    // 转换成功，这个坐标是百度专用的
                }
                else
                {
                    /// 转换失败
                }
            }
            catch (Exception)
            {
            }

        }




        static double DEF_PI = 3.14159265359; // PI
        static double DEF_2PI = 6.28318530712; // 2*PI
        static double DEF_PI180 = 0.01745329252; // PI/180.0
        static double DEF_R = 6370693.5; // radius of earth

        public static double GetShortDistance(double lon1, double lat1, double lon2, double lat2)
        {
            double ew1, ns1, ew2, ns2;
            double dx, dy, dew;
            double distance;
            // 角度转换为弧度
            ew1 = lon1 * DEF_PI180;
            ns1 = lat1 * DEF_PI180;
            ew2 = lon2 * DEF_PI180;
            ns2 = lat2 * DEF_PI180;
            // 经度差
            dew = ew1 - ew2;
            // 若跨东经和西经180 度，进行调整
            if (dew > DEF_PI)
                dew = DEF_2PI - dew;
            else if (dew < -DEF_PI)
                dew = DEF_2PI + dew;
            dx = DEF_R * Math.Cos(ns1) * dew; // 东西方向长度(在纬度圈上的投影长度)
            dy = DEF_R * (ns1 - ns2); // 南北方向长度(在经度圈上的投影长度)
            // 勾股定理求斜边长
            distance = Math.Sqrt(dx * dx + dy * dy);
            return distance;
        }

        public static double GetLongDistance(double lon1, double lat1, double lon2, double lat2)
        {
            double ew1, ns1, ew2, ns2;
            double distance;
            // 角度转换为弧度
            ew1 = lon1 * DEF_PI180;
            ns1 = lat1 * DEF_PI180;
            ew2 = lon2 * DEF_PI180;
            ns2 = lat2 * DEF_PI180;
            // 求大圆劣弧与球心所夹的角(弧度)
            distance = Math.Sin(ns1) * Math.Sin(ns2) + Math.Cos(ns1) * Math.Cos(ns2) * Math.Cos(ew1 - ew2);
            // 调整到[-1..1]范围内，避免溢出
            if (distance > 1.0)
                distance = 1.0;
            else if (distance < -1.0)
                distance = -1.0;
            // 求大圆劣弧长度
            distance = DEF_R * Math.Acos(distance);
            return distance;
        }
      

    }
}


