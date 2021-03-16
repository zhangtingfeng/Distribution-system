using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
///WX_Model 的摘要说明
/// </summary>
public class WX_Model
{
    public class WX_Model_Parent
    {
        private string private_ToUserName = null;
        private string private_FromUserName = null;
        private string private_CreateTime = null;
        private string private_MsgType = null;

        public string ToUserName
        {
            get { return private_ToUserName; }
            set { private_ToUserName = value; }
        }
        public string FromUserName
        {
            get { return private_FromUserName; }
            set { private_FromUserName = value; }
        }
        public string CreateTime
        {
            get { return private_CreateTime; }
            set { private_CreateTime = value; }
        }
        public string MsgType
        {
            get { return private_MsgType; }
            set { private_MsgType = value; }
        }



        public static String XmlSerialize(WX_Model_Parent xmlWX_Model_Parent)
        {
            String strXmlSerialize = "";
            strXmlSerialize += "<xml>";
            strXmlSerialize += "<ToUserName><![CDATA[" + xmlWX_Model_Parent.ToUserName + "]]></ToUserName>";
            strXmlSerialize += "<FromUserName><![CDATA[" + xmlWX_Model_Parent.FromUserName + "]]></FromUserName>";
            strXmlSerialize += "</xml>";
            return strXmlSerialize;

        }
        public WX_Model_Parent GetWX_Model_Parent(string xmlString)
        {
            WX_Model_Parent myWX_Model_Parent = new WX_Model_Parent();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_Parent.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_Parent.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_Parent.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_Parent.MsgType = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_Parent;
        }


    }

    public class WX_Model_Event : WX_Model_Parent
    {
        private string private_Event = null;

        public string Event
        {
            get { return private_Event; }
            set { private_Event = value; }
        }


        public WX_Model_Event GetWX_Model_Event(string xmlString)
        {
            WX_Model_Event myWX_Model_Event = new WX_Model_Event();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_Event.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_Event.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_Event.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_Event.MsgType = node.InnerText;
                            break;
                        case "Event":    //编码 
                            myWX_Model_Event.Event = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_Event;
        }

    }


    public class WX_Model_EventKey : WX_Model_Event
    {
        private string private_EventKey = null;
        public string EventKey
        {
            get { return private_EventKey; }
            set { private_EventKey = value; }
        }


        public WX_Model_EventKey GetWX_Model_EventKey(string xmlString)
        {
            WX_Model_EventKey myWX_Model_EventKey = new WX_Model_EventKey();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_EventKey.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_EventKey.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_EventKey.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_EventKey.MsgType = node.InnerText;
                            break;
                        case "Event":    //编码 
                            myWX_Model_EventKey.Event = node.InnerText;
                            break;
                        case "EventKey":    //编码 
                            myWX_Model_EventKey.EventKey = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_EventKey;
        }

    }


    public class WX_Model_EventLOCATION : WX_Model_Event
    {
        private string private_EventLatitude = null;
        public string EventLatitude
        {
            get { return private_EventLatitude; }
            set { private_EventLatitude = value; }
        }
        private string private_EventLongitude = null;
        public string EventLongitude
        {
            get { return private_EventLongitude; }
            set { private_EventLongitude = value; }
        }
        private string private_EventPrecision = null;
        public string EventPrecision
        {
            get { return private_EventPrecision; }
            set { private_EventPrecision = value; }
        }

        public WX_Model_EventLOCATION GetWX_Model_EventLOCATION(string xmlString)
        {
            /*
             
             <xml><ToUserName><![CDATA[gh_9de27d9eb2a1]]></ToUserName><FromUserName><![CDATA[oHUlduM0ISGy8gTO3lFRbxndm_A0]]></FromUserName><CreateTime>1432341460</CreateTime><MsgType><![CDATA[location]]></MsgType><Location_X>31.167341</Location_X><Location_Y>121.347672</Location_Y><Scale>15</Scale><Label><![CDATA[上海市闵行区航华三村内(航新路西)]]></Label><MsgId>6151859727626472428</MsgId></xml>
             */

            WX_Model_EventLOCATION myWX_Model_EventLOCATION = new WX_Model_EventLOCATION();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_EventLOCATION.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_EventLOCATION.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_EventLOCATION.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_EventLOCATION.MsgType = node.InnerText;
                            break;
                        case "Event":    //编码 
                            myWX_Model_EventLOCATION.Event = node.InnerText;
                            break;
                        case "Latitude":    //编码 
                            myWX_Model_EventLOCATION.EventLatitude = node.InnerText;
                            break;
                        case "Location_X":    //编码 地理位置维度
                            myWX_Model_EventLOCATION.EventLatitude = node.InnerText;//纬度
                            break;
                        case "Location_Y":    //编码 //地理位置经度
                            myWX_Model_EventLOCATION.EventLongitude = node.InnerText;//和经度
                            break;
                        case "Longitude":    //编码 
                            myWX_Model_EventLOCATION.EventLongitude = node.InnerText;
                            break;
                        case "Precision":    //编码 
                            myWX_Model_EventLOCATION.EventPrecision = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_EventLOCATION;
        }

    }


