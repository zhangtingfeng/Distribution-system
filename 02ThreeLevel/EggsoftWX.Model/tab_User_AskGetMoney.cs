using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User_AskGetMoney 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User_AskGetMoney
    {
        public tab_User_AskGetMoney()
        {}
        #region Model
        //ID,userRealName,payment_no,CardName,AskMoney,AskMemo,UserID,IFSendMoney,ResultCode,CreatTime,UpdateTime,Isdeleted,
        private Int32 _ID;
        private string _userRealName;
        private string _payment_no;
        private string _CardName;
        private decimal? _AskMoney;
        private string _AskMemo;
        private Int32? _UserID;
        private bool? _IFSendMoney;
        private string _ResultCode;
        private DateTime? _CreatTime;
        private DateTime? _UpdateTime;
        private Int32? _Isdeleted;
        /// <summary>
        ///编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string userRealName
        {
            set{ _userRealName=value;}
            get{return _userRealName;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string payment_no
        {
            set{ _payment_no=value;}
            get{return _payment_no;}
        }
        /// <summary>
        ///那一张卡  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string CardName
        {
            set{ _CardName=value;}
            get{return _CardName;}
        }
        /// <summary>
        ///请求数额  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AskMoney
        {
            set{ _AskMoney=value;}
            get{return _AskMoney;}
        }
        /// <summary>
        ///备注  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AskMemo
        {
            set{ _AskMemo=value;}
            get{return _AskMemo;}
        }
        /// <summary>
        ///user 的外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///是否已经得到该笔申请的款项  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IFSendMoney
        {
            set{ _IFSendMoney=value;}
            get{return _IFSendMoney;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string ResultCode
        {
            set{ _ResultCode=value;}
            get{return _ResultCode;}
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
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///是否删除  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Isdeleted
        {
            set{ _Isdeleted=value;}
            get{return _Isdeleted;}
        }
        #endregion Model
    }
}
