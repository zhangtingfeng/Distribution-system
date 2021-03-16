using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类b080_Shop_cardMemberPic 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class b080_Shop_cardMemberPic
    {
        public b080_Shop_cardMemberPic()
        {}
        #region Model
        //ID,PicUrl,Type,ShowText,LinkURL,Arg1,Arg2,UpdateTime,Writer,UserID,CreatTime,Pos,
        private Int32 _ID;
        private string _PicUrl;
        private int? _Type;
        private string _ShowText;
        private string _LinkURL;
        private string _Arg1;
        private string _Arg2;
        private DateTime? _UpdateTime;
        private string _Writer;
        private Int32? _UserID;
        private DateTime? _CreatTime;
        private int? _Pos;
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
        ///1 表示微信会员卡的轮播图  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Type
        {
            set{ _Type=value;}
            get{return _Type;}
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
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string Arg1
        {
            set{ _Arg1=value;}
            get{return _Arg1;}
        }
        /// <summary>
        ///  nvarchar 长度-1 占用字节数-1 小数位数0 允许空 默认值无 
        /// </summary>
        public string Arg2
        {
            set{ _Arg2=value;}
            get{return _Arg2;}
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
        ///排序位置 从小拍到大  0最小 09999比较大  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Pos
        {
            set{ _Pos=value;}
            get{return _Pos;}
        }
        #endregion Model
    }
}
