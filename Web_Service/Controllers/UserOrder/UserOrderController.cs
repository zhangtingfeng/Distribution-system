using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Service.Common;

namespace Web_Service.Controllers.UserOrder
{
    public class UserOrderController : BasicController
    {

        //
        // GET: /UserOrder/
        public ActionResult Index(string id, string CarNumber)
        {
            ViewBag.VipCarID = id;
            ViewBag.CarNumber2 = CarNumber;
            ViewBag.CarNumber = CarNumber;
            return View();
        }

        public ActionResult ViewByInfo(string id)
        {
            ViewBag.CarNumber = id;
            ViewBag.UserPhone = "";
            return View("Index");
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <returns></returns>
        public ActionResult OrderDetail(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return RedirectToAction("Index");
            ViewBag.OrderId = id;
            return View();
        }
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrder()
        {
            return View();
        }

        /// <summary>
        /// 查看日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Log(string id)
        {
            ViewBag.OrderId = id;
            return View();
        }
	}
}