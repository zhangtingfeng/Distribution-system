using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_ASK_Question_User_And_Shopclient_All_List 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_ASK_Question_User_And_Shopclient_All_List
    {
        public View_ASK_Question_User_And_Shopclient_All_List()
        {}
        #region Model
        //smallID,BigID,UserAsk,IsRead,UpdateTime,ID,
        private Int32 _smallID;
        private Int32 _BigID;
        private string _UserAsk;
        private bool _IsRead;
        private DateTime _UpdateTime;
        private Int32 _ID;
        /// <summary>
        /// 
        /// </summary>
        public Int32 smallID
        {
            set{ _smallID=value;}
            get{return _smallID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 BigID
        {
            set{ _BigID=value;}
            get{return _BigID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserAsk
        {
            set{ _UserAsk=value;}
            get{return _UserAsk;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsRead
        {
            set{ _IsRead=value;}
            get{return _IsRead;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        #endregion Model
    }
}
