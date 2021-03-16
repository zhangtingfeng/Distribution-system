using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._11RootMenu.QQMake
{
    public partial class GetPic1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_ErWeiMa_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string uploadName = FileUpload_ErWeiMa.Value;//获取待上传图片的完整路径，包括文件名 
                                                         //string uploadName = InputFile.PostedFile.FileName; 
            string pictureName = "";//上传后的图片名，以当前时间为文件名，确保文件名没有重复 
            if (FileUpload_ErWeiMa.Value != "")
            {
                int idx = uploadName.LastIndexOf(".");
                string suffix = uploadName.Substring(idx);//获得上传的图片的后缀名 
                pictureName = DateTime.Now.Ticks.ToString() + suffix;
            }
            try
            {
                if (uploadName != "")
                {
                    string path = Server.MapPath("QQ_Image/");
                    FileUpload_ErWeiMa.PostedFile.SaveAs(path + pictureName);

                    Eggsoft.Common.JsUtil.TipAndRedirect("上传完毕，马上处理识别结果", "MakePic.aspx?type=FileUpload_ErWeiMa&PicUrl=" + path + pictureName, "2");
                }
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally
            {

            }

        }
    }
}