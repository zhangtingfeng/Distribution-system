using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._09System_Status
{
    public partial class _15User_ModifyYunYingZhongXin : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_09OperationUserStatus")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限

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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
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
                    EggsoftWX.BLL.b005_UserID_Operation_ID bll_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                    EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = bll_b005_UserID_Operation_ID.GetModel("UserID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);

                    EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User_ = bll_tab_User.GetModel("ID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);

                    if (Model_b005_UserID_Operation_ID != null && Model_tab_User_ != null)
                    {
                        Label_UserID_ShopUserID.Text = Model_tab_User_.ShopUserID.toString();
                        Label_ContactMan.Text = Model_tab_User_.ContactMan;
                        Label_Nickname.Text = Model_tab_User_.NickName;

                        TextBox_YunYingZhongXinID.Text = Model_b005_UserID_Operation_ID.OperationCenterID.toString();
                        TextBox_YunYingZhongXinID_TextChanged(sender, e);
                    }
                    btnAdd.Text = "保 存";
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
                string strUserID = Request.QueryString["UserID"];
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strYunYingZhongXinID = TextBox_YunYingZhongXinID.Text;

                EggsoftWX.BLL.b005_UserID_Operation_ID bll_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                EggsoftWX.Model.b005_UserID_Operation_ID Modelb005_UserID_Operation_ID = bll_b005_UserID_Operation_ID.GetModel("UserID=" + strUserID.toInt32() + " and ShopClientID=" + strShopClient_ID);

                EggsoftWX.BLL.b002_OperationCenter bll_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter Modelb002_OperationCenter = bll_b002_OperationCenter.GetModel("ID=" + strYunYingZhongXinID.toInt32() + " and ShopClient_ID=" + strShopClient_ID);

                if (Modelb005_UserID_Operation_ID != null && Modelb002_OperationCenter != null)
                {
                    Modelb005_UserID_Operation_ID.OperationCenterID = Modelb002_OperationCenter.ID;
                    Modelb005_UserID_Operation_ID.OperationCenterID_UserID = Modelb002_OperationCenter.UserID;

                    Modelb005_UserID_Operation_ID.OperationCenterID = TextBox_YunYingZhongXinID.Text.toInt32();
                    Modelb005_UserID_Operation_ID.UpdateTime = DateTime.Now;
                    Modelb005_UserID_Operation_ID.UpdateBy = "后台修改运营中心" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                    bll_b005_UserID_Operation_ID.Update(Modelb005_UserID_Operation_ID);

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("保存成功!", strCallBackUrl);
                }
                else
                {///不设上级
                    JsUtil.ShowMsg("保存失败，没找到运营中心!", -1);
                }
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "后台修改运营中心");
            }
        }


        protected void TextBox_YunYingZhongXinID_TextChanged(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strYunYingZhongXinID = TextBox_YunYingZhongXinID.Text;


            EggsoftWX.BLL.b002_OperationCenter bll_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
            EggsoftWX.Model.b002_OperationCenter Modelbll_b002_OperationCenter = bll_b002_OperationCenter.GetModel("ID=" + strYunYingZhongXinID.toInt32() + " and ShopClient_ID=" + strShopClient_ID);

            if (Modelbll_b002_OperationCenter != null)
            {
                EggsoftWX.BLL.tab_User bll_OperationCentertab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_OperationCentertab_User_ = bll_OperationCentertab_User.GetModel("ID=" + Modelbll_b002_OperationCenter.UserID + " and ShopClientID=" + strShopClient_ID);

                if (Model_OperationCentertab_User_ != null)
                {
                    Label1YunYinZhongXinParentNickName.Text = Model_OperationCentertab_User_.ContactMan + Model_OperationCentertab_User_.NickName;
                    Image1YunYinZhongXin.ImageUrl = Model_OperationCentertab_User_.HeadImageUrl;
                }
                else
                {
                    Label1YunYinZhongXinParentNickName.Text = "";
                    Image1YunYinZhongXin.ImageUrl = "";
                }
            }
            else
            {
                Label1YunYinZhongXinParentNickName.Text = "";
                Image1YunYinZhongXin.ImageUrl = "";
            }
        }
    }
}