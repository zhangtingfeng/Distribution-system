using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ReturnMoney 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ReturnMoney
    {
        public tab_ReturnMoney()
        {}
        #region Model
        //ID,UpdateTime,OrderID,RefundMoney,AdminFinance_User,FinanceCheck,AdminCheck,ShopClientID,ReturnMoneyTitle,ReturnMoneyContent,
        private Int32 _ID;
        private DateTime _UpdateTime;
        private Int32 _OrderID;
        private decimal _RefundMoney;
        private Int32 _AdminFinance_User;
        private bool _FinanceCheck;
        private bool _AdminCheck;
        private Int32 _ShopClientID;
        private string _ReturnMoneyTitle;
        private string _ReturnMoneyContent;
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
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal RefundMoney
        {
            set{ _RefundMoney=value;}
            get{return _RefundMoney;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 AdminFinance_User
        {
            set{ _AdminFinance_User=value;}
            get{return _AdminFinance_User;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool FinanceCheck
        {
            set{ _FinanceCheck=value;}
            get{return _FinanceCheck;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AdminCheck
        {
            set{ _AdminCheck=value;}
            get{return _AdminCheck;}
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
        public string ReturnMoneyTitle
        {
            set{ _ReturnMoneyTitle=value;}
            get{return _ReturnMoneyTitle;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnMoneyContent
        {
            set{ _ReturnMoneyContent=value;}
            get{return _ReturnMoneyContent;}
        }
        #endregion Model
    }
}
