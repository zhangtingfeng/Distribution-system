using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Eggsoft.Common
{
    /// <summary>
    ///Pub 的摘要说明
    /// </summary>
    public class Marker_ErJiYuMing_tab_DoTask_Services
    {



        public Marker_ErJiYuMing_tab_DoTask_Services()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        public static void insertATask(String strYuMingHead)
        {

            string strMaxID = "0";
            string ConnStr = ConfigurationManager.ConnectionStrings["Shop.Earth17.Com_ConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(ConnStr);

            SqlCommand cmd = null;
            String strSQL = "";
            strSQL = "Select max(ID) as MaxID From tab_DoTask_Services";
            cmd = new SqlCommand(strSQL, conn);
            cmd.Connection.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                if (sdr.Read())
                {
                    strMaxID = sdr["MaxID"].ToString();
                }
                sdr.Close();
            }
            //cmd.Close(); //关闭连接 
            cmd.Connection.Close();


            //cmd.Connection.Close();
            //INSERT INTO 表名称 VALUES (值1, 值2,....)  INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)
            strSQL = "insert tab_DoTask_Services (TaskType,TaskXML,TaskIfDone,InsertTime,TaskMemo,ID)  VALUES ('ErJiYuMing_ShopEarth17','" + strYuMingHead + "','0','" + DateTime.Now + "','01ClientShop'," + (Int32.Parse(strMaxID) + 1).ToString() + ")";//set MarkerContent='" + argstrTextBox.Trim() + "',UpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + " where " + strWhere;
            cmd = new SqlCommand(strSQL, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            conn.Close();

        }
    }
}