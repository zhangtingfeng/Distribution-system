using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Eggsoft_Public_CL
{
    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class APPRunProcedure
    {
        public APPRunProcedure()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static void CheckAuthorTime()
        {
            string pGoodID = HttpContext.Current.Request.QueryString["GoodID"];
            String strShopClientID = "0";
            if (String.IsNullOrEmpty(pGoodID) == false)
            {
                if (new EggsoftWX.BLL.tab_Goods().Exists("ID=" + pGoodID))
                {

                    strShopClientID = new EggsoftWX.BLL.tab_Goods().GetList("ShopClient_ID", "ID=" + pGoodID).Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    Eggsoft.Common.JsUtil.ShowMsg("授权已过期！该店铺将关闭！");
                    Eggsoft.Common.JsUtil.CloseWindow();
                }
            }
            else
            {
                strShopClientID = HttpContext.Current.Request.QueryString["ID"];
            }

            if (strShopClientID != null)
            {
                IDataParameter[] iData = new SqlParameter[2];
                iData[0] = new SqlParameter("@ShopClient_ID", strShopClientID);
                iData[1] = new SqlParameter("@AuthorStatus", SqlDbType.BigInt, 8, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null);
                string strReturn = EggsoftWX.SQLServerDAL.DbHelperSQL.RunProcedure("RunProcedure_GetAuthor", iData).ToString();
                int intOutAuthorStatus = Convert.ToInt32(iData[1].Value.ToString());
                if (intOutAuthorStatus <= 0)
                {
                    //Eggsoft.Common.JsUtil.ShowMsg("授权已过期！该店铺将关闭！");
                    // Eggsoft.Common.JsUtil.CloseWindow();
                }
            }


        }


    }

}