using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_XianChangHuoDong_Number 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_XianChangHuoDong_Number
    {
        public tab_ShopClient_XianChangHuoDong_Number()
        {}
        #region Model
        //ID,XianChangHuoDongID,XianChangHuoDongNumberbyShopClientID,ShopClientID,IsDoing,BeginTime,EndTime,CreateTime,UpdateTime,
        private Int32 _ID;
        private Int32? _XianChangHuoDongID;
        private Int32? _XianChangHuoDongNumberbyShopClientID;
        private Int32? _ShopClientID;
        private Int32? _IsDoing;
        private DateTime? _BeginTime;
        private DateTime? _EndTime;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        /// <summary>
        ///当前现场活动  Int32 长度10 占用字节数4 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///现场活动的 序列号  外键  映射用  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongID
        {
            set{ _XianChangHuoDongID=value;}
            get{return _XianChangHuoDongID;}
        }
        /// <summary>
        ///当前现场活动编号,一个客户一个递增  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongNumberbyShopClientID
        {
            set{ _XianChangHuoDongNumberbyShopClientID=value;}
            get{return _XianChangHuoDongNumberbyShopClientID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///是否 正在进行  1  表示 是当前的 0 表示已关闭 。  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? IsDoing
        {
            set{ _IsDoing=value;}
            get{return _IsDoing;}
        }
        /// <summary>
        ///当次开始时间    datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? BeginTime
        {
            set{ _BeginTime=value;}
            get{return _BeginTime;}
        }
        /// <summary>
        ///当此结束时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? EndTime
        {
            set{ _EndTime=value;}
            get{return _EndTime;}
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
        #endregion Model
    }
}
