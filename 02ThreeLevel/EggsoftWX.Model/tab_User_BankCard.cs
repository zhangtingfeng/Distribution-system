using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User_BankCard 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User_BankCard
    {
        public tab_User_BankCard()
        {}
        #region Model
        //ID,bankName,Name,CardNum,UserID,UpdateTime,
        private Int32 _ID;
        private string _bankName;
        private string _Name;
        private string _CardNum;
        private Int32 _UserID;
        private DateTime _UpdateTime;
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
        public string bankName
        {
            set{ _bankName=value;}
            get{return _bankName;}
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
        public string CardNum
        {
            set{ _CardNum=value;}
            get{return _CardNum;}
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
        #endregion Model
    }
}
