using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Service.Common;
using Web_Service.ViewModels.Home;

namespace Web_Service.Controllers.Home
{
    public class TrackController : BasicController
    {
        public ActionResult Index(string id, string CarNumber)
        {
            ViewBag.VipCarID = id;
            ViewBag.CarNumber = CarNumber;
            return View();
        }
        
        public ActionResult Detail(string id)
        {
            ViewBag.CarInsureQueryHistoryID = id;
            return View();
        }

        public ActionResult Log(string id)
        {
            ViewBag.QueryTaskID = id;
            return View();
        }
    }
}