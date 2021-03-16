using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// WS_GetFileList 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_GetFileList : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private static Object muAPPCODE_getFileListddd = new object();
        [WebMethod]
        public String APPCODE_getFileList(String strFilePath)//强行获取
        {
            string strcustormerList = "";

            try
            {
                lock (muAPPCODE_getFileListddd)
                {
                    //如果上传文件夹不存在,则根据config创建一个
                    if (!Directory.Exists(Server.MapPath(strFilePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(strFilePath));
                    }
                    string fpath = Server.MapPath(strFilePath);
                    DirectoryInfo di = new DirectoryInfo(fpath);

                    List<Eggsoft.Common.FileSort_GetFileList> custormerList = new List<Eggsoft.Common.FileSort_GetFileList>();

                    foreach (FileSystemInfo fsi in di.GetFileSystemInfos())
                    {

                        if (fsi is FileInfo)//如果是文件
                        {
                            FileInfo fi = (FileInfo)fsi;
                            int sourceWidth = 0;
                            int sourceHeight = 0;
                            try
                            {
                                System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(fpath + fi.Name);
                                sourceWidth = imgPhoto.Width;
                                sourceHeight = imgPhoto.Height;
                            }
                            catch { }
                            finally { }

                            custormerList.Add(new Eggsoft.Common.FileSort_GetFileList { FileName = fi.Name, FileNameDate = fi.CreationTime, FileSize = fi.Length, File_Width = sourceWidth, File_Height = sourceHeight });
                        }
                    }
                    custormerList.Sort(intBigToSmall_TimeSpan);
                    strcustormerList = Eggsoft.Common.XmlHelper.XmlSerialize(custormerList, System.Text.Encoding.UTF8);
                    Eggsoft.Common.debug_Log.Call_WriteLog("strcustormerList:" + strcustormerList);
                }
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "获取目录", strFilePath);
            }
            finally
            {

            }
            return strcustormerList;
        }


        private static int intBigToSmall_TimeSpan(Eggsoft.Common.FileSort_GetFileList worker1, Eggsoft.Common.FileSort_GetFileList worker2)
        {
            //DateTime dt1 = new DateTime(2013, 5, 1, 10, 23, 10);  
            //DateTime dt2 = DateTime.Now;          
            TimeSpan ts = worker1.FileNameDate.Subtract(worker2.FileNameDate);
            Int32 intMild = -ts.Milliseconds;

            return intMild;
        }

    }
}
