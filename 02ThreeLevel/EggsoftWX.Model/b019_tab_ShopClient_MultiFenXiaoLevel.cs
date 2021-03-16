using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b019_tab_ShopClient_MultiFenXiaoLevel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b019_tab_ShopClient_MultiFenXiaoLevel
    {
        public b019_tab_ShopClient_MultiFenXiaoLevel()
        {}
        #region Model
        //ID,ShopClient_ID,Name,Sort,FenxiaoParentGet,FenxiaoGrandParentGet,FenxiaoGreatParentGet,ChildGet,GrandsonGet,GreatsonGet,ChildGet_Money,Grandson_Money,GreatsonGet_Money,OperationGet,OperationParentGet,OperationGrandParentGet,CreateBy,CreatTime,UpdateBy,UpdateTime,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private string _Name;
        private Int32? _Sort;
        private decimal? _FenxiaoParentGet;
        private decimal? _FenxiaoGrandParentGet;
        private decimal? _FenxiaoGreatParentGet;
        private decimal? _ChildGet;
        private decimal? _GrandsonGet;
        private decimal? _GreatsonGet;
        private bool? _ChildGet_Money;
        private bool? _Grandson_Money;
        private bool? _GreatsonGet_Money;
        private decimal? _OperationGet;
        private decimal? _OperationParentGet;
        private decimal? _OperationGrandParentGet;
        private string _CreateBy;
        private DateTime? _CreatTime;
        private string _UpdateBy;
        private DateTime? _UpdateTime;
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Name
        {
            set{ _Name=value;}
            get{return _Name;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///当前代理所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? FenxiaoParentGet
        {
            set{ _FenxiaoParentGet=value;}
            get{return _FenxiaoParentGet;}
        }
        /// <summary>
        ///上级代理所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? FenxiaoGrandParentGet
        {
            set{ _FenxiaoGrandParentGet=value;}
            get{return _FenxiaoGrandParentGet;}
        }
        /// <summary>
        ///上上上级代理所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? FenxiaoGreatParentGet
        {
            set{ _FenxiaoGreatParentGet=value;}
            get{return _FenxiaoGreatParentGet;}
        }
        /// <summary>
        ///直系下级所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? ChildGet
        {
            set{ _ChildGet=value;}
            get{return _ChildGet;}
        }
        /// <summary>
        ///孙子所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? GrandsonGet
        {
            set{ _GrandsonGet=value;}
            get{return _GrandsonGet;}
        }
        /// <summary>
        ///重孙子们所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? GreatsonGet
        {
            set{ _GreatsonGet=value;}
            get{return _GreatsonGet;}
        }
        /// <summary>
        ///(是否加权平均现金)强制现金积分方式或者据用户角色决定购物积分方式  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ChildGet_Money
        {
            set{ _ChildGet_Money=value;}
            get{return _ChildGet_Money;}
        }
        /// <summary>
        ///(是否加权平均现金)强制现金积分方式或者据用户角色决定购物积分方式  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Grandson_Money
        {
            set{ _Grandson_Money=value;}
            get{return _Grandson_Money;}
        }
        /// <summary>
        ///(是否加权平均现金)强制现金积分方式或者据用户角色决定购物积分方式  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? GreatsonGet_Money
        {
            set{ _GreatsonGet_Money=value;}
            get{return _GreatsonGet_Money;}
        }
        /// <summary>
        ///当前运营中心所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? OperationGet
        {
            set{ _OperationGet=value;}
            get{return _OperationGet;}
        }
        /// <summary>
        ///能得到下级级运营中心所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? OperationParentGet
        {
            set{ _OperationParentGet=value;}
            get{return _OperationParentGet;}
        }
        /// <summary>
        ///能得到下下级运营中心所得  decimal 长度18 占用字节数9 小数位数2 允许空 默认值((0)) 
        /// </summary>
        public decimal? OperationGrandParentGet
        {
            set{ _OperationGrandParentGet=value;}
            get{return _OperationGrandParentGet;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
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
