using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Shopping_VouchersScheme 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Shopping_VouchersScheme
    {
        public tab_ShopClient_Shopping_VouchersScheme()
        {}
        #region Model
        //ID,Vouchers_Title,Money,AllCount,ShopClientID,GoodList,HowToUse,HowToUseLimitMaxMoney,LimitHowMany,ValidateStartTime,ValidateEndTime,ValidateDateTypeRelative,ValidateTypeAbsoluteCheck,ValidateTypeRelativeCheck,HowToGet,CreatTime,UpdateTime,
        private Int32 _ID;
        private string _Vouchers_Title;
        private decimal? _Money;
        private Int32? _AllCount;
        private Int32? _ShopClientID;
        private string _GoodList;
        private Int32? _HowToUse;
        private decimal? _HowToUseLimitMaxMoney;
        private Int32? _LimitHowMany;
        private DateTime? _ValidateStartTime;
        private DateTime? _ValidateEndTime;
        private int? _ValidateDateTypeRelative;
        private bool? _ValidateTypeAbsoluteCheck;
        private bool? _ValidateTypeRelativeCheck;
        private Int32? _HowToGet;
        private DateTime? _CreatTime;
        private DateTime? _UpdateTime;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Vouchers_Title
        {
            set{ _Vouchers_Title=value;}
            get{return _Vouchers_Title;}
        }
        /// <summary>
        ///  money 长度19 占用字节数8 小数位数4 允许空 默认值无 
        /// </summary>
        public decimal? Money
        {
            set{ _Money=value;}
            get{return _Money;}
        }
        /// <summary>
        ///发放总量只能是正整数，在1-100000000之间  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? AllCount
        {
            set{ _AllCount=value;}
            get{return _AllCount;}
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
        ///可使用商品  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string GoodList
        {
            set{ _GoodList=value;}
            get{return _GoodList;}
        }
        /// <summary>
        ///限制使用方式  随意使用，不限制（按照商品最大购物券规则）（有效代替规则，多余的面额由系统回收） 满足多少金额才能使用（按照商品最大购物券规则）  smallint 长度5 占用字节数2 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? HowToUse
        {
            set{ _HowToUse=value;}
            get{return _HowToUse;}
        }
        /// <summary>
        ///满足限制金额不能为空  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? HowToUseLimitMaxMoney
        {
            set{ _HowToUseLimitMaxMoney=value;}
            get{return _HowToUseLimitMaxMoney;}
        }
        /// <summary>
        ///限制使用张数 0表示不限制  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? LimitHowMany
        {
            set{ _LimitHowMany=value;}
            get{return _LimitHowMany;}
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
        /// <summary>
        ///过期方式 领用后多少天过期  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? ValidateDateTypeRelative
        {
            set{ _ValidateDateTypeRelative=value;}
            get{return _ValidateDateTypeRelative;}
        }
        /// <summary>
        ///过期方式 领用后多少天过期  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ValidateTypeAbsoluteCheck
        {
            set{ _ValidateTypeAbsoluteCheck=value;}
            get{return _ValidateTypeAbsoluteCheck;}
        }
        /// <summary>
        ///过期方式 有效期起始日期  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ValidateTypeRelativeCheck
        {
            set{ _ValidateTypeRelativeCheck=value;}
            get{return _ValidateTypeRelativeCheck;}
        }
        /// <summary>
        ///领取方式线上发放（主动领取）线下发放（指定领取）线上发放（主动领取）线下发放（指定领取）线上发放（主动领取）线下发放（指定领取）  smallint 长度5 占用字节数2 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? HowToGet
        {
            set{ _HowToGet=value;}
            get{return _HowToGet;}
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
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        #endregion Model
    }
}
