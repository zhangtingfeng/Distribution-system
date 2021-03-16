using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class IOUtil
    {
        /// <summary>
        /// 按日期创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static string CreateDateTimeDir(string path)
        {
            DateTime dtNow=DateTime.Now;
            string strYear = dtNow.Year.ToString();
            string strMonth = dtNow.Month.ToString();
            string strDay = dtNow.Day.ToString();
            //创建目录
            string[] arr = path.Split('/');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "" && arr[i] != "..")
                { 
                    if(i!=0)
                    {
                        string part = "../";
                        for (int j = 1; j <= i; j++)
                        {
                            part += arr[j] + "/";
                        }
                       // CreateDir(part);
                    }
                }
            }
            CreateDir(path+"/"+strYear);
            CreateDir(path + "/" + strYear+"/"+strMonth);
            CreateDir(path + "/" + strYear + "/" + strMonth + "/" + strDay);
            return path + "/" + strYear + "/" + strMonth + "/" + strDay;
        }

        /// <summary>
        /// 按日期创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static string CreateDateTimeDir(string path,string pre)
        {
            DateTime dtNow = DateTime.Now;
            string strYear = dtNow.Year.ToString();
            string strMonth = dtNow.Month.ToString();
            string strDay = dtNow.Day.ToString();
            //创建目录
            string[] arr = path.Split('/');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "" && arr[i] != pre)
                {
                    if (i != 0)
                    {
                        string part = pre+"/";
                        for (int j = 1; j <= i; j++)
                        {
                            part += arr[j] + "/";
                        }
                        CreateDir(part);
                    }
                }
            }
            CreateDir(path + "/" + strYear);
            CreateDir(path + "/" + strYear + "/" + strMonth);
            CreateDir(path + "/" + strYear + "/" + strMonth + "/" + strDay);
            return path + "/" + strYear + "/" + strMonth + "/" + strDay;
        }


        private static void CreateDir(string path)
        {
            string p = HttpContext.Current.Server.MapPath("../"+path);
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
        }
    }
}
