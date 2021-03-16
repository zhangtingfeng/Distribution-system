using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_PE_Region 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_PE_Region
    {
        public tab_PE_Region()
        {}
        #region Model
        //ID,Country,Province,City,Area,PostCode,AreaCode,
        private Int32 _ID;
        private string _Country;
        private string _Province;
        private string _City;
        private string _Area;
        private string _PostCode;
        private string _AreaCode;
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
        public string Country
        {
            set{ _Country=value;}
            get{return _Country;}
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
        /// <summary>
        /// 
        /// </summary>
        public string PostCode
        {
            set{ _PostCode=value;}
            get{return _PostCode;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            set{ _AreaCode=value;}
            get{return _AreaCode;}
        }
        #endregion Model
    }
}
