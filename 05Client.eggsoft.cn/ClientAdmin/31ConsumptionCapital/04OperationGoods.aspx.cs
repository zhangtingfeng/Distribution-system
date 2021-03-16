using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _04OperationGoods : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strModifyASPX = "05OperationGoods_Manage.aspx";
        private string strBoardASPX = "04OperationGoods.aspx";

        public static string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";
        string str_Pub_upLoadpath = "";

        EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_04OperationGoods")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限

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
            gvAnnounce.DataSource = bllb004_OperationGoods.SelectList(strWhere);
            gvAnnounce.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string strZhongChouIDID = e.Row.Cells[0].Text;
                //str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                //EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                //EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                //string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                //string strLink = "http://" + strErJiYuMing + "/addfunction/04zc_project/03zc.html?zcid=" + strZhongChouIDID;

                //if (e.Row.Cells[1].Text == "True")
                //    e.Row.Cells[1].Text = "上架";
                //else
                //    e.Row.Cells[1].Text = "<span style=\"color: #FF0066;\">下架</span>";

                e.Row.Cells[4].Text = "<a href=\"" + strModifyASPX + "?type=Modify&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()) + "\">修改</a>";
                e.Row.Cells[5].Text += "<a href=\"" + strModifyASPX + "?type=Delete&ID=" + e.Row.Cells[0].Text + "&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()) + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";


                string strb004_OperationGoodsID = e.Row.Cells[0].Text;
                str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值
                string strLink = "https://" + strErJiYuMing + "/op-0-"+ strb004_OperationGoodsID + ".aspx";
                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLink, str_Pub_upLoadpath, "");
                e.Row.Cells[6].Text = "<a target=\"_blank\" href=\"" + strImageUrl + "\"><img style=\"margin-top:3px;margin-bottom:3px;\" width=\"100\" height=\"100\" id=\"ErWeiMaSao" + strb004_OperationGoodsID + "\" src=\"" + strImageUrl + "\" align=\"点击扫一扫\" /></a>";

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
            //string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            //string strNavigateUrl = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

            //string strAdd = strNavigateUrl + Server.UrlEncode("27ZC_Project/Zc01_Manage.aspx?type=Add");
            Response.Redirect(strModifyASPX + "?type=Add&CallBackUrl=" + Server.UrlEncode(strBoardASPX + "?PageIndex=" + ViewState["PageIndex"].ToString()));
        }


        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];
            ViewState["PageSize"] = 5;


            string strIfissaled = CheckBox_IfRunningState.Checked ? "1" : "0";
            string strWhere = " and (RunningStatus=" + strIfissaled + ")";


            if (string.IsNullOrEmpty(TextBox_GoodName.Text) == false)
            {
                strWhere += " and (Name like '%" + TextBox_GoodName.Text + "%'  or ShortInfo like '%"+ TextBox_GoodName.Text + "%')";
            }
            //if (string.IsNullOrEmpty(TextBox_YuE.Text) == false)
            //{
            //    strWhere += " and (b003_TotalCredits_OperationCenterRemainingSum " + DropDownList_DestinationPrice.Text + TextBox_YuE.Text + ")";
            //}


            //if (string.IsNullOrEmpty(TextBox_MasterName.Text) == false)
            //{
            //    strWhere += " and (MasterName like '%" + TextBox_MasterName.Text + "%')";
            //}
            //if (string.IsNullOrEmpty(TextBox_MasterPhone.Text) == false)
            //{
            //    strWhere += " and (MasterPhone like '%" + TextBox_MasterPhone.Text + "%')";
            //}
            //if (string.IsNullOrEmpty(TextBox_ShopUserID.Text) == false)
            //{
            //    strWhere += " and (ShopUserID =" + TextBox_ShopUserID.Text + ")";
            //}


            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strZhongChouJointWhere = "";
            strZhongChouJointWhere += " IsDeleted=0 ";

            string strSQLTable = string.Format(@" SELECT  tab_Goods.Name,tab_Goods.ShortInfo,b004_OperationGoods.* 
FROM     b004_OperationGoods LEFT OUTER JOIN
               tab_Goods ON b004_OperationGoods.GoodID =tab_Goods.ID AND 
               b004_OperationGoods.ShopClient_ID =tab_Goods.ShopClient_ID
 WHERE   (b004_OperationGoods.ShopClient_ID = {0})
", strShopClientID);

            ViewState["SQLTable"] = "(" + strSQLTable + ") vTable";

            ViewState["SQLWhere"] = strZhongChouJointWhere + strWhere;
            string strSQLRecordCount = String.Format("SELECT {0} FROM " + ViewState["SQLTable"] + " WHERE" + strZhongChouJointWhere, "count(*) as RecordCount") + strWhere;
            string strRecordCount = bllb004_OperationGoods.SelectList(strSQLRecordCount).Tables[0].Rows[0]["RecordCount"].ToString();

            ViewState["RecordCount"] = strRecordCount;
            ViewState["PageIndex"] = 1;

            pathSearch();
            InitGoPage();
        }


        protected void pathSearch()
        {
            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            //sql.addOrderField("tab_ZC_01Product.sort", "asc");//第一排序字段  
            sql.addOrderField("id", "asc");//第二排序字段  
            sql.table = ViewState["SQLTable"].ToString();

            string stroutfields = @"[Name]
      ,[ShortInfo]
      ,[ID]
      ,[GoodID]
      ,[ShopClient_ID]
      ,[RunningStatus]
      ,[MoneyConsumerWeighting]
      ,[ReturnMoneyShareA]
      ,[ReturnMoneyShareB]
      ,[ReturnMoneyOperationShareA]
      ,[ReturnMoneyOperationShareB]
      ,[ReturnMoneyToCompany]
      ,[ReturnConsumerWealth]    
      ,[CreateBy]
      ,[UpdateTime]
      ,[UpdateBy]
      ,[CreatTime]
      ,[IsDeleted]";


            sql.outfields = stroutfields;
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            string strwhere = ViewState["SQLWhere"].ToString();
            sql.where = strwhere;
            string strSql = sql.getSQL(Int32.Parse(ViewState["RecordCount"].ToString()));

            ViewState["searchWhere"] = strSql;// " and ShopClientID=" + strShopClientID + strWhere;


            BindAnnounce();
            ShowState();

        }


    }
}
