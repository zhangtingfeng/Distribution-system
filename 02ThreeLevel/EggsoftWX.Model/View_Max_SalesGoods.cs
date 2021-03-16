using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Max_SalesGoods 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Max_SalesGoods
    {
        public View_Max_SalesGoods()
        {}
        #region Model
        //GoodID,GoodOrderCount,TotalMoney,PromotePrice,IS_Admin_check,
        private Int32 _GoodID;
        private Int32 _GoodOrderCount;
        private decimal _TotalMoney;
        private decimal _PromotePrice;
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
        public Int32 GoodOrderCount
        {
            set{ _GoodOrderCount=value;}
            get{return _GoodOrderCount;}
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
        public decimal PromotePrice
        {
            set{ _PromotePrice=value;}
            get{return _PromotePrice;}
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
