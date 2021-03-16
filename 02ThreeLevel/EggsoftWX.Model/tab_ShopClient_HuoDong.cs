using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_HuoDong 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_HuoDong
    {
        public tab_ShopClient_HuoDong()
        {}
        #region Model
        //ID,Type,XML,UpdateTime,ShopClientID,
        private Int32 _ID;
        private string _Type;
        private string _XML;
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
        public string Type
        {
            set{ _Type=value;}
            get{return _Type;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string XML
        {
            set{ _XML=value;}
            get{return _XML;}
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
