using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _01Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";
        public string strTextBox_StartTime = "";
        public string strTextBox_EndTime = "";

        EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay bllb007_OperationReturnMoneyEveryDay = new EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay();
        protected void Page_Load(object sender, EventArgs e)
        {
           String str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_31ConsumptionCapital")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限
            btnOperationCenter.Visible = String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_31ConsumptionCapital"));///是否显示
            btnOperationCenterGoodAndReturn.Visible = btnOperationCenter.Visible;
            Button1FullEveryDay.Visible = btnOperationCenter.Visible;
            Button2BalanceofPaymentStatistics.Visible = btnOperationCenter.Visible;
            btnOperationCenterGoodAndReturn.Visible = btnOperationCenter.Visible;


            btnOperationCenter.Attributes.Add("onclick", "this.form.target='_blank'");
            btnOperationCenterGoodAndReturn.Attributes.Add("onclick", "this.form.target='_blank'");
            Button1FullEveryDay.Attributes.Add("onclick", "this.form.target='_blank'");
            Button2BalanceofPaymentStatistics.Attributes.Add("onclick", "this.form.target='_blank'");
            ButtonOperationsCenterMembershipStatistics.Attributes.Add("onclick", "this.form.target='_blank'");
            Button112OrderDetailEveryDay.Attributes.Add("onclick", "this.form.target='_blank'");
            //Button113OperationCenter_OrderManage.Attributes.Add("onclick", "this.form.target='_blank'");
            Button14WealthMoneyControlOperationCenter.Attributes.Add("onclick", "this.form.target='_blank'");


            #region 读取未处理信息
            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
            int intInfoAlertMessageExistsCount = bll_b011_InfoAlertMessage.ExistsCount("ShopClient_ID=@ShopClient_ID and Type='b010_AskModifyParent' and Done=0", str_Pub_ShopClientID.toInt32());
            Label1intInfoAlertMessageExistsCount.Text = intInfoAlertMessageExistsCount.ToString().toInt32().toString();
            #endregion 读取未处理信息
        }



        protected void btnbtnOperationCenter_Click(object sender, EventArgs e)
        {
            //Page.RegisterStartupScript("<script>window.open("","","")</script>")
            Response.Redirect("02OperationCenter.aspx");
        }

        protected void btnOperationCenterGoodAndReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("04OperationGoods.aspx");
        }

        protected void btn08FullEveryDay_Click(object sender, EventArgs e)
        {
            Response.Redirect("08FullEveryDay.aspx");
        }

        protected void btnbtnButtonOperationsCenterMembershipStatistics_Click(object sender, EventArgs e)
        {
            Response.Redirect("09OperationUserStatus.aspx");
        }

        protected void Button2BalanceofPaymentStatistics_Click(object sender, EventArgs e)
        {
            Response.Redirect("10BalanceofPaymentStatistics.aspx");
        }

        protected void Button112OrderDetailEveryDay_Click(object sender, EventArgs e)
        {
            Response.Redirect("12OrderDetailEveryDay.aspx");
        }

        //protected void btnbtn13OperationCenter_OrderManage_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("13OperationCenter_OrderManage.aspx?type=add&CallBackUrl=" + Server.UrlEncode("01Board.aspx"));
        //}
        protected void Button14WealthMoneyControlOperationCenter_Click(object sender, EventArgs e)
        {
            Response.Redirect("14WealthMoneyControlOperationCenter.aspx");
        }

        protected void ButtonButton16_16CheckModifyParent_Click(object sender, EventArgs e)
        {
            Response.Redirect("16CheckModifyParent.aspx");
        }
    }
}
