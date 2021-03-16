using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data;
using System.Collections;
using Eggsoft.Common;

namespace Eggsoft_Public_CL
{

    /// <summary>
    ///Pub 的摘要说明
    /// </summary>
    public class Pub
    {
        public Pub()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 得到一篇文章中的所有图标 如果没有 就用商店图片代替
        /// </summary>
        /// <returns></returns>
        public static String GetFirstHtmlImageUrlByShopClientID(string strHtmlContent, int pub_Int_ShopClientID)
        {

            string pub_str_FirstImageFull = Eggsoft.Common.Image.GetFirstHtmlImageUrl(strHtmlContent);
            if (pub_str_FirstImageFull == "")
            {

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strShopLogoImage;
            }
            if (pub_str_FirstImageFull.ToLower().IndexOf("http") == -1)
            {
                pub_str_FirstImageFull = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + pub_str_FirstImageFull;
            }
            return pub_str_FirstImageFull;
        }

        public static void Get_o2o_NestUserID__(int pub_Int_Session_CurUserID, out double argdoubleBaiDulng, out double argdoubleBaiDulat, out string argStringBaiDuAdress)
        {

            String strMaxID = "0";
            EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat BLL_tab_ShopClient_User_Lng_Lat = new EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat();
            bool boolWeiXin = BLL_tab_ShopClient_User_Lng_Lat.Exists("aspxDescription='微信公众平台地理位置' and UserID=" + pub_Int_Session_CurUserID);
            if (boolWeiXin)
            {
                System.Data.DataTable myDataTable = BLL_tab_ShopClient_User_Lng_Lat.SelectList("select max(ID) as maxID from tab_ShopClient_User_Lng_Lat where (aspxDescription='微信公众平台地理位置') and UserID=" + pub_Int_Session_CurUserID).Tables[0];
                if (myDataTable.Rows.Count > 0)
                {
                    strMaxID = myDataTable.Rows[0]["maxID"].ToString();
                }
            }
            else
            {
                bool boolAnyWeiXin = BLL_tab_ShopClient_User_Lng_Lat.Exists("UserID=" + pub_Int_Session_CurUserID);
                if (boolAnyWeiXin)
                {
                    System.Data.DataTable myDataTable = BLL_tab_ShopClient_User_Lng_Lat.SelectList("select max(ID) as maxID from tab_ShopClient_User_Lng_Lat where UserID=" + pub_Int_Session_CurUserID).Tables[0];
                    if (myDataTable.Rows.Count > 0)
                    {
                        strMaxID = myDataTable.Rows[0]["maxID"].ToString();
                    }
                }
            }
            double doubleBaiDulng = 113.132721;
            double doubleBaiDulat = 32.98138;
            string strBaiDuAdress = "";

            int intMaxID = 0;
            int.TryParse(strMaxID, out intMaxID);
            if (intMaxID > 0)
            {
                EggsoftWX.Model.tab_ShopClient_User_Lng_Lat Model_tab_ShopClient_User_Lng_Lat = BLL_tab_ShopClient_User_Lng_Lat.GetModel(intMaxID);
                string strBaiDulng = Model_tab_ShopClient_User_Lng_Lat.BaiDulng;
                string strBaiDulat = Model_tab_ShopClient_User_Lng_Lat.BaiDulat;
                strBaiDuAdress = Model_tab_ShopClient_User_Lng_Lat.BaiDuAdress;
                if (String.IsNullOrEmpty(strBaiDulng) == false)
                {
                    double.TryParse(strBaiDulng, out doubleBaiDulng);
                    double.TryParse(strBaiDulat, out doubleBaiDulat);
                }
            }
            argdoubleBaiDulng = doubleBaiDulng;
            argdoubleBaiDulat = doubleBaiDulat;
            argStringBaiDuAdress = strBaiDuAdress;
        }

        public static string Get_o2o_script_From_ShopClientID_(int intUserID, string strDesc)
        {

            string strGet_o2o_script_From_ShopClientID_ = "";
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();

            //string CacheKey = "Get_o2o_script_From_ShopClientID_" + strShopClientID;
            //object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            //if (objType == null)
            //{
            try
            {
                ///关键是减少对数据库的访问
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + strShopClientID);
                string strHTML = "";

                if (boolExsit)
                {
                    ///是否存在30分钟的数据
                    #region
                    EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat BLL_tab_ShopClient_User_Lng_Lat = new EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat();
                    System.Data.DataTable myDataTable = BLL_tab_ShopClient_User_Lng_Lat.SelectList("select max(ID) as maxID from tab_ShopClient_User_Lng_Lat where UserID=" + intUserID).Tables[0];
                    string strMaxID = "0";
                    int intMaxID = 0;
                    if (myDataTable.Rows.Count > 0)
                    {
                        strMaxID = myDataTable.Rows[0]["maxID"].ToString();
                    }
                    int.TryParse(strMaxID, out intMaxID);
                    if (intMaxID > 0)
                    {
                        EggsoftWX.Model.tab_ShopClient_User_Lng_Lat Model_tab_ShopClient_User_Lng_Lat = BLL_tab_ShopClient_User_Lng_Lat.GetModel(intMaxID);
                        DateTime tdDateTime = Model_tab_ShopClient_User_Lng_Lat.UpdateTime;
                        DateTime tdDateTimeNow = DateTime.Now;
                        TimeSpan TimeSpanAge = tdDateTimeNow - tdDateTime;
                        if (TimeSpanAge.TotalMinutes > 15)
                        {
                            strHTML += "<script src='https://api.map.baidu.com/api?v=1.3'></script>\n";
                            strHTML += "<script type=\"text/javascript\">\n";
                            strHTML += "    var varneedSetVisitInfo=escape(\"" + strDesc + "\")\n";
                            strHTML += "    var url_FootMark_Html5_Save_=\"userid=" + intUserID + "&needSetVisitInfo=\"+varneedSetVisitInfo;\n";
                            strHTML += "    $(document).ready(function(){\n";
                            strHTML += "        var t=setTimeout(\"getLocation()\",2000);  //testAsync(111111, 222222);\n";
                            strHTML += "        \n";
                            strHTML += "    });\n";
                            strHTML += "</script>\n";
                            strHTML += "<script src='/Scripts/FootMarker_Location.js'></script>";
                        }
                    }
                    #endregion
                }
                strGet_o2o_script_From_ShopClientID_ = strHTML;
                //Eggsoft.Common.DataCache.SetCache(CacheKey, strGet_o2o_script_From_ShopClientID_);// 写入缓存   
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            //}
            //else
            //{
            //    strGet_o2o_script_From_ShopClientID_ = (string)objType;
            //}

            return strGet_o2o_script_From_ShopClientID_;
        }

        /// <summary>
        /// 读取权限 
        /// </summary>
        /// <param name="strShopClientID"></param>
        /// <param name="strItemPower"></param>
        /// <param name="boolIFCanche">是否读缓存，默认不读缓存（是否发模板消息）</param>
        /// <returns></returns>

        public static bool boolShowPower(string strShopClientID, string strItemPower, bool boolIFCanche = false, int? intRoldID = null)
        {
            string strPowerINT = "0";
            if (intRoldID == null)////如果有值说明是后台权限的调用   后台不搞缓存
            {
                if (boolIFCanche)
                {
                    string CacheKey = "Application" + strItemPower + strShopClientID.ToString();
                    object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
                    if (objType == null)
                    {
                        try
                        {
                            EggsoftWX.BLL.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_bll = new EggsoftWX.BLL.tab_Admin_ShopClientPower();

                            System.Data.DataTable myDataTable = tab_Admin_ShopClientPower_bll.GetList("PowerINT", "ShopClientID=" + strShopClientID + " and ShopClientPowerItemName='" + strItemPower + "'").Tables[0];
                            if (myDataTable.Rows.Count > 0)
                            {
                                strPowerINT = myDataTable.Rows[0]["PowerINT"].ToString();
                            }

                            //bool boolboolShowPower = false;
                            //bool.TryParse(strPowerINT, out boolboolShowPower);

                            Eggsoft.Common.DataCache.SetCache(CacheKey, strPowerINT);// 写入缓存   
                        }
                        catch { }
                    }
                    else
                    {
                        strPowerINT = (string)objType;
                    }
                }
                else
                {

                    EggsoftWX.BLL.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_bll = new EggsoftWX.BLL.tab_Admin_ShopClientPower();

                    System.Data.DataTable myDataTable = tab_Admin_ShopClientPower_bll.GetList("PowerINT", "ShopClientID=" + strShopClientID + " and ShopClientPowerItemName='" + strItemPower + "'").Tables[0];
                    if (myDataTable.Rows.Count > 0)
                    {
                        strPowerINT = myDataTable.Rows[0]["PowerINT"].ToString();
                    }
                }
            }
            else///一个商铺的多用户登陆
            {
                EggsoftWX.BLL.tab_Admin_ShopClientPower BLLtab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_Admin_ShopClientPower();
                EggsoftWX.Model.tab_Admin_ShopClientPower Modeltab_Admin_ShopClientPower = BLLtab_ShopClient_AdminUser.GetModel("ShopClientID=" + strShopClientID + " and PowerRoleID=" + intRoldID + "" + " and ShopClientPowerItemName='" + strItemPower + "'");


                if (Modeltab_Admin_ShopClientPower != null && Modeltab_Admin_ShopClientPower.PowerINT == true)
                {
                    strPowerINT = "true";
                }
            }
            bool boolboolShowPower = false;
            bool.TryParse(strPowerINT, out boolboolShowPower);
            return boolboolShowPower;
        }




