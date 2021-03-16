using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using ZXing;
using ZXing.Common;

namespace _12upload.eggsoft.cn
{
    public class Class_Pub
    {



        public static void makeFooter(int intShopClient)
        {

            string strHTML = "";

            strHTML += "   <a href=\"javascript:void(0);\" class=\"WX_backtop J_backTop J_ytag\" id=\"gotopbtn\">返回顶部</a>\n";
            strHTML += "<script type=\"text/javascript\">\n";
            strHTML += "    //注册当点击返回顶部的时候，回到网页顶部  \n";
            strHTML += "   $('#gotopbtn').click(function () {\n";
            strHTML += "      $('body').scrollTop(0);\n";
            strHTML += "       test();\n";
            strHTML += "   });\n";
            strHTML += "   //注册当页面发生滚动事件的时候，判断他有没有滚动条，如果有滚动条就显示“返回”，如果没有就不返回  \n";
            strHTML += "   window.onscroll = test;\n";
            strHTML += "   function test() {\n";
            strHTML += "       if ($('body').scrollTop() == 0) {\n";
            strHTML += "          $('#gotopbtn').removeClass(\"WX_backtop_active\");\n";
            strHTML += "       } else {\n";
            strHTML += "          $('#gotopbtn').addClass(\"WX_backtop_active\");\n";
            strHTML += "      }\n";
            strHTML += "  }\n";
            strHTML += "   $('#menu').click(function () {\n";
            strHTML += "      $('#menulist').toggle();\n";
            strHTML += "  });\n";
            strHTML += "</script>\n";
            strHTML += "####<%=_Pub_strstrFooter%>####\n";

            EggsoftWX.BLL.tab_ShopClient_NET_ROOT_Menu BLL_tab_ShopClient_NET_ROOT_Menu = new EggsoftWX.BLL.tab_ShopClient_NET_ROOT_Menu();
            int intExsitCount = BLL_tab_ShopClient_NET_ROOT_Menu.ExistsCount("ShopClientID=" + intShopClient + " and ParentID=0");
            int intWidthPercent = 0;

            //intExsitCount = 0;
            if (intExsitCount == 0)
            {
                intWidthPercent = 0;
            }
            else if (intExsitCount == 1)
            {
                intWidthPercent = 100;
            }
            else if (intExsitCount == 2)
            {
                intWidthPercent = 49;
            }
            else if (intExsitCount == 3)
            {
                intWidthPercent = 33;
            }
            string strFooter = "";
            strFooter += "        <div id=\"foot777er\">\n";
            strFooter += "    <!-- 底部 -->\n";
            strFooter += "    <div id=\"WUC_Bottom\">\n";
            if (intExsitCount > 0)
            {
                strFooter += "   <style type=\"text/css\">\n";

                strFooter += "body:after {\n";
                strFooter += " content: '';\n";
                strFooter += "display: block;\n";
                strFooter += " height: 50px;\n";
                strFooter += "width: 100%;\n";
                strFooter += " border-bottom: 10px;\n";
                strFooter += "}\n";
                strFooter += "       \n";
                strFooter += "  \n";
                strFooter += "   </style>\n";



                strFooter += "        <div class=\"bottom_navs\">\n";
                strFooter += "            <ul>\n";
                System.Data.DataTable myDataTable = BLL_tab_ShopClient_NET_ROOT_Menu.GetList("ShopClientID=" + intShopClient + " and ParentID=0 order by pos,id asc").Tables[0];


                int[] intList = new int[3];
                for (int i = 0; i < intExsitCount; i++)
                {
                    string strID = myDataTable.Rows[i]["ID"].ToString();
                    string strMenuName = myDataTable.Rows[i]["MenuName"].ToString();
                    string strMenuLink = myDataTable.Rows[i]["MenuLink"].ToString();
                    int intExsitChildCount = BLL_tab_ShopClient_NET_ROOT_Menu.ExistsCount("ParentID=" + strID);


                    string strStyle = "style=\"width:" + intWidthPercent + "%;\"";
                    if (i > 0)
                    {
                        strStyle = "style=\"width:" + intWidthPercent + "%;border-left:1px solid #ccc;\"";
                    }
                    if (intExsitChildCount == 0)
                    {
                        strFooter += "              <a href=\"" + strMenuLink + "\">\n";
                        strFooter += "                    <li " + strStyle + ">" + strMenuName + "</li>\n";
                        strFooter += "              </a>\n";
                    }
                    else
                    {
                        strFooter += "             <a id=\"icontact" + i + "\" href=\"javascript:;\">\n";
                        strFooter += "                <li id=\"line_Right" + i + "\"  " + strStyle + ">\n";
                        strFooter += "                         <span class=\"FootMenu\">" + strMenuName + "</span>\n";
                        strFooter += "                </li>\n";
                        strFooter += "              </a>\n";
                        intList[i] = Int32.Parse(strID);
                    }
                }
                strFooter += "            </ul>\n";
                strFooter += "        </div>\n";

                for (int i = 0; i < intList.Length; i++)
                {
                    int intParentID = intList[i];
                    if (intParentID > 0)
                    {
                        System.Data.DataTable myDataTableChild = BLL_tab_ShopClient_NET_ROOT_Menu.GetList("ParentID=" + intParentID + " order by pos,id asc").Tables[0];
                        strFooter += "                     <div class=\"open_nav\" id=\"divContact" + i + "\" style=\"display: none\">\n";
                        strFooter += "                       <script type=\"text/javascript\">var varButton" + i + "=" + myDataTableChild.Rows.Count + "</script>\n";
                        strFooter += "                          <div class=\"open_nav_conter\">\n";
                        strFooter += "                          <ul>\n";
                        for (int j = myDataTableChild.Rows.Count - 1; j >= 0; j--)
                        {
                            string strChildID = myDataTableChild.Rows[j]["ID"].ToString();
                            string strChildMenuName = myDataTableChild.Rows[j]["MenuName"].ToString();
                            string strChildMenuLink = myDataTableChild.Rows[j]["MenuLink"].ToString();
                            strFooter += "                            <li><a href=\"" + strChildMenuLink + "\">" + strChildMenuName + "</a></li>\n";
                        }
                        strFooter += "                           </ul></div>\n";
                        strFooter += "                      </div>\n";
                    }
                }
            }
            strFooter += "   </div>\n";
            strFooter += "</div>\n";
            strHTML = strHTML.Replace("####<%=_Pub_strstrFooter%>####", strFooter);

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClient);
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";
            STR_tab_ShopClient_ModelUpLoadPath += "/03Footer.html";
            //   strFooter = strFooter.Replace("###ShengQingDaiLiOrShowGuanliDaiLi###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text());

