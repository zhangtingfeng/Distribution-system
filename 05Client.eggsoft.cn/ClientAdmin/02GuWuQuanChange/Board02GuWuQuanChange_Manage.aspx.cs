using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._02GuWuQuanChange
{
    public partial class Board02GuWuQuanChange_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
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
                    EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc bll = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();

                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "Board02GuWuQuanChange.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass(sender, e);
                }
            }
        }

        private void SetClass(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc bll = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();
                EggsoftWX.Model.tab_GouWuQuan2XianJInEtc Model = bll.GetModel(Int32.Parse(ID));

                txtGouWuQuan.Text = Model.UserGouWuQuan.ToString();
                txtXianJin.Text = Model.XianJinMoney.ToString();
                TextBoxShortDesc.Text = Model.ShortDesc.ToString();
                RadioButtonList_ChangeDestination.Items[(Convert.ToInt32(Model.ChangeDestination) - 1)].Selected = true;
                RadioButtonList_ChangeAutoOrHand.Items[(Model.ChangeAuto == "Auto" ? 0 : 1)].Selected = true;
                RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
            }






        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc bll = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();
                EggsoftWX.Model.tab_GouWuQuan2XianJInEtc Model = bll.GetModel(Int32.Parse(ID));

                Model.UserGouWuQuan = Decimal.Parse(txtGouWuQuan.Text);
                Model.XianJinMoney = Decimal.Parse(txtXianJin.Text);
                Model.ShortDesc = TextBoxShortDesc.Text;
                Model.ChangeDestination = RadioButtonList_ChangeDestination.SelectedItem.Value == "xianjin" ? 1 : 2;
                Model.ChangeAuto = RadioButtonList_ChangeAutoOrHand.SelectedItem.Value;

                Model.UpdateTime = DateTime.Now;

                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "Board02GuWuQuanChange.aspx");

            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc bll = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();
                    EggsoftWX.Model.tab_GouWuQuan2XianJInEtc Model = new EggsoftWX.Model.tab_GouWuQuan2XianJInEtc();
                    Model.ShopClientID = Int32.Parse(strINCID);
                    Model.ChangeDestination = RadioButtonList_ChangeDestination.SelectedItem.Value == "xianjin" ? 1 : 2;
                    Model.ChangeAuto = RadioButtonList_ChangeAutoOrHand.SelectedItem.Value;

                    Model.UserGouWuQuan = Decimal.Parse(txtGouWuQuan.Text);
                    if (Model.ChangeDestination == 1)
                    {
                        Model.XianJinMoney = Decimal.Parse(txtXianJin.Text);
                    }
                    Model.ShortDesc = TextBoxShortDesc.Text;
                    Model.UpdateTime = DateTime.Now;
                    bll.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "Board02GuWuQuanChange.aspx");
                }
        }
        protected void RadioButtonList_ChangeWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strChangeWay = RadioButtonList_ChangeDestination.SelectedItem.Value;
            if (strChangeWay == "xianjin")
            {
                RadioButtonList_ChangeAutoOrHand.Enabled = true;
                txtXianJin.Enabled = true;
                RequiredFieldValidator1txt_XianJin.Enabled = true;
                RegularExpressionValidator3txt_XianJin.Enabled = true;

                TextBoxShortDesc.Enabled = false;
                RequiredFieldValidatorTextBoxShortDesc_ShiWu.Enabled = false;
                RegularExpressionValidator5TextBoxShortDesc_ShiWu.Enabled = false;

                RadioButtonList_ChangeAutoOrHand.SelectedValue = "Auto";
            }
            else if (strChangeWay == "qita")
            {
                RadioButtonList_ChangeAutoOrHand.SelectedValue = "Hand";
                RadioButtonList_ChangeAutoOrHand.Enabled = false;

                txtXianJin.Enabled = false;
                RequiredFieldValidator1txt_XianJin.Enabled = false;
                RegularExpressionValidator3txt_XianJin.Enabled = false;

                TextBoxShortDesc.Enabled = true;
                RequiredFieldValidatorTextBoxShortDesc_ShiWu.Enabled = true;
                RegularExpressionValidator5TextBoxShortDesc_ShiWu.Enabled = true;
            }
        }
    }
}