using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_DistributionMoney 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_DistributionMoney
    {
        public tab_DistributionMoney()
        {}
        #region Model
        //ID,Partner,GreatParent,GrandParent,Parent,Name,UpdateTime,OperateMan,ShopGet,WeiBaiGet,Recommended,CreatTime,
        private Int32 _ID;
        private decimal? _Partner;
        private decimal? _GreatParent;
        private decimal? _GrandParent;
        private decimal? _Parent;
        private string _Name;
        private DateTime? _UpdateTime;
        private string _OperateMan;
        private decimal? _ShopGet;
        private decimal? _WeiBaiGet;
        private decimal? _Recommended;
        private DateTime? _CreatTime;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数0 允许空 默认值无 
        /// </summary>
        public decimal? Partner
        {
            set{ _Partner=value;}
            get{return _Partner;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数0 允许空 默认值无 
        /// </summary>
        public decimal? GreatParent
        {
            set{ _GreatParent=value;}
            get{return _GreatParent;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数0 允许空 默认值无 
        /// </summary>
        public decimal? GrandParent
        {
            set{ _GrandParent=value;}
            get{return _GrandParent;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数0 允许空 默认值无 
        /// </summary>
        public decimal? Parent
        {
            set{ _Parent=value;}
            get{return _Parent;}
        }
        /// <summary>
        ///  varchar 长度50 占用字节数50 小数位数0 允许空 默认值无 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
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
        ///  varchar 长度50 占用字节数50 小数位数0 允许空 默认值无 
        /// </summary>
        public string OperateMan
        {
            set{ _OperateMan=value;}
            get{return _OperateMan;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数0 允许空 默认值无 
        /// </summary>
        public decimal? ShopGet
        {
            set{ _ShopGet=value;}
            get{return _ShopGet;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数0 允许空 默认值无 
        /// </summary>
        public decimal? WeiBaiGet
        {
            set{ _WeiBaiGet=value;}
            get{return _WeiBaiGet;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数0 允许空 默认值无 
        /// </summary>
        public decimal? Recommended
        {
            set{ _Recommended=value;}
            get{return _Recommended;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        #endregion Model
    }
}
