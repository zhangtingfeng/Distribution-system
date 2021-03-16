using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_WatchGoods 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_WatchGoods
    {
        public View_WatchGoods()
        {}
        #region Model
        //ShopClient_ID,Class1_ID,Class2_ID,Class3_ID,Name,Good_Class,Parent_UserID,UserID,UpdateTime,Count_Visit,PromotePrice,Price,ID,Sort,GoodID,IsDeleted,IS_Admin_check,GrandParentID,GreatParentID,
        private Int32 _ShopClient_ID;
        private Int32 _Class1_ID;
        private Int32 _Class2_ID;
        private Int32 _Class3_ID;
        private string _Name;
        private Int32 _Good_Class;
        private Int32 _Parent_UserID;
        private Int32 _UserID;
        private DateTime _UpdateTime;
        private Int32 _Count_Visit;
        private decimal _PromotePrice;
        private decimal _Price;
        private Int32 _ID;
        private Int32 _Sort;
        private Int32 _GoodID;
        private bool _IsDeleted;
        private bool _IS_Admin_check;
        private Int32 _GrandParentID;
        private Int32 _GreatParentID;
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
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
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
        public Int32 Parent_UserID
        {
            set{ _Parent_UserID=value;}
            get{return _Parent_UserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
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
        public Int32 Count_Visit
        {
            set{ _Count_Visit=value;}
            get{return _Count_Visit;}
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
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
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
        public Int32 GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
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
        public bool IS_Admin_check
        {
            set{ _IS_Admin_check=value;}
            get{return _IS_Admin_check;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 GrandParentID
        {
            set{ _GrandParentID=value;}
            get{return _GrandParentID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 GreatParentID
        {
            set{ _GreatParentID=value;}
            get{return _GreatParentID;}
        }
        #endregion Model
    }
}
