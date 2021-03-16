using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._25WeiXianChang
{
    public partial class _25XianChangHuoDong_Number_UserShakeNum : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.View_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum bll_View_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum = new EggsoftWX.BLL.View_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strXianChangHuoDongNumberbyShopClientID = Request.QueryString["XianChangHuoDongNumberbyShopClientID"];

                Literaltab_ShopClient_XianChangHuoDong_Number.Text = "编号为" + strXianChangHuoDongNumberbyShopClientID;

                string strWhere = "XianChangHuoDongNumberbyShopClientID=" + strXianChangHuoDongNumberbyShopClientID + " and UserShopClientID=" + strShopClientID + " and ShopClientID=" + strShopClientID;
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;
                ViewState["RecordCount"] = bll_View_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.ExistsCount(strWhere).ToString();
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
            sql.addOrderField("UserShakeNumber", "desc");//第一排序字段  
            sql.addOrderField("ID", "asc");//第一排序字段  

            sql.table = "View_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum";

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strXianChangHuoDongNumberbyShopClientID = Request.QueryString["XianChangHuoDongNumberbyShopClientID"];
            string strWhere = "XianChangHuoDongNumberbyShopClientID=" + strXianChangHuoDongNumberbyShopClientID + " and UserShopClientID=" + strShopClientID + " and ShopClientID=" + strShopClientID;
   

            sql.outfields = "[ShopUserID],[NickName],[UserRealName],[ContactMan],[ContactPhone],[HeadImageUrl],[Subscribe],[UserShakeNumber],[XianChangHuoDongNumberbyShopClientID],[ID],[UserShopClientID],[ShopClientID]";

            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = "(Subscribe=1) and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            sql.where = strwhere;
            //Response.Write(sql.getSQL());

            //string strSql = "select top 20 * from tab_Goods_Class where UserID=12 order by tab_Goods_Class.sort asc,tab_Goods_Class.id asc";
            string strSql = sql.getSQL(bll_View_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.ExistsCount(strWhere));

            gvAnnounce.DataSource = bll_View_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.SelectList(strSql);

            //gvAnnounce.DataSource = bll.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,ClassName,Sort,UpdateTime", "UserID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString(), "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strID = e.Row.Cells[1].Text;
                //e.Row.Cells[1].Text = Eggsoft_Public_CL.GoodP.GetGood_Num_ID_From_Good_ID(Convert.ToInt32(strID));

                //string strUserID = e.Row.Cells[3].Text;
                //string strParentID = e.Row.Cells[4].Text;
                //string strGrandParentID = e.Row.Cells[5].Text;
                //string strGreatParentID = e.Row.Cells[6].Text;

                //e.Row.Cells[3].Text = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                //e.Row.Cells[4].Text = Eggsoft_Public_CL.Pub.GetNickName(strParentID);

                //Decimal myCountMoney = 0;
                //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney);
                //e.Row.Cells[8].Text = "<a href=\"UserStatus_Money.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney) + "</a>";


                //Decimal myCountMoney_Vouchers = 0;
                //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney_Vouchers);
                //e.Row.Cells[9].Text = "<a href=\"UserStatus_Quan.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "</a>";
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

        //protected void lnkDel_Click(object sender, EventArgs e)
        //{
        //    //pubstrXianChangHuoDongID = Request.QueryString["XianChangHuoDongID"];
        //    string strXianChangHuoDongNumberbyShopClientID = Request.QueryString["XianChangHuoDongNumberbyShopClientID"];

        //    string str_ShopClient_XianChangHuoDong_NumberShake_ID = ((LinkButton)sender).CommandArgument;
        //    string strRedirectURL = "25XianChangHuoDong_Number_UserShakeNum_Manage.aspx?Type=delete&ID={0}&CallBackUrl=25XianChangHuoDong_Number_UserShakeNum.aspx*XianChangHuoDongNumberbyShopClientID={1}@PageIndex={2}";
        //    strRedirectURL = String.Format(strRedirectURL, str_ShopClient_XianChangHuoDong_NumberShake_ID, strXianChangHuoDongNumberbyShopClientID, ViewState["PageIndex"].ToString());


        //    Response.Redirect(strRedirectURL);
        //}
    }
}