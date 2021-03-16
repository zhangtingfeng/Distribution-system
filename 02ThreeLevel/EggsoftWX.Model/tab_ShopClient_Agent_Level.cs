using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Agent_Level 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Agent_Level
    {
        public tab_ShopClient_Agent_Level()
        {}
        #region Model
        //ID,ShopClientID,AgentLevelName,Sort,AgentlevelMemo,GouWuQuanGoodPrice,OperationGetChild,OperationGetGrandChild,OperationGetGreatChild,CreatTime,CreatBy,UpdateTime,UpdateBy,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private string _AgentLevelName;
        private Int32? _Sort;
        private string _AgentlevelMemo;
        private decimal? _GouWuQuanGoodPrice;
        private bool? _OperationGetChild;
        private bool? _OperationGetGrandChild;
        private bool? _OperationGetGreatChild;
        private DateTime? _CreatTime;
        private string _CreatBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private int? _IsDeleted;
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
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///  nvarchar 长度30 占用字节数60 小数位数0 允许空 默认值无 
        /// </summary>
        public string AgentLevelName
        {
            set{ _AgentLevelName=value;}
            get{return _AgentLevelName;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AgentlevelMemo
        {
            set{ _AgentlevelMemo=value;}
            get{return _AgentlevelMemo;}
        }
        /// <summary>
        ///购物券等值价格  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? GouWuQuanGoodPrice
        {
            set{ _GouWuQuanGoodPrice=value;}
            get{return _GouWuQuanGoodPrice;}
        }
        /// <summary>
        ///能否取得下级团队分佣  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? OperationGetChild
        {
            set{ _OperationGetChild=value;}
            get{return _OperationGetChild;}
        }
        /// <summary>
        ///能否取得下下级团队分佣  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? OperationGetGrandChild
        {
            set{ _OperationGetGrandChild=value;}
            get{return _OperationGetGrandChild;}
        }
        /// <summary>
        ///能否取得下下下级团队分佣  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? OperationGetGreatChild
        {
            set{ _OperationGetGreatChild=value;}
            get{return _OperationGetGreatChild;}
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
        public string CreatBy
        {
            set{ _CreatBy=value;}
            get{return _CreatBy;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
