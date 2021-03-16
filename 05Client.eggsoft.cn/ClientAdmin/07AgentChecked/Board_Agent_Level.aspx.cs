using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{
    public partial class Board_Agent_Level : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_Agent_Level bll = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
        string str_Pub_ShopClientID = "";
         string str_Pub_upLoadpath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ShopAdvance_Agent_Level")))
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

                int intExistsCount = bll.ExistsCount("ShopClientID=" + str_Pub_ShopClientID);
                if (intExistsCount > 0) Button_ReadDefult.Visible = false;
                ViewState["RecordCount"] = intExistsCount;
                BindAnnounce();
                ShowState();
                InitGoPage();


                #region 代理须知
                string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
                string strHyperLink_MakeHtml = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + str_Pub_ShopClientID + "&GoToUrl=";
                HyperLink_MustRead.NavigateUrl = strHyperLink_MakeHtml + Server.UrlEncode("AgentMustReadAD.aspx");
                #endregion

                //str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt16(str_Pub_ShopClientID)) + "/QRCodeImage/";

                //if (int.Parse(ViewState["RecordCount"].ToString())<2)    btnAdd.Visible=true;

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
            sql.addOrderField("tab_ShopClient_Agent_Level.sort", "asc");//第一排序字段  
            sql.addOrderField("tab_ShopClient_Agent_Level.id", "asc");//第二排序字段  
            sql.table = "tab_ShopClient_Agent_Level";
            sql.outfields = "tab_ShopClient_Agent_Level.ID,GouWuQuanGoodPrice,tab_ShopClient_Agent_Level.ShopClientID,tab_ShopClient_Agent_Level.AgentLevelName,tab_ShopClient_Agent_Level.Sort";
            sql.nowPageIndex = int.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = int.Parse(ViewState["PageSize"].ToString());
            string strwhere = "ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            sql.where = strwhere;
            string strSql = sql.getSQL(bll.ExistsCount(strwhere));

            gvAnnounce.DataSource = bll.SelectList(strSql);

            //gvAnnounce.DataSource = bll.GetPageDataTable(int.Parse(ViewState["PageIndex"].ToString()), int.Parse(ViewState["PageSize"].ToString()), "ID,ClassName,Sort,UpdateTime", "UserID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[0].Text;

                e.Row.Cells[4].Text = "<a href=\"Agent_Level_Manage.aspx?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[5].Text = "<a href=\"Agent_Level_Manage.aspx?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
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
            if (int.Parse(ViewState["PageIndex"].ToString()) > 1)
            {
                ViewState["PageIndex"] = int.Parse(ViewState["PageIndex"].ToString()) - 1;
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
            if (int.Parse(ViewState["PageIndex"].ToString()) < GetPageCount())
            {
                ViewState["PageIndex"] = int.Parse(ViewState["PageIndex"].ToString()) + 1;
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
            ViewState["PageIndex"] = int.Parse(ddlGoPage.SelectedValue);
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
                if (int.Parse(ViewState["PageIndex"].ToString()) <= 1)
                {
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    lbtnNext.Enabled = true;
                    lbtnLast.Enabled = true;
                }
                else
                {
                    if (int.Parse(ViewState["PageIndex"].ToString()) >= GetPageCount())
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
            int pageCount = int.Parse(ViewState["RecordCount"].ToString()) % int.Parse(ViewState["PageSize"].ToString()) == 0 ? (int.Parse(ViewState["RecordCount"].ToString()) / int.Parse(ViewState["PageSize"].ToString())) : (int.Parse(ViewState["RecordCount"].ToString()) / int.Parse(ViewState["PageSize"].ToString()) + 1);
            return pageCount;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Agent_Level_Manage.aspx?type=Add");
        }
        protected void Button_ReadDefult_Click(object sender, EventArgs e)
        {
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.Model.tab_ShopClient_Agent_Level MyModel = new EggsoftWX.Model.tab_ShopClient_Agent_Level();
            MyModel.ShopClientID = int.Parse(str_Pub_ShopClientID);
            MyModel.Sort = 1;
            MyModel.AgentLevelName = "省级代理";
            MyModel.GouWuQuanGoodPrice = 135;
            MyModel.AgentlevelMemo = "订货金额 10万 代理金额135";
            bll.Add(MyModel);

            MyModel.Sort = 2;
            MyModel.AgentLevelName = "市级代理";
            MyModel.GouWuQuanGoodPrice = 145;
            MyModel.AgentlevelMemo = "订货金额 48000  代理金额145";
            bll.Add(MyModel);

            MyModel.Sort = 3;
            MyModel.AgentLevelName = "皇冠代理";
            MyModel.GouWuQuanGoodPrice = 160;
            MyModel.AgentlevelMemo = "订货金额 20800 代理金额160";
            bll.Add(MyModel);

            MyModel.Sort = 4;
            MyModel.AgentLevelName = "铂金代理";
            MyModel.GouWuQuanGoodPrice = 180;
            MyModel.AgentlevelMemo = "订货金额 6840  代理金额180";
            bll.Add(MyModel);


            MyModel.Sort = 5;
            MyModel.AgentLevelName = "天使代理";
            MyModel.GouWuQuanGoodPrice = 218;
            MyModel.AgentlevelMemo = "订货金额 1090  代理金额218";
            bll.Add(MyModel);

            Eggsoft.Common.JsUtil.ShowMsg("读取默认摸板完毕，生成静态页后，手机端会出现代理级别的申请.如果不需要代理功能，请逐个删除这里的分级", "Board_Agent_Level.aspx");

        }
    }
}