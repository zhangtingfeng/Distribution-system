using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
///Pub 的摘要说明
/// </summary>
public class Marker_tab_System_WeiXin
{
    public Marker_tab_System_WeiXin()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    //设置一个较老的时间。  为AccessToken
    public static string ReadTextNeedTime(string strTokenOrTicket, int intShopClientID, out DateTime argDatetime)
    {
        EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
        EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
        Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);
        string strReturn = "";
        argDatetime = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, 1); ; //设置一个较老的时间。  当然 数据库里面 可能有新的 ，要是没有记录 这个就起作用了  可以返回了
       if (Model_tab_ShopClient_EngineerMode != null)
        {

            //System.Data.DataTable myDataTable = bll.GetList("MarkerContent,UpdateTime", strWhere).Tables[0];
            if (strTokenOrTicket.ToLower() == "token")
            {
                strReturn = Model_tab_ShopClient_EngineerMode.ACCESS_TOKEN_WeiXin;
                argDatetime = Model_tab_ShopClient_EngineerMode.Get_ACCESS_TOKEN_Time_WeiXin;
            }
            else if (strTokenOrTicket.ToLower() == "ticket")
            {
                strReturn = Model_tab_ShopClient_EngineerMode.GetTicket_WeiXin;
                argDatetime = Model_tab_ShopClient_EngineerMode.Get_Ticket_Time_WeiXin;
            }
        }
        return strReturn;
    }

   
}




//public class MyClass
//{
//    public static void RunSnippet()
//    {
//        List<item> list = new List<item>();
//        list.Add(new item("c", 3));
//        list.Add(new item("b", 2));
//        list.Add(new item("d", 4));
//        list.Add(new item("a", 1));
//        list.Sort(BigToSmall);
//        foreach (item i in list)
//        {
//            Console.WriteLine(i.name);
//        }
//    }
//    private static int BigToSmall(item worker1, item worker2)
//    {
//        return worker2.count - worker1.count;
//    }
//    private static int SmallToBig(item worker1, item worker2)
//    {
//        return worker2.count - worker1.count;
//    }
//}

//public class item
//{
//    public string name;
//    public int count;
//    public item(string n, int c)
//    {
//        name = n;
//        count = c;
//    }
//}