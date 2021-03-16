using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
//using XLugia.Lib.XLog.Base;
using Microsoft.AspNet.SignalR.Hubs;
using InsureApi.Common.Common;

namespace Web_Service.Hubs
{
    public class ChatHubHelper
    {
        private static ChatHubHelper _instance = new ChatHubHelper();
        public static ChatHubHelper getIns() { return _instance; }

        //消息推送
        public void sendMessage(string userID, string title, string message, string url)
        {
            if (Check.getIns().isEmpty(userID))
            {
                return;
            }

            var onlineUsers = ChatHubData.getIns().GetOnlineUserByUserID(userID);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            foreach (var user in onlineUsers)
            {
                hubContext.Clients.Client(user.ConnectID).alertNewMessage(title, message);
            }
        }
    }
}