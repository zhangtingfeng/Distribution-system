using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_TuanGou 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_TuanGou
    {
        public tab_TuanGou()
        {}
        #region Model
        //ID,HowManyPeople,EachPeoplePrice,AgentPrice,TuanZhangBonus_AgentGet,TuanZhangBonus_GouWuQuan,TuanZhangBonus_Money,ShopClientID,SourceGoodID,IsDeleted,CreateTime,UpdateTime,IsSales,TuanFouRule,Sort,MustSubscribe_Master,MustSubscribe_Helper,MustAddress_Master,MustAgent_Master,WhenEndAllGroup,MaxTimeLengthDoGroup,ChoiceWhenEndAllGroup,ChoiceMaxTimeLengthDoGroup,BuyMultiOnlyOneAccount,
        private Int32 _ID;
        private Int32? _HowManyPeople;
        private decimal? _EachPeoplePrice;
        private decimal? _AgentPrice;
        private bool? _TuanZhangBonus_AgentGet;
        private decimal? _TuanZhangBonus_GouWuQuan;
        private decimal? _TuanZhangBonus_Money;
        private Int32? _ShopClientID;
        private Int32? _SourceGoodID;
        private bool? _IsDeleted;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private bool? _IsSales;
        private string _TuanFouRule;
        private Int32? _Sort;
        private bool? _MustSubscribe_Master;
        private bool? _MustSubscribe_Helper;
        private bool? _MustAddress_Master;
        private bool? _MustAgent_Master;
        private DateTime? _WhenEndAllGroup;
        private Int32? _MaxTimeLengthDoGroup;
        private bool? _ChoiceWhenEndAllGroup;
        private bool? _ChoiceMaxTimeLengthDoGroup;
        private bool? _BuyMultiOnlyOneAccount;
        /// <summary>
        ///序号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///多少人参与团购 才可以满足条件  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? HowManyPeople
        {
            set{ _HowManyPeople=value;}
            get{return _HowManyPeople;}
        }
        /// <summary>
        ///每个人的 团购所出的价格  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? EachPeoplePrice
        {
            set{ _EachPeoplePrice=value;}
            get{return _EachPeoplePrice;}
        }
        /// <summary>
        ///代理商利润  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AgentPrice
        {
            set{ _AgentPrice=value;}
            get{return _AgentPrice;}
        }
        /// <summary>
        ///取得代理商利润  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? TuanZhangBonus_AgentGet
        {
            set{ _TuanZhangBonus_AgentGet=value;}
            get{return _TuanZhangBonus_AgentGet;}
        }
        /// <summary>
        ///组团成功奖励购物券,0表示不奖励  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? TuanZhangBonus_GouWuQuan
        {
            set{ _TuanZhangBonus_GouWuQuan=value;}
            get{return _TuanZhangBonus_GouWuQuan;}
        }
        /// <summary>
        ///组团成功奖励现金,0表示不奖励  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? TuanZhangBonus_Money
        {
            set{ _TuanZhangBonus_Money=value;}
            get{return _TuanZhangBonus_Money;}
        }
        /// <summary>
        ///店铺ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///原商品详情  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SourceGoodID
        {
            set{ _SourceGoodID=value;}
            get{return _SourceGoodID;}
        }
        /// <summary>
        ///是否 删除  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreateTime
        {
            set{ _CreateTime=value;}
            get{return _CreateTime;}
        }
        /// <summary>
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///是否上架  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? IsSales
        {
            set{ _IsSales=value;}
            get{return _IsSales;}
        }
        /// <summary>
        ///团购商品描述  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string TuanFouRule
        {
            set{ _TuanFouRule=value;}
            get{return _TuanFouRule;}
        }
        /// <summary>
        ///排序位置  越大 越在后面  Int32 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///发起组团（成为团长）是否必须关注  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustSubscribe_Master
        {
            set{ _MustSubscribe_Master=value;}
            get{return _MustSubscribe_Master;}
        }
        /// <summary>
        ///参与组团（成为团员）是否必须关注  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustSubscribe_Helper
        {
            set{ _MustSubscribe_Helper=value;}
            get{return _MustSubscribe_Helper;}
        }
        /// <summary>
        ///成为团员是否必须输入收获地址  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustAddress_Master
        {
            set{ _MustAddress_Master=value;}
            get{return _MustAddress_Master;}
        }
        /// <summary>
        ///是否只有代理商才有资格发起组团（成为团长），否则跳至申请分销商资格页面  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustAgent_Master
        {
            set{ _MustAgent_Master=value;}
            get{return _MustAgent_Master;}
        }
        /// <summary>
        ///最终截至时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? WhenEndAllGroup
        {
            set{ _WhenEndAllGroup=value;}
            get{return _WhenEndAllGroup;}
        }
        /// <summary>
        ///开团指定小时后未达到指定人数由客服介入处理退款事宜  Int32 长度10 占用字节数4 小数位数0 允许空 默认值((24)) 
        /// </summary>
        public Int32? MaxTimeLengthDoGroup
        {
            set{ _MaxTimeLengthDoGroup=value;}
            get{return _MaxTimeLengthDoGroup;}
        }
        /// <summary>
        ///选择开团多少小时后自动结束  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? ChoiceWhenEndAllGroup
        {
            set{ _ChoiceWhenEndAllGroup=value;}
            get{return _ChoiceWhenEndAllGroup;}
        }
        /// <summary>
        ///选择开团指定小时后  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? ChoiceMaxTimeLengthDoGroup
        {
            set{ _ChoiceMaxTimeLengthDoGroup=value;}
            get{return _ChoiceMaxTimeLengthDoGroup;}
        }
        /// <summary>
        ///同一微信号（本店不支持一个手机切换微信号）是否可以在一次团购活动中购买多个。  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? BuyMultiOnlyOneAccount
        {
            set{ _BuyMultiOnlyOneAccount=value;}
            get{return _BuyMultiOnlyOneAccount;}
        }
        #endregion Model
    }
}
