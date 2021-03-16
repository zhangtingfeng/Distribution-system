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
using Web_Service.Common.Config;

namespace Web_Service.Api.Insurance
{
    public class InsuranceCompanyController : BasicController
    {
        // Get: 获取保险公司列表
        [HttpGet]
        public ResultAPIModel<List<InsuranceCompanyAPIModel>> Get()
        {
            string apiUrl = "post api/Insurance/InsuranceCompany";
            var result = new ResultAPIModel<List<InsuranceCompanyAPIModel>>();
            result.data = new List<InsuranceCompanyAPIModel>();

            try
            {
                //检索保险公司数据
                var bc = new InsuranceCompanyBC();
                var datas = bc.getInsuranceCompany("上海");

                foreach (var data in datas)
                {
                    //数据Copy
                    var model = new InsuranceCompanyAPIModel();
                    Function.getIns().copyValue(data, model);
                    result.data.Add(model);
                }

                return result;
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

        [HttpGet]
        [Route("api/Insurance/InsuranceCompany/{id}/{cityList}")]
        public ResultAPIModel<List<InsuranceCompanyAPIModel>> GetQuoteCompany(string id, string cityList)
        {
            var result = new ResultAPIModel<List<InsuranceCompanyAPIModel>>();
            try
            {
                //获取用户
                var userData = new TUserBC().SelectByPrimaryKey(id);
                var channelCompanys = new List<InsuranceCompanyAPIModel>();

                if (Check.getIns().isEmpty(userData.ChannelNumber))
                {
                    result.resultMessage.errorMessage = "渠道号不存在";
                    return result;
                }

                if (Check.getIns().isEmpty(cityList))
                {
                    result.resultMessage.errorMessage = "行驶城市为空";
                    return result;
                }
                //获取用户对应渠道的保险公司
                channelCompanys = GetChannelCompany(userData.ChannelNumber, cityList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList());
                //投保方案选择保险公司
                result.data = channelCompanys;
                return result;


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

        //Post 获取报价保险公司
        [HttpPost]
        public ResultAPIModel<List<InsuranceCompanyAPIModel>> GetQuoteCompany(ResultAPIModel<InsuranceCompanyAPIModel.SubmitData> m)
        {
            const string apiUrl = "get api/Insurance/InsuranceCompany/";
            var result = new ResultAPIModel<List<InsuranceCompanyAPIModel>>();

            try
            {
                result.data = new List<InsuranceCompanyAPIModel>();

                if (Check.getIns().isEmpty(m) || Check.getIns().isEmpty(m.data))
                {
                    result.resultMessage.errorMessage = "数据为空";
                    return result;
                }

                //获取用户
                var userData = new TUserBC().SelectByPrimaryKey(m.userCookie.userID);
                var channelCompanys = new List<InsuranceCompanyAPIModel>();

                if (userData == null || Check.getIns().isEmpty(userData.ChannelNumber))
                {
                    result.resultMessage.errorMessage = "渠道号不存在";
                    return result;
                }

                //有投保方案
                if (!Check.getIns().isEmpty(m.data.CarInsureQueryHistoryID))
                {
                    //获取投保方案
                    var carInsureQueryHistoryData =
                        new CarInsureQueryHistoryBC().SelectByPrimaryKey(m.data.CarInsureQueryHistoryID);

                    //if (Check.getIns().isEmpty(carInsureQueryHistoryData.RunCity))
                    //{
                    //    result.resultMessage.errorMessage = "行驶城市为空";
                    //    return result;
                    //}
                    //获取用户对应渠道的保险公司
                    //channelCompanys = GetChannelCompany(userData.ChannelNumber, carInsureQueryHistoryData.RunCity, userData.UserType == (int)AppEnum.UserTypeEnum.IndividualCustomers);
                    //报价列表保险公司
                    //result.data = GetQuotePrecisionSelectCompany(carInsureQueryHistoryData, channelCompanys);
                    return result;
                }
                //#46 多城市支持 2015-11-30 denglei begin
                if (Check.getIns().isEmpty(m.data.RunCity))
                {
                    result.resultMessage.errorMessage = "行驶城市为空";
                    return result;
                }
                //#46 end
                //获取用户对应渠道的保险公司
                //channelCompanys = GetChannelCompany(userData.ChannelNumber, m.data.RunCity, userData.UserType == (int)AppEnum.UserTypeEnum.IndividualCustomers);
                //投保方案选择保险公司
                result.data = channelCompanys;
                return result;
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

        // Get: 获取可投保的保险公司列表
        //[HttpGet]
        //public ResultAPIModel<List<InsuranceCompanyAPIModel>> GetCompany(string type, string id,string runCity="")
        //{
        //    string apiUrl = "get api/Insurance/InsuranceCompany/{type}/{id}";
        //    var result = new ResultAPIModel<List<InsuranceCompanyAPIModel>>();

        //    try
        //    {
        //        result.data = new List<InsuranceCompanyAPIModel>();

        //        var pcCompanys = new SettingBC().getSettingByType("PCWebCanQuoteCompany");
        //        var wechatCompanys = new SettingBC().getSettingByType("WeChatCanQuoteCompany");

        //        switch (type.ToUpper())
        //        {
        //            //WeChat、App
        //            case "QUOTEPLAN":
        //                result.data = GetQuotePlanSelectCompany(wechatCompanys);
        //                break;
        //            case "QUOTEPRECISION":
        //                result.data = GetQuotePrecisionSelectCompany(id, wechatCompanys);
        //                break;
        //            //PC
        //            case "PCQUOTEPLAN":
        //                result.data = GetQuotePlanSelectCompany(pcCompanys);
        //                break;
        //            case "PCQUOTEPRECISION":
        //                result.data = GetQuotePrecisionSelectCompany(id, pcCompanys);
        //                break;
        //            case "CHANNEL":
        //                result.data = GetChannelCompany(id, runCity);
        //                break;
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.resultMessage = new ResultMessageAPIModel()
        //        {
        //            code = (int)ResultMessageAPIModel.Codes.fail,
        //            message = ex.Message.ToString()
        //        };
        //    }

        //    return result;
        //}

        private List<InsuranceCompanyAPIModel> GetChannelCompany(string channelNumber, List<string> runCity)
        {
            var result = new List<InsuranceCompanyAPIModel>();
            //var list = new TChannelConfigBC().SelectTheChannelCompany(channelNumber, runCity);
            //if (list != null && list.Count > 0)
            //{
            //    foreach (var item in list)
            //    {
            //        var model = new InsuranceCompanyAPIModel();
            //        Function.getIns().copyValue(item, model);
            //        result.Add(model);
            //    }
            //}
            return result;
        }

        /// <summary>
        /// 获取投保方案可选择保险公司
        /// </summary>
        /// <returns></returns>
        //private List<InsuranceCompanyAPIModel> GetQuotePlanSelectCompany(List<SettingInfo> companys)
        //{
        //    var result = new List<InsuranceCompanyAPIModel>();

        //    //检索保险公司数据
        //    var bc = new InsuranceCompanyBC();
        //    var datas = bc.getInsuranceCompany("上海");

        //    foreach (var data in datas)
        //    {
        //        if (companys.Count((e) => { return e.SettingValue.ToUpper().Equals(data.CompanyCode.ToUpper()); }) <= 0)
        //        {
        //            continue;
        //        }

        //        //数据Copy
        //        var model = new InsuranceCompanyAPIModel();
        //        Function.getIns().copyValue(data, model);
        //        result.Add(model);
        //    }

        //    return result;
        //}

        /// <summary>
        /// 获取投保方案可选择保险公司
        /// </summary>
        /// <param name="isRemoveMemo">是否移除网销/传统</param>
        /// <returns></returns>
        private List<InsuranceCompanyAPIModel> GetQuotePrecisionSelectCompany(CarInsureQueryHistoryInfo carInsureQueryHistoryData, List<InsuranceCompanyAPIModel> companys)
        {
            var result = new List<InsuranceCompanyAPIModel>();

            //检索保险公司数据
            //var datas = new CarInsureQueryCompanyHistoryBC().getInfos(carInsureQueryHistoryData.CarInsureQueryHistoryID, Check.getIns().isEmpty(carInsureQueryHistoryData.RunCity) ? "上海" : carInsureQueryHistoryData.RunCity);

            //foreach (var data in datas)
            //{
            //    if (companys.Count((e) => { return e.CompanyCode.ToUpper().Equals(data.CompanyCode.ToUpper()); }) <= 0)
            //    {
            //        continue;
            //    }

            //    //数据Copy
            //    var model = new InsuranceCompanyAPIModel();
            //    Function.getIns().copyValue(data, model);
            //    result.Add(model);
            //}

            return result;
        }
    }
}