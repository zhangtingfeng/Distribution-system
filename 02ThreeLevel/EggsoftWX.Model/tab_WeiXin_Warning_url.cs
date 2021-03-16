using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_WeiXin_Warning_url 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_WeiXin_Warning_url
    {
        public tab_WeiXin_Warning_url()
        {}
        #region Model
        //ID,AppId,ErrorType,AlarmContent,TimeStamp,AppSignature,SignMethod,updateTime,
        private Int32 _ID;
        private string _AppId;
        private string _ErrorType;
        private string _AlarmContent;
        private string _TimeStamp;
        private string _AppSignature;
        private string _SignMethod;
        private DateTime _updateTime;
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
        public string ErrorType
        {
            set{ _ErrorType=value;}
            get{return _ErrorType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string AlarmContent
        {
            set{ _AlarmContent=value;}
            get{return _AlarmContent;}
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
        public string AppSignature
        {
            set{ _AppSignature=value;}
            get{return _AppSignature;}
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
        public DateTime updateTime
        {
            set{ _updateTime=value;}
            get{ if (_updateTime == DateTime.MinValue) _updateTime = DateTime.Now;return _updateTime;}
        }
        #endregion Model
    }
}
