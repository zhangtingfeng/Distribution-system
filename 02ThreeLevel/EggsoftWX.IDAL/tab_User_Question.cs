using System;
using System.Data;
using System.Collections.Generic;
namespace EggsoftWX.IDAL
{
	/// <summary>
	/// 接口层Itab_User_Question 的摘要说明。
	/// </summary>
	public interface Itab_User_Question
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		Int32 GetMaxId(); //memo 0001
		/// <summary>
		/// 增加一条数据 model
		/// </summary>
		Int32 Add(EggsoftWX.Model.tab_User_Question model); //memo 0002
		/// <summary>
		/// 增加一条数据 自定义 string strSet,string strValue
		/// </summary>
		Int32 Add(string strSet,string strValue); //memo 0003
		/// <summary>
		/// 是否存在该记录 strWhere
		/// </summary>
		bool Exists(string strWhere); //memo 0004
		/// <summary>
		/// 是否存在该记录 ID
		/// </summary>
		bool Exists(Int32 ID); //memo 0005
		/// <summary>
		/// 存在该记录条数
		/// </summary>
		Int32 ExistsCount(string strWhere); //memo 0006
		/// <summary>
		/// 获得数据列表SelectList string strSelect
		/// </summary>
		DataSet SelectList(string strSelect); //memo 0007
		/// <summary>
		/// 获得数据列表GetList string strWhere
		/// </summary>
		DataSet GetList(string strWhere); //memo 0008
            /// <summary>
		/// 获得数据列表GetList2 string strItem,string strWhere
		/// </summary>
        DataSet GetList(string strItem, string strWhere);//memo 0009
        /// <summary>
        /// 得到一GetFieldValues
        /// </summary>
        IList<string> GetFieldValues(string fields, string strWhere);// memo 0010
        /// <summary>
        /// 得到一个GetFieldValues
        /// </summary>
        IList<string> GetFieldValues(string topNum, string fields, string strWhere);//memo 0011
        /// <summary>
        /// 得到一个对象实体  Scalar
        /// </summary>
        object Scalar(string field, string strWhere);//memo 0012
        
        /// <summary>
        /// 得到一个对象实体  GetModel
        /// </summary>
        EggsoftWX.Model.tab_User_Question GetModel(string strWhere);//memo 0013
		/// <summary>
		/// 得到一个对象实体 strWhere
		/// </summary>
		EggsoftWX.Model.tab_User_Question GetModel(Int32 ID); //memo 0014
		/// <summary>
		/// delete strWhere 删除n条数据
		/// </summary>
		void Delete(string strWhere);//memo 0015
		/// <summary>
		/// region delete ID 删除一条数据
		/// </summary>
		void Delete(Int32 ID);//memo 0016
		/// <summary>
		/// update strSet strWhere 更新n条数据
		/// </summary>
		void Update(string strSet,string strWhere);//memo 0017
		/// <summary>
		///model update 更新一条数据
		/// </summary>
		void Update(EggsoftWX.Model.tab_User_Question model);//memo 0018
        DataTable GetList(string topNum, string fields, string strWhere);//memo 0019
        
        //分页列表
        DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string conditions, string orderField, bool isDesc);//memo 0020
		#endregion  成员方法
	}
}
