using Eggsoft.Common;
using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{

    //    运营中心报单流程
    //1.运营中心（以下简称甲方）支付货款给沁加收款人（以下简称乙方）。
    //2.乙方确认收款，返回支付流水号给甲方。
    //3.甲方到沁加服务号上输入订单信息（下单人ID、上级ID、购买数量、支付时间、支付流水号、购买数量）。输入后等待审核。
    //4.沁加运营，以下简称（丙方）。核对甲方的订单信息和乙方提供的支付流水号。决定是否批准甲方的报单申请。  并发送相应的反馈消息给甲方
    //5.如果批准甲方的申请。系统将根据该订单上下级归属关系， 查找是否相应的调整系统原有上下级关系，如果不一致就调整。订单进入7天返还的排队序列。

    /// <summary>
    ///  运营中心报单流程
    /// </summary>

    public partial class Multibutton_OperateCenter_Order : System.Web.UI.Page
    {

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    string type = Request.QueryString["type"];

                    #region 检查访问权限
                    EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("UserID=" + pub_Int_Session_CurUserID + " and RunningState=1 and IsDeleted=0");
                    if(Model_b002_OperationCenter != null && Model_b002_OperationCenter.RunningState.toBoolean())
                    {
                        ///继续访问
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.TipAndRedirect("权限不足", "/mywebuy.aspx", 2);
                        Response.End();
                    }
                    #endregion 检查访问权限

                    if(string.IsNullOrEmpty(type) == false && type == "iwantSubmitOrder")
                    {

                        #region 运营中心线上录单系统    
                        String BuyOrderShopUserID = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderShopUserID"]);
                        String BuyOrderUserRealName = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderShopUserIDRealName"]);
                        String BuyOrderShopUserIDIDCard = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderShopUserIDIDCard"]);
                        String BuyOrderShopUserIDContactPhone = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderShopUserIDContactPhone"]);


                        String BuyParentShopUserID = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyParentShopUserID"]);
                        String BuyOrderPaySerialNumber = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderPaySerialNumber"]);
                        String BuyOrderPayTime = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderPayTime"]);
                        String BuyOrderCount = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderCount"]);



                        String Usertel = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["Usertel"]);
                        String UserEmail = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserEmail"]);
                        String UserExtraMemo = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserExtraMemo"]);
                        String JSUserSign = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["JSUserSign"]);

                        EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User my_Model_tab_User = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                        #region 检查签名
                        string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(BuyOrderShopUserID+ BuyOrderPaySerialNumber + BuyParentShopUserID + Usertel + UserEmail + UserExtraMemo + pub_Int_ShopClientID + pub_Int_Session_CurUserID + Eggsoft.Common.DESCrypt.hex_md5_2(my_Model_tab_User.SafeCode));
                        if(JSUserSign != strNetSign)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("" + my_Model_tab_User.SafeCode, "运营中心录单签名失败", "pub_Int_Session_CurUserID=" + pub_Int_Session_CurUserID);
                            Eggsoft.Common.JsUtil.ShowMsg("签名失败,微店将很快联系您！", "javascript:history.back()");
                            return;
                        }
                        #endregion 检查签名


                        EggsoftWX.BLL.b013_WriteOrderByOperation bll_b013_WriteOrderByOperation = new EggsoftWX.BLL.b013_WriteOrderByOperation();
                        EggsoftWX.Model.b013_WriteOrderByOperation Model_b013_WriteOrderByOperation = new EggsoftWX.Model.b013_WriteOrderByOperation();
                        Model_b013_WriteOrderByOperation.ShopClient_ID = pub_Int_ShopClientID;
                        Model_b013_WriteOrderByOperation.OperationCenterID = Model_b002_OperationCenter.ID;
                        Model_b013_WriteOrderByOperation.OperationCenterUserID = pub_Int_Session_CurUserID;
                        Model_b013_WriteOrderByOperation.BuyOrderShopUserID = BuyOrderShopUserID.toInt32();
                        Model_b013_WriteOrderByOperation.OrderPayTime = BuyOrderPayTime.toDateTime();
                        Model_b013_WriteOrderByOperation.PaySerialNumber = BuyOrderPaySerialNumber;
                        Model_b013_WriteOrderByOperation.BuyGoodID = 1;////运营中心第一款商品
                        Model_b013_WriteOrderByOperation.BuyOrderShopUserIDRealName = BuyOrderUserRealName;
                        Model_b013_WriteOrderByOperation.BuyOrderShopUserIDIDCard = BuyOrderShopUserIDIDCard;
                        Model_b013_WriteOrderByOperation.BuyOrderShopUserIDContactPhone = BuyOrderShopUserIDContactPhone;
                        Model_b013_WriteOrderByOperation.BuyOrderCount = BuyOrderCount.toInt32();
                        Model_b013_WriteOrderByOperation.BuyParentShopUserID = BuyParentShopUserID.toInt32();
                        Model_b013_WriteOrderByOperation.OperationCenterTel = Usertel;
                        Model_b013_WriteOrderByOperation.OperationCenterEmail = UserEmail;
                        Model_b013_WriteOrderByOperation.UserExtraMemo = UserExtraMemo;
                        Model_b013_WriteOrderByOperation.CreateBy = "userID=" + pub_Int_Session_CurUserID;
                        Model_b013_WriteOrderByOperation.UpdateBy = "userID=" + pub_Int_Session_CurUserID;

                        #region 更细运营中心的Email地址
                        if(string.IsNullOrWhiteSpace(UserEmail) == false)
                        {
                            my_Model_tab_User.Email = UserEmail;
                            my_Model_tab_User.UpdateBy = "更细运营中心的Email地址";
                            my_Model_tab_User.Updatetime = DateTime.Now;
                            my_BLL_tab_User.Update(my_Model_tab_User);
                        }
                        #endregion 更细运营中心的Email地址

                       
                        int intMessageID = bll_b013_WriteOrderByOperation.Add(Model_b013_WriteOrderByOperation);
                        #region 增加未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "运营中心录单系统";
                        Model_b011_InfoAlertMessage.CreateBy = "userID=" + pub_Int_Session_CurUserID.toString();
                        Model_b011_InfoAlertMessage.UpdateBy = "userID=" + pub_Int_Session_CurUserID.toString();
                        Model_b011_InfoAlertMessage.ShopClient_ID = pub_Int_ShopClientID;
                        Model_b011_InfoAlertMessage.Type = "Info_b013_WriteOrderByOperation";
                        Model_b011_InfoAlertMessage.TypeTableID = intMessageID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加未处理信息

                        Eggsoft.Common.JsUtil.ShowMsg("已成功提交，管理方将很快联系您！", "multibutton_showyunyinzhongxinorderdata.aspx");
                        #endregion 运营中心线上录单系统
                    }

                    else
                    {


                        string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/Multibutton_OperateCenter_Order.html");
                        strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                        strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet);//申请提现的 回调事件
                        strTemplet = strTemplet.Replace("###Header###", "");
                        strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "申请提款"));
                        strTemplet = strTemplet.Replace("###GetstringShowPower_ShopName###", Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()));

                        strTemplet = strTemplet.Replace("###UserDrawMoneyShareFriend###", "0");
                        strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                        string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                        strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);

                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                        strTemplet = strTemplet.Replace("###UserEmail###", Model_tab_User.Email.toString());
                        strTemplet = strTemplet.Replace("###UserTel###", Model_tab_User.ContactPhone.toString());
                        strTemplet = strTemplet.Replace("###YunYingCenterUserID###", pub_Int_Session_CurUserID.toString());

                        strTemplet = strTemplet.Replace("###NetUserSafeCode###", Eggsoft.Common.DESCrypt.hex_md5_2(Model_tab_User.SafeCode));


                        strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                        Response.Write(strTemplet);
                    }
                }
                catch(System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "运营中心录单", "线程异常");
                }
                catch(Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "运营中心录单");
                }
                finally
                {

                }
            }

        }

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }

    }
}