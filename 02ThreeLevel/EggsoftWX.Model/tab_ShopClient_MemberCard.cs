using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_MemberCard 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_MemberCard
    {
        public tab_ShopClient_MemberCard()
        {}
        #region Model
        //ID,ShopClientID,InputMoney,BonusMoney,BonusGouWuQuan,PhoneNum,IfChangToWeiXinBonus,BonusDesc,BankSeraillnum,IsDeleted,CreateBy,CreateTime,UpdateBy,UpdateTime,
        private Int32 _ID;
        private int? _ShopClientID;
        private decimal? _InputMoney;
        private decimal? _BonusMoney;
        private decimal? _BonusGouWuQuan;
        private string _PhoneNum;
        private bool? _IfChangToWeiXinBonus;
        private string _BonusDesc;
        private string _BankSeraillnum;
        private int? _IsDeleted;
        private string _CreateBy;
        private DateTime? _CreateTime;
        private string _UpdateBy;
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
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? InputMoney
        {
            set{ _InputMoney=value;}
            get{return _InputMoney;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? BonusMoney
        {
            set{ _BonusMoney=value;}
            get{return _BonusMoney;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? BonusGouWuQuan
        {
            set{ _BonusGouWuQuan=value;}
            get{return _BonusGouWuQuan;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string PhoneNum
        {
            set{ _PhoneNum=value;}
            get{return _PhoneNum;}
        }
        /// <summary>
        ///是否已经成功转化到微信账户，如果没有，用户可以自己微信捆绑一下  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IfChangToWeiXinBonus
        {
            set{ _IfChangToWeiXinBonus=value;}
            get{return _IfChangToWeiXinBonus;}
        }
        /// <summary>
        ///  nvarchar 长度350 占用字节数700 小数位数0 允许空 默认值无 
        /// </summary>
        public string BonusDesc
        {
            set{ _BonusDesc=value;}
            get{return _BonusDesc;}
        }
        /// <summary>
        ///银行流水号。现金需注明相关信息  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BankSeraillnum
        {
            set{ _BankSeraillnum=value;}
            get{return _BankSeraillnum;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///创建人  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string CreateBy
        {
            set{ _CreateBy=value;}
            get{return _CreateBy;}
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
        ///更新人  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
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
