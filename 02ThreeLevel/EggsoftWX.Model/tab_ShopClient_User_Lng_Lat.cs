using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_User_Lng_Lat 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_User_Lng_Lat
    {
        public tab_ShopClient_User_Lng_Lat()
        {}
        #region Model
        //ID,UserID,lng,lat,UpdateTime,BaiDuAdress,BaiDuAllAdress,BaiDulng,BaiDulat,aspxLocation,aspxDescription,
        private Int32 _ID;
        private Int32 _UserID;
        private string _lng;
        private string _lat;
        private DateTime _UpdateTime;
        private string _BaiDuAdress;
        private string _BaiDuAllAdress;
        private string _BaiDulng;
        private string _BaiDulat;
        private string _aspxLocation;
        private string _aspxDescription;
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
        public string lng
        {
            set{ _lng=value;}
            get{return _lng;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string lat
        {
            set{ _lat=value;}
            get{return _lat;}
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
        public string BaiDuAdress
        {
            set{ _BaiDuAdress=value;}
            get{return _BaiDuAdress;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string BaiDuAllAdress
        {
            set{ _BaiDuAllAdress=value;}
            get{return _BaiDuAllAdress;}
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
        public string aspxLocation
        {
            set{ _aspxLocation=value;}
            get{return _aspxLocation;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string aspxDescription
        {
            set{ _aspxDescription=value;}
            get{return _aspxDescription;}
        }
        #endregion Model
    }
}