            Eggsoft.Common.FileFolder.WriteFile(STR_tab_ShopClient_ModelUpLoadPath, strHTML);
        }


        /** 
 * 商品校验码的算法  
 * ean-13条码算法  
 * 前12位的奇数位的和c1  
 * 前12位的偶数位的和c2  
 * 将奇数和跟偶数和的三倍相加  
 * 取结果的个位数，对十取余 （如果个位数是0，那么校验码不是10，而是0）
 * @author hanmj
  */

        public static void main(String[] args)
        {
            /*String eanCode = eanCode("123675223432");
            System.out.println(eanCode);*/
        }

        #region 条形码生成
        public static String eanCode(String Arg12code)
        {
            int c1 = 0;
            int c2 = 0;
            for (int i = 0; i < Arg12code.Length; i += 2)
            {
                char c = Arg12code[i];
                //字符串code中第i个位置上的字符 
                int n = c - '0';
                c1 += n;//累加奇数位的数字和   
            }
            for (int i = 1; i < Arg12code.Length; i += 2)
            {
                char c = Arg12code[i];//字符串code中第i个位置上的字符
                int n = c - '0';
                c2 += n;//累加偶数位的数字和   
            }
            int cc = c1 + c2 * 3;
            int check = cc % 10;
            check = (10 - cc % 10) % 10;
            string Return= Arg12code + check + "";
            return Return;
        }



