using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{

    //============================================================================
    // 新软交易论坛 官方支持：localhost:3359 
    //
    // 新软小组 QQ:605662917
    //============================================================================
    public partial class Board_AgentChecked : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.View_ShopClient_Agent bll_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
        EggsoftWX.BLL.tab_ShopClient_Agent_Level bll_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
        EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = new EggsoftWX.Model.tab_ShopClient_Agent_Level();

        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

        EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
        EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = new EggsoftWX.Model.tab_ShopClient_ShopPar();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_AgentChecked")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                try
                {
                    string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                    ViewState["PageIndex"] = 1;
                    if (String.IsNullOrEmpty(Request.QueryString["PageIndex"]) == false) ViewState["PageIndex"] = Request.QueryString["PageIndex"];///页面回调使用的

                    ViewState["PageSize"] = 20;

                    pathSearch();

                    #region 分销须知
                    string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];
                    string strHyperLink_MakeHtml = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClientID + "&GoToUrl=";
                    HyperLink_MustRead.NavigateUrl = strHyperLink_MakeHtml + Server.UrlEncode("AgentMustRead.aspx");
                    #endregion
                }
                catch (Exception Exceptiondddd)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
                }

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
            gvAnnounce.DataSource = bll_View_ShopClient_Agent.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,ShopName,ShopUserID,UserID,UserRealName,NickName,ContactPhone,Empowered,AlipayNumOrWeiXinPay,UpdateTime,AgentLevelSelect,ParentID,TeamParentID", strWhere, "ID", true);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int intAgentLevelSelect = 0;

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


                    string strintAgentLevelSelect = e.Row.Cells[9].Text;
                    int.TryParse(strintAgentLevelSelect, out intAgentLevelSelect);
                    if (intAgentLevelSelect > 0)
                    {
                        Model_tab_ShopClient_Agent_Level = bll_tab_ShopClient_Agent_Level.GetModel(intAgentLevelSelect);
                        if (Model_tab_ShopClient_Agent_Level != null)
                        {
                            e.Row.Cells[9].Text = Model_tab_ShopClient_Agent_Level.AgentLevelName;
                        }
                        else
                        {
                            e.Row.Cells[9].Text = "分销代理";
                        }
                    }
                    else
                    {
                        e.Row.Cells[9].Text = "分销代理";
                    }
                    e.Row.Cells[10].Text = "<a href=\"Agent_Manage.aspx?type=Delete&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=Board_AgentChecked.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\" onclick=\"return confirm('确定删除授权吗?')\">删除</a>";
                    e.Row.Cells[10].Text += "&nbsp;&nbsp;<a href=\"AgentLevel_Select_Manage.aspx?type=Modify&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=Board_AgentChecked.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">操作</a>";
                    e.Row.Cells[10].Text += "<br /><br /><a href=\"Agent_ModifyParent.aspx?type=Modify&UserID=" + e.Row.Cells[0].Text + "&CallBackUrl=Board_AgentChecked.aspx*PageIndex=" + ViewState["PageIndex"].ToString() + "\">修改上级</a>";



                    Decimal myCountMoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney);
                    e.Row.Cells[11].Text = "<a href=\"../09System_Status/UserStatus_Money.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney) + "</a>";


                    Decimal myCountMoney_Vouchers = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(e.Row.Cells[0].Text), out myCountMoney_Vouchers);
                    e.Row.Cells[12].Text = "<a href=\"../09System_Status/UserStatus_Quan.aspx?userid=" + e.Row.Cells[0].Text + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "</a>";

                    var myvar = bll_tab_ShopClient_Agent_.GetModel(e.Row.Cells[13].Text.toInt32());
                    if (myvar != null)
                    {
                        e.Row.Cells[13].Text = Eggsoft_Public_CL.Pub.GetNickName(myvar.UserID.toString());
                    }
                    else {
                        e.Row.Cells[13].Text = "无上级";
                    }
                }
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
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
            try
            {


                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strWhere = " ";
                bool boolEveryOneAutoAgentOnlyIsAngel = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "EveryOneAutoAgentOnlyIsAngel");
                if (boolEveryOneAutoAgentOnlyIsAngel == true)
                {
                    strWhere += " and OnlyIsAngel=0";////1  表示还是天使
                }



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

                ViewState["searchWhere"] = " and ShopClientID=" + strShopClientID + strWhere;
                string strSearchWhere = ViewState["searchWhere"].ToString();
                ViewState["RecordCount"] = bll_View_ShopClient_Agent.ExistsCount(strSearchWhere);


                ViewState["PageIndex"] = 1;
                BindAnnounce();
                ShowState();
                InitGoPage();
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }



        protected void lbtnLast_Click_DownLoad(object sender, EventArgs e)
        {
            try
            {
                #region Excel 表格下载
                string strTablename = "[Sheet1$]";
                Eggsoft.Common.debug_Log.Call_WriteLog(strTablename, "代理商表格下载", "数据记录sheet1");

                string strFromfilePath = "ExcelDocument/BaseDocument/AgentDownload.xlsx";
                string FromfilePath = HttpContext.Current.Server.MapPath(strFromfilePath);

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strDownFileName = "AgentDownload" + Eggsoft.Common.StringNum.Add000000Num(strShopClientID.toInt32(), 5) + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ".xlsx";
                string WillDowmFile = "ExcelDocument/DownLoadDocument/" + strDownFileName;
                string AccessfilePath = HttpContext.Current.Server.MapPath(WillDowmFile);

                Eggsoft.Common.debug_Log.Call_WriteLog(AccessfilePath, "代理商表格下载", "数据记录sheet1");


                System.IO.File.Copy(FromfilePath, AccessfilePath, true);////去掉只读属性
                System.IO.File.SetAttributes(AccessfilePath, System.IO.FileAttributes.Normal);
                string connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AccessfilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=0'";//记录连接Access的语句   
                System.Data.OleDb.OleDbConnection tem_conn = new System.Data.OleDb.OleDbConnection(connstr);//连接Access数据库   
                System.Data.OleDb.OleDbCommand tem_comm;//定义OleDbCommand类   
                tem_conn.Open();//打开连接的Access数据库   
                string SQLtem_sql = "";// "select Count(*) From " + strTablename;//设置SQL语句，获取记录个数   
                tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn);//实例化OleDbCommand类   
                                                                                    //int RecordCount = (int)tem_comm.ExecuteScalar();//执行SQL语句，并返回结果   

                BindAnnounce();
                System.Data.DataTable Data_DataTable = (System.Data.DataTable)gvAnnounce.DataSource;

                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                {
                    SQLtem_sql = "";
                    SQLtem_sql += "update  " + strTablename + " set ";
                    SQLtem_sql += "F0='" + Data_DataTable.Rows[i]["ShopUserID"] + "',";//用户编号
                    SQLtem_sql += "F1='" + Data_DataTable.Rows[i]["ShopName"] + "',";//店铺名称
                    SQLtem_sql += "F2='" + Data_DataTable.Rows[i]["UserRealName"] + "',";//联系人
                    SQLtem_sql += "F3='" + Data_DataTable.Rows[i]["NickName"] + "',";//昵称
                    SQLtem_sql += "F4='" + Data_DataTable.Rows[i]["ContactPhone"] + "',";//联系电话
                    SQLtem_sql += "F5='" + Data_DataTable.Rows[i]["AlipayNumOrWeiXinPay"] + "',";//支付账号
                    SQLtem_sql += "F6='" + Data_DataTable.Rows[i]["UpdateTime"] + "',";//更新日期

                    #region 是否授权
                    SQLtem_sql += "F7='" + Data_DataTable.Rows[i]["Empowered"] + "',";//是否授权
                    #endregion 是否授权
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
                    SQLtem_sql += "F8='" + strTitle + "',";//代理级别
                    #endregion 代理级别
                    #region 现金￥
                    string strUserID = Data_DataTable.Rows[i]["UserID"].toString();
                    Decimal myCountMoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(strUserID.toInt32(), out myCountMoney);
                    SQLtem_sql += "F9='" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney) + "',";//现金￥
                    #endregion 现金￥

                    #region 购物券￥
                    Decimal myCountMoney_Vouchers = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(strUserID.toInt32(), out myCountMoney_Vouchers);
                    SQLtem_sql += "F10='" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "',";//购物券￥
                    #endregion 购物券￥


                    #region 上级代理商￥
                    string strParentIDName = Eggsoft_Public_CL.Pub.GetNickName(Data_DataTable.Rows[i]["ParentID"].toString());
                    SQLtem_sql += "F11='" + strParentIDName + "'";//
                    #endregion 上级代理商

                    SQLtem_sql += " where E0='E" + (i + 1).toString() + "';";
                    Eggsoft.Common.debug_Log.Call_WriteLog(SQLtem_sql, "代理商表格下载", "数据记录sql");
                    tem_comm.CommandText = SQLtem_sql;
                    tem_comm.ExecuteNonQuery();
                }
                Eggsoft.Common.debug_Log.Call_WriteLog(SQLtem_sql, "代理商表格下载", "数据记录sql");
                //tem_comm = new System.Data.OleDb.OleDbCommand(SQLtem_sql, tem_conn);//实例化OleDbCommand类   
                //RecordCount = (int)tem_comm.ExecuteScalar();//执行SQL语句，并返回结果   

                tem_conn.Close();


                #endregion Excel 表格下载


                #region 流方式下载 

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

            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "代理商表格下载");
            }
        }
    }
}
