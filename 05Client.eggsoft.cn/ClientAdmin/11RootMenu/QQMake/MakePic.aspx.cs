using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;

namespace _05Client.eggsoft.cn.ClientAdmin._11RootMenu.QQMake
{
    public partial class MakePic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            if (String.IsNullOrEmpty(type) == false)
            {
                if (type.ToLower() == "fileupload_erweima")
                {
                    string stringPicUrl = Request.QueryString["PicUrl"];
                    ReadURLCreateImage(stringPicUrl);

                }
            }
            else
            {
                CreateImage();
            }
        }


        private void CreateImage()
        {

            int w = 0;
            int h = 0;

            try
            {
                w = int.Parse(Request.Form["width"]);
                h = int.Parse(Request.Form["height"]);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "QQ二维码识别", "程序报错");
                Eggsoft.Common.JsUtil.TipAndRedirect("识别失败", "GetPic.aspx", "4");
                HttpContext.Current.Response.End();

            }
            finally
            {

            }

            Bitmap newmap = new Bitmap(w, h);
            int rows = 0;
            int cols = 0;

            Graphics gp = Graphics.FromImage(newmap);
            gp.Clear(Color.White);
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[1];
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 95L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            gp.CompositingQuality = CompositingQuality.HighQuality;
            gp.SmoothingMode = SmoothingMode.HighQuality;
            gp.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gp.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            for (rows = 0; rows < newmap.Height; rows++) //循环图片高度
            {
                string px = Request["PX" + rows];
                string[] c_row = px.Split(',');
                for (cols = 0; cols < newmap.Width; cols++) //循环图片宽度
                {

                    string values = c_row[cols];
                    if (values != "" && values != null)
                    {
                        string hex = values;
                        while (hex.Length < 6)
                        {//防止颜色丢失
                            hex = "0" + hex;
                        }

                        int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                        int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                        int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

                        newmap.SetPixel(cols, rows, Color.FromArgb(r, g, b));

                    }
                }
            }
            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                gp.DrawImage(newmap, 0, 0, w, h);
                string CameraPath = Server.MapPath("QQ_Image/") + "Camera" + strShopClientID + ".jpg";
                //保存路径,修改这里
                newmap.Save(CameraPath, myImageCodecInfo, myEncoderParameters);
                myEncoderParameters.Dispose();
                gp.Dispose();
                newmap.Dispose();

                ReadURLCreateImage(CameraPath);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione,"QQ二维码识别","程序报错");
                Eggsoft.Common.JsUtil.TipAndRedirect("识别失败", "GetPic.aspx", "4");
                HttpContext.Current.Response.End();

            }
            finally
            {

            }

            //Response.Redirect(Path);
            //return true;
        }

        private void ReadURLCreateImage(string strPath)
        {

            System.Drawing.Image img = System.Drawing.Image.FromFile(strPath);
            Bitmap bmap;
            bmap = new Bitmap(img);
            BarcodeReader reader = null;
            reader = new BarcodeReader();
            Result result = reader.Decode(bmap); //通过reader解码  

            bool boolIFTrue = false;
            string strURL = "";
            if (result != null)
            {
                strURL = result.Text; //显示解析结果              //http://qm.qq.com/cgi-bin/qm/qr?k=CQHAgNatjmNXrZ3P5p8-kPsNF-zfwSXU
            }

            if (strURL.IndexOf("https://qm.qq.com/cgi-bin/qm/qr?k=") != -1)
            {
                if (strURL.Length == 65)
                {
                    boolIFTrue = true;
                }
            }
            else if (strURL.IndexOf("https://weixin.qq.com/r") != -1)
            {
                boolIFTrue = true;
            }


            if (boolIFTrue == false)
            {
                Eggsoft.Common.JsUtil.TipAndRedirect("识别失败", "GetPic.aspx", "4");
                HttpContext.Current.Response.End();
                // return null;
            }
            else
            {

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();
                tab_ShopClient_Model = tab_ShopClient_bll.GetModel(int.Parse(strShopClientID));
                tab_ShopClient_Model.QM_QQ_COM_QM_K_32 = strURL;
                tab_ShopClient_bll.Update(tab_ShopClient_Model);

                Eggsoft.Common.JsUtil.TipAndRedirect("识别成功", "/ClientAdmin/11RootMenu/URLShow.aspx", "4");
                HttpContext.Current.Response.End();
                // return null;
            }
            //return "";
        }
    }
}