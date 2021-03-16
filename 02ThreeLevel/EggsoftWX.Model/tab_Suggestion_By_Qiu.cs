using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Suggestion_By_Qiu 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Suggestion_By_Qiu
    {
        public tab_Suggestion_By_Qiu()
        {}
        #region Model
        //ID,ShopClientID,Title,Content,Money,Is_Paid,Is_Finished,UpdateTime,Is_passed,
        private Int32 _ID;
        private Int32 _ShopClientID;
        private string _Title;
        private string _Content;
        private decimal _Money;
        private bool _Is_Paid;
        private bool _Is_Finished;
        private DateTime _UpdateTime;
        private bool _Is_passed;
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
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
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
        public string Content
        {
            set{ _Content=value;}
            get{return _Content;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Money
        {
            set{ _Money=value;}
            get{return _Money;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Paid
        {
            set{ _Is_Paid=value;}
            get{return _Is_Paid;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Finished
        {
            set{ _Is_Finished=value;}
            get{return _Is_Finished;}
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
        public bool Is_passed
        {
            set{ _Is_passed=value;}
            get{return _Is_passed;}
        }
        #endregion Model
    }
}
