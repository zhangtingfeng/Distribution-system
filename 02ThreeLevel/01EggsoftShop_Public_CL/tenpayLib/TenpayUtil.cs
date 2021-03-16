using System;
using System.Text;
using System.Web;
namespace tenpayApp
{
	/// <summary>
	/// TenpayUtil 的摘要说明。
    /// 配置文件
	/// </summary>
	public class TenpayUtil
	{
        public static string tenpay = "1";
        public static string partner = "1218447701";                   //商户号
        public static string partnerkey = "a08ba97aa5f45484bf742b9a1ef8aa1f";  //密钥
        public static string appid = "wx87c32a66901c33e1";//appid
        public static string appkey = "t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv";//paysignkey(非appkey) 
        public static string tenpay_notify = "http://eggsoft.cn/weixinpay/notify_url.aspx"; //支付完成后的回调处理页面,*替换成notify_url.asp所在路径

//    您的微信支付商户申请已经审核通过！
//       商户号(PartnerID)： 	1218447701
//     商户名称： 	上海时仪电子有限公司
//     登录密码： 	111111
//     初始密钥(PartnerKey)： 	a08ba97aa5f45484bf742b9a1ef8aa1f
//     Appid： 	wx87c32a66901c33e1
//Paysignkey： 	
//     安全证书： 	1218447701_20140320151842.pfx（请下载邮件附件）
//   银行账号： 	7440210182600006773
//    提示：此账号用于您与财付通之间的结算
//    开始日期： 	2014-03-20

        //你的partnerId和partnerKey将由财付通另行邮件发给你，请随时留意。

        //        你的AppID为：wx87c32a66901c33e1
        //你的AppSecret为：a298817207894f2c373dbf069cc4b6d9
        //你的支付专用签名串PaySignKey为：t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv
//        恭喜你已经成功通过公众平台商户审核！

//同时，感谢你申请微信公众平台公众号支付功能。

//以下是你的支付重要信息，请注意保密：
//你的AppID为：wx87c32a66901c33e1
//你的AppSecret为：a298817207894f2c373dbf069cc4b6d9
//你的支付专用签名串PaySignKey为：t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv

//你的partnerId和partnerKey将由财付通另行邮件发给你，请随时留意。


		public TenpayUtil()
		{
          
		}


        //public static string getpartner()
        //{
        //    string strShopClientID = Eggsoft.Common.Cookie.Read("webuy8_ClientAdmin_Users").ToString();

        //    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
        //    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
  
        //}


        public static string getNoncestr()
        {
            Random random = new Random();
            return MD5Util.GetMD5(random.Next(1000).ToString(), "GBK");
        }


        public static string getTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
      

		/** 对字符串进行URL编码 */
		public static string UrlEncode(string instr, string charset)
		{
			//return instr;
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;
			}
		}

		/** 对字符串进行URL解码 */
		public static string UrlDecode(string instr, string charset)
		{
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;

			}
		}
       

		/** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
		public static UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** 取随机数 */
		public static string BuildRandomStr(int length) 
		{
			Random rand = new Random();

			int num = rand.Next();

			string str = num.ToString();

			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			
			return str;
		}
       
	}
}



//恭喜你已经成功通过公众平台商户审核！

//同时，感谢你申请微信公众平台公众号支付功能。

//以下是你的支付重要信息，请注意保密：
//你的AppID为：wx87c32a66901c33e1
//你的AppSecret为：a298817207894f2c373dbf069cc4b6d9
//你的支付专用签名串PaySignKey为：t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv

//你的partnerId和partnerKey将由财付通另行邮件发给你，请随时留意。

// 您的微信支付商户申请已经审核通过！
//       商户号(PartnerID)： 	1218447701
//     商户名称： 	深圳上海时仪网络科技有限公司
//     登录密码： 	111111
//     初始密钥(PartnerKey)： 	a08ba97aa5f45484bf742b9a1ef8aa1f
//     Appid： 	wx87c32a66901c33e1
//Paysignkey： 	
//     安全证书： 	1218447701_20140320151842.pfx（请下载邮件附件）
//   银行账号： 	7440210182600006773
//    提示：此账号用于您与财付通之间的结算
//    开始日期： 	2014-03-20
// 请按照以下步骤完成接入：
//1、如您使用的是IE7/8或以上版本浏览器，请先下载财付通信任证书到本地计算机进行安装，否则可跳过此步。
//2、从邮件附件中下载安全证书到本地计算机进行安装，当提示“为私钥键入密码”时请输入贵公司商户号。
//3、为确保您的账户安全，首次登录财付通企业版请于“账户设置”功能中修改密钥和登录密码。
//4、集成财付通支付网关，请下载接口开发文档；集成其他业务功能，请联系财付通技术支持确定方案并发送相关文档。
//5、请尽快将合同盖章邮寄至页面提示的地址。在我们收到您寄回的合同之前，您还无法使用资金结算功能
//6、微信支付技术接入，请关注“商家服务”公众号，获取技术接入文档和技术接入咨询。公众号关注方式：在微信搜索“商家服务”或“WXPayService”，也可扫描下方二维码进行关注。


//3126号腾讯客服 //075583767777
