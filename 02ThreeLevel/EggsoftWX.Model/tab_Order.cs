using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Order 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Order
    {
        public tab_Order()
        {}
        #region Model
        //ID,PayStatus,isReceipt,CreateDateTime,UserID,DeliveryText,TotalMoney,OrderNum,ShopClient_ID,OrderName,User_Address,PayWay,PaywayOrderNum,PayDateTime,O2OTakedID,FreightShowText,ModifyPriceUpdateDateTime,CreatTime,CreateBy,UpdateDateTime,UpdateBy,IsDeleted,
        private Int32 _ID;
        private Int32? _PayStatus;
        private bool? _isReceipt;
        private DateTime? _CreateDateTime;
        private Int32 _UserID;
        private string _DeliveryText;
        private decimal? _TotalMoney;
        private string _OrderNum;
        private Int32 _ShopClient_ID;
        private string _OrderName;
        private Int32? _User_Address;
        private string _PayWay;
        private string _PaywayOrderNum;
        private DateTime? _PayDateTime;
        private int? _O2OTakedID;
        private string _FreightShowText;
        private DateTime? _ModifyPriceUpdateDateTime;
        private DateTime? _CreatTime;
        private string _CreateBy;
        private DateTime? _UpdateDateTime;
        private string _UpdateBy;
        private bool? _IsDeleted;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///0或者null 表示未知付  ，1表示已支付  2表示用户取消订单 3表示退款退单  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? PayStatus
        {
            set{ _PayStatus=value;}
            get{return _PayStatus;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? isReceipt
        {
            set{ _isReceipt=value;}
            get{return _isReceipt;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreateDateTime
        {
            set{ _CreateDateTime=value;}
            get{return _CreateDateTime;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 不允许空 默认值((0)) 
        /// </summary>
        public Int32 UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值('') 
        /// </summary>
        public string DeliveryText
        {
            set{ _DeliveryText=value;}
            get{return _DeliveryText;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? TotalMoney
        {
            set{ _TotalMoney=value;}
            get{return _TotalMoney;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string OrderNum
        {
            set{ _OrderNum=value;}
            get{return _OrderNum;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 不允许空 默认值((0)) 
        /// </summary>
        public Int32 ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string OrderName
        {
            set{ _OrderName=value;}
            get{return _OrderName;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? User_Address
        {
            set{ _User_Address=value;}
            get{return _User_Address;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string PayWay
        {
            set{ _PayWay=value;}
            get{return _PayWay;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string PaywayOrderNum
        {
            set{ _PaywayOrderNum=value;}
            get{return _PaywayOrderNum;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? PayDateTime
        {
            set{ _PayDateTime=value;}
            get{return _PayDateTime;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? O2OTakedID
        {
            set{ _O2OTakedID=value;}
            get{return _O2OTakedID;}
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
        ///修改价格时更新该时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? ModifyPriceUpdateDateTime
        {
            set{ _ModifyPriceUpdateDateTime=value;}
            get{return _ModifyPriceUpdateDateTime;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
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
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
