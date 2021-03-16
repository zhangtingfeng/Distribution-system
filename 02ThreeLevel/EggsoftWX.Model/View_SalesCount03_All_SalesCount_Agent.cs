using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_SalesCount03_All_SalesCount_Agent 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_SalesCount03_All_SalesCount_Agent
    {
        public View_SalesCount03_All_SalesCount_Agent()
        {}
        #region Model
        //ShareAskCount,GoodID,Shopping_Vouchers,Sort,SalesCount,XML,IS_Admin_check,LimitTimerBuy_MaxSalesCount,MaxOrderNum,MinOrderNum,LimitTimerBuy_Bool,LimitTimerBuy_TimePrice,LimitTimerBuy_EndTime,LimitTimerBuy_StartTime,Good_Class,IsDeleted,PromotePrice,Price,Unit,KuCunCount,LongInfo,ShortInfo,Icon,Name,isSaled,Class3_ID,Class2_ID,ShopClientName,Class1_ID,ShopClient_ID,UserID,
        private Int32 _ShareAskCount;
        private Int32 _GoodID;
        private Int32 _Shopping_Vouchers;
        private Int32 _Sort;
        private Int32 _SalesCount;
        private string _XML;
        private bool _IS_Admin_check;
        private Int32 _LimitTimerBuy_MaxSalesCount;
        private Int32 _MaxOrderNum;
        private Int32 _MinOrderNum;
        private bool _LimitTimerBuy_Bool;
        private decimal _LimitTimerBuy_TimePrice;
        private DateTime _LimitTimerBuy_EndTime;
        private DateTime _LimitTimerBuy_StartTime;
        private Int32 _Good_Class;
        private bool _IsDeleted;
        private decimal _PromotePrice;
        private decimal _Price;
        private string _Unit;
        private Int32 _KuCunCount;
        private string _LongInfo;
        private string _ShortInfo;
        private string _Icon;
        private string _Name;
        private bool _isSaled;
        private Int32 _Class3_ID;
        private Int32 _Class2_ID;
        private string _ShopClientName;
        private Int32 _Class1_ID;
        private Int32 _ShopClient_ID;
        private Int32 _UserID;
        /// <summary>
        /// 
        /// </summary>
        public Int32 ShareAskCount
        {
            set{ _ShareAskCount=value;}
            get{return _ShareAskCount;}
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
        public Int32 Shopping_Vouchers
        {
            set{ _Shopping_Vouchers=value;}
            get{return _Shopping_Vouchers;}
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
        public Int32 SalesCount
        {
            set{ _SalesCount=value;}
            get{return _SalesCount;}
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
        public bool IS_Admin_check
        {
            set{ _IS_Admin_check=value;}
            get{return _IS_Admin_check;}
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
        public Int32 MaxOrderNum
        {
            set{ _MaxOrderNum=value;}
            get{return _MaxOrderNum;}
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
        public bool LimitTimerBuy_Bool
        {
            set{ _LimitTimerBuy_Bool=value;}
            get{return _LimitTimerBuy_Bool;}
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
        public DateTime LimitTimerBuy_EndTime
        {
            set{ _LimitTimerBuy_EndTime=value;}
            get{ if (_LimitTimerBuy_EndTime == DateTime.MinValue) _LimitTimerBuy_EndTime = DateTime.Now;return _LimitTimerBuy_EndTime;}
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
        public Int32 Good_Class
        {
            set{ _Good_Class=value;}
            get{return _Good_Class;}
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
        public string Unit
        {
            set{ _Unit=value;}
            get{return _Unit;}
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
        public string LongInfo
        {
            set{ _LongInfo=value;}
            get{return _LongInfo;}
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
        public string Icon
        {
            set{ _Icon=value;}
            get{return _Icon;}
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
        public bool isSaled
        {
            set{ _isSaled=value;}
            get{return _isSaled;}
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
        public Int32 Class2_ID
        {
            set{ _Class2_ID=value;}
            get{return _Class2_ID;}
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
        public Int32 Class1_ID
        {
            set{ _Class1_ID=value;}
            get{return _Class1_ID;}
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
        public Int32 UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        #endregion Model
    }
}
