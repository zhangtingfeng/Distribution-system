using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Web_Service.Common.Config;
using ClassInsureBLL.Model_Public;
using InsureApi.WebApi.Model.Order;
using InsureApi.Common.Common;

namespace Web_Service.Common
{
    public class CarHelper
    {
        private static CarHelper _instance = new CarHelper();
        public static CarHelper getIns() { return _instance; }


        /// <summary>
        /// 验证车辆信息
        /// </summary>
        public bool isValidCarInfo(UserCustomerCarAPIModel m)
        {
            return true;
        }

        /// <summary>
        /// 查询车辆信息
        /// </summary>
        public async Task<UserCustomerCarAPIModel> queryCarInfo(string carNumber)
        {
            //查询车辆基本信息
            try
            {
                ////车牌查询车辆信息接口
                //CarInfoApi api = new CarInfoApi(AppSettings.getIns().QybCarUrl);
                //var queryData = await api.getCarInfo(carNumber);

                //if (Check.getIns().isEmpty(queryData)
                //    || Check.getIns().isEmpty(queryData.data))
                //{
                //    return null;
                //}

                //var resultData = new UserCustomerCarAPIModel()
                //{
                //    CarMasterName = queryData.data.CarMasterName,
                //    CarBrand = queryData.data.CarBrand,
                //    CarIdentifiedCode = queryData.data.CarIdentifiedCode,
                //    EngineNumber = queryData.data.EngineNumber,
                //    //CarPurchasePrice = queryData.CarPurchasePrice,
                //    //VehicleCode = queryData.VehicleCode,
                //    //IsOwnerTransfer = queryData.IsOwnerTransfer,
                //    SeatCount = queryData.data.SeatCount.toString(),
                //    //DriveMileage = queryData.DriveMileage,
                //    //ExhaustScale = queryData.ExhaustScale,
                //    //importFlag = queryData.importFlag,
                //    //RunCity = queryData.Runcity
                //};

                //if (queryData.data.CarRegisteredDate.HasValue)
                //{
                //    resultData.CarRegisteredDate = queryData.data.CarRegisteredDate.Value;
                //}

                //return resultData;
                return null;
            }
            catch (Exception ex)
            {
                Insure.Common.debug_Log.Call_WriteLog("QybCar查询失败", "车辆查询-接口查询");
                Insure.Common.debug_Log.Call_WriteLog(ex, "车辆查询-接口查询");

                //LogController.netLog("QybCar查询失败", "车辆查询-接口查询");
                //LogController.errorLog(ex, "车辆查询-接口查询");
                return null;
            }
        }

        ///// <summary>
        ///// 查询车辆详细信息
        ///// </summary>
        //public async Task<VipCarInfo> queryCarDetailInfo(UserCustomerCarAPIModel m)
        //{
        //    CarQueryApi api = new CarQueryApi(AppSettings.getIns().QybCarUrl);
        //    var result = await api.QueryCarDetailInfo(m);
        //    if (!Check.getIns().isEmpty(result.resultMessage.errorMessage))
        //    {
        //        LogController.errorLog(result.resultMessage.errorMessage, "车辆查询-queryCarDetailInfo");
        //        return null;
        //    }
        //    return result.data;
        //}

        /// <summary>
        /// 获取投保起期
        /// </summary>
        public DateTime getCarInsureEndDate(string carNumber)
        {
            var result = DateTime.Now.AddDays(1);
            //非沪牌无法查询
            if (!carNumber.Contains("沪"))
            {
                return result;
            }

            try
            {
                //var api = new InsureApi.EctApi.WsdsApi();
                //var resultModel = api.getCarInfo(carNumber);

                //if (!Check.getIns().isEmpty(resultModel)
                //    && resultModel.InsureEndDate.HasValue)
                //{
                //    DateTime tInsureEndDate = resultModel.InsureEndDate.Value;
                //    while (tInsureEndDate.Subtract(DateTime.Now.Date).Days <= 0)
                //    {
                //        tInsureEndDate = tInsureEndDate.AddYears(1);
                //    }
                //    result = tInsureEndDate;
                //}
            }
            catch (Exception ex)
            {
                Insure.Common.debug_Log.Call_WriteLog(ex, "车辆查询-投保起期");
            }

            return result;
        }

        ///// <summary>
        ///// 接口 车型查询
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public async Task<dynamic> QueryCarvehicleModelList(dynamic m)
        //{
        //    CarQueryVehicleApi api = new CarQueryVehicleApi(AppSettings.getIns().QybCarUrl);
        //    var result = await api.QueryCarvehicleModelList(m);
        //    if (!Check.getIns().isEmpty(result.resultMessage.errorMessage))
        //    {
        //        LogController.errorLog(result.resultMessage.errorMessage, "车辆查询-QueryCarvehicleModelList");
        //        return null;
        //    }
        //    return result.data;
        //}

        /// <summary>
        /// 产地转换
        /// </summary>
        /// <param name="importFlag">The import flag.</param>
        /// <returns></returns>
        public static string TranslateImportFlag(string importFlag = "")
        {
            if (string.IsNullOrWhiteSpace(importFlag)) return "C";
            if (importFlag.IndexOf("进口", StringComparison.Ordinal) >= 0) return "A";
            if (importFlag.IndexOf("国产", StringComparison.Ordinal) >= 0) return "B";
            if (importFlag.IndexOf("合资", StringComparison.Ordinal) >= 0) return "C";
            return importFlag;
            //return importFlag.IndexOf("国产", StringComparison.Ordinal)>=0 ? "B" : "C";
        }

        ///// <summary>
        ///// 查询客户下车辆是否已经存在
        ///// </summary>
        ///// <param name="userID">客户ID</param>
        ///// <param name="carNumber">车牌</param>
        //public bool IsExistsCar(string userID, string carNumber)
        //{
        //    //获取VipCarID
        //    var dataList = new VipCarBC().SelectUserCustomerCar(userID, carNumber);

        //    return dataList.Count > 0;
        //}
    }
}