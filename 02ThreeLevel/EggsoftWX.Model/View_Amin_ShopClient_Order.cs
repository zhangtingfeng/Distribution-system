using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Amin_ShopClient_Order 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Amin_ShopClient_Order
    {
        public View_Amin_ShopClient_Order()
        {}
        #region Model
        //ID,ShopClientName,ShopClientType,Username,AllNotDelivery_In7Days,AllOrder_In7Days,ContactMan,ErJiYuMing,AllNotDelivery,sumTotalMoney,
        private Int32 _ID;
        private string _ShopClientName;
        private string _ShopClientType;
        private string _Username;
        private Int32? _AllNotDelivery_In7Days;
        private Int32? _AllOrder_In7Days;
        private string _ContactMan;
        private string _ErJiYuMing;
        private Int32? _AllNotDelivery;
        private decimal? _sumTotalMoney;
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
        public string ShopClientName
        {
            set{ _ShopClientName=value;}
            get{return _ShopClientName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShopClientType
        {
            set{ _ShopClientType=value;}
            get{return _ShopClientType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Username
        {
            set{ _Username=value;}
            get{return _Username;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? AllNotDelivery_In7Days
        {
            set{ _AllNotDelivery_In7Days=value;}
            get{return _AllNotDelivery_In7Days;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? AllOrder_In7Days
        {
            set{ _AllOrder_In7Days=value;}
            get{return _AllOrder_In7Days;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContactMan
        {
            set{ _ContactMan=value;}
            get{return _ContactMan;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ErJiYuMing
        {
            set{ _ErJiYuMing=value;}
            get{return _ErJiYuMing;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? AllNotDelivery
        {
            set{ _AllNotDelivery=value;}
            get{return _AllNotDelivery;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? sumTotalMoney
        {
            set{ _sumTotalMoney=value;}
            get{return _sumTotalMoney;}
        }
        #endregion Model
    }
}
