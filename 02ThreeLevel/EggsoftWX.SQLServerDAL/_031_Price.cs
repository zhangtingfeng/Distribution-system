using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类_031_Price。
    /// </summary>
    public class _031_Price:I_031_Price
        {
        public _031_Price()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [_031_Price]");
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
		public Int32 Add(EggsoftWX.Model._031_Price model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "_031_Price");
			iData[1] = new SqlParameter("@MMaxID", SqlDbType.BigInt, 8, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null);
			string strReturn = DbHelperSQL.RunProcedure("RunProcedure_Insert_NeedID", iData).ToString();
			model.ID = Int32.Parse(iData[1].Value.ToString());
			if (Update(model) > 0)
			{
			    return model.ID;
			}
			else
			{
			    return 0;
			}
		}
       #endregion
            
       #region 增加一条数据 自定义 string strSet,string strValue memo 0003
       /// <summary>
		/// 增加一条数据 string strSet,string strValue
		/// </summary>
		public Int32 Add(string strSet,string strValue, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [_031_Price]");
            if (!strSet.ToLower().Contains("id"))
            {
                strSet = strSet + ",ID";
                strValue = strValue + "," + GetMaxId().ToString();
            }
			strSql.Append("(" + strSet +")");
			strSql.Append(" VALUES ");
			strSql.Append("(" + strValue  +")");
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);
			return 1;
		}
       #endregion
            
       #region 是否存在该记录 strWhere  memo 0004
       /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string strWhere, params object[] objs)
		{
            strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [_031_Price] where 1>0 "+strWhere+" ");
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			object obj=DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
			int cmdresult;
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
            strSql.Append("select count(1) from [_031_Price] where ID=" + ID + "");
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
		public Int32 ExistsCount(string strWhere, params object[] objs)
		{
           strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [_031_Price] where 1>0 "+strWhere+" ");
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			object obj=DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
			int cmdresult;
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
		public DataSet SelectList(string strSelect, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(strSelect);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			return DbHelperSQL.Query(strSql.ToString(),ParameterToList);
		}
       #endregion
            
       #region 获得数据列表GetList1 string strWhere memo 0008
            /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere, params object[] objs)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [_031_Price] ");
            if (strWhere.Trim() != "" && (strWhere.ToLower().Contains("=") || strWhere.ToLower().Contains("like")))
            {
                strSql.Append(" where " + strWhere);
            }
            if (strWhere.ToLower().Contains("order") && !strSql.ToString().ToLower().Contains("order"))
                strSql.Append(strWhere);
            else if (!strSql.ToString().ToLower().Contains("order"))
                strSql.Append(" order by ID ");

            SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.Query(strSql.ToString(),ParameterToList);
		}
       #endregion
            
       #region 获得指定数据列表GetList2 string strItem,string strWhere memo 0009
            /// <summary>
		/// 获得指定数据列表
		/// </summary>
		public DataSet GetList(string strItem,string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select " + strItem + " from [_031_Price] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
           //排除SQL聚合函数
           if (!strSql.ToString().ToLower().Contains("order") && !strSql.ToString().ToLower().Contains("group") && !strSql.ToString().ToLower().Contains("count") && !strSql.ToString().ToLower().Contains("sum") && !strSql.ToString().ToLower().Contains("avg") && !strSql.ToString().ToLower().Contains("max") && !strSql.ToString().ToLower().Contains("min"))
			    strSql.Append(" order by ID ");
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			return DbHelperSQL.Query(strSql.ToString(),ParameterToList);
		}
       #endregion
            
            
       #region IList<string> GetFieldValues  memo 0010
            /// <summary>
            /// IList<string> GetFieldValues
            /// </summary>   
            public IList<string> GetFieldValues(string fields, string strWhere, params object[] objs)
            {
                StringBuilder strSql = new StringBuilder();
                strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
                strSql.Append("select " + fields + " from [_031_Price] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
                return DbHelperSQL.GetList(strSql.ToString(),ParameterToList);
            }
       #endregion
            
       #region IList<string> GetFieldValues(string topNum, string fields, string strWhere) memo 0011
            /// <summary>
            /// IList<string> GetFieldValues(string topNum, string fields, string strWhere)
            /// </summary>   
        public IList<string> GetFieldValues(string topNum, string fields, string strWhere, params object[] objs)
        {
            strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + topNum + " " + fields + " from [_031_Price] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetList(strSql.ToString(),ParameterToList);
        }
       #endregion
            
       #region object Scalar(string field, string strWhere) memo 0012
            /// <summary>
            ///ExecuteScalar方法返回的类型是object类型，这个方法返回sql语句执行后的第一行第一列的值，由于不知到sql语句到底是什么样的结构（有可能是Int32，有可能是char等等），所以ExecuteScalar方法返回一个最基本的类型object，这个类型是所有类型的基类，换句话说：可以转换为任意类型。
            /// object Scalar(string field, string strWhere)
            /// </summary>   
        public object Scalar(string field, string strWhere, params object[] objs)
        {
            strWhere = strWhere.Trim().ToLower(); if (strWhere != "") if (strWhere.IndexOf("and") != 0) strWhere = "and " + strWhere;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + field + " from [_031_Price] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model._031_Price GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [_031_Price] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model._031_Price model=new EggsoftWX.Model._031_Price();
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.Channel=ds.Tables[0].Rows[0]["Channel"].ToString();
				model.Type=ds.Tables[0].Rows[0]["Type"].ToString();
				if(ds.Tables[0].Rows[0]["Kgs"].ToString()!="")
				{
					model.Kgs=decimal.Parse(ds.Tables[0].Rows[0]["Kgs"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_1"].ToString()!="")
				{
					model._1=decimal.Parse(ds.Tables[0].Rows[0]["_1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_2"].ToString()!="")
				{
					model._2=decimal.Parse(ds.Tables[0].Rows[0]["_2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_3"].ToString()!="")
				{
					model._3=decimal.Parse(ds.Tables[0].Rows[0]["_3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_4"].ToString()!="")
				{
					model._4=decimal.Parse(ds.Tables[0].Rows[0]["_4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_5"].ToString()!="")
				{
					model._5=decimal.Parse(ds.Tables[0].Rows[0]["_5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_6"].ToString()!="")
				{
					model._6=decimal.Parse(ds.Tables[0].Rows[0]["_6"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_7"].ToString()!="")
				{
					model._7=decimal.Parse(ds.Tables[0].Rows[0]["_7"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_8"].ToString()!="")
				{
					model._8=decimal.Parse(ds.Tables[0].Rows[0]["_8"].ToString());
				}
				if(ds.Tables[0].Rows[0]["_9"].ToString()!="")
				{
					model._9=decimal.Parse(ds.Tables[0].Rows[0]["_9"].ToString());
				}
				if(ds.Tables[0].Rows[0]["A"].ToString()!="")
				{
					model.A=decimal.Parse(ds.Tables[0].Rows[0]["A"].ToString());
				}
				if(ds.Tables[0].Rows[0]["B"].ToString()!="")
				{
					model.B=decimal.Parse(ds.Tables[0].Rows[0]["B"].ToString());
				}
				if(ds.Tables[0].Rows[0]["D"].ToString()!="")
				{
					model.D=decimal.Parse(ds.Tables[0].Rows[0]["D"].ToString());
				}
				if(ds.Tables[0].Rows[0]["E"].ToString()!="")
				{
					model.E=decimal.Parse(ds.Tables[0].Rows[0]["E"].ToString());
				}
				if(ds.Tables[0].Rows[0]["F"].ToString()!="")
				{
					model.F=decimal.Parse(ds.Tables[0].Rows[0]["F"].ToString());
				}
				if(ds.Tables[0].Rows[0]["G"].ToString()!="")
				{
					model.G=decimal.Parse(ds.Tables[0].Rows[0]["G"].ToString());
				}
				if(ds.Tables[0].Rows[0]["H"].ToString()!="")
				{
					model.H=decimal.Parse(ds.Tables[0].Rows[0]["H"].ToString());
				}
				if(ds.Tables[0].Rows[0]["M"].ToString()!="")
				{
					model.M=decimal.Parse(ds.Tables[0].Rows[0]["M"].ToString());
				}
				if(ds.Tables[0].Rows[0]["N"].ToString()!="")
				{
					model.N=decimal.Parse(ds.Tables[0].Rows[0]["N"].ToString());
				}
				if(ds.Tables[0].Rows[0]["O"].ToString()!="")
				{
					model.O=decimal.Parse(ds.Tables[0].Rows[0]["O"].ToString());
				}
				if(ds.Tables[0].Rows[0]["P"].ToString()!="")
				{
					model.P=decimal.Parse(ds.Tables[0].Rows[0]["P"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Q"].ToString()!="")
				{
					model.Q=decimal.Parse(ds.Tables[0].Rows[0]["Q"].ToString());
				}
				if(ds.Tables[0].Rows[0]["R"].ToString()!="")
				{
					model.R=decimal.Parse(ds.Tables[0].Rows[0]["R"].ToString());
				}
				if(ds.Tables[0].Rows[0]["S"].ToString()!="")
				{
					model.S=decimal.Parse(ds.Tables[0].Rows[0]["S"].ToString());
				}
				if(ds.Tables[0].Rows[0]["T"].ToString()!="")
				{
					model.T=decimal.Parse(ds.Tables[0].Rows[0]["T"].ToString());
				}
				if(ds.Tables[0].Rows[0]["U"].ToString()!="")
				{
					model.U=decimal.Parse(ds.Tables[0].Rows[0]["U"].ToString());
				}
				if(ds.Tables[0].Rows[0]["V"].ToString()!="")
				{
					model.V=decimal.Parse(ds.Tables[0].Rows[0]["V"].ToString());
				}
				if(ds.Tables[0].Rows[0]["X"].ToString()!="")
				{
					model.X=decimal.Parse(ds.Tables[0].Rows[0]["X"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Y"].ToString()!="")
				{
					model.Y=decimal.Parse(ds.Tables[0].Rows[0]["Y"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Z"].ToString()!="")
				{
					model.Z=decimal.Parse(ds.Tables[0].Rows[0]["Z"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString()!="")
				{
					model.ShopClient_ID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				model.Creatby=ds.Tables[0].Rows[0]["Creatby"].ToString();
				model.UpdateBy=ds.Tables[0].Rows[0]["UpdateBy"].ToString();
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=int.Parse(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
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
		public EggsoftWX.Model._031_Price GetModel(Int32 ID)
		{
			return GetModel("ID="+ID+"");
		}
       #endregion
            
       #region delete strWhere 删除n条数据 memo 0015
       /// <summary>
		/// 删除n条数据
		/// </summary>
		public int Delete(string strWhere, params object[] objs)
		{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from [_031_Price] ");
				strSql.Append(" where "+strWhere);
				SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
				return DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);
		}
       #endregion
            
       #region delete ID 删除一条数据  memo 0016
       /// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(Int32 ID)
		{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from [_031_Price] ");
				strSql.Append(" where ID="+ID);
				return DbHelperSQL.ExecuteSql(strSql.ToString());
		}
       #endregion
            
       #region update strSet strWhere 更新n条数据 更新n条数据 memo 0017
 		/// <summary>
		/// 更新n条数据
		/// </summary>
		public int Update(string strSet,string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [_031_Price] set ");
			strSql.Append(strSet);
			strSql.Append( " where " );
			strSql.Append(strWhere);
				SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			return DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);
		}
       #endregion
            
       #region model update 更新一条数据 memo 0018
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(EggsoftWX.Model._031_Price model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [_031_Price] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (String.IsNullOrEmpty(model.Channel) == false) strSql.Append("[Channel]=@Channel,");
			if (String.IsNullOrEmpty(model.Type) == false) strSql.Append("[Type]=@Type,");
			if (model.Kgs != null) strSql.Append("[Kgs]=@Kgs,");
			if (model._1 != null) strSql.Append("[_1]=@_1,");
			if (model._2 != null) strSql.Append("[_2]=@_2,");
			if (model._3 != null) strSql.Append("[_3]=@_3,");
			if (model._4 != null) strSql.Append("[_4]=@_4,");
			if (model._5 != null) strSql.Append("[_5]=@_5,");
			if (model._6 != null) strSql.Append("[_6]=@_6,");
			if (model._7 != null) strSql.Append("[_7]=@_7,");
			if (model._8 != null) strSql.Append("[_8]=@_8,");
			if (model._9 != null) strSql.Append("[_9]=@_9,");
			if (model.A != null) strSql.Append("[A]=@A,");
			if (model.B != null) strSql.Append("[B]=@B,");
			if (model.D != null) strSql.Append("[D]=@D,");
			if (model.E != null) strSql.Append("[E]=@E,");
			if (model.F != null) strSql.Append("[F]=@F,");
			if (model.G != null) strSql.Append("[G]=@G,");
			if (model.H != null) strSql.Append("[H]=@H,");
			if (model.M != null) strSql.Append("[M]=@M,");
			if (model.N != null) strSql.Append("[N]=@N,");
			if (model.O != null) strSql.Append("[O]=@O,");
			if (model.P != null) strSql.Append("[P]=@P,");
			if (model.Q != null) strSql.Append("[Q]=@Q,");
			if (model.R != null) strSql.Append("[R]=@R,");
			if (model.S != null) strSql.Append("[S]=@S,");
			if (model.T != null) strSql.Append("[T]=@T,");
			if (model.U != null) strSql.Append("[U]=@U,");
			if (model.V != null) strSql.Append("[V]=@V,");
			if (model.X != null) strSql.Append("[X]=@X,");
			if (model.Y != null) strSql.Append("[Y]=@Y,");
			if (model.Z != null) strSql.Append("[Z]=@Z,");
			if (model.ShopClient_ID != null) strSql.Append("[ShopClient_ID]=@ShopClient_ID,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if (String.IsNullOrEmpty(model.Creatby) == false) strSql.Append("[Creatby]=@Creatby,");
			if (String.IsNullOrEmpty(model.UpdateBy) == false) strSql.Append("[UpdateBy]=@UpdateBy,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (String.IsNullOrEmpty(model.Channel) == false) ParameterToArrayList.Add(new SqlParameter("@Channel",model.Channel));
			if (String.IsNullOrEmpty(model.Type) == false) ParameterToArrayList.Add(new SqlParameter("@Type",model.Type));
			if (model.Kgs != null) ParameterToArrayList.Add(new SqlParameter("@Kgs",model.Kgs));
			if (model._1 != null) ParameterToArrayList.Add(new SqlParameter("@_1",model._1));
			if (model._2 != null) ParameterToArrayList.Add(new SqlParameter("@_2",model._2));
			if (model._3 != null) ParameterToArrayList.Add(new SqlParameter("@_3",model._3));
			if (model._4 != null) ParameterToArrayList.Add(new SqlParameter("@_4",model._4));
			if (model._5 != null) ParameterToArrayList.Add(new SqlParameter("@_5",model._5));
			if (model._6 != null) ParameterToArrayList.Add(new SqlParameter("@_6",model._6));
			if (model._7 != null) ParameterToArrayList.Add(new SqlParameter("@_7",model._7));
			if (model._8 != null) ParameterToArrayList.Add(new SqlParameter("@_8",model._8));
			if (model._9 != null) ParameterToArrayList.Add(new SqlParameter("@_9",model._9));
			if (model.A != null) ParameterToArrayList.Add(new SqlParameter("@A",model.A));
			if (model.B != null) ParameterToArrayList.Add(new SqlParameter("@B",model.B));
			if (model.D != null) ParameterToArrayList.Add(new SqlParameter("@D",model.D));
			if (model.E != null) ParameterToArrayList.Add(new SqlParameter("@E",model.E));
			if (model.F != null) ParameterToArrayList.Add(new SqlParameter("@F",model.F));
			if (model.G != null) ParameterToArrayList.Add(new SqlParameter("@G",model.G));
			if (model.H != null) ParameterToArrayList.Add(new SqlParameter("@H",model.H));
			if (model.M != null) ParameterToArrayList.Add(new SqlParameter("@M",model.M));
			if (model.N != null) ParameterToArrayList.Add(new SqlParameter("@N",model.N));
			if (model.O != null) ParameterToArrayList.Add(new SqlParameter("@O",model.O));
			if (model.P != null) ParameterToArrayList.Add(new SqlParameter("@P",model.P));
			if (model.Q != null) ParameterToArrayList.Add(new SqlParameter("@Q",model.Q));
			if (model.R != null) ParameterToArrayList.Add(new SqlParameter("@R",model.R));
			if (model.S != null) ParameterToArrayList.Add(new SqlParameter("@S",model.S));
			if (model.T != null) ParameterToArrayList.Add(new SqlParameter("@T",model.T));
			if (model.U != null) ParameterToArrayList.Add(new SqlParameter("@U",model.U));
			if (model.V != null) ParameterToArrayList.Add(new SqlParameter("@V",model.V));
			if (model.X != null) ParameterToArrayList.Add(new SqlParameter("@X",model.X));
			if (model.Y != null) ParameterToArrayList.Add(new SqlParameter("@Y",model.Y));
			if (model.Z != null) ParameterToArrayList.Add(new SqlParameter("@Z",model.Z));
			if (model.ShopClient_ID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClient_ID",model.ShopClient_ID));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if (String.IsNullOrEmpty(model.Creatby) == false) ParameterToArrayList.Add(new SqlParameter("@Creatby",model.Creatby));
			if (String.IsNullOrEmpty(model.UpdateBy) == false) ParameterToArrayList.Add(new SqlParameter("@UpdateBy",model.UpdateBy));
			if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted",model.IsDeleted));
			strSql.Append(" where ID='"+model.ID+"'");
			return DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToArrayList.ToArray());
		}
       #endregion
            
       #region  GetList(string topNum, string fields, string strWhere memo 0019
        /// <summary>   //memo 0019
        /// 
        /// </summary>
       /// <param name=/strWhere/></param>
        /// <returns></returns>
        public DataTable GetList(string topNum, string fields, string strWhere, params object[] objs)
       {
           strWhere = strWhere.ToLower();
           if ((strWhere.IndexOf("and") == -1) && (strWhere!="")) strWhere = "and " + strWhere;
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select top " + topNum.ToString() +" "+ fields);
           strSql.Append(" FROM   [_031_Price]  where 1 > 0 " + strWhere);
           SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
           return DbHelperSQL.GetDataTable(strSql.ToString(),ParameterToList);
       }

         #endregion  成员方法
       #region 大数据量快速分页,50万以上数据分页 memo 0020
         //大数据量快速分页,50万以上数据分页  memo 0020
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc, params object[] objs)
        {
           strConditions = strConditions.ToLower();
           if ((strConditions.IndexOf("and") == -1) && (strConditions!="")) strConditions = "and " + strConditions;
           SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(fields+" "+strConditions+" "+orderField, objs);
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[_031_Price]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
