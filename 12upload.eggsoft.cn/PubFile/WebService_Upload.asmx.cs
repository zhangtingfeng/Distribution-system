using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// WebService_Upload 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [Serializable]    
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_Upload : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
       public string UploadFile(byte[] fBytes, string fileName, string DirectoryName, ref string eMsg, string strOldImgName)
        {
            try
            {
                string savePath = "\\UpLoad\\" + DirectoryName;
                if (!Directory.Exists(Server.MapPath("\\UpLoad\\" + DirectoryName)))
                {
                    Directory.CreateDirectory(Server.MapPath("\\UpLoad\\" + DirectoryName));
                }
                string WPath = Server.MapPath(savePath + "\\" + fileName);
                MemoryStream memoryStream = new MemoryStream(fBytes); //1.定义并实例化一个内存流，以存放提交上来的字节数组。
                FileStream fileUpload = new FileStream(WPath, FileMode.Create); ///2.定义实际文件对象，保存上载的文件。
                memoryStream.WriteTo(fileUpload); ///3.把内存流里的数据写入物理文件
                memoryStream.Close();
                fileUpload.Close();
                fileUpload = null;
                memoryStream = null;

                eMsg = "上传成功";
                if (!string.IsNullOrEmpty(strOldImgName))
                {
                    if (File.Exists(Server.MapPath(strOldImgName)))
                    {
                        File.Delete(Server.MapPath(strOldImgName));
                    }
                }
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                eMsg = ex.Message;
                return "Error";
            }
            return "Error";
        }


    }
}
