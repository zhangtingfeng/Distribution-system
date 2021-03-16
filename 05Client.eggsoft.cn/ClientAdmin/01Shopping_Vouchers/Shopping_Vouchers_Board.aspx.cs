using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._01Shopping_Vouchers
{
    public partial class Shopping_Vouchers_Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme bll_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_Vouchers")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                string strwebuy8_ClientAdmin_Users = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 40;
                ViewState["RecordCount"] = bll_tab_Shopping_Vouchers.ExistsCount("ShopClientID=" + strwebuy8_ClientAdmin_Users + " ");
                BindAnnounce();
                ShowState();
                InitGoPage();

                //#region  是否启用优惠券及购物红包功能
                //EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                //EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                //Model_tab_ShopClient = bll_tab_ShopClient.GetModel(Int32.Parse(strwebuy8_ClientAdmin_Users));
                //CheckBox_Shopping_Vouchers.Checked = Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers);
                //#endregion

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
            gvAnnounce.DataSource = bll_tab_Shopping_Vouchers.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,Vouchers_Title,Money,AllCount,HowToGet,ValidateStartTime,ValidateEndTime,UpdateTime,LimitHowMany", "ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "ID", true);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[2].Text = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(e.Row.Cells[2].Text)) + "元"; ;

                if (e.Row.Cells[4].Text.toString() == "0")
                {
                    e.Row.Cells[4].Text = "不限制";
                }
                if (e.Row.Cells[7].Text.toString() == "1")
                {
                    e.Row.Cells[7].Text = "手动(线下)";
                }
                else
                {
                    e.Row.Cells[7].Text = "线上领取";
                }
                e.Row.Cells[8].Text = "<a href=\"Shopping_Vouchers_Manage.aspx?type=Modify&Scheme_ID=" + e.Row.Cells[0].Text + "\">修改</a>";

                #region 失效判断
                DateTime DateTimeShiXiaoTime = Convert.ToDateTime(e.Row.Cells[6].Text.toDateTime());
                if (DateTimeShiXiaoTime < DateTime.Now)
                {
                    e.Row.Cells[9].Text = "已失效";
                }
                else
                {
                    e.Row.Cells[9].Text = "<a href=\"Shopping_Vouchers_Manage.aspx?type=ShiXiao&Scheme_ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定失效吗?')\">失效</a>";
                }
                #endregion 失效判断

                //}
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
            Response.Redirect("Shopping_Vouchers_Manage.aspx?type=Add");
        }
        /// <summary>
        /// 是否启用优惠券及购物红包功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void CheckBox_Shopping_Vouchers_CheckedChanged(object sender, EventArgs e)
        //{
        //string strwebuy8_ClientAdmin_Users = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
        //EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
        //EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
        //Model_tab_ShopClient = bll_tab_ShopClient.GetModel(Int32.Parse(strwebuy8_ClientAdmin_Users));
        //Model_tab_ShopClient.Shopping_Vouchers = CheckBox_Shopping_Vouchers.Checked;
        //bll_tab_ShopClient.Update(Model_tab_ShopClient);
        //}
    }
}