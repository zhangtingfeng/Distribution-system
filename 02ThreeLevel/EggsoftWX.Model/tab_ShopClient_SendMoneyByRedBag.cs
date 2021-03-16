using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_SendMoneyByRedBag 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_SendMoneyByRedBag
    {
        public tab_ShopClient_SendMoneyByRedBag()
        {}
        #region Model
        //ID,ShopClient_ID,UpdateTime,SendedStatus,SendToType,MsgTypeNewsTitle,MsgTypeNewsDescription,ValidStartTime,ValidEndTime,
        private Int32 _ID;
        private Int32 _ShopClient_ID;
        private DateTime _UpdateTime;
        private bool _SendedStatus;
        private string _SendToType;
        private string _MsgTypeNewsTitle;
        private string _MsgTypeNewsDescription;
        private DateTime _ValidStartTime;
        private DateTime _ValidEndTime;
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
        public Int32 ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
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
        public bool SendedStatus
        {
            set{ _SendedStatus=value;}
            get{return _SendedStatus;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendToType
        {
            set{ _SendToType=value;}
            get{return _SendToType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MsgTypeNewsTitle
        {
            set{ _MsgTypeNewsTitle=value;}
            get{return _MsgTypeNewsTitle;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MsgTypeNewsDescription
        {
            set{ _MsgTypeNewsDescription=value;}
            get{return _MsgTypeNewsDescription;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ValidStartTime
        {
            set{ _ValidStartTime=value;}
            get{ if (_ValidStartTime == DateTime.MinValue) _ValidStartTime = DateTime.Now;return _ValidStartTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ValidEndTime
        {
            set{ _ValidEndTime=value;}
            get{ if (_ValidEndTime == DateTime.MinValue) _ValidEndTime = DateTime.Now;return _ValidEndTime;}
        }
        #endregion Model
    }
}
