using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_WeiXin_Stateurl 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_WeiXin_Stateurl
    {
        public tab_ShopClient_WeiXin_Stateurl()
        {}
        #region Model
        //ID,ShopClientID,UrlFrom,intFromCount,updateTime,CreatTime,
        private Int32 _ID;
        private Int32 _ShopClientID;
        private string _UrlFrom;
        private Int32? _intFromCount;
        private DateTime? _updateTime;
        private DateTime? _CreatTime;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///  nvarchar 长度400 占用字节数800 小数位数0 不允许空 默认值无 
        /// </summary>
        public string UrlFrom
        {
            set{ _UrlFrom=value;}
            get{return _UrlFrom;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? intFromCount
        {
            set{ _intFromCount=value;}
            get{return _intFromCount;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? updateTime
        {
            set{ _updateTime=value;}
            get{return _updateTime;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        #endregion Model
    }
}
