using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b005_UserID_Operation_ID 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b005_UserID_Operation_ID
    {
        public b005_UserID_Operation_ID()
        {}
        #region Model
        //ID,UserID,ShopClientID,OperationCenterID,OperationCenterID_UserID,UserParentID,ActiveAccount,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _ShopClientID;
        private Int32? _OperationCenterID;
        private Int32? _OperationCenterID_UserID;
        private Int32? _UserParentID;
        private bool? _ActiveAccount;
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
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
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
        public Int32? OperationCenterID_UserID
        {
            set{ _OperationCenterID_UserID=value;}
            get{return _OperationCenterID_UserID;}
        }
        /// <summary>
        ///用户的上级 本运营中心的 本用户的上级  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserParentID
        {
            set{ _UserParentID=value;}
            get{return _UserParentID;}
        }
        /// <summary>
        ///是否激活分红  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ActiveAccount
        {
            set{ _ActiveAccount=value;}
            get{return _ActiveAccount;}
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
