using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类b001_Phone_Message__CheckCode。
    /// </summary>
    public class b001_Phone_Message__CheckCode:Ib001_Phone_Message__CheckCode
        {
        public b001_Phone_Message__CheckCode()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [b001_Phone_Message__CheckCode]");
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
		public Int32 Add(EggsoftWX.Model.b001_Phone_Message__CheckCode model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "b001_Phone_Message__CheckCode");
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
			strSql.Append("insert into [b001_Phone_Message__CheckCode]");
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
			strSql.Append("select count(1) from [b001_Phone_Message__CheckCode] where 1>0 "+strWhere+" ");
			object obj=DbHelperSQL.GetSingle(strSql.ToString());
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
            strSql.Append("select count(1) from [b001_Phone_Message__CheckCode] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [b001_Phone_Message__CheckCode] where 1>0 "+strWhere+" ");
			object obj=DbHelperSQL.GetSingle(strSql.ToString());
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
            strSql.Append("select * from [b001_Phone_Message__CheckCode] ");
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
			strSql.Append("select " + strItem + " from [b001_Phone_Message__CheckCode] ");
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
                strSql.Append("select " + fields + " from [b001_Phone_Message__CheckCode] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [b001_Phone_Message__CheckCode] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [b001_Phone_Message__CheckCode] where 1>0 " + strWhere);
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.b001_Phone_Message__CheckCode GetModel(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [b001_Phone_Message__CheckCode] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.b001_Phone_Message__CheckCode model=new EggsoftWX.Model.b001_Phone_Message__CheckCode();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.SendPhoneNum=ds.Tables[0].Rows[0]["SendPhoneNum"].ToString();
				if(ds.Tables[0].Rows[0]["SendTime"].ToString()!="")
				{
					model.SendTime=DateTime.Parse(ds.Tables[0].Rows[0]["SendTime"].ToString());
				}
				model.innerIP=ds.Tables[0].Rows[0]["innerIP"].ToString();
				model.IP=ds.Tables[0].Rows[0]["IP"].ToString();
				model.IPDetailDesc=ds.Tables[0].Rows[0]["IPDetailDesc"].ToString();
				model.CheckCode=ds.Tables[0].Rows[0]["CheckCode"].ToString();
				if(ds.Tables[0].Rows[0]["CheckTime"].ToString()!="")
				{
					model.CheckTime=DateTime.Parse(ds.Tables[0].Rows[0]["CheckTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CheckStatus"].ToString()!="")
				{
					model.CheckStatus=Convert.ToBoolean(ds.Tables[0].Rows[0]["CheckStatus"].ToString());
				}
				model.MessageContent=ds.Tables[0].Rows[0]["MessageContent"].ToString();
				if(ds.Tables[0].Rows[0]["SendStatus"].ToString()!="")
				{
					model.SendStatus=Int32.Parse(ds.Tables[0].Rows[0]["SendStatus"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MessageCheckStatus"].ToString()!="")
				{
					model.MessageCheckStatus=int.Parse(ds.Tables[0].Rows[0]["MessageCheckStatus"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SendType"].ToString()!="")
				{
					model.SendType=int.Parse(ds.Tables[0].Rows[0]["SendType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopClientID"].ToString()!="")
				{
					model.ShopClientID=int.Parse(ds.Tables[0].Rows[0]["ShopClientID"].ToString());
				}
				model.CreateBy=ds.Tables[0].Rows[0]["CreateBy"].ToString();
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				model.UpdateBy=ds.Tables[0].Rows[0]["UpdateBy"].ToString();
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=Int32.Parse(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["consumeMoney"].ToString()!="")
				{
					model.consumeMoney=decimal.Parse(ds.Tables[0].Rows[0]["consumeMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AuthorMoney"].ToString()!="")
				{
					model.AuthorMoney=decimal.Parse(ds.Tables[0].Rows[0]["AuthorMoney"].ToString());
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
		public EggsoftWX.Model.b001_Phone_Message__CheckCode GetModel(Int32 ID)
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
				strSql.Append("delete from [b001_Phone_Message__CheckCode] ");
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
				strSql.Append("delete from [b001_Phone_Message__CheckCode] ");
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
			strSql.Append("update [b001_Phone_Message__CheckCode] set ");
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
		public void Update(EggsoftWX.Model.b001_Phone_Message__CheckCode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [b001_Phone_Message__CheckCode] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (String.IsNullOrEmpty(model.SendPhoneNum) == false) strSql.Append("[SendPhoneNum]=@SendPhoneNum,");
			if ((model.SendTime != null)&&(model.SendTime != DateTime.MinValue)) strSql.Append("[SendTime]=@SendTime,");
			if (String.IsNullOrEmpty(model.innerIP) == false) strSql.Append("[innerIP]=@innerIP,");
			if (String.IsNullOrEmpty(model.IP) == false) strSql.Append("[IP]=@IP,");
			if (String.IsNullOrEmpty(model.IPDetailDesc) == false) strSql.Append("[IPDetailDesc]=@IPDetailDesc,");
			if (String.IsNullOrEmpty(model.CheckCode) == false) strSql.Append("[CheckCode]=@CheckCode,");
			if ((model.CheckTime != null)&&(model.CheckTime != DateTime.MinValue)) strSql.Append("[CheckTime]=@CheckTime,");
			if (model.CheckStatus != null) strSql.Append("[CheckStatus]=@CheckStatus,");
			if (String.IsNullOrEmpty(model.MessageContent) == false) strSql.Append("[MessageContent]=@MessageContent,");
			if (model.SendStatus != null) strSql.Append("[SendStatus]=@SendStatus,");
			if (model.MessageCheckStatus != null) strSql.Append("[MessageCheckStatus]=@MessageCheckStatus,");
			if (model.SendType != null) strSql.Append("[SendType]=@SendType,");
			if (model.ShopClientID != null) strSql.Append("[ShopClientID]=@ShopClientID,");
			if (String.IsNullOrEmpty(model.CreateBy) == false) strSql.Append("[CreateBy]=@CreateBy,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if (String.IsNullOrEmpty(model.UpdateBy) == false) strSql.Append("[UpdateBy]=@UpdateBy,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			if (model.consumeMoney != null) strSql.Append("[consumeMoney]=@consumeMoney,");
			if (model.AuthorMoney != null) strSql.Append("[AuthorMoney]=@AuthorMoney,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (String.IsNullOrEmpty(model.SendPhoneNum) == false) ParameterToArrayList.Add(new SqlParameter("@SendPhoneNum",model.SendPhoneNum));
			if ((model.SendTime != null)&&(model.SendTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@SendTime",model.SendTime));
			if (String.IsNullOrEmpty(model.innerIP) == false) ParameterToArrayList.Add(new SqlParameter("@innerIP",model.innerIP));
			if (String.IsNullOrEmpty(model.IP) == false) ParameterToArrayList.Add(new SqlParameter("@IP",model.IP));
			if (String.IsNullOrEmpty(model.IPDetailDesc) == false) ParameterToArrayList.Add(new SqlParameter("@IPDetailDesc",model.IPDetailDesc));
			if (String.IsNullOrEmpty(model.CheckCode) == false) ParameterToArrayList.Add(new SqlParameter("@CheckCode",model.CheckCode));
			if ((model.CheckTime != null)&&(model.CheckTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CheckTime",model.CheckTime));
			if (model.CheckStatus != null) ParameterToArrayList.Add(new SqlParameter("@CheckStatus",model.CheckStatus));
			if (String.IsNullOrEmpty(model.MessageContent) == false) ParameterToArrayList.Add(new SqlParameter("@MessageContent",model.MessageContent));
			if (model.SendStatus != null) ParameterToArrayList.Add(new SqlParameter("@SendStatus",model.SendStatus));
			if (model.MessageCheckStatus != null) ParameterToArrayList.Add(new SqlParameter("@MessageCheckStatus",model.MessageCheckStatus));
			if (model.SendType != null) ParameterToArrayList.Add(new SqlParameter("@SendType",model.SendType));
			if (model.ShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClientID",model.ShopClientID));
			if (String.IsNullOrEmpty(model.CreateBy) == false) ParameterToArrayList.Add(new SqlParameter("@CreateBy",model.CreateBy));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
			if (String.IsNullOrEmpty(model.UpdateBy) == false) ParameterToArrayList.Add(new SqlParameter("@UpdateBy",model.UpdateBy));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted",model.IsDeleted));
			if (model.consumeMoney != null) ParameterToArrayList.Add(new SqlParameter("@consumeMoney",model.consumeMoney));
			if (model.AuthorMoney != null) ParameterToArrayList.Add(new SqlParameter("@AuthorMoney",model.AuthorMoney));
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
           strSql.Append(" FROM   [b001_Phone_Message__CheckCode]  where 1 > 0 " + strWhere);
           return DbHelperSQL.GetDataTable(strSql.ToString());
       }

         #endregion  成员方法
       #region 大数据量快速分页,50万以上数据分页 memo 0020
         //大数据量快速分页,50万以上数据分页  memo 0020
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc)
        {
           strConditions = strConditions.ToLower();
           if ((strConditions.IndexOf("and") == -1) && (strConditions!="")) strConditions = "and " + strConditions;
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[b001_Phone_Message__CheckCode]", strConditions, orderField, isDesc);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
