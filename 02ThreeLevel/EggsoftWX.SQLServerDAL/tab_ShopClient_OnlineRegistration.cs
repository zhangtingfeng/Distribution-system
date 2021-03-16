using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_ShopClient_OnlineRegistration。
    /// </summary>
    public class tab_ShopClient_OnlineRegistration:Itab_ShopClient_OnlineRegistration
        {
        public tab_ShopClient_OnlineRegistration()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_ShopClient_OnlineRegistration]");
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
		public Int32 Add(EggsoftWX.Model.tab_ShopClient_OnlineRegistration model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_ShopClient_OnlineRegistration");
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
		public Int32 Add(string strSet,string strValue, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [tab_ShopClient_OnlineRegistration]");
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
			strSql.Append("select count(1) from [tab_ShopClient_OnlineRegistration] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_ShopClient_OnlineRegistration] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_ShopClient_OnlineRegistration] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_ShopClient_OnlineRegistration] ");
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
		public DataSet GetList(string strItem,string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select " + strItem + " from [tab_ShopClient_OnlineRegistration] ");
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
                strSql.Append("select " + fields + " from [tab_ShopClient_OnlineRegistration] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_ShopClient_OnlineRegistration] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_ShopClient_OnlineRegistration] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_ShopClient_OnlineRegistration GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_ShopClient_OnlineRegistration] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_ShopClient_OnlineRegistration model=new EggsoftWX.Model.tab_ShopClient_OnlineRegistration();
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString()!="")
				{
					model.ShopClient_ID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClient_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=Int32.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				if(ds.Tables[0].Rows[0]["Sex"].ToString()!="")
				{
					model.Sex=Convert.ToBoolean(ds.Tables[0].Rows[0]["Sex"].ToString());
				}
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				if(ds.Tables[0].Rows[0]["birthDate"].ToString()!="")
				{
					model.birthDate=DateTime.Parse(ds.Tables[0].Rows[0]["birthDate"].ToString());
				}
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				model.LocalCall=ds.Tables[0].Rows[0]["LocalCall"].ToString();
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				if(ds.Tables[0].Rows[0]["PeopleNum"].ToString()!="")
				{
					model.PeopleNum=int.Parse(ds.Tables[0].Rows[0]["PeopleNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OnlineID"].ToString()!="")
				{
					model.OnlineID=Int32.Parse(ds.Tables[0].Rows[0]["OnlineID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Valid"].ToString()!="")
				{
					model.Valid=Convert.ToBoolean(ds.Tables[0].Rows[0]["Valid"].ToString());
				}
				model.AddExp1=ds.Tables[0].Rows[0]["AddExp1"].ToString();
				model.AddExp2=ds.Tables[0].Rows[0]["AddExp2"].ToString();
				model.AddExp3=ds.Tables[0].Rows[0]["AddExp3"].ToString();
				model.AddExp4=ds.Tables[0].Rows[0]["AddExp4"].ToString();
				model.AddExp5=ds.Tables[0].Rows[0]["AddExp5"].ToString();
				model.AddExp6=ds.Tables[0].Rows[0]["AddExp6"].ToString();
				model.AddExp7=ds.Tables[0].Rows[0]["AddExp7"].ToString();
				model.AddExp8=ds.Tables[0].Rows[0]["AddExp8"].ToString();
				model.AddExp9=ds.Tables[0].Rows[0]["AddExp9"].ToString();
				model.AddExp10=ds.Tables[0].Rows[0]["AddExp10"].ToString();
				model.AddExp11=ds.Tables[0].Rows[0]["AddExp11"].ToString();
				model.AddExp12=ds.Tables[0].Rows[0]["AddExp12"].ToString();
				model.AddExp13=ds.Tables[0].Rows[0]["AddExp13"].ToString();
				model.AddExp14=ds.Tables[0].Rows[0]["AddExp14"].ToString();
				model.AddExp15=ds.Tables[0].Rows[0]["AddExp15"].ToString();
				model.AddExp16=ds.Tables[0].Rows[0]["AddExp16"].ToString();
				model.AddExp17=ds.Tables[0].Rows[0]["AddExp17"].ToString();
				model.AddExp18=ds.Tables[0].Rows[0]["AddExp18"].ToString();
				model.AddExp19=ds.Tables[0].Rows[0]["AddExp19"].ToString();
				model.AddExp20=ds.Tables[0].Rows[0]["AddExp20"].ToString();
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				model.Createby=ds.Tables[0].Rows[0]["Createby"].ToString();
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				model.Updateby=ds.Tables[0].Rows[0]["Updateby"].ToString();
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
		public EggsoftWX.Model.tab_ShopClient_OnlineRegistration GetModel(Int32 ID)
		{
			return GetModel("ID="+ID+"");
		}
       #endregion
            
       #region delete strWhere 删除n条数据 memo 0015
       /// <summary>
		/// 删除n条数据
		/// </summary>
		public void Delete(string strWhere, params object[] objs)
		{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from [tab_ShopClient_OnlineRegistration] ");
				strSql.Append(" where "+strWhere);
				SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
				DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);
		}
       #endregion
            
       #region delete ID 删除一条数据  memo 0016
       /// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Int32 ID)
		{
				StringBuilder strSql=new StringBuilder();
				strSql.Append("delete from [tab_ShopClient_OnlineRegistration] ");
				strSql.Append(" where ID="+ID);
				DbHelperSQL.ExecuteSql(strSql.ToString());
		}
       #endregion
            
       #region update strSet strWhere 更新n条数据 更新n条数据 memo 0017
 		/// <summary>
		/// 更新n条数据
		/// </summary>
		public void Update(string strSet,string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_ShopClient_OnlineRegistration] set ");
			strSql.Append(strSet);
			strSql.Append( " where " );
			strSql.Append(strWhere);
				SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);
		}
       #endregion
            
       #region model update 更新一条数据 memo 0018
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(EggsoftWX.Model.tab_ShopClient_OnlineRegistration model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_ShopClient_OnlineRegistration] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (model.ShopClient_ID != null) strSql.Append("[ShopClient_ID]=@ShopClient_ID,");
			if (model.UserID != null) strSql.Append("[UserID]=@UserID,");
			if (String.IsNullOrEmpty(model.Name) == false) strSql.Append("[Name]=@Name,");
			if (model.Sex != null) strSql.Append("[Sex]=@Sex,");
			if (String.IsNullOrEmpty(model.Email) == false) strSql.Append("[Email]=@Email,");
			if ((model.birthDate != null)&&(model.birthDate != DateTime.MinValue)) strSql.Append("[birthDate]=@birthDate,");
			if (String.IsNullOrEmpty(model.Phone) == false) strSql.Append("[Phone]=@Phone,");
			if (String.IsNullOrEmpty(model.LocalCall) == false) strSql.Append("[LocalCall]=@LocalCall,");
			if (String.IsNullOrEmpty(model.Address) == false) strSql.Append("[Address]=@Address,");
			if (model.PeopleNum != null) strSql.Append("[PeopleNum]=@PeopleNum,");
			if (model.OnlineID != null) strSql.Append("[OnlineID]=@OnlineID,");
			if (model.Valid != null) strSql.Append("[Valid]=@Valid,");
			if (String.IsNullOrEmpty(model.AddExp1) == false) strSql.Append("[AddExp1]=@AddExp1,");
			if (String.IsNullOrEmpty(model.AddExp2) == false) strSql.Append("[AddExp2]=@AddExp2,");
			if (String.IsNullOrEmpty(model.AddExp3) == false) strSql.Append("[AddExp3]=@AddExp3,");
			if (String.IsNullOrEmpty(model.AddExp4) == false) strSql.Append("[AddExp4]=@AddExp4,");
			if (String.IsNullOrEmpty(model.AddExp5) == false) strSql.Append("[AddExp5]=@AddExp5,");
			if (String.IsNullOrEmpty(model.AddExp6) == false) strSql.Append("[AddExp6]=@AddExp6,");
			if (String.IsNullOrEmpty(model.AddExp7) == false) strSql.Append("[AddExp7]=@AddExp7,");
			if (String.IsNullOrEmpty(model.AddExp8) == false) strSql.Append("[AddExp8]=@AddExp8,");
			if (String.IsNullOrEmpty(model.AddExp9) == false) strSql.Append("[AddExp9]=@AddExp9,");
			if (String.IsNullOrEmpty(model.AddExp10) == false) strSql.Append("[AddExp10]=@AddExp10,");
			if (String.IsNullOrEmpty(model.AddExp11) == false) strSql.Append("[AddExp11]=@AddExp11,");
			if (String.IsNullOrEmpty(model.AddExp12) == false) strSql.Append("[AddExp12]=@AddExp12,");
			if (String.IsNullOrEmpty(model.AddExp13) == false) strSql.Append("[AddExp13]=@AddExp13,");
			if (String.IsNullOrEmpty(model.AddExp14) == false) strSql.Append("[AddExp14]=@AddExp14,");
			if (String.IsNullOrEmpty(model.AddExp15) == false) strSql.Append("[AddExp15]=@AddExp15,");
			if (String.IsNullOrEmpty(model.AddExp16) == false) strSql.Append("[AddExp16]=@AddExp16,");
			if (String.IsNullOrEmpty(model.AddExp17) == false) strSql.Append("[AddExp17]=@AddExp17,");
			if (String.IsNullOrEmpty(model.AddExp18) == false) strSql.Append("[AddExp18]=@AddExp18,");
			if (String.IsNullOrEmpty(model.AddExp19) == false) strSql.Append("[AddExp19]=@AddExp19,");
			if (String.IsNullOrEmpty(model.AddExp20) == false) strSql.Append("[AddExp20]=@AddExp20,");
			if ((model.CreateTime != null)&&(model.CreateTime != DateTime.MinValue)) strSql.Append("[CreateTime]=@CreateTime,");
			if (String.IsNullOrEmpty(model.Createby) == false) strSql.Append("[Createby]=@Createby,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if (String.IsNullOrEmpty(model.Updateby) == false) strSql.Append("[Updateby]=@Updateby,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (model.ShopClient_ID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClient_ID",model.ShopClient_ID));
			if (model.UserID != null) ParameterToArrayList.Add(new SqlParameter("@UserID",model.UserID));
			if (String.IsNullOrEmpty(model.Name) == false) ParameterToArrayList.Add(new SqlParameter("@Name",model.Name));
			if (model.Sex != null) ParameterToArrayList.Add(new SqlParameter("@Sex",model.Sex));
			if (String.IsNullOrEmpty(model.Email) == false) ParameterToArrayList.Add(new SqlParameter("@Email",model.Email));
			if ((model.birthDate != null)&&(model.birthDate != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@birthDate",model.birthDate));
			if (String.IsNullOrEmpty(model.Phone) == false) ParameterToArrayList.Add(new SqlParameter("@Phone",model.Phone));
			if (String.IsNullOrEmpty(model.LocalCall) == false) ParameterToArrayList.Add(new SqlParameter("@LocalCall",model.LocalCall));
			if (String.IsNullOrEmpty(model.Address) == false) ParameterToArrayList.Add(new SqlParameter("@Address",model.Address));
			if (model.PeopleNum != null) ParameterToArrayList.Add(new SqlParameter("@PeopleNum",model.PeopleNum));
			if (model.OnlineID != null) ParameterToArrayList.Add(new SqlParameter("@OnlineID",model.OnlineID));
			if (model.Valid != null) ParameterToArrayList.Add(new SqlParameter("@Valid",model.Valid));
			if (String.IsNullOrEmpty(model.AddExp1) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp1",model.AddExp1));
			if (String.IsNullOrEmpty(model.AddExp2) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp2",model.AddExp2));
			if (String.IsNullOrEmpty(model.AddExp3) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp3",model.AddExp3));
			if (String.IsNullOrEmpty(model.AddExp4) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp4",model.AddExp4));
			if (String.IsNullOrEmpty(model.AddExp5) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp5",model.AddExp5));
			if (String.IsNullOrEmpty(model.AddExp6) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp6",model.AddExp6));
			if (String.IsNullOrEmpty(model.AddExp7) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp7",model.AddExp7));
			if (String.IsNullOrEmpty(model.AddExp8) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp8",model.AddExp8));
			if (String.IsNullOrEmpty(model.AddExp9) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp9",model.AddExp9));
			if (String.IsNullOrEmpty(model.AddExp10) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp10",model.AddExp10));
			if (String.IsNullOrEmpty(model.AddExp11) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp11",model.AddExp11));
			if (String.IsNullOrEmpty(model.AddExp12) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp12",model.AddExp12));
			if (String.IsNullOrEmpty(model.AddExp13) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp13",model.AddExp13));
			if (String.IsNullOrEmpty(model.AddExp14) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp14",model.AddExp14));
			if (String.IsNullOrEmpty(model.AddExp15) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp15",model.AddExp15));
			if (String.IsNullOrEmpty(model.AddExp16) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp16",model.AddExp16));
			if (String.IsNullOrEmpty(model.AddExp17) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp17",model.AddExp17));
			if (String.IsNullOrEmpty(model.AddExp18) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp18",model.AddExp18));
			if (String.IsNullOrEmpty(model.AddExp19) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp19",model.AddExp19));
			if (String.IsNullOrEmpty(model.AddExp20) == false) ParameterToArrayList.Add(new SqlParameter("@AddExp20",model.AddExp20));
			if ((model.CreateTime != null)&&(model.CreateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreateTime",model.CreateTime));
			if (String.IsNullOrEmpty(model.Createby) == false) ParameterToArrayList.Add(new SqlParameter("@Createby",model.Createby));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
			if (String.IsNullOrEmpty(model.Updateby) == false) ParameterToArrayList.Add(new SqlParameter("@Updateby",model.Updateby));
			if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted",model.IsDeleted));
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
        public DataTable GetList(string topNum, string fields, string strWhere, params object[] objs)
       {
           strWhere = strWhere.ToLower();
           if ((strWhere.IndexOf("and") == -1) && (strWhere!="")) strWhere = "and " + strWhere;
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select top " + topNum.ToString() +" "+ fields);
           strSql.Append(" FROM   [tab_ShopClient_OnlineRegistration]  where 1 > 0 " + strWhere);
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
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_ShopClient_OnlineRegistration]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
