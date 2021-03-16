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
    /// WS_UserAgentCertification 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_UserAgentCertification : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        #region 代理二维码

        [WebMethod]
        public string WebMethod_APPCODE_getImage_UserAgentCertification(String strUserID)//
        {
            return APPCODE_getImage_UserAgentCertification(strUserID);
        }


        private static object APPobjectCODE_getImage_UserAgentCertification = new object();
        private String APPCODE_getImage_UserAgentCertification(String strUserID)//
        {
            String ls_Des_fileName = "";
            try
            {
                /// object lock_ojb = new object();

                lock (APPobjectCODE_getImage_UserAgentCertification)
                {
                    /*get AgentURL*/
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));

                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);


                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + "   and IsDeleted=0 and ShopClientID=" + Model_tab_User.ShopClientID);
                    string stringHttp = "";
                    ls_Des_fileName = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_GetAgent_BookMarkerPath(Int32.Parse(strUserID), out stringHttp);

                    String strAgentTextName = Eggsoft_Public_CL.Pub.GetstringShowPower_AgentShopTextDesc(Model_tab_User.ShopClientID.ToString());// "代理店铺:";
                    String strAgentLevelName = "";

                    if (Model_tab_ShopClient_Agent_ != null)///说明有权限
                    {
                        if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)///代理模式的代理证书 例如合伙人
                        {
                            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                            EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetModel("id=" + Model_tab_ShopClient_Agent_.AgentLevelSelect);
                            if (Model_tab_ShopClient_Agent_Level != null)
                            {
                                strAgentLevelName = Model_tab_ShopClient_Agent_Level.AgentLevelName.Trim() + "";
                            }
                        }


                        #region  制作推广二维码
                        string strPath = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/SAgent_01_" + strUserID + ".jpg";
                        if (Eggsoft.Common.FileFolder.File_Exists(strPath) == false)///不要重复下载
                        {
                            string strHttp = Eggsoft_Public_CL.Pub_GetOpenID_And_.MakeOpenIDBitmap(Convert.ToInt32(Model_tab_User.ShopClientID), "SAgent_" + strUserID, false);//false表示永久二维码
                            Eggsoft.Common.Download.DownLoadFile(strHttp, strPath);
                        }
                        #endregion

                        #region  制作证书
                        string strBase = "/UpLoad/images/UserAgentBase_Out.jpg";


                        String ls_savePath = Server.MapPath(ls_Des_fileName);
                        String ls_savePath_01Source = System.Web.HttpContext.Current.Server.MapPath(strBase);
                        string str_Des_Certifcation_fileName = System.Web.HttpContext.Current.Server.MapPath(ls_Des_fileName);
                        Eggsoft.Common.FileFolder.DeleteFile(str_Des_Certifcation_fileName);
                        System.IO.File.Copy(ls_savePath_01Source, str_Des_Certifcation_fileName, false);

                        Mark_ZhengShu_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strPath), new Rectangle(91, 396, 270, 270));

                        string strUserHead = Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(Int32.Parse(strUserID));
                        Mark_ZhengShu_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strUserHead), new Rectangle(10, 35, 88, 88));
                        Eggsoft.Common.debug_Log.Call_WriteLog("APPCODE_getImage_UserAgentCertification:1");

                        string strXML = Model_tab_ShopClient.XML;
                        Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                        bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                        string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                        Eggsoft.Common.debug_Log.Call_WriteLog("APPCODE_getImage_UserAgentCertification:1" + strShopLogoImage);

                        if (String.IsNullOrEmpty(strShopLogoImage) == false) Mark_ZhengShu_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strShopLogoImage), new Rectangle(170, 122, 108, 105));
                        Eggsoft.Common.debug_Log.Call_WriteLog("APPCODE_getImage_UserAgentCertification:2");


                        Mark_ZhengShu_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, Model_tab_ShopClient.ShopClientName + strAgentLevelName + "资格证书", new Rectangle(100, 55, 255, 37));

                        Mark_ZhengShu_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, strAgentTextName + "：" + Model_tab_ShopClient_Agent_.ShopName, new Rectangle(100, 85, 255, 37));


                        Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(Convert.ToInt32(Model_tab_User.ShopClientID));


                        #endregion
                    }
                    else
                    {///取消代理权限 删除它
                        String ls_savePath = Server.MapPath(ls_Des_fileName);
                        Eggsoft.Common.FileFolder.DeleteFile(ls_savePath);

                    }
                }


            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(strUserID, "制作证书", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "制作证书");
            }

            return ls_Des_fileName;

        }

        #endregion 代理二维码


        #region 海报

        [WebMethod]
        public string WebMethod_APPCODE_getImage_UserPoster(String strUserID)//
        {
            return APPCODE_getImage_UserUserPoster(strUserID);
        }


        private static object APPobjectCODE_getImage_UserUserPoster = new object();
        private String APPCODE_getImage_UserUserPoster(String strUserID)//
        {
            String ls_Des_fileName = "";
            try
            {
                /// object lock_ojb = new object();

                lock (APPobjectCODE_getImage_UserUserPoster)
                {
                    /*get AgentURL*/
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));

                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);


                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + "   and IsDeleted=0 and ShopClientID=" + Model_tab_User.ShopClientID);
                    string stringHttp = "";
                    ls_Des_fileName = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_GetAgent_PosterPath(Int32.Parse(strUserID), out stringHttp);

                    String strAgentTextName = Eggsoft_Public_CL.Pub.GetstringShowPower_AgentShopTextDesc(Model_tab_User.ShopClientID.ToString());// "代理店铺:";
                    String strAgentLevelName = "";

                    if (Model_tab_ShopClient_Agent_ != null)///说明有权限
                    {
                        if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)///代理模式的代理证书 例如合伙人
                        {
                            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                            EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetModel("id=" + Model_tab_ShopClient_Agent_.AgentLevelSelect);
                            if (Model_tab_ShopClient_Agent_Level != null)
                            {
                                strAgentLevelName = Model_tab_ShopClient_Agent_Level.AgentLevelName.Trim() + "";
                            }
                        }


                        #region  制作海报
                        string strPath = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/Poster_" + strUserID + ".jpg";
                        if (Eggsoft.Common.FileFolder.File_Exists(strPath) == false)///不要重复下载
                        {
                            string strHttp = Eggsoft_Public_CL.Pub_GetOpenID_And_.MakeOpenIDBitmap(Convert.ToInt32(Model_tab_User.ShopClientID), "SAgent_" + strUserID, false);//false表示永久二维码
                            Eggsoft.Common.Download.DownLoadFile(strHttp, strPath);
                        }
                        #endregion

                        #region  制作证书
                        string strBase = "/UpLoad/images/PosterOUT.jpg";


                        String ls_savePath = Server.MapPath(ls_Des_fileName);
                        String ls_savePath_01Source = System.Web.HttpContext.Current.Server.MapPath(strBase);
                        string str_Des_Certifcation_fileName = System.Web.HttpContext.Current.Server.MapPath(ls_Des_fileName);
                        Eggsoft.Common.FileFolder.DeleteFile(str_Des_Certifcation_fileName);
                        System.IO.File.Copy(ls_savePath_01Source, str_Des_Certifcation_fileName, false);

                        Mark_ZhengShu_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strPath), new Rectangle(374, 1434, 320, 320));///二维码

                        string strUserHead = Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(Int32.Parse(strUserID));
                        Mark_ZhengShu_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strUserHead), new Rectangle(60, 50, 200, 200));//头像
                        //Eggsoft.Common.debug_Log.Call_WriteLog("APPCODE_getImage_UserAgentCertification:1");

                        //string strXML = Model_tab_ShopClient.XML;
                        //Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                        //bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                        //string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                        //Eggsoft.Common.debug_Log.Call_WriteLog("APPCODE_getImage_UserAgentCertification:1" + strShopLogoImage);

                        //if (String.IsNullOrEmpty(strShopLogoImage) == false) Mark_ZhengShu_WithBase_GivedImage(str_Des_Certifcation_fileName, System.Web.HttpContext.Current.Server.MapPath(strShopLogoImage), new Rectangle(170, 122, 108, 105));
                        //Eggsoft.Common.debug_Log.Call_WriteLog("APPCODE_getImage_UserAgentCertification:2");


                        //Mark_ZhengShu_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, Model_tab_ShopClient.ShopClientName + strAgentLevelName + "海报", new Rectangle(100, 55, 255, 37));

                        //Mark_ZhengShu_WithBase_GivedImage_WithText(str_Des_Certifcation_fileName, strAgentTextName + "：" + Model_tab_ShopClient_Agent_.ShopName, new Rectangle(100, 85, 255, 37));


                        Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(Convert.ToInt32(Model_tab_User.ShopClientID));


                        #endregion
                    }
                    else
                    {///取消代理权限 删除它
                        String ls_savePath = Server.MapPath(ls_Des_fileName);
                        Eggsoft.Common.FileFolder.DeleteFile(ls_savePath);

                    }
                }


            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(strUserID, "制作专属海报", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "制作专属海报");
            }

            return ls_Des_fileName;

        }

        #endregion 海报


        public static void Mark_ZhengShu_WithBase_GivedImage_WithText(string fileDes_CertifcationPath, string strText, RectangleF textareaMy)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(fileDes_CertifcationPath);
            Bitmap bitmap = new Bitmap(image, image.Width, image.Height);
            Graphics g = Graphics.FromImage(bitmap);
            try
            {

                float fontsize = 12.0f; //字体大小 
                float textwidth = strText.Length * fontsize; //文本的长度 
                //下面定义一个矩形区域，以后在这个矩形里画上白底黑字 
                float rectx = textareaMy.Left;
                float recty = textareaMy.Top;
                float rectwidth = strText.Length * (fontsize + 8);
                float rectheight = fontsize + 8; //声明矩形域 
                RectangleF textarea = new RectangleF(rectx, recty, rectwidth, rectheight);
                Font font = new Font("微软雅黑", fontsize); //定义字体 
                Brush whitebrush = new SolidBrush(Color.Black); //白笔刷，画文字用 
                Brush blackbrush = new SolidBrush(Color.White); //黑笔刷，画背景用 
                g.FillRectangle(blackbrush, textarea.Left, textarea.Top, textarea.Width, textarea.Height);
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
        public static void Mark_ZhengShu_WithBase_GivedImage(string fileDes_CertifcationPath, string waterErWeimaFile, Rectangle Rectangle_Mark_ZhengShu_WithBase_GivedImage)
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
