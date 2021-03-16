using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_User_AskGetMoney 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_User_AskGetMoney
    {
        public View_User_AskGetMoney()
        {}
        #region Model
        //ShopClientID,ID,CardName,AskMoney,AskMemo,UserID,IFSendMoney,UpdateTime,
        private Int32 _ShopClientID;
        private Int32 _ID;
        private string _CardName;
        private decimal _AskMoney;
        private string _AskMemo;
        private Int32 _UserID;
        private bool _IFSendMoney;
        private DateTime _UpdateTime;
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
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardName
        {
            set{ _CardName=value;}
            get{return _CardName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AskMoney
        {
            set{ _AskMoney=value;}
            get{return _AskMoney;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string AskMemo
        {
            set{ _AskMemo=value;}
            get{return _AskMemo;}
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
        public bool IFSendMoney
        {
            set{ _IFSendMoney=value;}
            get{return _IFSendMoney;}
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
