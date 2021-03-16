using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_AdminUser 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_AdminUser
    {
        public tab_ShopClient_AdminUser()
        {}
        #region Model
        //ID,UserRealName,ShopClientAdmin,ShopClientAdminPassword,ShopClientAdminDesc,ShopClient_Role_PowerID,EnterpriseOrganizationID,ShopClientID,isDeleted,CreatTime,Updatetime,
        private Int32 _ID;
        private string _UserRealName;
        private string _ShopClientAdmin;
        private string _ShopClientAdminPassword;
        private string _ShopClientAdminDesc;
        private Int32? _ShopClient_Role_PowerID;
        private Int32? _EnterpriseOrganizationID;
        private Int32? _ShopClientID;
        private bool? _isDeleted;
        private DateTime? _CreatTime;
        private DateTime? _Updatetime;
        /// <summary>
        ///商铺的管理员 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///用户姓名 核对使用  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserRealName
        {
            set{ _UserRealName=value;}
            get{return _UserRealName;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopClientAdmin
        {
            set{ _ShopClientAdmin=value;}
            get{return _ShopClientAdmin;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopClientAdminPassword
        {
            set{ _ShopClientAdminPassword=value;}
            get{return _ShopClientAdminPassword;}
        }
        /// <summary>
        ///备注说明  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopClientAdminDesc
        {
            set{ _ShopClientAdminDesc=value;}
            get{return _ShopClientAdminDesc;}
        }
        /// <summary>
        ///相应角色  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClient_Role_PowerID
        {
            set{ _ShopClient_Role_PowerID=value;}
            get{return _ShopClient_Role_PowerID;}
        }
        /// <summary>
        ///企业组织机构代码  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? EnterpriseOrganizationID
        {
            set{ _EnterpriseOrganizationID=value;}
            get{return _EnterpriseOrganizationID;}
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
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? isDeleted
        {
            set{ _isDeleted=value;}
            get{return _isDeleted;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? Updatetime
        {
            set{ _Updatetime=value;}
            get{return _Updatetime;}
        }
        #endregion Model
    }
}
