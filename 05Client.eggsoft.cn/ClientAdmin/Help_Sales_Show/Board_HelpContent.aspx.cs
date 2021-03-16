using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin.Help_Sales_Show
{
    public partial class Board_HelpContent : System.Web.UI.Page
    {
        string strHelp_Class1_ID = "";

        public string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

        EggsoftWX.BLL.Help_Content bll = new EggsoftWX.BLL.Help_Content();
        protected void Page_Load(object sender, EventArgs e)
        {
            strHelp_Class1_ID = Request.QueryString["Help_Class1_ID"];

            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = bll.ExistsCount("Help_Class1_ID=" + strHelp_Class1_ID + "");
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

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("Help_Content.Help_Class1_ID", "asc");//第一排序字段  
            sql.addOrderField("Help_Content.sort", "asc");//第一排序字段  
            sql.addOrderField("Help_Content.id", "asc");//第二排序字段  
            sql.table = "Help_Content";
            sql.outfields = "ID,Help_Class1_ID,Name,Sort,UpdateTime";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());

            String strWhere = "Help_Class1_ID='" + strHelp_Class1_ID + "'";
            sql.where = strWhere;

            string strSql = sql.getSQL(bll.ExistsCount(strWhere));
            gvAnnounce.DataSource = bll.SelectList(strSql);

            gvAnnounce.DataBind();
        }



        private string getClassName(string strID)
        {
            EggsoftWX.BLL.Help_Class1 bll = new EggsoftWX.BLL.Help_Class1();
            EggsoftWX.Model.Help_Class1 Model = new EggsoftWX.Model.Help_Class1();
            Model = bll.GetModel(Int32.Parse(strID));


            string strClassName = Model.ClassName;
            return strClassName;

        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {




                string strClassID = e.Row.Cells[2].Text;

                string strID = e.Row.Cells[0].Text;

                e.Row.Cells[1].Text = "<a href='" + strUpLoadURL + "/UpLoadFile/ShowContent_Web.aspx?Contentid=" + strID + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";

                e.Row.Cells[2].Text = getClassName(strClassID);


                e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce'");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t");
                e.Row.Attributes.Add("onclick", "if(this.style.backgroundColor=='#ebebce')this.style.backgroundColor=t;else{this.style.backgroundColor='#ebebce'}");
                e.Row.Attributes.CssStyle.Add("cursor", "hand");

                e.Row.Attributes.Add("onclick", "javascript:window.location.href='" + strUpLoadURL + "/UpLoadFile/ShowContent_Web.aspx?Contentid=" + strID + "';");

            }
        }

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
    }
}