using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile
{
    public partial class ShowContent_Web : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strContentid = Request.QueryString["Contentid"];

            if (!IsPostBack)
            {

                EggsoftWX.BLL.Help_Content UpLoad = new EggsoftWX.BLL.Help_Content();
                EggsoftWX.Model.Help_Content Model_Help_Content = new EggsoftWX.Model.Help_Content();


                Model_Help_Content = UpLoad.GetModel(Int32.Parse(strContentid));


                Literal_HelpContent.Text = Server.HtmlDecode(Model_Help_Content.LongInfo);
                Literal_HelpTitle.Text = Model_Help_Content.Name;
            }
        }
    }
}