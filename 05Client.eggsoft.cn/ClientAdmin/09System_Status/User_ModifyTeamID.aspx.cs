using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._09System_Status
{
    public partial class User_ModifyTeamID : Eggsoft.Common.DotAdminPage_ClientAdmin
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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "User_ModifyTeamID");
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

                    Label_ContactMan.Text = Model_tab_User_.ContactMan.ToString();
                    Label_Nickname.Text = Model_tab_User_.NickName;
                    Label_UserID_ShopUserID.Text = Model_tab_User_.ShopUserID.toString();

                    EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ TeamModel_tab_ShopClient_Agent__ = bll_tab_ShopClient_Agent_.GetModel("ID=" + Model_tab_User_.TeamID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (TeamModel_tab_ShopClient_Agent__ != null)
                    {
                        Label1NickTeamID.Text = TeamModel_tab_ShopClient_Agent__.ShopTeamID.toString();
                        TextBox_TeamID.Text = Label1NickTeamID.Text;
                        TextBox_TeamID_TextChanged(sender, e);
                    }
                    btnAdd.Text = "保 存";
                }
            }
            catch(System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "后台修改团队User_ModifyTeamID", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "后台修改团队User_ModifyTeamID");
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strUserID = Request.QueryString["UserID"];
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strParentID_ShopUserD = TextBox_TeamID.Text;

                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ tab_ShopClient_Agent__User_ = bll_tab_ShopClient_Agent_.GetModel("ShopTeamID=" + strParentID_ShopUserD.toInt32() + " and ShopClientID=" + strShopClient_ID);

                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                //EggsoftWX.Model.tab_User ParentModel_tab_User_ = bll_tab_User.GetModel("ShopUserID=" + strParentID_ShopUserD.toInt32() + " and ShopClientID=" + strShopClient_ID);
                if (tab_ShopClient_Agent__User_ != null)
                {
                    EggsoftWX.Model.tab_User Model_tab_User_ = bll_tab_User.GetModel("ID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    if (Model_tab_User_ != null)
                    {
                        Model_tab_User_.TeamID = tab_ShopClient_Agent__User_.ID;
                        Model_tab_User_.Updatetime = DateTime.Now;
                        Model_tab_User_.UpdateBy = "后台修改团队"+ strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        bll_tab_User.Update(Model_tab_User_);
                    }

                    


                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("保存成功!", strCallBackUrl);
                }
                else if (strParentID_ShopUserD.toInt32() == 0)
                {///不设上级
                    EggsoftWX.Model.tab_User Model_tab_User_ = bll_tab_User.GetModel("ID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                    Model_tab_User_.TeamID = 0;
                    Model_tab_User_.Updatetime = DateTime.Now;
                    Model_tab_User_.UpdateBy = "后台不设置团队"+ strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    bll_tab_User.Update(Model_tab_User_);

                  

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("保存成功!", strCallBackUrl);
                }
                else
                {
                    JsUtil.ShowMsg("保存失败，没找到团队!", -1);
                }
            }
            catch(System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "后台修改团队User_ModifyTeamID", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "后台修改团队User_ModifyTeamID");
            }
        }

        protected void TextBox_TeamID_TextChanged(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strTextBox_TeamID_ShopTeamID = TextBox_TeamID.Text;

            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ ShopTeamIDtab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("ShopTeamID=" + strTextBox_TeamID_ShopTeamID.toInt32() + " and ShopClientID=" + strShopClient_ID);

            if (ShopTeamIDtab_ShopClient_Agent_ != null)
            {
                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User ParentModel_tab_User_ = bll_tab_User.GetModel("ID=" + ShopTeamIDtab_ShopClient_Agent_.UserID.toInt32() + " and ShopClientID=" + strShopClient_ID);
                if (ParentModel_tab_User_ != null)
                {
                    Label1NickTeamID.Text = ParentModel_tab_User_.NickName;
                    Image1TeamID.ImageUrl = ParentModel_tab_User_.HeadImageUrl;
                    Image1TeamID.ImageAlign = ImageAlign.Left;
                }
                else
                {
                    Label1NickTeamID.Text = "";
                    Image1TeamID.ImageUrl = "";
                }
            }
            else {
                Label1NickTeamID.Text = "";
                Image1TeamID.ImageUrl = "";
            }
           

        }
    }
}