using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Threading.Tasks;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model;
using InsureApi.BLL;
using InsureApi.Common.Common;

namespace Web_Service.Api.Insurance
{
    /// <summary>
    /// 车辆投保查询
    /// </summary>
    public class CarInsureQueryTaskController : BasicController
    {
        [HttpGet]
        public ResultAPIModel<CarInsuranceQueryTaskApiModel> Query(string id)
        {
            //"get api/Insurance/CarInsureQueryTask";
            var result = new ResultAPIModel<CarInsuranceQueryTaskApiModel>();
            try
            {
                result.data = new CarInsuranceQueryTaskApiModel();

                #region 获取数据
                var data = new CarInsuranceQueryTaskBC().SelectByPrimaryKey(id);
                if (Check.getIns().isEmpty(data))
                {
                    result.resultMessage.errorMessage = "没有查询到数据";
                    return result;
                }
                #endregion

                result.data = new CarInsuranceQueryTaskApiModel()
                {
                    QueryTaskID = data.CarInsureQueryHistoryID,
                    ReturnErrorCode = data.ReturnErrorCode,
                    FreeGivePriceXML = data.FreeGivePriceXML
                };
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