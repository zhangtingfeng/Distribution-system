using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_TuanGou_Number 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_TuanGou_Number
    {
        public tab_TuanGou_Number()
        {}
        #region Model
        //ID,ShopClientID,BuyPrice,TuanGouID,IFFinshedCurMemberShip,IsDelete,CreateTime,UpdateTime,Efficacy,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private decimal? _BuyPrice;
        private Int32? _TuanGouID;
        private bool? _IFFinshedCurMemberShip;
        private bool? _IsDelete;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private bool? _Efficacy;
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
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
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? BuyPrice
        {
            set{ _BuyPrice=value;}
            get{return _BuyPrice;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? TuanGouID
        {
            set{ _TuanGouID=value;}
            get{return _TuanGouID;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IFFinshedCurMemberShip
        {
            set{ _IFFinshedCurMemberShip=value;}
            get{return _IFFinshedCurMemberShip;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDelete
        {
            set{ _IsDelete=value;}
            get{return _IsDelete;}
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
        /// <summary>
        ///是否失效   超时  等原因关闭  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Efficacy
        {
            set{ _Efficacy=value;}
            get{return _Efficacy;}
        }
        #endregion Model
    }
}
