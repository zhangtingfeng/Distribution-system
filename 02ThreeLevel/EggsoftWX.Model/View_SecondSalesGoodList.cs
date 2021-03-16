using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_SecondSalesGoodList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_SecondSalesGoodList
    {
        public View_SecondSalesGoodList()
        {}
        #region Model
        //LimitTimerBuy_StartTime,LimitTimerBuy_EndTime,LimitTimerBuy_TimePrice,Name,ID,PromotePrice,Price,ShowWhenSales,ShowWhenEndSales,ShowWhenSalesSecond,ShowWhenEndSalesSecond,Icon,KuCunCount,LimitTimerBuy_MaxSalesCount,IS_Admin_check,ShareAskCount,SalesCount,kg,
        private DateTime _LimitTimerBuy_StartTime;
        private DateTime _LimitTimerBuy_EndTime;
        private decimal _LimitTimerBuy_TimePrice;
        private string _Name;
        private Int32 _ID;
        private decimal _PromotePrice;
        private decimal _Price;
        private string _ShowWhenSales;
        private string _ShowWhenEndSales;
        private string _ShowWhenSalesSecond;
        private string _ShowWhenEndSalesSecond;
        private string _Icon;
        private Int32 _KuCunCount;
        private Int32 _LimitTimerBuy_MaxSalesCount;
        private bool _IS_Admin_check;
        private Int32 _ShareAskCount;
        private Int32 _SalesCount;
        private decimal _kg;
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
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
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
        public string ShowWhenSales
        {
            set{ _ShowWhenSales=value;}
            get{return _ShowWhenSales;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShowWhenEndSales
        {
            set{ _ShowWhenEndSales=value;}
            get{return _ShowWhenEndSales;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShowWhenSalesSecond
        {
            set{ _ShowWhenSalesSecond=value;}
            get{return _ShowWhenSalesSecond;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShowWhenEndSalesSecond
        {
            set{ _ShowWhenEndSalesSecond=value;}
            get{return _ShowWhenEndSalesSecond;}
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
        public Int32 KuCunCount
        {
            set{ _KuCunCount=value;}
            get{return _KuCunCount;}
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
        public Int32 ShareAskCount
        {
            set{ _ShareAskCount=value;}
            get{return _ShareAskCount;}
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
        public decimal kg
        {
            set{ _kg=value;}
            get{return _kg;}
        }
        #endregion Model
    }
}
