using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_SignWorkingEveryDay 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_SignWorkingEveryDay
    {
        public tab_ShopClient_SignWorkingEveryDay()
        {}
        #region Model
        //ID,UserID,NickName,ShopUserID,SignTime,CreateTime,UpdateTime,IsDelete,ShopClientID,SendMoney,SendGouWuQuan,
        private Int32 _ID;
        private Int32? _UserID;
        private string _NickName;
        private Int32? _ShopUserID;
        private DateTime? _SignTime;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private Int32? _IsDelete;
        private Int32? _ShopClientID;
        private decimal? _SendMoney;
        private decimal? _SendGouWuQuan;
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string NickName
        {
            set{ _NickName=value;}
            get{return _NickName;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopUserID
        {
            set{ _ShopUserID=value;}
            get{return _ShopUserID;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? SignTime
        {
            set{ _SignTime=value;}
            get{return _SignTime;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? IsDelete
        {
            set{ _IsDelete=value;}
            get{return _IsDelete;}
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
        public decimal? SendMoney
        {
            set{ _SendMoney=value;}
            get{return _SendMoney;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? SendGouWuQuan
        {
            set{ _SendGouWuQuan=value;}
            get{return _SendGouWuQuan;}
        }
        #endregion Model
    }
}
