using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _12upload.eggsoft.cn.PubFile
{
    /// <summary>
    /// Pub_Take_Goods_Path 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Pub_Take_Goods_Path : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        [WebMethod]
        public string WebMethod_APPCODE_Take_Goods_Path(String strOrderID)//
        {
            string strPath = "";
            string strMapPath = Eggsoft_Public_CL.FahuoDan.Pub_Take_Goods_Path(Int32.Parse(strOrderID), out strPath);


            int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromOrderID(Int32.Parse(strOrderID));
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);

            string strLinkURL = "https://" + Model_tab_ShopClient.ErJiYuMing + "/cart_good2_o2o_book.aspx?type=givegood&orderid=" + strOrderID;
            Class_Pub.Class_Pub_APPCODE_getImage_QRCodeImages(strMapPath, strLinkURL, "");
            return "1";
            //return APPCODE_getImage_UserAgentCertification(strUserID);
        }

        /// <summary>
        /// http://localhost:8014/SmallProgram/index.aspx?UserID=55182&opt=getQR&type=0&ShopClientID=26
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="intShopClientID"></param>
        /// <param name="strCardmemberNum"></param>
        /// <returns></returns>
        [WebMethod]
        public string Pub_Take__Path(String UserID, String intShopClientID, String strCardmemberNum)//
        {
            string strPath = "";
            string strMapPath = Eggsoft_Public_CL.FahuoDan.Pub_Take__Path(UserID.toInt32(), intShopClientID.toInt32(), out strPath);


            //int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromOrderID(Int32.Parse(strOrderID));
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID.toInt32());

            //string strLinkURL = "https://" + Model_tab_ShopClient.ErJiYuMing + "/cart_good2_o2o_book.aspx?type=givegood&orderid=" + strOrderID;
            Class_Pub.Class_Pub_APPCODE_getImage_QRCodeImages(strMapPath, strCardmemberNum, "");
            Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(intShopClientID.toInt32());
            return strMapPath;
            //return APPCODE_getImage_UserAgentCertification(strUserID);
        }

        /// <summary>
        /// http://localhost:8014/SmallProgram/index.aspx?UserID=55182&opt=getQR&type=0&ShopClientID=26
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="intShopClientID"></param>
        /// <param name="strCardmemberNum"></param>
        /// <returns></returns>
        [WebMethod]
        public string Pub_Take__Path_BarCode(String UserID, String intShopClientID, String strCardmemberNum)//
        {
            string strPath = "";
            string strMapPath = Eggsoft_Public_CL.FahuoDan.Pub_Take__PathBarCode(UserID.toInt32(), intShopClientID.toInt32(), out strPath);


            //int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromOrderID(Int32.Parse(strOrderID));
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID.toInt32());

            Class_Pub.Class_Pub_APPCODE_getImage_GenerateBarCodeBySpire(strMapPath, strCardmemberNum);
            Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(intShopClientID.toInt32());
            return strMapPath;
            //return APPCODE_getImage_UserAgentCertification(strUserID);
        }
    }
}
