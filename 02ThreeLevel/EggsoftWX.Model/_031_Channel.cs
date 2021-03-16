using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类_031_Channel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class _031_Channel
    {
        public _031_Channel()
        {}
        #region Model
        //ID,ChannelName,ChannelMemo,ShopClient_ID,UpdateTime,CreatTime,Creatby,UpdateBy,IsDeleted,
        private Int32 _ID;
        private string _ChannelName;
        private string _ChannelMemo;
        private Int32? _ShopClient_ID;
        private DateTime? _UpdateTime;
        private DateTime? _CreatTime;
        private string _Creatby;
        private string _UpdateBy;
        private int? _IsDeleted;
        /// <summary>
        ///  bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ChannelName
        {
            set{ _ChannelName=value;}
            get{return _ChannelName;}
        }
        /// <summary>
        ///  nvarchar 长度400 占用字节数800 小数位数0 允许空 默认值无 
        /// </summary>
        public string ChannelMemo
        {
            set{ _ChannelMemo=value;}
            get{return _ChannelMemo;}
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
        /// <summary>
        ///  nchar 长度10 占用字节数20 小数位数0 允许空 默认值无 
        /// </summary>
        public string UpdateBy
        {
            set{ _UpdateBy=value;}
            get{return _UpdateBy;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
