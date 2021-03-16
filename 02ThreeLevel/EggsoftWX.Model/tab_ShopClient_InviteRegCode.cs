using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_InviteRegCode 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_InviteRegCode
    {
        public tab_ShopClient_InviteRegCode()
        {}
        #region Model
        //ID,ValidDays,InvitePeople,UpdateTime,ValidCode,IfUsed,
        private Int32 _ID;
        private Int32 _ValidDays;
        private string _InvitePeople;
        private DateTime _UpdateTime;
        private string _ValidCode;
        private bool _IfUsed;
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
        public Int32 ValidDays
        {
            set{ _ValidDays=value;}
            get{return _ValidDays;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvitePeople
        {
            set{ _InvitePeople=value;}
            get{return _InvitePeople;}
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
        public string ValidCode
        {
            set{ _ValidCode=value;}
            get{return _ValidCode;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IfUsed
        {
            set{ _IfUsed=value;}
            get{return _IfUsed;}
        }
        #endregion Model
    }
}
