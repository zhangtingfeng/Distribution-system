using DALMongoDbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Service.Common;
using Web_Service.ViewModels.Home;

namespace Web_Service.Controllers.Home
{
    public class LogsEveryDayController : BasicController
    {
        #region Other操作记录
        /// <summary>
        /// 初始页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Detail(string id)
        {
            ViewBag.LogsEveryDayID = id;
            return View();
        }
        #endregion


        [HttpPost]
        // POST api/values
        public String Post(Insure.Common.Logs_EveryDay thisLogs_EveryDay)
        {
            try
            {
              

                //setTimerglobal(thisLogs_EveryDay);

            }
            catch (Exception Exceptione)
            {
                //Insure.Common.debug_Log.Call_WriteLog(Exceptione, "WriteLogController");
            }
            finally
            {

            }



            return "ok";
        }

    }
}