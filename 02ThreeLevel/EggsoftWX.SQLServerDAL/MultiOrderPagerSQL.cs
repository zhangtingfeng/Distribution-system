//生成多字段排序分页的SQL的通用类


//如果的单一字段排序分页，现在有很多的存储过程和SQL语句，分页的时候，只取pageSize的记录，可遇见的问题是： 
//这个单一字段必须是唯一的 
//这个字段必须是可以被排序的 
//不支持多字段排序 
//针对这一问题，我用C#做了一个类，解决以上的对多字段排序分页和每次都取pageSize条记录的问题 先看看代码： 
//  复制代码 代码如下:

using System;
using System.Collections.Specialized;
namespace web
{
    /// <summary>  
    /// MultiOrderPagerSQL 的摘要说明  
    /// </summary>  
    public class MultiOrderPagerSQL
    {
        private NameValueCollection orders = new NameValueCollection();
        private string table_;
        private string where_ = "";//1=1 and 2=2 的格式  
        private string outfields_;
        private Int32 nowPageIndex_ = 0;
        private Int32 pagesize_ = 0;
        private string sql_;//要返回的SQL  
        public MultiOrderPagerSQL()
        {
        }
        /****************方法*******************/
        public void addOrderField(string field, string direction)
        {
            orders.Add(field, direction);
        }
        public string getSQL(Int32 SQLRecordCount = 0)
        {//计算最后一页

            Int32 intThisPageSize = pagesize_;

            if (nowPageIndex_ * pagesize_ > SQLRecordCount)
            {
                intThisPageSize = SQLRecordCount - (nowPageIndex_ - 1) * pagesize_;
                if (intThisPageSize < 0) { intThisPageSize = 0; }
            }
            //排序字段  
            string orderList = "";//用户期望的排序  
            string orderList2 = "";//对用户期望的排序的反排序  
            string orderList3 = "";//用户期望的排序,去掉了前缀.复合查询里的外层的排序不能是类似这样的table1.id,要去掉table1.。  
            if (orders.Count > 0)
            {
                string[] str = orders.AllKeys;
                foreach (string s in str)
                {
                    string direction = "asc";//默认一个方向  
                    if (orders[s].ToString() == "asc")
                        direction = "desc";
                    //去掉前缀的字段名称  
                    string s2 = "";
                    Int32 index = s.IndexOf(".") + 1;
                    s2 = s.Substring(index);
                    orderList = orderList + s + " " + orders[s] + ",";
                    orderList2 = orderList2 + s2 + " " + direction + ",";
                    orderList3 = orderList3 + s2 + " " + orders[s] + ",";
                }
                //去掉最后的,号  
                orderList = orderList.Substring(0, orderList.Length - 1);
                orderList2 = orderList2.Substring(0, orderList2.Length - 1);
                orderList3 = orderList3.Substring(0, orderList3.Length - 1);
            }
            //return orderList2;  
            //形成SQL   
            string strTemp;
            strTemp = "select * from \n ( select top {7} * from ( select top {6} {0} from {1} \n";
            if (where_ != "")
                strTemp = strTemp + " where {2} \n";
            if (orderList != "")
                strTemp = strTemp + " order by {3} ) as tmp order by {4} \n ) \n as tmp2 \n order by {5} \n";
            strTemp = string.Format(strTemp, outfields_, table_, where_, orderList, orderList2, orderList3, nowPageIndex_ * pagesize_, intThisPageSize);
            return strTemp;
        }
        /// <summary>
        /// 统计使用的代码
        /// </summary>
        /// <param name="SQLRecordCount"></param>
        /// <returns></returns>
        public string getSQLTop100percent(Int32 SQLRecordCount = 0)
        {//计算最后一页

            Int32 intThisPageSize = pagesize_;

            if (nowPageIndex_ * pagesize_ > SQLRecordCount)
            {
                intThisPageSize = SQLRecordCount - (nowPageIndex_ - 1) * pagesize_;
                if (intThisPageSize < 0) { intThisPageSize = 0; }
            }
            //排序字段  
            string orderList = "";//用户期望的排序  
            string orderList2 = "";//对用户期望的排序的反排序  
            string orderList3 = "";//用户期望的排序,去掉了前缀.复合查询里的外层的排序不能是类似这样的table1.id,要去掉table1.。  
            if (orders.Count > 0)
            {
                string[] str = orders.AllKeys;
                foreach (string s in str)
                {
                    string direction = "asc";//默认一个方向  
                    if (orders[s].ToString() == "asc")
                        direction = "desc";
                    //去掉前缀的字段名称  
                    string s2 = "";
                    Int32 index = s.IndexOf(".") + 1;
                    s2 = s.Substring(index);
                    orderList = orderList + s + " " + orders[s] + ",";
                    orderList2 = orderList2 + s2 + " " + direction + ",";
                    orderList3 = orderList3 + s2 + " " + orders[s] + ",";
                }
                //去掉最后的,号  
                orderList = orderList.Substring(0, orderList.Length - 1);
                orderList2 = orderList2.Substring(0, orderList2.Length - 1);
                orderList3 = orderList3.Substring(0, orderList3.Length - 1);
            }
            //return orderList2;  
            //形成SQL   
            string strTemp;
            strTemp = "select top 100 percent * from \n ( select top {7} * from ( select top {6} {0} from {1} \n";
            if (where_ != "")
                strTemp = strTemp + " where {2} \n";
            if (orderList != "")
                strTemp = strTemp + " order by {3} ) as tmp order by {4} \n ) \n as tmp2 \n order by {5} \n";
            strTemp = string.Format(strTemp, outfields_, table_, where_, orderList, orderList2, orderList3, nowPageIndex_ * pagesize_, intThisPageSize);
            return strTemp;
        }




