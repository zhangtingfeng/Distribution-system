using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace Eggsoft.Common
{
    public class Excel
    {

        public static DataSet ImportExcel(string SexcelName)
        {
            //文件路径
            string strLogPath = "";
            try
            {
                strLogPath = System.Web.HttpContext.Current.Server.MapPath(@"/InsureDocument/");
            }
            catch
            {
                strLogPath = Eggsoft.Common.FileFolder.GetAssemblyPath();
                strLogPath = strLogPath.Remove(strLogPath.Length - ("/bin/").Length, "/bin/".Length) + "/InsureDocument/";
            }
            finally { }

            string ExcelName = strLogPath + @"\01QinYiBao\" + SexcelName;
            if (File.Exists(ExcelName) == false)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ExcelName + "  不存在");
            }
            else
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ExcelName + "  正常调用");
            }
            string strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=2';";//连接excel文件的字符串　当 IMEX=2 时为“连結模式”，这个模式开启的 Excel 档案可同时支援“读取”与“写入”用途。
            if (ExcelName == null)
            {
                return null;
            }
            OleDbConnection odcon = new OleDbConnection(strcon);//建立连接
            odcon.Open();//打开连接
            System.Data.DataTable sTable = odcon.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            //Sheets Name
            string tableName = sTable.Rows[0][2].ToString().Trim();
            if (tableName == "")
            {
                return null;
            }
            else
            {
                tableName = "[" + tableName + "]";
            }
            OleDbDataAdapter odda = new OleDbDataAdapter("select * from " + tableName, odcon);
            DataSet ds = new DataSet();
            try
            {
                odda.Fill(ds);
                odda.Dispose();
                odcon.Close();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return ds;
        }


        /// 将DataTable中数据写入到CSV文件中
        ///

        /// 提供保存数据的DataTable
        /// CSV的文件路径
        public static void SaveCSV(DataTable dt, string strTypename = "")
        {
            string strLogPath = "";
            try
            {
                strLogPath = System.Web.HttpContext.Current.Server.MapPath(@"/logs/");
            }
            catch
            {
                strLogPath = Eggsoft.Common.FileFolder.GetAssemblyPath();
                strLogPath = strLogPath.Remove(strLogPath.Length - ("/bin/").Length, "/bin/".Length) + "/logs/";
            }


            if (!Directory.Exists(strLogPath))
            {
                Directory.CreateDirectory(strLogPath);
            }
            string filename = "";


            filename = strLogPath + "/" + strTypename + "log" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (strTypename == "SQLServer") filename = strLogPath + "/" + strTypename + "log" + System.DateTime.Now.ToString("yyyyMMddHH") + ".txt";//每小时执行

         

            FileStream fs = new FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";
            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            //ddMessageBox.Show("CSV文件保存成功！");
        }

    }
}
