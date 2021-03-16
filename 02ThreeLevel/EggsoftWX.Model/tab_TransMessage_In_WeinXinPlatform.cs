using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_TransMessage_In_WeinXinPlatform 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_TransMessage_In_WeinXinPlatform
    {
        public tab_TransMessage_In_WeinXinPlatform()
        {}
        #region Model
        //ID,FromUserId,ToUserId,UpdateTime,IFClosedTalked,
        private Int32 _ID;
        private Int32 _FromUserId;
        private Int32 _ToUserId;
        private DateTime _UpdateTime;
        private bool _IFClosedTalked;
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
        public Int32 FromUserId
        {
            set{ _FromUserId=value;}
            get{return _FromUserId;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ToUserId
        {
            set{ _ToUserId=value;}
            get{return _ToUserId;}
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
        public bool IFClosedTalked
        {
            set{ _IFClosedTalked=value;}
            get{return _IFClosedTalked;}
        }
        #endregion Model
    }
}
