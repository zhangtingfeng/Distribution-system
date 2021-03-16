using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using EggsoftWX.IDAL;
namespace EggsoftWX.BLL
{
	/// <summary>
	/// 业务逻辑类Help_Content 的摘要说明。
	/// </summary>
	public class Help_Content
	{
		IHelp_Content dal=EggsoftWX.DALFactory.Help_Content.Create();
		public Help_Content()
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
		/// 增加一条数据
		/// </summary>
		public Int32 Add(EggsoftWX.Model.Help_Content model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 增加一条数据 自定义 string strSet,string strValue
		/// </summary>
		public Int32 Add(string strSet,string strValue)
		{
			return dal.Add(strSet,strValue);
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
		public EggsoftWX.Model.Help_Content GetModel(Int32 ID)
		{
			return dal.GetModel(ID);
		}

            /// <summary>
        /// 得到一个对象实体 strWhere
        /// </summary>
        public EggsoftWX.Model.Help_Content GetModel(string strWhere)
        {
            return dal.GetModel(strWhere);
        }
		/// <summary>
		/// delete strWhere 删除n条数据
		/// </summary>
		public void Delete(string strWhere)
		{
			dal.Delete(strWhere);
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
		public void Update(string strSet,string strWhere)
		{
			dal.Update(strSet,strWhere);
		}

		/// <summary>
		///model update 更新一条数据
		/// </summary>
		public void Update(EggsoftWX.Model.Help_Content model)
		{
			dal.Update(model);
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
