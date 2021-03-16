using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._09System_Status
{
    public partial class User_ModifyParent : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    string type = Request.QueryString["type"];

                    if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                    {
                        SetClass(sender, e);
                    }


                }

            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "User_ModifyParent");
            }
        }


        private void SetClass(object sender, EventArgs e)
        {
            try
            {
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string strUserID = Request.QueryString["UserID"];
                    EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User_ = bll_tab_User.GetModel("ID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);

                    Label_ContactMan.Text = Model_tab_User_.ContactMan;
                    Label_Nickname.Text = Model_tab_User_.NickName;
                    Label_UserID_ShopUserID.Text = Model_tab_User_.ShopUserID.toString();

                    EggsoftWX.Model.tab_User ParentModel_tab_User_ = bll_tab_User.GetModel("ID=" + Model_tab_User_.ParentID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (ParentModel_tab_User_ != null)
                    {
                        TextBox_PaerntID_ShopUserD.Text = ParentModel_tab_User_.ShopUserID.toString();
                        TextBox_PaerntID_ShopUserD_TextChanged(sender, e);
                    }
                    btnAdd.Text = "保 存";
                }
            }
            catch(System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "07AgentChecked", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strUserID = Request.QueryString["UserID"];
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strParentID_ShopUserD = TextBox_PaerntID_ShopUserD.Text;

                EggsoftWX.BLL.b005_UserID_Operation_ID bll_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User ParentModel_tab_User_ = bll_tab_User.GetModel("ShopUserID=" + strParentID_ShopUserD.toInt32() + " and ShopClientID=" + strShopClient_ID);
                if (ParentModel_tab_User_ != null)
                {
                    EggsoftWX.Model.tab_User Model_tab_User_ = bll_tab_User.GetModel("ID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (Model_tab_User_ != null)
                    {
                        Model_tab_User_.ParentID = ParentModel_tab_User_.ID;
                        Model_tab_User_.Updatetime = DateTime.Now;
                        Model_tab_User_.UpdateBy = "后台修改上级"+ strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        bll_tab_User.Update(Model_tab_User_);
                    }

                    EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = bll_b005_UserID_Operation_ID.GetModel("UserID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (Model_b005_UserID_Operation_ID != null)
                    {
                        Model_b005_UserID_Operation_ID.UserParentID = ParentModel_tab_User_.ID;
                        Model_b005_UserID_Operation_ID.UpdateTime = DateTime.Now;
                        Model_b005_UserID_Operation_ID.UpdateBy = "后台修改运营代理上级"+ strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        bll_b005_UserID_Operation_ID.Update(Model_b005_UserID_Operation_ID);
                    }

                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        Model_tab_ShopClient_Agent_.ParentID = ParentModel_tab_User_.ID;
                        Model_tab_ShopClient_Agent_.UpdateTime = DateTime.Now;
                        Model_tab_ShopClient_Agent_.UpdateBy = "后台修改代理上级"+ strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        bll_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_);
                    }


                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("保存成功!", strCallBackUrl);
                }
                else if (strParentID_ShopUserD.toInt32() == 0)
                {///不设上级
                    EggsoftWX.Model.tab_User Model_tab_User_ = bll_tab_User.GetModel("ID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    Model_tab_User_.ParentID = 0;
                    Model_tab_User_.Updatetime = DateTime.Now;
                    Model_tab_User_.UpdateBy = "后台不设置上级";
                    bll_tab_User.Update(Model_tab_User_);

                    EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = bll_b005_UserID_Operation_ID.GetModel("UserID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (Model_b005_UserID_Operation_ID != null)
                    {
                        Model_b005_UserID_Operation_ID.UserParentID = 0;
                        Model_b005_UserID_Operation_ID.UpdateTime = DateTime.Now;
                        Model_b005_UserID_Operation_ID.UpdateBy = "后台运营修改代理上级"+ strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        bll_b005_UserID_Operation_ID.Update(Model_b005_UserID_Operation_ID);
                    }

                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        Model_tab_ShopClient_Agent_.ParentID = 0;
                        Model_tab_ShopClient_Agent_.UpdateTime = DateTime.Now;
                        Model_tab_ShopClient_Agent_.UpdateBy = "后台修改代理上级"+ strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        bll_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_);
                    }

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("保存成功!", strCallBackUrl);
                }
                else
                {
                    JsUtil.ShowMsg("保存失败，没找到上级!", -1);
                }
            }
            catch(System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "后台修改上级", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "后台修改上级");
            }
        }

        protected void TextBox_PaerntID_ShopUserD_TextChanged(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strParentID_ShopUserD = TextBox_PaerntID_ShopUserD.Text;
            EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User ParentModel_tab_User_ = bll_tab_User.GetModel("ShopUserID=" + strParentID_ShopUserD.toInt32() + " and ShopClientID=" + strShopClient_ID);
            if (ParentModel_tab_User_ != null)
            {
                Label1ParentNickName.Text = ParentModel_tab_User_.NickName;
                Image1Parent.ImageUrl = ParentModel_tab_User_.HeadImageUrl;
                Image1Parent.ImageAlign = ImageAlign.Left;
            }
            else
            {
                Label1ParentNickName.Text = "";
                Image1Parent.ImageUrl = "";
            }

        }
    }
}