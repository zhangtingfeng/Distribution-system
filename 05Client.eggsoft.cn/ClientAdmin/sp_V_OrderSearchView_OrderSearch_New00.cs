using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _05Client.eggsoft.cn.ClientAdmin
{
    public class sp_V_OrderSearchView_OrderSearch_New00
    {
        public bool sp_V_OrderSearchView_OrderSearch_New00_StoredProcedure(int intShopClientID, string strStartTime, string strEndTime)
        {
            IDataParameter[] iData = new SqlParameter[4];
            iData[0] = new SqlParameter("@ShopClientID", intShopClientID.ToString());
            iData[1] = new SqlParameter("@StartTime", strStartTime);
            iData[2] = new SqlParameter("@EndTime", strEndTime);
            iData[3] = new SqlParameter("@OkDo", SqlDbType.Bit, 1, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null);
            string strReturn = EggsoftWX.SQLServerDAL.DbHelperSQL.RunProcedure("sp_V_OrderSearchView_OrderSearch_New00", iData).ToString();
            string strOKDo = iData[3].Value.ToString();
            return bool.Parse(strOKDo);
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
    }
}