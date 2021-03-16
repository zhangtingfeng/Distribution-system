using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._13Order_Cancel
{
    public partial class IS_FinanceAdmin_check_ReturnMoney_Asnc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String strOrderID = Request.QueryString["OrderID"];
                String strIS_Admin_check = Request.QueryString["IS_Admin_check"];

                EggsoftWX.BLL.tab_ReturnMoney BLL_tab_ReturnMoney = new EggsoftWX.BLL.tab_ReturnMoney();
                BLL_tab_ReturnMoney.Update("FinanceCheck=" + strIS_Admin_check, "OrderID=" + strOrderID);

                if (strIS_Admin_check == "1")
                {
                    Eggsoft_Public_CL.Pub_FenXiao.DoReturnMoney_By_OrderIDByIDCountyuEArgMoney(Int32.Parse(strOrderID));
                    EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                }
                Response.Write(strIS_Admin_check);
            }
        }
    }
}