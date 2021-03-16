using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_ShopPar 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_ShopPar
    {
        public tab_ShopClient_ShopPar()
        {}
        #region Model
        //ID,ShopClientID,ShopShareGiveMoney,ShopShareGiveVouchers,GoodShareGiveMoney,GoodShareGiveVouchers,AddExpListTextShow,SubscribeGiveMoney,SubscribeGiveVouchers,AskAgentAuto,AskAgentAutoAfterBuy,SubscribeTipInfo,GouWuQuan_FirstVisitShop,Money_FirstVisitShop,PayMoneyMustHaveAddress,DeafaultOnlyShowAnounceBitmap,LimitMoney_AskMoney,GiveMoneyAfterOntime,CreatTime,CreateBy,UpdateTime,UpdateBy,IsDeleted,
        private int _ID;
        private Int32? _ShopClientID;
        private decimal? _ShopShareGiveMoney;
        private decimal? _ShopShareGiveVouchers;
        private decimal? _GoodShareGiveMoney;
        private decimal? _GoodShareGiveVouchers;
        private string _AddExpListTextShow;
        private decimal? _SubscribeGiveMoney;
        private decimal? _SubscribeGiveVouchers;
        private bool? _AskAgentAuto;
        private bool? _AskAgentAutoAfterBuy;
        private string _SubscribeTipInfo;
        private decimal? _GouWuQuan_FirstVisitShop;
        private decimal? _Money_FirstVisitShop;
        private bool? _PayMoneyMustHaveAddress;
        private bool? _DeafaultOnlyShowAnounceBitmap;
        private decimal? _LimitMoney_AskMoney;
        private bool? _GiveMoneyAfterOntime;
        private DateTime? _CreatTime;
        private string _CreateBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private Int32? _IsDeleted;
        /// <summary>
        /// 主键 int 长度10 占用字节数4 小数位数0 不允许空 默认值无 
        /// </summary>
        public int ID
        {
            set{ _ID=value;}
            get{return _ID;}
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
        ///分享商铺奖励现金  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0.00)) 
        /// </summary>
        public decimal? ShopShareGiveMoney
        {
            set{ _ShopShareGiveMoney=value;}
            get{return _ShopShareGiveMoney;}
        }
        /// <summary>
        ///分享商铺奖励购物券  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0.00)) 
        /// </summary>
        public decimal? ShopShareGiveVouchers
        {
            set{ _ShopShareGiveVouchers=value;}
            get{return _ShopShareGiveVouchers;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? GoodShareGiveMoney
        {
            set{ _GoodShareGiveMoney=value;}
            get{return _GoodShareGiveMoney;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? GoodShareGiveVouchers
        {
            set{ _GoodShareGiveVouchers=value;}
            get{return _GoodShareGiveVouchers;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExpListTextShow
        {
            set{ _AddExpListTextShow=value;}
            get{return _AddExpListTextShow;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? SubscribeGiveMoney
        {
            set{ _SubscribeGiveMoney=value;}
            get{return _SubscribeGiveMoney;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? SubscribeGiveVouchers
        {
            set{ _SubscribeGiveVouchers=value;}
            get{return _SubscribeGiveVouchers;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? AskAgentAuto
        {
            set{ _AskAgentAuto=value;}
            get{return _AskAgentAuto;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? AskAgentAutoAfterBuy
        {
            set{ _AskAgentAutoAfterBuy=value;}
            get{return _AskAgentAutoAfterBuy;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string SubscribeTipInfo
        {
            set{ _SubscribeTipInfo=value;}
            get{return _SubscribeTipInfo;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? GouWuQuan_FirstVisitShop
        {
            set{ _GouWuQuan_FirstVisitShop=value;}
            get{return _GouWuQuan_FirstVisitShop;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Money_FirstVisitShop
        {
            set{ _Money_FirstVisitShop=value;}
            get{return _Money_FirstVisitShop;}
        }
        /// <summary>
        ///支付时必须有收货地址  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? PayMoneyMustHaveAddress
        {
            set{ _PayMoneyMustHaveAddress=value;}
            get{return _PayMoneyMustHaveAddress;}
        }
        /// <summary>
        ///首页只显示轮播图  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? DeafaultOnlyShowAnounceBitmap
        {
            set{ _DeafaultOnlyShowAnounceBitmap=value;}
            get{return _DeafaultOnlyShowAnounceBitmap;}
        }
        /// <summary>
        ///提现最低额度  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((100)) 
        /// </summary>
        public decimal? LimitMoney_AskMoney
        {
            set{ _LimitMoney_AskMoney=value;}
            get{return _LimitMoney_AskMoney;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? GiveMoneyAfterOntime
        {
            set{ _GiveMoneyAfterOntime=value;}
            get{return _GiveMoneyAfterOntime;}
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
