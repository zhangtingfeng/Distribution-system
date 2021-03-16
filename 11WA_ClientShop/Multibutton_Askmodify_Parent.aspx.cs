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
    public partial class Multibutton_Askmodify_Parent : System.Web.UI.Page
    {

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    string type = Request.QueryString["type"];

                    #region 检查访问权限
                    EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("UserID=" + pub_Int_Session_CurUserID + " and RunningState=1 and IsDeleted=0");
                    if (Model_b002_OperationCenter != null && Model_b002_OperationCenter.RunningState.toBoolean())
                    {
                        ///继续访问
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.TipAndRedirect("权限不足", "/mywebuy.aspx", 2);
                        Response.End();
                    }
                    #endregion 检查访问权限

                    if (string.IsNullOrEmpty(type) == false && type == "iwantaskmodify")
                    {

                        #region 处理申请修改订单      
                        String BuyOrderShopUserID = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderShopUserID"]);
                        String BuyOrderUserRealName = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyOrderUserRealName"]);
                        String BuyParentShopUserID = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyParentShopUserID"]);
                        String BuyGrandParentShopUserID = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["BuyGrandParentShopUserID"]);
                        String UserEmail = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserEmail"]);
                        String Usertel = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["Usertel"]);
                        String UserExtraMemo = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["UserExtraMemo"]);
                        String JSUserSign = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["JSUserSign"]);

                        EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User my_Model_tab_User = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                        #region 检查签名
                        string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(BuyOrderShopUserID + BuyOrderUserRealName + BuyParentShopUserID + BuyGrandParentShopUserID + UserEmail + UserExtraMemo + Eggsoft.Common.DESCrypt.hex_md5_2(my_Model_tab_User.SafeCode));
                        if (JSUserSign != strNetSign)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("" + my_Model_tab_User.SafeCode, "申请修改订单签名失败", "pub_Int_Session_CurUserID=" + pub_Int_Session_CurUserID);
                            Eggsoft.Common.JsUtil.ShowMsg("签名失败,微店将很快联系您！", "javascript:history.back()");
                            return;
                        }
                        #endregion 检查签名



                        EggsoftWX.BLL.b010_AskModifyParent bll_b010_AskModifyParent = new EggsoftWX.BLL.b010_AskModifyParent();
                        EggsoftWX.Model.b010_AskModifyParent Model_AskModifyParent = new EggsoftWX.Model.b010_AskModifyParent();
                        Model_AskModifyParent.ShopClient_ID = pub_Int_ShopClientID;
                        Model_AskModifyParent.OperationCenterID = Model_b002_OperationCenter.ID;
                        Model_AskModifyParent.OperationCenterUserID = pub_Int_Session_CurUserID;
                        Model_AskModifyParent.BuyOrderShopUserID = BuyOrderShopUserID.toInt32();
                        Model_AskModifyParent.BuyOrderUserRealName = BuyOrderUserRealName;
                        Model_AskModifyParent.BuyParentShopUserID = BuyParentShopUserID.toInt32();
                        Model_AskModifyParent.BuyGrandParentShopUserID = BuyGrandParentShopUserID.toInt32();
                        Model_AskModifyParent.Usertel = Usertel;
                        Model_AskModifyParent.UserEmail = UserEmail;

                        #region 更细运营中心的Email地址
                        if (string.IsNullOrWhiteSpace(UserEmail) == false)
                        {
                            my_Model_tab_User.Email = UserEmail;
                            my_Model_tab_User.UpdateBy = "更细运营中心的Email地址";
                            my_Model_tab_User.Updatetime = DateTime.Now;
                            my_BLL_tab_User.Update(my_Model_tab_User);
                        }
                        #endregion 更细运营中心的Email地址

                        Model_AskModifyParent.UserExtraMemo = UserExtraMemo;
                        int intMessageID = bll_b010_AskModifyParent.Add(Model_AskModifyParent);
                        #region 增加未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "请求订单干预";
                        Model_b011_InfoAlertMessage.CreateBy = "userID=" + pub_Int_Session_CurUserID.toString();
                        Model_b011_InfoAlertMessage.UpdateBy = "userID=" + pub_Int_Session_CurUserID.toString();
                        Model_b011_InfoAlertMessage.ShopClient_ID = pub_Int_ShopClientID;
                        Model_b011_InfoAlertMessage.Type = "Info_b010_AskModifyParent";
                        Model_b011_InfoAlertMessage.TypeTableID = intMessageID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加未处理信息

                        Eggsoft.Common.JsUtil.ShowMsg("已成功提交，管理方将很快联系您！", "/mywebuy.aspx");
                        #endregion 处理申请修改订单
                    }

                    else
                    {


                        string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_AskModify_Parent.html");
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
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "申请修改订单");
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