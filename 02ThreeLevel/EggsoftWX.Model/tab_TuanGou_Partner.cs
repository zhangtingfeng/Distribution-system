using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_TuanGou_Partner 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_TuanGou_Partner
    {
        public tab_TuanGou_Partner()
        {}
        #region Model
        //ID,TuanGouIDNumber,TuanGouID,OrderID,UserID,ShopClientID,BuyPrice,ContactMan,ContactPhone,GetGoodsAddress,ParterRole,IsDelete,CreateTime,UpdateTime,
        private Int32 _ID;
        private Int32? _TuanGouIDNumber;
        private Int32? _TuanGouID;
        private Int32? _OrderID;
        private Int32? _UserID;
        private Int32? _ShopClientID;
        private decimal? _BuyPrice;
        private string _ContactMan;
        private string _ContactPhone;
        private Int32? _GetGoodsAddress;
        private Int32? _ParterRole;
        private bool? _IsDelete;
        private DateTime? _CreateTime;
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
        ///团购编号  相同的编号 是一组活动。我的团长 我的团  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? TuanGouIDNumber
        {
            set{ _TuanGouIDNumber=value;}
            get{return _TuanGouIDNumber;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? TuanGouID
        {
            set{ _TuanGouID=value;}
            get{return _TuanGouID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? BuyPrice
        {
            set{ _BuyPrice=value;}
            get{return _BuyPrice;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactMan
        {
            set{ _ContactMan=value;}
            get{return _ContactMan;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactPhone
        {
            set{ _ContactPhone=value;}
            get{return _ContactPhone;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? GetGoodsAddress
        {
            set{ _GetGoodsAddress=value;}
            get{return _GetGoodsAddress;}
        }
        /// <summary>
        ///参与人角色  1 表示发起人  2  表示 参与人  smallint 长度5 占用字节数2 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ParterRole
        {
            set{ _ParterRole=value;}
            get{return _ParterRole;}
        }
        /// <summary>
        ///是否 删除  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IsDelete
        {
            set{ _IsDelete=value;}
            get{return _IsDelete;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? CreateTime
        {
            set{ _CreateTime=value;}
            get{return _CreateTime;}
        }
        /// <summary>
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        #endregion Model
    }
}
