using System;
using System.Text;
using System.Runtime.Serialization;
using EggsoftWX.SQLServerDAL.Exceptions.Util;

namespace EggsoftWX.SQLServerDAL.Exceptions
{
	/// <summary>
	/// �쳣��Ϣ��Ӧ����
	/// </summary>
	[Serializable]
	public class ExceptionBase : Exception, System.Runtime.Serialization.ISerializable
    {
		public ExceptionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#region ISerializable ��Ա

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion

		#region ���캯��

        public ExceptionBase()
        { }

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="id"></param>
		/// <param name="msg"></param>
		public ExceptionBase(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="id"></param>
		/// <param name="msg"></param>
		public ExceptionBase(string id, string msg)
			: base(msg)
		{
			MessageID = id;
		}

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="originalErrorMsg"></param>
        public ExceptionBase(string msg, Exception ee): base(msg, ee){}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="id"></param>
		/// <param name="msg"></param>
		/// <param name="originalErrorMsg"></param>
		public ExceptionBase(string id, string msg, Exception ee)
			: base(msg, ee)
        {
			MessageID = id;
		}

		#endregion

		#region �쳣���

		/// <summary>
		/// �쳣���
		/// </summary>
		public string MessageID = "";

		#endregion

		#region ��ʽ����־��Ϣ

		/// <summary>
		/// ��ʽ����־��Ϣ
		/// </summary>
		/// <returns></returns>
		public virtual string FormatExceptionLog()
		{
			//д��־�ļ�
            return ExceptionFormatter.FormatException(this);
		}

		#endregion

		#region �����־����

		/// <summary>
		/// ��ʽ����־��Ϣ
		/// </summary>
		/// <returns></returns>
		public virtual void ToXmlElements(StringBuilder sbXml)
		{
		}

		#endregion

        public Exception Exception
        {
            get
            {

                string MessageIdInfo = this.MessageID == string.Empty ? string.Empty : "_" + this.MessageID;
                string ExceptionTitle = "Exception_" + this.GetType().Name + MessageIdInfo + "_" + this.Message;
                return new Exception(ExceptionTitle, this.InnerException);
            }
        }
	}

    public static class ExceptionFormatter
    {
        public static string FormatException(Exception e)
        {
            StringBuilder sbXml = new StringBuilder();

			sbXml.AppendLine();
            sbXml.AppendLine(StringTools.GetNameValue("TypeName", System.Web.HttpUtility.HtmlEncode(e.GetType().Name)));
            sbXml.AppendLine(StringTools.GetNameValue("Message", System.Web.HttpUtility.HtmlEncode(e.Message)));
            sbXml.AppendLine(StringTools.GetNameValue("StackTrace", System.Web.HttpUtility.HtmlEncode(e.StackTrace)));
            sbXml.AppendLine(StringTools.GetNameValue("Source", System.Web.HttpUtility.HtmlEncode(e.Source)));
            sbXml.AppendLine(StringTools.GetNameValue("TargetSite", e.TargetSite));

            if (e.InnerException != null)
            {
                sbXml.AppendLine("<InnerException>\r\n");
                sbXml.AppendLine(FormatException(e.InnerException));
                sbXml.AppendLine("</InnerException>\r\n");
            }

            if (e is ExceptionBase)
            {
                (e as ExceptionBase).ToXmlElements(sbXml);
            }

            return sbXml.ToString();
        }
    }
}
