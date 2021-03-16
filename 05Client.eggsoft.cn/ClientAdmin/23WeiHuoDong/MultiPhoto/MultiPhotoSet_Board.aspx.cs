using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.MultiPhoto
{
    public partial class MultiPhotoSet_Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto bll = new EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = bll.ExistsCount("");
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
            string strCondition = " INC_User_ID=" + Eggsoft.Common.Session.Read("INCID").ToString() + "";///只有oliver 才能看到所有记录


            gvAnnounce.DataSource = bll.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,Title", strCondition, "ID", true);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = "<a target=\"_blank\" href=\"/05MutliPhoto-" + Eggsoft.Common.Session.Read("INCID").ToString() + "-" + e.Row.Cells[0].Text + ".aspx\"><font color=blue>" + e.Row.Cells[1].Text + "</font></a>";


                e.Row.Cells[2].Text = "<a target=\"_blank\" href=\"/05MutliPhoto-" + Eggsoft.Common.Session.Read("INCID").ToString() + "-" + e.Row.Cells[0].Text + ".aspx\"><font color=blue>" + "/05MutliPhoto-" + Eggsoft.Common.Session.Read("INCID").ToString() + "-" + e.Row.Cells[0].Text + ".aspx" + "</font></a>"; ;
                e.Row.Cells[3].Text = "<a href=\"MultiPhotoSet_Manage.aspx?Type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[4].Text = "<a href=\"MultiPhotoSet_Manage.aspx?Type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

                string strURL = Eggsoft.Common.Application.getwebHttp() + "/05MutliPhoto-" + Eggsoft.Common.Session.Read("INCID").ToString() + "-" + e.Row.Cells[0].Text + ".aspx";
                //string strAPPCODE_get_INC_Upload = Pub.APPCODE_get_INC_QRImages_Upload();

                //e.Row.Cells[5].Text = "<a href=\"/05MutliPhoto-" + Eggsoft.Common.Session.Read("INCID").ToString() + "-" + e.Row.Cells[0].Text + ".aspx\"><img width=\"100px;\" src=\"" + Eggsoft.Common.Image.creatQRCodeImage(strURL, strAPPCODE_get_INC_Upload) + "\"></a>";

                //e.Row.Cells[6].Text = "<a href=\"Default.aspx?TypeManageID=" + e.Row.Cells[0].Text + "\">管理</a>";

                //Button bt = new Button();
                //bt.Text = string.Format("  x={0},y={1}  ", 0, 0);
                //bt.ID = "txtTest"; 
                //bt.Click += new EventHandler(bt_Click);
                //e.Row.Cells[6].Controls.Add(bt);
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
            Response.Redirect("MultiPhotoSet_Manage.aspx?Type=Add");
        }
    }
}