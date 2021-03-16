using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _03OperationCenter_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strPubBoard = "";
        private string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_02OperationCenter")))
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
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.b002_OperationCenter bll = new EggsoftWX.BLL.b002_OperationCenter();
                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", strPubBoard);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }
        private void read_ShopClient_Agent_ID()
        {

            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                int intb002_OperationCenterID = Request.QueryString["ID"].toInt32();
                string strthisPID = "0";



                EggsoftWX.BLL.b002_OperationCenter bll_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = bll_b002_OperationCenter.GetModel("ID=" + intb002_OperationCenterID);
                if (Model_b002_OperationCenter != null) strthisPID = Model_b002_OperationCenter.ParentID.ToString();
                string strSQL = @"SELECT   tab_User.ContactMan, tab_User.ContactPhone, tab_User.NickName, tab_User.UserRealName, 
                tab_User.ShopUserID, b002_OperationCenter.*
FROM      b002_OperationCenter LEFT OUTER JOIN
                tab_User ON b002_OperationCenter.ShopClient_ID = tab_User.ShopClientID AND 
                b002_OperationCenter.UserID = tab_User.ID where b002_OperationCenter.ShopClient_ID={0} and b002_OperationCenter.ID<>{1} and b002_OperationCenter.IsDeleted<>1 order by b002_OperationCenter.id desc";
                strSQL = string.Format(strSQL, strShopClientID, intb002_OperationCenterID);
                System.Data.DataTable myDataTable2 = bll_b002_OperationCenter.SelectList(strSQL).Tables[0];

                ListItem myThisListItem = new ListItem("直接访问，无上级", "0");
                DropDownListChoiceParentIDList.Items.Add(myThisListItem);

                for (int i = 0; i < myDataTable2.Rows.Count; i++)
                {
                    string strID = myDataTable2.Rows[i]["ID"].ToString();
                    string strParentID = myDataTable2.Rows[i]["userID"].ToString();
                    string strShopUserID = myDataTable2.Rows[i]["ShopUserID"].ToString();
                    string strUserRealName = myDataTable2.Rows[i]["UserRealName"].ToString();
                    string strContactPhone = myDataTable2.Rows[i]["ContactPhone"].ToString();
                    string strNickName = myDataTable2.Rows[i]["NickName"].ToString();


                    myThisListItem = new ListItem("运营中心ID：" + strID + " " + "用户ID：" + strShopUserID + " " + strNickName + " " + strUserRealName + " " + strContactPhone, strID);
                    DropDownListChoiceParentIDList.Items.Add(myThisListItem);
                }
                DropDownListChoiceParentIDList.SelectedValue = strthisPID;
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "运营中心");
            }

        }
        private void SetClass()
        {
            read_ShopClient_Agent_ID();
            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.b002_OperationCenter bll = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter Model = bll.GetModel(Int32.Parse(ID));
                txtUserID.Text = "微店ID：" + Eggsoft_Public_CL.Pub.GetMyShopUserID(Model.UserID.toString()) + "  昵称：" + Eggsoft_Public_CL.Pub.GetNickName(Model.UserID.toString());
                txtUserID.Enabled = false;

                TextBox1MasterName.Text = Model.MasterName;
                TextBox2MasterPhone.Text = Model.MasterPhone;
                TextBox3MasterAddress.Text = Model.MasterAddress;
                TextBox4BankAccountUserName.Text = Model.BankAccountUserName;
                TextBox5BankAccountName.Text = Model.BankAccountName;
                TextBox6BankAccountNumber.Text = Model.BankAccountNumber;


                CheckBoxRunningState.Checked = Model.RunningState.toBoolean();
                CheckBoxAccountState.Checked = Model.AccountState.toBoolean();
                CheckBox1ShareholderState.Checked = Model.ShareholderState.toBoolean();

                HyperLink_b003_TotalCredits_OperationCenter.NavigateUrl = "11CenterUser_MoneyStatus.aspx?userid=" + Model.UserID.toString();
                HyperLink_b003_TotalCredits_OperationCenter.Enabled = true;
                btnAdd.Text = "保 存";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            strPubBoard = Request.QueryString["CallBackUrl"].toString();
            try
            {
                string ID = Request.QueryString["ID"];// 修改ID
                String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string type = Request.QueryString["type"];
                string strOtherOperationCenterID = DropDownListChoiceParentIDList.SelectedValue;





                if (type.ToLower() == "modify")
                {
                    EggsoftWX.BLL.b002_OperationCenter bll = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.Model.b002_OperationCenter Model = bll.GetModel(Int32.Parse(ID));
                    Model.ParentID = strOtherOperationCenterID.toInt32();
                    Model.MasterName = TextBox1MasterName.Text.Trim();
                    Model.MasterPhone = TextBox2MasterPhone.Text.Trim();
                    Model.MasterAddress = TextBox3MasterAddress.Text.Trim();
                    Model.BankAccountUserName = TextBox4BankAccountUserName.Text.Trim();
                    Model.BankAccountName = TextBox5BankAccountName.Text.Trim();
                    Model.BankAccountNumber = TextBox6BankAccountNumber.Text.Trim();

                    Model.RunningState = CheckBoxRunningState.Checked;
                    Model.AccountState = CheckBoxAccountState.Checked;
                    Model.ShareholderState = CheckBox1ShareholderState.Checked;
                    
                    Model.UpdateTime = DateTime.Now;
                    Model.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;

                    bll.Update(Model);

                    #region 自动给健康独立法人
                    #region 一键给予高级代理资质证书
                    int intAdd = Eggsoft_Public_CL.Pub_Agent.add_AdvanceAgent_Default_OnlyOneKey(Model.UserID.toInt32(), strINCID.toInt32());

                    #endregion 一键给予高级代理资质证书
                    #endregion 自动给健康独立法人

                    #region 初始化所有运营中心数据  粉丝数据
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32()))
                        {


                            Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(Model.UserID.toInt32(), strINCID.toInt32(), Model.ParentID.toInt32(), Model.ID.toInt32(), Model.UserID.toInt32());
                        }
                    });
                    #endregion 初始化所有运营中心数据

                    JsUtil.ShowMsg("修改成功!" + ((intAdd == 2) ? "警告，需手动批准高级代理资格。已自动为该用户申请高级代理资格，请进入分销商代理商管理，批准该用户的高级代理授权申请" : ""), strPubBoard);

                }
                else
                    if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.b002_OperationCenter bllb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.Model.b002_OperationCenter Modelb002_OperationCenter = new EggsoftWX.Model.b002_OperationCenter();

                

                    Modelb002_OperationCenter.ShopClient_ID = strINCID.toInt32();
                    string strShopID = txtUserID.Text;
                    EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Modeltab_User = blltab_User.GetModel("ShopUserID=" + strShopID + " and ShopClientID=" + strINCID);
                    if (Modeltab_User != null)
                    {
                        if (bllb002_OperationCenter.Exists("UserID=" + Modeltab_User.ID + " and IsDeleted=0"))
                        {
                            JsUtil.ShowMsg("添加失败，当前用户的运营中心已存在!", strPubBoard);
                            return;
                        }
                        Modelb002_OperationCenter.UserID = Modeltab_User.ID;
                    }
                    else
                    {
                        JsUtil.ShowMsg("添加失败，微店ID不存在!", strPubBoard);
                        return;
                    }
                    #region 自动给健康独立法人
                    #region 一键给予高级代理资质证书
                    int intAdd = Eggsoft_Public_CL.Pub_Agent.add_AdvanceAgent_Default_OnlyOneKey(Modelb002_OperationCenter.UserID.toInt32(), strINCID.toInt32());

                    #endregion 一键给予高级代理资质证书
                    #endregion 自动给健康独立法人
                    Modelb002_OperationCenter.MasterName = TextBox1MasterName.Text.Trim();
                    Modelb002_OperationCenter.MasterPhone = TextBox2MasterPhone.Text.Trim();
                    Modelb002_OperationCenter.MasterAddress = TextBox3MasterAddress.Text.Trim();
                    Modelb002_OperationCenter.BankAccountUserName = TextBox4BankAccountUserName.Text.Trim();
                    Modelb002_OperationCenter.BankAccountName = TextBox5BankAccountName.Text.Trim();
                    Modelb002_OperationCenter.BankAccountNumber = TextBox6BankAccountNumber.Text.Trim();
                    Modelb002_OperationCenter.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Modelb002_OperationCenter.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Modelb002_OperationCenter.ParentID = strOtherOperationCenterID.toInt32();
                    Modelb002_OperationCenter.RunningState = CheckBoxRunningState.Checked;
                    Modelb002_OperationCenter.AccountState = CheckBoxAccountState.Checked;
                    Modelb002_OperationCenter.ShareholderState = CheckBox1ShareholderState.Checked;
                    

                    int intOperationCenterID = bllb002_OperationCenter.Add(Modelb002_OperationCenter);


                    #region 初始化所有运营中心数据  粉丝数据
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32()))
                        {
                            Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(Modelb002_OperationCenter.UserID.toInt32(), strINCID.toInt32(), Modelb002_OperationCenter.ParentID.toInt32(), intOperationCenterID, Modelb002_OperationCenter.UserID.toInt32());
                        }
                    });
                    #endregion 初始化所有运营中心数据
                    JsUtil.ShowMsg("添加成功!" + ((intAdd == 2) ? "警告，需手动批准高级代理资格。已自动为该用户申请高级代理资格，请进入分销商代理商管理，批准该用户的高级代理授权申请" : ""), strPubBoard);

                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "后台运营中心", "线程异常");
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog(Exceptione, "后台运营中心");
            }
            finally
            {

            }
        }
    }
}