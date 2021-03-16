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

namespace Web_Service.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            Debug.Print("OnConnected");
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            Debug.Print("OnReconnected");
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //当使用者离开时，移除在清单内的ConnectionId
            ChatHubData.getIns().RemoveOnlineUser(Context.ConnectionId);
            Debug.Print("OnDisconnected");
            return base.OnDisconnected(stopCalled);
        }

        public void UserConnect(string userID)
        {
            //进行编码，防止XSS攻击
            userID = HttpUtility.HtmlEncode(userID);
            ChatHubData.getIns().AddOnlineUser(Context.ConnectionId, userID);
        }
    }
}