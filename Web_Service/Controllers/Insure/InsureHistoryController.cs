﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Service.Common;
using Web_Service.ViewModels.Home;

namespace Web_Service.Controllers.Home
{
    public class InsureHistoryController : BasicController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}