        public static void Class_Pub_APPCODE_getImage_GenerateBarCodeBySpire(String str12LengthFilePath, string str13Number)//强行获取
        {
            String ls_savePath = System.Web.HttpContext.Current.Server.MapPath(str12LengthFilePath);
            string strgetDirectory = Eggsoft.Common.FileFolder.getDirectoryName(str12LengthFilePath);
            if (Directory.Exists(strgetDirectory))//判断是否存在
            {
                // Response.Write("已存在");
            }
            else
            {
                // Response.Write("不存在，正在创建");
                Directory.CreateDirectory(strgetDirectory);//创建新路径
            }

            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Width = 500;
            encodeOption.Height = 120; // 必须制定高度、宽度
             ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = BarcodeFormat.EAN_13; //  条形码规格：EAN13规格：12（无校验位）或13位数字
            //string streanCode = eanCode("123486789011");
            Bitmap img = wr.Write(str13Number); // 生成图片
            img.Save(ls_savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
          
        }
        #endregion 条形码生成

        /// <summary>
        /// 制作URl 二维码
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="strLinkURL"></param>
        /// <param name="errorCorrectIcon">LMQH   空为M</param>
        public static void Class_Pub_APPCODE_getImage_QRCodeImages(String strFilePath, string strLinkURL, string errorCorrectIcon)//强行获取
        {
            String ls_savePath = System.Web.HttpContext.Current.Server.MapPath(strFilePath);
            string strgetDirectory = Eggsoft.Common.FileFolder.getDirectoryName(strFilePath);
            if (Directory.Exists(strgetDirectory))//判断是否存在
            {
                // Response.Write("已存在");
            }
            else
            {
                // Response.Write("不存在，正在创建");
                Directory.CreateDirectory(strgetDirectory);//创建新路径
            }

            if (File.Exists(ls_savePath))
            {
               return;
            }


            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Numeric";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = Convert.ToInt32("4");
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);

                //msg.Text = "Invalid size!" + ex.Message;
                //return "";
            }
            try
            {
                int version = Convert.ToInt32("7");
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }

            string errorCorrect = errorCorrectIcon;
            if (errorCorrect == "")
            {
                errorCorrect = "M";
            }

            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            try
            {
                qrCodeEncoder.Encode(strLinkURL).Save(ls_savePath);
                qrCodeEncoder.Encode(strLinkURL).Dispose();
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
        }



