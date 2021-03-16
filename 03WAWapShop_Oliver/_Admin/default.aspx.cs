using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver._Admin
{
    public partial class _default : Eggsoft.Common.DotAdminPage__Admin//System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label_INC.Text = Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "综合性微店后台管理系统";
        }
    }
}