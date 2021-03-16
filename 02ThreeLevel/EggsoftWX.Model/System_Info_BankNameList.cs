using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类System_Info_BankNameList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class System_Info_BankNameList
    {
        public System_Info_BankNameList()
        {}
        #region Model
        //BankNameList,ID,
        private string _BankNameList;
        private Int32 _ID;
        /// <summary>
        /// 
        /// </summary>
        public string BankNameList
        {
            set{ _BankNameList=value;}
            get{return _BankNameList;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        #endregion Model
    }
}
