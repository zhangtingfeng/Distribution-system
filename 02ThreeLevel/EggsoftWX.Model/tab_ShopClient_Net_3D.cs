using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类tab_ShopClient_Net_3D 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class tab_ShopClient_Net_3D
    {
        public tab_ShopClient_Net_3D()
        {}
        #region Model
        //ID,Filename,selectIF,ShowPos,ShopClient_ID,SMALL150PX,
        private Int32 _ID;
        private string _Filename;
        private bool _selectIF;
        private Int32 _ShowPos;
        private Int32 _ShopClient_ID;
        private string _SMALL150PX;
        /// <summary>
        /// 
        /// </summary>
        public Int32 ID
        {
            set{ _ID=value;}
            get{return _ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string Filename
        {
            set{ _Filename=value;}
            get{return _Filename;}
        }
        /// <summary>
        /// 
        /// </summary>
        public bool selectIF
        {
            set{ _selectIF=value;}
            get{return _selectIF;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ShowPos
        {
            set{ _ShowPos=value;}
            get{return _ShowPos;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 ShopClient_ID
        {
            set{ _ShopClient_ID=value;}
            get{return _ShopClient_ID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string SMALL150PX
        {
            set{ _SMALL150PX=value;}
            get{return _SMALL150PX;}
        }
        #endregion Model
    }
}
