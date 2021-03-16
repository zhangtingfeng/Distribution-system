using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Agent_ 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Agent_
    {
        public tab_ShopClient_Agent_()
        {}
        #region Model
        //ID,ShopClientID,ShopTeamID,UserID,ShopName,ParentID,OnlyIsAngel,Empowered,AgentLevelSelect,TeamParentID,Vouchers_Consume_Or_Recharge,AddExp0,AddExp1,AddExp2,AddExp3,AddExp4,AddExp5,AddExp6,AddExp7,AddExp8,CreatTime,UpdateTime,UpdateBy,CreateBy,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClientID;
        private Int32? _ShopTeamID;
        private Int32? _UserID;
        private string _ShopName;
        private Int32? _ParentID;
        private bool? _OnlyIsAngel;
        private bool? _Empowered;
        private Int32? _AgentLevelSelect;
        private Int32? _TeamParentID;
        private decimal? _Vouchers_Consume_Or_Recharge;
        private string _AddExp0;
        private string _AddExp1;
        private string _AddExp2;
        private string _AddExp3;
        private string _AddExp4;
        private string _AddExp5;
        private string _AddExp6;
        private string _AddExp7;
        private string _AddExp8;
        private DateTime? _CreatTime;
        private DateTime? _UpdateTime;
        private string _UpdateBy;
        private string _CreateBy;
        private Int32? _IsDeleted;
        /// <summary>
        /// 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值((0)) 
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
        ///本人的团队ID。如果AgentLevelSelect》0  必有团队ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShopTeamID
        {
            set{ _ShopTeamID=value;}
            get{return _ShopTeamID;}
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
        ///  nvarchar 长度30 占用字节数60 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShopName
        {
            set{ _ShopName=value;}
            get{return _ShopName;}
        }
        /// <summary>
        ///父亲店的User编号  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ParentID
        {
            set{ _ParentID=value;}
            get{return _ParentID;}
        }
        /// <summary>
        ///和相关参数中是否选中天使功能有关。如果是0表示申请了代理 默认值是1 天使分销功能，对标微信小店功能，任何访问都自动给予代理权，不过只有提出代理申请的用户才能参与分销提成、团队奖励  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? OnlyIsAngel
        {
            set{ _OnlyIsAngel=value;}
            get{return _OnlyIsAngel;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public bool? Empowered
        {
            set{ _Empowered=value;}
            get{return _Empowered;}
        }
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? AgentLevelSelect
        {
            set{ _AgentLevelSelect=value;}
            get{return _AgentLevelSelect;}
        }
        /// <summary>
        ///上级团队ID，如果AgentLevelSelect》0   必有上级团队ID。TeamID就是tab_ShopClient_Agent_ 表的ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? TeamParentID
        {
            set{ _TeamParentID=value;}
            get{return _TeamParentID;}
        }
        /// <summary>
        ///  decimal 长度10 占用字节数9 小数位数2 允许空 默认值无 
        /// </summary>
        public decimal? Vouchers_Consume_Or_Recharge
        {
            set{ _Vouchers_Consume_Or_Recharge=value;}
            get{return _Vouchers_Consume_Or_Recharge;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp0
        {
            set{ _AddExp0=value;}
            get{return _AddExp0;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp1
        {
            set{ _AddExp1=value;}
            get{return _AddExp1;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp2
        {
            set{ _AddExp2=value;}
            get{return _AddExp2;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp3
        {
            set{ _AddExp3=value;}
            get{return _AddExp3;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp4
        {
            set{ _AddExp4=value;}
            get{return _AddExp4;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp5
        {
            set{ _AddExp5=value;}
            get{return _AddExp5;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp6
        {
            set{ _AddExp6=value;}
            get{return _AddExp6;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp7
        {
            set{ _AddExp7=value;}
            get{return _AddExp7;}
        }
        /// <summary>
        ///  nvarchar 长度255 占用字节数510 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExp8
        {
            set{ _AddExp8=value;}
            get{return _AddExp8;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string CreateBy
        {
            set{ _CreateBy=value;}
            get{return _CreateBy;}
        }
        /// <summary>
        ///是否删除。如果当前 userid重新启用 则恢复这个参数  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
