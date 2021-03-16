using System;
using System.Text;
using System.Runtime.Serialization;
using EggsoftWX.SQLServerDAL.Exceptions.Util;

namespace EggsoftWX.SQLServerDAL.Exceptions
{
	/// <summary>
	/// 异常信息响应基类
	/// </summary>
	[Serializable]
	public class ExceptionBase : Exception, System.Runtime.Serialization.ISerializable
    {
		public ExceptionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#region ISerializable 成员

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion

		#region 构造函数

        public ExceptionBase()
        { }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="id"></param>
		/// <param name="msg"></param>
		public ExceptionBase(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="id"></param>
		/// <param name="msg"></param>
		public ExceptionBase(string id, string msg)
			: base(msg)
		{
			MessageID = id;
		}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="originalErrorMsg"></param>
        public ExceptionBase(string msg, Exception ee): base(msg, ee){}

		/// <summary>
		/// 构造函数
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

		#region 异常编号

		/// <summary>
		/// 异常编号
		/// </summary>
		public string MessageID = "";

		#endregion

		#region 格式化日志信息

		/// <summary>
		/// 格式化日志信息
		/// </summary>
		/// <returns></returns>
		public virtual string FormatExceptionLog()
		{
			//写日志文件
            return ExceptionFormatter.FormatException(this);
		}

		#endregion

		#region 输出日志内容

		/// <summary>
		/// 格式化日志信息
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
