using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QiNiuUpload;

namespace Eggsoft_Public_CL
{
    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class Upload
    {
        /// <summary>
        /// 发起一次七牛的同步任务
        /// </summary>
        /// <param name="intShopClientID"></param>
        public static void doUploadToQiNiu_Task(int intShopClientID)
        {
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(intShopClientID);


            string strUpLoadPath = Model_tab_ShopClient.UpLoadPath;///Upload/000001_sh
            strUpLoadPath = strUpLoadPath.Substring("/Upload/".Length, strUpLoadPath.Length - "/Upload/".Length);
            //EggsoftWX.BLL.tab_DoTask_Services bll_tab_DoTask_Services = new EggsoftWX.BLL.tab_DoTask_Services();

            try
            {///System.Configuration.ConfigurationManager.AppSettings
                
                string strUpload = "Upload";
                string strAddPath = strUpLoadPath;
                new DoUpload().go(strUpload, strAddPath);
            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "七牛报错");
            }

            //bool boolhaveAdd = bll_tab_DoTask_Services.Exists("TaskIfDone=0 and TaskType='DoUploadResource' and TaskXML='" + strUpLoadPath + "'");
            //if (boolhaveAdd == false)
            //{
            //EggsoftWX.Model.tab_DoTask_Services Model_tab_DoTask_Services = new EggsoftWX.Model.tab_DoTask_Services();
            //Model_tab_DoTask_Services.InsertTime = DateTime.Now;
            //Model_tab_DoTask_Services.TaskIfDone = false;
            //Model_tab_DoTask_Services.TaskType = "DoUploadResource";
            //Model_tab_DoTask_Services.TaskXML = strUpLoadPath;
            //bll_tab_DoTask_Services.Add(Model_tab_DoTask_Services);
            //}///已有任务  就不赛了
        }

        public Upload()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string getUploadPathFromShopClientID(int intShopClientID)
        {
            string STR_tab_ShopClient_ModelUpLoadPath = new EggsoftWX.BLL.tab_ShopClient().GetList("UpLoadPath", "id=" + intShopClientID).Tables[0].Rows[0][0].ToString();

            return STR_tab_ShopClient_ModelUpLoadPath;
        }


        public static string getBaseUrlD()
        {
            string strwebuy8_ClientAdmin_Users = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");

            string BaseUrl = "";

            if (String.IsNullOrEmpty(strwebuy8_ClientAdmin_Users))
            {
                BaseUrl = "/upload/";
            }
            else
            {
                BaseUrl = getUploadPathFromShopClientID(Int32.Parse(strwebuy8_ClientAdmin_Users.ToString())) + "/images/";
            }
            //string BaseUrl = getUploadPathFromShopClientID(Int32.Parse()) + "/images/";
            return BaseUrl;
            //   string STR_tab_ShopClient_ModelUpLoadPath = new EggsoftWX.BLL.tab_ShopClient().GetList("UpLoadPath", "id=" + intUserID).Tables[0].Rows[0][0].ToString();

            // return STR_tab_ShopClient_ModelUpLoadPath;
        }
        //  BaseUrl = upload.getBaseUrlD();
        //        BaseUrl = upload.getUploadPathFromShopClientID(Int32.Parse(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString())) + "/images/";


    }
}