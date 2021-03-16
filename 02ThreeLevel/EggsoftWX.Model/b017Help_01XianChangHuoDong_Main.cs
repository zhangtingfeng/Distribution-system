using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b017Help_01XianChangHuoDong_Main 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b017Help_01XianChangHuoDong_Main
    {
        public b017Help_01XianChangHuoDong_Main()
        {}
        #region Model
        //ID,ShopClientID,MaxTracks,XianChangHuoDongNum,XianChangHuoDongStatus,XianChangHuoDongNum_BeginTime,XianChangHuoDongNum_LongTime,XianChangHuoDongNum_EndTime,XianChangHuoDongNum_CountHowMany,CreateTime,UpdateTime,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private int? _MaxTracks;
        private Int32? _XianChangHuoDongNum;
        private Int32? _XianChangHuoDongStatus;
        private DateTime? _XianChangHuoDongNum_BeginTime;
        private Int32? _XianChangHuoDongNum_LongTime;
        private DateTime? _XianChangHuoDongNum_EndTime;
        private Int32? _XianChangHuoDongNum_CountHowMany;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///商家序列号  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
        }
        /// <summary>
        ///现场活动序号  每个商家 的 相应递增  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? MaxTracks
        {
            set{ _MaxTracks=value;}
            get{return _MaxTracks;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongNum
        {
            set{ _XianChangHuoDongNum=value;}
            get{return _XianChangHuoDongNum;}
        }
        /// <summary>
        ///当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongStatus
        {
            set{ _XianChangHuoDongStatus=value;}
            get{return _XianChangHuoDongStatus;}
        }
        /// <summary>
        ///本次活动  开始时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? XianChangHuoDongNum_BeginTime
        {
            set{ _XianChangHuoDongNum_BeginTime=value;}
            get{return _XianChangHuoDongNum_BeginTime;}
        }
        /// <summary>
        ///活动的时间 长  主要是不能相信客户端的数据  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongNum_LongTime
        {
            set{ _XianChangHuoDongNum_LongTime=value;}
            get{return _XianChangHuoDongNum_LongTime;}
        }
        /// <summary>
        ///本次活动  结束时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? XianChangHuoDongNum_EndTime
        {
            set{ _XianChangHuoDongNum_EndTime=value;}
            get{return _XianChangHuoDongNum_EndTime;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongNum_CountHowMany
        {
            set{ _XianChangHuoDongNum_CountHowMany=value;}
            get{return _XianChangHuoDongNum_CountHowMany;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
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
