using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange
{
    public partial class Manage_28MemberBonus : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strBoardAsx = "Board_28MemberBonus.aspx";

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
                    EggsoftWX.BLL.tab_ShopClient_MemberCardBonus bll = new EggsoftWX.BLL.tab_ShopClient_MemberCardBonus();

                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", strBoardAsx);
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
                EggsoftWX.BLL.tab_ShopClient_MemberCardBonus bll = new EggsoftWX.BLL.tab_ShopClient_MemberCardBonus();
                EggsoftWX.Model.tab_ShopClient_MemberCardBonus Model = bll.GetModel(Int32.Parse(ID));

                txtInputMoney.Text = Model.InputMoney.ToString();
                TextBoxBonusMoney.Text = Model.BonusMoney.ToString();
                TextBoxBonusGouWuQuan.Text = Model.BonusGouWuQuan.ToString();
                TextBoxBonusDesc.Text = Model.BonusDesc;
                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                // RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_MemberCardBonus bll = new EggsoftWX.BLL.tab_ShopClient_MemberCardBonus();
                EggsoftWX.Model.tab_ShopClient_MemberCardBonus Model = bll.GetModel(Int32.Parse(ID));

                Model.InputMoney = Decimal.Parse(txtInputMoney.Text);
                Model.BonusMoney = Decimal.Parse(TextBoxBonusMoney.Text);
                Model.BonusGouWuQuan = Decimal.Parse(TextBoxBonusGouWuQuan.Text);
                Model.UpdateBy = Eggsoft_Public_CL.Pub.geLoginShow();
                Model.BonusDesc = TextBoxBonusDesc.Text;

                Model.UpdateTime = DateTime.Now;

                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", strBoardAsx);

            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.BLL.tab_ShopClient_MemberCardBonus bll = new EggsoftWX.BLL.tab_ShopClient_MemberCardBonus();
                EggsoftWX.Model.tab_ShopClient_MemberCardBonus Model = new EggsoftWX.Model.tab_ShopClient_MemberCardBonus();
                Model.ShopClientID = Int32.Parse(strINCID);
                Model.InputMoney = Decimal.Parse(txtInputMoney.Text);
                Model.BonusMoney = Decimal.Parse(TextBoxBonusMoney.Text);
                Model.BonusGouWuQuan = Decimal.Parse(TextBoxBonusGouWuQuan.Text);
                Model.CreateBy = Eggsoft_Public_CL.Pub.geLoginShow();
                Model.UpdateBy = Eggsoft_Public_CL.Pub.geLoginShow();
                Model.BonusDesc = TextBoxBonusDesc.Text;
                bll.Add(Model);
                JsUtil.ShowMsg("添加成功!", strBoardAsx);
            }
        }
       
    }
}