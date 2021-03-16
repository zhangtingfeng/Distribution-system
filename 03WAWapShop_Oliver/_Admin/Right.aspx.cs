using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver._Admin
{
    public partial class Right : Eggsoft.Common.DotAdminPage__Admin//System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {

                Literal_UserCount.Text = new EggsoftWX.BLL.tab_User().ExistsCount("1=1").ToString();

                EggsoftWX.BLL.tab_ShopClient my_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();

                Literal_CompanyCount.Text = my_tab_ShopClient.ExistsCount("IFCompany=1").ToString();


                Localize_HuanCun.Text = showAllCache();


                EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();
                string strWhere = "PayStatus=1 and isReceipt=0 and DeliveryText=\'\' ";
                Label_Board_WaitGiveGoods.Text = bll_tab_Order.ExistsCount(strWhere).ToString();

                // in 7 days
                #region in 7 days
                Decimal DecimalTotalMoneyCount = 0;
                Decimal DecimalFenXiaoMoneyCount = 0;

                int intstrCountCount = 0;
                strWhere = "PayStatus=1 and isReceipt=1 and DeliveryText<>\'\'  ";
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
                        System.Data.DataTable myDataTable = bll_View_SalesGoods.GetList("*", "OrderID='" + strOrder_ID + "' order by ID_Orderdetails asc").Tables[0];
                        for (int inti = 0; inti < myDataTable.Rows.Count; inti++)
                        {

                            String strGoodPrice = myDataTable.Rows[inti]["GoodPrice"].ToString();
                            String strOrderCount = (myDataTable.Rows[inti]["OrderCount"].ToString());


                            Decimal DecimalAllMoney = Decimal.Parse(strGoodPrice) * Int64.Parse(strOrderCount);

                            #region

                            #endregion

                        }
                    }
                    Label_In7Days.Text = "已完成订单数量 <a href=\"/ClientAdmin/19tab_Order/tab_Order_Board_Wait_Finished.aspx\"><font color=blue>" + intstrCountCount + "</font></a>   ,总金额：¥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalTotalMoneyCount) + ",分销所得(90%):¥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalFenXiaoMoneyCount) + ",";
                    Label_In7Days.Text += "订单号列表：" + strOrderNumList;
                }
                #endregion
                //over 7 day
                #region over 7 day
                DecimalTotalMoneyCount = 0;
                DecimalFenXiaoMoneyCount = 0;

                intstrCountCount = 0;

                strWhere = "PayStatus=1 and isReceipt=1 and DeliveryText<>\'\'  ";
                strWhere += " and datediff(d,PayDateTime,getdate())> 7";
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
                        System.Data.DataTable myDataTable = bll_View_SalesGoods.GetList("*", "OrderID='" + strOrder_ID + "' order by ID_Orderdetails asc").Tables[0];
                        for (int inti = 0; inti < myDataTable.Rows.Count; inti++)
                        {

                            String strGoodPrice = myDataTable.Rows[inti]["GoodPrice"].ToString();
                            // String strFenXiaoMoney = myDataTable.Rows[inti]["FenXiaoMoney"].ToString();
                            String strOrderCount = (myDataTable.Rows[inti]["OrderCount"].ToString());

                            Decimal DecimalAllMoney = Decimal.Parse(strGoodPrice) * Int64.Parse(strOrderCount);

                            #region
                            #endregion
                        }
                    }
                    Label_Over7Days.Text = "已完成订单数量 <a href=\"/ClientAdmin/19tab_Order/tab_Order_Board_Wait_Finished.aspx\"><font color=blue>" + intstrCountCount + "</font></a>   ,总金额：¥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalTotalMoneyCount) + ",分销所得(90%):¥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalFenXiaoMoneyCount) + ",";
                    Label_Over7Days.Text += "订单号列表：" + strOrderNumList;
                }
                #endregion


            }



        }

        protected void Button_Clear_Click(object sender, EventArgs e)
        {
            RemoveAllCache();
        }


        //清除所有缓存

        protected void RemoveAllCache()
        {

            System.Web.Caching.Cache _cache = HttpRuntime.Cache;

            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();

            ArrayList al = new ArrayList();

            while (CacheEnum.MoveNext())
            {

                al.Add(CacheEnum.Key);

            }

            foreach (string key in al)
            {

                _cache.Remove(key);

            }



        }

        //显示所有缓存

        String showAllCache()
        {

            string str = "";

            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();

            while (CacheEnum.MoveNext())
            {

                str += "缓存名<b>[" + CacheEnum.Key + "]</b><br />";

            }

            return "当前网站总缓存数:" + HttpRuntime.Cache.Count.ToString() + "<br />" + str;
            //"当前网站总缓存数:" + HttpRuntime.Cache.Count + "<br />"+str;

        }


    }
}