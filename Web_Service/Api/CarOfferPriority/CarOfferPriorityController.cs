using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Transactions;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Order;
using InsureApi.BLL;
using InsureApi.Common.Common;

namespace Web_Service.Api
{
    public class CarOfferPriorityController : BasicController
    {
        [HttpPost]
        public ResultAPIModel<bool> Post()
        {
            var result = new ResultAPIModel<bool>();

            result.data = false;
            try
            {
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
        /// 获取所有的保险公司
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultAPIModel<List<InsuranceCompanyAPIModel>> GetCompany()
        {
            string apiUrl = "API/CarOfferPriority/CarOfferPriority";
            var result = new ResultAPIModel<List<InsuranceCompanyAPIModel>>();
            result.data = new List<InsuranceCompanyAPIModel>();
            try
            {
                var list = new InsuranceCompanyBC().SelectAll();

                foreach (var data in list)
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
                    errorMessage = ex.Message.ToString()
                };
            }

            return result;
        }

    }
}