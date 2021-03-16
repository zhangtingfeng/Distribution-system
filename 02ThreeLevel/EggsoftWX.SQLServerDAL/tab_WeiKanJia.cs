using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_WeiKanJia。
    /// </summary>
    public class tab_WeiKanJia:Itab_WeiKanJia
        {
        public tab_WeiKanJia()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_WeiKanJia]");
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
            
       #region 增加一条数据 model  memo 0002
       /// <summary>
		/// 增加一条数据 model
		/// </summary>
		public Int32 Add(EggsoftWX.Model.tab_WeiKanJia model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_WeiKanJia");
			iData[1] = new SqlParameter("@MMaxID", SqlDbType.BigInt, 8, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null);
			string strReturn = DbHelperSQL.RunProcedure("RunProcedure_Insert_NeedID", iData).ToString();
			model.ID = Int32.Parse(iData[1].Value.ToString());
			Update(model);
			return model.ID;
		}
       #endregion
            
       #region 增加一条数据 自定义 string strSet,string strValue memo 0003
       /// <summary>
		/// 增加一条数据 string strSet,string strValue
		/// </summary>
		public Int32 Add(string strSet,string strValue)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [tab_WeiKanJia]");
            if (!strSet.ToLower().Contains("id"))
            {
                strSet = strSet + ",ID";
                strValue = strValue + "," + GetMaxId().ToString();
            }
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
			strSql.Append("select count(1) from [tab_WeiKanJia] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_WeiKanJia] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_WeiKanJia] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_WeiKanJia] ");
            if (strWhere.Trim() != "" && (strWhere.ToLower().Contains("=") || strWhere.ToLower().Contains("like")))
            {
                strSql.Append(" where " + strWhere);
            }
            if (strWhere.ToLower().Contains("order") && !strSql.ToString().ToLower().Contains("order"))
                strSql.Append(strWhere);
            else if (!strSql.ToString().ToLower().Contains("order"))
                strSql.Append(" order by ID ");

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
			strSql.Append("select " + strItem + " from [tab_WeiKanJia] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
           //排除SQL聚合函数
           if (!strSql.ToString().ToLower().Contains("order") && !strSql.ToString().ToLower().Contains("group") && !strSql.ToString().ToLower().Contains("count") && !strSql.ToString().ToLower().Contains("sum") && !strSql.ToString().ToLower().Contains("avg") && !strSql.ToString().ToLower().Contains("max") && !strSql.ToString().ToLower().Contains("min"))
			    strSql.Append(" order by ID ");
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
                strSql.Append("select " + fields + " from [tab_WeiKanJia] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_WeiKanJia] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_WeiKanJia] where 1>0 " + strWhere);
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_WeiKanJia GetModel(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_WeiKanJia] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_WeiKanJia model=new EggsoftWX.Model.tab_WeiKanJia();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.Topic=ds.Tables[0].Rows[0]["Topic"].ToString();
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				model.KanJiaRule=ds.Tables[0].Rows[0]["KanJiaRule"].ToString();
				if(ds.Tables[0].Rows[0]["StartPrice"].ToString()!="")
				{
					model.StartPrice=decimal.Parse(ds.Tables[0].Rows[0]["StartPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndPrice"].ToString()!="")
				{
					model.EndPrice=decimal.Parse(ds.Tables[0].Rows[0]["EndPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AgentPrice"].ToString()!="")
				{
					model.AgentPrice=decimal.Parse(ds.Tables[0].Rows[0]["AgentPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EachAction_LowPrice"].ToString()!="")
				{
					model.EachAction_LowPrice=decimal.Parse(ds.Tables[0].Rows[0]["EachAction_LowPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EachAction_HighPrice"].ToString()!="")
				{
					model.EachAction_HighPrice=decimal.Parse(ds.Tables[0].Rows[0]["EachAction_HighPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopClientID"].ToString()!="")
				{
					model.ShopClientID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClientID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MustAddress_Master"].ToString()!="")
				{
					model.MustAddress_Master=Convert.ToBoolean(ds.Tables[0].Rows[0]["MustAddress_Master"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MustSubscribe_Master"].ToString()!="")
				{
					model.MustSubscribe_Master=Convert.ToBoolean(ds.Tables[0].Rows[0]["MustSubscribe_Master"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MustSubscribe_Helper"].ToString()!="")
				{
					model.MustSubscribe_Helper=Convert.ToBoolean(ds.Tables[0].Rows[0]["MustSubscribe_Helper"].ToString());
				}
				model.KanJiaTopicDescContent=ds.Tables[0].Rows[0]["KanJiaTopicDescContent"].ToString();
				if(ds.Tables[0].Rows[0]["EndTime"].ToString()!="")
				{
					model.EndTime=DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MustSubscribe_Agent"].ToString()!="")
				{
					model.MustSubscribe_Agent=Convert.ToBoolean(ds.Tables[0].Rows[0]["MustSubscribe_Agent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isSaled"].ToString()!="")
				{
					model.isSaled=Convert.ToBoolean(ds.Tables[0].Rows[0]["isSaled"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=Int32.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isdeleted"].ToString()!="")
				{
					model.isdeleted=Int32.Parse(ds.Tables[0].Rows[0]["isdeleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodID"].ToString()!="")
				{
					model.GoodID=Int32.Parse(ds.Tables[0].Rows[0]["GoodID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
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
		public EggsoftWX.Model.tab_WeiKanJia GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_WeiKanJia] ");
				strSql.Append(" where "+strWhere);
				DbHelperSQL.ExecuteSql(strSql.ToString());
		}
       #endregion
            
       #region delete ID 删除一条数据  memo 0016
       /// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Int32 ID)
		{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from [tab_WeiKanJia] ");
				strSql.Append(" where ID="+ID);
				DbHelperSQL.ExecuteSql(strSql.ToString());
		}
       #endregion
            
       #region update strSet strWhere 更新n条数据 更新n条数据 memo 0017
 		/// <summary>
		/// 更新n条数据
		/// </summary>
		public void Update(string strSet,string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_WeiKanJia] set ");
			strSql.Append(strSet);
			strSql.Append( " where " );
			strSql.Append(strWhere);
			DbHelperSQL.ExecuteSql(strSql.ToString());
		}
       #endregion
            
       #region model update 更新一条数据 memo 0018
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(EggsoftWX.Model.tab_WeiKanJia model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_WeiKanJia] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (String.IsNullOrEmpty(model.Topic) == false) strSql.Append("[Topic]=@Topic,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if (String.IsNullOrEmpty(model.KanJiaRule) == false) strSql.Append("[KanJiaRule]=@KanJiaRule,");
			if (model.StartPrice != null) strSql.Append("[StartPrice]=@StartPrice,");
			if (model.EndPrice != null) strSql.Append("[EndPrice]=@EndPrice,");
			if (model.AgentPrice != null) strSql.Append("[AgentPrice]=@AgentPrice,");
			if (model.EachAction_LowPrice != null) strSql.Append("[EachAction_LowPrice]=@EachAction_LowPrice,");
			if (model.EachAction_HighPrice != null) strSql.Append("[EachAction_HighPrice]=@EachAction_HighPrice,");
			if (model.ShopClientID != null) strSql.Append("[ShopClientID]=@ShopClientID,");
			if (model.MustAddress_Master != null) strSql.Append("[MustAddress_Master]=@MustAddress_Master,");
			if (model.MustSubscribe_Master != null) strSql.Append("[MustSubscribe_Master]=@MustSubscribe_Master,");
			if (model.MustSubscribe_Helper != null) strSql.Append("[MustSubscribe_Helper]=@MustSubscribe_Helper,");
			if (String.IsNullOrEmpty(model.KanJiaTopicDescContent) == false) strSql.Append("[KanJiaTopicDescContent]=@KanJiaTopicDescContent,");
			if ((model.EndTime != null)&&(model.EndTime != DateTime.MinValue)) strSql.Append("[EndTime]=@EndTime,");
			if (model.MustSubscribe_Agent != null) strSql.Append("[MustSubscribe_Agent]=@MustSubscribe_Agent,");
			if (model.isSaled != null) strSql.Append("[isSaled]=@isSaled,");
			if (model.Sort != null) strSql.Append("[Sort]=@Sort,");
			if (model.isdeleted != null) strSql.Append("[isdeleted]=@isdeleted,");
			if (model.GoodID != null) strSql.Append("[GoodID]=@GoodID,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (String.IsNullOrEmpty(model.Topic) == false) ParameterToArrayList.Add(new SqlParameter("@Topic",model.Topic));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
			if (String.IsNullOrEmpty(model.KanJiaRule) == false) ParameterToArrayList.Add(new SqlParameter("@KanJiaRule",model.KanJiaRule));
			if (model.StartPrice != null) ParameterToArrayList.Add(new SqlParameter("@StartPrice",model.StartPrice));
			if (model.EndPrice != null) ParameterToArrayList.Add(new SqlParameter("@EndPrice",model.EndPrice));
			if (model.AgentPrice != null) ParameterToArrayList.Add(new SqlParameter("@AgentPrice",model.AgentPrice));
			if (model.EachAction_LowPrice != null) ParameterToArrayList.Add(new SqlParameter("@EachAction_LowPrice",model.EachAction_LowPrice));
			if (model.EachAction_HighPrice != null) ParameterToArrayList.Add(new SqlParameter("@EachAction_HighPrice",model.EachAction_HighPrice));
			if (model.ShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClientID",model.ShopClientID));
			if (model.MustAddress_Master != null) ParameterToArrayList.Add(new SqlParameter("@MustAddress_Master",model.MustAddress_Master));
			if (model.MustSubscribe_Master != null) ParameterToArrayList.Add(new SqlParameter("@MustSubscribe_Master",model.MustSubscribe_Master));
			if (model.MustSubscribe_Helper != null) ParameterToArrayList.Add(new SqlParameter("@MustSubscribe_Helper",model.MustSubscribe_Helper));
			if (String.IsNullOrEmpty(model.KanJiaTopicDescContent) == false) ParameterToArrayList.Add(new SqlParameter("@KanJiaTopicDescContent",model.KanJiaTopicDescContent));
			if ((model.EndTime != null)&&(model.EndTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@EndTime",model.EndTime));
			if (model.MustSubscribe_Agent != null) ParameterToArrayList.Add(new SqlParameter("@MustSubscribe_Agent",model.MustSubscribe_Agent));
			if (model.isSaled != null) ParameterToArrayList.Add(new SqlParameter("@isSaled",model.isSaled));
			if (model.Sort != null) ParameterToArrayList.Add(new SqlParameter("@Sort",model.Sort));
			if (model.isdeleted != null) ParameterToArrayList.Add(new SqlParameter("@isdeleted",model.isdeleted));
			if (model.GoodID != null) ParameterToArrayList.Add(new SqlParameter("@GoodID",model.GoodID));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			strSql.Append(" where ID='"+model.ID+"'");
			DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToArrayList.ToArray());
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
           strSql.Append(" FROM   [tab_WeiKanJia]  where 1 > 0 " + strWhere);
           return DbHelperSQL.GetDataTable(strSql.ToString());
       }

         #endregion  成员方法
       #region 大数据量快速分页,50万以上数据分页 memo 0020
         //大数据量快速分页,50万以上数据分页  memo 0020
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc)
        {
           strConditions = strConditions.ToLower();
           if ((strConditions.IndexOf("and") == -1) && (strConditions!="")) strConditions = "and " + strConditions;
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_WeiKanJia]", strConditions, orderField, isDesc);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
