using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_NET_ROOT_Menu 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_NET_ROOT_Menu
    {
        public tab_ShopClient_NET_ROOT_Menu()
        {}
        #region Model
        //ID,MenuName,MenuLink,UpdateTime,ParentID,Pos,ShopClientID,
        private Int32 _ID;
        private string _MenuName;
        private string _MenuLink;
        private DateTime _UpdateTime;
        private Int32 _ParentID;
        private Int32 _Pos;
        private Int32 _ShopClientID;
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
        public string MenuName
        {
            set{ _MenuName=value;}
            get{return _MenuName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MenuLink
        {
            set{ _MenuLink=value;}
            get{return _MenuLink;}
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
        public Int32 ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Pos
        {
            set{ _Pos=value;}
            get{return _Pos;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        #endregion Model
    }
}
