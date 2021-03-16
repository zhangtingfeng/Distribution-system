using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Data;
using System.Transactions;
using System.Diagnostics;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.User;
using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.Entity;

namespace Web_Service.Api.User
{
    public class UsersController : BasicController
    {
        #region 根据帐号获取用户信息
        /// <summary>
        /// 根据帐号获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultAPIModel<UserManageAPIModel.UserInfo> GetUserInfoByUserID(string id)
        {
            string apiUrl = "get api/User/Users";
            var result = new ResultAPIModel<UserManageAPIModel.UserInfo>();
            try
            {
                result.data = new UserManageAPIModel.UserInfo();
                string outMsg ="";
                TUserBC userBc = new TUserBC();
                //result.data = userBc.GetCustomerAuthenticationInfo(id, out outMsg);
                if (!Check.getIns().isEmpty(outMsg))
                {
                    result.resultMessage.errorMessage = outMsg;
                }
            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    errorMessage = ex.Message.ToString()
                };
            }

            return result;
        }
        #endregion

        #region 查询所有用户
        /// <summary>
        /// 查询所有用户
        /// </summary>
        [HttpPost]
        public ResultAPIModel<List<UserManageAPIModel.UserList>> Post(ResultAPIModel<UserManageAPIModel.UserList> m)
        {
            string apiUrl = "post api/User/Users";
            var result = new ResultAPIModel<List<UserManageAPIModel.UserList>>();
            try
            {
                result.data = new List<UserManageAPIModel.UserList>();

                string msg = "";
                if (!m.IsValidUserInfo(out msg)
                    || !m.IsValidPagingInfo(out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                //页数从1开始
                if (m.paging.pageNumber <= 0)
                {
                    m.paging.pageNumber = 1;
                }

                #region 业务数据查询

                TUserInfoExt searchModel = new TUserInfoExt
                {
                    UserType = m.data.UserType,
                    //IsIDCard = m.data.IsIDCard,
                    IsWeChat = m.data.IsWeChat,
                    SearchKey = m.data.SearchKey,
                    SalesRangeStart = m.data.SalesRangeStart,
                    SalesRangeEnd = m.data.SalesRangeEnd,
                    BusinessSources = m.data.BusinessSources,
                    PendingItem = m.data.PendingItem,
                    IsDelete2 = m.data.IsDelete2,
                    DevelopmentCode = new TUserBC().GetDevelopmentCodeByUserID(m.userCookie.userID),
                    PageNumber = m.paging.pageNumber,
                    PageSize = m.paging.pageSize
                };

                //如果是管理员UserId不作处理
                if (m.userCookie.userID.ToUpper().Trim() == "BC405B083F2E41DD838E9BF77517775A")
                {
                    searchModel.UserID = null;
                }

                List<TUserInfoExt> userInfos = (new TUserBC()).SelUsersInfos(searchModel);

                //组织数据
                var dataCount = 0;
                if (userInfos != null && userInfos.Count > 0)
                {
                    dataCount = userInfos.First().DataCount;
                    foreach (var data in userInfos)
                    {
                        var model = new UserManageAPIModel.UserList();
                        Function.getIns().copyValue(data, model);
                        result.data.Add(model);
                    }
                }

                result.paging = new PagingAPIModel()
                {
                    dataCount = dataCount,
                    pageSize = m.paging.pageSize,
                    pageNumber = m.paging.pageNumber
                };
                #endregion
            }
            catch (Exception ex)
            {
                Debug.Write(ex.StackTrace);
                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    errorMessage = ex.Message.ToString()
                };
            }

            return result;
        }
        #endregion
    }
}