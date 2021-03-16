using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_Class3 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_Class3
    {
        public tab_Class3()
        {}
        #region Model
        //ID,Class1_ID,Class2_ID,ClassName,ClassIcon,Updatetime,Sort,IsShow,IsLock,ShopClientID,CreatTime,BigPicpath,
        private int _ID;
        private int? _Class1_ID;
        private int? _Class2_ID;
        private string _ClassName;
        private string _ClassIcon;
        private DateTime? _Updatetime;
        private int? _Sort;
        private bool? _IsShow;
        private bool? _IsLock;
        private int? _ShopClientID;
        private DateTime? _CreatTime;
        private string _BigPicpath;
        /// <summary>
        ///编号 主键 int 长度10 占用字节数4 小数位数0 不允许空 默认值无 
        /// </summary>
        public int ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        ///分类1的id  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Class1_ID
        {
            set{ _Class1_ID=value;}
            get{return _Class1_ID;}
        }
        /// <summary>
        ///分类2的id  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Class2_ID
        {
            set{ _Class2_ID=value;}
            get{return _Class2_ID;}
        }
        /// <summary>
        ///分类的名字  nvarchar 长度50 占用字节数100 小数位数0 允许空 默认值无 
        /// </summary>
        public string ClassName
        {
            set{ _ClassName=value;}
            get{return _ClassName;}
        }
        /// <summary>
        ///分类的icon  nvarchar 长度80 占用字节数160 小数位数0 允许空 默认值无 
        /// </summary>
        public string ClassIcon
        {
            set{ _ClassIcon=value;}
            get{return _ClassIcon;}
        }
        /// <summary>
        ///更新时间  datetime 长度23 占用字节数8 小数位数3 允许空 默认值无 
        /// </summary>
        public DateTime? Updatetime
        {
            set{ _Updatetime=value;}
            get{return _Updatetime;}
        }
        /// <summary>
        ///排序方式  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? Sort
        {
            set{ _Sort=value;}
            get{return _Sort;}
        }
        /// <summary>
        ///是否显示  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IsShow
        {
            set{ _IsShow=value;}
            get{return _IsShow;}
        }
        /// <summary>
        ///是否锁定  bit 长度1 占用字节数1 小数位数0 允许空 默认值无 
        /// </summary>
        public bool? IsLock
        {
            set{ _IsLock=value;}
            get{return _IsLock;}
        }
        /// <summary>
        ///  int 长度10 占用字节数4 小数位数0 允许空 默认值无 
        /// </summary>
        public int? ShopClientID
        {
            set{ _ShopClientID=value;}
            get{return _ShopClientID;}
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
        ///首页显示的大图标路径  nvarchar 长度80 占用字节数160 小数位数0 允许空 默认值无 
        /// </summary>
        public string BigPicpath
        {
            set{ _BigPicpath=value;}
            get{return _BigPicpath;}
        }
        #endregion Model
    }
}
