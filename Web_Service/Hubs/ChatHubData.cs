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
    public class ChatHubData
    {
        private static ChatHubData _instance = new ChatHubData();
        public static ChatHubData getIns() { return _instance; }

        public class OnlineUser
        {
            public string ConnectID { get; set; }
            public string UserID { get; set; }
        }
        public List<OnlineUser> OnlineUsers = new List<OnlineUser>();

        /// <summary>
        /// 添加在线用户
        /// </summary>
        public void AddOnlineUser(string ConnectID, string UserID)
        {
            OnlineUsers.Add(new OnlineUser()
            {
                ConnectID = ConnectID,
                UserID = UserID
            });
        }

        /// <summary>
        /// 移除
        /// </summary>
        public void RemoveOnlineUser(string ConnectID)
        {
            var data = GetOnlineUserByConnectID(ConnectID);
            if (data != null)
            {
                OnlineUsers.Remove(data);
            }
        }

        public OnlineUser GetOnlineUserByConnectID(string ConnectID)
        {
            if (OnlineUsers.Count <= 0) return null;
            return OnlineUsers.First((e) => { return e.ConnectID.ToUpper().Equals(ConnectID.ToUpper()); });
        }

        public List<OnlineUser> GetOnlineUserByUserID(string UserID)
        {
            if (OnlineUsers.Count <= 0) return OnlineUsers;
            return OnlineUsers.FindAll((e) => { return e.UserID.ToUpper().Equals(UserID.ToUpper()); });
        }
    }
}