using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.Entity;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Insurance;
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
    public class InsuranceCityController : BasicController
    {
        [HttpGet]
        [Route("api/Insurance/city/InsuranceCity/{cityCode}")]
        public ResultAPIModel<InsuranceCityAPIModel> GetCity(string cityCode)
        {
            var result = new ResultAPIModel<InsuranceCityAPIModel>();
            try
            {
                if (Check.getIns().isEmpty(cityCode))
                {
                    result.resultMessage.message = "无效城市编码";
                    return result;
                }
                var info = new InsuranceCityBC().SelectByPrimaryKey(cityCode);
                if (info.IsDelete != 0)
                {
                    result.resultMessage.message = "无效城市编码";
                    return result;
                }
                result.data = new InsuranceCityAPIModel
                {
                    CityCode = cityCode,
                    CityName = info.CityShortName
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

        /// <summary>
        /// Get 投保城市 List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultAPIModel<List<InsuranceCityAPIModel>> GetInsuranceCity(string id)
        {
            string apiUrl = "API/Insurance/InsuranceCity/{id}";
            var result = new ResultAPIModel<List<InsuranceCityAPIModel>>();
            try
            {
                result.data = null;

                List<InsuranceCityAPIModel> rstList = new List<InsuranceCityAPIModel>();
                InsuranceCityBC cityBC = new InsuranceCityBC();

                List<InsuranceCityInfoExt> list = cityBC.GetInsureCityList();

                if (list != null && list.Count > 0)
                {
                    foreach (InsuranceCityInfoExt item in list)
                    {
                        InsuranceCityAPIModel obj = new InsuranceCityAPIModel();
                        Function.getIns().copyValue(item, obj, false);

                        rstList.Add(obj);
                    }
                }

                result.data = rstList;
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