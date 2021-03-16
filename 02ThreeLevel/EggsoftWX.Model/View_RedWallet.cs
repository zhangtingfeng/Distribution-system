using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_RedWallet 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_RedWallet
    {
        public View_RedWallet()
        {}
        #region Model
        //GoodID,salesCount,TotalMoney,Webuy8_DistributionMoney_Value,CheckBox_WeiBai_RedMoney,Name,Icon,ShortInfo,KuCunCount,Price,PromotePrice,isSaled,IS_Admin_check,IsDeleted,
        private Int32 _GoodID;
        private Int32 _salesCount;
        private decimal _TotalMoney;
        private Int32 _Webuy8_DistributionMoney_Value;
        private bool _CheckBox_WeiBai_RedMoney;
        private string _Name;
        private string _Icon;
        private string _ShortInfo;
        private Int32 _KuCunCount;
        private decimal _Price;
        private decimal _PromotePrice;
        private bool _isSaled;
        private bool _IS_Admin_check;
        private bool _IsDeleted;
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
        public Int32 salesCount
        {
            set{ _salesCount=value;}
            get{return _salesCount;}
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
        public Int32 Webuy8_DistributionMoney_Value
        {
            set{ _Webuy8_DistributionMoney_Value=value;}
            get{return _Webuy8_DistributionMoney_Value;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool CheckBox_WeiBai_RedMoney
        {
            set{ _CheckBox_WeiBai_RedMoney=value;}
            get{return _CheckBox_WeiBai_RedMoney;}
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
        public bool isSaled
        {
            set{ _isSaled=value;}
            get{return _isSaled;}
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
        public bool IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
