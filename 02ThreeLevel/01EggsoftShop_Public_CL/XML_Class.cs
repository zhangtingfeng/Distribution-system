using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Eggsoft_Public_CL
{
    /// <summary>
    ///XML_Class 的摘要说明
    /// </summary>
    public class XML_Class
    {
        public XML_Class()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        public class LimitTimerBuy
        {
            public LimitTimerBuy()
            { }
            #region LimitTimerBuy
            private bool _IFLimitTimer;
            private DateTime _StartTime;
            private DateTime _EndTime;
            private Decimal _TimePrice;

            /// <summary>
            /// 
            /// </summary>
            public bool IFLimitTimer
            {
                set { _IFLimitTimer = value; }
                get { return _IFLimitTimer; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime StartTime
            {
                set { _StartTime = value; }
                get { return _StartTime; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime EndTime
            {
                set { _EndTime = value; }
                get { return _EndTime; }
            }
            /// <summary>
            /// 
            /// </summary>
            public Decimal TimePrice
            {
                set { _TimePrice = value; }
                get { return _TimePrice; }
            }
            #endregion
        }


        public class GoodsClass_Cover
        {
            public GoodsClass_Cover()
            { }
            #region Model
            private string _Font_Color;
            private string _Background_Color;
            private string _MemoText;

            /// <summary>
            /// 
            /// </summary>
            public String Font_Color
            {
                set { _Font_Color = value; }
                get { return _Font_Color; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String Background_Color
            {
                set { _Background_Color = value; }
                get { return _Background_Color; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String MemoText
            {
                set { _MemoText = value; }
                get { return _MemoText; }
            }
            #endregion
        }



        public class ShopClient_Dictionaries
        {

            //           PromotionalPrice 促销价
            //                Price 价格
            //SalesCount 销量
            //BuyNow 立即购买
            //AddToCart 加入购物车




            public ShopClient_Dictionaries()
            { }
            #region Model
            //private string _PromotionalPrice ;
            //private string _PriceText ;
            //private string _BuyNow ;
            //private string _AddToCart ;
            //private string _NoStock ;
            //private string _StockBalance;
            private string _PromotionalPrice = "促销价";
            private string _PriceText = "价格";
            private string _SalesCount = "销量";
            private string _BuyNow = "立即购买";
            private string _AddToCart = "加入购物车";
            private string _NoStock = "暂无库存";
            private string _StockBalance = "库存量";
            private string _Postage = "包邮";
            private string _OffSalses = "此商品已下架";
            private bool _NeedRegIfBuy = false;


            /// <summary>
            /// 
            /// </summary>
            public String PromotionalPrice
            {
                set { _PromotionalPrice = value; }
                get { return _PromotionalPrice; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String PriceText
            {
                set { _PriceText = value; }
                get { return _PriceText; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String SalesCount
            {
                set { _SalesCount = value; }
                get { return _SalesCount; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String BuyNow
            {
                set { _BuyNow = value; }
                get { return _BuyNow; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String AddToCart
            {
                set { _AddToCart = value; }
                get { return _AddToCart; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String NoStock
            {
                set { _NoStock = value; }
                get { return _NoStock; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String StockBalance
            {
                set { _StockBalance = value; }
                get { return _StockBalance; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String Postage
            {
                set { _Postage = value; }
                get { return _Postage; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String OffSalses
            {
                set { _OffSalses = value; }
                get { return _OffSalses; }
            }
            /// <summary>
            /// 
            /// </summary>
            public bool NeedRegIfBuy
            {
                set { _NeedRegIfBuy = value; }
                get { return _NeedRegIfBuy; }
            }
            #endregion
        }


        public class ContactUS
        {
            public ContactUS()
            { }
            #region Model
            private string _Font_Color;
            private string _Background_Color;
            private string _ContactUSText;
            private bool _IFShow;
            private int _ShowHeadType;
            private string _LinkText;

            /// <summary>
            /// 
            /// </summary>
            public String Font_Color
            {
                set { _Font_Color = value; }
                get { return _Font_Color; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String Background_Color
            {
                set { _Background_Color = value; }
                get { return _Background_Color; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String ContactUSText
            {
                set { _ContactUSText = value; }
                get { return _ContactUSText; }
            }
            /// <summary>
            /// 
            /// </summary>
            public bool IFShow
            {
                set { _IFShow = value; }
                get { return _IFShow; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int ShowHeadType
            {
                set { _ShowHeadType = value; }
                get { return _ShowHeadType; }
            }
            /// <summary>
            /// 
            /// </summary>
            public String LinkText
            {
                set { _LinkText = value; }
                get { return _LinkText; }
            }
            #endregion
        }
    }
}