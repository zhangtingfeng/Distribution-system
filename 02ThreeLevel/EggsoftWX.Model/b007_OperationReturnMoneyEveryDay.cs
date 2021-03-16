using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b007_OperationReturnMoneyEveryDay 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b007_OperationReturnMoneyEveryDay
    {
        public b007_OperationReturnMoneyEveryDay()
        {}
        #region Model
        //ID,ShopClient_ID,ThisDayReturnActual,ThisDayMoneyByBoss,ThisDayMoneyAuto,ThisDay,ThisDayAllActiveOrder,EveryOrderGet,b004_OperationGoodsID,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private decimal? _ThisDayReturnActual;
        private decimal? _ThisDayMoneyByBoss;
        private decimal? _ThisDayMoneyAuto;
        private string _ThisDay;
        private Int32? _ThisDayAllActiveOrder;
        private decimal? _EveryOrderGet;
        private Int32? _b004_OperationGoodsID;
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
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        ///今天实际返还的钱  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ThisDayReturnActual
        {
            set{ _ThisDayReturnActual=value;}
            get{return _ThisDayReturnActual;}
        }
        /// <summary>
        ///老板决定返还的金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ThisDayMoneyByBoss
        {
            set{ _ThisDayMoneyByBoss=value;}
            get{return _ThisDayMoneyByBoss;}
        }
        /// <summary>
        ///今天应该全部返回的金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ThisDayMoneyAuto
        {
            set{ _ThisDayMoneyAuto=value;}
            get{return _ThisDayMoneyAuto;}
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
        ///当天总活动订单数  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ThisDayAllActiveOrder
        {
            set{ _ThisDayAllActiveOrder=value;}
            get{return _ThisDayAllActiveOrder;}
        }
        /// <summary>
        ///平均每订单得到的  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? EveryOrderGet
        {
            set{ _EveryOrderGet=value;}
            get{return _EveryOrderGet;}
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
