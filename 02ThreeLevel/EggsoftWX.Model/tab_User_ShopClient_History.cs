using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User_ShopClient_History 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User_ShopClient_History
    {
        public tab_User_ShopClient_History()
        {}
        #region Model
        //ID,UserID,ShopClientID,Parent_UserID,UpdateTime,Count_Visit,Type_Visit,CreatTime,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _ShopClientID;
        private Int32? _Parent_UserID;
        private DateTime? _UpdateTime;
        private Int32? _Count_Visit;
        private string _Type_Visit;
        private DateTime? _CreatTime;
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
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
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
        public string Type_Visit
        {
            set{ _Type_Visit=value;}
            get{return _Type_Visit;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        #endregion Model
    }
}
