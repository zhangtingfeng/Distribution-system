using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_OnlineRegistration 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_OnlineRegistration
    {
        public tab_ShopClient_OnlineRegistration()
        {}
        #region Model
        //ID,ShopClient_ID,UserID,Name,Sex,Email,birthDate,Phone,LocalCall,Address,PeopleNum,OnlineID,Valid,AddExp1,AddExp2,AddExp3,AddExp4,AddExp5,AddExp6,AddExp7,AddExp8,AddExp9,AddExp10,AddExp11,AddExp12,AddExp13,AddExp14,AddExp15,AddExp16,AddExp17,AddExp18,AddExp19,AddExp20,CreateTime,Createby,UpdateTime,Updateby,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private Int32? _UserID;
        private string _Name;
        private bool? _Sex;
        private string _Email;
        private DateTime? _birthDate;
        private string _Phone;
        private string _LocalCall;
        private string _Address;
        private int? _PeopleNum;
        private Int32? _OnlineID;
        private bool? _Valid;
        private string _AddExp1;
        private string _AddExp2;
        private string _AddExp3;
        private string _AddExp4;
        private string _AddExp5;
        private string _AddExp6;
        private string _AddExp7;
        private string _AddExp8;
        private string _AddExp9;
        private string _AddExp10;
        private string _AddExp11;
        private string _AddExp12;
        private string _AddExp13;
        private string _AddExp14;
        private string _AddExp15;
        private string _AddExp16;
        private string _AddExp17;
        private string _AddExp18;
        private string _AddExp19;
        private string _AddExp20;
        private DateTime? _CreateTime;
        private string _Createby;
        private DateTime? _UpdateTime;
        private string _Updateby;
        private int? _IsDeleted;
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
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
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
        ///  varchar 长度20 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
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
        ///  varchar 长度50 占用字节数50 小数位数0 允许空 默认值无 
        /// </summary>
        public string Email
        {
            set{ _Email=value;}
            get{return _Email;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? birthDate
        {
            set{ _birthDate=value;}
            get{return _birthDate;}
        }
        /// <summary>
        ///  varchar 长度20 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string Phone
        {
            set{ _Phone=value;}
            get{return _Phone;}
        }
        /// <summary>
        ///  varchar 长度20 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string LocalCall
        {
            set{ _LocalCall=value;}
            get{return _LocalCall;}
        }
        /// <summary>
        ///  varchar 长度200 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string Address
        {
            set{ _Address=value;}
            get{return _Address;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? PeopleNum
        {
            set{ _PeopleNum=value;}
            get{return _PeopleNum;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OnlineID
        {
            set{ _OnlineID=value;}
            get{return _OnlineID;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Valid
        {
            set{ _Valid=value;}
            get{return _Valid;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp1
        {
            set{ _AddExp1=value;}
            get{return _AddExp1;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp2
        {
            set{ _AddExp2=value;}
            get{return _AddExp2;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp3
        {
            set{ _AddExp3=value;}
            get{return _AddExp3;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp4
        {
            set{ _AddExp4=value;}
            get{return _AddExp4;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp5
        {
            set{ _AddExp5=value;}
            get{return _AddExp5;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp6
        {
            set{ _AddExp6=value;}
            get{return _AddExp6;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp7
        {
            set{ _AddExp7=value;}
            get{return _AddExp7;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp8
        {
            set{ _AddExp8=value;}
            get{return _AddExp8;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp9
        {
            set{ _AddExp9=value;}
            get{return _AddExp9;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp10
        {
            set{ _AddExp10=value;}
            get{return _AddExp10;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp11
        {
            set{ _AddExp11=value;}
            get{return _AddExp11;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp12
        {
            set{ _AddExp12=value;}
            get{return _AddExp12;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp13
        {
            set{ _AddExp13=value;}
            get{return _AddExp13;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp14
        {
            set{ _AddExp14=value;}
            get{return _AddExp14;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp15
        {
            set{ _AddExp15=value;}
            get{return _AddExp15;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp16
        {
            set{ _AddExp16=value;}
            get{return _AddExp16;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp17
        {
            set{ _AddExp17=value;}
            get{return _AddExp17;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp18
        {
            set{ _AddExp18=value;}
            get{return _AddExp18;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp19
        {
            set{ _AddExp19=value;}
            get{return _AddExp19;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp20
        {
            set{ _AddExp20=value;}
            get{return _AddExp20;}
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
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
