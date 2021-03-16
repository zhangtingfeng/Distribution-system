using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// WS_WeiXinErWeiMa 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_WeiXinErWeiMa : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        [WebMethod]
        public string WebMethod_APPCODE_getImage_WeiXinErWeiMa(String strShopClientID)//
        {


            String ls_Des_fileName = "";
            try
            {



                /*get AgentURL*/

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(strShopClientID));




                if (Model_tab_ShopClient != null)///说明有权限
                {


                    lock ("WebMethod_APPCODE_getImage_WeiXinErWeiMa2015075")
                    {

                        string strContactMan = Model_tab_ShopClient.ContactMan;
                        string strAddress = Model_tab_ShopClient.Address;
                        string strContactManPostion = Model_tab_ShopClient.ContactManPostion;
                        string strContactPhone = Model_tab_ShopClient.ContactPhone;
                        string strShopClientName = Model_tab_ShopClient.ShopClientName;
                        string strShopMemo = Model_tab_ShopClient.ShopMemo;
                        string strXML = Model_tab_ShopClient.XML;


                        Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                        string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                        string strWeiXinErWeiMaIMG = XML__Class_Shop_Client.WeiXinErWeiMaIMG;


                        #region  制作证书
                        string strBase = "/UpLoad/images/ShopContactCard_Out.jpg";
                        ls_Des_fileName = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/images/weixinerweima_tuiguang.jpg";

                        String ls_savePath = Server.MapPath(ls_Des_fileName);
                        String ls_savePath_01Source = System.Web.HttpContext.Current.Server.MapPath(strBase);
                        string str_Des_Certifcation_fileName = System.Web.HttpContext.Current.Server.MapPath(ls_Des_fileName);
                        Eggsoft.Common.FileFolder.DeleteFile(str_Des_Certifcation_fileName);
                        System.IO.File.Copy(ls_savePath_01Source, str_Des_Certifcation_fileName, true);

                        Mark_ErWeiMa_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strWeiXinErWeiMaIMG), new Rectangle(160, 358, 250, 341));
                        Mark_ErWeiMa_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strShopLogoImage), new Rectangle(20, 48, 140, 163));


                        Mark_ErWeiMa_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, "姓名:" + strContactMan, new Rectangle(170, 67, 270, 124), 24f);
                        Mark_ErWeiMa_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, "职位:" + strContactManPostion, new Rectangle(170, 123, 270, 66), 12f);
                        Mark_ErWeiMa_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, strShopClientName, new Rectangle(51, 209, 390, 52), 18f);
                        Mark_ErWeiMa_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, "联系电话:" + strContactPhone, new Rectangle(51, 270, 360, 48), 12f);
                        Mark_ErWeiMa_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, "业务介绍:" + strShopMemo, new Rectangle(51, 312, 360, 48), 12f);



                        //Eggsoft_Public_CL.Pub.Get_HeadImage(userID.ToString())

                        //qrCodeEncoder.Encode(strURL).Save(ls_savePath);
                        //qrCodeEncoder.Encode(strURL).Dispose();
                        //Eggsoft.Common.Image.Mark_ErWeiMa_WithBase_Goods(ls_savePath, strTemp);

                        //Image_ErWei_ShopClient.ImageUrl = "/upload/QRCodeImages/" + ls_fileName;
                        //ImageButton2.ImageUrl = "/QRCodeImages/PNG/" + ls_fileName;
                        #endregion
                    }
                }
                else
                {///取消权限 删除它
                    String ls_savePath = Server.MapPath(ls_Des_fileName);
                    Eggsoft.Common.FileFolder.DeleteFile(ls_savePath);

                }

            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }

            return ls_Des_fileName;

        }

        public static void Mark_ErWeiMa_WithBase_GivedImage_WithText(string fileDes_CertifcationPath, string strText, RectangleF textareaMy, float fontSize)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(fileDes_CertifcationPath);
            Bitmap bitmap = new Bitmap(image, image.Width, image.Height);
            Graphics g = Graphics.FromImage(bitmap);
            try
            {

                //float fontsize = 12.0f; //字体大小 
                float textwidth = strText.Length * fontSize; //文本的长度 
                //下面定义一个矩形区域，以后在这个矩形里画上白底黑字 
                float rectx = textareaMy.Left;
                float recty = textareaMy.Top;
                float rectwidth = strText.Length * (fontSize + 10);
                float rectheight = fontSize + fontSize; //声明矩形域 
                RectangleF textarea = new RectangleF(rectx, recty, rectwidth, rectheight);
                Font font = new Font("微软雅黑", fontSize); //定义字体 
                Brush whitebrush = new SolidBrush(Color.Black); //白笔刷，画文字用 
                Brush blackbrush = new SolidBrush(Color.White); //黑笔刷，画背景用 
                Brush b = new SolidBrush(Color.FromArgb(50, Color.White));//使用solidBrush新建画刷，定义画刷的颜色为透明色
                g.FillRectangle(b, textarea.Left, textarea.Top, textarea.Width, textarea.Height);
                g.DrawString(strText, font, whitebrush, textarea);
                MemoryStream ms = new MemoryStream(); //保存为jpg类型 
                bitmap.Save(ms, ImageFormat.Jpeg); //输出处理后的图像，这里为了演示方便，我将图片显示在页面中了 
                bitmap.Save(System.Web.HttpContext.Current.Server.MapPath("/UpLoad/TempUpload/tempText.jpg"), ImageFormat.Jpeg); //保存到磁盘上 
                //System.Web.HttpContext.Current.Response.Clear();
                //System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

            }
            catch { }

            finally
            {
                g.Dispose();
                bitmap.Dispose();
                image.Dispose();
            }
            Eggsoft.Common.FileFolder.DeleteFile(fileDes_CertifcationPath);
            System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath("/UpLoad/TempUpload/tempText.jpg"), fileDes_CertifcationPath, true);

        }
        public static void Mark_ErWeiMa_WithBase_GivedImage(string fileDes_CertifcationPath, string waterErWeimaFile, Rectangle Rectangle_Mark_ZhengShu_WithBase_GivedImage)
        {
            //GIF不水印 
            if (File.Exists(waterErWeimaFile) == false) return;
            //string ModifyImagePath = BasePath + filePath;//修改的图像路径 
            int lucencyPercent = 100;
            System.Drawing.Image modifyImage = null;
            System.Drawing.Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象 
                modifyImage = System.Drawing.Image.FromFile(fileDes_CertifcationPath, true);
                drawedImage = System.Drawing.Image.FromFile(waterErWeimaFile, true);
                g = Graphics.FromImage(modifyImage);
                //获取要绘制图形坐标 
                int x = modifyImage.Width - drawedImage.Width;
                int y = modifyImage.Height - drawedImage.Height;
                //设置颜色矩阵 
                float[][] matrixItems ={ 
            new float[] {1, 0, 0, 0, 0}, 
            new float[] {0, 1, 0, 0, 0}, 
            new float[] {0, 0, 1, 0, 0}, 
            new float[] {0, 0, 0, (float)lucencyPercent/100f, 0}, 
            new float[] {0, 0, 0, 0, 1}};

                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //绘制阴影图像 

                #region 计算按比例缩放
                float floatEndWidth = 0;
                float floatEndHeight = 0;

                float desfloatWidth = Rectangle_Mark_ZhengShu_WithBase_GivedImage.Width;
                float desfloatHeight = Rectangle_Mark_ZhengShu_WithBase_GivedImage.Height;

                float desfloatWH = desfloatWidth * 1.00f / desfloatHeight;
                float drawedImageWH = drawedImage.Width * 1.0f / drawedImage.Height;
                if (desfloatWH >= drawedImageWH)///按照高度缩放
                {
                    floatEndHeight = desfloatHeight;
                    floatEndWidth = drawedImageWH * floatEndHeight;

                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.X = Rectangle_Mark_ZhengShu_WithBase_GivedImage.X + (Rectangle_Mark_ZhengShu_WithBase_GivedImage.Width - (int)floatEndWidth) / 2;
                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.Y = Rectangle_Mark_ZhengShu_WithBase_GivedImage.Y;
                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.Width = (int)floatEndWidth;
                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.Height = (int)floatEndHeight;
                }
                else
                {
                    floatEndWidth = desfloatWidth;
                    floatEndHeight = floatEndWidth / drawedImageWH;

                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.X = Rectangle_Mark_ZhengShu_WithBase_GivedImage.X;
                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.Y = Rectangle_Mark_ZhengShu_WithBase_GivedImage.Y + (Rectangle_Mark_ZhengShu_WithBase_GivedImage.Height - (int)floatEndHeight) / 2;
                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.Width = (int)floatEndWidth;
                    Rectangle_Mark_ZhengShu_WithBase_GivedImage.Height = (int)floatEndHeight;

                }
                #endregion

                g.DrawImage(drawedImage, Rectangle_Mark_ZhengShu_WithBase_GivedImage, 0, 0, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                //保存文件 
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(fileDes_CertifcationPath);
                ImageFormat imageType = ImageFormat.Gif;
                switch (fi.Extension.ToLower())
                {
                    case ".jpg": imageType = ImageFormat.Jpeg; break;
                    case ".gif": imageType = ImageFormat.Gif; break;
                    case ".png": imageType = ImageFormat.Png; break;
                    case ".bmp": imageType = ImageFormat.Bmp; break;
                    case ".tif": imageType = ImageFormat.Tiff; break;
                    case ".wmf": imageType = ImageFormat.Wmf; break;
                    case ".ico": imageType = ImageFormat.Icon; break;
                    default: break;
                }
                MemoryStream ms = new MemoryStream();
                modifyImage.Save(ms, imageType);
                byte[] imgData = ms.ToArray();
                modifyImage.Dispose();
                drawedImage.Dispose();
                g.Dispose();
                FileStream fs = null;
                File.Delete(fileDes_CertifcationPath);
                fs = new FileStream(fileDes_CertifcationPath, FileMode.Create, FileAccess.Write);
                if (fs != null)
                {
                    fs.Write(imgData, 0, imgData.Length);
                    fs.Close();
                }
            }
            finally
            {
                try
                {
                    drawedImage.Dispose();
                    modifyImage.Dispose();
                    g.Dispose();
                }
                catch
                {
                }
            }
        }
    }
}
