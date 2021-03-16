using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b009_EveryGetOrderIDDetailIDWillActiveReturnMoney 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b009_EveryGetOrderIDDetailIDWillActiveReturnMoney
    {
        public b009_EveryGetOrderIDDetailIDWillActiveReturnMoney()
        {}
        #region Model
        //ID,ThisDay,ShopClientID,OperationGoodsID,DecimalReturnMoney,OrderID,OrderDatailID,OrderCount,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private string _ThisDay;
        private Int32? _ShopClientID;
        private Int32? _OperationGoodsID;
        private decimal? _DecimalReturnMoney;
        private Int32? _OrderID;
        private Int32? _OrderDatailID;
        private Int32? _OrderCount;
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
        ///今天   格式 yyyy-MM-dd  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ThisDay
        {
            set{ _ThisDay=value;}
            get{return _ThisDay;}
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OperationGoodsID
        {
            set{ _OperationGoodsID=value;}
            get{return _OperationGoodsID;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? DecimalReturnMoney
        {
            set{ _DecimalReturnMoney=value;}
            get{return _DecimalReturnMoney;}
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
        public Int32? OrderDatailID
        {
            set{ _OrderDatailID=value;}
            get{return _OrderDatailID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OrderCount
        {
            set{ _OrderCount=value;}
            get{return _OrderCount;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
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
        ///  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
