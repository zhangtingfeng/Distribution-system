using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ZC_01Product_PartnerList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ZC_01Product_PartnerList
    {
        public tab_ZC_01Product_PartnerList()
        {}
        #region Model
        //ID,ZC_01ProductID,SupportID,PayPrice,ZCBuysSay,Ispay,PayTime,OrderID,Credentials,IsCanSendGoods,GetBonusID,ShopClientID,IsDeleted,CreateTime,UpdateTime,UserID,GetGoodsAddress,
        private Int32 _ID;
        private Int32? _ZC_01ProductID;
        private Int32? _SupportID;
        private decimal? _PayPrice;
        private string _ZCBuysSay;
        private bool? _Ispay;
        private DateTime? _PayTime;
        private Int32? _OrderID;
        private Int32? _Credentials;
        private bool? _IsCanSendGoods;
        private Int32? _GetBonusID;
        private Int32? _ShopClientID;
        private bool? _IsDeleted;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private Int32? _UserID;
        private Int32? _GetGoodsAddress;
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
        public Int32? ZC_01ProductID
        {
            set{ _ZC_01ProductID=value;}
            get{return _ZC_01ProductID;}
        }
        /// <summary>
        ///参与的那种方式的团购  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SupportID
        {
            set{ _SupportID=value;}
            get{return _SupportID;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? PayPrice
        {
            set{ _PayPrice=value;}
            get{return _PayPrice;}
        }
        /// <summary>
        ///众筹的留言  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string ZCBuysSay
        {
            set{ _ZCBuysSay=value;}
            get{return _ZCBuysSay;}
        }
        /// <summary>
        ///是否支付  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Ispay
        {
            set{ _Ispay=value;}
            get{return _Ispay;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? PayTime
        {
            set{ _PayTime=value;}
            get{return _PayTime;}
        }
        /// <summary>
        ///支付的订单号码 支付完成这里才填写。不支付 没必要写  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
        }
        /// <summary>
        ///抽奖的凭据 。这里写 顺序号码。。。写循序号。   bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Credentials
        {
            set{ _Credentials=value;}
            get{return _Credentials;}
        }
        /// <summary>
        ///中奖或者自然购买才能TRUE出现。 出现可以 发货。。订单表中 才显示 可以发货  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsCanSendGoods
        {
            set{ _IsCanSendGoods=value;}
            get{return _IsCanSendGoods;}
        }
        /// <summary>
        ///相关中奖详情  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? GetBonusID
        {
            set{ _GetBonusID=value;}
            get{return _GetBonusID;}
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
        public Int32? GetGoodsAddress
        {
            set{ _GetGoodsAddress=value;}
            get{return _GetGoodsAddress;}
        }
        #endregion Model
    }
}
