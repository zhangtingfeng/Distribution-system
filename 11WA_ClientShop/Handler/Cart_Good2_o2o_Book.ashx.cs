using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Cart_Good2_o2o_Book 的摘要说明
    /// </summary>
    public class Cart_Good2_o2o_Book : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                String strResult = "0";
                String strorderid = context.Request["orderid"];
                if (String.IsNullOrEmpty(strorderid)) return;

                EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order Model_tab_Order = BLL_tab_Order.GetModel(Int32.Parse(strorderid));
                string strDeliveryText = Model_tab_Order.DeliveryText;
                if (strDeliveryText.Length > 0)
                {
                    Model_tab_Order.isReceipt = true;//收获成功
                    BLL_tab_Order.Update(Model_tab_Order);
                    strResult = "1";
                }
                else
                {
                    strResult = "0";
                }
                string json = strResult;
                context.Response.Write(json);
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}