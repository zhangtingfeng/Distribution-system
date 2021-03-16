using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Shopping_VouchersScheme_Detail 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Shopping_VouchersScheme_Detail
    {
        public tab_ShopClient_Shopping_VouchersScheme_Detail()
        {}
        #region Model
        //ID,Scheme_ID,VouchersNum,GuWuCheIDOrOrderDetailID,ShopClientID,UserID,UserID_Old,Money,MoneyUsed,CreatTime,UpdateTime,ValidateStartTime,ValidateEndTime,
        private Int32 _ID;
        private Int32? _Scheme_ID;
        private string _VouchersNum;
        private Int32? _GuWuCheIDOrOrderDetailID;
        private Int32? _ShopClientID;
        private Int32? _UserID;
        private Int32? _UserID_Old;
        private decimal? _Money;
        private decimal? _MoneyUsed;
        private DateTime? _CreatTime;
        private DateTime? _UpdateTime;
        private DateTime? _ValidateStartTime;
        private DateTime? _ValidateEndTime;
        /// <summary>
        ///全局编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Scheme_ID
        {
            set{ _Scheme_ID=value;}
            get{return _Scheme_ID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string VouchersNum
        {
            set{ _VouchersNum=value;}
            get{return _VouchersNum;}
        }
        /// <summary>
        ///使用该购物券的订单列表  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? GuWuCheIDOrOrderDetailID
        {
            set{ _GuWuCheIDOrOrderDetailID=value;}
            get{return _GuWuCheIDOrOrderDetailID;}
        }
        /// <summary>
        ///商户的ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
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
        ///第一个领用人  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID_Old
        {
            set{ _UserID_Old=value;}
            get{return _UserID_Old;}
        }
        /// <summary>
        ///购物券面值金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Money
        {
            set{ _Money=value;}
            get{return _Money;}
        }
        /// <summary>
        ///有效消费的金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? MoneyUsed
        {
            set{ _MoneyUsed=value;}
            get{return _MoneyUsed;}
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
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? ValidateStartTime
        {
            set{ _ValidateStartTime=value;}
            get{return _ValidateStartTime;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? ValidateEndTime
        {
            set{ _ValidateEndTime=value;}
            get{return _ValidateEndTime;}
        }
        #endregion Model
    }
}
