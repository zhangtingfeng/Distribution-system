using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange
{
    public class EnterpriseOrganizationClass
    {
        public String OrganizationNameShow { get; set; }

        public Int32 OrganizationNameShowID { get; set; }

    }


    public partial class Manage_29AdminPower : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strBoardAsx = "Board_29AdminPower.aspx";
        private EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization BLL_tab_ShopClient_EnterpriseOrganization = new EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization();
        List<EnterpriseOrganizationClass> privateList_EnterpriseOrganizationClass = new List<EnterpriseOrganizationClass>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_AdminUser bll = new EggsoftWX.BLL.tab_ShopClient_AdminUser();

                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", strBoardAsx);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    InitClass1_DataSource();
                    SetClass(sender, e);
                }
            }
        }

        private void InitClass1_DataSource()
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (DropDownList_RoleSelect.SelectedValue.IndexOf("请选择") == -1)
            {
                DropDownList_RoleSelect.DataSource = new EggsoftWX.BLL.tab_ShopClient_Role_Power().GetDataTable("100", "ID,RoleName", " and isnull(isDeleted,0)=0 and ShopClientID=" + strShopClient_ID + "  order by ID asc");
                DropDownList_RoleSelect.DataTextField = "RoleName";
                DropDownList_RoleSelect.DataValueField = "ID";
                DropDownList_RoleSelect.DataBind();
            }
            DropDownList_RoleSelect.Items.Insert(0, new ListItem("请选择角色", "0"));


            if (DropDownListEnterpriseOrganization.SelectedValue.IndexOf("请选择") == -1)
            {
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(int.Parse(strShopClient_ID));

                List<EnterpriseOrganizationClass> myRepeatList = new List<EnterpriseOrganizationClass>();
                getEnterpriseOrganizationClass(strShopClient_ID, myRepeatList, 0, Model_tab_ShopClient.ShopClientName);
                for (int i = 0; i < myRepeatList.Count; i++)
                {
                    //DropDownListEnterpriseOrganization.Items.Insert(i, new ListItem(myRepeatList[i].OrganizationNameShow + "管理团队", myRepeatList[i].OrganizationNameShowID.ToString()));
                    DropDownListEnterpriseOrganization.Items.Insert(i, new ListItem(myRepeatList[i].OrganizationNameShow, myRepeatList[i].OrganizationNameShowID.ToString()));
                }

                //DropDownListEnterpriseOrganization.DataSource = new EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization().GetDataTable("100", "ID,OrganizationName", " and isnull(isDeleted,0)=0 and ShopClientID=" + strShopClient_ID + "  order by ID asc");
                //DropDownListEnterpriseOrganization.DataTextField = "OrganizationName";
                //DropDownListEnterpriseOrganization.DataValueField = "ID";
                //DropDownListEnterpriseOrganization.DataBind();
            }
            DropDownListEnterpriseOrganization.Items.Insert(0, new ListItem("企业管理团队", "0"));
            DropDownListEnterpriseOrganization.Items.Insert(0, new ListItem("请确认所在组织机构", "-1"));


        }

        private List<EnterpriseOrganizationClass> getEnterpriseOrganizationClass(string strShopClient_ID, List<EnterpriseOrganizationClass> myRepeatList, Int32 Int32GetThisID, string strArgParnetOrganizationName)
        {
            string strPutParnetOrganizationName = "";
            if (Int32GetThisID == 0)
            {
                #region

                ////  add nothing
                #endregion

            }
            else
            {
                EggsoftWX.Model.tab_ShopClient_EnterpriseOrganization Model_tab_ShopClient_EnterpriseOrganization = BLL_tab_ShopClient_EnterpriseOrganization.GetModel(Int32GetThisID);
                strPutParnetOrganizationName = Model_tab_ShopClient_EnterpriseOrganization.OrganizationName;
                if (string.IsNullOrEmpty(strArgParnetOrganizationName)) strArgParnetOrganizationName = "总部";
                //bool boolIfExsitSon = BLL_tab_ShopClient_EnterpriseOrganization.Exists("ShopClientID = " + strShopClient_ID + " and ParentID = " + Int32GetThisID + " and isnull(isDeleted, 0) = 0");
                //string strIfManager = "";
                //if (boolIfExsitSon) strIfManager = "管理团队";
                myRepeatList.Add(new EnterpriseOrganizationClass()
                {
                    OrganizationNameShow = Model_tab_ShopClient_EnterpriseOrganization.OrganizationName + "管理团队" + "--上级机构(" + strArgParnetOrganizationName + ")",
                    OrganizationNameShowID = Int32GetThisID
                });
                myRepeatList.Add(new EnterpriseOrganizationClass()
                {
                    OrganizationNameShow = Model_tab_ShopClient_EnterpriseOrganization.OrganizationName + "普通员工",
                    OrganizationNameShowID = -Int32GetThisID
                });
            }
            System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_EnterpriseOrganization.GetList("OrganizationName,ID", "ShopClientID=" + strShopClient_ID + " and ParentID=" + Int32GetThisID + " and isnull(isDeleted,0)=0 order by POS asc,ID asc").Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                getEnterpriseOrganizationClass(strShopClient_ID, myRepeatList, Int32.Parse(Data_DataTable.Rows[i]["ID"].ToString()), strPutParnetOrganizationName);
            }

            return myRepeatList;
        }





        private void SetClass(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_AdminUser bll = new EggsoftWX.BLL.tab_ShopClient_AdminUser();
                EggsoftWX.Model.tab_ShopClient_AdminUser Model = bll.GetModel(Int32.Parse(ID));

                TextBoxUserRealName.Text = Model.UserRealName;
                txtInputMoneyShopClientAdmin.Text = Model.ShopClientAdmin;
                DropDownListEnterpriseOrganization.SelectedValue = Model.EnterpriseOrganizationID.ToString();
                DropDownList_RoleSelect.SelectedValue = Model.ShopClient_Role_PowerID.ToString();
                btnAdd.Text = "保 存";

                #region 密码不修改请置空
                TextboxUserPasswordLabel_ModifyTip.Visible = true;
                TextboxRePasswordLabel_ModifyTip0.Visible = true;

                RequiredFieldValidatorTextboxUserPassword.Enabled = false;
                CompareValidator5451.Enabled = false;
                #endregion

            }
            else if (type.ToLower() == "add")
            {
                // RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strRoleSelectValue = DropDownList_RoleSelect.SelectedValue;
            if (strRoleSelectValue == "0")
            {
                if (new EggsoftWX.BLL.tab_ShopClient_Role_Power().Exists("ShopClientID=" + strShopClient_ID + " and isnull(isDeleted,0)=0") == false)
                {
                    JsUtil.ShowMsg("用户添加失败,必须选择角色", "/ClientAdmin/29ShopClientPower/Manage_29RolePower.aspx?type=Add");
                }
                else
                {
                    JsUtil.ShowMsg("用户添加失败,必须选择角色", -1);
                }
                return;
            }
            string strEnterpriseOrganizationSelectValue = DropDownListEnterpriseOrganization.SelectedValue;
            if (strEnterpriseOrganizationSelectValue == "-1")
            {
                if (BLL_tab_ShopClient_EnterpriseOrganization.Exists("ShopClientID=" + strShopClient_ID + " and isnull(isDeleted,0)=0") == false)
                {
                    JsUtil.ShowMsg("用户添加失败,必须选择企业组织机构", "/ClientAdmin/28Member/EnterpriseOrganization.aspx");
                }
                else
                {
                    JsUtil.ShowMsg("用户添加失败,必须选择企业组织机构", -1);
                }
                return;
            }


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_AdminUser blltab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_ShopClient_AdminUser();
                EggsoftWX.Model.tab_ShopClient_AdminUser Model = blltab_ShopClient_AdminUser.GetModel(Int32.Parse(ID));
                if (txtInputMoneyShopClientAdmin.Text != Model.ShopClientAdmin)////修改用户名 检查是否重复
                {

                    if (!blltab_ShopClient_AdminUser.Exists("ShopClientAdmin='" + txtInputMoneyShopClientAdmin.Text + "' and ShopClientID="+ strShopClient_ID + " and isDeleted=0"))
                    {
                        Model.UserRealName = TextBoxUserRealName.Text;
                        Model.ShopClientAdmin = txtInputMoneyShopClientAdmin.Text;
                        if (string.IsNullOrEmpty(TextboxUserPassword.Text) == false)
                        {
                            Model.ShopClientAdminPassword = Eggsoft.Common.DESCrypt.GetMd5Str32(TextboxUserPassword.Text);
                        }
                        Model.ShopClient_Role_PowerID = int.Parse(strRoleSelectValue);
                        Model.EnterpriseOrganizationID = (strEnterpriseOrganizationSelectValue).toInt32();
                        blltab_ShopClient_AdminUser.Update(Model);
                        JsUtil.ShowMsg("修改成功!", strBoardAsx);
                    }
                    else
                    {
                        JsUtil.ShowMsg("修改的用户名已存在,修改失败!", -1);
                    }
                }
                else
                {
                    Model.UserRealName = TextBoxUserRealName.Text;
                    Model.EnterpriseOrganizationID = int.Parse(strEnterpriseOrganizationSelectValue);
                    Model.ShopClient_Role_PowerID = int.Parse(strRoleSelectValue);
                    if (string.IsNullOrEmpty(TextboxUserPassword.Text) == false)
                    {
                        Model.ShopClientAdminPassword = Eggsoft.Common.DESCrypt.GetMd5Str32(TextboxUserPassword.Text);
                    }
                    Model.Updatetime = DateTime.Now;
                    blltab_ShopClient_AdminUser.Update(Model);
                    JsUtil.ShowMsg("修改成功!", strBoardAsx);
                }

            }
            else if (type.ToLower() == "add")
            {
                if (String.IsNullOrEmpty(TextboxUserPassword.Text))
                {
                    JsUtil.ShowMsg("必须输入密码!", -1);
                }

                EggsoftWX.BLL.tab_ShopClient_AdminUser blltab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_ShopClient_AdminUser();
                EggsoftWX.Model.tab_ShopClient_AdminUser Model = new EggsoftWX.Model.tab_ShopClient_AdminUser();


                string strWillAdd = txtInputMoneyShopClientAdmin.Text;
                //string strSafeFilterWillAdd = Eggsoft.Common.CommUtil.SafeFilter(strWillAdd);

                EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();


                if (!bll_tab_ShopClient.Exists("UserName='" + strWillAdd + "'"))
                {
                    if (!blltab_ShopClient_AdminUser.Exists("ShopClientAdmin='" + strWillAdd + "' and ShopClientID=" + strShopClient_ID + "  and isDeleted=0"))
                    {
                        Model.ShopClientID = Int32.Parse(strShopClient_ID);
                        Model.UserRealName = TextBoxUserRealName.Text;
                        Model.ShopClientAdmin = txtInputMoneyShopClientAdmin.Text;
                        Model.ShopClientAdminPassword = Eggsoft.Common.DESCrypt.GetMd5Str32(TextboxUserPassword.Text);
                        Model.ShopClient_Role_PowerID = int.Parse(strRoleSelectValue);
                        Model.EnterpriseOrganizationID = int.Parse(strEnterpriseOrganizationSelectValue);
                        blltab_ShopClient_AdminUser.Add(Model);
                        JsUtil.ShowMsg("添加成功!", strBoardAsx);
                    }
                    else
                    {
                        JsUtil.ShowMsg("新增的用户名已存在,添加失败!", -1);
                    }
                }
                else
                {
                    JsUtil.ShowMsg("店铺用户名已存在,添加失败!", -1);
                }

            }
        }

    }
}