using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Eggsoft_Public_CL
{
    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class ClassP
    {
        public ClassP()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public class ArrayList_Shop_Distance
        {
            public string strShop_ID;
            public double DecimalDistance;
        }

        public static void getNearestShop_IDFrom_UserID(int intpub_Int_UserID, out int outintShopo2oID, out double outDecimalDistance, out string strUserBaiDuAdress)
        {
            //Eggsoft.Common.debug_Log.Call_WriteLog("getNearestShop_IDFrom_UserID=" + intpub_Int_UserID, "Get_NestShopName");


            int intpub_Int_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intpub_Int_UserID.ToString());

            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
            EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo();

            System.Data.DataTable ShopData_DataTable = BLL_tab_ShopClient_O2O_ShopInfo.GetList("ShopClientID=" + intpub_Int_ShopClientID).Tables[0];

            double doubleBaiDulng = 0; double doubleBaiDulat = 0; Eggsoft_Public_CL.Pub.Get_o2o_NestUserID__(intpub_Int_UserID, out doubleBaiDulng, out doubleBaiDulat, out strUserBaiDuAdress);

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


            string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();



            string strNearestShop_ID = mList[0].strShop_ID;
            double outTempDecimalDistance = mList[0].DecimalDistance / 1000;
            outintShopo2oID = Int32.Parse(strNearestShop_ID);
            outDecimalDistance = outTempDecimalDistance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShopClientID"></param>
        /// <param name="boolAlert">是否弹窗警告</param>
        /// <returns></returns>             
        public static int CheckAuthorTime(string ShopClientID, out string strAlertDayScript, out DateTime? _AuthorTime)
        {
            //bool boolAuthorTimeed = true;
            strAlertDayScript = "";

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(ShopClientID));
            TimeSpan span = new TimeSpan();
            if (Model_tab_ShopClient == null)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("ShopClientID=" + ShopClientID, "CheckAuthorTime", "程序报错");

                strAlertDayScript = "尊敬的商户" + "程序报错";
                _AuthorTime = DateTime.Now.AddDays(-7);
                span = (TimeSpan)(_AuthorTime - DateTime.Now);
            }
            else
            {
                DateTime DateTime_AuthorTime = Convert.ToDateTime(Model_tab_ShopClient.AuthorTime);
                DateTime DateTime2 = DateTime.Now;
                _AuthorTime = DateTime_AuthorTime;
                span = (TimeSpan)(DateTime_AuthorTime - DateTime2);
                if (span.Days <= 90)
                {
                    strAlertDayScript = "尊敬的商户" + Model_tab_ShopClient.ShopClientName + ",你的平台服务有效期还有" + span.Days + "天,请通知您的财务进行付款。\\n以免影响认证情况.";

                    if (String.IsNullOrEmpty(Model_tab_ShopClient.XML) == false)
                    {

                        Eggsoft_Public_CL.XML__Class_Shop_Client myXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);

                        if (myXML__Class_Shop_Client.CheckEmail == true)
                        {
                            string strTo = myXML__Class_Shop_Client.Email;
                            string strSubject = "有效期通知！";
                            string strBody = "尊敬的商户" + Model_tab_ShopClient.ShopClientName + "您的公众号商城正在被用户访问，所以触发该通知。\n\n";
                            strBody += ",你的有效期还有" + span.Days + "天.到期后我们将不会再对贵商户的数据进行安全工作(如镜像备份), 请慎重考虑， 谢谢。";


                            Eggsoft_Public_CL.Pub.SendEmail_AddTask("微云基石", strTo, strSubject, strBody);

                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("商家Email尚未验证=" + myXML__Class_Shop_Client.Email);

                        }
                    }
                }

            }


            return span.Days;
        }


        public class WeiXinTuWen
        {
            public WeiXinTuWen() { }

            public WeiXinTuWen(String strTitle, String strImage, String strDescription, String strClickUrl)
            {
                this.Title = strTitle.Trim();
                this.Image = strImage.Trim();
                this.Description = strDescription.Trim();
                this.Url = strClickUrl.ToLower().Trim();

            }

            private String _Title;
            public String Title
            {
                get { return _Title; }
                set { _Title = value; }
            }
            private String _Image;
            public String Image
            {
                get { return _Image; }
                set { _Image = value; }
            }

            private String _Description;
            public String Description
            {
                get { return _Description; }
                set { _Description = value; }
            }
            private String _Url;
            public String Url
            {
                get { return _Url; }
                set { _Url = value; }
            }
        }


        public static string RemoveLastDouHao(string strClass_ID)
        {
            bool boolLast = true;
            if (string.IsNullOrEmpty(strClass_ID) == true)
            {
            }
            else
            {
                while (boolLast)
                {
                    int intLast = strClass_ID.LastIndexOf(',') + 1;
                    if (intLast == strClass_ID.Length)
                    {
                        strClass_ID = strClass_ID.Substring(0, intLast - 1);
                        boolLast = true;
                    }
                    else
                    {
                        boolLast = false;
                    }
                }
            }

            return strClass_ID;
        }


        public static int GetClass1_ID_From_Class2_ID(int Class2_ID)
        {
            int Myint = 0;
            string strInt = "0";
            EggsoftWX.BLL.tab_Class2 bll = new EggsoftWX.BLL.tab_Class2();

            if (bll.Exists("ID=" + Class2_ID.ToString()))
            {
                strInt = bll.GetList("Class1_ID", "ID=" + Class2_ID.ToString()).Tables[0].Rows[0][0].ToString();
            }
            Myint = Convert.ToInt32(strInt);
            return Myint;
        }

        public static int GetClass1_ID_From_Class3_ID(int Class3_ID)
        {
            int Myint = 0;
            string strInt = "0";
            EggsoftWX.BLL.tab_Class3 bll = new EggsoftWX.BLL.tab_Class3();

            if (bll.Exists("ID=" + Class3_ID.ToString()))
            {
                strInt = bll.GetList("Class1_ID", "ID=" + Class3_ID.ToString()).Tables[0].Rows[0][0].ToString();
            }
            Myint = Convert.ToInt32(strInt);
            return Myint;
        }

        public static int GetClass2_ID_From_Class3_ID(int Class3_ID)
        {
            int Myint = 0;
            string strInt = "0";
            EggsoftWX.BLL.tab_Class3 bll = new EggsoftWX.BLL.tab_Class3();

            if (bll.Exists("ID=" + Class3_ID.ToString()))
            {
                strInt = bll.GetList("Class2_ID", "ID=" + Class3_ID.ToString()).Tables[0].Rows[0][0].ToString();
            }
            Myint = Convert.ToInt32(strInt);
            return Myint;
        }


    }
}