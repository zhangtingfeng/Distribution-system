using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
///PostMenu 的摘要说明
/// </summary>
public class PostMenu
{

    public static string delete_PostMenu(String strACCESS_TOKEN)
	{

        string url="https://api.weixin.qq.com/cgi-bin/menu/delete?access_token="+strACCESS_TOKEN;
        return Post(url, "");
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static string Post(string url, string data)
    {
        string returnData = null;
        try
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.ContentLength = buffer.Length;
            Stream postData = webReq.GetRequestStream();
            postData.Write(buffer, 0, buffer.Length);
            postData.Close();
            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            Stream answer = webResp.GetResponseStream();
            StreamReader answerData = new StreamReader(answer);
            returnData = answerData.ReadToEnd();
        }
        catch (Exception)
        {
           // Response.Write(ex.Message);
        }
        return returnData.Trim() + "\n";
    }


    public static string PostXmlToURL(string url, string data)
    {
        HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(url);
        hwr.Method = "POST";
        Stream stream = hwr.GetRequestStream();
        StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.UTF8);
        sw.Write(data);
        sw.Close();
        stream = hwr.GetResponse().GetResponseStream();
        StreamReader sr = new StreamReader(stream);
        string result = sr.ReadToEnd();
        sr.Close();
        return result;
    }

    public static string PostXml(string url, string strPost)
    {
        string result = "";

        StreamWriter myWriter = null;
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
        objRequest.Method = "POST";
        //objRequest.ContentLength = strPost.Length;
        objRequest.ContentType = "text/xml";//提交xml 
        //objRequest.ContentType = "application/x-www-form-urlencoded";//提交表单
        try
        {
        myWriter = new StreamWriter(objRequest.GetRequestStream());
        myWriter.Write(strPost);
        }
        catch (Exception e)
        {
        return e.Message;
        }
        finally
        {
        myWriter.Close();
        }

        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
        result = sr.ReadToEnd();
        sr.Close();
        }
        return result;
        }
    }
