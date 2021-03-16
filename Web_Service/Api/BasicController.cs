using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.Entity;
using InsureApi.Mvc;
using InsureApi.WebApi.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Security;

namespace Web_Service.Api
{
    [ApiLogFilter]
    public class BasicController : ApiController
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

        /// <summary>
        /// 支持前台跨域访问
        /// </summary>
        public string Options()
        {
            // HTTP 200 response with empty body
            return null;
        }

        /// <summary>
        /// 验证是否是有效的UserID
        /// </summary>
        internal bool IsValidUserID(UserCookieAPIModel userCookie, out string msg)
        {
            msg = "";
            if (Check.getIns().isEmpty(userCookie))
            {
                msg = "用户信息不正确";
                return false;
            }
            var userInfo = new TUserBC().SelectByPrimaryKey(userCookie.userID);
            if (Check.getIns().isEmpty(userInfo))
            {
                msg = "用户信息不正确";
                return false;
            }
            return IsValidUserID(userInfo, out msg);
        }

        /// <summary>
        /// 验证是否是有效的UserID
        /// </summary>
        internal bool IsValidUserID(string userID, out string msg)
        {
            msg = "";
            return IsValidUserID(new TUserBC().SelectByPrimaryKey(userID), out msg);
        }

        /// <summary>
        /// 验证是否是有效的UserID
        /// </summary>
        internal bool IsValidUserID(TUserInfo TUser_Info, out string msg)
        {
            msg = "";
            if (TUser_Info == null)
            {
                msg = "帐号不存在！";
                return false;
            }
            if (TUser_Info.IsDelete != 0)
            {
                msg = "帐号已禁用！";
                return false;
            }
            if (TUser_Info.State != 1)
            {
                msg = "帐号未授权";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置错误消息
        /// <remarks>Created by denglei 2016年3月25日</remarks>
        /// </summary>
        /// <param name="result"></param>
        /// <param name="errorMsg"></param>
        /// <typeparam name="T"></typeparam>
        protected void SetErrorMessage<T>(ResultAPIModel<T> result, string errorMsg)
        {
            if (Check.getIns().isEmpty(result))
                result = new ResultAPIModel<T>();
            if (Check.getIns().isEmpty(result.resultMessage))
                result.resultMessage = new ResultMessageAPIModel();
            result.resultMessage.errorCode = (int)ResultMessageAPIModel.Codes.fail;
            result.resultMessage.errorMessage = errorMsg;
        }
    }
}