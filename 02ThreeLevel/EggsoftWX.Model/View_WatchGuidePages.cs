using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_WatchGuidePages 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_WatchGuidePages
    {
        public View_WatchGuidePages()
        {}
        #region Model
        //ID,UserID,Parent_UserID,UpdateTime,Count_Visit,GrandParentID,GreatParentID,GuidePagesID,MenuName,ShopClientID,IsDeleted,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _Parent_UserID;
        private DateTime? _UpdateTime;
        private Int32? _Count_Visit;
        private Int32? _GrandParentID;
        private Int32? _GreatParentID;
        private int? _GuidePagesID;
        private string _MenuName;
        private Int32? _ShopClientID;
        private bool? _IsDeleted;
        /// <summary>
        /// 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? Parent_UserID
        {
            set{ _Parent_UserID=value;}
            get{return _Parent_UserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? Count_Visit
        {
            set{ _Count_Visit=value;}
            get{return _Count_Visit;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? GrandParentID
        {
            set{ _GrandParentID=value;}
            get{return _GrandParentID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? GreatParentID
        {
            set{ _GreatParentID=value;}
            get{return _GreatParentID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GuidePagesID
        {
            set{ _GuidePagesID=value;}
            get{return _GuidePagesID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MenuName
        {
            set{ _MenuName=value;}
            get{return _MenuName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
