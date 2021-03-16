using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using EggsoftWX.IDAL;
namespace EggsoftWX.BLL
{
	/// <summary>
	/// 业务逻辑类View_WeiKanJia 的摘要说明。
	/// </summary>
	public class View_WeiKanJia
	{
		IView_WeiKanJia dal=EggsoftWX.DALFactory.View_WeiKanJia.Create();
		public View_WeiKanJia()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录 strWhere
		/// </summary>
		public bool Exists(string strWhere)
		{
			return dal.Exists(strWhere);
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
		public Int32 ExistsCount(string strWhere)
		{
			return dal.ExistsCount(strWhere);
		}


		/// <summary>
		/// 直接选择   ---------memo7  
		/// </summary>
		/// <param name="strSelect"></param>
		/// <returns></returns>
		public DataSet SelectList(string strSelect)
		{
		    return dal.SelectList(strSelect);
		}

		/// <summary>
		/// 获得数据列表GetList1 string strWhere
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表GetList2 string strItem,string strWhere
		/// </summary>
		public DataSet GetList(string strItem,string strWhere)
		{
			return dal.GetList(strItem,strWhere);
		}

		/// <summary>
		/// 得到一个对象实体 strWhere
		/// </summary>
		public EggsoftWX.Model.View_WeiKanJia GetModel(Int32 ID)
		{
			return dal.GetModel(ID);
		}

            /// <summary>
        /// 得到一个对象实体 strWhere
        /// </summary>
        public EggsoftWX.Model.View_WeiKanJia GetModel(string strWhere)
        {
            return dal.GetModel(strWhere);
        }
//ztf modify 2010-10-19
        public DataTable GetDataTable(string topNum, string fields, string strWhere)
        {
            return dal.GetList(topNum, fields, strWhere);
        }
        //分页列表
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string conditions, string orderField, bool isDesc)
        {
            return dal.GetPageDataTable(pageIndex, pageSize, fields, conditions, orderField, isDesc);
        }


		#endregion  成员方法
	}
}
