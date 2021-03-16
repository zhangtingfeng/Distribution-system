using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.Entity;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web_Service.Api.Order
{
    public class UserOrdersAdminController : BasicController
    {
        [HttpPost]
        /// <summary>
        /// Post 获取订单列表
        /// </summary>
        public ResultAPIModel<List<UserOrdersAPIModel.UserOrderListReturn>> Post(ResultAPIModel<UserOrdersAPIModel.UserOrderListSearch> m)
        {
            string apiUrl = "post api/Order/UserOrders";
            var result = new ResultAPIModel<List<UserOrdersAPIModel.UserOrderListReturn>>();

            try
            {
                result.data = new List<UserOrdersAPIModel.UserOrderListReturn>();

                string msg = "";
                if (!m.IsValidUserInfo(out msg)
                    || !m.IsValidPagingInfo(out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                #region 业务数据查询
                var searchModel = new InsuranceOrderInfoExt
                {
                    InsuranceOrderTimeBegin = m.data.InsuranceOrderTimeBegin,
                    InsuranceOrderTimeEnd = m.data.InsuranceOrderTimeEnd,
                    CarMasterName = m.data.CarMasterName,
                    CarNumber = m.data.CarNumber,
                    InsuranceProvince = m.data.InsuranceProvince,
                    InsuranceCity = m.data.InsuranceCity,
                    InsuranceCompany = m.data.InsuranceCompany,
                    InsuranceOrderStatus = m.data.InsuranceOrderStatus,
                    //VipAmountStatuText = m.data.VipAmountStatuText,
                    FullTextSearch = m.data.FullTextSearch,
                    PageNumber = m.paging.pageNumber,
                    PageSize = m.paging.pageSize,
                    UserID = m.userCookie.userID,
                    VipCarID = m.data.VipCarID,
                    PendingItem = m.data.PendingItem
                };

                #region 验证用户
                var userInfo = new TUserBC().SelectByPrimaryKey(m.userCookie.userID);
                if (Check.getIns().isEmpty(userInfo)
                    || userInfo.IsDelete != 0)
                {
                    result.resultMessage.errorMessage = "用户信息错误";
                    return result;
                }
                #endregion

                var orderInfos = (new InsuranceOrderBC()).GetInsuranceOrderInfos(searchModel);

                //组织数据
                var dataCount = 0;
                if (orderInfos != null && orderInfos.Count > 0)
                {
                    dataCount = orderInfos.First().DataCount;
                    foreach (var data in orderInfos)
                    {
                        var model = new UserOrdersAPIModel.UserOrderListReturn();
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