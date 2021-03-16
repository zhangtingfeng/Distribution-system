using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Default_Get_NestShopNameListByo2o 的摘要说明
    /// </summary>
    public class Default_Get_NestShopNameListByo2o : IHttpHandler
    {

        public class ArrayList_Shop_Distance
        {
            public string strShop_ID;
            public double DecimalDistance;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strpub_UserID = context.Request.QueryString["strUser"];
            string strstrNearestShopNameList = "";
            try
            {
                //

                Eggsoft.Common.debug_Log.Call_WriteLog("Default_Get_NestShopNameListByo2o strpub_UserID=" + strpub_UserID, "Get_NestShopName");

                int pub_Int_UserID = 0;
                int.TryParse(strpub_UserID, out pub_Int_UserID);

                int intpub_Int_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_UserID);

                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo();

                System.Data.DataTable ShopData_DataTable = BLL_tab_ShopClient_O2O_ShopInfo.GetList("ShopClientID=" + intpub_Int_ShopClientID).Tables[0];

                string strUserBaiDuAdress = ""; double doubleBaiDulng = 0; double doubleBaiDulat = 0; Eggsoft_Public_CL.Pub.Get_o2o_NestUserID__(pub_Int_UserID, out doubleBaiDulng, out doubleBaiDulat, out strUserBaiDuAdress);

                System.Collections.Generic.List<ArrayList_Shop_Distance> mList = new System.Collections.Generic.List<ArrayList_Shop_Distance>();




                for (int inti = 0; inti < ShopData_DataTable.Rows.Count; inti++)
                {
                    string strShop_ID = ShopData_DataTable.Rows[inti]["ID"].ToString();
                    string str_Shop_BaiDulng = ShopData_DataTable.Rows[inti]["BaiDulng"].ToString();
                    string str_Shop_BaiDulat = ShopData_DataTable.Rows[inti]["BaiDulat"].ToString();

                    double mLat1 = 39.90923; // point1纬度
                    double mLng1 = 116.357428; // point1经度

                    double.TryParse(str_Shop_BaiDulng, out mLng1);
                    double.TryParse(str_Shop_BaiDulat, out mLat1);

                    double distance = Eggsoft.Common.GPS.GetShortDistance(mLng1, mLat1, doubleBaiDulng, doubleBaiDulat);

                    ArrayList_Shop_Distance cur = new ArrayList_Shop_Distance();
                    cur.strShop_ID = strShop_ID;
                    cur.DecimalDistance = distance;
                    mList.Add(cur);
                }

                ArrayList_Shop_Distance temp = new ArrayList_Shop_Distance();
                for (int i = mList.Count; i > 0; i--)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        if (mList[j].DecimalDistance > mList[j + 1].DecimalDistance)
                        {
                            temp = mList[j];
                            mList[j] = mList[j + 1];
                            mList[j + 1] = temp;
                        }
                    }

                }

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_UserID);
                int intDefaultO2OShop = Convert.ToInt32(Model_tab_User.DefaultO2OShop);



                //string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
                strstrNearestShopNameList += "            <table id=\"RadioButtonList_idGeto2oShop_Address\" class=\"RadioButtonList_Address_SmallFont\"><tbody>\n";
                for (int i = 0; i < mList.Count; i++)
                {
                    string strNearestShop_ID = mList[i].strShop_ID;
                    double DecimalDistance = mList[i].DecimalDistance / 1000;
                    Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(Int32.Parse(strNearestShop_ID));

                    string strCheck = "";
                    if (intDefaultO2OShop == 0)//直接选中第一个
                    {
                        if (i == 0)//直接选中第一个
                        {
                            strCheck = "checked";
                        }
                    }
                    else
                    {
                        if (intDefaultO2OShop.ToString() == strNearestShop_ID)//查询选中
                        {
                            strCheck = "checked";
                        }
                    }
                    strstrNearestShopNameList += "<tr><td><label><input id=\"RadioButtonList_idGeto2oShop_Address_" + strNearestShop_ID + "\" type=\"radio\" " + strCheck + " value=\"" + strNearestShop_ID + "\" name=\"RadioButtonList_dGeto2oShop\">\n";
                    //strstrNearestShopNameList += "" + "距离：" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(DecimalDistance)) + "公里 " + Model_tab_ShopClient_O2O_ShopInfo.ShopName + " " + Model_tab_ShopClient_O2O_ShopInfo.ShopAdMsg + " " + Model_tab_ShopClient_O2O_ShopInfo.ContactMan + " " + Model_tab_ShopClient_O2O_ShopInfo.Tel + " " + Model_tab_ShopClient_O2O_ShopInfo.ShopDayTime + "</label>\n";
                    strstrNearestShopNameList += "" + Model_tab_ShopClient_O2O_ShopInfo.ShopName + " " + Model_tab_ShopClient_O2O_ShopInfo.ShopAdMsg + " " + Model_tab_ShopClient_O2O_ShopInfo.ContactMan + " " + Model_tab_ShopClient_O2O_ShopInfo.Tel + " " + Model_tab_ShopClient_O2O_ShopInfo.ShopDayTime + "</label>\n";///公里 距离计算出错 暂时去掉
                    strstrNearestShopNameList += "</td></tr>\n";
                }
                strstrNearestShopNameList += "</tbody></table>\n";



            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally { }




            context.Response.Write(strstrNearestShopNameList);
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