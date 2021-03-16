
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace IpHelper
{


    #region AddressForQueryIPFromBaidu [Serializable]
    public class AddressForQueryIPFromBaidu
    {
        public string Address { get; set; }
        public Content Content { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class Content
    {
        public string Address { get; set; }
        public Address_Detail Address_Detail { get; set; }
        public Point Point { get; set; }
    }
    [Serializable]
    public class Address_Detail
    {
        public string City { get; set; }
        public string City_Code { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
        public string Street_Number { get; set; }
    }
    [Serializable]
    public class Point
    {
        public string X { get; set; }
        public string Y { get; set; }
    }
    #endregion
    


    public class IpDetail
	{
		public String Ret { get; set; }

		public String Start { get; set; }

		public String End { get; set; }

		public String Country { get; set; }

		public String Province { get; set; }

		public String City { get; set; }

		public String District { get; set; }

		public String Isp { get; set; }

		public String type { get; set; }

		public String Desc { get; set; }
	}

	public class IpHelper
	{
        

        public static AddressForQueryIPFromBaidu GetAddressFromIP_BaiDu(string ipAddress)
        {
            string baiduKey = "D115c637a1d10e58c7ed20711db00cca";
            string url = "https://api.map.baidu.com/location/ip?ak=" + baiduKey + "&ip=" + ipAddress + "&coor=bd09ll"; HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
            string responseText = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            responseStream.Close();
            string jsonData = responseText;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            AddressForQueryIPFromBaidu addressForQueryIPFromBaidu = jss.Deserialize<AddressForQueryIPFromBaidu>(jsonData);
            return addressForQueryIPFromBaidu;
        }

        //返回的是相关类对象，直接取属性即可获得相关内容。



		/// <summary>
		/// 获取IP地址的详细信息，调用的借口为
		/// http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip={ip}
		/// </summary>
		/// <param name="ipAddress">请求分析得IP地址</param>
		/// <param name="sourceEncoding">服务器返回的编码类型</param>
		/// <returns>IpUtils.IpDetail</returns>
		public static IpDetail Get(String ipAddress,Encoding sourceEncoding)
		{
			String ip = string.Empty;
			if(sourceEncoding==null)
				sourceEncoding = Encoding.UTF8;
			using (var receiveStream = WebRequest.Create("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip="+ipAddress).GetResponse().GetResponseStream())
			{
				using (var sr = new StreamReader(receiveStream, sourceEncoding))
				{
					var readbuffer = new char[256];
					int n = sr.Read(readbuffer, 0, readbuffer.Length);
					int realLen = 0;
					while (n > 0)
					{
						realLen = n;
						n = sr.Read(readbuffer, 0, readbuffer.Length);
					}
					ip = sourceEncoding.GetString(sourceEncoding.GetBytes(readbuffer, 0, realLen));
				}
			}
			return  !string.IsNullOrEmpty(ip)?new JavaScriptSerializer().Deserialize<IpDetail>(ip):null;
		}


        /// <summary> 
        ///clearUserPhoneCookie 
        //    IP地址转换为整数
        //原理：IP地址每段可以看成是8位无符号整数即0-255，把每段拆分成一个二进制形式组合起来，然后把这个二进制数转变成一个无符号的32位整数。
        //举例：一个ip地址为10.0.3.193
        //每段数字 相对应的二进制数
        //10 00001010
        //0 00000000
        //3 00000011
        //193 1400030062001
        //组合起来即为：00001010 00000000 00000011 1400030062001，转换为10进制就是：167773121，即该IP地址转换后的数字就是它了。
        //   
        /// </summary> 
        /// <param></param> 
        /// <param></param> 


        public static long IpToInt(string ip)
        {
            char[] separator = new char[] { '.' };
            string[] items = ip.Split(separator);
            return long.Parse(items[0]) << 24
                    | long.Parse(items[1]) << 16
                    | long.Parse(items[2]) << 8
                    | long.Parse(items[3]);
        }

        #region 获取web客户端ip
        /// <summary>
        /// 获取web客户端ip
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {

            string userIP = "未获取用户IP";


            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";


                string CustomerIP = "";


                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }


                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];



                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;


                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                }


                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
               // strIP = "101.229.50.131";
                if (CustomerIP == "::1") CustomerIP = "101.229.50.131";  ///上海
              
                //if (CustomerIP == "::1") CustomerIP = "202.102.227.68";  ///河南
                return CustomerIP;

            }
            catch { }


            return userIP;

        }
        #endregion


	}

	public class EncodingHelper
	{
		public static String GetString(Encoding source, Encoding dest, String soureStr)
		{
			return dest.GetString(Encoding.Convert(source, dest, source.GetBytes(soureStr)));
		}

		public static String GetString(Encoding source, Encoding dest, Char[] soureCharArr, int offset, int len)
		{
			return dest.GetString(Encoding.Convert(source, dest, source.GetBytes(soureCharArr, offset, len)));
		}
	}

   
}
