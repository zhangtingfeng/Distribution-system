using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_System_XML_Message 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_System_XML_Message
    {
        public tab_System_XML_Message()
        {}
        #region Model
        //ID,FromUserName,INCUserToUserName,type,MessageType,MessageContent,UpdateTime,FromUserID,ToUserID,
        private Int32 _ID;
        private string _FromUserName;
        private string _INCUserToUserName;
        private string _Type;
        private string _MessageType;
        private string _MessageContent;
        private DateTime _UpdateTime;
        private Int32 _FromUserID;
        private Int32 _ToUserID;
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
        public string FromUserName
        {
            set{ _FromUserName=value;}
            get{return _FromUserName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string INCUserToUserName
        {
            set{ _INCUserToUserName=value;}
            get{return _INCUserToUserName;}
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
        public string MessageType
        {
            set{ _MessageType=value;}
            get{return _MessageType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MessageContent
        {
            set{ _MessageContent=value;}
            get{return _MessageContent;}
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
        public Int32 FromUserID
        {
            set{ _FromUserID=value;}
            get{return _FromUserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ToUserID
        {
            set{ _ToUserID=value;}
            get{return _ToUserID;}
        }
        #endregion Model
    }
}
