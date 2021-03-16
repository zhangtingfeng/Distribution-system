using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient
    {
        public tab_ShopClient()
        {}
        #region Model
        //ID,ShopClientName,ShopClientType,ShopClientClass,ShopButton,ShopBackgroundLogoImage,ContactMan,ContactPhone,ShopMemo,Sort,UserClientLevel,Updatetime,UpdateMan,Username,PassWord,UserRealName,Country,Sheng,City,Area,PostCode,Email,Address,IsLocked,IFCompany,UpLoadPath,UserMemo,Sex,UserIntergentMark,AuthorTime,AuthorMoney,XML,ShenMaShopping,Shopping_Vouchers,Shopping_Vouchers_Goods_Percent,PartnerWeiXinID,RecommendWeiXinID,QM_QQ_COM_QM_K_32,Shopping_Vouchers_Money,ErJiYuMing,Username_Finance,PassWord_Finance,Style_Model,AutoMidifyAgentGoods,AgentMustRead,AgentMustReadAd,ContactManPostion,IsDeleted,PCYuMing,CreatTime,TPL_ID,
        private Int32 _ID;
        private string _ShopClientName;
        private string _ShopClientType;
        private string _ShopClientClass;
        private string _ShopButton;
        private string _ShopBackgroundLogoImage;
        private string _ContactMan;
        private string _ContactPhone;
        private string _ShopMemo;
        private int? _Sort;
        private int? _UserClientLevel;
        private DateTime? _Updatetime;
        private string _UpdateMan;
        private string _Username;
        private string _PassWord;
        private string _UserRealName;
        private string _Country;
        private string _Sheng;
        private string _City;
        private string _Area;
        private string _PostCode;
        private string _Email;
        private string _Address;
        private bool? _IsLocked;
        private bool? _IFCompany;
        private string _UpLoadPath;
        private string _UserMemo;
        private bool? _Sex;
        private string _UserIntergentMark;
        private DateTime? _AuthorTime;
        private decimal? _AuthorMoney;
        private string _XML;
        private bool? _ShenMaShopping;
        private bool? _Shopping_Vouchers;
        private int? _Shopping_Vouchers_Goods_Percent;
        private Int32? _PartnerWeiXinID;
        private Int32? _RecommendWeiXinID;
        private string _QM_QQ_COM_QM_K_32;
        private decimal? _Shopping_Vouchers_Money;
        private string _ErJiYuMing;
        private string _Username_Finance;
        private string _PassWord_Finance;
        private int? _Style_Model;
        private bool? _AutoMidifyAgentGoods;
        private string _AgentMustRead;
        private string _AgentMustReadAd;
        private string _ContactManPostion;
        private bool? _IsDeleted;
        private string _PCYuMing;
        private DateTime? _CreatTime;
        private string _TPL_ID;
        /// <summary>
        ///编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///商铺名称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopClientName
        {
            set{ _ShopClientName=value;}
            get{return _ShopClientName;}
        }
        /// <summary>
        ///商铺类型 个人或者公司  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopClientType
        {
            set{ _ShopClientType=value;}
            get{return _ShopClientType;}
        }
        /// <summary>
        ///商铺所在的分类  nvarchar 长度10 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopClientClass
        {
            set{ _ShopClientClass=value;}
            get{return _ShopClientClass;}
        }
        /// <summary>
        ///商铺的图标 路径  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopButton
        {
            set{ _ShopButton=value;}
            get{return _ShopButton;}
        }
        /// <summary>
        ///商铺的背景图  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopBackgroundLogoImage
        {
            set{ _ShopBackgroundLogoImage=value;}
            get{return _ShopBackgroundLogoImage;}
        }
        /// <summary>
        ///商铺的联系人  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactMan
        {
            set{ _ContactMan=value;}
            get{return _ContactMan;}
        }
        /// <summary>
        ///商铺的联系电话  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactPhone
        {
            set{ _ContactPhone=value;}
            get{return _ContactPhone;}
        }
        /// <summary>
        ///商铺备注  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopMemo
        {
            set{ _ShopMemo=value;}
            get{return _ShopMemo;}
        }
        /// <summary>
        ///排序  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///商铺的级别 三马  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? UserClientLevel
        {
            set{ _UserClientLevel=value;}
            get{return _UserClientLevel;}
        }
        /// <summary>
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? Updatetime
        {
            set{ _Updatetime=value;}
            get{return _Updatetime;}
        }
        /// <summary>
        ///更新人  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateMan
        {
            set{ _UpdateMan=value;}
            get{return _UpdateMan;}
        }
        /// <summary>
        ///用户名称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Username
        {
            set{ _Username=value;}
            get{return _Username;}
        }
        /// <summary>
        ///用户登录密码  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string PassWord
        {
            set{ _PassWord=value;}
            get{return _PassWord;}
        }
        /// <summary>
        ///用户真实名称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserRealName
        {
            set{ _UserRealName=value;}
            get{return _UserRealName;}
        }
        /// <summary>
        ///用户的国家  nvarchar 长度10 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string Country
        {
            set{ _Country=value;}
            get{return _Country;}
        }
        /// <summary>
        ///省  nvarchar 长度10 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string Sheng
        {
            set{ _Sheng=value;}
            get{return _Sheng;}
        }
        /// <summary>
        ///市  nvarchar 长度10 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string City
        {
            set{ _City=value;}
            get{return _City;}
        }
        /// <summary>
        ///区  nvarchar 长度10 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string Area
        {
            set{ _Area=value;}
            get{return _Area;}
        }
        /// <summary>
        ///邮编  nvarchar 长度6 占用字节数12 小数位数0 允许空 默认值无 
        /// </summary>
        public string PostCode
        {
            set{ _PostCode=value;}
            get{return _PostCode;}
        }
        /// <summary>
        ///邮件地址  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Email
        {
            set{ _Email=value;}
            get{return _Email;}
        }
        /// <summary>
        ///地址  nvarchar 长度80 占用字节数160 小数位数0 允许空 默认值无 
        /// </summary>
        public string Address
        {
            set{ _Address=value;}
            get{return _Address;}
        }
        /// <summary>
        ///是否锁定  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IsLocked
        {
            set{ _IsLocked=value;}
            get{return _IsLocked;}
        }
        /// <summary>
        ///是否公司类型  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IFCompany
        {
            set{ _IFCompany=value;}
            get{return _IFCompany;}
        }
        /// <summary>
        ///用户自己商品图片的上传路径  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpLoadPath
        {
            set{ _UpLoadPath=value;}
            get{return _UpLoadPath;}
        }
        /// <summary>
        ///用户备注  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserMemo
        {
            set{ _UserMemo=value;}
            get{return _UserMemo;}
        }
        /// <summary>
        ///性别  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Sex
        {
            set{ _Sex=value;}
            get{return _Sex;}
        }
        /// <summary>
        ///用户的唯一标志号  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserIntergentMark
        {
            set{ _UserIntergentMark=value;}
            get{return _UserIntergentMark;}
        }
        /// <summary>
        ///授权到期日期  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? AuthorTime
        {
            set{ _AuthorTime=value;}
            get{return _AuthorTime;}
        }
        /// <summary>
        ///有效短信数量  有效剩余资金  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AuthorMoney
        {
            set{ _AuthorMoney=value;}
            get{return _AuthorMoney;}
        }
        /// <summary>
        ///商户的扩展信息存储位置  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string XML
        {
            set{ _XML=value;}
            get{return _XML;}
        }
        /// <summary>
        ///是否是微百直营  这和购物券等有关系  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? ShenMaShopping
        {
            set{ _ShenMaShopping=value;}
            get{return _ShenMaShopping;}
        }
        /// <summary>
        ///是否启用购物券功能  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Shopping_Vouchers
        {
            set{ _Shopping_Vouchers=value;}
            get{return _Shopping_Vouchers;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? Shopping_Vouchers_Goods_Percent
        {
            set{ _Shopping_Vouchers_Goods_Percent=value;}
            get{return _Shopping_Vouchers_Goods_Percent;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? PartnerWeiXinID
        {
            set{ _PartnerWeiXinID=value;}
            get{return _PartnerWeiXinID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? RecommendWeiXinID
        {
            set{ _RecommendWeiXinID=value;}
            get{return _RecommendWeiXinID;}
        }
        /// <summary>
        ///  nvarchar 长度80 占用字节数160 小数位数0 允许空 默认值无 
        /// </summary>
        public string QM_QQ_COM_QM_K_32
        {
            set{ _QM_QQ_COM_QM_K_32=value;}
            get{return _QM_QQ_COM_QM_K_32;}
        }
        /// <summary>
        ///  money 长度19 占用字节数8 小数位数4 允许空 默认值((0)) 
        /// </summary>
        public decimal? Shopping_Vouchers_Money
        {
            set{ _Shopping_Vouchers_Money=value;}
            get{return _Shopping_Vouchers_Money;}
        }
        /// <summary>
        ///  nvarchar 长度80 占用字节数160 小数位数0 允许空 默认值无 
        /// </summary>
        public string ErJiYuMing
        {
            set{ _ErJiYuMing=value;}
            get{return _ErJiYuMing;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Username_Finance
        {
            set{ _Username_Finance=value;}
            get{return _Username_Finance;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string PassWord_Finance
        {
            set{ _PassWord_Finance=value;}
            get{return _PassWord_Finance;}
        }
        /// <summary>
        ///模版选择  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? Style_Model
        {
            set{ _Style_Model=value;}
            get{return _Style_Model;}
        }
        /// <summary>
        ///更新商品时自动更新代理商的经销商品范围，代理商不用重新挑选商品  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? AutoMidifyAgentGoods
        {
            set{ _AutoMidifyAgentGoods=value;}
            get{return _AutoMidifyAgentGoods;}
        }
        /// <summary>
        ///申请代理必须阅读 分销须知  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AgentMustRead
        {
            set{ _AgentMustRead=value;}
            get{return _AgentMustRead;}
        }
        /// <summary>
        ///代理须知  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string AgentMustReadAd
        {
            set{ _AgentMustReadAd=value;}
            get{return _AgentMustReadAd;}
        }
        /// <summary>
        ///  nvarchar 长度80 占用字节数160 小数位数0 允许空 默认值无 
        /// </summary>
        public string ContactManPostion
        {
            set{ _ContactManPostion=value;}
            get{return _ContactManPostion;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string PCYuMing
        {
            set{ _PCYuMing=value;}
            get{return _PCYuMing;}
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
        ///短信验证码模板ID  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string TPL_ID
        {
            set{ _TPL_ID=value;}
            get{return _TPL_ID;}
        }
        #endregion Model
    }
}
