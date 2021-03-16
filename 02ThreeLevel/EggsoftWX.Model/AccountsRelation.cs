using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类AccountsRelation 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AccountsRelation
    {
        public AccountsRelation()
        {}
        #region Model
        //ID,Uid,Pid,Wid,Yid,
        private Int32 _ID;
        private Int32 _Uid;
        private Int32 _Pid;
        private Int32 _Wid;
        private Int32 _Yid;
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
        public Int32 Uid
        {
            set{ _Uid=value;}
            get{return _Uid;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Pid
        {
            set{ _Pid=value;}
            get{return _Pid;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Wid
        {
            set{ _Wid=value;}
            get{return _Wid;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Yid
        {
            set{ _Yid=value;}
            get{return _Yid;}
        }
        #endregion Model
    }
}
