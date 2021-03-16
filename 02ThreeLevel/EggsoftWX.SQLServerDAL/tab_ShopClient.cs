using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_ShopClient。
    /// </summary>
    public class tab_ShopClient:Itab_ShopClient
        {
        public tab_ShopClient()
        {}
        #region  成员方法
            
       #region 得到最大ID  memo 0001
       /// <summary>
		/// 得到最大ID
		/// </summary>
		public Int32 GetMaxId()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select max(ID)+1 from [tab_ShopClient]");
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
		public Int32 Add(EggsoftWX.Model.tab_ShopClient model)
		{
			IDataParameter[] iData = new SqlParameter[2];
			iData[0] = new SqlParameter("@TableName", "tab_ShopClient");
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
			strSql.Append("insert into [tab_ShopClient]");
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
			strSql.Append("select count(1) from [tab_ShopClient] where 1>0 "+strWhere+" ");
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
            strSql.Append("select count(1) from [tab_ShopClient] where ID=" + ID + "");
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
			strSql.Append("select count(*) from [tab_ShopClient] where 1>0 "+strWhere+" ");
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
            strSql.Append("select * from [tab_ShopClient] ");
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
			strSql.Append("select " + strItem + " from [tab_ShopClient] ");
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
                strSql.Append("select " + fields + " from [tab_ShopClient] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_ShopClient] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_ShopClient] where 1>0 " + strWhere);
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);
        }
       #endregion
       #region 得到一个对象实体 strWhere memo 0013
           /// <summary>
		/// 得到一个对象实体  strWhere
		/// </summary>
		public EggsoftWX.Model.tab_ShopClient GetModel(string strWhere, params object[] objs)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from [tab_ShopClient] ");
			strSql.Append(" where "+strWhere);
			EggsoftWX.Model.tab_ShopClient model=new EggsoftWX.Model.tab_ShopClient();
			SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.ShopClientName=ds.Tables[0].Rows[0]["ShopClientName"].ToString();
				model.ShopClientType=ds.Tables[0].Rows[0]["ShopClientType"].ToString();
				model.ShopClientClass=ds.Tables[0].Rows[0]["ShopClientClass"].ToString();
				model.ShopButton=ds.Tables[0].Rows[0]["ShopButton"].ToString();
				model.ShopBackgroundLogoImage=ds.Tables[0].Rows[0]["ShopBackgroundLogoImage"].ToString();
				model.ContactMan=ds.Tables[0].Rows[0]["ContactMan"].ToString();
				model.ContactPhone=ds.Tables[0].Rows[0]["ContactPhone"].ToString();
				model.ShopMemo=ds.Tables[0].Rows[0]["ShopMemo"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserClientLevel"].ToString()!="")
				{
					model.UserClientLevel=int.Parse(ds.Tables[0].Rows[0]["UserClientLevel"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Updatetime"].ToString()!="")
				{
					model.Updatetime=DateTime.Parse(ds.Tables[0].Rows[0]["Updatetime"].ToString());
				}
				model.UpdateMan=ds.Tables[0].Rows[0]["UpdateMan"].ToString();
				model.Username=ds.Tables[0].Rows[0]["Username"].ToString();
				model.PassWord=ds.Tables[0].Rows[0]["PassWord"].ToString();
				model.UserRealName=ds.Tables[0].Rows[0]["UserRealName"].ToString();
				model.Country=ds.Tables[0].Rows[0]["Country"].ToString();
				model.Sheng=ds.Tables[0].Rows[0]["Sheng"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Area=ds.Tables[0].Rows[0]["Area"].ToString();
				model.PostCode=ds.Tables[0].Rows[0]["PostCode"].ToString();
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				if(ds.Tables[0].Rows[0]["IsLocked"].ToString()!="")
				{
					model.IsLocked=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLocked"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IFCompany"].ToString()!="")
				{
					model.IFCompany=Convert.ToBoolean(ds.Tables[0].Rows[0]["IFCompany"].ToString());
				}
				model.UpLoadPath=ds.Tables[0].Rows[0]["UpLoadPath"].ToString();
				model.UserMemo=ds.Tables[0].Rows[0]["UserMemo"].ToString();
				if(ds.Tables[0].Rows[0]["Sex"].ToString()!="")
				{
					model.Sex=Convert.ToBoolean(ds.Tables[0].Rows[0]["Sex"].ToString());
				}
				model.UserIntergentMark=ds.Tables[0].Rows[0]["UserIntergentMark"].ToString();
				if(ds.Tables[0].Rows[0]["AuthorTime"].ToString()!="")
				{
					model.AuthorTime=DateTime.Parse(ds.Tables[0].Rows[0]["AuthorTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AuthorMoney"].ToString()!="")
				{
					model.AuthorMoney=decimal.Parse(ds.Tables[0].Rows[0]["AuthorMoney"].ToString());
				}
				model.XML=ds.Tables[0].Rows[0]["XML"].ToString();
				if(ds.Tables[0].Rows[0]["ShenMaShopping"].ToString()!="")
				{
					model.ShenMaShopping=Convert.ToBoolean(ds.Tables[0].Rows[0]["ShenMaShopping"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Shopping_Vouchers"].ToString()!="")
				{
					model.Shopping_Vouchers=Convert.ToBoolean(ds.Tables[0].Rows[0]["Shopping_Vouchers"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Shopping_Vouchers_Goods_Percent"].ToString()!="")
				{
					model.Shopping_Vouchers_Goods_Percent=int.Parse(ds.Tables[0].Rows[0]["Shopping_Vouchers_Goods_Percent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PartnerWeiXinID"].ToString()!="")
				{
					model.PartnerWeiXinID=Int32.Parse(ds.Tables[0].Rows[0]["PartnerWeiXinID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RecommendWeiXinID"].ToString()!="")
				{
					model.RecommendWeiXinID=Int32.Parse(ds.Tables[0].Rows[0]["RecommendWeiXinID"].ToString());
				}
				model.QM_QQ_COM_QM_K_32=ds.Tables[0].Rows[0]["QM_QQ_COM_QM_K_32"].ToString();
				if(ds.Tables[0].Rows[0]["Shopping_Vouchers_Money"].ToString()!="")
				{
					model.Shopping_Vouchers_Money=decimal.Parse(ds.Tables[0].Rows[0]["Shopping_Vouchers_Money"].ToString());
				}
				model.ErJiYuMing=ds.Tables[0].Rows[0]["ErJiYuMing"].ToString();
				model.Username_Finance=ds.Tables[0].Rows[0]["Username_Finance"].ToString();
				model.PassWord_Finance=ds.Tables[0].Rows[0]["PassWord_Finance"].ToString();
				if(ds.Tables[0].Rows[0]["Style_Model"].ToString()!="")
				{
					model.Style_Model=int.Parse(ds.Tables[0].Rows[0]["Style_Model"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AutoMidifyAgentGoods"].ToString()!="")
				{
					model.AutoMidifyAgentGoods=Convert.ToBoolean(ds.Tables[0].Rows[0]["AutoMidifyAgentGoods"].ToString());
				}
				model.AgentMustRead=ds.Tables[0].Rows[0]["AgentMustRead"].ToString();
				model.AgentMustReadAd=ds.Tables[0].Rows[0]["AgentMustReadAd"].ToString();
				model.ContactManPostion=ds.Tables[0].Rows[0]["ContactManPostion"].ToString();
				if(ds.Tables[0].Rows[0]["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
				}
				model.PCYuMing=ds.Tables[0].Rows[0]["PCYuMing"].ToString();
				if(ds.Tables[0].Rows[0]["CreatTime"].ToString()!="")
				{
					model.CreatTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
				}
				model.TPL_ID=ds.Tables[0].Rows[0]["TPL_ID"].ToString();
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
		public EggsoftWX.Model.tab_ShopClient GetModel(Int32 ID)
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
				strSql.Append("delete from [tab_ShopClient] ");
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
				strSql.Append("delete from [tab_ShopClient] ");
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
			strSql.Append("update [tab_ShopClient] set ");
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
		public void Update(EggsoftWX.Model.tab_ShopClient model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [tab_ShopClient] set ");
			if (model.ID != null) strSql.Append("[ID]=@ID,");
			if (String.IsNullOrEmpty(model.ShopClientName) == false) strSql.Append("[ShopClientName]=@ShopClientName,");
			if (String.IsNullOrEmpty(model.ShopClientType) == false) strSql.Append("[ShopClientType]=@ShopClientType,");
			if (String.IsNullOrEmpty(model.ShopClientClass) == false) strSql.Append("[ShopClientClass]=@ShopClientClass,");
			if (String.IsNullOrEmpty(model.ShopButton) == false) strSql.Append("[ShopButton]=@ShopButton,");
			if (String.IsNullOrEmpty(model.ShopBackgroundLogoImage) == false) strSql.Append("[ShopBackgroundLogoImage]=@ShopBackgroundLogoImage,");
			if (String.IsNullOrEmpty(model.ContactMan) == false) strSql.Append("[ContactMan]=@ContactMan,");
			if (String.IsNullOrEmpty(model.ContactPhone) == false) strSql.Append("[ContactPhone]=@ContactPhone,");
			if (String.IsNullOrEmpty(model.ShopMemo) == false) strSql.Append("[ShopMemo]=@ShopMemo,");
			if (model.Sort != null) strSql.Append("[Sort]=@Sort,");
			if (model.UserClientLevel != null) strSql.Append("[UserClientLevel]=@UserClientLevel,");
			if ((model.Updatetime != null)&&(model.Updatetime != DateTime.MinValue)) strSql.Append("[Updatetime]=@Updatetime,");
			if (String.IsNullOrEmpty(model.UpdateMan) == false) strSql.Append("[UpdateMan]=@UpdateMan,");
			if (String.IsNullOrEmpty(model.Username) == false) strSql.Append("[Username]=@Username,");
			if (String.IsNullOrEmpty(model.PassWord) == false) strSql.Append("[PassWord]=@PassWord,");
			if (String.IsNullOrEmpty(model.UserRealName) == false) strSql.Append("[UserRealName]=@UserRealName,");
			if (String.IsNullOrEmpty(model.Country) == false) strSql.Append("[Country]=@Country,");
			if (String.IsNullOrEmpty(model.Sheng) == false) strSql.Append("[Sheng]=@Sheng,");
			if (String.IsNullOrEmpty(model.City) == false) strSql.Append("[City]=@City,");
			if (String.IsNullOrEmpty(model.Area) == false) strSql.Append("[Area]=@Area,");
			if (String.IsNullOrEmpty(model.PostCode) == false) strSql.Append("[PostCode]=@PostCode,");
			if (String.IsNullOrEmpty(model.Email) == false) strSql.Append("[Email]=@Email,");
			if (String.IsNullOrEmpty(model.Address) == false) strSql.Append("[Address]=@Address,");
			if (model.IsLocked != null) strSql.Append("[IsLocked]=@IsLocked,");
			if (model.IFCompany != null) strSql.Append("[IFCompany]=@IFCompany,");
			if (String.IsNullOrEmpty(model.UpLoadPath) == false) strSql.Append("[UpLoadPath]=@UpLoadPath,");
			if (String.IsNullOrEmpty(model.UserMemo) == false) strSql.Append("[UserMemo]=@UserMemo,");
			if (model.Sex != null) strSql.Append("[Sex]=@Sex,");
			if (String.IsNullOrEmpty(model.UserIntergentMark) == false) strSql.Append("[UserIntergentMark]=@UserIntergentMark,");
			if ((model.AuthorTime != null)&&(model.AuthorTime != DateTime.MinValue)) strSql.Append("[AuthorTime]=@AuthorTime,");
			if (model.AuthorMoney != null) strSql.Append("[AuthorMoney]=@AuthorMoney,");
			if (String.IsNullOrEmpty(model.XML) == false) strSql.Append("[XML]=@XML,");
			if (model.ShenMaShopping != null) strSql.Append("[ShenMaShopping]=@ShenMaShopping,");
			if (model.Shopping_Vouchers != null) strSql.Append("[Shopping_Vouchers]=@Shopping_Vouchers,");
			if (model.Shopping_Vouchers_Goods_Percent != null) strSql.Append("[Shopping_Vouchers_Goods_Percent]=@Shopping_Vouchers_Goods_Percent,");
			if (model.PartnerWeiXinID != null) strSql.Append("[PartnerWeiXinID]=@PartnerWeiXinID,");
			if (model.RecommendWeiXinID != null) strSql.Append("[RecommendWeiXinID]=@RecommendWeiXinID,");
			if (String.IsNullOrEmpty(model.QM_QQ_COM_QM_K_32) == false) strSql.Append("[QM_QQ_COM_QM_K_32]=@QM_QQ_COM_QM_K_32,");
			if (model.Shopping_Vouchers_Money != null) strSql.Append("[Shopping_Vouchers_Money]=@Shopping_Vouchers_Money,");
			if (String.IsNullOrEmpty(model.ErJiYuMing) == false) strSql.Append("[ErJiYuMing]=@ErJiYuMing,");
			if (String.IsNullOrEmpty(model.Username_Finance) == false) strSql.Append("[Username_Finance]=@Username_Finance,");
			if (String.IsNullOrEmpty(model.PassWord_Finance) == false) strSql.Append("[PassWord_Finance]=@PassWord_Finance,");
			if (model.Style_Model != null) strSql.Append("[Style_Model]=@Style_Model,");
			if (model.AutoMidifyAgentGoods != null) strSql.Append("[AutoMidifyAgentGoods]=@AutoMidifyAgentGoods,");
			if (String.IsNullOrEmpty(model.AgentMustRead) == false) strSql.Append("[AgentMustRead]=@AgentMustRead,");
			if (String.IsNullOrEmpty(model.AgentMustReadAd) == false) strSql.Append("[AgentMustReadAd]=@AgentMustReadAd,");
			if (String.IsNullOrEmpty(model.ContactManPostion) == false) strSql.Append("[ContactManPostion]=@ContactManPostion,");
			if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
			if (String.IsNullOrEmpty(model.PCYuMing) == false) strSql.Append("[PCYuMing]=@PCYuMing,");
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) strSql.Append("[CreatTime]=@CreatTime,");
			if (String.IsNullOrEmpty(model.TPL_ID) == false) strSql.Append("[TPL_ID]=@TPL_ID,");
			strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号
			
			List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
			if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID",model.ID));
			if (String.IsNullOrEmpty(model.ShopClientName) == false) ParameterToArrayList.Add(new SqlParameter("@ShopClientName",model.ShopClientName));
			if (String.IsNullOrEmpty(model.ShopClientType) == false) ParameterToArrayList.Add(new SqlParameter("@ShopClientType",model.ShopClientType));
			if (String.IsNullOrEmpty(model.ShopClientClass) == false) ParameterToArrayList.Add(new SqlParameter("@ShopClientClass",model.ShopClientClass));
			if (String.IsNullOrEmpty(model.ShopButton) == false) ParameterToArrayList.Add(new SqlParameter("@ShopButton",model.ShopButton));
			if (String.IsNullOrEmpty(model.ShopBackgroundLogoImage) == false) ParameterToArrayList.Add(new SqlParameter("@ShopBackgroundLogoImage",model.ShopBackgroundLogoImage));
			if (String.IsNullOrEmpty(model.ContactMan) == false) ParameterToArrayList.Add(new SqlParameter("@ContactMan",model.ContactMan));
			if (String.IsNullOrEmpty(model.ContactPhone) == false) ParameterToArrayList.Add(new SqlParameter("@ContactPhone",model.ContactPhone));
			if (String.IsNullOrEmpty(model.ShopMemo) == false) ParameterToArrayList.Add(new SqlParameter("@ShopMemo",model.ShopMemo));
			if (model.Sort != null) ParameterToArrayList.Add(new SqlParameter("@Sort",model.Sort));
			if (model.UserClientLevel != null) ParameterToArrayList.Add(new SqlParameter("@UserClientLevel",model.UserClientLevel));
			if ((model.Updatetime != null)&&(model.Updatetime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@Updatetime",model.Updatetime));
			if (String.IsNullOrEmpty(model.UpdateMan) == false) ParameterToArrayList.Add(new SqlParameter("@UpdateMan",model.UpdateMan));
			if (String.IsNullOrEmpty(model.Username) == false) ParameterToArrayList.Add(new SqlParameter("@Username",model.Username));
			if (String.IsNullOrEmpty(model.PassWord) == false) ParameterToArrayList.Add(new SqlParameter("@PassWord",model.PassWord));
			if (String.IsNullOrEmpty(model.UserRealName) == false) ParameterToArrayList.Add(new SqlParameter("@UserRealName",model.UserRealName));
			if (String.IsNullOrEmpty(model.Country) == false) ParameterToArrayList.Add(new SqlParameter("@Country",model.Country));
			if (String.IsNullOrEmpty(model.Sheng) == false) ParameterToArrayList.Add(new SqlParameter("@Sheng",model.Sheng));
			if (String.IsNullOrEmpty(model.City) == false) ParameterToArrayList.Add(new SqlParameter("@City",model.City));
			if (String.IsNullOrEmpty(model.Area) == false) ParameterToArrayList.Add(new SqlParameter("@Area",model.Area));
			if (String.IsNullOrEmpty(model.PostCode) == false) ParameterToArrayList.Add(new SqlParameter("@PostCode",model.PostCode));
			if (String.IsNullOrEmpty(model.Email) == false) ParameterToArrayList.Add(new SqlParameter("@Email",model.Email));
			if (String.IsNullOrEmpty(model.Address) == false) ParameterToArrayList.Add(new SqlParameter("@Address",model.Address));
			if (model.IsLocked != null) ParameterToArrayList.Add(new SqlParameter("@IsLocked",model.IsLocked));
			if (model.IFCompany != null) ParameterToArrayList.Add(new SqlParameter("@IFCompany",model.IFCompany));
			if (String.IsNullOrEmpty(model.UpLoadPath) == false) ParameterToArrayList.Add(new SqlParameter("@UpLoadPath",model.UpLoadPath));
			if (String.IsNullOrEmpty(model.UserMemo) == false) ParameterToArrayList.Add(new SqlParameter("@UserMemo",model.UserMemo));
			if (model.Sex != null) ParameterToArrayList.Add(new SqlParameter("@Sex",model.Sex));
			if (String.IsNullOrEmpty(model.UserIntergentMark) == false) ParameterToArrayList.Add(new SqlParameter("@UserIntergentMark",model.UserIntergentMark));
			if ((model.AuthorTime != null)&&(model.AuthorTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@AuthorTime",model.AuthorTime));
			if (model.AuthorMoney != null) ParameterToArrayList.Add(new SqlParameter("@AuthorMoney",model.AuthorMoney));
			if (String.IsNullOrEmpty(model.XML) == false) ParameterToArrayList.Add(new SqlParameter("@XML",model.XML));
			if (model.ShenMaShopping != null) ParameterToArrayList.Add(new SqlParameter("@ShenMaShopping",model.ShenMaShopping));
			if (model.Shopping_Vouchers != null) ParameterToArrayList.Add(new SqlParameter("@Shopping_Vouchers",model.Shopping_Vouchers));
			if (model.Shopping_Vouchers_Goods_Percent != null) ParameterToArrayList.Add(new SqlParameter("@Shopping_Vouchers_Goods_Percent",model.Shopping_Vouchers_Goods_Percent));
			if (model.PartnerWeiXinID != null) ParameterToArrayList.Add(new SqlParameter("@PartnerWeiXinID",model.PartnerWeiXinID));
			if (model.RecommendWeiXinID != null) ParameterToArrayList.Add(new SqlParameter("@RecommendWeiXinID",model.RecommendWeiXinID));
			if (String.IsNullOrEmpty(model.QM_QQ_COM_QM_K_32) == false) ParameterToArrayList.Add(new SqlParameter("@QM_QQ_COM_QM_K_32",model.QM_QQ_COM_QM_K_32));
			if (model.Shopping_Vouchers_Money != null) ParameterToArrayList.Add(new SqlParameter("@Shopping_Vouchers_Money",model.Shopping_Vouchers_Money));
			if (String.IsNullOrEmpty(model.ErJiYuMing) == false) ParameterToArrayList.Add(new SqlParameter("@ErJiYuMing",model.ErJiYuMing));
			if (String.IsNullOrEmpty(model.Username_Finance) == false) ParameterToArrayList.Add(new SqlParameter("@Username_Finance",model.Username_Finance));
			if (String.IsNullOrEmpty(model.PassWord_Finance) == false) ParameterToArrayList.Add(new SqlParameter("@PassWord_Finance",model.PassWord_Finance));
			if (model.Style_Model != null) ParameterToArrayList.Add(new SqlParameter("@Style_Model",model.Style_Model));
			if (model.AutoMidifyAgentGoods != null) ParameterToArrayList.Add(new SqlParameter("@AutoMidifyAgentGoods",model.AutoMidifyAgentGoods));
			if (String.IsNullOrEmpty(model.AgentMustRead) == false) ParameterToArrayList.Add(new SqlParameter("@AgentMustRead",model.AgentMustRead));
			if (String.IsNullOrEmpty(model.AgentMustReadAd) == false) ParameterToArrayList.Add(new SqlParameter("@AgentMustReadAd",model.AgentMustReadAd));
			if (String.IsNullOrEmpty(model.ContactManPostion) == false) ParameterToArrayList.Add(new SqlParameter("@ContactManPostion",model.ContactManPostion));
			if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted",model.IsDeleted));
			if (String.IsNullOrEmpty(model.PCYuMing) == false) ParameterToArrayList.Add(new SqlParameter("@PCYuMing",model.PCYuMing));
			if ((model.CreatTime != null)&&(model.CreatTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreatTime",model.CreatTime));
			if (String.IsNullOrEmpty(model.TPL_ID) == false) ParameterToArrayList.Add(new SqlParameter("@TPL_ID",model.TPL_ID));
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
           strSql.Append(" FROM   [tab_ShopClient]  where 1 > 0 " + strWhere);
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
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_ShopClient]", strConditions, orderField, isDesc,ParameterToList);
        }
         #endregion  大数据量快速分页,50万以上数据分页 memo 0020
     #endregion  成员方法
     }
}
