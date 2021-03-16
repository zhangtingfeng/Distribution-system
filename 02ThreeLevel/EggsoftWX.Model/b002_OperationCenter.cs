using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b002_OperationCenter 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b002_OperationCenter
    {
        public b002_OperationCenter()
        {}
        #region Model
        //ID,ShopCenterID,ParentID,UserID,ShopClient_ID,MasterName,MasterPhone,MasterAddress,BankAccountUserName,BankAccountName,BankAccountNumber,RunningState,AccountState,ShareholderState,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopCenterID;
        private Int32? _ParentID;
        private Int32? _UserID;
        private Int32? _ShopClient_ID;
        private string _MasterName;
        private string _MasterPhone;
        private string _MasterAddress;
        private string _BankAccountUserName;
        private string _BankAccountName;
        private string _BankAccountNumber;
        private bool? _RunningState;
        private bool? _AccountState;
        private bool? _ShareholderState;
        private string _CreateBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private DateTime? _CreatTime;
        private Int32? _IsDeleted;
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
        public Int32? ShopCenterID
        {
            set{ _ShopCenterID=value;}
            get{return _ShopCenterID;}
        }
        /// <summary>
        ///运营中心上级ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
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
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        ///注明公司或个人名称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string MasterName
        {
            set{ _MasterName=value;}
            get{return _MasterName;}
        }
        /// <summary>
        ///注明公司或个人名称联系人手机号  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string MasterPhone
        {
            set{ _MasterPhone=value;}
            get{return _MasterPhone;}
        }
        /// <summary>
        ///注明公司或个人名称联系人地址  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string MasterAddress
        {
            set{ _MasterAddress=value;}
            get{return _MasterAddress;}
        }
        /// <summary>
        ///银行账户姓名  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BankAccountUserName
        {
            set{ _BankAccountUserName=value;}
            get{return _BankAccountUserName;}
        }
        /// <summary>
        ///开户行  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BankAccountName
        {
            set{ _BankAccountName=value;}
            get{return _BankAccountName;}
        }
        /// <summary>
        ///银行账号  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BankAccountNumber
        {
            set{ _BankAccountNumber=value;}
            get{return _BankAccountNumber;}
        }
        /// <summary>
        ///账户运营状态。不选表示取消运营资格，但是不影响提现申请  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? RunningState
        {
            set{ _RunningState=value;}
            get{return _RunningState;}
        }
        /// <summary>
        ///银行账户状态。不选表示冻结账户  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? AccountState
        {
            set{ _AccountState=value;}
            get{return _AccountState;}
        }
        /// <summary>
        ///是否股东账户  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ShareholderState
        {
            set{ _ShareholderState=value;}
            get{return _ShareholderState;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string CreateBy
        {
            set{ _CreateBy=value;}
            get{return _CreateBy;}
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
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
        }
        /// <summary>
        ///同一手机号 最多2分钟 创建一行数据。即使验证码正确。  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        /// <summary>
        ///  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
