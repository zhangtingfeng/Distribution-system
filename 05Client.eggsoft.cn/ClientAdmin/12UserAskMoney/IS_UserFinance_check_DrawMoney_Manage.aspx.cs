using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._12UserAskMoney
{
    public partial class IS_UserFinance_check_DrawMoney_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string strUser_AskGetMoneyID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strUser_AskGetMoneyID))
                        MyError.ThrowException("传递参数错误!");


                    //string strOrderINT = Request.QueryString["OrderINT"];//订单记录的ID
                    // Eggsoft_Public_CL.GoodP.DeleteOrder(strOrderINT);

                    EggsoftWX.BLL.tab_User_AskGetMoney bll_tab_User_AskGetMoney = new EggsoftWX.BLL.tab_User_AskGetMoney();
                    bll_tab_User_AskGetMoney.Update("Isdeleted=1", "id=@ID", strUser_AskGetMoneyID);

                    //EggsoftWX.BLL.tab_Orderdetails bll_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    //bll_tab_Orderdetails.Delete("OrderID=" + strID);

                    string typeCallBackUrl = Request.QueryString["CallBackUrl"];
                    typeCallBackUrl = typeCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("删除成功!", typeCallBackUrl);
                }

            }
        }
    }
}