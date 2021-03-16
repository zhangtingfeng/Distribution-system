using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._15Advance
{
    public partial class MultiFenXiaoLevel_Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
        string str_Pub_ShopClientID = "";
        //string str_Pub_WapApp = ConfigurationManager.AppSettings["WapApp"];
        //string str_Pub_upLoadpath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ShopAdvance_FenXiaoLevel")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

            if (!IsPostBack)
            {
                str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;

                int intExistsCount = bll.ExistsCount("ShopClient_ID=" + str_Pub_ShopClientID+ " and IsDeleted=0");
                ViewState["RecordCount"] = intExistsCount;
                //if (intExistsCount > 0)
                //{
                //    ViewState["allPercentCount"] = bll.SelectList("select sum(LevelPercent) as allPercent from tab_ShopClient_FenXiaoLevel where ShopClient_ID=" + str_Pub_ShopClientID).Tables[0].Rows[0][0].ToString();
                //    Label_Show.Text = "当前使用是的" + intExistsCount + "级代理分销制度";
                //}
                //else
                //{
                //    Label_Show.Text = "当前使用是平台默认的2级代理分销制度，默认按照1：3结算，即每件商品的代理商利润的25%结算给一级，代理商利润的75%结算给二级";
                //}

                BindAnnounce();
                ShowState();
                InitGoPage();


                //if (Int32.Parse(ViewState["RecordCount"].ToString())<2)    btnAdd.Visible=true;

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
            sql.addOrderField("b019_tab_ShopClient_MultiFenXiaoLevel.sort", "asc");//第一排序字段  
            sql.addOrderField("b019_tab_ShopClient_MultiFenXiaoLevel.id", "asc");//第二排序字段  
            sql.table = "b019_tab_ShopClient_MultiFenXiaoLevel";
            sql.outfields = "[ID]      ,[ShopClient_ID]      ,[Name]      ,[Sort]      ,[UpdateTime] ";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = "ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " and IsDeleted=0";
            sql.where = strwhere;
            string strSql = sql.getSQL(bll.ExistsCount(strwhere));
            gvAnnounce.DataSource = bll.SelectList(strSql);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[0].Text;

                //string stringChnNames = "零一二三四五六七八九";
                //e.Row.Cells[1].Text = stringChnNames[e.Row.RowIndex + 1] + "级分销";

                //e.Row.Cells[3].Text = e.Row.Cells[3].Text + "(" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(e.Row.Cells[3].Text) * 100 / Decimal.Parse(ViewState["allPercentCount"].ToString())) + "%)";


                e.Row.Cells[3].Text = "<a href=\"MultiFenXiaoLevel_Manage.aspx?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[4].Text = "<a href=\"MultiFenXiaoLevel_Manage.aspx?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //int intCount = Int32.Parse(ViewState["RecordCount"].ToString());
            //if (intCount >= 3)
            //{
            //    Eggsoft.Common.JsUtil.ShowMsg("本平台最多3级", -1);
            //}
            //else
            //{
            Response.Redirect("MultiFenXiaoLevel_Manage.aspx?type=Add");
            //}
        }
        protected void btnbtnResetAllGood_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ResetAllGoodOneKey.aspx");
        }

        protected void btnResetAllGood_Click(object sender, EventArgs e)
        {
           // Response.Redirect("ResetAllGoodOneKey.aspx");
        }
    }
}