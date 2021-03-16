using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver.ModifyDB
{
    public partial class WFModifiedOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();

            Dictionary<Int32, Boolean> openWithDictionary = new Dictionary<Int32, Boolean>();

            ///string strSQL = "select ID from tab_Order where Paystatus=1 and Over7DaysToBeans=1 and creattime>CONVERT(datetime,'2017-01-30',120) order by id desc ";

            string strOrderIDSQL = "select ID,OrderID from tab_Orderdetails where Over7DaysToBeans=1 and creattime>CONVERT(datetime,'2017-01-30',120) order by id desc ";


            System.Data.DataTable Data_DataTable = BLL_tab_Order.SelectList(strOrderIDSQL).Tables[0];

            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                String strOrderdetailsID = Data_DataTable.Rows[i]["ID"].ToString();
                String strOrderID = Data_DataTable.Rows[i]["OrderID"].ToString();

                if (BLL_tab_Order.Exists("Paystatus=1 and ID=" + strOrderID))
                {
                    if (!openWithDictionary.ContainsKey(int.Parse(strOrderID)))
                    {

                        bool boolTrue = BLL_tab_TotalCredits_Consume_Or_Recharge.Exists("ConsumeTypeOrRecharge like '%代理收入(订单" + strOrderID + ")%'");
                        openWithDictionary.Add(int.Parse(strOrderID), boolTrue);

                        if (boolTrue == false)
                        {
                            BLL_tab_Orderdetails.Update("Over7DaysToBeans=0", "ID=" + strOrderdetailsID);
                        }
                    }
                }
            }

            //foreach (KeyValuePair<Int32, Boolean> kvp in openWithDictionary)
            //{
            //    if (kvp.Value == false)
            //    { 


            //    }
            //    Console.WriteLine("姓名：{0},电影：{1}", kvp.Key, kvp.Value);
            //}




        }
    }
}