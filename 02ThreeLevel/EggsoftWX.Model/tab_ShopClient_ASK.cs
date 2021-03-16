using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_ASK 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_ASK
    {
        public tab_ShopClient_ASK()
        {}
        #region Model
        //ID,ShopClientID,ShopClientAsk,UpdateTime,type,OrderID,IFPayByShenMa_Finace,IFPayByShenMa_Manager,
        private Int32 _ID;
        private Int32 _ShopClientID;
        private string _ShopClientAsk;
        private DateTime _UpdateTime;
        private string _Type;
        private Int32 _OrderID;
        private bool _IFPayByShenMa_Finace;
        private bool _IFPayByShenMa_Manager;
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
        public string ShopClientAsk
        {
            set{ _ShopClientAsk=value;}
            get{return _ShopClientAsk;}
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
        public string type
        {
            set{ _Type=value;}
            get{return _Type;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 OrderID
        {
            set{ _OrderID=value;}
            get{return _OrderID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IFPayByShenMa_Finace
        {
            set{ _IFPayByShenMa_Finace=value;}
            get{return _IFPayByShenMa_Finace;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IFPayByShenMa_Manager
        {
            set{ _IFPayByShenMa_Manager=value;}
            get{return _IFPayByShenMa_Manager;}
        }
        #endregion Model
    }
}
