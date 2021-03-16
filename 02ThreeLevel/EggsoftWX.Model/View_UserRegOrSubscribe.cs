using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_UserRegOrSubscribe 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_UserRegOrSubscribe
    {
        public View_UserRegOrSubscribe()
        {}
        #region Model
        //Updatetime,RegCount,SubCount,
        private string _Updatetime;
        private Int32 _RegCount;
        private Int32 _SubCount;
        /// <summary>
        /// 
        /// </summary>
        public string Updatetime
        {
            set{ _Updatetime=value;}
            get{return _Updatetime;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 RegCount
        {
            set{ _RegCount=value;}
            get{return _RegCount;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 SubCount
        {
            set{ _SubCount=value;}
            get{return _SubCount;}
        }
        #endregion Model
    }
}
