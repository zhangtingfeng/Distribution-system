using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_ShenMaShopIng 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_ShenMaShopIng
    {
        public View_ShenMaShopIng()
        {}
        #region Model
        //ShopClient_ID,GoodID,SalesCount,Price,Icon,Name,PromotePrice,
        private Int32 _ShopClient_ID;
        private Int32 _GoodID;
        private Int32 _SalesCount;
        private decimal _Price;
        private string _Icon;
        private string _Name;
        private decimal _PromotePrice;
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
        public Int32 GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
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
        public decimal Price
        {
            set{ _Price=value;}
            get{return _Price;}
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
        public decimal PromotePrice
        {
            set{ _PromotePrice=value;}
            get{return _PromotePrice;}
        }
        #endregion Model
    }
}
