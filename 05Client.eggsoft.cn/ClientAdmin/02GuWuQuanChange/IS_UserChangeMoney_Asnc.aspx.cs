using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._02GuWuQuanChange
{
    public partial class IS_UserChangeMoney_Asnc : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    String strID = Request.QueryString["ID"];
                    String strIS_Admin_Passed = Request.QueryString["IS_Admin_Passed"];


                    EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc_User BLL_tab_GouWuQuan2XianJInEtc_User = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc_User();
                    EggsoftWX.Model.tab_GouWuQuan2XianJInEtc_User Model_tab_GouWuQuan2XianJInEtc_User = BLL_tab_GouWuQuan2XianJInEtc_User.GetModel(Int32.Parse(strID));

                    BLL_tab_GouWuQuan2XianJInEtc_User.Update("ISpassed=" + strIS_Admin_Passed, "ID=" + strID);

                    if (strIS_Admin_Passed == "1")
                    {
                      
                        int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_GouWuQuan2XianJInEtc_User.UserID.ToString());///保证同源
                        if (intUserID_ShopClientID != Model_tab_GouWuQuan2XianJInEtc_User.ShopClientID)
                        {
                            strIS_Admin_Passed = "-1";
                            //Response.Write("-1");///兑换出错
                            //Response.End();
                        }
                        else
                        {
                            EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc BLL_tab_GouWuQuan2XianJInEtc = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();
                            EggsoftWX.Model.tab_GouWuQuan2XianJInEtc Model_BLL_tab_GouWuQuan2XianJInEtc = BLL_tab_GouWuQuan2XianJInEtc.GetModel((int)Model_tab_GouWuQuan2XianJInEtc_User.ID_GouWuQuan2XianJInEtc);


                            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = (Decimal)Model_BLL_tab_GouWuQuan2XianJInEtc.UserGouWuQuan; ;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "申请兑现";
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = (int)Model_tab_GouWuQuan2XianJInEtc_User.UserID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intUserID_ShopClientID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                            BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);


                            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                            my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(intUserID_ShopClientID);
                            string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path((int)Model_tab_GouWuQuan2XianJInEtc_User.UserID);

                            string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/mywebuy.aspx";
                            bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(intUserID_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("兑换通知", "", "申请兑现成功,点击'我'查看", strmywebuyURL);
                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            WeiXinTuWens_ArrayList.Add(First);

                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID_ShopClientID, 0, "申请兑现成功");
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage((int)Model_tab_GouWuQuan2XianJInEtc_User.UserID, intUserID_ShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }
                        }
                    }
                    Response.Write(strIS_Admin_Passed);

                }
                catch (Exception oe)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(oe, "积分兑换");
                }
                finally
                {

                }
            }
        }
    }
}