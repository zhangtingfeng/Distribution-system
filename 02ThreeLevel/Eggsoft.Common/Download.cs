using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace Eggsoft.Common
{
    public class Download
    {



        #region 文件下载
        public static void DownLoadFile(string address, string filename)
        {//address 文件下载路径,filename文件存放的本地路径
            WebClient client = new WebClient();
            client.DownloadFile(address, System.Web.HttpContext.Current.Server.MapPath(filename));
        }


        public static void DownLoadFile_Service(string address, string filename)
        {

            string ConnStr = ConfigurationManager.AppSettings["UpLoadURL"].ToString();

            string url =ConnStr+ "/PubFile/DownLoadService.asmx";
            string[] args = new string[2];
            args[0] = address;
            args[1] = filename;
            object result = WebServiceHelper.WsCaller.InvokeWebService(url, "DownLoadFile", args);
        

            //SR_DownLoad.DownLoadServiceSoapClient upl = new SR_DownLoad.DownLoadServiceSoapClient("DownLoadServiceSoap");
            //upl.DownLoadFile(address, filename);

        }
        #endregion



        /// <summary>  
        /// DownloadFileAsync异步下载文件,下载时间还是很长  
        /// 会出现报401错误的,这个需要你去设置IIS的权限了  
        /// </summary>  
        /// <param name="Url"></param>  
        public void OperDownloadFileAsync(string Url, string filename)
        {
            WebClient _client = new WebClient();
            string Path = System.Web.HttpContext.Current.Server.MapPath(filename);
            _client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
            _client.DownloadFileAsync(new Uri(Url, UriKind.RelativeOrAbsolute), Path);
        }

        /// <summary>  
        /// 完成异步下载文件  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        public void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();  
            if (e.Error == null && e.Cancelled == false)
            {
                //this.Label1.Text = "下载成功!";
            }
            else
            {
                //this.Label1.Text = "下载失败,原因:" + e.Error.Message;

            }
        }



        /// <summary>
        /// 输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        /// </summary>
        /// <param name="_Request">Page.Request对象</param>
        /// <param name="_Response">Page.Response对象</param>
        /// <param name="_fileName">下载文件名</param>
        /// <param name="_fullPath">带文件名下载路径</param>
        /// <param name="_speed">每秒允许下载的字节数</param>
        /// <returns>返回是否成功</returns>
        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    int pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor((decimal)1000 * pack / _speed) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((decimal)(fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        //

    }
}
