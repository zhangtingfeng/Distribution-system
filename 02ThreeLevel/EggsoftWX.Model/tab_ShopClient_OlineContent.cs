using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_OlineContent 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_OlineContent
    {
        public tab_ShopClient_OlineContent()
        {}
        #region Model
        //ID,ShopClient_ID,Oline_Content,Title,XML,AddExpListTextShow,AddExpListText,CreateTime,Createby,UpdateTime,Updateby,IsDeleted,
        private Int32 _ID;
        private Int32? _ShopClient_ID;
        private string _Oline_Content;
        private string _Title;
        private string _XML;
        private bool? _AddExpListTextShow;
        private string _AddExpListText;
        private DateTime? _CreateTime;
        private string _Createby;
        private DateTime? _UpdateTime;
        private string _Updateby;
        private int? _IsDeleted;
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
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string Oline_Content
        {
            set{ _Oline_Content=value;}
            get{return _Oline_Content;}
        }
        /// <summary>
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Title
        {
            set{ _Title=value;}
            get{return _Title;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string XML
        {
            set{ _XML=value;}
            get{return _XML;}
        }
        /// <summary>
        ///  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? AddExpListTextShow
        {
            set{ _AddExpListTextShow=value;}
            get{return _AddExpListTextShow;}
        }
        /// <summary>
        ///  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string AddExpListText
        {
            set{ _AddExpListText=value;}
            get{return _AddExpListText;}
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
        ///  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Createby
        {
            set{ _Createby=value;}
            get{return _Createby;}
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
        public string Updateby
        {
            set{ _Updateby=value;}
            get{return _Updateby;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值((0)) 
        /// </summary>
        public int? IsDeleted
        {
            set{ _IsDeleted=value;}
            get{return _IsDeleted;}
        }
        #endregion Model
    }
}
