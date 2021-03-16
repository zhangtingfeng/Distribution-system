using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Goods_XML_Goods_ID 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Goods_XML_Goods_ID
    {
        public tab_Goods_XML_Goods_ID()
        {}
        #region Model
        //ID,XMLName_ID,GoodID,UpdateTime,
        private Int32 _ID;
        private Int32 _XMLName_ID;
        private Int32 _GoodID;
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
        public Int32 XMLName_ID
        {
            set{ _XMLName_ID=value;}
            get{return _XMLName_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
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
