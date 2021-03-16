using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.Handler
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //       Response.Write("{\"list\":[{\"id\":1,\"name\":1,\"time\":" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "}]}");
            string strList = "{\"list\":[";
            for (int i = 0; i < 20; i++)
            {
                strList += "{\"id\":\"" + i.ToString() + "\",\"name\":\"name" + i.ToString() + "\",\"time\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\"}";
                if (i != 19) strList += ",";

            }
            strList += "]}";


            Response.Write(strList);
        }
    }
}