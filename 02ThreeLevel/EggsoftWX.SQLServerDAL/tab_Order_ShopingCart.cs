using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_Order_ShopingCart。
    /// </summary>
    public class tab_Order_ShopingCart:Itab_Order_ShopingCart
        {
        public tab_Order_ShopingCart()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_Order_ShopingCart]");
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
		public Int32 Add(EggsoftWX.Model.tab_Order_ShopingCart model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_Order_ShopingCart");
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
			strSql.Append("insert into [tab_Order_ShopingCart]");
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
			strSql.Append("select count(1) from [tab_Order_ShopingCart] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_Order_ShopingCart] where ID=" + ID + "");
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
			strSql.Append("select count(1) from [tab_Order_ShopingCart] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_Order_ShopingCart] ");
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
			strSql.Append("select " + strItem + " from [tab_Order_ShopingCart] ");
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
                strSql.Append("select " + fields + " from [tab_Order_ShopingCart] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_Order_ShopingCart] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_Order_ShopingCart] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_Order_ShopingCart GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_Order_ShopingCart] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_Order_ShopingCart model=new EggsoftWX.Model.tab_Order_ShopingCart();
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=Int32.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodID"].ToString()!="")
				{
					model.GoodID=Int32.Parse(ds.Tables[0].Rows[0]["GoodID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodIDCount"].ToString()!="")
				{
					model.GoodIDCount=int.Parse(ds.Tables[0].Rows[0]["GoodIDCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MultiBuyType"].ToString()!="")
				{
					model.MultiBuyType=Int32.Parse(ds.Tables[0].Rows[0]["MultiBuyType"].ToString());
				}
				model.VouchersNum_List=ds.Tables[0].Rows[0]["VouchersNum_List"].ToString();
				if(ds.Tables[0].Rows[0]["Beans"].ToString()!="")
				{
					model.Beans=Int32.Parse(ds.Tables[0].Rows[0]["Beans"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MoneyCredits"].ToString()!="")
				{
					model.MoneyCredits=decimal.Parse(ds.Tables[0].Rows[0]["MoneyCredits"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MoneyWeBuy8Credits"].ToString()!="")
				{
					model.MoneyWeBuy8Credits=decimal.Parse(ds.Tables[0].Rows[0]["MoneyWeBuy8Credits"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WealthMoney"].ToString()!="")
				{
					model.WealthMoney=decimal.Parse(ds.Tables[0].Rows[0]["WealthMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopClientID"].ToString()!="")
				{
					model.ShopClientID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClientID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodType"].ToString()!="")
				{
					model.GoodType=Int32.Parse(ds.Tables[0].Rows[0]["GoodType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodTypeId"].ToString()!="")
				{
					model.GoodTypeId=Int32.Parse(ds.Tables[0].Rows[0]["GoodTypeId"].ToString());
				}
				model.GoodTypeIdBuyInfo=ds.Tables[0].Rows[0]["GoodTypeIdBuyInfo"].ToString();
				model.UserSay=ds.Tables[0].Rows[0]["UserSay"].ToString();
				if(ds.Tables[0].Rows[0]["checkChoice"].ToString()!="")
				{
					model.checkChoice=Convert.ToBoolean(ds.Tables[0].Rows[0]["checkChoice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				model.CreateBy=ds.Tables[0].Rows[0]["CreateBy"].ToString();
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				model.UpdateBy=ds.Tables[0].Rows[0]["UpdateBy"].ToString();
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeletedTime"].ToString()!="")
				{
					model.IsDeletedTime=DateTime.Parse(ds.Tables[0].Rows[0]["IsDeletedTime"].ToString());
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
		public EggsoftWX.Model.tab_Order_ShopingCart GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_Order_ShopingCart] ");
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
				strSql.Append("delete from [tab_Order_ShopingCart] ");
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
			strSql.Append("update [tab_Order_ShopingCart] set ");
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
		public int Update(EggsoftWX.Model.tab_Order_ShopingCart model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_Order_ShopingCart] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (model.UserID != null) strSql.Append("[UserID]=@UserID,");
			if (model.GoodID != null) strSql.Append("[GoodID]=@GoodID,");
			if (model.GoodIDCount != null) strSql.Append("[GoodIDCount]=@GoodIDCount,");
			if (model.MultiBuyType != null) strSql.Append("[MultiBuyType]=@MultiBuyType,");
			if (String.IsNullOrEmpty(model.VouchersNum_List) == false) strSql.Append("[VouchersNum_List]=@VouchersNum_List,");
			if (model.Beans != null) strSql.Append("[Beans]=@Beans,");
			if (model.MoneyCredits != null) strSql.Append("[MoneyCredits]=@MoneyCredits,");
			if (model.MoneyWeBuy8Credits != null) strSql.Append("[MoneyWeBuy8Credits]=@MoneyWeBuy8Credits,");
			if (model.WealthMoney != null) strSql.Append("[WealthMoney]=@WealthMoney,");
			if (model.ShopClientID != null) strSql.Append("[ShopClientID]=@ShopClientID,");
			if (model.GoodType != null) strSql.Append("[GoodType]=@GoodType,");
			if (model.GoodTypeId != null) strSql.Append("[GoodTypeId]=@GoodTypeId,");
			if (String.IsNullOrEmpty(model.GoodTypeIdBuyInfo) == false) strSql.Append("[GoodTypeIdBuyInfo]=@GoodTypeIdBuyInfo,");
			if (String.IsNullOrEmpty(model.UserSay) == false) strSql.Append("[UserSay]=@UserSay,");
			if (model.checkChoice != null) strSql.Append("[checkChoice]=@checkChoice,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if (String.IsNullOrEmpty(model.CreateBy) == false) strSql.Append("[CreateBy]=@CreateBy,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if (String.IsNullOrEmpty(model.UpdateBy) == false) strSql.Append("[UpdateBy]=@UpdateBy,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			if ((model.IsDeletedTime != null)&&(model.IsDeletedTime != DateTime.MinValue)) strSql.Append("[IsDeletedTime]=@IsDeletedTime,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (model.UserID != null) ParameterToArrayList.Add(new SqlParameter("@UserID",model.UserID));
			if (model.GoodID != null) ParameterToArrayList.Add(new SqlParameter("@GoodID",model.GoodID));
			if (model.GoodIDCount != null) ParameterToArrayList.Add(new SqlParameter("@GoodIDCount",model.GoodIDCount));
			if (model.MultiBuyType != null) ParameterToArrayList.Add(new SqlParameter("@MultiBuyType",model.MultiBuyType));
			if (String.IsNullOrEmpty(model.VouchersNum_List) == false) ParameterToArrayList.Add(new SqlParameter("@VouchersNum_List",model.VouchersNum_List));
			if (model.Beans != null) ParameterToArrayList.Add(new SqlParameter("@Beans",model.Beans));
			if (model.MoneyCredits != null) ParameterToArrayList.Add(new SqlParameter("@MoneyCredits",model.MoneyCredits));
			if (model.MoneyWeBuy8Credits != null) ParameterToArrayList.Add(new SqlParameter("@MoneyWeBuy8Credits",model.MoneyWeBuy8Credits));
			if (model.WealthMoney != null) ParameterToArrayList.Add(new SqlParameter("@WealthMoney",model.WealthMoney));
			if (model.ShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClientID",model.ShopClientID));
			if (model.GoodType != null) ParameterToArrayList.Add(new SqlParameter("@GoodType",model.GoodType));
			if (model.GoodTypeId != null) ParameterToArrayList.Add(new SqlParameter("@GoodTypeId",model.GoodTypeId));
			if (String.IsNullOrEmpty(model.GoodTypeIdBuyInfo) == false) ParameterToArrayList.Add(new SqlParameter("@GoodTypeIdBuyInfo",model.GoodTypeIdBuyInfo));
			if (String.IsNullOrEmpty(model.UserSay) == false) ParameterToArrayList.Add(new SqlParameter("@UserSay",model.UserSay));
			if (model.checkChoice != null) ParameterToArrayList.Add(new SqlParameter("@checkChoice",model.checkChoice));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if (String.IsNullOrEmpty(model.CreateBy) == false) ParameterToArrayList.Add(new SqlParameter("@CreateBy",model.CreateBy));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
			if (String.IsNullOrEmpty(model.UpdateBy) == false) ParameterToArrayList.Add(new SqlParameter("@UpdateBy",model.UpdateBy));
			if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted",model.IsDeleted));
			if ((model.IsDeletedTime != null)&&(model.IsDeletedTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@IsDeletedTime",model.IsDeletedTime));
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
           strSql.Append(" FROM   [tab_Order_ShopingCart]  where 1 > 0 " + strWhere);
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
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_Order_ShopingCart]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
