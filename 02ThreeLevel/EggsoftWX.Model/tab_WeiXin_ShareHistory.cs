using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_WeiXin_ShareHistory 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_WeiXin_ShareHistory
    {
        public tab_WeiXin_ShareHistory()
        {}
        #region Model
        //ID,UserID,PageID_Marker,AddTime,ParentID,UpdateTime,Count_Visit,GrandParentID,GreatParentID,
        private Int32 _ID;
        private Int32 _UserID;
        private string _PageID_Marker;
        private DateTime _AddTime;
        private Int32 _ParentID;
        private DateTime _UpdateTime;
        private Int32 _Count_Visit;
        private Int32 _GrandParentID;
        private Int32 _GreatParentID;
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
        public Int32 UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string PageID_Marker
        {
            set{ _PageID_Marker=value;}
            get{return _PageID_Marker;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime
        {
            set{ _AddTime=value;}
            get{ if (_AddTime == DateTime.MinValue) _AddTime = DateTime.Now;return _AddTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Count_Visit
        {
            set{ _Count_Visit=value;}
            get{return _Count_Visit;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 GrandParentID
        {
            set{ _GrandParentID=value;}
            get{return _GrandParentID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 GreatParentID
        {
            set{ _GreatParentID=value;}
            get{return _GreatParentID;}
        }
        #endregion Model
    }
}
