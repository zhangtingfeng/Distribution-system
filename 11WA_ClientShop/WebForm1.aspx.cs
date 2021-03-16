using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Eggsoft.Common.HolidayHelper myHolidayHelper = Eggsoft.Common.HolidayHelper.GetInstance();
            //DateTime mydatgedate = myHolidayHelper.GetReckonDate(5, false);
            //int intNUM=myHolidayHelper.GetWorkDayNum(DateTime.Now.AddDays(1), true);
            Response.Write(myHolidayHelper.isWorkDay(DateTime.Now));
        }
    }
}