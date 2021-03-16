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
    /// WS_Redwallet 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_Redwallet : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public void WMeImage_GetWallet(string strintShopClientID)//
        {
            int intShopClientID = 0;
            int.TryParse(strintShopClientID, out intShopClientID);

            try
            {
                /// object lock_ojb = new object();

                lock ("WebMethod_APPCODE_getImage_GetWallet20150721989258454")
                {
                    /*get AgentURL*/

                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);





                    #region 复制四个红包
                    string strreadWallet0_Opened_From = "/UpLoad/ShopClientIcon/MreadWallet0_Opened.jpg";
                    string strreadWallet1_Opened_From = "/UpLoad/ShopClientIcon/MreadWallet1_Opened.jpg";
                    string strredWallet0_Share_From = "/UpLoad/ShopClientIcon/MredWallet0_Share.jpg";
                    string strredWallet1_Share_From = "/UpLoad/ShopClientIcon/MredWallet1_Share.jpg";
                    string strredWallet0_Share_Icon_From = "/UpLoad/ShopClientIcon/MredWeBuy0_Icon.jpg";
                    string strredWallet1_Share_Icon_From = "/UpLoad/ShopClientIcon/MredWeBuy1_Icon.jpg";



                    string strreadWallet0_Opened = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MreadWallet0_Opened.jpg";
                    string strreadWallet1_Opened = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MreadWallet1_Opened.jpg";
                    string strredWallet0_Share = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MredWallet0_Share.jpg";
                    string strredWallet1_Share = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MredWallet1_Share.jpg";
                    string strredWallet0_Share_Icon_ = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MredWeBuy0_Icon.jpg";
                    string strredWallet1_Share_Icon_ = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MredWeBuy1_Icon.jpg";
                    if (Eggsoft.Common.FileFolder.File_Exists(strreadWallet0_Opened) == false)///不要重复下载
                    {
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(strreadWallet0_Opened_From), System.Web.HttpContext.Current.Server.MapPath(strreadWallet0_Opened), true);
                    }

                    if (Eggsoft.Common.FileFolder.File_Exists(strreadWallet1_Opened) == false)///不要重复下载
                    {
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(strreadWallet1_Opened_From), System.Web.HttpContext.Current.Server.MapPath(strreadWallet1_Opened), true);
                    }

                    if (Eggsoft.Common.FileFolder.File_Exists(strredWallet0_Share) == false)///不要重复下载
                    {
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(strredWallet0_Share_From), System.Web.HttpContext.Current.Server.MapPath(strredWallet0_Share), true);
                    }

                    if (Eggsoft.Common.FileFolder.File_Exists(strredWallet1_Share) == false)///不要重复下载
                    {
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(strredWallet1_Share_From), System.Web.HttpContext.Current.Server.MapPath(strredWallet1_Share), true);
                    }
                    if (Eggsoft.Common.FileFolder.File_Exists(strredWallet0_Share_Icon_) == false)///不要重复下载
                    {
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(strredWallet0_Share_Icon_From), System.Web.HttpContext.Current.Server.MapPath(strredWallet0_Share_Icon_), true);
                    }
                    if (Eggsoft.Common.FileFolder.File_Exists(strredWallet1_Share_Icon_) == false)///不要重复下载
                    {
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(strredWallet1_Share_Icon_From), System.Web.HttpContext.Current.Server.MapPath(strredWallet1_Share_Icon_), true);
                    }

                    #endregion

                    #region  制作证书

                    string strXML = Model_tab_ShopClient.XML;
                    Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);


                    #region
                    string strname = Model_tab_ShopClient.ShopClientName;
                    if (strname.Length < 14) strname += "专用微商红包";

                    Eggsoft.Common.debug_Log.Call_WriteLog("1234:");
                    Mark_ZhengShu_WithBase_GivedImage_WithText(System.Web.HttpContext.Current.Server.MapPath(strreadWallet0_Opened), strname, new Rectangle(8, 680, 624, 68));
                    Mark_ZhengShu_WithBase_GivedImage_WithText(System.Web.HttpContext.Current.Server.MapPath(strreadWallet1_Opened), strname, new Rectangle(8, 680, 624, 68));
                    Mark_ZhengShu_WithBase_GivedImage_WithText(System.Web.HttpContext.Current.Server.MapPath(strredWallet0_Share), strname, new Rectangle(8, 404, 624, 52));
                    Mark_ZhengShu_WithBase_GivedImage_WithText(System.Web.HttpContext.Current.Server.MapPath(strredWallet1_Share), strname, new Rectangle(8, 404, 624, 52));
                    #endregion

                    Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(intShopClientID);

                    Eggsoft.Common.debug_Log.Call_WriteLog("5678910:");

                    #endregion

                }//endlock 


            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }

            // return ls_Des_fileName;

        }

        public static void Mark_ZhengShu_WithBase_GivedImage_WithText(string fileDes_CertifcationPath, string strText, RectangleF textareaMy)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(fileDes_CertifcationPath);
            Bitmap bitmap = new Bitmap(image, image.Width, image.Height);
            Graphics g = Graphics.FromImage(bitmap);
            try
            {

                float fontsize = 24.0f; //字体大小 
                float textwidth = strText.Length * fontsize; //文本的长度 
                //下面定义一个矩形区域，以后在这个矩形里画上白底黑字 
                float rectx = textareaMy.Left;
                float recty = textareaMy.Top;
                //float rectwidth = strText.Length * (fontsize + 8);
                //float rectheight = fontsize + 10; //声明矩形域 
                RectangleF textarea = new RectangleF(rectx, recty, textareaMy.Width, textareaMy.Height);
                Font font = new Font("微软雅黑", fontsize); //定义字体 
                Brush whitebrush = new SolidBrush(Color.Black); //白笔刷，画文字用 
                Brush blackbrush = new SolidBrush(Color.Red); //黑笔刷，画背景用 
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
    }
}
