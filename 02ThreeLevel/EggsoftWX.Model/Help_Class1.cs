using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类Help_Class1 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class Help_Class1
    {
        public Help_Class1()
        {}
        #region Model
        //ID,ClassName,Updatetime,Sort,IsShow,BuyOrSalse,
        private Int32 _ID;
        private string _ClassName;
        private DateTime _Updatetime;
        private Int32 _Sort;
        private bool _IsShow;
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
        public string ClassName
        {
            set{ _ClassName=value;}
            get{return _ClassName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Updatetime
        {
            set{ _Updatetime=value;}
            get{ if (_Updatetime == DateTime.MinValue) _Updatetime = DateTime.Now;return _Updatetime;}
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
        public bool IsShow
        {
            set{ _IsShow=value;}
            get{return _IsShow;}
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
