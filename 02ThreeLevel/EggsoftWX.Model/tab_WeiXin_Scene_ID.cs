using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_WeiXin_Scene_ID 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_WeiXin_Scene_ID
    {
        public tab_WeiXin_Scene_ID()
        {}
        #region Model
        //ID,Scene_ActionName,Scene_Memo,UpdateTime,
        private Int32 _ID;
        private string _Scene_ActionName;
        private string _Scene_Memo;
        private DateTime _UpdateTime;
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
        public string Scene_ActionName
        {
            set{ _Scene_ActionName=value;}
            get{return _Scene_ActionName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Scene_Memo
        {
            set{ _Scene_Memo=value;}
            get{return _Scene_Memo;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set{ _UpdateTime=value;}
            get{ if (_UpdateTime == DateTime.MinValue) _UpdateTime = DateTime.Now;return _UpdateTime;}
        }
        #endregion Model
    }
}
