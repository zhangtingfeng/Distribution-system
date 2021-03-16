using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ZC_01Product_Support_GetBonus 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ZC_01Product_Support_GetBonus
    {
        public tab_ZC_01Product_Support_GetBonus()
        {}
        #region Model
        //ID,ZC_01ProductID,GoodID,SupportID,BonusContent,ShopClientID,Sort,IsDeleted,CreateTime,UpdateTime,
        private Int32 _ID;
        private Int32? _ZC_01ProductID;
        private Int32? _GoodID;
        private Int32? _SupportID;
        private string _BonusContent;
        private Int32? _ShopClientID;
        private Int32? _Sort;
        private bool? _IsDeleted;
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ZC_01ProductID
        {
            set{ _ZC_01ProductID=value;}
            get{return _ZC_01ProductID;}
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SupportID
        {
            set{ _SupportID=value;}
            get{return _SupportID;}
        }
        /// <summary>
        ///开奖详情.  HTML字段  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string BonusContent
        {
            set{ _BonusContent=value;}
            get{return _BonusContent;}
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreateTime
        {
            set{ _CreateTime=value;}
            get{return _CreateTime;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        #endregion Model
    }
}
