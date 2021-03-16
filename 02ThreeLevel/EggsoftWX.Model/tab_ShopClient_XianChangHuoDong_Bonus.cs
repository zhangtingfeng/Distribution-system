using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_XianChangHuoDong_Bonus 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_XianChangHuoDong_Bonus
    {
        public tab_ShopClient_XianChangHuoDong_Bonus()
        {}
        #region Model
        //ID,XianChangHuoDongID,XianChangHuoDongBonusNumberbyShopClientID,BonusName,ShopClientID,Sort,ISDoing,CreateTime,UpdateTime,
        private Int32 _ID;
        private Int32? _XianChangHuoDongID;
        private Int32? _XianChangHuoDongBonusNumberbyShopClientID;
        private string _BonusName;
        private Int32? _ShopClientID;
        private Int32? _Sort;
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
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongID
        {
            set{ _XianChangHuoDongID=value;}
            get{return _XianChangHuoDongID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? XianChangHuoDongBonusNumberbyShopClientID
        {
            set{ _XianChangHuoDongBonusNumberbyShopClientID=value;}
            get{return _XianChangHuoDongBonusNumberbyShopClientID;}
        }
        /// <summary>
        ///特等奖 一等奖 二等奖 三等奖   nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string BonusName
        {
            set{ _BonusName=value;}
            get{return _BonusName;}
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
        ///排序位置  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///是否正在进行 本次抽奖  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
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
