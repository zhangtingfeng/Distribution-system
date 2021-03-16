using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User
    {
        public tab_User()
        {}
        #region Model
        //ID,ContactMan,ContactPhone,UserRealName,Country,Sheng,City,Area,PostCode,Sex,Email,Address,IDCard,Default_Address,OpenID,unionID,SmallProgramOpenID,HeadImageUrl,Api_Authorize,Subscribe,IFShowCityHelp,RemainingSum,IFSendWeiBaiQuan,IFSendWeiBaiQuan_LiuZong,SocialPlatform,ShopClientID,AlipayNumOrWeiXinPay,ShopUserID,ParentID,TeamID,HowToGetProduct,DefaultO2OShop,multi_DuoKeFu_Lastupdatetime,Password,UserAccount,InsertTime,SafeCode,CreatTime,Updatetime,CreateBy,UpdateBy,NickName,Isdeleted,
        private Int32 _ID;
        private string _ContactMan;
        private string _ContactPhone;
        private string _UserRealName;
        private string _Country;
        private string _Sheng;
        private string _City;
        private string _Area;
        private string _PostCode;
        private bool? _Sex;
        private string _Email;
        private string _Address;
        private string _IDCard;
        private Int32? _Default_Address;
        private string _OpenID;
        private string _unionID;
        private string _SmallProgramOpenID;
        private string _HeadImageUrl;
        private bool? _Api_Authorize;
        private bool? _Subscribe;
        private bool? _IFShowCityHelp;
        private decimal? _RemainingSum;
        private bool? _IFSendWeiBaiQuan;
        private bool? _IFSendWeiBaiQuan_LiuZong;
        private string _SocialPlatform;
        private Int32? _ShopClientID;
        private string _AlipayNumOrWeiXinPay;
        private Int32? _ShopUserID;
        private Int32? _ParentID;
        private Int32? _TeamID;
        private int? _HowToGetProduct;
        private int? _DefaultO2OShop;
        private DateTime? _multi_DuoKeFu_Lastupdatetime;
        private string _Password;
        private string _UserAccount;
        private DateTime? _InsertTime;
        private string _SafeCode;
        private DateTime? _CreatTime;
        private DateTime? _Updatetime;
        private string _CreateBy;
        private string _UpdateBy;
        private string _NickName;
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactMan
        {
            set{ _ContactMan=value;}
            get{return _ContactMan;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactPhone
        {
            set{ _ContactPhone=value;}
            get{return _ContactPhone;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserRealName
        {
            set{ _UserRealName=value;}
            get{return _UserRealName;}
        }
        /// <summary>
        ///  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string Country
        {
            set{ _Country=value;}
            get{return _Country;}
        }
        /// <summary>
        ///  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string Sheng
        {
            set{ _Sheng=value;}
            get{return _Sheng;}
        }
        /// <summary>
        ///  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string City
        {
            set{ _City=value;}
            get{return _City;}
        }
        /// <summary>
        ///  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string Area
        {
            set{ _Area=value;}
            get{return _Area;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string PostCode
        {
            set{ _PostCode=value;}
            get{return _PostCode;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Sex
        {
            set{ _Sex=value;}
            get{return _Sex;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Email
        {
            set{ _Email=value;}
            get{return _Email;}
        }
        /// <summary>
        ///  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string Address
        {
            set{ _Address=value;}
            get{return _Address;}
        }
        /// <summary>
        ///身份证号码  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string IDCard
        {
            set{ _IDCard=value;}
            get{return _IDCard;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Default_Address
        {
            set{ _Default_Address=value;}
            get{return _Default_Address;}
        }
        /// <summary>
        ///  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string OpenID
        {
            set{ _OpenID=value;}
            get{return _OpenID;}
        }
        /// <summary>
        ///在返回值里就包含有用户的unionID。这里不再详述。  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string unionID
        {
            set{ _unionID=value;}
            get{return _unionID;}
        }
        /// <summary>
        ///小程序所获得的微信OpenID  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string SmallProgramOpenID
        {
            set{ _SmallProgramOpenID=value;}
            get{return _SmallProgramOpenID;}
        }
        /// <summary>
        ///  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string HeadImageUrl
        {
            set{ _HeadImageUrl=value;}
            get{return _HeadImageUrl;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Api_Authorize
        {
            set{ _Api_Authorize=value;}
            get{return _Api_Authorize;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Subscribe
        {
            set{ _Subscribe=value;}
            get{return _Subscribe;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IFShowCityHelp
        {
            set{ _IFShowCityHelp=value;}
            get{return _IFShowCityHelp;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? RemainingSum
        {
            set{ _RemainingSum=value;}
            get{return _RemainingSum;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IFSendWeiBaiQuan
        {
            set{ _IFSendWeiBaiQuan=value;}
            get{return _IFSendWeiBaiQuan;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IFSendWeiBaiQuan_LiuZong
        {
            set{ _IFSendWeiBaiQuan_LiuZong=value;}
            get{return _IFSendWeiBaiQuan_LiuZong;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string SocialPlatform
        {
            set{ _SocialPlatform=value;}
            get{return _SocialPlatform;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string AlipayNumOrWeiXinPay
        {
            set{ _AlipayNumOrWeiXinPay=value;}
            get{return _AlipayNumOrWeiXinPay;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? ShopUserID
        {
            set{ _ShopUserID=value;}
            get{return _ShopUserID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        /// <summary>
        ///所在团队的ID TeamID就是tab_ShopClient_Agent_ 表的ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? TeamID
        {
            set{ _TeamID=value;}
            get{return _TeamID;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? HowToGetProduct
        {
            set{ _HowToGetProduct=value;}
            get{return _HowToGetProduct;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? DefaultO2OShop
        {
            set{ _DefaultO2OShop=value;}
            get{return _DefaultO2OShop;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? multi_DuoKeFu_Lastupdatetime
        {
            set{ _multi_DuoKeFu_Lastupdatetime=value;}
            get{return _multi_DuoKeFu_Lastupdatetime;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Password
        {
            set{ _Password=value;}
            get{return _Password;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserAccount
        {
            set{ _UserAccount=value;}
            get{return _UserAccount;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值(replace(newid(),'-','')) 
        /// </summary>
        public string SafeCode
        {
            set{ _SafeCode=value;}
            get{return _SafeCode;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? Updatetime
        {
            set{ _Updatetime=value;}
            get{return _Updatetime;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
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
        ///  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Isdeleted
        {
            set{ _Isdeleted=value;}
            get{return _Isdeleted;}
        }
        #endregion Model
    }
}
