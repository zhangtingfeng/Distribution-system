using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Goods_Unit 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Goods_Unit
    {
        public tab_Goods_Unit()
        {}
        #region Model
        //ID,Unit,Updatetime,UpdatePerson,LastLoginMan,
        private Int32 _ID;
        private string _Unit;
        private DateTime _Updatetime;
        private string _UpdatePerson;
        private string _LastLoginMan;
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
        public string Unit
        {
            set{ _Unit=value;}
            get{return _Unit;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Updatetime
        {
            set{ _Updatetime=value;}
            get{ if (_Updatetime == DateTime.MinValue) _Updatetime = DateTime.Now;return _Updatetime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdatePerson
        {
            set{ _UpdatePerson=value;}
            get{return _UpdatePerson;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastLoginMan
        {
            set{ _LastLoginMan=value;}
            get{return _LastLoginMan;}
        }
        #endregion Model
    }
}
