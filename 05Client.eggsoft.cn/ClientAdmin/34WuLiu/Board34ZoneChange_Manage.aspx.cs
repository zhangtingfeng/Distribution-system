using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._34WuLiu
{
    public partial class Board34ZoneChange_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL._031_ZONE bllView = new EggsoftWX.BLL._031_ZONE();

       
        string  strBoard = "Board34ZoneChange.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            int intPageIndex = Request.QueryString["PageIndex"].toInt32();
            strBoard += "?PageIndex=" + intPageIndex;

            if (!IsPostBack)
            {
              
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");

                    bllView.Update("IsDeleted=1", "ID=@ID", ID);//.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", strBoard);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass(sender, e);
                }
            }
        }

        private void SetClass(object sender, EventArgs e)
        {
            EggsoftWX.BLL._031_Channel bllView1111 = new EggsoftWX.BLL._031_Channel();
            DropDownList1.DataSource = bllView1111.GetDataTable("1000", "ChannelName", " and isnull(IsDeleted,0)=0 and ShopClient_ID = " + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
            DropDownList1.DataTextField = "ChannelName";
            DropDownList1.DataValueField = "ChannelName";
            DropDownList1.DataBind();

            System.Collections.ArrayList ar = new System.Collections.ArrayList();
            for (int i = 1; i <= 10; i++)
            {
                ar.Add(i);
            }
            for (char i = 'A'; i <= 'Z'; i++)
            {
                ar.Add(i);
               
            }

            this.DropDownList2Zone.DataSource = ar;
            this.DropDownList2Zone.DataBind();
           

           

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model._031_ZONE Model = bllView.GetModel(Int32.Parse(ID));

                TextBox1CNCountry.Text = Model.CNCountry.ToString();
                DropDownList2Zone.SelectedValue = Model.Zone.ToString();
                DropDownList1.SelectedValue = Model.Channel;

                //txtXianJin.Text = Model.XianJinMoney.ToString();
                //TextBoxShortDesc.Text = Model.ShortDesc.ToString();
                //RadioButtonList_ChangeDestination.Items[(Convert.ToInt32(Model.ChangeDestination) - 1)].Selected = true;
                //RadioButtonList_ChangeAutoOrHand.Items[(Model.ChangeAuto == "Auto" ? 0 : 1)].Selected = true;
                //RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                //   RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
            }






        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model._031_ZONE Model = bllView.GetModel(Int32.Parse(ID));




                Model.Channel = DropDownList1.SelectedValue;
                Model.CNCountry = TextBox1CNCountry.Text;
                Model.Zone = DropDownList2Zone.SelectedValue;


                Model.UpdateTime = DateTime.Now;

                bllView.Update(Model);
                JsUtil.ShowMsg("修改成功!", strBoard);

            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.Model._031_ZONE Model = new EggsoftWX.Model._031_ZONE();

                Model.Channel = DropDownList1.SelectedValue;
                Model.CNCountry = TextBox1CNCountry.Text;
                Model.Zone = DropDownList2Zone.SelectedValue; ;
                Model.ShopClient_ID = strINCID.toInt32();
                Model.UpdateTime = DateTime.Now;
                bllView.Add(Model);
                JsUtil.ShowMsg("添加成功!", strBoard);
            }
        }
    }

}