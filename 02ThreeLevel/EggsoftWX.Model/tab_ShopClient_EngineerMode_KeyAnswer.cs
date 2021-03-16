using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_EngineerMode_KeyAnswer 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_EngineerMode_KeyAnswer
    {
        public tab_ShopClient_EngineerMode_KeyAnswer()
        {}
        #region Model
        //ID,Marker,MarkerContent,UpdateTime,ShopClientID,
        private Int32 _ID;
        private string _Marker;
        private string _MarkerContent;
        private DateTime _UpdateTime;
        private Int32 _ShopClientID;
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
        public string Marker
        {
            set{ _Marker=value;}
            get{return _Marker;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MarkerContent
        {
            set{ _MarkerContent=value;}
            get{return _MarkerContent;}
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
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        #endregion Model
    }
}
