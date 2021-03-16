using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft.Common;
using Eggsoft_Public_CL;

namespace _05ClientEggsoftCn.ClientAdmin._28Member
{
    public partial class Board_28Member : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strManageURL = "Manage_28Member.aspx";


        EggsoftWX.BLL.tab_ShopClient_MemberCard blltab_ShopClient_MemberCard = new EggsoftWX.BLL.tab_ShopClient_MemberCard();
        protected void Page_Load(object sender, EventArgs e)
        { 
            #region 没有打开的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board28Member")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开的权限

            btnManagerHowAddMoney.Visible = String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board28Member_MemberBonus"));

            if (!IsPostBack)
            {


                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;


                ViewState["RecordCount"] = blltab_ShopClient_MemberCard.ExistsCount(gerWhere());
                BindAnnounce();
                ShowState();
                InitGoPage();
            }
        }

        private string gerWhere()
        {
            Int32? int32ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toInt32();
            string strWhere = "";
            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
            if (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" || strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_AdminShopClient")
            {
                strWhere = "ShopClientID=" + int32ShopClientID + " and isnull(IsDeleted,0)=0";
            }
            else
            {
                strWhere = "ShopClientID=" + int32ShopClientID + " and isnull(IsDeleted,0)=0 and UpdateBy in (" + Pub_FenXiao.getAllMySonAdminPowerList(strwebuy8_ClientAdmin_Users_ClientUserAccount, int32ShopClientID) + ")";
            }
            Eggsoft.Common.debug_Log.Call_WriteLog(strWhere, "查看会员卡", "SQL");
            return strWhere;
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


            gvAnnounce.DataSource = blltab_ShopClient_MemberCard.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,[InputMoney],[BonusMoney],[BonusGouWuQuan],[PhoneNum],[IfChangToWeiXinBonus],BonusDesc,BankSeraillnum,CreateBy,UpdateBy", " and isnull(IsDeleted,0)=0 and " + gerWhere(), "ID", true);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text != "1")///没有转化的 删除的才可以
                {
                    e.Row.Cells[10].Text = "<a href=\"" + strManageURL + "?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
                }
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
            Response.Redirect("" + strManageURL + "?type=Add");
        }

        protected void btnAdd_ClickManagerHowAddMoney(object sender, EventArgs e)
        {
            Response.Redirect("Board_28MemberBonus.aspx");
        }
    }
}