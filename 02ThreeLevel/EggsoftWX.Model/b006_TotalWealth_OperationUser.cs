using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b006_TotalWealth_OperationUser 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b006_TotalWealth_OperationUser
    {
        public b006_TotalWealth_OperationUser()
        {}
        #region Model
        //ID,UserID,ShopClient_ID,OrderDetailID,UpdateTime,ConsumeOrRechargeType,ConsumeOrRechargeWealth,ConsumeTypeOrRecharge,RemainingSum,Bool_ConsumeOrRecharge,BoolIfOnlyonceUpdate,CreatTime,Creatby,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _ShopClient_ID;
        private Int32? _OrderDetailID;
        private DateTime? _UpdateTime;
        private Int32? _ConsumeOrRechargeType;
        private decimal? _ConsumeOrRechargeWealth;
        private string _ConsumeTypeOrRecharge;
        private decimal? _RemainingSum;
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OrderDetailID
        {
            set{ _OrderDetailID=value;}
            get{return _OrderDetailID;}
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
        ///从 0 到 255 的整型数据。存储大小为 1 字节。  消费类型或者收入类型。1-100 表示 收入类型（10表示分销收入，20表示下级访问商品收入，30表示商家调节收入 40表示商品访问收入 50表示咨询访问收入 60表示签到收入 70表示红包收入 80表示清除购物车 90表示订单取消）     101-200表示消费类型。（110表示购物车消耗 120表示红包消耗 ）  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ConsumeOrRechargeType
        {
            set{ _ConsumeOrRechargeType=value;}
            get{return _ConsumeOrRechargeType;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? ConsumeOrRechargeWealth
        {
            set{ _ConsumeOrRechargeWealth=value;}
            get{return _ConsumeOrRechargeWealth;}
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
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? RemainingSum
        {
            set{ _RemainingSum=value;}
            get{return _RemainingSum;}
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
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string Creatby
        {
            set{ _Creatby=value;}
            get{return _Creatby;}
        }
        #endregion Model
    }
}
