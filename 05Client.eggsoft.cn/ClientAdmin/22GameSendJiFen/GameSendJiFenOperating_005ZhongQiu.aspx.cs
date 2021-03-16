using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._22GameSendJiFen
{
    public partial class GameSendJiFenOperating_005ZhongQiu : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String DisPlayStatus_New_None = "";

        protected string strTextBox_EndTime = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string strVouchersNum = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strVouchersNum))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_Game bll = new EggsoftWX.BLL.tab_ShopClient_Game();
                    bll.Delete("ID='" + strVouchersNum + "'");


                    JsUtil.ShowMsg("删除成功!", "GameSendJiFenBoard.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }

        private void SetClass()
        {
            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_Game bll = new EggsoftWX.BLL.tab_ShopClient_Game();
                EggsoftWX.Model.tab_ShopClient_Game Model = bll.GetModel("ID=" + strID + "");
                lblID.Text = Model.ID.ToString();
                TextBox_Name.Text = Model.GameName;
                RadioButtonList_SendType.SelectedValue = Model.SendType.ToString();
                txtSendMoney.Text = Model.HowManyMoney.ToString();
                strTextBox_EndTime = Model.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                btnAdd.Text = "保 存";
                DisPlayStatus_New_None = "";
            }
            else
            {
                DisPlayStatus_New_None = "display:none;";
                strTextBox_EndTime = DateTime.Now.AddMonths(2).ToString("yyyy-MM-dd HH:mm:ss");
            }





        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_Game bll = new EggsoftWX.BLL.tab_ShopClient_Game();
                EggsoftWX.Model.tab_ShopClient_Game Model = bll.GetModel("ID=" + strID + "");

                Model.ID = Convert.ToInt32(lblID.Text);
                Model.GameName = TextBox_Name.Text;
                Model.SendType = Int32.Parse(RadioButtonList_SendType.SelectedValue);
                Model.HowManyMoney = Convert.ToDecimal(txtSendMoney.Text);

                string strTextBox_EndTime = Request.Form["TextBox_EndTime"];
                if (string.IsNullOrEmpty(strTextBox_EndTime) == false)
                {
                    DateTime my_TextBox_EndTime = DateTime.ParseExact(strTextBox_EndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    Model.EndTime = my_TextBox_EndTime;
                }
                Model.UpdateTime = DateTime.Now;

                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "GameSendJiFenBoard.aspx");
            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_ShopClient_Game bll = new EggsoftWX.BLL.tab_ShopClient_Game();
                    EggsoftWX.Model.tab_ShopClient_Game Model = new EggsoftWX.Model.tab_ShopClient_Game();
                    Model.ShopClientID = Convert.ToInt32(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users"));
                    Model.GameName = TextBox_Name.Text;
                    Model.FromName = "005ZhongQiu";
                    Model.SendType = Int32.Parse(RadioButtonList_SendType.SelectedValue);
                    Model.HowManyMoney = Convert.ToDecimal(txtSendMoney.Text);
                    string strTextBox_EndTime = Request.Form["TextBox_EndTime"];
                    if (string.IsNullOrEmpty(strTextBox_EndTime) == false)
                    {
                        DateTime my_TextBox_EndTime = DateTime.ParseExact(strTextBox_EndTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        Model.EndTime = my_TextBox_EndTime;
                    }
                    Model.UpdateTime = DateTime.Now;

                    bll.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "GameSendJiFenBoard.aspx");
                }
        }
    }
}