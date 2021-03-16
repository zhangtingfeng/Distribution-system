using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_WeiKanJia_Master 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_WeiKanJia_Master
    {
        public tab_WeiKanJia_Master()
        {}
        #region Model
        //ID,UpdateTime,UserID,ShopClientID,InsertTime,NowPrice,WeikanJiaID,CreatTime,MasterContactMan,MasteContactPhone,IsDeleted,IsBuyed,
        private Int32 _ID;
        private DateTime? _UpdateTime;
        private Int32? _UserID;
        private Int32? _ShopClientID;
        private DateTime? _InsertTime;
        private decimal? _NowPrice;
        private Int32? _WeikanJiaID;
        private DateTime? _CreatTime;
        private string _MasterContactMan;
        private string _MasteContactPhone;
        private bool? _IsDeleted;
        private bool? _IsBuyed;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? InsertTime
        {
            set{ _InsertTime=value;}
            get{return _InsertTime;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? NowPrice
        {
            set{ _NowPrice=value;}
            get{return _NowPrice;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? WeikanJiaID
        {
            set{ _WeikanJiaID=value;}
            get{return _WeikanJiaID;}
        }
        /// <summary>
        /// 创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string MasterContactMan
        {
            set{ _MasterContactMan=value;}
            get{return _MasterContactMan;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string MasteContactPhone
        {
            set{ _MasteContactPhone=value;}
            get{return _MasteContactPhone;}
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
        ///已经发起的砍价  不能多次购买  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsBuyed
        {
            set{ _IsBuyed=value;}
            get{return _IsBuyed;}
        }
        #endregion Model
    }
}
