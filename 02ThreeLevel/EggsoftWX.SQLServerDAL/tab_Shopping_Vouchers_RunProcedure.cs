using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EggsoftWX.IDAL;
using System.Collections.Generic;
using Eggsoft.Common;

namespace EggsoftWX.SQLServerDAL
{
    /// <summary>
    /// 数据访问类tab_Shopping_Vouchers。
    /// </summary>
    public class tab_Shopping_Vouchers_RunProcedure : Itab_Shopping_Vouchers_RunProcedure
        {
        public tab_Shopping_Vouchers_RunProcedure()
        {}
        #region  成员方法
            
            
       #region 增加2000条数据 model  memo 002000
       /// <summary>
		/// 增加一条数据 model
		/// </summary>
        public Boolean AddAllNum(string [] strSetstrValue)
        {

            IDataParameter[] iData = new SqlParameter[7];
            iData[0] = new SqlParameter("@ShopClientID", strSetstrValue[0].toInt32());
            iData[1] = new SqlParameter("@Scheme_ID", strSetstrValue[1].toInt32());
            iData[2] = new SqlParameter("@Money", strSetstrValue[2].toDecimal());
            iData[3] = new SqlParameter("@AllNum", strSetstrValue[3].toInt32());
            iData[4] = new SqlParameter("@ValidateStartTime", Convert.ToDateTime(strSetstrValue[4]));
            iData[5] = new SqlParameter("@ValidateEndTime", Convert.ToDateTime(strSetstrValue[5]));


            //iData[0] = new SqlParameter("@AllNum",Convert.ToInt32(strSetstrValue[0]));
            //iData[1] = new SqlParameter("@Vouchers_Title",Convert.ToString(strSetstrValue[1]));
            //iData[2] = new SqlParameter("@Vouchers_Des", Convert.ToString(strSetstrValue[2]));
            //iData[6] = new SqlParameter("@Validate", Convert.ToBoolean(strSetstrValue[6]));
            //iData[7] = new SqlParameter("@UpdateTime", Convert.ToDateTime(strSetstrValue[7]));
            // iData[9] = new SqlParameter("@UserID", Convert.ToInt32(strSetstrValue[9]));
         
            //IDataParameter[] iData = new SqlParameter[9];
            //iData[0] = new SqlParameter("@TableName", "tab_Shopping_Vouchers");
            //iData[1] = new SqlParameter("@Vouchers_Title", "tab_Shopping_Vouchers");
            //iData[2] = new SqlParameter("@ValidateStartTime", "tab_Shopping_Vouchers");
            //iData[3] = new SqlParameter("@ValidateEndTime", "tab_Shopping_Vouchers");
            //iData[4] = new SqlParameter("@Money", "tab_Shopping_Vouchers");
            //iData[5] = new SqlParameter("@Validate", "tab_Shopping_Vouchers");
            //iData[6] = new SqlParameter("@UpdateTime", "tab_Shopping_Vouchers");
            //iData[7] = new SqlParameter("@AllNum", "tab_Shopping_Vouchers");       
            iData[6] = new SqlParameter("@DoFinshed", SqlDbType.Bit, 1, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null);
            string strReturn = DbHelperSQL.RunProcedure("RunProcedure_DoShopping_Vouchers", iData).ToString();
            return Convert.ToBoolean(iData[6].Value.ToString());
        }
       #endregion
         
     #endregion  成员方法
     }
}
