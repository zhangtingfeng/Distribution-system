using InsureApi.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_Service.Common.Config;

namespace Web_Service.Common
{
    public class BasicController : Controller
    {
        internal string actionName
        {
            get
            {
                return this.ControllerContext.RouteData.Values["Action"].toString();
            }
        }

        internal string controllerName
        {
            get
            {
                return this.ControllerContext.RouteData.Values["Controller"].toString();
            }
        }

        internal void writeLog(object data)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Action:{0}", this.actionName));
            sb.AppendLine(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            //LogController.systemLog(sb.toString(), XLugia.Lib.XLog.LogType.getIns().info.net, this.controllerName);
        }
    }
}