using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.GuidePages
{
    public partial class GuidePages__Delete : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String MenuLink = "";
        string str_Pub_ClientApp = ConfigurationManager.AppSettings["ClientApp"];
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

        //private bool boolOnlyDO = false;
        EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                string ID = Request.QueryString["ID"];
                if (!CommUtil.IsNumStr(ID))
                    MyError.ThrowException("传递参数错误!");
                EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();

                bll.Delete(Int32.Parse(ID));

                JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/04GuidePages/GuidePages_Board.aspx");

            }
        }
    }
}