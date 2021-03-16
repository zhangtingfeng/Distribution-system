using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_Goods。
    /// </summary>
    public class tab_Goods:Itab_Goods
        {
        public tab_Goods()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_Goods]");
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
		public Int32 Add(EggsoftWX.Model.tab_Goods model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_Goods");
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
			strSql.Append("insert into [tab_Goods]");
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
			strSql.Append("select count(1) from [tab_Goods] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_Goods] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_Goods] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_Goods] ");
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
			strSql.Append("select " + strItem + " from [tab_Goods] ");
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
                strSql.Append("select " + fields + " from [tab_Goods] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_Goods] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_Goods] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_Goods GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_Goods] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_Goods model=new EggsoftWX.Model.tab_Goods();
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
				if(ds.Tables[0].Rows[0]["Class1_ID"].ToString()!="")
				{
					model.Class1_ID=int.Parse(ds.Tables[0].Rows[0]["Class1_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Class2_ID"].ToString()!="")
				{
					model.Class2_ID=int.Parse(ds.Tables[0].Rows[0]["Class2_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Class3_ID"].ToString()!="")
				{
					model.Class3_ID=int.Parse(ds.Tables[0].Rows[0]["Class3_ID"].ToString());
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
					model.KuCunCount=int.Parse(ds.Tables[0].Rows[0]["KuCunCount"].ToString());
				}
				model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString();
				if(ds.Tables[0].Rows[0]["kg"].ToString()!="")
				{
					model.kg=decimal.Parse(ds.Tables[0].Rows[0]["kg"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MemberPrice"].ToString()!="")
				{
					model.MemberPrice=decimal.Parse(ds.Tables[0].Rows[0]["MemberPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PromotePrice"].ToString()!="")
				{
					model.PromotePrice=decimal.Parse(ds.Tables[0].Rows[0]["PromotePrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsCommend"].ToString()!="")
				{
					model.IsCommend=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCommend"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HitCount"].ToString()!="")
				{
					model.HitCount=int.Parse(ds.Tables[0].Rows[0]["HitCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PromoteCount"].ToString()!="")
				{
					model.PromoteCount=int.Parse(ds.Tables[0].Rows[0]["PromoteCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpTime"].ToString()!="")
				{
					model.UpTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				model.ContactMan=ds.Tables[0].Rows[0]["ContactMan"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Good_Class"].ToString()!="")
				{
					model.Good_Class=Int32.Parse(ds.Tables[0].Rows[0]["Good_Class"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SalesCount"].ToString()!="")
				{
					model.SalesCount=int.Parse(ds.Tables[0].Rows[0]["SalesCount"].ToString());
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
				if(ds.Tables[0].Rows[0]["Shopping_Vouchers"].ToString()!="")
				{
					model.Shopping_Vouchers=Convert.ToBoolean(ds.Tables[0].Rows[0]["Shopping_Vouchers"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IS_Admin_check"].ToString()!="")
				{
					model.IS_Admin_check=Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_Admin_check"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CheckBox_WeiBai_RedMoney"].ToString()!="")
				{
					model.CheckBox_WeiBai_RedMoney=Convert.ToBoolean(ds.Tables[0].Rows[0]["CheckBox_WeiBai_RedMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Webuy8_DistributionMoney_Value"].ToString()!="")
				{
					model.Webuy8_DistributionMoney_Value=int.Parse(ds.Tables[0].Rows[0]["Webuy8_DistributionMoney_Value"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FreightTemplate_ID"].ToString()!="")
				{
					model.FreightTemplate_ID=int.Parse(ds.Tables[0].Rows[0]["FreightTemplate_ID"].ToString());
				}
				model.XML=ds.Tables[0].Rows[0]["XML"].ToString();
				if(ds.Tables[0].Rows[0]["AgentPercent"].ToString()!="")
				{
					model.AgentPercent=decimal.Parse(ds.Tables[0].Rows[0]["AgentPercent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Shopping_Vouchers_Percent"].ToString()!="")
				{
					model.Shopping_Vouchers_Percent=decimal.Parse(ds.Tables[0].Rows[0]["Shopping_Vouchers_Percent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WealthMoney"].ToString()!="")
				{
					model.WealthMoney=decimal.Parse(ds.Tables[0].Rows[0]["WealthMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Send_Vouchers_IfBuy"].ToString()!="")
				{
					model.Send_Vouchers_IfBuy=decimal.Parse(ds.Tables[0].Rows[0]["Send_Vouchers_IfBuy"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Send_Money_IfBuy"].ToString()!="")
				{
					model.Send_Money_IfBuy=decimal.Parse(ds.Tables[0].Rows[0]["Send_Money_IfBuy"].ToString());
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
		public EggsoftWX.Model.tab_Goods GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_Goods] ");
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
				strSql.Append("delete from [tab_Goods] ");
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
			strSql.Append("update [tab_Goods] set ");
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
		public int Update(EggsoftWX.Model.tab_Goods model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_Goods] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (model.ShopClient_ID != null) strSql.Append("[ShopClient_ID]=@ShopClient_ID,");
			if (model.Class1_ID != null) strSql.Append("[Class1_ID]=@Class1_ID,");
			if (model.Class2_ID != null) strSql.Append("[Class2_ID]=@Class2_ID,");
			if (model.Class3_ID != null) strSql.Append("[Class3_ID]=@Class3_ID,");
			if (model.isSaled != null) strSql.Append("[isSaled]=@isSaled,");
			if (String.IsNullOrEmpty(model.Name) == false) strSql.Append("[Name]=@Name,");
			if (String.IsNullOrEmpty(model.Icon) == false) strSql.Append("[Icon]=@Icon,");
			if (String.IsNullOrEmpty(model.ShortInfo) == false) strSql.Append("[ShortInfo]=@ShortInfo,");
			if (String.IsNullOrEmpty(model.LongInfo) == false) strSql.Append("[LongInfo]=@LongInfo,");
			if (model.KuCunCount != null) strSql.Append("[KuCunCount]=@KuCunCount,");
			if (String.IsNullOrEmpty(model.Unit) == false) strSql.Append("[Unit]=@Unit,");
			if (model.kg != null) strSql.Append("[kg]=@kg,");
			if (model.Price != null) strSql.Append("[Price]=@Price,");
			if (model.MemberPrice != null) strSql.Append("[MemberPrice]=@MemberPrice,");
			if (model.PromotePrice != null) strSql.Append("[PromotePrice]=@PromotePrice,");
			if (model.IsCommend != null) strSql.Append("[IsCommend]=@IsCommend,");
			if (model.HitCount != null) strSql.Append("[HitCount]=@HitCount,");
			if (model.PromoteCount != null) strSql.Append("[PromoteCount]=@PromoteCount,");
			if ((model.UpTime != null)&&(model.UpTime != DateTime.MinValue)) strSql.Append("[UpTime]=@UpTime,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if (String.IsNullOrEmpty(model.ContactMan) == false) strSql.Append("[ContactMan]=@ContactMan,");
			if (model.Sort != null) strSql.Append("[Sort]=@Sort,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			if (model.Good_Class != null) strSql.Append("[Good_Class]=@Good_Class,");
			if (model.SalesCount != null) strSql.Append("[SalesCount]=@SalesCount,");
			if ((model.LimitTimerBuy_StartTime != null)&&(model.LimitTimerBuy_StartTime != DateTime.MinValue)) strSql.Append("[LimitTimerBuy_StartTime]=@LimitTimerBuy_StartTime,");
			if ((model.LimitTimerBuy_EndTime != null)&&(model.LimitTimerBuy_EndTime != DateTime.MinValue)) strSql.Append("[LimitTimerBuy_EndTime]=@LimitTimerBuy_EndTime,");
			if (model.LimitTimerBuy_TimePrice != null) strSql.Append("[LimitTimerBuy_TimePrice]=@LimitTimerBuy_TimePrice,");
			if (model.LimitTimerBuy_Bool != null) strSql.Append("[LimitTimerBuy_Bool]=@LimitTimerBuy_Bool,");
			if (model.MinOrderNum != null) strSql.Append("[MinOrderNum]=@MinOrderNum,");
			if (model.MaxOrderNum != null) strSql.Append("[MaxOrderNum]=@MaxOrderNum,");
			if (model.LimitTimerBuy_MaxSalesCount != null) strSql.Append("[LimitTimerBuy_MaxSalesCount]=@LimitTimerBuy_MaxSalesCount,");
			if (model.Shopping_Vouchers != null) strSql.Append("[Shopping_Vouchers]=@Shopping_Vouchers,");
			if (model.IS_Admin_check != null) strSql.Append("[IS_Admin_check]=@IS_Admin_check,");
			if (model.CheckBox_WeiBai_RedMoney != null) strSql.Append("[CheckBox_WeiBai_RedMoney]=@CheckBox_WeiBai_RedMoney,");
			if (model.Webuy8_DistributionMoney_Value != null) strSql.Append("[Webuy8_DistributionMoney_Value]=@Webuy8_DistributionMoney_Value,");
			if (model.FreightTemplate_ID != null) strSql.Append("[FreightTemplate_ID]=@FreightTemplate_ID,");
			if (String.IsNullOrEmpty(model.XML) == false) strSql.Append("[XML]=@XML,");
			if (model.AgentPercent != null) strSql.Append("[AgentPercent]=@AgentPercent,");
			if (model.Shopping_Vouchers_Percent != null) strSql.Append("[Shopping_Vouchers_Percent]=@Shopping_Vouchers_Percent,");
			if (model.WealthMoney != null) strSql.Append("[WealthMoney]=@WealthMoney,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if (model.Send_Vouchers_IfBuy != null) strSql.Append("[Send_Vouchers_IfBuy]=@Send_Vouchers_IfBuy,");
			if (model.Send_Money_IfBuy != null) strSql.Append("[Send_Money_IfBuy]=@Send_Money_IfBuy,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (model.ShopClient_ID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClient_ID",model.ShopClient_ID));
			if (model.Class1_ID != null) ParameterToArrayList.Add(new SqlParameter("@Class1_ID",model.Class1_ID));
			if (model.Class2_ID != null) ParameterToArrayList.Add(new SqlParameter("@Class2_ID",model.Class2_ID));
			if (model.Class3_ID != null) ParameterToArrayList.Add(new SqlParameter("@Class3_ID",model.Class3_ID));
			if (model.isSaled != null) ParameterToArrayList.Add(new SqlParameter("@isSaled",model.isSaled));
			if (String.IsNullOrEmpty(model.Name) == false) ParameterToArrayList.Add(new SqlParameter("@Name",model.Name));
			if (String.IsNullOrEmpty(model.Icon) == false) ParameterToArrayList.Add(new SqlParameter("@Icon",model.Icon));
			if (String.IsNullOrEmpty(model.ShortInfo) == false) ParameterToArrayList.Add(new SqlParameter("@ShortInfo",model.ShortInfo));
			if (String.IsNullOrEmpty(model.LongInfo) == false) ParameterToArrayList.Add(new SqlParameter("@LongInfo",model.LongInfo));
			if (model.KuCunCount != null) ParameterToArrayList.Add(new SqlParameter("@KuCunCount",model.KuCunCount));
			if (String.IsNullOrEmpty(model.Unit) == false) ParameterToArrayList.Add(new SqlParameter("@Unit",model.Unit));
			if (model.kg != null) ParameterToArrayList.Add(new SqlParameter("@kg",model.kg));
			if (model.Price != null) ParameterToArrayList.Add(new SqlParameter("@Price",model.Price));
			if (model.MemberPrice != null) ParameterToArrayList.Add(new SqlParameter("@MemberPrice",model.MemberPrice));
			if (model.PromotePrice != null) ParameterToArrayList.Add(new SqlParameter("@PromotePrice",model.PromotePrice));
			if (model.IsCommend != null) ParameterToArrayList.Add(new SqlParameter("@IsCommend",model.IsCommend));
			if (model.HitCount != null) ParameterToArrayList.Add(new SqlParameter("@HitCount",model.HitCount));
			if (model.PromoteCount != null) ParameterToArrayList.Add(new SqlParameter("@PromoteCount",model.PromoteCount));
			if ((model.UpTime != null)&&(model.UpTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpTime",model.UpTime));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
			if (String.IsNullOrEmpty(model.ContactMan) == false) ParameterToArrayList.Add(new SqlParameter("@ContactMan",model.ContactMan));
			if (model.Sort != null) ParameterToArrayList.Add(new SqlParameter("@Sort",model.Sort));
			if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted",model.IsDeleted));
			if (model.Good_Class != null) ParameterToArrayList.Add(new SqlParameter("@Good_Class",model.Good_Class));
			if (model.SalesCount != null) ParameterToArrayList.Add(new SqlParameter("@SalesCount",model.SalesCount));
			if ((model.LimitTimerBuy_StartTime != null)&&(model.LimitTimerBuy_StartTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@LimitTimerBuy_StartTime",model.LimitTimerBuy_StartTime));
			if ((model.LimitTimerBuy_EndTime != null)&&(model.LimitTimerBuy_EndTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@LimitTimerBuy_EndTime",model.LimitTimerBuy_EndTime));
			if (model.LimitTimerBuy_TimePrice != null) ParameterToArrayList.Add(new SqlParameter("@LimitTimerBuy_TimePrice",model.LimitTimerBuy_TimePrice));
			if (model.LimitTimerBuy_Bool != null) ParameterToArrayList.Add(new SqlParameter("@LimitTimerBuy_Bool",model.LimitTimerBuy_Bool));
			if (model.MinOrderNum != null) ParameterToArrayList.Add(new SqlParameter("@MinOrderNum",model.MinOrderNum));
			if (model.MaxOrderNum != null) ParameterToArrayList.Add(new SqlParameter("@MaxOrderNum",model.MaxOrderNum));
			if (model.LimitTimerBuy_MaxSalesCount != null) ParameterToArrayList.Add(new SqlParameter("@LimitTimerBuy_MaxSalesCount",model.LimitTimerBuy_MaxSalesCount));
			if (model.Shopping_Vouchers != null) ParameterToArrayList.Add(new SqlParameter("@Shopping_Vouchers",model.Shopping_Vouchers));
			if (model.IS_Admin_check != null) ParameterToArrayList.Add(new SqlParameter("@IS_Admin_check",model.IS_Admin_check));
			if (model.CheckBox_WeiBai_RedMoney != null) ParameterToArrayList.Add(new SqlParameter("@CheckBox_WeiBai_RedMoney",model.CheckBox_WeiBai_RedMoney));
			if (model.Webuy8_DistributionMoney_Value != null) ParameterToArrayList.Add(new SqlParameter("@Webuy8_DistributionMoney_Value",model.Webuy8_DistributionMoney_Value));
			if (model.FreightTemplate_ID != null) ParameterToArrayList.Add(new SqlParameter("@FreightTemplate_ID",model.FreightTemplate_ID));
			if (String.IsNullOrEmpty(model.XML) == false) ParameterToArrayList.Add(new SqlParameter("@XML",model.XML));
			if (model.AgentPercent != null) ParameterToArrayList.Add(new SqlParameter("@AgentPercent",model.AgentPercent));
			if (model.Shopping_Vouchers_Percent != null) ParameterToArrayList.Add(new SqlParameter("@Shopping_Vouchers_Percent",model.Shopping_Vouchers_Percent));
			if (model.WealthMoney != null) ParameterToArrayList.Add(new SqlParameter("@WealthMoney",model.WealthMoney));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if (model.Send_Vouchers_IfBuy != null) ParameterToArrayList.Add(new SqlParameter("@Send_Vouchers_IfBuy",model.Send_Vouchers_IfBuy));
			if (model.Send_Money_IfBuy != null) ParameterToArrayList.Add(new SqlParameter("@Send_Money_IfBuy",model.Send_Money_IfBuy));
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
           strSql.Append(" FROM   [tab_Goods]  where 1 > 0 " + strWhere);
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
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_Goods]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
