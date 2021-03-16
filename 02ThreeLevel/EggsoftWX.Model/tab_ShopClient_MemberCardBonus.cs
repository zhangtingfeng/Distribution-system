using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_MemberCardBonus 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_MemberCardBonus
    {
        public tab_ShopClient_MemberCardBonus()
        {}
        #region Model
        //ID,ShopClientID,InputMoney,BonusMoney,BonusGouWuQuan,BonusDesc,IsDeleted,CreateTime,UpdateTime,UpdateBy,CreateBy,
        private Int32 _ID;
        private int? _ShopClientID;
        private decimal? _InputMoney;
        private decimal? _BonusMoney;
        private decimal? _BonusGouWuQuan;
        private string _BonusDesc;
        private int? _IsDeleted;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private string _CreateBy;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///充值金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? InputMoney
        {
            set{ _InputMoney=value;}
            get{return _InputMoney;}
        }
        /// <summary>
        ///奖励金额  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? BonusMoney
        {
            set{ _BonusMoney=value;}
            get{return _BonusMoney;}
        }
        /// <summary>
        ///赠送购物券  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? BonusGouWuQuan
        {
            set{ _BonusGouWuQuan=value;}
            get{return _BonusGouWuQuan;}
        }
        /// <summary>
        ///  nvarchar 长度300 占用字节数600 小数位数0 允许空 默认值无 
        /// </summary>
        public string BonusDesc
        {
            set{ _BonusDesc=value;}
            get{return _BonusDesc;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? IsDeleted
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
        /// <summary>
        ///修改人  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
        }
        /// <summary>
        ///添加人  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string CreateBy
        {
            set{ _CreateBy=value;}
            get{return _CreateBy;}
        }
        #endregion Model
    }
}
