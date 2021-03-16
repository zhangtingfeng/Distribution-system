using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_NoMoreThen100 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_NoMoreThen100
    {
        public View_NoMoreThen100()
        {}
        #region Model
        //ID,salesCount,isSaled,Name,Icon,ShortInfo,KuCunCount,PromotePrice,Price,TotalMoney,IS_Admin_check,
        private Int32 _ID;
        private Int32 _salesCount;
        private bool _isSaled;
        private string _Name;
        private string _Icon;
        private string _ShortInfo;
        private Int32 _KuCunCount;
        private decimal _PromotePrice;
        private decimal _Price;
        private decimal _TotalMoney;
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
        public Int32 salesCount
        {
            set{ _salesCount=value;}
            get{return _salesCount;}
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
        public Int32 KuCunCount
        {
            set{ _KuCunCount=value;}
            get{return _KuCunCount;}
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
