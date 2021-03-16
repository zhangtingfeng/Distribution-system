using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _02WFA
{
    public partial class SQL2008Form : Form
    {

        public String str02ThreeLevel = "";

        public SQL2008Form()
        {
            InitializeComponent();
        }

        private void SQL2008Form_Load(object sender, EventArgs e)
        {
            ReadSql2008 myReadSql2008 = new ReadSql2008();
            str02ThreeLevel = myReadSql2008.strGetOutPut;
            dataGridView2.DataSource = myReadSql2008.GetAllTableToDataSet();
            myReadSql2008.GetAllTableToDataSet(listBox1);
            listBox1.Sorted = true;


            //string ddd = myReadSql2008.ToString();
        }

        private void ReadTable(string strTablename)
        {
            //if ( listBox1.SelectedItem==null ) {return;}
            //dataGridView1.DataSource = null;
            ReadSql2008 myReadSql2008 = new ReadSql2008();

            DataTable myDataTable = myReadSql2008.GetAllTableToDataSetItem(dataGridView2, strTablename);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = myDataTable;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string[] sArray1;
            string strTablename;
            string strView;
            if (this.listBox1.SelectedIndex != -1)
            {
                sArray1 = this.listBox1.Items[this.listBox1.SelectedIndex].ToString().Split(new char[] { ',' });
                strTablename = sArray1[0];


                strView = sArray1[1];
                this.ReadTable(strTablename);
                this.writeModel(strTablename, strView);
                //this.writeOleDbDAL(strTablename, strView);
                //this.writeOleDbDAL_DbHelperOleDb();
                this.writeIDAL(strTablename, strView);
                this.writeDALFactory(strTablename);
                //this.writeDALFactory_DataCache();
                this.writeBLL(strTablename, strView);
                this.writeSQLServerDAL(strTablename, strView);

            }
            else
            {
                for (Int32 i = 0; i <= (this.listBox1.Items.Count - 1); i++)
                {
                    sArray1 = this.listBox1.Items[i].ToString().Split(new char[] { ',' });
                    strTablename = sArray1[0];

                    strView = sArray1[1];
                    this.ReadTable(strTablename);
                    this.writeModel(strTablename, strView);
                    //this.writeOleDbDAL(strTablename, strView);
                    //this.writeOleDbDAL_DbHelperOleDb();
                    this.writeIDAL(strTablename, strView);
                    this.writeDALFactory(strTablename);
                    //this.writeDALFactory_DataCache();
                    this.writeBLL(strTablename, strView);
                    this.writeSQLServerDAL(strTablename, strView);

                }
            }
        }



        private void write(string strName)
        {
            string[] sArray1 = strName.ToString().Split(',');
            string strTablename = sArray1[0];
            string strView = sArray1[1];
            ReadTable(strTablename);
            ///return;
            //if (i > 0) return;
            writeModel(strTablename, strView);

            //writeOleDbDAL(strTablename, strView);
            //writeOleDbDAL_DbHelperOleDb();
            // return;
            writeIDAL(strTablename, strView);
            writeDALFactory(strTablename);
            //writeDALFactory_DataCache();
            writeBLL(strTablename, strView);
            ////
            writeSQLServerDAL(strTablename, strView);
            //writeSQLServerDAL_DbHelperSQL();        
        }



        private void writeBLL(string strTablename, string strView)
        {
            this.richTextBox1.Clear();
            this.richTextBox1.AppendText("using System;\n");
            this.richTextBox1.AppendText("using System.Data;\n");
            this.richTextBox1.AppendText("using System.Text;\n");
            this.richTextBox1.AppendText("using System.Data.OleDb;\n");
            this.richTextBox1.AppendText("using EggsoftWX.IDAL;\n");
            this.richTextBox1.AppendText("namespace EggsoftWX.BLL\n");
            this.richTextBox1.AppendText("{\n");

            this.richTextBox1.AppendText("\t/// <summary>\n");
            this.richTextBox1.AppendText("\t/// 业务逻辑类" + strTablename + " 的摘要说明。\n");
            this.richTextBox1.AppendText("\t/// </summary>\n");
            this.richTextBox1.AppendText("\tpublic class " + strTablename + "\n");
            this.richTextBox1.AppendText("\t{\n");
            this.richTextBox1.AppendText("\t\tI" + strTablename + " dal=EggsoftWX.DALFactory." + strTablename + ".Create();\n");
            this.richTextBox1.AppendText("\t\tpublic " + strTablename + "()\n");
            this.richTextBox1.AppendText("\t\t{}\n");
            this.richTextBox1.AppendText("\t\t#region  成员方法\n");
            this.richTextBox1.AppendText("\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 1 得到最大ID\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic Int32 GetMaxId()\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn dal.GetMaxId();\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");

            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 2 增加一条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic Int32 Add(EggsoftWX.Model." + strTablename + " model)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\treturn dal.Add(model);\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("\n");
            }

            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 3 增加一条数据 自定义 string strSet,string strValue\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic Int32 Add(string strSet,string strValue, params object[] objs)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\treturn dal.Add(strSet,strValue,objs);\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("\n");
            }
            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 是否存在该记录 strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic bool Exists(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn dal.Exists(strWhere,objs);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 是否存在该记录\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic bool Exists(Int32 ID)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn dal.Exists(ID);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");



            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 存在该记录条数  ---------memo6\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic Int32 ExistsCount(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn dal.ExistsCount(strWhere,objs);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");

            this.richTextBox1.AppendText("\n");
            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 直接选择   ---------memo7  \n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\t/// <param name=\"strSelect\"></param>\n");
            this.richTextBox1.AppendText("\t\t/// <returns></returns>\n");
            this.richTextBox1.AppendText("\t\tpublic DataSet SelectList(string strSelect, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t    return dal.SelectList(strSelect,objs);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");


            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得数据列表GetList1 string strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic DataSet GetList(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn dal.GetList(strWhere,objs);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得数据列表GetList2 string strItem,string strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic DataSet GetList(string strItem,string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn dal.GetList(strItem,strWhere,objs);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 得到一个对象实体 strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic EggsoftWX.Model." + strTablename + " GetModel(Int32 ID)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn dal.GetModel(ID);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\n");

            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("        /// 得到一个对象实体 strWhere\n");
            this.richTextBox1.AppendText("        /// </summary>\n");
            this.richTextBox1.AppendText("        public EggsoftWX.Model." + strTablename + " GetModel(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("        {\n");
            this.richTextBox1.AppendText("            return dal.GetModel(strWhere,objs);\n");
            this.richTextBox1.AppendText("        }\n");

            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// delete strWhere 删除n条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic void Delete(string strWhere, params object[] objs)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\tdal.Delete(strWhere,objs);\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("\n");
            }

            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// region delete ID 删除一条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic int Delete(Int32 ID)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\treturn dal.Delete(ID);\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("\n");
            }

            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// update strSet strWhere 更新n条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic int Update(string strSet,string strWhere, params object[] objs)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\treturn dal.Update(strSet,strWhere,objs);\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("\n");
            }

            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t///model update 更新一条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic int Update(EggsoftWX.Model." + strTablename + " model)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\treturn dal.Update(model);\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("\n");
            }

            this.richTextBox1.AppendText("//ztf modify 2010-10-19\n");
            this.richTextBox1.AppendText("        public DataTable GetDataTable(string topNum, string fields, string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("        {\n");
            this.richTextBox1.AppendText("            return dal.GetList(topNum, fields, strWhere,objs);\n");
            this.richTextBox1.AppendText("        }\n");
            this.richTextBox1.AppendText("        //分页列表\n");
            this.richTextBox1.AppendText("        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string conditions, string orderField, bool isDesc, params object[] objs)\n");
            this.richTextBox1.AppendText("        {\n");
            this.richTextBox1.AppendText("            return dal.GetPageDataTable(pageIndex, pageSize, fields, conditions, orderField, isDesc,objs);\n");
            this.richTextBox1.AppendText("        }\n");
            this.richTextBox1.AppendText("\n");
            this.richTextBox1.AppendText("\n");
            this.richTextBox1.AppendText("\t\t#endregion  成员方法\n");
            this.richTextBox1.AppendText("\t}\n");
            this.richTextBox1.AppendText("}\n");
            FileIOMy.writeFile(str02ThreeLevel + @"EggsoftWX.BLL\" + strTablename + ".cs", this.richTextBox1.Text);

        }

        private void writeDALFactory(string strTablename)
        {
            this.richTextBox1.Clear();
            this.richTextBox1.AppendText("using System;\n");
            this.richTextBox1.AppendText("using System.Reflection;\n");
            this.richTextBox1.AppendText("using System.Configuration;\n");
            this.richTextBox1.AppendText("using EggsoftWX.IDAL;\n");
            this.richTextBox1.AppendText("namespace EggsoftWX.DALFactory\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("\t/// <summary>\n");
            this.richTextBox1.AppendText("\t/// 工厂类" + strTablename + " 的摘要说明。\n");
            this.richTextBox1.AppendText("\t/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口)  \n");
            this.richTextBox1.AppendText("\t/// DataCache类在导出代码的文件夹里\n");
            this.richTextBox1.AppendText("\t/// <appSettings>  \n");
            this.richTextBox1.AppendText("\t/// <add key=\"DAL\" value=\"EggsoftWX.SQLServerDAL\" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)\n");
            this.richTextBox1.AppendText("\t/// </appSettings> \n");
            this.richTextBox1.AppendText("\t/// </summary>\n");
            this.richTextBox1.AppendText("\tpublic class " + strTablename + "\n");
            this.richTextBox1.AppendText("\t{\n");
            this.richTextBox1.AppendText("\t\tpublic static EggsoftWX.IDAL.I" + strTablename + " Create()\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\tstring path = System.Configuration.ConfigurationManager.AppSettings[\"EggsoftWX.DALFactory\"];\n");
            this.richTextBox1.AppendText("\t\t\tstring CacheKey = path+\"." + strTablename + "\";\n");
            this.richTextBox1.AppendText("\t\t\tobject objType = Eggsoft.Common.DataCache.GetCache(CacheKey);\n");
            this.richTextBox1.AppendText("\t\t\tif (objType == null)\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\ttry\n");
            this.richTextBox1.AppendText("\t\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\t\tobjType = Assembly.Load(path).CreateInstance(CacheKey);\n");
            this.richTextBox1.AppendText("\t\t\t\t\tEggsoft.Common.DataCache.SetCache(CacheKey, objType);// 写入缓存\n");
            this.richTextBox1.AppendText("\t\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\t\tcatch{}\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\treturn (I" + strTablename + ")objType;\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("\t}\n");
            this.richTextBox1.AppendText("}\n");
            FileIOMy.writeFile(str02ThreeLevel + @"EggsoftWX.DALFactory\" + strTablename + ".cs", this.richTextBox1.Text);

        }

        private void writeIDAL(string strTablename, string strView)
        {
            this.richTextBox1.Clear();
            this.richTextBox1.AppendText("using System;\n");
            this.richTextBox1.AppendText("using System.Data;\n");
            this.richTextBox1.AppendText("using System.Collections.Generic;\n");
            this.richTextBox1.AppendText("namespace EggsoftWX.IDAL\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("\t/// <summary>\n");
            this.richTextBox1.AppendText("\t/// 接口层I" + strTablename + " 的摘要说明。\n");
            this.richTextBox1.AppendText("\t/// </summary>\n");
            this.richTextBox1.AppendText("\tpublic interface I" + strTablename + "\n");
            this.richTextBox1.AppendText("\t{\n");
            this.richTextBox1.AppendText("\t\t#region  成员方法\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 得到最大ID\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");

            this.richTextBox1.AppendText("\t\tint GetMaxId(); //memo 0001\n");
            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 增加一条数据 model\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\tint Add(EggsoftWX.Model." + strTablename + " model); //memo 0002\n");
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 增加一条数据 自定义 string strSet,string strValue\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
            }
            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\tint Add(string strSet,string strValue, params object[] objs); //memo 0003\n");
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 是否存在该记录 strWhere\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
            }

            this.richTextBox1.AppendText("\t\tbool Exists(string strWhere, params object[] objs); //memo 0004\n");
            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 是否存在该记录 ID\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");

            this.richTextBox1.AppendText("\t\tbool Exists(Int32 ID); //memo 0005\n");


            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 存在该记录条数\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tint ExistsCount(string strWhere, params object[] objs); //memo 0006\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得数据列表SelectList string strSelect\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tDataSet SelectList(string strSelect, params object[] objs); //memo 0007\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得数据列表GetList string strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tDataSet GetList(string strWhere, params object[] objs); //memo 0008\n");
            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得数据列表GetList2 string strItem,string strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("        DataSet GetList(string strItem, string strWhere, params object[] objs);//memo 0009\n");

            this.richTextBox1.AppendText("        /// <summary>\n");
            this.richTextBox1.AppendText("        /// 得到一GetFieldValues\n");
            this.richTextBox1.AppendText("        /// </summary>\n");
            this.richTextBox1.AppendText("        IList<string> GetFieldValues(string fields, string strWhere, params object[] objs);// memo 0010\n");
            this.richTextBox1.AppendText("        /// <summary>\n");
            this.richTextBox1.AppendText("        /// 得到一个GetFieldValues\n");
            this.richTextBox1.AppendText("        /// </summary>\n");
            this.richTextBox1.AppendText("        IList<string> GetFieldValues(string topNum, string fields, string strWhere, params object[] objs);//memo 0011\n");

            this.richTextBox1.AppendText("        /// <summary>\n");
            this.richTextBox1.AppendText("        /// 得到一个对象实体  Scalar\n");
            this.richTextBox1.AppendText("        /// </summary>\n");
            this.richTextBox1.AppendText("        object Scalar(string field, string strWhere, params object[] objs);//memo 0012\n");
            this.richTextBox1.AppendText("        \n");
            this.richTextBox1.AppendText("        /// <summary>\n");
            this.richTextBox1.AppendText("        /// 得到一个对象实体  GetModel\n");
            this.richTextBox1.AppendText("        /// </summary>\n");
            this.richTextBox1.AppendText("        EggsoftWX.Model." + strTablename + " GetModel(string strWhere, params object[] objs);//memo 0013\n");

            this.richTextBox1.AppendText("\t\t/// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 得到一个对象实体 strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tEggsoftWX.Model." + strTablename + " GetModel(Int32 ID); //memo 0014\n");

            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// delete strWhere 删除n条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tint Delete(string strWhere, params object[] objs);//memo 0015\n");
            }
            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// region delete ID 删除一条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tint Delete(Int32 ID);//memo 0016\n");
            }
            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// update strSet strWhere 更新n条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tint Update(string strSet,string strWhere, params object[] objs);//memo 0017\n");
            }
            if (strView == "U")
            {
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t///model update 更新一条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tint Update(EggsoftWX.Model." + strTablename + " model);//memo 0018\n");
            }
            this.richTextBox1.AppendText("        DataTable GetList(string topNum, string fields, string strWhere, params object[] objs);//memo 0019\n");
            this.richTextBox1.AppendText("        \n");
            this.richTextBox1.AppendText("        //分页列表\n");
            this.richTextBox1.AppendText("        DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string conditions, string orderField, bool isDesc, params object[] objs);//memo 0020\n");
            this.richTextBox1.AppendText("\t\t#endregion  成员方法\n");
            this.richTextBox1.AppendText("\t}\n");
            this.richTextBox1.AppendText("}\n");
            FileIOMy.writeFile(str02ThreeLevel + @"EggsoftWX.IDAL\" + strTablename + ".cs", this.richTextBox1.Text);

        }


        private void writeDALFactory_DataCache()
        {
            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。 
            string str = System.Environment.CurrentDirectory;
            string strMother = str + @"\DataCache.cs";
            richTextBox1.Text = FileIOMy.readFile(strMother);
            FileIOMy.writeFile(str02ThreeLevel + @"EggsoftWX.DALFactory\DataCache.cs", richTextBox1.Text);

        }


        /// <summary>
        /// 获取程序类型　
        /// </summary>
        /// <param name="sqltype">数据库类型</param>
        /// <returns>C＃类型</returns>
        private string GetType(string sqltype)
        {
            switch (sqltype.ToLower())
            {
                case "char":
                    return "string";

                case "nchar":
                    return "string";

                case "varchar":
                    return "string";

                case "nvarchar":
                    return "string";

                case "text":
                    return "string";

                case "ntext":
                    return "string";
                case "tinyint":
                    return "Int32";
                case "Int32":
                    return "Int32";
                case "bigint identity":
                    return "Int32";
                case "int":
                    return "int";

                case "smallint":
                    return "Int32";

                case "bigint":
                    return "Int32";

                case "Int32 identity":
                    return "Int32";

                case "float":
                    return "float";

                case "money":
                    return "decimal";

                case "numeric":
                    return "decimal";

                case "decimal":
                    return "decimal";

                case "datetime":
                    return "DateTime";

                case "bit":
                    return "bool";
            }
            return "string";

        }
        private void writeModel(string strTablename, string strView)
        {
            ReadSql2008 myReadSql2008 = new ReadSql2008();
          
            Int32 i;
            string strItemName;
            string strItemType;
            string strNULLABLE;
            this.richTextBox1.Clear();
            this.richTextBox1.AppendText("using System;\n");
            this.richTextBox1.AppendText("namespace EggsoftWX.Model\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("    /// <summary>\n");
            this.richTextBox1.AppendText("    /// 实体类" + strTablename + " 。(属性说明自动提取数据库字段的描述信息)\n");
            this.richTextBox1.AppendText("    /// </summary>\n");
            this.richTextBox1.AppendText("    public class " + strTablename + "\n");
            this.richTextBox1.AppendText("    {\n");
            this.richTextBox1.AppendText("        public " + strTablename + "()\n");
            this.richTextBox1.AppendText("        {}\n");
            this.richTextBox1.AppendText("        #region Model\n");
            this.richTextBox1.AppendText("        //");
            for (i = 0; i < (this.dataGridView1.RowCount - 1); i++)
            {
                this.richTextBox1.AppendText(this.dataGridView1.Rows[i].Cells["COLUMN_NAME"].Value.ToString() + ",");
            }
            this.richTextBox1.AppendText("\n");
            for (i = 0; i < (this.dataGridView1.RowCount - 1); i++)
            {
                strItemName = this.dataGridView1.Rows[i].Cells["COLUMN_NAME"].Value.ToString();
                strItemType = this.dataGridView1.Rows[i].Cells["TYPE_NAME"].Value.ToString();
                strNULLABLE = this.dataGridView1.Rows[i].Cells["NULLABLE"].Value.ToString();
                this.richTextBox1.AppendText("        private " + this.GetType(strItemType) + (((this.GetType(strItemType) != "string") && strNULLABLE == "1") ? "?" : "") + " _" + strItemName + ";\n");
            }
            for (i = 0; i < (this.dataGridView1.RowCount - 1); i++)
            {
                Int32 intddd = this.dataGridView1.Rows[i].Cells.Count;
                string strM = "";
                for (Int32 m = 0; m < (intddd - 1); m++)
                {
                    strM += this.dataGridView1.Rows[i].Cells[m].Value.ToString() + "   \"";
                }
                strItemName = this.dataGridView1.Rows[i].Cells["COLUMN_NAME"].Value.ToString();
                strItemType = this.dataGridView1.Rows[i].Cells["TYPE_NAME"].Value.ToString();
                strNULLABLE = this.dataGridView1.Rows[i].Cells["NULLABLE"].Value.ToString();
                //string strdata_type = this.dataGridView1.Rows[i].Cells["description"].Value.ToString();
                //string strCHARACTER_MAXIMUM_LENGTH = this.dataGridView1.Rows[i].Cells["CHARACTER_MAXIMUM_LENGTH"].Value.ToString();
                //string strcoldesc = this.dataGridView1.Rows[i].Cells["coldesc"].Value.ToString();
                // (case when CHARACTER_MAXIMUM_LENGTH is null then '' else CAST(CHARACTER_MAXIMUM_LENGTH as varchar(50)) end) as FieldLength
                //data_type ,data_length,data_precision,data_scale
                string strGetDesc = myReadSql2008.GetItemDES(strTablename, strItemName);////得到备注的字段名称
                this.richTextBox1.AppendText("        /// <summary>\n");
                this.richTextBox1.AppendText("        ///" + strGetDesc + " \n");
                this.richTextBox1.AppendText("        /// </summary>\n");
                this.richTextBox1.AppendText("        public " + this.GetType(strItemType) + (((this.GetType(strItemType) != "string") && strNULLABLE == "1") ? "?" : "") + " " + strItemName + "\n");
                this.richTextBox1.AppendText("        {\n");
                this.richTextBox1.AppendText("            set{ _" + strItemName + "=value;}\n");
                if ((this.GetType(strItemType).ToLower() == "datetime") && (strNULLABLE == "0"))
                {
                    this.richTextBox1.AppendText("            get{ if (_" + strItemName + " == DateTime.MinValue) _" + strItemName + " = DateTime.Now;return _" + strItemName + ";}\n");
                }
                else
                {
                    this.richTextBox1.AppendText("            get{return _" + strItemName + ";}\n");
                }
                this.richTextBox1.AppendText("        }\n");
            }
            this.richTextBox1.AppendText("        #endregion Model\n");
            this.richTextBox1.AppendText("    }\n");
            this.richTextBox1.AppendText("}\n");
            FileIOMy.writeFile(str02ThreeLevel + @"EggsoftWX.Model\" + strTablename + ".cs", this.richTextBox1.Text);
        }

        ////////////

        /// <summary>
        /// ////////////////
        /// </summary>
        /// 
        /// 
        private void writeSQLServerDAL(string strTablename, string strView)
        {
            Int32 i;
            string strItemName;
            string strItemType;
            string strNULLABLE;
            this.richTextBox1.Clear();
            this.richTextBox1.AppendText("using System;\n");
            this.richTextBox1.AppendText("using System.Data;\n");
            this.richTextBox1.AppendText("using System.Text;\n");
            this.richTextBox1.AppendText("using System.Data.SqlClient;\n");
            this.richTextBox1.AppendText("using EggsoftWX.IDAL;\n");
            this.richTextBox1.AppendText("using System.Collections.Generic;\n");
            this.richTextBox1.AppendText("namespace EggsoftWX.SQLServerDAL\n");
            this.richTextBox1.AppendText("{\n");
            this.richTextBox1.AppendText("    /// <summary>\n");
            this.richTextBox1.AppendText("    /// 数据访问类" + strTablename + "。\n");
            this.richTextBox1.AppendText("    /// </summary>\n");
            this.richTextBox1.AppendText("    public class " + strTablename + ":I" + strTablename + "\n");
            this.richTextBox1.AppendText("        {\n");
            this.richTextBox1.AppendText("        public " + strTablename + "()\n");
            this.richTextBox1.AppendText("        {}\n");
            this.richTextBox1.AppendText("        #region  成员方法\n");
            //if (strView == "U")
            //{
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("       #region 得到最大ID  memo 0001\n");
            this.richTextBox1.AppendText("       /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 得到最大ID\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic Int32 GetMaxId()\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
            this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"select max(ID)+1 from [" + strTablename + "]\");\n");
            this.richTextBox1.AppendText("\t\t\tobject obj=DbHelperSQL.GetSingle(strSql.ToString());\n");
            this.richTextBox1.AppendText("\t\t\tif(obj==null)\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\treturn 1;\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\telse\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\treturn Int32.Parse(obj.ToString());\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            //}
            if (strView == "U")
            {
                this.richTextBox1.AppendText("       #region 增加一条数据 model  memo 0002\n");
                this.richTextBox1.AppendText("       /// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 增加一条数据 model\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic Int32 Add(EggsoftWX.Model." + strTablename + " model)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\tIDataParameter[] iData = new SqlParameter[2];\n");
                this.richTextBox1.AppendText("\t\t\tiData[0] = new SqlParameter(\"@TableName\", \"" + strTablename + "\");\n");
                this.richTextBox1.AppendText("\t\t\tiData[1] = new SqlParameter(\"@MMaxID\", SqlDbType.BigInt, 8, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Default, null);\n");
                this.richTextBox1.AppendText("\t\t\tstring strReturn = DbHelperSQL.RunProcedure(\"RunProcedure_Insert_NeedID\", iData).ToString();\n");
                //this.richTextBox1.AppendText("\t\t\tEggsoft.Common.JsUtil.ShowMsg(iData[1].Value.ToString());\n");
                this.richTextBox1.AppendText("\t\t\tmodel.ID = Int32.Parse(iData[1].Value.ToString());\n");
                this.richTextBox1.AppendText("\t\t\tif (Update(model) > 0)\n");
                this.richTextBox1.AppendText("\t\t\t{\n");
                this.richTextBox1.AppendText("\t\t\t    return model.ID;\n");
                this.richTextBox1.AppendText("\t\t\t}\n");
                this.richTextBox1.AppendText("\t\t\telse\n");
                this.richTextBox1.AppendText("\t\t\t{\n");
                this.richTextBox1.AppendText("\t\t\t    return 0;\n");
                this.richTextBox1.AppendText("\t\t\t}\n");
                //this.richTextBox1.AppendText("\t\t\tUpdate(model);\n");
                //this.richTextBox1.AppendText("\t\t\treturn model.ID;\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("       #endregion\n");
                this.richTextBox1.AppendText("            \n");

            }




            if (strView == "U")///视图是V 
            {

                bool flag1 = strTablename == "paytype";
                this.richTextBox1.AppendText("       #region 增加一条数据 自定义 string strSet,string strValue memo 0003\n");
                this.richTextBox1.AppendText("       /// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 增加一条数据 string strSet,string strValue\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic Int32 Add(string strSet,string strValue, params object[] objs)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"insert into [" + strTablename + "]\");\n");
                if (strView == "U")
                {
                    this.richTextBox1.AppendText("            if (!strSet.ToLower().Contains(\"id\"))\n");
                    this.richTextBox1.AppendText("            {\n");
                    this.richTextBox1.AppendText("                strSet = strSet + \",ID\";\n");
                    this.richTextBox1.AppendText("                strValue = strValue + \",\" + GetMaxId().ToString();\n");
                    this.richTextBox1.AppendText("            }\n");
                }
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"(\" + strSet +\")\");\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(\" VALUES \");\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"(\" + strValue  +\")\");\n");
                this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
                this.richTextBox1.AppendText("\t\t\tDbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);\n");
                this.richTextBox1.AppendText("\t\t\treturn 1;\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("       #endregion\n");
                this.richTextBox1.AppendText("            \n");
            }

            this.richTextBox1.AppendText("       #region 是否存在该记录 strWhere  memo 0004\n");
            this.richTextBox1.AppendText("       /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 是否存在该记录\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic bool Exists(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("            strWhere = strWhere.Trim().ToLower(); if (strWhere != \"\") if (strWhere.IndexOf(\"and\") != 0) strWhere = \"and \" + strWhere;\n");
            this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
            this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"select count(1) from [" + strTablename + "] where 1>0 \"+strWhere+\" \");\n");
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("\t\t\tobject obj=DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("\t\t\tint cmdresult;\n");
            this.richTextBox1.AppendText("\t\t\tif((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\tcmdresult=0;\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\telse\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\tcmdresult=Int32.Parse(obj.ToString());\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\tif(cmdresult==0)\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\treturn false;\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\telse\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\treturn true;\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            //if (strView == "U")
            //{
            this.richTextBox1.AppendText("       #region 是否存在该记录 ID memo 0005\n");
            this.richTextBox1.AppendText("       /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 是否存在该记录\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic bool Exists(Int32 ID)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\n");
            this.richTextBox1.AppendText("            StringBuilder strSql = new StringBuilder();\n");
            this.richTextBox1.AppendText("            strSql.Append(\"select count(1) from [" + strTablename + "] where ID=\" + ID + \"\");\n");
            this.richTextBox1.AppendText("            object obj = DbHelperSQL.GetSingle(strSql.ToString());\n");
            this.richTextBox1.AppendText("            Int32 cmdresult;\n");
            this.richTextBox1.AppendText("            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))\n");
            this.richTextBox1.AppendText("            {\n");
            this.richTextBox1.AppendText("                cmdresult = 0;\n");
            this.richTextBox1.AppendText("            }\n");
            this.richTextBox1.AppendText("            else\n");
            this.richTextBox1.AppendText("            {\n");
            this.richTextBox1.AppendText("                cmdresult = Int32.Parse(obj.ToString());\n");
            this.richTextBox1.AppendText("            }\n");
            this.richTextBox1.AppendText("            if (cmdresult == 0)\n");
            this.richTextBox1.AppendText("            {\n");
            this.richTextBox1.AppendText("                return false;\n");
            this.richTextBox1.AppendText("            }\n");
            this.richTextBox1.AppendText("            else\n");
            this.richTextBox1.AppendText("            {\n");
            this.richTextBox1.AppendText("                return true;\n");
            this.richTextBox1.AppendText("            } \n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            //}
            this.richTextBox1.AppendText("       #region 存在该记录条数 memo 0006\n");
            this.richTextBox1.AppendText("       /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 存在该记录条数\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic Int32 ExistsCount(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("           strWhere = strWhere.Trim().ToLower(); if (strWhere != \"\") if (strWhere.IndexOf(\"and\") != 0) strWhere = \"and \" + strWhere;\n");
            this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
            this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"select count(1) from [" + strTablename + "] where 1>0 \"+strWhere+\" \");\n");
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("\t\t\tobject obj=DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("\t\t\tint cmdresult;\n");
            this.richTextBox1.AppendText("\t\t\tif((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\tcmdresult=0;\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\telse\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\tcmdresult=Int32.Parse(obj.ToString());\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\treturn cmdresult;\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("       #region 获得数据列表 自定义SelectList strSelect oderby  group 等等 memo 0007\n");
            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得数据列表 自定义SelectList strSelect oderby  ，group 等等\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic DataSet SelectList(string strSelect, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
            this.richTextBox1.AppendText("\t\t\tstrSql.Append(strSelect);\n");
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("\t\t\treturn DbHelperSQL.Query(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("       #region 获得数据列表GetList1 string strWhere memo 0008\n");
            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得数据列表\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic DataSet GetList(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("            StringBuilder strSql = new StringBuilder();\n");
            this.richTextBox1.AppendText("            strSql.Append(\"select * from [" + strTablename + "] \");\n");
            this.richTextBox1.AppendText("            if (strWhere.Trim() != \"\" && (strWhere.ToLower().Contains(\"=\") || strWhere.ToLower().Contains(\"like\")))\n");
            this.richTextBox1.AppendText("            {\n");
            this.richTextBox1.AppendText("                strSql.Append(\" where \" + strWhere);\n");
            this.richTextBox1.AppendText("            }\n");
            this.richTextBox1.AppendText("            if (strWhere.ToLower().Contains(\"order\") && !strSql.ToString().ToLower().Contains(\"order\"))\n");
            this.richTextBox1.AppendText("                strSql.Append(strWhere);\n");
            if (strView == "U")
            {
                this.richTextBox1.AppendText("            else if (!strSql.ToString().ToLower().Contains(\"order\"))\n");
                this.richTextBox1.AppendText("                strSql.Append(\" order by ID \");\n");
            }
            this.richTextBox1.AppendText("\n");
            this.richTextBox1.AppendText("            SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("            return DbHelperSQL.Query(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("       #region 获得指定数据列表GetList2 string strItem,string strWhere memo 0009\n");
            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 获得指定数据列表\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic DataSet GetList(string strItem,string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
            this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"select \" + strItem + \" from [" + strTablename + "] \");\n");
            this.richTextBox1.AppendText("\t\t\tif(strWhere.Trim()!=\"\")\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\tstrSql.Append(\" where \"+strWhere);\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            if (strView == "U")
            {
                this.richTextBox1.AppendText("           //排除SQL聚合函数\n");
                this.richTextBox1.AppendText("           if (!strSql.ToString().ToLower().Contains(\"order\") && !strSql.ToString().ToLower().Contains(\"group\") && !strSql.ToString().ToLower().Contains(\"count\") && !strSql.ToString().ToLower().Contains(\"sum\") && !strSql.ToString().ToLower().Contains(\"avg\") && !strSql.ToString().ToLower().Contains(\"max\") && !strSql.ToString().ToLower().Contains(\"min\"))\n");
                this.richTextBox1.AppendText("\t\t\t    strSql.Append(\" order by ID \");\n");
            }
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("\t\t\treturn DbHelperSQL.Query(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("       #region IList<string> GetFieldValues  memo 0010\n");
            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("            /// IList<string> GetFieldValues\n");
            this.richTextBox1.AppendText("            /// </summary>   \n");
            this.richTextBox1.AppendText("            public IList<string> GetFieldValues(string fields, string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("            {\n");
            this.richTextBox1.AppendText("                StringBuilder strSql = new StringBuilder();\n");
            this.richTextBox1.AppendText("                strWhere = strWhere.Trim().ToLower(); if (strWhere != \"\") if (strWhere.IndexOf(\"and\") != 0) strWhere = \"and \" + strWhere;\n");
            this.richTextBox1.AppendText("                strSql.Append(\"select \" + fields + \" from [" + strTablename + "] where 1>0 \" + strWhere);\n");
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("                return DbHelperSQL.GetList(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("            }\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("       #region IList<string> GetFieldValues(string topNum, string fields, string strWhere) memo 0011\n");
            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("            /// IList<string> GetFieldValues(string topNum, string fields, string strWhere)\n");
            this.richTextBox1.AppendText("            /// </summary>   \n");
            this.richTextBox1.AppendText("        public IList<string> GetFieldValues(string topNum, string fields, string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("        {\n");
            this.richTextBox1.AppendText("            strWhere = strWhere.Trim().ToLower(); if (strWhere != \"\") if (strWhere.IndexOf(\"and\") != 0) strWhere = \"and \" + strWhere;\n");
            this.richTextBox1.AppendText("            StringBuilder strSql = new StringBuilder();\n");
            this.richTextBox1.AppendText("            strSql.Append(\"select top \" + topNum + \" \" + fields + \" from [" + strTablename + "] where 1>0 \" + strWhere);\n");
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("            return DbHelperSQL.GetList(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("        }\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            this.richTextBox1.AppendText("       #region object Scalar(string field, string strWhere) memo 0012\n");
            this.richTextBox1.AppendText("            /// <summary>\n");
            this.richTextBox1.AppendText("            ///ExecuteScalar方法返回的类型是object类型，这个方法返回sql语句执行后的第一行第一列的值，由于不知到sql语句到底是什么样的结构（有可能是Int32，有可能是char等等），所以ExecuteScalar方法返回一个最基本的类型object，这个类型是所有类型的基类，换句话说：可以转换为任意类型。\n");
            this.richTextBox1.AppendText("            /// object Scalar(string field, string strWhere)\n");
            this.richTextBox1.AppendText("            /// </summary>   \n");
            this.richTextBox1.AppendText("        public object Scalar(string field, string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("        {\n");
            this.richTextBox1.AppendText("            strWhere = strWhere.Trim().ToLower(); if (strWhere != \"\") if (strWhere.IndexOf(\"and\") != 0) strWhere = \"and \" + strWhere;\n");
            this.richTextBox1.AppendText("            StringBuilder strSql = new StringBuilder();\n");
            this.richTextBox1.AppendText("            strSql.Append(\"select \" + field + \" from [" + strTablename + "] where 1>0 \" + strWhere);\n");
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("            return DbHelperSQL.GetSingle(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("        }\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("       #region 得到一个对象实体 strWhere memo 0013\n");
            this.richTextBox1.AppendText("           /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 得到一个对象实体  strWhere\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic EggsoftWX.Model." + strTablename + " GetModel(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
            this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"select * from [" + strTablename + "] \");\n");
            this.richTextBox1.AppendText("\t\t\tstrSql.Append(\" where \"+strWhere);\n");
            this.richTextBox1.AppendText("\t\t\tEggsoftWX.Model." + strTablename + " model=new EggsoftWX.Model." + strTablename + "();\n");
            this.richTextBox1.AppendText("\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("\t\t\tDataSet ds=DbHelperSQL.Query(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("\t\t\tif(ds.Tables[0].Rows.Count>0)\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            for (i = 0; i < (this.dataGridView1.RowCount - 1); i++)
            {
                strItemName = this.dataGridView1.Rows[i].Cells["COLUMN_NAME"].Value.ToString();
                strItemType = this.dataGridView1.Rows[i].Cells["TYPE_NAME"].Value.ToString();
                if (this.GetType(strItemType.Trim().ToLower()).ToLower() == "int32")
                {
                    this.richTextBox1.AppendText("\t\t\t\tif(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString()!=\"\")\n");
                    this.richTextBox1.AppendText("\t\t\t\t{\n");
                    this.richTextBox1.AppendText("\t\t\t\t\tmodel." + strItemName + "=Int32.Parse(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString());\n");
                    this.richTextBox1.AppendText("\t\t\t\t}\n");
                }
                else if (this.GetType(strItemType.Trim().ToLower()).ToLower() == "int")
                {
                    this.richTextBox1.AppendText("\t\t\t\tif(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString()!=\"\")\n");
                    this.richTextBox1.AppendText("\t\t\t\t{\n");
                    this.richTextBox1.AppendText("\t\t\t\t\tmodel." + strItemName + "=int.Parse(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString());\n");
                    this.richTextBox1.AppendText("\t\t\t\t}\n");
                }
                else if (this.GetType(strItemType.Trim().ToLower()).ToLower() == "string")
                {
                    this.richTextBox1.AppendText("\t\t\t\tmodel." + strItemName + "=ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString();\n");
                }
                else if (this.GetType(strItemType.Trim().ToLower()).ToLower() == "datetime")
                {
                    this.richTextBox1.AppendText("\t\t\t\tif(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString()!=\"\")\n");
                    this.richTextBox1.AppendText("\t\t\t\t{\n");
                    this.richTextBox1.AppendText("\t\t\t\t\tmodel." + strItemName + "=DateTime.Parse(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString());\n");
                    this.richTextBox1.AppendText("\t\t\t\t}\n");
                }
                else if (this.GetType(strItemType.Trim().ToLower()).ToLower() == "bool")
                {
                    this.richTextBox1.AppendText("\t\t\t\tif(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString()!=\"\")\n");
                    this.richTextBox1.AppendText("\t\t\t\t{\n");
                    this.richTextBox1.AppendText("\t\t\t\t\tmodel." + strItemName + "=Convert.ToBoolean(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString());\n");
                    this.richTextBox1.AppendText("\t\t\t\t}\n");
                }
                else if (((this.GetType(strItemType.Trim().ToLower()).ToLower() == "money") || (this.GetType(strItemType.Trim().ToLower()).ToLower() == "numeric")) || (this.GetType(strItemType.Trim().ToLower()).ToLower() == "decimal"))
                {
                    this.richTextBox1.AppendText("\t\t\t\tif(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString()!=\"\")\n");
                    this.richTextBox1.AppendText("\t\t\t\t{\n");
                    this.richTextBox1.AppendText("\t\t\t\t\tmodel." + strItemName + "=decimal.Parse(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString());\n");
                    this.richTextBox1.AppendText("\t\t\t\t}\n");
                }
                else if ((this.GetType(strItemType.Trim().ToLower()).ToLower() == "float"))
                {
                    this.richTextBox1.AppendText("\t\t\t\tif(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString()!=\"\")\n");
                    this.richTextBox1.AppendText("\t\t\t\t{\n");
                    this.richTextBox1.AppendText("\t\t\t\t\tmodel." + strItemName + "=float.Parse(ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString());\n");
                    this.richTextBox1.AppendText("\t\t\t\t}\n");
                }
                else
                {
                    this.richTextBox1.AppendText("\t\t\t\tmodel." + strItemName + "=ds.Tables[0].Rows[0][\"" + strItemName + "\"].ToString();\n");
                }
            }
            this.richTextBox1.AppendText("\t\t\t\treturn model;\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t\telse\n");
            this.richTextBox1.AppendText("\t\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn null;\n");
            this.richTextBox1.AppendText("\t\t\t}\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            //if (strView == "U")
            //{
            this.richTextBox1.AppendText("       #region 得到一个对象实体 ID  memo 0014\n");
            this.richTextBox1.AppendText("           /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 得到一个对象实体\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic EggsoftWX.Model." + strTablename + " GetModel(Int32 ID)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\treturn GetModel(\"ID=\"+ID+\"\");\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            //}
            this.richTextBox1.AppendText("       #region delete strWhere 删除n条数据 memo 0015\n");
            this.richTextBox1.AppendText("       /// <summary>\n");
            this.richTextBox1.AppendText("\t\t/// 删除n条数据\n");
            this.richTextBox1.AppendText("\t\t/// </summary>\n");
            this.richTextBox1.AppendText("\t\tpublic int Delete(string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("\t\t{\n");
            this.richTextBox1.AppendText("\t\t\t\tStringBuilder strSql=new StringBuilder();\n");
            this.richTextBox1.AppendText("\t\t\t\tstrSql.Append(\"delete from [" + strTablename + "] \");\n");
            this.richTextBox1.AppendText("\t\t\t\tstrSql.Append(\" where \"+strWhere);\n");
            this.richTextBox1.AppendText("\t\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("\t\t\t\treturn DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("\t\t}\n");
            this.richTextBox1.AppendText("       #endregion\n");
            this.richTextBox1.AppendText("            \n");
            if (strView == "U")
            {
                this.richTextBox1.AppendText("       #region delete ID 删除一条数据  memo 0016\n");
                this.richTextBox1.AppendText("       /// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 删除一条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic int Delete(Int32 ID)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\t\tStringBuilder strSql=new StringBuilder();\n");
                this.richTextBox1.AppendText("\t\t\t\tstrSql.Append(\"delete from [" + strTablename + "] \");\n");
                this.richTextBox1.AppendText("\t\t\t\tstrSql.Append(\" where ID=\"+ID);\n");
                this.richTextBox1.AppendText("\t\t\t\treturn DbHelperSQL.ExecuteSql(strSql.ToString());\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("       #endregion\n");
                this.richTextBox1.AppendText("            \n");
            }
            if (strView == "U")
            {
                this.richTextBox1.AppendText("       #region update strSet strWhere 更新n条数据 更新n条数据 memo 0017\n");
                this.richTextBox1.AppendText(" \t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 更新n条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic int Update(string strSet,string strWhere, params object[] objs)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"update [" + strTablename + "] set \");\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(strSet);\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append( \" where \" );\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(strWhere);\n");
                this.richTextBox1.AppendText("\t\t\t\tSqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
                this.richTextBox1.AppendText("\t\t\treturn DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToList);\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("       #endregion\n");
                this.richTextBox1.AppendText("            \n");
            }

            if (strView == "U")
            {
                this.richTextBox1.AppendText("       #region model update 更新一条数据 memo 0018\n");
                this.richTextBox1.AppendText("\t\t/// <summary>\n");
                this.richTextBox1.AppendText("\t\t/// 更新一条数据\n");
                this.richTextBox1.AppendText("\t\t/// </summary>\n");
                this.richTextBox1.AppendText("\t\tpublic int Update(EggsoftWX.Model." + strTablename + " model)\n");
                this.richTextBox1.AppendText("\t\t{\n");
                this.richTextBox1.AppendText("\t\t\tStringBuilder strSql=new StringBuilder();\n");
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(\"update [" + strTablename + "] set \");\n");
                string strstrSet = "";
                for (i = 0; i < (this.dataGridView1.RowCount - 1); i++)
                {
                    strItemName = this.dataGridView1.Rows[i].Cells["COLUMN_NAME"].Value.ToString();
                    strItemType = this.dataGridView1.Rows[i].Cells["TYPE_NAME"].Value.ToString();
                    if (strItemType == "datetime")
                    {
                        strstrSet += "\t\t\tif ((model." + strItemName + " != null)&&(model." + strItemName + " != DateTime.MinValue)) strSql.Append(\"[" + strItemName + "]=@" + strItemName + ",\");\n";
                    }
                    else if (this.GetType(strItemType.Trim().ToLower()).ToLower() == "string")
                    {
                        strstrSet += "\t\t\tif (String.IsNullOrEmpty(model." + strItemName + ") == false) strSql.Append(\"[" + strItemName + "]=@" + strItemName + ",\");\n";
                    }
                    else
                    {
                        strstrSet += "\t\t\tif (model." + strItemName + " != null) strSql.Append(\"[" + strItemName + "]=@" + strItemName + ",\");\n";
                    }
                }
                strstrSet += "\t\t\tstrSql.Remove(strSql.Length - 1, 1);///移除最后一个逗号\n";
                this.richTextBox1.AppendText(strstrSet);
                this.richTextBox1.AppendText("\t\t\t\n");
                this.richTextBox1.AppendText("\t\t\tList<SqlParameter> ParameterToArrayList = new List<SqlParameter>();\n");


                for (i = 0; i < (this.dataGridView1.RowCount - 1); i++)
                {
                    strItemName = this.dataGridView1.Rows[i].Cells["COLUMN_NAME"].Value.ToString();
                    strItemType = this.dataGridView1.Rows[i].Cells["TYPE_NAME"].Value.ToString();
                    strNULLABLE = this.dataGridView1.Rows[i].Cells["NULLABLE"].Value.ToString();
                    string strAdd = "model." + strItemName;
                    if (strItemType == "datetime")
                    {
                        this.richTextBox1.AppendText("\t\t\tif ((model." + strItemName + " != null)&&(model." + strItemName + " != DateTime.MinValue)) ParameterToArrayList.Add(new SqlParameter(\"@" + strItemName + "\"," + strAdd + "));\n");
                    }
                    else if (this.GetType(strItemType.Trim().ToLower()).ToLower() == "string")
                    {
                        this.richTextBox1.AppendText("\t\t\tif (String.IsNullOrEmpty(model." + strItemName + ") == false) ParameterToArrayList.Add(new SqlParameter(\"@" + strItemName + "\"," + strAdd + "));\n");
                    }
                    else
                    {
                        this.richTextBox1.AppendText("\t\t\tif (model." + strItemName + " != null) ParameterToArrayList.Add(new SqlParameter(\"@" + strItemName + "\"," + strAdd + "));\n");
                    }
                }
                this.richTextBox1.AppendText("\t\t\tstrSql.Append(\" where " + "ID='\"+model." + "ID+\"'\");\n");
                this.richTextBox1.AppendText("\t\t\treturn DbHelperSQL.ExecuteSql(strSql.ToString(),ParameterToArrayList.ToArray());\n");
                this.richTextBox1.AppendText("\t\t}\n");
                this.richTextBox1.AppendText("       #endregion\n");
                this.richTextBox1.AppendText("            \n");


            }


            this.richTextBox1.AppendText("       #region  GetList(string topNum, string fields, string strWhere memo 0019\n");
            this.richTextBox1.AppendText("        /// <summary>   //memo 0019\n");
            this.richTextBox1.AppendText("        /// \n");
            this.richTextBox1.AppendText("        /// </summary>\n");
            this.richTextBox1.AppendText("       /// <param name=/strWhere/></param>\n");
            this.richTextBox1.AppendText("        /// <returns></returns>\n");
            this.richTextBox1.AppendText("        public DataTable GetList(string topNum, string fields, string strWhere, params object[] objs)\n");
            this.richTextBox1.AppendText("       {\n");
            this.richTextBox1.AppendText("           strWhere = strWhere.ToLower();\n");
            this.richTextBox1.AppendText("           if ((strWhere.IndexOf(\"and\") == -1) && (strWhere!=\"\")) strWhere = \"and \" + strWhere;\n");
            this.richTextBox1.AppendText("           StringBuilder strSql = new StringBuilder();\n");
            this.richTextBox1.AppendText("           strSql.Append(\"select top \" + topNum.ToString() +\" \"+ fields);\n");
            this.richTextBox1.AppendText("           strSql.Append(\" FROM   [" + strTablename + "]  where 1 > 0 \" + strWhere);\n");
            this.richTextBox1.AppendText("           SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(strSql.ToString(), objs);\n");
            this.richTextBox1.AppendText("           return DbHelperSQL.GetDataTable(strSql.ToString(),ParameterToList);\n");
            this.richTextBox1.AppendText("       }\n");
            this.richTextBox1.AppendText("\n");
            this.richTextBox1.AppendText("         #endregion  成员方法\n");

            this.richTextBox1.AppendText("       #region 大数据量快速分页,50万以上数据分页 memo 0020\n");
            this.richTextBox1.AppendText("         //大数据量快速分页,50万以上数据分页  memo 0020\n");
            this.richTextBox1.AppendText("        public DataTable GetPageDataTable(Int32 pageIndex, Int32 pageSize, string fields, string strConditions, string orderField, bool isDesc, params object[] objs)\n");
            this.richTextBox1.AppendText("        {\n");
            this.richTextBox1.AppendText("           strConditions = strConditions.ToLower();\n");
            this.richTextBox1.AppendText("           if ((strConditions.IndexOf(\"and\") == -1) && (strConditions!=\"\")) strConditions = \"and \" + strConditions;\n");
            this.richTextBox1.AppendText("           SqlParameter[] ParameterToList = EggsoftWX.SQLServerDAL.Util.ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction(fields+\" \"+strConditions+\" \"+orderField, objs);\n");
            this.richTextBox1.AppendText("            return DbHelperSQL.GetPageDataTable(pageIndex, pageSize, fields, \"[" + strTablename + "]\", strConditions, orderField, isDesc,ParameterToList);\n");
            this.richTextBox1.AppendText("        }\n");
            this.richTextBox1.AppendText("         #endregion  大数据量快速分页,50万以上数据分页 memo 0020\n");
            this.richTextBox1.AppendText("     #endregion  成员方法\n");
            this.richTextBox1.AppendText("     }\n");
            this.richTextBox1.AppendText("}\n");
            FileIOMy.writeFile(str02ThreeLevel + @"EggsoftWX.SQLServerDAL\" + strTablename + ".cs", this.richTextBox1.Text);

        }


        /// 
        /// 
        /// <param name="strTablename"></param>
        /// <param name="strView"></param>


        private void writeSQLServerDAL_DbHelperSQL()
        {
            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。 
            string str = System.Environment.CurrentDirectory;
            string strMother = str + @"\DbHelperSQL.cs";
            richTextBox1.Text = FileIOMy.readFile(strMother);
            FileIOMy.writeFile(str02ThreeLevel + @"EggsoftWX.SQLServerDAL\DbHelperSQL.cs", richTextBox1.Text);

        }
    }
}
