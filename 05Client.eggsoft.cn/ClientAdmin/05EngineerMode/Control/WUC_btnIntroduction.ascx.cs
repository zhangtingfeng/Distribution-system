using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode.Control
{
    public partial class WUC_btnIntroduction : System.Web.UI.UserControl
    {
        public string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (url.IndexOf("Resource-1.aspx") != -1)
            {
                btnIntroduction1.CssClass = "ajax__tab_active";
            }
            //else if (url.IndexOf("Resource-ImageType5.aspx") != -1)
            //{
            //    btnIntroduction1_Resource_ImageType5.CssClass = "ajax__tab_active";
            //}
            else if (url.IndexOf("Resource-2.aspx") != -1)
            {
                btnIntroduction2.CssClass = "ajax__tab_active";
            }
            else if (url.IndexOf("Resource-3.aspx") != -1)
            {
                btnIntroduction3.CssClass = "ajax__tab_active";
            }
            //else if (url.IndexOf("Resource-VoiceType6.aspx") != -1)
            //{
            //    btnIntroduction_Resource_VoiceType6.CssClass = "ajax__tab_active";
            //}
            //else if (url.IndexOf("Resource-VideoType7.aspx") != -1)
            //{
            //    btnIntroduction_Resource_VideoType7.CssClass = "ajax__tab_active";
            //}
            //else if (url.IndexOf("Resource-MusicType8.aspx") != -1)
            //{
            //    btnIntroduction_Resource_MusicType8.CssClass = "ajax__tab_active";
            //}
            //else if (url.IndexOf("Resource-ZhengWen.aspx") != -1)
            //{
            //    btnIntroduction_Resource_ZhengWen.CssClass = "ajax__tab_active";
            //}

        }


        protected void btnIntroduction_Click1(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-1.aspx");
        }



        protected void btnIntroduction_Clic_Resource_ImageType5(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-ImageType5.aspx");
        }


        protected void btnIntroduction_Click2(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-2.aspx");
        }


        protected void btnIntroduction_Click3(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-3.aspx");

        }

        protected void btnIntroduction_Clic_Resource_VoiceType6(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-VoiceType6.aspx");

        }

        protected void btnIntroduction_Clic_Resource_VideoType7(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-VideoType7.aspx");
        }


        protected void btnIntroduction_Clic_Resource_MusicType8(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-MusicType8.aspx");

        }

        //protected void btnIntroduction_Clic_Resource_ZhengWen(object sender, EventArgs e)
        //{
        //    Eggsoft.Common.JsUtil.LocationNewHref("Resource-ZhengWen.aspx");

        //    //string strZhengWen = strUpLoadURL + "/UpLoadFile/_Admin/LoginAdmin.aspx?Act=gotouserFrom_Admin&UserID=" + Eggsoft.Common.Session.Read("Eggsoft__Admin__Users") + "&GoToUrl=" + Server.UrlEncode("EngineerMode/Resource-ZhengWen.aspx");

        //    //Eggsoft.Common.JsUtil.LocationNewHref(strZhengWen);
        //}

    }
}