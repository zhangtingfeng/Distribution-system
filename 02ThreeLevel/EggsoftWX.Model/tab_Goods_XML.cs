using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Goods_XML 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Goods_XML
    {
        public tab_Goods_XML()
        {}
        #region Model
        //ID,XMLName,ShopClient_ID,UpdateTime,XMLContent,
        private Int32 _ID;
        private string _XMLName;
        private Int32 _ShopClient_ID;
        private DateTime _UpdateTime;
        private string _XMLContent;
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
        public string XMLName
        {
            set{ _XMLName=value;}
            get{return _XMLName;}
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
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string XMLContent
        {
            set{ _XMLContent=value;}
            get{return _XMLContent;}
        }
        #endregion Model
    }
}
