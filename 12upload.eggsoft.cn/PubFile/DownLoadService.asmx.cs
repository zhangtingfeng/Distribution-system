using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// DownLoadService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DownLoadService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void DownLoadFile(string address, string filename)
        {
            if (!Eggsoft.Common.FileFolder.File_Exists(filename))
            {
                //address 文件下载路径,filename文件存放的本地路径
                WebClient client = new WebClient();

                string strMapPath = System.Web.HttpContext.Current.Server.MapPath(filename);

                client.DownloadFile(address, strMapPath);
            }
        }
    }
}
