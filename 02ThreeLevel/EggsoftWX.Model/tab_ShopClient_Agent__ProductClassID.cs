using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Agent__ProductClassID 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Agent__ProductClassID
    {
        public tab_ShopClient_Agent__ProductClassID()
        {}
        #region Model
        //ID,ShopClientID,UserID,ProductID,OnlyIsAngel,Empowered,StockNum_MeHavebuyNum,ProductRightNum,Full_Vouchers_,ProductPrice,CreatTime,Createby,UpdateTime,Updateby,Isdeleted,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private Int32? _UserID;
        private Int32? _ProductID;
        private bool? _OnlyIsAngel;
        private bool? _Empowered;
        private Int32? _StockNum_MeHavebuyNum;
        private Int32? _ProductRightNum;
        private bool? _Full_Vouchers_;
        private decimal? _ProductPrice;
        private DateTime? _CreatTime;
        private string _Createby;
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
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
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
        ///和相关参数中是否选中天使功能有关。如果是0表示申请了代理 默认值是1 天使分销功能，对标微信小店功能，任何访问都自动给予代理权，不过只有提出代理申请的用户才能参与分销提成、团队奖励  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? OnlyIsAngel
        {
            set{ _OnlyIsAngel=value;}
            get{return _OnlyIsAngel;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Empowered
        {
            set{ _Empowered=value;}
            get{return _Empowered;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? StockNum_MeHavebuyNum
        {
            set{ _StockNum_MeHavebuyNum=value;}
            get{return _StockNum_MeHavebuyNum;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ProductRightNum
        {
            set{ _ProductRightNum=value;}
            get{return _ProductRightNum;}
        }
        /// <summary>
        ///购物券最大金额  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Full_Vouchers_
        {
            set{ _Full_Vouchers_=value;}
            get{return _Full_Vouchers_;}
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
        public string Createby
        {
            set{ _Createby=value;}
            get{return _Createby;}
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
