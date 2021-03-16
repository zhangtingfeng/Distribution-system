using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _06ThirdplatForm.eggsoft.cn
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Eggsoft_Public_CL.OperationCenter.update_Only_One_UserID_Operation_ID(41192,21, 43179);

            ///http://testoliver.eggsoft.cn/Status/Default_DoOrderShow.aspx

            Eggsoft_Public_CL.GoodP.IGetMoney("2018011420381912359", "Tenpay", "2018011420381912359");
        }
    }
}