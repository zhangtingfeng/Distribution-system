using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._22GameSendJiFen
{
    public partial class GameSendJiFenBoard : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        EggsoftWX.BLL.tab_ShopClient_Game BLL_tab_ShopClient_Game = new EggsoftWX.BLL.tab_ShopClient_Game();
        protected void Page_Load(object sender, EventArgs e)
        { 
            #region 没有打开的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_GameSendJiFenBoard")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开的权限
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = BLL_tab_ShopClient_Game.ExistsCount(" AND ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " and ISNULL(IsDeleted,0)=0");
                BindAnnounce();
                ShowState();
                InitGoPage();
            }
        }

        private void BindAnnounce()
        {
            string strCondition = " AND ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " and ISNULL(IsDeleted,0)=0";
            gvAnnounce.DataSource = BLL_tab_ShopClient_Game.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "*", strCondition, "ID", true);
            gvAnnounce.DataBind();
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

        private int GetPageCount()
        {
            int pageCount = Int32.Parse(ViewState["RecordCount"].ToString()) % Int32.Parse(ViewState["PageSize"].ToString()) == 0 ? (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString())) : (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString()) + 1);
            return pageCount;
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
        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].Text = Eggsoft_Public_CL.Pub.getGameTypeShowName(Int32.Parse(e.Row.Cells[3].Text));
                e.Row.Cells[7].Text = "<a href=\"" + Add_Click_OrModifypath(e.Row.Cells[7].Text) + "?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[8].Text = "<a href=\"" + Add_Click_OrModifypath(e.Row.Cells[8].Text) + "?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

                #region 二维码显示
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(strShopClient_ID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                ///
                string strGamePath = "";
                if (e.Row.Cells[1].Text == "001yuebing2")
                {
                    e.Row.Cells[1].Text = "中秋节吃月饼";
                    strGamePath = "/game/001yuebing2/index.html";
                }
                else if (e.Row.Cells[1].Text == "002qiangfeicui")
                {
                    e.Row.Cells[1].Text = "土豪抢翡翠";
                    strGamePath = "/game/002qiangfeicui/index.html";
                }
                else if (e.Row.Cells[1].Text == "003HowColor")
                {
                    e.Row.Cells[1].Text = "看你有多色";
                    strGamePath = "/game/003HowColor/index.html";
                }
                else if (e.Row.Cells[1].Text == "004zuiqiangyanli")
                {
                    e.Row.Cells[1].Text = "最强眼力";
                    strGamePath = "/game/004zuiqiangyanli/index.html";
                }
                else if (e.Row.Cells[1].Text == "005ZhongQiu")
                {
                    e.Row.Cells[1].Text = "国庆贺卡";
                    strGamePath = "/game/005ZhongQiu/index.html";
                }
                string strURL = "https://" + strErJiYuMing + strGamePath + "?gameid=" + e.Row.Cells[0].Text;
                strURL = strURL.ToLower();
                string str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClient_ID)) + "/QRCodeImage/";
                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strURL, str_Pub_upLoadpath, "");
                e.Row.Cells[9].Text = "<a href=\"" + strURL + "\"><img alt=\"" + strURL + "\" class=\"ClassWeWeima\" src=\"" + strImageUrl + "\"></a>";
                #endregion

            }
        }

        protected string Add_Click_OrModifypath(string strFromGameType)
        {
            string strPath = "";
            switch (strFromGameType)
            {
                case "001yuebing2":
                    strPath = "GameSendJiFenOperating_001yuebing2.aspx";
                    break;
                case "002qiangfeicui":
                    strPath = "GameSendJiFenOperating_002qiangfeicui.aspx";
                    break;
                case "003HowColor":
                    strPath = "GameSendJiFenOperating_003HowColor.aspx";
                    break;
                case "004zuiqiangyanli":
                    strPath = "GameSendJiFenOperating_004zuiqiangyanli.aspx";
                    break;
                case "005ZhongQiu":
                    strPath = "GameSendJiFenOperating_005ZhongQiu.aspx";
                    break;
                default:
                    strPath = "GameSendJiFenOperating_001yuebing2.aspx";
                    break;
            }
            return strPath;
        }


        protected void btnAdd_Click_001yuebing2(object sender, EventArgs e)
        {
            Response.Redirect(Add_Click_OrModifypath("001yuebing2") + "?type=Add");
        }
        protected void btnAdd_Click_002qiangfeicui(object sender, EventArgs e)
        {
            Response.Redirect(Add_Click_OrModifypath("002qiangfeicui") + "?type=Add");
        }
        protected void btnAdd_Click_003HowColor(object sender, EventArgs e)
        {
            Response.Redirect(Add_Click_OrModifypath("003HowColor") + "?type=Add");
        }
        protected void btnAdd_Click_004zuiqiangyanli(object sender, EventArgs e)
        {
            Response.Redirect(Add_Click_OrModifypath("004zuiqiangyanli") + "?type=Add");
        }
        protected void btnAdd_Click_005ZhongQiu(object sender, EventArgs e)
        {
            Response.Redirect(Add_Click_OrModifypath("005ZhongQiu") + "?type=Add");
        }
    }
}