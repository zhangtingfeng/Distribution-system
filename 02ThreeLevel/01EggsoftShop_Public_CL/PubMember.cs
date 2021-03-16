using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data;
using System.Collections;
using Eggsoft.Common;

namespace Eggsoft_Public_CL
{

    /// <summary>
    ///Pub 的摘要说明
    /// </summary>
    public class PubMember
    {
        public PubMember()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 得到会员卡充值奖励政策
        /// </summary>
        /// <returns></returns>
        public static EggsoftWX.Model.tab_ShopClient_MemberCardBonus getBonusPolicy(int intShopClientID, Decimal? DecimalInputMoney)
        {
            EggsoftWX.Model.tab_ShopClient_MemberCardBonus Model_tab_MemberCardBonus = null;

            EggsoftWX.BLL.tab_ShopClient_MemberCardBonus MBLL_tab_MemberCardBonus = new EggsoftWX.BLL.tab_ShopClient_MemberCardBonus();
            System.Data.DataTable Data_DataTable = MBLL_tab_MemberCardBonus.GetList("ID,InputMoney", "ShopClientID=" + intShopClientID + " and IsDeleted<>1 order by InputMoney asc").Tables[0];
            for(int i = 0; i < Data_DataTable.Rows.Count - 1; i++)
            {
                int intPreID = Convert.ToInt32(Data_DataTable.Rows[i]["ID"]);
                Decimal DecimalPreDBInputMoney = Convert.ToDecimal(Data_DataTable.Rows[i]["InputMoney"]);

                int intCurID = Convert.ToInt32(Data_DataTable.Rows[i + 1]["ID"]);
                Decimal DecimalCureDBInputMoney = Convert.ToDecimal(Data_DataTable.Rows[i + 1]["InputMoney"]);


                if(DecimalInputMoney >= DecimalPreDBInputMoney && DecimalInputMoney < DecimalCureDBInputMoney)
                {
                    Model_tab_MemberCardBonus = MBLL_tab_MemberCardBonus.GetModel(intPreID);
                }
            }
            if(Data_DataTable.Rows.Count > 0)
            {
                if(Model_tab_MemberCardBonus == null && DecimalInputMoney >= Convert.ToDecimal(Data_DataTable.Rows[Data_DataTable.Rows.Count - 1]["InputMoney"]))
                {
                    Model_tab_MemberCardBonus = MBLL_tab_MemberCardBonus.GetModel(Convert.ToInt32(Data_DataTable.Rows[Data_DataTable.Rows.Count - 1]["ID"]));
                }
            }


            return Model_tab_MemberCardBonus;
        }


