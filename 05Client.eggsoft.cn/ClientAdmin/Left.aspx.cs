using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05ClientEggsoftCn.ClientAdmin
{
    public partial class Left : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String stringShowHelpNameList = "";
        public string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                    HyperLink_BoardINC_Manage.NavigateUrl = "/ClientAdmin/10tab_ShopClient/BoardINC_Manage.aspx?type=Modify";
                    HyperLink_tab_ShopClient_ShopPar.NavigateUrl = "/ClientAdmin/10tab_ShopClient/01BoardINC_Manage_ShopClient_ShopPar.aspx?type=Modify";
                    #region 生成静态页

                    string strHyperLink_MakeHtml = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";
                    HyperLink_MakeHtml.NavigateUrl = strHyperLink_MakeHtml + Server.UrlEncode("WriteHtml/Default.aspx");
                    #endregion

                    EggsoftWX.BLL.Help_Class1 BLL_Help_Class1 = new EggsoftWX.BLL.Help_Class1();
                    System.Data.DataTable DataTablemy = BLL_Help_Class1.GetList("BuyOrSalse='Salse' order by sort asc,id asc").Tables[0];

                    for (int i = 0; i < DataTablemy.Rows.Count; i++)
                    {
                        string strClassName = DataTablemy.Rows[i]["ClassName"].ToString();
                        string strClassID = DataTablemy.Rows[i]["ID"].ToString();
                        stringShowHelpNameList += "<li><a target=\"main\" href=\"Help_Sales_Show/Board_HelpContent.aspx?Help_Class1_ID=" + strClassID + "\">" + strClassName + "</a></li>";
                    }
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "19tab_Order");
            }
        }


    }
}