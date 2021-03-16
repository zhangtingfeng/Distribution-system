using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using InsureApi.Common.Common;

namespace Web_Service.Common
{
    public class OrderHelper
    {
        private static OrderHelper _instance = new OrderHelper();
        public static OrderHelper getIns() { return _instance; }

        /// <summary>
        /// 核保
        /// </summary>
        public string GoHeBaoAsync(string insuranceOrderID)
        {
            string errorMessage = "";

            //try
            //{
            //    #region 订单
            //    var insuranceOrderInfo = new InsuranceOrderBC().SelectByPrimaryKey(insuranceOrderID);
            //    if (Check.getIns().isEmpty(insuranceOrderInfo)
            //        || insuranceOrderInfo.IsDelete != 0)
            //    {
            //        errorMessage = "无订单数据";
            //        return errorMessage;
            //    }
            //    #endregion

            //    //人保重新核保
            //    callQybCasperOrder(insuranceOrderID);
            //}
            //catch (Exception ex)
            //{
            //    LogController.writeErrorLog(ex, "OrderHelper");
            //}

            return errorMessage;
        }

        private void callQybCasperOrder(string OrderID)
        {
            //Task.Run(() =>
            //{
            //    try
            //    {
            //        RenBaoCasperApi api = new RenBaoCasperApi();
            //        api.order(OrderID);
            //    }
            //    catch (Exception ex)
            //    {
            //        LogController.errorLog(ex, "OrderHelper");
            //    }
            //});
        }
    }
}