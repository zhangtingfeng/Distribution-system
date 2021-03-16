using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_TuanGou。
    /// </summary>
    public class tab_TuanGou : Itab_TuanGou
    {
        public tab_TuanGou()
        { }
        #region  成员方法

        #region 得到最大ID  memo 0001
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public Int32 GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(ID)+1 from [tab_TuanGou]");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
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
        public Int32 Add(EggsoftWX.Model.tab_TuanGou model)
        {
            IDataParameter[] iData = new SqlParameter[2];
            iData[0] = new SqlParameter("@TableName", "tab_TuanGou");
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
        public Int32 Add(string strSet, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [tab_TuanGou]");
            if (!strSet.ToLower().Contains("id"))
            {
                strSet = strSet + ",ID";
                strValue = strValue + "," + GetMaxId().ToString();
            }
            strSql.Append("(" + strSet + ")");
            strSql.Append(" VALUES ");
            strSql.Append("(" + strValue + ")");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [tab_TuanGou] where 1>0 " + strWhere + " ");
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

        #region 是否存在该记录 ID memo 0005
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Int32 ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [tab_TuanGou] where ID=" + ID + "");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from [tab_TuanGou] where 1>0 " + strWhere + " ");
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
            return cmdresult;
        }
        #endregion

        #region 获得数据列表 自定义SelectList strSelect oderby  group 等等 memo 0007
        /// <summary>
        /// 获得数据列表 自定义SelectList strSelect oderby  ，group 等等
        /// </summary>
        public DataSet SelectList(string strSelect)
        {
            StringBuilder strSql = new StringBuilder();
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
            strSql.Append("select * from [tab_TuanGou] ");
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
        public DataSet GetList(string strItem, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + strItem + " from [tab_TuanGou] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            strSql.Append("select " + fields + " from [tab_TuanGou] where 1>0 " + strWhere);
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
            strSql.Append("select top " + topNum + " " + fields + " from [tab_TuanGou] where 1>0 " + strWhere);
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
            strSql.Append("select " + field + " from [tab_TuanGou] where 1>0 " + strWhere);
            return DbHelperSQL.GetSingle(strSql.ToString());
        }
        #endregion
        #region 得到一个对象实体 strWhere memo 0013
        /// <summary>
        /// 得到一个对象实体  strWhere
        /// </summary>
        public EggsoftWX.Model.tab_TuanGou GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [tab_TuanGou] ");
            strSql.Append(" where " + strWhere);
            EggsoftWX.Model.tab_TuanGou model = new EggsoftWX.Model.tab_TuanGou();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HowManyPeople"].ToString() != "")
                {
                    model.HowManyPeople = Int32.Parse(ds.Tables[0].Rows[0]["HowManyPeople"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EachPeoplePrice"].ToString() != "")
                {
                    model.EachPeoplePrice = decimal.Parse(ds.Tables[0].Rows[0]["EachPeoplePrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgentPrice"].ToString() != "")
                {
                    model.AgentPrice = decimal.Parse(ds.Tables[0].Rows[0]["AgentPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TuanZhangBonus_AgentGet"].ToString() != "")
                {
                    model.TuanZhangBonus_AgentGet = Convert.ToBoolean(ds.Tables[0].Rows[0]["TuanZhangBonus_AgentGet"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TuanZhangBonus_GouWuQuan"].ToString() != "")
                {
                    model.TuanZhangBonus_GouWuQuan = decimal.Parse(ds.Tables[0].Rows[0]["TuanZhangBonus_GouWuQuan"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TuanZhangBonus_Money"].ToString() != "")
                {
                    model.TuanZhangBonus_Money = decimal.Parse(ds.Tables[0].Rows[0]["TuanZhangBonus_Money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShopClientID"].ToString() != "")
                {
                    model.ShopClientID = Int32.Parse(ds.Tables[0].Rows[0]["ShopClientID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SourceGoodID"].ToString() != "")
                {
                    model.SourceGoodID = Int32.Parse(ds.Tables[0].Rows[0]["SourceGoodID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDeleted"].ToString() != "")
                {
                    model.IsDeleted = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDeleted"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSales"].ToString() != "")
                {
                    model.IsSales = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSales"].ToString());
                }
                model.TuanFouRule = ds.Tables[0].Rows[0]["TuanFouRule"].ToString();
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = Int32.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MustSubscribe_Master"].ToString() != "")
                {
                    model.MustSubscribe_Master = Convert.ToBoolean(ds.Tables[0].Rows[0]["MustSubscribe_Master"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MustSubscribe_Helper"].ToString() != "")
                {
                    model.MustSubscribe_Helper = Convert.ToBoolean(ds.Tables[0].Rows[0]["MustSubscribe_Helper"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MustAddress_Master"].ToString() != "")
                {
                    model.MustAddress_Master = Convert.ToBoolean(ds.Tables[0].Rows[0]["MustAddress_Master"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MustAgent_Master"].ToString() != "")
                {
                    model.MustAgent_Master = Convert.ToBoolean(ds.Tables[0].Rows[0]["MustAgent_Master"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WhenEndAllGroup"].ToString() != "")
                {
                    model.WhenEndAllGroup = DateTime.Parse(ds.Tables[0].Rows[0]["WhenEndAllGroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxTimeLengthDoGroup"].ToString() != "")
                {
                    model.MaxTimeLengthDoGroup = Int32.Parse(ds.Tables[0].Rows[0]["MaxTimeLengthDoGroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChoiceWhenEndAllGroup"].ToString() != "")
                {
                    model.ChoiceWhenEndAllGroup = Convert.ToBoolean(ds.Tables[0].Rows[0]["ChoiceWhenEndAllGroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChoiceMaxTimeLengthDoGroup"].ToString() != "")
                {
                    model.ChoiceMaxTimeLengthDoGroup = Convert.ToBoolean(ds.Tables[0].Rows[0]["ChoiceMaxTimeLengthDoGroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BuyMultiOnlyOneAccount"].ToString() != "")
                {
                    model.BuyMultiOnlyOneAccount = Convert.ToBoolean(ds.Tables[0].Rows[0]["BuyMultiOnlyOneAccount"].ToString());
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
        public EggsoftWX.Model.tab_TuanGou GetModel(Int32 ID)
        {
            return GetModel("ID=" + ID + "");
        }
        #endregion

        #region delete strWhere 删除n条数据 memo 0015
        /// <summary>
        /// 删除n条数据
        /// </summary>
        public void Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [tab_TuanGou] ");
            strSql.Append(" where " + strWhere);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion

        #region delete ID 删除一条数据  memo 0016
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(Int32 ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [tab_TuanGou] ");
            strSql.Append(" where ID=" + ID);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion

        #region update strSet strWhere 更新n条数据 更新n条数据 memo 0017
        /// <summary>
        /// 更新n条数据
        /// </summary>
        public void Update(string strSet, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [tab_TuanGou] set ");
            strSql.Append(strSet);
            strSql.Append(" where ");
            strSql.Append(strWhere);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion

        #region model update 更新一条数据 memo 0018
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(EggsoftWX.Model.tab_TuanGou model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [tab_TuanGou] set ");
            if (model.ID != null) strSql.Append("[ID]=@ID,");
            if (model.HowManyPeople != null) strSql.Append("[HowManyPeople]=@HowManyPeople,");
            if (model.EachPeoplePrice != null) strSql.Append("[EachPeoplePrice]=@EachPeoplePrice,");
            if (model.AgentPrice != null) strSql.Append("[AgentPrice]=@AgentPrice,");
            if (model.TuanZhangBonus_AgentGet != null) strSql.Append("[TuanZhangBonus_AgentGet]=@TuanZhangBonus_AgentGet,");
            if (model.TuanZhangBonus_GouWuQuan != null) strSql.Append("[TuanZhangBonus_GouWuQuan]=@TuanZhangBonus_GouWuQuan,");
            if (model.TuanZhangBonus_Money != null) strSql.Append("[TuanZhangBonus_Money]=@TuanZhangBonus_Money,");
            if (model.ShopClientID != null) strSql.Append("[ShopClientID]=@ShopClientID,");
            if (model.SourceGoodID != null) strSql.Append("[SourceGoodID]=@SourceGoodID,");
            if (model.IsDeleted != null) strSql.Append("[IsDeleted]=@IsDeleted,");
            if ((model.CreateTime != null) && (model.CreateTime != DateTime.MinValue)) strSql.Append("[CreateTime]=@CreateTime,");
            if ((model.UpdateTime != null) && (model.UpdateTime != DateTime.MinValue)) strSql.Append("[UpdateTime]=@UpdateTime,");
            if (model.IsSales != null) strSql.Append("[IsSales]=@IsSales,");
            if (String.IsNullOrEmpty(model.TuanFouRule) == false) strSql.Append("[TuanFouRule]=@TuanFouRule,");
            if (model.Sort != null) strSql.Append("[Sort]=@Sort,");
            if (model.MustSubscribe_Master != null) strSql.Append("[MustSubscribe_Master]=@MustSubscribe_Master,");
            if (model.MustSubscribe_Helper != null) strSql.Append("[MustSubscribe_Helper]=@MustSubscribe_Helper,");
            if (model.MustAddress_Master != null) strSql.Append("[MustAddress_Master]=@MustAddress_Master,");
            if (model.MustAgent_Master != null) strSql.Append("[MustAgent_Master]=@MustAgent_Master,");
            if ((model.WhenEndAllGroup != null) && (model.WhenEndAllGroup != DateTime.MinValue)) strSql.Append("[WhenEndAllGroup]=@WhenEndAllGroup,");
            if (model.MaxTimeLengthDoGroup != null) strSql.Append("[MaxTimeLengthDoGroup]=@MaxTimeLengthDoGroup,");
            if (model.ChoiceWhenEndAllGroup != null) strSql.Append("[ChoiceWhenEndAllGroup]=@ChoiceWhenEndAllGroup,");
            if (model.ChoiceMaxTimeLengthDoGroup != null) strSql.Append("[ChoiceMaxTimeLengthDoGroup]=@ChoiceMaxTimeLengthDoGroup,");
            if (model.BuyMultiOnlyOneAccount != null) strSql.Append("[BuyMultiOnlyOneAccount]=@BuyMultiOnlyOneAccount,");
            strSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号

            List<SqlParameter> ParameterToArrayList = new List<SqlParameter>();
            if (model.ID != null) ParameterToArrayList.Add(new SqlParameter("@ID", model.ID));
            if (model.HowManyPeople != null) ParameterToArrayList.Add(new SqlParameter("@HowManyPeople", model.HowManyPeople));
            if (model.EachPeoplePrice != null) ParameterToArrayList.Add(new SqlParameter("@EachPeoplePrice", model.EachPeoplePrice));
            if (model.AgentPrice != null) ParameterToArrayList.Add(new SqlParameter("@AgentPrice", model.AgentPrice));
            if (model.TuanZhangBonus_AgentGet != null) ParameterToArrayList.Add(new SqlParameter("@TuanZhangBonus_AgentGet", model.TuanZhangBonus_AgentGet));
            if (model.TuanZhangBonus_GouWuQuan != null) ParameterToArrayList.Add(new SqlParameter("@TuanZhangBonus_GouWuQuan", model.TuanZhangBonus_GouWuQuan));
            if (model.TuanZhangBonus_Money != null) ParameterToArrayList.Add(new SqlParameter("@TuanZhangBonus_Money", model.TuanZhangBonus_Money));
            if (model.ShopClientID != null) ParameterToArrayList.Add(new SqlParameter("@ShopClientID", model.ShopClientID));
            if (model.SourceGoodID != null) ParameterToArrayList.Add(new SqlParameter("@SourceGoodID", model.SourceGoodID));
            if (model.IsDeleted != null) ParameterToArrayList.Add(new SqlParameter("@IsDeleted", model.IsDeleted));
            if ((model.CreateTime != null) && (model.CreateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@CreateTime", model.CreateTime));
            if ((model.UpdateTime != null) && (model.UpdateTime != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@UpdateTime", model.UpdateTime));
            if (model.IsSales != null) ParameterToArrayList.Add(new SqlParameter("@IsSales", model.IsSales));
            if (String.IsNullOrEmpty(model.TuanFouRule) == false) ParameterToArrayList.Add(new SqlParameter("@TuanFouRule", model.TuanFouRule));
            if (model.Sort != null) ParameterToArrayList.Add(new SqlParameter("@Sort", model.Sort));
            if (model.MustSubscribe_Master != null) ParameterToArrayList.Add(new SqlParameter("@MustSubscribe_Master", model.MustSubscribe_Master));
            if (model.MustSubscribe_Helper != null) ParameterToArrayList.Add(new SqlParameter("@MustSubscribe_Helper", model.MustSubscribe_Helper));
            if (model.MustAddress_Master != null) ParameterToArrayList.Add(new SqlParameter("@MustAddress_Master", model.MustAddress_Master));
            if (model.MustAgent_Master != null) ParameterToArrayList.Add(new SqlParameter("@MustAgent_Master", model.MustAgent_Master));
            if ((model.WhenEndAllGroup != null) && (model.WhenEndAllGroup != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter("@WhenEndAllGroup", model.WhenEndAllGroup));
            if (model.MaxTimeLengthDoGroup != null) ParameterToArrayList.Add(new SqlParameter("@MaxTimeLengthDoGroup", model.MaxTimeLengthDoGroup));
            if (model.ChoiceWhenEndAllGroup != null) ParameterToArrayList.Add(new SqlParameter("@ChoiceWhenEndAllGroup", model.ChoiceWhenEndAllGroup));
            if (model.ChoiceMaxTimeLengthDoGroup != null) ParameterToArrayList.Add(new SqlParameter("@ChoiceMaxTimeLengthDoGroup", model.ChoiceMaxTimeLengthDoGroup));
            if (model.BuyMultiOnlyOneAccount != null) ParameterToArrayList.Add(new SqlParameter("@BuyMultiOnlyOneAccount", model.BuyMultiOnlyOneAccount));
            strSql.Append(" where ID='" + model.ID + "'");
            DbHelperSQL.ExecuteSql(strSql.ToString(), ParameterToArrayList.ToArray());
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
            if ((strWhere.IndexOf("and") == -1) && (strWhere != "")) strWhere = "and " + strWhere;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + topNum.ToString() + " " + fields);
            strSql.Append(" FROM   [tab_TuanGou]  where 1 > 0 " + strWhere);
            return DbHelperSQL.GetDataTable(strSql.ToString());
        }

        #endregion  成员方法
        #region 大数据量快速分页,50万以上数据分页 memo 0020
        //大数据量快速分页,50万以上数据分页  memo 0020
        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc)
        {
            strConditions = strConditions.ToLower();
            if ((strConditions.IndexOf("and") == -1) && (strConditions != "")) strConditions = "and " + strConditions;
            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, "[tab_TuanGou]", strConditions, orderField, isDesc);
        }
        #endregion  大数据量快速分页,50万以上数据分页 memo 0020
        #endregion  成员方法
    }
}
