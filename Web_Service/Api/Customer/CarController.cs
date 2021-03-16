using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Threading.Tasks;
using Web_Service.Common;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Order;
using InsureApi.Common.Common;

namespace Web_Service.Api.Customer
{
    public class CarController : BasicController
    {
        /// <summary>
        ///     查詢投保车辆信息
        /// </summary>
        /// <param name="insureID">投保ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Customer/InsuredCar/Car/{insureID}")]
        public ResultAPIModel<UserCustomerCarAPIModel> QueryInsuredCar(string insureID)
        {
            string apiUrl = "get api/Customer/InsuredCar/Car/{insureID}";
            var result = new ResultAPIModel<UserCustomerCarAPIModel>();

            try
            {
                string outMsg = "";
                //result = new VipCarBC().GetInsuredCar(insureID, out outMsg);
                if (!Check.getIns().isEmpty(outMsg))
                    result.resultMessage = new ResultMessageAPIModel
                    {
                        code = (int)ResultMessageAPIModel.Codes.fail,
                        errorMessage = outMsg
                    };
            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    message = ex.Message
                };
            }

            return result;
        }

        //get 根据车牌和userID查询车辆信息
        [HttpGet]
        [Route("api/Customer/Car/{carNumber}/{userID}")]
        public async Task<ResultAPIModel<UserCustomerCarAPIModel>> QueryCar(string carNumber, string userID)
        {
            const string apiUrl = "get api/Customer/{CarNumber}/{UserID}";
            var result = new ResultAPIModel<UserCustomerCarAPIModel>();

            try
            {
                result.data = new UserCustomerCarAPIModel();

                //获取车辆数据
                if (Check.getIns().isEmpty(carNumber, userID)
                    || carNumber.Length != 7)
                {
                    result.resultMessage.errorMessage = "查询信息不正确";
                    return result;
                }

                //验证UserID
                string msg;
                if (!IsValidUserID(userID, out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                ////查询数据库，是否有该辆车
                //var vipCarData = (new VipCarBC()).SelectCarAndCustomer(carNumber, userID);
                //if (!Check.getIns().isEmpty(vipCarData)
                //    && vipCarData.Count > 0)
                //{
                //    result.data.UserCustomerID = vipCarData[0].UserCustomerID;
                //    result.data.CustomerName = vipCarData[0].CustomerName;
                //    result.data.CarMasterName = vipCarData[0].CarMasterName;
                //    result.data.CarBrand = vipCarData[0].CarBrand;
                //    result.data.CarIdentifiedCode = vipCarData[0].CarIdentifiedCode;
                //    result.data.EngineNumber = vipCarData[0].EngineNumber;
                //    result.data.CarRegisteredDate = vipCarData[0].CarRegisteredDate;
                //    result.data.VehicleAge = DateTime.Now.Year - result.data.CarRegisteredDate.Value.Year;
                //    result.data.CarPurchasePrice = vipCarData[0].CarPurchasePrice;
                //    result.data.InsureEndDate = vipCarData[0].InsureEndDate;
                //    result.data.importFlag = CarHelper.TranslateImportFlag(vipCarData[0].ImportFlag);
                //    result.data.UserCustomerID = vipCarData[0].UserCustomerID;
                //    result.data.CarMasterIDNum = vipCarData[0].IDCard;
                //    result.data.VipCarID = vipCarData[0].VipCarID;
                //    result.data.VehicleModelListID = vipCarData[0].VehicleModelListID;
                //    result.data.InvoiceDateTime = vipCarData[0].InvoiceDateTime;
                //    result.data.RunCity = vipCarData[0].RunCity;
                //    result.data.RunCityName = new InsuranceCityBC().GetCityNamebyCityID(vipCarData[0].RunCity);
                //    //产品型号
                //    if (Check.getIns().isEmpty(result.data.VehicleModelListID))
                //        return result;
                //    var vehicleModel = new VehicleModelListBC().SelectByPrimaryKey(result.data.VehicleModelListID);
                //    result.data.VehicleModel = Check.getIns().isEmpty(vehicleModel) ? "" : Check.getIns().isEmpty(vehicleModel.VehicleStyleDesc) ? vehicleModel.BrandName : vehicleModel.VehicleStyleDesc;
                //    return result;
                //}

                ////车牌查询车辆数据
                //var carData = await CarHelper.getIns().queryCarInfo(carNumber);
                //if (Check.getIns().isEmpty(carData))
                //{
                //    //result.resultMessage.errorMessage = "无法查询到车辆信息";
                //    return result;
                //}

                //result.data.CarMasterName = carData.CarMasterName;
                //result.data.CarBrand = carData.CarBrand;
                //result.data.CarIdentifiedCode = carData.CarIdentifiedCode;
                //result.data.EngineNumber = carData.EngineNumber;
                //result.data.CarRegisteredDate = carData.CarRegisteredDate;
                //result.data.RunCity = carData.RunCity;
                //result.data.VehicleAge = carData.CarRegisteredDate.HasValue
                //    ? DateTime.Now.Year - carData.CarRegisteredDate.Value.Year
                //    : -1;
                //result.data.CarPurchasePrice = carData.CarPurchasePrice;
                //result.data.importFlag = CarHelper.TranslateImportFlag(carData.importFlag);
                //result.data.InsureEndDate = CarHelper.getIns().getCarInsureEndDate(carNumber);

            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    message = ex.Message
                };
            }

            return result;
        }

        // Get: 根据车辆ID获取车主和车辆信息
        [HttpGet]
        public ResultAPIModel<UserCustomerCarAPIModel> GetCarInfo(string id)
        {
            const string apiUrl = "get api/Customer/{VipCarID}";
            var result = new ResultAPIModel<UserCustomerCarAPIModel>();

            try
            {
                //result = new VipCarBC().SelectCarInfo(id);
                if (!Check.getIns().isEmpty(result) && Check.getIns().isEmpty(result.data))
                    result.data.importFlag = CarHelper.TranslateImportFlag(result.data.importFlag);
            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    message = ex.Message
                };
            }

            return result;
        }

    }
}