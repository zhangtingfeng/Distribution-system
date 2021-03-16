using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.User;
using InsureApi.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using System.Web.Security;
using Web_Service.Common.Config;

namespace Web_Service.Api.User
{
    //[CustomAuthorizationFilter]
    public class UserInfoController : BasicController
    {
        /// <summary>
        /// Get 用户信息
        /// </summary>
        [HttpGet]
        public ResultAPIModel<UserAPIModel> Get(string id)
        {
            const string apiUrl = "API/User/UserInfo/{UserID} or {UserSharedID}";
            var result = new ResultAPIModel<UserAPIModel>();
            try
            {
                if (Check.getIns().isEmpty(id))
                {
                    result.resultMessage.errorMessage = "用户ID不正确";
                    return result;
                }
                //责任链模式，传递请求一直到找到该用户 
                TUserBC tuserBc = new TUserBC();
                string errorMsg = "";
                //result.data = tuserBc.GetCustomer(id, out errorMsg);
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    result.resultMessage.errorMessage = errorMsg;
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

    }
}
