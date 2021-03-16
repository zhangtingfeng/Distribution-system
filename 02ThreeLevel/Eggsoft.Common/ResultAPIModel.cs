using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eggsoft.Common
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class MainBasicAPIModel
    {
        /// <summary>
        /// 将对象转换成json字符串
        /// </summary>
        public string toJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }

    /// <summary>
    /// 消息模型
    /// </summary>
    public class ResultMessageAPIModel
    {
        public enum Codes
        {
            success = 200,
            fail = 404
        }

        public ResultMessageAPIModel()
        {
            this.code = (int)Codes.success;
            this.message = "";
            this.errorCode = 0;
            this.errorMessage = "";
        }

        /// <summary>
        /// 返回代码(用于系统级消息)
        /// 200 : 正常
        /// 404 : 错误
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 返回信息(用于系统级消息)
        /// 对应返回代码错误
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 错误代码(用于逻辑处理用)
        /// </summary>
        public int errorCode { get; set; }

        /// <summary>
        /// 错误信息(用于逻辑处理用)
        /// </summary>
        public string errorMessage { get; set; }
    }
    public class PagingAPIModel
    {
        #region properties
        /// <summary>
        /// 数据量
        /// </summary>
        public int dataCount { get; set; }

        /// <summary>
        /// 当前页号
        /// 页数从1开始
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 分页查询用，起始行号
        /// </summary>
        public int startRowNumber
        {
            get
            {
                var val = (pageNumber - 1) * pageSize;
                if (val < 0) val = 0;
                return val + 1;
            }
        }

        /// <summary>
        /// 分页查询用，结束行号
        /// </summary>
        public int endRowNumber
        {
            get
            {
                var val = pageNumber * pageSize;
                if (val < 0) val = 0;
                return val;
            }
        }
        #endregion

        /// <summary>
        /// 验证信息
        /// </summary>
        public bool IsValidInfo(out string errorMessage)
        {
            errorMessage = "";
            if (pageSize <= 0)
            {
                errorMessage = "pageSize未定义(必须>=1)";
                return false;
            }

            return true;
        }
    }

    public class UserCookieAPIModel
    {

        public UserCookieAPIModel()
        {
            this.userID = "";
            this.userName = "";
            this.userAccount = "";
            this.channelNumber = "";
            this.companyName = "";
            this.clientType = "";
            this.platformID = "";
            this.ShopClientID = 0;
            this.DevelopmentCode = "";
            this.AuthorTime = null;
            this.AllMySonAdminPowerList = "";
        }

        #region properties
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userID { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string userAccount { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 渠道号
        /// </summary>
        public string channelNumber { get; set; }

        /// <summary>
        /// 渠道名称/商户名
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// 访问客户端类型
        /// </summary>
        public string clientType { get; set; }

        /// <summary>
        /// 渠道标识用户ID
        /// </summary>
        public int? ShopClientID { get; set; }

        /// <summary>
        /// Get or set the platform identifier.
        /// </summary>
        /// <value>
        /// The platform identifier.
        /// </value>
        /// <remarks>
        ///  Created By denglei 2015/11/3
        /// </remarks>
        public string platformID { get; set; }
        /// <summary>
        /// 所有的子孙账户 updateby 字段匹配权限
        /// </summary>
        public String AllMySonAdminPowerList { get; set; }
        /// <summary>
        /// 授权到期时间
        /// </summary>
        public DateTime? AuthorTime { get; set; }

        public string DevelopmentCode { get; set; }
        #endregion


       

        /// <summary>
        /// 验证信息
        /// </summary>
        public bool IsValidInfo(out string errorMessage)
        {
            errorMessage = "";
            if (CommUtil.isEmpty(userID))
            {
                errorMessage = "userID未定义";
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 返回对象
    /// </summary>
    public class ResultAPIModel<T> : MainBasicAPIModel
    {
        public ResultAPIModel()
        {
            this.resultMessage = new ResultMessageAPIModel();
            this.paging = new PagingAPIModel();
            this.userCookie = new UserCookieAPIModel();
        }

        /// <summary>
        /// 数据
        /// </summary>
        public T data = default(T);

        /// <summary>
        /// 返回信息
        /// </summary>
        public ResultMessageAPIModel resultMessage { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingAPIModel paging { get; set; }

        /// <summary>
        /// 用户cookie
        /// </summary>
        public UserCookieAPIModel userCookie { get; set; }

        /// <summary>
        /// 验证用户
        /// </summary>
        public bool IsValidUserInfo(out string errorMessage)
        {
            errorMessage = "";
            if (CommUtil.isEmpty(this.userCookie))
            {
                errorMessage = "登录信息不正确(userCookie)";
                return false;
            }

            if (CommUtil.isEmpty(this.userCookie.userID))
            {
                errorMessage = "登录信息不正确(userID)";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证分页数据
        /// </summary>
        public bool IsValidPagingInfo(out string errorMessage)
        {
            errorMessage = "";
            if (CommUtil.isEmpty(this.paging))
            {
                errorMessage = "分页信息不正确(paging)";
                return false;
            }

            if (this.paging.pageNumber <= 0)
            {
                errorMessage = "分页信息不正确(pageNumber)";
                return false;
            }

            if (this.paging.pageSize <= 0)
            {
                errorMessage = "分页信息不正确(pageSize)";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证渠道用户
        /// </summary>
        public bool IsValidChannelUserInfo(out string errorMessage)
        {
            errorMessage = "";
            if (CommUtil.isEmpty(this.userCookie))
            {
                errorMessage = "登录信息不正确(userCookie)";
                return false;
            }

            if (CommUtil.isEmpty(this.userCookie.userID))
            {
                errorMessage = "登录信息不正确(userID)";
                return false;
            }

            return true;
        }
    }
}
