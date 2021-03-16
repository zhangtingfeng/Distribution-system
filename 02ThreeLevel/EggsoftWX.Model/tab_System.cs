using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_System 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_System
    {
        public tab_System()
        {}
        #region Model
        //ID,ShopType,ShopInfo,UpdateTime,
        private Int32 _ID;
        private string _ShopType;
        private string _ShopInfo;
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
        public string ShopType
        {
            set{ _ShopType=value;}
            get{return _ShopType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShopInfo
        {
            set{ _ShopInfo=value;}
            get{return _ShopInfo;}
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
