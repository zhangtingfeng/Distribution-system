using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.User
{
    /// <summary>
    /// Game_Vouchers 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Game_Vouchers : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 游戏赠送购物券 
        /// </summary>
        /// <param name="strGoodID">代理管理  </param>
        /// <param name="strParentID">分享人</param>
        /// <returns></returns>
        [WebMethod]
        public String _GameSend_VouchersSave()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            String strUserID = context.QueryString["UserID"];
            String strShopClientID = context.QueryString["ShopClientID"];
            String strintHowMany = context.QueryString["intHowMany"];//多少钱
            String strBoolConsumeOrRecharge = context.QueryString["BoolConsumeOrRecharge"];
            String strGameID = context.QueryString["gameid"];

            int intErrorCode = 1;
            string strReturn = "";
            try
            {
                int intUserID = 0;
                int.TryParse(strUserID, out intUserID);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);

                int intGameID = 0;
                int.TryParse(strGameID, out intGameID);

                Decimal DecimalVouchers = 0;
                Decimal.TryParse(strintHowMany, out DecimalVouchers);


                bool BoolConsumeOrRecharge = true;
                bool.TryParse(strBoolConsumeOrRecharge, out BoolConsumeOrRecharge);

                EggsoftWX.BLL.tab_ShopClient_Game BLL_tab_ShopClient_Game = new EggsoftWX.BLL.tab_ShopClient_Game();
                EggsoftWX.Model.tab_ShopClient_Game Model_tab_ShopClient_Game = BLL_tab_ShopClient_Game.GetModel(intGameID);
                DecimalVouchers = DecimalVouchers * Model_tab_ShopClient_Game.HowManyMoney;

                int intCheckShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if(intCheckShopClientID != intShopClientID)
                {
                    intErrorCode = -1;
                    strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
                }
                else if((DecimalVouchers > 0) && (BoolConsumeOrRecharge))
                {//购物券
                    string strDESMoneyOrGouWuQuan = "";
                    if(Model_tab_ShopClient_Game.SendType == 2)
                    {

                        EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalVouchers;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Model_tab_ShopClient_Game.GameName;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = intUserID;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                        int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                        #region 增加购物券未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                        Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加购物券未处理信息 

                        strDESMoneyOrGouWuQuan = Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(strShopClientID);
                    }
                    else if(Model_tab_ShopClient_Game.SendType == 1)
                    {
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalVouchers;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Model_tab_ShopClient_Game.GameName;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = intUserID;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 22;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                        int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                        #region 增加账户余额未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                        Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加账户余额未处理信息 

                        strDESMoneyOrGouWuQuan = "现金";
                    }

                    string strClientName = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(intShopClientID);
                    string strNickName = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, strNickName + Model_tab_ShopClient_Game.GameName + "送" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalVouchers) + "元" + strDESMoneyOrGouWuQuan + "");

                    intErrorCode = 0;

                    Decimal myCountMoney_Vouchers = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(intUserID, out myCountMoney_Vouchers);

                    string strOK = strNickName + "：" + strClientName + " 已赠送你" + strDESMoneyOrGouWuQuan + Eggsoft_Public_CL.Pub.getPubMoney(DecimalVouchers) + "元,现在总额" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "元,快去逛街吧。";

                    EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                    my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(intShopClientID);
                    string strJumpURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing;
                    strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\",\"OkDesc\":\"" + strOK + "\",\"JumpURL\":\"" + strJumpURL + "\"}";
                    Eggsoft.Common.debug_Log.Call_WriteLog("游戏赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(strShopClientID) + "strUserID=" + strUserID);
                }
            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                intErrorCode = -1;
                strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
            }
            finally
            {

            }
            if(HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.ContentType = ("application/json;charset=UTF-8");
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "";
        }
        /// <summary>
        /// 取游戏信息 
        /// </summary>
        /// <param name="strGameID">游戏的ID</param>
        /// <returns></returns>
        [WebMethod]
        public String _GetGameInfo()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            String strGameID = context.QueryString["GameID"];

            int intErrorCode = 1;
            string strReturn = "";
            try
            {
                int intGameID = 0;
                int.TryParse(strGameID, out intGameID);
                string strJumpURL = "http://000001shiyidianzi.eggsoft.cn";

                EggsoftWX.BLL.tab_ShopClient_Game BLL_tab_ShopClient_Game = new EggsoftWX.BLL.tab_ShopClient_Game();
                EggsoftWX.Model.tab_ShopClient_Game Model_tab_ShopClient_Game = BLL_tab_ShopClient_Game.GetModel("id=" + intGameID + " and ISNULL(IsDeleted,0)=0");

                if(Model_tab_ShopClient_Game != null)
                {
                    EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                    my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Model_tab_ShopClient_Game.ShopClientID);
                    strJumpURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing;
                }
                if(Model_tab_ShopClient_Game == null)
                {
                    intErrorCode = -1;
                    strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\",\"JumpURL\":\"" + strJumpURL + "\"}";
                }
                else if(Model_tab_ShopClient_Game.EndTime < DateTime.Now)//游戏过期
                {
                    intErrorCode = -2;
                    strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\",\"JumpURL\":\"" + strJumpURL + "\"}";
                }

                else
                {//购物券

                    strReturn = "{\"ErrorCode\":\"" + 0 + "\",\"OkDesc\":\"" + "\",\"JumpURL\":\"" + strJumpURL + "\",\"GameName\":\"" + Model_tab_ShopClient_Game.GameName + "\"}";
                    Eggsoft.Common.debug_Log.Call_WriteLog("游戏信息strReturn=" + strReturn);
                }
            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                intErrorCode = -1;
                strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
            }
            finally
            {

            }
            if(HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.ContentType = ("application/json;charset=UTF-8");
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + strReturn + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "";
        }
    }
}
