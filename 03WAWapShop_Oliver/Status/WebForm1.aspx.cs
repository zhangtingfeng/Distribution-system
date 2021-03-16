using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft.Common;

namespace _03WAWapShop_Oliver.Status
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Eggsoft.Common.debug_Log.Call_WriteLog("/Status/WebForm1.aspx", "Application_End");

            //#region 每天运营中心加权分红
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红开始执行4", "每天更新");
            //Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay mmPub_Default_DoYunYingZhongXin28EveryDay = new Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay(1);
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红执行完毕4", "每天更新");
            //#endregion 每天运营中心加权分红

            //#region 结算用户加权总额   下一步分权使用
            //EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay my_BLL_b007_OperationReturnMoneyEveryDay = new EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay();
            //EggsoftWX.Model.b007_OperationReturnMoneyEveryDay my_Model_b007_OperationReturnMoneyEveryDay = new EggsoftWX.Model.b007_OperationReturnMoneyEveryDay();
            //my_Model_b007_OperationReturnMoneyEveryDay.ShopClient_ID = 1;
            //my_Model_b007_OperationReturnMoneyEveryDay.ThisDay = DateTime.Now.ToString("yyyy-MM-dd");
            //my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyAuto = 0;
            //my_BLL_b007_OperationReturnMoneyEveryDay.Add(my_Model_b007_OperationReturnMoneyEveryDay);
            //#endregion 结算用户加权总额 
            //Decimal mySonemoney = 0;
            //Eggsoft_Public_CL.Pub_FenXiao.GetCountMySonMoney(34339, out mySonemoney);
            //return;

            //#region 找出一个人的所有上线

            //Eggsoft_Public_CL.Pub_Default_Status_User_AllLeader mmPub_Default_Status_User_AllLeader = new Eggsoft_Public_CL.Pub_Default_Status_User_AllLeader();
            //#endregion
        }
    }
}