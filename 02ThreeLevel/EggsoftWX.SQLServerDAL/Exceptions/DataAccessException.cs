using System;
using System.Text;
using System.Runtime.Serialization;
using EggsoftWX.SQLServerDAL.Exceptions.Util;

namespace EggsoftWX.SQLServerDAL.Exceptions
{
    [Serializable]
    public class DataAccessException : ExceptionBase, System.Runtime.Serialization.ISerializable
    {
		public DataAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#region ISerializable 成员

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion


        public string SQL = "";

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="id"></param>
		/// <param name="inner"></param>
		public DataAccessException(string msg)
			: base(msg) {}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="id"></param>
		/// <param name="inner"></param>
		public DataAccessException(string id, string msg, string sql)
			: base(id,msg) { SQL = sql; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="id"></param>
		/// <param name="inner"></param>
		public DataAccessException(string msg, string sql)
			: base(msg) { SQL = sql; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inner"></param>
		public DataAccessException(string id, string msg, Exception inner) : base(id, msg, inner) { }


        public override void ToXmlElements(StringBuilder sbXml)
        {
            sbXml.Append(StringTools.GetNameValue("SQL", SQL));
			base.ToXmlElements(sbXml);
        }
    }
}
