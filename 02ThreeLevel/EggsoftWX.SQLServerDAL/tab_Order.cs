using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_Order。
    /// </summary>
    public class tab_Order:Itab_Order
        {
        public tab_Order()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_Order]");
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
		public Int32 Add(EggsoftWX.Model.tab_Order model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_Order");
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
			strSql.Append("insert into [tab_Order]");
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
			strSql.Append("select count(1) from [tab_Order] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_Order] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_Order] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_Order] ");
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
			strSql.Append("select " + strItem + " from [tab_Order] ");
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
                strSql.Append("select " + fields + " from [tab_Order] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_Order] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_Order] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_Order GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_Order] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_Order model=new EggsoftWX.Model.tab_Order();
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PayStatus"].ToString()!="")
				{
					model.PayStatus=Int32.Parse(ds.Tables[0].Rows[0]["PayStatus"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isReceipt"].ToString()!="")
				{
					model.isReceipt=Convert.ToBoolean(ds.Tables[0].Rows[0]["isReceipt"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateDateTime"].ToString()!="")
				{
					model.CreateDateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=Int32.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				model.DeliveryText=ds.Tables[0].Rows[0]["DeliveryText"].ToString();
				if(ds.Tables[0].Rows[0]["TotalMoney"].ToString()!="")
				{
					model.TotalMoney=decimal.Parse(ds.Tables[0].Rows[0]["TotalMoney"].ToString());
				}
				model.OrderNum=ds.Tables[0].Rows[0]["OrderNum"].ToString();
				if(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString()!="")
				{
					model.ShopClient_ID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString());
				}
				model.OrderName=ds.Tables[0].Rows[0]["OrderName"].ToString();
				if(ds.Tables[0].Rows[0]["User_Address"].ToString()!="")
				{
					model.User_Address=Int32.Parse(ds.Tables[0].Rows[0]["User_Address"].ToString());
				}
				model.PayWay=ds.Tables[0].Rows[0]["PayWay"].ToString();
				model.PaywayOrderNum=ds.Tables[0].Rows[0]["PaywayOrderNum"].ToString();
				if(ds.Tables[0].Rows[0]["PayDateTime"].ToString()!="")
				{
					model.PayDateTime=DateTime.Parse(ds.Tables[0].Rows[0]["PayDateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["O2OTakedID"].ToString()!="")
				{
					model.O2OTakedID=int.Parse(ds.Tables[0].Rows[0]["O2OTakedID"].ToString());
				}
				model.FreightShowText=ds.Tables[0].Rows[0]["FreightShowText"].ToString();
				if(ds.Tables[0].Rows[0]["ModifyPriceUpdateDateTime"].ToString()!="")
				{
					model.ModifyPriceUpdateDateTime=DateTime.Parse(ds.Tables[0].Rows[0]["ModifyPriceUpdateDateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				model.CreateBy=ds.Tables[0].Rows[0]["CreateBy"].ToString();
				if(ds.Tables[0].Rows[0]["UpdateDateTime"].ToString()!="")
				{
					model.UpdateDateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDateTime"].ToString());
				}
				model.UpdateBy=ds.Tables[0].Rows[0]["UpdateBy"].ToString();
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
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
		public EggsoftWX.Model.tab_Order GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_Order] ");
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
				strSql.Append("delete from [tab_Order] ");
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
			strSql.Append("update [tab_Order] set ");
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
		public int Update(EggsoftWX.Model.tab_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_Order] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (model.PayStatus != null) strSql.Append("[PayStatus]=@PayStatus,");
			if (model.isReceipt != null) strSql.Append("[isReceipt]=@isReceipt,");
			if ((model.CreateDateTime != null)&&(model.CreateDateTime != DateTime.MinValue)) strSql.Append("[CreateDateTime]=@CreateDateTime,");
			if (model.UserID != null) strSql.Append("[UserID]=@UserID,");
			if (String.IsNullOrEmpty(model.DeliveryText) == false) strSql.Append("[DeliveryText]=@DeliveryText,");
			if (model.TotalMoney != null) strSql.Append("[TotalMoney]=@TotalMoney,");
			if (String.IsNullOrEmpty(model.OrderNum) == false) strSql.Append("[OrderNum]=@OrderNum,");
			if (model.ShopClient_ID != null) strSql.Append("[ShopClient_ID]=@ShopClient_ID,");
			if (String.IsNullOrEmpty(model.OrderName) == false) strSql.Append("[OrderName]=@OrderName,");
			if (model.User_Address != null) strSql.Append("[User_Address]=@User_Address,");
			if (String.IsNullOrEmpty(model.PayWay) == false) strSql.Append("[PayWay]=@PayWay,");
			if (String.IsNullOrEmpty(model.PaywayOrderNum) == false) strSql.Append("[PaywayOrderNum]=@PaywayOrderNum,");
			if ((model.PayDateTime != null)&&(model.PayDateTime != DateTime.MinValue)) strSql.Append("[PayDateTime]=@PayDateTime,");
			if (model.O2OTakedID != null) strSql.Append("[O2OTakedID]=@O2OTakedID,");
			if (String.IsNullOrEmpty(model.FreightShowText) == false) strSql.Append("[FreightShowText]=@FreightShowText,");
			if ((model.ModifyPriceUpdateDateTime != null)&&(model.ModifyPriceUpdateDateTime != DateTime.MinValue)) strSql.Append("[ModifyPriceUpdateDateTime]=@ModifyPriceUpdateDateTime,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if (String.IsNullOrEmpty(model.CreateBy) == false) strSql.Append("[CreateBy]=@CreateBy,");
			if ((model.UpdateDateTime != null)&&(model.UpdateDateTime != DateTime.MinValue)) strSql.Append("[UpdateDateTime]=@UpdateDateTime,");
			if (String.IsNullOrEmpty(model.UpdateBy) == false) strSql.Append("[UpdateBy]=@UpdateBy,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (model.PayStatus != null) ParameterToArrayList.Add(new SqlParameter("@PayStatus",model.PayStatus));
			if (model.isReceipt != null) ParameterToArrayList.Add(new SqlParameter("@isReceipt",model.isReceipt));
			if ((model.CreateDateTime != null)&&(model.CreateDateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreateDateTime",model.CreateDateTime));
			if (model.UserID != null) ParameterToArrayList.Add(new SqlParameter("@UserID",model.UserID));
			if (String.IsNullOrEmpty(model.DeliveryText) == false) ParameterToArrayList.Add(new SqlParameter("@DeliveryText",model.DeliveryText));
			if (model.TotalMoney != null) ParameterToArrayList.Add(new SqlParameter("@TotalMoney",model.TotalMoney));
			if (String.IsNullOrEmpty(model.OrderNum) == false) ParameterToArrayList.Add(new SqlParameter("@OrderNum",model.OrderNum));
			if (model.ShopClient_ID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClient_ID",model.ShopClient_ID));
			if (String.IsNullOrEmpty(model.OrderName) == false) ParameterToArrayList.Add(new SqlParameter("@OrderName",model.OrderName));
			if (model.User_Address != null) ParameterToArrayList.Add(new SqlParameter("@User_Address",model.User_Address));
			if (String.IsNullOrEmpty(model.PayWay) == false) ParameterToArrayList.Add(new SqlParameter("@PayWay",model.PayWay));
			if (String.IsNullOrEmpty(model.PaywayOrderNum) == false) ParameterToArrayList.Add(new SqlParameter("@PaywayOrderNum",model.PaywayOrderNum));
			if ((model.PayDateTime != null)&&(model.PayDateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@PayDateTime",model.PayDateTime));
			if (model.O2OTakedID != null) ParameterToArrayList.Add(new SqlParameter("@O2OTakedID",model.O2OTakedID));
			if (String.IsNullOrEmpty(model.FreightShowText) == false) ParameterToArrayList.Add(new SqlParameter("@FreightShowText",model.FreightShowText));
			if ((model.ModifyPriceUpdateDateTime != null)&&(model.ModifyPriceUpdateDateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@ModifyPriceUpdateDateTime",model.ModifyPriceUpdateDateTime));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if (String.IsNullOrEmpty(model.CreateBy) == false) ParameterToArrayList.Add(new SqlParameter("@CreateBy",model.CreateBy));
			if ((model.UpdateDateTime != null)&&(model.UpdateDateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateDateTime",model.UpdateDateTime));
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
           strSql.Append(" FROM   [tab_Order]  where 1 > 0 " + strWhere);
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
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_Order]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
