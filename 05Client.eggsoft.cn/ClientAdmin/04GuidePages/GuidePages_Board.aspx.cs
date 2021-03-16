using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._04GuidePages
{
    public partial class GuidePages_Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        //string str_Pub_ClientApp = ConfigurationManager.AppSettings["ClientApp"];
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

        EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Eggsoft.Common.Session.Read("INCID").ToString() == "") Eggsoft.Common.Session.Add("INCID", "10002");
            //if (Eggsoft.Common.Session.Read("INCUploadpath").ToString() == "") Eggsoft.Common.Session.Add("INCUploadpath", "010002_sh");
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_GuidePages")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

            if (!IsPostBack)
            {


                //ViewState["PageIndex"] = 1;
                //ViewState["PageSize"] = 30;
                //ViewState["RecordCount"] = bll.ExistsCount("");
                BindAnnounce();
                //ShowState();
                //InitGoPage();
            }
        }

        //private void InitGoPage()
        //{
        //    ddlGoPage.Items.Clear();
        //    for (int i = 1; i <= GetPageCount(); i++)
        //    {
        //        ddlGoPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
        //    }
        //    ddlGoPage.SelectedValue = ViewState["PageIndex"].ToString();
        //}    string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


        private void BindAnnounce()
        {
            string strCondition = " and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " and parentID=0 and isnull(IsDeleted,0)=0 order by MenuPos asc,id desc";///只有oliver 才能看到所有记录


            gvMenu.DataSource = bll.GetDataTable("1000", "ID,MenuName,LinkOrText,MenuIcon,MenuLink", strCondition);


            gvMenu.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(strShopClient_ID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                string strURL = "https://" + strErJiYuMing + "/guidepage-" + e.Row.Cells[0].Text + ".aspx";

                //e.Row.Cells[1].Text = "<a target=\"_blank\" href=\"" + strURL + "\"><font color=blue>" + e.Row.Cells[1].Text + "</font></a>";

                e.Row.Cells[2].Text = "<a href=\"" + strURL + ".aspx\"><img width=\"100px;\"  src=\"" + strUpLoadURL + e.Row.Cells[2].Text + "\"></a>";

                if (e.Row.Cells[3].Text == "True")
                {
                    e.Row.Cells[3].Text = "链接";
                }
                else
                {
                    e.Row.Cells[3].Text = "内容";
                }

                string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

                string strModify = strNavigateUrl + Server.UrlEncode("GuidePages/GuidePages__Manage.aspx?type=Modify&ID=" + e.Row.Cells[0].Text);
                string strDelete = strNavigateUrl + Server.UrlEncode("GuidePages/GuidePages__Delete.aspx?type=Delete&ID=" + e.Row.Cells[0].Text);

                e.Row.Cells[5].Text = "<a href=\"" + strModify + "\">修改</a>";
                e.Row.Cells[6].Text = "<a href=\"" + strDelete + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

                string str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClient_ID)) + "/QRCodeImage/";
                e.Row.Cells[4].Text = strURL;
                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strURL, str_Pub_upLoadpath,"");

                e.Row.Cells[7].Text = "<a href=\"" + strURL + "\"><img width=\"100px;\" height=\"100px;\" src=\"" + strImageUrl + "\"></a>";


            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

            string strAdd = strNavigateUrl + Server.UrlEncode("GuidePages/GuidePages__Manage.aspx?type=Add&ParentID=0");
            Response.Redirect(strAdd);
        }
    }
}