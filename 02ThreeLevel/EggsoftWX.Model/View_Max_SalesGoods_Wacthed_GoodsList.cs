using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Max_SalesGoods_Wacthed_GoodsList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Max_SalesGoods_Wacthed_GoodsList
    {
        public View_Max_SalesGoods_Wacthed_GoodsList()
        {}
        #region Model
        //GoodID,MaxPercent,GoodOrderCount,SumCount,IS_Admin_check,
        private Int32 _GoodID;
        private decimal _MaxPercent;
        private Int32 _GoodOrderCount;
        private Int32 _SumCount;
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
        public decimal MaxPercent
        {
            set{ _MaxPercent=value;}
            get{return _MaxPercent;}
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
        public Int32 SumCount
        {
            set{ _SumCount=value;}
            get{return _SumCount;}
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
