using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_WeiKanJia_Master_Helper 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_WeiKanJia_Master_Helper
    {
        public tab_WeiKanJia_Master_Helper()
        {}
        #region Model
        //ID,WeiKanJiaMasterID,WeiKanJiaMasterUserID,HelperUserID,InsertTime,UpdateTime,MyHelperPrice,ISDelete,ShopClientID,CreatTime,AfterMyHelperPrice,
        private Int32 _ID;
        private Int32? _WeiKanJiaMasterID;
        private Int32? _WeiKanJiaMasterUserID;
        private Int32? _HelperUserID;
        private DateTime? _InsertTime;
        private DateTime? _UpdateTime;
        private decimal? _MyHelperPrice;
        private bool? _ISDelete;
        private Int32? _ShopClientID;
        private DateTime? _CreatTime;
        private decimal? _AfterMyHelperPrice;
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
        public Int32? WeiKanJiaMasterID
        {
            set{ _WeiKanJiaMasterID=value;}
            get{return _WeiKanJiaMasterID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? WeiKanJiaMasterUserID
        {
            set{ _WeiKanJiaMasterUserID=value;}
            get{return _WeiKanJiaMasterUserID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? HelperUserID
        {
            set{ _HelperUserID=value;}
            get{return _HelperUserID;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? InsertTime
        {
            set{ _InsertTime=value;}
            get{return _InsertTime;}
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
        ///我帮砍的价格  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? MyHelperPrice
        {
            set{ _MyHelperPrice=value;}
            get{return _MyHelperPrice;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ISDelete
        {
            set{ _ISDelete=value;}
            get{return _ISDelete;}
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
        /// 创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        /// <summary>
        ///我帮砍成功以后的商品价格  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? AfterMyHelperPrice
        {
            set{ _AfterMyHelperPrice=value;}
            get{return _AfterMyHelperPrice;}
        }
        #endregion Model
    }
}
