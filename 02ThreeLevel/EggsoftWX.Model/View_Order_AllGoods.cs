using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Order_AllGoods 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Order_AllGoods
    {
        public View_Order_AllGoods()
        {}
        #region Model
        //OrderCount,OrderID,PayStatus,GoodID,UserID,NickName,OrderNum,PromotePrice,GoodPrice,GoodName,ID_Orderdetails,Pinglun,CreatDateTime,PayDateTime,ParentID,IS_Admin_check,
        private Int32 _OrderCount;
        private Int32 _OrderID;
        private bool _PayStatus;
        private Int32 _GoodID;
        private Int32 _UserID;
        private string _NickName;
        private string _OrderNum;
        private decimal _PromotePrice;
        private decimal _GoodPrice;
        private string _GoodName;
        private Int32 _ID_Orderdetails;
        private string _Pinglun;
        private DateTime _CreatDateTime;
        private DateTime _PayDateTime;
        private Int32 _ParentID;
        private bool _IS_Admin_check;
        /// <summary>
        /// 
        /// </summary>
        public Int32 OrderCount
        {
            set{ _OrderCount=value;}
            get{return _OrderCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool PayStatus
        {
            set{ _PayStatus=value;}
            get{return _PayStatus;}
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
        public Int32 UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string NickName
        {
            set{ _NickName=value;}
            get{return _NickName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderNum
        {
            set{ _OrderNum=value;}
            get{return _OrderNum;}
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
        public decimal GoodPrice
        {
            set{ _GoodPrice=value;}
            get{return _GoodPrice;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodName
        {
            set{ _GoodName=value;}
            get{return _GoodName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ID_Orderdetails
        {
            set{ _ID_Orderdetails=value;}
            get{return _ID_Orderdetails;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pinglun
        {
            set{ _Pinglun=value;}
            get{return _Pinglun;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatDateTime
        {
            set{ _CreatDateTime=value;}
            get{ if (_CreatDateTime == DateTime.MinValue) _CreatDateTime = DateTime.Now;return _CreatDateTime;}
        }
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
        public Int32 ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
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