        private static Object LockCardBonusChangeToUserAccount = new object();
        /// <summary>
        ///  店员充值 为已检测到的 手机号进行充值 转移到到实际现金中
        /// </summary>
        /// <param name="ShopClinettab_MemberCard"></param>
        /// <returns></returns>
        public static bool CardBonusChangeToUserAccount(EggsoftWX.Model.tab_ShopClient_MemberCard ShopClinettab_MemberCard)
        {
            lock(LockCardBonusChangeToUserAccount)
            {
                try
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User ModelUser = BLL_tab_User.GetModel("UserAccount='" + ShopClinettab_MemberCard.PhoneNum + "' and ShopClientID=" + ShopClinettab_MemberCard.ShopClientID);
                    if(ModelUser != null)
                    {
                        Decimal myOLDCountMoney = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(ModelUser.ID, out myOLDCountMoney);


                        if(ShopClinettab_MemberCard.InputMoney > 0)
                        {
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 1;//1表示充值收入
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Convert.ToDecimal(ShopClinettab_MemberCard.InputMoney);
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.StringNum.MaxLengthString(ShopClinettab_MemberCard.CreateBy + ShopClinettab_MemberCard.BonusDesc, 200);
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = ModelUser.ID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = ShopClinettab_MemberCard.ShopClientID;
                            BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                        }
                        if(ShopClinettab_MemberCard.BonusMoney > 0)
                        {
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 1;//1表示充值收入
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Convert.ToDecimal(ShopClinettab_MemberCard.BonusMoney);
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.StringNum.MaxLengthString(ShopClinettab_MemberCard.CreateBy + ShopClinettab_MemberCard.BonusDesc, 200);
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = ModelUser.ID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = ShopClinettab_MemberCard.ShopClientID;
                            int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                            #region 增加账户余额未处理信息
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.CreateBy ="充值";
                            Model_b011_InfoAlertMessage.UpdateBy = "充值";
                            Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                            Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                            Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加账户余额未处理信息 
                        }

                        #region 发送微信模板消息
                        if(ShopClinettab_MemberCard.InputMoney > 0)
                        {
                            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClinettab_MemberCard.ShopClientID));
                            string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                            string strURL = "https://" + strErJiYuMing + "/mywebuy.aspx";

                            Decimal myNowCountMoney = 0;
                            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(ModelUser.ID, out myNowCountMoney);
                            Eggsoft_Public_CL.Pub_GetOpenID_And_.InputMoneyTempletMessage InputMoneyTempletMessage = new Pub_GetOpenID_And_.InputMoneyTempletMessage
                            {
                                ContactPhone = Model_tab_ShopClient.ContactPhone,
                                InputMoney = Convert.ToDecimal(ShopClinettab_MemberCard.InputMoney),
                                LookURL = strURL,
                                OldMoney = myOLDCountMoney,
                                TotalMoney = myNowCountMoney,
                                UserAccount = ShopClinettab_MemberCard.PhoneNum
                            };
                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTemplenputMoneyMessage(ModelUser.ID, Convert.ToInt32(ShopClinettab_MemberCard.ShopClientID), InputMoneyTempletMessage);
                        }
                        #endregion 发送微信模板消息

                        if(ShopClinettab_MemberCard.BonusGouWuQuan > 0)
                        {
                            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Convert.ToDecimal(ShopClinettab_MemberCard.BonusGouWuQuan);
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.StringNum.MaxLengthString(ShopClinettab_MemberCard.CreateBy + ShopClinettab_MemberCard.BonusDesc, 200);
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = ModelUser.ID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = ShopClinettab_MemberCard.ShopClientID;
                            int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                            #region 增加购物券未处理信息
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.CreateBy = "充值赠送";
                            Model_b011_InfoAlertMessage.UpdateBy = "充值赠送";
                            Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                            Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                            Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加购物券未处理信息 
                        }
                        return true;
                    }
                }
                catch(Exception eeeee)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(eeeee, "会员充值", "程序报错");
                    return false;
                }
            }
            return false;
        }


        public static String DisPlayPower(string stringDisPlayPower)
        {
            string strNotDisPlay = " style=\"DISPLAY: none\"";

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

            if(strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" || strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_AdminShopClient")
            {////系统管理员   拥有完全权限
                strNotDisPlay = "";
            }
            else
            {
                EggsoftWX.BLL.tab_ShopClient_AdminUser BLL_tab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_ShopClient_AdminUser();
                EggsoftWX.Model.tab_ShopClient_AdminUser Model_tab_ShopClient_AdminUser = BLL_tab_ShopClient_AdminUser.GetModel("ShopClientAdmin='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "' and ShopClientID=" + strShopClientID + " and isnull(isDeleted,0)=0");

                if(Model_tab_ShopClient_AdminUser != null)
                {
                    EggsoftWX.BLL.tab_Admin_ShopClientPower BLL_tab_Admin_ShopClientPower = new EggsoftWX.BLL.tab_Admin_ShopClientPower();
                    EggsoftWX.Model.tab_Admin_ShopClientPower Model_tab_Admin_ShopClientPower = BLL_tab_Admin_ShopClientPower.GetModel("PowerRoleID=" + Model_tab_ShopClient_AdminUser.ShopClient_Role_PowerID + " and ShopClientPowerItemName='" + stringDisPlayPower + "' and ShopClientID=" + strShopClientID + " and isnull(isDeleted,0)=0");

                    if(Model_tab_Admin_ShopClientPower == null)
                    {
                        return strNotDisPlay;
                    }


                    //Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "BasicSetting", false, Model.RoleName);
                    bool boolPower = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, stringDisPlayPower, false, Model_tab_Admin_ShopClientPower.PowerRoleID);
                    if(boolPower)
                    {
                        strNotDisPlay = "";
                    }
                }
            }
            return strNotDisPlay;
        }


    }
}