using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_System_Menu_WeiXin 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_System_Menu_WeiXin
    {
        public tab_ShopClient_System_Menu_WeiXin()
        {}
        #region Model
        //ID,MenuName,MenuType,MenuContent,UpdateTime,ParentID,Pos,ShopClientID,
        private Int32 _ID;
        private string _MenuName;
        private Int32 _MenuType;
        private string _MenuContent;
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
        public Int32 MenuType
        {
            set{ _MenuType=value;}
            get{return _MenuType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string MenuContent
        {
            set{ _MenuContent=value;}
            get{return _MenuContent;}
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
