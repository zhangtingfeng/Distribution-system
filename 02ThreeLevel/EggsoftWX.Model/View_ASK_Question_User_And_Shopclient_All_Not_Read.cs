using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_ASK_Question_User_And_Shopclient_All_Not_Read 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_ASK_Question_User_And_Shopclient_All_Not_Read
    {
        public View_ASK_Question_User_And_Shopclient_All_Not_Read()
        {}
        #region Model
        //smallID,BigID,CountMessage,updatetime,CountNotReadMessage,
        private string _smallID;
        private string _BigID;
        private Int32 _CountMessage;
        private DateTime _updatetime;
        private Int32 _CountNotReadMessage;
        /// <summary>
        /// 
        /// </summary>
        public string smallID
        {
            set{ _smallID=value;}
            get{return _smallID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string BigID
        {
            set{ _BigID=value;}
            get{return _BigID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 CountMessage
        {
            set{ _CountMessage=value;}
            get{return _CountMessage;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime updatetime
        {
            set{ _updatetime=value;}
            get{ if (_updatetime == DateTime.MinValue) _updatetime = DateTime.Now;return _updatetime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 CountNotReadMessage
        {
            set{ _CountNotReadMessage=value;}
            get{return _CountNotReadMessage;}
        }
        #endregion Model
    }
}
