using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类Help_Content 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class Help_Content
    {
        public Help_Content()
        {}
        #region Model
        //ID,Help_Class1_ID,Name,LongInfo,UpdateTime,Sort,BuyOrSalse,
        private Int32 _ID;
        private Int32 _Help_Class1_ID;
        private string _Name;
        private string _LongInfo;
        private DateTime _UpdateTime;
        private Int32 _Sort;
        private string _BuyOrSalse;
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
        public Int32 Help_Class1_ID
        {
            set{ _Help_Class1_ID=value;}
            get{return _Help_Class1_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string LongInfo
        {
            set{ _LongInfo=value;}
            get{return _LongInfo;}
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
        public Int32 Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuyOrSalse
        {
            set{ _BuyOrSalse=value;}
            get{return _BuyOrSalse;}
        }
        #endregion Model
    }
}
