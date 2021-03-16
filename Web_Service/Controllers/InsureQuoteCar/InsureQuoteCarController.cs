using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Service.Common;

namespace Web_Service.Controllers.Customer
{
    public class InsureQuoteCarController : BasicController
    {
        public ActionResult Index(string id)
        {
            ViewBag.UserID = id;
            return View();
        }

        //
        // GET: /Customer/
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id">车辆ID</param>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            ViewBag.CarInsureQueryCarID = id;
            return View();
        }
	}
}