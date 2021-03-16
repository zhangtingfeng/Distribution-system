using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Total_Vouchers_Consume_Or_Recharge 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Total_Vouchers_Consume_Or_Recharge
    {
        public tab_Total_Vouchers_Consume_Or_Recharge()
        {}
        #region Model
        //ID,UserID,ShopClient_ID,UpdateTime,ConsumeOrRechargeType,ConsumeOrRecharge_Vouchers,ConsumeTypeOrRecharge,RemainingSum_Vouchers,Bool_ConsumeOrRecharge,BoolIfOnlyonceUpdate,CreatTime,Creatby,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _ShopClient_ID;
        private DateTime? _UpdateTime;
        private Int32? _ConsumeOrRechargeType;
        private decimal? _ConsumeOrRecharge_Vouchers;
        private string _ConsumeTypeOrRecharge;
        private decimal? _RemainingSum_Vouchers;
        private bool? _Bool_ConsumeOrRecharge;
        private bool? _BoolIfOnlyonceUpdate;
        private DateTime? _CreatTime;
        private string _Creatby;
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
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
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
        ///从 0 到 255 的整型数据。存储大小为 1 字节。  消费类型或者收入类型。1-100 表示 收入类型（1表示充值收入 10表示分销收入，11表示团队收入，12表示真情收入13组团收入 20表示下级访问商品收入，21购买赠送，22游戏赠送，30表示平台增加商家调节收入 40表示商品访问收入 41表示扫描代理赠送42关注赠送43首次访问赠送44签到赠送 50表示咨询访问收入 60表示签到收入 70表示红包收入 71红包退回72取消提现 80表示清除购物车81自动兑现现金 90表示待支付订单取消 91已支付取消92市场调查推广费发放）     101-200表示消费类型。（110表示购物车消耗 120表示红包消耗  130提现 140平台减少）  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ConsumeOrRechargeType
        {
            set{ _ConsumeOrRechargeType=value;}
            get{return _ConsumeOrRechargeType;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ConsumeOrRecharge_Vouchers
        {
            set{ _ConsumeOrRecharge_Vouchers=value;}
            get{return _ConsumeOrRecharge_Vouchers;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string ConsumeTypeOrRecharge
        {
            set{ _ConsumeTypeOrRecharge=value;}
            get{return _ConsumeTypeOrRecharge;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? RemainingSum_Vouchers
        {
            set{ _RemainingSum_Vouchers=value;}
            get{return _RemainingSum_Vouchers;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Bool_ConsumeOrRecharge
        {
            set{ _Bool_ConsumeOrRecharge=value;}
            get{return _Bool_ConsumeOrRecharge;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? BoolIfOnlyonceUpdate
        {
            set{ _BoolIfOnlyonceUpdate=value;}
            get{return _BoolIfOnlyonceUpdate;}
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
        public string Creatby
        {
            set{ _Creatby=value;}
            get{return _Creatby;}
        }
        #endregion Model
    }
}
