using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User_GuidePages_History 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User_GuidePages_History
    {
        public tab_User_GuidePages_History()
        {}
        #region Model
        //ID,UserID,GuidePagesID,Parent_UserID,UpdateTime,Count_Visit,GrandParentID,GreatParentID,Type_Visit,CreatTime,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _GuidePagesID;
        private Int32? _Parent_UserID;
        private DateTime? _UpdateTime;
        private Int32? _Count_Visit;
        private Int32? _GrandParentID;
        private Int32? _GreatParentID;
        private string _Type_Visit;
        private DateTime? _CreatTime;
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
        public Int32? GuidePagesID
        {
            set{ _GuidePagesID=value;}
            get{return _GuidePagesID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Parent_UserID
        {
            set{ _Parent_UserID=value;}
            get{return _Parent_UserID;}
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Count_Visit
        {
            set{ _Count_Visit=value;}
            get{return _Count_Visit;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? GrandParentID
        {
            set{ _GrandParentID=value;}
            get{return _GrandParentID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? GreatParentID
        {
            set{ _GreatParentID=value;}
            get{return _GreatParentID;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值('Visit') 
        /// </summary>
        public string Type_Visit
        {
            set{ _Type_Visit=value;}
            get{return _Type_Visit;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        #endregion Model
    }
}
