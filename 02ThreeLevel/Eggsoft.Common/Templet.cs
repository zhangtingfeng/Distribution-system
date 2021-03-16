using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Eggsoft.Common
{
    public class DoTemplet
    {

        
        /// < summary> 
        /// 传入URL返回网页的html代码  ASP.NET模版生成HTML静态页面方案1： 
        /// < /summary> 
        /// < param name="Url">URL< /param> 
        /// < returns>< /returns> 
        public static string getUrltoHtml(string Url) 
        {
            string errorMsg = ""; 
            try 
            { 
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url); 
                // Get the response instance. 
                System.Net.WebResponse wResp =wReq.GetResponse(); 
                // Read an HTTP-specific property 
                //if (wResp.GetType() ==HttpWebResponse) 
                //{ 
                //DateTime updated =((System.Net.HttpWebResponse)wResp).LastModified; 
                //} 
                // Get the response stream. 
                System.IO.Stream respStream = wResp.GetResponseStream(); 
                // Dim reader As StreamReader = New StreamReader(respStream) 
                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, System.Text.Encoding.GetEncoding("gb2312")); 
                return reader.ReadToEnd(); 
              } 
             catch(System.Exception ex) 
             { 
                errorMsg = ex.Message ; 
            } 
            return ""; 
        }


        /// < summary> 
        /// 你可以用这个函数获取网页的客户端的html代码，然后保存到.html文件里就可以了。 
        //ASP.NET模版生成HTML静态页面方案2： 
        //生成单个的静态页面不是难点，难的是各个静态页面间的关联和链接如何保持完整； 
        //特别是在页面频繁更新、修改、或删除的情况下； 
        //像阿里巴巴的页面也全部是html的，估计用的是地址映射的功能 
        //可以看看这个页面，分析一下他的“竞价倒计时”功能 
        //http://info.china.alibaba.com/news/subject/v1-s5011580.html?head=top4&Bidding=home5 
        //ASP.Net生成静态HTML页 
        //在Asp中实现的生成静态页用到的FileSystemObject对象! 
        //在.Net中涉及此类操作的是System.IO 
        //以下是程序代码 注:此代码非原创!参考别人代码 
        /// < /summary> 
        /// < param name="Url">URL< /param> 
        /// < returns>< /returns> 
        //生成HTML页 
        public static bool WriteFile(string strText, string strContent, string strAuthor)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/news/");
            Encoding code = Encoding.GetEncoding("gb2312");
            // 读取模板文件 
            string temp = System.Web.HttpContext.Current.Server.MapPath("/news/text.html");
            StreamReader sr = null;
            StreamWriter sw = null;
            string str = "";
            try
            {
                sr = new StreamReader(temp, code);
                str = sr.ReadToEnd(); // 读取文件 
            }
            catch (Exception exp)
            {
                HttpContext.Current.Response.Write(exp.Message);
                HttpContext.Current.Response.End();
                sr.Close();
            }

            string htmlfilename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            // 替换内容 
            // 这时,模板文件已经读入到名称为str的变量中了 
            str = str.Replace("ShowArticle", strText); //模板页中的ShowArticle 
            str = str.Replace("biaoti", strText);
            str = str.Replace("content", strContent);
            str = str.Replace("author", strAuthor);
            // 写文件 
            try
            {
                sw = new StreamWriter(path + htmlfilename, false, code);
                sw.Write(str);
                sw.Flush();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
                HttpContext.Current.Response.End();
            }
            finally
            {
                sw.Close();
            }
            return true;
        }
       

    }
}
