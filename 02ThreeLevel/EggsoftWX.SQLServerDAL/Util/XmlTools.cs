using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace EggsoftWX.SQLServerDAL.Exceptions.Util
{
    /// <summary>
    /// XmlTools 的摘要说明。
    /// </summary>
    public class XmlTools
	{
		#region 获取XML序列器

		private static Dictionary<string, XmlSerializer> XmlSerializerDict = new Dictionary<string, XmlSerializer>();

        #region 得到XML序列器, 指定序列器的默认元素头文件

        /// <summary>
        /// 得到XML序列器, 指定序列器的默认元素头文件
        /// </summary>
        /// <param name="typeXS"></param>
        /// <param name="rootnodeName"></param>
        /// <returns></returns>
        public static XmlSerializer GetXmlSerializer(Type typeXS, string rootnodeName)
        {
            return GetXmlSerializerInternal(typeXS, rootnodeName, false);
        }

        #endregion

        #region 得到XML序列器

        /// <summary>
        /// 得到XML序列器
        /// </summary>
        /// <param name="typeXS"></param>
        /// <returns></returns>
        public static XmlSerializer GetXmlSerializer(Type typeXS)
        {
            return GetXmlSerializerInternal(typeXS, string.Empty, false);
        }

        #endregion

        #region 得到SOAP XML序列器, 指定序列器的默认元素头文件

        /// <summary>
        /// 得到SOAP XML序列器, 指定序列器的默认元素头文件
        /// </summary>
        /// <param name="typeXS"></param>
        /// <param name="rootnodeName"></param>
        /// <returns></returns>
        public static XmlSerializer GetSoapSerializer(Type typeXS, string rootnodeName)
        {
            return GetXmlSerializerInternal(typeXS, rootnodeName, true);
        }

        #endregion

        #region 得到SOAP XML序列器

        /// <summary>
        /// 得到SOAP XML序列器
        /// </summary>
        /// <param name="typeXS"></param>
        /// <param name="rootnodeName"></param>
        /// <returns></returns>
        public static XmlSerializer GetSoapSerializer(Type typeXS)
        {
            return GetXmlSerializerInternal(typeXS, string.Empty, true);
        }

        #endregion

        #region Xml序列器内部缓存实现

        /// <summary>
		/// 缓存XmlSerializer对象,以避免XmlSerializer产生时自动编译
		/// </summary>
		/// <param name="typeXS">XmlSerializer类型</param>
		/// <param name="rootnodeName">根元素名称</param>
		/// <param name="IsSoapXml">是否生成Soap格式的Xml文件</param>
		/// <returns>XmlSerializer对象</returns>
		private static XmlSerializer GetXmlSerializerInternal(Type typeXS, string rootnodeName, bool IsSoapXml)
		{
			XmlSerializer rtVal;
			string keyHsXS = string.Format("{0}_{1}_{2}_{3}", typeXS.AssemblyQualifiedName, typeXS.FullName, rootnodeName, IsSoapXml);


			lock (XmlSerializerDict)
			{
				if (XmlSerializerDict.ContainsKey(keyHsXS))
				{
					rtVal = XmlSerializerDict[keyHsXS];
				}
				else
				{
					if (IsSoapXml)
					{
						XmlTypeMapping myTypeMapping = (new SoapReflectionImporter()).ImportTypeMapping(typeXS);
						rtVal = new XmlSerializer(myTypeMapping);
					}
					else
					{
						if (!string.IsNullOrEmpty(rootnodeName))
						{
							rtVal = new XmlSerializer(typeXS, new XmlRootAttribute(rootnodeName));
						}
						else
						{
							rtVal = new XmlSerializer(typeXS);
						}
					}

					XmlSerializerDict.Add(keyHsXS, rtVal);
				}
			}

			return rtVal;
        }

        #endregion

		#endregion

		#region 从XmlReader中获取指定节点的指定属性的值

		/// <summary>
		/// 从XmlReader中获取指定节点的指定属性的值, 
		/// 此方法读取整篇文档，未处理异常，未归位
		/// </summary>
		/// <param name="xtr"></param>
		/// <param name="ElementName"></param>
		/// <param name="AttributeName"></param>
		/// <returns>指定节点的指定属性的值，如果没有找到，返回空</returns>
		public static string GetAttributeName(XmlReader xtr, string ElementName, string AttributeName)
		{
			xtr.MoveToFirstAttribute();
			while (xtr.Read())
			{
				if (xtr.Name == ElementName && xtr.IsStartElement())
				{
					return xtr.GetAttribute(AttributeName);
				}
			}

			return "";
		}

		#endregion

        #region XML序列化

        #region 内部实现, 将对象格式化为Xml字符串

        /// <summary>
		/// 将对象格式化为Xml字符串
		/// </summary>
		/// <param name="o">需要序列化的对象实体</param>
		/// <param name="rootnodeName">根元素名称</param>
		/// <param name="IsCompleteXml">是否生成完整的Xml文件(带有XML头)</param>
		/// <param name="IsSoapXml">是否生成Soap格式的Xml文件</param>
		private static string ToXmlInternal(object o, string rootnodeName, bool IsCompleteXml, bool IsSoapXml)
		{
			if (o == null)
			{
				return string.Empty;
			}

			Type t = o.GetType();
			XmlSerializer xs = GetXmlSerializerInternal(t, rootnodeName, IsSoapXml);
			StringBuilder sbXml = new StringBuilder();
			StringBuilder sbRet = new StringBuilder();
			XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
			xns.Add("", "");

			using (StringWriter sw = new StringWriter(sbXml))
			{
				xs.Serialize(sw, o, xns);
				sw.Close();
			}

			if (!IsCompleteXml)
			{
				sbXml.Replace(
					"<?xml version=\"1.0\" encoding=\"utf-16\"?>","");
				sbRet.Append(sbXml.ToString().Trim());
			}
			else
			{
				sbXml.Replace(
					"<?xml version=\"1.0\" encoding=\"utf-16\"?>",
					"<?xml version=\"1.0\" encoding=\"utf-8\"?>");

				sbRet.Append(sbXml.ToString());
			}

			return sbRet.ToString();
		}

        #endregion

        #region XML序列化(采用默认根元素名称)


        /// <summary>
        /// XML序列化(采用默认根元素名称)
        /// </summary>
        /// <param name="o">需要序列化的对象实体</param>
        /// <param name="IsCompleteXml">是否生成完整的Xml文件(带有XML头)</param>
        /// <param name="IsSoapXml">是否生成Soap格式的Xml文件</param>
		public static string ToXml(object o, bool IsCompleteXml)
		{
			return ToXmlInternal(o, string.Empty, IsCompleteXml, false);
        }

        #endregion

        #region XML序列化

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <param name="o">需要序列化的对象实体</param>
        /// <param name="rootnodeName">根元素名称</param>
        /// <param name="IsCompleteXml">是否生成完整的Xml文件(带有XML头)</param>
        public static string ToXml(object o, string rootnodeName, bool IsCompleteXml)
		{
            return ToXmlInternal(o, rootnodeName, IsCompleteXml, false);
        }

        #endregion

        #region XML序列化(完整XML, 自定义根元素名称)

        /// <summary>
        /// XML序列化(完整XML, 自定义根元素名称)
        /// </summary>
        /// <param name="o">需要序列化的对象实体</param>
        /// <param name="rootnodeName">根元素名称</param>
        public static string ToXml(object o, string rootnodeName)
		{
            return ToXmlInternal(o, rootnodeName, true, false);
        }

        #endregion

        #region XML序列化(全部采用默认设置)

        /// <summary>
        /// XML序列化(全部采用默认设置)
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
		public static string ToXml(object o)
		{
            return ToXmlInternal(o, string.Empty, true, false);
		}

		#endregion

        #region SOAP序列化

        /// <summary>
        /// SOAP序列化
        /// </summary>
        /// <param name="o">需要序列化的对象实体</param>
        /// <param name="rootnodeName">根元素名称</param>
        public static string ToSoap(object o, string rootnodeName)
        {
            return ToXmlInternal(o, rootnodeName, true, true);
        }

        #endregion

        #region SOAP序列化(全部采用默认设置)

        /// <summary>
        /// SOAP序列化(全部采用默认设置)
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToSoap(object o)
        {
            return ToXmlInternal(o, string.Empty, true, true);
        }

        #endregion

        #endregion

        #region 将Xml字符串反序列化为对象

        public static object FromXml(string xml, Type objType)
		{
			object rtVal = null;

			using (StringReader reader = new StringReader(xml))
			{
				XmlSerializer serializer = XmlTools.GetXmlSerializer(objType);
				rtVal = serializer.Deserialize(reader);
			}

			return rtVal;
		}

		#endregion

		#region 使用XSL转义XML生成字符串

		/// <summary>
		/// 使用XSL转义XML生成字符串
		/// </summary>
		/// <param name="xml">xml参数</param>
		/// <param name="xsl">xsl参数</param>
		/// <param name="isXmlUri">使用URI作为xml参数</param>
		/// <param name="isXslUri">使用URI作为xsl参数</param>
		/// <returns></returns>
		public static string XslTransferXml(string xml, string xsl, bool isXmlUri, bool isXslUri)
		{
			StringBuilder sbRet = new StringBuilder();
			XslCompiledTransform xslDoc = new XslCompiledTransform();
			System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

			if (isXslUri)
			{
				xslDoc.Load(xsl);
			}
			else
			{
				xmlDoc.LoadXml(xsl);
				xslDoc.Load(xmlDoc);
			}

			if (isXmlUri)
			{
				xmlDoc.Load(xml);
			}
			else
			{
				xmlDoc.LoadXml(xml);
			}

			using (System.IO.StringWriter sw = new System.IO.StringWriter(sbRet))
			{
				xslDoc.Transform(xmlDoc, null, sw);
				sw.Close();
			}

			return sbRet.ToString();
		}

		public static string XslTransferXml(string xml, string xslUri)
		{
			return XslTransferXml(xml, xslUri, false, true);
		}

		#endregion

        #region 获取指定路径下的InnerText

        /// <summary>
        /// 获取指定路径下的InnerText
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xPath"></param>
        /// <param name="IsThrowError">如果没有找到是否抛出错误</param>
        /// <returns></returns>
        public static string GetInnerText(XmlDocument xml, string xPath, bool IsThrowError)
        {
            XmlNode node = xml.SelectSingleNode(xPath);

            if (node == null && IsThrowError == true)
                throw new XmlToolsException("", string.Format("没有在Xml文档中找到路径为{0}的节点", xPath));

            return node.InnerText;
        }

        /// <summary>
        /// 获取指定路径下的InnerText
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xPath"></param>
        /// <param name="AttributeName"></param>
        /// <param name="IsThrowError">如果没有找到是否抛出错误</param>
        /// <returns></returns>
        public static string GetAttribute(XmlDocument xml, string xPath,string AttributeName, bool IsThrowError)
        {
            XmlNode node = xml.SelectSingleNode(xPath);

            if (node == null && IsThrowError == true)
                throw new XmlToolsException("", string.Format("没有在Xml文档中找到路径为{0}的节点", xPath));

            try
            {
                string attrText = node.Attributes[AttributeName].Value;

                if (string.IsNullOrEmpty(attrText) && IsThrowError == true)
                    throw new XmlToolsException("", string.Format("没有在路径为{0}的节点中没有找到{1}的属性", xPath, AttributeName));

                return attrText;
            }
            catch (XmlToolsException) { throw; }
            catch (Exception ex)
            {
                if(IsThrowError)
                    throw new XmlToolsException("", string.Format("没有在路径为{0}的节点中没有找到{1}的属性", xPath, AttributeName), ex);

                return string.Empty;
            }

            
        }

        /// <summary>
        /// 获取指定路径下的InnerText
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xPath"></param>
        /// <param name="IsThrowError">如果没有找到是否抛出错误</param>
        /// <returns></returns>
        public static int GetInnerTextInt32(XmlDocument xml, string xPath, bool IsThrowError)
        {
            XmlNode node = xml.SelectSingleNode(xPath);

            if (node == null && IsThrowError == true)
                throw new XmlToolsException("", string.Format("没有在Xml文档中找到路径为{0}的节点", xPath));

            string text = node.InnerText;

            int i = int.MinValue;

            if (!int.TryParse(text, out i) && IsThrowError == true)
            {
                throw new XmlToolsException("", string.Format("路径为{0}的节点{1}无法转换为Int32类型", xPath, text));
            }

            return i;
        }

        #endregion
    }

    public class XmlToolsException : ExceptionBase, ISerializable
    {
        public XmlToolsException(){ }

        public XmlToolsException(string id,string msg)
            : base(id,msg)
        {
        }

        public XmlToolsException(string id, string msg, Exception innerException)
            : base(id, msg, innerException)
        {
        }

        public XmlToolsException(SerializationInfo info, StreamingContext context)
		{
		}

		#region ISerializable 成员

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
    }
}