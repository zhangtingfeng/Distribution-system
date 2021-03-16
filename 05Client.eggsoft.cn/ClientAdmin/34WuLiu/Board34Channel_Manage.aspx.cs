using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._34WuLiu
{
    public partial class Board34Channel_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL._031_Channel bllView = new EggsoftWX.BLL._031_Channel();
        string strBoard = "Board34ChannelChange.aspx";
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


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model._031_Channel Model = bllView.GetModel(Int32.Parse(ID));

                txtChannelName.Text = Model.ChannelName.ToString();
                TextBoxShortDesc.Text = Model.ChannelMemo.ToString();

                txtChannelName.ReadOnly = true;
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
                EggsoftWX.Model._031_Channel Model = bllView.GetModel(Int32.Parse(ID));




                Model.ChannelMemo = TextBoxShortDesc.Text;


                Model.UpdateTime = DateTime.Now;

                bllView.Update(Model);
                JsUtil.ShowMsg("修改成功!", strBoard);

            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.Model._031_Channel Model = new EggsoftWX.Model._031_Channel();


                Model.ChannelName = txtChannelName.Text;
                Model.ChannelMemo = TextBoxShortDesc.Text;
                Model.ShopClient_ID = strINCID.toInt32();
                Model.UpdateTime = DateTime.Now;
                bllView.Add(Model);
                JsUtil.ShowMsg("添加成功!", strBoard);
            }
        }
    }

}