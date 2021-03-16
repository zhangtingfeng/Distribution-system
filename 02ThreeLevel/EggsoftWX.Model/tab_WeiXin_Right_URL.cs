using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_WeiXin_Right_URL 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_WeiXin_Right_URL
    {
        public tab_WeiXin_Right_URL()
        {}
        #region Model
        //ID,AppId,TimeStamp,OpenId,AppSignature,MsgType,FeedBackId,TransId,Reason,Solution,ExtInfo,SignMethod,txt,WeiQuanType,PicInfo,UpdateTime,
        private Int32 _ID;
        private string _AppId;
        private string _TimeStamp;
        private string _OpenId;
        private string _AppSignature;
        private string _MsgType;
        private string _FeedBackId;
        private string _TransId;
        private string _Reason;
        private string _Solution;
        private string _ExtInfo;
        private string _SignMethod;
        private string _txt;
        private string _WeiQuanType;
        private string _PicInfo;
        private DateTime _UpdateTime;
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
        public string AppId
        {
            set{ _AppId=value;}
            get{return _AppId;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string TimeStamp
        {
            set{ _TimeStamp=value;}
            get{return _TimeStamp;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string OpenId
        {
            set{ _OpenId=value;}
            get{return _OpenId;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string AppSignature
        {
            set{ _AppSignature=value;}
            get{return _AppSignature;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MsgType
        {
            set{ _MsgType=value;}
            get{return _MsgType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string FeedBackId
        {
            set{ _FeedBackId=value;}
            get{return _FeedBackId;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string TransId
        {
            set{ _TransId=value;}
            get{return _TransId;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reason
        {
            set{ _Reason=value;}
            get{return _Reason;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Solution
        {
            set{ _Solution=value;}
            get{return _Solution;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtInfo
        {
            set{ _ExtInfo=value;}
            get{return _ExtInfo;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string SignMethod
        {
            set{ _SignMethod=value;}
            get{return _SignMethod;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string txt
        {
            set{ _txt=value;}
            get{return _txt;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string WeiQuanType
        {
            set{ _WeiQuanType=value;}
            get{return _WeiQuanType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string PicInfo
        {
            set{ _PicInfo=value;}
            get{return _PicInfo;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        #endregion Model
    }
}
