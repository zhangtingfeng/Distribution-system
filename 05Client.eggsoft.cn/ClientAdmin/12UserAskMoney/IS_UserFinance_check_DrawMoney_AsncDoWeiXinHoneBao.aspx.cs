using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._12UserAskMoney
{
    public partial class IS_UserFinance_check_DrawMoney_AsncDoWeiXinHoneBao : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private static object myIS_UserFinance_check_DrawMoney_AsncDoWeiXinHoneBao = new Object();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lock (myIS_UserFinance_check_DrawMoney_AsncDoWeiXinHoneBao)
                {
                    String strID = Request.QueryString["ID"];
                    String strIS_Admin_check = Request.QueryString["IS_Admin_check"];
                    String strNeedMoney = Request.QueryString["NeedMoney"];


                    Decimal Decimal_NeedMoney = 0;
                    Decimal.TryParse(strNeedMoney, out Decimal_NeedMoney);

                    EggsoftWX.BLL.tab_User_AskGetMoney BLL_tab_User_AskGetMoney = new EggsoftWX.BLL.tab_User_AskGetMoney();
                    EggsoftWX.Model.tab_User_AskGetMoney Model_tab_User_AskGetMoney = BLL_tab_User_AskGetMoney.GetModel(Int32.Parse(strID));
                    Eggsoft.Common.debug_Log.Call_WriteLog("strID=" + strID + " strIS_Admin_check=" + strIS_Admin_check + " strNeedMoney=" + strNeedMoney, "微信现金");

                    Decimal myCountyuEArgMoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Model_tab_User_AskGetMoney.UserID.toInt32(), out myCountyuEArgMoney);
                    if (myCountyuEArgMoney >= Decimal_NeedMoney)//余额是否充足
                    {
                        string strTransNo = DateTime.Now.ToString("yyyyMMdd") + Eggsoft.Common.StringNum.Add000000Num(strID.toInt32(), 8);
                        Eggsoft_Public_CL.WXRed myWXRed = new Eggsoft_Public_CL.WXRed();

                        string strresult_code = ""; string strpayment_no = ""; string strerr_code_des = "";
                        myWXRed.sendMoney(Model_tab_User_AskGetMoney.UserID.toInt32(), strTransNo, Decimal_NeedMoney, out strresult_code, out strpayment_no, out strerr_code_des);

                        if (strresult_code == "success" && String.IsNullOrEmpty(strpayment_no) == false)
                        {
                            BLL_tab_User_AskGetMoney.Update("IFSendMoney=1,payment_no=@payment_no,UpdateTime=@UpdateTime,ResultCode=@ResultCode", "ID=@ID", strpayment_no, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"成功", strID);
                            if (strIS_Admin_check == "1")
                            {
                                if (Decimal.Round(Model_tab_User_AskGetMoney.AskMoney.toDecimal(), 2) > 0)
                                {
                                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 130;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_tab_User_AskGetMoney.AskMoney.toDecimal();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "官方微信现金转账" + strpayment_no;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User_AskGetMoney.UserID.toInt32();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User_AskGetMoney.UserID.toString());
                                    BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                                }
                            }
                        }
                        else
                        {
                            BLL_tab_User_AskGetMoney.Update("IFSendMoney=0,payment_no=@payment_no,UpdateTime=@UpdateTime,ResultCode=@ResultCode", "ID=@ID", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strerr_code_des, strID);
                            strIS_Admin_check = strerr_code_des;//转账失败
                        }
                    }
                    Response.Clear();
                    Response.Write(strIS_Admin_check);
                }
            }
        }
    }
}