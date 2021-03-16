using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Cheapst_SalesCount 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Cheapst_SalesCount
    {
        public View_Cheapst_SalesCount()
        {}
        #region Model
        //GoodID,Price,ShopClient_ID,isSaled,Name,Icon,ShortInfo,LongInfo,UpdateTime,CheapPrice,GoodOrderCount,PromotePrice,ShenMaShopping,Shopping_Vouchers,TotalMoney,IS_Admin_check,
        private Int32 _GoodID;
        private decimal _Price;
        private Int32 _ShopClient_ID;
        private bool _isSaled;
        private string _Name;
        private string _Icon;
        private string _ShortInfo;
        private string _LongInfo;
        private DateTime _UpdateTime;
        private decimal _CheapPrice;
        private Int32 _GoodOrderCount;
        private decimal _PromotePrice;
        private bool _ShenMaShopping;
        private bool _Shopping_Vouchers;
        private decimal _TotalMoney;
        private bool _IS_Admin_check;
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
        public decimal Price
        {
            set{ _Price=value;}
            get{return _Price;}
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
        public decimal CheapPrice
        {
            set{ _CheapPrice=value;}
            get{return _CheapPrice;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 GoodOrderCount
        {
            set{ _GoodOrderCount=value;}
            get{return _GoodOrderCount;}
        }
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
        public decimal TotalMoney
        {
            set{ _TotalMoney=value;}
            get{return _TotalMoney;}
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
