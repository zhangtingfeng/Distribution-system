using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Model 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Model
    {
        public tab_ShopClient_Model()
        {}
        #region Model
        //ID,ModelType,UserID,ModelName,ModelContent,ModelUpdateTime,
        private Int32 _ID;
        private Int32 _ModelType;
        private Int32 _UserID;
        private string _ModelName;
        private string _ModelContent;
        private DateTime _ModelUpdateTime;
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
        public Int32 ModelType
        {
            set{ _ModelType=value;}
            get{return _ModelType;}
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
        public string ModelName
        {
            set{ _ModelName=value;}
            get{return _ModelName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModelContent
        {
            set{ _ModelContent=value;}
            get{return _ModelContent;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModelUpdateTime
        {
            set{ _ModelUpdateTime=value;}
            get{ if (_ModelUpdateTime == DateTime.MinValue) _ModelUpdateTime = DateTime.Now;return _ModelUpdateTime;}
        }
        #endregion Model
    }
}
