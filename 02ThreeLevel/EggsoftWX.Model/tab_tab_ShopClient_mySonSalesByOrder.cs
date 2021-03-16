using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_tab_ShopClient_mySonSalesByOrder 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_tab_ShopClient_mySonSalesByOrder
    {
        public tab_tab_ShopClient_mySonSalesByOrder()
        {}
        #region Model
        //ID,UserID,SalesMoney,OrderNum,GoodName,TotalMoney,DeliveryDesc,ReciverText,ShopClientID,BuyUserID,BuyUserIDNickName,
        private Int32 _ID;
        private Int32 _UserID;
        private decimal _SalesMoney;
        private string _OrderNum;
        private string _GoodName;
        private decimal _TotalMoney;
        private string _DeliveryDesc;
        private string _ReciverText;
        private Int32 _ShopClientID;
        private Int32 _BuyUserID;
        private string _BuyUserIDNickName;
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
        public decimal SalesMoney
        {
            set{ _SalesMoney=value;}
            get{return _SalesMoney;}
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
        public string GoodName
        {
            set{ _GoodName=value;}
            get{return _GoodName;}
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
        public string DeliveryDesc
        {
            set{ _DeliveryDesc=value;}
            get{return _DeliveryDesc;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReciverText
        {
            set{ _ReciverText=value;}
            get{return _ReciverText;}
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
        #endregion Model
    }
}
