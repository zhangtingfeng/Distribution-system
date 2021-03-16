using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b010_AskModifyParent 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b010_AskModifyParent
    {
        public b010_AskModifyParent()
        {}
        #region Model
        //ID,ShopClient_ID,OperationCenterID,OperationCenterUserID,BuyOrderShopUserID,BuyOrderUserRealName,BuyParentShopUserID,BuyGrandParentShopUserID,Usertel,UserEmail,UserExtraMemo,FeedbackStatus,FeedbackMemo,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private Int32? _OperationCenterID;
        private Int32? _OperationCenterUserID;
        private Int32? _BuyOrderShopUserID;
        private string _BuyOrderUserRealName;
        private Int32? _BuyParentShopUserID;
        private Int32? _BuyGrandParentShopUserID;
        private string _Usertel;
        private string _UserEmail;
        private string _UserExtraMemo;
        private Int32? _FeedbackStatus;
        private string _FeedbackMemo;
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
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OperationCenterID
        {
            set{ _OperationCenterID=value;}
            get{return _OperationCenterID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? OperationCenterUserID
        {
            set{ _OperationCenterUserID=value;}
            get{return _OperationCenterUserID;}
        }
        /// <summary>
        ///7天内下过单的用户ID(用户申请页面可以看到)  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? BuyOrderShopUserID
        {
            set{ _BuyOrderShopUserID=value;}
            get{return _BuyOrderShopUserID;}
        }
        /// <summary>
        ///请输入下单用户真实姓名，对账需要  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BuyOrderUserRealName
        {
            set{ _BuyOrderUserRealName=value;}
            get{return _BuyOrderUserRealName;}
        }
        /// <summary>
        ///请输入上级用户ID（直推），对账需要  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? BuyParentShopUserID
        {
            set{ _BuyParentShopUserID=value;}
            get{return _BuyParentShopUserID;}
        }
        /// <summary>
        ///请输入上上级用户ID（间推），对账需要  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? BuyGrandParentShopUserID
        {
            set{ _BuyGrandParentShopUserID=value;}
            get{return _BuyGrandParentShopUserID;}
        }
        /// <summary>
        ///请输入您的运营中心移动电话，以利于管理方核实信息  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Usertel
        {
            set{ _Usertel=value;}
            get{return _Usertel;}
        }
        /// <summary>
        ///请输入接受反馈的邮件地址  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserEmail
        {
            set{ _UserEmail=value;}
            get{return _UserEmail;}
        }
        /// <summary>
        ///请输入附加说明，以利于管理方更清楚的理解你的申请，提高申请成功率  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserExtraMemo
        {
            set{ _UserExtraMemo=value;}
            get{return _UserExtraMemo;}
        }
        /// <summary>
        ///反馈状态  0 表示未处理   1表示接受申请  2 表示 拒绝申请   tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? FeedbackStatus
        {
            set{ _FeedbackStatus=value;}
            get{return _FeedbackStatus;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string FeedbackMemo
        {
            set{ _FeedbackMemo=value;}
            get{return _FeedbackMemo;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
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
