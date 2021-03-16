using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_O2O_TakeGoods 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_O2O_TakeGoods
    {
        public tab_ShopClient_O2O_TakeGoods()
        {}
        #region Model
        //ID,UserID,TakeName,TakeOrderID,TakePhone,TakeDateTime,Updatetime,TakeO2OShopID,HadTaked,ISDeleted,
        private Int32 _ID;
        private Int32 _UserID;
        private string _TakeName;
        private string _TakeOrderID;
        private string _TakePhone;
        private DateTime _TakeDateTime;
        private DateTime _Updatetime;
        private Int32 _TakeO2OShopID;
        private Int32 _HadTaked;
        private bool _ISDeleted;
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
        public string TakeName
        {
            set{ _TakeName=value;}
            get{return _TakeName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string TakeOrderID
        {
            set{ _TakeOrderID=value;}
            get{return _TakeOrderID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string TakePhone
        {
            set{ _TakePhone=value;}
            get{return _TakePhone;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime TakeDateTime
        {
            set{ _TakeDateTime=value;}
            get{ if (_TakeDateTime == DateTime.MinValue) _TakeDateTime = DateTime.Now;return _TakeDateTime;}
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
        public Int32 TakeO2OShopID
        {
            set{ _TakeO2OShopID=value;}
            get{return _TakeO2OShopID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 HadTaked
        {
            set{ _HadTaked=value;}
            get{return _HadTaked;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ISDeleted
        {
            set{ _ISDeleted=value;}
            get{return _ISDeleted;}
        }
        #endregion Model
    }
}
