using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Admin_ShopClientPower 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Admin_ShopClientPower
    {
        public tab_Admin_ShopClientPower()
        {}
        #region Model
        //ID,ShopClientID,ShopClientPowerItemName,PowerINT,PowerRoleID,PowerINTDatail,isDeleted,CreatTime,Updatetime,Creatby,Updateby,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private string _ShopClientPowerItemName;
        private bool? _PowerINT;
        private Int32? _PowerRoleID;
        private string _PowerINTDatail;
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
        ///  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopClientPowerItemName
        {
            set{ _ShopClientPowerItemName=value;}
            get{return _ShopClientPowerItemName;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? PowerINT
        {
            set{ _PowerINT=value;}
            get{return _PowerINT;}
        }
        /// <summary>
        ///有权限的角色ID 。 通过 用户姓名 用户名 也可以是手机号码查询tab_ShopClient_Role_Power 得到该ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? PowerRoleID
        {
            set{ _PowerRoleID=value;}
            get{return _PowerRoleID;}
        }
        /// <summary>
        ///权限的具体描述   如消息通知模板  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string PowerINTDatail
        {
            set{ _PowerINTDatail=value;}
            get{return _PowerINTDatail;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? isDeleted
        {
            set{ _isDeleted=value;}
            get{return _isDeleted;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
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
