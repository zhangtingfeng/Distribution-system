using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Goods_MultiSelectTypePrice 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Goods_MultiSelectTypePrice
    {
        public tab_Goods_MultiSelectTypePrice()
        {}
        #region Model
        //ID,GoodID,GoodMultiName,GoodPrice,UpdateTime,
        private Int32 _ID;
        private Int32 _GoodID;
        private string _GoodMultiName;
        private decimal _GoodPrice;
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
        public Int32 GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodMultiName
        {
            set{ _GoodMultiName=value;}
            get{return _GoodMultiName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodPrice
        {
            set{ _GoodPrice=value;}
            get{return _GoodPrice;}
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
