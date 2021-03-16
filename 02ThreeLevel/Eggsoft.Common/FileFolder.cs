using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;
using System.Collections;
using System.Net;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{


    public class FileSort_GetFileList
    {
        public string FileName { get; set; }
        public DateTime FileNameDate { get; set; }
        public long FileSize { get; set; }

        public int File_Width { get; set; }

        public int File_Height { get; set; }
    }

    public class FileInformation : FileSort_GetFileList
    {
        public string MD5Marker { get; set; }
        public string FullName { get; set; }
        public string DirectoryName { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }

        public string QiNiuUplodFullName { get; set; }
    }


    #region 递归遍历指定文件夹内的所有文件（包含子文件夹）
    public class DirectoryAllFiles
    {
        private String strPath = ""; private String strHeadPath = "";
        private List<FileInformation> FileList = null;
        public List<FileInformation> getDirectoryAllFiles(string strargPath, string strargHeadPath)
        {
            strPath = strargPath;
            strHeadPath = strargHeadPath;

            FileList = new List<FileInformation>();
            GetAllFiles(new System.IO.DirectoryInfo(strPath));
            return FileList;
        }

        private List<FileInformation> GetAllFiles(DirectoryInfo dir)
        {
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {


                FileInformation ddd000 = (new FileInformation
                {
                    FileName = fi.Name,
                    CreationTime = fi.CreationTime,
                    CreationTimeUtc = fi.CreationTimeUtc,
                    DirectoryName = fi.DirectoryName,
                    FullName = fi.FullName,
                    LastWriteTime = fi.LastWriteTime,
                    LastWriteTimeUtc = fi.LastWriteTimeUtc,
                    FileSize = fi.Length,
                    QiNiuUplodFullName = strHeadPath + fi.FullName.Remove(0, strPath.Length).Replace("\\", "/").Replace("\"", "/")
                });
                StringBuilder strSign = new StringBuilder();
                strSign.Append(ddd000.FileName);
                strSign.Append(ddd000.CreationTime);
                strSign.Append(ddd000.CreationTimeUtc);
                strSign.Append(ddd000.DirectoryName);
                strSign.Append(ddd000.FullName);
                strSign.Append(ddd000.LastWriteTime);
                strSign.Append(ddd000.LastWriteTimeUtc);
                strSign.Append(ddd000.FileSize);

                ddd000.MD5Marker = Eggsoft.Common.DESCrypt.GetMd5Str32(strSign.ToString());
                FileList.Add(ddd000);
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAllFiles(d);
            }
            return FileList;
        }
    }


    #endregion
    /// <summary>
    /// 数据库表影射
    /// </summary>
    public class FileFolder
    {





        ///1.判断远程文件是否存在 
        ///fileUrl:远程文件路径，包括IP地址以及详细的路径
        public static bool RemoteFileExists(string fileUrl)
        {
            //Eggsoft.Common.debug_Log.Call_WriteLog("a1");

            bool result = false;//下载结果
            WebResponse response = null;
            //Eggsoft.Common.debug_Log.Call_WriteLog("a2");
            try
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("a31");
                WebRequest req = WebRequest.Create(fileUrl);
                //Eggsoft.Common.debug_Log.Call_WriteLog("a32");
                response = req.GetResponse();
                //Eggsoft.Common.debug_Log.Call_WriteLog("a33");
                result = response == null ? false : true;
                //Eggsoft.Common.debug_Log.Call_WriteLog("a34");
                //Eggsoft.Common.debug_Log.Call_WriteLog("a35");

            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(fileUrl, "判断远程文件是否存在", "不存在");
                //Eggsoft.Common.debug_Log.Call_WriteLog(ex, "判断远程文件是否存在");

                result = false;
            }
            finally
            {
                if (response != null)
                {
                    // Eggsoft.Common.debug_Log.Call_WriteLog("a5");

                    response.Close();
                }
            }
            //Eggsoft.Common.debug_Log.Call_WriteLog("a6");

            return result;
        }

        public static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);    // 8是 file:// 的长度

            string[] arrSection = _CodeBase.Split(new char[] { '/' });

            string _FolderPath = "";
            for (int i = 0; i < arrSection.Length - 1; i++)
            {
                _FolderPath += arrSection[i] + "/";
            }

            return _FolderPath;
        }




        public static bool File_Exists(String strPathFile)
        {
            string strpath = System.Web.HttpContext.Current.Server.MapPath(strPathFile);

            if (File.Exists(strpath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static String btnFileUpload(FileUpload argFileUpload1, String strUploadimages)
        {
            String strReturn = "";
            if (argFileUpload1.HasFile)
            {

                string strServerPath = System.Web.HttpContext.Current.Server.MapPath(strUploadimages);
                string fileContentType = argFileUpload1.PostedFile.ContentType;
                //if (fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg")
                //{
                string name = argFileUpload1.PostedFile.FileName;                  // 客户端文件路径
                System.Threading.Thread.Sleep(500);
                FileInfo file = new FileInfo(name);
                string fileName = file.Name;                                    // 文件名称
                string fileType = fileName.Substring(fileName.LastIndexOf("."));
                string timenow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:sssss");//得到系统时间
                string dNow = timenow.Trim().Replace("-", "").Replace(":", "").Replace(" ", "");
                fileName = dNow.ToString() + fileType.ToString();   //实现文件的重命名

                string fileName_s = "s_" + fileName;                           // 缩略图文件名称
                string fileName_sy = "sy_" + fileName;                         // 水印图文件名称（文字）
                string fileName_syp = "syp_" + fileName;                       // 水印图文件名称（图片）
                string webFilePath = strServerPath + fileName;        // 服务器端文件路径
                if (!Directory.Exists(webFilePath)) Eggsoft.Common.FileFolder.makeFolder(strServerPath);
                string webFilePath_s = strServerPath + fileName_s;　　// 服务器端缩略图路径
                string web_DB_FilePath_s = strUploadimages + fileName_s;　　// 服务器端缩略图路径
                string web_DB_FilePath = strUploadimages + fileName;　　// 服务器端缩略图路径

                if (File.Exists(webFilePath))
                {
                    File.Delete(webFilePath);
                }

                if (!File.Exists(webFilePath))
                {
                    try
                    {
                        argFileUpload1.SaveAs(webFilePath);                                // 使用 SaveAs 方法保存文件
                        strReturn = web_DB_FilePath;
                    }
                    catch (Exception ex)
                    {
                        JsUtil.ShowMsg("提示：文件上传失败，失败原因：" + ex.Message, "javascript:history.back()");
                    }
                }
                else
                {
                    JsUtil.ShowMsg("提示：文件已经存在，请重命名后上传", "javascript:history.back()");
                }
            }
            return strReturn;
        }

        public static String btnFileUpload_Small(FileUpload argFileUpload1, String serverpath_Uploadimages)
        {
            String strReturn = "";
            if (argFileUpload1.HasFile)
            {
                string fileContentType = argFileUpload1.PostedFile.ContentType;
                if (fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg")
                {
                    string name = argFileUpload1.PostedFile.FileName;                  // 客户端文件路径

                    FileInfo file = new FileInfo(name);
                    string fileName = file.Name;                                    // 文件名称
                    string fileType = fileName.Substring(fileName.LastIndexOf("."));
                    string timenow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");//得到系统时间
                    string dNow = timenow.Trim().Replace("-", "").Replace(":", "").Replace(" ", "");
                    fileName = dNow.ToString() + fileType.ToString();   //实现文件的重命名

                    string fileName_s = "s_" + fileName;                           // 缩略图文件名称
                    string fileName_sy = "sy_" + fileName;                         // 水印图文件名称（文字）
                    string fileName_syp = "syp_" + fileName;                       // 水印图文件名称（图片）
                    string webFilePath = System.Web.HttpContext.Current.Server.MapPath(serverpath_Uploadimages) + fileName;        // 服务器端文件路径

                    string webFilePath_s = System.Web.HttpContext.Current.Server.MapPath(serverpath_Uploadimages) + fileName_s;　　// 服务器端缩略图路径
                    string web_DB_FilePath_s = serverpath_Uploadimages + fileName_s;　　// 服务器端缩略图路径

                    if (File.Exists(webFilePath))
                    {
                        File.Delete(webFilePath);
                    }
                    if (!File.Exists(webFilePath))
                    {
                        try
                        {
                            argFileUpload1.SaveAs(webFilePath);                                // 使用 SaveAs 方法保存文件

                            Eggsoft.Common.Image.MakeThumbnail(webFilePath, webFilePath_s, 130, 130, "Cut");     // 生成缩略图方法
                            Eggsoft.Common.FileFolder.DeleteFile(webFilePath);//卸磨杀驴
                            strReturn = web_DB_FilePath_s;

                        }
                        catch (Exception ex)
                        {
                            JsUtil.ShowMsg("提示：文件上传失败，失败原因：" + ex.Message, "javascript:history.back()");
                        }
                    }
                    else
                    {
                        JsUtil.ShowMsg("提示：文件已经存在，请重命名后上传", "javascript:history.back()");
                    }
                }
                else
                {
                    JsUtil.ShowMsg("提示：缩略图文件类型不符!", "javascript:history.back()");

                }
            }
            return strReturn;
        }



        public static void makeFolder(string strPath)
        {
            DirectoryInfo di = Directory.CreateDirectory(strPath);
        }

        public static void DeleteFile(string strDeleteFile)
        {
            try
            {
                //string strMapDeleteFile = System.Web.HttpContext.Current.Server.MapPath(strDeleteFile);

                if (File.Exists(strDeleteFile))
                {
                    File.Delete(strDeleteFile);///防止笨蛋人删除 文件 我很麻烦
                }
            }
            catch
            { }

            finally
            { }
        }

        //读取txt文件的内容
        public static string MoveAJAXFile(string strfile, string strUpLoadpath)
        {
            string strServerPath = System.Web.HttpContext.Current.Server.MapPath(strUpLoadpath);

            if (!Directory.Exists(strServerPath)) Eggsoft.Common.FileFolder.makeFolder(strServerPath);


            FileInfo file = new FileInfo(strfile);
            string fileName = file.Name;                                    // 文件名称
            string fileType = fileName.Substring(fileName.LastIndexOf("."));
            string timenow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");//得到系统时间
            string dNow = timenow.Trim().Replace("-", "").Replace(":", "").Replace(" ", "");
            fileName = dNow.ToString() + fileType.ToString();   //实现文件的重命名

            string webFilePath_Source = System.Web.HttpContext.Current.Server.MapPath(strfile);        // 服务器端文件路径
            string webFilePath_Destination = strServerPath + fileName;        // 服务器端文件路径

            File.Move(webFilePath_Source, webFilePath_Destination);

            return strUpLoadpath + fileName;
        }


        //读取txt文件的内容
        public static string Read_Remote_File(string strMapPathfile)
        {
            string strout;
            strout = "";
            if (!RemoteFileExists(strMapPathfile))
            {
            }
            else
            {
                WebRequest request = WebRequest.Create(strMapPathfile);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                strout = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            return strout;
        }


        //读取txt文件的内容
        public static string ReadFile(string strfile)
        {
            string strout;
            strout = "";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(strfile)))
            {
            }
            else
            {
                StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(strfile), System.Text.Encoding.UTF8);
                String input = sr.ReadToEnd();
                sr.Close();
                strout = input;
            }
            return strout;
        }

        public static string getFileType(string strPathFile)
        {
            string fileType = strPathFile.Substring(strPathFile.LastIndexOf("."));
            return fileType;
        }

        public static string getFileNormalName(string strPathFile)
        {
            string str_MapPath = System.Web.HttpContext.Current.Server.MapPath(strPathFile);

            FileInfo file = new FileInfo(str_MapPath);
            string fileName = file.Name;                                    // 文件名称        
            string FileNormalName = fileName.Substring(0, fileName.LastIndexOf("."));
            return FileNormalName;
        }

        public static string getDirectoryName(string strPathFile)
        {
            string str_MapPath = System.Web.HttpContext.Current.Server.MapPath(strPathFile);

            FileInfo file = new FileInfo(str_MapPath);
            //string fileName = file.Name;                                    // 文件名称
            string PathName = file.DirectoryName;                                    // 文件名称
            return PathName;
        }


        public static string WriteFile150px(string strPathFile)
        {
            //Server.MapPath(
            string str_MapPath = System.Web.HttpContext.Current.Server.MapPath(strPathFile);

            FileInfo file = new FileInfo(str_MapPath);
            string fileName = file.Name;                                    // 文件名称
            string PathName = file.DirectoryName;                                    // 文件名称


            string annotherPath = PathName + "\\" + "3DImages";
            if (!Directory.Exists(annotherPath)) Directory.CreateDirectory(annotherPath);

            string fileType = fileName.Substring(fileName.LastIndexOf("."));
            string SamllFilename = annotherPath + "/" + fileName.Substring(0, fileName.IndexOf(".")) + "_150px" + fileType;                                    // 文件名称
            if (!File.Exists(SamllFilename))
            {
                Eggsoft.Common.Image.MakeThumbnail(str_MapPath, SamllFilename, 150, 150, "HW");     // 生成缩略图方法
            }

            string sSamllFilenameParent = strPathFile.Substring(0, strPathFile.LastIndexOf("/"));
            string sSamllFilename = sSamllFilenameParent + "/3DImages/" + fileName.Substring(0, fileName.IndexOf(".")) + "_150px" + fileType;
            //string sSamllFilename = strPathFile.Substring(0, strPathFile.LastIndexOf("/")) + "/" + fileName.Substring(0, fileName.IndexOf(".")) + "_150px" + fileType;


            return sSamllFilename;
        }


        public static FileInfo[] MergerArray(FileInfo[] First, FileInfo[] Second)
        {
            FileInfo[] result = new FileInfo[First.Length + Second.Length];
            First.CopyTo(result, 0);
            Second.CopyTo(result, First.Length);
            return result;
        }
        public static FileInfo[] getAllFileList_IterationFile(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            //输出当前目录。 
            //Response.Write(di.ToString() + "<br />");
            //取得当前目录中所有文件 
            FileInfo[] fiArray = di.GetFiles();

            //循环每一个文件 
            for (int i = 0; i < fiArray.Length; i++)
            {
                //Response.Write(fiArray[i].ToString() + "<br/>");
            }

            //每个目录结束，写一空行。 
            //Response.Write("----------------------------------------------------------------------------<br/>");
            //取得当前目录中所有子目录 
            DirectoryInfo[] diArray = di.GetDirectories();

            //循环每一个目录 
            for (int j = 0; j < diArray.Length; j++)
            {
                fiArray = MergerArray(fiArray, getAllFileList_IterationFile(diArray[j].FullName));
            }
            return fiArray;
        }

        /// <summary>
        /// 写入HTML文件
        /// </summary>
        /// <param name="str">HTML代码</param>
        /// <param name="htmlfilename">完整带路径的文件名</param>
        /// <returns></returns>
        public static bool WriteHtmlFile(string str, string htmlfilename)
        {
            string strpath = System.Web.HttpContext.Current.Server.MapPath(htmlfilename);

            Encoding code = Encoding.GetEncoding("UTF-8");//定义文字编码 UTF-8

            StreamWriter sw = null;
            // 写文件 
            try
            {
                sw = new StreamWriter(strpath, false, code);
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
                sw.Dispose();
                sw.Close();
            }
            return true;
        }

        public static void WriteFile(string strPathFile, string strContent)
        {
            string strpath = System.Web.HttpContext.Current.Server.MapPath(strPathFile);
            StreamWriter sr = null;
            //new StreamWriter(strpath);

            if (!File.Exists(strpath))
            {
                sr = File.CreateText(strpath);
            }
            else
            {
                File.Delete(strpath);
                sr = File.CreateText(strpath);
            }
            sr.Write(strContent);
            sr.Close();

        }

        #region 物理路径和相对路径的转换
        //本地路径转换成URL相对路径  
        public static string urlconvertor(string imagesurl1)
        {
            string tmpRootDir = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //imagesurl2 = imagesurl2.Replace(@"Aspx_Uc/", @"");
            return imagesurl2;
        }
        //相对路径转换成服务器本地物理路径  
        public static string urlconvertorlocal(string imagesurl1)
        {
            string tmpRootDir = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录 
            string imagesurl2 = tmpRootDir + imagesurl1.Replace(@"/", @"\"); //转换成绝对路径 
            return imagesurl2;
        }
        #endregion


        //asp.net读取模板生成HTML
        public static string ReadRemoteHTML(string strReadHTMLPath)
        {
            string html = "";
            try
            {

                WebRequest request = WebRequest.Create(strReadHTMLPath);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                html = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                response.Close();

                ;

            }
            catch { }

            return html;
        }


        //asp.net读取模板生成HTML
        public static string ReadHTML(string strReadHTMLPath)
        {
            string html = "";
            try
            {
                string gh = HttpRuntime.AppDomainAppPath;
                StreamReader reader = new StreamReader(gh + strReadHTMLPath, Encoding.GetEncoding("utf-8"));
                html = reader.ReadToEnd();
                reader.Close();

            }
            catch { }

            return html;
        }



        //asp.net读取模板生成HTML
        public static string ReadTemple(string strReadTemple)
        {
            string html = "";

            string CacheKey = "HuanCunKey_" + strReadTemple.Replace("/", "").Replace("\"", "").Replace(".", "");
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    string gh = HttpRuntime.AppDomainAppPath;
                    StreamReader reader = new StreamReader(gh + strReadTemple, Encoding.GetEncoding("utf-8"));
                    html = reader.ReadToEnd();
                    reader.Close();
                    //Eggsoft.Common.debug_Log.Call_WriteLog("gh + strReadTemple" + DateTime.Now + gh + strReadTemple);
                    //Eggsoft.Common.debug_Log.Call_WriteLog("gh + strReadTemple html" + DateTime.Now + html);

                    //objType = Assembly.Load(path).CreateInstance(CacheKey);

                    //先不执行 便于调试
                    System.Web.Configuration.CompilationSection ds = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
                    bool isDebugEnable = ds.Debug;
                    if (isDebugEnable == false) Eggsoft.Common.DataCache.SetCache(CacheKey, html);// 写入缓存   
                }
                catch { }
            }
            else
            {
                html = (string)objType;
            }
            //return (string)objType;



            return html;
            //html = html.Replace("<!--123-->", "好好");
            ////Response.Write(html);
            //StreamWriter sw = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("311.htm"), false, System.Text.Encoding.GetEncoding("utf8"));
            //sw.WriteLine(html);
            //sw.Flush();
            //sw.Close();
        }

        //asp.net读取模板生成HTML
        public static string ReadRemoteTempleToCacheKey_ShopClientID(string strReadTemple, int shopClientID)
        {
            string html = "";

            string CacheKey = "HuanCunKey" + shopClientID + "_" + strReadTemple.Replace(":", "_").Replace("/", "_").Replace(".", "_").Replace("?", "_").Replace("&", "_");
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {

                    if (!RemoteFileExists(strReadTemple))
                    {
                    }
                    else
                    {
                        WebRequest request = WebRequest.Create(strReadTemple);
                        WebResponse response = request.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                        html = reader.ReadToEnd();
                        reader.Close();
                        reader.Dispose();
                        response.Close();
                    }
                    //return strout;



                    //objType = Assembly.Load(path).CreateInstance(CacheKey);

                    //先不执行 便于调试
                    System.Web.Configuration.CompilationSection ds = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
                    bool isDebugEnable = ds.Debug;
                    if (isDebugEnable == false) Eggsoft.Common.DataCache.SetCache(CacheKey, html);// 写入缓存   
                }
                catch { }
            }
            else
            {
                html = (string)objType;
            }
            //return (string)objType;
            return html;
        }


        //真正判断文件类型的关键函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hifile"></param>
        /// <param name="strValidFileType"> mp3    jpgbmpgjif  jpg </param>
        /// <returns></returns>
        public static bool IsAllowedExtension(FileUpload hifile, string strValidFileType)
        {
            System.IO.FileStream fs = new System.IO.FileStream(hifile.PostedFile.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            //这里的位长要具体判断.
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {

            }
            r.Close();
            fs.Close();
            if (strValidFileType == "mp3" && fileclass == "215216")
            {
                return true;
            }
            else if (strValidFileType == "jpg" && fileclass == "255216")
            {
                return true;
            }
            else if ((fileclass == "255216" || fileclass == "7173") && (strValidFileType == "jpgbmpgjif"))//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }


    /// <summary>
    /// 文件排序类
    /// </summary>
    public class FileSort : IComparer
    {
        private FileOrder _fileorder;
        private FileAsc _fileasc;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileSort()
            : this(FileOrder.Name, FileAsc.Asc)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileorder"></param>
        public FileSort(FileOrder fileorder)
            : this(fileorder, FileAsc.Asc)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileorder"></param>
        /// <param name="fileasc"></param>
        public FileSort(FileOrder fileorder, FileAsc fileasc)
        {
            _fileorder = fileorder;
            _fileasc = fileasc;
        }

        /// <summary>
        /// 比较函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            FileInfo file1 = x as FileInfo;
            FileInfo file2 = y as FileInfo;
            FileInfo file3;

            if (file1 == null || file2 == null)
                throw new ArgumentException("参数不是FileInfo类实例.");

            if (_fileasc == FileAsc.Desc)
            {
                file3 = file1;
                file1 = file2;
                file2 = file3;
            }

            switch (_fileorder)
            {
                case FileOrder.Name:
                    return file1.Name.CompareTo(file2.Name);
                case FileOrder.Length:
                    return file1.Length.CompareTo(file2.Length);
                case FileOrder.Extension:
                    return file1.Extension.CompareTo(file2.Extension);
                case FileOrder.CreationTime:
                    return file1.CreationTime.CompareTo(file2.CreationTime);
                case FileOrder.LastAccessTime:
                    return file1.LastAccessTime.CompareTo(file2.LastAccessTime);
                case FileOrder.LastWriteTime:
                    return file1.LastWriteTime.CompareTo(file2.LastWriteTime);
                default:
                    return 0;
            }
        }


    }


    /// <summary>
    /// 排序依据
    /// </summary>
    public enum FileOrder
    {
        /// <summary>
        /// 文件名
        /// </summary>
        Name,
        /// <summary>
        /// 大小
        /// </summary>
        Length,
        /// <summary>
        /// 类型
        /// </summary>
        Extension,
        /// <summary>
        /// 创建时间
        /// </summary>
        CreationTime,
        /// <summary>
        /// 访问时间
        /// </summary>
        LastAccessTime,
        /// <summary>
        /// 修改时间
        /// </summary>
        LastWriteTime
    }

    /// <summary>
    /// 升序降序
    /// </summary>
    public enum FileAsc
    {
        /// <summary>
        /// 升序
        /// </summary>
        Asc,
        /// <summary>
        /// 降序
        /// </summary>
        Desc
    }




}
