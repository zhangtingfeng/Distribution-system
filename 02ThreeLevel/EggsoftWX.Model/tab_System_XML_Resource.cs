using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_System_XML_Resource 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_System_XML_Resource
    {
        public tab_System_XML_Resource()
        {}
        #region Model
        //ID,type,Text,Pic,UpdateTime,LinkURL,ParentID,
        private Int32 _ID;
        private Int32 _Type;
        private string _Text;
        private string _Pic;
        private DateTime _UpdateTime;
        private string _LinkURL;
        private Int32 _ParentID;
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
        public Int32 type
        {
            set{ _Type=value;}
            get{return _Type;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            set{ _Text=value;}
            get{return _Text;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pic
        {
            set{ _Pic=value;}
            get{return _Pic;}
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
        public string LinkURL
        {
            set{ _LinkURL=value;}
            get{return _LinkURL;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        #endregion Model
    }
}
