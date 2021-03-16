using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_DistributionMoney 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_DistributionMoney
    {
        public tab_ShopClient_DistributionMoney()
        {}
        #region Model
        //ID,Partner,Partner1,Partner2,Name,UpdateTime,OperateMan,ShopGet,ShopClientID,
        private Int32 _ID;
        private decimal _Partner;
        private decimal _Partner1;
        private decimal _Partner2;
        private string _Name;
        private DateTime _UpdateTime;
        private string _OperateMan;
        private decimal _ShopGet;
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
        public decimal Partner
        {
            set{ _Partner=value;}
            get{return _Partner;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Partner1
        {
            set{ _Partner1=value;}
            get{return _Partner1;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Partner2
        {
            set{ _Partner2=value;}
            get{return _Partner2;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
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
        public string OperateMan
        {
            set{ _OperateMan=value;}
            get{return _OperateMan;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ShopGet
        {
            set{ _ShopGet=value;}
            get{return _ShopGet;}
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
