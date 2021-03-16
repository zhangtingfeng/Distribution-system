using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_AdminFinance_User 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_AdminFinance_User
    {
        public tab_AdminFinance_User()
        {}
        #region Model
        //UserName,Password,ID,LoginTimes,lastLoginIP,lastLoginTimes,ManagerLevel,
        private string _UserName;
        private string _Password;
        private Int32 _ID;
        private Int32 _LoginTimes;
        private string _lastLoginIP;
        private DateTime _lastLoginTimes;
        private Int32 _ManagerLevel;
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set{ _UserName=value;}
            get{return _UserName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set{ _Password=value;}
            get{return _Password;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 LoginTimes
        {
            set{ _LoginTimes=value;}
            get{return _LoginTimes;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastLoginIP
        {
            set{ _lastLoginIP=value;}
            get{return _lastLoginIP;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime lastLoginTimes
        {
            set{ _lastLoginTimes=value;}
            get{ if (_lastLoginTimes == DateTime.MinValue) _lastLoginTimes = DateTime.Now;return _lastLoginTimes;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ManagerLevel
        {
            set{ _ManagerLevel=value;}
            get{return _ManagerLevel;}
        }
        #endregion Model
    }
}
