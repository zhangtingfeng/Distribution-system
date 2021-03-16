using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b018Help_01XianChangHuoDong_UserShake 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b018Help_01XianChangHuoDong_UserShake
    {
        public b018Help_01XianChangHuoDong_UserShake()
        {}
        #region Model
        //ID,ShopClientID,UserID,UserIDNickName,UserIDHeadURL,UserIDWeiXinHeadURL,XianChangHuoDongNum,ThisScore,ThisAllScoreShakeCount,CreateTime,UpdateTime,isDoing,isReturnedToBigScreenHeadIMG,UserIP,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private Int32? _UserID;
        private string _UserIDNickName;
        private string _UserIDHeadURL;
        private string _UserIDWeiXinHeadURL;
        private Int32? _XianChangHuoDongNum;
        private decimal? _ThisScore;
        private Int32? _ThisAllScoreShakeCount;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private bool? _isDoing;
        private bool? _isReturnedToBigScreenHeadIMG;
        private string _UserIP;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///用户 昵称  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserIDNickName
        {
            set{ _UserIDNickName=value;}
            get{return _UserIDNickName;}
        }
        /// <summary>
        ///  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserIDHeadURL
        {
            set{ _UserIDHeadURL=value;}
            get{return _UserIDHeadURL;}
        }
        /// <summary>
        ///七牛小头像 地址  nvarchar 长度200 占用字节数400 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserIDWeiXinHeadURL
        {
            set{ _UserIDWeiXinHeadURL=value;}
            get{return _UserIDWeiXinHeadURL;}
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
        ///最终的分数 .百分数 统计的 .最好的分数 算是100分  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? ThisScore
        {
            set{ _ThisScore=value;}
            get{return _ThisScore;}
        }
        /// <summary>
        ///摇的 总次数 ,可以累加的  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ThisAllScoreShakeCount
        {
            set{ _ThisAllScoreShakeCount=value;}
            get{return _ThisAllScoreShakeCount;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((1)) 
        /// </summary>
        public bool? isDoing
        {
            set{ _isDoing=value;}
            get{return _isDoing;}
        }
        /// <summary>
        ///是否 在 大屏幕上 显示了 我的 头像  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? isReturnedToBigScreenHeadIMG
        {
            set{ _isReturnedToBigScreenHeadIMG=value;}
            get{return _isReturnedToBigScreenHeadIMG;}
        }
        /// <summary>
        ///  nvarchar 长度80 占用字节数160 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserIP
        {
            set{ _UserIP=value;}
            get{return _UserIP;}
        }
        #endregion Model
    }
}
