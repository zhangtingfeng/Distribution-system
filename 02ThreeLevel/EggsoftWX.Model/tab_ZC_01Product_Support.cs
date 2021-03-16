using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ZC_01Product_Support 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ZC_01Product_Support
    {
        public tab_ZC_01Product_Support()
        {}
        #region Model
        //ID,Name,ZC_01ProductID,SalesPrice,AgentPrice,SalesLimit,SalesPricePromiseAndReturn,SourceGoodID,ShopClientID,IsSales,Sort,IsDeleted,CreateTime,UpdateTime,SupportWay,SupportHowMany,MustSubscribe,MustAddress,OnlyBuyOneOnlyOneAccount,
        private Int32 _ID;
        private string _Name;
        private Int32? _ZC_01ProductID;
        private decimal? _SalesPrice;
        private decimal? _AgentPrice;
        private Int32? _SalesLimit;
        private string _SalesPricePromiseAndReturn;
        private Int32? _SourceGoodID;
        private Int32? _ShopClientID;
        private bool? _IsSales;
        private Int32? _Sort;
        private bool? _IsDeleted;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private Int32? _SupportWay;
        private Int32? _SupportHowMany;
        private bool? _MustSubscribe;
        private bool? _MustAddress;
        private bool? _OnlyBuyOneOnlyOneAccount;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///众筹名称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ZC_01ProductID
        {
            set{ _ZC_01ProductID=value;}
            get{return _ZC_01ProductID;}
        }
        /// <summary>
        ///打包销售价格  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? SalesPrice
        {
            set{ _SalesPrice=value;}
            get{return _SalesPrice;}
        }
        /// <summary>
        ///给代理商的代理金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AgentPrice
        {
            set{ _AgentPrice=value;}
            get{return _AgentPrice;}
        }
        /// <summary>
        ///档位名额限制。表示参与众筹名额。一旦达到。就不能产生新的购买。购物车 未支付的订单都算是已经参与。系统定期清除无效的参与。0表示不限制  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? SalesLimit
        {
            set{ _SalesLimit=value;}
            get{return _SalesLimit;}
        }
        /// <summary>
        ///当前价格将如何回报 最多250字 前台展示给用户  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string SalesPricePromiseAndReturn
        {
            set{ _SalesPricePromiseAndReturn=value;}
            get{return _SalesPricePromiseAndReturn;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SourceGoodID
        {
            set{ _SourceGoodID=value;}
            get{return _SourceGoodID;}
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
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IsSales
        {
            set{ _IsSales=value;}
            get{return _IsSales;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreateTime
        {
            set{ _CreateTime=value;}
            get{return _CreateTime;}
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
        ///0 需要发货   1无偿支持，不需回报，无需发货   2股权类众筹，后期回报，无需发货  本平台不支持众筹库存量/销量自动统计 0支付即发货  1双色球计算中奖发货 2福彩3D计算中奖发货  3无偿支持，不 需回报，无需发货  4股权类众筹，后期回报，无需发货  是否 选用开奖方法。  0  表示 不选 。1表示使用双色球数据 （双色球开奖时间为每周二、四、日的21：30），系统将在（每周二、四、日）10点开奖。  2表示使用3d数据（每天晚上8点30分） 系统将在每天的10点开奖。  如果选择，必须输入满足多少个后开奖。  开奖结果 微信推送给相关用户。或者 登陆网页查看   bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? SupportWay
        {
            set{ _SupportWay=value;}
            get{return _SupportWay;}
        }
        /// <summary>
        ///每销售多少个开奖，如果满足，开奖时间见开奖方法。如果不满足，也开奖，开奖时间在项目结束当天的晚10点   bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SupportHowMany
        {
            set{ _SupportHowMany=value;}
            get{return _SupportHowMany;}
        }
        /// <summary>
        ///发起众筹是否必须关注  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustSubscribe
        {
            set{ _MustSubscribe=value;}
            get{return _MustSubscribe;}
        }
        /// <summary>
        ///参与是否必须输入收获地址  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustAddress
        {
            set{ _MustAddress=value;}
            get{return _MustAddress;}
        }
        /// <summary>
        ///是否限制 每个微信号只能参与一次当前档位众筹  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? OnlyBuyOneOnlyOneAccount
        {
            set{ _OnlyBuyOneOnlyOneAccount=value;}
            get{return _OnlyBuyOneOnlyOneAccount;}
        }
        #endregion Model
    }
}
