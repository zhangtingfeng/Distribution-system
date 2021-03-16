using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;   //引用系统文件

namespace _02WFA
{
    public class FileIOMy
    {

        public static void writeFile(string strFileName,string strContent)
        {
            if (File.Exists(strFileName)) { File.Delete(strFileName); }
            string   sFilePath=   System.IO.Path.GetDirectoryName   (strFileName);

            if (Directory.Exists(sFilePath) == false) { Directory.CreateDirectory(sFilePath); } 



            //实例化一个文件流--->与写入文件相关联  
            FileStream fs = new FileStream(strFileName,FileMode.Create); 
            //获得字节数组  
            byte [] data =new UTF8Encoding().GetBytes(strContent);  
            //开始写入  
            fs.Write(data,0,data.Length);  
            //清空缓冲区、关闭流  
            fs.Flush(); 
            fs.Close();  
        }

        public static string readFile(string strFileName)
        {
            string xieyi = "";
            FileInfo fi = new FileInfo(strFileName);
            StreamReader sr = fi.OpenText();
            xieyi = sr.ReadToEnd();
            sr.Close();
            return xieyi;

        }

    }
}
