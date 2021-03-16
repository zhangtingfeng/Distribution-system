using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eggsoft.Common;

namespace QiNiuUpload
{
    
    public class ClassGetFile
    {
        private static object myLock = new object();

        public List<FileInformation> getUploadList(string strPath, string HeadPath)
        {
            lock (myLock) {
                List<FileInformation> dddd = GETDisk(strPath, HeadPath);
                return dddd;
            }
            
        }
        List<FileInformation> GETDisk(string strPath, string strHeadPath)
        {
            string QiNiuUplaodCache = System.Configuration.ConfigurationManager.AppSettings["QiNiuUplaodCache"];

             List<FileInformation> listGetDisk = null;
            string strFileName = QiNiuUplaodCache + strPath.Replace("\\", "_").Replace("\"", "").Replace(" ", "").Replace(":", "") + ".txt";
            if (File.Exists(strFileName))
            {
                StreamReader sr = new StreamReader(strFileName, System.Text.Encoding.UTF8);
                String strGetDisk = sr.ReadToEnd();
                sr.Close();
                listGetDisk = Eggsoft.Common.JsonHelper.DeserializeJsonToList<FileInformation>(strGetDisk);
            }
            List<FileInformation> NeedUplaodlist = new List<FileInformation>();

            List<FileInformation> NewReadlist = new DirectoryAllFiles().getDirectoryAllFiles(strPath, strHeadPath);
            NewReadlist = NewReadlist.OrderBy(u => u.FileName).ToList(); //其中u也是隐藏写法，因为我们没有需要调用的，直接用自己比较就直接这样比较久可以了。


            if (listGetDisk != null)
            {
                for (int i = 0; i < NewReadlist.Count; i++)
                {
                    bool ifGetThisFile = false;
                    for (int j = 0; j < listGetDisk.Count; j++)
                    {
                        if (NewReadlist[i].FullName == listGetDisk[j].FullName)
                        {
                            ifGetThisFile = true;
                            if (NewReadlist[i].MD5Marker != listGetDisk[j].MD5Marker)
                            {
                                NeedUplaodlist.Add(NewReadlist[i]);
                            }
                            break;
                        }
                    }
                    if (!ifGetThisFile)
                    {
                        NeedUplaodlist.Add(NewReadlist[i]);
                    }
                }
            }
            else
            {
                NeedUplaodlist = NewReadlist;
            }

            if (NeedUplaodlist != null && NeedUplaodlist.Count > 0)
            {
                if (listGetDisk != null) File.Delete(strFileName);
                string strContentSaveDisk = Eggsoft.Common.JsonHelper.SerializeObject(NewReadlist);
                StreamWriter srWrite = null;
                srWrite = File.CreateText(strFileName);
                srWrite.Write(strContentSaveDisk);
                srWrite.Close();
            }
            return NeedUplaodlist;
        }
    }


}
