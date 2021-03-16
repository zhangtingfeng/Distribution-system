using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._19tab_Order
{
    public partial class tab_Order_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if(!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_Board")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if(!IsPostBack)
            {
                try
                {
                    string type = Request.QueryString["type"];

                    if(type.ToLower() == "delete")
                    {
                        string strOrderINT = Request.QueryString["ID"];
                        if(!CommUtil.IsNumStr(strOrderINT))
                            MyError.ThrowException("传递参数错误!");

                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                        //string strOrderINT = Request.QueryString["OrderINT"];//订单记录的ID
                        Eggsoft_Public_CL.GoodP.DeleteOrder(strOrderINT, strwebuy8_ClientAdmin_Users_ClientUserAccount+"删除");

                        //EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();
                        //bll_tab_Order.Delete(Int32.Parse(strID));

                        //EggsoftWX.BLL.tab_Orderdetails bll_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                        //bll_tab_Orderdetails.Delete("OrderID=" + strID);

                        string typeCallBackUrl = Request.QueryString["CallBackUrl"];
                        typeCallBackUrl = typeCallBackUrl.Replace("*", "?");
                        JsUtil.ShowMsg("删除成功!", typeCallBackUrl);
                    }
                }
                catch(System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch(Exception Exceptiondddd)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "19tab_Order");
                }
            }
        }
    }
}