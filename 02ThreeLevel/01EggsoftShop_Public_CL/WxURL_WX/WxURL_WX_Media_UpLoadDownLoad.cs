using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Xml;
using System.Net;
using System.IO;

/// <summary>
///WX_Model 的摘要说明
/// </summary>
public class WxURL_WX_Media_UpLoadDownLoad
{
    /// <summary>
    /// 下载保存多媒体文件,返回多媒体保存路径
    /// </summary>
    /// <param name="ACCESS_TOKEN"></param>
    /// <param name="MEDIA_ID"></param>
    /// <returns></returns>
    public string GetMultimedia(string ACCESS_TOKEN, string MEDIA_ID)
    {
        string file = string.Empty;
        string content = string.Empty;
        string strpath = string.Empty;
        string savepath = string.Empty;
        string stUrl = "https://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;

        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

        req.Method = "GET";
        using (WebResponse wr = req.GetResponse())
        {
            HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

            strpath = myResponse.ResponseUri.ToString();
            Eggsoft.Common.debug_Log.Call_WriteLog("接收类别://" + myResponse.ContentType);
            WebClient mywebclient = new WebClient();
            savepath = System.Web.HttpContext.Current.Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
            Eggsoft.Common.debug_Log.Call_WriteLog("路径://" + savepath);
            try
            {
                mywebclient.DownloadFile(strpath, savepath);
                file = savepath;
            }
            catch (Exception ex)
            {
                savepath = ex.ToString();
            }

        }
        return file;
    }
    class wxmessage
    {
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string MsgType { get; set; }
        public string EventName { get; set; }
        public string Content { get; set; }
        public string Recognition { get; set; }
        public string MediaId { get; set; }
        public string EventKey { get; set; }
    }

    private wxmessage GetWxMessage()
    {
        wxmessage wx = new wxmessage();
        StreamReader str = new StreamReader(System.Web.HttpContext.Current.Request.InputStream, System.Text.Encoding.UTF8);
        XmlDocument xml = new XmlDocument();
        xml.Load(str);
        wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
        wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
        wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
        if (wx.MsgType.Trim() == "text")
        {
            wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
        }
        if (wx.MsgType.Trim() == "event")
        {
            wx.EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
            wx.EventKey = xml.SelectSingleNode("xml").SelectSingleNode("EventKey").InnerText;
        }
        if (wx.MsgType.Trim() == "voice")
        {
            wx.Recognition = xml.SelectSingleNode("xml").SelectSingleNode("Recognition").InnerText;
        }
        if (wx.MsgType.Trim() == "image")
        {
            wx.MediaId = xml.SelectSingleNode("xml").SelectSingleNode("MediaId").InnerText;
        }

        return wx;
    }

    /// <summary>
    /// 上传多媒体文件,返回 MediaId
    /// </summary>
    /// <param name="ACCESS_TOKEN"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public string UploadMultimedia(string ACCESS_TOKEN, string type)
    {
        string result = "";
        string wxurl = "https://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + ACCESS_TOKEN + "&type=" + type;
        string filepath = System.Web.HttpContext.Current.Server.MapPath("image") + "\\hemeng80.jpg"; //(本地服务器的地址)
        Eggsoft.Common.debug_Log.Call_WriteLog("上传路径:" + filepath);
        WebClient myWebClient = new WebClient();
        myWebClient.Credentials = CredentialCache.DefaultCredentials;
        try
        {
            byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filepath);
            result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
            Eggsoft.Common.debug_Log.Call_WriteLog("上传result:" + result);
            //result = _mode.media_id;
        }
        catch (Exception ex)
        {
            result = "Error:" + ex.Message;
        }
        Eggsoft.Common.debug_Log.Call_WriteLog("上传MediaId:" + result);
        return result;
    }


//    protected string sendPicTextMessage(Msg _mode, string MediaId)
//    {
//        string res = string.Format(@"<xml>
//                                            <ToUserName><![CDATA[{0}]]></ToUserName>
//                                            <FromUserName><![CDATA[{1}]]></FromUserName>
//                                            <CreateTime>{2}</CreateTime>
//                                            <MsgType><![CDATA[image]]></MsgType>
//                                            <Image>
//                                            <MediaId><![CDATA[{3}]]></MediaId>
//                                            </Image>
//                                   </xml> ",
//           _mode.FromUserName, _mode.ToUserName, DateTime.Now, MediaId);

//        return res;
//    }


}

