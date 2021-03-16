using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Service.Common;
using Web_Service.ViewModels.Home;

namespace Web_Service.Controllers.User
{
    public class UserController : BasicController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        //[AuthorizeFilter("Customer_User_Userlist")]
        public ActionResult IndexNew()
        {
            return View();
        }

        public ActionResult AddNewUser()
        {
            ViewBag.UserID = Guid.NewGuid().ToString().Replace("-","");
            ViewBag.IsEdit = "false";
            return View("UserAuthentcation");
        }

        public ActionResult UserAuthentcation(string id)
        {
            ViewBag.UserID = id;
            ViewBag.IsEdit = "true";
            return View();
        }

        //public ActionResult UserInsert()
        //{
        //    return View();
        //}
    }
}