using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class ResourceSave : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                String strType = Request.QueryString["type"];
                String strResourceID = Request.QueryString["ResourceID"];
                String strTextContent = Request.QueryString["TextContent"];


                EggsoftWX.BLL.tab_ShopClient_System_XML_Resource myBLLnew = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();


                if (strType == "Modify")
                {

                    myBLLnew.Update("Text='" + strTextContent + "'", "ID=" + strResourceID);
                    Response.Write("1");
                }
            }
        }
    }
}