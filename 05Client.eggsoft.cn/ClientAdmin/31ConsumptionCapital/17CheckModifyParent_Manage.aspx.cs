using Eggsoft.Common;
using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _17CheckModifyParent_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strPubBoard = "";
        private string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_16CheckModifyParent")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限
            if (!IsPostBack)
            {
                strPubBoard = Request.QueryString["CallBackUrl"].toString();

                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string strID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.b010_AskModifyParent bll = new EggsoftWX.BLL.b010_AskModifyParent();
                    EggsoftWX.Model.b010_AskModifyParent Model = bll.GetModel("ID=@ID", strID.toInt32());
                    Model.IsDeleted = 1;
                    strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                    Model.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model.UpdateTime = DateTime.Now;
                    bll.Update(Model);

                    #region 消除处理消息
                    strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = bll_b011_InfoAlertMessage.GetModel("Type='Info_b010_AskModifyParent' and TypeTableID=" + strID + "");
                    if (Model_b011_InfoAlertMessage != null)
                    {
                        Model_b011_InfoAlertMessage.Done = true;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateTime = DateTime.Now;
                        bll_b011_InfoAlertMessage.Update(Model_b011_InfoAlertMessage);
                    }
                    #endregion 消除处理消息

                    JsUtil.ShowMsg("删除成功!", strPubBoard);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }

        private void SetClass()
        {
            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toString();
                int intb010_AskModifyParent = Request.QueryString["ID"].toInt32();

                EggsoftWX.BLL.b010_AskModifyParent bll_b010_AskModifyParent = new EggsoftWX.BLL.b010_AskModifyParent();
                EggsoftWX.Model.b010_AskModifyParent Model_b010_AskModifyParent = bll_b010_AskModifyParent.GetModel("ID=" + intb010_AskModifyParent);

                TextBox1FeedbackMemo.Text = Model_b010_AskModifyParent.FeedbackMemo.toString();


                string strUserIDInfoAlert = "";
                strUserIDInfoAlert += "下单用户编号：" + Model_b010_AskModifyParent.BuyOrderShopUserID + "<br />";
                strUserIDInfoAlert += "下单用户姓名：" + Model_b010_AskModifyParent.BuyOrderUserRealName + "<br />";

                strUserIDInfoAlert += "上级编号：" + Model_b010_AskModifyParent.BuyParentShopUserID + "<br />";
                EggsoftWX.BLL.tab_User bll_Parenttab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_Parenttab_User = bll_Parenttab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", Model_b010_AskModifyParent.BuyParentShopUserID, strShopClientID.toInt32());
                if (Model_Parenttab_User != null)
                {
                    strUserIDInfoAlert += "上级姓名：" + Model_Parenttab_User.UserRealName + "<br />";
                }

                strUserIDInfoAlert += "上上级编号：" + Model_b010_AskModifyParent.BuyGrandParentShopUserID + "";
                EggsoftWX.Model.tab_User Model_GrandParenttab_User = bll_Parenttab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", Model_b010_AskModifyParent.BuyGrandParentShopUserID, strShopClientID.toInt32());
                if (Model_GrandParenttab_User != null)
                {
                    strUserIDInfoAlert += "上上级姓名：" + Model_GrandParenttab_User.UserRealName + "<br />";
                }
                Label1AskForInfo.Text = strUserIDInfoAlert;


                string strOperationCenterInfoAlert = "";
                strOperationCenterInfoAlert += "运营中心编号：" + Model_b010_AskModifyParent.OperationCenterID + "<br />";
                strOperationCenterInfoAlert += "运营中心联系电话：" + Model_b010_AskModifyParent.Usertel + "<br />";
                strOperationCenterInfoAlert += "运营中心邮件地址：" + Model_b010_AskModifyParent.UserEmail + "";
                Label1OperationCenterInfo.Text = strOperationCenterInfoAlert;

                Label1UserExtraMemo.Text = Model_b010_AskModifyParent.UserExtraMemo.toString();
                TextBox1FeedbackMemo.Text = Model_b010_AskModifyParent.FeedbackMemo.toString();


                btnAdd.Text = "提 交";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {



            strPubBoard = Request.QueryString["CallBackUrl"].toString();
            try
            {
                string TypeTableID = Request.QueryString["ID"];// 修改ID
                String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    #region 消除处理消息
                    strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = bll_b011_InfoAlertMessage.GetModel("Type='Info_b010_AskModifyParent' and TypeTableID=" + TypeTableID + "");
                    if (Model_b011_InfoAlertMessage != null)
                    {
                        Model_b011_InfoAlertMessage.Done = true;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateTime = DateTime.Now;
                        bll_b011_InfoAlertMessage.Update(Model_b011_InfoAlertMessage);
                    }
                    #endregion 消除处理消息

                    EggsoftWX.BLL.b010_AskModifyParent bllb010_AskModifyParent = new EggsoftWX.BLL.b010_AskModifyParent();
                    EggsoftWX.Model.b010_AskModifyParent Modelb010_AskModifyParent = bllb010_AskModifyParent.GetModel(Int32.Parse(TypeTableID));



                    Modelb010_AskModifyParent.FeedbackStatus = RadioButtonList1FeedbackStatus.SelectedValue.toInt32();
                    Modelb010_AskModifyParent.FeedbackMemo = TextBox1FeedbackMemo.Text.Trim();

                    Modelb010_AskModifyParent.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Modelb010_AskModifyParent.UpdateTime = DateTime.Now;


                    bllb010_AskModifyParent.Update(Modelb010_AskModifyParent);

                    #region 发送微信消息
                    string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(Modelb010_AskModifyParent.OperationCenterUserID.toString());
                    string strTitle = strUserNickName + "你好。你的申请上下级关系的调整:" + RadioButtonList1FeedbackStatus.SelectedItem + "\n";
                    strTitle += "申请信息：" + Label1AskForInfo.Text.Replace("<br />", "\n") + "\n";
                    strTitle += "我们的处理结果:" + RadioButtonList1FeedbackStatus.SelectedItem + "\n";
                    strTitle += "我们的备注信息:" + Modelb010_AskModifyParent.FeedbackMemo + "\n";
                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Modelb010_AskModifyParent.OperationCenterUserID.toInt32(), 0, strTitle);
                    #endregion 发送微信消息

                    #region 发送邮件消息
                    EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(strINCID));


                    string strTo = Modelb010_AskModifyParent.UserEmail;
                    if (string.IsNullOrWhiteSpace(strTo) == false)
                    {
                        string strSubject = tab_System_And_.getTab_System("CityName") + "微店" + " 你的申请上下级关系的调整！";
                        string strBody = "你好，我们给你发信，是因为" + tab_System_And_.getTab_System("CityName") + "微店" + "反馈上下级关系的调整通知引起！" + "\n";
                        strBody += "你的申请信息:" + Label1AskForInfo.Text + "\n";
                        strBody += "你的备注信息:" + Modelb010_AskModifyParent.UserExtraMemo + "\n";
                        strBody += "我们的处理结果:" + RadioButtonList1FeedbackStatus.SelectedItem + "\n";
                        strBody += "我们的备注信息:" + Modelb010_AskModifyParent.FeedbackMemo + "\n";
                        Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName + "微店", strTo, strSubject, strBody);
                    }
                    #endregion 发送邮件消息


                    JsUtil.ShowMsg("反馈成功!", strPubBoard);

                }
                else
                    if (type.ToLower() == "add")
                {


                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "后台运营中心处理申请上下级关系", "线程异常");
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog(Exceptione, "后台运营中心处理申请上下级关系");
            }
            finally
            {

            }
        }


        //private void sendSNSToMyParentBonus_WeiXin(int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        //{
        //    string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
        //    string strTitle = "致歉信:" + strUserNickName + "您好，由于今早机房网络更换，导致部分用户分红未到账。你的今天分红已补发完毕。给您带来的不便深表歉意";
        //    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pub_Int_Session_CurUserID, pub_Int_Session_CurUserID, strTitle);

        //}
    }
}