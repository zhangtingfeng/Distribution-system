using InsureApi.BLL;
using InsureApi.Entity;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web_Service.Api.Insurance
{
    public class SelAllWaitQuoteListController : BasicController
    {
        [HttpPost]
        public ResultAPIModel<List<UserOrderQuoteApiModel>> Post(ResultAPIModel<UserOrderQuoteApiModel> m)
        {
            string apiUrl = "post api/Insurance/SelAllWaitQuoteList/Post";
            var result = new ResultAPIModel<List<UserOrderQuoteApiModel>>
            {
                resultMessage = new ResultMessageAPIModel
                {
                    errorMessage = ""
                }
            };
            try
            {
                string msg = "";
                if (!m.userCookie.IsValidInfo(out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                if (!m.paging.IsValidInfo(out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                //页数从1开始
                if (m.paging.pageNumber <= 0)
                {
                    m.paging.pageNumber = 1;
                }

                result.data = new List<UserOrderQuoteApiModel>();

                #region 业务数据查询

                var searchModel = new CarInsuranceQueryTaskInfoExt
                {
                    BeginTime = m.data.CreateStartTime,
                    EndTime = m.data.CreateEndTime,
                    CarInsureQueryCarID = m.data.CarInsureQueryCarID,
                    CarMasterName = m.data.CarMasterName,
                    LicenseNo = m.data.LicenseNo,
                    CarInsureQueryHistoryID = m.data.CarInsureQueryHistoryID,
                    InsuranceProvince = m.data.InsuranceProvince,
                    InsuranceCity = m.data.InsuranceCity,
                    CompanyID = m.data.InsuranceCompany,
                    PageNumber = m.paging.pageNumber,
                    PageSize = m.paging.pageSize
                };

                var orderInfos = (new CarInsuranceQueryTaskBC()).GetInsuranceQueryTaskInfoAll(searchModel);
                var rowNumber = 0;
                if (orderInfos != null && orderInfos.Count > 0)
                {
                    rowNumber = orderInfos.First().DataCount;
                    foreach (var t in orderInfos)
                    {
                        result.data.Add(new UserOrderQuoteApiModel
                        {
                            CarInsuranceQueryTaskID = t.CarInsuranceQueryTaskID,
                            CreateTime = t.CreateTime.Value,
                            CarMasterName = t.CarMasterName,
                            LicenseNo = t.LicenseNo,
                            CarInsureQueryHistoryID = t.CarInsureQueryHistoryID,
                            InsuranceProvince = t.InsuranceProvince,
                            ProvinceName = t.ProvinceName,
                            InsuranceCity = t.InsuranceCity,
                            CityName = t.CityName,
                            InsuranceCompany = t.CompanyName,
                            CompanyCode = t.CompanyCode,
                            IsFinished = t.IsFinished,
                            ReturnErrorCode = t.ReturnErrorCode,
                            ChannelID = t.ChannelID,
                            ChannelName = t.ChannelName
                        });
                    }
                }

                result.paging = new PagingAPIModel()
                {
                    dataCount = rowNumber,
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