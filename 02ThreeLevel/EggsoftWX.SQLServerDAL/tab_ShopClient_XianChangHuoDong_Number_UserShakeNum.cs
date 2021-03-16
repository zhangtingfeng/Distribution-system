using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_ShopClient_XianChangHuoDong_Number_UserShakeNum。
    /// </summary>
    public class tab_ShopClient_XianChangHuoDong_Number_UserShakeNum:Itab_ShopClient_XianChangHuoDong_Number_UserShakeNum
        {
        public tab_ShopClient_XianChangHuoDong_Number_UserShakeNum()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum]");
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
		public Int32 Add(EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_ShopClient_XianChangHuoDong_Number_UserShakeNum");
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
			strSql.Append("insert into [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum]");
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
			strSql.Append("select count(1) from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] ");
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
			strSql.Append("select " + strItem + " from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] ");
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
                strSql.Append("select " + fields + " from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] where 1>0 " + strWhere);
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum GetModel(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum model=new EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["XianChangHuoDongNumberbyShopClientID"].ToString()!="")
				{
					model.XianChangHuoDongNumberbyShopClientID=Int32.Parse(ds.Tables[0].Rows[0]["XianChangHuoDongNumberbyShopClientID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=Int32.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserShopClientID"].ToString()!="")
				{
					model.UserShopClientID=Int32.Parse(ds.Tables[0].Rows[0]["UserShopClientID"].ToString());
				}
				model.UserNickName=ds.Tables[0].Rows[0]["UserNickName"].ToString();
				if(ds.Tables[0].Rows[0]["UserShakeNumber"].ToString()!="")
				{
					model.UserShakeNumber=Int32.Parse(ds.Tables[0].Rows[0]["UserShakeNumber"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
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
		public EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] ");
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
				strSql.Append("delete from [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] ");
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
			strSql.Append("update [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] set ");
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
		public void Update(EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (model.XianChangHuoDongNumberbyShopClientID != null) strSql.Append("[XianChangHuoDongNumberbyShopClientID]=@XianChangHuoDongNumberbyShopClientID,");
			if (model.UserID != null) strSql.Append("[UserID]=@UserID,");
			if (model.UserShopClientID != null) strSql.Append("[UserShopClientID]=@UserShopClientID,");
			if (String.IsNullOrEmpty(model.UserNickName) == false) strSql.Append("[UserNickName]=@UserNickName,");
			if (model.UserShakeNumber != null) strSql.Append("[UserShakeNumber]=@UserShakeNumber,");
			if ((model.CreateTime != null)&&(model.CreateTime != DateTime.MinValue)) strSql.Append("[CreateTime]=@CreateTime,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (model.XianChangHuoDongNumberbyShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@XianChangHuoDongNumberbyShopClientID",model.XianChangHuoDongNumberbyShopClientID));
			if (model.UserID != null) ParameterToArrayList.Add(new SqlParameter("@UserID",model.UserID));
			if (model.UserShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@UserShopClientID",model.UserShopClientID));
			if (String.IsNullOrEmpty(model.UserNickName) == false) ParameterToArrayList.Add(new SqlParameter("@UserNickName",model.UserNickName));
			if (model.UserShakeNumber != null) ParameterToArrayList.Add(new SqlParameter("@UserShakeNumber",model.UserShakeNumber));
			if ((model.CreateTime != null)&&(model.CreateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreateTime",model.CreateTime));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
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
           strSql.Append(" FROM   [tab_ShopClient_XianChangHuoDong_Number_UserShakeNum]  where 1 > 0 " + strWhere);
           return DbHelperSQL.GetDataTable(strSql.ToString());
       }

         #endregion  成员方法
       #region 大数据量快速分页,50万以上数据分页 memo 0020
         //大数据量快速分页,50万以上数据分页  memo 0020
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc)
        {
           strConditions = strConditions.ToLower();
           if ((strConditions.IndexOf("and") == -1) && (strConditions!="")) strConditions = "and " + strConditions;
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_ShopClient_XianChangHuoDong_Number_UserShakeNum]", strConditions, orderField, isDesc);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
