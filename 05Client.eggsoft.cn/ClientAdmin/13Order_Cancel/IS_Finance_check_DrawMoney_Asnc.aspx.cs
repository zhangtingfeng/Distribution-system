using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._13Order_Cancel
{
    public partial class IS_Finance_check_DrawMoney_Asnc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                String strOrderID = Request.QueryString["OrderID"];
                String strIS_Admin_check = Request.QueryString["IS_Admin_check"];


                EggsoftWX.BLL.tab_ShopClient_ASK BLL_tab_ShopClient_ASK = new EggsoftWX.BLL.tab_ShopClient_ASK();
                BLL_tab_ShopClient_ASK.Update("IFPayByShenMa_Finace=" + strIS_Admin_check, "OrderID=" + strOrderID);

                //if (strIS_Admin_check == "1")
                //{
                //     string strInfo = "<a href=\"" + Eggsoft.Common.CommAuthen._Admin_GetAminURL() + "/_Admin/tab_ShopClient/IS_Admin_check_DrawMoney.aspx\">单击这里进行查看</a>";
                //    Eggsoft_Public_CL.Pub.SendEmail_AddTask("财务处理商户提款信息", "admin@eggsoft.cn", "财务处理结单信息", strInfo);
                //}
                Response.Write(strIS_Admin_check);
            }
        }
    }
}