using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Orderdetails 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Orderdetails
    {
        public tab_Orderdetails()
        {}
        #region Model
        //ID,OrderID,GoodID,Discount,GoodName,GoodPrice,OrderCount,Pinglun,ParentID,GrandParentID,GreatParentID,TeamID,Over7DaysToBeans,VouchersNum_List,Beans,MoneyCredits,MoneyWeBuy8Credits,WealthMoney,Freight,FreightShowText,ModifyPriceUpdateDateTime,ShopClient_ID,GoodType,GoodTypeId,GoodTypeIdBuyInfo,CreatDateTime,CreateBy,CreatTime,UpdateDateTime,UpdateBy,isdeleted,
        private Int32 _ID;
        private Int32? _OrderID;
        private Int32? _GoodID;
        private float? _Discount;
        private string _GoodName;
        private decimal? _GoodPrice;
        private int? _OrderCount;
        private string _Pinglun;
        private Int32? _ParentID;
        private Int32? _GrandParentID;
        private Int32? _GreatParentID;
        private Int32? _TeamID;
        private bool? _Over7DaysToBeans;
        private string _VouchersNum_List;
        private Int32? _Beans;
        private decimal? _MoneyCredits;
        private decimal? _MoneyWeBuy8Credits;
        private decimal? _WealthMoney;
        private decimal? _Freight;
        private string _FreightShowText;
        private DateTime? _ModifyPriceUpdateDateTime;
        private Int32? _ShopClient_ID;
        private Int32? _GoodType;
        private Int32? _GoodTypeId;
        private string _GoodTypeIdBuyInfo;
        private DateTime? _CreatDateTime;
        private string _CreateBy;
        private DateTime? _CreatTime;
        private DateTime? _UpdateDateTime;
        private string _UpdateBy;
        private bool? _isdeleted;
        /// <summary>
        ///编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///订单的id  外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
        }
        /// <summary>
        ///货物的id 外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
        }
        /// <summary>
        ///打折率  float 长度53 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public float? Discount
        {
            set{ _Discount=value;}
            get{return _Discount;}
        }
        /// <summary>
        ///货物名称  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string GoodName
        {
            set{ _GoodName=value;}
            get{return _GoodName;}
        }
        /// <summary>
        ///货物价格  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? GoodPrice
        {
            set{ _GoodPrice=value;}
            get{return _GoodPrice;}
        }
        /// <summary>
        ///商品订购数量  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? OrderCount
        {
            set{ _OrderCount=value;}
            get{return _OrderCount;}
        }
        /// <summary>
        ///本订单货物的评价  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string Pinglun
        {
            set{ _Pinglun=value;}
            get{return _Pinglun;}
        }
        /// <summary>
        ///支付后的订单 才有值  否否则请保持为null  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        /// <summary>
        ///支付后的订单 才有值  否否则请保持为null  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? GrandParentID
        {
            set{ _GrandParentID=value;}
            get{return _GrandParentID;}
        }
        /// <summary>
        ///支付后的订单 才有值  否否则请保持为null  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? GreatParentID
        {
            set{ _GreatParentID=value;}
            get{return _GreatParentID;}
        }
        /// <summary>
        ///支付后的订单该值才出现 否则 请保持为null.TeamID未 表tab_ShopClient_Agent_的主键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? TeamID
        {
            set{ _TeamID=value;}
            get{return _TeamID;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Over7DaysToBeans
        {
            set{ _Over7DaysToBeans=value;}
            get{return _Over7DaysToBeans;}
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
        ///运费  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? Freight
        {
            set{ _Freight=value;}
            get{return _Freight;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string FreightShowText
        {
            set{ _FreightShowText=value;}
            get{return _FreightShowText;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? ModifyPriceUpdateDateTime
        {
            set{ _ModifyPriceUpdateDateTime=value;}
            get{return _ModifyPriceUpdateDateTime;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
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
        ///微团购  是从 那里 什么人的 组团的 团购 信息 。参与的 谁的团，商品运营中心ID  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public string GoodTypeIdBuyInfo
        {
            set{ _GoodTypeIdBuyInfo=value;}
            get{return _GoodTypeIdBuyInfo;}
        }
        /// <summary>
        ///订单创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatDateTime
        {
            set{ _CreatDateTime=value;}
            get{return _CreatDateTime;}
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
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? UpdateDateTime
        {
            set{ _UpdateDateTime=value;}
            get{return _UpdateDateTime;}
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
        public bool? isdeleted
        {
            set{ _isdeleted=value;}
            get{return _isdeleted;}
        }
        #endregion Model
    }
}
