using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Eggsoft.Common;
using QiNiuUpload;
using static Eggsoft.Common.FileFolder;

namespace ayscTime
{
    class Program
    {


        static void Main(string[] args)
        {

            try
            {
                string strConstPath = @"E:\Works_Dream\0055eggsoft.cnvs2015_GaoErFu\12upload.eggsoft.cn";
                string strUpload = "Upload";
                string strAddPath = "000001_sh";

                List<FileInformation> ListdddFileInformationd = new ClassGetFile().getUploadList(strConstPath + @"\" + strUpload + @"\" + strAddPath, @"/" + strUpload + @"/" + strAddPath);
                if (ListdddFileInformationd.Count > 0)
                {
                    new QiNiuUpload.ClassUploadAction().UploadList(ListdddFileInformationd);
                }
            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "七牛报错");
            }

        }



    }
}