        public static string GetstringShowPower_ShopName(string strShopClientID)
        {
            string strVouchersShopName = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "VouchersShopName");
            if (String.IsNullOrEmpty(strVouchersShopName))
            {
                strVouchersShopName = "购物券";
            }
            return strVouchersShopName;
        }

        public static string GetstringShowPower_AgentShopTextDesc(string strShopClientID)
        {
            string strAgentShopTextDesc = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "AgentShopTextDesc");
            if (String.IsNullOrEmpty(strAgentShopTextDesc))
            {
                strAgentShopTextDesc = "代理店铺";
            }
            return strAgentShopTextDesc;
        }
        public static void BoolSaveRoleShowPower(string strShopClientID, bool boolSelected, string strItemPower, int? RoleINT)
        {
            EggsoftWX.BLL.tab_Admin_ShopClientPower BLLttab_Admin_ShopClientPower_bll = new EggsoftWX.BLL.tab_Admin_ShopClientPower();
            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

            if (boolSelected)
            {
                EggsoftWX.Model.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_Model = new EggsoftWX.Model.tab_Admin_ShopClientPower();
                tab_Admin_ShopClientPower_Model = BLLttab_Admin_ShopClientPower_bll.GetModel("ShopClientID=" + strShopClientID + " and ShopClientPowerItemName='" + strItemPower + "'" + " and PowerRoleID=" + RoleINT + "");
                if (tab_Admin_ShopClientPower_Model == null)
                {
                    tab_Admin_ShopClientPower_Model = new EggsoftWX.Model.tab_Admin_ShopClientPower();
                    tab_Admin_ShopClientPower_Model.PowerINT = boolSelected;
                    tab_Admin_ShopClientPower_Model.ShopClientPowerItemName = strItemPower;
                    tab_Admin_ShopClientPower_Model.PowerRoleID = RoleINT;
                    tab_Admin_ShopClientPower_Model.Creatby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    tab_Admin_ShopClientPower_Model.ShopClientID = Int32.Parse(strShopClientID);
                    BLLttab_Admin_ShopClientPower_bll.Add(tab_Admin_ShopClientPower_Model);
                }
                else
                {
                    tab_Admin_ShopClientPower_Model.Updateby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    tab_Admin_ShopClientPower_Model.Updatetime = DateTime.Now;
                    tab_Admin_ShopClientPower_Model.PowerINT = boolSelected;
                    BLLttab_Admin_ShopClientPower_bll.Update(tab_Admin_ShopClientPower_Model);
                }
            }
            else
            {
                string strDelete = "ShopClientID = " + strShopClientID + " and ShopClientPowerItemName = '" + strItemPower + "'" + " and PowerRoleID = " + RoleINT + "";
                BLLttab_Admin_ShopClientPower_bll.Delete(strDelete);
            }



        }

        private static string GetCacheKeyname(string strShopClientID, string strItemPower)
        {
            string CacheKey = "perationCenter" + strShopClientID.toString() + strItemPower;
            return CacheKey;
        }


        public static string stringShowPower(string strShopClientID, string strItemPower)
        {
            string stringShowPower = "";

            string CacheKey = GetCacheKeyname(strShopClientID, strItemPower);
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null || string.IsNullOrEmpty(objType.toString()))
            {
                try
                {
                    #region
                    EggsoftWX.BLL.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_bll = new EggsoftWX.BLL.tab_Admin_ShopClientPower();

                    System.Data.DataTable myDataTable = tab_Admin_ShopClientPower_bll.GetList("PowerINTDatail", "ShopClientID=" + strShopClientID + " and ShopClientPowerItemName='" + strItemPower + "'").Tables[0];
                    if (myDataTable.Rows.Count > 0)
                    {
                        stringShowPower = myDataTable.Rows[0]["PowerINTDatail"].ToString();
                    }

                    #endregion
                    Eggsoft.Common.DataCache.SetCache(CacheKey, stringShowPower);// 写入缓存
                    //objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
                }
                catch { }
            }
            else
            {
                stringShowPower = Eggsoft.Common.DataCache.GetCache(CacheKey).toString();
            }
            return stringShowPower;
        }

        public static void boolSaveShowPower(string strShopClientID, string strItemPower, bool boolSelected)
        {

            EggsoftWX.BLL.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_bll = new EggsoftWX.BLL.tab_Admin_ShopClientPower();
            EggsoftWX.Model.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_Model = new EggsoftWX.Model.tab_Admin_ShopClientPower();
            tab_Admin_ShopClientPower_Model = tab_Admin_ShopClientPower_bll.GetModel("ShopClientID=" + strShopClientID + " and ShopClientPowerItemName='" + strItemPower + "'");
            if (tab_Admin_ShopClientPower_Model == null)
            {
                tab_Admin_ShopClientPower_Model = new EggsoftWX.Model.tab_Admin_ShopClientPower();
                tab_Admin_ShopClientPower_Model.PowerINT = boolSelected;
                tab_Admin_ShopClientPower_Model.ShopClientPowerItemName = strItemPower;
                tab_Admin_ShopClientPower_Model.ShopClientID = Int32.Parse(strShopClientID);
                tab_Admin_ShopClientPower_bll.Add(tab_Admin_ShopClientPower_Model);
            }
            else
            {
                tab_Admin_ShopClientPower_Model.PowerINT = boolSelected;
                tab_Admin_ShopClientPower_bll.Update(tab_Admin_ShopClientPower_Model);
            }
        }

        public static void boolSaveShowPower(string strShopClientID, string strItemPower, String stringShowPower)
        {

            EggsoftWX.BLL.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_bll = new EggsoftWX.BLL.tab_Admin_ShopClientPower();
            EggsoftWX.Model.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_Model = new EggsoftWX.Model.tab_Admin_ShopClientPower();
            tab_Admin_ShopClientPower_Model = tab_Admin_ShopClientPower_bll.GetModel("ShopClientID=" + strShopClientID + " and ShopClientPowerItemName='" + strItemPower + "'");
            if (tab_Admin_ShopClientPower_Model == null)
            {
                tab_Admin_ShopClientPower_Model = new EggsoftWX.Model.tab_Admin_ShopClientPower();
                tab_Admin_ShopClientPower_Model.PowerINTDatail = stringShowPower;
                tab_Admin_ShopClientPower_Model.ShopClientPowerItemName = strItemPower;
                tab_Admin_ShopClientPower_Model.ShopClientID = Int32.Parse(strShopClientID);
                tab_Admin_ShopClientPower_bll.Add(tab_Admin_ShopClientPower_Model);
            }
            else
            {
                string CacheKey = GetCacheKeyname(strShopClientID, strItemPower);
                Eggsoft.Common.DataCache.Remove(CacheKey);

                tab_Admin_ShopClientPower_Model.PowerINTDatail = stringShowPower;
                tab_Admin_ShopClientPower_Model.Updatetime = DateTime.Now;
                tab_Admin_ShopClientPower_bll.Update(tab_Admin_ShopClientPower_Model);
            }
        }



        public static void SendRed_Get_ABC___Task(string strShopClientID, out ArrayList ArrayListA, out ArrayList ArrayListB, out ArrayList ArrayListC)
        {

            EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
            DataTable myTab_User = bll_tab_User.GetList("ShopClientID=" + strShopClientID + " and Subscribe=1 and OpenID is not null order by id asc").Tables[0];
            ArrayList alcollectTab_UserTrue = new ArrayList();
            for (int i = 0; i < myTab_User.Rows.Count; i++)
            {
                String strUserID = myTab_User.Rows[i]["ID"].ToString();
                if (alcollectTab_UserTrue.Contains(strUserID))
                {
                }
                else
                {
                    alcollectTab_UserTrue.Add(strUserID);
                }
            }
            ArrayList alcollectTab_UserCCCC = (ArrayList)alcollectTab_UserTrue.Clone();//copy it

            EggsoftWX.BLL.View_ShopClient_Agent bll_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
            DataTable myShopClient_Agent = bll_View_ShopClient_Agent.GetList("ShopClientID=" + strShopClientID + " and Empowered=1 order by userID asc").Tables[0];
            ArrayList alcollectShopClient_AgentTrueAAAA = new ArrayList();
            for (int i = 0; i < myShopClient_Agent.Rows.Count; i++)
            {
                String strUserID = myShopClient_Agent.Rows[i]["userID"].ToString();
                if (alcollectShopClient_AgentTrueAAAA.Contains(strUserID))
                {
                }
                else
                {
                    if (alcollectTab_UserTrue.Contains(strUserID))///取消关注的 滚蛋
                    {
                        alcollectTab_UserCCCC.Remove(strUserID);
                        alcollectShopClient_AgentTrueAAAA.Add(strUserID);
                    }
                }
            }

            ArrayList alcollectPayStatusTrueBBBBB = new ArrayList();

            DataTable my = EggsoftWX.SQLServerDAL.DbHelperSQL.GetDataTable("SELECT tab_Order.PayStatus, tab_User.ID as userID, tab_User.ShopUserID, tab_User.ShopClientID, tab_User.OpenID, tab_User.Subscribe, tab_User.NickName FROM  tab_User LEFT OUTER JOIN  tab_Order ON tab_User.ID = tab_Order.UserID where tab_Order.PayStatus=1 and tab_User.ShopClientID=" + strShopClientID + " order by userID asc");

            for (int i = 0; i < my.Rows.Count; i++)
            {
                String strUserID = my.Rows[i]["userID"].ToString();
                if (alcollectPayStatusTrueBBBBB.Contains(strUserID))
                {
                    //Console.WriteLine(alcollect.IndexOf(str));
                }
                else
                {
                    if (!alcollectPayStatusTrueBBBBB.Contains(strUserID))
                    {
                        if (alcollectTab_UserTrue.Contains(strUserID))///取消关注的 滚蛋
                        {
                            alcollectTab_UserCCCC.Remove(strUserID);
                            alcollectPayStatusTrueBBBBB.Add(strUserID);
                        }
                    }
                    // Console.WriteLine("Not found!");
                }
            }

            ArrayListA = (ArrayList)alcollectShopClient_AgentTrueAAAA.Clone();//copy it
            ArrayListB = (ArrayList)alcollectPayStatusTrueBBBBB.Clone();//copy it
            ArrayListC = (ArrayList)alcollectTab_UserCCCC.Clone();//copy it


        }
        public static bool SendEmail_AddTask(string strFromCityName, string strTo, string strSubject, string strBody)
        {
            Eggsoft.Common.ClassEmail_Task myXML_Class = new Eggsoft.Common.ClassEmail_Task();
            myXML_Class.Email_FromCityName = strFromCityName;
            myXML_Class.Email_To = strTo;
            myXML_Class.Email_Subject = strSubject;
            myXML_Class.Email_Body = strBody;
            string strAndOther = Eggsoft.Common.XmlHelper.XmlSerialize(myXML_Class, System.Text.Encoding.UTF8);


            EggsoftWX.BLL.tab_DoTask_Services BLL_tab_DoTask_Services = new EggsoftWX.BLL.tab_DoTask_Services();
            EggsoftWX.Model.tab_DoTask_Services Model_tab_DoTask_Services = new EggsoftWX.Model.tab_DoTask_Services();
            Model_tab_DoTask_Services.InsertTime = DateTime.Now;
            Model_tab_DoTask_Services.TaskIfDone = false;
            Model_tab_DoTask_Services.TaskType = "SendEmail";
            Model_tab_DoTask_Services.TaskXML = strAndOther;
            BLL_tab_DoTask_Services.Add(Model_tab_DoTask_Services);

            return true;
            //Eggsoft_Public_CL.Pub.SendEmail_AddTask_AddTask
        }

        public static bool SendWeiXinMessage_AddTask(string strSendURL, string strJSON)
        {
            Eggsoft.Common.ClassWeiXin_Task myXML_Class = new Eggsoft.Common.ClassWeiXin_Task();
            myXML_Class.SendURL = strSendURL;
            myXML_Class.JSON = strJSON;
            string strAndOther = Eggsoft.Common.XmlHelper.XmlSerialize(myXML_Class, System.Text.Encoding.UTF8);


            EggsoftWX.BLL.tab_DoTask_Services BLL_tab_DoTask_Services = new EggsoftWX.BLL.tab_DoTask_Services();
            EggsoftWX.Model.tab_DoTask_Services Model_tab_DoTask_Services = new EggsoftWX.Model.tab_DoTask_Services();
            Model_tab_DoTask_Services.InsertTime = DateTime.Now;
            Model_tab_DoTask_Services.TaskIfDone = false;
            Model_tab_DoTask_Services.TaskType = "SendWeiXin";
            Model_tab_DoTask_Services.TaskXML = strAndOther;
            BLL_tab_DoTask_Services.Add(Model_tab_DoTask_Services);

            return true;
            //Eggsoft_Public_CL.Pub.SendEmail_AddTask_AddTask
        }

        //asp.net读取模板生成HTML
        public static string GetAppConfiug_WeiXin_Developmebt_URL()
        {
            string strUpLoadURL = "";

            string CacheKey = "GetAppConfiug_WeiXin_Developmebt_URL";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    strUpLoadURL = ConfigurationManager.AppSettings["WeiXin_Developmebt_URL"];
                    Eggsoft.Common.DataCache.SetCache(CacheKey, strUpLoadURL);// 写入缓存   
                }
                catch { }
            }
            else
            {
                strUpLoadURL = (string)objType;
            }



            return strUpLoadURL;

        }

        /// <summary>
        /// 得到Cookies  变更角色使用 。。。如果是代理 取ID Userid Parent  。如果不是 取ID+UserID+ParentID
        /// </summary>
        /// <returns></returns>
        public static string GetUserID_AgentID_ApplicationCheckName(int intUserID)///sesion 不能太长
        {
            string strUserID__ = "";

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            if (BLL_tab_ShopClient_Agent_.Exists("UserID=" + intUserID + "  and IsDeleted=0 and isnull(Empowered, 0) = 1"))
            {
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intUserID);
                strUserID__ = "A" + Model_tab_ShopClient_Agent_.ID.ToString() + "_" + intUserID.ToString() + "_" + Model_tab_ShopClient_Agent_.ParentID;
            }
            else
            {
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
                if (Model_tab_User != null)
                {
                    strUserID__ = "U" + Model_tab_User.ID.ToString() + "_" + intUserID.ToString() + "_" + Model_tab_User.ParentID;
                }
            }
            return strUserID__;
        }

        /// <summary>
        /// 得到Cookies名称  主要是调试使用
        /// </summary>
        /// <returns></returns>
        public static string GetAppConfiug_ApplicationCheckName()
        {
            string strApplicationCheckName = "";

            string CacheKey = "Application666CheckName__";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    strApplicationCheckName = ConfigurationManager.AppSettings["ApplicationCheckName"];
                    Eggsoft.Common.DataCache.SetCache(CacheKey, strApplicationCheckName);// 写入缓存   
                }
                catch { }
            }
            else
            {
                strApplicationCheckName = (string)objType;
            }
            return strApplicationCheckName;

        }


        public static string GetAppConfiugUpLoadResourceURL()
        {
            string strUpLoadURL = "";

            string CacheKey = "GetAppConfiugUplaodResourceURL";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    strUpLoadURL = ConfigurationManager.AppSettings["UpLoadResourceURL"];


                    //objType = Assembly.Load(path).CreateInstance(CacheKey);

                    //先不执行 便于调试
                    //System.Web.Configuration.CompilationSection ds = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
                    Eggsoft.Common.DataCache.SetCache(CacheKey, strUpLoadURL);// 写入缓存   
                }
                catch { }
            }
            else
            {
                strUpLoadURL = (string)objType;
            }
            //return (string)objType;



            return strUpLoadURL;

        }

        //asp.net读取模板生成HTML
        public static string GetAppConfiugUplaod()
        {
            string strUpLoadURL = "";

            string CacheKey = "GetAppConfiugUplaod";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];


                    //objType = Assembly.Load(path).CreateInstance(CacheKey);

                    //先不执行 便于调试
                    //System.Web.Configuration.CompilationSection ds = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
                    Eggsoft.Common.DataCache.SetCache(CacheKey, strUpLoadURL);// 写入缓存   
                }
                catch { }
            }
            else
            {
                strUpLoadURL = (string)objType;
            }
            //return (string)objType;



            return strUpLoadURL;

        }


        public static string GetAppConfiugServicesURL()
        {
            string strServicesURL = "";

            string CacheKey = "GetAppConfiugServicesURL";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    strServicesURL = ConfigurationManager.AppSettings["ServicesURL"];


                    //objType = Assembly.Load(path).CreateInstance(CacheKey);

                    //先不执行 便于调试
                    //System.Web.Configuration.CompilationSection ds = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
                    Eggsoft.Common.DataCache.SetCache(CacheKey, strServicesURL);// 写入缓存   
                }
                catch { }
            }
            else
            {
                strServicesURL = (string)objType;
            }
            //return (string)objType;



            return strServicesURL;

        }


        public static string GetAppConfiugServicesURL_HelpMachine()
        {
            string strServicesURL_HelpMachine = "";

            string CacheKey = "GetAppConfiugServicesURL_HelpMachine";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    strServicesURL_HelpMachine = ConfigurationManager.AppSettings["ServicesURL_HelpMachine"];


                    //objType = Assembly.Load(path).CreateInstance(CacheKey);

                    //先不执行 便于调试
                    //System.Web.Configuration.CompilationSection ds = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
                    Eggsoft.Common.DataCache.SetCache(CacheKey, strServicesURL_HelpMachine);// 写入缓存   
                }
                catch { }
            }
            else
            {
                strServicesURL_HelpMachine = (string)objType;
            }
            //return (string)objType;



            return strServicesURL_HelpMachine;

        }

        private static string InitEveryPageWeiXinShareLink(string strShareTitle, string strFirstImageURL, string strUrl, string strDes)
        {
            string cssFrageMent = "";
            try
            {


                if (Pub_SocialPlatform.Check_SocialPlatform() == "WeiXin")
                {
                    cssFrageMent = Pub.GetshareFriendString_WeiXin(strShareTitle, strFirstImageURL, strUrl, strDes);
                }
                else if (Pub_SocialPlatform.Check_SocialPlatform() == "YiXin")
                {
                    cssFrageMent = Pub.GetshareFriendString_YiXin(strShareTitle, strFirstImageURL, strUrl, strDes);

                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return cssFrageMent;

        }


        public static String Get_WeiXinRalationUserID_o2o_List_ID_FromDateBase(String strShopClientID_o2o)
        {

            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
            EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo();
            Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel("ID=" + strShopClientID_o2o + "");

            string strWeiXinRalationUserIDList = "";

            if (Model_tab_ShopClient_O2O_ShopInfo != null)
            {
                Eggsoft_Public_CL.XML__Class_Shop_O2o XML__Class_Shop_O2o;
                string strXML = Model_tab_ShopClient_O2O_ShopInfo.XML;
                if (string.IsNullOrEmpty(strXML) == false)
                {
                    XML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_O2o>(strXML, System.Text.Encoding.UTF8);
                    if (String.IsNullOrEmpty(XML__Class_Shop_O2o.WeiXinRalationUserIDList) == false)
                    {
                        strWeiXinRalationUserIDList = XML__Class_Shop_O2o.WeiXinRalationUserIDList;
                    }
                    else
                    {
                        strWeiXinRalationUserIDList = "0";
                    }
                }
            }
            else
            {

            }
            return strWeiXinRalationUserIDList;
        }


        public static String Get_WeiXinRalationUserIDList_ID_FromDateBase(String strShopClientID)
        {

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("ID=" + strShopClientID + "");

            string strWeiXinRalationUserIDList = "";

            if (Model_tab_ShopClient != null)
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client;
                string strXML = Model_tab_ShopClient.XML;
                if (string.IsNullOrEmpty(strXML) == false)
                {
                    XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                    if (String.IsNullOrEmpty(XML__Class_Shop_Client.WeiXinRalationUserIDList) == false)
                    {
                        strWeiXinRalationUserIDList = XML__Class_Shop_Client.WeiXinRalationUserIDList;
                    }
                    else
                    {
                        strWeiXinRalationUserIDList = "0";
                    }
                }
            }
            else
            {

            }
            return strWeiXinRalationUserIDList;
        }


        public static String[] GetstringWeiXinRalation_o2o_UserIDList(string strModeINCo2o_User_XML)
        {
            //Eggsoft.Common.debug_Log.Call_WriteLog("41=");

            String[] stringAcceptMSGList = { };
            if (String.IsNullOrEmpty(strModeINCo2o_User_XML) == false)
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("42=");

                XML__Class_Shop_O2o myFahuoDan_Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_O2o>(strModeINCo2o_User_XML, System.Text.Encoding.UTF8);
                //Eggsoft.Common.debug_Log.Call_WriteLog("43=");

                if (string.IsNullOrEmpty(myFahuoDan_Class_Shop_O2o.WeiXinRalationUserIDList) == false)
                {
                    //Eggsoft.Common.debug_Log.Call_WriteLog("44=");

                    stringAcceptMSGList = myFahuoDan_Class_Shop_O2o.WeiXinRalationUserIDList.Split(',');
                }
            }
            return stringAcceptMSGList;
        }

        public static String[] GetstringWeiXinRalationUserIDList(string strModeINC_User_XML)
        {
            String[] stringAcceptMSGList = { };
            if (String.IsNullOrEmpty(strModeINC_User_XML) == false)
            {
                XML__Class_Shop_Client myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(strModeINC_User_XML, System.Text.Encoding.UTF8);

                if (string.IsNullOrEmpty(myFahuoDan.WeiXinRalationUserIDList) == false)
                {
                    stringAcceptMSGList = myFahuoDan.WeiXinRalationUserIDList.Split(',');
                }
            }
            return stringAcceptMSGList;
        }

        public static String[] GetstringAcceptMSGList(string strModeINC_User_XML)
        {
            String[] stringAcceptMSGList = { };

            try
            {
                if (String.IsNullOrEmpty(strModeINC_User_XML) == false)
                {
                    XML__Class_Shop_Client myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(strModeINC_User_XML, System.Text.Encoding.UTF8);

                    if (string.IsNullOrEmpty(myFahuoDan.AcceptMsgList) == false)
                    {
                        stringAcceptMSGList = myFahuoDan.AcceptMsgList.Split(',');
                    }
                }
            }
            catch (Exception eee)
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("strModeINC_User_XML=" + strModeINC_User_XML);
                Eggsoft.Common.debug_Log.Call_WriteLog(eee);
            }
            return stringAcceptMSGList;
        }

        public static String[] GetstringPowerList(string strModeINC_User_XML)
        {
            String[] stringPowerList = { };
            if (String.IsNullOrEmpty(strModeINC_User_XML) == false)
            {
                XML__Class_Shop_Client myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(strModeINC_User_XML, System.Text.Encoding.UTF8);

                if (string.IsNullOrEmpty(myFahuoDan.stringPowerList) == false)
                {
                    stringPowerList = myFahuoDan.stringPowerList.Split(',');
                }
            }
            return stringPowerList;
        }



        public class ticket_Json
        {
            public String ticket { get; set; }
            public String expire_seconds { get; set; }
        }




        public static void insert_tab_User_Question(string intFromuserIDOrShopClientString, string intToUserIDORUserlisting, String strToContent, String strMessageType)
        {
            EggsoftWX.BLL.tab_User_Question BLL_tab_User_Question = new EggsoftWX.BLL.tab_User_Question();
            EggsoftWX.Model.tab_User_Question Model_tab_User_Question = new EggsoftWX.Model.tab_User_Question();

            Model_tab_User_Question.ToUserID = intToUserIDORUserlisting;
            Model_tab_User_Question.UserID = intFromuserIDOrShopClientString;
            Model_tab_User_Question.UserAsk = strToContent;
            Model_tab_User_Question.type = strMessageType;

            BLL_tab_User_Question.Add(Model_tab_User_Question);

        }



        public static String gePayChineseName(string strPayType)
        {
            String gePayChineseName = "已支付";
            if (strPayType == "Tenpay")
            {
                gePayChineseName = "微信支付";
            }
            else if (strPayType == "Alipay")
            {
                gePayChineseName = "支付宝支付";
            }
            else if (strPayType == "KQPay")
            {
                gePayChineseName = "快钱支付";
            }
            else if (strPayType == "WeiBaiPay")
            {
                gePayChineseName = "微店支付";
            }
            return gePayChineseName;
        }


        public static String GetShopClientNameFromShopClientID(int intShopClient_ID)
        {

            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(intShopClient_ID);

            string strShopClientName = my_Model_tab_ShopClient.ShopClientName.ToString();

            return strShopClientName;
        }
        public static String GetShopClientNameFromGoodID(int GoodID)
        {
            string strShopClientName = "";

            EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
            my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(GoodID);


            int intShopClient_ID = Convert.ToInt32(my_Model_tab_Goods.ShopClient_ID);

            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(intShopClient_ID);

            strShopClientName = my_Model_tab_ShopClient.ShopClientName.ToString();

            return strShopClientName;
        }
        /// <summary>
        /// 获取 店铺 ID 一般核对转发使用  前台 也需要传过来
        /// </summary>
        /// <param name="WeiKanJiaID"></param>
        /// <returns></returns>
        public static int GetShopClientIDFromWeiKanJiaID(int WeiKanJiaID)
        {

            EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
            EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();
            my_Model_tab_WeiKanJia = my_BLL_tab_WeiKanJia.GetModel(WeiKanJiaID);

            int intShopClient_ID = 0;
            if (my_Model_tab_WeiKanJia != null)
            {
                intShopClient_ID = Convert.ToInt32(my_Model_tab_WeiKanJia.ShopClientID);
            }

            return intShopClient_ID;
        }

        public static int GetShopClientIDFromGoodID(int GoodID)
        {

            EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
            my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(GoodID);

            int intShopClient_ID = 0;
            if (my_Model_tab_Goods != null)
            {
                intShopClient_ID = Convert.ToInt32(my_Model_tab_Goods.ShopClient_ID);
            }

            return intShopClient_ID;
        }


        public static int GetShopClientIDFromOrderID(int OrderID)
        {

            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
            my_Model_tab_Order = my_BLL_tab_Order.GetModel(OrderID);


            int intShopClient_ID = Convert.ToInt32(my_Model_tab_Order.ShopClient_ID);



            return intShopClient_ID;
        }

        public static int GetShopClientIDFromUserID(string UserID)
        {
            if (String.IsNullOrEmpty(UserID)) UserID = "0";
            if (UserID == "0") return 0;
            EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
            my_Model_tab_User = my_BLL_tab_User.GetModel(Int32.Parse(UserID));

            if (my_Model_tab_User == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(my_Model_tab_User.ShopClientID);
            }
        }

        /// <summary>
        /// 得到上级团队的ID
        /// </summary>
        /// <param name="Int32tab_ShopClient_Agent_"></param>
        /// <returns></returns>
        public static Int32 GetParentTeamIDFromTeamID(Int32 Int32tab_ShopClient_Agent_)
        {
            if (Int32tab_ShopClient_Agent_ == 0) return 0;
            EggsoftWX.BLL.tab_ShopClient_Agent_ my_BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ my_Model_tab_ShopClient_Agent_ = my_BLL_tab_ShopClient_Agent_.GetModel(Int32tab_ShopClient_Agent_);
            return my_Model_tab_ShopClient_Agent_.TeamParentID.toInt32();

        }


        public static Int32 GetUserIDFromTeamID(Int32 Int32tab_ShopClient_Agent_)
        {
            if (Int32tab_ShopClient_Agent_ == 0) return 0;

            EggsoftWX.BLL.tab_ShopClient_Agent_ my_BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ my_Model_tab_ShopClient_Agent_ = my_BLL_tab_ShopClient_Agent_.GetModel(Int32tab_ShopClient_Agent_);
            return my_Model_tab_ShopClient_Agent_.UserID.toInt32();

        }

        /// <summary>
        /// 取得默认的TeamID 所在团队的ID tab_ShopClient_Agent_表的主键
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static Int32 GetMyOrganizationTeamIDFromUserID(string UserID)
        {
            if (String.IsNullOrEmpty(UserID)) UserID = "0";
            if (UserID == "0") return 0;
            EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
            my_Model_tab_User = my_BLL_tab_User.GetModel(Int32.Parse(UserID));



            if (my_Model_tab_User == null)
            {
                //#region 运营中心默认配置编号（ConsumptionCapital_OperationCenterID）
                string strTeamID = Eggsoft_Public_CL.Pub.stringShowPower(my_Model_tab_User.ShopClientID.toString(), "ConsumptionCapital_OperationCenterID");///
                EggsoftWX.BLL.tab_ShopClient_Agent_ my_BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ my_Model_tab_ShopClient_Agent_ = my_BLL_tab_ShopClient_Agent_.GetModel("ShopClientID=@ShopClientID and ShopTeamID=@ShopTeamID and isnull(IsDeleted,0)=0", my_Model_tab_User.ShopClientID, strTeamID.toInt32());
                if (my_Model_tab_ShopClient_Agent_ != null)
                {
                    return my_Model_tab_ShopClient_Agent_.ID.toInt32();
                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("模拟异常 运营中心默认配置编号1", "模拟异常 运营中心默认配置编号", "程序报错UserID=" + UserID);
                    //throw new Exception("模拟异常 运营中心默认配置编号");
                    return 0;////说明出错了 
                }
            }
            else
            {
                ////检测 这个ID是否还有效
                EggsoftWX.BLL.tab_ShopClient_Agent_ my_BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolExisit = my_BLL_tab_ShopClient_Agent_.Exists("ShopClientID=@ShopClientID and ID=@ID and isnull(IsDeleted,0)=0 and AgentLevelSelect>0", my_Model_tab_User.ShopClientID, my_Model_tab_User.TeamID.toInt32());
                if (boolExisit)
                {
                    return Convert.ToInt32(my_Model_tab_User.TeamID);
                }
                else
                {
                    //#region 运营中心默认配置编号（ConsumptionCapital_OperationCenterID）
                    string strTeamID = Eggsoft_Public_CL.Pub.stringShowPower(my_Model_tab_User.ShopClientID.toString(), "ConsumptionCapital_OperationCenterID");///
                    EggsoftWX.Model.tab_ShopClient_Agent_ my_Model_tab_ShopClient_Agent_ = my_BLL_tab_ShopClient_Agent_.GetModel("ShopClientID=@ShopClientID and ShopTeamID=@ShopTeamID and isnull(IsDeleted,0)=0", my_Model_tab_User.ShopClientID, strTeamID.toInt32());
                    if (my_Model_tab_ShopClient_Agent_ != null)
                    {
                        return my_Model_tab_ShopClient_Agent_.ID.toInt32();
                    }
                    else
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog("模拟异常 运营中心默认配置编号2", "模拟异常 运营中心默认配置编号", "程序报错UserID=" + UserID);
                        //throw new Exception("模拟异常 运营中心默认配置编号");
                        return 0;////说明出错了 
                    }
                }
            }
        }

        public static String GetUserIDFromOpenID(string strOpenID)
        {
            string strReturnUserID = "0";
            if (String.IsNullOrEmpty(strOpenID) == false)
            {
                EggsoftWX.BLL.tab_User EggsoftWX_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                if (EggsoftWX_BLL_tab_User.Exists("OpenID='" + strOpenID + "'"))
                {
                    System.Data.DataTable myDataTable = EggsoftWX_BLL_tab_User.GetList("ID", "OpenID='" + strOpenID + "'").Tables[0];
                    strReturnUserID = myDataTable.Rows[0]["ID"].ToString();
                }
                else
                {
                    strReturnUserID = "0";
                }
            }
            return strReturnUserID;
        }


        public static String GetEmailFromShopClientID(int intShopClientID)
        {
            string strGetEmailFromShopClientID = "";


            try
            {
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
                //string strOpenID = Model_tab_ShopClient.OpenID;

                strGetEmailFromShopClientID = Model_tab_ShopClient.Email;
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            { }

            return strGetEmailFromShopClientID;
        }



        public static String Get_HeadImage(string strUserID)
        {
            string strGet_OtherHeadImage = "/UpLoad/HeadImage/DefaultHead.jpg";
            if ((String.IsNullOrEmpty(strUserID) == false) && (strUserID != "0"))
            {
                EggsoftWX.BLL.tab_User EggsoftWX_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                System.Data.DataTable myDataTable = EggsoftWX_BLL_tab_User.GetList("ID=" + strUserID).Tables[0];
                if (myDataTable.Rows.Count > 0)
                {
                    if (String.IsNullOrEmpty(myDataTable.Rows[0]["HeadImageUrl"].ToString()) == false)
                    {
                        strGet_OtherHeadImage = myDataTable.Rows[0]["HeadImageUrl"].ToString();
                    }
                }
            }
            return strGet_OtherHeadImage;
        }

        /// <summary>
        /// errorCorrectIcon LMQH   空为M
        /// </summary>
        /// <param name="strLinkURL"></param>
        /// <param name="strUplaodPath"></param>
        /// <param name="errorCorrectIcon">LMQH   空为M</param>
        /// <returns></returns>
        public static String Get_Remote_creatQRCodeImage(string strLinkURL, string strUplaodPath, string errorCorrectIcon)
        {
            string strcreatQRCodeImage = "";
            string strFileName = strLinkURL.Replace(":", "");
            strFileName = strFileName.Replace("/", "");
            strFileName = strFileName.Replace("?", "");
            strFileName = strFileName.Replace("&", "");
            strFileName = strFileName.Replace("#", "");
            strFileName = strFileName.Replace(".", "");
            strFileName = strFileName.Replace("=", "");
            String ls_fileName = strFileName + "_" + errorCorrectIcon + ".jpg";
            strcreatQRCodeImage = strUplaodPath + "" + ls_fileName;
            string strRemoteFile = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "" + strcreatQRCodeImage;
            if (Eggsoft.Common.FileFolder.RemoteFileExists(strRemoteFile) == false)//不存在的话 请求一下
            {
                string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/QRCodeImages.asmx";
                string[] args = new string[3];
                args[0] = strUplaodPath + "/" + ls_fileName;
                args[1] = strLinkURL;
                args[2] = errorCorrectIcon;//LMQH   空为M
                WebServiceHelper.WsCaller.InvokeWebService(url, "APPCODE_getImage_QRCodeImages", args);

            }
            //Eggsoft.Common.debug_Log.Call_WriteLog("2222");
            return strRemoteFile;
        }


        public static String Get_MyDisk_HeadImage(int intUserID)
        {
            //本地文件
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
            if (Model_tab_User != null)
            {
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));
                string strHead_Parent_Image = Model_tab_ShopClient.UpLoadPath + "/HeadImage/User" + Eggsoft.Common.StringNum.Add000000Num(intUserID, 6) + ".jpg";





                //远程文件
                string strGet_OtherHeadImage = "";

                EggsoftWX.BLL.tab_User EggsoftWX_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                System.Data.DataTable myDataTable = EggsoftWX_BLL_tab_User.GetList("ID=" + intUserID).Tables[0];

                if (String.IsNullOrEmpty(myDataTable.Rows[0]["HeadImageUrl"].ToString()) == false)
                {
                    strGet_OtherHeadImage = myDataTable.Rows[0]["HeadImageUrl"].ToString();
                }
                if (Eggsoft.Common.FileFolder.RemoteFileExists(Pub.GetAppConfiugUplaod() + strHead_Parent_Image) == false)//不存在的话 请求一下
                {
                    if (Eggsoft.Common.FileFolder.RemoteFileExists(strGet_OtherHeadImage))
                    {

                        Eggsoft_Public_CL.GoodP.DownLoadFile_Service_ScaleBMP_makeHeadImage_User_path(strHead_Parent_Image);
                        Eggsoft_Public_CL.GoodP.DownLoadFile_Service_ScaleBMP(strGet_OtherHeadImage, strHead_Parent_Image, 100, 100, "HW");
                        Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(Convert.ToInt32(Model_tab_User.ShopClientID));
                    }
                }



                if (Eggsoft.Common.FileFolder.RemoteFileExists(Pub.GetAppConfiugUplaod() + strHead_Parent_Image))
                {
                    return strHead_Parent_Image;
                }
                else
                {
                    return "/UpLoad/HeadImage/DefaultHead.jpg";
                }
            }
            else
            {
                return "/UpLoad/HeadImage/DefaultHead.jpg";
            }
        }


        public static int GetMyShopUserID(string strUserID)
        {
            int intUserID = 0;
            int.TryParse(strUserID, out intUserID);

            EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(intUserID);



            if (Model_tab_User != null)
            {
                return Convert.ToInt32(Model_tab_User.ShopUserID);
            }
            else
            {
                return 0;
            }
            ////string strWeBuyNumberOrPicName = string.IsNullOrEmpty(strGetNickName) ? ("微店号：" + strUserID) : strGetNickName;

            //return strWeBuyNumberOrPicName;
        }
        /// <summary>
        /// 根据微店号查找UserID
        /// </summary>
        /// <param name="intShopUserID"></param>
        /// <param name="intShopClientID"></param>
        /// <returns></returns>
        public static int GetMyUserIDFromShopUserID(int intShopUserID, int intShopClientID)
        {
            EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", intShopUserID, intShopClientID);

            if (Model_tab_User != null)
            {
                return Convert.ToInt32(Model_tab_User.ID);
            }
            else
            {
                return 0;
            }
        }

        public static String GetShopLogoImage(string strShopClientID)
        {
            EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();
            tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strShopClientID));
            string strXML = tab_ShopClient_Model.XML;
            Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
            string strImageUrl = ConfigurationManager.AppSettings["UpLoadURL"] + XML__Class_Shop_Client.ShopLogoImage;

            return strImageUrl;
        }
        public static String GetNickName(string strUserID)
        {
            int intuserID = 0;
            int.TryParse(strUserID, out intuserID);

            string strWeBuyNumberOrPicName = "";
            string strGetNickName = "";
            if (intuserID > 0)
            {
                EggsoftWX.BLL.tab_User EggsoftWX_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                System.Data.DataTable myDataTable = EggsoftWX_BLL_tab_User.GetList("NickName", "ID=" + intuserID).Tables[0];
                if (myDataTable.Rows.Count > 0) strGetNickName = myDataTable.Rows[0]["NickName"].ToString();
                strWeBuyNumberOrPicName = string.IsNullOrEmpty(strGetNickName) ? ("微店号：" + Eggsoft_Public_CL.Pub.GetMyShopUserID(strUserID)) : strGetNickName;
            }
            else
            {
                strWeBuyNumberOrPicName = "直接访问";
            }
            //string strWeBuyNumberOrPicName = string.IsNullOrEmpty(strGetNickName) ? ("微店号：" + strUserID) : strGetNickName;

            return strWeBuyNumberOrPicName;
        }

        public static String GetshareFriendString_WeiXin(String StrshareTitle, String StrImgUrl, String StrUrl, String StrDescContent)
        {
            String strGetshareFriendString = "";
            strGetshareFriendString += "	<script>					\n";
            strGetshareFriendString += "	var	imgUrl = '" + StrImgUrl + "';										\n";
            strGetshareFriendString += "	var	lineLink = '" + StrUrl + "';										\n";
            strGetshareFriendString += "	var	descContent = \"" + StrDescContent.Trim() + "\";\n";
            strGetshareFriendString += "	var	shareTitle = '" + StrshareTitle.Trim() + "';										\n";
            strGetshareFriendString += "	var	appid = '';										\n";
            strGetshareFriendString += "												\n";
            strGetshareFriendString += "	function shareFriend() {										\n";
            strGetshareFriendString += "		WeixinJSBridge.invoke('sendAppMessage',{\n";
            strGetshareFriendString += "		\"appid\": appid,										\n";
            strGetshareFriendString += "		\"img_url\": imgUrl,										\n";
            strGetshareFriendString += "		\"img_width\": \"640\",										\n";
            strGetshareFriendString += "		\"img_height\": \"640\",										\n";
            strGetshareFriendString += "		\"link\": lineLink,										\n";
            strGetshareFriendString += "		\"desc\": descContent,										\n";
            strGetshareFriendString += "		\"title\": shareTitle										\n";
            strGetshareFriendString += "		}, function(res) {										\n";
            strGetshareFriendString += "		_report('send_msg', res.err_msg);										\n";
            strGetshareFriendString += "		})										\n";
            strGetshareFriendString += "	}											\n";


            strGetshareFriendString += "	function weixinShareTimeline() {										\n";
            strGetshareFriendString += "		WeixinJSBridge.invoke('shareTimeline',{\n";
            strGetshareFriendString += "		\"appid\": appid,										\n";
            strGetshareFriendString += "		\"img_url\": imgUrl,										\n";
            strGetshareFriendString += "		\"img_width\": \"640\",										\n";
            strGetshareFriendString += "		\"img_height\": \"640\",										\n";
            strGetshareFriendString += "		\"link\": lineLink,										\n";
            strGetshareFriendString += "		\"desc\": descContent,										\n";
            strGetshareFriendString += "		\"title\": shareTitle										\n";
            strGetshareFriendString += "		}, function(res) {										\n";
            strGetshareFriendString += "		_report('timeline', res.err_msg);										\n";
            strGetshareFriendString += "		})										\n";
            strGetshareFriendString += "	}											\n";




            strGetshareFriendString += "												\n";
            strGetshareFriendString += "	function shareWeibo() {										\n";
            strGetshareFriendString += "		WeixinJSBridge.invoke('shareWeibo',{										\n";
            strGetshareFriendString += "		\"content\": descContent,										\n";
            strGetshareFriendString += "		\"url\": lineLink,										\n";
            strGetshareFriendString += "		}, function(res) {										\n";
            strGetshareFriendString += "		_report('weibo', res.err_msg);										\n";
            strGetshareFriendString += "		});										\n";
            strGetshareFriendString += "	}											\n";
            strGetshareFriendString += "	//	当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。										\n";
            strGetshareFriendString += "	document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {										\n";
            strGetshareFriendString += "												\n";
            strGetshareFriendString += "		// 发送给好友										\n";
            strGetshareFriendString += "		WeixinJSBridge.on('menu:share:appmessage', function(argv){										\n";
            strGetshareFriendString += "		shareFriend();										\n";
            strGetshareFriendString += "		});										\n";


            strGetshareFriendString += "		// 分享到朋友圈  										\n";
            strGetshareFriendString += "		WeixinJSBridge.on('menu:share:timeline', function(argv){										\n";
            strGetshareFriendString += "		weixinShareTimeline();										\n";
            strGetshareFriendString += "		});										\n";


            strGetshareFriendString += "												\n";
            strGetshareFriendString += "												\n";
            strGetshareFriendString += "		}, false);										\n";
            strGetshareFriendString += "	</script>										\n";

            return strGetshareFriendString;
        }


        public static String GetshareFriendString_YiXin(String StrshareTitle, String StrImgUrl, String StrUrl, String StrDescContent)
        {
            String strGetshareFriendString = "";
            strGetshareFriendString += "	<script>					\n";

            strGetshareFriendString += "	var	imgUrl = '" + StrImgUrl + "';										\n";
            strGetshareFriendString += "	var	lineLink = '" + StrUrl + "';										\n";
            strGetshareFriendString += "	var	descContent = \"" + StrDescContent.Trim() + "\";\n";
            strGetshareFriendString += "	var	shareTitle = '" + StrshareTitle.Trim() + "';										\n";
            strGetshareFriendString += "	var	appid = '';										\n";
            strGetshareFriendString += "												\n";


            strGetshareFriendString += "        window.shareData = {\n";
            strGetshareFriendString += "    \"imgUrl\": imgUrl,\n";
            strGetshareFriendString += "    \"tImgUrl\": imgUrl,\n";
            strGetshareFriendString += "    \"fImgUrl\": imgUrl,\n";
            strGetshareFriendString += "    \"wImgUrl\": imgUrl,\n";
            strGetshareFriendString += "    \"timeLineLink\": lineLink,\n";
            strGetshareFriendString += "    \"sendFriendLink\":lineLink,\n";
            strGetshareFriendString += "    \"weiboLink\": lineLink,\n";
            strGetshareFriendString += "    \"tTitle\": shareTitle,\n";
            strGetshareFriendString += "    \"tContent\": descContent,\n";
            strGetshareFriendString += "    \"fTitle\": shareTitle,\n";
            strGetshareFriendString += "    \"fContent\": descContent,\n";
            strGetshareFriendString += "    \"wContent\": descContent \n";
            strGetshareFriendString += "};\n";

            strGetshareFriendString += "       // \\分享给好友\n";
            strGetshareFriendString += "        YixinJSBridge.on('menu:share:appmessage', function (argv) {\n";
            strGetshareFriendString += "YixinJSBridge.invoke('sendAppMessage', { \n";
            strGetshareFriendString += "    \"img_url\": window.shareData.imgUrl,\n";
            strGetshareFriendString += "    \"img_width\": \"640\",\n";
            strGetshareFriendString += "    \"img_height\": \"640\",\n";
            strGetshareFriendString += "    \"link\": window.shareData.sendFriendLink,\n";
            strGetshareFriendString += "    \"desc\": window.shareData.fContent,\n";
            strGetshareFriendString += "   \"title\": window.shareData.fTitle\n";
            strGetshareFriendString += "}, function (res) {\n";
            strGetshareFriendString += "    //\\不用处理，客户端会有分享结果提示\n";
            strGetshareFriendString += "}); \n";
            strGetshareFriendString += "});\n";

            strGetshareFriendString += "        //\\分享到朋友圈\n";
            strGetshareFriendString += "        YixinJSBridge.on('menu:share:timeline', function (argv) {\n";
            strGetshareFriendString += "YixinJSBridge.invoke('shareTimeline', {\n";
            strGetshareFriendString += "    \"img_url\": window.shareData.imgUrl,\n";
            strGetshareFriendString += "    \"img_width\": \"640\",\n";
            strGetshareFriendString += "    \"img_height\": \"640\",\n";
            strGetshareFriendString += "    \"link\": window.shareData.timeLineLink,\n";
            strGetshareFriendString += "    \"desc\": window.shareData.tContent,\n";
            strGetshareFriendString += "    \"title\": window.shareData.tTitle\n";
            strGetshareFriendString += "}, function (res) {\n";
            strGetshareFriendString += "    //\\不用处理，客户端会有分享结果提示\n";
            strGetshareFriendString += "});\n";
            strGetshareFriendString += "});\n";

            strGetshareFriendString += "       //\\分享到微博\n";
            strGetshareFriendString += "        YixinJSBridge.on('menu:share:weibo', function (argv) {\n";
            strGetshareFriendString += "YixinJSBridge.invoke('shareWeibo', {\n";
            strGetshareFriendString += "    \"content\": window.shareData.wContent,\n";
            strGetshareFriendString += "    \"url\": window.shareData.weiboLink,\n";
            strGetshareFriendString += "}, function (res) {\n";
            strGetshareFriendString += "    //\\不用处理，客户端会有分享结果提示\n";
            strGetshareFriendString += "});\n";
            strGetshareFriendString += "});\n";


            //strGetshareFriendString += "												\n";
            //strGetshareFriendString += "												\n";
            //strGetshareFriendString += "		}, false);										\n";
            strGetshareFriendString += "	</script>										\n";

            return strGetshareFriendString;
        }


        public static string getPubPromotePrice_ZheKou(decimal PromotePrice, decimal Price)
        {
            Decimal myZhe = (Decimal)0;
            if (Price == 0)
            {
                myZhe = 0;
            }
            else
            {
                myZhe = PromotePrice / Price;
            }


            int number = Decimal.ToInt32(myZhe * 100);

            string strUpart = number.ToString();

            if (number == 100)
            {
                strUpart = "一口价不打";
            }
            else
            {
                strUpart = strUpart.Replace("0", "").Replace("1", "壹").Replace("2", "贰").Replace("3", "叁").Replace("4", "肆").Replace("5", "伍").Replace("6", "陆").Replace("7", "柒").Replace("8", "捌").Replace("9", "玖");
            }
            return strUpart;
        }



        public static string getBankPubMoney(decimal MyBankDecimal)
        {
            Decimal MyDecimal = Math.Floor((Decimal)MyBankDecimal * 100) / 100;

            string strMoney = MyDecimal.ToString("#0.00");

            if (strMoney.Substring(strMoney.Length - 3) == ".00")
            {
                strMoney = strMoney.Substring(0, strMoney.Length - 3);
            }
            return strMoney;
        }

        public static string getGameTypeShowName(int intGameType)
        {
            string strMoneyType = "商城购物券";
            switch (intGameType)
            {
                case 1:
                    strMoneyType = "商城现金";
                    break;
                case 2:
                    strMoneyType = "商城购物券";
                    break;
                case 3:
                    strMoneyType = "微信红包";
                    break;
                case 4:
                    strMoneyType = "微信零钱";
                    break;
                default:
                    strMoneyType = "商城购物券";
                    break;
            }
            return strMoneyType;
        }
        public static string getPubMoney(decimal MyDecimal)
        {

            string strMoney = MyDecimal.ToString("#0.00");

            if (strMoney.Substring(strMoney.Length - 3) == ".00")
            {
                strMoney = strMoney.Substring(0, strMoney.Length - 3);
            }
            return strMoney;
        }

        /// <summary>
        /// 商品 发货提示  。主要用于微团购 不满足条件 不要发货
        /// </summary>
        /// <param name="strGoodType"></param>
        /// <param name="GoodTypeId"></param>
        /// <param name="GoodTypeIdBuyInfo"></param>
        /// <returns></returns>
        public static string getPubTuanGouDescToAdministrator(string strGoodType, string GoodTypeId, string GoodTypeIdBuyInfo, string strOrderdetailsID)
        {
            string strReturn = "";

            if (strGoodType == "2")//团购
            {
                int intGoodTypeIdBuyInfo = 0;
                int.TryParse(GoodTypeIdBuyInfo, out intGoodTypeIdBuyInfo);
                if (intGoodTypeIdBuyInfo > 0)
                {
                    EggsoftWX.BLL.tab_TuanGou_Number BLL_tab_TuanGou_Number = new EggsoftWX.BLL.tab_TuanGou_Number();
                    EggsoftWX.Model.tab_TuanGou_Number Model_tab_TuanGou_Number = BLL_tab_TuanGou_Number.GetModel(intGoodTypeIdBuyInfo);

                    if (Model_tab_TuanGou_Number.Efficacy == false)
                    {
                        strReturn = "(<span style=\"Color:#8A00FF\">团购已失效,可以做退款处理</span>)";
                    }
                    else
                    {
                        if (Model_tab_TuanGou_Number.IFFinshedCurMemberShip == true)
                        {
                            strReturn = "(<span style=\"Color:red\">团购已满足条件,可以发货</span>)";
                        }
                        else
                        {
                            strReturn = "(<span style=\"Color:FF8400\">组团尚未成功,建议暂不发货</span>)";
                        }
                    }
                }
            }
            else if (strGoodType == "3")////众筹 
            {
                int intSupportID = 0;
                int.TryParse(GoodTypeIdBuyInfo, out intSupportID);
                if (intSupportID > 0)
                {
                    EggsoftWX.BLL.tab_ZC_01Product BLL_tab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                    EggsoftWX.Model.tab_ZC_01Product Model_tab_ZC_01Product = BLL_tab_ZC_01Product.GetModel(Int32.Parse(GoodTypeId));
                    EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods Model_tab_Goods = BLL_tab_Goods.GetModel(Convert.ToInt32(Model_tab_ZC_01Product.SourceGoodID));

                    EggsoftWX.BLL.tab_ZC_01Product_Support_GetBonus BLL_tab_ZC_01Product_Support_GetBonus = new EggsoftWX.BLL.tab_ZC_01Product_Support_GetBonus();



                    EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(intSupportID);
                    string strDesc = "众筹" + Model_tab_ZC_01Product_Support.Name + "," + Model_tab_Goods.Name;


                    EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel(Int32.Parse(strOrderdetailsID));

                    EggsoftWX.BLL.tab_ZC_01Product_PartnerList BLL_tab_ZC_01Product_PartnerList = new EggsoftWX.BLL.tab_ZC_01Product_PartnerList();
                    System.Data.DataTable DataTable_PartnerList = BLL_tab_ZC_01Product_PartnerList.GetList("OrderID=" + Model_tab_Orderdetails.OrderID + " and ShopClientID=" + Model_tab_Orderdetails.ShopClient_ID + " and SupportID=" + intSupportID).Tables[0];

                    if (DataTable_PartnerList.Rows.Count > 0)////测试数据  被删除了
                    {
                        for (int i = 0; i < DataTable_PartnerList.Rows.Count; i++)
                        {
                            string strCreateTime = DataTable_PartnerList.Rows[i]["CreateTime"].ToString();
                            string strPartnerListID = DataTable_PartnerList.Rows[i]["ID"].ToString();
                            string strIsCanSendGoods = DataTable_PartnerList.Rows[i]["IsCanSendGoods"].ToString();
                            string strGetBonusID = DataTable_PartnerList.Rows[i]["GetBonusID"].ToString();
                            string strZCBuysSay = DataTable_PartnerList.Rows[i]["ZCBuysSay"].ToString();
                            string strCredentials = DataTable_PartnerList.Rows[i]["Credentials"].ToString();

                            int intGetBonusID = 0;
                            int.TryParse(strGetBonusID, out intGetBonusID);
                            int intCredentials = 0;
                            int.TryParse(strCredentials, out intCredentials);

                            string strPartnerListNumber = "抽奖编号" + DateTime.Parse(strCreateTime).ToString("yyyyMM") + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strPartnerListID), 3);
                            strPartnerListNumber += ",您的抽奖序号" + (intCredentials + 1);
                            string strReturnDesc = "";

                            EggsoftWX.Model.tab_ZC_01Product_Support_GetBonus Model_tab_ZC_01Product_Support_GetBonus = BLL_tab_ZC_01Product_Support_GetBonus.GetModel(intGetBonusID);

                            if (Model_tab_ZC_01Product_Support.SupportWay == 0)
                            {
                                strReturnDesc = "(<span style=\"Color:red\">众筹,可以发货(" + strDesc + ")</span>)";
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 3)
                            {
                                strReturnDesc = "(<span style=\"Color:#550088\">无偿支持,不需回报,无需发货(" + strDesc + ")</span>)";
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 4)
                            {
                                strReturnDesc = "(<span style=\"Color:#550088\">股权类众筹,后期回报,无需发货(" + strDesc + ")</span>)";
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 1 && strIsCanSendGoods != "True" && (intGetBonusID == 0))
                            {
                                strReturnDesc = "(<span style=\"Color:#227700\">双色球待抽奖,建议暂不发货(" + strDesc + ")</span>)";
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 1 && strIsCanSendGoods == "True" && intGetBonusID > 0)
                            {
                                strReturnDesc = "(<span style=\"Color:red\">双色球中奖,可以发货(" + strDesc + ")</span>)" + Model_tab_ZC_01Product_Support_GetBonus.BonusContent;
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 1 && strIsCanSendGoods != "True" && intGetBonusID > 0)
                            {
                                strReturnDesc = "(<span style=\"Color:#000088\">双色球未中奖,无需发货,下次好运(" + strDesc + ")</span>)" + Model_tab_ZC_01Product_Support_GetBonus.BonusContent;
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 2 && strIsCanSendGoods != "True" && (intGetBonusID == 0))
                            {
                                strReturnDesc = "(<span style=\"Color:#227700\">3D待抽奖,建议暂不发货(" + strDesc + ")</span>)";
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 2 && strIsCanSendGoods == "True" && intGetBonusID > 0)
                            {
                                strReturnDesc = "(<span style=\"Color:red\">3D中奖,可以发货(" + strDesc + ")</span>)" + Model_tab_ZC_01Product_Support_GetBonus.BonusContent;
                            }
                            else if (Model_tab_ZC_01Product_Support.SupportWay == 2 && strIsCanSendGoods != "True" && intGetBonusID > 0)
                            {
                                strReturnDesc = "(<span style=\"Color:#000088\">3D未中奖,无需发货,下次好运(" + strDesc + ")</span>)" + Model_tab_ZC_01Product_Support_GetBonus.BonusContent;
                            }
                            strReturnDesc += "<br />留言:" + strZCBuysSay;

                            strReturn += "<br />" + strPartnerListNumber + "<br />" + strReturnDesc + "<br />";
                        }






                        //if (Model_tab_ZC_01Product_Support.SalseGoodsType == 0)
                        //{
                        //    if ((Model_tab_ZC_01Product_PartnerList.IsCanSendGoods == true) && Model_tab_ZC_01Product_PartnerList.GetBonusID > 0)
                        //    {
                        //        strReturn = "(<span style=\"Color:red\">已中奖,可以发货(" + strDesc + ")</span>)";
                        //    }
                        //    else if (Model_tab_ZC_01Product_PartnerList.IsCanSendGoods == true)
                        //    {
                        //        strReturn = "(<span style=\"Color:red\">众筹,可以发货(" + strDesc + ")</span>)";
                        //    }
                        //    if (Model_tab_ZC_01Product_PartnerList.IsCanSendGoods == false && Model_tab_ZC_01Product_PartnerList.GetBonusID > 0)
                        //    {
                        //        strReturn = "(<span style=\"Color:#8A00FF\">未中奖,建议免发货(" + strDesc + ")</span>)";
                        //    }
                        //    else if (Model_tab_ZC_01Product_PartnerList.IsCanSendGoods == false)
                        //    {
                        //        strReturn = "(<span style=\"Color:#8A00FF\">未开奖,建议等待开奖(" + strDesc + ")</span>)";
                        //    }
                        //}
                        //else if (Model_tab_ZC_01Product_Support.SalseGoodsType == 1)
                        //{
                        //    strReturn = "(<span style=\"Color:#8A00FF\">无偿支持，不需回报，无需发货(" + strDesc + ")</span>)";
                        //}
                        //else if (Model_tab_ZC_01Product_Support.SalseGoodsType == 2)
                        //{
                        //    strReturn = "(<span style=\"Color:#8A00FF\">股权类众筹，后期回报，无需发货(" + strDesc + ")</span>)";
                        //}
                    }
                }
            }
            return strReturn;
        }
        /// <summary>
        /// 扫描代理二维码奖励现金  扫描代理二维码奖励购物券
        /// </summary>
        /// <param name="intUserID"></param>
        public static void SendYouhuiquan100DollorScanAgentErWeiMa(int intUserID)
        {
            try
            {
                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(intUserID);

                int intShopClientID = Convert.ToInt32(Model_tab_User.ShopClientID);

                string strScanAgentErWeiMaMoney_Money = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "ScanAgentErWeiMaMoney_Money");///扫描代理二维码奖励现金
                string strScanAgentErWeiMaGouWuQuan_GouWuQuan = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "ScanAgentErWeiMaGouWuQuan_GouWuQuan");///扫描代理二维码奖励购物券
                Decimal DecimalScanAgentErWeiMaMoney_Money = 0;
                Decimal DecimalScanAgentErWeiMaGouWuQuan_GouWuQuan = 0;

                Decimal.TryParse(strScanAgentErWeiMaMoney_Money, out DecimalScanAgentErWeiMaMoney_Money);
                Decimal.TryParse(strScanAgentErWeiMaGouWuQuan_GouWuQuan, out DecimalScanAgentErWeiMaGouWuQuan_GouWuQuan);

                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                bool ifSendScanAgentErWeiMaMoney_Money = BLL_tab_TotalCredits_Consume_Or_Recharge.Exists("UserID=" + Model_tab_User.ID + " and Bool_ConsumeOrRecharge=1 and ConsumeTypeOrRecharge like '扫描代理赠送%'");
                if (Decimal.Round(DecimalScanAgentErWeiMaMoney_Money, 2) > 0 && !ifSendScanAgentErWeiMaMoney_Money)
                {
                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 41;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalScanAgentErWeiMaMoney_Money;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "扫描代理赠送" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalScanAgentErWeiMaMoney_Money) + "元";
                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = intUserID;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                    int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                    #region 增加账户余额未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加账户余额未处理信息 
                    Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, "扫描代理已赠送现金" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalScanAgentErWeiMaMoney_Money) + "元");
                }

                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                bool ifSendScanAgentErWeiMaGouWuQuan_GouWuQuan = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Exists("UserID=" + Model_tab_User.ID + " and Bool_ConsumeOrRecharge=1 and ConsumeTypeOrRecharge like '扫描代理赠送%'");
                if (Decimal.Round(DecimalScanAgentErWeiMaGouWuQuan_GouWuQuan, 2) > 0 && !ifSendScanAgentErWeiMaGouWuQuan_GouWuQuan)
                {
                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalScanAgentErWeiMaGouWuQuan_GouWuQuan;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "扫描代理赠送" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalScanAgentErWeiMaGouWuQuan_GouWuQuan) + "元";
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = intUserID;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                    int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                    #region 增加购物券未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加购物券未处理信息 

                    Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, "扫描代理已赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClientID.ToString()) + Eggsoft_Public_CL.Pub.getPubMoney(DecimalScanAgentErWeiMaGouWuQuan_GouWuQuan) + "元");
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }

        public static void SendYouhuiquan100Dollor(int intUserID)
        {
            try
            {
                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(intUserID);

                int intShopClientID = Convert.ToInt32(Model_tab_User.ShopClientID);
                EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + intShopClientID + "");

                if (Model_tab_ShopClient_ShopPar != null)
                {
                    if (Decimal.Round(Model_tab_ShopClient_ShopPar.SubscribeGiveVouchers.toDecimal(), 2) > 0)
                    {
                        EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Model_tab_ShopClient_ShopPar.SubscribeGiveVouchers;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "关注赠送";
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                        int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                        #region 增加购物券未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                        Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加购物券未处理信息 

                        Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, "已赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClientID.ToString()) + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_ShopPar.SubscribeGiveVouchers.toDecimal()) + "元");
                    }

                    if (Decimal.Round(Model_tab_ShopClient_ShopPar.SubscribeGiveMoney.toDecimal(), 2) > 0)
                    {
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_tab_ShopClient_ShopPar.SubscribeGiveMoney;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "关注赠送";
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 42;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                        int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                        #region 增加账户余额未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                        Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加账户余额未处理信息 

                        Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, "已赠送现金" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_ShopPar.SubscribeGiveMoney.toDecimal()) + "元");
                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }


        public static void SendYouhuiquan100Dollor_ToFirstVisit(int intUserID)
        {
            try
            {
                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(intUserID);

                int intShopClientID = Convert.ToInt32(Model_tab_User.ShopClientID);
                EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + intShopClientID + "");

                if (Model_tab_ShopClient_ShopPar != null)
                {
                    if (Decimal.Round(Model_tab_ShopClient_ShopPar.GouWuQuan_FirstVisitShop.toDecimal(), 2) > 0)
                    {
                        EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Model_tab_ShopClient_ShopPar.GouWuQuan_FirstVisitShop;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "首次访问赠送";
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                        int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                        #region 增加购物券未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                        Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加购物券未处理信息  

                        if (Convert.ToBoolean(Model_tab_User.Subscribe))
                        {
                            Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, "已赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(Model_tab_User.ShopClientID.ToString()) + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_ShopPar.GouWuQuan_FirstVisitShop.toDecimal()) + "元");
                        }
                        else
                        {
                            Pub_GetOpenID_And_.SendTextTipInfoMessage(intUserID, "首次访问微店用户,已赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(Model_tab_User.ShopClientID.ToString()) + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_ShopPar.GouWuQuan_FirstVisitShop.toDecimal()) + "元");
                        }
                    }

                    if (Decimal.Round(Model_tab_ShopClient_ShopPar.Money_FirstVisitShop.toDecimal(), 2) > 0)
                    {
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_tab_ShopClient_ShopPar.Money_FirstVisitShop;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "首次访问赠送";
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 43;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                        int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                        #region 增加账户余额未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                        Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加账户余额未处理信息 

                        if (Convert.ToBoolean(Model_tab_User.Subscribe))
                        {
                            Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, "已赠送现金" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_ShopPar.Money_FirstVisitShop.toDecimal()) + "元");
                        }
                        else
                        {
                            Pub_GetOpenID_And_.SendTextTipInfoMessage(intUserID, "首次访问微店用户,已赠送现金" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_ShopPar.Money_FirstVisitShop.toDecimal()) + "元");
                        }
                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }


        static public string geLoginShow()
        {
            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

            if (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" || strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_AdminShopClient")
            {
                return "系统管理员";
            }
            else
            {
                return strwebuy8_ClientAdmin_Users_ClientUserAccount;
            }
        }
    }
}