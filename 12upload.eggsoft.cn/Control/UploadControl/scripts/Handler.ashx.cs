using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _12upload.eggsoft.cn.Control.UploadControl.scripts
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {

        //public void ProcessRequest (HttpContext context) {
        //    context.Response.ContentType = "text/plain";
        //    context.Response.Write("Hello World");
        //}

        string op = "0";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            op = context.Request["op"];
            bindData(context);

        }


        private void bindData(HttpContext context)
        {
            switch (op)
            {
                case "1":
                    Add(context);
                    break;
                case "2":
                    delete(context);
                    break;
                default:
                    break;
            }
        }

        private void delete(HttpContext context)
        {
            string strimg = context.Request["strImg"];
            if (!string.IsNullOrEmpty(strimg))
            {
                string[] imgArray = strimg.Split('|');
                foreach (string item in imgArray)
                {
                    string FilePath = context.Server.MapPath(item);
                    if (System.IO.File.Exists(FilePath))
                    {
                        System.IO.File.Delete(FilePath);
                    }
                }
            }
        }

        private void Add(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = context.Server.MapPath(context.Request["BaseUrl"]);


            if (file != null)
            {
                if (!System.IO.Directory.Exists(uploadPath))
                {
                    System.IO.Directory.CreateDirectory(uploadPath);
                }

                string fileType = file.FileName.Substring(file.FileName.LastIndexOf("."));
                Random r = new Random();
                string timenow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ffff");//得到系统时间
                string dNow = timenow.Trim().Replace("-", "").Replace(":", "").Replace(" ", "");
                string fileRenameName = dNow.ToString() + fileType.ToString();   //实现文件的重命名

                file.SaveAs(uploadPath + fileRenameName);
                //生成缩略图
                //MakeThumbnail(uploadPath + file.FileName, uploadPath + "s/" + fileRenameName, 80, 80);
            }
        }

        private void MakeThumbnail(string sourcePath, string newPath, int width, int height)
        {
            System.Drawing.Image ig = System.Drawing.Image.FromFile(sourcePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = ig.Width;
            int oh = ig.Height;
            if ((double)ig.Width / (double)ig.Height > (double)towidth / (double)toheight)
            {
                oh = ig.Height;
                ow = ig.Height * towidth / toheight;
                y = 0;
                x = (ig.Width - ow) / 2;

            }
            else
            {
                ow = ig.Width;
                oh = ig.Width * height / towidth;
                x = 0;
                y = (ig.Height - oh) / 2;
            }
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(ig, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);
            try
            {
                bitmap.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ig.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}