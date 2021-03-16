using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace EggsoftWX.SQLServerDAL //可以修改成实际项目的命名空间名称
{
    /// <summary>
    /// Copyright (C) 2004-2008 LiTianPing 
    /// 数据访问基础类(基于SQLServer)
    /// 用户可以修改满足自己项目的需要。
    /// </summary>
    public abstract class DbHelperSQL
    {
        //数据库连接字符串(web.config来配置)
        //<add key="ConnectionString" value="server=127.0.0.1;database=DATABASE;uid=sa;pwd=" />		
        private static string getConnectionString()
        {
            //Eggsoft.Common.MyRegistry MyRegistry = new Eggsoft.Common.MyRegistry();
            //string ConnStr = "workstation id=" + MyRegistry.ReadStrfromReg("Wkst_Id") + ";" +
            //          "packet size=" + MyRegistry.ReadStrfromReg("pkt_size") + ";" +
            //          "user id=" + MyRegistry.ReadStrfromReg("SQL_User") + ";" +
            //          "data source=" + MyRegistry.ReadStrfromReg("SQLServerName") + ";" +
            //          "persist security info=" + MyRegistry.ReadStrfromReg("ps_info") + ";" +
            //          "initial catalog=" + MyRegistry.ReadStrfromReg("DataBaseName") + ";" +
            //          "password=" + MyRegistry.ReadStrfromReg("SQL_PSW");
            string ConnStr = ConfigurationManager.ConnectionStrings["Shop.Earth17.Com_ConnectionString"].ToString();
            return ConnStr;
        }
        protected static readonly string connectionString = getConnectionString();


        public static void
           testMySqlBulkCopyDbHelperSQL()
        {
            SqlDataAdapter sqldataadapter = new SqlDataAdapter("select * from tab_Temp where 1=2", connectionString);

            DataSet dataset = new DataSet();

            sqldataadapter.Fill(dataset, "Table_1");

            DataTable datatable = dataset.Tables[0];

            for (Int32 i = 0; i < 2; i++)
            {
                DataRow datarow = datatable.NewRow();
                //datarow["ID"] = i;
                datarow["Name1"] = "Name1" + string.Format("{0:0000}", i);
                datarow["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                datatable.Rows.Add(datarow);
            }
            SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction);
            sqlbulkcopy.DestinationTableName = "tab_Temp";//数据库中的表名                        
            sqlbulkcopy.WriteToServer(dataset.Tables[0]);
            //DbHelperSQL.Query("SELECT * FROM tab_Temp WITH (HOLDLOCK)");   1770732798@qq.com         
        }


        public DbHelperSQL()
        {
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static Int32 ExecuteSql(string SQLString)
        {
            Int32 rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        rows = cmd.ExecuteNonQuery();

                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(E, "Sqlserver", "程序报错");
                        Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer", "程序报错");

                        connection.Close();
                        //throw new Exception(E.Message);
                    }
                }
            }
            return rows;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //try
                //{
                Int32 k = 0;
                string strAddAll = "";
                for (Int32 n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        strAddAll += strsql + ";";
                    }
                    k++;
                    if (k > 100)
                    {
                        if (String.IsNullOrEmpty(strAddAll) == false)
                        {


                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            SqlTransaction tx = conn.BeginTransaction();
                            cmd.Transaction = tx;
                            try
                            {
                                //EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(" EXEC sp_UpdateStats");
                                cmd.CommandText = strAddAll;
                                cmd.ExecuteNonQuery();
                                tx.Commit();
                                strAddAll = "";
                                k = 0;
                            }
                            catch (System.Data.SqlClient.SqlException E)
                            {
                                tx.Rollback();
                                Eggsoft.Common.debug_Log.Call_WriteLog(E, "Sqlserver", "程序报错");
                                Eggsoft.Common.debug_Log.Call_WriteLog("strAddAll=" + strAddAll, "SQLServer", "程序报错");
                                //throw new Exception(E.Message);
                            }
                        }
                    }
                }
                if (String.IsNullOrEmpty(strAddAll) == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    SqlTransaction tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    try
                    {
                        //EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(" EXEC sp_UpdateStats");

                        cmd.CommandText = strAddAll;
                        cmd.ExecuteNonQuery();
                        tx.Commit();
                        k = 0;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        tx.Rollback();
                        Eggsoft.Common.debug_Log.Call_WriteLog(E, "Sqlserver", "程序报错");
                        Eggsoft.Common.debug_Log.Call_WriteLog("strAddAll=" + strAddAll, "SQLServer", "程序报错");

                        //throw new Exception(E.Message);
                    }
                }
            }
        }





        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static Int32 ExecuteSql(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer");
                //if (SQLString.Contains("3944")) Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer3944");

                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    Int32 rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(E, "Sqlserver", "程序报错");
                    Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString + ", content=" + content, "SQLServer", "程序报错");
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static Int32 ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            Int32 rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    rows = cmd.ExecuteNonQuery();

                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strSQL=" + strSQL, "SQLServer", "程序报错");
                    Eggsoft.Common.debug_Log.Call_WriteLog(E, "Sqlserver", "程序报错");
                    //throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
            return rows;
        }

        #region object Scalar(string SQLString)
        /// <summary>
        ///ExecuteScalar方法返回的类型是object类型，这个方法返回sql语句执行后的第一行第一列的值，由于不知到sql语句到底是什么样的结构（有可能是Int32，有可能是char等等），所以ExecuteScalar方法返回一个最基本的类型object，这个类型是所有类型的基类，换句话说：可以转换为任意类型。
        /// object Scalar(string field, string where)
        /// </summary>   
        public static object ExecuteScalar(string SQLString, SqlParameter[] cmdParms = null)
        {
            object obj = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using (SqlConnection conn = new SqlConnection(connectionString))
                //{
                //    conn.Open();
                //    SqlDataAdapter dap = new SqlDataAdapter(commandText, conn);
                //    dap.SelectCommand.CommandType = CommandType.Text;  //执行类型：命令文本
                //    if (cmdParms != null)
                //    {
                //        foreach (SqlParameter parameter in cmdParms)
                //            dap.SelectCommand.Parameters.Add(parameter);
                //    }

                //    dap.Fill(dt);

                //}

                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    string strSQL = parametersDebugLog(SQLString, cmdParms);
                    try
                    {
                        connection.Open();

                        if (cmdParms != null)
                        {
                            foreach (SqlParameter parameter in cmdParms)
                                cmd.Parameters.Add(parameter);
                        }

                        obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();//多了这一句，就解决了问题
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {

                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(e, "SQLServer");
                        Eggsoft.Common.debug_Log.Call_WriteLog("strSQL=" + strSQL, "SQLServer");
                        connection.Close();
                        //throw new Exception(e.Message);
                    }
                }
                return obj;
            }

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select " + field + " from [tblScore] where 1>0 " + where);
            //return DbHelperOleDb.ExecuteScalar(strSql.ToString());
        }
        #endregion

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary> ExecuteScalar方法返回的类型是object类型，这个方法返回sql语句执行后的第一行第一列的值，由于不知到sql语句到底
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            object obj = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer");
                //if (SQLString.Contains("3944")) Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer3944");

                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {

                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(e, "SQLServer", "程序报错");
                        Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer", "程序报错");
                        connection.Close();
                        //throw new Exception(e.Message);
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlDataReader myReader = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    myReader = cmd.ExecuteReader();

                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(e, "SQLServer", "程序报错");
                    Eggsoft.Common.debug_Log.Call_WriteLog("strSQL=" + strSQL, "SQLServer", "程序报错");
                    //throw new Exception(e.Message);
                }
            }

            return myReader;
        }


        public static DataSet GetDataSet(string SQLString, params object[] objs)
        {
            SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(SQLString.ToString(), objs);
            return GetDataSet(SQLString, ParameterToList);
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string SQLString, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer");
                //if (SQLString.Contains("3944")) Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer3944");
                string strSQL = parametersDebugLog(SQLString, cmdParms);

                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    if (cmdParms != null)
                    {
                        foreach (SqlParameter parameter in cmdParms)
                            command.SelectCommand.Parameters.Add(parameter);
                    }
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ex, "SQLServer", "程序报错");
                    Eggsoft.Common.debug_Log.Call_WriteLog("SQLString=" + SQLString, "SQLServer", "程序报错");
                    //throw new Exception(ex.Message);
                }

            }
            return ds;
        }

        public static DataTable GetDataTable(string commandText, params SqlParameter[] cmdParms)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = parametersDebugLog(commandText, cmdParms);
                //Eggsoft.Common.debug_Log.Call_WriteLog("commandText=" + commandText, "SQLServer");
                //if (commandText.Contains("3944")) Eggsoft.Common.debug_Log.Call_WriteLog("commandText=" + commandText, "SQLServer3944");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter dap = new SqlDataAdapter(commandText, conn);
                    dap.SelectCommand.CommandType = CommandType.Text;  //执行类型：命令文本
                    if (cmdParms != null)
                    {
                        foreach (SqlParameter parameter in cmdParms)
                            dap.SelectCommand.Parameters.Add(parameter);
                    }

                    dap.Fill(dt);
                    dap.SelectCommand.Parameters.Clear();//多了这一句，就解决了问题
                }
            }
            catch (Exception ex)
            {
                string tsql = "";
                // if (1)
                tsql = commandText;

                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "SQLServer", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog("commandText=" + commandText, "SQLServer", "程序报错");
                //throw new Exception(tsql + " " + ex.Message);
            }
            return dt;
        }


        public static IList<string> GetList(string commandText, params SqlParameter[] cmdParms)
        {
            DataTable dt = GetDataTable(commandText, cmdParms);
            List<string> il = new List<string>();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows[0].ItemArray.Length; i++)
                        il.Add(dt.Rows[0].ItemArray.GetValue(i).ToString());
                }
                return il;
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string tableName, string conditions, string orderField, bool isDesc, params object[] objs)
        {
            SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(fields + " " + tableName + " " + conditions + " " + orderField, objs);
            return GetPageDataTable(pageIndex, pageSize, fields, tableName, conditions, orderField, isDesc, ParameterToList);
        }

        /// <summary>
        /// 大数据量快速分页,50万以上数据分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyField"></param>
        /// <param name="fields"></param>
        /// <param name="tableName"></param>
        /// <param name="conditions"></param>
        /// <param name="orderField"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public static DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string tableName, string conditions, string orderField, bool isDesc, SqlParameter[] cmdParms = null)
        {
            DataTable dt = null;
            string sql = "";
            try
            {
                if (conditions.ToLower().Contains("and") == false && string.IsNullOrEmpty(conditions) == false) conditions = " and " + conditions;

                //Eggsoft.Common.debug_Log.Call_WriteLog("tableName=" + tableName + " conditions=" + conditions, "SQLServer");
                Int32 recordCount = 0;
                object objRecordCount = ExecuteScalar("select count(1) from " + tableName + " where 1 > 0 " + conditions + "", cmdParms);
                if (objRecordCount != null)
                {
                    recordCount = Int32.Parse(objRecordCount.ToString());
                }
                Int32 lastPageSize = recordCount % pageSize == 0 ? pageSize : (recordCount % pageSize);
                Int32 pageCount = recordCount % pageSize == 0 ? (recordCount / pageSize) : (Eggsoft.Common.CommUtil.GetInt(recordCount / pageSize) + 1);

                if (isDesc)
                {
                    if (pageIndex <= 1)
                    {
                        sql = "select top " + pageSize + " " + fields + " from " + tableName + " where 1 > 0 " + conditions + " order by " + orderField + " desc";
                    }
                    else
                    {
                        if (pageIndex == pageCount)
                        {
                            sql = "select * from (select top " + lastPageSize + " " + fields + " from " + tableName + " where 1 > 0 " + conditions + " order by " + orderField + " asc) T order by " + orderField + " desc";
                        }
                        else if (pageIndex > pageCount)
                        {
                            return null;
                        }
                        else
                        {
                            sql = "Select Top " + pageSize + " " + fields + " From " + tableName + " Where " + orderField + "<(Select Min(" + orderField + ") From (Select Top " + (pageIndex - 1) * pageSize + " " + orderField + " From " + tableName + " where 1=1 " + conditions + " Order By " + orderField + " desc) T) " + conditions + " Order By " + orderField + " desc ";
                        }
                    }
                }
                else
                {
                    if (pageIndex <= 1)
                    {
                        sql = "select top " + pageSize + " " + fields + " from " + tableName + " where 1 > 0 " + conditions + " order by " + orderField + " asc";
                    }
                    else
                    {
                        if (pageIndex == pageCount)
                        {
                            sql = "select *  from (select top " + lastPageSize + " " + fields + " from " + tableName + " where 1 > 0 " + conditions + " order by " + orderField + " desc) T order by " + orderField + " asc";
                        }
                        else if (pageIndex > pageCount)
                        {
                            return null;
                        }
                        else
                        {
                            sql = "Select Top " + pageSize + " " + fields + " From " + tableName + " Where " + orderField + ">=(Select Max(" + orderField + ") From (Select Top " + (pageIndex - 1) * pageSize + " " + orderField + " From " + tableName + " where 1=1 " + conditions + " Order By " + orderField + " Asc) T) " + conditions + "  Order By " + orderField + " Asc";
                        }
                    }
                }
                dt = GetDataTable(sql, cmdParms);
            }
            catch (Exception ex)
            {
                string tsql = "";
                //if (bShowSql)
                if (true)
                    tsql = sql;
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "SQLServer", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog("sql=" + sql, "SQLServer", "程序报错");
                //throw new Exception(tsql + " " + ex.Message);
            }
            return dt;
        }


        #endregion





        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static Int32 ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            Int32 rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog(parametersDebugLog(SQLString, cmdParms), "SQLServer");

                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(E, "Sqlserver", "程序报错");
                        Eggsoft.Common.debug_Log.Call_WriteLog(parametersDebugLog(SQLString, cmdParms), "Sqlserver", "程序报错");
                        //throw new Exception(E.Message);
                    }
                }
            }
            return rows;
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            Int32 val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch (Exception E)
                    {
                        trans.Rollback();
                        Eggsoft.Common.debug_Log.Call_WriteLog(E, "Sqlserver", "程序报错");
                        //throw;
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            object obj = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        //Eggsoft.Common.debug_Log.Call_WriteLog("commandText=" + SQLString, "SQLServer");

                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {

                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(e, "Sqlserver", "程序报错");
                        Eggsoft.Common.debug_Log.Call_WriteLog(parametersDebugLog(SQLString, cmdParms), "Sqlserver", "程序报错");
                        //throw new Exception(e.Message);
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlDataReader myReader = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("commandText=" + SQLString, "SQLServer");

                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();

            }
            catch (System.Data.SqlClient.SqlException e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "Sqlserver", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog(parametersDebugLog(SQLString, cmdParms), "Sqlserver", "程序报错");
                //Eggsoft.Common.debug_Log.Call_WriteLog(e, "Sqlserver");
                //throw new Exception(e.Message);
            }
            return myReader;
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string strparametersDebugLog = parametersDebugLog(SQLString, cmdParms);

                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {

                    try
                    {
                        da.Fill(ds, "ds");
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(ex, "Sqlserver", "程序报错");
                        Eggsoft.Common.debug_Log.Call_WriteLog(parametersDebugLog(SQLString, cmdParms), "Sqlserver", "程序报错");
                    }
                }
            }
            return ds;
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    //if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) && (parm.Value == null))
                    //{
                    //    parm.Value = DBNull.Value;
                    //}
                    cmd.Parameters.Add(parm);
                }
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            //Eggsoft.Common.debug_Log.Call_WriteLog(parametersDebugLog(storedProcName, parameters), "SQLServer");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader returnReader;
                connection.Open();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    returnReader = command.ExecuteReader();
                }
                finally
                {

                }


                return returnReader;
            }
            //            SqlConnection connection = new SqlConnection(connectionString);

        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static Int32 RunProcedure(string storedProcName, IDataParameter[] parameters, out Int32 rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Int32 result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (Int32)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.BigInt, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }




        #endregion




        #region DataTable批量添加相比之下,灵活度高(有事务)
        /// <summary>
        /// DataTable批量添加(有事务)
        /// </summary>
        /// <param name="Table">数据源</param>
        /// <param name="Mapping">定义数据源和目标源列的关系集合</param>
        /// <param name="DestinationTableName">目标表</param>
        public static bool MySqlBulkCopy(DataTable Table, SqlBulkCopyColumnMapping[] Mapping, string DestinationTableName)
        {
            bool Bool = true;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction Tran = con.BeginTransaction())
                {
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con, SqlBulkCopyOptions.FireTriggers, Tran))
                    {
                        Copy.DestinationTableName = DestinationTableName;//指定目标表
                        if (Mapping != null)
                        {
                            //如果有数据
                            foreach (SqlBulkCopyColumnMapping Map in Mapping)
                            {
                                Copy.ColumnMappings.Add(Map);
                            }
                        }
                        try
                        {
                            Copy.WriteToServer(Table);//批量添加
                            Tran.Commit();//提交事务
                        }
                        catch (Exception ex)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(ex, "Sqlserver", "程序报错");
                            Tran.Rollback();//回滚事务
                            Bool = false;
                        }
                    }
                }
            }
            return Bool;
        }
        #endregion


        private static string parametersDebugLog(string stringSQL, IDataParameter[] parameters)
        {
            //#if true
            StringBuilder sb = new StringBuilder();
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    var name = string.IsNullOrEmpty(p.ParameterName) ? "" : p.ParameterName;
                    var val = p.Value == null ? "" : p.Value.ToString().Replace("'", "''");
                    sb.AppendLine("DECLARE " + name + " NVARCHAR(200)");
                    sb.AppendLine("SET " + name + " = '" + val + "'");
                }
            }
            sb.AppendLine(stringSQL);
            return sb.ToString();
        }
        /// <summary>
        /// 更新Excel 返回受影响的行数
        /// </summary>
        /// <param name="strAccessfilePath"></param>
        /// <param name="SQLtem_sql"></param>
        /// <returns></returns>
        public static Int32 updateExcel(string strAccessfilePath, string SQLtem_sql)
        {
            Int32 intRow = 0;
            string connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strAccessfilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";//记录连接Access的语句   
            using (System.Data.OleDb.OleDbConnection tem_conn = new System.Data.OleDb.OleDbConnection(connstr))
            {
                using (System.Data.OleDb.OleDbCommand tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn))
                {
                    try
                    {
                        tem_conn.Open();//打开连接的Access数据库  
                        tem_comm.CommandText = SQLtem_sql;
                        intRow = tem_comm.ExecuteNonQuery();
                        tem_conn.Close();
                    }
                    catch (System.Data.OleDb.OleDbException E)
                    {

                        Eggsoft.Common.debug_Log.Call_WriteLog(E, "Microsoft.Jet", "程序报错");
                        Eggsoft.Common.debug_Log.Call_WriteLog("Microsoft.Jet=" + SQLtem_sql, "Microsoft.Jet", "程序报错");

                        tem_conn.Close();
                        //throw new Exception(E.Message);
                    }
                }
            }
            return intRow;
        }
        /// <summary>
        /// 更新Excel 返回受影响的行数
        /// </summary>
        /// <param name="strAccessfilePath"></param>
        /// <param name="SQLtem_sql"></param>
        /// <returns></returns>
        public static DataSet ReadExcel(string strAccessfilePath, string SQLtem_sql)
        {
            DataSet ds = new DataSet();
            string connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strAccessfilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";//记录连接Access的语句   

            using (System.Data.OleDb.OleDbConnection tem_conn = new System.Data.OleDb.OleDbConnection(connstr))
            {
                using (System.Data.OleDb.OleDbCommand tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn))
                {
                    using (System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(tem_comm))
                    {

                        try
                        {
                            da.Fill(ds, "ds");
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(ex, "SqlOLE", "程序报错");
                        }
                    }

                }
            }
            return ds;
        }

    }
}
