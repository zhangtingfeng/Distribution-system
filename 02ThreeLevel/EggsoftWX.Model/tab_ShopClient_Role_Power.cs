using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Role_Power 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Role_Power
    {
        public tab_ShopClient_Role_Power()
        {}
        #region Model
        //ID,ShopClientID,RoleName,isDeleted,CreatTime,Updatetime,Creatby,Updateby,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private string _RoleName;
        private bool? _isDeleted;
        private DateTime? _CreatTime;
        private DateTime? _Updatetime;
        private string _Creatby;
        private string _Updateby;
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
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string RoleName
        {
            set{ _RoleName=value;}
            get{return _RoleName;}
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
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Creatby
        {
            set{ _Creatby=value;}
            get{return _Creatby;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Updateby
        {
            set{ _Updateby=value;}
            get{return _Updateby;}
        }
        #endregion Model
    }
}
