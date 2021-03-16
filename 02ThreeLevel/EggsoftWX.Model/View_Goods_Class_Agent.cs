using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_Goods_Class_Agent 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_Goods_Class_Agent
    {
        public View_Goods_Class_Agent()
        {}
        #region Model
        //ID,ShopClient_ID,ClassName,Sort,UpdateTime,AddinInfoList,CoverPicUrl,CoverAndOther,AgentLevel,UserID,
        private Int32 _ID;
        private Int32 _ShopClient_ID;
        private string _ClassName;
        private Int32 _Sort;
        private DateTime _UpdateTime;
        private string _AddinInfoList;
        private string _CoverPicUrl;
        private string _CoverAndOther;
        private Int32 _AgentLevel;
        private Int32 _UserID;
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
        public Int32 ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassName
        {
            set{ _ClassName=value;}
            get{return _ClassName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
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
        public string AddinInfoList
        {
            set{ _AddinInfoList=value;}
            get{return _AddinInfoList;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string CoverPicUrl
        {
            set{ _CoverPicUrl=value;}
            get{return _CoverPicUrl;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string CoverAndOther
        {
            set{ _CoverAndOther=value;}
            get{return _CoverAndOther;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 AgentLevel
        {
            set{ _AgentLevel=value;}
            get{return _AgentLevel;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        #endregion Model
    }
}
