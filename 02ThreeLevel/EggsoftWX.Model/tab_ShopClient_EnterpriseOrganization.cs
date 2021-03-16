using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_EnterpriseOrganization 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_EnterpriseOrganization
    {
        public tab_ShopClient_EnterpriseOrganization()
        {}
        #region Model
        //ID,OrganizationName,OrganizationType,OrganizationContent,UpdateTime,ParentID,Pos,ShopClientID,CreateTime,isDeleted,
        private Int32 _ID;
        private string _OrganizationName;
        private int? _OrganizationType;
        private string _OrganizationContent;
        private DateTime? _UpdateTime;
        private int? _ParentID;
        private int? _Pos;
        private Int32? _ShopClientID;
        private DateTime? _CreateTime;
        private bool? _isDeleted;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string OrganizationName
        {
            set{ _OrganizationName=value;}
            get{return _OrganizationName;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? OrganizationType
        {
            set{ _OrganizationType=value;}
            get{return _OrganizationType;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string OrganizationContent
        {
            set{ _OrganizationContent=value;}
            get{return _OrganizationContent;}
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
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Pos
        {
            set{ _Pos=value;}
            get{return _Pos;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreateTime
        {
            set{ _CreateTime=value;}
            get{return _CreateTime;}
        }
        /// <summary>
        ///是否删除  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? isDeleted
        {
            set{ _isDeleted=value;}
            get{return _isDeleted;}
        }
        #endregion Model
    }
}
