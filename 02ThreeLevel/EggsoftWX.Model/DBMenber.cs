using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类DBMenber 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class DBMenber
    {
        public DBMenber()
        {}
        #region Model
        //ID,DbName,FilePath,CreateTime,
        private Int32 _ID;
        private string _DbName;
        private string _FilePath;
        private DateTime _CreateTime;
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
        public string DbName
        {
            set{ _DbName=value;}
            get{return _DbName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string FilePath
        {
            set{ _FilePath=value;}
            get{return _FilePath;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set{ _CreateTime=value;}
            get{ if (_CreateTime == DateTime.MinValue) _CreateTime = DateTime.Now;return _CreateTime;}
        }
        #endregion Model
    }
}
