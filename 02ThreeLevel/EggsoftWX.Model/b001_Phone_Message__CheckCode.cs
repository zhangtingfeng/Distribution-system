using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b001_Phone_Message__CheckCode 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b001_Phone_Message__CheckCode
    {
        public b001_Phone_Message__CheckCode()
        {}
        #region Model
        //ID,SendPhoneNum,SendTime,innerIP,IP,IPDetailDesc,CheckCode,CheckTime,CheckStatus,MessageContent,SendStatus,MessageCheckStatus,SendType,ShopClientID,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,consumeMoney,AuthorMoney,
        private Int32 _ID;
        private string _SendPhoneNum;
        private DateTime? _SendTime;
        private string _innerIP;
        private string _IP;
        private string _IPDetailDesc;
        private string _CheckCode;
        private DateTime? _CheckTime;
        private bool? _CheckStatus;
        private string _MessageContent;
        private Int32? _SendStatus;
        private int? _MessageCheckStatus;
        private int? _SendType;
        private int? _ShopClientID;
        private string _CreateBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private DateTime? _CreatTime;
        private Int32? _IsDeleted;
        private decimal? _consumeMoney;
        private decimal? _AuthorMoney;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///发送对象的手机号码  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string SendPhoneNum
        {
            set{ _SendPhoneNum=value;}
            get{return _SendPhoneNum;}
        }
        /// <summary>
        ///短信验证码发送时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? SendTime
        {
            set{ _SendTime=value;}
            get{return _SendTime;}
        }
        /// <summary>
        ///用户内部IP  局域网IP  jS所产生的  不可靠 仅分析数据 使用  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string innerIP
        {
            set{ _innerIP=value;}
            get{return _innerIP;}
        }
        /// <summary>
        ///客户端请求IP 公网 IP地址  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string IP
        {
            set{ _IP=value;}
            get{return _IP;}
        }
        /// <summary>
        ///IP地址描述  nvarchar 长度500 占用字节数1000 小数位数0 允许空 默认值无 
        /// </summary>
        public string IPDetailDesc
        {
            set{ _IPDetailDesc=value;}
            get{return _IPDetailDesc;}
        }
        /// <summary>
        ///  nvarchar 长度10 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string CheckCode
        {
            set{ _CheckCode=value;}
            get{return _CheckCode;}
        }
        /// <summary>
        ///短信验证码 验证时间  最终时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? CheckTime
        {
            set{ _CheckTime=value;}
            get{return _CheckTime;}
        }
        /// <summary>
        ///本短信验证码  是否正确  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? CheckStatus
        {
            set{ _CheckStatus=value;}
            get{return _CheckStatus;}
        }
        /// <summary>
        ///所发送的短消息内容  nvarchar 长度500 占用字节数1000 小数位数0 允许空 默认值无 
        /// </summary>
        public string MessageContent
        {
            set{ _MessageContent=value;}
            get{return _MessageContent;}
        }
        /// <summary>
        ///发送状态  0  表示 发送失败  1 表示发送成功  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? SendStatus
        {
            set{ _SendStatus=value;}
            get{return _SendStatus;}
        }
        /// <summary>
        ///短信 验证码使用状态  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? MessageCheckStatus
        {
            set{ _MessageCheckStatus=value;}
            get{return _MessageCheckStatus;}
        }
        /// <summary>
        ///短信消息类型 。  1 表示 验证码类  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? SendType
        {
            set{ _SendType=value;}
            get{return _SendType;}
        }
        /// <summary>
        ///商户代码  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
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
        /// <summary>
        ///消费现金  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? consumeMoney
        {
            set{ _consumeMoney=value;}
            get{return _consumeMoney;}
        }
        /// <summary>
        ///剩余现金  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AuthorMoney
        {
            set{ _AuthorMoney=value;}
            get{return _AuthorMoney;}
        }
        #endregion Model
    }
}