    public class WX_Model_Subscribe : WX_Model_Event
    {
        public WX_Model_Subscribe GetWX_Model_Subscribe(string xmlString)
        {
            WX_Model_Subscribe myWX_Model_Subscribe = new WX_Model_Subscribe();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_Subscribe.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_Subscribe.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_Subscribe.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_Subscribe.MsgType = node.InnerText;
                            break;
                        case "Event":    //编码 
                            myWX_Model_Subscribe.Event = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_Subscribe;
        }

    }



    public class WX_Model_Text : WX_Model_Parent
    {
        private string private_Content = null;
        private string private_MsgId = null;

        public string Content
        {
            get { return private_Content; }
            set { private_Content = value; }
        }

        public string MsgId
        {
            get { return private_MsgId; }
            set { private_MsgId = value; }
        }

        public WX_Model_Text GeWX_Model_Text(string xmlString)
        {
            WX_Model_Text myWX_Model_Text = new WX_Model_Text();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_Text.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_Text.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_Text.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_Text.MsgType = node.InnerText;
                            break;
                        case "Content":    //编码 
                            myWX_Model_Text.Content = node.InnerText;
                            break;
                        case "MsgId":    //编码 
                            myWX_Model_Text.MsgId = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_Text;
        }



    }



    public class WX_Model_Image : WX_Model_Parent
    {
        private string private_PicUrl = null;
        private string private_MsgId = null;
        private string private_MediaId = null;



        public string PicUrl
        {
            get { return private_PicUrl; }
            set { private_PicUrl = value; }
        }

        public string MsgId
        {
            get { return private_MsgId; }
            set { private_MsgId = value; }
        }

        public string MediaId
        {
            get { return private_MediaId; }
            set { private_MediaId = value; }
        }

        public WX_Model_Image GetWX_Model_Image(string xmlString)
        {
            WX_Model_Image myWX_Model_Image = new WX_Model_Image();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_Image.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_Image.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_Image.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_Image.MsgType = node.InnerText;
                            break;
                        case "PicUrl":    //编码 
                            myWX_Model_Image.PicUrl = node.InnerText;
                            break;
                        case "MediaId":    //编码 
                            myWX_Model_Image.MediaId = node.InnerText;
                            break;
                        case "MsgId":    //编码 
                            myWX_Model_Image.MsgId = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_Image;
        }



    }



    public class WX_Model_Voice : WX_Model_Parent
    {
        private string private_MsgId = null;
        private string private_MediaId = null;





        public string MsgId
        {
            get { return private_MsgId; }
            set { private_MsgId = value; }
        }

        public string MediaId
        {
            get { return private_MediaId; }
            set { private_MediaId = value; }
        }

        public WX_Model_Voice Get_Model_Voice(string xmlString)
        {
            WX_Model_Voice myWX_Model_Voice = new WX_Model_Voice();
            XmlDocument doc = new XmlDocument();//Xml解析 
            doc.LoadXml(xmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("xml"); //取得节点名为Object的集合   
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体 
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合 
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "FromUserName":    //编码 
                            myWX_Model_Voice.FromUserName = node.InnerText;
                            break;
                        case "ToUserName":    //编码 
                            myWX_Model_Voice.ToUserName = node.InnerText;
                            break;
                        case "CreateTime":    //编码 
                            myWX_Model_Voice.CreateTime = node.InnerText;
                            break;
                        case "MsgType":    //编码 
                            myWX_Model_Voice.MsgType = node.InnerText;
                            break;
                        case "MediaId":    //编码 
                            myWX_Model_Voice.MediaId = node.InnerText;
                            break;
                        case "MsgId":    //编码 
                            myWX_Model_Voice.MsgId = node.InnerText;
                            break;
                    }
                }
            }
            return myWX_Model_Voice;
        }



    }


    public WX_Model()
    {

    }


    //    <ToUserName><![CDATA[gh_f4feee3012a1]]></ToUserName>
    //<FromUserName><![CDATA[otI-ujh5kzgFdIqVPfFAqz_P2FtA]]></FromUserName>
    //<CreateTime>1376211723</CreateTime>
    //<MsgType><![CDATA[event]]></MsgType>





}


// <xml>
//<ToUserName><![CDATA[gh_f4feee3012a1]]></ToUserName>
//<FromUserName><![CDATA[otI-ujh5kzgFdIqVPfFAqz_P2FtA]]></FromUserName>
//<CreateTime>1376211723</CreateTime>
//<MsgType><![CDATA[event]]></MsgType>
//<Event><![CDATA[CLICK]]></Event>
//<EventKey><![CDATA[M1001]]></EventKey>
//</xml>


//public class class_CLICK
//{
//    private string Public_StrError = null;
//    public string ToUserName
//    {
//        get { return ToUserName; }
//        set { ToUserName = value; }
//    }
//    public string ToUserName
//    {
//        get { return ToUserName; }
//        set { ToUserName = value; }
//    }
//    public string ToUserName
//    {
//        get { return ToUserName; }
//        set { ToUserName = value; }
//    }
//}
