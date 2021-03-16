using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_System_AnnouncePic 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_System_AnnouncePic
    {
        public tab_System_AnnouncePic()
        {}
        #region Model
        //ID,PicUrl,LinkURL,UpdateTime,ShopClientID,GoodID,
        private Int32 _ID;
        private string _PicUrl;
        private string _LinkURL;
        private DateTime _UpdateTime;
        private Int32 _ShopClientID;
        private Int32 _GoodID;
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
        public string PicUrl
        {
            set{ _PicUrl=value;}
            get{return _PicUrl;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkURL
        {
            set{ _LinkURL=value;}
            get{return _LinkURL;}
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
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
        }
        #endregion Model
    }
}
