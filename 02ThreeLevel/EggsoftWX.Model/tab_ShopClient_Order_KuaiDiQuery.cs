using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Order_KuaiDiQuery 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Order_KuaiDiQuery
    {
        public tab_ShopClient_Order_KuaiDiQuery()
        {}
        #region Model
        //ID,OrderID,ShopClient_ID,UpdateTime,InsertTime,UserID,KuaiDiCompany,QueryCount,KuaiDiNumber,JuHeResultList,FreeXML,
        private Int32 _ID;
        private Int32? _OrderID;
        private Int32? _ShopClient_ID;
        private DateTime? _UpdateTime;
        private DateTime? _InsertTime;
        private Int32? _UserID;
        private string _KuaiDiCompany;
        private Int32? _QueryCount;
        private string _KuaiDiNumber;
        private string _JuHeResultList;
        private string _FreeXML;
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
        public Int32? OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InsertTime
        {
            set{ _InsertTime=value;}
            get{return _InsertTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string KuaiDiCompany
        {
            set{ _KuaiDiCompany=value;}
            get{return _KuaiDiCompany;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? QueryCount
        {
            set{ _QueryCount=value;}
            get{return _QueryCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string KuaiDiNumber
        {
            set{ _KuaiDiNumber=value;}
            get{return _KuaiDiNumber;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string JuHeResultList
        {
            set{ _JuHeResultList=value;}
            get{return _JuHeResultList;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string FreeXML
        {
            set{ _FreeXML=value;}
            get{return _FreeXML;}
        }
        #endregion Model
    }
}
