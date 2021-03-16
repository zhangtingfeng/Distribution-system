using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b004_OperationGoods 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b004_OperationGoods
    {
        public b004_OperationGoods()
        {}
        #region Model
        //ID,GoodID,ShopClient_ID,RunningStatus,MoneyConsumerWeighting,LimitBuyEveryMonth,ReturnMoneyShareA,ReturnMoneyShareB,ReturnMoneyOperationShareA,ReturnMoneyOperationShareB,ReturnMoneyToCompany,ReturnConsumerWealth,HowToReturnMoneyA,HowToReturnMoneyB,DiscountGoodID,ShowConsumerWealthAgreement,ConsumerWealthAgreement,ConsumerWealthDrawMoney,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _GoodID;
        private Int32? _ShopClient_ID;
        private bool? _RunningStatus;
        private decimal? _MoneyConsumerWeighting;
        private Int32? _LimitBuyEveryMonth;
        private decimal? _ReturnMoneyShareA;
        private decimal? _ReturnMoneyShareB;
        private decimal? _ReturnMoneyOperationShareA;
        private decimal? _ReturnMoneyOperationShareB;
        private decimal? _ReturnMoneyToCompany;
        private decimal? _ReturnConsumerWealth;
        private decimal? _HowToReturnMoneyA;
        private decimal? _HowToReturnMoneyB;
        private Int32? _DiscountGoodID;
        private bool? _ShowConsumerWealthAgreement;
        private string _ConsumerWealthAgreement;
        private string _ConsumerWealthDrawMoney;
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
        ///选择消费商品  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
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
        ///商品运营状态  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? RunningStatus
        {
            set{ _RunningStatus=value;}
            get{return _RunningStatus;}
        }
        /// <summary>
        ///企业利润的X%作为消费者消费投资回报给消费者  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? MoneyConsumerWeighting
        {
            set{ _MoneyConsumerWeighting=value;}
            get{return _MoneyConsumerWeighting;}
        }
        /// <summary>
        ///每个自然月 限制购买单数  0表示不限制  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? LimitBuyEveryMonth
        {
            set{ _LimitBuyEveryMonth=value;}
            get{return _LimitBuyEveryMonth;}
        }
        /// <summary>
        ///分享者A（间接推）%：  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ReturnMoneyShareA
        {
            set{ _ReturnMoneyShareA=value;}
            get{return _ReturnMoneyShareA;}
        }
        /// <summary>
        ///分享者B（间接推）%：  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ReturnMoneyShareB
        {
            set{ _ReturnMoneyShareB=value;}
            get{return _ReturnMoneyShareB;}
        }
        /// <summary>
        ///运营中心A（直接推）  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ReturnMoneyOperationShareA
        {
            set{ _ReturnMoneyOperationShareA=value;}
            get{return _ReturnMoneyOperationShareA;}
        }
        /// <summary>
        ///运营中心B（直接推）  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ReturnMoneyOperationShareB
        {
            set{ _ReturnMoneyOperationShareB=value;}
            get{return _ReturnMoneyOperationShareB;}
        }
        /// <summary>
        ///产品成本、运营成本、税金、物流费、系统维护费  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ReturnMoneyToCompany
        {
            set{ _ReturnMoneyToCompany=value;}
            get{return _ReturnMoneyToCompany;}
        }
        /// <summary>
        ///消费者下单购买。按照购买金额进行财富返还。消费者的财富中心直接增加多少倍的财富基金。按照企业返还给消费者的回报，每日加权分红  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ReturnConsumerWealth
        {
            set{ _ReturnConsumerWealth=value;}
            get{return _ReturnConsumerWealth;}
        }
        /// <summary>
        ///股东类 每天如何归还 钱 。--1 表示不归还   0表示按照系统产生的订单自动归还。  其他>0的表示按照这个值进行加权分红  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? HowToReturnMoneyA
        {
            set{ _HowToReturnMoneyA=value;}
            get{return _HowToReturnMoneyA;}
        }
        /// <summary>
        ///非股东类 每天如何归还 钱 。--1 表示不归还   0表示按照系统产生的订单自动归还。  其他>0的表示按照这个值进行加权分红  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? HowToReturnMoneyB
        {
            set{ _HowToReturnMoneyB=value;}
            get{return _HowToReturnMoneyB;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? DiscountGoodID
        {
            set{ _DiscountGoodID=value;}
            get{return _DiscountGoodID;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ShowConsumerWealthAgreement
        {
            set{ _ShowConsumerWealthAgreement=value;}
            get{return _ShowConsumerWealthAgreement;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string ConsumerWealthAgreement
        {
            set{ _ConsumerWealthAgreement=value;}
            get{return _ConsumerWealthAgreement;}
        }
        /// <summary>
        ///提现显示的阅读须知  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string ConsumerWealthDrawMoney
        {
            set{ _ConsumerWealthDrawMoney=value;}
            get{return _ConsumerWealthDrawMoney;}
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
