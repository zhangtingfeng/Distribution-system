using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Order_ShopingCart 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Order_ShopingCart
    {
        public tab_Order_ShopingCart()
        {}
        #region Model
        //ID,UserID,GoodID,GoodIDCount,MultiBuyType,VouchersNum_List,Beans,MoneyCredits,MoneyWeBuy8Credits,WealthMoney,ShopClientID,GoodType,GoodTypeId,GoodTypeIdBuyInfo,UserSay,checkChoice,CreatTime,CreateBy,UpdateTime,UpdateBy,IsDeleted,IsDeletedTime,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _GoodID;
        private int? _GoodIDCount;
        private Int32? _MultiBuyType;
        private string _VouchersNum_List;
        private Int32? _Beans;
        private decimal? _MoneyCredits;
        private decimal? _MoneyWeBuy8Credits;
        private decimal? _WealthMoney;
        private Int32? _ShopClientID;
        private Int32? _GoodType;
        private Int32? _GoodTypeId;
        private string _GoodTypeIdBuyInfo;
        private string _UserSay;
        private bool? _checkChoice;
        private DateTime? _CreatTime;
        private string _CreateBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private bool? _IsDeleted;
        private DateTime? _IsDeletedTime;
        /// <summary>
        ///编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///用户表的Id 外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///商品的ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
        }
        /// <summary>
        ///购物车 本商品的数量  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? GoodIDCount
        {
            set{ _GoodIDCount=value;}
            get{return _GoodIDCount;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? MultiBuyType
        {
            set{ _MultiBuyType=value;}
            get{return _MultiBuyType;}
        }
        /// <summary>
        ///  nvarchar 长度300 占用字节数600 小数位数0 允许空 默认值无 
        /// </summary>
        public string VouchersNum_List
        {
            set{ _VouchersNum_List=value;}
            get{return _VouchersNum_List;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Beans
        {
            set{ _Beans=value;}
            get{return _Beans;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? MoneyCredits
        {
            set{ _MoneyCredits=value;}
            get{return _MoneyCredits;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? MoneyWeBuy8Credits
        {
            set{ _MoneyWeBuy8Credits=value;}
            get{return _MoneyWeBuy8Credits;}
        }
        /// <summary>
        ///是否使用财富购买  大于0  就表示使用了  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? WealthMoney
        {
            set{ _WealthMoney=value;}
            get{return _WealthMoney;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心  smallint 长度5 占用字节数2 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? GoodType
        {
            set{ _GoodType=value;}
            get{return _GoodType;}
        }
        /// <summary>
        ///商品 来源 类型  表的 主键   正常订单 这个 为空  .微砍价 团购 会出现相关主键运营中心ID    bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? GoodTypeId
        {
            set{ _GoodTypeId=value;}
            get{return _GoodTypeId;}
        }
        /// <summary>
        ///微团购  是从 那里 什么人的 组团的 团购 信息 。参与的 谁的团商品运营中心ID  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public string GoodTypeIdBuyInfo
        {
            set{ _GoodTypeIdBuyInfo=value;}
            get{return _GoodTypeIdBuyInfo;}
        }
        /// <summary>
        ///用户的购买留言  。众筹购买 可能会留下留言  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserSay
        {
            set{ _UserSay=value;}
            get{return _UserSay;}
        }
        /// <summary>
        ///小程序是否选择  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? checkChoice
        {
            set{ _checkChoice=value;}
            get{return _checkChoice;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
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
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
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
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? IsDeletedTime
        {
            set{ _IsDeletedTime=value;}
            get{return _IsDeletedTime;}
        }
        #endregion Model
    }
}
