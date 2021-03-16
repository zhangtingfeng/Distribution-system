using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.LightApp
{
    public partial class LightApp_Boad : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_LightApp bll_tab_LightApp = new EggsoftWX.BLL.tab_ShopClient_LightApp();
        protected void Page_Load(object sender, EventArgs e)
        { 
            #region 没有打开的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_LightApp")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开的权限
            if (!IsPostBack)
            {
                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                string strCondition = " ShopClientID=" + str_Pub_ShopClientID + "";///只有oliver 才能看到所有记录
                ViewState["RecordCount"] = bll_tab_LightApp.ExistsCount(strCondition);
                BindAnnounce();
                ShowState();
                InitGoPage();
            }
        }

        private void InitGoPage()
        {
            ddlGoPage.Items.Clear();
            for (int i = 1; i <= GetPageCount(); i++)
            {
                ddlGoPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlGoPage.SelectedValue = ViewState["PageIndex"].ToString();
        }

        private void BindAnnounce()
        {
            string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strCondition = " ShopClientID=" + str_Pub_ShopClientID + "";///


            gvAnnounce.DataSource = bll_tab_LightApp.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,Title", strCondition, "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                string strURL = "https://" + strErJiYuMing + "/huodong/lightappcn/d-" + e.Row.Cells[0].Text + ".aspx";
                string str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                string strImageUrl_ = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strURL, str_Pub_upLoadpath, "");

                //string strURL = Eggsoft.Common.Application.getwebHttp() + "/LightAppCN/D-" + Eggsoft.Common.Session.Read("INCID").ToString() + "-" + e.Row.Cells[0].Text + ".aspx";
                //string strAPPCODE_get_INC_Upload = Pub.APPCODE_get_INC_QRImages_Upload();
                e.Row.Cells[1].Text = "<a target=\"_blank\" href=\"" + strURL + "\"><font color=blue>" + e.Row.Cells[1].Text + "</font></a>";

                e.Row.Cells[2].Text = "<a target=\"_blank\" href=\"" + strURL + "\"><font color=blue>" + strURL + "</font></a>"; ;
                e.Row.Cells[3].Text = "<a href=\"LightApp_Manage.aspx?Type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[4].Text = "<a href=\"LightApp_Manage.aspx?Type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
                e.Row.Cells[5].Text = "<a href=\"LightApp_EachPage_Boad.aspx?LightApp_Boad_ID=" + e.Row.Cells[0].Text + "\">管理子页面</a>";

                e.Row.Cells[6].Text = "<a href=\"" + strURL + "\"><img style=\"width:100px;height:100px;\" src=\"" + strImageUrl_ + "\"></a>";

            }
        }

        ////为动态创建的按钮事件写一个方法 
        //protected void bt_Click(object sender, EventArgs e)
        //{
        //    ((Button)sender).BackColor = System.Drawing.Color.Red;
        //    Eggsoft.Common.JsUtil.ShowMsg("dd");
        //}

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            BindAnnounce();
            ShowState();
            InitGoPage();
        }

        protected void lbtnPrev_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(ViewState["PageIndex"].ToString()) > 1)
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].ToString()) - 1;
            }
            else
            {
                ViewState["PageIndex"] = GetPageCount();
            }
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(ViewState["PageIndex"].ToString()) < GetPageCount())
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].ToString()) + 1;
            }
            else
            {
                ViewState["PageIndex"] = 1;
            }
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = GetPageCount();
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void ddlGoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = Int32.Parse(ddlGoPage.SelectedValue);
            BindAnnounce();
            ShowState();
        }

        private void ShowState()
        {
            lblMsg.Text = "当前页:" + ViewState["PageIndex"].ToString() + "/" + GetPageCount().ToString() + " 每页:" + ViewState["PageSize"].ToString() + "条 共:" + ViewState["RecordCount"].ToString() + "条";
            if (GetPageCount() <= 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
                lbtnNext.Enabled = false;
                lbtnLast.Enabled = false;
            }
            else
            {
                if (Int32.Parse(ViewState["PageIndex"].ToString()) <= 1)
                {
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    lbtnNext.Enabled = true;
                    lbtnLast.Enabled = true;
                }
                else
                {
                    if (Int32.Parse(ViewState["PageIndex"].ToString()) >= GetPageCount())
                    {
                        lbtnFirst.Enabled = true;
                        lbtnPrev.Enabled = true;
                        lbtnNext.Enabled = false;
                        lbtnLast.Enabled = false;
                    }
                    else
                    {
                        lbtnFirst.Enabled = true;
                        lbtnPrev.Enabled = true;
                        lbtnNext.Enabled = true;
                        lbtnLast.Enabled = true;
                    }
                }
            }
        }

        private int GetPageCount()
        {
            int pageCount = Int32.Parse(ViewState["RecordCount"].ToString()) % Int32.Parse(ViewState["PageSize"].ToString()) == 0 ? (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString())) : (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString()) + 1);
            return pageCount;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("LightApp_Manage.aspx?Type=Add");
        }
    }
}