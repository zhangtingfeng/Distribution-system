using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_AnnouncePic 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_AnnouncePic
    {
        public tab_AnnouncePic()
        {}
        #region Model
        //ID,PicUrl,ShowText,LinkURL,UpdateTime,Writer,UserID,CreatTime,Pos,
        private Int32 _ID;
        private string _PicUrl;
        private string _ShowText;
        private string _LinkURL;
        private DateTime? _UpdateTime;
        private string _Writer;
        private Int32? _UserID;
        private DateTime? _CreatTime;
        private Int32? _Pos;
        /// <summary>
        ///编号 主键 bigint 长度19 占用字节数8 小数位数0 不允许空 默认值无 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///上传的图片路径  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string PicUrl
        {
            set{ _PicUrl=value;}
            get{return _PicUrl;}
        }
        /// <summary>
        ///广告上显示的文本  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ShowText
        {
            set{ _ShowText=value;}
            get{return _ShowText;}
        }
        /// <summary>
        ///点击图片跳转的路径  nvarchar 长度250 占用字节数500 小数位数0 允许空 默认值无 
        /// </summary>
        public string LinkURL
        {
            set{ _LinkURL=value;}
            get{return _LinkURL;}
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
        ///是谁更新的  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string Writer
        {
            set{ _Writer=value;}
            get{return _Writer;}
        }
        /// <summary>
        ///商户的关联的ID  bigint 长度19 占用字节数8 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? UserID
        {
            set{ _UserID=value;}
            get{return _UserID;}
        }
        /// <summary>
        ///创建时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值(getdate()) 
        /// </summary>
        public DateTime? CreatTime
        {
            set{ _CreatTime=value;}
            get{return _CreatTime;}
        }
        /// <summary>
        ///排序位置 从小拍到大  0最小 09999比较大  Int32 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public Int32? Pos
        {
            set{ _Pos=value;}
            get{return _Pos;}
        }
        #endregion Model
    }
}
