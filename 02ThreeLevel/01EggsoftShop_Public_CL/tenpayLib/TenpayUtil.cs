using System;
using System.Text;
using System.Web;
namespace tenpayApp
{
	/// <summary>
	/// TenpayUtil ��ժҪ˵����
    /// �����ļ�
	/// </summary>
	public class TenpayUtil
	{
        public static string tenpay = "1";
        public static string partner = "1218447701";                   //�̻���
        public static string partnerkey = "a08ba97aa5f45484bf742b9a1ef8aa1f";  //��Կ
        public static string appid = "wx87c32a66901c33e1";//appid
        public static string appkey = "t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv";//paysignkey(��appkey) 
        public static string tenpay_notify = "http://eggsoft.cn/weixinpay/notify_url.aspx"; //֧����ɺ�Ļص�����ҳ��,*�滻��notify_url.asp����·��

//    ����΢��֧���̻������Ѿ����ͨ����
//       �̻���(PartnerID)�� 	1218447701
//     �̻����ƣ� 	�Ϻ�ʱ�ǵ������޹�˾
//     ��¼���룺 	111111
//     ��ʼ��Կ(PartnerKey)�� 	a08ba97aa5f45484bf742b9a1ef8aa1f
//     Appid�� 	wx87c32a66901c33e1
//Paysignkey�� 	
//     ��ȫ֤�飺 	1218447701_20140320151842.pfx���������ʼ�������
//   �����˺ţ� 	7440210182600006773
//    ��ʾ�����˺���������Ƹ�֮ͨ��Ľ���
//    ��ʼ���ڣ� 	2014-03-20

        //���partnerId��partnerKey���ɲƸ�ͨ�����ʼ������㣬����ʱ���⡣

        //        ���AppIDΪ��wx87c32a66901c33e1
        //���AppSecretΪ��a298817207894f2c373dbf069cc4b6d9
        //���֧��ר��ǩ����PaySignKeyΪ��t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv
//        ��ϲ���Ѿ��ɹ�ͨ������ƽ̨�̻���ˣ�

//ͬʱ����л������΢�Ź���ƽ̨���ں�֧�����ܡ�

//���������֧����Ҫ��Ϣ����ע�Ᵽ�ܣ�
//���AppIDΪ��wx87c32a66901c33e1
//���AppSecretΪ��a298817207894f2c373dbf069cc4b6d9
//���֧��ר��ǩ����PaySignKeyΪ��t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv

//���partnerId��partnerKey���ɲƸ�ͨ�����ʼ������㣬����ʱ���⡣


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
      

		/** ���ַ�������URL���� */
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

		/** ���ַ�������URL���� */
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
       

		/** ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ�� */
		public static UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** ȡ����� */
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



//��ϲ���Ѿ��ɹ�ͨ������ƽ̨�̻���ˣ�

//ͬʱ����л������΢�Ź���ƽ̨���ں�֧�����ܡ�

//���������֧����Ҫ��Ϣ����ע�Ᵽ�ܣ�
//���AppIDΪ��wx87c32a66901c33e1
//���AppSecretΪ��a298817207894f2c373dbf069cc4b6d9
//���֧��ר��ǩ����PaySignKeyΪ��t409LtHElyJgtL259aigqmVjrP3VyLG9mSdbd6b2tRCYMHhERFANqBMaZWJYNCQluGaj0jAQULNNIYeflSBMylioAijA2OjysXUTXa43Oj0LdIHlQspikVR8bNxuwKBv

//���partnerId��partnerKey���ɲƸ�ͨ�����ʼ������㣬����ʱ���⡣

// ����΢��֧���̻������Ѿ����ͨ����
//       �̻���(PartnerID)�� 	1218447701
//     �̻����ƣ� 	�����Ϻ�ʱ������Ƽ����޹�˾
//     ��¼���룺 	111111
//     ��ʼ��Կ(PartnerKey)�� 	a08ba97aa5f45484bf742b9a1ef8aa1f
//     Appid�� 	wx87c32a66901c33e1
//Paysignkey�� 	
//     ��ȫ֤�飺 	1218447701_20140320151842.pfx���������ʼ�������
//   �����˺ţ� 	7440210182600006773
//    ��ʾ�����˺���������Ƹ�֮ͨ��Ľ���
//    ��ʼ���ڣ� 	2014-03-20
// �밴�����²�����ɽ��룺
//1������ʹ�õ���IE7/8�����ϰ汾��������������زƸ�ͨ����֤�鵽���ؼ�������а�װ������������˲���
//2�����ʼ����������ذ�ȫ֤�鵽���ؼ�������а�װ������ʾ��Ϊ˽Կ�������롱ʱ�������˾�̻��š�
//3��Ϊȷ�������˻���ȫ���״ε�¼�Ƹ�ͨ��ҵ�����ڡ��˻����á��������޸���Կ�͵�¼���롣
//4�����ɲƸ�֧ͨ�����أ������ؽӿڿ����ĵ�����������ҵ���ܣ�����ϵ�Ƹ�ͨ����֧��ȷ����������������ĵ���
//5���뾡�콫��ͬ�����ʼ���ҳ����ʾ�ĵ�ַ���������յ����Ļصĺ�֮ͬǰ�������޷�ʹ���ʽ���㹦��
//6��΢��֧���������룬���ע���̼ҷ��񡱹��ںţ���ȡ���������ĵ��ͼ���������ѯ�����ںŹ�ע��ʽ����΢���������̼ҷ��񡱻�WXPayService����Ҳ��ɨ���·���ά����й�ע��


//3126����Ѷ�ͷ� //075583767777
