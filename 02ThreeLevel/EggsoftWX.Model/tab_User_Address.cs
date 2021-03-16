using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_User_Address 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_User_Address
    {
        public tab_User_Address()
        {}
        #region Model
        //ID,XiangXiDiZHi,PostCode,RealName,MobilePhone,TelPhone,BeiZhu,UserID,UpdateTime,IsDeleted,pc_province,pc_city,pc_district,pc_street,pc_state,CreatTime,
        private Int32 _ID;
        private string _XiangXiDiZHi;
        private string _PostCode;
        private string _RealName;
        private string _MobilePhone;
        private string _TelPhone;
        private string _BeiZhu;
        private Int32? _UserID;
        private DateTime? _UpdateTime;
        private bool? _IsDeleted;
        private string _pc_province;
        private string _pc_city;
        private string _pc_district;
        private string _pc_street;
        private string _pc_state;
        private DateTime? _CreatTime;
        /// <summary>
        ///编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///详细地址  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string XiangXiDiZHi
        {
            set{ _XiangXiDiZHi=value;}
            get{return _XiangXiDiZHi;}
        }
        /// <summary>
        ///邮编  nvarchar 长度6 占用字节数12 小数位数0 允许空 默认值无 
        /// </summary>
        public string PostCode
        {
            set{ _PostCode=value;}
            get{return _PostCode;}
        }
        /// <summary>
        ///真实姓名  nvarchar 长度20 占用字节数40 小数位数0 允许空 默认值无 
        /// </summary>
        public string RealName
        {
            set{ _RealName=value;}
            get{return _RealName;}
        }
        /// <summary>
        ///联系电话  nvarchar 长度20 占用字节数40 小数位数0 允许空 默认值无 
        /// </summary>
        public string MobilePhone
        {
            set{ _MobilePhone=value;}
            get{return _MobilePhone;}
        }
        /// <summary>
        ///联系电话  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string TelPhone
        {
            set{ _TelPhone=value;}
            get{return _TelPhone;}
        }
        /// <summary>
        ///备注  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string BeiZhu
        {
            set{ _BeiZhu=value;}
            get{return _BeiZhu;}
        }
        /// <summary>
        ///用户的id  外键  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? UpdateTime
        {
            set{ _UpdateTime=value;}
            get{return _UpdateTime;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string pc_province
        {
            set{ _pc_province=value;}
            get{return _pc_province;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string pc_city
        {
            set{ _pc_city=value;}
            get{return _pc_city;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string pc_district
        {
            set{ _pc_district=value;}
            get{return _pc_district;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string pc_street
        {
            set{ _pc_street=value;}
            get{return _pc_street;}
        }
        /// <summary>
        ///  nvarchar 长度1 占用字节数2 小数位数0 允许空 默认值无 
        /// </summary>
        public string pc_state
        {
            set{ _pc_state=value;}
            get{return _pc_state;}
        }
        /// <summary>
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        #endregion Model
    }
}
