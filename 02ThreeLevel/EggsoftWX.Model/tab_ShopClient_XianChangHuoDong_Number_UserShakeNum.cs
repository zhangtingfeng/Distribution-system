using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_XianChangHuoDong_Number_UserShakeNum 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_XianChangHuoDong_Number_UserShakeNum
    {
        public tab_ShopClient_XianChangHuoDong_Number_UserShakeNum()
        {}
        #region Model
        //ID,XianChangHuoDongNumberbyShopClientID,UserID,UserShopClientID,UserNickName,UserShakeNumber,CreateTime,UpdateTime,
        private Int32 _ID;
        private Int32? _XianChangHuoDongNumberbyShopClientID;
        private Int32? _UserID;
        private Int32? _UserShopClientID;
        private string _UserNickName;
        private Int32? _UserShakeNumber;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        /// <summary>
        ///  Int32 长度10 占用字节数4 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///现场活动的 编号 ID  外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongNumberbyShopClientID
        {
            set{ _XianChangHuoDongNumberbyShopClientID=value;}
            get{return _XianChangHuoDongNumberbyShopClientID;}
        }
        /// <summary>
        ///参与的Userid  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserShopClientID
        {
            set{ _UserShopClientID=value;}
            get{return _UserShopClientID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UserNickName
        {
            set{ _UserNickName=value;}
            get{return _UserNickName;}
        }
        /// <summary>
        ///用户摇动次数  最终统计使用  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserShakeNumber
        {
            set{ _UserShakeNumber=value;}
            get{return _UserShakeNumber;}
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
