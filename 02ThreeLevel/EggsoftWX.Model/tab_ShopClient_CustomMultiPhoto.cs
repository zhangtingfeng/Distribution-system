using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_CustomMultiPhoto 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_CustomMultiPhoto
    {
        public tab_ShopClient_CustomMultiPhoto()
        {}
        #region Model
        //ID,Title,ContentMultiPhoto,Author,UpdateTime,ShopClient_ID,
        private Int32 _ID;
        private string _Title;
        private string _ContentMultiPhoto;
        private string _Author;
        private DateTime _UpdateTime;
        private Int32 _ShopClient_ID;
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
        public string Title
        {
            set{ _Title=value;}
            get{return _Title;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentMultiPhoto
        {
            set{ _ContentMultiPhoto=value;}
            get{return _ContentMultiPhoto;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Author
        {
            set{ _Author=value;}
            get{return _Author;}
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
        public Int32 ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        #endregion Model
    }
}
