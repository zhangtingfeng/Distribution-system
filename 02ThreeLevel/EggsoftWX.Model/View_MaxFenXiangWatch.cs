using System;
namespace EggsoftWX.Model
{
    /// <summary>
    /// 实体类View_MaxFenXiangWatch 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class View_MaxFenXiangWatch
    {
        public View_MaxFenXiangWatch()
        {}
        #region Model
        //GoodID,SumCount,
        private Int32 _GoodID;
        private Int32 _SumCount;
        /// <summary>
        /// 
        /// </summary>
        public Int32 GoodID
        {
            set{ _GoodID=value;}
            get{return _GoodID;}
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 SumCount
        {
            set{ _SumCount=value;}
            get{return _SumCount;}
        }
        #endregion Model
    }
}
