using InsureApi.BLL;
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
    public class InsureHistoryController : BasicController
    {
        [HttpPost]
        public ResultAPIModel<int> Post(ResultAPIModel<InsureHistoryApiModel> m)
        {
            string apiUrl = "post api/Insurance/InsureHistory/Post";
            var result = new ResultAPIModel<int>
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

                result.data = 0;

                #region 业务数据查询

                var searchModel = new CarInsureQueryHistoryInfoExt
                {
                    BeginTime = m.data.CreateStartTime,
                    EndTime = m.data.CreateEndTime,
                    CarMasterName = m.data.CarMasterName,
                    CarNumber = m.data.LicenseNo,
                    InsuranceProvince = m.data.InsuranceProvince,
                    InsuranceCity = m.data.InsuranceCity
                };

                result.data = (new CarInsureQueryHistoryBC()).Update(searchModel);
                
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