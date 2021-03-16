using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b015_OrderDetail_WealthBuy 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b015_OrderDetail_WealthBuy
    {
        public b015_OrderDetail_WealthBuy()
        {}
        #region Model
        //ID,UserID,ShopingCartID,OrdetailID,OrderID,ShopClientID,UseOrNotuse,HowMuchWealth,WealthDetailID,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _ShopingCartID;
        private Int32? _OrdetailID;
        private Int32? _OrderID;
        private Int32? _ShopClientID;
        private bool? _UseOrNotuse;
        private decimal? _HowMuchWealth;
        private Int32? _WealthDetailID;
        private string _CreateBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private DateTime? _CreatTime;
        private Int32? _IsDeleted;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///财富进了那个购物车  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopingCartID
        {
            set{ _ShopingCartID=value;}
            get{return _ShopingCartID;}
        }
        /// <summary>
        ///消耗在哪个订单上面  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OrdetailID
        {
            set{ _OrdetailID=value;}
            get{return _OrdetailID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///使用还是不使用 这个钱 。   使用用1  取消使用用0  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? UseOrNotuse
        {
            set{ _UseOrNotuse=value;}
            get{return _UseOrNotuse;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? HowMuchWealth
        {
            set{ _HowMuchWealth=value;}
            get{return _HowMuchWealth;}
        }
        /// <summary>
        ///这个钱来自哪个订单  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? WealthDetailID
        {
            set{ _WealthDetailID=value;}
            get{return _WealthDetailID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string CreateBy
        {
            set{ _CreateBy=value;}
            get{return _CreateBy;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        /// <summary>
        ///  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
