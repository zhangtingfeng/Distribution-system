using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类View_SalesCount03_All_SalesCount。
    /// </summary>
    public class View_SalesCount03_All_SalesCount:IView_SalesCount03_All_SalesCount
        {
        public View_SalesCount03_All_SalesCount()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [View_SalesCount03_All_SalesCount]");
			object obj=DbHelperSQL.GetSingle(strSql.ToString());
			if(obj==null)
			{
				return 1;
			}
			else
			{
				return Int32.Parse(obj.ToString());
			}
		}
       #endregion
            
       #region 增加一条数据 自定义 string strSet,string strValue memo 0003
       /// <summary>
		/// 增加一条数据 string strSet,string strValue
		/// </summary>
		public Int32 Add(string strSet,string strValue)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [View_SalesCount03_All_SalesCount]");
			strSql.Append("(" + strSet +")");
			strSql.Append(" VALUES ");
			strSql.Append("(" + strValue  +")");
			DbHelperSQL.ExecuteSql(strSql.ToString());
			return 1;
		}
       #endregion
            
       #region 是否存在该记录 strWhere  memo 0004
       /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string strWhere)
		{
            strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [View_SalesCount03_All_SalesCount] where 1>0 "+strWhere+" ");
			object obj=DbHelperSQL.GetSingle(strSql.ToString());
			Int32 cmdresult;
			if((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))
			{
				cmdresult=0;
			}
			else
			{
				cmdresult=Int32.Parse(obj.ToString());
			}
			if(cmdresult==0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
       #endregion
            
       #region 是否存在该记录 ID memo 0005
       /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Int32 ID)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [View_SalesCount03_All_SalesCount] where ID=" + ID + "");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            Int32 cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = Int32.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            } 
		}
       #endregion
            
       #region 存在该记录条数 memo 0006
       /// <summary>
		/// 存在该记录条数
		/// </summary>
		public Int32 ExistsCount(string strWhere)
		{
           strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(*) from [View_SalesCount03_All_SalesCount] where 1>0 "+strWhere+" ");
			object obj=DbHelperSQL.GetSingle(strSql.ToString());
			Int32 cmdresult;
			if((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))
			{
				cmdresult=0;
			}
			else
			{
				cmdresult=Int32.Parse(obj.ToString());
			}
			return cmdresult;
		}
       #endregion
            
       #region 获得数据列表 自定义SelectList strSelect oderby  group 等等 memo 0007
            /// <summary>
		/// 获得数据列表 自定义SelectList strSelect oderby  ，group 等等
		/// </summary>
		public DataSet SelectList(string strSelect)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(strSelect);
			return DbHelperSQL.Query(strSql.ToString());
		}
       #endregion
            
       #region 获得数据列表GetList1 string strWhere memo 0008
            /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [View_SalesCount03_All_SalesCount] ");
            if (strWhere.Trim() != "" && (strWhere.ToLower().Contains("=") || strWhere.ToLower().Contains("like")))
            {
                strSql.Append(" where " + strWhere);
            }
            if (strWhere.ToLower().Contains("order") && !strSql.ToString().ToLower().Contains("order"))
                strSql.Append(strWhere);

            return DbHelperSQL.Query(strSql.ToString());
		}
       #endregion
            
       #region 获得指定数据列表GetList2 string strItem,string strWhere memo 0009
            /// <summary>
		/// 获得指定数据列表
		/// </summary>
		public DataSet GetList(string strItem,string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select " + strItem + " from [View_SalesCount03_All_SalesCount] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
       #endregion
            
            
       #region IList<string> GetFieldValues  memo 0010
            /// <summary>
            /// IList<string> GetFieldValues
            /// </summary>   
            public IList<string> GetFieldValues(string fields, string strWhere)
            {
                StringBuilder strSql = new StringBuilder();
                strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
                strSql.Append("select " + fields + " from [View_SalesCount03_All_SalesCount] where 1>0 " + strWhere);
                return DbHelperSQL.GetList(strSql.ToString());
            }
       #endregion
            
       #region IList<string> GetFieldValues(string topNum, string fields, string strWhere) memo 0011
            /// <summary>
            /// IList<string> GetFieldValues(string topNum, string fields, string strWhere)
            /// </summary>   
        public IList<string> GetFieldValues(string topNum, string fields, string strWhere)
        {
            strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + topNum + " " + fields + " from [View_SalesCount03_All_SalesCount] where 1>0 " + strWhere);
            return DbHelperSQL.GetList(strSql.ToString());
        }
       #endregion
            
       #region object Scalar(string field, string strWhere) memo 0012
            /// <summary>
            ///ExecuteScalar方法返回的类型是object类型，这个方法返回sql语句执行后的第一行第一列的值，由于不知到sql语句到底是什么样的结构（有可能是Int32，有可能是char等等），所以ExecuteScalar方法返回一个最基本的类型object，这个类型是所有类型的基类，换句话说：可以转换为任意类型。
            /// object Scalar(string field, string strWhere)
            /// </summary>   
        public object Scalar(string field, string strWhere)
        {
            strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + field + " from [View_SalesCount03_All_SalesCount] where 1>0 " + strWhere);
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.View_SalesCount03_All_SalesCount GetModel(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [View_SalesCount03_All_SalesCount] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.View_SalesCount03_All_SalesCount model=new EggsoftWX.Model.View_SalesCount03_All_SalesCount();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString()!="")
				{
					model.ShopClient_ID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Class1_ID"].ToString()!="")
				{
					model.Class1_ID=Int32.Parse(ds.Tables[0].Rows[0]["Class1_ID"].ToString());
				}
				model.ShopClientName=ds.Tables[0].Rows[0]["ShopClientName"].ToString();
				if(ds.Tables[0].Rows[0]["Class2_ID"].ToString()!="")
				{
					model.Class2_ID=Int32.Parse(ds.Tables[0].Rows[0]["Class2_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Class3_ID"].ToString()!="")
				{
					model.Class3_ID=Int32.Parse(ds.Tables[0].Rows[0]["Class3_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isSaled"].ToString()!="")
				{
					model.isSaled=Convert.ToBoolean(ds.Tables[0].Rows[0]["isSaled"].ToString());
				}
				model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				model.Icon=ds.Tables[0].Rows[0]["Icon"].ToString();
				model.ShortInfo=ds.Tables[0].Rows[0]["ShortInfo"].ToString();
				model.LongInfo=ds.Tables[0].Rows[0]["LongInfo"].ToString();
				if(ds.Tables[0].Rows[0]["KuCunCount"].ToString()!="")
				{
					model.KuCunCount=Int32.Parse(ds.Tables[0].Rows[0]["KuCunCount"].ToString());
				}
				model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString();
				if(ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PromotePrice"].ToString()!="")
				{
					model.PromotePrice=decimal.Parse(ds.Tables[0].Rows[0]["PromotePrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Good_Class"].ToString()!="")
				{
					model.Good_Class=Int32.Parse(ds.Tables[0].Rows[0]["Good_Class"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LimitTimerBuy_StartTime"].ToString()!="")
				{
					model.LimitTimerBuy_StartTime=DateTime.Parse(ds.Tables[0].Rows[0]["LimitTimerBuy_StartTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LimitTimerBuy_EndTime"].ToString()!="")
				{
					model.LimitTimerBuy_EndTime=DateTime.Parse(ds.Tables[0].Rows[0]["LimitTimerBuy_EndTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LimitTimerBuy_TimePrice"].ToString()!="")
				{
					model.LimitTimerBuy_TimePrice=decimal.Parse(ds.Tables[0].Rows[0]["LimitTimerBuy_TimePrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LimitTimerBuy_Bool"].ToString()!="")
				{
					model.LimitTimerBuy_Bool=Convert.ToBoolean(ds.Tables[0].Rows[0]["LimitTimerBuy_Bool"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MinOrderNum"].ToString()!="")
				{
					model.MinOrderNum=Int32.Parse(ds.Tables[0].Rows[0]["MinOrderNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MaxOrderNum"].ToString()!="")
				{
					model.MaxOrderNum=Int32.Parse(ds.Tables[0].Rows[0]["MaxOrderNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LimitTimerBuy_MaxSalesCount"].ToString()!="")
				{
					model.LimitTimerBuy_MaxSalesCount=Int32.Parse(ds.Tables[0].Rows[0]["LimitTimerBuy_MaxSalesCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IS_Admin_check"].ToString()!="")
				{
					model.IS_Admin_check=Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_Admin_check"].ToString());
				}
				model.XML=ds.Tables[0].Rows[0]["XML"].ToString();
				if(ds.Tables[0].Rows[0]["SalesCount"].ToString()!="")
				{
					model.SalesCount=Int32.Parse(ds.Tables[0].Rows[0]["SalesCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=Int32.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Shopping_Vouchers"].ToString()!="")
				{
					model.Shopping_Vouchers=Int32.Parse(ds.Tables[0].Rows[0]["Shopping_Vouchers"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodID"].ToString()!="")
				{
					model.GoodID=Int32.Parse(ds.Tables[0].Rows[0]["GoodID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShareAskCount"].ToString()!="")
				{
					model.ShareAskCount=Int32.Parse(ds.Tables[0].Rows[0]["ShareAskCount"].ToString());
				}
				return model;
			}
			else
			{
			return null;
			}
		}
       #endregion
            
       #region 得到一个对象实体 ID  memo 0014
           /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public EggsoftWX.Model.View_SalesCount03_All_SalesCount GetModel(Int32 ID)
		{
			return GetModel("ID="+ID+"");
		}
       #endregion
            
       #region delete strWhere 删除n条数据 memo 0015
       /// <summary>
		/// 删除n条数据
		/// </summary>
		public void Delete(string strWhere)
		{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from [View_SalesCount03_All_SalesCount] ");
				strSql.Append(" where "+strWhere);
				DbHelperSQL.ExecuteSql(strSql.ToString());
		}
       #endregion
            
       #region  GetList(string topNum, string fields, string strWhere memo 0019
        /// <summary>   //memo 0019
        /// 
        /// </summary>
       /// <param name=/strWhere/></param>
        /// <returns></returns>
        public DataTable GetList(string topNum, string fields, string strWhere)
       {
           strWhere = strWhere.ToLower();
           if ((strWhere.IndexOf("and") == -1) && (strWhere!="")) strWhere = "and " + strWhere;
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select top " + topNum.ToString() +" "+ fields);
           strSql.Append(" FROM   [View_SalesCount03_All_SalesCount]  where 1 > 0 " + strWhere);
           return DbHelperSQL.GetDataTable(strSql.ToString());
       }

         #endregion  成员方法
       #region 大数据量快速分页,50万以上数据分页 memo 0020
         //大数据量快速分页,50万以上数据分页  memo 0020
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc)
        {
           strConditions = strConditions.ToLower();
           if ((strConditions.IndexOf("and") == -1) && (strConditions!="")) strConditions = "and " + strConditions;
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[View_SalesCount03_All_SalesCount]", strConditions, orderField, isDesc);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
