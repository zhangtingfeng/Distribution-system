using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class DotUpload
    {
        /// <summary>
        /// 上传文件最大值,以KB为单位
        /// </summary>
        private int _maxFileSize;
        public int MaxFileSize
        {
            set { _maxFileSize = value; }
        }

        /// <summary>
        /// 允许的文件类型，形式"gif|jpg|bmp|png|zip|rar"
        /// </summary>
        private string _allowFileExtens;
        public string AllowFileExtens
        {
            set { _allowFileExtens = value; }
        }


        /// <summary>
        /// 文件保存路径
        /// </summary>
        private string _filePath;
        public string FilePath
        {
            set { _filePath = value; }
        }

        /// <summary>
        /// 返回上传信息
        /// </summary>
        private string _resultMessage;
        public string ResultMessage
        {
            get { return _resultMessage; }
        }

        /// <summary>
        /// 返回文件名称
        /// </summary>
        private string _resultFileName;
        public string ResultFileName
        {
            get { return _resultFileName; }
        }

        public DotUpload()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 本方法生成文件名时,加前缀
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="type"></param>
        public bool DoUpload(HttpPostedFile postedFile,string type)
        {
            bool retValue = true;
            int maxFileSize = _maxFileSize;//1M
            string allowFileExtents = _allowFileExtens;
            bool isError = false;
            try
            {
                if (postedFile.ContentLength > 0)
                {
                    if (postedFile.ContentLength > maxFileSize * 1000)
                    {
                        _resultMessage = "上文件大小超过限定值!(最大<b>" + CaculatorSize(maxFileSize * 1000) + "</b>)";
                        isError = true;
                        _resultFileName = "";
                        retValue = false;
                    }
                    if (!isError)
                    {
                        if (!IsAllowFileExtens(allowFileExtents, GetFileExtens(postedFile.FileName.ToLower())))
                        {
                            _resultMessage = "上传的文件类型不正确!(只允许上传<b>" + allowFileExtents + "</b>)";
                            isError = true;
                            _resultFileName = "";
                            retValue = false;
                        }
                    }
                    if (!isError)
                    {
                        if (!IsRightFile(postedFile))
                        {
                            _resultMessage = "上传文件出错!";
                            isError = true;
                            _resultFileName = "";
                            retValue = false;
                        }
                    }
                    if (!isError)
                    {
                        _resultMessage = "上传成功!";
                        string dtFileName=CommUtil.GetDataTimeRandomFileName() + "." + GetFileExtens(postedFile.FileName);
                        string filePath = _filePath+"/"+ type+dtFileName;
                        postedFile.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                        _resultFileName = filePath.Replace("~", "");
                        retValue = true;
                    }
                }
                else
                {
                    _resultMessage = "请选择文件!";
                    _resultFileName = "";
                    retValue = false;
                }
            }
            catch
            {
                _resultMessage = "上传文件出错!";
                _resultFileName = "";
                retValue = false;
            }
            return retValue;
        }

        protected string CaculatorSize(int s)
        {
            if (s < 1024)
            {
                return s + " B";
            }
            if (s / 1024 < 1024)
            {
                return s / 1024 + " KB";
            }
            if (s / 1024 / 1024 < 1024)
            {
                return s / 1024 / 1024 + " M";
            }
            if (s / 1024 / 1024 / 1024 < 1024)
            {
                return s / 1024 / 1024 / 1024 + " G";
            }
            else
            {
                return "";
            }
        }
        protected string GetFileExtens(string p)
        {
            string fileName = p.Substring(p.LastIndexOf("\\")+1);
            return fileName.Split('.')[1].ToLower();
        }
        protected bool IsAllowFileExtens(string allowExtens, string nowExtens)
        {
            if (allowExtens.IndexOf(nowExtens) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected Hashtable GetStandardExtentsFeater()
        {
            Hashtable extensTable = new Hashtable();
            extensTable.Add("gif", "image/gif");
            extensTable.Add("jpg", "image/pjpeg");
            extensTable.Add("jpeg", "image/pjpeg");
            extensTable.Add("bmp", "image/bmp");
            extensTable.Add("png", "image/x-png");
            extensTable.Add("tif", "image/tiff");
            extensTable.Add("tiff", "image/tiff");
            extensTable.Add("zip", "application/x-zip-compressed");
            extensTable.Add("rar", "application/octet-stream");
            return extensTable;
        }

        protected string GetExtentsFeatureString(string FileExtents)
        {
            Hashtable standardTable = GetStandardExtentsFeater();
            string FileFeature = "";
            foreach (DictionaryEntry de in standardTable)
            {
                if (de.Key.ToString() == FileExtents)
                {
                    FileFeature = de.Value.ToString();
                    break;
                }
            }
            return FileFeature;
        }

        protected bool IsRightFile(HttpPostedFile postedFile)
        {
            string nowExtens = GetFileExtens(postedFile.FileName);
            string standardFeature = GetExtentsFeatureString(nowExtens);
            if (postedFile.ContentType == standardFeature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
