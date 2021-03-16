using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.Entity;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web_Service.Api.Insurance
{
    public class CarInsureQueryCarController : BasicController
    {
        [HttpPost]
        public ResultAPIModel<List<InsureQueryCarInfoAPIModel>> Post(ResultAPIModel<InsureQueryCarInfoAPIModel> m)
        {
            string apiUrl = "post api/Insurance/CarInsureQueryCar/Post";
            var result = new ResultAPIModel<List<InsureQueryCarInfoAPIModel>>
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

                result.data = new List<InsureQueryCarInfoAPIModel>();

                #region 业务数据查询

                var searchModel = new CarInsureQueryCarInfoExt
                {
                    BeginTime = m.data.CreateStartTime,
                    EndTime = m.data.CreateEndTime,
                    QouteBeginTime = m.data.QouteBeginTime,
                    QouteEndTime = m.data.QouteEndTime,
                    desc = m.data.desc,
                    UserID = m.data.UserID,
                    CarMasterName = m.data.CarMasterName,
                    CarNumber = m.data.CarNumber,
                    
                    CarIdentifiedCode = m.data.CarIdentifiedCode,
                    InsureEndDate = m.data.InsureEndDate,
                    SortColumn = m.data.SortColumn,
                    SortDirection = m.data.SortDirection,
                    PageSize = m.paging.pageSize,
                    PageNumber = m.paging.pageNumber
                };


                var orderInfos = (new CarInsureQueryCarBC()).GetCarInsureQueryCarInfos(searchModel);
                var rowNumber = 0;
                if (orderInfos != null && orderInfos.Count > 0)
                {
                    rowNumber = orderInfos.First().DataCount;
                    foreach (var t in orderInfos)
                    {
                        result.data.Add(new InsureQueryCarInfoAPIModel
                        {
                            CarInsureQueryCarID = t.CarInsureQueryCarID,
                            ID = t.ID,
                            UserID = t.UserID,
                            CreateTime = t.CreateTime.Value,
                            CarMasterName = t.CarMasterName,
                            CarNumber = t.CarNumber,
                            CarIdentifiedCode = t.CarIdentifiedCode,
                            InsureEndDate = t.InsureEndDate,
                            ChannelNumber = t.ChannelNumber,
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

        /// <summary>
        /// 根据ID获取日志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultAPIModel<InsureQueryCarInfoAPIModel> GetActionRecordByID(string id)
        {
            string apiUrl = "get api/Insurance/CarInsureQueryCar";
            var result = new ResultAPIModel<InsureQueryCarInfoAPIModel>();
            try
            {
                result.data = new InsureQueryCarInfoAPIModel();

                if (string.IsNullOrEmpty(id))
                {
                    result.resultMessage = new ResultMessageAPIModel()
                    {
                        code = (int)ResultMessageAPIModel.Codes.fail,
                        errorMessage = "无效的查询条件"
                    };
                }

                var item = new CarInsureQueryCarBC().SelectByPrimaryKey(id);

                if (item != null)
                {
                    string strCityName = "";
                    if (!Check.getIns().isEmpty(item.RunCity))
                    {
                        var itemCity = new InsuranceCityBC().SelectByPrimaryKey(item.RunCity);
                        strCityName = itemCity != null ? itemCity.CityNameCN : "";
                    }

                    result.data = new InsureQueryCarInfoAPIModel
                    {
                        CarInsureQueryCarID = item.CarInsureQueryCarID,
                        CreateTime = item.CreateTime.Value,
                        CarMasterName = item.CarMasterName,
                        CarNumber = item.LicenseNo,
                        CarIdentifiedCode = item.FrameNo,
                        PurchasePrice = item.PurchasePrice.toString(),
                        VehicleStyleDesc = item.VehicleStyleDesc,
                        VehicleModelListID = item.VehicleModelListID,
                        BrandName = item.BrandName,
                        EnrollDate = item.EnrollDate,
                        EngineNo = item.EngineNo,
                        RunCity = item.RunCity,
                        RunCityName = strCityName
                        //InsureEndDate = item.InsureBizStartDate.t.InsureEndDate,
                        //ChannelNumber = t.ChannelNumber,
                    };
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
