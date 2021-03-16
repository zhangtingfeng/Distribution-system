using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b012_RefundMoney 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b012_RefundMoney
    {
        public b012_RefundMoney()
        {}
        #region Model
        //ID,ShopClient_ID,orderID,returnMoney,success,RefundNumber,Description,CreateBy,UpdateTime,UpdateBy,CreatTime,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private Int32? _orderID;
        private decimal? _returnMoney;
        private bool? _success;
        private string _RefundNumber;
        private string _Description;
        private string _CreateBy;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private DateTime? _CreatTime;
        private Int32? _IsDeleted;
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
        public Int32? ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? orderID
        {
            set{ _orderID=value;}
            get{return _orderID;}
        }
        /// <summary>
        ///  decimal 长度18 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? returnMoney
        {
            set{ _returnMoney=value;}
            get{return _returnMoney;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? success
        {
            set{ _success=value;}
            get{return _success;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string RefundNumber
        {
            set{ _RefundNumber=value;}
            get{return _RefundNumber;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public string Description
        {
            set{ _Description=value;}
            get{return _Description;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string CreateBy
        {
            set{ _CreateBy=value;}
            get{return _CreateBy;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
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
        ///  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
