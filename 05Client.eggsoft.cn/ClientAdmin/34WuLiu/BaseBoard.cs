using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _05Client.eggsoft.cn.ClientAdmin._34WuLiu
{
    public class BaseBoard: Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                //ViewState["PageIndex"] = 1;
                //ViewState["PageSize"] = 20;
                //ViewState["RecordCount"] = bllView_.ExistsCount("ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                //BindAnnounce();
                //ShowState();
                //InitGoPage();
            }
        }
    }
}