        /****************属性*******************/
        public string table
        {
            set { table_ = value; }
        }
        public string where
        {
            set { where_ = value; }
        }
        public string outfields
        {
            set { outfields_ = value; }
        }
        public Int32 nowPageIndex
        {
            set { nowPageIndex_ = value; }
        }
        public Int32 pagesize
        {
            set { pagesize_ = value; }
        }
    }
}


//说一下原理先：其实很简单，由于AC和MS SQL 2000 没有象MS SQL 2005的row_number函数，我们就不能从这里下手了，比如你取第二页，那就是序号从10-20，我们先按照某一排序规则 把 前 20条的数据取出来，然后再按照先前的排序规则的反规则把这个数据反排序，再取前10条，那么这个时候就是要取的数据了，这个时候还没有结束，再把结果按照先前的排序规则排序即可。我觉得效率瓶颈会出现在排序上。看看是怎么来使用的： 




//  复制代码 代码如下:

//  using System;  
//using System.Data;  
//using System.Configuration;  
//using System.Collections;  
//using System.Web;  
//using System.Web.Security;  
//using System.Web.UI;  
//using System.Web.UI.WebControls;  
//using System.Web.UI.WebControls.WebParts;  
//using System.Web.UI.HtmlControls;  
//public partial class MultiOrderPagerSQLTest : System.Web.UI.Page  
//{  
//    protected void Page_Load(object sender, EventArgs e)  
//    {  
//        web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();  
//        //sql.addOrderField("t1.id", "desc");//第一排序字段  
//        sql.addOrderField("t1.hits", "desc");//第二排序字段  
//        sql.table = "joke t1,type t2";  
//        sql.outfields = "t1.*,t2.type";  
//        sql.nowPageIndex = 5;  
//        sql.pagesize = 10;  
//        sql.where = "t1.typeid=t2.typeid";  
//        Response.Write(sql.getSQL());  
//    }  
//} 


//以上在AC和MS SQL 2000（5）上测试通过。 

//暂时做出这样一个类，没有做成存储过程，要做的话，还有一点难度呢 ，呵呵。