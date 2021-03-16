using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Today_Cheapest 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Today_Cheapest
    {
        public View_Today_Cheapest()
        {}
        #region Model
        //PromotePrice,Price,ID,ShopClient_ID,isSaled,Name,Icon,ShortInfo,LongInfo,UpdateTime,IsDeleted,CheapPrice,ShenMaShopping,Shopping_Vouchers,IS_Admin_check,
        private decimal _PromotePrice;
        private decimal _Price;
        private Int32 _ID;
        private Int32 _ShopClient_ID;
        private bool _isSaled;
        private string _Name;
        private string _Icon;
        private string _ShortInfo;
        private string _LongInfo;
        private DateTime _UpdateTime;
        private bool _IsDeleted;
        private decimal _CheapPrice;
        private bool _ShenMaShopping;
        private bool _Shopping_Vouchers;
        private bool _IS_Admin_check;
        /// <summary>
        /// 
        /// </summary>
        public decimal PromotePrice
        {
            set{ _PromotePrice=value;}
            get{return _PromotePrice;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            set{ _Price=value;}
            get{return _Price;}
        }
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
        public bool isSaled
        {
            set{ _isSaled=value;}
            get{return _isSaled;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Icon
        {
            set{ _Icon=value;}
            get{return _Icon;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortInfo
        {
            set{ _ShortInfo=value;}
            get{return _ShortInfo;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string LongInfo
        {
            set{ _LongInfo=value;}
            get{return _LongInfo;}
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
        public bool IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CheapPrice
        {
            set{ _CheapPrice=value;}
            get{return _CheapPrice;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ShenMaShopping
        {
            set{ _ShenMaShopping=value;}
            get{return _ShenMaShopping;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Shopping_Vouchers
        {
            set{ _Shopping_Vouchers=value;}
            get{return _Shopping_Vouchers;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IS_Admin_check
        {
            set{ _IS_Admin_check=value;}
            get{return _IS_Admin_check;}
        }
        #endregion Model
    }
}
