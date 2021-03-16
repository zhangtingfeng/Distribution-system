using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WriteHtml
{
    public partial class Default : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strpShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            int ShopClientID = Int32.Parse(strpShopClientID);
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(ShopClientID);
            int intStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);

            Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(ShopClientID);


            Eggsoft.Common.JsUtil.LocationNewHref("writeHtml_StyleModel" + intStyle_Model + ".aspx");
        }
    }
}