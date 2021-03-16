using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_ShopClient_XianChangHuoDong。
    /// </summary>
    public class tab_ShopClient_XianChangHuoDong:Itab_ShopClient_XianChangHuoDong
        {
        public tab_ShopClient_XianChangHuoDong()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_ShopClient_XianChangHuoDong]");
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
		public Int32 Add(EggsoftWX.Model.tab_ShopClient_XianChangHuoDong model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_ShopClient_XianChangHuoDong");
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
			strSql.Append("insert into [tab_ShopClient_XianChangHuoDong]");
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
			strSql.Append("select count(1) from [tab_ShopClient_XianChangHuoDong] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_ShopClient_XianChangHuoDong] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_ShopClient_XianChangHuoDong] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_ShopClient_XianChangHuoDong] ");
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
			strSql.Append("select " + strItem + " from [tab_ShopClient_XianChangHuoDong] ");
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
                strSql.Append("select " + fields + " from [tab_ShopClient_XianChangHuoDong] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_ShopClient_XianChangHuoDong] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_ShopClient_XianChangHuoDong] where 1>0 " + strWhere);
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_ShopClient_XianChangHuoDong GetModel(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_ShopClient_XianChangHuoDong] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_ShopClient_XianChangHuoDong model=new EggsoftWX.Model.tab_ShopClient_XianChangHuoDong();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.ActivityName=ds.Tables[0].Rows[0]["ActivityName"].ToString();
				model.PassWord=ds.Tables[0].Rows[0]["PassWord"].ToString();
				if(ds.Tables[0].Rows[0]["ShopClientID"].ToString()!="")
				{
					model.ShopClientID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClientID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShowAgentErWeiMa_UserID_ByAgent"].ToString()!="")
				{
					model.ShowAgentErWeiMa_UserID_ByAgent=Int32.Parse(ds.Tables[0].Rows[0]["ShowAgentErWeiMa_UserID_ByAgent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Subscribe_Must"].ToString()!="")
				{
					model.Subscribe_Must=Convert.ToBoolean(ds.Tables[0].Rows[0]["Subscribe_Must"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ActivityState"].ToString()!="")
				{
					model.ActivityState=Convert.ToBoolean(ds.Tables[0].Rows[0]["ActivityState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GetBonusRepeat"].ToString()!="")
				{
					model.GetBonusRepeat=Convert.ToBoolean(ds.Tables[0].Rows[0]["GetBonusRepeat"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GetBonusRepeat_OneDrawBonus"].ToString()!="")
				{
					model.GetBonusRepeat_OneDrawBonus=Convert.ToBoolean(ds.Tables[0].Rows[0]["GetBonusRepeat_OneDrawBonus"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Address_Must"].ToString()!="")
				{
					model.Address_Must=Convert.ToBoolean(ds.Tables[0].Rows[0]["Address_Must"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=Int32.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				model.Background_PIC_BigScreen=ds.Tables[0].Rows[0]["Background_PIC_BigScreen"].ToString();
				model.Background_SoundPath=ds.Tables[0].Rows[0]["Background_SoundPath"].ToString();
				if(ds.Tables[0].Rows[0]["LongShakeTime"].ToString()!="")
				{
					model.LongShakeTime=Int32.Parse(ds.Tables[0].Rows[0]["LongShakeTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CountHowMany"].ToString()!="")
				{
					model.CountHowMany=Int32.Parse(ds.Tables[0].Rows[0]["CountHowMany"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MaxTracks"].ToString()!="")
				{
					model.MaxTracks=Int32.Parse(ds.Tables[0].Rows[0]["MaxTracks"].ToString());
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
		public EggsoftWX.Model.tab_ShopClient_XianChangHuoDong GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_ShopClient_XianChangHuoDong] ");
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
				strSql.Append("delete from [tab_ShopClient_XianChangHuoDong] ");
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
			strSql.Append("update [tab_ShopClient_XianChangHuoDong] set ");
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
		public void Update(EggsoftWX.Model.tab_ShopClient_XianChangHuoDong model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_ShopClient_XianChangHuoDong] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (String.IsNullOrEmpty(model.ActivityName) == false) strSql.Append("[ActivityName]=@ActivityName,");
			if (String.IsNullOrEmpty(model.PassWord) == false) strSql.Append("[PassWord]=@PassWord,");
			if (model.ShopClientID != null) strSql.Append("[ShopClientID]=@ShopClientID,");
			if (model.ShowAgentErWeiMa_UserID_ByAgent != null) strSql.Append("[ShowAgentErWeiMa_UserID_ByAgent]=@ShowAgentErWeiMa_UserID_ByAgent,");
			if (model.Subscribe_Must != null) strSql.Append("[Subscribe_Must]=@Subscribe_Must,");
			if (model.ActivityState != null) strSql.Append("[ActivityState]=@ActivityState,");
			if (model.GetBonusRepeat != null) strSql.Append("[GetBonusRepeat]=@GetBonusRepeat,");
			if (model.GetBonusRepeat_OneDrawBonus != null) strSql.Append("[GetBonusRepeat_OneDrawBonus]=@GetBonusRepeat_OneDrawBonus,");
			if (model.Address_Must != null) strSql.Append("[Address_Must]=@Address_Must,");
			if (model.Sort != null) strSql.Append("[Sort]=@Sort,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			if ((model.CreateTime != null)&&(model.CreateTime != DateTime.MinValue)) strSql.Append("[CreateTime]=@CreateTime,");
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
			if (String.IsNullOrEmpty(model.Background_PIC_BigScreen) == false) strSql.Append("[Background_PIC_BigScreen]=@Background_PIC_BigScreen,");
			if (String.IsNullOrEmpty(model.Background_SoundPath) == false) strSql.Append("[Background_SoundPath]=@Background_SoundPath,");
			if (model.LongShakeTime != null) strSql.Append("[LongShakeTime]=@LongShakeTime,");
			if (model.CountHowMany != null) strSql.Append("[CountHowMany]=@CountHowMany,");
			if (model.MaxTracks != null) strSql.Append("[MaxTracks]=@MaxTracks,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (String.IsNullOrEmpty(model.ActivityName) == false) ParameterToArrayList.Add(new SqlParameter("@ActivityName",model.ActivityName));
			if (String.IsNullOrEmpty(model.PassWord) == false) ParameterToArrayList.Add(new SqlParameter("@PassWord",model.PassWord));
			if (model.ShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClientID",model.ShopClientID));
			if (model.ShowAgentErWeiMa_UserID_ByAgent != null) ParameterToArrayList.Add(new SqlParameter("@ShowAgentErWeiMa_UserID_ByAgent",model.ShowAgentErWeiMa_UserID_ByAgent));
			if (model.Subscribe_Must != null) ParameterToArrayList.Add(new SqlParameter("@Subscribe_Must",model.Subscribe_Must));
			if (model.ActivityState != null) ParameterToArrayList.Add(new SqlParameter("@ActivityState",model.ActivityState));
			if (model.GetBonusRepeat != null) ParameterToArrayList.Add(new SqlParameter("@GetBonusRepeat",model.GetBonusRepeat));
			if (model.GetBonusRepeat_OneDrawBonus != null) ParameterToArrayList.Add(new SqlParameter("@GetBonusRepeat_OneDrawBonus",model.GetBonusRepeat_OneDrawBonus));
			if (model.Address_Must != null) ParameterToArrayList.Add(new SqlParameter("@Address_Must",model.Address_Must));
			if (model.Sort != null) ParameterToArrayList.Add(new SqlParameter("@Sort",model.Sort));
			if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted",model.IsDeleted));
			if ((model.CreateTime != null)&&(model.CreateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreateTime",model.CreateTime));
			if ((model.UpdateTime != null)&&(model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime",model.UpdateTime));
			if (String.IsNullOrEmpty(model.Background_PIC_BigScreen) == false) ParameterToArrayList.Add(new SqlParameter("@Background_PIC_BigScreen",model.Background_PIC_BigScreen));
			if (String.IsNullOrEmpty(model.Background_SoundPath) == false) ParameterToArrayList.Add(new SqlParameter("@Background_SoundPath",model.Background_SoundPath));
			if (model.LongShakeTime != null) ParameterToArrayList.Add(new SqlParameter("@LongShakeTime",model.LongShakeTime));
			if (model.CountHowMany != null) ParameterToArrayList.Add(new SqlParameter("@CountHowMany",model.CountHowMany));
			if (model.MaxTracks != null) ParameterToArrayList.Add(new SqlParameter("@MaxTracks",model.MaxTracks));
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
           strSql.Append(" FROM   [tab_ShopClient_XianChangHuoDong]  where 1 > 0 " + strWhere);
           return DbHelperSQL.GetDataTable(strSql.ToString());
       }

         #endregion  成员方法
       #region 大数据量快速分页,50万以上数据分页 memo 0020
         //大数据量快速分页,50万以上数据分页  memo 0020
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc)
        {
           strConditions = strConditions.ToLower();
           if ((strConditions.IndexOf("and") == -1) && (strConditions!="")) strConditions = "and " + strConditions;
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_ShopClient_XianChangHuoDong]", strConditions, orderField, isDesc);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
