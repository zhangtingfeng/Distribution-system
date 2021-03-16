using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_ShopClient_ShopPar。
    /// </summary>
    public class tab_ShopClient_ShopPar:Itab_ShopClient_ShopPar
        {
        public tab_ShopClient_ShopPar()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_ShopClient_ShopPar]");
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
		public Int32 Add(EggsoftWX.Model.tab_ShopClient_ShopPar model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_ShopClient_ShopPar");
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
			strSql.Append("insert into [tab_ShopClient_ShopPar]");
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
			strSql.Append("select count(1) from [tab_ShopClient_ShopPar] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_ShopClient_ShopPar] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_ShopClient_ShopPar] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_ShopClient_ShopPar] ");
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
			strSql.Append("select " + strItem + " from [tab_ShopClient_ShopPar] ");
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
                strSql.Append("select " + fields + " from [tab_ShopClient_ShopPar] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_ShopClient_ShopPar] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_ShopClient_ShopPar] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_ShopClient_ShopPar GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_ShopClient_ShopPar] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_ShopClient_ShopPar model=new EggsoftWX.Model.tab_ShopClient_ShopPar();
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopClientID"].ToString()!="")
				{
					model.ShopClientID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClientID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopShareGiveMoney"].ToString()!="")
				{
					model.ShopShareGiveMoney=decimal.Parse(ds.Tables[0].Rows[0]["ShopShareGiveMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShopShareGiveVouchers"].ToString()!="")
				{
					model.ShopShareGiveVouchers=decimal.Parse(ds.Tables[0].Rows[0]["ShopShareGiveVouchers"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodShareGiveMoney"].ToString()!="")
				{
					model.GoodShareGiveMoney=decimal.Parse(ds.Tables[0].Rows[0]["GoodShareGiveMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodShareGiveVouchers"].ToString()!="")
				{
					model.GoodShareGiveVouchers=decimal.Parse(ds.Tables[0].Rows[0]["GoodShareGiveVouchers"].ToString());
				}
				model.AddExpListTextShow=ds.Tables[0].Rows[0]["AddExpListTextShow"].ToString();
				if(ds.Tables[0].Rows[0]["SubscribeGiveMoney"].ToString()!="")
				{
					model.SubscribeGiveMoney=decimal.Parse(ds.Tables[0].Rows[0]["SubscribeGiveMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubscribeGiveVouchers"].ToString()!="")
				{
					model.SubscribeGiveVouchers=decimal.Parse(ds.Tables[0].Rows[0]["SubscribeGiveVouchers"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AskAgentAuto"].ToString()!="")
				{
					model.AskAgentAuto=Convert.ToBoolean(ds.Tables[0].Rows[0]["AskAgentAuto"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AskAgentAutoAfterBuy"].ToString()!="")
				{
					model.AskAgentAutoAfterBuy=Convert.ToBoolean(ds.Tables[0].Rows[0]["AskAgentAutoAfterBuy"].ToString());
				}
				model.SubscribeTipInfo=ds.Tables[0].Rows[0]["SubscribeTipInfo"].ToString();
				if(ds.Tables[0].Rows[0]["GouWuQuan_FirstVisitShop"].ToString()!="")
				{
					model.GouWuQuan_FirstVisitShop=decimal.Parse(ds.Tables[0].Rows[0]["GouWuQuan_FirstVisitShop"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Money_FirstVisitShop"].ToString()!="")
				{
					model.Money_FirstVisitShop=decimal.Parse(ds.Tables[0].Rows[0]["Money_FirstVisitShop"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PayMoneyMustHaveAddress"].ToString()!="")
				{
					model.PayMoneyMustHaveAddress=Convert.ToBoolean(ds.Tables[0].Rows[0]["PayMoneyMustHaveAddress"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DeafaultOnlyShowAnounceBitmap"].ToString()!="")
				{
					model.DeafaultOnlyShowAnounceBitmap=Convert.ToBoolean(ds.Tables[0].Rows[0]["DeafaultOnlyShowAnounceBitmap"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LimitMoney_AskMoney"].ToString()!="")
				{
					model.LimitMoney_AskMoney=decimal.Parse(ds.Tables[0].Rows[0]["LimitMoney_AskMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GiveMoneyAfterOntime"].ToString()!="")
				{
					model.GiveMoneyAfterOntime=Convert.ToBoolean(ds.Tables[0].Rows[0]["GiveMoneyAfterOntime"].ToString());
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
					model.IsDeleted=Int32.Parse(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
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
		public EggsoftWX.Model.tab_ShopClient_ShopPar GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_ShopClient_ShopPar] ");
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
				strSql.Append("delete from [tab_ShopClient_ShopPar] ");
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
			strSql.Append("update [tab_ShopClient_ShopPar] set ");
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
		public int Update(EggsoftWX.Model.tab_ShopClient_ShopPar model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_ShopClient_ShopPar] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (model.ShopClientID != null) strSql.Append("[ShopClientID]=@ShopClientID,");
			if (model.ShopShareGiveMoney != null) strSql.Append("[ShopShareGiveMoney]=@ShopShareGiveMoney,");
			if (model.ShopShareGiveVouchers != null) strSql.Append("[ShopShareGiveVouchers]=@ShopShareGiveVouchers,");
			if (model.GoodShareGiveMoney != null) strSql.Append("[GoodShareGiveMoney]=@GoodShareGiveMoney,");
			if (model.GoodShareGiveVouchers != null) strSql.Append("[GoodShareGiveVouchers]=@GoodShareGiveVouchers,");
			if (String.IsNullOrEmpty(model.AddExpListTextShow) == false) strSql.Append("[AddExpListTextShow]=@AddExpListTextShow,");
			if (model.SubscribeGiveMoney != null) strSql.Append("[SubscribeGiveMoney]=@SubscribeGiveMoney,");
			if (model.SubscribeGiveVouchers != null) strSql.Append("[SubscribeGiveVouchers]=@SubscribeGiveVouchers,");
			if (model.AskAgentAuto != null) strSql.Append("[AskAgentAuto]=@AskAgentAuto,");
			if (model.AskAgentAutoAfterBuy != null) strSql.Append("[AskAgentAutoAfterBuy]=@AskAgentAutoAfterBuy,");
			if (String.IsNullOrEmpty(model.SubscribeTipInfo) == false) strSql.Append("[SubscribeTipInfo]=@SubscribeTipInfo,");
			if (model.GouWuQuan_FirstVisitShop != null) strSql.Append("[GouWuQuan_FirstVisitShop]=@GouWuQuan_FirstVisitShop,");
			if (model.Money_FirstVisitShop != null) strSql.Append("[Money_FirstVisitShop]=@Money_FirstVisitShop,");
			if (model.PayMoneyMustHaveAddress != null) strSql.Append("[PayMoneyMustHaveAddress]=@PayMoneyMustHaveAddress,");
			if (model.DeafaultOnlyShowAnounceBitmap != null) strSql.Append("[DeafaultOnlyShowAnounceBitmap]=@DeafaultOnlyShowAnounceBitmap,");
			if (model.LimitMoney_AskMoney != null) strSql.Append("[LimitMoney_AskMoney]=@LimitMoney_AskMoney,");
			if (model.GiveMoneyAfterOntime != null) strSql.Append("[GiveMoneyAfterOntime]=@GiveMoneyAfterOntime,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if (String.IsNullOrEmpty(model.CreateBy) == false) strSql.Append("[CreateBy]=@CreateBy,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if (String.IsNullOrEmpty(model.UpdateBy) == false) strSql.Append("[UpdateBy]=@UpdateBy,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (model.ShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClientID",model.ShopClientID));
			if (model.ShopShareGiveMoney != null) ParameterToArrayList.Add(new SqlParameter("@ShopShareGiveMoney",model.ShopShareGiveMoney));
			if (model.ShopShareGiveVouchers != null) ParameterToArrayList.Add(new SqlParameter("@ShopShareGiveVouchers",model.ShopShareGiveVouchers));
			if (model.GoodShareGiveMoney != null) ParameterToArrayList.Add(new SqlParameter("@GoodShareGiveMoney",model.GoodShareGiveMoney));
			if (model.GoodShareGiveVouchers != null) ParameterToArrayList.Add(new SqlParameter("@GoodShareGiveVouchers",model.GoodShareGiveVouchers));
			if (String.IsNullOrEmpty(model.AddExpListTextShow) == false) ParameterToArrayList.Add(new SqlParameter("@AddExpListTextShow",model.AddExpListTextShow));
			if (model.SubscribeGiveMoney != null) ParameterToArrayList.Add(new SqlParameter("@SubscribeGiveMoney",model.SubscribeGiveMoney));
			if (model.SubscribeGiveVouchers != null) ParameterToArrayList.Add(new SqlParameter("@SubscribeGiveVouchers",model.SubscribeGiveVouchers));
			if (model.AskAgentAuto != null) ParameterToArrayList.Add(new SqlParameter("@AskAgentAuto",model.AskAgentAuto));
			if (model.AskAgentAutoAfterBuy != null) ParameterToArrayList.Add(new SqlParameter("@AskAgentAutoAfterBuy",model.AskAgentAutoAfterBuy));
			if (String.IsNullOrEmpty(model.SubscribeTipInfo) == false) ParameterToArrayList.Add(new SqlParameter("@SubscribeTipInfo",model.SubscribeTipInfo));
			if (model.GouWuQuan_FirstVisitShop != null) ParameterToArrayList.Add(new SqlParameter("@GouWuQuan_FirstVisitShop",model.GouWuQuan_FirstVisitShop));
			if (model.Money_FirstVisitShop != null) ParameterToArrayList.Add(new SqlParameter("@Money_FirstVisitShop",model.Money_FirstVisitShop));
			if (model.PayMoneyMustHaveAddress != null) ParameterToArrayList.Add(new SqlParameter("@PayMoneyMustHaveAddress",model.PayMoneyMustHaveAddress));
			if (model.DeafaultOnlyShowAnounceBitmap != null) ParameterToArrayList.Add(new SqlParameter("@DeafaultOnlyShowAnounceBitmap",model.DeafaultOnlyShowAnounceBitmap));
			if (model.LimitMoney_AskMoney != null) ParameterToArrayList.Add(new SqlParameter("@LimitMoney_AskMoney",model.LimitMoney_AskMoney));
			if (model.GiveMoneyAfterOntime != null) ParameterToArrayList.Add(new SqlParameter("@GiveMoneyAfterOntime",model.GiveMoneyAfterOntime));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if (String.IsNullOrEmpty(model.CreateBy) == false) ParameterToArrayList.Add(new SqlParameter("@CreateBy",model.CreateBy));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
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
           strSql.Append(" FROM   [tab_ShopClient_ShopPar]  where 1 > 0 " + strWhere);
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
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_ShopClient_ShopPar]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
