using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Goods 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Goods
    {
        public tab_Goods()
        {}
        #region Model
        //ID,ShopClient_ID,Class1_ID,Class2_ID,Class3_ID,isSaled,Name,Icon,ShortInfo,LongInfo,KuCunCount,Unit,kg,Price,MemberPrice,PromotePrice,IsCommend,HitCount,PromoteCount,UpTime,UpdateTime,ContactMan,Sort,IsDeleted,Good_Class,SalesCount,LimitTimerBuy_StartTime,LimitTimerBuy_EndTime,LimitTimerBuy_TimePrice,LimitTimerBuy_Bool,MinOrderNum,MaxOrderNum,LimitTimerBuy_MaxSalesCount,Shopping_Vouchers,IS_Admin_check,CheckBox_WeiBai_RedMoney,Webuy8_DistributionMoney_Value,FreightTemplate_ID,XML,AgentPercent,Shopping_Vouchers_Percent,WealthMoney,CreatTime,Send_Vouchers_IfBuy,Send_Money_IfBuy,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private int? _Class1_ID;
        private int? _Class2_ID;
        private int? _Class3_ID;
        private bool? _isSaled;
        private string _Name;
        private string _Icon;
        private string _ShortInfo;
        private string _LongInfo;
        private int? _KuCunCount;
        private string _Unit;
        private decimal? _kg;
        private decimal? _Price;
        private decimal? _MemberPrice;
        private decimal? _PromotePrice;
        private bool? _IsCommend;
        private int? _HitCount;
        private int? _PromoteCount;
        private DateTime? _UpTime;
        private DateTime? _UpdateTime;
        private string _ContactMan;
        private int? _Sort;
        private bool? _IsDeleted;
        private Int32? _Good_Class;
        private int? _SalesCount;
        private DateTime? _LimitTimerBuy_StartTime;
        private DateTime? _LimitTimerBuy_EndTime;
        private decimal? _LimitTimerBuy_TimePrice;
        private bool? _LimitTimerBuy_Bool;
        private Int32? _MinOrderNum;
        private Int32? _MaxOrderNum;
        private Int32? _LimitTimerBuy_MaxSalesCount;
        private bool? _Shopping_Vouchers;
        private bool? _IS_Admin_check;
        private bool? _CheckBox_WeiBai_RedMoney;
        private int? _Webuy8_DistributionMoney_Value;
        private int? _FreightTemplate_ID;
        private string _XML;
        private decimal? _AgentPercent;
        private decimal? _Shopping_Vouchers_Percent;
        private decimal? _WealthMoney;
        private DateTime? _CreatTime;
        private decimal? _Send_Vouchers_IfBuy;
        private decimal? _Send_Money_IfBuy;
        /// <summary>
        ///编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///商户的ID 外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        ///分类1的Id 外键  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? Class1_ID
        {
            set{ _Class1_ID=value;}
            get{return _Class1_ID;}
        }
        /// <summary>
        ///分类2的Id 外键  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? Class2_ID
        {
            set{ _Class2_ID=value;}
            get{return _Class2_ID;}
        }
        /// <summary>
        ///分类3的Id 外键  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? Class3_ID
        {
            set{ _Class3_ID=value;}
            get{return _Class3_ID;}
        }
        /// <summary>
        ///是否上架  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? isSaled
        {
            set{ _isSaled=value;}
            get{return _isSaled;}
        }
        /// <summary>
        ///货物名称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
        }
        /// <summary>
        ///货物图片  nvarchar 长度1024 占用字节数2048 小数位数0 允许空 默认值无 
        /// </summary>
        public string Icon
        {
            set{ _Icon=value;}
            get{return _Icon;}
        }
        /// <summary>
        ///短描述  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShortInfo
        {
            set{ _ShortInfo=value;}
            get{return _ShortInfo;}
        }
        /// <summary>
        ///长描述  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string LongInfo
        {
            set{ _LongInfo=value;}
            get{return _LongInfo;}
        }
        /// <summary>
        ///库存数量  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? KuCunCount
        {
            set{ _KuCunCount=value;}
            get{return _KuCunCount;}
        }
        /// <summary>
        ///单位名称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Unit
        {
            set{ _Unit=value;}
            get{return _Unit;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数3 允许空 默认值无 
        /// </summary>
        public decimal? kg
        {
            set{ _kg=value;}
            get{return _kg;}
        }
        /// <summary>
        ///价格  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Price
        {
            set{ _Price=value;}
            get{return _Price;}
        }
        /// <summary>
        ///会员价  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? MemberPrice
        {
            set{ _MemberPrice=value;}
            get{return _MemberPrice;}
        }
        /// <summary>
        ///打折价  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? PromotePrice
        {
            set{ _PromotePrice=value;}
            get{return _PromotePrice;}
        }
        /// <summary>
        ///是否推荐  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IsCommend
        {
            set{ _IsCommend=value;}
            get{return _IsCommend;}
        }
        /// <summary>
        ///点击次数  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? HitCount
        {
            set{ _HitCount=value;}
            get{return _HitCount;}
        }
        /// <summary>
        ///打折比率  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? PromoteCount
        {
            set{ _PromoteCount=value;}
            get{return _PromoteCount;}
        }
        /// <summary>
        ///上架时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? UpTime
        {
            set{ _UpTime=value;}
            get{return _UpTime;}
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
        ///联系人  由于该字段 废弃不用 所以 Model_tab_Goods.ContactMan = "WeiKanJiaOLdGoodID" + intOldGoodID;///利用该字段 存放核对微砍价信息  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactMan
        {
            set{ _ContactMan=value;}
            get{return _ContactMan;}
        }
        /// <summary>
        ///排序  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///是否删除  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///商铺的分类ID 外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Good_Class
        {
            set{ _Good_Class=value;}
            get{return _Good_Class;}
        }
        /// <summary>
        ///销售数量  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? SalesCount
        {
            set{ _SalesCount=value;}
            get{return _SalesCount;}
        }
        /// <summary>
        ///限时打折的开始时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? LimitTimerBuy_StartTime
        {
            set{ _LimitTimerBuy_StartTime=value;}
            get{return _LimitTimerBuy_StartTime;}
        }
        /// <summary>
        ///限时打折的结束时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? LimitTimerBuy_EndTime
        {
            set{ _LimitTimerBuy_EndTime=value;}
            get{return _LimitTimerBuy_EndTime;}
        }
        /// <summary>
        ///限时的价格  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? LimitTimerBuy_TimePrice
        {
            set{ _LimitTimerBuy_TimePrice=value;}
            get{return _LimitTimerBuy_TimePrice;}
        }
        /// <summary>
        ///是否限时打折  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? LimitTimerBuy_Bool
        {
            set{ _LimitTimerBuy_Bool=value;}
            get{return _LimitTimerBuy_Bool;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public Int32? MinOrderNum
        {
            set{ _MinOrderNum=value;}
            get{return _MinOrderNum;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? MaxOrderNum
        {
            set{ _MaxOrderNum=value;}
            get{return _MaxOrderNum;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? LimitTimerBuy_MaxSalesCount
        {
            set{ _LimitTimerBuy_MaxSalesCount=value;}
            get{return _LimitTimerBuy_MaxSalesCount;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Shopping_Vouchers
        {
            set{ _Shopping_Vouchers=value;}
            get{return _Shopping_Vouchers;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? IS_Admin_check
        {
            set{ _IS_Admin_check=value;}
            get{return _IS_Admin_check;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? CheckBox_WeiBai_RedMoney
        {
            set{ _CheckBox_WeiBai_RedMoney=value;}
            get{return _CheckBox_WeiBai_RedMoney;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? Webuy8_DistributionMoney_Value
        {
            set{ _Webuy8_DistributionMoney_Value=value;}
            get{return _Webuy8_DistributionMoney_Value;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? FreightTemplate_ID
        {
            set{ _FreightTemplate_ID=value;}
            get{return _FreightTemplate_ID;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string XML
        {
            set{ _XML=value;}
            get{return _XML;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AgentPercent
        {
            set{ _AgentPercent=value;}
            get{return _AgentPercent;}
        }
        /// <summary>
        ///购物券最多多少钱  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Shopping_Vouchers_Percent
        {
            set{ _Shopping_Vouchers_Percent=value;}
            get{return _Shopping_Vouchers_Percent;}
        }
        /// <summary>
        ///最大财富积分允许金额  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? WealthMoney
        {
            set{ _WealthMoney=value;}
            get{return _WealthMoney;}
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
        ///购买赠送的购物券 如果 取消购买 要扣除 直至为0  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Send_Vouchers_IfBuy
        {
            set{ _Send_Vouchers_IfBuy=value;}
            get{return _Send_Vouchers_IfBuy;}
        }
        /// <summary>
        ///购买赠送的现金余额 如果 取消购买 要扣除 直至为0  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Send_Money_IfBuy
        {
            set{ _Send_Money_IfBuy=value;}
            get{return _Send_Money_IfBuy;}
        }
        #endregion Model
    }
}
