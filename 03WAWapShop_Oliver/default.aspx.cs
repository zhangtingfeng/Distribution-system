using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          string strddd= HttpContext.Current.Request.Url.Host.ToString();
            Response.Write(strddd);
            //#region 每天运营中心加权分红
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红开始执行4", "每天更新");
            //Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay mmPub_Default_DoYunYingZhongXin28EveryDay = new Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay(1);
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红执行完毕4", "每天更新");
            //#endregion 每天运营中心加权分红

            Eggsoft.Common.JsUtil.LocationNewHref("/_Admin/default.aspx");
        }
    }
}