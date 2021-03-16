using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Service.Common;
using Web_Service.ViewModels.Home;

namespace Web_Service.Controllers.Home
{
    public class ActionRecordController : BasicController
    {
        #region API操作记录
        /// <summary>
        /// 初始页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ActionRecord()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ActionDetail(string id)
        {
            ViewBag.APIActionRecordID = id;
            return View();
        }
        #endregion

    }
}