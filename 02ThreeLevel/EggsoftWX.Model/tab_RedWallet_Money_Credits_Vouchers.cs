using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_RedWallet_Money_Credits_Vouchers 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_RedWallet_Money_Credits_Vouchers
    {
        public tab_RedWallet_Money_Credits_Vouchers()
        {}
        #region Model
        //ID,PID,Money,UserID,ISClosed,HowmanyPeople,Type_Or_Money_Credits_Vouchers,ShopClientID,SendMoneyByRedBagID,CreatTime,CreateBy,UpdateTime,UpdateBy,
        private Int32 _ID;
        private Int32? _PID;
        private decimal? _Money;
        private Int32? _UserID;
        private bool? _ISClosed;
        private int? _HowmanyPeople;
        private int? _Type_Or_Money_Credits_Vouchers;
        private int? _ShopClientID;
        private Int32? _SendMoneyByRedBagID;
        private DateTime? _CreatTime;
        private string _CreateBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///这个红包是哪个ID发出的  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? PID
        {
            set{ _PID=value;}
            get{return _PID;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Money
        {
            set{ _Money=value;}
            get{return _Money;}
        }
        /// <summary>
        ///那个用户发出的  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///是否关闭  只要针对PID=0   的 超过 24小时的  就要关闭它  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? ISClosed
        {
            set{ _ISClosed=value;}
            get{return _ISClosed;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? HowmanyPeople
        {
            set{ _HowmanyPeople=value;}
            get{return _HowmanyPeople;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Type_Or_Money_Credits_Vouchers
        {
            set{ _Type_Or_Money_Credits_Vouchers=value;}
            get{return _Type_Or_Money_Credits_Vouchers;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SendMoneyByRedBagID
        {
            set{ _SendMoneyByRedBagID=value;}
            get{return _SendMoneyByRedBagID;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
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
        #endregion Model
    }
}
