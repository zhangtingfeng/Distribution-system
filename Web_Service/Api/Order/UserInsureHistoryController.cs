using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Threading.Tasks;
using Web_Service.Common;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.User;
using InsureApi.Common.Common;
using InsureApi.BLL;
using InsureApi.WebApi.Model.Order;

namespace Web_Service.Api.Order
{
    /// <summary>
    /// 用户投保记录
    /// </summary>
    public class UserInsureHistoryController : BasicController
    {
        [HttpGet]
        public ResultAPIModel<UserInsureHistoryAPIModel> Query(string id)
        {
            string apiUrl = "get api/Order/UserInsureHistory";
            var result = new ResultAPIModel<UserInsureHistoryAPIModel>();
            try
            {
                result.data = new UserInsureHistoryAPIModel();

                #region 投保方案数据
                var carInsureQueryHistoryInfo = new CarInsureQueryHistoryBC().SelectByPrimaryKey(id);
                if (Check.getIns().isEmpty(carInsureQueryHistoryInfo)
                    || carInsureQueryHistoryInfo.IsDelete != 0)
                {
                    result.resultMessage.errorMessage = "无投保方案数据";
                    return result;
                }
                Function.getIns().copyValue(carInsureQueryHistoryInfo, result.data.InsureQueryHistoryInfo);
                #endregion

                #region 投保城市
                var CarInfo = new CarInsureQueryCarBC().SelectByPrimaryKey(carInsureQueryHistoryInfo.CarInsureQueryCarID);
                if (CarInfo != null && !Check.getIns().isEmpty(CarInfo.RunCity))
                {
                    var CityInfo = new InsuranceCityBC().SelectByPrimaryKey(CarInfo.RunCity);
                    if (!Check.getIns().isEmpty(CityInfo))
                    {
                        result.data.CityName = CityInfo.CityNameCN;
                    }
                }
                #endregion

                #region 代理人数据
                //var userInfo = new TUserBC().SelectByPrimaryKey(carInsureQueryHistoryInfo.UserID.toString());
                //if (!Check.getIns().isEmpty(userInfo))
                //{
                //    result.data.UserID = userInfo.UserID;
                //    result.data.UserName = userInfo.UserName + (userInfo.IsDelete == 0 ? "" : "（已禁用）");
                //    result.data.UserPhone = userInfo.UserPhone;
                //}
                #endregion

                #region 车辆数据
                //var vipCarInfo = new VipCarBC().SelectByPrimaryKey(carInsureQueryHistoryInfo.VipCarID.toString());
                if (!Check.getIns().isEmpty(CarInfo))
                {
                    DateTime? dtEndDate = CarInfo.InsureBizStartDate.toDateTime();
                    dtEndDate = (dtEndDate != null && dtEndDate.Value > DateTime.MinValue) ? dtEndDate.Value.AddYears(1) : new DateTime(1, 1, 1);
                    result.data.CarInfo = new UserCustomerCarAPIModel()
                    {
                        CarMasterName = CarInfo.CarMasterName,
                        CarNumber = CarInfo.LicenseNo + (CarInfo.IsDelete == 0 ? "" : "（已禁用）"),
                        CarBrand = CarInfo.BrandName,
                        CarIdentifiedCode = CarInfo.FrameNo,
                        EngineNumber = CarInfo.EngineNo,
                        CarPurchasePrice = CarInfo.PurchasePrice.Value,
                        CarRegisteredDate = CarInfo.EnrollDate.toDateTime(),
                        InsureEndDate = dtEndDate,
                        //DrivingCertificate = CarInfo.DrivingCertificate
                    };
                }
                #endregion

                #region 客户数据
                //var UserCustomerInfo = new TUserBC().SelectUserCustomer(vipCarInfo.UserCustomerID.toString());
                //if (!Check.getIns().isEmpty(UserCustomerInfo))
                //{
                //    result.data.VipID = UserCustomerInfo.ID;
                //    result.data.VipName = UserCustomerInfo.CustomerName;
                //    result.data.VipPhone = UserCustomerInfo.CustomerPhone;
                //}
                #endregion

                //检索数据库获取报价数据
                result.data.InsureList = InsureHistoryHelper.getIns().queryCarInsureQueryInfo(carInsureQueryHistoryInfo,false).CarInsuranceDataList;
                if(result.data.InsureList.Count > 0) result.data.InsureList.OrderBy(O => O.CompanyName);
            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    message = ex.Message.ToString()
                };
            }
            return result;
        }
    }
}