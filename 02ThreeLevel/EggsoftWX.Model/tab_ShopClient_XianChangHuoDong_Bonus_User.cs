using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_XianChangHuoDong_Bonus_User 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_XianChangHuoDong_Bonus_User
    {
        public tab_ShopClient_XianChangHuoDong_Bonus_User()
        {}
        #region Model
        //ID,XianChangHuoDongBonusNumberbyShopClientID,XianChangHuoDongID,ShopClientID,GetBonusName,UserID,ISDoing,CreateTime,UpdateTime,
        private Int32 _ID;
        private Int32? _XianChangHuoDongBonusNumberbyShopClientID;
        private Int32? _XianChangHuoDongID;
        private Int32? _ShopClientID;
        private string _GetBonusName;
        private Int32? _UserID;
        private bool? _ISDoing;
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
        public Int32? XianChangHuoDongBonusNumberbyShopClientID
        {
            set{ _XianChangHuoDongBonusNumberbyShopClientID=value;}
            get{return _XianChangHuoDongBonusNumberbyShopClientID;}
        }
        /// <summary>
        ///现场活动的 编号 ID  外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongID
        {
            set{ _XianChangHuoDongID=value;}
            get{return _XianChangHuoDongID;}
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
        ///是 几等奖  奖励 自动发货  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string GetBonusName
        {
            set{ _GetBonusName=value;}
            get{return _GetBonusName;}
        }
        /// <summary>
        /// 通过 这个 查找 收获 地址 联系 方式  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///是否正在进行 抽奖   有利于 统计 使用 作用  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ISDoing
        {
            set{ _ISDoing=value;}
            get{return _ISDoing;}
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
