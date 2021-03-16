using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using EggsoftWX.IDAL;
namespace EggsoftWX.BLL
{
	/// <summary>
	/// 业务逻辑类tab_ShopClient 的摘要说明。
	/// </summary>
	public class tab_ShopClient
	{
		Itab_ShopClient dal=EggsoftWX.DALFactory.tab_ShopClient.Create();
		public tab_ShopClient()
		{}
		#region  成员方法

		/// <summary>
		/// 1 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 2 增加一条数据
		/// </summary>
		public Int32 Add(EggsoftWX.Model.tab_ShopClient model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 3 增加一条数据 自定义 string strSet,string strValue
		/// </summary>
		public Int32 Add(string strSet,string strValue, params object[] objs)
		{
			return dal.Add(strSet,strValue,objs);
		}

		/// <summary>
		/// 是否存在该记录 strWhere
		/// </summary>
		public bool Exists(string strWhere, params object[] objs)
		{
			return dal.Exists(strWhere,objs);
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Int32 ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 存在该记录条数  ---------memo6
		/// </summary>
		public Int32 ExistsCount(string strWhere, params object[] objs)
		{
			return dal.ExistsCount(strWhere,objs);
		}


		/// <summary>
		/// 直接选择   ---------memo7  
		/// </summary>
		/// <param name="strSelect"></param>
		/// <returns></returns>
		public DataSet SelectList(string strSelect, params object[] objs)
		{
		    return dal.SelectList(strSelect,objs);
		}

		/// <summary>
		/// 获得数据列表GetList1 string strWhere
		/// </summary>
		public DataSet GetList(string strWhere, params object[] objs)
		{
			return dal.GetList(strWhere,objs);
		}

		/// <summary>
		/// 获得数据列表GetList2 string strItem,string strWhere
		/// </summary>
		public DataSet GetList(string strItem,string strWhere, params object[] objs)
		{
			return dal.GetList(strItem,strWhere,objs);
		}

		/// <summary>
		/// 得到一个对象实体 strWhere
		/// </summary>
		public EggsoftWX.Model.tab_ShopClient GetModel(Int32 ID)
		{
			return dal.GetModel(ID);
		}

            /// <summary>
        /// 得到一个对象实体 strWhere
        /// </summary>
        public EggsoftWX.Model.tab_ShopClient GetModel(string strWhere, params object[] objs)
        {
            return dal.GetModel(strWhere,objs);
        }
		/// <summary>
		/// delete strWhere 删除n条数据
		/// </summary>
		public void Delete(string strWhere, params object[] objs)
		{
			dal.Delete(strWhere,objs);
		}

		/// <summary>
		/// region delete ID 删除一条数据
		/// </summary>
		public void Delete(Int32 ID)
		{
			dal.Delete(ID);
		}

		/// <summary>
		/// update strSet strWhere 更新n条数据
		/// </summary>
		public void Update(string strSet,string strWhere, params object[] objs)
		{
			dal.Update(strSet,strWhere,objs);
		}

		/// <summary>
		///model update 更新一条数据
		/// </summary>
		public void Update(EggsoftWX.Model.tab_ShopClient model)
		{
			dal.Update(model);
		}

//ztf modify 2010-10-19
        public DataTable GetDataTable(string topNum, string fields, string strWhere, params object[] objs)
        {
            return dal.GetList(topNum, fields, strWhere,objs);
        }
        //分页列表
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string conditions, string orderField, bool isDesc, params object[] objs)
        {
            return dal.GetPageDataTable(pageIndex, pageSize, fields, conditions, orderField, isDesc,objs);
        }


		#endregion  成员方法
	}
}
