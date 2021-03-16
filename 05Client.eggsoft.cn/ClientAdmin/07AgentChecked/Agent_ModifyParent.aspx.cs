using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{
    public partial class Agent_ModifyParent : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    string type = Request.QueryString["type"];

                    if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                    {
                        SetClass();
                    }


                }

            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }
        private void read_ShopClient_Agent_ID()
        {

            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                string strUserID = Request.QueryString["UserID"];





                EggsoftWX.BLL.View_ShopClient_Agent bll_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
                EggsoftWX.Model.View_ShopClient_Agent Model_View_ShopClient_Agent = bll_View_ShopClient_Agent.GetModel("userID=" + strUserID);
                string strthisPID = Model_View_ShopClient_Agent.ParentID.ToString();

                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel("ID=@ID and ShopClientID=@ShopClientID", strthisPID.toInt32(), strShopClientID.toInt32());
                if (Model_tab_User != null)
                {
                    TextBox_NickName.Text = Model_tab_User.NickName;
                    TextBox_ShopUserID.Text = Model_tab_User.ShopUserID.toString();
                    Image_HeadURL.ImageUrl = Model_tab_User.HeadImageUrl;
                }
                else
                {
                    TextBox_NickName.Text = "";
                    TextBox_ShopUserID.Text = "0";
                    Image_HeadURL.ImageUrl = "";
                }

            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "修改代理的上级");
            }

        }

        private void SetClass()
        {
            try
            {
                read_ShopClient_Agent_ID();

                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string strUserID = Request.QueryString["UserID"];
                    EggsoftWX.BLL.View_ShopClient_Agent bll = new EggsoftWX.BLL.View_ShopClient_Agent();
                    EggsoftWX.Model.View_ShopClient_Agent Model = bll.GetModel("UserID=" + strUserID);

                    Label_ContactMan.Text = Model.UserRealName;
                    Label_ShopClientName.Text = Model.ShopName;



                    btnAdd.Text = "保 存";
                    //RequiredFieldValidator3.Enabled = false;
                }
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
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                string strUserID = Request.QueryString["UserID"];
                string strParentID = "0";
                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", TextBox_ShopUserID.Text.toInt32(), strShopClient_ID.toInt32());
                if (Model_tab_User != null)
                {
                    strParentID = Model_tab_User.ID.toString();
                }
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_bll_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("userID=" + strUserID + " and ShopClientID=" + strShopClient_ID);
                if (Model_bll_tab_ShopClient_Agent_ != null)
                {
                    Model_bll_tab_ShopClient_Agent_.ParentID = strParentID.toInt32();
                    Model_bll_tab_ShopClient_Agent_.UpdateTime = DateTime.Now;
                    bll_tab_ShopClient_Agent_.Update(Model_bll_tab_ShopClient_Agent_);


                    EggsoftWX.Model.tab_User Model_tab_User_ = bll_tab_User.GetModel("ID=" + strUserID + " and ShopClientID=" + strShopClient_ID);
                    if (Model_tab_User_ != null)
                    {
                        Model_tab_User_.ParentID = strParentID.toInt32();
                        Model_tab_User_.Updatetime = DateTime.Now;
                        bll_tab_User.Update(Model_tab_User_);
                    }
                }

                string strCallBackUrl = Request.QueryString["CallBackUrl"];
                strCallBackUrl = strCallBackUrl.Replace("*", "?");

                JsUtil.ShowMsg("保存成功!", strCallBackUrl);

            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "修改代理上级");
            }

        }


        protected void TextBox_NickName_TextChanged(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            int Int64ID = 0;
            EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
            String strSQL = "select top 1 ID from tab_User where [ContactMan] like @NickName  or [UserRealName] like @NickName  or [UserRealName] like @NickName or [NickName] like @NickName and ShopClientID=@ShopClientID order by ID desc";
            System.Data.DataTable myDataTable = bll_tab_User.SelectList(strSQL, "%" + TextBox_NickName.Text.Trim() + "%", strShopClient_ID.toInt32()).Tables[0];
            if (myDataTable.Rows.Count > 0)
            {
                Int64ID = myDataTable.Rows[0][0].toInt32();
            }
            EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(Int64ID);
            if (Model_tab_User != null)
            {
                //TextBox_NickName.Text = Model_tab_User.NickName;
                TextBox_ShopUserID.Text = Model_tab_User.ShopUserID.toString();
                Image_HeadURL.ImageUrl = Model_tab_User.HeadImageUrl;
            }
            else
            {
                //TextBox_NickName.Text = "";
                TextBox_ShopUserID.Text = "0";
                Image_HeadURL.ImageUrl = "";
            }
        }

        protected void TextBox_ShopUserID_TextChanged(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", TextBox_ShopUserID.Text.toInt32(), strShopClient_ID.toInt32());
            if (Model_tab_User != null)
            {
                TextBox_NickName.Text = Model_tab_User.NickName;
                //TextBox_ShopUserID.Text = Model_tab_User.ShopUserID.toString();
                Image_HeadURL.ImageUrl = Model_tab_User.HeadImageUrl;
            }
            else
            {
                TextBox_NickName.Text = "";
                //TextBox_ShopUserID.Text = "0";
                Image_HeadURL.ImageUrl = "";
            }
        }
    }
}