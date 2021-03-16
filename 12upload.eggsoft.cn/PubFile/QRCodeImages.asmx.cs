using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// QRCodeImages 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class QRCodeImages : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="strLinkURL"></param>
        /// <param name="strerrorCorrectIcon">LMQH   空为M</param>
        [WebMethod]
        public void APPCODE_getImage_QRCodeImages(String strFilePath, string strLinkURL, string strerrorCorrectIcon)//强行获取
        {
            Class_Pub.Class_Pub_APPCODE_getImage_QRCodeImages(strFilePath, strLinkURL, strerrorCorrectIcon);
           
        }
    }
}
