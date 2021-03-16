using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_O2O_ShopInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_O2O_ShopInfo
    {
        public tab_ShopClient_O2O_ShopInfo()
        {}
        #region Model
        //ID,ShopClientID,ShopName,AdddressProvince,AdddressCity,AdddressCountry,BaiDulng,BaiDulat,UpdateTime,ShopAdress,Lng,Lat,Tel,ContactMan,XML,ShopAdMsg,ShopDayTime,ISDeleted,
        private Int32 _ID;
        private Int32 _ShopClientID;
        private string _ShopName;
        private Int32 _AdddressProvince;
        private Int32 _AdddressCity;
        private Int32 _AdddressCountry;
        private string _BaiDulng;
        private string _BaiDulat;
        private DateTime _UpdateTime;
        private string _ShopAdress;
        private string _Lng;
        private string _Lat;
        private string _Tel;
        private string _ContactMan;
        private string _XML;
        private string _ShopAdMsg;
        private string _ShopDayTime;
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
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShopName
        {
            set{ _ShopName=value;}
            get{return _ShopName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 AdddressProvince
        {
            set{ _AdddressProvince=value;}
            get{return _AdddressProvince;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 AdddressCity
        {
            set{ _AdddressCity=value;}
            get{return _AdddressCity;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 AdddressCountry
        {
            set{ _AdddressCountry=value;}
            get{return _AdddressCountry;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string BaiDulng
        {
            set{ _BaiDulng=value;}
            get{return _BaiDulng;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string BaiDulat
        {
            set{ _BaiDulat=value;}
            get{return _BaiDulat;}
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
        public string ShopAdress
        {
            set{ _ShopAdress=value;}
            get{return _ShopAdress;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lng
        {
            set{ _Lng=value;}
            get{return _Lng;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lat
        {
            set{ _Lat=value;}
            get{return _Lat;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set{ _Tel=value;}
            get{return _Tel;}
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
        public string XML
        {
            set{ _XML=value;}
            get{return _XML;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShopAdMsg
        {
            set{ _ShopAdMsg=value;}
            get{return _ShopAdMsg;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShopDayTime
        {
            set{ _ShopDayTime=value;}
            get{return _ShopDayTime;}
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
