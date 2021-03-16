using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_ThisWeekSalesMaxGoods 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_ThisWeekSalesMaxGoods
    {
        public View_ThisWeekSalesMaxGoods()
        {}
        #region Model
        //PayDateTime,GoodID,GoodOrderCount,
        private DateTime _PayDateTime;
        private Int32 _GoodID;
        private Int32 _GoodOrderCount;
        /// <summary>
        /// 
        /// </summary>
        public DateTime PayDateTime
        {
            set{ _PayDateTime=value;}
            get{ if (_PayDateTime == DateTime.MinValue) _PayDateTime = DateTime.Now;return _PayDateTime;}
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
        public Int32 GoodOrderCount
        {
            set{ _GoodOrderCount=value;}
            get{return _GoodOrderCount;}
        }
        #endregion Model
    }
}
