using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b011_InfoAlertMessage 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b011_InfoAlertMessage
    {
        public b011_InfoAlertMessage()
        {}
        #region Model
        //ID,ShopClient_ID,UserID,Type,InfoTip,TypeTableID,Readed,Done,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private Int32? _UserID;
        private string _Type;
        private string _InfoTip;
        private Int32? _TypeTableID;
        private bool? _Readed;
        private bool? _Done;
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
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///未读类型 。例如 运营中心的订单提交  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Type
        {
            set{ _Type=value;}
            get{return _Type;}
        }
        /// <summary>
        ///  nvarchar 长度350 占用字节数700 小数位数0 允许空 默认值无 
        /// </summary>
        public string InfoTip
        {
            set{ _InfoTip=value;}
            get{return _InfoTip;}
        }
        /// <summary>
        ///相应表格的主键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? TypeTableID
        {
            set{ _TypeTableID=value;}
            get{return _TypeTableID;}
        }
        /// <summary>
        ///是否阅读过  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Readed
        {
            set{ _Readed=value;}
            get{return _Readed;}
        }
        /// <summary>
        ///是否处理过  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Done
        {
            set{ _Done=value;}
            get{return _Done;}
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
