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
    /// XmlTools ��ժҪ˵����
    /// </summary>
    public class XmlTools
	{
		#region ��ȡXML������

		private static Dictionary<string, XmlSerializer> XmlSerializerDict = new Dictionary<string, XmlSerializer>();

        #region �õ�XML������, ָ����������Ĭ��Ԫ��ͷ�ļ�

        /// <summary>
        /// �õ�XML������, ָ����������Ĭ��Ԫ��ͷ�ļ�
        /// </summary>
        /// <param name="typeXS"></param>
        /// <param name="rootnodeName"></param>
        /// <returns></returns>
        public static XmlSerializer GetXmlSerializer(Type typeXS, string rootnodeName)
        {
            return GetXmlSerializerInternal(typeXS, rootnodeName, false);
        }

        #endregion

        #region �õ�XML������

        /// <summary>
        /// �õ�XML������
        /// </summary>
        /// <param name="typeXS"></param>
        /// <returns></returns>
        public static XmlSerializer GetXmlSerializer(Type typeXS)
        {
            return GetXmlSerializerInternal(typeXS, string.Empty, false);
        }

        #endregion

        #region �õ�SOAP XML������, ָ����������Ĭ��Ԫ��ͷ�ļ�

        /// <summary>
        /// �õ�SOAP XML������, ָ����������Ĭ��Ԫ��ͷ�ļ�
        /// </summary>
        /// <param name="typeXS"></param>
        /// <param name="rootnodeName"></param>
        /// <returns></returns>
        public static XmlSerializer GetSoapSerializer(Type typeXS, string rootnodeName)
        {
            return GetXmlSerializerInternal(typeXS, rootnodeName, true);
        }

        #endregion

        #region �õ�SOAP XML������

        /// <summary>
        /// �õ�SOAP XML������
        /// </summary>
        /// <param name="typeXS"></param>
        /// <param name="rootnodeName"></param>
        /// <returns></returns>
        public static XmlSerializer GetSoapSerializer(Type typeXS)
        {
            return GetXmlSerializerInternal(typeXS, string.Empty, true);
        }

        #endregion

        #region Xml�������ڲ�����ʵ��

        /// <summary>
		/// ����XmlSerializer����,�Ա���XmlSerializer����ʱ�Զ�����
		/// </summary>
		/// <param name="typeXS">XmlSerializer����</param>
		/// <param name="rootnodeName">��Ԫ������</param>
		/// <param name="IsSoapXml">�Ƿ�����Soap��ʽ��Xml�ļ�</param>
		/// <returns>XmlSerializer����</returns>
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

		#region ��XmlReader�л�ȡָ���ڵ��ָ�����Ե�ֵ

		/// <summary>
		/// ��XmlReader�л�ȡָ���ڵ��ָ�����Ե�ֵ, 
		/// �˷�����ȡ��ƪ�ĵ���δ�����쳣��δ��λ
		/// </summary>
		/// <param name="xtr"></param>
		/// <param name="ElementName"></param>
		/// <param name="AttributeName"></param>
		/// <returns>ָ���ڵ��ָ�����Ե�ֵ�����û���ҵ������ؿ�</returns>
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

        #region XML���л�

        #region �ڲ�ʵ��, �������ʽ��ΪXml�ַ���

        /// <summary>
		/// �������ʽ��ΪXml�ַ���
		/// </summary>
		/// <param name="o">��Ҫ���л��Ķ���ʵ��</param>
		/// <param name="rootnodeName">��Ԫ������</param>
		/// <param name="IsCompleteXml">�Ƿ�����������Xml�ļ�(����XMLͷ)</param>
		/// <param name="IsSoapXml">�Ƿ�����Soap��ʽ��Xml�ļ�</param>
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

        #region XML���л�(����Ĭ�ϸ�Ԫ������)


        /// <summary>
        /// XML���л�(����Ĭ�ϸ�Ԫ������)
        /// </summary>
        /// <param name="o">��Ҫ���л��Ķ���ʵ��</param>
        /// <param name="IsCompleteXml">�Ƿ�����������Xml�ļ�(����XMLͷ)</param>
        /// <param name="IsSoapXml">�Ƿ�����Soap��ʽ��Xml�ļ�</param>
		public static string ToXml(object o, bool IsCompleteXml)
		{
			return ToXmlInternal(o, string.Empty, IsCompleteXml, false);
        }

        #endregion

        #region XML���л�

        /// <summary>
        /// XML���л�
        /// </summary>
        /// <param name="o">��Ҫ���л��Ķ���ʵ��</param>
        /// <param name="rootnodeName">��Ԫ������</param>
        /// <param name="IsCompleteXml">�Ƿ�����������Xml�ļ�(����XMLͷ)</param>
        public static string ToXml(object o, string rootnodeName, bool IsCompleteXml)
		{
            return ToXmlInternal(o, rootnodeName, IsCompleteXml, false);
        }

        #endregion

        #region XML���л�(����XML, �Զ����Ԫ������)

        /// <summary>
        /// XML���л�(����XML, �Զ����Ԫ������)
        /// </summary>
        /// <param name="o">��Ҫ���л��Ķ���ʵ��</param>
        /// <param name="rootnodeName">��Ԫ������</param>
        public static string ToXml(object o, string rootnodeName)
		{
            return ToXmlInternal(o, rootnodeName, true, false);
        }

        #endregion

        #region XML���л�(ȫ������Ĭ������)

        /// <summary>
        /// XML���л�(ȫ������Ĭ������)
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
		public static string ToXml(object o)
		{
            return ToXmlInternal(o, string.Empty, true, false);
		}

		#endregion

        #region SOAP���л�

        /// <summary>
        /// SOAP���л�
        /// </summary>
        /// <param name="o">��Ҫ���л��Ķ���ʵ��</param>
        /// <param name="rootnodeName">��Ԫ������</param>
        public static string ToSoap(object o, string rootnodeName)
        {
            return ToXmlInternal(o, rootnodeName, true, true);
        }

        #endregion

        #region SOAP���л�(ȫ������Ĭ������)

        /// <summary>
        /// SOAP���л�(ȫ������Ĭ������)
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToSoap(object o)
        {
            return ToXmlInternal(o, string.Empty, true, true);
        }

        #endregion

        #endregion

        #region ��Xml�ַ��������л�Ϊ����

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

		#region ʹ��XSLת��XML�����ַ���

		/// <summary>
		/// ʹ��XSLת��XML�����ַ���
		/// </summary>
		/// <param name="xml">xml����</param>
		/// <param name="xsl">xsl����</param>
		/// <param name="isXmlUri">ʹ��URI��Ϊxml����</param>
		/// <param name="isXslUri">ʹ��URI��Ϊxsl����</param>
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

        #region ��ȡָ��·���µ�InnerText

        /// <summary>
        /// ��ȡָ��·���µ�InnerText
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xPath"></param>
        /// <param name="IsThrowError">���û���ҵ��Ƿ��׳�����</param>
        /// <returns></returns>
        public static string GetInnerText(XmlDocument xml, string xPath, bool IsThrowError)
        {
            XmlNode node = xml.SelectSingleNode(xPath);

            if (node == null && IsThrowError == true)
                throw new XmlToolsException("", string.Format("û����Xml�ĵ����ҵ�·��Ϊ{0}�Ľڵ�", xPath));

            return node.InnerText;
        }

        /// <summary>
        /// ��ȡָ��·���µ�InnerText
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xPath"></param>
        /// <param name="AttributeName"></param>
        /// <param name="IsThrowError">���û���ҵ��Ƿ��׳�����</param>
        /// <returns></returns>
        public static string GetAttribute(XmlDocument xml, string xPath,string AttributeName, bool IsThrowError)
        {
            XmlNode node = xml.SelectSingleNode(xPath);

            if (node == null && IsThrowError == true)
                throw new XmlToolsException("", string.Format("û����Xml�ĵ����ҵ�·��Ϊ{0}�Ľڵ�", xPath));

            try
            {
                string attrText = node.Attributes[AttributeName].Value;

                if (string.IsNullOrEmpty(attrText) && IsThrowError == true)
                    throw new XmlToolsException("", string.Format("û����·��Ϊ{0}�Ľڵ���û���ҵ�{1}������", xPath, AttributeName));

                return attrText;
            }
            catch (XmlToolsException) { throw; }
            catch (Exception ex)
            {
                if(IsThrowError)
                    throw new XmlToolsException("", string.Format("û����·��Ϊ{0}�Ľڵ���û���ҵ�{1}������", xPath, AttributeName), ex);

                return string.Empty;
            }

            
        }

        /// <summary>
        /// ��ȡָ��·���µ�InnerText
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xPath"></param>
        /// <param name="IsThrowError">���û���ҵ��Ƿ��׳�����</param>
        /// <returns></returns>
        public static int GetInnerTextInt32(XmlDocument xml, string xPath, bool IsThrowError)
        {
            XmlNode node = xml.SelectSingleNode(xPath);

            if (node == null && IsThrowError == true)
                throw new XmlToolsException("", string.Format("û����Xml�ĵ����ҵ�·��Ϊ{0}�Ľڵ�", xPath));

            string text = node.InnerText;

            int i = int.MinValue;

            if (!int.TryParse(text, out i) && IsThrowError == true)
            {
                throw new XmlToolsException("", string.Format("·��Ϊ{0}�Ľڵ�{1}�޷�ת��ΪInt32����", xPath, text));
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

		#region ISerializable ��Ա

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
    }
}