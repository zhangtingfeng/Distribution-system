using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_tab_ShopClient_myGetFenXiaoMoneyHistory 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_tab_ShopClient_myGetFenXiaoMoneyHistory
    {
        public tab_tab_ShopClient_myGetFenXiaoMoneyHistory()
        {}
        #region Model
        //ID,UserID,BuyUserID,BuyUserIDNickName,CreatDateTime,TotalMoney,OrderNum,MyGetPercent,MyWillGetMoney,IFGetToMoney,IFGetToMoneyDesc,ShopClientID,
        private Int32 _ID;
        private Int32 _UserID;
        private Int32 _BuyUserID;
        private string _BuyUserIDNickName;
        private DateTime _CreatDateTime;
        private decimal _TotalMoney;
        private string _OrderNum;
        private decimal _MyGetPercent;
        private decimal _MyWillGetMoney;
        private bool _IFGetToMoney;
        private string _IFGetToMoneyDesc;
        private Int32 _ShopClientID;
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
        public Int32 BuyUserID
        {
            set{ _BuyUserID=value;}
            get{return _BuyUserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuyUserIDNickName
        {
            set{ _BuyUserIDNickName=value;}
            get{return _BuyUserIDNickName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatDateTime
        {
            set{ _CreatDateTime=value;}
            get{ if (_CreatDateTime == DateTime.MinValue) _CreatDateTime = DateTime.Now;return _CreatDateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalMoney
        {
            set{ _TotalMoney=value;}
            get{return _TotalMoney;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderNum
        {
            set{ _OrderNum=value;}
            get{return _OrderNum;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MyGetPercent
        {
            set{ _MyGetPercent=value;}
            get{return _MyGetPercent;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MyWillGetMoney
        {
            set{ _MyWillGetMoney=value;}
            get{return _MyWillGetMoney;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IFGetToMoney
        {
            set{ _IFGetToMoney=value;}
            get{return _IFGetToMoney;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string IFGetToMoneyDesc
        {
            set{ _IFGetToMoneyDesc=value;}
            get{return _IFGetToMoneyDesc;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        #endregion Model
    }
}
