using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_ShopClient_Agent 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_ShopClient_Agent
    {
        public View_ShopClient_Agent()
        {}
        #region Model
        //ShopClientID,ContactPhone,NickName,ShopName,Empowered,UserRealName,AlipayNumOrWeiXinPay,UpdateTime,ID,UserID,AgentLevelSelect,ParentID,ShopUserID,OnlyIsAngel,
        private Int32? _ShopClientID;
        private string _ContactPhone;
        private string _NickName;
        private string _ShopName;
        private bool? _Empowered;
        private string _UserRealName;
        private string _AlipayNumOrWeiXinPay;
        private DateTime? _UpdateTime;
        private Int32 _ID;
        private Int32? _UserID;
        private int? _AgentLevelSelect;
        private Int32? _ParentID;
        private Int32? _ShopUserID;
        private bool? _OnlyIsAngel;
        /// <summary>
        /// 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContactPhone
        {
            set{ _ContactPhone=value;}
            get{return _ContactPhone;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string NickName
        {
            set{ _NickName=value;}
            get{return _NickName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShopName
        {
            set{ _ShopName=value;}
            get{return _ShopName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? Empowered
        {
            set{ _Empowered=value;}
            get{return _Empowered;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserRealName
        {
            set{ _UserRealName=value;}
            get{return _UserRealName;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string AlipayNumOrWeiXinPay
        {
            set{ _AlipayNumOrWeiXinPay=value;}
            get{return _AlipayNumOrWeiXinPay;}
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
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
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AgentLevelSelect
        {
            set{ _AgentLevelSelect=value;}
            get{return _AgentLevelSelect;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32? ShopUserID
        {
            set{ _ShopUserID=value;}
            get{return _ShopUserID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? OnlyIsAngel
        {
            set{ _OnlyIsAngel=value;}
            get{return _OnlyIsAngel;}
        }
        #endregion Model
    }
}
