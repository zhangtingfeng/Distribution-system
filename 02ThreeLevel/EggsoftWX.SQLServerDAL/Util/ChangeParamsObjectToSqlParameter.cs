using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace EggsoftWX.SQLServerDAL.Util
{
    public static class ChangeParamsObjectToSqlParameter
    {

        public static SqlParameter[] ChangeParamsObjectToSqlParameterAction(string strWhere, params object[] objs)
        {
            List<SqlParameter> ParameterToList = new List<SqlParameter>();
            IDataParameter[] IDataParameterCurList = GetParametersFromSQL(strWhere);
            for (int i = 0; i < IDataParameterCurList.Length; i++)
            {
                ParameterToList.Add(new SqlParameter(IDataParameterCurList[i].ToString(), objs[i]));
            }

            return ParameterToList.ToArray();
        }

        private static Regex ParameterReg = new Regex("[@]\\w+", RegexOptions.Compiled);
        private static Regex ParameterRegQuestionMask = new Regex("[?]", RegexOptions.Compiled);
        //private static Dictionary<string, IDataParameter[]> SqlParameterCache = new Dictionary<string, IDataParameter[]>();
        /// <summary>
        /// 私有方法, 从SQL语句中生成参数数组
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="prepare"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        private static IDataParameter[] GetParametersFromSQL(string SQL)
        {
            int iAlt = SQL.IndexOf('@');
            int iQue = SQL.IndexOf('?');
            if (iAlt < 0 && iQue < 0)
                return new IDataParameter[0];

            IDataParameter[] idpList = null;

            try
            {


                if (iAlt >= 0)
                {
                    MatchCollection matchs = ParameterReg.Matches(SQL);
                    Dictionary<string, IDataParameter> paraList = new Dictionary<string, IDataParameter>();

                    foreach (Match m in matchs)
                    {
                        if (!paraList.ContainsKey(m.Value))
                        {
                            IDataParameter idp = new System.Data.Odbc.OdbcParameter();
                            idp.ParameterName = m.Value;
                            idp.Direction = ParameterDirection.Input;



                            paraList.Add(m.Value, idp);
                        }
                    }

                    idpList = new IDataParameter[paraList.Count];
                    paraList.Values.CopyTo(idpList, 0);
                }
                else if (iQue >= 0)
                {
                    MatchCollection matchs = ParameterRegQuestionMask.Matches(SQL);

                    idpList = new IDataParameter[matchs.Count];
                    for (int i = 0; i < idpList.Length; i++)
                    {
                        IDataParameter parameter = new System.Data.Odbc.OdbcParameter();
                        parameter.ParameterName = "";
                        parameter.Value = null;

                        idpList[i] = parameter;
                    }
                }

                //if (SqlParameterCache.ContainsKey(SQL) == false)
                //    SqlParameterCache.Add(SQL, idpList);


                return idpList;
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "GetParametersFromSQL抛出错误", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog(SQL, "GetParametersFromSQL抛出错误", "程序报错抛出错误");

                throw new EggsoftWX.SQLServerDAL.Exceptions.DataAccessException(ex.Message, SQL);
            }

        }

    }
}
