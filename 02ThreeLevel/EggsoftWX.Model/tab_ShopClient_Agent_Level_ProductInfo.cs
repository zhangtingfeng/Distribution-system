using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Agent_Level_ProductInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Agent_Level_ProductInfo
    {
        public tab_ShopClient_Agent_Level_ProductInfo()
        {}
        #region Model
        //ID,ShopClientID,ShopClient_Agent_Level_ID,ProductPrice,AgentPercent,MaxGouWuQuan,MaxWealth,ProductID,b019_tab_ShopClient_MultiFenXiaoLevelID,CreatTime,Creatby,UpdateTime,Updateby,Isdeleted,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private Int32? _ShopClient_Agent_Level_ID;
        private decimal? _ProductPrice;
        private decimal? _AgentPercent;
        private decimal? _MaxGouWuQuan;
        private decimal? _MaxWealth;
        private Int32? _ProductID;
        private Int32? _b019_tab_ShopClient_MultiFenXiaoLevelID;
        private DateTime? _CreatTime;
        private string _Creatby;
        private DateTime? _UpdateTime;
        private string _Updateby;
        private Int32? _Isdeleted;
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClient_Agent_Level_ID
        {
            set{ _ShopClient_Agent_Level_ID=value;}
            get{return _ShopClient_Agent_Level_ID;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ProductPrice
        {
            set{ _ProductPrice=value;}
            get{return _ProductPrice;}
        }
        /// <summary>
        ///分销推广利润 。代理商的 取这个价格给大家分钱  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AgentPercent
        {
            set{ _AgentPercent=value;}
            get{return _AgentPercent;}
        }
        /// <summary>
        ///本代理商的最大购物券允许金额  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? MaxGouWuQuan
        {
            set{ _MaxGouWuQuan=value;}
            get{return _MaxGouWuQuan;}
        }
        /// <summary>
        ///本代理商的最大财富积分允许金额  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? MaxWealth
        {
            set{ _MaxWealth=value;}
            get{return _MaxWealth;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ProductID
        {
            set{ _ProductID=value;}
            get{return _ProductID;}
        }
        /// <summary>
        ///分销方案选择 ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? b019_tab_ShopClient_MultiFenXiaoLevelID
        {
            set{ _b019_tab_ShopClient_MultiFenXiaoLevelID=value;}
            get{return _b019_tab_ShopClient_MultiFenXiaoLevelID;}
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
        public string Updateby
        {
            set{ _Updateby=value;}
            get{return _Updateby;}
        }
        /// <summary>
        ///  smallint 长度5 占用字节数2 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Isdeleted
        {
            set{ _Isdeleted=value;}
            get{return _Isdeleted;}
        }
        #endregion Model
    }
}
