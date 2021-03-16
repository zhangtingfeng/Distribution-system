using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Games_LightApp_EachPage 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Games_LightApp_EachPage
    {
        public tab_Games_LightApp_EachPage()
        {}
        #region Model
        //ID,UpdateTime,PicPath,LightApp_ID,NavName,NavPath,ShowPos,ShowNav,
        private Int32 _ID;
        private DateTime _UpdateTime;
        private string _PicPath;
        private Int32 _LightApp_ID;
        private string _NavName;
        private string _NavPath;
        private Int32 _ShowPos;
        private bool _ShowNav;
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
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string PicPath
        {
            set{ _PicPath=value;}
            get{return _PicPath;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 LightApp_ID
        {
            set{ _LightApp_ID=value;}
            get{return _LightApp_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string NavName
        {
            set{ _NavName=value;}
            get{return _NavName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string NavPath
        {
            set{ _NavPath=value;}
            get{return _NavPath;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ShowPos
        {
            set{ _ShowPos=value;}
            get{return _ShowPos;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ShowNav
        {
            set{ _ShowNav=value;}
            get{return _ShowNav;}
        }
        #endregion Model
    }
}
