using InsureApi.Common.Common;
using InsureApi.WebApi.Model;
using InsureApi.WebApi.Model.Common;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web_Service.Hubs;

namespace Web_Service.Api
{
    public class SignalRController : BasicController
    {
        [HttpPost]
        public ResultAPIModel<bool> sendMessge(ResultAPIModel<SignalRCApiModel.FormData> m)
        {

            string apiUrl = "post api/Message/SignalR";
            var result = new ResultAPIModel<bool>();
            try
            {
                result.data = false;

                if (Check.getIns().isEmpty(m.data))
                {
                    result.resultMessage.message = "发送数据不正确";
                    return result;
                }

                ChatHubHelper.getIns().sendMessage(m.data.UserID, m.data.Title, m.data.Content, m.data.Url);
                //var _hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                //_hubContext.Clients.All.alertNewMessage(m.data.Title, m.data.Content);

                result.data = true;
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