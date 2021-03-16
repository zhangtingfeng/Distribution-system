using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _01WA_WebDestop._01LogisticalPrice
{
    public partial class _02QueryList : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected string strShow = "ddd";
        protected void Page_Load(object sender, EventArgs e)
        {
            //          @CNCountryName = N'乌拉圭',
            //@PakType = N'文件',
            //@InputKgs = 2.7
            //Eggsoft.Common.Session.Add("Country", TextBox_DesCountry.Text);
            //Eggsoft.Common.Session.Add("kgs", TextBox1kgs.Text);
            //Eggsoft.Common.Session.Add("type", DropDownListtype.Text);
            string Country = Eggsoft.Common.Session.Read("Country");
            string kgs = Eggsoft.Common.Session.Read("kgs");
            string type = Eggsoft.Common.Session.Read("type");

            Literal2SendInfo.Text = "发往：" + Country + "，" + type + "，重量:" + kgs + "公斤";


            System.Data.IDataParameter[] iData = new System.Data.SqlClient.SqlParameter[3];
            iData[0] = new System.Data.SqlClient.SqlParameter("@CNCountryName", Country);
            iData[1] = new System.Data.SqlClient.SqlParameter("@PakType", type);
            iData[2] = new System.Data.SqlClient.SqlParameter("@InputKgs", kgs);
            System.Data.DataSet strReturn = EggsoftWX.SQLServerDAL.DbHelperSQL.RunProcedure("sp_34WuLiu", iData, "Price");
            System.Data.DataTable myDataTable = strReturn.Tables[0];
            int dddCount = myDataTable.Rows.Count;

            string strLine = "";

            for (int i = 0; i < dddCount; i++)
            {
                strLine += "<tr align=\"center\" bgcolor=\"#F5F9FA\">";
                strLine += "                                 <td align=\"center\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\" id=\"oEmsKd_0\">" + myDataTable.Rows[i]["Channel"] + "</td>";
                strLine += "                                 <td align=\"right\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\" id=\"osPrc_0\">" + myDataTable.Rows[i]["Price"] + "</td>";
                strLine += "                                 <td align=\"center\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\"></td>";
                strLine += "                                 <td align=\"center\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\"></td>";
                strLine += "                                 <td align=\"right\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\" id=\"oPrc_0\">" + myDataTable.Rows[i]["Price"] + "</td>";
                strLine += "                                 <td align=\"center\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\"></td>";
                strLine += "                                 <td align=\"center\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\">√" + myDataTable.Rows[i]["kgs"] + " </td>";
                strLine += "                                 <td align=\"left\" bgcolor=\"#F5F9FA\" class=\"priceListOdd\">" + myDataTable.Rows[i]["ChannelMemo"] + " </td>";
                strLine += "                             </tr>";
            }
            Literal1.Text = strLine;
            //--object strReturnTable = EggsoftWX.SQLServerDAL.DbHelperSQL.RunProcedure("sp_34WuLiu", iData);
            // --- System.Data.DataTable myDataSet = (System.Data.DataTable)strReturnTable;


        }
    }
}