using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._02GuWuQuanChange
{
    public partial class IS_UserChangeMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.View_GouWuQuan2XianJInEtcUser bll_View_GouWuQuan2XianJInEtcUser = new EggsoftWX.BLL.View_GouWuQuan2XianJInEtcUser();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                InitGoPageReadData();
            }
        }


        private void InitGoPageReadData()
        {

            ViewState["PageIndex"] = 1;
            ViewState["PageSize"] = 5;

            string strID_GouWuQuan2XianJInEtc = Request.QueryString["ID_GouWuQuan2XianJInEtc"];
            string strtype = Request.QueryString["type"];
            if (strtype == "ispassedCount")
            {
                CheckBox_IIS_Admin.Checked = true;
            }
            else if (strtype == "isnotpassedCount")
            {
                CheckBox_IIS_Admin.Checked = false;
            }

            EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc bll_tab_GouWuQuan2XianJInEtc = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();
            EggsoftWX.Model.tab_GouWuQuan2XianJInEtc Model_tab_GouWuQuan2XianJInEtc = bll_tab_GouWuQuan2XianJInEtc.GetModel(Int32.Parse(strID_GouWuQuan2XianJInEtc));

            LiteralTitle.Text = Model_tab_GouWuQuan2XianJInEtc.ShortDesc;
            Literal_Desc.Text = (Model_tab_GouWuQuan2XianJInEtc.ChangeAuto == "Hand" ? "手动处理：" : "自动处理：") + Model_tab_GouWuQuan2XianJInEtc.ShortDesc;

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            String strWhere = "ID_GouWuQuan2XianJInEtc=" + strID_GouWuQuan2XianJInEtc + " and  ShopClientID=" + strShopClientID + " and ISDeleteed=0 and ";

            if (strtype == "ispassedCount")
            {
                ViewState["RecordCount"] = bll_View_GouWuQuan2XianJInEtcUser.ExistsCount(strWhere + "ISpassed=1");
            }
            else if (strtype == "isnotpassedCount")
            {
                ViewState["RecordCount"] = bll_View_GouWuQuan2XianJInEtcUser.ExistsCount(strWhere + "ISpassed=0");
            }



            BindAnnounce();
            ShowState();
            InitGoPage();

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
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("View_GouWuQuan2XianJInEtcUser.ID", "desc");//第一排序字段  
            sql.table = "View_GouWuQuan2XianJInEtcUser";
            sql.outfields = "ID,ShopUserID,(NickName+UserRealName+ContactMan) AS UserName,ContactPhone,UserGouWuQuan,RemainingSum_Vouchers,RemainingSum,CreateTime,ISpassed";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());

            string strID_GouWuQuan2XianJInEtc = Request.QueryString["ID_GouWuQuan2XianJInEtc"];

            String strWhere = " ID_GouWuQuan2XianJInEtc=" + strID_GouWuQuan2XianJInEtc + " and ShopClientID=" + strShopClientID + " and ISDeleteed=0 and ";

            string strtype = Request.QueryString["type"];
            if (strtype == "ispassedCount")
            {
                strWhere += "ISpassed=1 ";
            }
            else if (strtype == "isnotpassedCount")
            {
                strWhere += "ISpassed=0 ";
            }



            sql.where = strWhere;

            //Query()
            int intRecordCount = bll_View_GouWuQuan2XianJInEtcUser.ExistsCount(strWhere);
            string strSql = sql.getSQL(intRecordCount);
            gvAnnounce.DataSource = bll_View_GouWuQuan2XianJInEtcUser.SelectList(strSql);

            gvAnnounce.DataBind();
        }



        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[0].Text;




                if (e.Row.Cells[9].Text == "1")
                {
                    e.Row.Cells[9].Text = "已处理";
                    e.Row.Cells[8].Text = "已处理";
                }
                else
                {
                    e.Row.Cells[8].Text = "待处理";
                    e.Row.Cells[9].Text = "<span id=\"IS_Admin_check_AsyncStatus" + strID + "\"><a href=\"#\" style=\"color:blue\" onclick=\"IS_Admin_check_Async_ShowFuction(" + strID + ",1);\">已经处理的猛击这里</a></span>";
                }
                e.Row.Cells[10].Text = "<a href=\"IS_UserChangeMoney_Manage.aspx?type=Delete&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=IS_UserChangeMoney.aspx\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

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

        protected void CheckBox_IIS_Admin_CheckedChanged(object sender, EventArgs e)
        {
            // type=ispassedCount&ID_GouWuQuan2XianJInEtc=12
            string strID_GouWuQuan2XianJInEtc = Request.QueryString["ID_GouWuQuan2XianJInEtc"];

            if (CheckBox_IIS_Admin.Checked)
            {
                Response.Redirect("IS_UserChangeMoney.aspx?type=ispassedCount&ID_GouWuQuan2XianJInEtc=" + strID_GouWuQuan2XianJInEtc);
            }
            else
            {
                Response.Redirect("IS_UserChangeMoney.aspx?type=isnotpassedCount&ID_GouWuQuan2XianJInEtc=" + strID_GouWuQuan2XianJInEtc);

            }
            ///InitGoPageReadData();
        }
    }
}