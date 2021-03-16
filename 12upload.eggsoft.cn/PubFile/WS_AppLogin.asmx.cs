using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.IO;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// WS_AppLogin 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_AppLogin : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        #region 得到验证码
        private static object staticLoclFileIMG = new object();
        [WebMethod]
        public string GetCheckImages()
        {
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;
                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strShopClientID, out pub_Int_ShopClientID);
                string strPhoneNum = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strPhoneNum"]);


                string strIP = context.UserHostAddress;///System.Web.HttpContext.Current.Request.UserHostAddress; 这样获取IP是没问题的。   一个IP限制每分钟100个
                string strcom_VCode = GenerateCheckCode();

               
                lock (staticLoclFileIMG)
                {
                    String ls_Des_fileName = "";
                    ls_Des_fileName = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/CheckCode/"+ DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg"; ;
                    String ls_savePath = Server.MapPath(ls_Des_fileName);
                    string str_Des_Certifcation_fileName = System.Web.HttpContext.Current.Server.MapPath(ls_Des_fileName);
                    CreateCheckCodeImage(strcom_VCode, str_Des_Certifcation_fileName);
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "WS_AppLogin线程异常");
            }
            catch (Exception eee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eee);
            }
            string str = "";
            str = "{\"ErrorCode\":0}";////表示ok            
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }


        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                code = (char)('0' + (char)(number % 10));

                checkCode += code.ToString();
            }

            return checkCode;
        }
        private void CreateCheckCodeImage(string checkCode, string fileDes_CertifcationPath)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 23);
            Graphics g = Graphics.FromImage(image);


            //生成随机生成器
            Random random = new Random();

            //图片背景色
            g.Clear(Color.Blue);

            //画图片的背景噪音线

            Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));

            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.White, Color.Silver, 1.2f, true);

            g.DrawString(checkCode, font, brush, 2, 2);

            //画图片的前景噪音点
            for (int i = 0; i < 298; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Gold), 0, 0, image.Width - 1, image.Height - 1);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            byte[] imgData = ms.ToArray();

            FileStream fs = null;
            File.Delete(fileDes_CertifcationPath);
            fs = new FileStream(fileDes_CertifcationPath, FileMode.Create, FileAccess.Write);
            if (fs != null)
            {
                fs.Write(imgData, 0, imgData.Length);
                fs.Close();
            }

            //Response.ClearContent();
            //Response.ContentType = "image/Gif";
            //Response.BinaryWrite(ms.ToArray());

        }
        #endregion 得到验证码
    }
}