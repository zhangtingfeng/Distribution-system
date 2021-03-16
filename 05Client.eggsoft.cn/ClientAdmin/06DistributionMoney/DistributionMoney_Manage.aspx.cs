using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._06DistributionMoney
{
    public partial class DistributionMoney_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];

                // Link0.Text = "/default.html";

                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_DistributionMoney bll = new EggsoftWX.BLL.tab_ShopClient_DistributionMoney();


                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "Board_DistributionMoney.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }

        private void intPercent()
        {
            for (int i = 0; i < 100; i++)
            {
                //ListItem myItem = new ListItem();
                //myItem.Text = i.ToString() + "%";
                //myItem.Value = i.ToString();

                ListItem ListItemNew = new ListItem((i).ToString() + "%", (i).ToString());
                DropDownList_Partner0.Items.Add(ListItemNew);

                ListItem ListItemNew1 = new ListItem((i).ToString() + "%", (i).ToString());
                DropDownList_Partner1.Items.Add(ListItemNew1);

                ListItem ListItemNew2 = new ListItem((i).ToString() + "%", (i).ToString());
                DropDownList_Partner2.Items.Add(ListItemNew2);


            }




        }

        private void SetClass()
        {

            intPercent();

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_DistributionMoney bll = new EggsoftWX.BLL.tab_ShopClient_DistributionMoney();
                EggsoftWX.Model.tab_ShopClient_DistributionMoney Model = bll.GetModel(Int32.Parse(ID));

                TextBox_Name.Text = Model.Name;

                DropDownList_Partner0.SelectedIndex = Int32.Parse(Model.Partner.ToString());
                DropDownList_Partner0.Text = Int32.Parse(Model.Partner.ToString()).ToString() + "%";
                DropDownList_Partner0.SelectedValue = Int32.Parse(Model.Partner.ToString()).ToString();

                DropDownList_Partner1.SelectedIndex = Int32.Parse(Model.Partner1.ToString());
                DropDownList_Partner1.Text = Int32.Parse(Model.Partner1.ToString()).ToString() + "%";
                DropDownList_Partner1.SelectedValue = Int32.Parse(Model.Partner1.ToString()).ToString();


                DropDownList_Partner2.SelectedIndex = Int32.Parse(Model.Partner2.ToString());
                DropDownList_Partner2.Text = Int32.Parse(Model.Partner2.ToString()).ToString() + "%";
                DropDownList_Partner2.SelectedValue = Int32.Parse(Model.Partner2.ToString()).ToString();



                //DropDownList_GreatParent.SelectedValue = Int32.Parse(Model.GreatParent.ToString()).ToString();
                //DropDownList_GrandParent.SelectedValue = Int32.Parse(Model.GrandParent.ToString()).ToString() + "%";
                //DropDownList_Parent.SelectedValue = Int32.Parse(Model.Parent.ToString()).ToString() + "%";
                //DropDownList_ShopGet.SelectedValue = Int32.Parse(Model.ShopGet.ToString()).ToString() + "%";

                TextBox_ShopGet_FeiLv.Text = Int32.Parse(Model.ShopGet.ToString()).ToString();
                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }






        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {



            string strDropDownList_Partner0 = DropDownList_Partner0.Text;
            string strDropDownList_Partner1 = DropDownList_Partner1.Text;
            string strDropDownList_Partner2 = DropDownList_Partner2.Text;
            string strTextBox_ShopGet_FeiLv = TextBox_ShopGet_FeiLv.Text;

            int intDropDownList_Partner = 0;
            int intDropDownList_Partner1 = 0;
            int intDropDownList_Partner2 = 0;
            int intTextBox_ShopGet_FeiLv = 0;


            int.TryParse(strDropDownList_Partner0, out intDropDownList_Partner);
            int.TryParse(strDropDownList_Partner1, out intDropDownList_Partner1);
            int.TryParse(strDropDownList_Partner2, out intDropDownList_Partner2);
            int.TryParse(strTextBox_ShopGet_FeiLv, out intTextBox_ShopGet_FeiLv);


            if ((intDropDownList_Partner + intDropDownList_Partner1 + intDropDownList_Partner2 + intTextBox_ShopGet_FeiLv) != 100)
            {
                Eggsoft.Common.JsUtil.ShowMsg("各项总和应该是100", -1);
                return;
            }

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_DistributionMoney bll = new EggsoftWX.BLL.tab_ShopClient_DistributionMoney();
                EggsoftWX.Model.tab_ShopClient_DistributionMoney Model = bll.GetModel(Int32.Parse(ID));


                Model.Name = TextBox_Name.Text;
                Model.Partner = (Decimal)intDropDownList_Partner;
                Model.Partner1 = (Decimal)intDropDownList_Partner1;
                Model.Partner2 = (Decimal)intDropDownList_Partner2;
                Model.ShopGet = (Decimal)intTextBox_ShopGet_FeiLv;
                Model.UpdateTime = DateTime.Now;

                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "Board_DistributionMoney.aspx");

            }
            else
                if (type.ToLower() == "add")
                {
                    string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                    EggsoftWX.BLL.tab_ShopClient_DistributionMoney bll = new EggsoftWX.BLL.tab_ShopClient_DistributionMoney();
                    EggsoftWX.Model.tab_ShopClient_DistributionMoney Model = new EggsoftWX.Model.tab_ShopClient_DistributionMoney();

                    Model.Name = TextBox_Name.Text;
                    Model.Partner = (Decimal)intDropDownList_Partner;
                    Model.Partner1 = (Decimal)intDropDownList_Partner1;
                    Model.Partner2 = (Decimal)intDropDownList_Partner2;
                    Model.ShopGet = (Decimal)intTextBox_ShopGet_FeiLv;
                    Model.UpdateTime = DateTime.Now;
                    Model.ShopClientID = Int32.Parse(strShopClientID);

                    bll.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "Board_DistributionMoney.aspx");
                }
        }
    }
}