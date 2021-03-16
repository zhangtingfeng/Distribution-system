using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_FenxiaoTeQu 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_FenxiaoTeQu
    {
        public View_FenxiaoTeQu()
        {}
        #region Model
        //ID,salesCount,Name,Icon,ShortInfo,KuCunCount,Price,PromotePrice,Webuy8_DistributionMoney_Value,TotalMoney,
        private Int32 _ID;
        private Int32 _salesCount;
        private string _Name;
        private string _Icon;
        private string _ShortInfo;
        private Int32 _KuCunCount;
        private decimal _Price;
        private decimal _PromotePrice;
        private Int32 _Webuy8_DistributionMoney_Value;
        private decimal _TotalMoney;
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
        public Int32 salesCount
        {
            set{ _salesCount=value;}
            get{return _salesCount;}
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
        public Int32 KuCunCount
        {
            set{ _KuCunCount=value;}
            get{return _KuCunCount;}
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
        public Int32 Webuy8_DistributionMoney_Value
        {
            set{ _Webuy8_DistributionMoney_Value=value;}
            get{return _Webuy8_DistributionMoney_Value;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalMoney
        {
            set{ _TotalMoney=value;}
            get{return _TotalMoney;}
        }
        #endregion Model
    }
}
