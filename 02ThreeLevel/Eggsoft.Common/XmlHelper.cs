using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Collections;
//Eggsoft.Common.XmlHelper
namespace Eggsoft.Common
{

    public class ArrayListHelper
    {
        public string SerializeArrayList(ArrayList al, Type type)
        {
            Type[] extra = new Type[1];
            extra[0] = type;
            XmlSerializer xs = new XmlSerializer(typeof(ArrayList), extra);
            MemoryStream ms = new MemoryStream();
            XmlTextWriter tw = new XmlTextWriter(ms, Encoding.Default);
            xs.Serialize(tw, al);
            tw.Close();
            return Encoding.Default.GetString(ms.ToArray());
        }
        public ArrayList DeserializeArrayList(string serQuestions, Type type, Type[] extratype)
        {
            XmlSerializer xs = new XmlSerializer(type, extratype);
            StreamReader sr = new StreamReader(new MemoryStream(System.Text.Encoding.Default.GetBytes(serQuestions)), System.Text.Encoding.Default);
            return (ArrayList)xs.Deserialize(sr);
        }
    }

    public class XmlHelper
    {
        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer serializer = new XmlSerializer(o.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }

        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XmlSerialize(object o, Encoding encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, encoding);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="path">保存文件路径</param>
        /// <param name="encoding">编码方式</param>
        public static void XmlSerializeToFile(object o, string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializeInternal(file, o, encoding);
            }
        }

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("s");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(s)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }

        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            string xml = File.ReadAllText(path, encoding);
            return XmlDeserialize<T>(xml, encoding);
        }
    }
}




//FahuoDan myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<FahuoDan>(getGetFaHuoXML, System.Text.Encoding.UTF8);

//                           getGetFaHuoTitleXML +="发送单位："+ myFahuoDan.FaHuoGongSi + "<br />\n";
//                           getGetFaHuoTitleXML += "运单号：" + myFahuoDan.FaHuoDanHao + "<br />\n";
//                           getGetFaHuoTitleXML += "收货人姓名：" + myFahuoDan.ShouHuoRenXinMing + "<br />\n";
//                           getGetFaHuoTitleXML += "收货人电话：" + myFahuoDan.ShouHuoRenDianHua + "<br />\n";
//                           getGetFaHuoTitleXML += "收货人地址：" + myFahuoDan.ShouHuoRenDiZhi + "<br />\n";
//                           getGetFaHuoTitleXML += "发货人姓名：" + myFahuoDan.FaHuoRenXingMing + "<br />\n";
//                           getGetFaHuoTitleXML += "发货人电话：" + myFahuoDan.FaHuoRenXDianHua + "<br />\n";
//                           getGetFaHuoTitleXML += "发货人地址：" + myFahuoDan.FaHuoRenDiZhi;



//       XML_Class.GoodsClass_Cover myXML_Class = new XML_Class.GoodsClass_Cover();
//       if (txt_Add_Info.Text.Length == 0) txt_Add_Info.Text = " ";
//       myXML_Class.MemoText = Server.HtmlEncode(txt_Add_Info.Text);
//       myXML_Class.Font_Color = txt_Add_Info_ColorPicker_Font.Color;
//       myXML_Class.Background_Color = txt_Add_Info_ColorPicker_Background.Color;
//       string strCoverAndOther = Eggsoft.Common.XmlHelper.XmlSerialize(myXML_Class, System.Text.Encoding.UTF8);
