using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._20Agent_Status
{
    public partial class AgentStatus : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        //EggsoftWX.BLL.View_ShopClient_Agent_SalesOrderBy bll_View_ShopClient_Agent_SalesOrderBy = new EggsoftWX.BLL.View_ShopClient_Agent_SalesOrderBy();
        EggsoftWX.BLL.tab_ShopClient_Agent_Level bll_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
        EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = new EggsoftWX.Model.tab_ShopClient_Agent_Level();

        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

        EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
        EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = new EggsoftWX.Model.tab_ShopClient_ShopPar();

        protected void Page_Load(object sender, EventArgs e)
        {  
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_AgentStatus")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                ViewState["PageIndex"] = 1;
                if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];

                ViewState["PageSize"] = 20;

                pathSearch();



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
            //string ViewState["ShowOrderby"] = strShowOrderby;

            // = strdayName;
            //ViewState["ShowOrderby"] = str_OrderBy;
            string strShowOrderbyName = ViewState["ShowOrderbyName"].ToString();
            string strShowOrderby = ViewState["ShowOrderby"].ToString();


            // gvAnnounce.DataSource = bll_View_ShopClient_Agent_SalesOrderBy.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "", strWhere, strShowOrderbyName, boolOrder);


            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            sql.addOrderField(strShowOrderbyName, strShowOrderby);//第一排序字段  
            sql.table = "View_ShopClient_Agent_SalesOrderBy";
            sql.outfields = "View_ShopClient_Agent_SalesOrderBy.LastMonthSales_my_AND_myAllSon,View_ShopClient_Agent_SalesOrderBy.ThisMonth_SalesMoney_my_AND_myAllSon,View_ShopClient_Agent_SalesOrderBy.AllSalesMoney_my_AND_myAllSon,View_ShopClient_Agent_SalesOrderBy.ID,View_ShopClient_Agent_SalesOrderBy.ShopName,View_ShopClient_Agent_SalesOrderBy.AllFenXiaoMoney,View_ShopClient_Agent_SalesOrderBy.myAgentSonSum,View_ShopClient_Agent_SalesOrderBy.AllFenXiaoMoney_my_AND_myAllSon,View_ShopClient_Agent_SalesOrderBy.ShopUserID,View_ShopClient_Agent_SalesOrderBy.UserID,View_ShopClient_Agent_SalesOrderBy.UserRealName,View_ShopClient_Agent_SalesOrderBy.NickName,View_ShopClient_Agent_SalesOrderBy.ContactPhone,View_ShopClient_Agent_SalesOrderBy.Empowered,View_ShopClient_Agent_SalesOrderBy.AlipayNumOrWeiXinPay,View_ShopClient_Agent_SalesOrderBy.UpdateTime,View_ShopClient_Agent_SalesOrderBy.AgentLevelSelect,View_ShopClient_Agent_SalesOrderBy.ParentID,ISNULL(View_ShopClient_Agent_SalesOrderBy.RemainingSum, 0.00) AS RemainingSum,ISNULL(View_ShopClient_Agent_SalesOrderBy.RemainingSum_Vouchers, 0.00) AS RemainingSum_Vouchers";
            sql.nowPageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
            sql.pagesize = Int32.Parse(ViewState["PageSize"].ToString());
            sql.where = strWhere;
            string strSql = sql.getSQL(Int32.Parse(ViewState["RecordCount"].ToString()));

            //gvAnnounce.DataSource = bll_View_ShopClient_Agent_SalesOrderBy.SelectList(strSql);


            //gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int intAgentLevelSelect = 0;

                #region 收货地址
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + strShopClientID);
                if (Model_tab_ShopClient_ShopPar != null)
                {
                    string strAddExpListTextShow = Model_tab_ShopClient_ShopPar.AddExpListTextShow;
                    if ((String.IsNullOrEmpty(strAddExpListTextShow) == false))
                    {
                        string strID = e.Row.Cells[0].Text;
                        System.Data.DataTable myDataTable = bll_tab_ShopClient_Agent_.GetList("userid=" + strID + " and ShopClientID=" + strShopClientID).Tables[0];

                        string[] strAddExpListTextList = strAddExpListTextShow.Split('#');
                        for (int i = 0; i < strAddExpListTextList.Length; i++)
                        {
                            e.Row.Cells[3].Text += "<br />" + strAddExpListTextList[i] + ":";
                            if (myDataTable.Rows.Count > i)
                            { e.Row.Cells[3].Text += myDataTable.Rows[0]["AddExp" + i].ToString(); }
                        }
                    }

                }
                #endregion 收获地址


                string strintAgentLevelSelect = e.Row.Cells[8].Text;
                int.TryParse(strintAgentLevelSelect, out intAgentLevelSelect);
                if (intAgentLevelSelect > 0)
                {
                    Model_tab_ShopClient_Agent_Level = bll_tab_ShopClient_Agent_Level.GetModel(intAgentLevelSelect);
                    if (Model_tab_ShopClient_Agent_Level != null)
                    {
                        e.Row.Cells[8].Text = Model_tab_ShopClient_Agent_Level.AgentLevelName;
                    }
                    else
                    {
                        e.Row.Cells[8].Text = "分销代理";
                    }
                }
                else
                {
                    e.Row.Cells[8].Text = "分销代理";
                }
                e.Row.Cells[15].Text = Eggsoft_Public_CL.Pub.GetNickName(e.Row.Cells[15].Text);

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



        protected void Button1_Click_Query(object sender, EventArgs e)
        {
            pathSearch();
        }


        protected void pathSearch()
        {
            string strWhere = "";


            if (string.IsNullOrEmpty(TextBox_ShopUserID.Text) == false)
            {
                strWhere += " and ShopUserID=" + TextBox_ShopUserID.Text;
            }
            if (string.IsNullOrEmpty(TextBox_ShopName.Text) == false)
            {
                strWhere += " and ShopName like '%" + TextBox_ShopName.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_ContactMan.Text) == false)
            {
                strWhere += " and UserRealName like '%" + TextBox_ContactMan.Text + "%'";
            }
            if (string.IsNullOrEmpty(TextBox_NickName.Text) == false)
            {
                strWhere += " and NickName like '%" + TextBox_NickName.Text + "%'";
            }
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strdayName = "";
            switch (RadioButtonList_AgentMoney.SelectedValue)
            {
                case "6":
                    strdayName = "LastMonthSales_my_AND_myAllSon";
                    break;
                case "7":
                    strdayName = "ThisMonth_SalesMoney_my_AND_myAllSon";
                    break;
                case "5":
                    strdayName = "AllSalesMoney_my_AND_myAllSon";
                    break;
                case "0":
                    strdayName = "AllFenXiaoMoney_my_AND_myAllSon";
                    break;
                case "1":
                    strdayName = "myAgentSonSum";
                    break;
                case "2":
                    strdayName = "AllFenXiaoMoney";
                    break;
                case "3":
                    strdayName = "RemainingSum";
                    break;
                case "4":
                    strdayName = "RemainingSum_Vouchers";
                    break;
            }
            string str_OrderBy = "";
            switch (RadioButtonList__AgentMoney_OrderBy.SelectedValue)
            {
                case "0":
                    str_OrderBy = "asc";
                    break;
                case "1":
                    str_OrderBy = "desc";
                    break;
            }

            ViewState["ShowOrderbyName"] = strdayName;
            ViewState["ShowOrderby"] = str_OrderBy;


            ViewState["searchWhere"] = "1=1 and Empowered=1 and ShopClientID=" + strShopClientID + strWhere;
            string strSearchWhere = ViewState["searchWhere"].ToString();
            /////ViewState["RecordCount"] = bll_View_ShopClient_Agent_SalesOrderBy.ExistsCount(strSearchWhere);暂时去掉20171220


            ViewState["PageIndex"] = 1;
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void Button_Status_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnLast_Click_DownLoad(object sender, EventArgs e)
        {
            try
            {
                #region Excel 表格下载
                string strTablename = "[Sheet1$]";
                Eggsoft.Common.debug_Log.Call_WriteLog(strTablename, "昨日报表-分销商代理商统计下载", "数据记录sheet1");

                string strFromfilePath = "ExcelDocument/BaseDocument/AgentStatusDownload.xls";
                string FromfilePath = HttpContext.Current.Server.MapPath(strFromfilePath);

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strDownFileName = "AgentStatusDownload" + Eggsoft.Common.StringNum.Add000000Num(strShopClientID.toInt32(), 5) + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ".xls";
                string WillDowmFile = "ExcelDocument/DownLoadDocument/" + strDownFileName;
                string AccessfilePath = HttpContext.Current.Server.MapPath(WillDowmFile);

                Eggsoft.Common.debug_Log.Call_WriteLog(AccessfilePath, "昨日报表-分销商代理商统计下载", "数据记录sheet1");


                System.IO.File.Copy(FromfilePath, AccessfilePath, true);////去掉只读属性
                System.IO.File.SetAttributes(AccessfilePath, System.IO.FileAttributes.Normal);


                #region 获取语句
                BindAnnounce();
                System.Data.DataTable Data_DataTable = ((System.Data.DataSet)gvAnnounce.DataSource).Tables[0];

                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                {
                    string SQLtem_sql = "";
                    SQLtem_sql += "update  " + strTablename + " set ";
                    SQLtem_sql += "F0='" + Data_DataTable.Rows[i]["ShopUserID"] + "',";//用户编号
                    SQLtem_sql += "F1='" + Data_DataTable.Rows[i]["ShopName"] + "',";//店铺名称
                    SQLtem_sql += "F2='" + Data_DataTable.Rows[i]["UserRealName"] + "',";//联系人
                    SQLtem_sql += "F3='" + Data_DataTable.Rows[i]["NickName"] + "',";//昵称
                    SQLtem_sql += "F4='" + Data_DataTable.Rows[i]["ContactPhone"] + "',";//联系电话
                    SQLtem_sql += "F5='" + Data_DataTable.Rows[i]["AlipayNumOrWeiXinPay"] + "',";//支付账号
                    SQLtem_sql += "F6='" + Data_DataTable.Rows[i]["UpdateTime"] + "',";//更新日期

                    #region 代理级别
                    string strTitle = "";
                    int intAgentLevelSelect = 0;
                    string strintAgentLevelSelect = Data_DataTable.Rows[i]["AgentLevelSelect"].ToString();
                    int.TryParse(strintAgentLevelSelect, out intAgentLevelSelect);
                    if (intAgentLevelSelect > 0)
                    {
                        Model_tab_ShopClient_Agent_Level = bll_tab_ShopClient_Agent_Level.GetModel(intAgentLevelSelect);
                        if (Model_tab_ShopClient_Agent_Level != null)
                        {
                            strTitle = Model_tab_ShopClient_Agent_Level.AgentLevelName;
                        }
                        else
                        {
                            strTitle = "分销代理";
                        }
                    }
                    else
                    {
                        strTitle = "分销代理";
                    }
                    SQLtem_sql += "F7='" + strTitle + "',";//代理级别
                    #endregion 代理级别
                    #region 总销量￥

                    SQLtem_sql += "F8='" + Data_DataTable.Rows[i]["AllSalesMoney_my_AND_myAllSon"].toDecimal() + "',";
                    #endregion 总销量￥
                    #region 总分销收入

                    SQLtem_sql += "F9='" + Data_DataTable.Rows[i]["AllFenXiaoMoney_my_AND_myAllSon"].toDecimal() + "',";
                    #endregion 总分销收入
                    #region 下线个数

                    SQLtem_sql += "F10='" + Data_DataTable.Rows[i]["myAgentSonSum"] + "',";
                    #endregion 下线个数
                    #region 会员分销收入￥

                    SQLtem_sql += "F11='" + Data_DataTable.Rows[i]["AllFenXiaoMoney"].toDecimal() + "',";
                    #endregion 会员分销收入￥




                    #region 现金￥
                    string strUserID = Data_DataTable.Rows[i]["UserID"].toString();
                    Decimal myCountMoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(strUserID.toInt32(), out myCountMoney);
                    SQLtem_sql += "F12='" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney) + "',";//现金￥
                    #endregion 现金￥

                    #region 购物券￥
                    Decimal myCountMoney_Vouchers = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(strUserID.toInt32(), out myCountMoney_Vouchers);
                    SQLtem_sql += "F13='" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "',";//购物券￥
                    #endregion 购物券￥


                    #region 上级代理商￥
                    string strParentIDName = Eggsoft_Public_CL.Pub.GetNickName(Data_DataTable.Rows[i]["ParentID"].toString());
                    SQLtem_sql += "F14='" + strParentIDName + "',";//
                    #endregion 上级代理商

                    #region 上月销量

                    SQLtem_sql += "F15='" + Data_DataTable.Rows[i]["LastMonthSales_my_AND_myAllSon"].toDecimal().toString() + "',";

                    #endregion 上月销量


                    #region 本月销量

                    SQLtem_sql += "F16='" + Data_DataTable.Rows[i]["ThisMonth_SalesMoney_my_AND_myAllSon"].toDecimal().toString() + "'";

                    #endregion 本月销量


                    SQLtem_sql += " where E0='E" + (i + 1).toString() + "';";
                    EggsoftWX.SQLServerDAL.DbHelperSQL.updateExcel(AccessfilePath, SQLtem_sql);
                }
                // Eggsoft.Common.debug_Log.Call_WriteLog(SQLtem_sql, "昨日报表-分销商代理商统计下载", "数据记录sql");
                //tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn);//实例化OleDbCommand类   
                //RecordCount = (int)tem_comm.ExecuteScalar();//执行SQL语句，并返回结果   
                #endregion 执行更新动作



                //System.Data.OleDb.OleDbConnection tem_conn = new System.Data.OleDb.OleDbConnection(connstr);//连接Access数据库   
                //System.Data.OleDb.OleDbCommand tem_comm;//定义OleDbCommand类   
                //tem_conn.Open();//打开连接的Access数据库   
                //string SQLtem_sql = "";// "select Count(*) From " + strTablename;//设置SQL语句，获取记录个数   
                //tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn);//实例化OleDbCommand类   
                //int RecordCount = (int)tem_comm.ExecuteScalar();//执行SQL语句，并返回结果   



                #endregion Excel 表格下载


                #region //流方式下载 

                string filePath = Server.MapPath(WillDowmFile);//路径 

                //以字符流的形式下载文件 
                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开 
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strDownFileName, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                #endregion
                //ShowState();
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "昨日报表-分销商代理商统计");
            }
        }


    }
}