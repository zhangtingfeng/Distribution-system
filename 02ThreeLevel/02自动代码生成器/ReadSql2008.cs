using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _02WFA
{
    /// <summary>
    /// 用于 访问 ReadSql2008 数据库的例子.
    /// 
    /// 
    /// </summary>
    public class ReadSql2008
    {

        /// <summary>
        /// Access 的数据库连接字符串.
        /// </summary>
        private String connString = getConn();//webuy8_f;Password=eggsoft.cn!QAZ2wsx


        //  string _connectionString = ConfigurationSettings.AppSettings["preFix"]; 

        //private const String connString = "Data Source=112.124.103.9,3699;Initial Catalog=eggsoft.cn;Persist Security Info=True;User ID=webuy8_f;Password=eggsoft.cn!QAZ2wsx";

        private static string getConn()
        {
          

            return FileIOMy.readFile("sql.txt");
        }

        public String strGetOutPut = getOutPut();//webuy8_f;Password=eggsoft.cn!QAZ2wsx

        //private const String connString = "Data Source=112.124.103.9,3699;Initial Catalog=eggsoft.cn;Persist Security Info=True;User ID=webuy8_f;Password=eggsoft.cn!QAZ2wsx";

        private static string getOutPut()
        {
            return FileIOMy.readFile("OutPut.txt");
        }


        /// <summary>
        /// 用于查询的 SQL 语句.
        /// </summary>
        //private const String SQL = "SELECT admin_name, admin_pwd FROM Eggsoft_admin";
        // private const String SQL = "SELECT admin_name, admin_pwd FROM Eggsoft_admin";


        /// <summary>
        /// DataSet 导出的文件
        /// </summary>
        private const String DATATABLE_XML_FILE = "datatable.xml";
        private const String DATATABLE_SCHEMA_XML_FILE = "datatable_schema.xml";

        public DataTable GetAllTableToDataSet()
        {
            try
            {
             

                SqlConnection chkConn = new SqlConnection(connString);
                chkConn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * FROM SysObjects  where (XType='U' or XType='v')", chkConn);//Where XType='U' orDER BY Name
                DataTable dt = new DataTable();
                da.Fill(dt);


                // DataTable schemaTable = chkConn.GetSchema(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                chkConn.Close();

                return dt;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return null;
                throw e;

            }
            finally
            {

            }
            //            try { //执行的代码，其中可能有异常。一旦发现异常，则立即跳到catch执行。否则不会执行catch里面的内容 }

            //catch { //除非try里面执行代码发生了异常，否则这里的代码不会执行 }

            //finally { //不管什么情况都会执行，包括try catch 里面用了return ,可以理解为只要执行了try或者catch，就一定会执行 finally }
        }
        public DataTable GetAllTableToDataSet(ListBox ListBox1)
        {
            try
            {
                SqlConnection chkConn = new SqlConnection(connString);
                chkConn.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * FROM SysObjects  where (XType='U' or XType='v') order by name asc", chkConn);//Where XType='U' orDER BY Name
                DataTable dt = new DataTable();
                da.Fill(dt);

                Int32 intAllLength = dt.Rows.Count;
                Int32 inti = 0;
                //foreach (DataRow dr in dt.Rows)
                for (Int32 i = 0; i < intAllLength; i++)
                {
                    DataRow dr = dt.Rows[i];
                    inti++;
                    try
                    {
                        //表名 
                        //ListBox1.Items.
                        //string strname = dr["NAME"].ToString().Trim();
                        string strCountSelec = "select count(*) as CountLines from " + dr["NAME"].ToString().Trim();
                        SqlDataAdapter daLines = new SqlDataAdapter(strCountSelec, chkConn);//Where XType='U' orDER BY Name
                        DataTable dtlines = new DataTable();
                        daLines.Fill(dtlines);

                        //ListBox1.Items.Add(dr["NAME"].ToString().Trim() + "," + inti.ToString() + " 共" + intAllLength + " " + dr["XType"].ToString().Trim() + "," + dtlines.Rows[0][0]);
                        ListBox1.Items.Add(dr["NAME"].ToString().Trim() + "," + dr["XType"].ToString().Trim() + "," + dtlines.Rows[0][0]);

                    }
                    catch (Exception eeee)
                    {
                        System.Console.WriteLine("非自定义异常。其值为：{0}", eeee);
                    }
                    finally
                    {

                    }
                    //Console.WriteLine(dr["NAME"].ToString().Trim() + "  " + inti.ToString() + " 共" + intAllLength);
                }


                chkConn.Close();
                return null;
            }
            catch (Exception eeee)
            {
                System.Console.WriteLine("非自定义异常。其值为：{0}", eeee);
                return null;
            }
        }


        public DataTable GetAllTableToDataSetItem(DataGridView dgv, string strTablenaName)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SysColumns WHERE id=Object_Id('"+strTablenaName+"')", connString);//Where XType='U' orDER BY Name
            SqlDataAdapter da = new SqlDataAdapter("exec sp_columns " + strTablenaName, connection);//查询表中列的信息);//Where XType='U' orDER BY Name
            DataTable table = new DataTable();
            da.Fill(table);
            connection.Close();
            return table;
            // return null;     
        }


        public string GetItemDES(string strTablename, string strItemName)
        {
            SqlConnection connection = new SqlConnection(connString);
            //connection.Open();

            string strGetDesc = "";
            String strLongSQL = "select   b.[value] from sys.columns a left join sys.extended_properties b on a.object_id=b.major_id";
            strLongSQL += " and a.column_id=b.minor_id inner join sysobjects c on a.column_id=c.id";
            strLongSQL += " and a.[name]='列名' and c.[name]='表名'";
            strLongSQL += " SELECT";
            strLongSQL += " 表名=case   when   a.colorder=1   then   d.name   else   ''   end,";
            strLongSQL += " 表说明=case   when   a.colorder=1   then   isnull(f.value,'')   else   ''   end,";
            strLongSQL += " 字段序号=a.colorder,";
            strLongSQL += " 字段名=a.name,";
            strLongSQL += " 标识=case   when   COLUMNPROPERTY(   a.id,a.name,'IsIdentity')=1   then   '√'else   ''   end,";
            strLongSQL += " 主键=case   when   exists(SELECT   1   FROM   sysobjects   where   xtype='PK'   and   name   in   (";
            strLongSQL += " SELECT   name   FROM   sysindexes   WHERE   indid   in(";
            strLongSQL += " SELECT   indid   FROM   sysindexkeys   WHERE   id   =   a.id   AND   colid=a.colid";
            strLongSQL += " )))   then   '√'   else   ''   end,";
            strLongSQL += " 类型=b.name,";
            strLongSQL += " 占用字节数=a.length,";
            strLongSQL += " 长度=COLUMNPROPERTY(a.id,a.name,'PRECISION'),";
            strLongSQL += " 小数位数=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),";
            strLongSQL += " 允许空=case   when   a.isnullable=1   then   '√'else   ''   end,";
            strLongSQL += " 默认值=isnull(e.text,''),";
            strLongSQL += " 字段说明=isnull(g.[value],''),";
            strLongSQL += " a.id ";
            strLongSQL += " FROM   syscolumns  a";
            strLongSQL += " left   join   systypes   b   on   a.xusertype=b.xusertype";
            strLongSQL += " inner   join   sysobjects   d   on   a.id=d.id     and   d.xtype='U'   and     d.name<>'dtproperties'";
            strLongSQL += " left   join   syscomments   e   on   a.cdefault=e.id";
            strLongSQL += " left   join   sys.extended_properties   g   on   a.id=g.major_id   and   a.colid=g.minor_id";
            strLongSQL += " left   join   sys.extended_properties   f   on   d.id=f.major_id   and   f.minor_id=0";
            strLongSQL += " where   d.name='" + strTablename + "'  and a.name='" + strItemName + "'        ";/// --如果只查询指定表,加上此条件
            strLongSQL += " order   by   a.id,a.colorder";

            //strLongSQL = "Select * FROM SysObjects  where (XType='U' or XType='v')";

            SqlDataAdapter da = new SqlDataAdapter(strLongSQL, connection);//查询表中列的信息);//Where XType='U' orDER BY Name

            DataSet ds111 = new DataSet();
            da.Fill(ds111, "MyTableName");// SqlDataAdapter填充指定DataSet的特定表。


            //DataTable table = new DataTable();
            //da.Fill(table);

            if (ds111.Tables[1].Rows.Count > 0)
            {
                strGetDesc = ds111.Tables[1].Rows[0]["字段说明"].ToString();
                strGetDesc += " " + (ds111.Tables[1].Rows[0]["主键"].ToString() == "√" ? "主键" : "");
                strGetDesc += " " + ds111.Tables[1].Rows[0]["类型"].ToString();
                strGetDesc += " 长度" + ds111.Tables[1].Rows[0]["长度"].ToString();
                strGetDesc += " 占用字节数" + ds111.Tables[1].Rows[0]["占用字节数"].ToString();
                strGetDesc += " 小数位数" + ds111.Tables[1].Rows[0]["小数位数"].ToString();
                strGetDesc += " " + (ds111.Tables[1].Rows[0]["允许空"].ToString() == "√" ? "允许空" : "不允许空");
                strGetDesc += " 默认值" + (ds111.Tables[1].Rows[0]["默认值"].ToString() == "" ? "无" : ds111.Tables[1].Rows[0]["默认值"].ToString());

            }
            connection.Close();
            //connection.Close();
            return strGetDesc;
            // return null;     
        }

    }
}
