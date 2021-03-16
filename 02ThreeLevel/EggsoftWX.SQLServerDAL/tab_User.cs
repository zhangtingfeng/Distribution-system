using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_User。
    /// </summary>
    public class tab_User:Itab_User
        {
        public tab_User()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_User]");
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
		public Int32 Add(EggsoftWX.Model.tab_User model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_User");
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
			strSql.Append("insert into [tab_User]");
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
			strSql.Append("select count(1) from [tab_User] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_User] where ID=" + ID + "");
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
			strSql.Append("select count(1) from [tab_User] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_User] ");
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
			strSql.Append("select " + strItem + " from [tab_User] ");
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
                strSql.Append("select " + fields + " from [tab_User] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_User] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_User] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_User GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_User] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_User model=new EggsoftWX.Model.tab_User();
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.ContactMan=ds.Tables[0].Rows[0]["ContactMan"].ToString();
				model.ContactPhone=ds.Tables[0].Rows[0]["ContactPhone"].ToString();
				model.UserRealName=ds.Tables[0].Rows[0]["UserRealName"].ToString();
				model.Country=ds.Tables[0].Rows[0]["Country"].ToString();
				model.Sheng=ds.Tables[0].Rows[0]["Sheng"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Area=ds.Tables[0].Rows[0]["Area"].ToString();
				model.PostCode=ds.Tables[0].Rows[0]["PostCode"].ToString();
				if(ds.Tables[0].Rows[0]["Sex"].ToString()!="")
				{
					model.Sex=Convert.ToBoolean(ds.Tables[0].Rows[0]["Sex"].ToString());
				}
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				model.IDCard=ds.Tables[0].Rows[0]["IDCard"].ToString();
				if(ds.Tables[0].Rows[0]["Default_Address"].ToString()!="")
				{
					model.Default_Address=Int32.Parse(ds.Tables[0].Rows[0]["Default_Address"].ToString());
				}
				model.OpenID=ds.Tables[0].Rows[0]["OpenID"].ToString();
				model.unionID=ds.Tables[0].Rows[0]["unionID"].ToString();
				model.SmallProgramOpenID=ds.Tables[0].Rows[0]["SmallProgramOpenID"].ToString();
				model.HeadImageUrl=ds.Tables[0].Rows[0]["HeadImageUrl"].ToString();
				if(ds.Tables[0].Rows[0]["Api_Authorize"].ToString()!="")
				{
					model.Api_Authorize=Convert.ToBoolean(ds.Tables[0].Rows[0]["Api_Authorize"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Subscribe"].ToString()!="")
				{
					model.Subscribe=Convert.ToBoolean(ds.Tables[0].Rows[0]["Subscribe"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IFShowCityHelp"].ToString()!="")
				{
					model.IFShowCityHelp=Convert.ToBoolean(ds.Tables[0].Rows[0]["IFShowCityHelp"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RemainingSum"].ToString()!="")
				{
					model.RemainingSum=decimal.Parse(ds.Tables[0].Rows[0]["RemainingSum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IFSendWeiBaiQuan"].ToString()!="")
				{
					model.IFSendWeiBaiQuan=Convert.ToBoolean(ds.Tables[0].Rows[0]["IFSendWeiBaiQuan"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IFSendWeiBaiQuan_LiuZong"].ToString()!="")
				{
					model.IFSendWeiBaiQuan_LiuZong=Convert.ToBoolean(ds.Tables[0].Rows[0]["IFSendWeiBaiQuan_LiuZong"].ToString());
				}
				model.SocialPlatform=ds.Tables[0].Rows[0]["SocialPlatform"].ToString();
				if(ds.Tables[0].Rows[0]["ShopClientID"].ToString()!="")
				{
					model.ShopClientID=Int32.Parse(ds.Tables[0].Rows[0]["ShopClientID"].ToString());
				}
				model.AlipayNumOrWeiXinPay=ds.Tables[0].Rows[0]["AlipayNumOrWeiXinPay"].ToString();
				if(ds.Tables[0].Rows[0]["ShopUserID"].ToString()!="")
				{
					model.ShopUserID=Int32.Parse(ds.Tables[0].Rows[0]["ShopUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					model.ParentID=Int32.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TeamID"].ToString()!="")
				{
					model.TeamID=Int32.Parse(ds.Tables[0].Rows[0]["TeamID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HowToGetProduct"].ToString()!="")
				{
					model.HowToGetProduct=int.Parse(ds.Tables[0].Rows[0]["HowToGetProduct"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DefaultO2OShop"].ToString()!="")
				{
					model.DefaultO2OShop=int.Parse(ds.Tables[0].Rows[0]["DefaultO2OShop"].ToString());
				}
				if(ds.Tables[0].Rows[0]["multi_DuoKeFu_Lastupdatetime"].ToString()!="")
				{
					model.multi_DuoKeFu_Lastupdatetime=DateTime.Parse(ds.Tables[0].Rows[0]["multi_DuoKeFu_Lastupdatetime"].ToString());
				}
				model.Password=ds.Tables[0].Rows[0]["Password"].ToString();
				model.UserAccount=ds.Tables[0].Rows[0]["UserAccount"].ToString();
				if(ds.Tables[0].Rows[0]["InsertTime"].ToString()!="")
				{
					model.InsertTime=DateTime.Parse(ds.Tables[0].Rows[0]["InsertTime"].ToString());
				}
				model.SafeCode=ds.Tables[0].Rows[0]["SafeCode"].ToString();
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Updatetime"].ToString()!="")
				{
					model.Updatetime=DateTime.Parse(ds.Tables[0].Rows[0]["Updatetime"].ToString());
				}
				model.CreateBy=ds.Tables[0].Rows[0]["CreateBy"].ToString();
				model.UpdateBy=ds.Tables[0].Rows[0]["UpdateBy"].ToString();
				model.NickName=ds.Tables[0].Rows[0]["NickName"].ToString();
				if(ds.Tables[0].Rows[0]["Isdeleted"].ToString()!="")
				{
					model.Isdeleted=Int32.Parse(ds.Tables[0].Rows[0]["Isdeleted"].ToString());
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
		public EggsoftWX.Model.tab_User GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_User] ");
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
				strSql.Append("delete from [tab_User] ");
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
			strSql.Append("update [tab_User] set ");
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
		public int Update(EggsoftWX.Model.tab_User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_User] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (String.IsNullOrEmpty(model.ContactMan) == false) strSql.Append("[ContactMan]=@ContactMan,");
			if (String.IsNullOrEmpty(model.ContactPhone) == false) strSql.Append("[ContactPhone]=@ContactPhone,");
			if (String.IsNullOrEmpty(model.UserRealName) == false) strSql.Append("[UserRealName]=@UserRealName,");
			if (String.IsNullOrEmpty(model.Country) == false) strSql.Append("[Country]=@Country,");
			if (String.IsNullOrEmpty(model.Sheng) == false) strSql.Append("[Sheng]=@Sheng,");
			if (String.IsNullOrEmpty(model.City) == false) strSql.Append("[City]=@City,");
			if (String.IsNullOrEmpty(model.Area) == false) strSql.Append("[Area]=@Area,");
			if (String.IsNullOrEmpty(model.PostCode) == false) strSql.Append("[PostCode]=@PostCode,");
			if (model.Sex != null) strSql.Append("[Sex]=@Sex,");
			if (String.IsNullOrEmpty(model.Email) == false) strSql.Append("[Email]=@Email,");
			if (String.IsNullOrEmpty(model.Address) == false) strSql.Append("[Address]=@Address,");
			if (String.IsNullOrEmpty(model.IDCard) == false) strSql.Append("[IDCard]=@IDCard,");
			if (model.Default_Address != null) strSql.Append("[Default_Address]=@Default_Address,");
			if (String.IsNullOrEmpty(model.OpenID) == false) strSql.Append("[OpenID]=@OpenID,");
			if (String.IsNullOrEmpty(model.unionID) == false) strSql.Append("[unionID]=@unionID,");
			if (String.IsNullOrEmpty(model.SmallProgramOpenID) == false) strSql.Append("[SmallProgramOpenID]=@SmallProgramOpenID,");
			if (String.IsNullOrEmpty(model.HeadImageUrl) == false) strSql.Append("[HeadImageUrl]=@HeadImageUrl,");
			if (model.Api_Authorize != null) strSql.Append("[Api_Authorize]=@Api_Authorize,");
			if (model.Subscribe != null) strSql.Append("[Subscribe]=@Subscribe,");
			if (model.IFShowCityHelp != null) strSql.Append("[IFShowCityHelp]=@IFShowCityHelp,");
			if (model.RemainingSum != null) strSql.Append("[RemainingSum]=@RemainingSum,");
			if (model.IFSendWeiBaiQuan != null) strSql.Append("[IFSendWeiBaiQuan]=@IFSendWeiBaiQuan,");
			if (model.IFSendWeiBaiQuan_LiuZong != null) strSql.Append("[IFSendWeiBaiQuan_LiuZong]=@IFSendWeiBaiQuan_LiuZong,");
			if (String.IsNullOrEmpty(model.SocialPlatform) == false) strSql.Append("[SocialPlatform]=@SocialPlatform,");
			if (model.ShopClientID != null) strSql.Append("[ShopClientID]=@ShopClientID,");
			if (String.IsNullOrEmpty(model.AlipayNumOrWeiXinPay) == false) strSql.Append("[AlipayNumOrWeiXinPay]=@AlipayNumOrWeiXinPay,");
			if (model.ShopUserID != null) strSql.Append("[ShopUserID]=@ShopUserID,");
			if (model.ParentID != null) strSql.Append("[ParentID]=@ParentID,");
			if (model.TeamID != null) strSql.Append("[TeamID]=@TeamID,");
			if (model.HowToGetProduct != null) strSql.Append("[HowToGetProduct]=@HowToGetProduct,");
			if (model.DefaultO2OShop != null) strSql.Append("[DefaultO2OShop]=@DefaultO2OShop,");
			if ((model.multi_DuoKeFu_Lastupdatetime != null)&&(model.multi_DuoKeFu_Lastupdatetime != DateTime.MinValue)) strSql.Append("[multi_DuoKeFu_Lastupdatetime]=@multi_DuoKeFu_Lastupdatetime,");
			if (String.IsNullOrEmpty(model.Password) == false) strSql.Append("[Password]=@Password,");
			if (String.IsNullOrEmpty(model.UserAccount) == false) strSql.Append("[UserAccount]=@UserAccount,");
			if ((model.InsertTime != null)&&(model.InsertTime != DateTime.MinValue)) strSql.Append("[InsertTime]=@InsertTime,");
			if (String.IsNullOrEmpty(model.SafeCode) == false) strSql.Append("[SafeCode]=@SafeCode,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if ((model.Updatetime != null)&&(model.Updatetime != DateTime.MinValue)) strSql.Append("[Updatetime]=@Updatetime,");
			if (String.IsNullOrEmpty(model.CreateBy) == false) strSql.Append("[CreateBy]=@CreateBy,");
			if (String.IsNullOrEmpty(model.UpdateBy) == false) strSql.Append("[UpdateBy]=@UpdateBy,");
			if (String.IsNullOrEmpty(model.NickName) == false) strSql.Append("[NickName]=@NickName,");
			if (model.Isdeleted != null) strSql.Append("[Isdeleted]=@Isdeleted,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (String.IsNullOrEmpty(model.ContactMan) == false) ParameterToArrayList.Add(new SqlParameter("@ContactMan",model.ContactMan));
			if (String.IsNullOrEmpty(model.ContactPhone) == false) ParameterToArrayList.Add(new SqlParameter("@ContactPhone",model.ContactPhone));
			if (String.IsNullOrEmpty(model.UserRealName) == false) ParameterToArrayList.Add(new SqlParameter("@UserRealName",model.UserRealName));
			if (String.IsNullOrEmpty(model.Country) == false) ParameterToArrayList.Add(new SqlParameter("@Country",model.Country));
			if (String.IsNullOrEmpty(model.Sheng) == false) ParameterToArrayList.Add(new SqlParameter("@Sheng",model.Sheng));
			if (String.IsNullOrEmpty(model.City) == false) ParameterToArrayList.Add(new SqlParameter("@City",model.City));
			if (String.IsNullOrEmpty(model.Area) == false) ParameterToArrayList.Add(new SqlParameter("@Area",model.Area));
			if (String.IsNullOrEmpty(model.PostCode) == false) ParameterToArrayList.Add(new SqlParameter("@PostCode",model.PostCode));
			if (model.Sex != null) ParameterToArrayList.Add(new SqlParameter("@Sex",model.Sex));
			if (String.IsNullOrEmpty(model.Email) == false) ParameterToArrayList.Add(new SqlParameter("@Email",model.Email));
			if (String.IsNullOrEmpty(model.Address) == false) ParameterToArrayList.Add(new SqlParameter("@Address",model.Address));
			if (String.IsNullOrEmpty(model.IDCard) == false) ParameterToArrayList.Add(new SqlParameter("@IDCard",model.IDCard));
			if (model.Default_Address != null) ParameterToArrayList.Add(new SqlParameter("@Default_Address",model.Default_Address));
			if (String.IsNullOrEmpty(model.OpenID) == false) ParameterToArrayList.Add(new SqlParameter("@OpenID",model.OpenID));
			if (String.IsNullOrEmpty(model.unionID) == false) ParameterToArrayList.Add(new SqlParameter("@unionID",model.unionID));
			if (String.IsNullOrEmpty(model.SmallProgramOpenID) == false) ParameterToArrayList.Add(new SqlParameter("@SmallProgramOpenID",model.SmallProgramOpenID));
			if (String.IsNullOrEmpty(model.HeadImageUrl) == false) ParameterToArrayList.Add(new SqlParameter("@HeadImageUrl",model.HeadImageUrl));
			if (model.Api_Authorize != null) ParameterToArrayList.Add(new SqlParameter("@Api_Authorize",model.Api_Authorize));
			if (model.Subscribe != null) ParameterToArrayList.Add(new SqlParameter("@Subscribe",model.Subscribe));
			if (model.IFShowCityHelp != null) ParameterToArrayList.Add(new SqlParameter("@IFShowCityHelp",model.IFShowCityHelp));
			if (model.RemainingSum != null) ParameterToArrayList.Add(new SqlParameter("@RemainingSum",model.RemainingSum));
			if (model.IFSendWeiBaiQuan != null) ParameterToArrayList.Add(new SqlParameter("@IFSendWeiBaiQuan",model.IFSendWeiBaiQuan));
			if (model.IFSendWeiBaiQuan_LiuZong != null) ParameterToArrayList.Add(new SqlParameter("@IFSendWeiBaiQuan_LiuZong",model.IFSendWeiBaiQuan_LiuZong));
			if (String.IsNullOrEmpty(model.SocialPlatform) == false) ParameterToArrayList.Add(new SqlParameter("@SocialPlatform",model.SocialPlatform));
			if (model.ShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClientID",model.ShopClientID));
			if (String.IsNullOrEmpty(model.AlipayNumOrWeiXinPay) == false) ParameterToArrayList.Add(new SqlParameter("@AlipayNumOrWeiXinPay",model.AlipayNumOrWeiXinPay));
			if (model.ShopUserID != null) ParameterToArrayList.Add(new SqlParameter("@ShopUserID",model.ShopUserID));
			if (model.ParentID != null) ParameterToArrayList.Add(new SqlParameter("@ParentID",model.ParentID));
			if (model.TeamID != null) ParameterToArrayList.Add(new SqlParameter("@TeamID",model.TeamID));
			if (model.HowToGetProduct != null) ParameterToArrayList.Add(new SqlParameter("@HowToGetProduct",model.HowToGetProduct));
			if (model.DefaultO2OShop != null) ParameterToArrayList.Add(new SqlParameter("@DefaultO2OShop",model.DefaultO2OShop));
			if ((model.multi_DuoKeFu_Lastupdatetime != null)&&(model.multi_DuoKeFu_Lastupdatetime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@multi_DuoKeFu_Lastupdatetime",model.multi_DuoKeFu_Lastupdatetime));
			if (String.IsNullOrEmpty(model.Password) == false) ParameterToArrayList.Add(new SqlParameter("@Password",model.Password));
			if (String.IsNullOrEmpty(model.UserAccount) == false) ParameterToArrayList.Add(new SqlParameter("@UserAccount",model.UserAccount));
			if ((model.InsertTime != null)&&(model.InsertTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@InsertTime",model.InsertTime));
			if (String.IsNullOrEmpty(model.SafeCode) == false) ParameterToArrayList.Add(new SqlParameter("@SafeCode",model.SafeCode));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if ((model.Updatetime != null)&&(model.Updatetime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@Updatetime",model.Updatetime));
			if (String.IsNullOrEmpty(model.CreateBy) == false) ParameterToArrayList.Add(new SqlParameter("@CreateBy",model.CreateBy));
			if (String.IsNullOrEmpty(model.UpdateBy) == false) ParameterToArrayList.Add(new SqlParameter("@UpdateBy",model.UpdateBy));
			if (String.IsNullOrEmpty(model.NickName) == false) ParameterToArrayList.Add(new SqlParameter("@NickName",model.NickName));
			if (model.Isdeleted != null) ParameterToArrayList.Add(new SqlParameter("@Isdeleted",model.Isdeleted));
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
           strSql.Append(" FROM   [tab_User]  where 1 > 0 " + strWhere);
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
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_User]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
