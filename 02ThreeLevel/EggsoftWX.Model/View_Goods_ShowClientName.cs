using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Goods_ShowClientName 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Goods_ShowClientName
    {
        public View_Goods_ShowClientName()
        {}
        #region Model
        //ID,ShopClient_ID,Class1_ID,Class2_ID,Class3_ID,isSaled,Name,Icon,ShortInfo,LongInfo,KuCunCount,Unit,Price,MemberPrice,PromotePrice,HitCount,UpTime,UpdateTime,ContactMan,Sort,IsDeleted,Good_Class,LimitTimerBuy_StartTime,LimitTimerBuy_EndTime,LimitTimerBuy_TimePrice,LimitTimerBuy_Bool,ShopClientName,Class1Name,Class2Name,Class3Name,Class_ShopClient_Name,IS_Admin_check,
        private Int32 _ID;
        private Int32 _ShopClient_ID;
        private Int32 _Class1_ID;
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
        private decimal _MemberPrice;
        private decimal _PromotePrice;
        private Int32 _HitCount;
        private DateTime _UpTime;
        private DateTime _UpdateTime;
        private string _ContactMan;
        private Int32 _Sort;
        private bool _IsDeleted;
        private Int32 _Good_Class;
        private DateTime _LimitTimerBuy_StartTime;
        private DateTime _LimitTimerBuy_EndTime;
        private decimal _LimitTimerBuy_TimePrice;
        private bool _LimitTimerBuy_Bool;
        private string _ShopClientName;
        private string _Class1Name;
        private string _Class2Name;
        private string _Class3Name;
        private string _Class_ShopClient_Name;
        private bool _IS_Admin_check;
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
        public Int32 Class1_ID
        {
            set{ _Class1_ID=value;}
            get{return _Class1_ID;}
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
        public decimal MemberPrice
        {
            set{ _MemberPrice=value;}
            get{return _MemberPrice;}
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
        public Int32 HitCount
        {
            set{ _HitCount=value;}
            get{return _HitCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpTime
        {
            set{ _UpTime=value;}
            get{ if (_UpTime == DateTime.MinValue) _UpTime = DateTime.Now;return _UpTime;}
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
        public string ContactMan
        {
            set{ _ContactMan=value;}
            get{return _ContactMan;}
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
        public string ShopClientName
        {
            set{ _ShopClientName=value;}
            get{return _ShopClientName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Class1Name
        {
            set{ _Class1Name=value;}
            get{return _Class1Name;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Class2Name
        {
            set{ _Class2Name=value;}
            get{return _Class2Name;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Class3Name
        {
            set{ _Class3Name=value;}
            get{return _Class3Name;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Class_ShopClient_Name
        {
            set{ _Class_ShopClient_Name=value;}
            get{return _Class_ShopClient_Name;}
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