        #region
        /// <summary>
        ///  string strType = Request.QueryString["type"];
        //string strShopID = Request.QueryString["ShopID"];
        //string strhttpUrl = Request.QueryString["httpUrl"];
        //string strParentID = Request.QueryString["ParentID"];
        /// </summary>
        /// <param name="strIcon"></param>
        /// <returns></returns>
        public static string APPCODE_GetGoodErWeiMaImage(String strType, String strShopID, String strhttpUrl, String strParentID, String strGoodsIDOrb004_OperationGoods, String strOperationID = "0")
        {
            string strReturnURL = "";
            try
            {
                ///代理路径
                if (strParentID == "0") strParentID = "";
                string strAgent = "";
                if (String.IsNullOrEmpty(strParentID) == false) strAgent = "/sagent-" + strParentID;///代理路径
                //end  代理路径                                                                         ///


                string strFileName = "";
                if ((strType == "ShopClientID") || (strType.ToLower() == "shopclientid"))
                {

                    string strHttp = strhttpUrl + strAgent + "/default.aspx";

                    strFileName = "Default-" + strShopID + "-" + strParentID;
                    strReturnURL = APPCODE_GetGoodErWeiMaImage_creatQRCodeImage(strHttp, strFileName);
                }
                else if ((strType == "Goods") || (strType.ToLower() == "goods"))
                {


                    if (string.IsNullOrEmpty(strGoodsIDOrb004_OperationGoods)) strGoodsIDOrb004_OperationGoods = "0";


                    string strHttp = strhttpUrl + strAgent + "/product-" + strGoodsIDOrb004_OperationGoods + ".aspx";
                    strFileName = "product-" + strGoodsIDOrb004_OperationGoods + "-" + strParentID;
                    int intShopID = Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(Int32.Parse(strGoodsIDOrb004_OperationGoods));
                    strReturnURL = APPCODE_GetGoodErWeiMaImage_creatQRCodeImage_Goods_Merge(intShopID, strHttp, strFileName, Eggsoft_Public_CL.GoodP.APPCODE_getFirstImage(Int32.Parse(strGoodsIDOrb004_OperationGoods)));
                }
                else if ((strType == "Operation") || (strType.ToLower() == "operation"))
                {
                    if (string.IsNullOrEmpty(strGoodsIDOrb004_OperationGoods)) strGoodsIDOrb004_OperationGoods = "0";
                    string strHttp = strhttpUrl + strAgent + "/op-" + strOperationID + "-" + strGoodsIDOrb004_OperationGoods + ".aspx";
                    strFileName = "op-" + strOperationID + "-" + strGoodsIDOrb004_OperationGoods + "-" + strParentID;

                    EggsoftWX.BLL.b004_OperationGoods BLLb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                    EggsoftWX.Model.b004_OperationGoods Modelb004_OperationGoods = BLLb004_OperationGoods.GetModel(strGoodsIDOrb004_OperationGoods.toInt32());

                    int intShopID = Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(Modelb004_OperationGoods.GoodID.toInt32());
                    strReturnURL = APPCODE_GetGoodErWeiMaImage_creatQRCodeImage_Goods_Merge(intShopID, strHttp, strFileName, Eggsoft_Public_CL.GoodP.APPCODE_getFirstImage(Modelb004_OperationGoods.GoodID.toInt32()));
                }

            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }
            finally { }
            return strReturnURL;
        }
        private static Object objcGoodTempFile202 = new object();
        public static string APPCODE_GetGoodErWeiMaImage_creatQRCodeImage_Goods_Merge(int intShopID, string strURL, string strFileName, string strGoodFileName)
        {
            string strReturnURL = "";
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = Convert.ToInt32("4");
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            try
            {
                int version = Convert.ToInt32("7");
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }

            string errorCorrect = "Q";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            try
            {
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopID);
                string strUploadPath = Model_tab_ShopClient.UpLoadPath;
                string strShopIDQRCodeImagePath = strUploadPath + "/QRCodeImage/";


                lock (objcGoodTempFile202)
                {
                    String ls_fileName = strFileName + ".png";
                    //String ls_fileName = DateTime.Now.ToString("yyyyMMddhhmmsssss") + ".png";
                    String ls_savePath = HttpContext.Current.Server.MapPath(strShopIDQRCodeImagePath + ls_fileName);
                    String ls_savePath_01Source = System.Web.HttpContext.Current.Server.MapPath("/upload/QRCodeImages/01Source.png");


                    Eggsoft.Common.FileFolder.makeFolder(System.Web.HttpContext.Current.Server.MapPath(strShopIDQRCodeImagePath));
                    string strTemp = System.Web.HttpContext.Current.Server.MapPath(strShopIDQRCodeImagePath + "GoodTempFile202.png");
                    Eggsoft.Common.FileFolder.DeleteFile(strTemp);
                    System.IO.File.Copy(ls_savePath_01Source, strTemp, true);

                    Eggsoft.Common.Image.Mark_ErWeiMa_Goods(strTemp, System.Web.HttpContext.Current.Server.MapPath(strGoodFileName));

                    qrCodeEncoder.Encode(strURL).Save(ls_savePath);///字符串较长的情况下，用ThoughtWorks.QRCode生成二维码时出现“索引超出了数组界限”的错误。

                    // 解决方法：将 QRCodeVersion 改为0。
                    qrCodeEncoder.Encode(strURL).Dispose();
                    Eggsoft.Common.Image.Mark_ErWeiMa_WithBase_Goods(ls_savePath, strTemp);
                    strReturnURL = strShopIDQRCodeImagePath + ls_fileName;
                    Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(intShopID);

                }
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("int intShopID=" + intShopID + ", string strURL=" + strURL + ", string strFileName=" + strFileName + ", string strGoodFileName=" + strGoodFileName + "", "二维码生成");
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "二维码生成");
            }

            return strReturnURL;
        }


        protected static string APPCODE_GetGoodErWeiMaImage_creatQRCodeImage(string strURL, string strFileName)
        {
            string strReturn = "";
            //if (txtEncodeData.Text.Trim() == String.Empty)
            //{
            //    msg.Text = "Data must not be empty.";
            //    return;
            //}

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = Convert.ToInt32("4");
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            try
            {
                int version = Convert.ToInt32("7");
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            try
            {
                String ls_fileName = strFileName + ".png";
                //String ls_fileName = DateTime.Now.ToString("yyyyMMddhhmmsssss") + ".png";
                String ls_savePath = HttpContext.Current.Server.MapPath("/upload/QRCodeImages/" + ls_fileName);
                //msg.Text = ls_savePath;
                //String ls_savePath_01Source = Server.MapPath("/upload/QRCodeImages/01Source.png");

                qrCodeEncoder.Encode(strURL).Save(ls_savePath);
                qrCodeEncoder.Encode(strURL).Dispose();
                //Eggsoft.Common.Image.Mark_ErWeiMa(ls_savePath, ls_savePath_01Source);

                strReturn = "/upload/QRCodeImages/" + ls_fileName;
                //ImageButton2.ImageUrl = "/QRCodeImages/PNG/" + ls_fileName;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            return strReturn;
        }
        #endregion

    }

}