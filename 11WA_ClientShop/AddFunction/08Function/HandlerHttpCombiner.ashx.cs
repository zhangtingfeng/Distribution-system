using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace _11WA_ClientShop.AddFunction._08Function
{
    /// <summary>
    /// HandlerHttpCombiner 的摘要说明
    /// </summary>
    public class HandlerHttpCombiner : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //查看请求头部  
            string acceptEncoding = context.Request.Headers["Accept-Encoding"].ToString().ToUpperInvariant();
            if(!String.IsNullOrEmpty(acceptEncoding))
            {
                //如果头部里有包含"GZIP","DEFLATE",表示你浏览器支持GZIP,DEFLATE压缩  
                if(acceptEncoding.Contains("GZIP"))
                {
                    //向输出流头部添加压缩信息  
                    context.Response.AppendHeader("Content-encoding", "gzip");
                    context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                }
                else if(acceptEncoding.Contains("DEFLATE"))
                {
                    //向输出流头部添加压缩信息  
                    context.Response.AppendHeader("Content-encoding", "deflate");
                    context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
                }
            }
            string outputtype = "";
            if(context.Request.QueryString["t"] != null)
                outputtype = context.Request.QueryString["t"].ToString() == "css" ? "text/css" : "text/javascript";
            context.Response.ContentType = outputtype;
            string[] filelist = context.Request.QueryString["file"].ToString().Split(',');

            Int64 filecount = filelist.Length;
            string htmlcontent = "";
            for(int i = 0; i < filecount; i++)
            {
                string path = "";
                //判断是网址还是服务器上的文件  
                path = filelist[i].ToString().IndexOf("http://") != -1 ? filelist[i] : context.Request.MapPath(filelist[i]);
                htmlcontent += Get_Http_Contents(path);
            }
            context.Response.Write(htmlcontent);



            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        private string Get_Http_Contents(string urlstr)
        {
            string coding = "utf-8";
            int time = 50000;
            string sException = null;
            string sRslt = null;
            WebResponse WebRps = null;
            WebRequest WebRqst = WebRequest.Create(urlstr);

            WebRqst.Timeout = time;
            try
            {
                WebRps = WebRqst.GetResponse();
            }
            catch(WebException e)
            {
                sException = e.Message.ToString();

            }
            catch(Exception e)
            {
                sException = e.ToString();

            }
            finally
            {
                if(WebRps != null)
                {
                    StreamReader StreamRd = new StreamReader(WebRps.GetResponseStream(), Encoding.GetEncoding(coding));
                    sRslt = StreamRd.ReadToEnd();
                    StreamRd.Close();
                    WebRps.Close();
                }
            }
            return sRslt;

        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}