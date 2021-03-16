using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_XianChangHuoDong 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_XianChangHuoDong
    {
        public tab_ShopClient_XianChangHuoDong()
        {}
        #region Model
        //ID,ActivityName,PassWord,ShopClientID,ShowAgentErWeiMa_UserID_ByAgent,Subscribe_Must,ActivityState,GetBonusRepeat,GetBonusRepeat_OneDrawBonus,Address_Must,Sort,IsDeleted,CreateTime,UpdateTime,Background_PIC_BigScreen,Background_SoundPath,LongShakeTime,CountHowMany,MaxTracks,
        private Int32 _ID;
        private string _ActivityName;
        private string _PassWord;
        private Int32? _ShopClientID;
        private Int32? _ShowAgentErWeiMa_UserID_ByAgent;
        private bool? _Subscribe_Must;
        private bool? _ActivityState;
        private bool? _GetBonusRepeat;
        private bool? _GetBonusRepeat_OneDrawBonus;
        private bool? _Address_Must;
        private Int32? _Sort;
        private bool? _IsDeleted;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;
        private string _Background_PIC_BigScreen;
        private string _Background_SoundPath;
        private Int32? _LongShakeTime;
        private Int32? _CountHowMany;
        private Int32? _MaxTracks;
        /// <summary>
        /// 主键 Int32 长度10 占用字节数4 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///现场活动主题  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string ActivityName
        {
            set{ _ActivityName=value;}
            get{return _ActivityName;}
        }
        /// <summary>
        ///微现场密码  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string PassWord
        {
            set{ _PassWord=value;}
            get{return _PassWord;}
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
        ///显示 哪一个代理的 二维码。0  表示 显示 公众平台的二维码  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? ShowAgentErWeiMa_UserID_ByAgent
        {
            set{ _ShowAgentErWeiMa_UserID_ByAgent=value;}
            get{return _ShowAgentErWeiMa_UserID_ByAgent;}
        }
        /// <summary>
        ///是否必须关注 才能参与现场活动  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Subscribe_Must
        {
            set{ _Subscribe_Must=value;}
            get{return _Subscribe_Must;}
        }
        /// <summary>
        ///生效状态，只能有一个是 有效状态  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? ActivityState
        {
            set{ _ActivityState=value;}
            get{return _ActivityState;}
        }
        /// <summary>
        ///用户是否能重复抽奖，在一次现场活动  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? GetBonusRepeat
        {
            set{ _GetBonusRepeat=value;}
            get{return _GetBonusRepeat;}
        }
        /// <summary>
        ///用户是否能重复抽奖，一次抽奖过程中  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? GetBonusRepeat_OneDrawBonus
        {
            set{ _GetBonusRepeat_OneDrawBonus=value;}
            get{return _GetBonusRepeat_OneDrawBonus;}
        }
        /// <summary>
        ///必须输入 收获地址  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? Address_Must
        {
            set{ _Address_Must=value;}
            get{return _Address_Must;}
        }
        /// <summary>
        ///排序位置 升序排列  Int32 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public Int32? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
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
        ///  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreateTime
        {
            set{ _CreateTime=value;}
            get{return _CreateTime;}
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
        ///大屏幕背景图片,建议尺寸大小和演示大屏幕的分辨率一致  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string Background_PIC_BigScreen
        {
            set{ _Background_PIC_BigScreen=value;}
            get{return _Background_PIC_BigScreen;}
        }
        /// <summary>
        ///大屏幕背景音乐  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string Background_SoundPath
        {
            set{ _Background_SoundPath=value;}
            get{return _Background_SoundPath;}
        }
        /// <summary>
        ///S(秒)(价值高的奖品可设置时间长一点)  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? LongShakeTime
        {
            set{ _LongShakeTime=value;}
            get{return _LongShakeTime;}
        }
        /// <summary>
        ///次数(摇动多少次中奖,第一个用户摇动就全部停止,建议价值高的产品可适当设置更多的时间)  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? CountHowMany
        {
            set{ _CountHowMany=value;}
            get{return _CountHowMany;}
        }
        /// <summary>
        ///大屏幕显示的轨道数目  3-20  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? MaxTracks
        {
            set{ _MaxTracks=value;}
            get{return _MaxTracks;}
        }
        #endregion Model
    }
}
