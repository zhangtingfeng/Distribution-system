using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver.Admin.tab_ShopClient
{
    public partial class UserManage : Eggsoft.Common.DotAdminPage__Admin//System.Web.UI.Page
    {
        EggsoftWX.BLL.View_Amin_ShopClient_Order BLL_View_Amin_ShopClient_Order = new EggsoftWX.BLL.View_Amin_ShopClient_Order();
        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckQuery();
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = GetRecordCount();
                BindUser();
                ShowState();
                InitGoPage();
                btnDeleteAll.Attributes.Add("onclick", "return confirm('确认删除吗，删除后不可恢复!')");
                DoAct();
            }
        }


        private void DoAct()
        {
            if (Request.QueryString["Act"] != null)
            {
                string act = Request.QueryString["Act"].ToString();
                if (act == "lock")
                {
                    int ID = Int32.Parse(Request.QueryString["ID"].ToString());
                    {
                        BLL_tab_ShopClient.Update("IsLocked=1", "ID=" + ID);
                        JsUtil.ShowMsg("锁定成功!", "?");
                    }
                }
                if (act == "unlock")
                {
                    int ID = Int32.Parse(Request.QueryString["ID"].ToString());
                    {
                        BLL_tab_ShopClient.Update("IsLocked=0", "ID=" + ID);
                        JsUtil.ShowMsg("解锁成功!", "?");
                    }
                }
                if (act == "gotouser")
                {
                    string UserID = Request.QueryString["UserID"].ToString();
                    {
                        Session["DotBBS_User"] = UserID;
                        Response.Write("<script>window.location='../ClientAdmin/default.aspx';</script>");
                    }
                }
                if (act == "delete")
                {
                    int UserID = Convert.ToInt32(Request.QueryString["UserID"].ToString());
                    EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = BLL_tab_ShopClient.GetModel(UserID);
                    Eggsoft.Common.FileFolder.DeleteFile(tab_ShopClient_Model.ShopButton);
                    BLL_tab_ShopClient.Delete(UserID);
                    JsUtil.ShowMsg("删除成功!");
                }
            }
        }

        private void CheckQuery()
        {
            if (Request.QueryString["keyWord"] != null)
            {
                ViewState["keyWord"] = Request.QueryString["keyWord"].ToString();
                ViewState["type"] = Request.QueryString["t"].ToString();
            }
            if (Request.QueryString["pdate"] != null)
            {
                ViewState["pdate"] = Request.QueryString["pdate"].ToString();
            }
        }

        private int GetRecordCount()
        {
            return BLL_View_Amin_ShopClient_Order.ExistsCount(GetConditions());
            //string conditions = GetConditions();
            //object obj = new UserBll().Scalar("count(0)", conditions);
            //if (obj != null)
            //{
            //    return Int32.Parse(obj.ToString());
            //}
            //else
            //{
            //    return 0;
            //}
        }

        private string GetConditions()
        {
            return "1=1";
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

        private void BindUser()
        {
            string conditions = GetConditions();

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField("View_Amin_ShopClient_Order.id", "asc");//第二排序字段  
            sql.table = "View_Amin_ShopClient_Order";
            string sql_outfields = "[ID]";
            sql_outfields += ",[ShopClientName]";
            sql_outfields += ",[ShopClientType]";
            sql_outfields += ",[Username]";
            sql_outfields += ",[AllNotDelivery_In7Days]";
            sql_outfields += ",[AllOrder_In7Days]";
            sql_outfields += ",[ContactMan]";
            sql_outfields += ",[ErJiYuMing]";
            sql_outfields += ",[AllPayedOrder_In7Days]";
            sql_outfields += ",[AllPayedMoney_In7Days]";
            sql_outfields += ",[PayedAverageMoney_In7Days]";
            sql_outfields += ",[AllNotDelivery]";
            sql_outfields += ",[sumTotalMoney]";
            sql.outfields = sql_outfields;

            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            sql.where = conditions;

            //string strSql = "select top 20 * from tab_Goods_Class where UserID=12 order by tab_Goods_Class.sort asc,tab_Goods_Class.id asc";
            string strSql = sql.getSQL(BLL_View_Amin_ShopClient_Order.ExistsCount(conditions));

            // gvAnnounce.DataSource = bll.SelectList(strSql);
            gvUser.DataSource = BLL_View_Amin_ShopClient_Order.SelectList(strSql);

            //        gvUser.DataSource = new EggsoftWX.BLL.tab_ShopClient().GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,UserName,UserRealName,UpdateTime,IFCompany,AuthorTime", conditions, "ID", true);
            gvUser.DataBind();
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string UserID = e.Row.Cells[0].Text;
                //e.Row.Cells[2].Text = "<a href=\"/http://apps.eggsoft.cn/ShopCover-" + UserID + ".aspx\" target=\"_blank\">" + e.Row.Cells[2].Text + "</a>";
                string lockStr = "";
                //int intCount = BLL_tab_ShopClient.ExistsCount("ID=" + Int32.Parse(e.Row.Cells[1].Text) + " and isLocked=1");

                //string strIFCompany = e.Row.Cells[3].Text.Trim().ToLower();
                //if (strIFCompany == "false")
                //{
                //    e.Row.Cells[3].Text = "个人";
                //}
                //else
                //{
                //    e.Row.Cells[3].Text = "单位";
                //}

                //if (intCount != 0)
                //{
                //    lockStr = "&nbsp;&nbsp;<a href=\"?Act=unlock&ID=" + e.Row.Cells[1].Text + "\" style=\"color:#ff0000\">解锁</a>";
                //}
                //else
                //{
                //    lockStr = "&nbsp;&nbsp;<a href=\"?Act=lock&ID=" + e.Row.Cells[1].Text + "\">锁定</a>";
                //}

                string delStr = "&nbsp;&nbsp;<a href=\"?Act=delete&UserID=" + UserID + "\" onclick=\"return confirm('确定删除吗?删除后不可恢复！')\">删除</a>";
                lockStr = "";

                //EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();

                //string strWhere = "ShopClient_ID=" + UserID + " and PayStatus=1 and isReceipt=0 and DeliveryText=\'\' ";

                //e.Row.Cells[5].Text = bll_tab_Order.ExistsCount(strWhere).ToString();


                //e.Row.Cells[6].Text = "￥" + Eggsoft_Public_CL.Pub_FenXiao.Get7DayS90Percent(Int32.Parse(UserID));
                e.Row.Cells[10].Text = lockStr + "&nbsp;&nbsp;<a href=\"BoardINC_Manage.aspx?type=Modify&ID=" + UserID + "\">修改</a>" + delStr + "&nbsp;&nbsp;<a target=\"_blank\"  href=\"" + strClientAdminURL + "/ClientAdmin/Login.aspx?Act=gotouserFrom_Admin&UserID=oliver" + UserID + "\">商户后台</a>";
            }
        }

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            BindUser();
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
            BindUser();
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
            BindUser();
            ShowState();
            InitGoPage();
        }
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = GetPageCount();
            BindUser();
            ShowState();
            InitGoPage();
        }
        protected void ddlGoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = Int32.Parse(ddlGoPage.SelectedValue);
            BindUser();
            ShowState();
        }

        private void ShowState()
        {
            lblMsg.Text = "当前页:" + ViewState["PageIndex"].ToString() + "/" + GetPageCount().ToString() + " 每页:" + ViewState["PageSize"].ToString() + " 共:" + ViewState["RecordCount"].ToString() + "条";
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

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridViewRow r in gvUser.Rows)
            {
                CheckBox myChkItem = (CheckBox)r.Cells[0].FindControl("chkItem");
                if (myChkItem != null)
                {
                    if (myChkItem.Checked)
                    {
                        DeleteUserByID(Int32.Parse(r.Cells[1].Text));
                        i += 1;
                    }
                }
            }
            if (i != 0)
            {
                JsUtil.ShowMsg("删除成功!", "?");
            }
        }

        /// <summary>
        /// 全选按钮改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox myChkAll = (CheckBox)gvUser.HeaderRow.Cells[0].FindControl("chkAll");
            foreach (GridViewRow r in gvUser.Rows)
            {
                CheckBox myChkItem = (CheckBox)r.Cells[0].FindControl("chkItem");
                if (myChkItem != null)
                {
                    myChkItem.Checked = myChkAll.Checked;
                }
            }
        }

        private void DeleteUserByID(int id)
        {

            //EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
            BLL_tab_ShopClient.Delete(id);
        }
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string t = "";
        //    if (rbtnUserID.Checked)
        //        t = "UserID";
        //    else
        //        t = "NickName";
        //    Response.Redirect("UserManage.aspx?keyWord=" + txtKeyWord.Text.Trim() + "&t=" + t);
        //}
        protected void btnAddAll_Click(object sender, EventArgs e)
        {
            JsUtil.LocationNewHref("BoardINC_Manage.aspx?type=add");
        }

    }
}