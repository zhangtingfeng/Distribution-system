using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_WeiKanJia 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_WeiKanJia
    {
        public tab_WeiKanJia()
        {}
        #region Model
        //ID,Topic,UpdateTime,KanJiaRule,StartPrice,EndPrice,AgentPrice,EachAction_LowPrice,EachAction_HighPrice,ShopClientID,MustAddress_Master,MustSubscribe_Master,MustSubscribe_Helper,KanJiaTopicDescContent,EndTime,MustSubscribe_Agent,isSaled,Sort,isdeleted,GoodID,CreatTime,
        private Int32 _ID;
        private string _Topic;
        private DateTime? _UpdateTime;
        private string _KanJiaRule;
        private decimal? _StartPrice;
        private decimal? _EndPrice;
        private decimal? _AgentPrice;
        private decimal? _EachAction_LowPrice;
        private decimal? _EachAction_HighPrice;
        private Int32? _ShopClientID;
        private bool? _MustAddress_Master;
        private bool? _MustSubscribe_Master;
        private bool? _MustSubscribe_Helper;
        private string _KanJiaTopicDescContent;
        private DateTime? _EndTime;
        private bool? _MustSubscribe_Agent;
        private bool? _isSaled;
        private Int32? _Sort;
        private Int32? _isdeleted;
        private Int32? _GoodID;
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
        ///砍价主题  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string Topic
        {
            set{ _Topic=value;}
            get{return _Topic;}
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
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string KanJiaRule
        {
            set{ _KanJiaRule=value;}
            get{return _KanJiaRule;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? StartPrice
        {
            set{ _StartPrice=value;}
            get{return _StartPrice;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? EndPrice
        {
            set{ _EndPrice=value;}
            get{return _EndPrice;}
        }
        /// <summary>
        ///砍价的 销售提成  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? AgentPrice
        {
            set{ _AgentPrice=value;}
            get{return _AgentPrice;}
        }
        /// <summary>
        ///每次砍价的最小值  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? EachAction_LowPrice
        {
            set{ _EachAction_LowPrice=value;}
            get{return _EachAction_LowPrice;}
        }
        /// <summary>
        ///每次砍价的最大值  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? EachAction_HighPrice
        {
            set{ _EachAction_HighPrice=value;}
            get{return _EachAction_HighPrice;}
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
        ///发起砍价是否必须输入收获地址  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustAddress_Master
        {
            set{ _MustAddress_Master=value;}
            get{return _MustAddress_Master;}
        }
        /// <summary>
        ///是否必须关注才能发起砍价  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustSubscribe_Master
        {
            set{ _MustSubscribe_Master=value;}
            get{return _MustSubscribe_Master;}
        }
        /// <summary>
        ///是否必须关注才能帮助砍价  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustSubscribe_Helper
        {
            set{ _MustSubscribe_Helper=value;}
            get{return _MustSubscribe_Helper;}
        }
        /// <summary>
        ///砍价描述  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string KanJiaTopicDescContent
        {
            set{ _KanJiaTopicDescContent=value;}
            get{return _KanJiaTopicDescContent;}
        }
        /// <summary>
        ///砍价终止时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? EndTime
        {
            set{ _EndTime=value;}
            get{return _EndTime;}
        }
        /// <summary>
        ///是否代理商才能发起砍价  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? MustSubscribe_Agent
        {
            set{ _MustSubscribe_Agent=value;}
            get{return _MustSubscribe_Agent;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? isSaled
        {
            set{ _isSaled=value;}
            get{return _isSaled;}
        }
        /// <summary>
        ///  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///  Int32 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? isdeleted
        {
            set{ _isdeleted=value;}
            get{return _isdeleted;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
        }
        /// <summary>
        /// 创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        #endregion Model
    }
}
