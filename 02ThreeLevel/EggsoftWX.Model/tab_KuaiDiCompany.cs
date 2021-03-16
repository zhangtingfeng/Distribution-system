using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_KuaiDiCompany 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_KuaiDiCompany
    {
        public tab_KuaiDiCompany()
        {}
        #region Model
        //ID,KuaidiCompanyCode,KuaidiName,Updatetime,
        private Int32 _ID;
        private string _KuaidiCompanyCode;
        private string _KuaidiName;
        private DateTime? _Updatetime;
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
        public string KuaidiCompanyCode
        {
            set{ _KuaidiCompanyCode=value;}
            get{return _KuaidiCompanyCode;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string KuaidiName
        {
            set{ _KuaidiName=value;}
            get{return _KuaidiName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Updatetime
        {
            set{ _Updatetime=value;}
            get{return _Updatetime;}
        }
        #endregion Model
    }
}
