using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._12UserAskMoney
{
    /// <summary>
    /// 后台处理 申请提现
    /// </summary>
    public partial class IS_UserFinance_check_DrawMoney_Asnc : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                String strID = Request.QueryString["ID"];
                String strIS_Admin_check = Request.QueryString["IS_Admin_check"];


                EggsoftWX.BLL.tab_User_AskGetMoney BLL_tab_User_AskGetMoney = new EggsoftWX.BLL.tab_User_AskGetMoney();
                EggsoftWX.Model.tab_User_AskGetMoney Model_tab_User_AskGetMoney = BLL_tab_User_AskGetMoney.GetModel(Int32.Parse(strID));
                BLL_tab_User_AskGetMoney.Update("IFSendMoney=" + strIS_Admin_check + ",UpdateTime=@UpdateTime,ResultCode=@ResultCode", "ID=@ID", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "线下操作成功", strID);
                //BLL_tab_User_AskGetMoney.Update("IFSendMoney=" + strIS_Admin_check, "ID=" + strID);

                if (strIS_Admin_check == "1")
                {
                    if (Decimal.Round(Model_tab_User_AskGetMoney.AskMoney.toDecimal(), 2) > 0)
                    {
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_tab_User_AskGetMoney.AskMoney.toDecimal();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "官方现金转账";
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 130;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User_AskGetMoney.UserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User_AskGetMoney.UserID.toString());
                        BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                    }
                }
                else if (strIS_Admin_check == "0")
                {
                    if (Decimal.Round(Model_tab_User_AskGetMoney.AskMoney.toDecimal(), 2) > 0)
                    {
                        ////取消该功能的使用

                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_tab_User_AskGetMoney.AskMoney.toDecimal();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "官方取消现金转账";
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 72;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User_AskGetMoney.UserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User_AskGetMoney.UserID.toString());
                        Eggsoft.Common.debug_Log.Call_WriteLog(Model_tab_TotalCredits_Consume_Or_Recharge.toJsonString(), "官方取消现金转账", "需要查看日志");

                        //BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                    }
                }
                Response.Write(strIS_Admin_check);
            }
        }
    }
}