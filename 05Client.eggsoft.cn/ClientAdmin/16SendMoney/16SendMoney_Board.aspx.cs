using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._16SendMoney
{
    public partial class _16SendMoney_Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers bll_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
        EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag bll_tab_ShopClient_SendMoneyByRedBag = new EggsoftWX.BLL.tab_ShopClient_SendMoneyByRedBag();
        protected void Page_Load(object sender, EventArgs e)
        { 
            #region 没有打开的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_16SendMoney")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开的权限
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 40;
                ViewState["RecordCount"] = bll_tab_ShopClient_SendMoneyByRedBag.ExistsCount("ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " ");
                BindAnnounce();
                ShowState();
                InitGoPage();

                Eggsoft_Public_CL.Pub_FenXiao.FileUploadRedJPGCheck(Int32.Parse(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString()));

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


            gvAnnounce.DataSource = bll_tab_ShopClient_SendMoneyByRedBag.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "[ID],[ShopClient_ID],[UpdateTime],ValidStartTime,[SendedStatus],[SendToType],[MsgTypeNewsTitle],[MsgTypeNewsDescription]", " and ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers Model_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers();
                Model_tab_RedWallet_Money_Credits_Vouchers = bll_tab_RedWallet_Money_Credits_Vouchers.GetModel("SendMoneyByRedBagID=" + e.Row.Cells[0].Text);
                e.Row.Cells[3].Text = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_RedWallet_Money_Credits_Vouchers.Money.toDecimal());
                e.Row.Cells[4].Text = Model_tab_RedWallet_Money_Credits_Vouchers.HowmanyPeople.ToString();

                DataTable mydddd = bll_tab_RedWallet_Money_Credits_Vouchers.SelectList("select count(*) as countNum,sum(Money) as MoneyGet from tab_RedWallet_Money_Credits_Vouchers where PID=" + Model_tab_RedWallet_Money_Credits_Vouchers.ID).Tables[0];
                string countNum = mydddd.Rows[0]["countNum"].ToString();
                string countMoney = mydddd.Rows[0]["MoneyGet"].ToString();
                e.Row.Cells[7].Text = countNum;
                e.Row.Cells[8].Text = countMoney;

                CheckBox cb = e.Row.Cells[6].Controls[0] as CheckBox;
                //  string strboolSend = e.Row.Cells[6].ToString();
                bool boolSend = cb.Checked;
                //bool.TryParse(strboolSend, out boolSend);

                e.Row.Cells[10].Text = boolSend == false ? "<a href=\"Net16SendMoney.aspx?type=Modify&SendBagID=" + e.Row.Cells[0].Text + "&CallBackUrl=16SendMoney_Board.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">修改</a>" : "";
                e.Row.Cells[10].Text += "&nbsp; &nbsp; <a href=\"Net16SendMoney.aspx?type=ModifyCopy&SendBagID=" + e.Row.Cells[0].Text + "&CallBackUrl=16SendMoney_Board.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">复制</a>";

                e.Row.Cells[11].Text = boolSend == false ? "<a href=\"Net16SendMoney.aspx?type=Delete&SendBagID=" + e.Row.Cells[0].Text + "&CallBackUrl=16SendMoney_Board.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>" : "";

                //e.Row.Cells[2].Text = Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(e.Row.Cells[2].Text)) + "元"; ;

                //bool boolConsumed = false;
                //bool.TryParse(e.Row.Cells[6].Text, out boolConsumed);

                //e.Row.Cells[7].Text = Eggsoft_Public_CL.Pub.GetNickName(e.Row.Cells[7].Text);


                //if (boolConsumed)
                //{
                //    e.Row.Cells[6].Text = "已消费";
                //}
                //else
                //{

                //    e.Row.Cells[6].Text = "未消费";
                //    e.Row.Cells[8].Text = "<a href=\"Shopping_Vouchers_Manage.aspx?type=Modify&VouchersNum=" + e.Row.Cells[0].Text + "\">修改</a>";
                //    e.Row.Cells[9].Text = "<a href=\"Shopping_Vouchers_Manage.aspx?type=Delete&VouchersNum=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
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
            Response.Redirect("Net16SendMoney.aspx?type=Add&CallBackUrl=16SendMoney_Board.aspx*PageIndex=1");
        }
    }
}