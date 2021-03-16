using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI.WebControls;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    /**/
    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <param name="mode">生成缩略图的方式</param>   
    public class Image
    {

        public static string[] getFileBMPWidthAndHeight(string originalImagePath)
        {
            string MapPathO = System.Web.HttpContext.Current.Server.MapPath(originalImagePath);
            if (File.Exists(MapPathO))
            {
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(MapPathO);

                int ow = originalImage.Width;
                int oh = originalImage.Height;

                String[] myString = { ow.ToString(), oh.ToString() };
                return myString;
            }
            return null;
            //string[] mystring=new ;

        }



        public static void ScaleBMP(string originalImagePath, int width, int height, string mode)
        {
            string strTemp = @"/upload/TempUpload/Temp.jpg";

            string MapPathO = Eggsoft.Common.FileFolder.urlconvertorlocal(originalImagePath);
            //string MapPathO=System.Web.HttpContext.Current.Server.MapPath(originalImagePath);
            string MapPath_Temp = System.Web.HttpContext.Current.Server.MapPath(strTemp);
            MakeThumbnail(MapPathO, MapPath_Temp, width, height, mode);
            FileFolder.DeleteFile(MapPathO);
            File.Move(MapPath_Temp, MapPathO);
        }

        /**/
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>   
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "hw"://指定高宽缩放（按比例饿放在这里面）           
                    Decimal oDecimal = (Decimal)ow / oh;
                    Decimal SDecimal = (Decimal)towidth / toheight;
                    if (oDecimal >= SDecimal)//宽度优先
                    {
                        toheight = Decimal.ToInt32(towidth / oDecimal);//指定宽，高按比例   
                    }
                    else
                    {
                        towidth = Decimal.ToInt32(toheight * oDecimal);//指定高，宽按比例 
                    }

                    break;
                case "HW"://指定高宽缩放        （可能变形）
                    break;
                case "NoreW"://  指定宽，高按比例      不超过宽度 就算聊
                    if (ow < width)
                    {
                        toheight = oh;
                        towidth = ow;
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case "W"://指定宽，高按比例                   
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）               
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "生成缩略图", "程序报错");
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /**/
        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>
        protected void AddShuiYinWord(string Path, string Path_sy)
        {
            string addText = "测试水印";
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 16);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);

            g.DrawString(addText, f, b, 15, 15);
            g.Dispose();

            image.Save(Path_sy);
            image.Dispose();
        }

        /**/
        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        /// <param name="Path_sypf">水印图片路径</param>
        protected void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            image.Save(Path_syp);
            image.Dispose();
        }

        /// <summary>
        /// 取得HTML中所有图片的 URL。  一篇文章的第一张图
        /// </summary>
        /// <param name="sHtmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        public static string GetFirstHtmlImageUrl(string sHtmlText)
        {
            string strFirstImageURL = "";
            string[] sUrlList = GetHtmlImageUrlList(sHtmlText);
            if (sUrlList.Length > 0)
            {
                strFirstImageURL = sUrlList[0];
            }
            return strFirstImageURL;
        }

        /// <summary>
        /// 取得HTML中所有图片的 URL。
        /// </summary>
        /// <param name="sHtmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            System.Text.RegularExpressions.Regex regImg = new System.Text.RegularExpressions.Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            System.Text.RegularExpressions.MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (System.Text.RegularExpressions.Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }


        /**/
        /// <summary>
        /// 在图片上生成图片二维码
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        /// <param name="Path_sypf">水印图片路径</param>
        public static void AddErWeiMaPic(string goodUrlPath, string GoodPicPath_syp)
        {

            System.Drawing.Image image = System.Drawing.Image.FromFile(goodUrlPath);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(GoodPicPath_syp);
            System.Drawing.Graphics gOrigion = System.Drawing.Graphics.FromImage(image);

            int intOW = image.Width / 6;
            int intOH = image.Height / 6;


            gOrigion.DrawImage(copyImage, new System.Drawing.Rectangle(intOW * 2, intOH * 2, intOW * 2, intOH * 2), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);

            image.Save(goodUrlPath);
            image.Dispose();


            gOrigion.Dispose();
        }


        //using System; 
        //using System.Web; 
        //using System.Drawing; 
        //using System.Drawing.Drawing2D; 
        //using System.Drawing.Imaging; 
        //using System.IO; 
        //using System.Reflection; 

        /// <summary> 
        /// 给图片上水印 
        /// </summary> 
        /// <param name="filePath">原图片地址</param> 
        /// <param name="waterFile">水印图片地址</param> 
        public static void Mark_ErWeiMa_Goods(string filePath, string waterFile)
        {
            //GIF不水印 

            //string ModifyImagePath = BasePath + filePath;//修改的图像路径 
            int lucencyPercent = 100;
            System.Drawing.Image modifyImage = null;
            System.Drawing.Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象 
                modifyImage = System.Drawing.Image.FromFile(filePath, true);
                drawedImage = System.Drawing.Image.FromFile(waterFile, true);
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



                g.DrawImage(drawedImage, new Rectangle(6, 6, modifyImage.Width - 12, modifyImage.Height - 12), 0, 0, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                imgAttr.Dispose();

                //保存文件 
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(filePath);
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
                File.Delete(filePath);
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                if (fs != null)
                {
                    fs.Write(imgData, 0, imgData.Length);
                    fs.Close();
                }
            }
            catch (Exception eeeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeeee);
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


        public static void Mark_ErWeiMa_WithBase_Goods(string filePath, string waterFile)
        {
            //GIF不水印 

            //string ModifyImagePath = BasePath + filePath;//修改的图像路径 
            int lucencyPercent = 100;
            System.Drawing.Image modifyImage = null;
            System.Drawing.Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象 
                modifyImage = System.Drawing.Image.FromFile(filePath, true);
                drawedImage = System.Drawing.Image.FromFile(waterFile, true);
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
                int intOW = modifyImage.Width / 6;
                int intOH = modifyImage.Height / 6;


                g.DrawImage(drawedImage, new Rectangle(intOW * 2, intOH * 2, intOW * 2, intOH * 2), 0, 0, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                //保存文件 
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(filePath);
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
                File.Delete(filePath);
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
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





        public static void Mark_Logo_WithBase_GoodsPic_640_400_200_100(string filePath, string waterFile)
        {
            //GIF不水印 

            //string ModifyImagePath = BasePath + filePath;//修改的图像路径 
            int lucencyPercent = 100;
            System.Drawing.Image modifyImage = null;
            System.Drawing.Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象 
                modifyImage = System.Drawing.Image.FromFile(filePath, true);
                drawedImage = System.Drawing.Image.FromFile(waterFile, true);
                g = Graphics.FromImage(modifyImage);
                //获取要绘制图形坐标 
                //int x = modifyImage.Width - drawedImage.Width;
                //int y = modifyImage.Height - drawedImage.Height;
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
                // int intOW = modifyImage.Width / 6;
                // int intOH = modifyImage.Height / 6;
                /*判断 是什么形状  长宽比 长的话  按高度  48px去贴，  高的话 按宽度64px 去贴 ，我们假设标准的显示 是640*400px */
                #region /*判断 是什么形状  长宽比 长的话  按高度  48px去贴，  高的话 按宽度64px 去贴 ，我们假设标准的显示 是640*400px */
                Double Decimalmy = (drawedImage.Width * 1.0) / (drawedImage.Height);
                int intx = 0;
                int inty = 0;
                int intwidth = 0;
                int intHeight = 0;


                if (Decimalmy > 1.0)
                {
                    intx = 0;
                    inty = 0;
                    intwidth = (modifyImage.Width) / 3;
                    intHeight = (intwidth * drawedImage.Height) / (drawedImage.Width);
                }
                else
                {
                    intx = 0;
                    inty = 0;
                    intwidth = modifyImage.Width / 10;
                    intHeight = (intwidth * drawedImage.Height) / (drawedImage.Width);
                }
                g.DrawImage(drawedImage, new Rectangle(intx, inty, intwidth, intHeight), 0, 0, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                #endregion
                //保存文件 
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(filePath);
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
                File.Delete(filePath);
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
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




        public static bool CheckPic(FileUpload FileUpload1, int intCheckWidth, int CheckHeight)
        {
            bool isSafe = false;
            HttpPostedFile pic = FileUpload1.PostedFile;
            //扩展名检查
            string picext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
            if (picext == ".jpg" || picext == ".gif" || picext == ".bmp" || picext == ".png")
            {
                isSafe = true;
            }
            else
            {
                isSafe = false;
                Eggsoft.Common.JsUtil.ShowMsg("图片格式不对,请转换成常见的图片格式 jpg,gif,bmp,png");
                //                Kit.Alert(this.Page, "图片格式不对,请转换成常见的图片格式 jpg,gif,bmp,png");
                return isSafe;
            }
            //图片大小检查
            if (pic.ContentLength > 1024000)
            {
                isSafe = false;
                Eggsoft.Common.JsUtil.ShowMsg("超过图片限制大小");

                return isSafe;
            }
            //图片尺寸检查
            System.IO.Stream picstream = pic.InputStream;
            System.Drawing.Image img = System.Drawing.Image.FromStream(picstream);
            if (img.Width > 0 && img.Height > 0)
            {

                if ((intCheckWidth > 0) && (img.Width != intCheckWidth))
                {
                    Eggsoft.Common.JsUtil.ShowMsg("宽度不符合要求");
                    isSafe = false;
                }

                else if ((CheckHeight > 0) && (img.Height != CheckHeight))
                {
                    Eggsoft.Common.JsUtil.ShowMsg("高度不符合要求");
                    isSafe = false;
                }
                else
                {
                    isSafe = true;
                }


                //第三步验证身份证图片并上传
                picstream.Close();
                picstream.Dispose();

                img.Dispose();
                // picstream.Flush();
            }
            else
            {
                isSafe = false;
                Eggsoft.Common.JsUtil.ShowMsg("非法的图片文件");
                //img.Dispose();
                return isSafe;
            }

            //img.Dispose();
            return isSafe;
        }

    }


}