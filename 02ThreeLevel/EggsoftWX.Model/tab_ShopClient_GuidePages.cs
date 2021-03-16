using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_GuidePages 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_GuidePages
    {
        public tab_ShopClient_GuidePages()
        {}
        #region Model
        //ID,MenuName,MenuIcon,MenuLink,ShopClientID,LinkOrText,MenuText,MenuPos,MenuLevel,ParentID,UpdateTime,CreatTime,IsDeleted,
        private int _ID;
        private string _MenuName;
        private string _MenuIcon;
        private string _MenuLink;
        private Int32? _ShopClientID;
        private bool? _LinkOrText;
        private string _MenuText;
        private Int32? _MenuPos;
        private int? _MenuLevel;
        private int? _ParentID;
        private DateTime? _UpdateTime;
        private DateTime? _CreatTime;
        private bool? _IsDeleted;
        /// <summary>
        /// 主键 int 长度10 占用字节数4 小数位数0 不允许空 默认值无 
        /// </summary>
        public int ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string MenuName
        {
            set{ _MenuName=value;}
            get{return _MenuName;}
        }
        /// <summary>
        ///  nvarchar 长度100 占用字节数200 小数位数0 允许空 默认值无 
        /// </summary>
        public string MenuIcon
        {
            set{ _MenuIcon=value;}
            get{return _MenuIcon;}
        }
        /// <summary>
        ///  nvarchar 长度1000 占用字节数2000 小数位数0 允许空 默认值无 
        /// </summary>
        public string MenuLink
        {
            set{ _MenuLink=value;}
            get{return _MenuLink;}
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
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? LinkOrText
        {
            set{ _LinkOrText=value;}
            get{return _LinkOrText;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string MenuText
        {
            set{ _MenuText=value;}
            get{return _MenuText;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? MenuPos
        {
            set{ _MenuPos=value;}
            get{return _MenuPos;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? MenuLevel
        {
            set{ _MenuLevel=value;}
            get{return _MenuLevel;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
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
        ///是否删除  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
