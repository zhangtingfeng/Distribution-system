using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ZC_01Product 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ZC_01Product
    {
        public tab_ZC_01Product()
        {}
        #region Model
        //ID,DestinationPrice,WhenEndAllGroup,ZCDescribe,ZCReason,ZCPromiseAndReturn,SourceGoodID,ShopClientID,IsSales,Sort,IsDeleted,CreateTime,UpdateTime,
        private Int32 _ID;
        private decimal? _DestinationPrice;
        private DateTime? _WhenEndAllGroup;
        private string _ZCDescribe;
        private string _ZCReason;
        private string _ZCPromiseAndReturn;
        private Int32? _SourceGoodID;
        private Int32? _ShopClientID;
        private bool? _IsSales;
        private Int32? _Sort;
        private bool? _IsDeleted;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        /// <summary>
        ///序号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///目标金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? DestinationPrice
        {
            set{ _DestinationPrice=value;}
            get{return _DestinationPrice;}
        }
        /// <summary>
        ///最终截至时间  页面上显示最终天数  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? WhenEndAllGroup
        {
            set{ _WhenEndAllGroup=value;}
            get{return _WhenEndAllGroup;}
        }
        /// <summary>
        ///众筹描述  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string ZCDescribe
        {
            set{ _ZCDescribe=value;}
            get{return _ZCDescribe;}
        }
        /// <summary>
        ///众筹原因  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string ZCReason
        {
            set{ _ZCReason=value;}
            get{return _ZCReason;}
        }
        /// <summary>
        ///承诺与回报  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string ZCPromiseAndReturn
        {
            set{ _ZCPromiseAndReturn=value;}
            get{return _ZCPromiseAndReturn;}
        }
        /// <summary>
        ///原商品详情  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SourceGoodID
        {
            set{ _SourceGoodID=value;}
            get{return _SourceGoodID;}
        }
        /// <summary>
        ///店铺ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///是否上架  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? IsSales
        {
            set{ _IsSales=value;}
            get{return _IsSales;}
        }
        /// <summary>
        ///排序位置  越大 越在后面  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///是否 删除  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
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
