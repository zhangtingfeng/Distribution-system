using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b013_WriteOrderByOperation 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b013_WriteOrderByOperation
    {
        public b013_WriteOrderByOperation()
        {}
        #region Model
        //ID,ShopClient_ID,OperationCenterID,OperationCenterUserID,BuyOrderShopUserID,BuyOrderShopUserIDRealName,BuyOrderShopUserIDIDCard,BuyOrderShopUserIDContactPhone,OrderPayTime,PaySerialNumber,BuyGoodID,BuyOrderCount,BuyParentShopUserID,OperationCenterTel,OperationCenterEmail,UserExtraMemo,FeedbackStatus,FeedbackMemo,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private Int32? _OperationCenterID;
        private Int32? _OperationCenterUserID;
        private Int32? _BuyOrderShopUserID;
        private string _BuyOrderShopUserIDRealName;
        private string _BuyOrderShopUserIDIDCard;
        private string _BuyOrderShopUserIDContactPhone;
        private DateTime? _OrderPayTime;
        private string _PaySerialNumber;
        private Int32? _BuyGoodID;
        private Int32? _BuyOrderCount;
        private Int32? _BuyParentShopUserID;
        private string _OperationCenterTel;
        private string _OperationCenterEmail;
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? BuyOrderShopUserID
        {
            set{ _BuyOrderShopUserID=value;}
            get{return _BuyOrderShopUserID;}
        }
        /// <summary>
        ///下单人真实姓名  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BuyOrderShopUserIDRealName
        {
            set{ _BuyOrderShopUserIDRealName=value;}
            get{return _BuyOrderShopUserIDRealName;}
        }
        /// <summary>
        ///下单人身份证号码  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BuyOrderShopUserIDIDCard
        {
            set{ _BuyOrderShopUserIDIDCard=value;}
            get{return _BuyOrderShopUserIDIDCard;}
        }
        /// <summary>
        ///下单人联系电话  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BuyOrderShopUserIDContactPhone
        {
            set{ _BuyOrderShopUserIDContactPhone=value;}
            get{return _BuyOrderShopUserIDContactPhone;}
        }
        /// <summary>
        ///用户下单时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? OrderPayTime
        {
            set{ _OrderPayTime=value;}
            get{return _OrderPayTime;}
        }
        /// <summary>
        ///支付流水号 乙方确认收款，返回支付流水号给甲方。  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string PaySerialNumber
        {
            set{ _PaySerialNumber=value;}
            get{return _PaySerialNumber;}
        }
        /// <summary>
        ///b004_OperationGoods  运营中心的商品 这个表的主键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? BuyGoodID
        {
            set{ _BuyGoodID=value;}
            get{return _BuyGoodID;}
        }
        /// <summary>
        ///购买商品数量  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? BuyOrderCount
        {
            set{ _BuyOrderCount=value;}
            get{return _BuyOrderCount;}
        }
        /// <summary>
        ///上级的 商铺ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? BuyParentShopUserID
        {
            set{ _BuyParentShopUserID=value;}
            get{return _BuyParentShopUserID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string OperationCenterTel
        {
            set{ _OperationCenterTel=value;}
            get{return _OperationCenterTel;}
        }
        /// <summary>
        ///运营中心邮件  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string OperationCenterEmail
        {
            set{ _OperationCenterEmail=value;}
            get{return _OperationCenterEmail;}
        }
        /// <summary>
        ///备注说明  运营中心下单时间填写  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserExtraMemo
        {
            set{ _UserExtraMemo=value;}
            get{return _UserExtraMemo;}
        }
        /// <summary>
        ///反馈状态  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? FeedbackStatus
        {
            set{ _FeedbackStatus=value;}
            get{return _FeedbackStatus;}
        }
        /// <summary>
        ///反馈备注  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
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
