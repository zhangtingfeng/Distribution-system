using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_DoTask_Services 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_DoTask_Services
    {
        public tab_DoTask_Services()
        {}
        #region Model
        //ID,TaskType,TaskXML,TaskIfDone,InsertTime,DoTime,TaskMemo,CreatTime,
        private Int32 _ID;
        private string _TaskType;
        private string _TaskXML;
        private bool? _TaskIfDone;
        private DateTime? _InsertTime;
        private DateTime? _DoTime;
        private string _TaskMemo;
        private DateTime? _CreatTime;
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string TaskType
        {
            set{ _TaskType=value;}
            get{return _TaskType;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string TaskXML
        {
            set{ _TaskXML=value;}
            get{return _TaskXML;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? TaskIfDone
        {
            set{ _TaskIfDone=value;}
            get{return _TaskIfDone;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? InsertTime
        {
            set{ _InsertTime=value;}
            get{return _InsertTime;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? DoTime
        {
            set{ _DoTime=value;}
            get{return _DoTime;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string TaskMemo
        {
            set{ _TaskMemo=value;}
            get{return _TaskMemo;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        #endregion Model
    }
}
