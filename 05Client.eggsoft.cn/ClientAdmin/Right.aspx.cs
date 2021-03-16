using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin
{
    public partial class Right : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.Model.tab_DistributionMoney p_DistributionMoney_List_From_Good_ID = null;

        //[DllImport("kernel32")]
        //public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);

        ////定义内存的信息结构
        //[StructLayout(LayoutKind.Sequential)]
        //public struct MEMORY_INFO
        //{
        //    public uint dwLength;
        //    public uint dwMemoryLoad;
        //    public uint dwTotalPhys;
        //    public uint dwAvailPhys;
        //    public uint dwTotalPageFile;
        //    public uint dwAvailPageFile;
        //    public uint dwTotalVirtual;
        //    public uint dwAvailVirtual;
        //}
        public String strShopClientId = "";
        public String strDisplayDone = "display:none";


        protected void Page_Load(object sender, EventArgs e)
        {
            strShopClientId = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (strShopClientId != "34")
            {
                strDisplayDone = "";
            }
            //Response.Expires = 0;
            //Response.CacheControl = "no-cache";
            if (!(IsPostBack))
            {
                Label_Info.Text = "微云基石将推出（O2O门店功能，用户微店足迹功能，用户地理位置足迹功能）。该功能涉及相关第三方费用支出问题。有需要的商家请联系我们。";
                Label_Info.Text = "";

                oldInfo();
                alertInfo();
            }
        }

        protected void alertInfo()
        {
            EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();

            tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strShopClientId));

            //if (String.IsNullOrEmpty(tab_ShopClient_Model.OpenID))
            //{
            //    Label_Info.Text = "尚未关联微信号，不能及时和用户沟通交流。<br />";
            //}
            #region email
            string strXML = tab_ShopClient_Model.XML;


            if (string.IsNullOrEmpty(strXML))
            {
                Label_Info.Text += "商家Email尚未验证！<br />";
            }
            else
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                if ((XML__Class_Shop_Client.CheckEmail == false) || (XML__Class_Shop_Client.Email != tab_ShopClient_Model.Email))
                {
                    Label_Info.Text += "商家Email尚未验证！<br />";
                }
            }
            #endregion

            #region 投诉信息
            //string strUserId = Eggsoft_Public_CL.Pub.GetUserIDFromShopClientID(Int32.Parse(strShopClientId));
            //EggsoftWX.BLL.tab_User_Question BLL_tab_User_Question = new EggsoftWX.BLL.tab_User_Question();
            //int intNotReadCount = BLL_tab_User_Question.ExistsCount("IsRead=0 and ToUserID=" + strUserId);
            //Label_Info.Text += "尚未阅读用户信息！" + intNotReadCount + "<br />";
            #endregion

        }


        protected void oldInfo()
        {
            strShopClientId = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strWapApp = ConfigurationManager.AppSettings["WapApp"];
            string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

            EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(strShopClientId));
            string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
            //Eggsoft.Common.JsUtil.ShowMsg(strErJiYuMing);

            string strhttpURL = Server.UrlEncode(strWapApp);
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientId)) + "/QRCodeImage/";
            //Eggsoft.Common.JsUtil.ShowMsg(upLoadpath);
            string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage("https://" + strErJiYuMing, upLoadpath, "");

            Label_WeiXin.Text = "";
            Label_WeiXin.Text += " <a target=\"_blank\" href=\"" + strImageUrl + "\">";
            Label_WeiXin.Text += "<img id=\"ErWeiMaSao\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" />";
            Label_WeiXin.Text += "</a>";


            HeadPage.Text = "<a target=\"_blank\" href=\"https://" + strErJiYuMing + "\"><font color=blue>https://" + strErJiYuMing + "</font></a>";

            //待发货的
            EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();
            string strWhere = "ShopClient_ID=" + strShopClientId + " and PayStatus=1 and isReceipt=0 and DeliveryText=''";///PayStatus=1 and isReceipt=0 and DeliveryText=\'\'
            //Label_Board_WaitGiveGoods.Text = "<a href=\"/ClientAdmin/tab_Order/tab_Order_Board_WaitGiveGoods.aspx\"><font color=blue>" + bll_tab_Order.ExistsCount(strWhere) + "</font></a>";
            //HeadPage=""



            // in 7 days
            #region in 7 days
            //Decimal DecimalTotalMoneyCount = 0;
            //Decimal DecimalFenXiaoMoneyCount = 0;

            int intstrCountCount = 0;
            strWhere = "ShopClient_ID=" + strShopClientId + " and PayStatus=1 ";
            strWhere += " and datediff(d,PayDateTime,getdate())<= 7";
            if (bll_tab_Order.ExistsCount(strWhere) > 0)
            {
                strWhere += " order by id desc";

                System.Data.DataTable myOrderDataTable = bll_tab_Order.GetList(strWhere).Tables[0];
                intstrCountCount = myOrderDataTable.Rows.Count;

                string strOrderNumList = "";
                for (int i = 0; i < myOrderDataTable.Rows.Count; i++)
                {
                    string strOrder_ID = myOrderDataTable.Rows[i]["id"].ToString();

                    EggsoftWX.BLL.View_SalesGoods bll_View_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();
                    //Eggsoft.Common.JsUtil.ShowMsg("OrderID='" + strOrder_ID);
                    if (i < myOrderDataTable.Rows.Count - 1)
                    {
                        strOrderNumList += Eggsoft_Public_CL.GoodP.GetOrderNum_From_OrderID(Int32.Parse(strOrder_ID)) + ",";
                    }
                    else
                    {
                        strOrderNumList += Eggsoft_Public_CL.GoodP.GetOrderNum_From_OrderID(Int32.Parse(strOrder_ID));
                    }
                    

                }
                Label_In7Days.Text += "" + strOrderNumList + "";
            }
            #endregion

        }


        //protected void Testbtn_Click(object sender, EventArgs e)
        //{
        //    TimeSpan t1 = DateTime.Now.TimeOfDay;

        //    for (int i = 1; i <= 40003006200000; i++)
        //    {
        //        i += 1;
        //    }
        //    TimeSpan t2 = DateTime.Now.TimeOfDay;
        //    TimeSpan t3 = t2.Subtract(t1);
        //    showtest.Text = t3.TotalMilliseconds + "毫秒";
        //}
    }
}