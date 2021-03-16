using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_HelpSSQ_3D 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_HelpSSQ_3D
    {
        public tab_HelpSSQ_3D()
        {}
        #region Model
        //ID,Name,HaoMa,QiShu,BonusDay,ISUsedEveryDay,CreateTime,UpdateTime,IsDeleted,
        private Int32 _ID;
        private string _Name;
        private string _HaoMa;
        private string _QiShu;
        private string _BonusDay;
        private Int32? _ISUsedEveryDay;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private bool? _IsDeleted;
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
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string HaoMa
        {
            set{ _HaoMa=value;}
            get{return _HaoMa;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string QiShu
        {
            set{ _QiShu=value;}
            get{return _QiShu;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BonusDay
        {
            set{ _BonusDay=value;}
            get{return _BonusDay;}
        }
        /// <summary>
        ///开奖状态 0 表示没有使用过 1 表示已被使用过 。2表示运行过，但是没有被抽奖数据  smallint 长度5 占用字节数2 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? ISUsedEveryDay
        {
            set{ _ISUsedEveryDay=value;}
            get{return _ISUsedEveryDay;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreateTime
        {
            set{ _CreateTime=value;}
            get{return _CreateTime;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
