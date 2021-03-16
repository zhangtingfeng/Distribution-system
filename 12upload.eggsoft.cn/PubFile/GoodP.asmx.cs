using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// GoodP 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GoodP : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string APPCODE_getImage_ForceGet(String strPath, String strintWidth, String strintHeight)//强行获取
        {


            if (string.IsNullOrEmpty(strPath)) return "";
            String strAPPCODE_getImage = "";


            try
            {
                int intWidth = Int32.Parse(strintWidth);
                int intHeight = Int32.Parse(strintHeight);


                String[] myString = Eggsoft.Common.Image.getFileBMPWidthAndHeight(strPath);
                //Eggsoft.Common.FileFolder.

                String strNormalName = Eggsoft.Common.FileFolder.getFileNormalName(strPath);
                String strDirectoryName = Eggsoft.Common.FileFolder.getDirectoryName(strPath);
                String strFileType = Eggsoft.Common.FileFolder.getFileType(strPath);

                string str_intWidth = strDirectoryName + @"\" + strNormalName + "_" + intWidth.ToString() + strFileType;
                if (File.Exists(str_intWidth))
                {
                    strAPPCODE_getImage = "/" + Eggsoft.Common.FileFolder.urlconvertor(str_intWidth);
                }
                else
                {
                    String[] myWHString = Eggsoft.Common.Image.getFileBMPWidthAndHeight(strPath);

                    if (((Int32.Parse(myString[0]) > intWidth) || (Int32.Parse(myString[1]) > intHeight)))
                    {
                        string str100 = strDirectoryName + @"\" + strNormalName + "_100" + strFileType;
                        File.Copy(System.Web.HttpContext.Current.Server.MapPath(strPath), str100, true);
                        Eggsoft_Public_CL.GoodP.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str100), 100, intHeight, "hw");
                        strPath = str100;
                    }
                    strAPPCODE_getImage = strPath;
                }
            }

            catch { }
            finally { }
            return strAPPCODE_getImage;
        }

        [WebMethod]
        public string APPCODE_getFirstImage_Int(String strintGoods)//强行获取
        {
            int intGoods = Int32.Parse(strintGoods);

            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

            Model_tab_Goods = BLL_tab_Goods.GetModel(intGoods);

            return APPCODE_getFirstImage_String(Model_tab_Goods.Icon);
        }

        [WebMethod]
        public string APPCODE_getFirstImage_String(String strTab_Goods_Icon)//强行获取
        {
            return Eggsoft_Public_CL.GoodP.APPCODE_getFirstImage_String(strTab_Goods_Icon);

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public string APPCODE_getImage_First(String strGoodIconPath, String stringWidth)
        {
            if (string.IsNullOrEmpty(strGoodIconPath)) return "";
            String strAPPCODE_getImage = "";
            string strPath = APPCODE_getFirstImage_String(strGoodIconPath);
            try
            {

                String strNormalName = Eggsoft.Common.FileFolder.getFileNormalName(strPath);
                String strDirectoryName = Eggsoft.Common.FileFolder.getDirectoryName(strPath);
                String strFileType = Eggsoft.Common.FileFolder.getFileType(strPath);
                string str_intWidth = strDirectoryName + @"\" + strNormalName + "_" + stringWidth + strFileType;
                if (File.Exists(str_intWidth))
                {
                    strAPPCODE_getImage = "/" + Eggsoft.Common.FileFolder.urlconvertor(str_intWidth);
                }
                else
                {
                    APPCODE_saveOtherImage(strPath);


                    if (File.Exists(str_intWidth))///重新检
                    {
                        strAPPCODE_getImage = "/" + Eggsoft.Common.FileFolder.urlconvertor(str_intWidth);
                    }
                    else
                    {


                        strAPPCODE_getImage = strPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally { }
            return strAPPCODE_getImage;
        }


        [WebMethod]
        public string APPCODE_getImage(String strPath, String stringWidth)
        {
            if (string.IsNullOrEmpty(strPath)) return "";
            String strAPPCODE_getImage = "";

            try
            {

                String strNormalName = Eggsoft.Common.FileFolder.getFileNormalName(strPath);
                String strDirectoryName = Eggsoft.Common.FileFolder.getDirectoryName(strPath);
                String strFileType = Eggsoft.Common.FileFolder.getFileType(strPath);
                string str_intWidth = strDirectoryName + @"\" + strNormalName + "_" + stringWidth + strFileType;
                if (File.Exists(str_intWidth))
                {
                    strAPPCODE_getImage = "/" + Eggsoft.Common.FileFolder.urlconvertor(str_intWidth);
                }


                else
                {
                    APPCODE_saveOtherImage(strPath);


                    if (File.Exists(str_intWidth))///重新检
                    {
                        strAPPCODE_getImage = "/" + Eggsoft.Common.FileFolder.urlconvertor(str_intWidth);
                    }
                    else
                    {


                        strAPPCODE_getImage = strPath;
                    }
                    //debug_Log.Call_WriteLog("APPCODE_getImage(String strPath, int intWidth)=4");
                }



            }

            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally { }
            return strAPPCODE_getImage;
        }


        [WebMethod]
        public void Scale_Down_BMP_HW_(String DownLoadOriginalImagePath, String originalImagePath, String strwidth, String strheight, string mode)
        {
            try
            {
                #region only是检测石是否位图
                WebRequest webreq = WebRequest.Create(DownLoadOriginalImagePath);
                //红色部分为文件URL地址，这里是一张图片
                WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();
                System.Drawing.Image image;
                image = System.Drawing.Image.FromStream(stream);//如果不是位图 这里会出错
                stream.Close();
                #endregion

                if (Eggsoft.Common.FileFolder.File_Exists(originalImagePath))
                {
                    Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(originalImagePath));
                }

                WebClient client = new WebClient();
                string strMapPath = System.Web.HttpContext.Current.Server.MapPath(originalImagePath);

                client.DownloadFile(DownLoadOriginalImagePath, strMapPath);
                Eggsoft.Common.Image.ScaleBMP(originalImagePath, Int32.Parse(strwidth), Int32.Parse(strheight), mode);
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally { }
        }
        /// <summary>
        /// 确保 头像 小文件的路径 存在
        /// </summary>
        /// <param name="DownLoadOriginalImagePath"></param>
        /// <param name="originalImagePath"></param>
        /// <param name="strwidth"></param>
        /// <param name="strheight"></param>
        /// <param name="mode"></param>
        [WebMethod]
        public void Scale_Down_BMP_HW__User_path(String strHead_Parent_Image)
        {
            try
            {
                String ls_savePath = System.Web.HttpContext.Current.Server.MapPath(strHead_Parent_Image);
                string strgetDirectory = Eggsoft.Common.FileFolder.getDirectoryName(strHead_Parent_Image);
                if (Directory.Exists(strgetDirectory))//判断是否存在
                {
                }
                else
                {
                    Directory.CreateDirectory(strgetDirectory);//创建新路径
                }
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally { }
        }

        [WebMethod]
        public void Scale_BMP_HW_(String strPath, String strintWidth, String strintHeight)
        {
            Eggsoft_Public_CL.GoodP.ScaleBMP(strPath, Int32.Parse(strintWidth), Int32.Parse(strintHeight), "HW");

        }

        [WebMethod]
        public string APPCODE_getImage_HW_(String strPath, String strintWidth, String strintHeight)
        {


            if (string.IsNullOrEmpty(strPath)) return "";
            String strAPPCODE_getImage = "";

            try
            {
                String strNormalName = Eggsoft.Common.FileFolder.getFileNormalName(strPath);
                String strDirectoryName = Eggsoft.Common.FileFolder.getDirectoryName(strPath);
                String strFileType = Eggsoft.Common.FileFolder.getFileType(strPath);

                //debug_Log.Call_WriteLog("APPCODE_getImage(String strPath, int intWidth)=1");
                string str_intWidth_intHeight = strDirectoryName + @"\" + strNormalName + "_" + strintWidth + "_" + strintHeight + strFileType;
                if (File.Exists(str_intWidth_intHeight))
                {
                    strAPPCODE_getImage = "/" + Eggsoft.Common.FileFolder.urlconvertor(str_intWidth_intHeight);
                }


                else
                {


                    APPCODE_saveOtherImage_HW_(strPath, strintWidth, strintHeight);

                    if (File.Exists(str_intWidth_intHeight))///重新检
                    {
                        strAPPCODE_getImage = "/" + Eggsoft.Common.FileFolder.urlconvertor(str_intWidth_intHeight);
                    }
                    else
                    {

                        strAPPCODE_getImage = strPath;
                    }
                }



            }

            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally { }
            return strAPPCODE_getImage;
        }


        public static void APPCODE_saveOtherImage_HW_(String strIcon, String strintWidth, String strintHeight)
        {
            try
            {
                int intWidth = Int32.Parse(strintWidth);
                int intHeight = Int32.Parse(strintHeight);

                //string strIcon = myDataTable.Rows[i]["Icon"].ToString();
                String[] myString = Eggsoft.Common.Image.getFileBMPWidthAndHeight(strIcon);
                //if (myString == null) continue;
                //Eggsoft.Common.FileFolder.
                if (myString == null) return;///本地没有文件

                String strNormalName = Eggsoft.Common.FileFolder.getFileNormalName(strIcon);
                String strDirectoryName = Eggsoft.Common.FileFolder.getDirectoryName(strIcon);
                String strFileType = Eggsoft.Common.FileFolder.getFileType(strIcon);



                string str_intWidth_Height = strDirectoryName + @"\" + strNormalName + "_" + intWidth + "_" + intHeight + strFileType;
                File.Copy(System.Web.HttpContext.Current.Server.MapPath(strIcon), str_intWidth_Height, true);
                Eggsoft_Public_CL.GoodP.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str_intWidth_Height), intWidth, intHeight, "HW");

            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally { }

        }
        [WebMethod]
        public static void APPCODE_saveOtherImage(String strIcon)
        {
            Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage(strIcon);

        }
        [WebMethod]
        public static String APPCODE_GetGoodErWeiMaImage(string strType, string strShopID, string strhttpUrl, string strParentID, string strGoodsIDOrb004_OperationGoods)
        {
            //Eggsoft_Public_CL.GoodP.APPCODE_GetGoodErWeiMaImage(strIcon);

            string strGetGoodErWeiMaImage = Class_Pub.APPCODE_GetGoodErWeiMaImage(strType, strShopID, strhttpUrl, strParentID, strGoodsIDOrb004_OperationGoods);
            return strGetGoodErWeiMaImage;

        }
    }
}
