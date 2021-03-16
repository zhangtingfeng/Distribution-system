using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User_Question 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User_Question
    {
        public tab_User_Question()
        {}
        #region Model
        //ID,UserID,UserAsk,UpdateTime,MemoXML,type,IsRead,ToUserID,
        private Int32 _ID;
        private string _UserID;
        private string _UserAsk;
        private DateTime _UpdateTime;
        private string _MemoXML;
        private string _Type;
        private bool _IsRead;
        private string _ToUserID;
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
        public string UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
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
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MemoXML
        {
            set{ _MemoXML=value;}
            get{return _MemoXML;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set{ _Type=value;}
            get{return _Type;}
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
        public string ToUserID
        {
            set{ _ToUserID=value;}
            get{return _ToUserID;}
        }
        #endregion Model
    }
}
