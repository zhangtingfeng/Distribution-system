using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_App.ViewModels.User
{
    /// <summary>
    /// 登录视图数据模型
    /// </summary>
    public class AuthenticationViewModel
    {
        /// <summary>
        /// RSA加密 用户ID
        /// </summary>
        public string UserIDRSA { get; set; }

        /// <summary>
        /// MD5校验 用户ID
        /// </summary>
        public string UserIDMD5 { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 渠道发展编码
        /// </summary>
        public string DevelopmentCode { get; set; }

        /// <summary>
        /// 回跳Url
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}