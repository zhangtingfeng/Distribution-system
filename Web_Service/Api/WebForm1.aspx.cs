using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Service.Api
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        System.Timers.Timer tglobal = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            setTimerglobal();
        }

        private void setTimerglobal()
        {
            if (tglobal == null)
            {
                tglobal = new System.Timers.Timer(6000);//实例化Timer类，设置间隔时间为10000毫秒；
                tglobal.Elapsed += new System.Timers.ElapsedEventHandler(tglobaltheout);//到达时间的时候执行事件；
                tglobal.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
                tglobal.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
                Insure.Common.debug_Log.Call_WriteLog(DateTime.Now.ToString(), "定时器测试", "启动事件");
            }
        }

        private void tglobaltheout(object sender, ElapsedEventArgs e)
        {
            tglobal = null;
            Insure.Common.debug_Log.Call_WriteLog(DateTime.Now.ToString(), "定时器测试", "到达事件");
            //throw new NotImplementedException();
        }
    }
}