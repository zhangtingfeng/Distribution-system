using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b008_OpterationUserActiveReturnMoneyOrderNum 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b008_OpterationUserActiveReturnMoneyOrderNum
    {
        public b008_OpterationUserActiveReturnMoneyOrderNum()
        {}
        #region Model
        //ID,UserID,OrderID,OrderDetailID,OrderCount,PayDateTime,ShopClient_ID,InputShouldReturnCount,ActiveOrderNum,OutHadGivedUserNum,ReturnMoneyUnit,b004_OperationGoodsID,Logss,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _OrderID;
        private Int32? _OrderDetailID;
        private Int32? _OrderCount;
        private DateTime? _PayDateTime;
        private Int32? _ShopClient_ID;
        private Int32? _InputShouldReturnCount;
        private Int32? _ActiveOrderNum;
        private Int32? _OutHadGivedUserNum;
        private decimal? _ReturnMoneyUnit;
        private Int32? _b004_OperationGoodsID;
        private string _Logss;
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
        public Int32? OrderDetailID
        {
            set{ _OrderDetailID=value;}
            get{return _OrderDetailID;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? PayDateTime
        {
            set{ _PayDateTime=value;}
            get{return _PayDateTime;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        ///新增的应该归还用户的个数  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? InputShouldReturnCount
        {
            set{ _InputShouldReturnCount=value;}
            get{return _InputShouldReturnCount;}
        }
        /// <summary>
        ///购买用户返现权限个数  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ActiveOrderNum
        {
            set{ _ActiveOrderNum=value;}
            get{return _ActiveOrderNum;}
        }
        /// <summary>
        ///出栈的 已经还完的用户数量  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OutHadGivedUserNum
        {
            set{ _OutHadGivedUserNum=value;}
            get{return _OutHadGivedUserNum;}
        }
        /// <summary>
        ///记录这款商品 还有多少钱没还给用户  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ReturnMoneyUnit
        {
            set{ _ReturnMoneyUnit=value;}
            get{return _ReturnMoneyUnit;}
        }
        /// <summary>
        ///关联的回馈的哪一个商品的返还  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? b004_OperationGoodsID
        {
            set{ _b004_OperationGoodsID=value;}
            get{return _b004_OperationGoodsID;}
        }
        /// <summary>
        ///操作 活动订单数的日志  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string Logss
        {
            set{ _Logss=value;}
            get{return _Logss;}
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
