using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin
{
    public partial class Default : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(strID));

                string _Pub_BackUrl = "";

                _Pub_BackUrl = Request.QueryString["BackUrl"];
                if (String.IsNullOrEmpty(_Pub_BackUrl) == true)
                {
                    _Pub_BackUrl = "/ClientAdmin/Right.aspx";
                }
                else//判断是否当前域名
                {
                    string strhttpUrl = Eggsoft.Common.Application.httpUrl;
                    if (string.IsNullOrEmpty(_Pub_BackUrl)) _Pub_BackUrl = "";
                    if (_Pub_BackUrl.ToLower().IndexOf(strhttpUrl.ToLower()) > -1)
                    {
                        //是本站域名 什么的都不做 继续访问
                    }
                    else
                    {
                        _Pub_BackUrl = "/ClientAdmin/Right.aspx";
                    }
                    // _Pub_BackUrl = "default.aspx";
                }
                Iframe1_src.Value = _Pub_BackUrl;

                Label_INC.Text = Model.ShopClientName + "企业后台管理系统";// "<%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>微店";

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "ClientAdmin/Default");
            }
        }
    }
}