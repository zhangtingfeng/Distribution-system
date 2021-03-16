using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_SearchKeyWordList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_SearchKeyWordList
    {
        public tab_ShopClient_SearchKeyWordList()
        {}
        #region Model
        //ID,ShopClientID,InsertTime,UpdateTime,Keyword,KeywordCount,SearchArea,SearchUserNickName,
        private Int32 _ID;
        private Int32 _ShopClientID;
        private DateTime? _InsertTime;
        private DateTime? _UpdateTime;
        private string _Keyword;
        private Int32? _KeywordCount;
        private string _SearchArea;
        private string _SearchUserNickName;
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
        public DateTime? InsertTime
        {
            set{ _InsertTime=value;}
            get{return _InsertTime;}
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
        public string Keyword
        {
            set{ _Keyword=value;}
            get{return _Keyword;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? KeywordCount
        {
            set{ _KeywordCount=value;}
            get{return _KeywordCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string SearchArea
        {
            set{ _SearchArea=value;}
            get{return _SearchArea;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string SearchUserNickName
        {
            set{ _SearchUserNickName=value;}
            get{return _SearchUserNickName;}
        }
        #endregion Model
    }
}
