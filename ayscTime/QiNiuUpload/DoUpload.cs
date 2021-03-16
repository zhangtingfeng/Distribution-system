using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eggsoft.Common;

namespace QiNiuUpload
{
    public class DoUpload
    {
        public void go(string strUpload, string strAddPath)
        {
            string strConstPath = System.Configuration.ConfigurationManager.AppSettings["QiNiuUplaodCPath"];
            List<FileInformation> ListdddFileInformationd = new ClassGetFile().getUploadList(strConstPath + @"\" + strUpload + @"\" + strAddPath, @"/" + strUpload + @"/" + strAddPath);
            if (ListdddFileInformationd.Count > 0)
            {
                new QiNiuUpload.ClassUploadAction().UploadList(ListdddFileInformationd);
            }
        }

    }
}
