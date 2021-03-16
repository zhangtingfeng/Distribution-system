using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_FreightTemplate 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_FreightTemplate
    {
        public tab_FreightTemplate()
        {}
        #region Model
        //ID,Name,ShopClient_ID,Freight,FreightMore,UpdateTime,CreateTime,Remarks,HowmanysNoFreight,HowmuchNoFreight,HowkgNoFreight,Province,City,Area,
        private Int32 _ID;
        private string _Name;
        private Int32 _ShopClient_ID;
        private decimal _Freight;
        private decimal _FreightMore;
        private DateTime _UpdateTime;
        private DateTime _CreateTime;
        private string _Remarks;
        private Int32 _HowmanysNoFreight;
        private decimal _HowmuchNoFreight;
        private decimal _HowkgNoFreight;
        private string _Province;
        private string _City;
        private string _Area;
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
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Freight
        {
            set{ _Freight=value;}
            get{return _Freight;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FreightMore
        {
            set{ _FreightMore=value;}
            get{return _FreightMore;}
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
        public DateTime CreateTime
        {
            set{ _CreateTime=value;}
            get{ if (_CreateTime == DateTime.MinValue) _CreateTime = DateTime.Now;return _CreateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks
        {
            set{ _Remarks=value;}
            get{return _Remarks;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 HowmanysNoFreight
        {
            set{ _HowmanysNoFreight=value;}
            get{return _HowmanysNoFreight;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal HowmuchNoFreight
        {
            set{ _HowmuchNoFreight=value;}
            get{return _HowmuchNoFreight;}
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal HowkgNoFreight
        {
            set{ _HowkgNoFreight=value;}
            get{return _HowkgNoFreight;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Province
        {
            set{ _Province=value;}
            get{return _Province;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            set{ _City=value;}
            get{return _City;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Area
        {
            set{ _Area=value;}
            get{return _Area;}
        }
        #endregion Model
    }
}
