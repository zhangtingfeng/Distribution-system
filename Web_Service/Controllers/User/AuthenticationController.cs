using InsureApi.Common.Common;
using InsureApi.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_App.ViewModels.User;
using Web_Service.Common;
using Web_Service.Common.Config;

namespace Web_App.Controllers.User
{
    [AllowAnonymous]
    public class AuthenticationController : BasicController
    {
        /// <summary>
        /// 用户登录 Get
        /// </summary>
        [HttpGet]
        public ActionResult login()
        {
            //webApp返回地址
            var returnUrl = Check.getIns().isEmpty(Request.Params["ReturnUrl"]) ?
                "" :
                Request.Params["ReturnUrl"].ToString();

            var model = new AuthenticationViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View("login", model);
        }

        /// <summary>
        /// 用户注销 Get
        /// </summary>
        [HttpGet]
        public ActionResult logout()
        {
            var userSession = new XUserSession(this.HttpContext);
            userSession.reset();
            return login();
        }

        /// <summary>
        /// 用户登录 Post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> login(AuthenticationViewModel m)
        {
            if (Check.getIns().isEmpty(m.UserAccount, m.UserName, m.UserIDRSA, m.UserIDMD5))
            {
                ViewBag.errorMsg = "用户名或密码不正确";
                return View("login", m);
            }

            //if (!m.UserAccount.ToUpper().Equals("Admin".ToUpper()))
            //{
            //    ViewBag.errorMsg = "用户名或密码不正确";
            //    return View("login", m);
            //}

            //rsa解密userid
            var userID = Secret.getIns().decryptRSA(m.UserIDRSA);
            //md5校验解密后的userid
            if (!m.UserIDMD5.Equals(Secret.getIns().encryptMD5(userID)))
            {
                ViewBag.errorMsg = "用户名或密码不正确";
                return View("login", m);
            }

            //session
            var userSession = new XUserSession(this.HttpContext);
            userSession.user = new XUserSessionModel()
            {
                UserID = userID,
                UserAccount = m.UserAccount,
                IsAllowLogin = true,
            };

            if (Check.getIns().isEmpty(m.ReturnUrl))
            {
                return RedirectToAction("Index", "Track");
            }

            return Redirect(m.ReturnUrl);
        }
    }
}