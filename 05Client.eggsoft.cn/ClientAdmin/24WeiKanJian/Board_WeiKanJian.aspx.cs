using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._24WeiKanJian
{
    public partial class Board_WeiKanJian : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.View_WeiKanJia bllView_WeiKanJia = new EggsoftWX.BLL.View_WeiKanJia();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board_WeiKanJian")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开的权限
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";

            if (!IsPostBack)
            {
                Button1_Click_Query(null, null);
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
            string strWhere = ViewState["searchWhere"].ToString();
            gvAnnounce.DataSource = bllView_WeiKanJia.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strID = e.Row.Cells[0].Text;
                str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                string strLink = "https://" + strErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + strID;
                e.Row.Cells[1].Text = "<a href=\"" + strLink + "\">" + e.Row.Cells[1].Text + "</a>";


                if (e.Row.Cells[2].Text == "True")
                    e.Row.Cells[2].Text = "上架";
                else
                    e.Row.Cells[2].Text = "<span style=\"color: #FF0066;\">下架</span>";

                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
                string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";


                string strModify = strNavigateUrl + Server.UrlEncode("WeiKanJia/WeiKanJia_Manage.aspx?type=Modify&ID=" + strID + "&CallBackUrl=Board_WeiKanJian.aspx*PageIndex=" + ViewState["PageIndex"].ToString());
                string strDelete = strNavigateUrl + Server.UrlEncode("WeiKanJia/WeiKanJia_Manage.aspx?type=Delete&ID=" + strID + "&CallBackUrl=Board_WeiKanJian.aspx*PageIndex=" + ViewState["PageIndex"].ToString());

                e.Row.Cells[7].Text = "<a href=\"" + strModify + "\">修改</a>";
                e.Row.Cells[8].Text = "<a href=\"" + strDelete + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";


                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLink, str_Pub_upLoadpath, "");
                e.Row.Cells[9].Text = "<a target=\"_blank\" href=\"" + strImageUrl + "\"><img style=\"margin-top:3px;margin-bottom:3px;\" width=\"100\" height=\"100\" id=\"ErWeiMaSao" + strID + "\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" /></a>";
            }
        }

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            pathSearch();
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
            pathSearch();
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
            pathSearch();
            InitGoPage();
        }
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = GetPageCount();
            pathSearch();
            InitGoPage();
        }
        protected void ddlGoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = Int32.Parse(ddlGoPage.SelectedValue);
            pathSearch();
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
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

            string strAdd = strNavigateUrl + Server.UrlEncode("WeiKanJia/WeiKanJia_Manage.aspx?type=Add");
            Response.Redirect(strAdd);
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 5;


            string strIfissaled = CheckBox_IfUp.Checked ? "1" : "0";
            string strWhere = " and issaled=" + strIfissaled;


            if (string.IsNullOrEmpty(TextBox_Topic.Text) == false)
            {
                strWhere += " and Topic like '%" + TextBox_Topic.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_StartPrice.Text) == false)
            {
                strWhere += " and StartPrice " + DropDownList_StartPrice.Text + TextBox_StartPrice.Text;
            }
            if (string.IsNullOrEmpty(TextBox_EndPrice.Text) == false)
            {
                strWhere += " and EndPrice " + DropDownList_EndPrice.Text + TextBox_EndPrice.Text;
            }
            if (string.IsNullOrEmpty(TextBox_count_WeikanJiaID.Text) == false)
            {
                int intTextBox_count_WeikanJiaID = 0;
                int.TryParse(TextBox_count_WeikanJiaID.Text, out intTextBox_count_WeikanJiaID);
                if (intTextBox_count_WeikanJiaID > 0)
                {
                    strWhere += " and count_WeikanJiaID " + DropDownList_count_WeikanJiaID.Text + intTextBox_count_WeikanJiaID;
                }
            }
            if (string.IsNullOrEmpty(DropDownList_COUNT_COUNT_WeiKanJiaMasterID.Text) == false)
            {
                int intTextBox_COUNT_COUNT_WeiKanJiaMasterID = 0;
                int.TryParse(DropDownList_COUNT_COUNT_WeiKanJiaMasterID.Text, out intTextBox_COUNT_COUNT_WeiKanJiaMasterID);
                if (intTextBox_COUNT_COUNT_WeiKanJiaMasterID > 0)
                {
                    strWhere += " and count_WeikanJiaID " + DropDownList_COUNT_COUNT_WeiKanJiaMasterID.Text + intTextBox_COUNT_COUNT_WeiKanJiaMasterID;
                }
            }
            if (string.IsNullOrEmpty(TextBox_KanJiaTopicDescContent.Text) == false)
            {
                strWhere += " and KanJiaTopicDescContent like '%" + TextBox_KanJiaTopicDescContent.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_KanJiaRule.Text) == false)
            {
                strWhere += " and KanJiaRule like '%" + TextBox_KanJiaRule.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_GoodName.Text) == false)
            {
                strWhere += " and GoodName like '%" + TextBox_GoodName.Text + "%'";
            }


            ViewState["SQLWhere"] = strWhere;

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            ViewState["RecordCount"] = bllView_WeiKanJia.ExistsCount("ShopClientID=" + str_Pub_ShopClientID + " and IsDeleted=0" + strWhere);
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {

            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            //  sql.addOrderField("View_WeiKanJia.UpdateTime", "desc");//第二排序字段  
            sql.addOrderField("View_WeiKanJia.sort", "asc");//第一排序字段  
            sql.addOrderField("View_WeiKanJia.id", "asc");//第二排序字段  
            sql.table = "View_WeiKanJia";

            string stroutfields = "";
            stroutfields = "[ID]      ,[Topic]      ,[UpdateTime]      ,[KanJiaRule]      ,[StartPrice]      ,[EndPrice]      ,[EachAction_LowPrice]      ,[EachAction_HighPrice]      ,[isdeleted]      ,[ShopClientID]      ,[MustSubscribe_Master]";
            stroutfields += ",[MustSubscribe_Helper]           ,[KuCunCount]      ,[KanJiaTopicDescContent]      ,[EndTime]      ,[MustSubscribe_Agent]";
            stroutfields += ",[isSaled],[Sort],[COUNT_COUNT_WeiKanJiaMasterID],[count_WeikanJiaID],[GoodName]";
            sql.outfields = stroutfields;
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = "ShopClientID=" + str_Pub_ShopClientID + "  and isdeleted=0 " + ViewState["SQLWhere"];
            sql.where = strwhere;
            string strSql = sql.getSQL(bllView_WeiKanJia.ExistsCount(strwhere));

            ViewState["searchWhere"] = strSql;// " and ShopClientID=" + strShopClientID + strWhere;


            BindAnnounce();
            ShowState();

        }
    }
}