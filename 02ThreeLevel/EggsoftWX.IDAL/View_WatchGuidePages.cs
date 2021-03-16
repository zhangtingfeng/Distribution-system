using System;
using System.Data;
using System.Collections.Generic;
namespace EggsoftWX.IDAL
{
	/// <summary>
	/// 接口层IView_WatchGuidePages 的摘要说明。
	/// </summary>
	public interface IView_WatchGuidePages
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId(); //memo 0001
		/// <summary>
		/// 增加一条数据 model
		/// </summary>
		bool Exists(string strWhere, params object[] objs); //memo 0004
		/// <summary>
		/// 是否存在该记录 ID
		/// </summary>
		bool Exists(Int32 ID); //memo 0005
		/// <summary>
		/// 存在该记录条数
		/// </summary>
		int ExistsCount(string strWhere, params object[] objs); //memo 0006
		/// <summary>
		/// 获得数据列表SelectList string strSelect
		/// </summary>
		DataSet SelectList(string strSelect, params object[] objs); //memo 0007
		/// <summary>
		/// 获得数据列表GetList string strWhere
		/// </summary>
		DataSet GetList(string strWhere, params object[] objs); //memo 0008
            /// <summary>
		/// 获得数据列表GetList2 string strItem,string strWhere
		/// </summary>
        DataSet GetList(string strItem, string strWhere, params object[] objs);//memo 0009
        /// <summary>
        /// 得到一GetFieldValues
        /// </summary>
        IList<string> GetFieldValues(string fields, string strWhere, params object[] objs);// memo 0010
        /// <summary>
        /// 得到一个GetFieldValues
        /// </summary>
        IList<string> GetFieldValues(string topNum, string fields, string strWhere, params object[] objs);//memo 0011
        /// <summary>
        /// 得到一个对象实体  Scalar
        /// </summary>
        object Scalar(string field, string strWhere, params object[] objs);//memo 0012
        
        /// <summary>
        /// 得到一个对象实体  GetModel
        /// </summary>
        EggsoftWX.Model.View_WatchGuidePages GetModel(string strWhere, params object[] objs);//memo 0013
		/// <summary>
		/// 得到一个对象实体 strWhere
		/// </summary>
		EggsoftWX.Model.View_WatchGuidePages GetModel(Int32 ID); //memo 0014
        DataTable GetList(string topNum, string fields, string strWhere, params object[] objs);//memo 0019
        
        //分页列表
        DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string conditions, string orderField, bool isDesc, params object[] objs);//memo 0020
		#endregion  成员方法
	}
}
