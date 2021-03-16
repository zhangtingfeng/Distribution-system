using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._02GuWuQuanChange
{
    public partial class IS_UserChangeMoney_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string strUser_AskChangeMoneyID_User = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strUser_AskChangeMoneyID_User))
                        MyError.ThrowException("传递参数错误!");


                    //string strOrderINT = Request.QueryString["OrderINT"];//订单记录的ID
                    // Eggsoft_Public_CL.GoodP.DeleteOrder(strOrderINT);

                    EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc_User bll_tab_GouWuQuan2XianJInEtc_User = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc_User();
                    bll_tab_GouWuQuan2XianJInEtc_User.Delete(Int32.Parse(strUser_AskChangeMoneyID_User));

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