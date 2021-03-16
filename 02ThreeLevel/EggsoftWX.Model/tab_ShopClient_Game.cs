using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Game 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Game
    {
        public tab_ShopClient_Game()
        {}
        #region Model
        //ID,GameName,FromName,SendType,HowManyMoney,EndTime,UpdateTime,ShopClientID,IsDeleted,
        private Int32 _ID;
        private string _GameName;
        private string _FromName;
        private Int32 _SendType;
        private decimal _HowManyMoney;
        private DateTime _EndTime;
        private DateTime _UpdateTime;
        private Int32 _ShopClientID;
        private bool _IsDeleted;
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
        public string GameName
        {
            set{ _GameName=value;}
            get{return _GameName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromName
        {
            set{ _FromName=value;}
            get{return _FromName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 SendType
        {
            set{ _SendType=value;}
            get{return _SendType;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal HowManyMoney
        {
            set{ _HowManyMoney=value;}
            get{return _HowManyMoney;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        {
            set{ _EndTime=value;}
            get{ if (_EndTime == DateTime.MinValue) _EndTime = DateTime.Now;return _EndTime;}
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
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
