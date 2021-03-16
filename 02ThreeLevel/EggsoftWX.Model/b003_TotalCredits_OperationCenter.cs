using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b003_TotalCredits_OperationCenter 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b003_TotalCredits_OperationCenter
    {
        public b003_TotalCredits_OperationCenter()
        {}
        #region Model
        //ID,UserID,ShopClient_ID,UpdateTime,ConsumeOrRechargeMoney,ConsumeTypeOrRecharge,RemainingSum,Bool_ConsumeOrRecharge,BoolIfOnlyonceUpdate,CreatTime,Creatby,
        private Int32 _ID;
        private Int32? _UserID;
        private Int32? _ShopClient_ID;
        private DateTime? _UpdateTime;
        private decimal? _ConsumeOrRechargeMoney;
        private string _ConsumeTypeOrRecharge;
        private decimal? _RemainingSum;
        private bool? _Bool_ConsumeOrRecharge;
        private bool? _BoolIfOnlyonceUpdate;
        private DateTime? _CreatTime;
        private string _Creatby;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
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
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? ConsumeOrRechargeMoney
        {
            set{ _ConsumeOrRechargeMoney=value;}
            get{return _ConsumeOrRechargeMoney;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string ConsumeTypeOrRecharge
        {
            set{ _ConsumeTypeOrRecharge=value;}
            get{return _ConsumeTypeOrRecharge;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? RemainingSum
        {
            set{ _RemainingSum=value;}
            get{return _RemainingSum;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Bool_ConsumeOrRecharge
        {
            set{ _Bool_ConsumeOrRecharge=value;}
            get{return _Bool_ConsumeOrRecharge;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? BoolIfOnlyonceUpdate
        {
            set{ _BoolIfOnlyonceUpdate=value;}
            get{return _BoolIfOnlyonceUpdate;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Creatby
        {
            set{ _Creatby=value;}
            get{return _Creatby;}
        }
        #endregion Model
    }
}
