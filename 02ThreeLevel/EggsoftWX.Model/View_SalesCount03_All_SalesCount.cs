using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_SalesCount03_All_SalesCount 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_SalesCount03_All_SalesCount
    {
        public View_SalesCount03_All_SalesCount()
        {}
        #region Model
        //ShopClient_ID,Class1_ID,ShopClientName,Class2_ID,Class3_ID,isSaled,Name,Icon,ShortInfo,LongInfo,KuCunCount,Unit,Price,PromotePrice,IsDeleted,Good_Class,LimitTimerBuy_StartTime,LimitTimerBuy_EndTime,LimitTimerBuy_TimePrice,LimitTimerBuy_Bool,MinOrderNum,MaxOrderNum,LimitTimerBuy_MaxSalesCount,IS_Admin_check,XML,SalesCount,Sort,Shopping_Vouchers,GoodID,ShareAskCount,
        private Int32 _ShopClient_ID;
        private Int32 _Class1_ID;
        private string _ShopClientName;
        private Int32 _Class2_ID;
        private Int32 _Class3_ID;
        private bool _isSaled;
        private string _Name;
        private string _Icon;
        private string _ShortInfo;
        private string _LongInfo;
        private Int32 _KuCunCount;
        private string _Unit;
        private decimal _Price;
        private decimal _PromotePrice;
        private bool _IsDeleted;
        private Int32 _Good_Class;
        private DateTime _LimitTimerBuy_StartTime;
        private DateTime _LimitTimerBuy_EndTime;
        private decimal _LimitTimerBuy_TimePrice;
        private bool _LimitTimerBuy_Bool;
        private Int32 _MinOrderNum;
        private Int32 _MaxOrderNum;
        private Int32 _LimitTimerBuy_MaxSalesCount;
        private bool _IS_Admin_check;
        private string _XML;
        private Int32 _SalesCount;
        private Int32 _Sort;
        private Int32 _Shopping_Vouchers;
        private Int32 _GoodID;
        private Int32 _ShareAskCount;
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
        public Int32 Class1_ID
        {
            set{ _Class1_ID=value;}
            get{return _Class1_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShopClientName
        {
            set{ _ShopClientName=value;}
            get{return _ShopClientName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Class2_ID
        {
            set{ _Class2_ID=value;}
            get{return _Class2_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Class3_ID
        {
            set{ _Class3_ID=value;}
            get{return _Class3_ID;}
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
        public Int32 KuCunCount
        {
            set{ _KuCunCount=value;}
            get{return _KuCunCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set{ _Unit=value;}
            get{return _Unit;}
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
        public decimal PromotePrice
        {
            set{ _PromotePrice=value;}
            get{return _PromotePrice;}
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
        public Int32 Good_Class
        {
            set{ _Good_Class=value;}
            get{return _Good_Class;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LimitTimerBuy_StartTime
        {
            set{ _LimitTimerBuy_StartTime=value;}
            get{ if (_LimitTimerBuy_StartTime == DateTime.MinValue) _LimitTimerBuy_StartTime = DateTime.Now;return _LimitTimerBuy_StartTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LimitTimerBuy_EndTime
        {
            set{ _LimitTimerBuy_EndTime=value;}
            get{ if (_LimitTimerBuy_EndTime == DateTime.MinValue) _LimitTimerBuy_EndTime = DateTime.Now;return _LimitTimerBuy_EndTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal LimitTimerBuy_TimePrice
        {
            set{ _LimitTimerBuy_TimePrice=value;}
            get{return _LimitTimerBuy_TimePrice;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool LimitTimerBuy_Bool
        {
            set{ _LimitTimerBuy_Bool=value;}
            get{return _LimitTimerBuy_Bool;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 MinOrderNum
        {
            set{ _MinOrderNum=value;}
            get{return _MinOrderNum;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 MaxOrderNum
        {
            set{ _MaxOrderNum=value;}
            get{return _MaxOrderNum;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 LimitTimerBuy_MaxSalesCount
        {
            set{ _LimitTimerBuy_MaxSalesCount=value;}
            get{return _LimitTimerBuy_MaxSalesCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IS_Admin_check
        {
            set{ _IS_Admin_check=value;}
            get{return _IS_Admin_check;}
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
        public Int32 SalesCount
        {
            set{ _SalesCount=value;}
            get{return _SalesCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Shopping_Vouchers
        {
            set{ _Shopping_Vouchers=value;}
            get{return _Shopping_Vouchers;}
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
        public Int32 ShareAskCount
        {
            set{ _ShareAskCount=value;}
            get{return _ShareAskCount;}
        }
        #endregion Model
    }
}
