using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HM6NR - QXX7C - DFW2Y - 8B82K - WTYJV
            //string strConn = "Data Source=219.235.0.112,3699;Initial Catalog=Shop.Earh17.com;Persist Security Info=True;User ID=ShopEarh17;Password=ShopEarh17com2wsx;Enlist=true;Pooling=true;Max Pool Size = 300; Min Pool Size=0; Connection Lifetime = 300;packet size=1000;";//SQL Server链接字符串  
            //string strSql = "SELECT top 1 * FROM tab_User order by id desc";

            //SqlDataAdapter da = new SqlDataAdapter(strSql, strConn);
            //DataSet ds = new DataSet();//创建DataSet实例  
            //da.Fill(ds, "自定义虚拟表名");//使用DataAdapter的Fill方法(填充)，调用SELECT命令  
            //DataTable ta = ds.Tables[0];//TA是你的表。
            //DataColumn col = ta.Columns["ID"];
            
            //da.SelectCommand.CommandTimeout
            //  da.DeleteCommand.CommandTimeout
            //int intuserID=

            //string strURL = "http://testservice.eggsoft.cn/User/WS_Agent_ChoiceGoods.asmx/_Service_Agent_Save?UserID=8568&ParentID=8&ShopClientID=1&ShopName=%25E5%25BC%25A0%25E5%25BB%25B7%25E9%2594%258B%25E5%2588%2586%25E9%2594%2580%25E5%2588%2586%25E7%25BA%25A2%25E8%25BF%2594%25E5%2588%25A9%25E5%258A%25A0%25E7%25B2%2589%25E7%25B3%25BB%25E5%2588%2597%25E8%25BD%25AF%25E4%25BB%25B6&ContactName=%25E5%25BC%25A0%25E5%25BB%25B7%25E9%2594%258B&ContactMobile=18917905147&AlipayOrWeiXinPay=%25E5%2581%25A5%25E5%2581%25A5%25E5%25BA%25B7%25E5%25BA%25B7&ChoiceGoodList=4,6,58,841,1517,1735,1753,2,3,1&AgentAdLevel=5";
            //string strJson = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strURL);
            //Eggsoft_Public_CL.Pub_Agent.add_Agent_Default_OnlyOneKey(Int32.Parse(strUserID));

        }
    }
}