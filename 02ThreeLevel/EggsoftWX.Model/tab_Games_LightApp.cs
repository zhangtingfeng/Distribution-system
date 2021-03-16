using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Games_LightApp 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Games_LightApp
    {
        public tab_Games_LightApp()
        {}
        #region Model
        //ID,UserID,UpdateTime,Title,Description,Mp3Path,
        private Int32 _ID;
        private Int32 _UserID;
        private DateTime _UpdateTime;
        private string _Title;
        private string _Description;
        private string _Mp3Path;
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
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
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
        public string Description
        {
            set{ _Description=value;}
            get{return _Description;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mp3Path
        {
            set{ _Mp3Path=value;}
            get{return _Mp3Path;}
        }
        #endregion Model
    }
}
