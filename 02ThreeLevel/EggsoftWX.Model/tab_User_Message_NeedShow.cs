using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User_Message_NeedShow 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User_Message_NeedShow
    {
        public tab_User_Message_NeedShow()
        {}
        #region Model
        //ID,UserID,InfoNeedShow,IFShowed,InfoType,UpdateTime,UpdateBy,CreatTime,CreateBy,Isdeleted,
        private Int32 _ID;
        private Int32? _UserID;
        private string _InfoNeedShow;
        private bool? _IFShowed;
        private string _InfoType;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private DateTime? _CreatTime;
        private string _CreateBy;
        private Int32? _Isdeleted;
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
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string InfoNeedShow
        {
            set{ _InfoNeedShow=value;}
            get{return _InfoNeedShow;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IFShowed
        {
            set{ _IFShowed=value;}
            get{return _IFShowed;}
        }
        /// <summary>
        ///警告类型  需要购买新单提示 用 NeedByNewOrder1779  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string InfoType
        {
            set{ _InfoType=value;}
            get{return _InfoType;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string CreateBy
        {
            set{ _CreateBy=value;}
            get{return _CreateBy;}
        }
        /// <summary>
        ///  smallint 长度5 占用字节数2 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Isdeleted
        {
            set{ _Isdeleted=value;}
            get{return _Isdeleted;}
        }
        #endregion Model
    }
}
